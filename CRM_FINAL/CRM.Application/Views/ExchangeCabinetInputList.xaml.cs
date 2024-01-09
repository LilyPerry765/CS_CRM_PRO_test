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
    public partial class ExchangeCabinetInputList : Local.TabWindow
    {
        #region Constructor & Fields

        public ExchangeCabinetInputList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
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
            int pageSize      = Pager.PageSize;
            Pager.TotalRecords =  Data.ExchangeCenralCableCabinetDB.SearchExchangeCenralCableCabinetCount(
                CityComboBox.SelectedIDs,
                CenterComboBox.SelectedIDs,
                OldCabinetIDComboBox.SelectedIDs,
                NewCabinetIDComboBox.SelectedIDs,
                FromOldCabinetInputIDComboBox.SelectedIDs,
                ToOldCabinetInputIDComboBox.SelectedIDs,
                FromNewCabinetInputIDComboBox.SelectedIDs,
                ToNewCabinetInputIDComboBox.SelectedIDs,
                AccomplishmentDateDate.SelectedDate,
                RequestLetterNoTextBox.Text,
                AccomplishmentTimeTextBox.Text
            );
           ItemsDataGrid.ItemsSource = Data.ExchangeCenralCableCabinetDB.SearchExchangeCenralCableCabinet(
               CityComboBox.SelectedIDs,
               CenterComboBox.SelectedIDs,
                OldCabinetIDComboBox.SelectedIDs,
                NewCabinetIDComboBox.SelectedIDs,
                FromOldCabinetInputIDComboBox.SelectedIDs,
                ToOldCabinetInputIDComboBox.SelectedIDs,
                FromNewCabinetInputIDComboBox.SelectedIDs,
                ToNewCabinetInputIDComboBox.SelectedIDs,
                AccomplishmentDateDate.SelectedDate,
                RequestLetterNoTextBox.Text,
                AccomplishmentTimeTextBox.Text,
                startRowIndex,
                pageSize     
            );
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            //if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            //try
            //{
            //    MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //    if (result == MessageBoxResult.Yes)
            //    {
            //        CRM.Data.RoundList item = ItemsDataGrid.SelectedItem as CRM.Data.RoundList;

            //        DB.Delete<Data.ExchangeCenralCableCabinet>(item.ID);
            //        ShowSuccessMessage("ExchangeCenralCableCabinet مورد نظر حذف شد");
            //        LoadData();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ShowErrorMessage("خطا در حذف ExchangeCenralCableCabinet", ex);
            //}
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CRM.Data.ExchangeCenralCableCabinetDB.ExchangeCenralCableCabinetInfo item = ItemsDataGrid.SelectedItem as CRM.Data.ExchangeCenralCableCabinetDB.ExchangeCenralCableCabinetInfo;
                if (item == null) return;

                ExchangeCabinetInputForm window = new ExchangeCabinetInputForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //try
            //{
            //    Data.ExchangeCenralCableCabinet item = e.Row.Item as Data.ExchangeCenralCableCabinet;

            //    item.Detach();
            //    DB.Save(item);

            //    Search(null, null);

            //    ShowSuccessMessage("ExchangeCenralCableCabinet مورد نظر ذخیره شد");
            //}
            //catch (Exception ex)
            //{
            //    e.Cancel = true;
            //    ShowErrorMessage("خطا در ذخیره ExchangeCenralCableCabinet", ex);
            //}
        }
        #endregion Event Handlers

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            ExchangeCabinetInputForm window = new ExchangeCabinetInputForm((int)DB.RequestType.ExchangeCabinetInput);
            window.ShowDialog();


            if (window.DialogResult == true)
                Search(null, null);
        }

        private void Forward(object sender, RoutedEventArgs e)
        {

        }
        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
         OldCabinetIDComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
         NewCabinetIDComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        private void OldCabinetIDComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            FromOldCabinetInputIDComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetIDs(OldCabinetIDComboBox.SelectedIDs);
            ToOldCabinetInputIDComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetIDs(OldCabinetIDComboBox.SelectedIDs);
        }

        private void NewCabinetIDComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            FromNewCabinetInputIDComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetIDs(NewCabinetIDComboBox.SelectedIDs);
            ToNewCabinetInputIDComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetIDs(NewCabinetIDComboBox.SelectedIDs);
        }
    }
}
