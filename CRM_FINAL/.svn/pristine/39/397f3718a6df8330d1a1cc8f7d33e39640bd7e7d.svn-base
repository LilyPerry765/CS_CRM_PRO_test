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
    public partial class WorkUnitList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public WorkUnitList()
        {            
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        { 
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
            ItemsDataGrid.ItemsSource = Data.WorkUnitDB.SearchWorkUnit(WorkUnitNameTextBox.Text.Trim());
        }

        private void ResetSearchList(object sender, RoutedEventArgs e)
        {
            WorkUnitNameTextBox.Text = string.Empty;

            Search(null, null);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            WorkUnitForm Window = new WorkUnitForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                WorkUnit item = ItemsDataGrid.SelectedItem as WorkUnit;

                if (item == null)
                    return;

                WorkUnitForm Window = new WorkUnitForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewPlaceholder}") return;
            
            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    WorkUnit item = ItemsDataGrid.SelectedItem as WorkUnit;
                    DB.Delete<Data.WorkUnit>(item.ID);
                    
                    ShowSuccessMessage("واحد مسئول مورد نظر حذف شد");
                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف واحد مسئول", ex);
            }
        }

        #endregion
    }
}
