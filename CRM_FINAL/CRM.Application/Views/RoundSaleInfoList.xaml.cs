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
    public partial class RoundSaleInfoList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public RoundSaleInfoList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods
        
        private void Initialize()
        {
            RoundTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.RoundType));
            RoundTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.RoundType));

        }

        #endregion Load Methods

        #region Event Handlers
        
        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Search(null,null);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
          
            long basePrice = -1;
            if (!string.IsNullOrWhiteSpace(BasePriceTextBox.Text)) basePrice = Convert.ToInt64(BasePriceTextBox.Text);

            ItemsDataGrid.ItemsSource = Data.RoundSaleInfoDB.SearchRoundSaleInfo(RoundTypeComboBox.SelectedIDs, EntryDateDate.SelectedDate, StartDateDate.SelectedDate, EndDateDate.SelectedDate, IsAuctionCheckBox.IsChecked, IsActiveCheckBox.IsChecked, basePrice);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            RoundSaleInfoForm window = new RoundSaleInfoForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                RoundSaleInfo item = ItemsDataGrid.SelectedItem as Data.RoundSaleInfo;
                if (item == null) return;

                RoundSaleInfoForm window = new RoundSaleInfoForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
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
                    CRM.Data.RoundSaleInfo  item = ItemsDataGrid.SelectedItem as CRM.Data.RoundSaleInfo;

                  
                    List<TelRoundSale> TelRoundSaleList = new List<TelRoundSale>();
                    TelRoundSaleList = Data.TelRoundSaleDB.GetTelRoundSaleByRoundSaleInfoID(item.ID);
                    List<long> DelTelRoundSaleList = new List<long>();
                    foreach(TelRoundSale tel in TelRoundSaleList)
                    {
                        DelTelRoundSaleList.Add(tel.ID);
                    }
                    if (TelRoundSaleList.Count != 0)
                        DB.DeleteAll<Data.TelRoundSale>(DelTelRoundSaleList);
                    DB.Delete<Data.RoundSaleInfo>(item.ID);
                    ShowSuccessMessage("مزایده تلفن رند مورد نظر حذف شد");
                    Search(null, null);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف مزایده تلفن رند", ex);
            }
        }

        #endregion
    }
}
