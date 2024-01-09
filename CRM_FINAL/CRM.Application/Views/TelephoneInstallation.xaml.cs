using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TelephoneInstallation.xaml
    /// </summary>
    public partial class TelephoneInstallation : Local.PopupWindow
    {
        long telephonNo = 0;
        Telephone telephoneItem { get; set; }
        AssignmentInfo assignmentInfo = new AssignmentInfo();

        Customer _customer { get; set; }
        Customer _primaryCustomer { get; set; }

        Address _installAddress { get; set; }
        Address _primaryInstallAddress { get; set; }
        Address _correspondenceAddress { get; set; }
        Address _primaryCorrespondenceAddress { get; set; }


        Bucht _bucht { get; set; }
        Bucht _primaryBucht { get; set; }

        Cabinet _cabinet { get; set; }
        Cabinet _primaryCabinet { get; set; }
        Post _Post { get; set; }
        Post _primaryPost { get; set; }
        PostContact _PostContact { get; set; }
        PostContact _primaryPostContact { get; set; }

        InstallRequest _installRequest { get; set; }
        public TelephoneInstallation()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {

        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ChargingTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ChargingGroup)); 
            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();

            PosessionTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PossessionType));
            OrderTypecomboBox.ItemsSource = EnumTypeNameHelper.OrderTypeEnumValues;

            ResetData();

        }

        private void ResetData()
        {
            _customer = null;
            _installAddress = null;
            _correspondenceAddress = null;
            _PostContact = null;
            _bucht = null;
            _cabinet = null;
            _Post = null;
            _PostContact = null;
        }

        private void SearchTelephone(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TelephonTextBox.Text.Trim()))
                {

                    ResetData();
                   

                    if (!long.TryParse(TelephonTextBox.Text.Trim(), out telephonNo))
                        throw new Exception("تلفن وارد شده صحیح نمی باشد");

                    telephoneItem = Data.TelephoneDB.GetTelephoneByTelephoneNo(telephonNo);
                    if(telephoneItem != null)
                    {
                        CenterTextBox.Text = CenterDB.GetCenterByCenterID(telephoneItem.CenterID).CenterName;
                        CenterLabel.Visibility = Visibility.Visible;
                        CenterTextBox.Visibility = Visibility.Visible;

                    if (!DB.CurrentUser.CenterIDs.Contains(telephoneItem.CenterID))
                    {
                        Folder.MessageBox.ShowError("دسترسی شما شامل مرکز تلفن وارد شده نمی باشد");
                    }
                    else if(telephoneItem.UsageType == (int)DB.TelephoneUsageType.PrivateWire || telephoneItem.UsageType == (int)DB.TelephoneUsageType.E1)
                    {
                        throw new Exception("امکان دایری سیم خصوصی نمی باشد");
                    }
                    else
                    {

                        if (telephoneItem.Status == (int)DB.TelephoneStatus.Free || telephoneItem.Status == (int)DB.TelephoneStatus.Discharge)
                        {
                            SaveButton.Content = "دایری تلفن";
                            SaveButton.IsEnabled = true;
                            InstallInfo.Visibility = Visibility.Visible;
                            _installRequest = new InstallRequest();
                            InstallInfo.DataContext = _installRequest;
                           
                        } 
                        else if (telephoneItem.Status == (int)DB.TelephoneStatus.Cut || telephoneItem.Status == (int)DB.TelephoneStatus.Connecting)
                        {
                            SaveButton.Content = "ویرایش تلفن";
                            SaveButton.IsEnabled = true;
                            InstallInfo.Visibility = Visibility.Collapsed;
                        }
                        else if (telephoneItem.Status == (int)DB.TelephoneStatus.Destruction)
                        {
                            throw new Exception("تلفن خراب می باشد امکان دایری فراهم نمی باشد");
                        }
                        else
                        {
                            throw new Exception("تلفن رزرو می باشد");
                        }



                        if (telephoneItem.InstallationDate.HasValue && (telephoneItem.Status == (int)DB.TelephoneStatus.Connecting || telephoneItem.Status == (int)DB.TelephoneStatus.Cut))
                        {
                            InstallInfoGrid.Visibility = Visibility.Visible;
                            InstallInfoGroupBox.IsEnabled = false;
                            InstallationDatePicker.SelectedDate = telephoneItem.InstallationDate.Value;
                        }
                        else
                        {
                            InstallInfoGrid.Visibility = Visibility.Visible;
                            InstallInfoGroupBox.IsEnabled = true;
                        }
                        if (telephoneItem.CustomerID.HasValue)
                        {
                            NameInformationGrid.Visibility = Visibility.Visible;
                            NewNameInformationGrid.Visibility = Visibility.Collapsed;
                            _primaryCustomer = CustomerDB.GetCustomerByID(telephoneItem.CustomerID.Value);

                            ExistCustomerNameTextBox.Text = (_primaryCustomer.FirstNameOrTitle ?? "") + " " + (_primaryCustomer.LastName ?? "");

                        }
                        else
                        {
                            NameInformationGrid.Visibility = Visibility.Collapsed;
                            NewNameInformationGrid.Visibility = Visibility.Visible;
                        }


                        if (telephoneItem.InstallAddressID.HasValue)
                        {
                            AddressInformationGrid.Visibility = Visibility.Visible;
                            NewAddressInformationGrid.Visibility = Visibility.Collapsed;
                            _primaryInstallAddress = AddressDB.GetAddressByID(telephoneItem.InstallAddressID.Value);
                            ExistInstallAddressTextBox.Text = _primaryInstallAddress.AddressContent;

                            _primaryCorrespondenceAddress = AddressDB.GetAddressByID(telephoneItem.CorrespondenceAddressID.Value);
                            ExistCorrespondenceAddressTextBox.Text = _primaryCorrespondenceAddress.AddressContent;


                        }
                        else
                        {
                            AddressInformationGrid.Visibility = Visibility.Collapsed;
                            NewAddressInformationGrid.Visibility = Visibility.Visible;
                        }

                        if (telephoneItem.SwitchPortID.HasValue)
                        {

                            _primaryBucht = BuchtDB.GetBuchtBySwitchPortID(telephoneItem.SwitchPortID.Value);


                            #region bucht
                            if (_primaryBucht != null)
                            {
                                BuchtGrid.Visibility = Visibility.Visible;
                                NewBuchtGrid.Visibility = Visibility.Collapsed;

                                ConnectionInfo connectionInfo = DB.GetBuchtInfoByID(_primaryBucht.ID);
                                MDFTextBox.Text = connectionInfo.MDF;
                                ColumnNoTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
                                RowNoTextBox.Text = connectionInfo.VerticalRowNo.ToString();
                                BuchtNoTextBox.Text = connectionInfo.BuchtNo.ToString();

                            }
                            else
                            {
                                BuchtGrid.Visibility = Visibility.Collapsed;
                                NewBuchtGrid.Visibility = Visibility.Visible;

                                MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(telephoneItem.CenterID);
                            }

                            #endregion bucht

                            #region abone
                            if (_primaryBucht != null && _primaryBucht.CabinetInputID.HasValue)
                            {
                                _primaryCabinet = CabinetDB.GetCabinetByCabinetInputID(_primaryBucht.CabinetInputID.Value);
                                Cabinet cabinet = CabinetDB.GetcabinetbyTelephonNo(telephoneItem.TelephoneNo);

                                if (_primaryCabinet != null && cabinet != null)
                                {

                                    if (_primaryCabinet.ID == cabinet.ID)
                                    {
                                        NewPostComboBox.ItemsSource = PostDB.GetPostCheckableByCabinetID((int)_primaryCabinet.ID);
                                    }
                                    else
                                    {
                                        Folder.MessageBox.ShowError(string.Format("بعلت صحیح نبودن بوخت امکان انتخاب پست نمی باشد"));
                                    }
                                }
                                else if (_primaryCabinet != null && cabinet == null)
                                {
                                    if (_primaryBucht.BuchtTypeID == (int)DB.BuchtType.InLine)
                                    {
                                        if (_primaryBucht.ConnectionID.HasValue)
                                        {
                                            PostContact postContact = PostContactDB.GetPostContactByID(_primaryBucht.ConnectionID.Value);
                                            if (postContact != null)
                                            {
                                                Post post = PostDB.GetPostByID(postContact.PostID);

                                                NewPostComboBox.SelectedValue = post.ID;
                                                NewPostComboBox_SelectionChanged(null, null);
                                                NewPostComboBox.IsEnabled = false;

                                                NewPostContactComboBox.SelectedValue = postContact.ID;
                                                NewPostContactComboBox_SelectionChanged(null, null);
                                                NewPostContactComboBox.IsEnabled = false;


                                            }
                                            else
                                            {
                                                NewPostComboBox.IsEnabled = false;
                                                NewPostContactComboBox.IsEnabled = false;
                                                Folder.MessageBox.ShowError("اتصالی پی سی ام یافت نشد");
                                            }
                                        }
                                        else
                                        {
                                            NewPostComboBox.IsEnabled = false;
                                            NewPostContactComboBox.IsEnabled = false;
                                        }

                                    }
                                    else
                                    {
                                        NewPostComboBox.ItemsSource = PostDB.GetPostCheckableByCabinetID((int)_primaryCabinet.ID);
                                    }
                                }
                                else
                                {
                                    Folder.MessageBox.ShowError(string.Format("بعلت صحیح نبودن بوخت امکان انتخاب پست نمی باشد"));
                                }


                            }


                            if (_primaryBucht != null && _primaryBucht.ConnectionID.HasValue)
                            {

                                AboneTechInformationGrid.Visibility = Visibility.Visible;
                                NewAboneTechInformationGrid.Visibility = Visibility.Collapsed;

                                _primaryPostContact = PostContactDB.GetPostContactByID(_primaryBucht.ConnectionID.Value);

                                if (_primaryPostContact != null)
                                {
                                    PostContactTextBox.Text = _primaryPostContact.ConnectionNo.ToString();

                                    _primaryPost = PostDB.GetPostByID(_primaryPostContact.PostID);

                                    if (_primaryPost != null)
                                    {
                                        PostTextBox.Text = _primaryPost.Number.ToString();

                                        _primaryCabinet = CabinetDB.GetCabinetByID(_primaryPost.CabinetID);

                                        if (_primaryCabinet != null)
                                        {
                                            CabinetNumberTextBox.Text = _primaryCabinet.CabinetNumber.ToString();
                                        }
                                    }
                                }


                            }
                            else
                            {
                                AboneTechInformationGrid.Visibility = Visibility.Collapsed;
                                NewAboneTechInformationGrid.Visibility = Visibility.Visible;
                            }

                            #endregion abone
                        }

                    }

                }
                else
                {

                    TelephonTextBox.Focus();
                }
                }
                else
                {
                    Folder.MessageBox.ShowInfo("تلفن در رنج پیش شماره ها قرار ندارد");
                }
            }
            catch (Exception ex)
            {
                Folder.MessageBox.ShowError("خطا در جستجو شماره", ex);
            }

            this.ResizeWindow();
        }

        private void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CustomerNameTextBox.Text = string.Empty;

                _customer = Data.CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
                if (_customer != null)
                {
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = _customer.FirstNameOrTitle + ' ' + _customer.LastName;
                }

                else
                {
                    CustomerForm customerForm = new CustomerForm();
                    customerForm.ShowDialog();
                    if (customerForm.DialogResult ?? false)
                    {
                        _customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = _customer.FirstNameOrTitle + ' ' + _customer.LastName;
                    }
                }
            }
            else
            {
                CustomerForm customerForm = new CustomerForm();
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {
                    _customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = _customer.FirstNameOrTitle + ' ' + _customer.LastName;
                }
            }
        }

        private void SearchInstallAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewInstallPostalCodeTextBox.Text.Trim()))
            {
                NewInstallAddressTextBox.Text = string.Empty;

                if (Data.AddressDB.GetAddressByPostalCodeCount(NewInstallPostalCodeTextBox.Text.Trim()) != 0)
                {
                    _installAddress = Data.AddressDB.GetAddressByPostalCode(NewInstallPostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                    NewInstallAddressTextBox.Text = string.Empty;
                    NewInstallAddressTextBox.Text = _installAddress.AddressContent;
                }

                else
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = NewInstallPostalCodeTextBox.Text.Trim();
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        _installAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);

                        NewInstallPostalCodeTextBox.Text = string.Empty;
                        NewInstallPostalCodeTextBox.Text = _installAddress.PostalCode;
                        NewInstallAddressTextBox.Text = string.Empty;
                        NewInstallAddressTextBox.Text = _installAddress.AddressContent;
                    }
                }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = NewInstallPostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    _installAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);

                    NewInstallPostalCodeTextBox.Text = string.Empty;
                    NewInstallPostalCodeTextBox.Text = _installAddress.PostalCode;
                    NewInstallAddressTextBox.Text = string.Empty;
                    NewInstallAddressTextBox.Text = _installAddress.AddressContent;
                }
            }

        }

        private void SearchCorrespondenceAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewCorrespondencePostalCodeTextBox.Text.Trim()))
            {
                NewCorrespondenceAddressTextBox.Text = string.Empty;

                if (Data.AddressDB.GetAddressByPostalCodeCount(NewCorrespondencePostalCodeTextBox.Text.Trim()) != 0)
                {
                    _correspondenceAddress = Data.AddressDB.GetAddressByPostalCode(NewCorrespondencePostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                    NewCorrespondenceAddressTextBox.Text = string.Empty;
                    NewCorrespondenceAddressTextBox.Text = _correspondenceAddress.AddressContent;
                }

                else
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = NewCorrespondencePostalCodeTextBox.Text.Trim();
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        _correspondenceAddress = Data.AddressDB.GetAddressByID(_correspondenceAddress.ID);

                        NewCorrespondencePostalCodeTextBox.Text = string.Empty;
                        NewCorrespondencePostalCodeTextBox.Text = _correspondenceAddress.PostalCode;
                        NewCorrespondenceAddressTextBox.Text = string.Empty;
                        NewCorrespondenceAddressTextBox.Text = _correspondenceAddress.AddressContent;
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
                    _correspondenceAddress = Data.AddressDB.GetAddressByID(_correspondenceAddress.ID);

                    NewCorrespondencePostalCodeTextBox.Text = string.Empty;
                    NewCorrespondencePostalCodeTextBox.Text = _correspondenceAddress.PostalCode;
                    NewCorrespondenceAddressTextBox.Text = string.Empty;
                    NewCorrespondenceAddressTextBox.Text = _correspondenceAddress.AddressContent;
                }
            }
        }


        //private void NewCabinetNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if(NewCabinetNumberComboBox.SelectedValue != null)
        //    {

        //    }

        //}

        private void NewPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewPostComboBox.SelectedValue != null)
            {
                NewPostContactComboBox.ItemsSource = PostContactDB.GetFreePostContactByPostID((int)NewPostComboBox.SelectedValue);
            }
        }

        private void MDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MDFComboBox.SelectedValue != null)
            {
                _bucht = null;
                ConnectionColumnComboBox.ItemsSource = DB.GetConnectionColumnInfo((int)MDFComboBox.SelectedValue);
            }
        }

        private void ConnectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionColumnComboBox.SelectedValue != null)
            {
                _bucht = null;
                ConnectionRowComboBox.ItemsSource = DB.GetConnectionRowInfo((int)ConnectionColumnComboBox.SelectedValue);
            }
        }

        private void ConnectionRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionRowComboBox.SelectedValue != null)
            {
                _bucht = null;
                ConnectionBuchtComboBox.ItemsSource = DB.GetConnectableConnectionBuchtInfo((int)ConnectionRowComboBox.SelectedValue);
            }
        }

        private void ConnectionBuchtComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionBuchtComboBox.SelectedValue != null)
            {
                _bucht = null;
                _bucht = BuchtDB.GetBuchetByID((long)ConnectionBuchtComboBox.SelectedValue);

                if (_bucht != null)
                {
                    if (_bucht.CabinetInputID.HasValue)
                    {
                        _cabinet = CabinetDB.GetCabinetByCabinetInputID(_bucht.CabinetInputID.Value);
                        Cabinet cabinet = CabinetDB.GetcabinetbyTelephonNo(telephoneItem.TelephoneNo);

                        if (_cabinet != null && cabinet != null)
                        {
                            // اگر بوخت انتخاب شده از همان کافو نوری است پست های آن کافو پر می شود
                            if (_cabinet.ID == cabinet.ID)
                            {
                                NewPostComboBox.ItemsSource = PostDB.GetPostCheckableByCabinetID((int)_cabinet.ID);
                            }
                            else
                            {
                                Folder.MessageBox.ShowError(string.Format("امکان انتخاب پست نمی باشد. تلفن مربوط به کافو نوری {0} می باشد اما بوخت مریوط به کافو {1} می باشد", cabinet.CabinetNumber, _cabinet.CabinetNumber));
                            }
                        }
                        // اگر تلفن مربوط به کافو نوری نیست پست های پر می شود
                        else if (_cabinet != null && cabinet == null)
                        {
                            NewPostComboBox.ItemsSource = PostDB.GetPostCheckableByCabinetID((int)_cabinet.ID);

                            // اگر بوخت انتخابی پی سی ام باشد اطلاعات پست و اتصالی آن از طریق سیستم انتخاب می شود
                            if (_bucht.BuchtTypeID == (int)DB.BuchtType.InLine)
                            {
                                if (_bucht.ConnectionID.HasValue)
                                {
                                    PostContact postContact = PostContactDB.GetPostContactByID(_bucht.ConnectionID.Value);
                                    if (postContact != null)
                                    {
                                        Post post = PostDB.GetPostByID(postContact.PostID);

                                        NewPostComboBox.SelectedValue = post.ID;
                                        NewPostComboBox_SelectionChanged(null, null);
                                        NewPostComboBox.IsEnabled = false;

                                        NewPostContactComboBox.SelectedValue = postContact.ID;
                                        NewPostContactComboBox_SelectionChanged(null, null);
                                        NewPostContactComboBox.IsEnabled = false;


                                    }
                                    else
                                    {
                                        ResetPostContact();
                                        Folder.MessageBox.ShowError("اتصالی پی سی ام یافت نشد");
                                    }
                                }
                                else
                                {
                                    ResetPostContact();
                                    Folder.MessageBox.ShowError("اتصالی پی سی ام یافت نشد");
                                    NewPostComboBox.IsEnabled = false;
                                    NewPostContactComboBox.IsEnabled = false;
                                }
                            }
                            else
                            {
                                NewPostComboBox.IsEnabled = true;
                                NewPostContactComboBox.IsEnabled = true;
                            }

                        }
                        else
                        {
                            Folder.MessageBox.ShowError(string.Format("کافو یافت نشد"));
                        }
                    }
                    else
                    {
                        Folder.MessageBox.ShowError(string.Format("بوخت به کافو متصل نمی باشد."));
                    }
                }
                else
                {
                    Folder.MessageBox.ShowError(string.Format("بوخت یافت نشد."));
                }
            }
        }

        private void ResetPostContact()
        {

            NewPostComboBox.SelectedValue = null;
            NewPostContactComboBox.ItemsSource = null;

            NewPostContactComboBox.SelectedValue = null;
            _PostContact = null;
        }
        private void NewPostContactComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewPostContactComboBox.SelectedValue != null)
            {
                _PostContact = PostContactDB.GetFreePostContactByID((long)NewPostContactComboBox.SelectedValue);

            }

        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                DateTime currentDateTime = DB.GetServerDate();
                if (telephoneItem == null)
                    return;

                if(_bucht != null && _PostContact != null)
                {
                    if (_bucht.BuchtTypeID != (int)DB.BuchtType.InLine && _PostContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal)
                    {
                        throw new Exception("برای بوخت غیر پی سی ام نمی توان بوخت پی سی ام انتخاب کرد");
                    }
                }

                using (TransactionScope ts = new TransactionScope())
                {



                    RequestLog requestLog = new RequestLog();
                    requestLog.IsReject = false;
                    requestLog.TelephoneNo = telephoneItem.TelephoneNo;
                    requestLog.UserID = DB.currentUser.ID;

                    if (telephoneItem.Status == (int)DB.TelephoneStatus.Connecting || telephoneItem.Status == (int)DB.TelephoneStatus.Cut)
                    {
                        requestLog.RequestTypeID = (int)DB.RequestType.EditTelephoneInstallation;


                        Data.Schema.EditTelephoneInstallation dayeri = new Data.Schema.EditTelephoneInstallation();
                        dayeri.TelephoneNo = telephoneItem.TelephoneNo;




                        #region Customer
                        if (_customer != null)
                        {
                            telephoneItem.CustomerID = _customer.ID;

                            requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_customer.ID);


                            dayeri.NationalCodeOrRecordNo = _customer.NationalCodeOrRecordNo;
                            dayeri.FirstNameOrTitle = _customer.FirstNameOrTitle;
                            dayeri.LastName = _customer.LastName;
                        }
                        else if (_primaryCustomer != null)
                        {
                            requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_primaryCustomer.ID);
                        }
                        //else
                        //{
                        //    Folder.MessageBox.ShowError("مشترک پیدا نشد");
                        //}
                        #endregion Customer

                        #region install address
                        if (_installAddress != null)
                        {
                            telephoneItem.InstallAddressID = _installAddress.ID;

                            dayeri.InstallPostalCode = _installAddress.PostalCode;
                            dayeri.InstallAddress = _installAddress.AddressContent;
                        }
                        //else
                        //{
                        //    Folder.MessageBox.ShowError("آدرس نصب پیدا نشد");
                        //}
                        #endregion install address

                        #region correspondence address
                        if (_correspondenceAddress != null)
                        {
                            telephoneItem.CorrespondenceAddressID = _correspondenceAddress.ID;

                            dayeri.CorrespondencePostalCode = _correspondenceAddress.PostalCode;
                            dayeri.CorrespondenceAddress = _correspondenceAddress.AddressContent;


                        }
                        //else
                        //{
                        //    Folder.MessageBox.ShowError("آدرس مکاتبه ای پیدا نشد");
                        //}
                        #endregion correspondence address

                        if (_bucht != null)
                        {

                            _bucht.SwitchPortID = telephoneItem.SwitchPortID;
                            _bucht.Status = (int)DB.BuchtStatus.Connection;

                            _bucht.Detach();
                            DB.Save(_bucht);


                            dayeri.Bucht = BuchtDB.GetConnectionByBuchtID(_bucht.ID);

                            dayeri.Cabinet = CabinetDB.GetCabinetByCabinetInputID((long)_bucht.CabinetInputID).CabinetNumber;
                            dayeri.CabinetInput = CabinetInputDB.GetCabinetInputByID((long)_bucht.CabinetInputID).InputNumber;

                        }


                        if (_PostContact != null)
                        {

                          

                            _PostContact.Status = (int)DB.PostContactStatus.CableConnection;
                            if (_bucht != null)
                            {
                                if (_bucht.BuchtTypeID != (int)DB.BuchtType.InLine)
                                {
                                    _bucht.ConnectionID = _PostContact.ID;
                                }
                                _bucht.Status = (int)DB.BuchtStatus.Connection;
                                _bucht.Detach();
                                DB.Save(_bucht);
                            }
                            else if (_primaryBucht != null)
                            {
                                if (_primaryBucht.BuchtTypeID != (int)DB.BuchtType.InLine)
                                {
                                    _primaryBucht.ConnectionID = _PostContact.ID;
                                }

                                _primaryBucht.Status = (int)DB.BuchtStatus.Connection;
                                _primaryBucht.Detach();
                                DB.Save(_primaryBucht);
                            }

                            _PostContact.Detach();
                            DB.Save(_PostContact);

                            dayeri.PostContact = _PostContact.ConnectionNo;

                            Post post = PostDB.GetPostByID(_PostContact.PostID);
                            dayeri.Post = post.Number;

                        }



                        telephoneItem.Detach();
                        DB.Save(telephoneItem);
                        requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.EditTelephoneInstallation>(dayeri, true));
                    }
                    else if (telephoneItem.Status == (int)DB.TelephoneStatus.Free || telephoneItem.Status == (int)DB.TelephoneStatus.Discharge)
                    {

                        if (!InstallationDatePicker.SelectedDate.HasValue) throw new Exception("تاریخ دایری را وارد کنید");

                        DateTime selectedDate = InstallationDatePicker.SelectedDate.Value;
                        
                        DateTime? dateTime =  DB.sh2mi("1393/8/17");

                        if (selectedDate >= dateTime)
                        {
                                throw new Exception(string.Format("تاریخ دایری باید قبل از {0} باشد", Convert.ToDateTime("11/08/2014").ToPersian(Date.DateStringType.Short)));
                        }


                        Request request = new Request();
                        request.ID = DB.GenerateRequestID();
                        request.RequestTypeID = (int)DB.RequestType.Dayri;
                        request.TelephoneNo = telephoneItem.TelephoneNo;
                        request.CenterID = (int)telephoneItem.CenterID;
                        request.CustomerID = _customer.ID;
                        request.RelatedRequestID = null;
                        request.StatusID = 1379;
                        request.CreatorUserID = DB.currentUser.ID;
                        request.InsertDate = selectedDate;
                        request.EndDate = selectedDate;
                        request.RequestDate = currentDateTime;
                        request.IsVisible = true;
                        request.Detach();
                        DB.Save(request, true);

                        _installRequest = InstallInfo.DataContext as InstallRequest;
                        _installRequest.RequestID = request.ID;
                        _installRequest.Status = 1;
                        _installRequest.InstallRequestTypeID = (int)DB.RequestType.Dayri;
                        _installRequest.InstallAddressID = _installAddress.ID;
                        _installRequest.CorrespondenceAddressID = _correspondenceAddress.ID;
                        _installRequest.InstallationDate = selectedDate;

                        telephoneItem.CustomerTypeID = _installRequest.TelephoneType;
                        telephoneItem.CustomerGroupID = _installRequest.TelephoneTypeGroup;
                        telephoneItem.ChargingType = _installRequest.ChargingType;
                        telephoneItem.PosessionType = _installRequest.PosessionType;

                        _installRequest.Detach();
                        DB.Save(_installRequest , true);

                        InvestigatePossibility investigate = new InvestigatePossibility();
                        investigate.RequestID = request.ID;
                        investigate.BuchtID = _bucht.ID;
                        investigate.PostContactID = _PostContact.ID;
                        investigate.ConnectionReserveDate = selectedDate;
                        investigate.Detach();
                        DB.Save(investigate, true);


                        SelectTelephone selectTelephone = new SelectTelephone();
                        selectTelephone.ID = request.ID;
                        selectTelephone.ReserveDate = selectedDate;
                        selectTelephone.TelephoneNo = telephoneItem.TelephoneNo;
                        selectTelephone.Detach();
                        DB.Save(selectTelephone , true);


                        IssueWiring issueWiring = new IssueWiring();
                        issueWiring.RequestID = request.ID;
                        issueWiring.InsertDate =selectedDate;
                        issueWiring.WiringIssueDate = selectedDate;
                        issueWiring.WiringNo = selectedDate.ToPersian(Date.DateStringType.Short);
                        issueWiring.Detach();
                        DB.Save(issueWiring, true);


                        Wiring wiring = new Wiring();
                        wiring.RequestID = request.ID;
                        wiring.IssueWiringID = issueWiring.ID;
                        wiring.InvestigatePossibilityID = investigate.ID;
                        wiring.Status = 1379;
                        wiring.BuchtID = _bucht.ID;
                        wiring.MDFInsertDate = selectedDate;
                        wiring.Detach();
                        DB.Save(wiring, true);

                        Wiring wiringNetwork = new Wiring();
                        wiringNetwork.RequestID = request.ID;
                        wiringNetwork.IssueWiringID = issueWiring.ID;
                        wiringNetwork.WiringInsertDate = selectedDate;
                        wiringNetwork.Status = 1379;
                        wiringNetwork.Detach();
                        DB.Save(wiringNetwork, true);



                        requestLog.RequestTypeID = (int)DB.RequestType.Dayri;


                        Data.Schema.Dayeri dayeri = new Data.Schema.Dayeri();
                        dayeri.TelephoneNo = telephoneItem.TelephoneNo;


                        #region Customer
                        if (_customer != null)
                        {
                            telephoneItem.CustomerID = _customer.ID;

                            requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_customer.ID);

                            dayeri.NationalCodeOrRecordNo = _customer.NationalCodeOrRecordNo;
                            dayeri.FirstNameOrTitle = _customer.FirstNameOrTitle;
                            dayeri.LastName = _customer.LastName;
                        }
                        else if (_primaryCustomer != null)
                        {
                            requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_primaryCustomer.ID);
                        }
                        //else
                        //{
                        //    Folder.MessageBox.ShowError("مشترک پیدا نشد");
                        //}
                        #endregion Customer

                        #region install address
                        if (_installAddress != null)
                        {
                            telephoneItem.InstallAddressID = _installAddress.ID;

                            dayeri.InstallPostalCode = _installAddress.PostalCode;
                            dayeri.InstallAddress = _installAddress.AddressContent;
                        }
                        //else
                        //{
                        //    Folder.MessageBox.ShowError("آدرس نصب پیدا نشد");
                        //}
                        #endregion install address

                        #region correspondence address
                        if (_correspondenceAddress != null)
                        {
                            telephoneItem.CorrespondenceAddressID = _correspondenceAddress.ID;

                            dayeri.CorrespondencePostalCode = _correspondenceAddress.PostalCode;
                            dayeri.CorrespondenceAddress = _correspondenceAddress.AddressContent;


                        }
                        //else
                        //{
                        //    Folder.MessageBox.ShowError("آدرس مکاتبه ای پیدا نشد");
                        //}
                        #endregion correspondence address
                        if (_bucht != null)
                        {

                            _bucht.SwitchPortID = telephoneItem.SwitchPortID;
                            _bucht.Status = (int)DB.BuchtStatus.Connection;

                            _bucht.Detach();
                            DB.Save(_bucht);


                            dayeri.Bucht = BuchtDB.GetConnectionByBuchtID(_bucht.ID);

                            dayeri.Cabinet = CabinetDB.GetCabinetByCabinetInputID((long)_bucht.CabinetInputID).CabinetNumber;
                            dayeri.CabinetInput = CabinetInputDB.GetCabinetInputByID((long)_bucht.CabinetInputID).InputNumber;

                        }


                        if (_PostContact != null)
                        {

                            _PostContact.Status = (int)DB.PostContactStatus.CableConnection;
                            if (_bucht != null)
                            {
                                if (_bucht.BuchtTypeID != (int)DB.BuchtType.InLine)
                                {
                                    _bucht.ConnectionID = _PostContact.ID;
                                }

                                _bucht.Detach();
                                DB.Save(_bucht);
                            }
                            else if (_primaryBucht != null)
                            {
                                if (_primaryBucht.BuchtTypeID != (int)DB.BuchtType.InLine)
                                {
                                    _primaryBucht.ConnectionID = _PostContact.ID;
                                }

                                _primaryBucht.Detach();
                                DB.Save(_primaryBucht);
                            }

                            _PostContact.Detach();
                            DB.Save(_PostContact);

                            dayeri.PostContact = _PostContact.ConnectionNo;

                            Post post = PostDB.GetPostByID(_PostContact.PostID);
                            dayeri.Post = post.Number;

                        }
                        else
                        {
                            throw new Exception("خطا در اطلاعات اتصالی پست");
                        }

                        telephoneItem.InstallationDate = selectedDate;
                        telephoneItem.DischargeDate = null;
                        telephoneItem.CauseOfTakePossessionID = null; 
                        telephoneItem.Status = (int)DB.TelephoneStatus.Connecting;
                        telephoneItem.Detach();
                        DB.Save(telephoneItem);
                        requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.Dayeri>(dayeri, true));

                    }

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog);

                    ts.Complete();

                }
                SearchTelephone(null, null);
                this.Cursor = Cursors.Arrow;
                ShowSuccessMessage("اطلاعات ذخیره شد");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Arrow;
                ShowErrorMessage("خطا در ذخیره مشترک", ex);
            }
        }

        private void TelephoneTypecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TelephoneTypeComboBox.SelectedValue != null)
            {
                TelephoneTypeGroupComboBox.ItemsSource = Data.CustomerGroupDB.GetCustomerGroupsCheckableByCustomerTypeID((int)TelephoneTypeComboBox.SelectedValue);
            }
        }









    }
}
