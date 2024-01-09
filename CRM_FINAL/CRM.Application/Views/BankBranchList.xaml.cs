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
    public partial class BankBranchList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public BankBranchList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            BankNameComboBox.ItemsSource = Data.BankDB.GetBanksCheckable();
            BankNameColumn.ItemsSource = Data.BankDB.GetBanksCheckable();
        }

        private void Loaddata()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Loaddata();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {            
            ItemsDataGrid.ItemsSource = BankBranchDB.SearchBankBranch(StatusCehckBox.IsChecked, BankNameComboBox.SelectedIDs, BranchCodeTextBox.Text.Trim(), BranchNameTextBox.Text.Trim(), AccountNoTextBox.Text.Trim());
        }
        
        private void AddItem(object sender, RoutedEventArgs e)
        {
            BankBranchForm window = new BankBranchForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Loaddata();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                BankBranch item = ItemsDataGrid.SelectedItem as BankBranch;
                if (item == null) 
                    return;

                BankBranchForm window = new BankBranchForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Loaddata();
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
                    BankBranch item = ItemsDataGrid.SelectedItem as BankBranch;
                    DB.Delete<BankBranch>(item.ID);

                    ShowSuccessMessage("شعبه بانک مورد نظر حذف شد");
                    Loaddata();
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف شعبه بانک", ex);
            }
        }

        #endregion event Handler
    }
}
