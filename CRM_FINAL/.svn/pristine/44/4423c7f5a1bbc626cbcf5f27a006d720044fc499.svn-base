using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.Threading.Tasks;
using CRM.Application.Local;
using System.Windows.Media;
using System.Data;

namespace CRM.Application.Views
{
    public partial class ManagementRequestsInbox : Local.TabWindow
    {
        #region Properties

        public int SelectedStepID { get; set; }
        public int RequestTypeID { get; set; }
        public bool IsInquiryMode { get; set; }
        public bool IsArchived { get; set; }

        #endregion

        #region Constructors

        public ManagementRequestsInbox()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RequestCityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            RequestTypesComboBox.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckableNullable().Union(new List<CheckableItemNullable> { new CheckableItemNullable { ID = null } }).ToList();
            ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.Delete, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward };
        }

        public void LoadData()
        {
            Search(null, null);
        }


        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            Pager.TotalRecords = Data.RequestDB.ManagementSearchRequestsCount(RequestCityComboBox.SelectedIDs, RequestCenterComboBox.SelectedIDs, RequestFromDateTextBox.SelectedDate,  (int?)RequestTypesComboBox.SelectedValue, IDTextBox.Text, TelephoneNoTextBox.Text, IsWaitingListCheckBox.IsChecked.Value, IsCancelationCheckBox.IsChecked.Value);
            ItemsDataGrid.ItemsSource = Data.RequestDB.ManagementSearchRequests(RequestCityComboBox.SelectedIDs, RequestCenterComboBox.SelectedIDs, RequestFromDateTextBox.SelectedDate, (int?)RequestTypesComboBox.SelectedValue, IDTextBox.Text, TelephoneNoTextBox.Text, IsWaitingListCheckBox.IsChecked.Value, IsCancelationCheckBox.IsChecked.Value, pageSize, startRowIndex);
            ItemsDataGrid.SelectedItem = null;

            FooterStatusBar.Visibility = Visibility.Visible;
            FooterStatusLine.Visibility = Visibility.Collapsed;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
        }
        private void ItemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActionUserControl.ItemIDs.Clear();

            if (ItemsDataGrid.SelectedItem != null)
            {
                RequestInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestInfo;
                FooterStatusLine.RequestStepID = requestInfo.StepID;
                FooterStatusLine.DrawStates(requestInfo.ID);
                FooterStatusBar.Visibility = Visibility.Collapsed;
                FooterStatusLine.Visibility = Visibility.Visible;

                ActionUserControl.ItemIDs.Clear();

                foreach (object currentItem in ItemsDataGrid.SelectedItems)
                {
                    requestInfo = currentItem as RequestInfo;
                    ActionUserControl.ItemIDs.Add(requestInfo.ID);
                }
            }

        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void NumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        private void RequestStatusClick(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                RequestInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestInfo;
                RequestStatusViewForm window = new RequestStatusViewForm(requestInfo);
                window.ShowDialog();
            }
        }
        #endregion

        private void ImageView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RequestCityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            RequestCenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(RequestCityComboBox.SelectedIDs);
        }

        private void RequestCenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

    }
}
