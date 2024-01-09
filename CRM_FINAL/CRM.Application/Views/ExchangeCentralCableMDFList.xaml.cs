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
    public partial class ExchangeCentralCableMDFList : Local.TabWindow
    {
        #region Constructor & Fields

        public ExchangeCentralCableMDFList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {

            MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckable();
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckable();



            StatusComboBox.ItemsSource = Data.WorkFlowDB.GetRequestStepsCheckable(10);
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

            Pager.TotalRecords = Data.ExchangeCenralCableMDFDB.SearchExchangeCenralCableMDFCount(MDFComboBox.SelectedIDs, CabinetComboBox.SelectedIDs, StatusComboBox.SelectedIDs, RequestLetterNoTextBox.Text);

            ItemsDataGrid.ItemsSource = Data.ExchangeCenralCableMDFDB.SearchExchangeCenralCableMDF(MDFComboBox.SelectedIDs, CabinetComboBox.SelectedIDs, StatusComboBox.SelectedIDs, RequestLetterNoTextBox.Text, startRowIndex, pageSize);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            //if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            //try
            //{
            //    MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //    if (result == MessageBoxResult.Yes)
            //    {
            //        CRM.Data.ExchangeCenralCableMDF item = ItemsDataGrid.SelectedItem as CRM.Data.ExchangeCenralCableMDF;

            //        DB.Delete<Data.ExchangeCenralCableMDF>(item.ID);
            //        ShowSuccessMessage("ExchangeCenralCableMDF مورد نظر حذف شد");
            //        LoadData();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ShowErrorMessage("خطا در حذف ExchangeCenralCableMDF", ex);
            //}
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ExchangeCenralCableMDFInfo item = ItemsDataGrid.SelectedItem as Data.ExchangeCenralCableMDFInfo;
                if (item == null) return;

                ExchangeCentralCableMDFForm window = new ExchangeCentralCableMDFForm(item.ID);

                    window.ShowDialog();
             

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //try
            //{
            //    Data.ExchangeCenralCableMDF item = e.Row.Item as Data.ExchangeCenralCableMDF;

            //    item.Detach();
            //    DB.Save(item);

            //    Search(null, null);

            //    ShowSuccessMessage("ExchangeCenralCableMDF مورد نظر ذخیره شد");
            //}
            //catch (Exception ex)
            //{
            //    e.Cancel = true;
            //    ShowErrorMessage("خطا در ذخیره ExchangeCenralCableMDF", ex);
            //}
        }
        #endregion Event Handlers

        private void NewClick(object sender, RoutedEventArgs e)
        {


                ExchangeCentralCableMDFForm window = new ExchangeCentralCableMDFForm((int)Data.DB.RequestType.ExchangeCenralCableMDF);
                window.ShowDialog();
                Search(null, null);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }
    }
}
