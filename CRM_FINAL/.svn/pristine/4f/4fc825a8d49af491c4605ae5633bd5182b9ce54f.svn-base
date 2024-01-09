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
    public partial class ADSLTelephoneAccuracyList : Local.TabWindow
    {
        #region Propperties

        #endregion

        #region Constructors

        public ADSLTelephoneAccuracyList()
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
            TelephoneNoTextBox.Text = string.Empty;
            RemoveFailureCheckBox.IsChecked = null;

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt32(TelephoneNoTextBox.Text);

            ItemsDataGrid.ItemsSource = Data.ADSLTelephoneAccuracyDB.SearchTelephoneAccuracy(CenterComboBox.SelectedIDs, telephoneNo, RemoveFailureCheckBox.IsChecked);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLTelephoneAccuracyForm Window = new ADSLTelephoneAccuracyForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLTelephoneAccuracy item = ItemsDataGrid.SelectedItem as ADSLTelephoneAccuracy;

                if (item == null)
                    return;

                ADSLTelephoneAccuracyForm Window = new ADSLTelephoneAccuracyForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void RemovalFailure(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLTelephoneAccuracy item = ItemsDataGrid.SelectedItem as ADSLTelephoneAccuracy;

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
