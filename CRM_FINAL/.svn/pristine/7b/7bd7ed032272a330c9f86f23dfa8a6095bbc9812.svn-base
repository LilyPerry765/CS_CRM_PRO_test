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
    public partial class ADSLTariffList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public ADSLTariffList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceType));
            GroupComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceGroupCheckable();
            TypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceType));
            GroupColumn.ItemsSource = Data.ADSLServiceDB.GetADSLServiceGroupCheckable();
            BandWidthComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceBandWidthCheckable();
            BandWidthColumn.ItemsSource = Data.ADSLServiceDB.GetADSLServiceBandWidthCheckable();
            TrafficComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceTrafficCheckable();
            TrafficColumn.ItemsSource = Data.ADSLServiceDB.GetADSLServiceTrafficCheckable();
            DurationComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceDurationCheckable();
            DurationColumn.ItemsSource = Data.ADSLServiceDB.GetADSLServiceDurationCheckable();
            //GiftProfileColumn.ItemsSource = Data.ADSLServiceDB.GetADSLServiceGiftProfileCheckable();
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void Search(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int totalRecords = 0;

            long? serviceCode = 0;
            if (!Helper.CheckDigitDataEntry(ServiceCodeTextBox, out serviceCode)) //آیا سرویس کد با یک مقدار عددی پر شده است یا خیر
            {
                serviceCode = null;
            }
            long price = -1;
            if (!string.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                price = Convert.ToInt32(PriceTextBox.Text);
            }

            ItemsDataGrid.ItemsSource = Data.ADSLServiceDB.SearchADSLService(
                                                                             TitleTextBox.Text, TypeComboBox.SelectedIDs, GroupComboBox.SelectedIDs,
                                                                             price, BandWidthComboBox.SelectedIDs, TrafficComboBox.SelectedIDs,
                                                                             DurationComboBox.SelectedIDs, IsRequiredLicenseCheckBox.IsChecked, IsOnlineRegisterCheckBox.IsChecked,
                                                                             HasModemCheckBox.IsChecked, IsActiveCheckBox.IsChecked, FromStartDate.SelectedDate,
                                                                             UntilStartDate.SelectedDate, FromEndDate.SelectedDate, UntilEndDate.SelectedDate,
                                                                             (int?)serviceCode, out totalRecords, startRowIndex, pageSize
                                                                            );
            Pager.TotalRecords = totalRecords;

            this.Cursor = Cursors.Arrow;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            TitleTextBox.Text = string.Empty;
            TypeComboBox.Reset();
            GroupComboBox.Reset();
            PriceTextBox.Text = string.Empty;
            BandWidthComboBox.Reset();
            TrafficComboBox.Reset();
            DurationComboBox.Reset();
            IsRequiredLicenseCheckBox.IsChecked = null;
            HasModemCheckBox.IsChecked = null;
            IsActiveCheckBox.IsChecked = null;
            FromStartDate.SelectedDate = null;
            UntilStartDate.SelectedDate = null;
            FromEndDate.SelectedDate = null;
            UntilEndDate.SelectedDate = null;
            ServiceCodeTextBox.Text = string.Empty;

            Search(null, null);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSlTariffForm window = new ADSlTariffForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLService service = ItemsDataGrid.SelectedItem as ADSLService;

                if (service == null)
                    return;

                ADSlTariffForm window = new ADSlTariffForm(service.ID);
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
                    ADSLService item = ItemsDataGrid.SelectedItem as ADSLService;

                    DB.Delete<Data.ADSLService>(item.ID);
                    ShowSuccessMessage("تعرفه مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف تعرفه", ex);
            }
        }

        private void GetCenterAvalibility(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLService service = ItemsDataGrid.SelectedItem as ADSLService;

                if (service == null)
                    return;

                ADSLServiceCenterForm window = new ADSLServiceCenterForm(service.ID, (byte)DB.ADSLServiceCenterMode.Service);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void GetADSLSellerAvalibility(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLService service = ItemsDataGrid.SelectedItem as ADSLService;

                if (service == null)
                    return;

                ADSLServiceSellerForm window = new ADSLServiceSellerForm(service.ID, (byte)DB.ADSLServiceSellerMode.Service);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion
    }
}
