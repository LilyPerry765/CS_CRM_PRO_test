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
    public partial class SwitchSpecialServiceList : Local.TabWindow
    {
        #region Constructor & Fields

        public SwitchSpecialServiceList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            SwitchColumn.ItemsSource = Data.SwitchDB.GetSwitchCheckable();
            SwitchComboBox.ItemsSource = Data.SwitchDB.GetSwitchCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.SwitchSpecialServicesStatus));
            StatusColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.SwitchSpecialServicesStatus));
            SpecialServiceTypeComboBox.ItemsSource = Data.SpecialServiceTypeDB.GetSpecialServiceTypeCheckable();
            SpecialServiceTypeColumn.ItemsSource = Data.SpecialServiceTypeDB.GetSpecialServiceTypeCheckable();
            SpecialServiceTypeColumn.ItemsSource = Data.SpecialServiceTypeDB.GetSpecialServiceTypeCheckable();

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
       
            int capacity = -1;
            if (!string.IsNullOrWhiteSpace(CapacityTextBox.Text)) capacity = Convert.ToInt32(CapacityTextBox.Text);

            ItemsDataGrid.ItemsSource = Data.SwitchSpecialServiceDB.SearchSwitchSpecialService(StatusComboBox.SelectedIDs, SwitchComboBox.SelectedIDs, SpecialServiceTypeComboBox.SelectedIDs, capacity);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.SpecialService item = ItemsDataGrid.SelectedItem as CRM.Data.SpecialService;

                    DB.Delete<Data.SwitchSpecialService>(item.ID);
                    ShowSuccessMessage("سرویس ویژه مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف سرویس ویژه", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                SwitchSpecialService item = ItemsDataGrid.SelectedItem as Data.SwitchSpecialService;
                if (item == null) return;

                SwitchSpecialServiceForm window = new SwitchSpecialServiceForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.SwitchSpecialService item = e.Row.Item as Data.SwitchSpecialService;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("سرویس ویژه مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره سرویس ویژه", ex);
            }
        }
        #endregion Event Handlers
    }
}
