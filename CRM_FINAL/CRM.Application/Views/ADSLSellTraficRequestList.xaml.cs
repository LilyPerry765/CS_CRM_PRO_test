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
    public partial class ADSLSellTraficRequestList : Local.TabWindow
    {
        #region Propperties

        #endregion

        #region Constructors

        public ADSLSellTraficRequestList()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void LoadData()
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
            ItemsDataGrid.ItemsSource = Data.ADSLSellTrafficDB.SearchADSLChangeServiceInfo(RequestIDTextBox.Text.Trim(), TelephoneNoTextBox.Text.Trim(),IsPaidCheckBox.IsChecked);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            RequestIDTextBox.Text = string.Empty;
            TelephoneNoTextBox.Text = string.Empty;
            IsPaidCheckBox.IsChecked = null;

            Search(null, null);
        }

        #endregion
    }
}
