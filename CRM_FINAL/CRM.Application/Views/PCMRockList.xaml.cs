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
    public partial class PCMRockList : Local.TabWindow
    {
        #region Constructor & Fields

        public PCMRockList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            CenterColumn.ItemsSource = Data.CenterDB.GetCenters();
        
        }
        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
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

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;


            Pager.TotalRecords = Data.PCMRockDB.SearchPCMRockCount(
            CenterUserControl.CityComboBox.SelectedIDs,
            CenterUserControl.CenterComboBox.SelectedIDs,
            RockCheckableComboBox.SelectedIDs
            );


            ItemsDataGrid.ItemsSource = Data.PCMRockDB.SearchPCMRock(
            CenterUserControl.CityComboBox.SelectedIDs,
            CenterUserControl.CenterComboBox.SelectedIDs,
            RockCheckableComboBox.SelectedIDs
            , startRowIndex
                , pageSize
                                      );
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PCMRock item = ItemsDataGrid.SelectedItem as PCMRock;

                    DB.Delete<Data.PCMRock>(item.ID);
                    ShowSuccessMessage("رک مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف رک", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PCMRock item = ItemsDataGrid.SelectedItem as Data.PCMRock;
                if (item == null) return;

                PCMRockForm window = new PCMRockForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.PCMRock item = e.Row.Item as Data.PCMRock;

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("رک مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره رک", ex);
            }

        }

          private void AddItem(object sender, RoutedEventArgs e)
        {
            PCMRockForm window = new PCMRockForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);

        }

          private void CenterUserControl_DoCenterComboBoxLostFocus_1()
          {
              RockCheckableComboBox.ItemsSource = Data.PCMRockDB.GetPCMRockCheckableByCenterIDs(CenterUserControl.CenterComboBox.SelectedIDs);
          }
        #endregion Event Handlers

       

     
   
       
      
    }
}
