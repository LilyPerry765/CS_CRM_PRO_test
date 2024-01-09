using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report.Dictionary;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for GeneralInformationForm.xaml
    /// </summary>
    public partial class GeneralInformationForm : Local.PopupWindow
    {
        #region Properties and Fields

        long _TelephoneNo = 0;
        private int CityID = 0;

        AssignmentInfo _assignmentInfo = new AssignmentInfo();

        List<TeleInfo> teleInfoList = new List<TeleInfo>();

        CRM.Data.InstallRequest _InstallRequest = new CRM.Data.InstallRequest();
        CRM.Data.E1 _E1Request = new CRM.Data.E1();
        CRM.Data.SpecialWire _SpecialWireRequest = new CRM.Data.SpecialWire();

        Telephone telephone = null;

        #endregion

        #region Constructor

        public GeneralInformationForm()
        {
            InitializeComponent();
            Initialize();
        }

        public GeneralInformationForm(long? telephoneNo)
            : this()
        {
            _TelephoneNo = telephoneNo ?? 0;
        }

        #endregion

        #region EventHandlers

        private void TelephoneSearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TechInfo.DataContext = null;
                CustomerInfo.DataContext = null;
                AddressInfo.DataContext = null;
                InstallUserControll.DataContext = null;
                SpecialWireInstallInfo.DataContext = null;
                E1InstallInfo.DataContext = null;
                InstallInfo.Visibility = Visibility.Collapsed;
                E1InstallInfo.Visibility = Visibility.Collapsed;
                SpecialWireInstallInfo.Visibility = Visibility.Collapsed;

                RequestPaymentDataGrid.ItemsSource = null;
                PhoneRecordsInfoDataGrid.ItemsSource = null;
                //PCMCabinetInputBuchtTextBox.Text = string.Empty;
                ItemsDataGrid.ItemsSource = null;
                string nationalCode = string.Empty;

                if (!string.IsNullOrEmpty(NationalCodeSearchTextBox.Text.Trim()))
                {
                    nationalCode = NationalCodeSearchTextBox.Text.Trim();
                    TelephoneTextBox.Text = string.Empty;
                    _TelephoneNo = 0;
                    TechInfo.DataContext = null;
                    CustomerInfo.DataContext = null;
                    ItemsDataGrid.ItemsSource = null;

                    List<Telephone> teleList = Data.TelephoneDB.GetTelephoneByNationalCode(nationalCode);
                    teleInfoList = Data.TelephoneDB.GetTelephoneInfoByTelePhonNo(teleList).ToList();
                    TelephoneDataGrid.ItemsSource = teleInfoList;
                }
                else if (!string.IsNullOrEmpty(TelephoneTextBox.Text.Trim()))
                {
                    _TelephoneNo = Convert.ToInt64(TelephoneTextBox.Text.Trim());
                    Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                    if (tele != null && !(tele.Status == (byte)DB.TelephoneStatus.Discharge || tele.Status == (byte)DB.TelephoneStatus.Free))
                    {
                        teleInfoList = TelephoneDB.GetAllTelphonOfCustomerByTelephonNo(tele);
                        TelephoneDataGrid.ItemsSource = teleInfoList;
                        TelephoneDataGrid.SelectedItem = teleInfoList.Where(t => t.TelephoneNo == _TelephoneNo).SingleOrDefault();
                        TelephoneDataGrid_SelectionChanged(null, null);
                    }
                    else if (tele != null && (tele.Status == (byte)DB.TelephoneStatus.Discharge || tele.Status == (byte)DB.TelephoneStatus.Free))
                    {
                        teleInfoList = Data.TelephoneDB.GetTelephoneInfoByTelePhonNo(new List<Telephone> { tele }).ToList();
                        TelephoneDataGrid.ItemsSource = teleInfoList;
                        TelephoneDataGrid.SelectedItem = teleInfoList.Where(t => t.TelephoneNo == _TelephoneNo).SingleOrDefault(); ;
                        TelephoneDataGrid_SelectionChanged(null, null);
                    }
                    else
                    {
                        TelephoneTextBox.Focus();
                        MessageBox.Show("رکوردی یافت نشد.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در نمایش اطلاعات", ex);
                Logger.Write(ex, "خطا در جستجوی تلفن - فرم اطلاعات جامع");
            }

            this.ResizeWindow();
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_TelephoneNo != 0)
            {
                TelephoneTextBox.Text = Convert.ToString(_TelephoneNo);
                TelephoneSearchButton_Click(null, null);
            }
            else
            {
                PersonRadioButton.IsSelected = true;
            }
            this.ResizeWindow();
        }

        private void TelephoneDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TelephoneDataGrid.SelectedItem != null)
            {
                TeleInfo teleInfo = TelephoneDataGrid.SelectedItem as TeleInfo;
                if (teleInfo != null && teleInfo.Status != (byte)DB.TelephoneStatus.Discharge && teleInfo.Status != (byte)DB.TelephoneStatus.Free)
                {
                    SearchTelephon((long)teleInfo.TelephoneNo);
                }
            }
        }

        private void CustomerUpdate(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = CustomerInfo.DataContext as Customer;
                //TODO:rad 13960123 بلاک زیر بدین خاطر اضافه شد که ساختار شاهکار رعایت شود
                //باید بعد از هر ویرایش اطلاعات مشترک مجدداً از شاهکار احراز هویت بگیرد
                if (customer.PersonType == (byte)DB.PersonType.Person)
                {
                    customer.IsAuthenticated = null;
                }
                //******************************************************************************
                customer.ChangeDate = DB.GetServerDate();
                customer.Detach();
                Save(customer);
                ShowSuccessMessage("به روزرسانی با موفقیت انجام شد.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("Exception in GeneralInformationForm - CustomerUpdateButton : {0}", ex.Message);
                ShowErrorMessage("خطا در به روزرسانی", ex);
            }
        }

        private void AddressSaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                Address address = AddressInfo.DataContext as Address;
                address.ChangeDate = DB.GetServerDate();
                address.Detach();
                Save(address);
                ShowSuccessMessage("به روزرسانی با موفقیت انجام شد.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("Exception in GeneralInformationForm - AddressSaveForm : {0}", ex.Message);
                ShowErrorMessage("خطا در به روزرسانی", ex);
            }

            //TODO:rad 13950517 
            //اگر بخواهیم در لاگ ویرایش مشخصات آدرس ، فیلد تلفن و مرکز ثبت شود تا وب سرویس اطلاعات را مشابه سایر روال های برنامه ارسال کندو نه خالی
            //DB.SaveEditAddressLog(address, telephone);
            //Request editAddressRequest = new Request();
            //editAddressRequest.RequestTypeID = (int)DB.RequestType.EditAddress;
            //editAddressRequest.CenterID=
        }

        private void InstallSaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    UserControls.Install install = InstallUserControll.Content as UserControls.Install;

                    _InstallRequest.TelephoneType = (int)install.TelephoneTypeComboBox.SelectedValue;
                    _InstallRequest.TelephoneTypeGroup = (int)install.TelephoneTypeGroupComboBox.SelectedValue;
                    _InstallRequest.ChargingType = (byte)install.ChargingTypecomboBox.SelectedValue;
                    _InstallRequest.PosessionType = (byte)install.PosessionTypecomboBox.SelectedValue;
                    _InstallRequest.Detach();
                    DB.Save(_InstallRequest);

                    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_assignmentInfo.TelePhoneNo ?? 0);
                    telephone.CustomerTypeID = _InstallRequest.TelephoneType;
                    telephone.CustomerGroupID = _InstallRequest.TelephoneTypeGroup;
                    telephone.ChargingType = _InstallRequest.ChargingType;
                    telephone.PosessionType = _InstallRequest.PosessionType;
                    telephone.ChangeDate = DB.GetServerDate();
                    telephone.Detach();
                    DB.Save(telephone);

                    ts.Complete();
                }

                MessageBox.Show("بروز رسانی انجام شد", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در بروز رسانی", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex, "خطا در بروزرسانی مشخصات دایری - فرم اطلاعات جامع");
            }
        }

        private void E1InstallSaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    UserControls.E1 E1install = E1InstallUserControll.Content as UserControls.E1;

                    _E1Request.TelephoneType = (int)E1install.TelephoneTypeComboBox.SelectedValue;
                    _E1Request.TelephoneTypeGroup = (int)E1install.TelephoneTypeGroupComboBox.SelectedValue;
                    _E1Request.Detach();
                    DB.Save(_E1Request);

                    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_assignmentInfo.TelePhoneNo ?? 0);
                    telephone.CustomerTypeID = _E1Request.TelephoneType;
                    telephone.CustomerGroupID = _E1Request.TelephoneTypeGroup;
                    telephone.ChangeDate = DB.GetServerDate();
                    telephone.Detach();
                    DB.Save(telephone);

                    ts.Complete();
                }

                MessageBox.Show("بروز رسانی انجام شد", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در بروز رسانی", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex, "خطا در بروزرسانی مشخصات دایری ایوان - فرم اطلاعات جامع");
            }
        }

        private void SpecialWireInstallSaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    UserControls.SpecialWireUserControl specialWireinstall = SpecialWireInstallUserControll.Content as UserControls.SpecialWireUserControl;

                    _SpecialWireRequest.CustomerTypeID = (int)specialWireinstall.TelephoneTypeComboBox.SelectedValue;
                    _SpecialWireRequest.CustomerGroupID = (int)specialWireinstall.TelephoneTypeGroupComboBox.SelectedValue;
                    _SpecialWireRequest.Detach();
                    DB.Save(_SpecialWireRequest);

                    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_assignmentInfo.TelePhoneNo ?? 0);
                    telephone.CustomerTypeID = _SpecialWireRequest.CustomerTypeID;
                    telephone.CustomerGroupID = _SpecialWireRequest.CustomerGroupID;
                    telephone.ChangeDate = DB.GetServerDate();
                    telephone.Detach();
                    DB.Save(telephone);

                    ts.Complete();
                }

                MessageBox.Show("بروز رسانی انجام شد", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در بروز رسانی", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex, "خطا در بروزرسانی مشخصات دایری سیم خصوصی - فرم اطلاعات جامع");
            }
        }

        private void TabControlForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (_TelephoneNo != 0)
                {
                    this.Cursor = Cursors.Wait;
                    if (PhoneRecordsInfo != null && PhoneRecordsInfo.IsSelected)
                    {
                        PhoneRecordsInfoDataGrid.ItemsSource = null;
                        // for not Hamper error in get reqeustLogs in load form, The Try was considered to be
                        List<RequestLog> reqeustLogs = new List<RequestLog>();
                        try
                        {
                            reqeustLogs = Data.RequestLogDB.GetReqeustLogByTelephone(_TelephoneNo);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("در دریافت سوابق تلفن خطا رخ داده است.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                            Logger.Write(ex, "خطا در دریافت سوابق - فرم اطلاعات جامع");
                        }
                        PhoneRecordsInfoDataGrid.ItemsSource = reqeustLogs;
                    }
                    else if (RequestPaymentTabItem != null && RequestPaymentTabItem.IsSelected)
                    {
                        RequestPaymentDataGrid.ItemsSource = null;
                        RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetInstalationPaymentbyTelephonNo(_TelephoneNo);
                    }
                    else if (PCMInfo != null && PCMInfo.IsSelected)
                    {
                        if (_assignmentInfo != null && _assignmentInfo.MUID != null)
                        {
                            int count = 0;
                            ItemsDataGrid.ItemsSource = DB.GetTotalInformationBucht(new List<int> { }, new List<int> { (int)_assignmentInfo.CenterID }, new List<int> { }, -1, -1, new List<int> { _assignmentInfo.CabinetID ?? 0 }, _assignmentInfo.InputNumber ?? -1, new List<int> { }, -1, string.Empty, -1, -1, 0, 10, -1, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }, null, new List<int> { }, new List<int> { }, null, null, out count, null, null, null, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }, true);

                            //if (_assignmentInfo.CabinetInputID != null)
                            //{
                            //    PCMCabinetInputBuchtTextBox.Text = BuchtDB.GetPCMCabinetInputBuchtInput((long)_assignmentInfo.CabinetInputID);
                            //}
                        }
                    }
                    else if (InstallInfo != null && InstallInfo.IsSelected)
                    {
                        InstallUserControll.DataContext = null;
                        if (telephone != null && (telephone.UsageType == null || telephone.UsageType == (int)DB.TelephoneUsageType.Usuall || telephone.UsageType == (int)DB.TelephoneUsageType.GSM))
                        {
                            _InstallRequest = Data.InstallRequestDB.GetLastInstallRequestByTelephone(telephone.TelephoneNo);
                            InstallUserControll.CustomeGroupBox.Visibility = Visibility.Collapsed;
                            InstallUserControll.AddressGroupBox.Visibility = Visibility.Collapsed;


                            if (_InstallRequest != null && _InstallRequest.ID != 0)
                            {
                                UserControls.Install install = new UserControls.Install(_InstallRequest.RequestID);
                                install.CustomeGroupBox.Visibility = Visibility.Collapsed;
                                install.AddressGroupBox.Visibility = Visibility.Collapsed;
                                //milad doran :install.GSMMessageTextBlock.Visibility = Visibility.Collapsed;
                                //TODO:rad 13950225
                                install.IsGsmAnouncementUserControl.Visibility = Visibility.Collapsed;
                                install.NearestTelephonTextBox.IsEnabled = false;
                                install.LastNameAt118TextBox.IsEnabled = false;
                                install.TitleAt118TextBox.IsEnabled = false;
                                install.RegisterAt118CheckBox.IsEnabled = false;
                                install.ZeroBlockComboBox.IsEnabled = false;
                                install.OrderTypecomboBox.IsEnabled = false;
                                install.InquiryInvestigatePossibility.IsEnabled = false;
                                install.ADSLCheckBox.IsEnabled = false;
                                install.GSMCheckBox.IsEnabled = false;

                                InstallUserControll.Content = install;
                                InstallUserControll.DataContext = install;
                                InstallInfo.Visibility = Visibility.Visible;
                            }
                        }
                        else if (telephone != null && (telephone.UsageType == (int)DB.TelephoneUsageType.E1))
                        {
                            _E1Request = Data.E1DB.GetLastE1RequestByTelephone(telephone.TelephoneNo);
                            E1InstallUserControll.CustomeGroupBox.Visibility = Visibility.Collapsed;
                            E1InstallUserControll.AddressInfoGroupBox.Visibility = Visibility.Collapsed;

                            if (_E1Request != null && _E1Request.RequestID != 0)
                            {
                                UserControls.E1 E1install = new UserControls.E1(_E1Request.RequestID, DB.RequestType.E1, telephone.TelephoneNo);
                                E1install.CustomeGroupBox.Visibility = Visibility.Collapsed;
                                E1install.LinkTypeComboBox.IsEnabled = false;
                                E1install.CodeTypeComboBox.IsEnabled = false;
                                E1install.ChanalTypeComboBox.IsEnabled = false;
                                E1install.NumberOfLineTextBox.IsEnabled = false;
                                E1install.ConnectionNoTypeComboBox.IsEnabled = false;
                                E1install.CompanyCodeComboBox.IsEnabled = false;
                                E1install.LineTypeComboBox.IsEnabled = false;
                                E1install.AddressInfoGroupBox.IsEnabled = false;

                                E1InstallUserControll.Content = E1install;
                                E1InstallUserControll.DataContext = E1install;
                                E1InstallInfo.Visibility = Visibility.Visible;
                            }
                        }
                        else if (telephone != null && (telephone.UsageType == (int)DB.TelephoneUsageType.PrivateWire))
                        {
                            _SpecialWireRequest = Data.SpecialWireDB.GetLastSpecialWireRequestByTelephone(telephone.TelephoneNo);
                            SpecialWireInstallUserControll.CustomeGroupBox.Visibility = Visibility.Collapsed;
                            SpecialWireInstallUserControll.AddressInfoGroupBox.Visibility = Visibility.Collapsed;

                            if (_SpecialWireRequest != null && _SpecialWireRequest.RequestID != 0)
                            {
                                UserControls.SpecialWireUserControl specialWireinstall = new UserControls.SpecialWireUserControl(_SpecialWireRequest.RequestID);
                                specialWireinstall.CustomeGroupBox.Visibility = Visibility.Collapsed;
                                specialWireinstall.TelephoneNoComboBox.IsEnabled = false;
                                specialWireinstall.CompanyCodeComboBox.IsEnabled = false;
                                specialWireinstall.BuchtTypeComboBox.IsEnabled = false;
                                specialWireinstall.AddressGroupBox.IsEnabled = false;
                                specialWireinstall.PointsInfoGroupBox.IsEnabled = false;

                                SpecialWireInstallUserControll.Content = specialWireinstall;
                                SpecialWireInstallUserControll.DataContext = specialWireinstall;
                                SpecialWireInstallInfo.Visibility = Visibility.Visible;
                            }
                        }
                    }
                    this.Cursor = Cursors.Arrow;
                }
            }
        }

        //TODO :rad
        private void PrintCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (Helper.AllPropertyIsEmpty(_assignmentInfo)) ? false : true;
        }

        private void PrintCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                AssignmentShortInfo assignmentSelection = new AssignmentShortInfo();

                //Customer info
                assignmentSelection.TelephoneNo = (_assignmentInfo.TelePhoneNo.HasValue) ? _assignmentInfo.TelePhoneNo.Value.ToString() : string.Empty;
                assignmentSelection.CustomerID = _assignmentInfo.Customer.ID.ToString();
                assignmentSelection.Gender = (_assignmentInfo.Customer.Gender.HasValue) ? _assignmentInfo.Customer.Gender.Value.ToString() : string.Empty;
                assignmentSelection.PersonType = _assignmentInfo.Customer.PersonType.ToString();
                assignmentSelection.FirstNameOrTitle = _assignmentInfo.Customer.FirstNameOrTitle;
                assignmentSelection.LastName = _assignmentInfo.Customer.LastName;
                assignmentSelection.BirthCertificateID = _assignmentInfo.Customer.BirthCertificateID;
                assignmentSelection.IssuePlace = _assignmentInfo.Customer.IssuePlace;
                assignmentSelection.NationalCodeOrRecordNo = _assignmentInfo.Customer.NationalCodeOrRecordNo;
                assignmentSelection.BirthDateOrRecordDate = (_assignmentInfo.Customer.BirthDateOrRecordDate.HasValue) ? _assignmentInfo.Customer.BirthDateOrRecordDate.Value.ToPersian(Date.DateStringType.Short) : string.Empty;
                assignmentSelection.UrgentTelNo = _assignmentInfo.Customer.UrgentTelNo;
                assignmentSelection.MobileNo = _assignmentInfo.Customer.MobileNo;
                assignmentSelection.Email = _assignmentInfo.Customer.Email;
                assignmentSelection.Agency = _assignmentInfo.Customer.Agency;
                assignmentSelection.AgencyNumber = _assignmentInfo.Customer.AgencyNumber;
                assignmentSelection.FatherName = _assignmentInfo.Customer.FatherName;

                //Technical info
                assignmentSelection.CityName = _assignmentInfo.CityName;
                assignmentSelection.CenterName = _assignmentInfo.CenterName;
                assignmentSelection.CabinetName = (_assignmentInfo.CabinetName.HasValue) ? _assignmentInfo.CabinetName.Value.ToString() : string.Empty;
                assignmentSelection.InputNumber = (_assignmentInfo.InputNumber.HasValue) ? _assignmentInfo.InputNumber.Value.ToString() : string.Empty;
                assignmentSelection.PostContact = (_assignmentInfo.PostContact.HasValue) ? _assignmentInfo.PostContact.Value.ToString() : string.Empty;
                assignmentSelection.Connection = _assignmentInfo.Connection;
                assignmentSelection.OtherBucht = _assignmentInfo.OtherBucht;
                assignmentSelection.MUID = _assignmentInfo.MUID;
                assignmentSelection.PAPName = _assignmentInfo.PAPName;
                assignmentSelection.ADSLBucht = _assignmentInfo.ADSLBucht;
                assignmentSelection.Address = _assignmentInfo.Address;
                assignmentSelection.PostalCode = _assignmentInfo.PostallCode;
                assignmentSelection.PostName = (_assignmentInfo.PostName.HasValue) ? _assignmentInfo.PostName.Value.ToString() : string.Empty;
                assignmentSelection.AorBType = _assignmentInfo.AorBType;
                //assignmentSelection.PcmCabinetInputBucht = _assignmentInfo.OtherBucht;
                assignmentSelection.CauseofCut = _assignmentInfo.CauseOfCut;
                assignmentSelection.ClassTelephone = _assignmentInfo.ClassTelephone;
                assignmentSelection.SpecialServices = _assignmentInfo.SpecialServices;
                assignmentSelection.RequestPaymentAmountSum = _assignmentInfo.RequestPaymentAmountSum;

                //TODO:rad نیاز به اصلاح دارد
                //در فکس 2 صفحه ای 13940210 خواسته بودند تا 
                //A or B
                //بودن پست مشخص شود  البته بهتر است این فیلد برای گزارش جداگانه ارسال شود نه این که همراه با شماره پست ارسال شود
                assignmentSelection.PostName = string.Format("{0} ({1})", assignmentSelection.PostName, assignmentSelection.AorBType);

                //result for printing.
                List<AssignmentShortInfo> result = new List<AssignmentShortInfo>() { assignmentSelection };

                //Stivarible for reportDate
                StiVariable variable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));

                ReportBase.SendToPrint(result, (int)DB.UserControlNames.GeneralInformationReport, variable);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در کامند چاپ - اطلاعات جامع - بخش مشترکین");
                MessageBox.Show("خطا در چاپ", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        #endregion

        #region Methods

        private void SearchTelephon(long telephonNo)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                _assignmentInfo = DB.GetGeneralInformationByTelephoneNo(telephonNo);

                if (_assignmentInfo != null && !string.IsNullOrEmpty(_assignmentInfo.OtherBuchtNo.ToString()))
                {
                    _assignmentInfo.OtherBucht = "ردیف : " + _assignmentInfo.OtherVerticalColumnNo.ToString() + " ،  " + "طبقه : " + _assignmentInfo.OtherVerticalRowNo.ToString() + " ،  " + "اتصالی : " + _assignmentInfo.OtherBuchtNo.ToString();
                }

                telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephonNo);

                if (_assignmentInfo != null)
                {
                    TechInfo.DataContext = _assignmentInfo;
                    CustomerInfo.DataContext = _assignmentInfo.Customer;

                    if (_assignmentInfo.InstallAddress != null)
                    {
                        CityID = Data.AddressDB.GetCity(_assignmentInfo.InstallAddress.ID);
                        AddressCityComboBox.SelectedValue = CityID;
                        AddressCityComboBox_SelectionChanged(null, null);
                        AddressInfo.DataContext = _assignmentInfo.InstallAddress;
                    }
                }
                else if (telephone != null)
                {
                    Address address = AddressDB.GetAddressByID(telephone.InstallAddressID ?? 0);
                    Customer customer = CustomerDB.GetCustomerByID(telephone.CustomerID ?? 0);

                    CustomerInfo.DataContext = customer;
                    AddressInfo.DataContext = address;
                }

                if (telephone != null && (telephone.UsageType == null || telephone.UsageType == (int)DB.TelephoneUsageType.Usuall || telephone.UsageType == (int)DB.TelephoneUsageType.GSM))
                {
                    InstallInfo.Visibility = Visibility.Visible;
                }
                else if (telephone != null && (telephone.UsageType == (int)DB.TelephoneUsageType.E1))
                {
                    E1InstallInfo.Visibility = Visibility.Visible;
                }
                else if (telephone != null && (telephone.UsageType == (int)DB.TelephoneUsageType.PrivateWire))
                {
                    SpecialWireInstallInfo.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در متد SearchTelephon - فرم اطلاعات جامع");
                ShowErrorMessage(".فراخوانی اطلاعات دارای خطا میباشد", ex);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void Initialize()
        {
            PaymentTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            CostTitleColumn.ItemsSource = Data.BaseCostDB.GetBaseCostCheckable();
            OtherCostTitleColumn.ItemsSource = Data.OtherCostDB.GetOtherCostCheckable();
            ReqeustDataGridComboBoxColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.RequestType));
            AddressCityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void AddressCityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddressCityComboBox.SelectedValue != null)
            {
                AddressCenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId((int)AddressCityComboBox.SelectedValue);
            }
        }

        #endregion

    }

}
