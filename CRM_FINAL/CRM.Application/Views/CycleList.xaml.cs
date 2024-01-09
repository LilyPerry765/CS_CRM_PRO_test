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
    public partial class CycleList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public CycleList()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Methods

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion Methods

        #region Event Handler

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
            ItemsDataGrid.ItemsSource = Data.CycleDB.SearchCycle(FromDateDataPicker.SelectedDate, ToDateDataPicker.SelectedDate, DueDateDataPicker.SelectedDate, CycleYearTextBox.Text, CycleNameTextBox.Text);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            CycleForm Window = new CycleForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Cycle item = ItemsDataGrid.SelectedItem as Cycle;
                
                if (item == null) 
                    return;

                CycleForm Window = new CycleForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
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
                    Cycle item = ItemsDataGrid.SelectedItem as Cycle;


                    DB.Delete<Cycle>(item.ID);
                    ShowSuccessMessage("دوره مورد نظر حذف شد");
                    LoadData();
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف دوره ", ex);
            }
        }

        #endregion
    }
}
