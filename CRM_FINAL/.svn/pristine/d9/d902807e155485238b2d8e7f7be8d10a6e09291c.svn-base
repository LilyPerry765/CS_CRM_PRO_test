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
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class ADSLTelephoneHistoryList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public ADSLTelephoneHistoryList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PAPInfoComboBox.ItemsSource = PAPInfoDB.GetPAPInfoCheckable();
            CentersComboBox.ItemsSource = CenterDB.GetCenterCheckable();
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

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            PAPInfoComboBox.Reset();
            CentersComboBox.Reset();
            TelephoneNoTextBox.Text = string.Empty;

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);
                        
            Pager.TotalRecords = ADSLTelephoneHistoryDB.SearchTelephoneHistorysCount(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs, telephoneNo);
            ItemsDataGrid.ItemsSource = ADSLTelephoneHistoryDB.SearchTelephoneHistory(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs, telephoneNo, startRowIndex, pageSize);
        }
                
        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion
    }
}
