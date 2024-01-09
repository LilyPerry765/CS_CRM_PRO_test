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
    public partial class SettingList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public SettingList()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

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
            long value = -1;
            if (!string.IsNullOrWhiteSpace(SettingValueTextBox.Text.Trim()))
            {
                value = Convert.ToInt64(SettingValueTextBox.Text);
                if (value == null)
                    value = 0;
            }

            ItemsDataGrid.ItemsSource = Data.SettingDB.SearchSettings(SettingNameTextBox.Text.Trim(), value);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            SettingForm window = new SettingForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Setting item = ItemsDataGrid.SelectedItem as Setting;
                if (item == null) return;

                //SettingForm window = new SettingForm(item.ID);
                //window.ShowDialog();

                //if (window.DialogResult == true)
                //    Search(null, null);
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
                    Setting item = ItemsDataGrid.SelectedItem as Setting;

                    DB.Delete<Data.Setting>(item.Key);
                    ShowSuccessMessage("تنظیمات مورد نظر حذف شد");
                    LoadData();
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage(" مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }

            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف بانک", ex);
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Setting setting = e.Row.Item as Setting;

                setting.Detach();
                DB.Save(setting);

                Search(null, null);
                ShowSuccessMessage("تنظیمات مورد نظر ویرایش شد.");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ویرایش تنظیمات", ex);
            }
        }

        private void SettingValueTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion
    }
}
