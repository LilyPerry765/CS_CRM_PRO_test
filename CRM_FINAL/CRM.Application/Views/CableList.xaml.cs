using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class CableList : Local.TabWindow
    {
        #region Constructor & Fields

        public CableList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            CableTypeComboBox.ItemsSource = Data.CableTypeDB.GetCableTypeCheckable();
            CableUsedChannelComboBox.ItemsSource = Data.CableUsedChannelDB.GetCableUsedChannelCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CableStatus));
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
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
            int count = 0;
            decimal cableDiameter = -1;
            if (!string.IsNullOrWhiteSpace(CableDiameterTextBox.Text)) cableDiameter = Convert.ToDecimal(CableDiameterTextBox.Text);

            ItemsDataGrid.ItemsSource = Data.CableDB.SearchCable
                (
                     CityComboBox.SelectedIDs,
                     CenterComboBox.SelectedIDs,
                     CableNumberComboBox.SelectedIDs_l,
                     CableTypeComboBox.SelectedIDs,
                     CableUsedChannelComboBox.SelectedIDs,
                     cableDiameter,
                     StatusComboBox.SelectedIDs,
                     startRowIndex,
                     pageSize,
                     out count
                );
            Pager.TotalRecords = count;
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("با حذف کابل، زوج کابل های آزاد حذف می شوند. آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.CableInfo item = ItemsDataGrid.SelectedItem as CRM.Data.CableInfo;
                    List<CablePair> cablePairs = Data.CablePairDB.GetCablePairByCableID(item.ID);

                    if (cablePairs.Any(t => t.CabinetInputID != null)) { MessageBox.Show("امکان حذف کابلی که به کافو متصل است نیست!"); return; }

                    List<Bucht> buchts = Data.BuchtDB.GetBuchtByCableID(item.ID);
                    if (buchts.Any(t => t.Status != (byte)DB.BuchtStatus.Free)) { MessageBox.Show("فقط کابل های آزاد را می توانید حذف کنید"); return; }

                    buchts.ForEach((Bucht buchtItem) => { buchtItem.CabinetInputID = null; buchtItem.CablePairID = null; buchtItem.Detach(); });

                    using (TransactionScope ts = new TransactionScope())
                    {
                        DB.UpdateAll(buchts);
                        DB.Delete<Data.Cable>(item.ID);

                        ts.Complete();
                    }

                    ShowSuccessMessage("کابل مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف کابل", ex);
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            CableForm window = new CableForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CableInfo item = ItemsDataGrid.SelectedItem as Data.CableInfo;
                if (item == null) return;

                CableForm window = new CableForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CableNumberComboBox.ItemsSource = Data.CableDB.GetCableCheckableByCenterID(CenterComboBox.SelectedIDs);
        }
        #endregion Event Handlers
    }
}
