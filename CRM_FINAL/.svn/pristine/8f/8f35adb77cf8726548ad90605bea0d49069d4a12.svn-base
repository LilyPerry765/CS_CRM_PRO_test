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
    public partial class Failure117PostAccuracyList : Local.TabWindow
    {
        #region Propperties

        private string city = string.Empty;

        #endregion

        #region Constructors

        public Failure117PostAccuracyList()
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
            CabinetNoTextBox.Text = string.Empty;
            PostNoTextBox.Text = string.Empty;
            RemoveFailureCheckBox.IsChecked = null;

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int cabinetID = -1;
            if (!string.IsNullOrWhiteSpace(CabinetNoTextBox.Text))
                cabinetID = Convert.ToInt32(CabinetNoTextBox.Text);

            int postID = -1;
            if (!string.IsNullOrWhiteSpace(PostNoTextBox.Text))
                postID = Convert.ToInt32(PostNoTextBox.Text);

            ItemsDataGrid.ItemsSource = Data.Failure117CabenitAccuracyDB.SearchPostAccuracy(CenterComboBox.SelectedIDs, cabinetID, postID, RemoveFailureCheckBox.IsChecked);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Failure117PostAccuracyForm Window = new Failure117PostAccuracyForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Failure117PostAccuracy item = ItemsDataGrid.SelectedItem as Failure117PostAccuracy;

                if (item == null)
                    return;

                Failure117PostAccuracyForm Window = new Failure117PostAccuracyForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void RemovalFailure(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItems != null)
            {
                foreach (Failure117PostAccuracy post in ItemsDataGrid.SelectedItems)
                {
                    post.CorrectDate = DB.GetServerDate();

                    post.Detach();
                    DB.Save(post, false);
                }

                Search(null, null);
            }
        }

        #endregion
    }
}
