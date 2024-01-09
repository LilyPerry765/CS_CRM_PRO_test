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
    public partial class SpaceandPower : UserControl
    {

        #region Properties

        private long _ReqID = 0;
        private long _CustomerID = 0;

        private Data.SpaceAndPower _SpaceAndPower { get; set; }
        private Request _Request { get; set; }

        public Customer Customer { get; set; }

        public long TelephoneNo { get; set; }

        private int _centerID = 0;
        public int CenterID
        {
            set { _centerID = value; }
        }

        #endregion

        #region Costructors

        public SpaceandPower()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        public SpaceandPower(long id)
            : this()
        {
            _ReqID = id;

            LoadData(null, null);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            SpaceTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.SpaceType));
            PowerTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PowerType));
            EquipmentTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.EquipmentType));


            //ServiceTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLServiceType));
            //ServiceCostComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLServiceCostPaymentType));
            //CustomerPriorityComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLCustomerPriority));
            //RegisterProjectTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLRegistrationProjectType));
            //ModemTypeComboBox.ItemsSource = DB.GetAllEntity<ADSLModem>().ToList();
            //TariffComboBox.ItemsSource = DB.GetAllEntity<ADSLTariff>().ToList();
        }

        private void DisableControls()
        {
            //ADSLOwnerStatusComboBox.IsEnabled = false;
            //NationalCodeTextBox.IsReadOnly = true;
            //CustomerNameTextBox.IsReadOnly = true;
            //searchButton.Visibility = Visibility.Collapsed;
            //CustomerNameRow.Width = new GridLength(180);
            //ServiceTypeComboBox.IsEnabled = false;
            //ServiceCostComboBox.IsEnabled = false;
            //CustomerPriorityComboBox.IsEnabled = false;
            //RequiredInstalationCheckBox.IsEnabled = false;
            //NeedModemCheckBox.IsEnabled = false;
            //ModemTypeComboBox.IsEnabled = false;
            //RegisterProjectTypeComboBox.IsEnabled = false;
            //TariffComboBox.IsEnabled = false;
            //LicenceLetterNoTextBox.IsReadOnly = true;
            //CustomerEndRentDate.IsEnabled = false;
        }

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            if (_ReqID == 0)
            {
                _Request = new Request();
                _SpaceAndPower = new SpaceAndPower();
            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID(_ReqID);
                _SpaceAndPower = DB.SearchByPropertyName<CRM.Data.SpaceAndPower>("ID", _ReqID).SingleOrDefault();
                if (_SpaceAndPower != null)
                {
                    Antenna _antenna = SpaceAndPowerDB.GetAntennaBySpaceAndPowerId(_SpaceAndPower.ID);
                    AntennaInfoGrid.DataContext = _antenna;
                }
                Customer = Data.CustomerDB.GetCustomerByID(_Request.CustomerID ?? 0);
                NationalCodeTextBox.Text = Customer.NationalCodeOrRecordNo;
                CustomerNameTextBox.Text = Customer.FirstNameOrTitle + " " + Customer.LastName;
                SpaceAnfPowerInfo.DataContext = _SpaceAndPower;
            }
        }

        private void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CustomerNameTextBox.Text = string.Empty;

                //if (Data.CustomerDB.GetCustomerByNationalCodeCount(NationalCodeTextBox.Text.Trim()) > 1)
                //{ MessageBox.Show("چند مشترک با این کد ملی یافت شد. ابتدا اطلاعات مشترک را اصلاح کنید"); return; }

                if (Data.BlackListDB.ExistNationalCodeInBlackList(NationalCodeTextBox.Text.Trim()))
                {
                    Folder.MessageBox.ShowError("کد ملی در لیست سیاه قرار دارد");
                }
                else
                {
                    Customer = Data.CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
                    if (Customer != null)
                    {

                        _Request.CustomerID = Customer.ID;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                    }
                    else
                    {
                        CustomerForm customerForm = new CustomerForm();
                        customerForm.CenterID = _centerID;
                        customerForm.ShowDialog();
                        if (customerForm.DialogResult ?? false)
                        {
                            Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                            _Request.CustomerID = Customer.ID;
                            CustomerNameTextBox.Text = string.Empty;
                            CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                        }
                    }
                }
            }
            else
            {
                CustomerForm customerForm = new CustomerForm();
                customerForm.CenterID = _centerID;
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {
                    Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                    _Request.CustomerID = Customer.ID;
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle ?? "" + " " + Customer.LastName ?? "";
                }
            }


            if (Customer != null)
            {
                (Window.GetWindow(this) as CRM.Application.Views.RequestForm).RequesterNametextBox.Text = Customer.FirstNameOrTitle + " " + Customer.LastName;
            }
            //(this.Parent as CRM.Application.Views.RequestForm).RequesterNametextBox.Text = Customer.FirstNameOrTitle + " " + Customer.LastName;


        }

        private void EditSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (Customer != null)
            {
                CustomerForm window = new CustomerForm(Customer.ID);
                window.ShowDialog();
            }
        }

        private void AntennaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AntennaInfoGrid.Visibility = Visibility.Visible;
        }

        private void AntennaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            AntennaInfoGrid.Visibility = Visibility.Collapsed;
        }

        private void HasFibreCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _SpaceAndPower.HasFibre = true;
        }

        private void HasFibreCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            _SpaceAndPower.HasFibre = false;
        }

        #endregion

    }
}
