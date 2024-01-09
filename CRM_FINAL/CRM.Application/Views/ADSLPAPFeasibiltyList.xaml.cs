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
    public partial class ADSLPAPFeasibiltyList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public ADSLPAPFeasibiltyList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PAPInfoComboBox.ItemsSource = PAPInfoDB.GetPAPInfoCheckable();
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.FeasibilityStatus));
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
            PAPInfoComboBox.Reset();
            CityComboBox.Reset();
            TelephoneNoTextBox.Text = string.Empty;
            Date.SelectedDate = null;
            StatusComboBox.Reset();

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

            DateTime? date = null;
            if (Date.SelectedDate != null)
                date = Date.SelectedDate.Value.Date;

            Pager.TotalRecords = Data.ADSLPAPFeasibilityDB.SearchPAPFeasibiltiesCount(PAPInfoComboBox.SelectedIDs, CityComboBox.SelectedIDs, telephoneNo, date, StatusComboBox.SelectedIDs);
            ItemsDataGrid.ItemsSource = Data.ADSLPAPFeasibilityDB.SearchPAPFeasibilties(PAPInfoComboBox.SelectedIDs, CityComboBox.SelectedIDs, telephoneNo, date, StatusComboBox.SelectedIDs, startRowIndex, pageSize);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLPAPPortForm window = new ADSLPAPPortForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PAPPortInfo item = ItemsDataGrid.SelectedItem as PAPPortInfo;
                if (item == null) return;

                ADSLPAPPortForm window = new ADSLPAPPortForm(item.ID);
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
                    PAPPortInfo item = ItemsDataGrid.SelectedItem as PAPPortInfo;

                    DB.Delete<Data.ADSLPAPPort>(item.ID);
                    ShowSuccessMessage("پورت مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف استان", ex);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion
    }
}
