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
    public partial class SpecialServiceTypeList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public SpecialServiceTypeList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PaymentTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PaymentType));
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
            ItemsDataGrid.ItemsSource = Data.SpecialServiceTypeDB.SearchSpecialServiceType(TitleTextBox.Text.Trim());
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            SpecialServiceTypeForm window = new SpecialServiceTypeForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                SpecialServiceType item = ItemsDataGrid.SelectedItem as Data.SpecialServiceType;
                if (item == null) return;

                SpecialServiceTypeForm window = new SpecialServiceTypeForm(item.ID);
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
                    SpecialServiceType item = ItemsDataGrid.SelectedItem as CRM.Data.SpecialServiceType;

                    DB.Delete<Data.SpecialServiceType>(item.ID);
                    ShowSuccessMessage("نوع سرویس ویژه مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف نوع سرویس ویژه", ex);
            }
        }
        
        #endregion
    }
}
