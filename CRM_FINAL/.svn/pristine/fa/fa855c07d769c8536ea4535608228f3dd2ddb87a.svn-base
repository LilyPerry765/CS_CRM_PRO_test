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
    public partial class QuotaDiscountList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public QuotaDiscountList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RequestTypeColumn.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();
            QuotaJobTitleColumn.ItemsSource = Data.QuotaJobTitleDB.GetQuotaJobTitleCheckable();
            AnnounceColumn.ItemsSource = Data.AnnounceDB.GetAnnounceCheckable();
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
            int discountAmount = -1;
            if (!string.IsNullOrWhiteSpace(DiscountAmountTextBox.Text)) discountAmount = Convert.ToInt32(DiscountAmountTextBox.Text);       

            ItemsDataGrid.ItemsSource = Data.QuotaDiscountDB.SearchQuotaDiscount(discountAmount, RequestTypeComboBox.SelectedIDs, JobTitleComboBox.SelectedIDs, AnnounceComboBox.SelectedIDs, FromStartDate.SelectedDate, ToStartDate.SelectedDate, FromEndDate.SelectedDate, ToEndDate.SelectedDate);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            QuotaDiscountForm window = new QuotaDiscountForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                QuotaDiscount item = ItemsDataGrid.SelectedItem as Data.QuotaDiscount;
                if (item == null) return;

                QuotaDiscountForm window = new QuotaDiscountForm(item.ID);
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
                    QuotaDiscount item = ItemsDataGrid.SelectedItem as QuotaDiscount;

                    DB.Delete<Data.QuotaDiscount>(item.ID);
                    ShowSuccessMessage("سهمیه تخفیف مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف سهمیه تخفیف", ex);
            }
        }

        #endregion Event Handlers
    }
}
