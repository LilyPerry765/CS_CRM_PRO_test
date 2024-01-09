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
    public partial class BlackListForm : Local.RequestFormBase
    {
        #region Properties

        private byte _Type = 0;
        private long _ID = 0;

        #endregion

        #region Constructors

        public BlackListForm()
        {
            InitializeComponent();
            Initialize();
        }

        public BlackListForm(byte type)
            : this()
        {
            _Type = type;
        }

        public BlackListForm(byte type, long id)
            : this()
        {
            _Type = type;
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.BlackListType));
            ReasonComboBox.ItemsSource = DB.GetAllEntity<BlackListReason>();

            ActionIDs = new List<byte> { (byte)DB.NewAction.SaveBlackList , (byte)DB.NewAction.ExitBlackList, (byte)DB.NewAction.Exit };
        }

        private void LoadData()
        {
            BlackListInfo blackList = new BlackListInfo();
            blackList.TypeMember = _Type;

            if (_ID != 0)
                blackList = Data.BlackListDB.GetBlackListById(_ID);

            this.DataContext = blackList;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeComboBox.SelectedValue = _Type;

            if (TypeComboBox.SelectedValue != null)
            {
                switch ((byte)TypeComboBox.SelectedValue)
                {
                    case (byte)DB.BlackListType.TelephoneNo:
                        TelephonePanel.Visibility = Visibility.Visible;
                        CustomerPanel.Visibility = Visibility.Collapsed;
                        AddressPanel.Visibility = Visibility.Collapsed;
                        break;

                    case (byte)DB.BlackListType.Customer:
                        TelephonePanel.Visibility = Visibility.Collapsed;
                        CustomerPanel.Visibility = Visibility.Visible;
                        AddressPanel.Visibility = Visibility.Collapsed;
                        break;

                    case (byte)DB.BlackListType.Address:
                        TelephonePanel.Visibility = Visibility.Collapsed;
                        CustomerPanel.Visibility = Visibility.Collapsed;
                        AddressPanel.Visibility = Visibility.Visible;
                        break;

                    default:
                        break;
                }
            }
        }

        public override bool SaveBlackList()
        {
            try
            {
                BlackListInfo BlackListInfo = this.DataContext as BlackListInfo; 
                BlackList blackList = new BlackList();

                blackList.ArrestReference = BlackListInfo.ArrestReference;
                blackList.ArrestLetterNoDate = BlackListInfo.ArrestLetterNoDate;
                blackList.ArrestLetterNo = BlackListInfo.ArrestLetterNo;

                if (TypeComboBox.SelectedValue != null)
                {
                    switch ((byte)TypeComboBox.SelectedValue)
                    {
                        case (byte)DB.BlackListType.TelephoneNo:
                            long telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text.Trim());
                            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);

                            if (telephone != null)
                            {
                                if ((bool)CustomerCheckBox.IsChecked)
                                {
                                    Data.BlackList blackCustomer = new Data.BlackList();
                                    blackCustomer.CustomerID = telephone.CustomerID;
                                    blackCustomer.TypeMember = (byte)DB.BlackListType.Customer;
                                    blackCustomer.ReasonID = (byte)ReasonComboBox.SelectedValue;
                                    blackCustomer.ArrestReference = BlackListInfo.ArrestReference;
                                    blackCustomer.ArrestLetterNoDate = BlackListInfo.ArrestLetterNoDate;
                                    blackCustomer.ArrestLetterNo = BlackListInfo.ArrestLetterNo;
                                   

                                    BlackListDB.SaveBlackList(blackCustomer, (byte)DB.BlackListType.Customer);
                                }

                                if ((bool)AddressCheckBox.IsChecked)
                                {
                                    Data.BlackList blackAddress = new Data.BlackList();
                                    blackAddress.AddressID = telephone.InstallAddressID;
                                    blackAddress.TypeMember = (byte)DB.BlackListType.Address;
                                    blackAddress.ReasonID = (byte)ReasonComboBox.SelectedValue;
                                    blackAddress.ArrestReference = BlackListInfo.ArrestReference;
                                    blackAddress.ArrestLetterNoDate = BlackListInfo.ArrestLetterNoDate;
                                    blackAddress.ArrestLetterNo = BlackListInfo.ArrestLetterNo;

                                    BlackListDB.SaveBlackList(blackAddress, (byte)DB.BlackListType.Address);
                                }

                                blackList.TelephoneNo = telephoneNo;
                                blackList.TypeMember = (byte)DB.BlackListType.TelephoneNo;
                                blackList.ReasonID = (byte)ReasonComboBox.SelectedValue;
                                BlackListDB.SaveBlackList(blackList, (byte)DB.BlackListType.TelephoneNo);
                            }
                            break;

                        case (byte)DB.BlackListType.Customer:
                            Customer customer = CustomerDB.GetCustomerByNationalCode(CustomerTextBox.Text.Trim());
                            if (customer == null)
                                throw new Exception("مشترک یافت نشد");
                            blackList.CustomerID = customer.ID;
                            blackList.TypeMember = (byte)DB.BlackListType.Customer;
                            blackList.ReasonID = (byte)ReasonComboBox.SelectedValue;
                            BlackListDB.SaveBlackList(blackList, (byte)DB.BlackListType.Customer);
                            break;

                        case (byte)DB.BlackListType.Address:
                            Address address = AddressDB.GetAddressByPostalCode(AddressTextBox.Text.Trim()).Take(1).SingleOrDefault();
                            if (address == null)
                                throw new Exception("آدرس یافت نشد");
                            blackList.AddressID = address.ID;
                            blackList.TypeMember = (byte)DB.BlackListType.Address;
                            blackList.ReasonID = (byte)ReasonComboBox.SelectedValue;
                            BlackListDB.SaveBlackList(blackList, (byte)DB.BlackListType.Address);
                            break;

                        default:
                            break;
                    }
                }

                IsSaveBlackListSuccess = true;

                ShowSuccessMessage("آیتم ورودی ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره آیتم ورودی", ex);
            }

            return IsSaveBlackListSuccess;
        }

        public override bool ExitBlackList()
        {
            try
            {
                if (_ID != 0)
                {
                    BlackList blackList = Data.BlackListDB.GetBlackListEntityById(_ID);
                    blackList.ExitUserID = DB.currentUser.ID;
                    blackList.ExitDate = DB.GetServerDate();
                    blackList.Status = false;
                    blackList.Detach();
                    DB.Save(blackList);
                }
                else
                {
                   throw new Exception("خطا در ذخیره اطلاعات");
                }

                
                
            }
            catch(Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            IsExitBlackListSuccess = true;

            return IsExitBlackListSuccess;
        }

        #endregion
    }
}
