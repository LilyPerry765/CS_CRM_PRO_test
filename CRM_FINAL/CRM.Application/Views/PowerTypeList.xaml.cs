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
    /// Interaction logic for PowerTypeList.xaml
    /// </summary>
    public partial class PowerTypeList : TabWindow
    {
        #region Constructor

        public PowerTypeList()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void Search(object sender, RoutedEventArgs e)
        {
            int rate = (!string.IsNullOrEmpty(RateTextBox.Text)) ? int.Parse(RateTextBox.Text.Trim()) : -1;
            ItemsDataGrid.ItemsSource = PowerTypeDB.SearchPowerTypes(TitleTextBox.Text.Trim(), rate);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            UIElement container = SearchExpander as UIElement;
            Helper.ResetSearch(container);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PowerType item = ItemsDataGrid.SelectedItem as PowerType;
                if (item == null) return;

                PowerTypeForm window = new PowerTypeForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            PowerTypeForm window = new PowerTypeForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;
            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    PowerType item = ItemsDataGrid.SelectedItem as PowerType;
                    DB.Delete<Data.PowerType>(item.ID);
                    ShowSuccessMessage(".پاور مورد نظر حذف شد");
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
                ShowErrorMessage("خطا در حذف نوع پاور", ex);
            }
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region Methods

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

    }
}
