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
    public partial class Failure117LineStatusList : Local.TabWindow
    {
        #region Propperties

        private string city = string.Empty;

        #endregion

        #region Constructors

        public Failure117LineStatusList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();

            LineStatusTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Failure117LineStatus));
            LineStatusTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Failure117LineStatus));
        }

        private void LoadData()
        {
            if (city == "kermanshah")
                SearchExpander.IsExpanded = false;

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
            ItemsDataGrid.ItemsSource = Data.Failure117DB.SearchLineStatus(LineStatusTypeComboBox.SelectedIDs, LineStatusTextBox.Text, IsActiveCheckBox.IsChecked);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            LineStatusTextBox.Text = string.Empty;
            LineStatusTypeComboBox.Reset();
            IsActiveCheckBox.IsChecked = null;

            Search(null, null);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Failure117LineStatusForm Window = new Failure117LineStatusForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Failure117LineStatus item = ItemsDataGrid.SelectedItem as Failure117LineStatus;

                if (item == null)
                    return;

                Failure117LineStatusForm Window = new Failure117LineStatusForm(item.ID);
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
                    Failure117LineStatus item = ItemsDataGrid.SelectedItem as Failure117LineStatus;
                    DB.Delete<Failure117LineStatus>(item.ID);

                    ShowSuccessMessage("وضعیت خط مورد نظر حذف شد");
                    
                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف وضعیت خط", ex);
            }
        }

        #endregion
    }
}
