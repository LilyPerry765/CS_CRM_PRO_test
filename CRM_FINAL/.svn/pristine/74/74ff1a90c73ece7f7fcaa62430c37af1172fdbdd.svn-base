using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class WaitingListList : Local.TabWindow
    {
        #region properties

        #endregion

        #region Constructor

        public WaitingListList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RequestTypeComboBox.ItemsSource = Data.TypesDB.GetRequestTypesCheckable();

            ReasonComboBox.ItemsSource = Data.WaitingListReasonDB.GetWaitingListReasonCheckableByRequestTypeID();
            ReasonIDColumn.ItemsSource = Data.WaitingListReasonDB.GetWaitingListReasonCheckableByRequestTypeID();

            ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.ExitWaitingList };
        }

        public override void Load()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            
            Pager.TotalRecords = Data.WaitingListDB.SearchWaitingListCounts(RequestIDTextBox.Text.Trim(),TelephoneNoTextBox.Text.Trim(), RequestTypeComboBox.SelectedIDs, ReasonComboBox.SelectedIDs, FromInsertDate.SelectedDate, ToInsertDate.SelectedDate, FromExitDate.SelectedDate, ToExitDate.SelectedDate, (bool)StatusCheckBox.IsChecked);
            ItemsDataGrid.ItemsSource = Data.WaitingListDB.SearchWaitingList(RequestIDTextBox.Text.Trim(),TelephoneNoTextBox.Text.Trim(), RequestTypeComboBox.SelectedIDs, ReasonComboBox.SelectedIDs, FromInsertDate.SelectedDate, ToInsertDate.SelectedDate, FromExitDate.SelectedDate, ToExitDate.SelectedDate, (bool)StatusCheckBox.IsChecked, startRowIndex, pageSize);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    WaitingList item = ItemsDataGrid.SelectedItem as WaitingList;

                    DB.Delete<Data.WaitingList>(item.ID);
                    ShowSuccessMessage("لیست انتظار مورد نظر حذف شد");
                    Load();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف لیست انتظار", ex);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void RequestIDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void ItemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActionUserControl.ItemIDs.Clear();

            if (ItemsDataGrid.SelectedItem != null)
            {
                WaitingListInfo waitingListInfo = new WaitingListInfo();
                FooterStatusBar.Visibility = Visibility.Collapsed;
                ActionUserControl.ItemIDs.Clear();

                foreach (object currentItem in ItemsDataGrid.SelectedItems)
                {
                    waitingListInfo = currentItem as WaitingListInfo;
                    ActionUserControl.ItemIDs.Add(waitingListInfo.ID);
                }
            }
        }

        private void ExitWaitingList(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedValue != null)
            {                
                WaitingListInfo waitingInfo = ItemsDataGrid.SelectedItem as WaitingListInfo;
                WaitingList waiting = WaitingListDB.GetWaitingListByID(waitingInfo.ID);

                Request request = RequestDB.GetRequestByID(waiting.RequestID);

                RequestForADSL.ExitWaitingList(waiting, request);

                Search(null, null);
            }
        }

        #endregion Event Handlers
    }
}
