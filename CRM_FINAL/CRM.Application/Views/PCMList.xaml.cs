using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.Transactions;
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    public partial class PCMList : Local.TabWindow
    {
        #region Properties And Fields

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructors

        public PCMList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Load Methods
        private void Initialize()
        {
            PCMBrandColumn.ItemsSource = Data.PCMBrandDB.GetPCMBrandCheckable();
            PCMTypeColumn.ItemsSource = Data.PCMTypeDB.GetPCMTypeCheckable();

            PCMBrandComboBox.ItemsSource = Data.PCMBrandDB.GetPCMBrandCheckable();
            PCMTypeComboBox.ItemsSource = Data.PCMTypeDB.GetPCMTypeCheckable();

            //milad doran
            //StatusCheckableComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PCMStatus));

            //TODO:rad 13941219

            StatusCheckableComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PCMStatus));
            StatusColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.PCMStatus));

            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion Load Methods

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            int rock = -1;
            if (!int.TryParse(RockCheckableComboBox.Text.Trim(), out rock)) { rock = -1; };

            int shelf = -1;
            if (!int.TryParse(ShelfCheckableComboBox.Text.Trim(), out shelf)) { shelf = -1; };

            int card = -1;
            if (!int.TryParse(CardCheckableComboBox.Text.Trim(), out card)) { card = -1; };

            int count = 0;

            ItemsDataGrid.ItemsSource = Data.PCMDB.SearchPCM(
                                                             CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, rock,
                                                             shelf, card, PCMBrandComboBox.SelectedIDs, StatusCheckableComboBox.SelectedIDs,
                                                             PCMTypeComboBox.SelectedIDs, InstallAddressTextBox.Text.Trim(), InstallPostCodeTextBox.Text.Trim(),
                                                             startRowIndex, pageSize, InstallationDateFromDate.SelectedDate,
                                                             InstallationDateToDate.SelectedDate, out count
                                                           );

            Pager.TotalRecords = count;
            this.Cursor = Cursors.Arrow;
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("با حذف پی سی ام، پورت های ازاد نیز حذف می شوند. آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {

                    using (TransactionScope ts = new TransactionScope())
                    {
                        PCMCardInfo item = ItemsDataGrid.SelectedItem as CRM.Data.PCMCardInfo;
                        List<PCMPort> Ports = Data.PCMPortDB.GetAllPCMPortByPCMID(item.ID);

                        List<Bucht> oldbucht = new List<Bucht>();
                        List<Bucht> deleteBucht = Data.BuchtDB.getBuchtByPCMPortID(Ports.Select(t => t.ID).ToList());
                        if (deleteBucht.Any(t => t.SwitchPortID.HasValue)) { MessageBox.Show("حذف فقط برای پی سی ام های بدون مشترک امکان پذیر می باشد"); return; }

                        if (deleteBucht.Any(t => t.BuchtTypeID == (int)DB.BuchtType.OutLine && t.BuchtIDConnectedOtherBucht.HasValue))
                        {
                            Bucht CabinetInputBucht = BuchtDB.GetBuchetByID(deleteBucht.Where(t => t.BuchtTypeID == (int)DB.BuchtType.OutLine && t.BuchtIDConnectedOtherBucht.HasValue).SingleOrDefault().BuchtIDConnectedOtherBucht.Value);
                            CabinetInputBucht.Status = (int)DB.BuchtStatus.Free;
                            CabinetInputBucht.BuchtIDConnectedOtherBucht = null;
                            CabinetInputBucht.SwitchPortID = null;
                            CabinetInputBucht.ConnectionID = null;
                            CabinetInputBucht.PCMMainBuchtID = null;
                            CabinetInputBucht.Detach();
                            DB.Save(CabinetInputBucht);

                        }


                        //if (deleteBucht.Any(t => t.SwitchPortID.HasValue)) { MessageBox.Show("حذف فقط برای پی سی ام های بدون مشترک امکان پذیر می باشد"); return; }
                        List<PostContact> postContacts = PostContactDB.GetPostContactByIDs(deleteBucht.Where(t => t.ConnectionID.HasValue).Select(t => t.ConnectionID.Value).ToList());
                        foreach (PostContact obj in postContacts)
                        {
                            if (obj.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal)
                            {
                                obj.Status = (int)DB.PostContactStatus.Deleted;
                            }
                            else if (obj.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                            {
                                obj.ConnectionType = (int)DB.PostContactConnectionType.Noraml;
                                obj.Status = (int)DB.PostContactStatus.Free;
                            }
                            obj.Detach();
                        }

                        DB.UpdateAll(postContacts);

                        foreach (Bucht obj in deleteBucht)
                        {
                            //TODO:
                            obj.BuchtTypeID = (byte)DB.BuchtType.CustomerSide;
                            obj.BuchtIDConnectedOtherBucht = null;
                            obj.PCMPortID = null;
                            obj.CabinetInputID = null;
                            obj.PortNo = null;
                            obj.ConnectionID = null;
                            obj.Status = (byte)DB.BuchtStatus.Free;
                            obj.PCMMainBuchtID = null;
                            obj.Detach();
                            oldbucht.Add(obj);
                        }



                        DB.UpdateAll(oldbucht);

                        DB.Delete<Data.PCM>(item.ID);
                        ShowSuccessMessage("پی سی ام مورد نظر حذف شد");
                        ts.Complete();
                    }
                    Search(null, null);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف پی سی ام", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PCMCardInfo item = ItemsDataGrid.SelectedItem as Data.PCMCardInfo;
                if (item == null) return;

                PCMForm window = new PCMForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.PCM item = e.Row.Item as Data.PCM;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("پی سی ام مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره پی سی ام", ex);
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            PCMForm window = new PCMForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void MalfactionClick(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                MalfuctionForm MalfuctionFormWindow = new MalfuctionForm((ItemsDataGrid.SelectedItem as PCMCardInfo).ID, (Byte)DB.MalfuctionType.PCM);
                MalfuctionFormWindow.ShowDialog();

                TabWindow_Loaded(null, null);
            }
        }

        private void ModifyMUIDClick(object sender, RoutedEventArgs e)
        {
            PCMCardInfo item = ItemsDataGrid.SelectedItem as Data.PCMCardInfo;
            if (item == null) return;

            ModifyMUIDForm window = new ModifyMUIDForm(item);
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
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
            if (!CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns))
            {
                Folder.MessageBox.ShowError("خطا در ذخیره اطلاعات");
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;

            int rock = -1;
            if (!int.TryParse(RockCheckableComboBox.Text.Trim(), out rock)) { rock = -1; };

            int shelf = -1;
            if (!int.TryParse(ShelfCheckableComboBox.Text.Trim(), out shelf)) { shelf = -1; };

            int card = -1;
            if (!int.TryParse(CardCheckableComboBox.Text.Trim(), out card)) { card = -1; };

            int count = 0;

            System.Data.DataSet data = Data.PCMDB.SearchPCM(
                                                             CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, rock,
                                                             shelf, card, PCMBrandComboBox.SelectedIDs, StatusCheckableComboBox.SelectedIDs,
                                                             PCMTypeComboBox.SelectedIDs, InstallAddressTextBox.Text.Trim(), InstallPostCodeTextBox.Text.Trim(),
                                                             startRowIndex, pageSize, InstallationDateFromDate.SelectedDate,
                                                             InstallationDateToDate.SelectedDate, out count
                                                           ).ToDataSet("Result", ItemsDataGrid);
            this.Cursor = Cursors.Arrow;
            Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
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

        #endregion Event Handlers

    }
}
