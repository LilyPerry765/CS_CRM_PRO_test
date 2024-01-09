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

namespace CRM.Application.Views
{
    public partial class CancelationList : Local.TabWindow
    {
        #region properties

        #endregion

        #region Constructor

        public CancelationList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RequestTypeComboBox.ItemsSource = Data.TypesDB.GetRequestTypesCheckable();
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            StepComboBox.ItemsSource = Data.RequestStepDB.GetRequestStepCheckable();
        }

        public override void Load()
        {
            Search(null, null);
        }

        #endregion

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

            Pager.TotalRecords = Data.CancelationListDB.SearchCancelationListCount(RequestIDTextBox.Text.Trim(), TelephoneNoTextBox.Text.Trim(), RequestTypeComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CustomerTextBox.Text.Trim(), InsertRequestDate.SelectedDate, StepComboBox.SelectedIDs, EntryDate.SelectedDate, ReasonTextBox.Text.Trim());
            ItemsDataGrid.ItemsSource = Data.CancelationListDB.SearchCancelationList(RequestIDTextBox.Text.Trim(), TelephoneNoTextBox.Text.Trim(), RequestTypeComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CustomerTextBox.Text.Trim(), InsertRequestDate.SelectedDate, StepComboBox.SelectedIDs, EntryDate.SelectedDate, ReasonTextBox.Text.Trim(), startRowIndex, pageSize);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }
    }
}
