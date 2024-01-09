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
    public partial class ADSLInstallCostCenterList : Local.TabWindow
    {
        #region Properties

        #endregion Properties

        #region Constructor
        public ADSLInstallCostCenterList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Methods

        private void Initialize()
        {
            CenterColumn.ItemsSource = Data.CenterDB.GetCenterCheckable();
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
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
            long cost = !string.IsNullOrWhiteSpace(CostTextBox.Text) ? Convert.ToInt64(CostTextBox.Text) : -1;
            ItemsDataGrid.ItemsSource = Data.ADSLInstallCostCenterDB.GetADSLInstallCostList(CenterComboBox.SelectedIDs, cost);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            CenterComboBox.Reset();
            CostTextBox.Text = string.Empty;

            Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLInstalCostCenter adslInstallcost = ItemsDataGrid.SelectedItem as ADSLInstalCostCenter;
                if (adslInstallcost == null) return;

                ADSLInstallCostSingleCenterForm window = new ADSLInstallCostSingleCenterForm(adslInstallcost.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void EditCityItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLInstalCostCenter adslInstallcost = ItemsDataGrid.SelectedItem as ADSLInstalCostCenter;
                if (adslInstallcost == null) return;

                ADSLInstallCostCenterForm window = new ADSLInstallCostCenterForm();
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion
    }
}
