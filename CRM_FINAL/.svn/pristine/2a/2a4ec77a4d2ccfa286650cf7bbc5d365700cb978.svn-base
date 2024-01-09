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
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class ADSLAssignmentLines : Local.RequestFormBase
    {
        #region Properties

        private Request _Request { get; set; }
        private ADSLRequest _ADSLRequest { get; set; }
        private Telephone _Telephone { get; set; }
        private Customer _Customer { get; set; }
        private ADSLServiceInfo _ADSLTariff { get; set; }
        private TelephoneSummenryInfo _TeleInfo { get; set; }

        #endregion

        #region Constructors

        public ADSLAssignmentLines(long requestID)
        {
            RequestID = requestID;

            InitializeComponent();
            Initialize();
            LoadData();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            //if (this.IsValidationMode == false)
            //{
                _Request = Data.RequestDB.GetRequestByID(RequestID);
                _ADSLRequest = ADSLRequestDB.GetADSLRequestByID(RequestID);
                //_Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_Request.TelephoneNo);
                _Customer = Data.CustomerDB.GetCustomerByID(_ADSLRequest.CustomerOwnerID);
                _ADSLTariff = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSLRequest.ServiceID);
                //_TeleInfo = Data.TelephoneDB.GetTelephoneSummneryInfoByTelephoneNo(_Request.TelephoneNo);
                _TeleInfo = GetTelephoneInfoByTelephoneNoWebService((long)_Request.TelephoneNo);
                ADSLEquipmentComboBox.ItemsSource = Data.ADSLEquipmentDB.GetAllADSLEquipment();

                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
           // }
        }

        private void LoadData()
        {
            ADSLTelephoneInfo.DataContext = _TeleInfo;

            //ADSLOwnerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (byte)_ADSLRequest.CustomerOwnerStatus);
            //NationalCodeTextBox.Text = _Customer.NationalCodeOrRecordNo.ToString();
            //CustomerNameTextBox.Text = _Customer.FirstNameOrTitle + " " + _Customer.LastName;
            ADSLOwnerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSLRequest.CustomerOwnerStatus);
            //-----
            //ServiceTypeTetxBox.Text = ADSLServiceDB.GetADSLServiceGroupById(_ADSLRequest.ServiceType).Title;
            //ServiceCostTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLServiceCostPaymentType), (byte)_ADSLRequest.ServiceCostPaymentType);
            //-----
            CustomerPriorityTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLCustomerPriority), (byte)_ADSLRequest.CustomerPriority);
            //RegisterProjectTypeTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLRegistrationProjectType), (byte)_ADSLRequest.RegistrationProjectType);
            RequiredInstalationCheckBox.IsChecked = _ADSLRequest.RequiredInstalation;
            NeedModemCheckBox.IsChecked = _ADSLRequest.NeedModem;

            TariffInfo.DataContext = _ADSLTariff;
            LicenceLetterNoTextBox.Text = _ADSLRequest.LicenseLetterNo;

            ResizeWindow();

            if (_ADSLRequest.ADSLPortID != null)
            {
                ADSLPort port = DB.SearchByPropertyName<ADSLPort>("ID", _ADSLRequest.ADSLPortID).SingleOrDefault();
                ADSLEquipmentComboBox.SelectedValue = port.ADSLEquipmentID;
            }
            else
            {
                List<ADSLPort> ADSLPortList;// = new List<ADSLPort>();
                List<ADSLEquipment> ADSLEquipmentList = Data.ADSLEquipmentDB.GetAllADSLEquipment();

                foreach (ADSLEquipment currenrEquipment in ADSLEquipmentList)
                {
                    ADSLPortList = DB.SearchByPropertyName<ADSLPort>("ADSLEquipmentID", currenrEquipment.ID);
                    foreach (ADSLPort port in ADSLPortList)
                    {
                        if (port.Status == (byte)DB.ADSLPortStatus.Free)
                        {
                            ADSLEquipmentComboBox.SelectedValue = currenrEquipment.ID;
                            return;
                        }
                    }
                }
            }
        }

        public override bool Save()
        {
            try
            {
                if (PortInfo.DataContext != null)
                {
                    ADSLPortsInfo portInfo = PortInfo.DataContext as ADSLPortsInfo;

                    if (portInfo.InputBucht == null)
                        throw new Exception("برای این پورت بوخت ورودی تعیین نشده است ! ");

                    //if (portInfo.OutputBucht == null)
                    //    throw new Exception("برای این پورت بوخت خروجی تعیین نشده است ! ");

                    ADSLPort port = ADSLPortDB.GetADSlPortByID(portInfo.ID);
                    _ADSLRequest = ADSLRequestDB.GetADSLRequestByID(RequestID);

                    if (_ADSLRequest.ADSLPortID != null)
                    {
                        if (_ADSLRequest.ADSLPortID != port.ID)
                        {
                            ADSLPort oldPort = DB.SearchByPropertyName<ADSLPort>("ID", _ADSLRequest.ADSLPortID).SingleOrDefault();
                            oldPort.Status = (byte)DB.ADSLPortStatus.Free;

                            oldPort.Detach();
                            DB.Save(oldPort);
                        }
                    }

                    port.Status = (byte)DB.ADSLPortStatus.reserve;
                    _ADSLRequest.ADSLPortID = port.ID;

                    RequestForADSL.SaveADSLTechnicalEquipment(_ADSLRequest, null, null, null, port, false);
                }
                else
                    throw new Exception("پورتی جهت تخصیص انتخاب نشده است !");

                ShowSuccessMessage("پورت انتخاب شده برای این شماره رزرو شد.");

                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            return IsSaveSuccess;
        }

        public override bool Forward()
        {
            try
            {
                if (PortInfo.DataContext != null)
                {
                    ADSLPortsInfo portInfo = PortInfo.DataContext as ADSLPortsInfo;

                    if (portInfo.InputBucht == null)
                        throw new Exception("برای این پورت بوخت ورودی تعیین نشده است ! ");

                    //if (portInfo.OutputBucht == null)
                    //    throw new Exception("برای این پورت بوخت خروجی تعیین نشده است ! ");

                    ADSLPort port = DB.SearchByPropertyName<ADSLPort>("ID", portInfo.ID).SingleOrDefault();
                    _ADSLRequest = DB.SearchByPropertyName<ADSLRequest>("ID", RequestID).SingleOrDefault();

                    if (_ADSLRequest.ADSLPortID != null)
                    {
                        if (_ADSLRequest.ADSLPortID != port.ID)
                        {
                            ADSLPort oldPort = DB.SearchByPropertyName<ADSLPort>("ID", _ADSLRequest.ADSLPortID).SingleOrDefault();
                            oldPort.Status = (byte)DB.ADSLPortStatus.Free;

                            oldPort.Detach();
                            DB.Save(oldPort);
                        }
                    }

                    port.Status = (byte)DB.ADSLPortStatus.reserve;
                    _ADSLRequest.ADSLPortID = port.ID;

                    _ADSLRequest.AssignmentLineDate = DB.GetServerDate();
                    _ADSLRequest.AssignmentLineUserID = DB.CurrentUser.ID;
                    _ADSLRequest.AssignmentLineCommnet = "";

                    RequestForADSL.SaveADSLTechnicalEquipment(_ADSLRequest, null, null, null, port, false);

                    IsForwardSuccess = true;
                }
                else
                    throw new Exception("پورتی جهت تخصیص انتخاب نشده است !");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            return IsForwardSuccess;
        }

        public override bool Deny()
        {
            return IsRejectSuccess;
        }

        private TelephoneSummenryInfo GetTelephoneInfoByTelephoneNoWebService(long telephoneNo)
        {
            TelephoneSummenryInfo teleInfo = new TelephoneSummenryInfo();
            Service1 service = new Service1();
            System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", telephoneNo.ToString());

            if (telephoneInfo.Rows.Count != 0)
            {
                teleInfo.TelephoneNo = Convert.ToInt64(telephoneNo);
                teleInfo.NationalCodeOrRecordNo = telephoneInfo.Rows[0]["MelliCode"].ToString();
                teleInfo.CustomerName = telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                teleInfo.Center = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"])).CenterName;
                teleInfo.PostalCode = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                teleInfo.Address = telephoneInfo.Rows[0]["ADDRESS"].ToString();
            }
            return teleInfo;
        }

        #endregion

        #region Event Handlers

        private void ADSLEquipmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ADSLEquipment equipment = new ADSLEquipment();
            if (ADSLEquipmentComboBox.SelectedItem != null)
            {
                equipment = ADSLEquipmentComboBox.SelectedItem as ADSLEquipment;
                if ((bool)AlBuchtCheckBox.IsChecked)
                    ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentID((int)equipment.ID);
                else
                    ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus(equipment.ID, (byte)DB.ADSLPortStatus.Free);

                StatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPortStatus));
            }
            else
            {
                if (_ADSLRequest.ADSLPortID != null)
                    ADSLEquipmentComboBox.SelectedValue = DB.SearchByPropertyName<ADSLPort>("ID", _ADSLRequest.ADSLPortID).SingleOrDefault().ADSLEquipmentID;
                //else
                //    ADSLEquipmentComboBox.SelectedIndex = 0;
            }

            if (_ADSLRequest.ADSLPortID != null)
            {
                ADSLPortsInfo port = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSLRequest.ADSLPortID);
                PortInfo.DataContext = port;
            }
            else
            {
                ADSLPortsInfo port = new ADSLPortsInfo();
                if (ADSLEquipmentComboBox.SelectedValue != null)
                    port = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free).FirstOrDefault();
                else
                {
                    port = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus(equipment.ID, (byte)DB.ADSLPortStatus.Free).FirstOrDefault();
                }
                PortInfo.DataContext = port;

                //List<ADSLPort> ADSLPortList;// = new List<ADSLPort>();
                //List<ADSLEquipment> ADSLEquipmentList = Data.ADSLEquipmentDB.GetAllADSLEquipment();

                //foreach (ADSLEquipment currenrEquipment in ADSLEquipmentList)
                //{
                //    ADSLPortList = DB.SearchByPropertyName<ADSLPort>("ADSLEquipmentID", currenrEquipment.ID);
                //    foreach (ADSLPort currentPort in ADSLPortList)
                //    {
                //        if (currentPort.Status == (byte)DB.ADSLPortStatus.Free)
                //        {
                //            ADSLEquipmentComboBox.SelectedValue = currenrEquipment.ID;
                //            return;
                //        }
                //    }
                //}
            }
        }

        private void AlBuchtCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (ADSLEquipmentComboBox.SelectedValue != null)
            {
                ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentID((int)ADSLEquipmentComboBox.SelectedValue);

                ADSLPortsInfo port = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free).FirstOrDefault();
                PortInfo.DataContext = port;
            }
            else
                if ((ADSLEquipmentComboBox.SelectedIndex == 0) || (ADSLEquipmentComboBox.SelectedItem != null))
                {
                    ADSLEquipment Equipment = ADSLEquipmentComboBox.SelectedItem as ADSLEquipment;
                    ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentID(Equipment.ID);
                }
        }

        private void AlBuchtCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ADSLEquipmentComboBox.SelectedValue != null)
            {
                ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free);

                ADSLPortsInfo port = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free).FirstOrDefault();
                PortInfo.DataContext = port;
            }
            else
                if ((ADSLEquipmentComboBox.SelectedIndex == 0) || (ADSLEquipmentComboBox.SelectedItem != null))
                {
                    ADSLEquipment Equipment = ADSLEquipmentComboBox.SelectedItem as ADSLEquipment;
                    ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus(Equipment.ID, (byte)DB.ADSLPortStatus.Free);
                }
        }

        private void ADSLPortDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ADSLPortDataGrid.SelectedItem != null)
            {
                try
                {
                    ADSLPortsInfo port = ADSLPortDataGrid.SelectedItem as ADSLPortsInfo;

                    if (port.StatusID == (byte)DB.ADSLPortStatus.Free)
                    {
                        PortInfo.DataContext = port;
                        HideMessage();
                    }
                    else
                    {
                        PortInfo.DataContext = null;
                        throw new Exception("لطفا یک پورت آزاد انتخاب نمایید !");
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        private void SavedValueLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (_ADSLRequest.ADSLPortID != null)
                {
                    ADSLPortsInfo port = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSLRequest.ADSLPortID);
                    PortInfo.DataContext = port;
                }
                else
                    throw new Exception("پورتی برای این درخواست ذخیره نشده است !");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        #endregion
    }
}
