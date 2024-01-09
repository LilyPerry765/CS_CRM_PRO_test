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
using CRM.Application.Views;
using CRM.Data.ShahkarBussines.Methods;
using CRM.WebAPI.Models.Shahkar.CustomClasses;
using Enterprise;
using System.Net.NetworkInformation;

namespace CRM.Application.UserControls
{
    public partial class Install : Local.UserControlBase
    {
        #region Properties

        private long _ReqID = 0;
        public Customer Customer { get; set; }
        public Address InstallAddress { get; set; }
        public Address CorrespondenceAddress { get; set; }
        public Request Request { get; set; }
        private int _centerID = 0;
        public int CenterID
        {
            set { _centerID = value; }
        }

        public InstallRequest InstallReq
        {

            get { return this.DataContext as InstallRequest; }
            set { this.DataContext = value; }
        }


        #endregion

        #region Constructors

        public Install()
        {
            //ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            //popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            //base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();

        }

        public Install(long id)
            : this()
        {
            _ReqID = id;
            Initialize();

            Request = new Request();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ChargingTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ChargingGroup));
            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();

            PosessionTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PossessionType));
            OrderTypecomboBox.ItemsSource = EnumTypeNameHelper.OrderTypeEnumValues;
            ReasonReinstallComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ReasonReinstall));
            Fiche fiche = FicheDB.GetCurrentFiche(DB.GetServerDate());

            if (fiche != null)
                FicheSaletextBox.Text = fiche.FicheName.ToString();
            else
                MessageBox.Show("اطلاعات مربوط به فیش یافت نشد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Error);

            ZeroBlockComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ClassTelephone));

            this.DataContext = InstallReq;
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

                TelephoneForChargeLabel.Visibility = Visibility.Collapsed;
                TelUnInstallLabel.Visibility = Visibility.Collapsed;
                TelInstallLabel.Visibility = Visibility.Collapsed;

                TelephoneForChargetextBox.IsEnabled = false;
                TelUnInstallDate.IsEnabled = false;
                TelInstallDate.IsEnabled = false;

                TelephoneForChargetextBox.Text = null;
                TelUnInstallDate.SelectedDate = null;
                TelInstallDate.SelectedDate = null;
            }
        }
        private void checkedRegisterAt118CheckBox()
        {
            if (RegisterAt118CheckBox.IsChecked == true)
            {

                if (Customer != null && Customer.PersonType == (int)DB.PersonType.Company)
                {
                    TitleAt118Label.Visibility = Visibility.Visible;
                    TitleAt118TextBox.Visibility = Visibility.Visible;
                    TitleAt118TextBox.Text = Customer.FirstNameOrTitle;
                }
                else if (Customer != null && Customer.PersonType == (int)DB.PersonType.Person)
                {
                    NameTitleAt118Label.Visibility = Visibility.Visible;
                    NameTitleAt118TextBox.Visibility = Visibility.Visible;
                    NameTitleAt118TextBox.Text = Customer.FirstNameOrTitle;

                    LastNameAt118Label.Visibility = Visibility.Visible;
                    LastNameAt118TextBox.Visibility = Visibility.Visible;
                    LastNameAt118TextBox.Text = Customer.LastName;
                }
            }
            else
            {
                NameTitleAt118TextBox.Text = string.Empty;
                LastNameAt118TextBox.Text = string.Empty;
                TitleAt118TextBox.Text = string.Empty;

                NameTitleAt118Label.Visibility = Visibility.Collapsed;
                NameTitleAt118TextBox.Visibility = Visibility.Collapsed;

                LastNameAt118Label.Visibility = Visibility.Collapsed;
                LastNameAt118TextBox.Visibility = Visibility.Collapsed;

                TitleAt118Label.Visibility = Visibility.Collapsed;
                TitleAt118TextBox.Visibility = Visibility.Collapsed;
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
            if (_ReqID == 0)
            {
                InstallReq = new InstallRequest();
                InstallReq.ClassTelephone = (byte)DB.ClassTelephone.LimitLess;
            }
            else
            {
                //FileInfoCopyCountLabel.Visibility = Visibility.Collapsed;
                //FileInfoCopyCountTextBox.Visibility = Visibility.Collapsed;

                Request = Data.RequestDB.GetRequestByID(_ReqID);
                InstallReq = Data.InstallRequestDB.GetInstallRequestByRequestID(_ReqID);

                TelephoneTypeComboBox.SelectedValue = InstallReq.TelephoneType;
                int? telephoneTypeGroup = InstallReq.TelephoneTypeGroup;
                TelephoneTypecomboBox_SelectionChanged(null, null);


                TelephoneTypeGroupComboBox.SelectedValue = telephoneTypeGroup;
                if (this.InstallReq.MethodOfPaymentForTelephoneConnection.HasValue && this.InstallReq.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Cash)
                {
                    CashRadioButton.IsChecked = true;
                }
                else if (this.InstallReq.MethodOfPaymentForTelephoneConnection.HasValue && this.InstallReq.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Installment)
                {
                    InstallmentRadioButton.IsChecked = true;
                }
                else if (this.InstallReq.MethodOfPaymentForTelephoneConnection.HasValue && this.InstallReq.MethodOfPaymentForTelephoneConnection.Value == (byte)DB.MethodOfPaymentForTelephoneConnection.Other)
                {
                    OtherRadioButton.IsChecked = true;
                }
                else
                {
                    OtherRadioButton.IsChecked = false;
                    OtherRadioButton.IsEnabled = false;
                    CashRadioButton.IsChecked = false;
                    CashRadioButton.IsEnabled = false;
                    InstallmentRadioButton.IsChecked = false;
                    InstallmentRadioButton.IsEnabled = false;
                }


                Fiche fiche = Data.FicheDB.GetFicheByID(InstallReq.SaleFicheID ?? 0);
                if (fiche != null)
                {
                    FicheSaletextBox.Text = fiche.FicheName;
                }

                Customer = Data.CustomerDB.GetCustomerByID(Request.CustomerID ?? 0);
                if (Customer != null)
                {
                    NationalCodeTextBox.Text = Customer.NationalCodeOrRecordNo;
                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle + " " + Customer.LastName;
                }

                InstallAddress = Data.AddressDB.GetAddressByID((long)InstallReq.InstallAddressID);
                InstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                InstallAddressTextBox.Text = InstallAddress.AddressContent;

                CorrespondenceAddress = Data.AddressDB.GetAddressByID((long)InstallReq.CorrespondenceAddressID);
                CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;

                // List<CostsOutsideBound> costsOutsideBound = Data.RequestDB.CostsOutsideBound(Request.ID);
                //    if(costsOutsideBound.Count() != 0)
                //    {
                //        OutsideBoundGroupBox.Visibility = Visibility.Visible;
                //        OutsideBoundDataGrid.ItemsSource = costsOutsideBound;
                //    }
                //this.DataContext = InstallReq;

                //InstallInfo.DataContext = installReq;
                //ReinsrallInfo.DataContext = installReq;


                //TODO:rad 13960126
                CustomeGroupBox.IsEnabled = false;
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
                    MessageBox.Show("کد ملی در لیست سیاه قرار دارد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Customer = Data.CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
                    if (Customer != null)
                    {

                        Request.CustomerID = Customer.ID;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                    }
                    else
                    {
                        CustomerForm customerForm = new CustomerForm();
                        customerForm.CenterID = _centerID;
                        customerForm.CustomerType = (int)(TelephoneTypeComboBox.SelectedValue ?? 0);
                        customerForm.ShowDialog();
                        if (customerForm.DialogResult ?? false)
                        {
                            Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                            Request.CustomerID = Customer.ID;
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
                customerForm.CustomerType = (int)(TelephoneTypeComboBox.SelectedValue ?? 0);
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {
                    Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                    Request.CustomerID = Customer.ID;
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

        //TODO:rad 13951109 SearchCustomerTestShahkarByRad_Click
        //private void SearchCustomerTestShahkarByRad_Click(object sender, RoutedEventArgs e)
        //{
        //    //TODO: در ابتدا باید آنلاین بودن شاهکار چک کنیم ، اگر مشکل داشت به کاربر بگوییم چند لحظه دیگ امتحان کنید.
        //    Ping pingSender = new Ping();
        //    PingReply reply = pingSender.Send("http://localhost:1305");

        //    //اگر مشترکی با کد ملی وارد شده در سیستم موجود نباشد ، میبایست از لیست مشترکین مشترک جدید ایجاد گردد
        //    Data.Customer foundCustomer = CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
        //    if (foundCustomer == null)
        //    {
        //        MessageBox.Show(".مشترک یافت نشد ، ایتدا از لیست مشترکین مشترک مورد نظر را ایجاد نمائید", "", MessageBoxButton.OK, MessageBoxImage.Information);
        //        (Window.GetWindow(this) as CRM.Application.Views.RequestForm).Close();
        //        return;
        //    }

        //    //بر اساس تصمیم گیری جدید ، قبل از هر گونه اقدامی باید مشترک از طرف شاهکار احراز هویت گردد
        //    IranianAuthentication iranianAuthentication = ShahkarEntityCreators.CreateIranianAuthenticationFromCustomer(foundCustomer);
        //    Result responseFromShahkarServer = new Send<IranianAuthentication>().SendHttpWebRequest(iranianAuthentication, "http://localhost:1305/api/Customer/PostCustomer", "POST");

        //    if (responseFromShahkarServer == null)
        //    {
        //        throw new ApplicationException(".در ارسال درخواست خطا رخ داده است");
        //    }

        //    if (!responseFromShahkarServer.HasInvalidData) //مشترک احراز هویت شده است
        //    {
        //        if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
        //        {
        //            CustomerNameTextBox.Text = string.Empty;

        //            //if (Data.CustomerDB.GetCustomerByNationalCodeCount(NationalCodeTextBox.Text.Trim()) > 1)
        //            //{ MessageBox.Show("چند مشترک با این کد ملی یافت شد. ابتدا اطلاعات مشترک را اصلاح کنید"); return; }

        //            if (Data.BlackListDB.ExistNationalCodeInBlackList(NationalCodeTextBox.Text.Trim()))
        //            {
        //                MessageBox.Show("کد ملی در لیست سیاه قرار دارد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }
        //            else
        //            {
        //                Customer = Data.CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
        //                if (Customer != null)
        //                {

        //                    Request.CustomerID = Customer.ID;
        //                    CustomerNameTextBox.Text = string.Empty;
        //                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
        //                }
        //                else
        //                {
        //                    CustomerForm customerForm = new CustomerForm();
        //                    customerForm.CenterID = _centerID;
        //                    customerForm.CustomerType = (int)(TelephoneTypeComboBox.SelectedValue ?? 0);
        //                    customerForm.ShowDialog();
        //                    if (customerForm.DialogResult ?? false)
        //                    {
        //                        Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
        //                        Request.CustomerID = Customer.ID;
        //                        CustomerNameTextBox.Text = string.Empty;
        //                        CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            CustomerForm customerForm = new CustomerForm();
        //            customerForm.CenterID = _centerID;
        //            customerForm.CustomerType = (int)(TelephoneTypeComboBox.SelectedValue ?? 0);
        //            customerForm.ShowDialog();
        //            if (customerForm.DialogResult ?? false)
        //            {
        //                Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
        //                Request.CustomerID = Customer.ID;
        //                CustomerNameTextBox.Text = string.Empty;
        //                CustomerNameTextBox.Text = Customer.FirstNameOrTitle ?? "" + " " + Customer.LastName ?? "";
        //            }
        //        }

        //        if (Customer != null)
        //        {
        //            (Window.GetWindow(this) as CRM.Application.Views.RequestForm).RequesterNametextBox.Text = Customer.FirstNameOrTitle + " " + Customer.LastName;
        //        }
        //    }
        //    else
        //    {
        //        //در این قسمت باید متناسب با جواب دریافتی از سمت سرور کرمانشاه به کاربر علت عدم معتبر بودن مشترک را نمایش دهیم
        //        if (responseFromShahkarServer.response == 311)
        //        {
        //            Logger.WriteInfo("response details from shahkar : {0}", responseFromShahkarServer.result);
        //            MessageBox.Show(responseFromShahkarServer.result, "", MessageBoxButton.OK, MessageBoxImage.Warning);
        //            (Window.GetWindow(this) as CRM.Application.Views.RequestForm).Close();
        //        }
        //    }

        //}


        //TODO: rad 13951113 احراز هویت به لیست مشترکیت انتقال یافت
        private void SearchCustomerTestShahkarByRad_Click(object sender, RoutedEventArgs e)
        {
            //اگر مشترکی با کد ملی وارد شده در سیستم موجود نباشد ، میبایست از لیست مشترکین مشترک جدید ایجاد گردد
            Data.Customer foundCustomer = CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
            if (foundCustomer == null)
            {
                MessageBox.Show(".مشترک یافت نشد ، ابتدا از لیست مشترکین مشترک مورد نظر را ایجاد نمائید", "", MessageBoxButton.OK, MessageBoxImage.Information);
                (Window.GetWindow(this) as CRM.Application.Views.RequestForm).Close();
                return;
            }
            else if ( //در حال حاضر فقط مشترکین حقیقی احراز هویت شده ، را دابل چک میکنیم چون دیدی از نماینده مشترکین حقوقی نداریم
                        (foundCustomer.PersonType == (byte)DB.PersonType.Person)
                        &&
                        (foundCustomer.IsAuthenticated == null || (foundCustomer.IsAuthenticated.HasValue && foundCustomer.IsAuthenticated.Value == false))
                    )
            {
                MessageBox.Show(".این مشترک توسط سامانه شاهکار احراز هویت نشده است", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CustomerNameTextBox.Text = string.Empty;

                //if (Data.CustomerDB.GetCustomerByNationalCodeCount(NationalCodeTextBox.Text.Trim()) > 1)
                //{ MessageBox.Show("چند مشترک با این کد ملی یافت شد. ابتدا اطلاعات مشترک را اصلاح کنید"); return; }

                if (Data.BlackListDB.ExistNationalCodeInBlackList(NationalCodeTextBox.Text.Trim()))
                {
                    MessageBox.Show("کد ملی در لیست سیاه قرار دارد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Customer = Data.CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
                    if (Customer != null)
                    {

                        Request.CustomerID = Customer.ID;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                    }
                    else
                    {
                        CustomerForm customerForm = new CustomerForm();
                        customerForm.CenterID = _centerID;
                        customerForm.CustomerType = (int)(TelephoneTypeComboBox.SelectedValue ?? 0);
                        customerForm.ShowDialog();
                        if (customerForm.DialogResult ?? false)
                        {
                            Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                            Request.CustomerID = Customer.ID;
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
                customerForm.CustomerType = (int)(TelephoneTypeComboBox.SelectedValue ?? 0);
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {
                    Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                    Request.CustomerID = Customer.ID;
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle ?? "" + " " + Customer.LastName ?? "";
                }
            }

            if (Customer != null)
            {
                (Window.GetWindow(this) as CRM.Application.Views.RequestForm).RequesterNametextBox.Text = Customer.FirstNameOrTitle + " " + Customer.LastName;
            }

        }

        private void SearchInstallAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(InstallPostalCodeTextBox.Text.Trim()))
            {
                if (BlackListDB.ExistPostallCodeInBlackList(InstallPostalCodeTextBox.Text.Trim()))
                {
                    MessageBox.Show("کد پستی در لیست سیاه قرار دارد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    InstallAddressTextBox.Text = string.Empty;

                    if (Data.AddressDB.GetAddressByPostalCode(InstallPostalCodeTextBox.Text.Trim()).Count != 0)
                    {
                        InstallAddress = Data.AddressDB.GetAddressByPostalCode(InstallPostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                        InstallReq.InstallAddressID = InstallAddress.ID;
                        InstallAddressTextBox.Text = string.Empty;
                        InstallAddressTextBox.Text = InstallAddress.AddressContent;
                    }

                    else
                    {
                        CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                        customerAddressForm.PostallCode = InstallPostalCodeTextBox.Text.Trim();
                        customerAddressForm.ShowDialog();
                        if (customerAddressForm.DialogResult ?? false)
                        {
                            InstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                            InstallReq.InstallAddressID = InstallAddress.ID;

                            InstallPostalCodeTextBox.Text = string.Empty;
                            InstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                            InstallAddressTextBox.Text = string.Empty;
                            InstallAddressTextBox.Text = InstallAddress.AddressContent;
                        }
                    }
                }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = InstallPostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    InstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                    InstallReq.InstallAddressID = InstallAddress.ID;

                    InstallPostalCodeTextBox.Text = string.Empty;
                    InstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                    InstallAddressTextBox.Text = string.Empty;
                    InstallAddressTextBox.Text = InstallAddress.AddressContent;
                }
            }
        }

        private void SearchCorrespondenceAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CorrespondencePostalCodeTextBox.Text.Trim()))
            {
                CorrespondenceAddressTextBox.Text = string.Empty;


                if (BlackListDB.ExistPostallCodeInBlackList(CorrespondencePostalCodeTextBox.Text.Trim()))
                {
                    MessageBox.Show("کد پستی در لیست سیاه قرار دارد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {

                    if (Data.AddressDB.GetAddressByPostalCode(CorrespondencePostalCodeTextBox.Text.Trim()).Count != 0)
                    {
                        CorrespondenceAddress = Data.AddressDB.GetAddressByPostalCode(CorrespondencePostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                        InstallReq.CorrespondenceAddressID = CorrespondenceAddress.ID;
                        CorrespondenceAddressTextBox.Text = string.Empty;
                        CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                    }

                    else
                    {
                        CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                        customerAddressForm.PostallCode = CorrespondencePostalCodeTextBox.Text.Trim();
                        customerAddressForm.ShowDialog();
                        if (customerAddressForm.DialogResult ?? false)
                        {
                            CorrespondenceAddress = Data.AddressDB.GetAddressByID(CorrespondenceAddress.ID);
                            InstallReq.CorrespondenceAddressID = CorrespondenceAddress.ID;

                            CorrespondencePostalCodeTextBox.Text = string.Empty;
                            CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                            CorrespondenceAddressTextBox.Text = string.Empty;
                            CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                        }
                    }
                }

            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = CorrespondencePostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    CorrespondenceAddress = Data.AddressDB.GetAddressByID(CorrespondenceAddress.ID);
                    InstallReq.CorrespondenceAddressID = CorrespondenceAddress.ID;

                    CorrespondencePostalCodeTextBox.Text = string.Empty;
                    CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                    CorrespondenceAddressTextBox.Text = string.Empty;
                    CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                }
            }
        }

        private void TelephoneTypeGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InquiryInvestigatePossibility_Click(object sender, RoutedEventArgs e)
        {
            InquiryInvestigatePossibilitiesForm window = new InquiryInvestigatePossibilitiesForm(NearestTelephonTextBox.Text.Trim());
            window.Show();
        }

        private void EditSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (Customer != null)
            {
                CustomerForm window = new CustomerForm(Customer.ID);
                bool? windowResult = window.ShowDialog();

                //TODO:rad 13960123 به علت شاهکار باید بعد از هر ویرایش اطلاعات (مشترک حقیقی البته فعلاً)مشترک مجدداً از شاهکار احراز هویت بگیرد
                if (windowResult.HasValue && windowResult.Value && Customer.PersonType == (byte)DB.PersonType.Person)
                {
                    Customer = CustomerDB.GetCustomerByID(Customer.ID);
                    Customer.IsAuthenticated = null;
                    Customer.Detach();
                    DB.Save(Customer);
                    MessageBox.Show(".این مشترک مجدداً باید از سمت سامانه شاهکار احراز هویت شود", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    (Window.GetWindow(this) as CRM.Application.Views.RequestForm).Close();
                }
                //**************************************************************************************************************
            }
        }

        private void CorrespondenceAddressButton_Click(object sender, RoutedEventArgs e)
        {
            if (CorrespondenceAddress != null)
            {
                CustomerAddressForm window = new CustomerAddressForm(CorrespondenceAddress.ID);
                window.ShowDialog();
                SearchCorrespondenceAddress_Click(null, null);
            }
        }

        private void InstallAddressButton_Click(object sender, RoutedEventArgs e)
        {
            if (InstallAddress != null)
            {
                CustomerAddressForm window = new CustomerAddressForm(InstallAddress.ID);
                window.ShowDialog();
                SearchInstallAddress_Click(null, null);
            }
        }

        private void CustomerTelephoneInfo_Click(object sender, RoutedEventArgs e)
        {

            if (Customer != null)
            {
                CustomerTelephoneInfoForm window = new CustomerTelephoneInfoForm(Customer.ID);
                window.ShowDialog();
            }
        }

        private void RegisterAt118CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            checkedRegisterAt118CheckBox();
        }

        private void RegisterAt118CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            checkedRegisterAt118CheckBox();
        }

        #endregion

        private void MethodOfPaymentForTelephoneConnectionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton source = e.OriginalSource as RadioButton;
            if (source != null)
            {
                switch (source.Name)
                {
                    case "CashRadioButton":
                        {
                            this.InstallReq.MethodOfPaymentForTelephoneConnection = (byte)DB.MethodOfPaymentForTelephoneConnection.Cash;
                        }
                        break;
                    case "InstallmentRadioButton":
                        {
                            this.InstallReq.MethodOfPaymentForTelephoneConnection = (byte)DB.MethodOfPaymentForTelephoneConnection.Installment;
                        }
                        break;
                    case "OtherRadioButton":
                        {
                            this.InstallReq.MethodOfPaymentForTelephoneConnection = (byte)DB.MethodOfPaymentForTelephoneConnection.Other;
                        }
                        break;

                }
            }
        }

    }
}
