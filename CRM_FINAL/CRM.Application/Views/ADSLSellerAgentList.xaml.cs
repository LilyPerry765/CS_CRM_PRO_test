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
    public partial class ADSLSellerAgentList : Local.TabWindow
    {
        #region Propperties

        #endregion

        #region Constructors

        public ADSLSellerAgentList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            CityColumn.ItemsSource = CityDB.GetAvailableCityCheckable();
            GroupComboBox.ItemsSource = Data.ADSLSellerGroupDB.GetADSLSellerGroupsCheckable();
            GroupColumn.ItemsSource = Data.ADSLSellerGroupDB.GetADSLSellerGroupsCheckable();
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

        private void Search(object sender, RoutedEventArgs e)
        {
            List<int> centerIDs = DB.CurrentUser.CenterIDs;
            List<int> cityIDs = CityDB.GetAvailableCity();

            ItemsDataGrid.ItemsSource = Data.ADSLSellerGroupDB.SearchADSLSellerAgents(cityIDs ,CityComboBox.SelectedIDs, GroupComboBox.SelectedIDs, TitleTextBox.Text.Trim(), TelephoneTextBox.Text.Trim(), MobileTextBox.Text.Trim(), AddressTextBox.Text.Trim(),IsActiveCheckBox.IsChecked);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            CityComboBox.Reset();
            GroupComboBox.Reset();
            TitleTextBox.Text = string.Empty;
            TelephoneTextBox.Text = string.Empty;
            MobileTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
            IsActiveCheckBox.IsChecked = null;

            Search(null, null);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLSellerAgentForm Window = new ADSLSellerAgentForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLSellerAgent item = ItemsDataGrid.SelectedItem as ADSLSellerAgent;

                if (item == null)
                    return;

                ADSLSellerAgentForm Window = new ADSLSellerAgentForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void ShowUsers(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLSellerAgent item = ItemsDataGrid.SelectedItem as Data.ADSLSellerAgent;
                if (item == null) return;

                ADSLSellerAgentUserList window = new ADSLSellerAgentUserList(item.ID, CityComboBox.SelectedIDs);
                Folder.Console.Navigate(window, "کاربران نماینده فروش");
            }
        }
        
        private void GrantAccess(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLSellerAgent item = ItemsDataGrid.SelectedItem as ADSLSellerAgent;

                if (item == null)
                    return;

                ADSLSellerAgentAccessForm Window = new ADSLSellerAgentAccessForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void GrantCommission(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLSellerAgent item = ItemsDataGrid.SelectedItem as ADSLSellerAgent;

                if (item == null)
                    return;

                ADSLSellerAgentCommissionForm Window = new ADSLSellerAgentCommissionForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") 
                return;
            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ADSLSellerAgent item = ItemsDataGrid.SelectedItem as ADSLSellerAgent;
                    DB.Delete<ADSLSellerAgent>(item.ID);

                    ShowSuccessMessage("نماینده فروش مورد نظر حذف شد");
                    
                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف نماینده فروش", ex);
            }
        }

        #endregion
    }
}
