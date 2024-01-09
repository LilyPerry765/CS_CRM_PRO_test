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
using System.Xml.Linq;
using CRM.Data.Services;
using CookComputing.XmlRpc;
using System.Collections;
using System.Security.Cryptography;

namespace CRM.Application.Views
{
    public partial class ADSLMDF : Local.RequestFormBase
    {
        #region Properties

        private Request _Request { get; set; }
        private ADSLRequest _ADSLRequest { get; set; }
        private ADSL _ADSL { get; set; }
        private Data.ADSLPAPRequest _ADSLPAPRequest { get; set; }
        private Data.ADSLDischarge _ADSLDischargeRequest { get; set; }
        private Data.ADSLChangePort1 _ADSLChangePortRequest { get; set; }
        private Data.ADSLChangePlace _ADSLChangePlaceRequest { get; set; }
        private Telephone _Telephone { get; set; }
        private SwitchPort _SwitchPort { get; set; }
        private Bucht _Bucht { get; set; }
        private Center _Center { get; set; }
        private Address _CustomerAddress { get; set; }
        private Service1 aDSLService { get; set; }
        private RequestLog _RequestLog { get; set; }
        private bool _IsForForward = false;
        System.Data.DataTable telephoneInfo { get; set; }
        public int tarrifId { get; set; }
        private string _UserID { get; set; }

        public static long? OldCustomerOwnerID { get; set; }
        public static string OldCustomerOwnerName { get; set; }
        public static int? OldModemID { get; set; }
        public static byte? OldCustomerOwnerStatus { get; set; }
        private ADSLModemProperty _ADSLModemProperty { get; set; }

        private UserControls.ADSLChangePlaceUserControl _ADSLChangePlaceUserControl;

        #endregion

        #region Constructors

        public ADSLMDF(long requestID)
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
            if (DB.City == "semnan")
                aDSLService = new Service1();

            _Request = Data.RequestDB.GetRequestByID(RequestID);

            switch (_Request.RequestTypeID)
            {
                case (byte)DB.RequestType.ADSL:
                    _ADSLRequest = ADSLRequestDB.GetADSLRequestByID(RequestID);
                    _ADSL = ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);
                    RowComboBox.ItemsSource = ADSLPortDB.GetADSLPortRowCheckable(_Request.CenterID, (long)_Request.TelephoneNo);
                    _Telephone = TelephoneDB.GetTelephoneByTelephoneNo((long)_Request.TelephoneNo);
                    //_SwitchPort = Data.SwitchPortDB.GetSwitchPortByID((int)_Telephone.SwitchPortID);
                    //_Bucht = Data.BuchtDB.GetBuchetBySwitchPortand(_SwitchPort.ID);
                    CommentComboBox.Visibility = Visibility.Visible;
                    CommentComboBox.ItemsSource = RequestRejectReasonDB.GetRequestReasonCheckable((byte)DB.ADSLRequestStep.MDF);

                    ActionIDs = new List<byte> { (byte)DB.NewAction.SaveWaitingList, (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };

                    break;

                case (byte)DB.RequestType.ADSLDischarge:

                    _ADSL = ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);
                    CommentComboBox.Visibility = Visibility.Visible;
                    CommentComboBox.ItemsSource = RequestRejectReasonDB.GetRequestReasonCheckable((byte)DB.ADSLDischargeRequestStep.MDF);
                    ADSLDischargePortGroupBox.Visibility = Visibility.Visible;
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
                    break;

                case (byte)DB.RequestType.ADSLChangePort:

