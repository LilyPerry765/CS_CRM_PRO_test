using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.Data;
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    public partial class PostContactList : Local.TabWindow
    {
        #region Constructor & Fields
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        public PostContactList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            List<CheckableItem> postContactStatus = Helper.GetEnumCheckable(typeof(DB.PostContactStatus));
            postContactStatus.RemoveAll(t => t.ID == (int)DB.PostContactStatus.Deleted || t.ID == (int)DB.PostContactStatus.NoCableConnection);
            StatusComboBox.ItemsSource = postContactStatus;

            ConnectionTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PostContactConnectionType));
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            PCMTypeComboBox.ItemsSource = Data.PCMTypeDB.GetPCMTypeCheckable();
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        public void LoadData()
        {
            //Search(null, null);
        }
        #endregion Load Methods

        #region Event Handlers
        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //  LoadData();
        }


        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            int connectionNo = -1;
            if (!int.TryParse(ConnectionNoTextBox.Text.Trim(), out connectionNo)) { connectionNo = -1; };

            long telephoneNo = -1;
            if (!long.TryParse(TelephoneNoTextBox.Text.Trim(), out telephoneNo)) { telephoneNo = -1; };

            long requestID = -1;
            if (!long.TryParse(RequestIDTextBox.Text.Trim(), out requestID)) { requestID = -1; };

            int count = default(int);

            ItemsDataGrid.ItemsSource = Data.PostContactDB.SearchPostContact(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CabinetComboBox.SelectedIDs, PostComboBox.SelectedIDs, ConnectionTypeComboBox.SelectedIDs, StatusComboBox.SelectedIDs, connectionNo, PCMTypeComboBox.SelectedIDs, startRowIndex, pageSize, telephoneNo, requestID, out count);
            Pager.TotalRecords = count;
            this.Cursor = Cursors.Arrow;
        }
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;


            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;

            int connectionNo = -1;
            if (!int.TryParse(ConnectionNoTextBox.Text.Trim(), out connectionNo)) { connectionNo = -1; };

            long telephoneNo = -1;
            if (!long.TryParse(TelephoneNoTextBox.Text.Trim(), out telephoneNo)) { telephoneNo = -1; };

            long requestID = -1;
            if (!long.TryParse(RequestIDTextBox.Text.Trim(), out requestID)) { requestID = -1; };
            int count = default(int);
            DataSet data = Data.PostContactDB.SearchPostContact(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CabinetComboBox.SelectedIDs, PostComboBox.SelectedIDs, ConnectionTypeComboBox.SelectedIDs, StatusComboBox.SelectedIDs, connectionNo, PCMTypeComboBox.SelectedIDs, startRowIndex, pageSize, telephoneNo, requestID, out count).ToDataSet("Result", ItemsDataGrid);
            Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);


            this.Cursor = Cursors.Arrow;
        }
        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ItemsDataGrid.Columns);
            ReportSettingForm reportSettingForm = new ReportSettingForm(dataGridColumn);
            reportSettingForm._title = _title;
            reportSettingForm._checkedList.Clear();
            reportSettingForm._checkedList = _groupingColumn;
            reportSettingForm._sumCheckedList = _sumColumn;
            reportSettingForm.ShowDialog();
            _sumColumn = reportSettingForm._sumCheckedList;
            _groupingColumn = reportSettingForm._checkedList;
            _title = reportSettingForm._title;

        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PostContactInfo item = ItemsDataGrid.SelectedItem as PostContactInfo;

                    DB.Delete<Data.PostContact>(item.ID);
                    ShowSuccessMessage("اتصال پست مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف اتصال پست", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PostContactInfo item = ItemsDataGrid.SelectedItem as Data.PostContactInfo;
                if (item == null) return;

                PostContactForm window = new PostContactForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //try
            //{
            //    Data.PostContact item = e.Row.Item as Data.PostContact;
            //  
            //    item.Detach();
            //    DB.Save(item);

            //    Search(null, null);

            //    ShowSuccessMessage("اتصال پست مورد نظر ذخیره شد");
            //}
            //catch (Exception ex)
            //{
            //    e.Cancel = true;
            //    ShowErrorMessage("خطا در ذخیره اتصال پست", ex);
            //}

        }

        private void MalfactionClick(object sender, RoutedEventArgs e)
        {
            //milad doran
            //if (ItemsDataGrid.SelectedItem != null)
            //{
            //    MalfuctionForm MalfuctionFormWindow = new MalfuctionForm((ItemsDataGrid.SelectedItem as PostContactInfo).ID, (Byte)DB.MalfuctionType.PostConntact);
            //    MalfuctionFormWindow.Title = "خراب / اصلاح اتصال پست";
            //    MalfuctionFormWindow.ShowDialog();

            //    TabWindow_Loaded(null, null);
            //}

            //TODO:rad 13950609
            if (ItemsDataGrid.SelectedItem != null)
            {
                PostContactInfo selectedItem = ItemsDataGrid.SelectedItem as PostContactInfo;
                byte? selectedPostContactStatus = PostContactDB.GetStatusByCheckConnectingToBucht(selectedItem.ID);

                if (selectedPostContactStatus.HasValue && selectedPostContactStatus == (byte)DB.PostContactStatus.CableConnection)
                {
                    MessageBox.Show(".این اتصال پست به تلفن متصل است و امکان خراب/اصلاح ندارد", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (selectedItem.Status == (byte)DB.PostContactStatus.FullBooking)
                {
                    MessageBox.Show(".این اتصال پست هم اکنون رزرو است و امکان خراب/اصلاح ندارد", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (selectedPostContactStatus.HasValue && selectedPostContactStatus != (byte)DB.PostContactStatus.CableConnection) //اگر اتصالی پست انتخاب شده به  مشترک متصل نباشد و همچنین رزرو هم نباشد، امکان اعلام خراب/اصلاح برای آن وجود دارد
                {
                    MalfuctionForm MalfuctionFormWindow = new MalfuctionForm(selectedItem.ID, (Byte)DB.MalfuctionType.PostConntact);
                    MalfuctionFormWindow.Title = "خراب / اصلاح اتصال پست";
                    MalfuctionFormWindow.ShowDialog();

                    TabWindow_Loaded(null, null);
                }
                else
                {
                    return;
                }
            }
        }


        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }
        private void CabinetComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            PostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID(CabinetComboBox.SelectedIDs);
        }


        #endregion Event Handlers

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ItemsDataGrid.SelectedItem != null)
                {
                    CRM.Data.PostContactInfo item = ItemsDataGrid.SelectedItem as CRM.Data.PostContactInfo;

                    if (
                         (item.Status == (byte)DB.PostContactStatus.Free || item.Status == (byte)DB.PostContactStatus.CableConnection) 
                         && 
                         (item.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal) 
                         || 
                         (item.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal)
                       )
                    {

                        CRM.Data.AssignmentInfo assignmentInfo = DB.GetAllInformationPostContactIDWithOutBuchtType(item.ID);
                        if (assignmentInfo.PostContactStatus == (byte)DB.PostContactStatus.CableConnection)
                        {
                            if (CRM.Data.ADSLPAPPortDB.HasADSLbyTelephoneNo((long)assignmentInfo.TelePhoneNo))
                                throw new Exception("امکان نسب تلفن روی تلفن ADSL  نمی باشد.");
                        }
                        Cabinet cabinet = Data.CabinetDB.GetCabinetByPostID((int)assignmentInfo.PostID);
                        if (cabinet != null)
                        {
                            assignmentInfo.CabinetID = cabinet.ID;
                            assignmentInfo.CenterID = cabinet.CenterID;
                        }
                        PCMAssignment window = new PCMAssignment(assignmentInfo, assignmentInfo.CenterID);
                        window.ShowDialog();
                        if (window.DialogResult == true)
                            LoadData();
                    }
                    else
                    {
                        Folder.MessageBox.ShowInfo("اتصالی انتخاب شده صحیح نیست");
                    }

                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در انتساب", ex);
            }
        }

        private void LeaveForAssignPCM(object sender, RoutedEventArgs e)
        {
            CRM.Data.PostContactInfo item = ItemsDataGrid.SelectedItem as CRM.Data.PostContactInfo;
            if (
                 (item.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote && item.Status == (int)DB.PostContactStatus.NoCableConnection)
                 ||
                 (item.ConnectionType == (int)DB.PostContactConnectionType.Noraml && item.Status == (int)DB.PostContactStatus.CableConnection)
               )
            {
                Bucht bucht = BuchtDB.GetBuchtByConnectionID(item.ID);
                if (bucht == null)
                {
                    PostContact postContact = PostContactDB.GetPostContactByID(item.ID);
                    postContact.ConnectionType = (int)DB.PostContactConnectionType.Noraml;
                    postContact.Status = (int)DB.PostContactStatus.Free;
                    postContact.Detach();
                    DB.Save(postContact);



                    List<PostContact> postContacts = PostContactDB.GetPostContactByPostIDAndConnectionNo(postContact.PostID, postContact.ConnectionNo)
                                                                  .Where(t => t.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal)
                                                                  .ToList();

                    List<Bucht> buchts = BuchtDB.GetBuchtByConnectionIDs(postContacts.Select(t => t.ID).ToList());
                    if (buchts.Count() == 0)
                    {
                        postContacts.ForEach(t => { t.Status = (int)DB.PostContactStatus.Deleted; t.Detach(); });
                        DB.UpdateAll(postContacts);
                    }

                }
                else if (
                            bucht.SwitchPortID == null
                            &&
                            bucht.PCMPortID == null
                            &&
                            bucht.Status != (int)DB.BuchtStatus.AllocatedToInlinePCM
                            &&
                            (bucht.BuchtTypeID != (int)DB.BuchtType.InLine || bucht.BuchtTypeID != (int)DB.BuchtType.OutLine)
                        )
                {
                    bucht.Status = (int)DB.BuchtStatus.Free;
                    bucht.ConnectionID = null;
                    bucht.Detach();
                    DB.Save(bucht);

                    PostContact postContact = PostContactDB.GetPostContactByID(item.ID);
                    postContact.ConnectionType = (int)DB.PostContactConnectionType.Noraml;
                    postContact.Status = (int)DB.PostContactStatus.Free;
                    postContact.Detach();
                    DB.Save(postContact);

                }
                else
                {
                    MessageBox.Show("اتصالی به بوخت متصل می باشد امکان آزاد سازی نمی باشد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("امکان آزاد سازی این اتصالی نمی باشد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

    }
}
