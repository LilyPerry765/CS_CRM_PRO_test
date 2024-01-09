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
    public partial class SystemChangesList : Local.TabWindow
    {
        #region Constructor & Fields

        public SystemChangesList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {

        }

        public void LoadData()
        {
            Search(null, null);
        }
        #endregion Load Methods

        #region Event Handlers
        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }


        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);

        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int version = -1;
            if (!string.IsNullOrWhiteSpace(VersionTextBox.Text)) version = Convert.ToInt32(VersionTextBox.Text);


            ItemsDataGrid.ItemsSource = Data.SystemChangesDB.SearchSystemChanges(version, ReqeustDateDate.SelectedDate, ApplyDateDate.SelectedDate, ReqeustNoTextBox.Text.Trim(), DescriptionTextBox.Text.Trim());
        }


        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                SystemChange item = ItemsDataGrid.SelectedItem as Data.SystemChange;
                if (item == null) return;

                SystemChangesForm window = new SystemChangesForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {
            SystemChangesForm window = new SystemChangesForm(0);
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        #endregion Event Handlers


    }
}
