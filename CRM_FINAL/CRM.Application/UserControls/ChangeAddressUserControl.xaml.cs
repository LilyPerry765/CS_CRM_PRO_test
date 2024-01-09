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
    public partial class ChangeAddressUserControl : Local.UserControlBase
    {
        #region Propertes

        private long _RequestID;
        public long TelephoneNo;
        private Data.Telephone _Telephone;
        public Address InstallAddress { get; set; }
        public Address CorrespondenceAddress { get; set; }       

        #endregion

        #region Constructors

        public ChangeAddressUserControl()
        {
            InitializeComponent();
        }

        public ChangeAddressUserControl(long request, long? telephoneNo)
            : this()
        {
            this._RequestID = request;
            TelephoneNo = telephoneNo ?? 0;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            if (TelephoneNo != 0)
            {
                _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(TelephoneNo);

                InstallAddress = Data.AddressDB.GetAddressByID( _Telephone.InstallAddressID ?? 0);

                OldInstallAddressTextBox.Text = InstallAddress.AddressContent;
                OldInstallPostalCodeTextBox.Text = InstallAddress.PostalCode;

                CorrespondenceAddress = Data.AddressDB.GetAddressByID( _Telephone.CorrespondenceAddressID ?? 0);
                OldCorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                OldCorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;

                if (_RequestID != 0)
                {
                    Data.ChangeAddress changeAddress = Data.ChangeAddressDB.GetChangeAddressByID( _RequestID);
                    //if (!(bool)changeAddress.ConfirmVaghozari)
                    //{
                    InstallAddress = Data.AddressDB.GetAddressByID(changeAddress.NewInstallAddressID ?? 0);
                        NewInstallAddressTextBox.Text = InstallAddress.AddressContent;
                        NewInstallPostalCodeTextBox.Text = InstallAddress.PostalCode;

                        CorrespondenceAddress = Data.AddressDB.GetAddressByID( changeAddress.NewCorrespondenceAddressID ?? 0);
                        NewCorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                        NewCorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                    //}
                }
            }
        }

        #endregion

        #region Event HAndlers

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SearchInstallAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewInstallPostalCodeTextBox.Text.Trim()))
            {
                NewInstallAddressTextBox.Text = string.Empty;
                if (BlackListDB.ExistPostallCodeInBlackList(NewInstallPostalCodeTextBox.Text.Trim()))
                {
                    Folder.MessageBox.ShowError("کد پستی در لیست سیاه قرار دارد");
                }
                else
                {

                if ( Data.AddressDB.GetAddressByPostalCodeCount(NewInstallPostalCodeTextBox.Text.Trim()) != 0)
                {
                    InstallAddress = Data.AddressDB.GetAddressByPostalCode(NewInstallPostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                    NewInstallAddressTextBox.Text = string.Empty;
                    NewInstallAddressTextBox.Text = InstallAddress.AddressContent;
                }

                else
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = NewInstallAddressTextBox.Text.Trim();
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        InstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                        
                        NewInstallPostalCodeTextBox.Text = string.Empty;
                        NewInstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                        NewInstallAddressTextBox.Text = string.Empty;
                        NewInstallAddressTextBox.Text = InstallAddress.AddressContent;
                    }
                }
              }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = NewInstallAddressTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    InstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                    
                    NewInstallPostalCodeTextBox.Text = string.Empty;
                    NewInstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                    NewInstallAddressTextBox.Text = string.Empty;
                    NewInstallAddressTextBox.Text = InstallAddress.AddressContent;
                }
            }
        }

        private void SearchCorrespondenceAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewCorrespondencePostalCodeTextBox.Text.Trim()))
            {
                NewCorrespondenceAddressTextBox.Text = string.Empty;

                if (BlackListDB.ExistPostallCodeInBlackList(NewCorrespondencePostalCodeTextBox.Text.Trim()))
                {
                    Folder.MessageBox.ShowError("کد پستی در لیست سیاه قرار دارد");
                }
                else
                {
                if ( Data.AddressDB.GetAddressByPostalCodeCount(NewCorrespondencePostalCodeTextBox.Text.Trim()) != 0)
                {
                    CorrespondenceAddress = Data.AddressDB.GetAddressByPostalCode(NewCorrespondencePostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                    NewCorrespondenceAddressTextBox.Text = string.Empty;
                    NewCorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                }

                else
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = NewCorrespondencePostalCodeTextBox.Text.Trim();
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        CorrespondenceAddress = Data.AddressDB.GetAddressByID(CorrespondenceAddress.ID);
                        
                        NewCorrespondencePostalCodeTextBox.Text = string.Empty;
                        NewCorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                        NewCorrespondenceAddressTextBox.Text = string.Empty;
                        NewCorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                    }
                }
             }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = NewCorrespondencePostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    CorrespondenceAddress = Data.AddressDB.GetAddressByID(CorrespondenceAddress.ID);
                    
                    NewCorrespondencePostalCodeTextBox.Text = string.Empty;
                    NewCorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                    NewCorrespondenceAddressTextBox.Text = string.Empty;
                    NewCorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                }
            }
        }

        #endregion
    }
}
