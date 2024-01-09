using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using CRM.Data;
using System.Transactions;
using System.IO;


namespace CRM.Application.Views
{
    public partial class PostForm : Local.PopupWindow
    {
        #region Properties and Fields

        private int _ID = 0;
        private int CityID = 0;
        private CRM.Application.UserControls.GetFileUserControl _getFileUserControl { get; set; }

        #endregion

        #region Constructors

        public PostForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PostForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _getFileUserControl = new UserControls.GetFileUserControl();
            FileInfoGroupBox.Content = _getFileUserControl;
            FileInfoGroupBox.DataContext = _getFileUserControl;

            PostTypeComboBox.ItemsSource = Data.PostTypeDB.GetPostTypeCheckable();
            List<EnumItem> postContactStatus = Helper.GetEnumItem(typeof(DB.PostContactStatus));
            postContactStatus.RemoveAll(t => t.ID == (int)DB.PostContactStatus.Deleted || t.ID == (int)DB.PostContactStatus.NoCableConnection);
            StatusPostContactComboBox.ItemsSource = postContactStatus;

            StatusPostComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PostStatus));
            AorBComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.AORBPostAndCabinet));
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void LoadData()
        {
            Post item = new Post();
            item.AorBType = (byte)DB.AORBPostAndCabinet.AORB;

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
                item.Capacity = 10;
            }
            else
            {
                item = Data.PostDB.GetPostByID(_ID);
                _getFileUserControl.FileGuid = item.DocumentFileID ?? Guid.Empty;
                _getFileUserControl.DefaultFileName = item.Number.ToString();
                CenterComboBox.SelectedValue = Data.CenterDB.GetCenterIDByPostID(item.ID);
                CenterComboBox_SelectionChanged_1(null, null);
                CabinetComboBox.SelectedValue = item.CabinetID;
                CabinetComboBox_SelectionChanged(null, null);
                FromPostContactLabel.IsEnabled = false;
                ToPostContactLabel.IsEnabled = false;
                FromPostContact.IsEnabled = false;
                ToPostContact.IsEnabled = false;
                CapacityLable.IsEnabled = false;
                CapacityTextBox.IsEnabled = false;
                //FromPostTextBox.Visibility = Visibility.Collapsed;
                //FromPostLabel.Visibility = Visibility.Collapsed;
                FromPostLabel.Content = "شماره پست";
                FromPostTextBox.Text = item.Number.ToString();
                ToPostTextBox.Visibility = Visibility.Collapsed;
                ToPostLabel.Visibility = Visibility.Collapsed;
                StatusPostContactComboBox.Visibility = Visibility.Collapsed;
                StatusPostContactLabel.Visibility = Visibility.Collapsed;
                CityID = Data.PostDB.GetCity(item.ID);

                List<AdjacentPostList> adjacentPostList = Data.PostDB.GetAdjacentPostList(_ID);
                AdjacentDataGrid.ItemsSource = adjacentPostList;

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
            if (CityID == 0)
            {
                CityComboBox.SelectedIndex = 0;
                CenterComboBox.SelectedIndex = 0;
            }
            else
                CityComboBox.SelectedValue = CityID;

            AorBComboBox_SelectionChanged_1(null, null);
            //CapacityTextBox_TextChanged_1(null, null);

        }

        #endregion

        #region EventHandlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    Post item = this.DataContext as Post;
                    PostType postType = Data.PostTypeDB.GetPostTypeByID(item.PostTypeID);

                    if (
                        postType != null
                        &&
                        postType.AorBable == false
                        &&
                        ((byte)DB.AORBPostAndCabinet.A == item.AorBType || (byte)DB.AORBPostAndCabinet.B == item.AorBType)
                       )
                    {
                        MessageBox.Show("نوع پست قابلیت A یا B ندارد!", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    if (_getFileUserControl.AppendixFilePath != null)
                    {
                        if (File.Exists(_getFileUserControl.AppendixFilePath))
                        {
                            if (item.DocumentFileID != null)
                            {
                                Data.DocumentsFileDB.DeleteDocumentsFileTable(item.DocumentFileID ?? Guid.Empty);
                            }
                            item.DocumentFileID = Data.DocumentsFileDB.SaveFileInDocumentsFileWithFilePathOnClinet(_getFileUserControl.AppendixFilePath);
                        }
                        else
                        {
                            MessageBox.Show("فایل وجود نداشت. اطلاعات مربوط به فایل ذخیره نشد.", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }

                    if (_ID == 0)
                    {
                        Cabinet cabinet = Data.CabinetDB.GetCabinetByID(item.CabinetID);
                        // check post limition
                        if (cabinet.IsLimitPost)
                        {
                            int currentPostNumber = PostDB.GetPostCountByCabinetID(cabinet.ID);

                            if ((currentPostNumber + 1) > cabinet.MaxNumberPost)
                            {
                                throw new Exception("حداکثر " + currentPostNumber.ToString() + " پست برروی این کافو می توان ایجاد کرد ");
                            }
                        }

                        cabinet.LastTimeAddFacility = DB.GetServerDate();
                        cabinet.Detach();
                        DB.Save(cabinet, false);

                        int fromPost = Convert.ToInt32(FromPostTextBox.Text.Trim() != string.Empty ? FromPostTextBox.Text.Trim() : "0");
                        int toPost = Convert.ToInt32(ToPostTextBox.Text.Trim() != string.Empty ? ToPostTextBox.Text.Trim() : FromPostTextBox.Text.Trim());

                        for (int j = fromPost; j <= toPost; j++)
                        {
                            item.ID = 0;
                            if (item.IsOutBorder == false)
                            {
                                item.OutBorderMeter = null;
                            }
                            item.Number = j;
                            item.Detach();
                            Save(item, true);

                            int toPostContact = Convert.ToInt32(ToPostContact.Text.Trim());
                            int fromPostContact = Convert.ToInt32(FromPostContact.Text.Trim());
                            List<PostContact> postContactlist = new List<PostContact>();

                            if (fromPostContact <= toPostContact)
                            {
                                for (int i = fromPostContact; i <= toPostContact; i++)
                                {
                                    PostContact postContact = new PostContact();
                                    postContact.PostID = item.ID;
                                    postContact.ConnectionType = (byte)DB.PostContactConnectionType.Noraml;
                                    postContact.ConnectionNo = i;
                                    postContact.Status = (byte)StatusPostContactComboBox.SelectedValue;
                                    postContactlist.Add(postContact);
                                }
                            }
                            DB.SaveAll(postContactlist);
                        }
                    }
                    else
                    {

                        if (item.Status == (byte)DB.PostStatus.Broken)
                        {
                            //TODO:rad 13950609
                            //در صورتی که کاربر قصد غیر فعال کردن پست را داشته باشد متد های زیر وضعیت اتصالی پست های این پست را بررسی میکنند
                            bool hasConnectedPostContact = PostDB.HasConnectedPostContact(item.ID);
                            bool hasReservedPostContact = PostDB.HasReservedPostContact(item.ID);

                            string errorMessage = string.Empty;

                            if (hasConnectedPostContact)
                            {
                                errorMessage = ".این پست دارای اتصالی پست متصل میباشد لذا امکان غیرفعال کردن آن وجود ندارد";
                            }

                            if (hasReservedPostContact)
                            {
                                errorMessage = ".این پست دارای اتصالی پست رزروشده، میباشد لذا امکان غیرفعال کردن آن وجود ندارد";
                            }

                            if (hasReservedPostContact || hasConnectedPostContact)
                            {
                                MessageBox.Show(errorMessage, "توجّه", MessageBoxButton.OK, MessageBoxImage.Error);
                                StatusPostComboBox.Focus();
                                StatusPostComboBox.IsDropDownOpen = true;
                                return;
                            }
                        }

                        item.Number = Convert.ToInt32(FromPostTextBox.Text.Trim());
                        if (item.IsOutBorder == false)
                        {
                            item.OutBorderMeter = null;
                        }
                        item.Detach();
                        DB.Save(item, false);
                    }
                    scope.Complete();
                    ShowSuccessMessage("پست ذخیره شد");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                {
                    ShowErrorMessage("نمی توان دو پست هم شماره در یک کافو وارد کرد .خطا در ذخیره پست", ex);
                }
                else
                {
                    ShowErrorMessage("خطا در ذخیره پست", ex);
                }
            }
        }

        private void CapacityTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            FromPostContact.Text = "1";
            (this.DataContext as Post).FromPostContact = 1;

            int Capacity = -1;
            if (!int.TryParse(CapacityTextBox.Text.Trim(), out Capacity))
            {
                Capacity = -1;
            };

            (this.DataContext as Post).ToPostContact = Capacity;
        }

        private void AorBComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (AorBComboBox.SelectedValue != null)
            {
                if (
                    (byte)AorBComboBox.SelectedValue == (byte)DB.AORBPostAndCabinet.A
                    ||
                    (byte)AorBComboBox.SelectedValue == (byte)DB.AORBPostAndCabinet.B
                   )
                {
                    (this.DataContext as Post).Capacity = 5;
                }
                else if ((byte)AorBComboBox.SelectedValue == (byte)DB.AORBPostAndCabinet.AORB)
                {
                    (this.DataContext as Post).Capacity = 10;

                }
            }
        }

        private void CenterComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID((int)CenterComboBox.SelectedValue);
                PostGroupComboBox.ItemsSource = Data.PostGroupDB.GetPostGroupAndCountPostsOfPostGroupCheckableByCenter((int)CenterComboBox.SelectedValue);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
            }
            else
            {
                if (CityComboBox.SelectedValue == null)
                {
                    City city = Data.CityDB.GetCityById(CityID);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
                else
                {
                    City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
            }
        }

        private void CabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CabinetComboBox.SelectedValue != null)
            {
                AdjacentPost.ItemsSource = Data.PostDB.GetPostCheckableByCabinetIDWithAB((int)CabinetComboBox.SelectedValue);
            }
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AdjacentDataGrid.SelectedItem != null)
                {
                    CRM.Data.AdjacentPostList adjacentPost = AdjacentDataGrid.SelectedItem as AdjacentPostList;
                    //if (adjacentPost != null)
                    //DB.Delete<CRM.Data.AdjacentPost>(adjacentPost.ID);

                    List<CRM.Data.AdjacentPost> allAdjacentPosts = Data.PostDB.GetAllAdjacentPost(adjacentPost.AdjacentPostID);
                    allAdjacentPosts.ForEach(t =>
                    {
                        CRM.Data.AdjacentPost otherAdjacentPosts = Data.PostDB.GetAdjacentPostWithAdjacentPostID(t.AdjacentPostID, t.PostID);
                        if (otherAdjacentPosts != null)
                            PostDB.DeleteAdjacentPostByID(otherAdjacentPosts.ID);
                        PostDB.DeleteAdjacentPostByID(t.ID);
                        //DB.Delete<CRM.Data.AdjacentPost>(otherAdjacentPosts.ID);
                        //DB.Delete<CRM.Data.AdjacentPost>(t.ID);
                    });

                    AdjacentPost item = Data.PostDB.GetAdjacentPostByID(adjacentPost.ID);
                    if (item != null)
                    {
                        PostDB.DeleteAdjacentPostByID(item.ID);
                    }

                }

                List<AdjacentPostList> adjacentPostList = Data.PostDB.GetAdjacentPostList(_ID);
                AdjacentDataGrid.ItemsSource = adjacentPostList;
            }
            catch
            {
                Folder.MessageBox.ShowError("خطا در حذف اطلاعات");
            }

        }

        private void AdjacentDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (AdjacentDataGrid.SelectedItem != null)
            {
                int adjacentPostID = (AdjacentDataGrid.SelectedItem as AdjacentPostList).AdjacentPostID;
                if (_ID != adjacentPostID && !Data.PostDB.CheckIsAdjacentPost(_ID, adjacentPostID))
                {

                    CRM.Data.AdjacentPost adjacentPost = new AdjacentPost();
                    adjacentPost.ID = 0;
                    adjacentPost.PostID = _ID;
                    adjacentPost.AdjacentPostID = adjacentPostID;
                    adjacentPost.Detach();
                    DB.Save(adjacentPost);

                    CRM.Data.AdjacentPost secondAdjacentPost = new AdjacentPost();
                    secondAdjacentPost.ID = 0;
                    secondAdjacentPost.PostID = (AdjacentDataGrid.SelectedItem as AdjacentPostList).AdjacentPostID;
                    secondAdjacentPost.AdjacentPostID = _ID;
                    secondAdjacentPost.Detach();
                    DB.Save(secondAdjacentPost);

                    List<AdjacentPostList> adjacentPostList = Data.PostDB.GetAdjacentPostList(_ID);
                    adjacentPostList.ForEach(t =>
                    {
                        if (!Data.PostDB.CheckIsAdjacentPost(adjacentPostID, t.AdjacentPostID) && adjacentPostID != t.AdjacentPostID)
                        {
                            CRM.Data.AdjacentPost otherAdjacentPost = new AdjacentPost();
                            otherAdjacentPost.ID = 0;
                            otherAdjacentPost.PostID = t.AdjacentPostID;
                            otherAdjacentPost.AdjacentPostID = adjacentPostID;
                            otherAdjacentPost.Detach();
                            DB.Save(otherAdjacentPost);

                            CRM.Data.AdjacentPost secondotherAdjacentPost = new AdjacentPost();
                            secondotherAdjacentPost.ID = 0;
                            secondotherAdjacentPost.PostID = adjacentPostID;
                            secondotherAdjacentPost.AdjacentPostID = t.AdjacentPostID;
                            secondotherAdjacentPost.Detach();
                            DB.Save(secondotherAdjacentPost);
                        }
                    });
                }
                else
                {
                    MessageBox.Show("خطا در اطلاعات انتخاب شده", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion

    }
}
