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
    public partial class PAPInfoCostList : Local.TabWindow
    {
        #region Propperties

        #endregion

        #region Constructors

        public PAPInfoCostList()
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
            ItemsDataGrid.ItemsSource = Data.PAPInfoCostDB.SearchPAPInfoCost(TitleTextBox.Text).OrderBy(t => t.CostID);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            TitleTextBox.Text = string.Empty;

            Search(null, null);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            PAPInfoCostForm Window = new PAPInfoCostForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PAPCostInfo item = ItemsDataGrid.SelectedItem as PAPCostInfo;

                if (item == null)
                    return;

                PAPInfoCostForm Window = new PAPInfoCostForm(item.CostID);
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
                    PAPInfoCost item = ItemsDataGrid.SelectedItem as PAPInfoCost;
                    DB.Delete<PAPInfoCost>(item.ID);

                    ShowSuccessMessage("هزینه شرکت PAP مورد نظر حذف شد");

                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف هزینه شرکت PAP", ex);
            }
        }

        #endregion
    }
}
