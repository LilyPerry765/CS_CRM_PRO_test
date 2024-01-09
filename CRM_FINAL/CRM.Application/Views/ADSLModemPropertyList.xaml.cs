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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Data;

namespace CRM.Application.Views
{    
    public partial class ADSLModemPropertyList : Local.TabWindow
    {
        #region Properties
        #endregion

        #region Constructor

        public ADSLModemPropertyList()
        {
            InitializeComponent();

            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            ModelComboBox.ItemsSource = ADSLModemDB.GetModemMOdelsCheckable();
            SoldComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLModemStatus));
        }

        public void LoadData()
        {
            Search(null, null);
        }


        #endregion

        #region EventHandlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            CityComboBox.Reset();
            ModelComboBox.Reset();
            SerialNoTextBox.Text = string.Empty;
            TelephoneNoTextBox.Text = string.Empty;
            SoldComboBox.Reset();

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;           

            Pager.TotalRecords = ADSLModemPropertyDB.SearchADSLModemPropertiesCount(CityComboBox.SelectedIDs, ModelComboBox.SelectedIDs, TelephoneNoTextBox.Text.Trim(), SerialNoTextBox.Text.Trim(), MACAddressTextBox.Text.Trim(), SoldComboBox.SelectedIDs);
            ItemsDataGrid.ItemsSource = ADSLModemPropertyDB.SearchADSLModemProperties(CityComboBox.SelectedIDs, ModelComboBox.SelectedIDs, TelephoneNoTextBox.Text.Trim(), SerialNoTextBox.Text.Trim(), MACAddressTextBox.Text.Trim(), SoldComboBox.SelectedIDs, startRowIndex, pageSize);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLModemPropertyForm window = new ADSLModemPropertyForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void AddGroupItems(object sender, RoutedEventArgs e)
        {
            ADSLModemPropertyGroupForm window = new ADSLModemPropertyGroupForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLModemPropertyInfo itemInfo = ItemsDataGrid.SelectedItem as ADSLModemPropertyInfo;
                if (itemInfo == null) return;

                ADSLModemProperty item = new ADSLModemProperty();
                item.CenterID = CenterDB.GetCenterIDByName(itemInfo.CenterName);
                item.ADSLModemID = ADSLModemDB.GetModemIDByModel(itemInfo.ModemModel);
                if (itemInfo.Status == "فروخته شده")
                    item.Status = 1;
                else if (itemInfo.Status == "فروخته نشده")
                    item.Status = 2;
                else
                    item.Status = null;

                item.ID = itemInfo.ID;
                item.SerialNo = itemInfo.SerialNo;

                ADSLModemPropertyForm window = new ADSLModemPropertyForm(item.ID);
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
                    ADSLModemPropertyInfo item = ItemsDataGrid.SelectedItem as ADSLModemPropertyInfo;

                    DB.Delete<ADSLModemProperty>(item.ID);
                    ShowSuccessMessage("مودم مورد نظر حذف شد");
                    LoadData();
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد !", ex);
            }

            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف مودم", ex);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void EditCenterItem(object sender, RoutedEventArgs e)
        {
            ADSLModemPropertyChangeCenterForm window = new ADSLModemPropertyChangeCenterForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void AddGroupItem(object sender, RoutedEventArgs e)
        {
            ADSLModemPropertyFileForm window = new ADSLModemPropertyFileForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        #endregion        
    }
}
