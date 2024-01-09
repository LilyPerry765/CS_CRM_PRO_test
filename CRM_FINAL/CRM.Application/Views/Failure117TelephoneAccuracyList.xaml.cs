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
    public partial class Failure117TelephoneAccuracyList : Local.TabWindow
    {
        #region Propperties

        private string city = string.Empty;

        #endregion

        #region Constructors

        public Failure117TelephoneAccuracyList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();

            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            CenterColumn.ItemsSource = Data.CenterDB.GetCenters();            
        }

        private void LoadData()
        {
            if (city == "kermanshah")
            {
                SearchExpander.IsExpanded = false;
                RemoveFailureCheckBox.IsChecked = false;
            }

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

            ItemsDataGrid.ItemsSource = Data.Failure117CabenitAccuracyDB.SearchTelephoneAccuracy(CenterComboBox.SelectedIDs, telephoneNo, RemoveFailureCheckBox.IsChecked);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Failure117TelephoneAccuracyForm Window = new Failure117TelephoneAccuracyForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Failure117TelephoneAccuracy item = ItemsDataGrid.SelectedItem as Failure117TelephoneAccuracy;

                if (item == null)
                    return;

                Failure117TelephoneAccuracyForm Window = new Failure117TelephoneAccuracyForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void RemovalFailure(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItems != null)
            {
                foreach (Failure117TelephoneAccuracy telephone in ItemsDataGrid.SelectedItems)
                {
                    telephone.CorrectDate = DB.GetServerDate();

                    telephone.Detach();
                    DB.Save(telephone, false);
                }

                Search(null, null);
            }
        }        

        #endregion
    }
}
