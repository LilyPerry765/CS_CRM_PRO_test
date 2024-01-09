using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using CRM.Application.Codes;
using System.Data;
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class PostList : Local.TabWindow
    {
        #region Constructor & Fields
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;
        public PostList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Methods

        private void Initialize()
        {
            //CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckable();
            PostTypeComboBox.ItemsSource = Data.PostTypeDB.GetPostTypeCheckable();
            //  PostGroupComboBox.ItemsSource = Data.PostGroupDB.GetPostGroupCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PostStatus));
            AorBTypeCheckableComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.AORBPostAndCabinet));

            ActiveConditionComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Condition));
            FreeConditionComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Condition));
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // LoadData();
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
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

            int capacity = -1;
            if (!string.IsNullOrWhiteSpace(CapacityTextBox.Text)) capacity = Convert.ToInt32(CapacityTextBox.Text);

            int distance = -1;
            if (!string.IsNullOrWhiteSpace(DistanceTextBox.Text)) distance = Convert.ToInt32(DistanceTextBox.Text);

            int outBorderMeter = -1;
            if (!string.IsNullOrWhiteSpace(OutBorderMeterTextBox.Text)) outBorderMeter = Convert.ToInt32(OutBorderMeterTextBox.Text);

            int postalCode = -1;
            if (!string.IsNullOrWhiteSpace(PostalCodeTextBox.Text)) outBorderMeter = Convert.ToInt32(PostalCodeTextBox.Text);


            int activePostContactCount = 0;
            int.TryParse(ActivePostContactCountTextBox.Text.Trim(), out activePostContactCount);

            int freePostContactCount = 0;
            int.TryParse(FreePostContactCountTextBox.Text.Trim(), out freePostContactCount);

            //string activeConditionString = string.Empty;
            //if (ActiveConditionComboBox.SelectedValue != null && !string.IsNullOrEmpty(ActivePostContactCountTextBox.Text.Trim()))
            //{
            //    int activePostContactCount = 0;
            //    int.TryParse(ActivePostContactCountTextBox.Text.Trim(), out activePostContactCount);
            //    if(activePostContactCount != 0)
            //    {
            //        activeConditionString = DB.GetConditionString((int)ActiveConditionComboBox.SelectedValue, activePostContactCount, "ActivePostContactCount");
            //    }
            //}


            //string freeConditionString = string.Empty;
            //if (FreeConditionComboBox.SelectedValue != null && !string.IsNullOrEmpty(FreePostContactCountTextBox.Text.Trim()))
            //{
            //    int freePostContactCount = 0;
            //    int.TryParse(FreePostContactCountTextBox.Text.Trim(), out freePostContactCount);
            //    if (freePostContactCount != 0)
            //    {
            //        freeConditionString = DB.GetConditionString((int)FreeConditionComboBox.SelectedValue, freePostContactCount, "FreePostContactCount");
            //    }
            //}



            //ComparisonByByPropertyName

            int count = 0;
            //ItemsDataGrid.ItemsSource = Data.PostDB.SearchPost(
            //                           CityComboBox.SelectedIDs,
            //                           CenterComboBox.SelectedIDs,
            //                           CabinetComboBox.SelectedIDs,
            //                           PostComboBox.SelectedIDs,
            //                           PostTypeComboBox.SelectedIDs,
            //                           PostGroupComboBox.SelectedIDs,
            //                           StatusComboBox.SelectedIDs,
            //                           capacity,
            //                           distance,
            //                           outBorderMeter,
            //                           postalCode,
            //                           AorBTypeCheckableComboBox.SelectedIDs,
            //                           AddressTextBox.Text.Trim(),
            //                           OutBoundMeterCheckBox.IsChecked,
            //                           ActiveConditionComboBox.SelectedValue , activeConditionString,
            //                           freeConditionString,
            //                           startRowIndex,
            //                           pageSize,
            //                           out count
            //                          );


            ItemsDataGrid.ItemsSource = Data.PostDB.SearchPost(
                           CityComboBox.SelectedIDs,
                           CenterComboBox.SelectedIDs,
                           CabinetComboBox.SelectedIDs,
                           PostComboBox.SelectedIDs,
                           PostTypeComboBox.SelectedIDs,
                           StatusComboBox.SelectedIDs,
                           capacity,
                           distance,
                           outBorderMeter,
                           postalCode,
                           AorBTypeCheckableComboBox.SelectedIDs,
                           AddressTextBox.Text.Trim(),
                           OutBoundMeterCheckBox.IsChecked,
                           (int?)ActiveConditionComboBox.SelectedValue,
                           activePostContactCount,
                           (int?)FreeConditionComboBox.SelectedValue,
                           freePostContactCount,
                           startRowIndex,
                           pageSize,
                           out count
                          );

            Pager.TotalRecords = count;

            this.Cursor = Cursors.Arrow;
        }


        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;


            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;

            int capacity = -1;
            if (!string.IsNullOrWhiteSpace(CapacityTextBox.Text)) capacity = Convert.ToInt32(CapacityTextBox.Text);

            int distance = -1;
            if (!string.IsNullOrWhiteSpace(DistanceTextBox.Text)) distance = Convert.ToInt32(DistanceTextBox.Text);

            int outBorderMeter = -1;
            if (!string.IsNullOrWhiteSpace(OutBorderMeterTextBox.Text)) outBorderMeter = Convert.ToInt32(OutBorderMeterTextBox.Text);

            int postalCode = -1;
            if (!string.IsNullOrWhiteSpace(PostalCodeTextBox.Text)) outBorderMeter = Convert.ToInt32(PostalCodeTextBox.Text);

            int activePostContactCount = 0;
            int.TryParse(ActivePostContactCountTextBox.Text.Trim(), out activePostContactCount);

            int freePostContactCount = 0;
            int.TryParse(FreePostContactCountTextBox.Text.Trim(), out freePostContactCount);

            int count = 0;
            DataSet data = Data.PostDB.SearchPost(
                                       CityComboBox.SelectedIDs,
                                       CenterComboBox.SelectedIDs,
                                       CabinetComboBox.SelectedIDs,
                                       PostComboBox.SelectedIDs,
                                       PostTypeComboBox.SelectedIDs,
                                       StatusComboBox.SelectedIDs,
                                       capacity,
                                       distance,
                                       outBorderMeter,
                                       postalCode,
                                       AorBTypeCheckableComboBox.SelectedIDs,
                                       AddressTextBox.Text.Trim(),
                                       OutBoundMeterCheckBox.IsChecked,
                                       (int?)ActiveConditionComboBox.SelectedValue,
                                       activePostContactCount,
                                       (int?)FreeConditionComboBox.SelectedValue,
                                       freePostContactCount,
                                       startRowIndex,
                                       pageSize,
                                       out count).ToDataSet("Result", ItemsDataGrid);
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
                MessageBoxResult result = MessageBox.Show("با حذف پست، اتصالی های ازاد نیز حذف می شوند. آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {

                    PostInfo item = ItemsDataGrid.SelectedItem as PostInfo;

                    List<PostContact> PostContacts = Data.PostContactDB.GetPostContactByPostID(item.ID);

                    string error = string.Empty;


                    if (PostContacts.Any(t => !(t.Status == (int)DB.PostContactStatus.Free || t.Status == (int)DB.PostContactStatus.PermanentBroken)))
                        error += "برای حذف پست باید همه اتصالی های آن آزاد باشد.";

                    if (PostContacts.Any(t => t.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal))
                        error += "برای حذف پست ابتدا پی سی ام های ان را حذف کنید.";

                    if (!string.IsNullOrEmpty(error))
                    {
                        Folder.MessageBox.ShowError(error);
                        return;
                    }

                    using (TransactionScope ts = new TransactionScope())
                    {
                        PostContacts.ForEach(t => { t.Status = (byte)DB.PostContactStatus.Deleted; t.Detach(); });
                        DB.UpdateAll(PostContacts);

                        Post post = PostDB.GetPostByID(item.ID);
                        post.IsDelete = true;
                        post.Detach();
                        DB.Save(post);


                        List<AdjacentPostList> adjacentPostList = Data.PostDB.GetAllAdjacentPostList(post.ID);
                        adjacentPostList.ForEach(t =>
                        {
                            DB.Delete<CRM.Data.AdjacentPost>(t.ID);
                        });

                        ts.Complete();
                    }

                    ShowSuccessMessage("پست مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف پست", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PostInfo item = ItemsDataGrid.SelectedItem as Data.PostInfo;
                if (item == null) return;

                PostForm window = new PostForm((int)item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //try
            //{
            //    Data.PostInfo item = e.Row.Item as Data.PostInfo;

            //    item.Detach();
            //    DB.Save(item);

            //    Search(null, null);

            //    ShowSuccessMessage("پست مورد نظر ذخیره شد");
            //}
            //catch (Exception ex)
            //{
            //    e.Cancel = true;
            //    ShowErrorMessage("خطا در ذخیره پست", ex);
            //}
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            PostForm window = new PostForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
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

    }
}
