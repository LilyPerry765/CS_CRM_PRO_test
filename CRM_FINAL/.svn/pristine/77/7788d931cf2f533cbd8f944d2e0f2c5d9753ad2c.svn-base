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
    public partial class ADSLServiceGiftProfileList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public ADSLServiceGiftProfileList()
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

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = ADSLServiceDB.SearchADSLServiceGiftProfiles(TitleTextBox.Text);
        }
        
        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLServiceGiftProfileForm window = new ADSLServiceGiftProfileForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLServiceGiftProfile item = ItemsDataGrid.SelectedItem as ADSLServiceGiftProfile;
                if (item == null) return;

                ADSLServiceGiftProfileForm window = new ADSLServiceGiftProfileForm(item.ID);
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
                    ADSLServiceGiftProfile item = ItemsDataGrid.SelectedItem as ADSLServiceGiftProfile;

                    DB.Delete<Data.ADSLServiceGiftProfile>(item.ID);
                    ShowSuccessMessage("پروفایل هدیه مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف پروفایل هدیه", ex);
            }
        }

        #endregion
    }
}
