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
using System.Threading.Tasks;
using Enterprise;

namespace CRM.Application.Views
{
    public partial class RoundList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public RoundList()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

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
            sbyte status = -1;
            if (!string.IsNullOrWhiteSpace(statusTextBox.Text)) status = Convert.ToSByte(statusTextBox.Text);

            long insertUserId = -1;
            if (!string.IsNullOrWhiteSpace(insertUserIdTextBox.Text)) insertUserId = Convert.ToInt64(insertUserIdTextBox.Text);

            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelNoTextBox.Text)) telephoneNo = Convert.ToInt64(TelNoTextBox.Text);

            ItemsDataGrid.ItemsSource = RoundListDB.SearchRound(status, entryDateDatePicker.SelectedDate, startDateDatePicker.SelectedDate, endDateDatePicker.SelectedDate, isSelectableCheckBox.IsChecked, telephoneNo, insertUserId);

        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            RoundForm window = new RoundForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Data.RoundSaleInfo item = ItemsDataGrid.SelectedItem as Data.RoundSaleInfo;
                if (item == null) return;

                RoundForm window = new RoundForm(item.ID);
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
                    CRM.Data.RoundSaleInfo item = ItemsDataGrid.SelectedItem as CRM.Data.RoundSaleInfo;

                    DB.Delete<Data.RoundSaleInfo>(item.ID);
                    ShowSuccessMessage("تلفن مورد نظر حذف شد");
                    LoadData();
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage(" مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد ", ex);
                Logger.Write(ex);

            }

            catch (Exception ex)
            {
                ShowErrorMessage(" خطا در حذف تلفن رند", ex);
                Logger.Write(ex);
            }
        }

        #endregion
    }
}