                    _ADSL = ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);
                    _ADSLChangePortRequest = ADSLChangePortDB.GetADSLChangePortByID(RequestID);
                    RowComboBox.ItemsSource = ADSLPortDB.GetADSLPortRowCheckable(_Request.CenterID, (long)_Request.TelephoneNo);
                    CommentComboBox.Visibility = Visibility.Visible;
                    CommentComboBox.ItemsSource = RequestRejectReasonDB.GetRequestReasonCheckable((byte)DB.ADSLDischargeRequestStep.MDF);
                    ADSLDischargePortGroupBox.Visibility = Visibility.Visible;
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };

                    break;

                case (byte)DB.RequestType.ADSLChangePlace:

                    _ADSL = ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);
                    _ADSLChangePlaceRequest = ADSLChangePlaceDB.GetADSLChangePlaceById(RequestID);

                    Status StatusPlace = StatusDB.GetStatusByID(_Request.StatusID);

                    if (StatusPlace.RequestStepID == (int)DB.ADSLChangePlaceRequestStep.MDFDischarge)
                    {
                        CommentComboBox.Visibility = Visibility.Visible;
                        CommentComboBox.ItemsSource = RequestRejectReasonDB.GetRequestReasonCheckable((byte)DB.ADSLDischargeRequestStep.MDF);
                        ADSLDischargePortGroupBox.Visibility = Visibility.Visible;
                        ActionIDs = new List<byte> { (byte)DB.NewAction.SaveWaitingList, (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
                    }
                    else
                        if (StatusPlace.RequestStepID == (int)DB.ADSLChangePlaceRequestStep.MDFInstall)
                        {
                            RowComboBox.ItemsSource = ADSLPortDB.GetADSLPortRowCheckable(_Request.CenterID, (long)_Request.TelephoneNo);
                            _Telephone = TelephoneDB.GetTelephoneByTelephoneNo((long)_Request.TelephoneNo);
                            //_SwitchPort = Data.SwitchPortDB.GetSwitchPortByID((int)_Telephone.SwitchPortID);
                            //_Bucht = Data.BuchtDB.GetBuchetBySwitchPortand(_SwitchPort.ID);
                            CommentComboBox.Visibility = Visibility.Visible;
                            CommentComboBox.ItemsSource = RequestRejectReasonDB.GetRequestReasonCheckable((byte)DB.ADSLRequestStep.MDF);

                            ActionIDs = new List<byte> { (byte)DB.NewAction.SaveWaitingList, (byte)DB.NewAction.Save, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                        }

                    break;

                case (byte)DB.RequestType.ADSLInstalPAPCompany:
                case (byte)DB.RequestType.ADSLDischargePAPCompany:
                case (byte)DB.RequestType.ADSLExchangePAPCompany:
                    _ADSLPAPRequest = ADSLPAPRequestDB.GetADSLPAPRequestByID(RequestID);

                    //if (_ADSLPAPRequest != null)
                    //    _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo( _ADSLPAPRequest.TelephoneNo).SingleOrDefault();
                    //else
                    //    throw new Exception("درخواست مورد نظر موجود نمی باشد !");

                    if (_ADSLPAPRequest == null)
                        throw new Exception("درخواست مورد نظر موجود نمی باشد !");

                    ADSLPAPGroupBox.Visibility = Visibility.Visible;

                    CommentMDFListBox.Visibility = Visibility.Visible;
                    CommentTextBox.Visibility = Visibility.Visible;

                    ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };

                    break;

                default:
                    break;
            }

            //_SwitchPort = Data.SwitchPortDB.GetSwitchPortByID((int) _Telephone.SwitchPortID).SingleOrDefault();
            //_Bucht = Data.BuchtDB.GetBuchetBySwitchPortandMDFUses(_SwitchPort.ID, (byte)DB.MDFUses.Normal);            
        }

        private void LoadData()
        {
            try
            {
                //TelephoneNoTextBox.Text = _Telephone.TelephoneNo.ToString();
                //BuchtPropertyTextBox.Text = DB.GetBuchtInfoByBuchtID(_Bucht.ID);
                //SwitchPortTextBox.Text = _SwitchPort.PortNo;

                if (DB.City == "semnan")
                {
                    System.Data.DataTable aDSLInfo = aDSLService.Phone_CUSTOMER_BOOKHTINFO(_Request.TelephoneNo.ToString());
                    System.Data.DataSet aDSLDataSet = new System.Data.DataSet();
                    aDSLDataSet.Tables.Add(aDSLInfo);
                    BuchtsDataGrid.DataContext = aDSLDataSet.Tables[0];
                }

                if (DB.City == "kermanshah" || DB.City == "gilan")
                {
                    TechnicalInfoFailure117 technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo((long)_Request.TelephoneNo);

                    if (technicalInfo != null)
                    {
                        List<TechnicalInfoFailure117> technicalInfoList = new List<TechnicalInfoFailure117>();

                        technicalInfo.BOOKHT_TYPE_NAME = "بوخت اصلی";

                        technicalInfoList.Add(technicalInfo);
                        BuchtsDataGrid.ItemsSource = technicalInfoList;
                    }
                }

                switch (_Request.RequestTypeID)
                {
                    #region ADSL
                    case (byte)DB.RequestType.ADSL:
                        telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());

                        string mDFDescription = ADSLMDFDB.GetMDFDescriptionbyTelephoneNo((long)_Request.TelephoneNo, _Request.CenterID);
                        if (!string.IsNullOrWhiteSpace(mDFDescription))
                            MDFDescriptionLabel.Content = "این شماره تلفن بر روی ام دی اف، " + mDFDescription + " قرار دارد.";
                        else
                            MDFDescriptionLabel.Content = "در اطلاعات فنی رنجی برای این شماره تلفن تعیین نشده است !";

                        TelephoneNoTextBox.Text = _Request.TelephoneNo.ToString();

                        if (telephoneInfo.Rows.Count != 0)
                        {
                            PostalCodeTextBox.Text = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                            AddressTextBox.Text = telephoneInfo.Rows[0]["ADDRESS"].ToString();
                            CenterTextBox.Text = telephoneInfo.Rows[0]["CEN_NAME"].ToString();
                            CustomerTextBox.Text = telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                        }

                        CustomerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSLRequest.CustomerOwnerStatus);
                        RequestDateTextBox.Text = Helper.GetPersianDate(_Request.InsertDate, Helper.DateStringType.DateTime);
                        if (_ADSLRequest.MDFCommnet != null)
                            CommentComboBox.SelectedValue = RequestRejectReasonDB.GetRequestRejectReasonByID((byte)DB.ADSLRequestStep.MDF, Convert.ToInt32(_ADSLRequest.MDFCommnet)).ID;
                        //SwitchPortTextBox.Text = _SwitchPort.PortNo;

                        ADSLPortDataGrid.ItemsSource = ADSLPortDB.GetADSLPortInfo10(_Request.CenterID, (long)_Request.TelephoneNo);

                        ADSLPortsInfo portInfo = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSLRequest.ADSLPortID);
                        if (portInfo != null)
                        {
                            PortInfo.DataContext = portInfo;
                            ADSLGroupBox.Visibility = Visibility.Visible;
                            EquipmentGroupBox.Visibility = Visibility.Visible;

                            ADSLPort port = ADSLPortDB.GetADSlPortByID(portInfo.ID);
                            //ADSLEquipmentComboBox.SelectedValue = port.ADSLEquipmentID;
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
                                        //ADSLEquipmentComboBox.SelectedValue = currenrEquipment.ID;
                                        return;
                                    }
                                }
                            }
                        }

                        if (telephoneInfo.Rows.Count != 0)
                        {
                            if (Convert.ToInt16(telephoneInfo.Rows[0]["PCM_STATUS"]) == 1)
                            {
                                System.Data.DataTable pCMInfo = aDSLService.GetPCMInformation("Admin", "alibaba123", _Request.TelephoneNo.ToString());

                                if (pCMInfo.Rows.Count != 0)
                                {
                                    PCMTechnicalInfo.Visibility = Visibility.Visible;
                                    PortPCMTextBox.Text = pCMInfo.Rows[0]["PORT"].ToString();
                                    ModelPCMTextBox.Text = pCMInfo.Rows[0]["PCM_MARK_NAME"].ToString();
                                    TypePCMTextBox.Text = pCMInfo.Rows[0]["PCM_TYPE_NAME"].ToString();
                                    RockPCMTextBox.Text = pCMInfo.Rows[0]["ROCK"].ToString();
                                    ShelfPCMTextBox.Text = pCMInfo.Rows[0]["SHELF"].ToString();
                                    CardPCMTextBox.Text = pCMInfo.Rows[0]["CARD"].ToString();

                                    PCMInfoGroupBox.Visibility = Visibility.Visible;
                                }
                            }
                            else
                                PCMTechnicalInfo.Visibility = Visibility.Collapsed;
                        }
                        else
                            PCMTechnicalInfo.Visibility = Visibility.Collapsed;

                        ResizeWindow();
                        break;
                    #endregion

                    #region ADSLDischarge
                    case (byte)DB.RequestType.ADSLDischarge:
                        telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());
                        TelephoneNoTextBox.Text = _Request.TelephoneNo.ToString();

                        if (telephoneInfo.Rows.Count != 0)
                        {
                            PostalCodeTextBox.Text = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                            AddressTextBox.Text = telephoneInfo.Rows[0]["ADDRESS"].ToString();
                            CenterTextBox.Text = telephoneInfo.Rows[0]["CEN_NAME"].ToString();
                            CustomerTextBox.Text = telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                        }

                        if (_ADSL.CustomerOwnerStatus != null)
                            CustomerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSL.CustomerOwnerStatus);

                        RequestDateTextBox.Text = Helper.GetPersianDate(_Request.InsertDate, Helper.DateStringType.DateTime);

                        if (_ADSL.ADSLPortID != null)
                        {
                            ADSLPortInfo DischargePortInfo = Data.ADSLPortDB.GetADSlPortInfoByID((long)_ADSL.ADSLPortID);

                            DischargePortInfoGrid.DataContext = DischargePortInfo;
                            DischargePortNoTextBox.Text = DischargePortInfo.Port;
                            MDFDescriptionTextBox.Text = DischargePortInfo.MDFTitle;
                            ADSLDischargePortGroupBox.Visibility = Visibility.Visible;
                        }

                        _ADSLDischargeRequest = ADSLDischargeDB.GetADSLDischargeByID(RequestID);
                        if (_ADSLDischargeRequest.ReasonID != null)
                            ReasonTextBox.Text = ADSLDischargeDB.GetADSLDischargeReasonByID((int)_ADSLDischargeRequest.ReasonID).Title;
                        DischargeCommentTextBox.Text = _ADSLDischargeRequest.Comment;

                        if (_ADSL.WasPCM == true)
                            PCMLabel.Visibility = Visibility.Visible;

                        break;

                    #endregion

                    #region ADSLChangePlace

                    case (byte)DB.RequestType.ADSLChangePlace:
                        telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());
                        _ADSLChangePlaceUserControl = new UserControls.ADSLChangePlaceUserControl(_Request.ID, (long)_Request.TelephoneNo);

                        if (telephoneInfo.Rows.Count == 0)
                        {
                            TelephoneNoTextBox.Text = _Request.TelephoneNo.ToString();
                            PostalCodeTextBox.Text = TelephoneDB.GetPostalCodeByTelephoneNo(Convert.ToInt64(_ADSLChangePlaceUserControl.NewTelNoTextBox.Text));
                            AddressTextBox.Text = TelephoneDB.GetAddressContentByTelephoneNo(Convert.ToInt64(_ADSLChangePlaceUserControl.NewTelNoTextBox.Text));
                            CenterTextBox.Text = TelephoneDB.GetCenterNameByTelephoneNo(Convert.ToInt64(_ADSLChangePlaceUserControl.NewTelNoTextBox.Text));
                            //CustomerTextBox.Text = CustomerDB.GetCustomerByTelephone(Convert.ToInt64(_ADSLChangePlaceUserControl.NewTelNoTextBox.Text)).FirstNameOrTitle + " " +
                            //    CustomerDB.GetCustomerByTelephone(Convert.ToInt64(_ADSLChangePlaceUserControl.NewTelNoTextBox.Text));
                        }
                        else
                        {
                            TelephoneNoTextBox.Text = _Request.TelephoneNo.ToString();
                            PostalCodeTextBox.Text = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                            AddressTextBox.Text = telephoneInfo.Rows[0]["ADDRESS"].ToString();
                            CenterTextBox.Text = telephoneInfo.Rows[0]["CEN_NAME"].ToString();
                            CustomerTextBox.Text = telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                        }

                        if (_ADSL != null)
                        {
                            if (_ADSL.CustomerOwnerStatus != null)
                                CustomerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSL.CustomerOwnerStatus);
                        }

                        RequestDateTextBox.Text = Helper.GetPersianDate(_Request.InsertDate, Helper.DateStringType.DateTime);

                        ReasonLabel.Visibility = Visibility.Collapsed;
                        ReasonTextBox.Visibility = Visibility.Collapsed;
                        CommentLabel.Visibility = Visibility.Collapsed;
                        CommentTextBox.Visibility = Visibility.Collapsed;

                        Status StatusPlace = StatusDB.GetStatusByID(_Request.StatusID);

                        if (StatusPlace.RequestStepID == (int)DB.ADSLChangePlaceRequestStep.MDFDischarge)
                        {
                            if (_ADSL != null)
                            {
                                if (_ADSL.ADSLPortID != null)
                                {
                                    ADSLPortInfo DischargePortInfo = Data.ADSLPortDB.GetADSlPortInfoByID((long)_ADSL.ADSLPortID);

                                    DischargePortInfoGrid.DataContext = DischargePortInfo;
                                    DischargePortNoTextBox.Text = DischargePortInfo.Port;
                                    MDFDescriptionTextBox.Text = DischargePortInfo.MDFTitle;
                                    ADSLDischargePortGroupBox.Visibility = Visibility.Visible;
                                }
                            }
                        }
                        else
                            if (StatusPlace.RequestStepID == (int)DB.ADSLChangePlaceRequestStep.MDFInstall)
                            {
                                ADSLPortDataGrid.ItemsSource = ADSLPortDB.GetADSLPortInfo10(_Request.CenterID, (long)_Request.TelephoneNo);

                                portInfo = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSLChangePlaceRequest.NewPortID);

                                if (portInfo != null)
                                {
                                    PortInfo.DataContext = portInfo;
                                    ADSLGroupBox.Visibility = Visibility.Visible;
                                    EquipmentGroupBox.Visibility = Visibility.Visible;

                                    ADSLPort port = ADSLPortDB.GetADSlPortByID(portInfo.ID);
                                    //ADSLEquipmentComboBox.SelectedValue = port.ADSLEquipmentID;
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
                                                //ADSLEquipmentComboBox.SelectedValue = currenrEquipment.ID;
                                                return;
                                            }
                                        }
                                    }
                                }

                                if (telephoneInfo.Rows.Count != 0)
                                {
                                    if (Convert.ToInt16(telephoneInfo.Rows[0]["PCM_STATUS"]) == 1)
                                    {
                                        System.Data.DataTable pCMInfo = aDSLService.GetPCMInformation("Admin", "alibaba123", _Request.TelephoneNo.ToString());

                                        if (pCMInfo.Rows.Count != 0)
                                        {
                                            PCMTechnicalInfo.Visibility = Visibility.Visible;
                                            PortPCMTextBox.Text = pCMInfo.Rows[0]["PORT"].ToString();
                                            ModelPCMTextBox.Text = pCMInfo.Rows[0]["PCM_MARK_NAME"].ToString();
                                            TypePCMTextBox.Text = pCMInfo.Rows[0]["PCM_TYPE_NAME"].ToString();
                                            RockPCMTextBox.Text = pCMInfo.Rows[0]["ROCK"].ToString();
                                            ShelfPCMTextBox.Text = pCMInfo.Rows[0]["SHELF"].ToString();
                                            CardPCMTextBox.Text = pCMInfo.Rows[0]["CARD"].ToString();

                                            PCMInfoGroupBox.Visibility = Visibility.Visible;
                                        }
                                    }
                                    else
                                        PCMTechnicalInfo.Visibility = Visibility.Collapsed;
                                }
                                else
                                    PCMTechnicalInfo.Visibility = Visibility.Collapsed;
                                //ADSLGroupBox.Visibility = Visibility.Visible;
                                //PortNoTextBox.Visibility = Visibility.Hidden;
                                //PortNoLabel.Visibility = Visibility.Hidden;
                                //PortNoChangePlaceLabel.Visibility = Visibility.Visible;
                                //PortNoChangePlacetextbox.Visibility = Visibility.Visible;
                                //PortNoChangePlacetextbox.Text = _ADSLChangePlaceRequest.NewPortID.ToString();

                                ResizeWindow();
                            }

                        break;

                    #endregion

                    #region ADSLChangePort

                    case (byte)DB.RequestType.ADSLChangePort:
                        telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());

                        TelephoneNoTextBox.Text = _Request.TelephoneNo.ToString();

                        if (telephoneInfo.Rows.Count != 0)
                        {
                            PostalCodeTextBox.Text = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                            AddressTextBox.Text = telephoneInfo.Rows[0]["ADDRESS"].ToString();
                            CenterTextBox.Text = telephoneInfo.Rows[0]["CEN_NAME"].ToString();
                            CustomerTextBox.Text = telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                        }

                        if (_ADSL.CustomerOwnerStatus != null)
                            CustomerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSL.CustomerOwnerStatus);

                        RequestDateTextBox.Text = Helper.GetPersianDate(_Request.InsertDate, Helper.DateStringType.DateTime);

                        ADSLPortDataGrid.ItemsSource = ADSLPortDB.GetADSLPortInfo10(_Request.CenterID, (long)_Request.TelephoneNo);

                        ADSLPortsInfo newPortInfo = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSLChangePortRequest.NewPortID);
                        if (newPortInfo != null)
                        {
                            PortInfo.DataContext = newPortInfo;
                            ADSLGroupBox.Visibility = Visibility.Visible;
                            EquipmentGroupBox.Visibility = Visibility.Visible;
                        }

                        if (_ADSL.ADSLPortID != null)
                        {
                            ADSLPortInfo changePortInfo = Data.ADSLPortDB.GetADSlPortInfoByID((long)_ADSL.ADSLPortID);

                            DischargePortInfoGrid.DataContext = changePortInfo;
                            DischargePortNoTextBox.Text = changePortInfo.Port;
                            MDFDescriptionTextBox.Text = changePortInfo.MDFTitle;
                            ADSLDischargePortGroupBox.Visibility = Visibility.Visible;
                        }

                        _ADSLChangePortRequest = ADSLChangePortDB.GetADSLChangePortByID(RequestID);
                        ReasonTextBox.Text = ADSLChangePortDB.GetADSLChangePortReasonByID(_ADSLChangePortRequest.ReasonID).Description;
                        DischargeCommentTextBox.Text = _ADSLChangePortRequest.Comment;

                        ResizeWindow();
                        break;

                    #endregion

                    #region ADSLINstallCompanyAndADSLDischargePAPCompany

                    case (byte)DB.RequestType.ADSLInstalPAPCompany:
                    case (byte)DB.RequestType.ADSLDischargePAPCompany:
                        if (DB.City == "semnan")
                        {
                            //if (_Telephone == null)
                            //{
                            //    TelephoneNoTextBox.Text = _ADSLPAPRequest.TelephoneNo.ToString();
                            //    TelephoneNoTextBox.Foreground = Brushes.Red;
                            //}
                            //else
                            //{
                            System.Data.DataTable addressDateTable = aDSLService.Get_Costumer_Address(_ADSLPAPRequest.TelephoneNo.ToString());
                            try
                            {
                                PostalCodeTextBox.Text = addressDateTable.Rows[0]["PostCode"].ToString();
                            }
                            catch (Exception ex)
                            {
                                PostalCodeTextBox.Text = string.Empty;
                            }
                            try
                            {
                                AddressTextBox.Text = addressDateTable.Rows[0]["Address"].ToString();
                            }
                            catch (Exception ex)
                            {
                                AddressTextBox.Text = string.Empty;
                            }
                            TelephoneNoTextBox.Text = _ADSLPAPRequest.TelephoneNo.ToString();
                            PAPInfoTextBox.Text = DB.SearchByPropertyName<PAPInfo>("ID", _ADSLPAPRequest.PAPInfoID).SingleOrDefault().Title;
                            //_CustomerAddress = DB.SearchByPropertyName<CustomerAddress>("ID", _Telephone.CustomerAddressID).SingleOrDefault();
                            //TelephoneNoTextBox.Text = _Telephone.TelephoneNo.ToString();
                            //PostalCodeTextBox.Text = _CustomerAddress.PostalCode;
                            //AddressTextBox.Text = _CustomerAddress.Address;
                            //}

                            CenterTextBox.Text = Data.CenterDB.GetCenterById(_Request.CenterID).CenterName;
                            CustomerTextBox.Text = _ADSLPAPRequest.Customer;
                            CustomerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSLPAPRequest.CustomerStatus);
                            RequestDateTextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                            //InstalDateTextBox.Text = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPInstalTimeOut), (int)_ADSLPAPRequest.InstalTimeOut);
                            PortTextBox.Text = _ADSLPAPRequest.SplitorBucht;
                            //LineBuchtTextBox.Text = _ADSLPAPRequest.LineBucht;

                            ExchangeRow.Height = GridLength.Auto;
                            OldPortLabel.Visibility = Visibility.Collapsed;
                            OldPortTextBox.Visibility = Visibility.Collapsed;
                            NewPortLabel.Visibility = Visibility.Collapsed;
                            NewPortTextBox.Visibility = Visibility.Collapsed;
                        }
                        if (DB.City == "kermanshah" || DB.City == "gilan")
                        {
                            TelephoneNoTextBox.Text = _ADSLPAPRequest.TelephoneNo.ToString();
                            PAPInfoTextBox.Text = DB.SearchByPropertyName<PAPInfo>("ID", _ADSLPAPRequest.PAPInfoID).SingleOrDefault().Title;
                            //_CustomerAddress = DB.SearchByPropertyName<CustomerAddress>("ID", _Telephone.CustomerAddressID).SingleOrDefault();
                            //TelephoneNoTextBox.Text = _Telephone.TelephoneNo.ToString();
                            //PostalCodeTextBox.Text = _CustomerAddress.PostalCode;
                            //AddressTextBox.Text = _CustomerAddress.Address;
                            //}

                            CenterTextBox.Text = Data.CenterDB.GetCenterById(_Request.CenterID).CenterName;
                            CustomerTextBox.Text = _ADSLPAPRequest.Customer;
                            if (_ADSLPAPRequest.CustomerStatus != null)
                                CustomerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSLPAPRequest.CustomerStatus);
                            RequestDateTextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                            //InstalDateTextBox.Text = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPInstalTimeOut), (int)_ADSLPAPRequest.InstalTimeOut);

                            if (!string.IsNullOrWhiteSpace(_ADSLPAPRequest.SplitorBucht))
                            {
                                string[] bucht = _ADSLPAPRequest.SplitorBucht.Split(',');
                                PortTextBox.Text = "ردیف : " + bucht[0] + " ، طبقه : " + bucht[1] + " ، اتصالی : " + bucht[2];
                            }
                            else
                            {
                                ADSLPAPPort bucht = ADSLPAPPortDB.GetADSLBuchtbyTelephoneNo(_ADSLPAPRequest.TelephoneNo);

                                if (bucht != null)
                                    PortTextBox.Text = "ردیف : " + bucht.RowNo + " ، طبقه : " + bucht.ColumnNo + " ، اتصالی : " + bucht.BuchtNo;
                                else
                                    PortTextBox.Text = "-";
                            }

                            ExchangeRow.Height = GridLength.Auto;
                            OldPortLabel.Visibility = Visibility.Collapsed;
                            OldPortTextBox.Visibility = Visibility.Collapsed;
                            NewPortLabel.Visibility = Visibility.Collapsed;
                            NewPortTextBox.Visibility = Visibility.Collapsed;

                            if (TelephoneDB.HasTelephoneNo(_ADSLPAPRequest.TelephoneNo))
                            {
                                CabinetLabel.Visibility = Visibility.Visible;
                                CabinetTextBox.Visibility = Visibility.Visible;
                                PostTextBox.Visibility = Visibility.Visible;
                                PostLabel.Visibility = Visibility.Visible;

                                CabinetTextBox.Text = Failure117DB.GetCabinetNobyTelephoneNo(_ADSLPAPRequest.TelephoneNo).ToString();
                                PostTextBox.Text = Failure117DB.GetPostNobyTelephoneNo(_ADSLPAPRequest.TelephoneNo).ToString();
                            }

                            if (_ADSLPAPRequest.RequestTypeID == (byte)DB.RequestType.ADSLInstalPAPCompany)
                            {
                                if (_ADSLPAPRequest.PAPInfoID == 1)
                                {
                                    SelectionBuchtRow.Visibility = Visibility.Visible;

                                    SelectionRowComboBox.ItemsSource = ADSLPAPPortDB.GetADSLPAPBuchtRowbyPAPInfoID(_ADSLPAPRequest.PAPInfoID, _Request.CenterID, _ADSLPAPRequest.ADSLPAPPortID);
                                }
                            }
                        }
                        break;

                    #endregion

                    #region ADSLExchangePAPCompany

                    case (byte)DB.RequestType.ADSLExchangePAPCompany:
                        if (DB.City == "semnan")
                        {
                            //if (_Telephone == null)
                            //{
                            //    TelephoneNoTextBox.Text = _ADSLPAPRequest.TelephoneNo.ToString();
                            //    TelephoneNoTextBox.Foreground = Brushes.Red;
                            //}
                            //else
                            //{
                            System.Data.DataTable addressDateTable1 = aDSLService.Get_Costumer_Address(_ADSLPAPRequest.TelephoneNo.ToString());
                            PostalCodeTextBox.Text = addressDateTable1.Rows[0]["PostCode"].ToString();
                            AddressTextBox.Text = addressDateTable1.Rows[0]["Address"].ToString();
                            TelephoneNoTextBox.Text = _ADSLPAPRequest.TelephoneNo.ToString();
                            PAPInfoTextBox.Text = DB.SearchByPropertyName<PAPInfo>("ID", _ADSLPAPRequest.PAPInfoID).SingleOrDefault().Title;
                            //_CustomerAddress = DB.SearchByPropertyName<CustomerAddress>("ID", _Telephone.CustomerAddressID).SingleOrDefault();
                            //TelephoneNoTextBox.Text = _Telephone.TelephoneNo.ToString();
                            //PostalCodeTextBox.Text = _CustomerAddress.PostalCode;
                            //AddressTextBox.Text = _CustomerAddress.Address;
                            //}

                            CenterTextBox.Text = DB.SearchByPropertyName<Center>("ID", _Request.CenterID).SingleOrDefault().CenterName;
                            CustomerTextBox.Text = _ADSLPAPRequest.Customer;
                            CustomerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSLPAPRequest.CustomerStatus);
                            RequestDateTextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                            //InstalDateTextBox.Text = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPInstalTimeOut), (int)_ADSLPAPRequest.InstalTimeOut);
                            OldPortTextBox.Text = _ADSLPAPRequest.SplitorBucht;
                            NewPortTextBox.Text = _ADSLPAPRequest.NewPort;
                            //LineBuchtTextBox.Text = _ADSLPAPRequest.LineBucht;

                            PortLabel.Visibility = Visibility.Collapsed;
                            PortTextBox.Visibility = Visibility.Collapsed;
                        }
                        if (DB.City == "kermanshah" || DB.City == "gilan")
                        {
                            TelephoneNoTextBox.Text = _ADSLPAPRequest.TelephoneNo.ToString();
                            PAPInfoTextBox.Text = DB.SearchByPropertyName<PAPInfo>("ID", _ADSLPAPRequest.PAPInfoID).SingleOrDefault().Title;
                            //_CustomerAddress = DB.SearchByPropertyName<CustomerAddress>("ID", _Telephone.CustomerAddressID).SingleOrDefault();
                            //TelephoneNoTextBox.Text = _Telephone.TelephoneNo.ToString();
                            //PostalCodeTextBox.Text = _CustomerAddress.PostalCode;
                            //AddressTextBox.Text = _CustomerAddress.Address;
                            //}

                            CenterTextBox.Text = DB.SearchByPropertyName<Center>("ID", _Request.CenterID).SingleOrDefault().CenterName;
                            CustomerTextBox.Text = _ADSLPAPRequest.Customer;
                            CustomerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSLPAPRequest.CustomerStatus);
                            RequestDateTextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                            //InstalDateTextBox.Text = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPInstalTimeOut), (int)_ADSLPAPRequest.InstalTimeOut);

                            ADSLPAPPort oldBucht = ADSLPAPPortDB.GetADSLBuchtbyTelephoneNo((long)_ADSLPAPRequest.TelephoneNo);
                            if (oldBucht != null)
                                OldPortTextBox.Text = "ردیف : " + oldBucht.RowNo.ToString() + " ، طبقه : " + oldBucht.ColumnNo.ToString() + " ، اتصالی : " + oldBucht.BuchtNo.ToString();
                            else
                                OldPortTextBox.Text = "اطلاعات بوخت قدیم موجود نمی باشد.";

                            if (!string.IsNullOrWhiteSpace(_ADSLPAPRequest.NewPort))
                            {
                                string[] newBucht = _ADSLPAPRequest.NewPort.Split(',');
                                NewPortTextBox.Text = "ردیف : " + newBucht[0] + " ، طبقه : " + newBucht[1] + " ، اتصالی : " + newBucht[2];
                            }
                            else
                                NewPortTextBox.Text = "-";

                            PortLabel.Visibility = Visibility.Collapsed;
                            PortTextBox.Visibility = Visibility.Collapsed;

                            if (TelephoneDB.HasTelephoneNo(_ADSLPAPRequest.TelephoneNo))
                            {
                                CabinetLabel.Visibility = Visibility.Visible;
                                CabinetTextBox.Visibility = Visibility.Visible;
                                PostTextBox.Visibility = Visibility.Visible;
                                PostLabel.Visibility = Visibility.Visible;

                                CabinetTextBox.Text = Failure117DB.GetCabinetNobyTelephoneNo(_ADSLPAPRequest.TelephoneNo).ToString();
                                PostTextBox.Text = Failure117DB.GetPostNobyTelephoneNo(_ADSLPAPRequest.TelephoneNo).ToString();
                            }

                            if (_ADSLPAPRequest.PAPInfoID == 1)
                            {
                                SelectionBuchtRow.Visibility = Visibility.Visible;

                                //List<CheckableItem> portList = ADSLPAPPortDB.GetADSLPAPBuchtRowbyPAPInfoID(_ADSLPAPRequest.PAPInfoID, _Request.CenterID);

                                //if (_ADSLPAPRequest.ADSLPAPPortID != null)
                                //{                                    
                                //    ADSLPAPPort reserveBucht = ADSLPAPPortDB.GetADSLPAPPortById((long)_ADSLPAPRequest.ADSLPAPPortID);

                                //    CheckableItem reserveBuchtCheckable = new CheckableItem { ID = (int)reserveBucht.RowNo, Name = reserveBucht.RowNo.ToString(), IsChecked = true };
                                //    portList.Add(reserveBuchtCheckable);
                                //}

                                SelectionRowComboBox.ItemsSource = ADSLPAPPortDB.GetADSLPAPBuchtRowbyPAPInfoID(_ADSLPAPRequest.PAPInfoID, _Request.CenterID, _ADSLPAPRequest.ADSLPAPPortID);
                            }
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Save()
        {
            try
            {
                if ((byte)_Request.RequestTypeID == (byte)DB.RequestType.ADSL || (byte)_Request.RequestTypeID == (byte)DB.RequestType.ADSLChangePort)
                {
                    if (PortInfo.DataContext != null)
                    {
                        ADSLPortsInfo portInfo = PortInfo.DataContext as ADSLPortsInfo;

                        switch (_Request.RequestTypeID)
                        {
                            #region ADSL

                            case (byte)DB.RequestType.ADSL:

                                if ((portInfo.StatusID == (byte)DB.ADSLPortStatus.Free) || (_ADSLRequest.ADSLPortID == portInfo.ID))
                                {
                                    ADSLPort port = ADSLPortDB.GetADSlPortByID(portInfo.ID);
                                    _ADSLRequest = ADSLRequestDB.GetADSLRequestByID(RequestID);

                                    if (_ADSLRequest.ADSLPortID != null)
                                    {
                                        if (_ADSLRequest.ADSLPortID != port.ID)
                                        {
                                            ADSLPort oldPort = ADSLPortDB.GetADSlPortByID((long)_ADSLRequest.ADSLPortID);
                                            oldPort.Status = (byte)DB.ADSLPortStatus.Free;
                                            oldPort.TelephoneNo = null;

                                            oldPort.Detach();
                                            DB.Save(oldPort);
                                        }
                                    }

                                    _ADSLRequest.ADSLPortID = port.ID;
                                    port.Status = (byte)DB.ADSLPortStatus.reserve;
                                    port.TelephoneNo = _Request.TelephoneNo;

                                    if (_IsForForward)
                                    {
                                        _ADSLRequest.MDFDate = DB.GetServerDate();
                                        _ADSLRequest.MDFUserID = DB.CurrentUser.ID;

                                        port.Status = (byte)DB.ADSLPortStatus.Install;
                                        port.InstalADSLDate = DB.GetServerDate();
                                    }

                                    if (CommentComboBox.SelectedValue != null)
                                        _ADSLRequest.MDFCommnet = RequestRejectReasonDB.GetRequestRejectReasonByID((int)CommentComboBox.SelectedValue).ID.ToString();

                                    RequestForADSL.SaveADSLTechnicalEquipment(_ADSLRequest, null, null, null, port, false);
                                }
                                else
                                    throw new Exception("پورتی جهت تخصیص انتخاب نشده است !");

                                break;
                            #endregion

                            #region ADSLChangePort

                            case (byte)DB.RequestType.ADSLChangePort:

                                if ((portInfo.StatusID == (byte)DB.ADSLPortStatus.Free) || (_ADSLChangePortRequest.NewPortID == portInfo.ID))
                                {
                                    ADSLPort port = ADSLPortDB.GetADSlPortByID(portInfo.ID);
                                    _ADSLChangePortRequest = ADSLChangePortDB.GetADSLChangePortByID(RequestID);

                                    if (_ADSLChangePortRequest.NewPortID != null)
                                    {
                                        if (_ADSLChangePortRequest.NewPortID != port.ID)
                                        {
                                            ADSLPort oldPort = ADSLPortDB.GetADSlPortByID((long)_ADSLChangePortRequest.NewPortID);
                                            oldPort.Status = (byte)DB.ADSLPortStatus.Free;
                                            oldPort.TelephoneNo = null;

                                            oldPort.Detach();
                                            DB.Save(oldPort);
                                        }
                                    }

                                    port.Status = (byte)DB.ADSLPortStatus.reserve;
                                    port.TelephoneNo = _Request.TelephoneNo;

                                    if (_IsForForward)
                                    {
                                        port.Status = (byte)DB.ADSLPortStatus.Install;
                                        port.InstalADSLDate = DB.GetServerDate();
                                    }

                                    port.Detach();
                                    DB.Save(port);

                                    _ADSLChangePortRequest.NewPortID = port.ID;

                                    _ADSLChangePortRequest.Detach();
                                    DB.Save(_ADSLChangePortRequest);
                                }
                                else
                                    throw new Exception("پورتی جهت تخصیص انتخاب نشده است !");

                                break;
                            #endregion
                        }
                    }
                    else
                        throw new Exception("پورتی جهت تخصیص انتخاب نشده است !");

                    ShowSuccessMessage("پورت انتخاب شده برای این شماره رزرو شد.");

                    IsSaveSuccess = true;
                }

                #region ADSLChangePlace

                if ((byte)_Request.RequestTypeID == (byte)DB.RequestType.ADSLChangePlace)
                {
                    _ADSLChangePlaceRequest = ADSLChangePlaceDB.GetADSLChangePlaceById(RequestID);
                    Status StatusPlace = StatusDB.GetStatusByID(_Request.StatusID);

                    if (StatusPlace.RequestStepID == (int)DB.ADSLChangePlaceRequestStep.MDFInstall)
                    {
                        ADSLPortsInfo portInfo = PortInfo.DataContext as ADSLPortsInfo;

                        if (PortInfo.DataContext != null)
                        {
                            if ((portInfo.StatusID == (byte)DB.ADSLPortStatus.Free) || (_ADSLChangePlaceRequest.NewPortID == portInfo.ID))
                            {
                                ADSLPort port = ADSLPortDB.GetADSlPortByID(portInfo.ID);

                                if (_ADSLChangePlaceRequest.NewPortID != null)
                                {
                                    if (_ADSLChangePlaceRequest.NewPortID != port.ID)
                                    {
                                        ADSLPort oldPort = ADSLPortDB.GetADSlPortByID((long)_ADSLChangePlaceRequest.NewPortID);
                                        oldPort.Status = (byte)DB.ADSLPortStatus.Free;
                                        oldPort.TelephoneNo = null;

                                        oldPort.Detach();
                                        DB.Save(oldPort);
                                    }
                                }

                                port.Status = (byte)DB.ADSLPortStatus.reserve;
                                port.TelephoneNo = _Request.TelephoneNo;

                                if (_IsForForward)
                                {
                                    _ADSLChangePlaceRequest.MDFInstallDate = DB.GetServerDate();
                                    _ADSLChangePlaceRequest.MDFInstallUserID = DB.CurrentUser.ID;

                                    port.Status = (byte)DB.ADSLPortStatus.Install;
                                    port.InstalADSLDate = DB.GetServerDate();
                                }

                                port.Detach();
                                DB.Save(port);

                                _ADSLChangePlaceRequest.NewPortID = port.ID;

                                _ADSLChangePlaceRequest.Detach();
                                DB.Save(_ADSLChangePlaceRequest);
                            }
                            else
                                throw new Exception("پورتی جهت تخصیص انتخاب نشده است !");
                        }
                        else
                            throw new Exception("پورتی جهت تخصیص انتخاب نشده است !");
                    }

                    ShowSuccessMessage("پورت انتخاب شده برای این شماره رزرو شد.");

                    IsSaveSuccess = true;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            return IsSaveSuccess;
        }

        public override bool Confirm()
        {
            try
            {
                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                XmlRpcStruct userAuthentication = new XmlRpcStruct();
                XmlRpcStruct userInfo = new XmlRpcStruct();

                switch (_Request.RequestTypeID)
                {
                    case (byte)DB.RequestType.ADSLDischarge:

                        userAuthentication.Clear();

                        userAuthentication.Add("auth_name", "pendar");
                        userAuthentication.Add("auth_pass", "Pendar#!$^");
                        userAuthentication.Add("auth_type", "ADMIN");

                        userInfo.Add("lock", "Lock user by Pendar_CRM, Reason: Discharge user");

                        userAuthentication.Add("user_id", _ADSL.UserID);
                        userAuthentication.Add("attrs", userInfo);
                        userAuthentication.Add("to_del_attrs", "");

                        try
                        {
                            ibsngService.UpdateUserAttrs(userAuthentication);
                        }
                        catch (Exception)
                        {
                            throw new Exception("انجام عملیات در AAA با خطا مواجه شده است");
                        }

                        if (_ADSL.ADSLPortID != null)
                        {
                            ADSLPort port = ADSLPortDB.GetADSlPortByID((long)_ADSL.ADSLPortID);
                            port.TelephoneNo = null;
                            port.InstalADSLDate = null;
                            port.Status = (byte)DB.ADSLPortStatus.Free;

                            port.Detach();
                            DB.Save(port);
                        }

                        _ADSL.ADSLPortID = null;
                        _ADSL.Status = (byte)DB.ADSLStatus.Discharge;

                        _ADSL.Detach();
                        DB.Save(_ADSL);

                        List<InstallmentRequestPayment> instalMetPaymentList = InstalmentRequestPaymentDB.GetInstalmentRequestPaymentbyTelephoneNo(_ADSL.TelephoneNo);

                        if (instalMetPaymentList.Count > 0)
                        {
                            DateTime now = DB.GetServerDate();

                            foreach (InstallmentRequestPayment currentInstalment in instalMetPaymentList)
                            {
                                if ((Convert.ToInt32((currentInstalment.StartDate - now).TotalDays)) > 0)
                                    DB.Delete<InstallmentRequestPayment>(currentInstalment.ID);
                            }
                        }

                        IsConfirmSuccess = true;

                        break;

                    case (byte)DB.RequestType.ADSLChangePort:
                        Save();

                        ADSLPort oldPort = ADSLPortDB.GetADSlPortByID((long)_ADSLChangePortRequest.OldPortID);
                        if (oldPort != null)
                        {
                            oldPort.TelephoneNo = null;
                            oldPort.InstalADSLDate = null;
                            oldPort.Status = (byte)Convert.ToInt16(_ADSLChangePortRequest.OldPortStatusID);

                            oldPort.Detach();
                            DB.Save(oldPort);
                        }

                        ADSLPort newPort = ADSLPortDB.GetADSlPortByID((long)_ADSLChangePortRequest.NewPortID);
                        if (newPort != null)
                        {
                            newPort.TelephoneNo = _Request.TelephoneNo;
                            newPort.InstalADSLDate = DB.GetServerDate();
                            newPort.Status = (byte)DB.ADSLPortStatus.Install;

                            newPort.Detach();
                            DB.Save(newPort);
                        }

                        _ADSL.ADSLPortID = _ADSLChangePortRequest.NewPortID;

                        _ADSL.Detach();
                        DB.Save(_ADSL);

                        IsConfirmSuccess = true;

                        break;

                    case (byte)DB.RequestType.ADSLChangePlace:
                        Save();

                        _ADSLChangePlaceRequest = ADSLChangePlaceDB.GetADSLChangePlaceById(RequestID);
                        ADSL _oldADSL = ADSLDB.GetADSLByTelephoneNo((long)_ADSLChangePlaceRequest.OldTelephoneNo);
                        ADSL _newADSL = ADSLDB.GetADSLByTelephoneNo((long)_ADSLChangePlaceRequest.NewTelephoneNo);

                        Status StatusPlace = StatusDB.GetStatusByID(_Request.StatusID);

                        if (StatusPlace.RequestStepID == (int)DB.ADSLChangePlaceRequestStep.MDFInstall)
                        {
                            XmlRpcStruct userInfos = new XmlRpcStruct();
                            bool isUpdated = true;

                            userAuthentication.Clear();
                            userAuthentication.Add("auth_name", "pendar");
                            userAuthentication.Add("auth_pass", "Pendar#!$^");
                            userAuthentication.Add("auth_type", "ADMIN");

                            try
                            {
                                userAuthentication.Add("normal_username", _Request.TelephoneNo.ToString());
                                userInfos = ibsngService.GetUserInfo(userAuthentication);
                                isUpdated = false;
                            }
                            catch (Exception ex)
                            {
                                isUpdated = true;

                                XmlRpcStruct dictionary = new XmlRpcStruct();
                                dictionary.Add("normal_username", _ADSLChangePlaceRequest.NewTelephoneNo.ToString());
                                userInfo.Add("normal_user_spec", dictionary);

                                XmlRpcStruct list = new XmlRpcStruct();
                                list.Add("to_del_attrs", "lock");

                                userAuthentication.Clear();

                                userAuthentication.Add("auth_name", "pendar");
                                userAuthentication.Add("auth_pass", "Pendar#!$^");
                                userAuthentication.Add("auth_type", "ADMIN");

                                userAuthentication.Add("user_id", _oldADSL.UserID.ToString());
                                userAuthentication.Add("attrs", userInfo);
                                userAuthentication.Add("to_del_attrs", list);
                                try
                                {
                                    ibsngService.UpdateUserAttrs(userAuthentication);
                                }
                                catch (Exception ex1)
                                {
                                }
                            }

                            if (isUpdated == false)
                            {
                                XmlRpcStruct dictionary1 = new XmlRpcStruct();
                                dictionary1.Add("normal_username", _ADSLChangePlaceRequest.NewTelephoneNo.ToString() + "2");
                                userInfo.Add("normal_user_spec", dictionary1);

                                userAuthentication.Clear();

                                userAuthentication.Add("auth_name", "pendar");
                                userAuthentication.Add("auth_pass", "Pendar#!$^");
                                userAuthentication.Add("auth_type", "ADMIN");

                                userAuthentication.Add("user_id", _newADSL.UserID.ToString());
                                userAuthentication.Add("attrs", userInfo);
                                userAuthentication.Add("to_del_attrs", "");
                                try
                                {
                                    ibsngService.UpdateUserAttrs(userAuthentication);
                                }
                                catch (Exception ex)
                                {
                                }

                                XmlRpcStruct dictionary = new XmlRpcStruct();
                                dictionary.Add("normal_username", _ADSLChangePlaceRequest.NewTelephoneNo.ToString());
                                userInfo.Add("normal_user_spec", dictionary);

                                XmlRpcStruct list = new XmlRpcStruct();
                                list.Add("to_del_attrs", "lock");

                                userAuthentication.Clear();

                                userAuthentication.Add("auth_name", "pendar");
                                userAuthentication.Add("auth_pass", "Pendar#!$^");
                                userAuthentication.Add("auth_type", "ADMIN");

                                userAuthentication.Add("user_id", _oldADSL.UserID.ToString());
                                userAuthentication.Add("attrs", userInfo);
                                userAuthentication.Add("to_del_attrs", list);
                                try
                                {
                                    ibsngService.UpdateUserAttrs(userAuthentication);
                                }
                                catch (Exception ex1)
                                {
                                }
                            }

                            if (_oldADSL.ModemID != null)
                            {
                                _ADSLModemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)_oldADSL.ModemID);
                                _ADSLModemProperty.TelephoneNo = (long)_ADSLChangePlaceRequest.NewTelephoneNo;

                                _ADSLModemProperty.Detach();
                                DB.Save(_ADSLModemProperty);
                            }

                            if (_newADSL != null)
                            {
                                _newADSL.TariffID = _oldADSL.TariffID;
                                _newADSL.ADSLPortID = (long)_ADSLChangePlaceRequest.NewPortID;
                                _newADSL.TelephoneNo = (long)_ADSLChangePlaceRequest.NewTelephoneNo;
                                _newADSL.Status = (byte)DB.ADSLStatus.Connect;
                                _newADSL.InsertDate = _oldADSL.InsertDate;
                                _newADSL.InstallDate = _oldADSL.InstallDate;
                                _newADSL.ExpDate = _oldADSL.ExpDate;
                                _newADSL.ModemID = _oldADSL.ModemID;
                                _newADSL.HasIP = _oldADSL.HasIP;
                                _newADSL.IPStaticID = _oldADSL.IPStaticID;
                                _newADSL.GroupIPStaticID = _oldADSL.GroupIPStaticID;
                                _newADSL.CustomerOwnerID = _oldADSL.CustomerOwnerID;
                                _newADSL.CustomerOwnerStatus = _oldADSL.CustomerOwnerStatus;
                                _newADSL.UserName = _ADSLChangePlaceRequest.NewTelephoneNo.ToString();
                                _newADSL.UserID = _oldADSL.UserID;
                                _newADSL.HashPassword = _oldADSL.HashPassword;
                                _newADSL.AAAPassword = _oldADSL.AAAPassword;

                                _newADSL.Detach();
                                DB.Save(_newADSL);
                            }
                            else
                            {
                                _newADSL = new ADSL();
                                _newADSL.TariffID = _oldADSL.TariffID;
                                _newADSL.ADSLPortID = (long)_ADSLChangePlaceRequest.NewPortID;
                                _newADSL.TelephoneNo = (long)_ADSLChangePlaceRequest.NewTelephoneNo;
                                _newADSL.Status = (byte)DB.ADSLStatus.Connect;
                                _newADSL.InsertDate = _oldADSL.InsertDate;
                                _newADSL.InstallDate = _oldADSL.InstallDate;
                                _newADSL.ExpDate = _oldADSL.ExpDate;
                                _newADSL.ModemID = _oldADSL.ModemID;
                                _newADSL.HasIP = _oldADSL.HasIP;
                                _newADSL.IPStaticID = _oldADSL.IPStaticID;
                                _newADSL.GroupIPStaticID = _oldADSL.GroupIPStaticID;
                                _newADSL.CustomerOwnerID = _oldADSL.CustomerOwnerID;
                                _newADSL.CustomerOwnerStatus = _oldADSL.CustomerOwnerStatus;
                                _newADSL.UserName = _ADSLChangePlaceRequest.NewTelephoneNo.ToString();
                                _newADSL.UserID = _oldADSL.UserID;
                                _newADSL.HashPassword = _oldADSL.HashPassword;
                                _newADSL.AAAPassword = _oldADSL.AAAPassword;

                                _newADSL.Detach();
                                DB.Save(_newADSL);
                            }

                            _oldADSL.TariffID = null;
                            _oldADSL.ADSLPortID = null;
                            _oldADSL.Status = (byte)DB.ADSLStatus.Discharge;
                            _oldADSL.InstallDate = null;
                            _oldADSL.ModemID = null;
                            _oldADSL.CustomerOwnerID = _oldADSL.CustomerOwnerID;
                            _oldADSL.UserName = null;
                            _oldADSL.UserID = null;
                            _oldADSL.HashPassword = null;

                            _oldADSL.Detach();
                            DB.Save(_oldADSL);

                            List<InstallmentRequestPayment> instalMetPaymentList1 = InstalmentRequestPaymentDB.GetInstalmentRequestPaymentbyTelephoneNo(_oldADSL.TelephoneNo);

                            if (instalMetPaymentList1.Count > 0)
                            {
                                foreach (InstallmentRequestPayment currentInstalment in instalMetPaymentList1)
                                {
                                    currentInstalment.TelephoneNo = _newADSL.TelephoneNo;
                                    currentInstalment.Detach();
                                    DB.Save(currentInstalment);
                                }
                            }

                            _ADSLChangePlaceRequest.MDFInstallUserID = DB.CurrentUser.ID;
                            _ADSLChangePlaceRequest.MDFInstallDate = DB.GetServerDate();

                            _ADSLChangePlaceRequest.Detach();
                            DB.Save(_ADSLChangePlaceRequest);

                            IsConfirmSuccess = true;
                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            return IsConfirmSuccess;
        }

        public override bool Forward()
        {
            try
            {
                switch (_Request.RequestTypeID)
                {
                    case (byte)DB.RequestType.ADSL:

                        _IsForForward = true;

                        Save();

                        AddUsertoIBSng();

                        _ADSL.TariffID = _ADSLRequest.ServiceID;
                        _ADSL.ADSLPortID = (long)_ADSLRequest.ADSLPortID;
                        _ADSL.TelephoneNo = (long)_Request.TelephoneNo;
                        _ADSL.UserID = _UserID;
                        _ADSL.Status = (byte)DB.ADSLStatus.Connect;

                        if (_ADSLRequest.ModemSerialNoID != null)
                            _ADSL.ModemID = _ADSLRequest.ModemSerialNoID;

                        if (_ADSLRequest.HasIP != null)
                        {
                            if (_ADSLRequest.HasIP == true)
                            {
                                if (_ADSLRequest.IPStaticID != null)
                                {
                                    _ADSL.IPStaticID = _ADSLRequest.IPStaticID;
                                    _ADSL.HasIP = true;
                                }
                                if (_ADSLRequest.GroupIPStaticID != null)
                                {
                                    _ADSL.GroupIPStaticID = _ADSLRequest.GroupIPStaticID;
                                    _ADSL.HasIP = true;
                                }
                            }
                        }

                        int? durationID = ADSLServiceDB.GetADSLServiceDurationByServiceID((int)_ADSLRequest.ServiceID);
                        if (durationID != 0 && durationID != -1)
                            _ADSL.ExpDate = DB.GetServerDate().AddMonths((int)durationID);
                        else
                            _ADSL.ExpDate = DB.GetServerDate().AddMonths(12);

                        _ADSL.Detach();
                        DB.Save(_ADSL);

                        ADSLHistory aDSLHistory = new ADSLHistory();
                        aDSLHistory.TelephoneNo = _Request.TelephoneNo.ToString();
                        aDSLHistory.ServiceID = _ADSL.TariffID;
                        aDSLHistory.UserID = DB.CurrentUser.ID;
                        aDSLHistory.InsertDate = DB.GetServerDate();

                        aDSLHistory.Detach();
                        DB.Save(aDSLHistory, true);

                        if (_ADSLRequest.RequiredInstalation == false)
                        {
                            CRM.Data.Schema.ADSL ADSLLog = new Data.Schema.ADSL();
                            ADSLLog.CustomerOwnerID = (long)_ADSL.CustomerOwnerID;
                            ADSLLog.TariffID = (int)_ADSL.TariffID;
                            ADSLLog.ADSLPortID = (long)_ADSL.ADSLPortID;

                            _RequestLog = new RequestLog();
                            _RequestLog.RequestID = _Request.ID;
                            _RequestLog.RequestTypeID = _Request.RequestTypeID;
                            _RequestLog.Date = DB.GetServerDate();
                            _RequestLog.TelephoneNo = _Request.TelephoneNo;
                            _RequestLog.UserID = DB.CurrentUser.ID;


                            _RequestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ADSL>(ADSLLog, true));

                            RequestForADSL.SaveADSLRequest(null, _ADSLRequest, null, null, null, _RequestLog, false);

                            Data.WorkFlowDB.SetNextState(DB.Action.AutomaticForward, _Request.StatusID, _Request.ID);

                            _Request = RequestDB.GetRequestByID(RequestID);
                            _Request.PreviousAction = (byte)DB.Action.Confirm;

                            _Request.Detach();
                            DB.Save(_Request);

                            this.DialogResult = true;
                            this.Close();

                            IsForwardSuccess = false;
                        }
                        else
                            IsForwardSuccess = true;

                        string mobileNos = "";
                        string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ConnectADSL));

                        string customerFullName = CustomerDB.GetFullNameByCustomerID(_ADSLRequest.CustomerOwnerID);
                        if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID(_ADSLRequest.CustomerOwnerID)))
                            mobileNos = CustomerDB.GetCustomerMobileByID(_ADSLRequest.CustomerOwnerID);

                        if (!string.IsNullOrWhiteSpace(mobileNos))
                        {
                            SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ConnectADSL));

                            if (SmsService.IsActive == true)
                            {
                                message = message.Replace("CustomerName", customerFullName).Replace("TeleohoneNo", _Request.TelephoneNo.ToString()).Replace("UserName", _ADSL.UserName).Replace("Password", _ADSL.OrginalPassword).Replace("Enter", Environment.NewLine);
                                CRMWebServiceDB.SendMessage(mobileNos, message);
                            }
                        }

                        break;

                    case (byte)DB.RequestType.ADSLInstalPAPCompany:
                    case (byte)DB.RequestType.ADSLDischargePAPCompany:
                        //_Bucht.ADSLStatus = true;

                        if (CommentMDFListBox.SelectedValue != null)
                            if ((byte)Convert.ToInt16(CommentMDFListBox.SelectedValue) != (byte)DB.ADSLMDFCommnet.NoProblem && (byte)Convert.ToInt16(CommentMDFListBox.SelectedValue) != (byte)DB.ADSLMDFCommnet.DamagedPort)
                                throw new Exception("به دلیل انتخاب دلایل رد امکان ارجاع وجود ندارد");

                        _ADSLPAPRequest.MDFDate = DB.GetServerDate();
                        _ADSLPAPRequest.MDFUserID = DB.CurrentUser.ID;
                        if (CommentMDFListBox.SelectedValue != null)
                            _ADSLPAPRequest.MDFComment = CommentTextBox.Text + " ، " + CommentMDFListBox.SelectedValue.ToString();
                        else
                            _ADSLPAPRequest.MDFComment = CommentTextBox.Text;

                        ADSLPAPPort aDSLPAPPort = null;

                        if (DB.City == "semnan")
                            aDSLPAPPort = ADSLPAPPortDB.GetADSLPAPPortByPortNoAndCenter(_ADSLPAPRequest.PAPInfoID, Convert.ToInt64(_ADSLPAPRequest.SplitorBucht), _Request.CenterID);

                        if (DB.City == "kermanshah" || DB.City == "gilan")
                        {
                            if (_ADSLPAPRequest.RequestTypeID == (byte)DB.RequestType.ADSLInstalPAPCompany)
                            {
                                if (_ADSLPAPRequest.PAPInfoID != 1)
                                {
                                    string[] bucht = _ADSLPAPRequest.SplitorBucht.Split(',');
                                    aDSLPAPPort = ADSLPAPPortDB.GetADSLPAPPortByBuchtNoAndCenter(_ADSLPAPRequest.PAPInfoID, Convert.ToInt32(bucht[0]), Convert.ToInt32(bucht[1]), Convert.ToInt64(bucht[2]), _Request.CenterID);
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(SelectionRowComboBox.Text) && !string.IsNullOrWhiteSpace(SelectionColumnComboBox.Text) && !string.IsNullOrWhiteSpace(SelectionBuchtComboBox.Text))
                                    {
                                        if (_ADSLPAPRequest.SplitorBucht != null && !string.IsNullOrWhiteSpace(_ADSLPAPRequest.SplitorBucht))
                                        {
                                            string[] bucht = _ADSLPAPRequest.SplitorBucht.Split(',');
                                            aDSLPAPPort = ADSLPAPPortDB.GetADSLPAPPortByBuchtNoAndCenter(_ADSLPAPRequest.PAPInfoID, Convert.ToInt32(bucht[0]), Convert.ToInt32(bucht[1]), Convert.ToInt64(bucht[2]), _Request.CenterID);

                                            aDSLPAPPort.Status = (byte)DB.ADSLPAPPortStatus.Free;
                                            aDSLPAPPort.TelephoneNo = null;

                                            aDSLPAPPort.Detach();
                                            DB.Save(aDSLPAPPort);
                                        }

                                        aDSLPAPPort = null;
                                        aDSLPAPPort = ADSLPAPPortDB.GetADSLPAPPortByBuchtNoAndCenter(_ADSLPAPRequest.PAPInfoID, Convert.ToInt32(SelectionRowComboBox.Text.Trim()), Convert.ToInt32(SelectionColumnComboBox.Text.Trim()), Convert.ToInt64(SelectionBuchtComboBox.Text.Trim()), _Request.CenterID);

                                        if (aDSLPAPPort != null)
                                        {
                                            if (aDSLPAPPort.Status == (byte)DB.ADSLPAPPortStatus.Instal || aDSLPAPPort.Status == (byte)DB.ADSLPAPPortStatus.Broken || aDSLPAPPort.Status == (byte)DB.ADSLPAPPortStatus.Reserve)
                                                throw new Exception("پورت وارد شده آزاد نمی باشد");

                                            _ADSLPAPRequest.ADSLPAPPortID = aDSLPAPPort.ID;

                                            _ADSLPAPRequest.Detach();
                                            DB.Save(_ADSLPAPRequest);
                                        }
                                    }
                                    else
                                        throw new Exception("لطفا ردیف، طبقه و اتصالی مورد نظر را وارد نمایید");
                                }

                                //Bucht aDSLBucht = ADSLPAPPortDB.GetADSLBuchtbyPAPInfoID(_ADSLPAPRequest.PAPInfoID, _Request.CenterID, (int)aDSLPAPPort.RowNo, (int)aDSLPAPPort.ColumnNo, (int)aDSLPAPPort.BuchtNo);

                                //if (aDSLBucht != null)
                                //{
                                //    Bucht cRMBucht = BuchtDB.GetBuchtbyTelephoneNo((long)_Request.TelephoneNo);

                                //    if (cRMBucht != null)
                                //    {
                                //        cRMBucht.BuchtIDConnectedOtherBucht = aDSLBucht.ID;
                                //        cRMBucht.ADSLStatus = true;

                                //        cRMBucht.Detach();
                                //        DB.Save(cRMBucht);

                                //        aDSLBucht.PAPInfoID = _ADSLPAPRequest.PAPInfoID;                                        

                                //        aDSLBucht.Detach();
                                //        DB.Save(aDSLBucht);                                        
                                //    }
                                //}
                            }
                            if (_ADSLPAPRequest.RequestTypeID == (byte)DB.RequestType.ADSLDischargePAPCompany)
                            {
                                aDSLPAPPort = ADSLPAPPortDB.GetADSLBuchtbyTelephoneNo(_ADSLPAPRequest.TelephoneNo);

                                //Bucht buchtCRM = BuchtDB.GetBuchtbyTelephoneNo((long)_Request.TelephoneNo);

                                //if (buchtCRM != null)
                                //{
                                //    if (buchtCRM.BuchtIDConnectedOtherBucht != null)
                                //    {
                                //        Bucht aDSLBucht = BuchtDB.GetBuchtByID((long)buchtCRM.BuchtIDConnectedOtherBucht);

                                //        buchtCRM.ADSLStatus = false;
                                //        buchtCRM.PAPInfoID = null;

                                //        buchtCRM.Detach();
                                //        DB.Save(buchtCRM);
                                //    }
                                //}
                            }
                        }

                        if (aDSLPAPPort != null)
                        {
                            if (_Request.RequestTypeID == (byte)DB.RequestType.ADSLInstalPAPCompany)
                            {
                                aDSLPAPPort.TelephoneNo = _ADSLPAPRequest.TelephoneNo;
                                aDSLPAPPort.InstallDate = DB.GetServerDate();
                                aDSLPAPPort.Status = (byte)DB.ADSLPAPPortStatus.Instal;

                                ADSLTelephoneNoHistory history = new ADSLTelephoneNoHistory();
                                history.TelephoneNo = (long)_Request.TelephoneNo;
                                history.CenterID = _Request.CenterID;
                                history.PAPInfoID = _ADSLPAPRequest.PAPInfoID;
                                history.InstalDate = DB.GetServerDate();

                                history.Detach();
                                DB.Save(history);
                            }

                            if (_Request.RequestTypeID == (byte)DB.RequestType.ADSLDischargePAPCompany)
                            {
                                aDSLPAPPort.TelephoneNo = null;
                                aDSLPAPPort.InstallDate = null;
                                aDSLPAPPort.Status = (byte)DB.ADSLPAPPortStatus.Discharge;

                                //ADSLTelephoneNoHistory history = ADSLTelephoneHistoryDB.GetADSLTelephoneHistoryforDischarge((long)_Request.TelephoneNo, _ADSLPAPRequest.PAPInfoID);
                                //if (history != null)
                                //{
                                //    history.DischargeDate = DB.GetServerDate();

                                //    history.Detach();
                                //    Save(history);
                                //}
                                //else
                                //    throw new Exception("سابقه ثبت شده برای این شماره تلفن ناقص می باشد، لطفا با مدیر سیستم تماس حاصل فرمایید");
                            }
                        }
                        else
                            throw new Exception("پورتی با این مشخصات در سیستم ثبت نشده است");

                        int nextStatusID = Data.WorkFlowDB.GetNextStatesID(DB.Action.Confirm, _Request.StatusID);
                        Status nextStatus = StatusDB.GetStatusByID(nextStatusID);

                        if (nextStatus.StatusType == (byte)DB.RequestStatusType.End)
                        {
                            _ADSLPAPRequest.Status = (byte)DB.ADSLPAPRequestStatus.Completed;
                            _ADSLPAPRequest.FinalUserID = DB.CurrentUser.ID;
                            _ADSLPAPRequest.FinalDate = DB.GetServerDate();

                            _Request.EndDate = DB.GetServerDate();

                            //_ADSL = new ADSL();

                            //_ADSL.TelephoneNo = _ADSLPAPRequest.TelephoneNo;
                            //_ADSL.PAPInfoID = _ADSLPAPRequest.PAPInfoID;
                            //_ADSL.Status = (byte)DB.ADSLStatus.Connect;                             

                            CRM.Data.Schema.ADSLPAP ADSLPAPLog = new Data.Schema.ADSLPAP();
                            ADSLPAPLog.PAPInfoID = (int)_ADSLPAPRequest.PAPInfoID;
                            ADSLPAPLog.CenterID = _Request.CenterID;

                            _RequestLog = new RequestLog();
                            _RequestLog.RequestID = _Request.ID;
                            _RequestLog.RequestTypeID = _Request.RequestTypeID;
                            _RequestLog.Date = DB.GetServerDate();
                            _RequestLog.TelephoneNo = _ADSLPAPRequest.TelephoneNo;

                            _RequestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ADSLPAP>(ADSLPAPLog, true));

                            RequestForADSL.SaveADSLPAPRequest(_Request, _ADSLPAPRequest, null/*_ADSL*/, aDSLPAPPort, null, null /* _Bucht*/, _RequestLog, false);
                        }
                        else
                            RequestForADSL.SaveADSLPAPRequest(_Request, _ADSLPAPRequest, null, aDSLPAPPort, null, null /* _Bucht*/, null, false);

                        IsForwardSuccess = true;

                        break;

                    case (byte)DB.RequestType.ADSLExchangePAPCompany:
                        //_Bucht.ADSLStatus = true;

                        if (CommentMDFListBox.SelectedValue != null)
                            if ((byte)Convert.ToInt16(CommentMDFListBox.SelectedValue) != (byte)DB.ADSLMDFCommnet.NoProblem && (byte)Convert.ToInt16(CommentMDFListBox.SelectedValue) != (byte)DB.ADSLMDFCommnet.DamagedPort)
                                throw new Exception("به دلیل انتخاب دلایل رد امکان ارجاع وجود ندارد");

                        _ADSLPAPRequest.MDFDate = DB.GetServerDate();
                        _ADSLPAPRequest.MDFUserID = DB.CurrentUser.ID;
                        if (CommentMDFListBox.SelectedValue != null)
                            _ADSLPAPRequest.MDFComment = CommentTextBox.Text + " ، " + CommentMDFListBox.SelectedValue.ToString();
                        else
                            _ADSLPAPRequest.MDFComment = CommentTextBox.Text;

                        ADSLPAPPort oldADSLPAPPort = null;
                        ADSLPAPPort newADSLPAPPort = null;

                        if (DB.City == "semnan")
                        {
                            oldADSLPAPPort = ADSLPAPPortDB.GetADSLBuchtbyTelephoneNo(_ADSLPAPRequest.TelephoneNo);
                            newADSLPAPPort = ADSLPAPPortDB.GetADSLPAPPortByPortNoAndCenter(_ADSLPAPRequest.PAPInfoID, Convert.ToInt64(_ADSLPAPRequest.NewPort), _Request.CenterID);
                        }
                        if (DB.City == "kermanshah" || DB.City == "gilan")
                        {
                            oldADSLPAPPort = ADSLPAPPortDB.GetADSLBuchtbyTelephoneNo((long)_ADSLPAPRequest.TelephoneNo);

                            if (_ADSLPAPRequest.PAPInfoID != 1)
                            {
                                string[] bucht = _ADSLPAPRequest.NewPort.Split(',');
                                newADSLPAPPort = ADSLPAPPortDB.GetADSLPAPPortByBuchtNoAndCenter(_ADSLPAPRequest.PAPInfoID, Convert.ToInt32(bucht[0]), Convert.ToInt32(bucht[1]), Convert.ToInt64(bucht[2]), _Request.CenterID);
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(SelectionRowComboBox.Text) && !string.IsNullOrWhiteSpace(SelectionColumnComboBox.Text) && !string.IsNullOrWhiteSpace(SelectionBuchtComboBox.Text))
                                {
                                    if (_ADSLPAPRequest.NewPort != null && !string.IsNullOrWhiteSpace(_ADSLPAPRequest.NewPort))
                                    {
                                        string[] bucht = _ADSLPAPRequest.NewPort.Split(',');
                                        newADSLPAPPort = ADSLPAPPortDB.GetADSLPAPPortByBuchtNoAndCenter(_ADSLPAPRequest.PAPInfoID, Convert.ToInt32(bucht[0]), Convert.ToInt32(bucht[1]), Convert.ToInt64(bucht[2]), _Request.CenterID);

                                        newADSLPAPPort.Status = (byte)DB.ADSLPAPPortStatus.Free;
                                        newADSLPAPPort.TelephoneNo = null;

                                        newADSLPAPPort.Detach();
                                        DB.Save(newADSLPAPPort);
                                    }

                                    newADSLPAPPort = null;
                                    newADSLPAPPort = ADSLPAPPortDB.GetADSLPAPPortByBuchtNoAndCenter(_ADSLPAPRequest.PAPInfoID, Convert.ToInt32(SelectionRowComboBox.Text.Trim()), Convert.ToInt32(SelectionColumnComboBox.Text.Trim()), Convert.ToInt64(SelectionBuchtComboBox.Text.Trim()), _Request.CenterID);

                                    if (newADSLPAPPort != null)
                                    {
                                        if (newADSLPAPPort.Status == (byte)DB.ADSLPAPPortStatus.Instal || newADSLPAPPort.Status == (byte)DB.ADSLPAPPortStatus.Broken || newADSLPAPPort.Status == (byte)DB.ADSLPAPPortStatus.Reserve)
                                            throw new Exception("پورت وارد شده آزاد نمی باشد");

                                        _ADSLPAPRequest.ADSLPAPPortID = newADSLPAPPort.ID;

                                        _ADSLPAPRequest.Detach();
                                        DB.Save(_ADSLPAPRequest);
                                    }
                                }
                                else
                                    throw new Exception("لطفا ردیف، طبقه و اتصالی مورد نظر را وارد نمایید");
                            }
                        }

                        if (oldADSLPAPPort != null && newADSLPAPPort != null)
                        {
                            oldADSLPAPPort.TelephoneNo = null;
                            oldADSLPAPPort.InstallDate = null;
                            oldADSLPAPPort.Status = (byte)DB.ADSLPAPPortStatus.Discharge;

                            if ((byte)Convert.ToInt16(CommentMDFListBox.SelectedValue) == (byte)DB.ADSLMDFCommnet.DamagedPort)
                                oldADSLPAPPort.Status = (byte)DB.ADSLPAPPortStatus.Broken;

                            newADSLPAPPort.TelephoneNo = _ADSLPAPRequest.TelephoneNo;
                            newADSLPAPPort.InstallDate = DB.GetServerDate();
                            newADSLPAPPort.Status = (byte)DB.ADSLPAPPortStatus.Instal;

                            //ADSLTelephoneNoHistory oldHistory = ADSLTelephoneHistoryDB.GetADSLTelephoneHistoryforDischarge((long)_Request.TelephoneNo, _ADSLPAPRequest.PAPInfoID);
                            //if (oldHistory != null)
                            //{
                            //    oldHistory.DischargeDate = DB.GetServerDate();

                            //    oldHistory.Detach();
                            //    Save(oldHistory);
                            //}
                            //else
                            //    throw new Exception("سابقه ثبت شده برای این شماره تلفن ناقص می باشد، لطفا با مدیر سیستم تماس حاصل فرمایید");

                            ADSLTelephoneNoHistory newHistory = new ADSLTelephoneNoHistory();
                            newHistory.TelephoneNo = (long)_Request.TelephoneNo;
                            newHistory.CenterID = _Request.CenterID;
                            newHistory.PAPInfoID = _ADSLPAPRequest.PAPInfoID;
                            newHistory.InstalDate = DB.GetServerDate();

                            newHistory.Detach();
                            DB.Save(newHistory);
                        }
                        else
                            throw new Exception("پورتی با این مشخصات در سیستم ثبت نشده است");

                        int nextStatusID1 = Data.WorkFlowDB.GetNextStatesID(DB.Action.Confirm, _Request.StatusID);
                        Status nextStatus1 = StatusDB.GetStatusByID(nextStatusID1);

                        if (nextStatus1.StatusType == (byte)DB.RequestStatusType.End)
                        {
                            _ADSLPAPRequest.Status = (byte)DB.ADSLPAPRequestStatus.Completed;
                            _ADSLPAPRequest.FinalUserID = DB.CurrentUser.ID;
                            _ADSLPAPRequest.FinalDate = DB.GetServerDate();

                            _Request.EndDate = DB.GetServerDate();

                            //_ADSL = new ADSL();

                            //_ADSL.TelephoneNo = _ADSLPAPRequest.TelephoneNo;
                            //_ADSL.PAPInfoID = _ADSLPAPRequest.PAPInfoID;
                            //_ADSL.Status = (byte)DB.ADSLStatus.Connect;                             

                            CRM.Data.Schema.ADSLPAP ADSLPAPLog = new Data.Schema.ADSLPAP();
                            ADSLPAPLog.PAPInfoID = (int)_ADSLPAPRequest.PAPInfoID;
                            ADSLPAPLog.CenterID = _Request.CenterID;

                            _RequestLog = new RequestLog();
                            _RequestLog.RequestID = _Request.ID;
                            _RequestLog.RequestTypeID = _Request.RequestTypeID;
                            _RequestLog.Date = DB.GetServerDate();
                            _RequestLog.TelephoneNo = _ADSLPAPRequest.TelephoneNo;

                            _RequestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ADSLPAP>(ADSLPAPLog, true));

                            RequestForADSL.SaveADSLPAPRequest(_Request, _ADSLPAPRequest, null/*_ADSL*/, oldADSLPAPPort, newADSLPAPPort, null /* _Bucht*/, _RequestLog, false);
                        }
                        else
                            RequestForADSL.SaveADSLPAPRequest(_Request, _ADSLPAPRequest, null, oldADSLPAPPort, newADSLPAPPort, null /* _Bucht*/, null, false);

                        IsForwardSuccess = true;

                        break;

                    case (byte)DB.RequestType.ADSLChangePlace:
                        IsForwardSuccess = true;

                        Save();

                        _ADSLChangePlaceRequest = ADSLChangePlaceDB.GetADSLChangePlaceById(RequestID);
                        _ADSLChangePlaceUserControl = new UserControls.ADSLChangePlaceUserControl(_Request.ID, (long)_Request.TelephoneNo);
                        ADSL _OldADSL = ADSLDB.GetADSLByTelephoneNo((long)_ADSLChangePlaceRequest.OldTelephoneNo);

                        Status StatusPlace = StatusDB.GetStatusByID(_Request.StatusID);

                        if (StatusPlace.RequestStepID == (int)DB.ADSLChangePlaceRequestStep.MDFDischarge)
                        {
                            int oldMDFID = ADSLMDFRangeDB.GetMDFinRangebyTelephoneNo((long)_ADSLChangePlaceRequest.OldTelephoneNo, (int)_ADSLChangePlaceRequest.OldCenterID);
                            int mewMDFID = ADSLMDFRangeDB.GetMDFinRangebyTelephoneNo((long)_ADSLChangePlaceRequest.NewTelephoneNo, (int)_ADSLChangePlaceRequest.NewCenterID);

                            if (_ADSLChangePlaceRequest.NewCenterID == _ADSLChangePlaceRequest.OldCenterID && oldMDFID == mewMDFID)
                            {
                                ADSLPort samePort = ADSLPortDB.GetADSLPortByID((long)_ADSLChangePlaceRequest.OldPortID);

                                samePort.Status = (byte)DB.ADSLPortStatus.reserve;
                                samePort.TelephoneNo = _ADSLChangePlaceRequest.NewTelephoneNo;

                                samePort.Detach();
                                DB.Save(samePort);

                                _ADSLChangePlaceRequest.NewPortID = samePort.ID;
                                _ADSLChangePlaceRequest.Detach();
                                DB.Save(_ADSLChangePlaceRequest);
                            }
                            else
                            {
                                List<Data.ADSLPort> portFreeList = ADSLMDFDB.GetFreeADSLPortByCenterID((int)_ADSLChangePlaceRequest.NewCenterID, mewMDFID);

                                if (portFreeList.Count != 0)
                                {
                                    Data.ADSLPort port = portFreeList[0];

                                    port.Status = (byte)DB.ADSLPortStatus.reserve;
                                    port.TelephoneNo = _ADSLChangePlaceRequest.NewTelephoneNo;
                                    port.Detach();
                                    DB.Save(port);

                                    _ADSLChangePlaceRequest.NewPortID = port.ID;
                                    _ADSLChangePlaceRequest.Detach();
                                    DB.Save(_ADSLChangePlaceRequest);
                                }
                                else
                                    throw new Exception("در این مرکز تجهیزات فنی وجود ندارد");

                                ADSLPort _OldADSLPort = ADSLPortDB.GetADSLPortByID((long)_ADSLChangePlaceRequest.OldPortID);

                                _OldADSLPort.Status = (byte)DB.ADSLPortStatus.Free;
                                _OldADSLPort.TelephoneNo = null;
                                _OldADSLPort.InstalADSLDate = null;

                                _OldADSLPort.Detach();
                                DB.Save(_OldADSLPort);
                            }

                            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                            XmlRpcStruct userAuthentication = new XmlRpcStruct();
                            XmlRpcStruct userInfo = new XmlRpcStruct();

                            userAuthentication.Clear();

                            userAuthentication.Add("auth_name", "pendar");
                            userAuthentication.Add("auth_pass", "Pendar#!$^");
                            userAuthentication.Add("auth_type", "ADMIN");

                            userInfo.Add("lock", "Lock user by Pendar_CRM, Reason: Change TelephoneNo");

                            userAuthentication.Add("user_id", _OldADSL.UserID);
                            userAuthentication.Add("attrs", userInfo);
                            userAuthentication.Add("to_del_attrs", "");

                            try
                            {
                                ibsngService.UpdateUserAttrs(userAuthentication);

                            }
                            catch (Exception)
                            {
                                throw new Exception("عملیات Lock کردن شماره قدیم در AAA با مشکل مواجه شده است");
                            }

                            _Request.TelephoneNo = _ADSLChangePlaceRequest.NewTelephoneNo;
                            _Request.CenterID = (int)_ADSLChangePlaceRequest.NewCenterID;

                            _Request.Detach();
                            DB.Save(_Request, false);
                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در عملیات ارجاع، " + ex.Message + " !", ex);
            }

            return IsForwardSuccess;
        }

        public override bool Deny()
        {
            try
            {
                switch (_Request.RequestTypeID)
                {
                    case (byte)DB.RequestType.ADSL:
                        if (CommentComboBox.SelectedValue != null)
                        {
                            _ADSLRequest.MDFCommnet = RequestRejectReasonDB.GetRequestRejectReasonByID((int)CommentComboBox.SelectedValue).ID.ToString();
                            _ADSLRequest.Detach();
                            DB.Save(_ADSLRequest);
                        }

                        ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID((int)_Request.CreatorUserID);
                        if (user != null)
                        {
                            ADSLSellerAgent sellerAgent = ADSLSellerGroupDB.GetADSLSellerAgentByID(user.SellerAgentID);
                            ADSLService service = ADSLServiceDB.GetADSLServiceById((int)_ADSLRequest.ServiceID);
                            ADSLIP ipStatic = new ADSLIP();
                            ADSLGroupIP groupIPStatic = new ADSLGroupIP();
                            long iPCost = 0;

                            if (_ADSLRequest.HasIP != null)
                            {
                                BaseCost cost = BaseCostDB.GetIPCostForADSL();

                                if ((bool)_ADSLRequest.HasIP)
                                {
                                    if (_ADSLRequest.IPStaticID != null)
                                    {
                                        ipStatic = ADSLIPDB.GetADSLIPById((int)_ADSLRequest.IPStaticID);
                                        iPCost = cost.Cost * (int)_ADSLRequest.IPDuration;
                                    }
                                    if (_ADSLRequest.GroupIPStaticID != null)
                                    {
                                        groupIPStatic = ADSLIPDB.GetADSLGroupIPById((int)_ADSLRequest.GroupIPStaticID);
                                        iPCost = cost.Cost * groupIPStatic.BlockCount * (int)_ADSLRequest.IPDuration;
                                    }
                                }
                            }

                            if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                            {
                                sellerAgent.CreditCashUse = sellerAgent.CreditCashUse - (service.PriceSum + iPCost);
                                sellerAgent.CreditCashRemain = sellerAgent.CreditCashRemain + (service.PriceSum + iPCost);
                            }

                            sellerAgent.Detach();
                            DB.Save(sellerAgent);
                        }

                        if (_ADSLRequest.ADSLPortID != null)
                        {
                            ADSLPort oldPort = ADSLPortDB.GetADSlPortByID((long)_ADSLRequest.ADSLPortID);
                            oldPort.Status = (byte)DB.ADSLPortStatus.Free;
                            oldPort.TelephoneNo = null;
                            oldPort.InstalADSLDate = null;

                            oldPort.Detach();
                            DB.Save(oldPort);
                        }

                        if (_ADSLRequest.ModemSerialNoID != null)
                        {
                            ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)_ADSLRequest.ModemSerialNoID);
                            modem.Status = (byte)DB.ADSLModemStatus.Reserve;

                            modem.Detach();
                            DB.Save(modem);
                        }

                        break;

                    case (byte)DB.RequestType.ADSLInstalPAPCompany:
                    case (byte)DB.RequestType.ADSLDischargePAPCompany:
                    case (byte)DB.RequestType.ADSLExchangePAPCompany:
                        //DB.SearchByPropertyName<Request>("ID", _Request.ID).SingleOrDefault();
                        //Data.RequestDB.GetRequestByID( _Request.ID).SingleOrDefault();                        

                        _ADSLPAPRequest.MDFDate = DB.GetServerDate();
                        _ADSLPAPRequest.MDFUserID = DB.CurrentUser.ID;

                        if (CommentMDFListBox.SelectedValue != null)
                        {
                            _ADSLPAPRequest.MDFComment = CommentTextBox.Text + " ، " + Helper.GetEnumDescriptionByValue(typeof(DB.ADSLMDFCommnet), Convert.ToInt32(CommentMDFListBox.SelectedValue));
                            _ADSLPAPRequest.CommnetReject = CommentTextBox.Text + " ، " + Helper.GetEnumDescriptionByValue(typeof(DB.ADSLMDFCommnet), Convert.ToInt32(CommentMDFListBox.SelectedValue));
                        }
                        else
                        {
                            _ADSLPAPRequest.MDFComment = CommentTextBox.Text;
                            _ADSLPAPRequest.CommnetReject = CommentTextBox.Text;
                        }

                        int nextStatusID = Data.WorkFlowDB.GetNextStatesID(DB.Action.Confirm, _Request.StatusID);
                        Status nextStatus = StatusDB.GetStatusByID(nextStatusID);

                        ADSLPAPPort bucht = null;
                        if (nextStatus.StatusType == (byte)DB.RequestStatusType.End)
                        {
                            if (_ADSLPAPRequest.ADSLPAPPortID != null)
                            {
                                bucht = ADSLPAPPortDB.GetADSLPAPPortById((long)_ADSLPAPRequest.ADSLPAPPortID);
                                if (_Request.RequestTypeID == (byte)DB.RequestType.ADSLInstalPAPCompany || _Request.RequestTypeID == (byte)DB.RequestType.ADSLExchangePAPCompany)
                                {
                                    bucht.Status = (byte)DB.ADSLPAPPortStatus.Free;
                                    bucht.TelephoneNo = null;

                                    if ((byte)Convert.ToInt16(CommentMDFListBox.SelectedValue) == (byte)DB.ADSLMDFCommnet.DamagedPort)
                                        bucht.Status = (byte)DB.ADSLPAPPortStatus.Broken;
                                }
                            }

                            _ADSLPAPRequest.Status = (byte)DB.ADSLPAPRequestStatus.Reject;
                            _ADSLPAPRequest.FinalUserID = DB.CurrentUser.ID;
                            _ADSLPAPRequest.FinalDate = DB.GetServerDate();

                            CRM.Data.Schema.ADSLPAP ADSLPAPLog = new Data.Schema.ADSLPAP();
                            ADSLPAPLog.PAPInfoID = (int)_ADSLPAPRequest.PAPInfoID;
                            ADSLPAPLog.CenterID = _Request.CenterID;

                            _RequestLog = new RequestLog();
                            _RequestLog.RequestID = _Request.ID;
                            _RequestLog.RequestTypeID = _Request.RequestTypeID;
                            _RequestLog.Date = DB.GetServerDate();
                            _RequestLog.IsReject = true;
                            _RequestLog.UserID = DB.CurrentUser.ID;
                            _RequestLog.TelephoneNo = _ADSLPAPRequest.TelephoneNo;

                            _RequestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ADSLPAP>(ADSLPAPLog, true));

                            _Request.EndDate = DB.GetServerDate();

                            RequestForADSL.SaveADSLPAPRequest(_Request, _ADSLPAPRequest, null/*_ADSL*/, bucht, null, null /* _Bucht*/, _RequestLog, false);
                        }
                        else
                            RequestForADSL.SaveADSLPAPRequest(_Request, _ADSLPAPRequest, null/*_ADSL*/, null, null, null /* _Bucht*/, null, false);
                        break;

                    default:
                        break;
                }

                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در عملیات رد درخواست، " + ex.Message + " !", ex);
            }

            return IsRejectSuccess;
        }

        public override bool SaveWaitingList()
        {
            try
            {
                Save();

                WaitingList waitingList = new WaitingList();
                waitingList.ReasonID = null;
                waitingList.Status = false;

                WaitingListDB.SaveWaitingList(RequestID, _Request, waitingList);

                RequestID = _Request.ID;

                IsSaveWatingListSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره در لیسست انتظار ، " + ex.Message + " !", ex);

            }

            return IsSaveWatingListSuccess;
        }

        public void AddUsertoIBSng()
        {
            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();
            object[] ids = new object[1];
            bool isAddable = false;

            userAuthentication.Clear();
            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            try
            {
                userAuthentication.Add("normal_username", _Request.TelephoneNo.ToString());
                userInfos = ibsngService.GetUserInfo(userAuthentication);
            }
            catch (Exception ex)
            {
                isAddable = true;
            }

            ADSLService service = ADSLServiceDB.GetADSLServiceById((int)_ADSLRequest.ServiceID);
            ADSLServiceTraffic traffic = ADSLServiceDB.GetADSLServiceTraffiBycID((int)service.TrafficID);
            string credit = (((traffic.Credit != null) ? traffic.Credit : 1024) + ((service.ReserveCridit != null) ? service.ReserveCridit : 0)).ToString();

            if (isAddable)
            {
                userAuthentication.Clear();

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                userAuthentication.Add("count", 1);
                userAuthentication.Add("credit", credit);
                userAuthentication.Add("isp_name", "Main");
                userAuthentication.Add("group_name", service.IBSngGroupName);
                userAuthentication.Add("credit_comment", "");

                ids = ibsngService.AddNewUsers(userAuthentication);
                if (ids.Count() == 0)
                    throw new Exception("ذخیره کاربر در سیستم IBSng با مشکل مواجه شد");
            }
            else
            {
                ids[0] = _ADSL.UserID;

                userAuthentication.Clear();

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                XmlRpcStruct list = new XmlRpcStruct();
                list.Add("to_del_attrs", "lock");

                userAuthentication.Add("user_id", ids[0].ToString());
                userAuthentication.Add("attrs", userInfo);
                userAuthentication.Add("to_del_attrs", list);

                ibsngService.UpdateUserAttrs(userAuthentication);

                userAuthentication.Clear();

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                userAuthentication.Add("user_id", ids[0].ToString());

                userAuthentication.Add("deposit", credit);
                userAuthentication.Add("is_absolute_change", false);
                userAuthentication.Add("deposit_type", "renew");
                userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, ADSL Request (Recharge)");

                try
                {
                    ibsngService.changeDeposit(userAuthentication);
                }
                catch (Exception ex1)
                {
                }

                userAuthentication.Clear();

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                userAuthentication.Add("user_id", ids[0].ToString());

                userInfo.Add("renew_next_group", service.IBSngGroupName);
                userInfo.Add("renew_remove_user_exp_dates", "1");
                userAuthentication.Add("attrs", userInfo);
                userAuthentication.Add("to_del_attrs", "");

                try
                {
                    ibsngService.UpdateUserAttrs(userAuthentication);
                }
                catch (Exception ex2)
                {
                }
            }

            userInfo = new XmlRpcStruct();
            XmlRpcStruct dictionary = new XmlRpcStruct();

            dictionary.Add("normal_username", _Request.TelephoneNo.ToString());
            dictionary.Add("normal_password", "");

            userInfo.Add("normal_user_spec", dictionary);

            userInfo.Add("normal_password_bind_on_login", "");

            string customerName = CustomerDB.GetFullNameByCustomerID(_ADSLRequest.CustomerOwnerID);
            userInfo.Add("name", customerName);

            Customer customer = CustomerDB.GetCustomerByID(_ADSLRequest.CustomerOwnerID);

            if (customer != null)
            {
                userInfo.Add("email", customer.Email);
                userInfo.Add("phone", _ADSL.UserName);
                userInfo.Add("cell_phone", customer.MobileNo);
                userInfo.Add("postal_code", PostalCodeTextBox.Text);
                userInfo.Add("address", AddressTextBox.Text);
            }

            if (_ADSLRequest.HasIP == true)
            {
                if (_ADSLRequest.IPStaticID != null)
                {
                    ADSLIP iPStatic = ADSLIPDB.GetADSLIPById((long)_ADSLRequest.IPStaticID);
                    userInfo.Add("assign_ip", iPStatic.IP);

                    iPStatic.Status = (byte)DB.ADSLIPStatus.Instal;
                    iPStatic.InstallDate = DB.GetServerDate();
                    iPStatic.ExpDate = DB.GetServerDate().AddMonths((int)_ADSLRequest.IPDuration);

                    iPStatic.Detach();
                    DB.Save(iPStatic);
                }

                if (_ADSLRequest.GroupIPStaticID != null)
                {
                    ADSLGroupIP groupIP = ADSLIPDB.GetADSLGroupIPById((long)_ADSLRequest.GroupIPStaticID);
                    userInfo.Add("assign_ip", groupIP.VirtualRange);
                    userInfo.Add("assign_route_ip", groupIP.StartRange);

                    groupIP.Status = (byte)DB.ADSLIPStatus.Instal;
                    groupIP.InstallDate = DB.GetServerDate();
                    groupIP.ExpDate = DB.GetServerDate().AddMonths((int)_ADSLRequest.IPDuration);

                    groupIP.Detach();
                    DB.Save(groupIP);
                }
            }

            _UserID = ids[0].ToString();

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            userAuthentication.Add("user_id", ids[0].ToString());
            userAuthentication.Add("attrs", userInfo);
            userAuthentication.Add("to_del_attrs", "");

            ibsngService.UpdateUserAttrs(userAuthentication);

            Update_FreeCounter(_Request.TelephoneNo.ToString(), (int)service.OverNightCredit);

            if (_ADSLRequest.AdditionalServiceID != null)
            {
                ADSLService additionalTraffic = ADSLServiceDB.GetADSLServiceById((int)_ADSLRequest.AdditionalServiceID);
                ADSLServiceTraffic additionalTrafficTraffic = ADSLServiceDB.GetADSLServiceTraffiBycID((int)additionalTraffic.TrafficID);

                userAuthentication.Clear();

                userAuthentication.Add("user_id", ids[0].ToString());
                userAuthentication.Add("deposit", additionalTrafficTraffic.Credit.ToString());
                userAuthentication.Add("is_absolute_change", false);
                userAuthentication.Add("deposit_type", "recharge");
                userAuthentication.Add("deposit_comment", "Change by Pendar_CRM Sell ADSL Request");

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                try
                {
                    ibsngService.changeDeposit(userAuthentication);
                    Update_FreeCounter(_Request.TelephoneNo.ToString(), (int)additionalTraffic.OverNightCredit);
                }
                catch (Exception ex)
                { }
            }
        }

        private string ToStringSpecial(object value)
        {
            if (value != null)
            {
                if (value.ToString().ToLower() == "Null")
                    return "";
                else
                    return value.ToString();
            }
            else
                return string.Empty;
        }

        private double GetNightFree_NormalBWPhoneNumber(string PhoneNumber)
        {
            double custom_field_free_counter = 0;

            try
            {
                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                XmlRpcStruct userAuthentication = new XmlRpcStruct();
                XmlRpcStruct userInfos = new XmlRpcStruct();
                XmlRpcStruct userInfo = new XmlRpcStruct();

                userAuthentication.Clear();
                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                try
                {
                    userAuthentication.Add("normal_username", PhoneNumber);
                    userInfos = ibsngService.GetUserInfo(userAuthentication);
                }
                catch (Exception ex)
                {

                }

                foreach (DictionaryEntry User in userInfos)
                {
                    userInfo = (XmlRpcStruct)User.Value;
                }

                try
                {
                    custom_field_free_counter = Convert.ToDouble(ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["custom_field_free_counter"]));

                }
                catch (Exception ex)
                {
                    custom_field_free_counter = 0;
                }
            }
            catch
            {

            }

            return custom_field_free_counter;
        }

        public void Update_FreeCounter(string telephoneNo, int overnightCredit)
        {
            double custom_field_free_counter = 0;
            custom_field_free_counter = GetNightFree_NormalBWPhoneNumber(telephoneNo);

            string Free_Counter = Convert.ToInt32((custom_field_free_counter + overnightCredit)).ToString();

            try
            {
                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
                XmlRpcStruct userAuthentication = new XmlRpcStruct();
                XmlRpcStruct userInfos = new XmlRpcStruct();
                XmlRpcStruct userInfo = new XmlRpcStruct();

                userAuthentication.Clear();
                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                try
                {
                    XmlRpcStruct to_del_attrs_list = new XmlRpcStruct();
                    XmlRpcStruct[] list = new XmlRpcStruct[2];

                    list[0] = new XmlRpcStruct();
                    list[1] = new XmlRpcStruct();
                    list[0].Add("custom_field_free_counter", Free_Counter);

                    XmlRpcStruct list_custom_fields = new XmlRpcStruct();
                    list_custom_fields.Add("custom_fields", list);

                    userAuthentication.Add("user_id", _UserID);
                    userAuthentication.Add("attrs", list_custom_fields);
                    userAuthentication.Add("to_del_attrs", to_del_attrs_list);
                    ibsngService.UpdateUserAttrs(userAuthentication);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Event Handlers

        private void AlBuchtCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (ADSLEquipmentComboBox.SelectedValue != null)
            //{
            //    ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentID((int)ADSLEquipmentComboBox.SelectedValue);

            //    ADSLPortInfo port = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free).FirstOrDefault();
            //    PortInfo.DataContext = port;
            //}
            //else
            //    if ((ADSLEquipmentComboBox.SelectedIndex == 0) || (ADSLEquipmentComboBox.SelectedItem != null))
            //    {
            //        ADSLEquipment Equipment = ADSLEquipmentComboBox.SelectedItem as ADSLEquipment;
            //        ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentID(Equipment.ID);
            //    }
        }

        private void AlBuchtCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (ADSLEquipmentComboBox.SelectedValue != null)
            //{
            //    ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free);

            //    ADSLPortInfo port = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free).FirstOrDefault();
            //    PortInfo.DataContext = port;
            //}
            //else
            //    if ((ADSLEquipmentComboBox.SelectedIndex == 0) || (ADSLEquipmentComboBox.SelectedItem != null))
            //    {
            //        ADSLEquipment Equipment = ADSLEquipmentComboBox.SelectedItem as ADSLEquipment;
            //        ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus(Equipment.ID, (byte)DB.ADSLPortStatus.Free);
            //    }
        }

        private void RowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RowComboBox.SelectedValue != null)
                ColumnComboBox.ItemsSource = ADSLPortDB.GetADSLPortColumnCheckable(_Request.CenterID, (int)RowComboBox.SelectedValue, (long)_Request.TelephoneNo);
        }

        private void ColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int columnID = 0;

            if (ColumnComboBox.SelectedValue != null)
            {
                columnID = (int)ColumnComboBox.SelectedValue;
                ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByColumnIDandStatus(_Request.CenterID, (int)RowComboBox.SelectedValue, columnID, (byte)DB.ADSLPortStatus.Free, (long)_Request.TelephoneNo);
            }
            else
            {
                if (_ADSLRequest.ADSLPortID != null)
                {
                    RowComboBox.SelectedValue = ADSLPortDB.GetADSlPortByID((int)_ADSLRequest.ADSLPortID).Radif;
                    ColumnComboBox.SelectedValue = ADSLPortDB.GetADSlPortByID((int)_ADSLRequest.ADSLPortID).Tabaghe;
                }
            }

            if (_ADSLRequest.ADSLPortID != null)
            {
                ADSLPortsInfo port = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSLRequest.ADSLPortID);
                PortInfo.DataContext = port;
            }
            else
            {
                ADSLPortsInfo port = new ADSLPortsInfo();

                if (ColumnComboBox.SelectedItem != null)
                    port = Data.ADSLPortDB.GetADSLPortsInfoByColumnIDandStatus(_Request.CenterID, (int)RowComboBox.SelectedValue, (int)ColumnComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free, (long)_Request.TelephoneNo)[0];
                else
                {
                    //port = Data.ADSLPortDB.GetADSLPortsInfoByColumnIDandStatus(equipment.ID, (byte)DB.ADSLPortStatus.Free).FirstOrDefault();
                }

                PortInfo.DataContext = port;
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
                switch (_Request.RequestTypeID)
                {
                    case (byte)DB.RequestType.ADSL:

                        if (_ADSLRequest.ADSLPortID != null)
                        {
                            ADSLPortsInfo port = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSLRequest.ADSLPortID);
                            PortInfo.DataContext = port;
                        }
                        else
                            throw new Exception("پورتی برای این درخواست ذخیره نشده است !");

                        break;

                    case (byte)DB.RequestType.ADSLChangePort:

                        if (_ADSLChangePortRequest.NewPortID != null)
                        {
                            ADSLPortsInfo port = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSLChangePortRequest.NewPortID);
                            PortInfo.DataContext = port;
                        }
                        else
                            throw new Exception("پورتی برای این درخواست ذخیره نشده است !");

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ADSLPortDataGrid.SelectedItem != null)
            {
                ADSLPortsInfo portInfo = ADSLPortDataGrid.SelectedItem as ADSLPortsInfo;
                ADSLPort port = ADSLPortDB.GetADSlPortByID(portInfo.ID);

                ADSLPortsForm window = new ADSLPortsForm(port.ID);
                window.ShowDialog();

                port = ADSLPortDB.GetADSlPortByID(portInfo.ID);

                ADSLPortDataGrid.ItemsSource = ADSLPortDB.GetADSLPortInfo10(_Request.CenterID, (long)_Request.TelephoneNo);

                ADSLPortsInfo currentPortInfo = PortInfo.DataContext as ADSLPortsInfo;

                if (currentPortInfo.ID == port.ID)
                {
                    if (port.Status != (byte)DB.ADSLPortStatus.Free)
                        PortInfo.DataContext = ADSLPortDB.GetADSLPortInfo10(_Request.CenterID, (long)_Request.TelephoneNo)[0];
                }
            }
        }

        private void SingleImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ADSLPort port = new ADSLPort();
            Image img = sender as Image;
            if (img != null)
            {
                int portID = Convert.ToInt32(img.Tag);
                port = ADSLPortDB.GetADSlPortByID(portID);

                if (port != null)
                {
                    ADSLPortsForm window = new ADSLPortsForm(port.ID);
                    window.ShowDialog();

                    port = ADSLPortDB.GetADSlPortByID(portID);

                    if (_ADSLRequest.ADSLPortID == port.ID)
                    {
                        if (port.Status == (byte)DB.ADSLPortStatus.reserve)
                            PortInfo.DataContext = ADSLPortDB.GetADSlPortInfoByID(port.ID);
                        else
                        {
                            List<ADSLPortsInfo> portList = ADSLPortDB.GetADSLPortInfo10(_Request.CenterID, (long)_Request.TelephoneNo);
                            if (portList.Count != 0)
                            {
                                ADSLPortsInfo portInfo = new ADSLPortsInfo();
                                port = new ADSLPort();

                                portInfo = ADSLPortDB.GetADSlPortsInfoByID(portList[0].ID);
                                port = ADSLPortDB.GetADSlPortByID(portList[0].ID);

                                if (port != null)
                                {
                                    port.Status = (byte)DB.ADSLPortStatus.reserve;
                                    port.TelephoneNo = _Request.TelephoneNo;
                                    port.Detach();
                                    DB.Save(port);

                                    _ADSLRequest.ADSLPortID = port.ID;
                                    _ADSLRequest.Detach();
                                    DB.Save(_ADSLRequest);

                                    PortInfo.DataContext = portInfo;
                                }
                            }
                            else
                                throw new Exception("پورت خالی موجود نمی باشد");
                        }
                    }
                    else
                        PortInfo.DataContext = ADSLPortDB.GetADSlPortByID((int)_ADSLRequest.ADSLPortID);
                }
            }

            ADSLPortDataGrid.ItemsSource = ADSLPortDB.GetADSLPortInfo10(_Request.CenterID, (long)_Request.TelephoneNo);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void SelectionRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionRowComboBox.SelectedValue != null)
                SelectionColumnComboBox.ItemsSource = ADSLPAPPortDB.GetADSLPAPBuchtColumnbyPAPInfoID(_ADSLPAPRequest.PAPInfoID, _Request.CenterID, (int)SelectionRowComboBox.SelectedValue, _ADSLPAPRequest.ADSLPAPPortID);


        }

        private void SelectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionColumnComboBox.SelectedValue != null)
                SelectionBuchtComboBox.ItemsSource = ADSLPAPPortDB.GetADSLPAPBuchtBuchtbyPAPInfoID(_ADSLPAPRequest.PAPInfoID, _Request.CenterID, (int)SelectionRowComboBox.SelectedValue, (int)SelectionColumnComboBox.SelectedValue, _ADSLPAPRequest.ADSLPAPPortID);
        }

        #endregion
    }
}
