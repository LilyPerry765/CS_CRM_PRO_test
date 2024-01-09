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
using CRM.Application.Views;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for ADSLChangeCustomerOwnerCharacteristicsUserControl.xaml
    /// </summary>
    public partial class ADSLChangeCustomerOwnerCharacteristicsUserControl : UserControl
    {

        #region Properties

        private long _RequsetID = 0;
        private long _CustomerID = 0;
        private int _CenterID = 0;
        private int _CityID = 0;

        private CRM.Data.ADSLRequest _ADSLRequest { get; set; }
        private Request _Request { get; set; }
        private Customer _Customer { get; set; }
        private Telephone _Telephone { get; set; }
        private Bucht _Bucht { get; set; }
        private Data.ADSL _ADSL { get; set; }

        public long TelephoneNo { get; set; }
        public bool _IsWaitingList { get; set; }
        public Customer ADSLCustomer { get; set; }
        public List<Customer> ADSLCustomerList { get; set; }
        public ADSLIP IPStatic { get; set; }
        public ADSLGroupIP GroupIPStatic { get; set; }
        private ADSLServiceInfo serviceInfo { get; set; }

        private Service1 service = new Service1();
        public System.Data.DataTable telephoneInfo { get; set; }

        public long _SumPriceService = 0;
        public long _SumPriceIP = 0;
        public long _SumPriceModem = 0;

        private int sellerAgentID { get; set; }
        private List<int> _ServiceAccessList { get; set; }
        private List<int> _ServiceGroupAccessList { get; set; }

        #endregion

        public ADSLChangeCustomerOwnerCharacteristicsUserControl()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        public ADSLChangeCustomerOwnerCharacteristicsUserControl(long requestID, long customerID, long telephoneNo)
            : this()
        {
            _RequsetID = requestID;
            _CustomerID = customerID;
            TelephoneNo = telephoneNo;

            LoadData(null, null);
        }

        #region Methods

        private void Initialize()
        {
            ADSLCustomer = new Customer();

            ADSLOwnerStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));
        }

        private void DisableControls()
        {
            ADSLOwnerStatusComboBox.IsEnabled = false;
            NationalCodeTextBox.IsReadOnly = true;
            CustomerNameTextBox.IsReadOnly = true;
            searchButton.Visibility = Visibility.Collapsed;
            CustomerNameRow.Width = new GridLength(180);
            CustomerEndRentDate.IsEnabled = false;
        }

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(TelephoneNo);
            if (telephone != null)
                _CenterID = telephone.CenterID;

            _CityID = CityDB.GetCityByCenterID(_CenterID).ID;

            if (_RequsetID == 0)
            {                
            }
            else
            {
                ADSLChangeCustomerOwnerCharacteristic adslChangeCustomer = new ADSLChangeCustomerOwnerCharacteristic();
                adslChangeCustomer = ADSLChangeCustomerOwnerCharacteristicsDB.GetADSLChangeCustomerOwnerCharacteristicsByID(_RequsetID);
                ADSLCustomer = Data.CustomerDB.GetCustomerByID((long)adslChangeCustomer.NewCustomerOwnerID);

                _Request = Data.RequestDB.GetRequestByID(_RequsetID);
                if (ADSLCustomer.NationalCodeOrRecordNo != null)
                    NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo;

                CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + " " + ADSLCustomer.LastName;
                ADSLOwnerStatusComboBox.SelectedValue = (byte)adslChangeCustomer.NewCustomerOwnerStatus;
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CustomerNameTextBox.Text = string.Empty;

                ADSLCustomer = null;
                ADSLCustomerList = null;
                ADSLCustomerList = CustomerDB.GetCustomerListByNationalCode(NationalCodeTextBox.Text.Trim());

                if (ADSLCustomerList != null)
                {
                    if (ADSLCustomerList.Count == 1)
                    {
                        ADSLCustomer = ADSLCustomerList[0];
                        NationalCodeTextBox.Text = string.Empty;
                        NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                    }
                    else
                    {
                        CustomerSearchForm customerSearchForm = new CustomerSearchForm(NationalCodeTextBox.Text.Trim());
                        customerSearchForm.ShowDialog();
                        if (customerSearchForm.DialogResult ?? false)
                        {
                            ADSLCustomer = Data.CustomerDB.GetCustomerByID(customerSearchForm.ID);

                            NationalCodeTextBox.Text = string.Empty;
                            NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo;
                            CustomerNameTextBox.Text = string.Empty;
                            CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                        }
                    }
                }
                else
                {
                    CustomerSearchForm customerSearchForm = new CustomerSearchForm();
                    customerSearchForm.ShowDialog();
                    if (customerSearchForm.DialogResult ?? false)
                    {
                        ADSLCustomer = Data.CustomerDB.GetCustomerByID(customerSearchForm.ID);

                        NationalCodeTextBox.Text = string.Empty;
                        NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                    }
                }

            }
            else
            {
                CustomerSearchForm customerSearchForm = new CustomerSearchForm();
                customerSearchForm.ShowDialog();
                if (customerSearchForm.DialogResult ?? false)
                {
                    ADSLCustomer = Data.CustomerDB.GetCustomerByID(customerSearchForm.ID);
                    NationalCodeTextBox.Text = string.Empty;
                    NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo.ToString();
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                }
            }
        }

        private void EditCustomerButto_Click(object sender, RoutedEventArgs e)
        {
            if (ADSLCustomer != null)
            {
                CustomerForm window = new CustomerForm(ADSLCustomer.ID);
                window.ShowDialog();
            }
        }

        private void ADSLOwnerStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ADSLOwnerStatusComboBox.SelectedValue != null)
            {
                if ((byte)ADSLOwnerStatusComboBox.SelectedValue == (byte)DB.ADSLOwnerStatus.Owner)
                {
                    CustomerEndRentLabel.Visibility = Visibility.Collapsed;
                    CustomerEndRentDate.Visibility = Visibility.Collapsed;
                }
                else
                {
                    NationalCodeTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = string.Empty;
                    if ((byte)ADSLOwnerStatusComboBox.SelectedValue == (byte)DB.ADSLOwnerStatus.Tenant)
                    {
                        CustomerEndRentDate.SelectedDate = null;
                        CustomerEndRentLabel.Visibility = Visibility.Visible;
                        CustomerEndRentDate.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CustomerEndRentDate.SelectedDate = null;
                        CustomerEndRentLabel.Visibility = Visibility.Collapsed;
                        CustomerEndRentDate.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        #endregion

    }

}
