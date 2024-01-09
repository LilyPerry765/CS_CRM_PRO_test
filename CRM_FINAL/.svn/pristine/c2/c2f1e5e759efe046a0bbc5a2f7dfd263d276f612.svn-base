using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;

namespace CRM.Application.Views
{
    public partial class FicheList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public FicheList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.FicheType));
            CentreComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();

            StatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.FicheType));
            CenterColumn.ItemsSource = Data.CenterDB.GetCenterCheckable();
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
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {            
            ItemsDataGrid.ItemsSource = Data.FicheDB.SearchFiche(FicheNameTextBox.Text, CentreComboBox.SelectedIDs, SaleStartDateDate.SelectedDate, SaleEndDateDate.SelectedDate, TransferStartDateDate.SelectedDate, TransferEndDateDate.SelectedDate, StatusComboBox.SelectedIDs);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            FicheForm window = new FicheForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Fiche item = ItemsDataGrid.SelectedItem as Data.Fiche;
                if (item == null) return;

                FicheForm window = new FicheForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
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
                    Fiche item = ItemsDataGrid.SelectedItem as Fiche;

                    DB.Delete<Data.Fiche>(item.ID);

                    ShowSuccessMessage("فیش مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف فیش", ex);
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.Fiche item = e.Row.Item as Data.Fiche;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("فیش مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره فیش", ex);
            }
        }
        
        #endregion Event Handlers
    }
}
