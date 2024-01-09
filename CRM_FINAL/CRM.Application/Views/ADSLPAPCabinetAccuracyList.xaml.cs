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
    public partial class ADSLPAPCabinetAccuracyList : Local.TabWindow
    {
        #region Propperties

        #endregion

        #region Constructors

        public ADSLPAPCabinetAccuracyList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            CenterColumn.ItemsSource = Data.CenterDB.GetCenters();            
        }

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

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            CenterComboBox.Reset();
            CabinetNoTextBox.Text = string.Empty;
            RemoveFailureCheckBox.IsChecked = null;

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int cabinetID = -1;
            if (!string.IsNullOrWhiteSpace(CabinetNoTextBox.Text))
                cabinetID = Convert.ToInt32(CabinetNoTextBox.Text);

            ItemsDataGrid.ItemsSource = Data.ADSLPAPCabinetAccuracyDB.SearchCabenitAccuracy(CenterComboBox.SelectedIDs, cabinetID, RemoveFailureCheckBox.IsChecked);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLPAPCabinetAccuracyForm Window = new ADSLPAPCabinetAccuracyForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLPAPCabinetAccuracy item = ItemsDataGrid.SelectedItem as ADSLPAPCabinetAccuracy;

                if (item == null)
                    return;

                ADSLPAPCabinetAccuracyForm Window = new ADSLPAPCabinetAccuracyForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void RemovalFailure(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLPAPCabinetAccuracy item = ItemsDataGrid.SelectedItem as ADSLPAPCabinetAccuracy;

                if (item == null)
                    return;

                item.CorrectDate = DB.GetServerDate();

                item.Detach();
                DB.Save(item, false);
            }
        }

        #endregion
    }
}
