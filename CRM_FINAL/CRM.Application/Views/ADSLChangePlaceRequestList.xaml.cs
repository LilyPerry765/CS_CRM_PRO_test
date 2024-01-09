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
    /// <summary>
    /// Interaction logic for ADSLChangePlaceRequestList.xaml
    /// </summary>
    public partial class ADSLChangePlaceRequestList : Local.TabWindow
    {        
        #region Propperties

        #endregion

        #region Constructors

        public ADSLChangePlaceRequestList()
        {
            InitializeComponent();
        }

        #endregion

        private void LoadData()
        {
            Search(null, null);
        }

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = Data.ADSLChangePlaceDB.SearchADSLChangePlaceInfo(RequestIDTextBox.Text.Trim(), OldTelephoneNoTextBox.Text.Trim(), NewTelephoneNoTextBox.Text.Trim());
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            RequestIDTextBox.Text = string.Empty;
            OldTelephoneNoTextBox.Text = string.Empty;
            NewTelephoneNoTextBox.Text = string.Empty;

            Search(null, null);
        }

        #endregion
    }
}
