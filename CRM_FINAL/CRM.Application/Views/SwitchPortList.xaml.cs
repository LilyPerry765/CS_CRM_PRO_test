using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;

namespace CRM.Application.Views
{
    public partial class SwitchPortList : Local.ExtendedTabWindowBase
    {
        #region Properties

        #endregion

        #region Constructor

        public SwitchPortList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Load Methods

        private void Initialize()
        {

            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.SwitchPortStatus));
            StatusColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.SwitchPortStatus));
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
        }

        public void LoadData()
        {
            //خط زیر به علت کند بودن کامنت شد
            //Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
            CenterComboBox_LostFocus(null, null);
        }

        //milad doran
        //private void Search(object sender, RoutedEventArgs e)
        //{
        //    this.Cursor = Cursors.Wait;
        //    int startRowIndex = Pager.StartRowIndex;
        //    int pageSize = Pager.PageSize;
        //    int count = 0;
        //
        //    //Pager.TotalRecords = Data.SwitchPortDB.SearchSwitchPortCount(StatusComboBox.SelectedIDs, SwitchComboBox.SelectedIDs, TypeCheckBox.IsChecked, PortNoTextBox.Text.Trim(), MDFHorizentalIDTextBox.Text.Trim(), CenterComboBox.SelectedIDs);
        //    ItemsDataGrid.ItemsSource = Data.SwitchPortDB.SearchSwitchPort(
        //                                                                    StatusComboBox.SelectedIDs, SwitchComboBox.SelectedIDs,
        //                                                                    TypeCheckBox.IsChecked, PortNoTextBox.Text.Trim(),
        //                                                                    MDFHorizentalIDTextBox.Text.Trim(), CenterComboBox.SelectedIDs,
        //                                                                    startRowIndex, pageSize, out count
        //                                                                  );
        //    Pager.TotalRecords = count;
        //    this.Cursor = Cursors.Arrow;
        //}

        //TODO:rad 13950711
        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = 0;
            List<int> statusesId = StatusComboBox.SelectedIDs;
            List<int> switchesId = SwitchComboBox.SelectedIDs;
            List<int> centersId = CenterComboBox.SelectedIDs;
            string portNo = PortNoTextBox.Text.Trim();
            string mDfHorizentalId = MDFHorizentalIDTextBox.Text.Trim();
            bool? type = TypeCheckBox.IsChecked;

            Action mainAction = new Action(() =>
            {
                List<SwitchPortInfo> result = Data.SwitchPortDB.SearchSwitchPort(statusesId, switchesId, type, portNo, mDfHorizentalId, centersId, startRowIndex, pageSize, out count);
                Dispatcher.BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                                                                                {
                                                                                    ItemsDataGrid.ItemsSource = result;
                                                                                    Pager.TotalRecords = count;
                                                                                }
                                                                              )
                                       );
            }
                                            );

            //مقداردهی عملیات اطلاع رسانی از وضعیت اجرای عملیات اصلی
            Action duringOperationAction = new Action(() =>
            {
                MainExtendedStatusBar.ShowProgressBar = true;
                MainExtendedStatusBar.MessageLabel.FontSize = 13;
                MainExtendedStatusBar.MessageLabel.FontWeight = FontWeights.Bold;
                MainExtendedStatusBar.MessageLabel.Text = "درحال بارگذاری...";
                Pager.IsEnabled = false;
                SearchExpander.IsEnabled = false;
                ItemsDataGrid.IsEnabled = false;
                this.Cursor = Cursors.Wait;
            });

            //مقداردهی عملیاتی که باید بعد از اتمام عملیات اصلی اجرا شود 
            Action afterOperationAction = new Action(() =>
            {
                MainExtendedStatusBar.ShowProgressBar = false;
                MainExtendedStatusBar.MessageLabel.FontSize = 8;
                MainExtendedStatusBar.MessageLabel.FontWeight = FontWeights.Normal;
                MainExtendedStatusBar.MessageLabel.Text = string.Empty;
                Pager.IsEnabled = true;
                SearchExpander.IsEnabled = true;
                ItemsDataGrid.IsEnabled = true;
                this.Cursor = Cursors.Arrow;
            });

            CRM.Application.Local.TimeConsumingOperation timeConsumingOperation = new Local.TimeConsumingOperation
            {
                MainOperationAction = mainAction,
                DuringOperationAction = duringOperationAction,
                AfterOperationAction = afterOperationAction
            };

            //اجرای عملیات
            this.RunTimeConsumingOperation(timeConsumingOperation);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.SwitchPortInfo item = ItemsDataGrid.SelectedItem as CRM.Data.SwitchPortInfo;

                    DB.Delete<Data.SwitchPort>(item.ID);
                    ShowSuccessMessage("پورت مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف پورت", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                SwitchPortInfo item = ItemsDataGrid.SelectedItem as Data.SwitchPortInfo;
                if (item == null) return;

                SwitchPortForm window = new SwitchPortForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //try
            //{
            //    Data.SwitchPort item = e.Row.Item as Data.SwitchPort;

            //    item.Detach();
            //    DB.Save(item);

            //    Search(null, null);

            //    ShowSuccessMessage("پورت مورد نظر ذخیره شد");
            //}
            //catch (Exception ex)
            //{
            //    e.Cancel = true;
            //    ShowErrorMessage("خطا در ذخیره پورت", ex);
            //}
        }

        private void NumberItem(object sender, RoutedEventArgs e)
        {
            SwitchPortToNumber window = new SwitchPortToNumber();
            window.ShowDialog();
        }

        private void AddOFFile(object sender, RoutedEventArgs e)
        {
            AddSwitchPortOfFileForm window = new AddSwitchPortOfFileForm();
            window.ShowDialog();
        }
        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SwitchComboBox.ItemsSource = SwitchDB.GetSwitchWithCenterNameCheckableByCentersID(CenterComboBox.SelectedIDs);
        }

        #endregion

    }
}
