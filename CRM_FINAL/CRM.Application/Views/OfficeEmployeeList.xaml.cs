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
    public partial class OfficeEmployeeList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public OfficeEmployeeList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            OfficeColumn.ItemsSource = DB.GetAllEntity<Office>();
            OfficeComboBox.ItemsSource = Data.OfficeDB.GetOfficesCheckable();
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
            OfficeComboBox.Reset();
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            
            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            Pager.TotalRecords = Data.OfficeDB.SearchOfficeEmployeeCount(OfficeComboBox.SelectedIDs, FirstNameTextBox.Text, LastNameTextBox.Text);
            ItemsDataGrid.ItemsSource = Data.OfficeDB.SearchOfficeEmployee(OfficeComboBox.SelectedIDs, FirstNameTextBox.Text, LastNameTextBox.Text, startRowIndex,pageSize);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            OfficeEmployeeForm window = new OfficeEmployeeForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                OfficeEmployee officeEmployee = ItemsDataGrid.SelectedItem as OfficeEmployee;
                if (officeEmployee == null) return;

                OfficeEmployeeForm window = new OfficeEmployeeForm(officeEmployee.ID);
                window.ShowDialog();
                
                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex == -1 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف کردن مطمئن هستید", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    OfficeEmployee item = ItemsDataGrid.SelectedItem as OfficeEmployee;
                    DB.Delete<Data.OfficeEmployee>(item.ID);
                    ShowSuccessMessage("کارمند دفتر خدماتی مورد نظر حذف شد");
                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف کارمند دفتر خدماتی", ex);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion
    }
}
