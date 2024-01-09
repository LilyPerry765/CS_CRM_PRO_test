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
    /// <summary>
    /// Interaction logic for BlackListCustomer.xaml
    /// </summary>
    public partial class BlackListCustomer : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public BlackListCustomer()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PersonTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType));
            GenderComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Gender));
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
            ItemsDataGrid.ItemsSource = BlackListDB.SearchBlackListCustomer(PersonTypeComboBox.SelectedIDs, NationalCodeOrRecordNoTextBox.Text, FirstNameOrTitleTextBox.Text, LastNameTextBox.Text, FatherNameTextBox.Text, GenderComboBox.SelectedIDs, BirthCertificateIDTextBox.Text, BirthDateOrRecordDate.SelectedDate, IssuePlaceTextBox.Text, UrgentTelNoTextBox.Text, MobileNoTextBox.Text, EmailTextBox.Text, (byte)DB.BlackListType.Customer);
        }
        
        private void AddItem(object sender, RoutedEventArgs e)
        {
            BlackListForm window = new BlackListForm((byte)DB.BlackListType.Customer);
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                BlackListCustomerInfo item = ItemsDataGrid.SelectedItem as BlackListCustomerInfo;
                if (item == null) return;

                BlackListForm window = new BlackListForm((byte)DB.BlackListType.Customer, item.ID);
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
                    Address item = ItemsDataGrid.SelectedItem as Address;

                    DB.Delete<Data.Address>(item.ID);
                    ShowSuccessMessage("آدرس مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف آدرس", ex);
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Address address = e.Row.Item as Address;

                address.Detach();
                DB.Save(address);

                Search(null, null);

                ShowSuccessMessage("استان مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره استان", ex);
            }
        }

        #endregion
    }
}
