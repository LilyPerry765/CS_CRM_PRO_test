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
    public partial class SMSServiceList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public SMSServiceList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
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
            TitleTextBox.Text = string.Empty;
            IsActiveCheckBox.IsChecked = null;

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = SMSServiceDB.SearchSMSServices(TitleTextBox.Text.Trim(), IsActiveCheckBox.IsChecked);
        }
        
        private void AddItem(object sender, RoutedEventArgs e)
        {
            SMSServiceForm window = new SMSServiceForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Data.SMSService item = ItemsDataGrid.SelectedItem as Data.SMSService;
                if (item == null) return;

                SMSServiceForm window = new SMSServiceForm(item.ID);
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
                    Data.SMSService item = ItemsDataGrid.SelectedItem as Data.SMSService;

                    DB.Delete<Data.SMSService>(item.ID);
                    ShowSuccessMessage("پیام مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف پیام", ex);
            }
        }

        #endregion
    }
}
