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
    public partial class Failure117NetworkContractorList : Local.TabWindow
    {
        #region Propperties

        private string city = string.Empty;

        #endregion

        #region Constructors

        public Failure117NetworkContractorList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
        }

        private void LoadData()
        {
            if (city == "kermanshah")
                SearchExpender.IsExpanded = false;

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
            ItemsDataGrid.ItemsSource = Failure117NetworkContractorDB.SearchContractor(TitleTextBox.Text, ManagerTextBox.Text, TelephoneNoTextBox.Text, FaxTextBox.Text, AddressTextBox.Text);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            TitleTextBox.Text = string.Empty;
            ManagerTextBox.Text = string.Empty;
            TelephoneNoTextBox.Text = string.Empty;
            FaxTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;

            Search(null, null);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Failure117NetworkContractorFrom Window = new Failure117NetworkContractorFrom();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Failure117NetworkContractor item = ItemsDataGrid.SelectedItem as Failure117NetworkContractor;

                if (item == null)
                    return;

                Failure117NetworkContractorFrom Window = new Failure117NetworkContractorFrom(item.ID);
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
                    Failure117NetworkContractor item = ItemsDataGrid.SelectedItem as Failure117NetworkContractor;
                    DB.Delete<Failure117NetworkContractor>(item.ID);

                    ShowSuccessMessage("شرکت پیمانکار مورد نظر حذف شد");

                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف شرکت پیمانکار", ex);
            }
        }

        #endregion
    }
}
