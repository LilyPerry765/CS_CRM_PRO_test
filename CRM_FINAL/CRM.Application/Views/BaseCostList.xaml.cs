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
    public partial class BaseCostList : Local.TabWindow
    {
        #region Constructor

        public BaseCostList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            //WorkUnitComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.WorkUnit));
            ChargingGroupComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ChargingGroup));
            //WorkUnitColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.WorkUnit));
            ChargingGroupColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.ChargingGroup));
            RequestTypeComboBox.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();
            QuotaDiscountComboBox.ItemsSource = Data.QuotaDiscountDB.GetQuotaDiscountCheckable();
            RequestTypeColumn.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();
            QuotaDiscountColumn.ItemsSource = Data.QuotaDiscountDB.GetQuotaDiscountCheckable();
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

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = Data.BaseCostDB.SearchBaseCost(/*WorkUnitComboBox.SelectedIDs,*/ ChargingGroupComboBox.SelectedIDs, IsActiveCheckBox.IsChecked, RequestTypeComboBox.SelectedIDs, FromDateDate.SelectedDate, ToDateDate.SelectedDate, IsPerodicCheckBox.IsChecked, QuotaDiscountComboBox.SelectedIDs, CostTitleTextBox.Text.Trim());
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            BaseCostForm window = new BaseCostForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                BaseCost item = ItemsDataGrid.SelectedItem as Data.BaseCost;
                if (item == null) return;

                BaseCostForm window = new BaseCostForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
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
                    BaseCost item = ItemsDataGrid.SelectedItem as BaseCost;
                    if (item.IsReadOnly == true)
                    {
                        throw new Exception("امکان حذف این هزینه نمی یاشد.");
                    }
                   

                    DB.Delete<Data.BaseCost>(item.ID);
                    ShowSuccessMessage("هزینه مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف هزینه", ex);
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.BaseCost item = e.Row.Item as Data.BaseCost;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("هزینه مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره هزینه", ex);
            }
        }

        #endregion
    }
}
