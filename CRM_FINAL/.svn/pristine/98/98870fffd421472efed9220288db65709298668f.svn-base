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
using System.Collections.ObjectModel;

namespace CRM.Application.UserControls
{
    public partial class InstallInfoSummary : UserControl
    {
        #region Properties

        public List<Address> adress = new List<Address>();

        public InstallRequest InstallRequest { get; set; }
        public List<UsedDocs> refDocs { get; set; }
        private Address _Address;
        public long _InstallAdrressID { get; set; }
        public long _CorrespondenceAddressID { get; set; }
        #endregion

        #region Constructors

        public InstallInfoSummary()
        {
            InitializeComponent();
        }

        public InstallInfoSummary(long id)
            : this()
        {
            Request request = Data.RequestDB.GetRequestByID(id);
            InstallRequest = Data.InstallRequestDB.GetInstallRequestByRequestID( id);


            refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == id && t.CustomerID == request.CustomerID).ToList();
            
            adress.Add(Data.AddressDB.GetAddressByID(InstallRequest.InstallAddressID ?? 0));
            adress.Add(Data.AddressDB.GetAddressByID(InstallRequest.CorrespondenceAddressID ?? 0));
            _InstallAdrressID = InstallRequest.InstallAddressID ?? 0;
            _CorrespondenceAddressID = InstallRequest.CorrespondenceAddressID ?? 0;
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ChargingTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ChargingGroup)); 
            ZeroBlockComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ClassTelephone));

            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();


            PosessionTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PossessionType));

            OrderTypecomboBox.ItemsSource = EnumTypeNameHelper.OrderTypeEnumValues;

            FicheSaletextBox.Text = Data.FicheDB.GetFicheByID(InstallRequest.SaleFicheID ?? 0).FicheName.ToString();


            if (refDocs.Where(t => t.IsRelatedTo3PercentQuota == true).Count() > 0)
                Use3PercentQuatacheckBox.IsChecked = true;
            else
                Use3PercentQuatacheckBox.IsChecked = false;

            if (refDocs.Where(t => t.IsRelatedToRoundContract == true).Count() > 0)
                IsRoundNumbercheckBox.IsChecked = true;
            else
                IsRoundNumbercheckBox.IsChecked = false;



            TelephoneTypeComboBox.SelectedValue = InstallRequest.TelephoneType;
            TelephoneTypecomboBox_SelectionChanged(null, null);
            TelephoneTypeGroupComboBox.SelectedValue = InstallRequest.TelephoneTypeGroup;
             
            AddressInfo.DataContext = adress;
            InstallInfo.DataContext = InstallRequest;

            _Address = Data.AddressDB.GetAddressByID(InstallRequest.InstallAddressID ?? 0);
            InstallAddresstextBox.Text = _Address.AddressContent;
            PostalCodeInstalltextBox.Text = _Address.PostalCode;

            _Address = Data.AddressDB.GetAddressByID(InstallRequest.CorrespondenceAddressID ?? 0);
            CorrespondenceAddressTextBox.Text = _Address.AddressContent;
            CorrespondencePostalCodeTextBox.Text = _Address.PostalCode;

        }

        private void CheckVisibility()
        {
            if ((TelephoneTypeComboBox.SelectedItem as CheckableItem).ID == (int)DB.TelephoneType.Temporary)
            {
                TelephoneForChargetextBox.Visibility = Visibility.Visible;
                TelUnInstallDate.Visibility = Visibility.Visible;
                TelInstallDate.Visibility = Visibility.Visible;

                TelephoneForChargeLabel.Visibility = Visibility.Visible;
                TelUnInstallLabel.Visibility = Visibility.Visible;
                TelInstallLabel.Visibility = Visibility.Visible;



                TelephoneForChargetextBox.IsEnabled = true;
                TelUnInstallDate.IsEnabled = true;
                TelInstallDate.IsEnabled = true;
            }
            else
            {
                TelephoneForChargetextBox.Visibility = Visibility.Collapsed;
                TelUnInstallDate.Visibility = Visibility.Collapsed;
                TelInstallDate.Visibility = Visibility.Collapsed;

                TelephoneForChargetextBox.IsEnabled = false;
                TelUnInstallDate.IsEnabled = false;
                TelInstallDate.IsEnabled = false;

                TelephoneForChargetextBox.Text = null;
                TelUnInstallDate.SelectedDate = null;
                TelInstallDate.SelectedDate = null;
            }
        }

        #endregion

        #region Event Handlers

        private void TelephoneTypecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TelephoneTypeComboBox.SelectedValue != null)
            {
                TelephoneTypeGroupComboBox.ItemsSource = Data.CustomerGroupDB.GetCustomerGroupsCheckableByCustomerTypeID((int)TelephoneTypeComboBox.SelectedValue);
                CheckVisibility();
            }
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
        }

        #endregion
    }
}
