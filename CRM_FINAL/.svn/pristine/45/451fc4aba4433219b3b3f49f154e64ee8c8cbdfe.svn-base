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
    public partial class SwitchList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constractors

        public SwitchList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            SwitchTypeComboBox.ItemsSource = Data.SwitchTypeDB.GetSwitchCheckable();
            WorkUnitResponsibleComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.WorkUnitResponsible));
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Status));
            CenterID.ItemsSource = Data.CenterDB.GetCenterCheckable();
            SwitchTypeID.ItemsSource = Data.SwitchTypeDB.GetSwitchCheckable();
            WorkUnitResponsible.ItemsSource = Helper.GetEnumItem(typeof(DB.WorkUnitResponsible));
            Status.ItemsSource = Helper.GetEnumItem(typeof(DB.Status));
        }

        private void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Search(null, null);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {

            int capacity = CapacityTextBox.Text == string.Empty ? -1 : Convert.ToInt32(CapacityTextBox.Text);

            int operationalCapacity = -1;
            if (OperationalCapacityTextBox.Text != string.Empty)
                operationalCapacity = Convert.ToInt32(OperationalCapacityTextBox.Text);

            int installCapacity = -1;
            if (InstallCapacityTextBox.Text != string.Empty)
                installCapacity = Convert.ToInt32(InstallCapacityTextBox.Text);

            ItemsDataGrid.ItemsSource = Data.SwitchDB.SearchData(CenterComboBox.SelectedIDs, SwitchTypeComboBox.SelectedIDs, WorkUnitResponsibleComboBox.SelectedIDs, SwitchCodeTextBox.Text.Trim(), capacity, operationalCapacity, installCapacity, StatusComboBox.SelectedIDs);
        }
                
        private void AddItem(object sender, RoutedEventArgs e)
        {
            SwitchForm window = new SwitchForm(0);
            window.ShowDialog();
            
            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Switch item = ItemsDataGrid.SelectedItem as Switch;
                if (item == null) return;
                SwitchForm window = new SwitchForm(item.ID);
                window.ShowDialog();
                if (window.DialogResult == true)
                    Search(null, null);

            }



        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;
            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Switch item = ItemsDataGrid.SelectedItem as Switch;
                    DB.Delete<Switch>(item.ID);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف شهر", ex);
            }

        }
        
        #endregion
    }
}
