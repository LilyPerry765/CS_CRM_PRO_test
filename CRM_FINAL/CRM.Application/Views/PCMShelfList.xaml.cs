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
    public partial class PCMShelfList : Local.TabWindow
    {
        #region Constructor & Fields

        public PCMShelfList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            PCMRockColumn.ItemsSource = Data.PCMRockDB.GetPCMRockCheckable();
            CitiesCheckableComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion Load Methods

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
        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }
        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = 0;
            int number = -1;
            if (!int.TryParse(NumberTextBox.Text.Trim(), out number)) { number = -1; };

            //Pager.TotalRecords = Data.PCMShelfDB.SearchPCMShelfCount(
            //CenterUserControl.CityComboBox.SelectedIDs,
            //CenterUserControl.CenterComboBox.SelectedIDs,
            //PCMRockComboBox.SelectedIDs, number);

            ItemsDataGrid.ItemsSource = Data.PCMShelfDB.SearchPCMShelf(
                                                                        CitiesCheckableComboBox.SelectedIDs,
                                                                        CentersCheckableComboBox.SelectedIDs,
                                                                        PCMRockComboBox.SelectedIDs, number, startRowIndex, pageSize, out count
                                                                      );
            Pager.TotalRecords = count;
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.PCMShelf item = ItemsDataGrid.SelectedItem as CRM.Data.PCMShelf;

                    DB.Delete<Data.PCMShelf>(item.ID);
                    ShowSuccessMessage("شلف مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف شلف", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PCMShelf item = ItemsDataGrid.SelectedItem as Data.PCMShelf;
                if (item == null) return;

                PCMShelfForm window = new PCMShelfForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            PCMShelfForm window = new PCMShelfForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);

        }

        private void CitiesCheckableComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersCheckableComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CitiesCheckableComboBox.SelectedIDs);
        }

        private void CentersCheckableComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PCMRockComboBox.ItemsSource = PCMRockDB.GetPCMRockCheckableByCenterIDs(CentersCheckableComboBox.SelectedIDs);
        }

        #endregion Event Handlers


        #region Milad Doran

        //private void CenterUserControl_DoCenterComboBoxLostFocus_1()
        //{
        //    PCMRockComboBox.ItemsSource = Data.PCMRockDB.GetPCMRockCheckableByCenterIDs(CenterUserControl.CenterComboBox.SelectedIDs);
        //} 

        #endregion
    }
}
