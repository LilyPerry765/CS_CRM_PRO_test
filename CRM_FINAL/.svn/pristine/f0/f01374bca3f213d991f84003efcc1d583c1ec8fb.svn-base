using CRM.Application.Local;
using CRM.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TelecomminucationServiceList.xaml
    /// </summary>
    public partial class TelecomminucationServiceList : TabWindow
    {
        public TelecomminucationServiceList()
        {
            InitializeComponent();
            Initialize();
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            TelecomminucationServiceForm window = new TelecomminucationServiceForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                TelecomminucationServiceInfo item = ItemsDataGrid.SelectedItem as TelecomminucationServiceInfo;
                if (item == null) return;

                TelecomminucationServiceForm windowe = new TelecomminucationServiceForm(item.ID);
                windowe.ShowDialog();

                if (windowe.DialogResult == true)
                {
                    Search(null, null);
                }
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
                    TelecomminucationServiceInfo item = ItemsDataGrid.SelectedItem as TelecomminucationServiceInfo;
                    DB.Delete<TelecomminucationService>(item.ID);
                    ShowSuccessMessage(".کالا - خدمات مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                string errorMessage = SqlExceptionHelper.ErrorMessage(sqlException);
                ShowErrorMessage(errorMessage, sqlException);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف کالا - خدمات", ex);
            }
        }

        public void Initialize()
        {
            RequestTypesComboBox.ItemsSource = RequestTypeDB.GetRequestTypeCheckable();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            long unitPrice = (!string.IsNullOrEmpty(UnitPriceNumericTextBox.Text.Trim())) ? long.Parse(UnitPriceNumericTextBox.Text.Trim()) : -1;
            int tax = (!string.IsNullOrEmpty(TaxNumericTextBox.Text.Trim())) ? int.Parse(TaxNumericTextBox.Text.Trim()) : -1;
            bool? isActive = (!ActiveCheckBox.IsChecked.HasValue) ? default(bool?) :
                             (ActiveCheckBox.IsChecked.HasValue && ActiveCheckBox.IsChecked.Value) ? true : false;

            ItemsDataGrid.ItemsSource = TelecomminucationServiceDB.SearchTelecomminucationServices(TitleTextBox.Text.Trim(), unitPrice, tax, RequestTypesComboBox.SelectedIDs, isActive);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            UIElement container = SearchExpander as UIElement;
            Helper.ResetSearch(container);
        }

        public void LoadData()
        {
            Search(null, null);
        }

    }
}