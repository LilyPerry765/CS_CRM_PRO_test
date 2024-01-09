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
    public partial class Failure117FailureStatusList : Local.TabWindow
    {
        #region Propperties

        private string city = string.Empty;

        #endregion

        #region Constructors

        public Failure117FailureStatusList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
         
            FailureStatusTypeComboBox.ItemsSource = Failure117DB.GetFailureStatusCheckable();
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
            int archivedTime = 0;
            if (!string.IsNullOrEmpty(ArchivedTimeTextBox.Text))
                archivedTime = Convert.ToInt32(ArchivedTimeTextBox.Text);

            ItemsDataGrid.ItemsSource = Failure117DB.SearchFailureStatus(FailureStatusTypeComboBox.SelectedIDs, FailureStatusTextBox.Text, archivedTime, IsActiveCheckBox.IsChecked);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            FailureStatusTextBox.Text = string.Empty;
            FailureStatusTypeComboBox.Reset();
            ArchivedTimeTextBox.Text = string.Empty;
            IsActiveCheckBox.IsChecked = null;

            Search(null, null);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Failure117FailureStatusForm Window = new Failure117FailureStatusForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                FailureStatusInfo item = ItemsDataGrid.SelectedItem as FailureStatusInfo;

                if (item == null)
                    return;

                Failure117FailureStatusForm Window = new Failure117FailureStatusForm(item.ID);
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
                    Failure117FailureStatus item = ItemsDataGrid.SelectedItem as Failure117FailureStatus;
                    DB.Delete<Failure117FailureStatus>(item.ID);

                    ShowSuccessMessage("وضعیت خرابی مورد نظر حذف شد");

                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف وضعیت خرابی", ex);
            }
        }

        #endregion
    }
}
