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
using System.IO;

namespace CRM.Application.Views
{
    public partial class Failure117Form : Local.RequestFormBase
    {
        #region Properties

        private Request _Request = new Request();
        private List<Request> _RequestList = new List<Request>();
        private Data.Failure117 _Failure117 = new Data.Failure117();
        private List<Data.Failure117> _Failure117List = new List<Data.Failure117>();
        private RequestInfo _RequestInfo = new RequestInfo();
        private List<RequestInfo> _RequestInfoList = new List<RequestInfo>();
        public List<long> RequestIDs = new List<long>();
        private byte _LineStatusTypeID = 0;
        private int _FailureStatusTypeID = 0;

        private TelephoneInfoForRequest telephoneInfo;
        private TechnicalInfoFailure117 technicalInfo;

        #endregion

        #region Constructors

        public Failure117Form(long requestID)
        {
            RequestID = requestID;

            InitializeComponent();

            Initialize();
            LoadData();

            SmsContentTextBox1.Text = "مشترک گرامی با شماره تلفن " + TelephoneNoTextBox.Text.Trim() + System.Environment.NewLine +
                             "مشکل تلفن شما از خرابی داخلی میباشد";
        }

        public Failure117Form(List<long> requestIDs)
        {
            RequestIDs = requestIDs;

            InitializeComponent();

            Initialize();
            LoadData();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            ActionStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.Failure117ActionStatus));
            FailureStatusTypeComboBox.ItemsSource = Failure117DB.GetParentFailureStatus((byte)DB.Failure117AvalibilityStatus.MDFAnalysis);

            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };

            if (DB.City == "semnan")
            {
                LineStatusTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.Failure117LineStatus)).Where(t => t.ID != 4).ToList();
                MDFPersonnelComboBox.ItemsSource = Data.MDFPersonnelDB.GetMDFPersonnelsCheckable();
                EndMDFPersonnelComboBox.ItemsSource = Data.MDFPersonnelDB.GetMDFPersonnelsCheckable();
            }
            if (DB.City == "kermanshah")
            {
                LineStatusTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.Failure117LineStatus));
                MDFPersonnelTextBox.Text = UserDB.GetUserFullName(DB.currentUser.ID);
                EndMDFPersonnelTextBox.Text = UserDB.GetUserFullName(DB.currentUser.ID);

                if (RequestID != 0)
                {
                    _RequestInfo = Data.RequestDB.GetRequestInfoByID(RequestID);

                    if (_RequestInfo.StepID == (int)DB.RequestStepFailure117.Saloon)
                        ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
                }

                if (RequestIDs != null && RequestIDs.Count != 0)
                {
                    _RequestInfoList = Data.RequestDB.GetRequestInfoListByIDs(RequestIDs);

                    if (_RequestInfoList[0].StepID == (int)DB.RequestStepFailure117.Saloon)
                        ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
                }
            }
        }

        public void LoadData()
        {
            if (DB.City == "semnan")
            {
                _Request = Data.RequestDB.GetRequestByID(RequestID);
                _Failure117 = Failure117DB.GetFailureRequestByID(RequestID);
                _RequestInfo = Data.RequestDB.GetRequestInfoByID(RequestID);
                System.Data.DataTable  CableColorInfodt = Data.RequestDB.GetColorInf(RequestID);

                if (CableColorInfodt != null)
                {
                    if (CableColorInfodt.Rows.Count > 0 )
                    {
                        try
                        {
                            string color1 = CableColorInfodt.Rows[0][2].ToString();
                            string color2 = CableColorInfodt.Rows[0][3].ToString();
                            SimColor.Text = color1 + "و " + color2;
                        }
                        catch
                        {
                            SimColor.Text = "";

                        }
                    }
                }

                if (_Request.TelephoneNo == 2333388205)
                {
                    _RequestInfo = Data.RequestDB.GetRequestInfoByID(RequestID);

                    this.DataContext = _Failure117;

                    Service1 service = new Service1();
                    System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());

                    RequestNoTextBox.Text = _Request.ID.ToString();
                    ElkaNoTextBox.Text = telephoneInfo.Rows[0]["FI_CODE"].ToString();
                    RequestDateTextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                    CallingNoTextBox.Text = _Failure117.CallingNo.ToString();

                    TelephoneGroupBox.Visibility = Visibility.Collapsed;
                    CustomerGroupBox.Visibility = Visibility.Collapsed;
                    TechnicalGroupBox.Visibility = Visibility.Collapsed;
                    OprationGroupBox.Visibility = Visibility.Collapsed;
                    NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;
                    CommentGroupBox.Visibility = Visibility.Collapsed;

                    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Delete, (byte)DB.NewAction.Exit };

                   
                }
                else
                {
                    NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    TelephoneGroupBox.Visibility = Visibility.Visible;
                    CustomerGroupBox.Visibility = Visibility.Visible;
                    TechnicalGroupBox.Visibility = Visibility.Visible;
                    OprationGroupBox.Visibility = Visibility.Visible;
                    NetworkPerformanceGroupBox.Visibility = Visibility.Visible;
                    CommentGroupBox.Visibility = Visibility.Visible;

                    this.DataContext = _Failure117;

                    Service1 service = new Service1();
                    System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());

                    RequestNoTextBox.Text = _Request.ID.ToString();
                    RequestDateTextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                    try
                    {
                        ElkaNoTextBox.Text = telephoneInfo.Rows[0]["FI_CODE"].ToString();
                    }
                    catch (Exception ex)
                    {
                        ElkaNoTextBox.Text = string.Empty;
                    }
                    try
                    {
                        CallingNoTextBox.Text = _Failure117.CallingNo.ToString();
                    }
                    catch (Exception ex)
                    {
                        CallingNoTextBox.Text = string.Empty;
                    }
                    try
                    {
                        TelephoneNoTextBox.Text = telephoneInfo.Rows[0]["Phone"].ToString();
                    }
                    catch (Exception ex)
                    {
                        TelephoneNoTextBox.Text = string.Empty;
                    }
                    try
                    {
                        CenterTextBox.Text = telephoneInfo.Rows[0]["CEN_NAME"].ToString();
                    }
                    catch (Exception ex)
                    {
                        CenterTextBox.Text = string.Empty;
                    }
                    try
                    {
                        PostalCodeTextBox.Text = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                    }
                    catch (Exception ex)
                    {
                        PostalCodeTextBox.Text = string.Empty;
                    }
                    try
                    {
                        AddressTextBox.Text = telephoneInfo.Rows[0]["ADDRESS"].ToString();
                    }
                    catch (Exception ex)
                    {
                        AddressTextBox.Text = string.Empty;
                    }
                    try
                    {
                        FirstNameTextBox.Text = telephoneInfo.Rows[0]["FirstName"].ToString();
                    }
                    catch (Exception ex)
                    {
                        FirstNameTextBox.Text = string.Empty;
                    }
                    try
                    {
                        LastNameTextBox.Text = telephoneInfo.Rows[0]["Lastname"].ToString();
                    }
                    catch (Exception ex)
                    {
                        LastNameTextBox.Text = string.Empty;
                    }
                    try
                    {
                        FatherNameTextBox.Text = telephoneInfo.Rows[0]["FatherName"].ToString();
                    }
                    catch (Exception ex)
                    {
                        FatherNameTextBox.Text = string.Empty;
                    }
                    try
                    {
                        MelliCodeTextBox.Text = telephoneInfo.Rows[0]["MelliCode"].ToString();
                    }
                    catch (Exception ex)
                    {
                        MelliCodeTextBox.Text = string.Empty;
                    }
                    try
                    {
                        MobileTextBox.Text = telephoneInfo.Rows[0]["MOBILE"].ToString();
                    }
                    catch (Exception ex)
                    {
                        MobileTextBox.Text = string.Empty;
                    }
                    try
                    {
                        EmailTextBox.Text = telephoneInfo.Rows[0]["EMAIL"].ToString();
                    }
                    catch (Exception ex)
                    {
                        EmailTextBox.Text = string.Empty;
                    }
                    try
                    {
                        CabinetNoTextBox.Text = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                    }
                    catch (Exception ex)
                    {
                        CabinetNoTextBox.Text = string.Empty;
                    }
                    try
                    {
                        CabinetinputNoTextBox.Text = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                    }
                    catch (Exception ex)
                    {
                        CabinetinputNoTextBox.Text = string.Empty;
                    }
                    try
                    {
                        PostNoTextBox.Text = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                    }
                    catch (Exception ex)
                    {
                        PostNoTextBox.Text = string.Empty;
                    }
                    try
                    {
                        PostEtesaliNoTextBox.Text = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();
                    }
                    catch (Exception ex)
                    {
                        PostEtesaliNoTextBox.Text = string.Empty;
                    }

                    if (Convert.ToInt16(telephoneInfo.Rows[0]["PCM_STATUS"]) == 1)
                    {
                        System.Data.DataTable pCMInfo = service.GetPCMInformation("Admin", "alibaba123", _Request.TelephoneNo.ToString());

                        if (pCMInfo.Rows.Count != 0)
                        {
                            PCMTechnicalInfo.Visibility = Visibility.Visible;
                            PortPCMTextBox.Text = pCMInfo.Rows[0]["PORT"].ToString();
                            ModelPCMTextBox.Text = pCMInfo.Rows[0]["PCM_MARK_NAME"].ToString();
                            TypePCMTextBox.Text = pCMInfo.Rows[0]["PCM_TYPE_NAME"].ToString();
                            RockPCMTextBox.Text = pCMInfo.Rows[0]["ROCK"].ToString();
                            ShelfPCMTextBox.Text = pCMInfo.Rows[0]["SHELF"].ToString();
                            CardPCMTextBox.Text = pCMInfo.Rows[0]["CARD"].ToString();
                        }
                    }
                    else
                    {
                        PCMTechnicalInfo.Visibility = Visibility.Collapsed;
                    }

                    System.Data.DataTable aDSLInfo = service.Phone_CUSTOMER_BOOKHTINFO(_Request.TelephoneNo.ToString());

                    if (aDSLInfo.Rows.Count != 0)
                    {
                        System.Data.DataSet aDSLDataSet = new System.Data.DataSet();
                        aDSLDataSet.Tables.Add(aDSLInfo);
                        BuchtsDataGrid.DataContext = aDSLDataSet.Tables[0];
                    }
                    else
                        BuchtsGrid.Visibility = Visibility.Collapsed;

                    if (_Failure117.HelpDeskTicketID != null)
                    {
                        HelpDeskTicketIDTextBox.Text = _Failure117.HelpDeskTicketID.ToString();
                        HelpDeskDescriptionTextBox.Text = _Failure117.HelpDeskDescription;

                        HelpDeskGroupBox.Visibility = Visibility.Visible;
                    }

                    if (_Failure117.LineStatusID != null)
                    {
                        Failure117LineStatus lineStatus = Failure117DB.GetLineStatusByID((int)_Failure117.LineStatusID);
                        _LineStatusTypeID = lineStatus.Type;
                        LineStatusTypeComboBox.SelectedValue = _LineStatusTypeID;
                        LineStatusComboBox.SelectedValue = _Failure117.LineStatusID;

                        if (string.Equals(lineStatus.Title, "همشنوایی"))
                        {
                            HearingTelephoneNoLabel.Visibility = Visibility.Visible;
                            HearingTelephoneNoTextBox.Text = _Failure117.HearingTelephoneNo.ToString();
                        }
                    }

                    if (_Failure117.FailureStatusID != null)
                    {
                        _FailureStatusTypeID = Convert.ToInt32(Failure117DB.GetFailureStatusByID((int)_Failure117.FailureStatusID).ParentID);
                        FailureStatusTypeComboBox.SelectedValue = _FailureStatusTypeID;
                        FailureStatusComboBox.SelectedValue = _Failure117.FailureStatusID;
                    }

                    switch (_RequestInfo.StepID)
                    {
                        case (int)DB.RequestStepFailure117.MDFAnalysis:
                            NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;
                            NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                            ResultLabel.Visibility = Visibility.Collapsed;
                            ResultListBox.Visibility = Visibility.Collapsed;

                            EndDateLabel.Visibility = Visibility.Collapsed;
                            EndDateTextBox.Visibility = Visibility.Collapsed;

                            OldCommentLabel.Visibility = Visibility.Collapsed;
                            OldCommentTextBox.Visibility = Visibility.Collapsed;

                            EndMDFPersonnelLabel.Visibility = Visibility.Collapsed;
                            EndMDFPersonnelComboBox.Visibility = Visibility.Collapsed;

                            CommentTextBox.Text = _Failure117.MDFCommnet;

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Exit };
                            break;

                        case (byte)DB.RequestStepFailure117.Network:
                            ActionStatusTab.Visibility = Visibility.Collapsed;
                            ActionStatusGrid.Visibility = Visibility.Collapsed;

                            LineStatusTypeComboBox.IsEnabled = false;
                            LineStatusComboBox.IsEnabled = false;

                            OldCommentLabel.Visibility = Visibility.Collapsed;
                            OldCommentTextBox.Visibility = Visibility.Collapsed;

                            ResultLabel.Visibility = Visibility.Collapsed;
                            ResultListBox.Visibility = Visibility.Collapsed;

                            EndDateLabel.Visibility = Visibility.Collapsed;
                            EndDateTextBox.Visibility = Visibility.Collapsed;
                            break;

                        case (byte)DB.RequestStepFailure117.Saloon:
                            ActionStatusTab.Visibility = Visibility.Collapsed;
                            ActionStatusGrid.Visibility = Visibility.Collapsed;

                            NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                            LineStatusTypeComboBox.IsEnabled = false;
                            LineStatusComboBox.IsEnabled = false;

                            OldCommentTextBox.Text = _Failure117.MDFCommnet;
                            CommentTextBox.Text = _Failure117.SaloonComment;

                            ResultLabel.Visibility = Visibility.Collapsed;
                            ResultListBox.Visibility = Visibility.Collapsed;

                            EndDateLabel.Visibility = Visibility.Collapsed;
                            EndDateTextBox.Visibility = Visibility.Collapsed;

                            break;

                        case (byte)DB.RequestStepFailure117.Cable:
                            ActionStatusTab.Visibility = Visibility.Collapsed;
                            ActionStatusGrid.Visibility = Visibility.Collapsed;

                            NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                            LineStatusTypeComboBox.IsEnabled = false;
                            LineStatusComboBox.IsEnabled = false;

                            OldCommentTextBox.Text = _Failure117.MDFCommnet;
                            CommentTextBox.Text = _Failure117.SaloonComment;

                            ResultLabel.Visibility = Visibility.Collapsed;
                            ResultListBox.Visibility = Visibility.Collapsed;

                            EndDateLabel.Visibility = Visibility.Collapsed;
                            EndDateTextBox.Visibility = Visibility.Collapsed;
                            break;

                        case (byte)DB.RequestStepFailure117.MDFConfirm:
                            LineStatusTypeComboBox.IsEnabled = false;
                            LineStatusComboBox.IsEnabled = false;
                            HearingTelephoneNoTextBox.IsReadOnly = true;

                            FailureStatusTypeComboBox.IsEnabled = false;
                            FailureStatusComboBox.IsEnabled = false;

                            ActionStatusComboBox.SelectedValue = (int)DB.Failure117ActionStatus.RemovalFailure;

                            EndDateLabel.Visibility = Visibility.Collapsed;
                            EndDateTextBox.Visibility = Visibility.Collapsed;

                            if (_Failure117.NetworkDate != null)
                                GetNetworkPerformance();
                            else
                                NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;
                            //if (_Failure117.NetworkComment != null)
                            //    OldCommentTextBox.Text = "شبکه هوایی : " + _Failure117.NetworkComment;
                            //if (_Failure117.SaloonComment != null)
                            //    OldCommentTextBox.Text = "سالن دستگاه : " + _Failure117.SaloonComment;

                            AdjacentTelephoneTextBox.IsReadOnly = true;

                            MDFPersonnelComboBox.IsEnabled = false;

                            OldCommentLabel.Visibility = Visibility.Collapsed;
                            OldCommentTextBox.Visibility = Visibility.Collapsed;

                            CommentTextBox.Text = _Failure117.EndMDFComment;
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Exit };
                            break;

                        case (byte)DB.RequestStepFailure117.Archived:
                            LineStatusTypeComboBox.IsEnabled = false;
                            LineStatusComboBox.IsEnabled = false;
                            HearingTelephoneNoTextBox.IsReadOnly = true;

                            FailureStatusTypeComboBox.IsEnabled = false;
                            FailureStatusComboBox.IsEnabled = false;

                            ActionStatusComboBox.IsEnabled = false;

                            if (_Failure117.NetworkDate != null)
                                GetNetworkPerformance();
                            else
                                NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                            ////if (_Failure117.NetworkComment != null)
                            ////    OldCommentTextBox.Text = "شبکه هوایی : " + _Failure117.NetworkComment;

                            //if (_Failure117.SaloonComment != null)
                            //    OldCommentTextBox.Text = "سالن دستگاه : " + _Failure117.SaloonComment;

                            ResultListBox.IsEnabled = false;
                            MDFPersonnelComboBox.IsEnabled = false;
                            EndMDFPersonnelComboBox.IsEnabled = false;

                            OldCommentLabel.Visibility = Visibility.Collapsed;
                            OldCommentTextBox.Visibility = Visibility.Collapsed;

                            EndDateTextBox.Text = Helper.GetPersianDate(_Failure117.EndMDFDate, Helper.DateStringType.DateTime);

                            CommentTextBox.Text = _Failure117.EndMDFComment;
                            CommentTextBox.IsReadOnly = true;

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                            break;

                        default:
                            break;
                    }

                    List<FailureHistoryInfo> historyList = Failure117DB.SearchFailureHistory(_Request.ID, (long)_Request.TelephoneNo);

                    if (historyList.Count != 0)
                    {
                        ItemsDataGrid.ItemsSource = historyList;
                        HistoryMessageLable.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        ItemsDataGrid.Visibility = Visibility.Collapsed;
                        HistoryMessageLable.Visibility = Visibility.Visible;
                    }

                    ResizeWindow();
                }
            }

            if (DB.City == "kermanshah")
            {
                RequestGroupBox.IsExpanded = false;
                TelephoneGroupBox.IsExpanded = false;
                CustomerGroupBox.IsExpanded = false;

                MDFPersonnelComboBox.Visibility = Visibility.Collapsed;
                EndMDFPersonnelComboBox.Visibility = Visibility.Collapsed;
                MDFPersonnelTextBox.Visibility = Visibility.Visible;
                EndMDFPersonnelTextBox.Visibility = Visibility.Visible;

                if (RequestID != 0)
                {
                    _Request = Data.RequestDB.GetRequestByID(RequestID);
                    _Failure117 = Failure117DB.GetFailureRequestByID(RequestID);
                    _RequestInfo = Data.RequestDB.GetRequestInfoByID(RequestID);

                    if (_Request.TelephoneNo == 2333388205)
                    {
                        _RequestInfo = Data.RequestDB.GetRequestInfoByID(RequestID);

                        this.DataContext = _Failure117;

                        RequestNoTextBox.Text = _Request.ID.ToString();
                        ElkaNoTextBox.Text = "";
                        RequestDateTextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                        CallingNoTextBox.Text = _Failure117.CallingNo.ToString();

                        TelephoneGroupBox.Visibility = Visibility.Collapsed;
                        CustomerGroupBox.Visibility = Visibility.Collapsed;
                        TechnicalGroupBox.Visibility = Visibility.Collapsed;
                        OprationGroupBox.Visibility = Visibility.Collapsed;
                        NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;
                        CommentGroupBox.Visibility = Visibility.Collapsed;

                        ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Delete, (byte)DB.NewAction.Exit };
                    }
                    else
                    {
                        NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                        TelephoneGroupBox.Visibility = Visibility.Visible;
                        CustomerGroupBox.Visibility = Visibility.Visible;
                        TechnicalGroupBox.Visibility = Visibility.Visible;
                        OprationGroupBox.Visibility = Visibility.Visible;
                        NetworkPerformanceGroupBox.Visibility = Visibility.Visible;
                        CommentGroupBox.Visibility = Visibility.Visible;

                        this.DataContext = _Failure117;

                        telephoneInfo = TelephoneDB.GetTelephoneInfoForFailure((long)_Request.TelephoneNo);

                        RequestNoTextBox.Text = _Request.ID.ToString();
                        RequestDateTextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                        ElkaNoTextBox.Visibility = Visibility.Collapsed;
                        ElkaNoLabel.Visibility = Visibility.Collapsed;
                        CallingNoTextBox.Text = _Failure117.CallingNo.ToString();
                        TelephoneNoTextBox.Text = _Request.TelephoneNo.ToString();

                        if (telephoneInfo != null)
                        {
                            CenterTextBox.Text = telephoneInfo.Center;
                            PostalCodeTextBox.Text = telephoneInfo.PostalCode;
                            AddressTextBox.Text = telephoneInfo.Address;
                            FirstNameTextBox.Text = telephoneInfo.FirstName;
                            LastNameTextBox.Text = telephoneInfo.LastName;
                            FatherNameTextBox.Text = telephoneInfo.FatherName;
                            MelliCodeTextBox.Text = telephoneInfo.NationalCodeOrRecordNo;
                            MobileTextBox.Text = telephoneInfo.Mobile;
                            EmailTextBox.Text = telephoneInfo.Email;
                        }

                        technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo((long)_Request.TelephoneNo);
                        if (technicalInfo != null)
                        {
                            List<TechnicalInfoFailure117> technicalInfoList = new List<TechnicalInfoFailure117>();

                            CabinetNoTextBox.Text = technicalInfo.CabinetNo;
                            CabinetinputNoTextBox.Text = technicalInfo.CabinetInputNumber;
                            PostNoTextBox.Text = technicalInfo.PostNo;
                            PostEtesaliNoTextBox.Text = technicalInfo.ConnectionNo;

                            if (technicalInfo.IsPCM)
                            {
                                PCMTechnicalInfo.Visibility = Visibility.Visible;

                                PortPCMTextBox.Text = technicalInfo.PCMPort;
                                ModelPCMTextBox.Text = technicalInfo.PCMModel;
                                TypePCMTextBox.Text = technicalInfo.PCMType;
                                RockPCMTextBox.Text = technicalInfo.PCMRock;
                                ShelfPCMTextBox.Text = technicalInfo.PCMShelf;
                                CardPCMTextBox.Text = technicalInfo.PCMCard;

                                TechnicalInfoFailure117 pCMInput = Failure117DB.GetPCMInputbyTelephoneNo((long)_Request.TelephoneNo);
                                pCMInput.BOOKHT_TYPE_NAME = "ورودی PCM";
                                technicalInfoList.Add(pCMInput);
                                BuchtsDataGrid.ItemsSource = technicalInfoList;
                            }
                            else
                                PCMTechnicalInfo.Visibility = Visibility.Collapsed;

                            if (technicalInfo.HasAnotherBucht == false)
                            {
                                technicalInfo.BOOKHT_TYPE_NAME = "بوخت اصلی";
                                technicalInfoList.Add(technicalInfo);
                            }
                            else
                            {
                                List<Bucht> anotheBuchtList = Failure117DB.GetAnotherBuchtInfobyTelephoneNo((long)_Request.TelephoneNo);
                                TechnicalInfoFailure117 technicalInfo1;
                                foreach (Bucht bucht in anotheBuchtList)
                                {
                                    technicalInfo1 = new TechnicalInfoFailure117();
                                    technicalInfo1.BOOKHT_TYPE_NAME = Helper.GetEnumDescriptionByValue(typeof(DB.BuchtType), bucht.BuchtTypeID);
                                    technicalInfo1.RADIF = bucht.ColumnNo.ToString();
                                    technicalInfo1.TABAGHE = bucht.RowNo.ToString();
                                    technicalInfo1.ETESALII = bucht.BuchtNo.ToString();

                                    technicalInfoList.Add(technicalInfo1);
                                }
                            }

                            BuchtsDataGrid.ItemsSource = technicalInfoList;

                            TechnicalInfoFailure117 aDSLInfo = Failure117DB.ADSLPAPbyTelephoneNo((long)_Request.TelephoneNo);

                            if (aDSLInfo != null)
                            {
                                technicalInfoList.Add(aDSLInfo);
                                BuchtsDataGrid.ItemsSource = technicalInfoList;
                            }
                        }

                        if (_Failure117.HelpDeskTicketID != null)
                        {
                            HelpDeskTicketIDTextBox.Text = _Failure117.HelpDeskTicketID.ToString();
                            HelpDeskDescriptionTextBox.Text = _Failure117.HelpDeskDescription;

                            HelpDeskGroupBox.Visibility = Visibility.Visible;
                        }

                        if (_Failure117.LineStatusID != null)
                        {
                            Failure117LineStatus lineStatus = Failure117DB.GetLineStatusByID((int)_Failure117.LineStatusID);
                            _LineStatusTypeID = lineStatus.Type;
                            LineStatusTypeComboBox.SelectedValue = _LineStatusTypeID;
                            LineStatusComboBox.SelectedValue = _Failure117.LineStatusID;

                            if (string.Equals(lineStatus.Title, "همشنوایی"))
                            {
                                HearingTelephoneNoLabel.Visibility = Visibility.Visible;
                                HearingTelephoneNoTextBox.Text = _Failure117.HearingTelephoneNo.ToString();
                            }
                        }

                        if (_Failure117.FailureStatusID != null)
                        {
                            _FailureStatusTypeID = Convert.ToInt32(Failure117DB.GetFailureStatusByID((int)_Failure117.FailureStatusID).ParentID);
                            FailureStatusTypeComboBox.SelectedValue = _FailureStatusTypeID;
                            FailureStatusComboBox.SelectedValue = _Failure117.FailureStatusID;
                        }

                        switch (_RequestInfo.StepID)
                        {
                            case (int)DB.RequestStepFailure117.MDFAnalysis:
                                FailureStatusTab.Visibility = Visibility.Collapsed;
                                FailureStatusTypeLabel.Visibility = Visibility.Collapsed;
                                FailureStatusTypeComboBox.Visibility = Visibility.Collapsed;
                                FailureStatusLabel.Visibility = Visibility.Collapsed;
                                FailureStatusComboBox.Visibility = Visibility.Collapsed;

                                NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;
                                NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                                ResultLabel.Visibility = Visibility.Collapsed;
                                ResultListBox.Visibility = Visibility.Collapsed;

                                EndDateLabel.Visibility = Visibility.Collapsed;
                                EndDateTextBox.Visibility = Visibility.Collapsed;

                                OldCommentLabel.Visibility = Visibility.Collapsed;
                                OldCommentTextBox.Visibility = Visibility.Collapsed;

                                EndMDFPersonnelLabel.Visibility = Visibility.Collapsed;
                                EndMDFPersonnelTextBox.Visibility = Visibility.Collapsed;

                                CommentTextBox.Text = _Failure117.MDFCommnet;

                                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Exit };
                                break;

                            case (byte)DB.RequestStepFailure117.Network:
                                ActionStatusTab.Visibility = Visibility.Collapsed;
                                ActionStatusGrid.Visibility = Visibility.Collapsed;

                                LineStatusTypeComboBox.IsEnabled = false;
                                LineStatusComboBox.IsEnabled = false;

                                OldCommentLabel.Visibility = Visibility.Collapsed;
                                OldCommentTextBox.Visibility = Visibility.Collapsed;

                                ResultLabel.Visibility = Visibility.Collapsed;
                                ResultListBox.Visibility = Visibility.Collapsed;

                                EndDateLabel.Visibility = Visibility.Collapsed;
                                EndDateTextBox.Visibility = Visibility.Collapsed;
                                break;

                            case (byte)DB.RequestStepFailure117.Saloon:
                                ActionStatusTab.Visibility = Visibility.Collapsed;
                                ActionStatusGrid.Visibility = Visibility.Collapsed;

                                NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                                LineStatusTypeComboBox.IsEnabled = false;
                                LineStatusComboBox.IsEnabled = false;

                                OldCommentTextBox.Text = _Failure117.MDFCommnet;
                                CommentTextBox.Text = _Failure117.SaloonComment;

                                ResultLabel.Visibility = Visibility.Collapsed;
                                ResultListBox.Visibility = Visibility.Collapsed;

                                EndDateLabel.Visibility = Visibility.Collapsed;
                                EndDateTextBox.Visibility = Visibility.Collapsed;

                                break;

                            case (byte)DB.RequestStepFailure117.Cable:
                                ActionStatusTab.Visibility = Visibility.Collapsed;
                                ActionStatusGrid.Visibility = Visibility.Collapsed;

                                NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                                LineStatusTypeComboBox.IsEnabled = false;
                                LineStatusComboBox.IsEnabled = false;

                                OldCommentTextBox.Text = _Failure117.MDFCommnet;
                                CommentTextBox.Text = _Failure117.SaloonComment;

                                ResultLabel.Visibility = Visibility.Collapsed;
                                ResultListBox.Visibility = Visibility.Collapsed;

                                EndDateLabel.Visibility = Visibility.Collapsed;
                                EndDateTextBox.Visibility = Visibility.Collapsed;
                                break;

                            case (byte)DB.RequestStepFailure117.MDFConfirm:
                                LineStatusTypeComboBox.IsEnabled = false;
                                LineStatusComboBox.IsEnabled = false;
                                HearingTelephoneNoTextBox.IsReadOnly = true;

                                FailureStatusTypeComboBox.IsEnabled = false;
                                FailureStatusComboBox.IsEnabled = false;

                                ActionStatusComboBox.SelectedValue = (int)DB.Failure117ActionStatus.RemovalFailure;

                                EndDateLabel.Visibility = Visibility.Collapsed;
                                EndDateTextBox.Visibility = Visibility.Collapsed;

                                if (_Failure117.NetworkDate != null)
                                    GetNetworkPerformance();
                                else
                                    NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;
                                //if (_Failure117.NetworkComment != null)
                                //    OldCommentTextBox.Text = "شبکه هوایی : " + _Failure117.NetworkComment;
                                //if (_Failure117.SaloonComment != null)
                                //    OldCommentTextBox.Text = "سالن دستگاه : " + _Failure117.SaloonComment;

                                AdjacentTelephoneTextBox.IsReadOnly = true;

                                OldCommentLabel.Visibility = Visibility.Collapsed;
                                OldCommentTextBox.Visibility = Visibility.Collapsed;

                                CommentTextBox.Text = _Failure117.EndMDFComment;
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Exit };
                                break;

                            case (byte)DB.RequestStepFailure117.Archived:
                                LineStatusTypeComboBox.IsEnabled = false;
                                LineStatusComboBox.IsEnabled = false;
                                HearingTelephoneNoTextBox.IsReadOnly = true;

                                FailureStatusTypeComboBox.IsEnabled = false;
                                FailureStatusComboBox.IsEnabled = false;

                                ActionStatusComboBox.IsEnabled = false;

                                if (_Failure117.NetworkDate != null)
                                    GetNetworkPerformance();
                                else
                                    NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                                ////if (_Failure117.NetworkComment != null)
                                ////    OldCommentTextBox.Text = "شبکه هوایی : " + _Failure117.NetworkComment;

                                //if (_Failure117.SaloonComment != null)
                                //    OldCommentTextBox.Text = "سالن دستگاه : " + _Failure117.SaloonComment;

                                ResultListBox.IsEnabled = false;
                                //MDFPersonnelComboBox.IsEnabled = false;
                                //EndMDFPersonnelComboBox.IsEnabled = false;

                                OldCommentLabel.Visibility = Visibility.Collapsed;
                                OldCommentTextBox.Visibility = Visibility.Collapsed;

                                EndDateTextBox.Text = Helper.GetPersianDate(_Failure117.EndMDFDate, Helper.DateStringType.DateTime);

                                CommentTextBox.Text = _Failure117.EndMDFComment;
                                CommentTextBox.IsReadOnly = true;

                                ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                                break;

                            default:
                                break;
                        }

                        List<FailureHistoryInfo> historyList = Failure117DB.SearchFailureHistory(_Request.ID, (long)_Request.TelephoneNo);
                        List<FailureHistoryInfo> historyList1 = new List<FailureHistoryInfo>();
                        if (historyList != null && historyList.Count != 0)
                        {
                            foreach (FailureHistoryInfo item in historyList)
                            {
                                FailureForm form = Failure117DB.GetFailureForm(item.ID);
                                if (form != null)
                                    item.GetNetworkFormDate = Helper.GetPersianDate(form.GetNetworkFormDate, Helper.DateStringType.DateTime);

                                historyList1.Add(item);
                            }
                        }
                        GetNetworkFormDateColumn.Visibility = Visibility.Visible;

                        if (historyList.Count != 0)
                        {
                            ItemsDataGrid.ItemsSource = historyList1;
                            HistoryMessageLable.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            ItemsDataGrid.Visibility = Visibility.Collapsed;
                            HistoryMessageLable.Visibility = Visibility.Visible;
                        }

                        ResizeWindow();
                    }
                }
                if (RequestIDs != null && RequestIDs.Count != 0)
                {
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };

                    _RequestList = Data.RequestDB.GetRequestListByID(RequestIDs);
                    _Failure117List = Failure117DB.GetFailureRequestListByID(RequestIDs);
                    _RequestInfoList = Data.RequestDB.GetRequestInfoListByIDs(RequestIDs);

                    RequestGroupBox.Visibility = Visibility.Collapsed;
                    NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                    TelephoneGroupBox.Visibility = Visibility.Collapsed;
                    CustomerGroupBox.Visibility = Visibility.Collapsed;
                    TechnicalGroupBox.Visibility = Visibility.Collapsed;
                    HelpDeskGroupBox.Visibility = Visibility.Collapsed;
                    NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;
                    HistoryTab.Visibility = Visibility.Collapsed;
                    RecordSoundTab.Visibility = Visibility.Collapsed;
                    LineStatusTab.IsSelected = true;
                    ItemsDataGrid.Visibility = Visibility.Collapsed;
                    HistoryMessageLable.Visibility = Visibility.Collapsed;

                    RequestListGroupBox.Visibility = Visibility.Visible;

                    switch (_RequestInfoList[0].StepID)
                    {
                        case (int)DB.RequestStepFailure117.MDFAnalysis:
                            FailureStatusTab.Visibility = Visibility.Collapsed;
                            FailureStatusTypeLabel.Visibility = Visibility.Collapsed;
                            FailureStatusTypeComboBox.Visibility = Visibility.Collapsed;
                            FailureStatusLabel.Visibility = Visibility.Collapsed;
                            FailureStatusComboBox.Visibility = Visibility.Collapsed;

                            NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;
                            NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                            ResultLabel.Visibility = Visibility.Collapsed;
                            ResultListBox.Visibility = Visibility.Collapsed;

                            EndDateLabel.Visibility = Visibility.Collapsed;
                            EndDateTextBox.Visibility = Visibility.Collapsed;

                            OldCommentLabel.Visibility = Visibility.Collapsed;
                            OldCommentTextBox.Visibility = Visibility.Collapsed;

                            EndMDFPersonnelLabel.Visibility = Visibility.Collapsed;
                            EndMDFPersonnelTextBox.Visibility = Visibility.Collapsed;

                            CommentTextBox.Text = _Failure117.MDFCommnet;

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Exit };
                            break;

                        case (byte)DB.RequestStepFailure117.Network:
                            ActionStatusTab.Visibility = Visibility.Collapsed;
                            ActionStatusGrid.Visibility = Visibility.Collapsed;

                            LineStatusTypeComboBox.IsEnabled = false;
                            LineStatusComboBox.IsEnabled = false;

                            OldCommentLabel.Visibility = Visibility.Collapsed;
                            OldCommentTextBox.Visibility = Visibility.Collapsed;

                            ResultLabel.Visibility = Visibility.Collapsed;
                            ResultListBox.Visibility = Visibility.Collapsed;

                            EndDateLabel.Visibility = Visibility.Collapsed;
                            EndDateTextBox.Visibility = Visibility.Collapsed;
                            break;

                        case (byte)DB.RequestStepFailure117.Saloon:
                            ActionStatusTab.Visibility = Visibility.Collapsed;
                            ActionStatusGrid.Visibility = Visibility.Collapsed;

                            NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                            LineStatusTypeComboBox.IsEnabled = false;
                            LineStatusComboBox.IsEnabled = false;

                            OldCommentTextBox.Text = _Failure117.MDFCommnet;
                            CommentTextBox.Text = _Failure117.SaloonComment;

                            ResultLabel.Visibility = Visibility.Collapsed;
                            ResultListBox.Visibility = Visibility.Collapsed;

                            EndDateLabel.Visibility = Visibility.Collapsed;
                            EndDateTextBox.Visibility = Visibility.Collapsed;

                            break;

                        case (byte)DB.RequestStepFailure117.Cable:
                            ActionStatusTab.Visibility = Visibility.Collapsed;
                            ActionStatusGrid.Visibility = Visibility.Collapsed;

                            NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                            LineStatusTypeComboBox.IsEnabled = false;
                            LineStatusComboBox.IsEnabled = false;

                            OldCommentTextBox.Text = _Failure117.MDFCommnet;
                            CommentTextBox.Text = _Failure117.SaloonComment;

                            ResultLabel.Visibility = Visibility.Collapsed;
                            ResultListBox.Visibility = Visibility.Collapsed;

                            EndDateLabel.Visibility = Visibility.Collapsed;
                            EndDateTextBox.Visibility = Visibility.Collapsed;
                            break;

                        case (byte)DB.RequestStepFailure117.MDFConfirm:
                            LineStatusTypeComboBox.IsEnabled = false;
                            LineStatusComboBox.IsEnabled = false;
                            HearingTelephoneNoTextBox.IsReadOnly = true;

                            FailureStatusTypeComboBox.IsEnabled = false;
                            FailureStatusComboBox.IsEnabled = false;

                            ActionStatusComboBox.SelectedValue = (int)DB.Failure117ActionStatus.RemovalFailure;

                            EndDateLabel.Visibility = Visibility.Collapsed;
                            EndDateTextBox.Visibility = Visibility.Collapsed;

                            if (_Failure117.NetworkDate != null)
                                GetNetworkPerformance();
                            else
                                NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;
                            //if (_Failure117.NetworkComment != null)
                            //    OldCommentTextBox.Text = "شبکه هوایی : " + _Failure117.NetworkComment;
                            //if (_Failure117.SaloonComment != null)
                            //    OldCommentTextBox.Text = "سالن دستگاه : " + _Failure117.SaloonComment;

                            AdjacentTelephoneTextBox.IsReadOnly = true;

                            //MDFPersonnelComboBox.IsEnabled = false;

                            OldCommentLabel.Visibility = Visibility.Collapsed;
                            OldCommentTextBox.Visibility = Visibility.Collapsed;

                            CommentTextBox.Text = _Failure117.EndMDFComment;
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Exit };
                            break;

                        case (byte)DB.RequestStepFailure117.Archived:
                            LineStatusTypeComboBox.IsEnabled = false;
                            LineStatusComboBox.IsEnabled = false;
                            HearingTelephoneNoTextBox.IsReadOnly = true;

                            FailureStatusTypeComboBox.IsEnabled = false;
                            FailureStatusComboBox.IsEnabled = false;

                            ActionStatusComboBox.IsEnabled = false;

                            if (_Failure117.NetworkDate != null)
                                GetNetworkPerformance();
                            else
                                NetworkPerformanceGroupBox.Visibility = Visibility.Collapsed;

                            ////if (_Failure117.NetworkComment != null)
                            ////    OldCommentTextBox.Text = "شبکه هوایی : " + _Failure117.NetworkComment;

                            //if (_Failure117.SaloonComment != null)
                            //    OldCommentTextBox.Text = "سالن دستگاه : " + _Failure117.SaloonComment;

                            ResultListBox.IsEnabled = false;
                            //MDFPersonnelComboBox.IsEnabled = false;
                            //EndMDFPersonnelComboBox.IsEnabled = false;

                            OldCommentLabel.Visibility = Visibility.Collapsed;
                            OldCommentTextBox.Visibility = Visibility.Collapsed;

                            EndDateTextBox.Text = Helper.GetPersianDate(_Failure117.EndMDFDate, Helper.DateStringType.DateTime);

                            CommentTextBox.Text = _Failure117.EndMDFComment;
                            CommentTextBox.IsReadOnly = true;

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                            break;

                        default:
                            break;
                    }

                    Failure117RequestPrintInfo failureInfo = null;
                    List<Failure117RequestPrintInfo> failureInfoList = new List<Failure117RequestPrintInfo>();

                    foreach (Request currentRequest in _RequestList)
                    {
                        failureInfo = new Failure117RequestPrintInfo();

                        failureInfo = Failure117DB.GetFailure117RequestPrintbyTelephoneNos((long)currentRequest.TelephoneNo);

                        if (failureInfo == null)
                            failureInfo = new Failure117RequestPrintInfo();

                        failureInfo.ID = currentRequest.ID.ToString();
                        failureInfo.TelephoneNo = currentRequest.TelephoneNo.ToString();

                        failureInfoList.Add(failureInfo);
                    }

                    RequestsDataGrid.ItemsSource = failureInfoList;
                }
            }
        }

        public override bool Save()
        {
            if (DB.City == "semnan")
            {
                try
                {
                    Service1 service = new Service1();

                    switch (_RequestInfo.StepID)
                    {
                        case (int)DB.RequestStepFailure117.MDFAnalysis:
                            if (_Request.TelephoneNo == 2333388205)
                            {
                                bool isPhoneExist = service.Is_Phone_Exist(NewTelephoneNoTextBox.Text.Replace(" ", ""));
                                if (!isPhoneExist)
                                    throw new Exception("شماره وارد شده موجود نمی باشد");
                                else
                                {
                                    Request request = new Request();

                                    request = Failure117DB.GetFailureRequest(Convert.ToInt64(NewTelephoneNoTextBox.Text));

                                    if (request != null)
                                    {
                                        if (request.EndDate == null || request.EndDate > DB.GetServerDate())
                                            throw new Exception("درخواست خرابی برای این شماره در حال پیگیری می باشد");
                                        else
                                        {
                                            _Request.TelephoneNo = Convert.ToInt64(NewTelephoneNoTextBox.Text);
                                            Service1 service2 = new Service1();
                                            System.Data.DataTable telephoneInfo = service2.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());
                                            _Request.CenterID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"]));

                                            _Failure117.CabinetNo = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                                            _Failure117.CabinetMarkazi = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                                            _Failure117.PostNo = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                                            _Failure117.PostEtesali = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();

                                            RequestForFailure117.SaveFailureRequest(_Request, _Failure117, false);
                                        }
                                    }
                                    else
                                    {
                                        _Request.TelephoneNo = Convert.ToInt64(NewTelephoneNoTextBox.Text);
                                        Service1 service2 = new Service1();
                                        System.Data.DataTable telephoneInfo = service2.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());
                                        _Request.CenterID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"]));

                                        _Failure117.CabinetNo = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                                        _Failure117.CabinetMarkazi = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                                        _Failure117.PostNo = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                                        _Failure117.PostEtesali = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();

                                        RequestForFailure117.SaveFailureRequest(_Request, _Failure117, false);
                                    }
                                }

                                List<CenterInfo> centers = CenterDB.GetCenters();
                                if (centers.Where(t => t.ID == _Request.CenterID).SingleOrDefault() != null)
                                    LoadData();
                                else
                                    this.DialogResult = true;
                            }
                            else
                            {
                                if (LineStatusComboBox.SelectedValue != null)
                                    _Failure117.LineStatusID = (int)LineStatusComboBox.SelectedValue;
                                else
                                    _Failure117.LineStatusID = null;

                                if (ActionStatusComboBox.SelectedValue != null)
                                    _Failure117.ActionStatusID = (byte)ActionStatusComboBox.SelectedValue;
                                else
                                    _Failure117.ActionStatusID = null;

                                if (FailureStatusComboBox.SelectedValue != null)
                                    _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                                else
                                    _Failure117.FailureStatusID = null;

                                if (!string.IsNullOrEmpty(HearingTelephoneNoTextBox.Text))
                                {
                                    if (!service.Is_Phone_Exist(HearingTelephoneNoTextBox.Text))
                                        throw new Exception("شماره تلفن همشنوایی وارد شده موجود نمی باشد");
                                    else
                                        _Failure117.HearingTelephoneNo = Convert.ToInt64(HearingTelephoneNoTextBox.Text);
                                }
                                else
                                    _Failure117.HearingTelephoneNo = null;
                                if (!string.IsNullOrEmpty(AdjacentTelephoneTextBox.Text))
                                {
                                    if (!service.Is_Phone_Exist(AdjacentTelephoneTextBox.Text))
                                        throw new Exception("شماره تلفن همجوار وارد شده موجود نمی باشد");
                                    _Failure117.AdjacentTelephoneNo = Convert.ToInt64(AdjacentTelephoneTextBox.Text);
                                }
                                else
                                    _Failure117.AdjacentTelephoneNo = null;

                                if (MDFPersonnelComboBox.SelectedValue != null)
                                    _Failure117.MDFPesonnelID = (int)MDFPersonnelComboBox.SelectedValue;
                                else
                                    _Failure117.MDFPesonnelID = null;
                                _Failure117.MDFCommnet = CommentTextBox.Text;
                            }
                            break;

                        case (byte)DB.RequestStepFailure117.Network:
                            if (FailureStatusComboBox.SelectedValue != null)
                                _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                            else
                                _Failure117.FailureStatusID = null;

                            _Failure117.NetworkComment = CommentTextBox.Text;
                            break;

                        case (byte)DB.RequestStepFailure117.Saloon:
                            if (FailureStatusComboBox.SelectedValue != null)
                                _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                            else
                                _Failure117.FailureStatusID = null;

                            _Failure117.SaloonComment = CommentTextBox.Text;
                            break;

                        case (byte)DB.RequestStepFailure117.Cable:
                            if (FailureStatusComboBox.SelectedValue != null)
                                _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                            else
                                _Failure117.FailureStatusID = null;

                            _Failure117.CableComment = CommentTextBox.Text;
                            break;

                        case (byte)DB.RequestStepFailure117.MDFConfirm:
                            if (ResultListBox.SelectedValue == null)
                                _Failure117.ResultAfterReturn = 0;
                            else
                                _Failure117.ResultAfterReturn = (byte)Convert.ToInt16(ResultListBox.SelectedValue);
                            if (EndMDFPersonnelComboBox.SelectedValue != null)
                                _Failure117.EndMDFPersonnelID = (int)EndMDFPersonnelComboBox.SelectedValue;
                            else
                                _Failure117.EndMDFPersonnelID = null;
                            _Failure117.EndMDFComment = CommentTextBox.Text;
                            break;

                        default:
                            break;
                    }

                    RequestForFailure117.SaveFailureActions(_Failure117);

                    ShowSuccessMessage("ذخیره با موفقیت انجام شد.");
                    IsSaveSuccess = true;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("خطا در ذخیره، " + ex.Message + "!", ex);
                }
            }

            if (DB.City == "kermanshah")
            {
                try
                {
                    switch (_RequestInfo.StepID)
                    {
                        case (int)DB.RequestStepFailure117.MDFAnalysis:
                            if (_Request.TelephoneNo == 2333388205)
                            {
                                if (TelephoneDB.GetTelephoneByTelephoneNo(Convert.ToInt64(NewTelephoneNoTextBox.Text.Replace(" ", ""))) == null)
                                    throw new Exception("شماره وارد شده موجود نمی باشد");
                                else
                                {
                                    Request request = new Request();

                                    request = Failure117DB.GetFailureRequest(Convert.ToInt64(NewTelephoneNoTextBox.Text));

                                    if (request != null)
                                    {
                                        if (request.EndDate == null || request.EndDate > DB.GetServerDate())
                                            throw new Exception("درخواست خرابی برای این شماره در حال پیگیری می باشد");
                                        else
                                        {
                                            _Request.TelephoneNo = Convert.ToInt64(NewTelephoneNoTextBox.Text);
                                            _Request.CenterID = telephoneInfo.CenterID;

                                            _Failure117.CabinetNo = technicalInfo.CabinetNo;
                                            _Failure117.CabinetMarkazi = technicalInfo.CabinetInputNumber;
                                            _Failure117.PostNo = technicalInfo.PostNo;
                                            _Failure117.PostEtesali = technicalInfo.ConnectionNo;

                                            RequestForFailure117.SaveFailureRequest(_Request, _Failure117, false);
                                        }
                                    }
                                    else
                                    {
                                        _Request.TelephoneNo = Convert.ToInt64(NewTelephoneNoTextBox.Text);
                                        _Request.CenterID = telephoneInfo.CenterID;

                                        _Failure117.CabinetNo = technicalInfo.CabinetNo;
                                        _Failure117.CabinetMarkazi = technicalInfo.CabinetInputNumber;
                                        _Failure117.PostNo = technicalInfo.PostNo;
                                        _Failure117.PostEtesali = technicalInfo.ConnectionNo;

                                        RequestForFailure117.SaveFailureRequest(_Request, _Failure117, false);
                                    }
                                }

                                List<CenterInfo> centers = CenterDB.GetCenters();
                                if (centers.Where(t => t.ID == _Request.CenterID).SingleOrDefault() != null)
                                    LoadData();
                                else
                                    this.DialogResult = true;
                            }
                            else
                            {
                                if (LineStatusComboBox.SelectedValue != null)
                                    _Failure117.LineStatusID = (int)LineStatusComboBox.SelectedValue;
                                else
                                    _Failure117.LineStatusID = null;

                                if (ActionStatusComboBox.SelectedValue != null)
                                    _Failure117.ActionStatusID = (byte)ActionStatusComboBox.SelectedValue;
                                else
                                    _Failure117.ActionStatusID = null;

                                if (FailureStatusComboBox.SelectedValue != null)
                                    _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                                else
                                    _Failure117.FailureStatusID = null;

                                if (!string.IsNullOrEmpty(HearingTelephoneNoTextBox.Text))
                                {
                                    if (TelephoneDB.GetTelephoneByTelephoneNo(Convert.ToInt64(HearingTelephoneNoTextBox.Text)) == null)
                                        throw new Exception("شماره تلفن همشنوایی وارد شده موجود نمی باشد");
                                    else
                                        _Failure117.HearingTelephoneNo = Convert.ToInt64(HearingTelephoneNoTextBox.Text);
                                }
                                else
                                    _Failure117.HearingTelephoneNo = null;
                                if (!string.IsNullOrEmpty(AdjacentTelephoneTextBox.Text))
                                {
                                    if (TelephoneDB.GetTelephoneByTelephoneNo(Convert.ToInt64((AdjacentTelephoneTextBox.Text))) == null)
                                        throw new Exception("شماره تلفن همجوار وارد شده موجود نمی باشد");
                                    _Failure117.AdjacentTelephoneNo = Convert.ToInt64(AdjacentTelephoneTextBox.Text);
                                }
                                else
                                    _Failure117.AdjacentTelephoneNo = null;

                                //if (MDFPersonnelComboBox.SelectedValue != null)
                                _Failure117.MDFPesonnelID = DB.currentUser.ID;// (int)MDFPersonnelComboBox.SelectedValue;
                                //else
                                //    _Failure117.MDFPesonnelID = null;
                                _Failure117.MDFCommnet = CommentTextBox.Text;
                            }
                            break;

                        case (byte)DB.RequestStepFailure117.Network:
                            if (FailureStatusComboBox.SelectedValue != null)
                                _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                            else
                                _Failure117.FailureStatusID = null;

                            _Failure117.NetworkComment = CommentTextBox.Text;
                            break;

                        case (byte)DB.RequestStepFailure117.Saloon:
                            if (FailureStatusComboBox.SelectedValue != null)
                                _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                            else
                                _Failure117.FailureStatusID = null;

                            _Failure117.SaloonComment = CommentTextBox.Text;
                            break;

                        case (byte)DB.RequestStepFailure117.Cable:
                            if (FailureStatusComboBox.SelectedValue != null)
                                _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                            else
                                _Failure117.FailureStatusID = null;

                            _Failure117.CableComment = CommentTextBox.Text;
                            break;

                        case (byte)DB.RequestStepFailure117.MDFConfirm:
                            if (ResultListBox.SelectedValue == null)
                                _Failure117.ResultAfterReturn = 0;
                            else
                                _Failure117.ResultAfterReturn = (byte)Convert.ToInt16(ResultListBox.SelectedValue);

                            //if (EndMDFPersonnelComboBox.SelectedValue != null)
                            _Failure117.EndMDFPersonnelID = DB.currentUser.ID;// (int)EndMDFPersonnelComboBox.SelectedValue;
                            //else
                            //    _Failure117.EndMDFPersonnelID = null;
                            _Failure117.EndMDFComment = CommentTextBox.Text;
                            break;

                        default:
                            break;
                    }

                    RequestForFailure117.SaveFailureActions(_Failure117);

                    ShowSuccessMessage("ذخیره با موفقیت انجام شد.");
                    IsSaveSuccess = true;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("خطا در ذخیره، " + ex.Message + "!", ex);
                }
            }

            return IsSaveSuccess;
        }

        public override bool Delete()
        {
            MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Request request = RequestDB.GetRequestByID(RequestID);
                if (request != null)
                {
                    request.IsCancelation = true;

                    request.Detach();
                    DB.Save(request);
                }
                //DB.Delete<Data.Failure117>(RequestID);
                //DB.Delete<Data.Request>(RequestID);

                this.DialogResult = true;

                //IsDeleteSuccess = true;
                IsDeleteSuccess = false;
            }

            return IsDeleteSuccess;
        }

        public override bool Forward()
        {
            if (DB.City == "semnan")
            {
                try
                {
                    Service1 service = new Service1();

                    switch (_RequestInfo.StepID)
                    {
                        case (int)DB.RequestStepFailure117.MDFAnalysis:
                            if (ActionStatusComboBox.SelectedValue == null)
                                throw new Exception("لطفا وضعیت اقدام را انتخاب نمایید");
                            else
                            {
                                if ((byte)ActionStatusComboBox.SelectedValue == (byte)DB.Failure117ActionStatus.RemovalFailure)
                                    throw new Exception("لطفا وضعیت اقدام را چک نمایید");

                                if (LineStatusComboBox.SelectedValue != null)
                                    _Failure117.LineStatusID = (int)LineStatusComboBox.SelectedValue;
                                else
                                    throw new Exception("لطفا وضعیت خط را تعیین نمایید");

                                if (ActionStatusComboBox.SelectedValue != null)
                                    _Failure117.ActionStatusID = (byte)ActionStatusComboBox.SelectedValue;
                                else
                                    _Failure117.ActionStatusID = null;

                                if (FailureStatusComboBox.SelectedValue != null)
                                    _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                                else
                                    _Failure117.FailureStatusID = null;

                                if (!string.IsNullOrEmpty(HearingTelephoneNoTextBox.Text))
                                {
                                    if (!service.Is_Phone_Exist(HearingTelephoneNoTextBox.Text))
                                        throw new Exception("شماره تلفن همشنوایی وارد شده موجود نمی باشد");
                                    else
                                        _Failure117.HearingTelephoneNo = Convert.ToInt64(HearingTelephoneNoTextBox.Text);
                                }
                                else
                                    _Failure117.HearingTelephoneNo = null;
                                if (!string.IsNullOrEmpty(AdjacentTelephoneTextBox.Text))
                                {
                                    if (!service.Is_Phone_Exist(AdjacentTelephoneTextBox.Text))
                                        throw new Exception("شماره تلفن همجوار وارد شده موجود نمی باشد");
                                    _Failure117.AdjacentTelephoneNo = Convert.ToInt64(AdjacentTelephoneTextBox.Text);
                                }
                                else
                                    _Failure117.AdjacentTelephoneNo = null;

                                if (MDFPersonnelComboBox.SelectedValue != null)
                                    _Failure117.MDFPesonnelID = (int)MDFPersonnelComboBox.SelectedValue;
                                else
                                    throw new Exception("لطفا نام مامور بررسی درخواست را وارد نمایید");
                                _Failure117.MDFUserID = DB.CurrentUser.ID;
                                _Failure117.MDFDate = DB.GetServerDate();
                                _Failure117.MDFCommnet = CommentTextBox.Text;

                                switch ((byte)ActionStatusComboBox.SelectedValue)
                                {
                                    case (byte)DB.Failure117ActionStatus.ReferenceNetwork:
                                        _Request.StatusID = 1364;

                                        FailureForm form = null;
                                        form = Failure117DB.GetFailureForm(RequestID);

                                        if (form == null)
                                        {
                                            form = new FailureForm();
                                            form.FailureRequestID = RequestID;
                                            form.RowNo = Failure117DB.GetFormRowNo(_Request.CenterID);
                                            form.CableColor1 = null;
                                            form.CableColor2 = null;
                                            form.CableTypeID = 0;
                                            form.FormInsertDate = DB.GetServerDate();

                                            RequestForFailure117.SaveFailureForm(form, true);
                                        }

                                        break;

                                    //case (byte)DB.Failure117ActionStatus.ReferenceCabel:
                                    //    _Request.StatusID = 1365;
                                    //    break;

                                    case (byte)DB.Failure117ActionStatus.ReferenceSaloon:
                                        _Request.StatusID = 1366;
                                        break;

                                    case (byte)DB.Failure117ActionStatus.RemovalFailure:
                                        _Request.StatusID = 1367;
                                        break;

                                    default:
                                        break;
                                }
                            }
                            break;

                        case (int)DB.RequestStepFailure117.Saloon:
                            if (FailureStatusComboBox.SelectedValue != null)
                                _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                            else
                                throw new Exception("لطفا وضعیت خرابی را تعیین نمایید");

                            _Failure117.SaloonUserID = DB.CurrentUser.ID;
                            _Failure117.SaloonDate = DB.GetServerDate();
                            _Failure117.SaloonComment = CommentTextBox.Text;

                            _Request.StatusID = 1367;
                            break;

                        //case (int)DB.RequestStepFailure117.Cable:
                        //    if (FailureStatusComboBox.SelectedValue == null && FailureStatusTypeComboBox.SelectedValue == null)
                        //        throw new Exception("لطفا وضعیت خرابی را تعیین نمایید");
                        //    else
                        //        _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;

                        //    _Failure117.CableUserID = DB.CurrentUser.ID;
                        //    _Failure117.CableDate = DB.GetServerDate();
                        //    _Failure117.CableComment = CommentTextBox.Text;

                        //    _Request.StatusID = 1367;
                        //    break;

                        case (int)DB.RequestStepFailure117.MDFConfirm:
                            if (ActionStatusComboBox.SelectedValue == null)
                                throw new Exception("لطفا وضعیت اقدام را انتخاب نمایید");

                            if ((byte)Convert.ToInt16(ActionStatusComboBox.SelectedValue) == (byte)DB.Failure117ActionStatus.ReferenceNetwork || (byte)Convert.ToInt16(ActionStatusComboBox.SelectedValue) == (byte)DB.Failure117ActionStatus.ReferenceSaloon)
                                throw new Exception("امکان ارجاع مجدد وجود ندارد");

                            if ((byte)ActionStatusComboBox.SelectedValue == (byte)DB.Failure117ActionStatus.RemovalFailure)
                                throw new Exception("لطفا وضعیت اقدام را چک نمایید");

                            if (EndMDFPersonnelComboBox.SelectedValue != null)
                                _Failure117.EndMDFPersonnelID = (int)EndMDFPersonnelComboBox.SelectedValue;
                            else
                                throw new Exception("لطفا نام مامور تایید درخواست را وارد نمایید");
                            _Failure117.EndMDFUserID = DB.CurrentUser.ID;
                            _Failure117.EndMDFDate = DB.GetServerDate();
                            _Failure117.EndMDFComment = CommentTextBox.Text;

                            if (ResultListBox.SelectedValue == null)
                                _Failure117.ResultAfterReturn = 0;
                            else
                                _Failure117.ResultAfterReturn = (byte)Convert.ToInt16(ResultListBox.SelectedValue);

                            switch ((byte)ActionStatusComboBox.SelectedValue)
                            {
                                case (byte)DB.Failure117ActionStatus.ReferenceNetwork:
                                    _Request.StatusID = 1364;
                                    break;

                                //case (byte)DB.Failure117ActionStatus.ReferenceCabel:
                                //    _Request.StatusID = 1365;
                                //    break;

                                case (byte)DB.Failure117ActionStatus.ReferenceSaloon:
                                    _Request.StatusID = 1366;
                                    break;

                                default:
                                    break;
                            }

                            break;

                        default:
                            break;
                    }

                    _Request.PreviousAction = (byte)DB.Action.Confirm;
                    _Request.ModifyDate = DB.GetServerDate();
                    _Request.ModifyUserID = DB.CurrentUser.ID;
                    if (_Request.EndDate == null)
                        _Request.IsViewed = false;
                    else
                        _Request.IsViewed = true;

                    RequestForFailure117.SaveFailureRequest(_Request, _Failure117, false);

                    IsForwardSuccess = false;
                    this.DialogResult = true;
                }

                catch (Exception ex)
                {
                    ShowErrorMessage("خطا در ارجاع، " + ex.Message + "!", ex);
                }
            }

            if (DB.City == "kermanshah")
            {
                if (RequestID != 0)
                {
                    try
                    {
                        switch (_RequestInfo.StepID)
                        {
                            case (int)DB.RequestStepFailure117.MDFAnalysis:
                                if (ActionStatusComboBox.SelectedValue == null)
                                    throw new Exception("لطفا وضعیت اقدام را انتخاب نمایید");
                                else
                                {
                                    if ((byte)ActionStatusComboBox.SelectedValue == (byte)DB.Failure117ActionStatus.RemovalFailure)
                                        throw new Exception("لطفا وضعیت اقدام را چک نمایید");

                                    if (LineStatusComboBox.SelectedValue == null)
                                        throw new Exception("لطفا وضعیت خط را تعیین نمایید");
                                    else
                                    {
                                        if ((byte)Convert.ToInt16(LineStatusTypeComboBox.SelectedValue) == (byte)DB.Failure117LineStatus.PhysicalMDF && (byte)ActionStatusComboBox.SelectedValue != (byte)DB.Failure117ActionStatus.RemovalFailure)
                                            throw new Exception("ارجاع امکان پذیر نمی باشد");

                                        if ((byte)Convert.ToInt16(LineStatusTypeComboBox.SelectedValue) == (byte)DB.Failure117LineStatus.Legal && (byte)ActionStatusComboBox.SelectedValue != (byte)DB.Failure117ActionStatus.RemovalFailure)
                                            throw new Exception("ارجاع امکان پذیر نمی باشد");

                                        if ((byte)Convert.ToInt16(LineStatusTypeComboBox.SelectedValue) == (byte)DB.Failure117LineStatus.Operational && (byte)ActionStatusComboBox.SelectedValue != (byte)DB.Failure117ActionStatus.ReferenceSaloon)
                                            throw new Exception("وضعیت اقدام و وضعیت خط با یکدیگر سازگاری ندارد");

                                        if ((byte)Convert.ToInt16(LineStatusTypeComboBox.SelectedValue) == (byte)DB.Failure117LineStatus.PhysicalNetwork && (byte)ActionStatusComboBox.SelectedValue != (byte)DB.Failure117ActionStatus.ReferenceNetwork)
                                            throw new Exception("وضعیت اقدام و وضعیت خط با یکدیگر سازگاری ندارد");

                                        _Failure117.LineStatusID = (int)LineStatusComboBox.SelectedValue;
                                    }

                                    if (ActionStatusComboBox.SelectedValue != null)
                                        _Failure117.ActionStatusID = (byte)ActionStatusComboBox.SelectedValue;
                                    else
                                        _Failure117.ActionStatusID = null;

                                    if (FailureStatusComboBox.SelectedValue != null)
                                        _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                                    else
                                        _Failure117.FailureStatusID = null;

                                    if (!string.IsNullOrEmpty(HearingTelephoneNoTextBox.Text))
                                    {
                                        if (TelephoneDB.GetTelephoneByTelephoneNo(Convert.ToInt64(HearingTelephoneNoTextBox.Text)) == null)
                                            throw new Exception("شماره تلفن همشنوایی وارد شده موجود نمی باشد");
                                        else
                                            _Failure117.HearingTelephoneNo = Convert.ToInt64(HearingTelephoneNoTextBox.Text);
                                    }
                                    else
                                        _Failure117.HearingTelephoneNo = null;
                                    if (!string.IsNullOrEmpty(AdjacentTelephoneTextBox.Text))
                                    {
                                        if (TelephoneDB.GetTelephoneByTelephoneNo(Convert.ToInt64(AdjacentTelephoneTextBox.Text)) == null)
                                            throw new Exception("شماره تلفن همجوار وارد شده موجود نمی باشد");
                                        _Failure117.AdjacentTelephoneNo = Convert.ToInt64(AdjacentTelephoneTextBox.Text);
                                    }
                                    else
                                        _Failure117.AdjacentTelephoneNo = null;

                                    //if (MDFPersonnelComboBox.SelectedValue != null)
                                    _Failure117.MDFPesonnelID = DB.currentUser.ID;// (int)MDFPersonnelComboBox.SelectedValue;
                                    //else
                                    //    throw new Exception("لطفا نام مامور بررسی درخواست را وارد نمایید");

                                    _Failure117.MDFUserID = DB.CurrentUser.ID;
                                    _Failure117.MDFDate = DB.GetServerDate();
                                    _Failure117.MDFCommnet = CommentTextBox.Text;

                                    switch ((byte)ActionStatusComboBox.SelectedValue)
                                    {
                                        case (byte)DB.Failure117ActionStatus.ReferenceNetwork:
                                            _Request.StatusID = 1364;

                                            FailureForm form = null;
                                            form = Failure117DB.GetFailureForm(RequestID);

                                            if (form == null)
                                            {
                                                form = new FailureForm();
                                                form.FailureRequestID = RequestID;
                                                form.CableColor1 = null;
                                                form.CableColor2 = null;
                                                form.CableTypeID = 0;
                                                form.FormInsertDate = DB.GetServerDate();
                                                form.RowNo = Failure117DB.GetFormRowNo(_Request.CenterID);

                                                RequestForFailure117.SaveFailureForm(form, true);
                                            }

                                            break;

                                        //case (byte)DB.Failure117ActionStatus.ReferenceCabel:
                                        //    _Request.StatusID = 1365;
                                        //    break;

                                        case (byte)DB.Failure117ActionStatus.ReferenceSaloon:
                                            _Request.StatusID = 1366;
                                            break;

                                        case (byte)DB.Failure117ActionStatus.RemovalFailure:
                                            _Request.StatusID = 1367;
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                break;

                            case (int)DB.RequestStepFailure117.Saloon:
                                if (FailureStatusComboBox.SelectedValue != null)
                                    _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                                else
                                    throw new Exception("لطفا وضعیت خرابی را تعیین نمایید");

                                _Failure117.SaloonUserID = DB.CurrentUser.ID;
                                _Failure117.SaloonDate = DB.GetServerDate();
                                _Failure117.SaloonComment = CommentTextBox.Text;

                                _Request.StatusID = 1367;
                                break;

                            //case (int)DB.RequestStepFailure117.Cable:
                            //    if (FailureStatusComboBox.SelectedValue == null && FailureStatusTypeComboBox.SelectedValue == null)
                            //        throw new Exception("لطفا وضعیت خرابی را تعیین نمایید");
                            //    else
                            //        _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;

                            //    _Failure117.CableUserID = DB.CurrentUser.ID;
                            //    _Failure117.CableDate = DB.GetServerDate();
                            //    _Failure117.CableComment = CommentTextBox.Text;

                            //    _Request.StatusID = 1367;
                            //    break;

                            case (int)DB.RequestStepFailure117.MDFConfirm:
                                if (ActionStatusComboBox.SelectedValue == null)
                                    throw new Exception("لطفا وضعیت اقدام را انتخاب نمایید");

                                if ((byte)Convert.ToInt16(ActionStatusComboBox.SelectedValue) == (byte)DB.Failure117ActionStatus.ReferenceNetwork || (byte)Convert.ToInt16(ActionStatusComboBox.SelectedValue) == (byte)DB.Failure117ActionStatus.ReferenceSaloon)
                                    throw new Exception("امکان ارجاع مجدد وجود ندارد");

                                if ((byte)ActionStatusComboBox.SelectedValue == (byte)DB.Failure117ActionStatus.RemovalFailure)
                                    throw new Exception("لطفا وضعیت اقدام را چک نمایید");

                                //if (EndMDFPersonnelComboBox.SelectedValue != null)
                                _Failure117.EndMDFPersonnelID = DB.currentUser.ID;// (int)EndMDFPersonnelComboBox.SelectedValue;
                                //else
                                //    throw new Exception("لطفا نام مامور تایید درخواست را وارد نمایید");

                                //_Failure117.EndMDFPersonnelID = DB.CurrentUser.ID;
                                _Failure117.EndMDFUserID = DB.CurrentUser.ID;
                                _Failure117.EndMDFDate = DB.GetServerDate();
                                _Failure117.EndMDFComment = CommentTextBox.Text;

                                if (ResultListBox.SelectedValue == null)
                                    _Failure117.ResultAfterReturn = 0;
                                else
                                    _Failure117.ResultAfterReturn = (byte)Convert.ToInt16(ResultListBox.SelectedValue);

                                switch ((byte)ActionStatusComboBox.SelectedValue)
                                {
                                    case (byte)DB.Failure117ActionStatus.ReferenceNetwork:
                                        _Request.StatusID = 1364;
                                        break;

                                    //case (byte)DB.Failure117ActionStatus.ReferenceCabel:
                                    //    _Request.StatusID = 1365;
                                    //    break;

                                    case (byte)DB.Failure117ActionStatus.ReferenceSaloon:
                                        _Request.StatusID = 1366;
                                        break;

                                    default:
                                        break;
                                }

                                break;

                            default:
                                break;
                        }

                        _Request.PreviousAction = (byte)DB.Action.Confirm;
                        _Request.ModifyDate = DB.GetServerDate();
                        _Request.ModifyUserID = DB.CurrentUser.ID;
                        if (_Request.EndDate == null)
                            _Request.IsViewed = false;
                        else
                            _Request.IsViewed = true;

                        RequestForFailure117.SaveFailureRequest(_Request, _Failure117, false);

                        IsForwardSuccess = false;
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("خطا در ارجاع، " + ex.Message + "!", ex);
                    }
                }

                if (RequestIDs != null && RequestIDs.Count != 0)
                {
                    try
                    {
                        _RequestList = Data.RequestDB.GetRequestListByID(RequestIDs);
                        _Failure117List = Failure117DB.GetFailureRequestListByID(RequestIDs);
                        _RequestInfoList = Data.RequestDB.GetRequestInfoListByIDs(RequestIDs);

                        switch (_RequestInfoList[0].StepID)
                        {
                            case (int)DB.RequestStepFailure117.MDFAnalysis:

                                if (ActionStatusComboBox.SelectedValue == null)
                                    throw new Exception("لطفا وضعیت اقدام را انتخاب نمایید");
                                else
                                {
                                    if ((byte)ActionStatusComboBox.SelectedValue == (byte)DB.Failure117ActionStatus.RemovalFailure)
                                        throw new Exception("لطفا وضعیت اقدام را چک نمایید");

                                    foreach (Request currentRequest in _RequestList)
                                    {
                                        _Failure117 = Failure117DB.GetFailureRequestByID(currentRequest.ID);

                                        if (LineStatusComboBox.SelectedValue != null)
                                            _Failure117.LineStatusID = (int)LineStatusComboBox.SelectedValue;
                                        else
                                            throw new Exception("لطفا وضعیت خط را تعیین نمایید");

                                        if (ActionStatusComboBox.SelectedValue != null)
                                            _Failure117.ActionStatusID = (byte)ActionStatusComboBox.SelectedValue;
                                        else
                                            _Failure117.ActionStatusID = null;

                                        if (FailureStatusComboBox.SelectedValue != null)
                                            _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                                        else
                                            _Failure117.FailureStatusID = null;

                                        _Failure117.HearingTelephoneNo = null;
                                        _Failure117.AdjacentTelephoneNo = null;

                                        //if (MDFPersonnelComboBox.SelectedValue != null)
                                        _Failure117.MDFPesonnelID = DB.currentUser.ID;// (int)MDFPersonnelComboBox.SelectedValue;
                                        //else
                                        //    throw new Exception("لطفا نام مامور بررسی درخواست را وارد نمایید");

                                        _Failure117.MDFUserID = DB.CurrentUser.ID;
                                        _Failure117.MDFDate = DB.GetServerDate();
                                        _Failure117.MDFCommnet = CommentTextBox.Text;

                                        switch ((byte)ActionStatusComboBox.SelectedValue)
                                        {
                                            case (byte)DB.Failure117ActionStatus.ReferenceNetwork:
                                                currentRequest.StatusID = 1364;
                                                FailureForm form = new FailureForm();
                                                form.FailureRequestID = currentRequest.ID;
                                                form.CableColor1 = null;
                                                form.CableColor2 = null;
                                                form.CableTypeID = 0;
                                                form.FormInsertDate = DB.GetServerDate();
                                                form.RowNo = Failure117DB.GetFormRowNo(currentRequest.CenterID);

                                                RequestForFailure117.SaveFailureForm(form, true);

                                                break;

                                            //case (byte)DB.Failure117ActionStatus.ReferenceCabel:
                                            //    _Request.StatusID = 1365;
                                            //    break;

                                            case (byte)DB.Failure117ActionStatus.ReferenceSaloon:
                                                currentRequest.StatusID = 1366;
                                                break;

                                            case (byte)DB.Failure117ActionStatus.RemovalFailure:
                                                currentRequest.StatusID = 1367;
                                                break;

                                            default:
                                                break;
                                        }

                                        currentRequest.PreviousAction = (byte)DB.Action.Confirm;
                                        currentRequest.ModifyDate = DB.GetServerDate();
                                        currentRequest.ModifyUserID = DB.CurrentUser.ID;
                                        if (currentRequest.EndDate == null)
                                            currentRequest.IsViewed = false;
                                        else
                                            currentRequest.IsViewed = true;

                                        RequestForFailure117.SaveFailureRequest(currentRequest, _Failure117, false);
                                    }
                                }

                                break;

                            case (int)DB.RequestStepFailure117.Saloon:
                                foreach (Request currentRequest in _RequestList)
                                {
                                    _Failure117 = Failure117DB.GetFailureRequestByID(currentRequest.ID);
                                    if (FailureStatusComboBox.SelectedValue != null)
                                        _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                                    else
                                        throw new Exception("لطفا وضعیت خرابی را تعیین نمایید");

                                    _Failure117.SaloonUserID = DB.CurrentUser.ID;
                                    _Failure117.SaloonDate = DB.GetServerDate();
                                    _Failure117.SaloonComment = CommentTextBox.Text;

                                    currentRequest.StatusID = 1367;

                                    currentRequest.PreviousAction = (byte)DB.Action.Confirm;
                                    currentRequest.ModifyDate = DB.GetServerDate();
                                    currentRequest.ModifyUserID = DB.CurrentUser.ID;
                                    if (currentRequest.EndDate == null)
                                        currentRequest.IsViewed = false;
                                    else
                                        currentRequest.IsViewed = true;

                                    RequestForFailure117.SaveFailureRequest(currentRequest, _Failure117, false);
                                }
                                break;

                            case (int)DB.RequestStepFailure117.MDFConfirm:
                                foreach (Request currentRequest in _RequestList)
                                {
                                    if (ActionStatusComboBox.SelectedValue == null)
                                        throw new Exception("لطفا وضعیت اقدام را انتخاب نمایید");

                                    if ((byte)Convert.ToInt16(ActionStatusComboBox.SelectedValue) == (byte)DB.Failure117ActionStatus.ReferenceNetwork || (byte)Convert.ToInt16(ActionStatusComboBox.SelectedValue) == (byte)DB.Failure117ActionStatus.ReferenceSaloon)
                                        throw new Exception("امکان ارجاع مجدد وجود ندارد");

                                    if ((byte)ActionStatusComboBox.SelectedValue == (byte)DB.Failure117ActionStatus.RemovalFailure)
                                        throw new Exception("لطفا وضعیت اقدام را چک نمایید");

                                    //if (EndMDFPersonnelComboBox.SelectedValue != null)
                                    _Failure117.EndMDFPersonnelID = DB.currentUser.ID;// (int)EndMDFPersonnelComboBox.SelectedValue;
                                    //else
                                    //    throw new Exception("لطفا نام مامور تایید درخواست را وارد نمایید");

                                    //_Failure117.EndMDFPersonnelID = DB.CurrentUser.ID;
                                    _Failure117.EndMDFUserID = DB.CurrentUser.ID;
                                    _Failure117.EndMDFDate = DB.GetServerDate();
                                    _Failure117.EndMDFComment = CommentTextBox.Text;

                                    if (ResultListBox.SelectedValue == null)
                                        _Failure117.ResultAfterReturn = 0;
                                    else
                                        _Failure117.ResultAfterReturn = (byte)Convert.ToInt16(ResultListBox.SelectedValue);

                                    switch ((byte)ActionStatusComboBox.SelectedValue)
                                    {
                                        case (byte)DB.Failure117ActionStatus.ReferenceNetwork:
                                            currentRequest.StatusID = 1364;
                                            break;

                                        //case (byte)DB.Failure117ActionStatus.ReferenceCabel:
                                        //    _Request.StatusID = 1365;
                                        //    break;

                                        case (byte)DB.Failure117ActionStatus.ReferenceSaloon:
                                            currentRequest.StatusID = 1366;
                                            break;

                                        default:
                                            break;
                                    }

                                    currentRequest.PreviousAction = (byte)DB.Action.Confirm;
                                    currentRequest.ModifyDate = DB.GetServerDate();
                                    currentRequest.ModifyUserID = DB.CurrentUser.ID;
                                    if (currentRequest.EndDate == null)
                                        currentRequest.IsViewed = false;
                                    else
                                        currentRequest.IsViewed = true;

                                    RequestForFailure117.SaveFailureRequest(currentRequest, _Failure117, false);
                                }
                                break;

                            default:
                                break;
                        }

                        IsForwardSuccess = false;
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("خطا در ارجاع، " + ex.Message + "!", ex);
                    }
                }
            }

            return IsForwardSuccess;
        }

        public override bool ConfirmEnd()
        {
            if (RequestID != 0)
            {
                try
                {
                    if (ActionStatusComboBox.SelectedValue == null)
                        throw new Exception("لطفا وضعیت اقدام را انتخاب نمایید");

                    if ((byte)ActionStatusComboBox.SelectedValue != (byte)DB.Failure117ActionStatus.RemovalFailure)
                        throw new Exception("لطفا وضعیت اقدام را چک نمایید");

                    if (LineStatusComboBox.SelectedValue != null)
                        _Failure117.LineStatusID = (int)LineStatusComboBox.SelectedValue;
                    else
                        throw new Exception("لطفا وضعیت خط را تعیین نمایید");

                    if (_RequestInfo.StepID == (int)DB.RequestStepFailure117.MDFAnalysis)
                    {
                        if (FailureStatusComboBox.SelectedValue != null)
                            _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;

                        _Failure117.ActionStatusID = (byte)DB.Failure117ActionStatus.RemovalFailure;

                        if (DB.City == "semnan")
                        {
                            if (MDFPersonnelComboBox.SelectedValue != null)
                                _Failure117.MDFPesonnelID = (int)MDFPersonnelComboBox.SelectedValue;
                            else
                                throw new Exception("لطفا نام مامور بررسی را وارد نمایید");
                        }
                        if (DB.City == "kermanshah")
                            _Failure117.MDFPesonnelID = DB.currentUser.ID;

                        _Failure117.MDFUserID = DB.CurrentUser.ID;
                        _Failure117.MDFDate = DB.GetServerDate();

                        _Failure117.EndMDFUserID = DB.CurrentUser.ID;
                        _Failure117.EndMDFDate = DB.GetServerDate();
                    }
                    else
                    {
                        if (FailureStatusComboBox.SelectedValue == null || FailureStatusTypeComboBox.SelectedValue == null)
                            throw new Exception("لطفا وضعیت خرابی را تعیین نمایید");
                        else
                            _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;

                        if (DB.City == "semnan")
                        {
                            if (EndMDFPersonnelComboBox.SelectedValue != null)
                                _Failure117.EndMDFPersonnelID = (int)EndMDFPersonnelComboBox.SelectedValue;
                            else
                                throw new Exception("لطفا نام مامور تایید را وارد نمایید");
                        }
                        if (DB.City == "kermanshah")
                            _Failure117.EndMDFPersonnelID = DB.currentUser.ID;

                        _Failure117.ActionStatusID = (byte)DB.Failure117ActionStatus.RemovalFailure;
                        _Failure117.EndMDFUserID = DB.CurrentUser.ID;
                        _Failure117.EndMDFDate = DB.GetServerDate();
                    }

                    if (ResultListBox.SelectedValue == null)
                        throw new Exception("لطفا نتیجه خرابی را انتخاب نمایید");
                    else
                        _Failure117.ResultAfterReturn = (byte)Convert.ToInt16(ResultListBox.SelectedValue);

                    _Request.StatusID = 1368;
                    _Request.PreviousAction = (byte)DB.Action.Confirm;
                    _Request.ModifyDate = DB.GetServerDate();
                    _Request.ModifyUserID = DB.CurrentUser.ID;
                    _Request.IsViewed = true;

                    if (_Failure117.FailureStatusID != null)
                    {
                        Failure117FailureStatus status = Failure117DB.GetFailureStatusByID((int)_Failure117.FailureStatusID);
                        _Request.EndDate = DB.GetServerDate().AddHours((int)status.ArchivedTime);
                    }
                    else
                        _Request.EndDate = DB.GetServerDate().AddHours(24);

                    if (_Failure117.HelpDeskTicketID != null)
                    {
                        if (string.IsNullOrEmpty(CommentTextBox.Text))
                            throw new Exception("لطفا توضیحات لازم را وارد نمایید");
                        else
                        {
                            bool inquiryResult = false;
                            bool result2 = true;

                            _Failure117.MDFCommnet = CommentTextBox.Text;
                            HelpDeskService.HelpDeskService helpDeskServise = new HelpDeskService.HelpDeskService();
                            helpDeskServise.ReplyHelpDeskMDFInquiry((long)_Request.TelephoneNo, true, (long)_Failure117.HelpDeskTicketID, true, CommentTextBox.Text, RequestID, true, out inquiryResult, out result2);
                            if (!inquiryResult)
                                throw new Exception("اختلالی در عمل تایید ایجاد شده است، لطفا مجددا سعی نمایید");
                        }
                    }

                    RequestForFailure117.SaveFailureRequest(_Request, _Failure117, false);

                    IsConfirmEndSuccess = false;
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("خطا در تایید نهایی، " + ex.Message + "!", ex);
                }
            }

            if (RequestIDs != null && RequestIDs.Count != 0)
            {
                try
                {
                    if (ActionStatusComboBox.SelectedValue == null)
                        throw new Exception("لطفا وضعیت اقدام را انتخاب نمایید");

                    if ((byte)ActionStatusComboBox.SelectedValue != (byte)DB.Failure117ActionStatus.RemovalFailure)
                        throw new Exception("لطفا وضعیت اقدام را چک نمایید");

                    foreach (Request currentRequest in _RequestList)
                    {
                        _Failure117 = Failure117DB.GetFailureRequestByID(currentRequest.ID);

                        if (_RequestInfoList[0].StepID == (int)DB.RequestStepFailure117.MDFAnalysis)
                        {
                            if (LineStatusComboBox.SelectedValue != null)
                                _Failure117.LineStatusID = (int)LineStatusComboBox.SelectedValue;
                            else
                                throw new Exception("لطفا وضعیت خط را تعیین نمایید");

                            //if (FailureStatusComboBox.SelectedValue != null)
                            //    _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;
                            //else
                            //    throw new Exception("لطفا وضعیت خرابی را تعیین نمایید");

                            _Failure117.ActionStatusID = (byte)DB.Failure117ActionStatus.RemovalFailure;

                            if (DB.City == "semnan")
                            {
                                if (MDFPersonnelComboBox.SelectedValue != null)
                                    _Failure117.MDFPesonnelID = (int)MDFPersonnelComboBox.SelectedValue;
                                else
                                    throw new Exception("لطفا نام مامور بررسی را وارد نمایید");
                            }
                            if (DB.City == "kermanshah")
                                _Failure117.MDFPesonnelID = DB.currentUser.ID;

                            _Failure117.MDFUserID = DB.CurrentUser.ID;
                            _Failure117.MDFDate = DB.GetServerDate();

                            _Failure117.EndMDFUserID = DB.CurrentUser.ID;
                            _Failure117.EndMDFDate = DB.GetServerDate();
                        }
                        else
                        {
                            //if (FailureStatusComboBox.SelectedValue == null || FailureStatusTypeComboBox.SelectedValue == null)
                            //    throw new Exception("لطفا وضعیت خرابی را تعیین نمایید");
                            //else
                            //    _Failure117.FailureStatusID = (int)FailureStatusComboBox.SelectedValue;

                            if (DB.City == "semnan")
                            {
                                if (EndMDFPersonnelComboBox.SelectedValue != null)
                                    _Failure117.EndMDFPersonnelID = (int)EndMDFPersonnelComboBox.SelectedValue;
                                else
                                    throw new Exception("لطفا نام مامور تایید را وارد نمایید");
                            }
                            if (DB.City == "kermanshah")
                                _Failure117.EndMDFPersonnelID = DB.currentUser.ID;

                            _Failure117.ActionStatusID = (byte)DB.Failure117ActionStatus.RemovalFailure;
                            _Failure117.EndMDFUserID = DB.CurrentUser.ID;
                            _Failure117.EndMDFDate = DB.GetServerDate();
                        }

                        if (ResultListBox.SelectedValue == null)
                            throw new Exception("لطفا نتیجه خرابی را انتخاب نمایید");
                        else
                            _Failure117.ResultAfterReturn = (byte)Convert.ToInt16(ResultListBox.SelectedValue);

                        currentRequest.StatusID = 1368;
                        currentRequest.PreviousAction = (byte)DB.Action.Confirm;
                        currentRequest.ModifyDate = DB.GetServerDate();
                        currentRequest.ModifyUserID = DB.CurrentUser.ID;
                        currentRequest.IsViewed = true;

                        if (_Failure117.FailureStatusID != null)
                        {
                            Failure117FailureStatus status = Failure117DB.GetFailureStatusByID((int)_Failure117.FailureStatusID);
                            currentRequest.EndDate = DB.GetServerDate().AddHours((int)status.ArchivedTime);
                        }
                        else
                            currentRequest.EndDate = DB.GetServerDate().AddHours(24);

                        if (_Failure117.HelpDeskTicketID != null)
                        {
                            if (string.IsNullOrEmpty(CommentTextBox.Text))
                                throw new Exception("لطفا توضیحات لازم را وارد نمایید");
                            else
                            {
                                bool inquiryResult = false;
                                bool result2 = true;

                                _Failure117.MDFCommnet = CommentTextBox.Text;
                                HelpDeskService.HelpDeskService helpDeskServise = new HelpDeskService.HelpDeskService();
                                helpDeskServise.ReplyHelpDeskMDFInquiry((long)currentRequest.TelephoneNo, true, (long)_Failure117.HelpDeskTicketID, true, CommentTextBox.Text, RequestID, true, out inquiryResult, out result2);
                                if (!inquiryResult)
                                    throw new Exception("اختلالی در عمل تایید ایجاد شده است، لطفا مجددا سعی نمایید");
                            }
                        }

                        RequestForFailure117.SaveFailureRequest(currentRequest, _Failure117, false);
                    }

                    IsConfirmEndSuccess = false;
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("خطا در تایید نهایی، " + ex.Message + "!", ex);
                }
            }
            return IsConfirmEndSuccess;
        }

        public override bool Confirm()
        {
            try
            {
                _Request.EndDate = DB.GetServerDate();

                _Request.Detach();
                DB.Save(_Request);

                IsConfirmSuccess = false;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در تایید ، " + ex.Message + "!", ex);
            }

            return IsConfirmSuccess;
        }

        public override bool Deny()
        {
            switch (_RequestInfo.StepID)
            {
                case (int)DB.RequestStepFailure117.Saloon:

                    _Failure117.MDFPesonnelID = null;
                    _Failure117.MDFUserID = null;
                    _Failure117.MDFDate = null;
                    _Failure117.MDFCommnet = null;

                    _Request.StatusID = 1363;
                    _Request.PreviousAction = (byte)DB.Action.Reject;
                    _Request.ModifyDate = DB.GetServerDate();
                    _Request.ModifyUserID = DB.CurrentUser.ID;
                    _Request.IsViewed = false;

                    RequestForFailure117.SaveFailureRequest(_Request, _Failure117, false);

                    IsRejectSuccess = true;
                    break;
            }

            this.DialogResult = true;
            return IsRejectSuccess;
        }

        public void GetNetworkPerformance()
        {
            NetworkDateTextBox.Text = Helper.GetPersianDate(_Failure117.NetworkDate, Helper.DateStringType.DateTime);

            //FailureForm form = DB.SearchByPropertyName<FailureForm>("FailureRequestID", RequestID).SingleOrDefault();
            //double compareResult = ((DateTime)form.GetNetworkFormDate - (DateTime)form.GiveNetworkFormDate).TotalMinutes;
            double compareResult = ((DateTime)_Failure117.NetworkDate - (DateTime)_Failure117.MDFDate).TotalMinutes;
            double hour = (compareResult < 60) ? 0 : Math.Round(compareResult / 60);
            double min = Math.Round(compareResult % 60, 2);
            NetworkSpeedTextBox.Text = string.Format("{0} : {1}", (min >= 10) ? min.ToString() : "0" + min.ToString(), (hour >= 10) ? hour.ToString() : "0" + hour.ToString());
        }

        public static byte[] ConvertMsg2Wave(byte[] msgBuffer)
        {
            if (msgBuffer == null || msgBuffer.Length == 0) return null;

            MemoryStream waveStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(waveStream);
            {
                writer.Write(Encoding.ASCII.GetBytes("RIFF"));
                writer.Write(msgBuffer.Length + 50);
                writer.Write(Encoding.ASCII.GetBytes("WAVEfmt "));
                writer.Write(0x12);
                writer.Write((short)6);
                writer.Write((short)1);
                writer.Write(8000);
                writer.Write(8000);
                writer.Write((short)1);
                writer.Write((short)8);
                writer.Write((short)0);
                writer.Write(Encoding.ASCII.GetBytes("fact"));
                writer.Write(new byte[] { 4, 0, 0, 0 });
                writer.Write(msgBuffer.Length);
                writer.Write(Encoding.ASCII.GetBytes("data"));
                writer.Write(msgBuffer.Length);
                writer.Write(msgBuffer);
            }

            writer.Close();
            return waveStream.GetBuffer();
        }

        #endregion

        #region Event Handlers

        private void LineStatusTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_LineStatusTypeID != 0)
            {
                if (LineStatusTypeComboBox.SelectedValue != null)
                    LineStatusComboBox.ItemsSource = Failure117DB.GetFailure117LineStatusbyTypeID((byte)Convert.ToInt16(LineStatusTypeComboBox.SelectedValue));// DB.SearchByPropertyName<Failure117LineStatus>("Type", LineStatusTypeComboBox.SelectedValue).ToList();
                else
                    LineStatusComboBox.ItemsSource = Failure117DB.GetFailure117LineStatusbyTypeID(_LineStatusTypeID);// DB.SearchByPropertyName<Failure117LineStatus>("Type", _LineStatusTypeID).ToList();
            }
            else
                LineStatusComboBox.ItemsSource = Failure117DB.GetFailure117LineStatusbyTypeID((byte)Convert.ToInt16(LineStatusTypeComboBox.SelectedValue));// DB.SearchByPropertyName<Failure117LineStatus>("Type", LineStatusTypeComboBox.SelectedValue).ToList();
        }

        private void FailureStatusTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int stepID = 0;

            if (RequestID != 0)
                stepID = _RequestInfo.StepID;

            if (RequestIDs != null && RequestIDs.Count != 0)
                stepID = _RequestInfoList[0].StepID;

            switch (stepID)
            {
                case (int)DB.RequestStepFailure117.MDFAnalysis:
                    if (_FailureStatusTypeID != 0)
                    {
                        if (FailureStatusTypeComboBox.SelectedValue != null)
                            FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID((int)FailureStatusTypeComboBox.SelectedValue, (byte)DB.Failure117AvalibilityStatus.MDFAnalysis);
                        else
                            FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID(_FailureStatusTypeID, (byte)DB.Failure117AvalibilityStatus.MDFAnalysis);
                    }
                    else
                        FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID((int)FailureStatusTypeComboBox.SelectedValue, (byte)DB.Failure117AvalibilityStatus.MDFAnalysis);
                    break;

                case (int)DB.RequestStepFailure117.Saloon:
                    if (_FailureStatusTypeID != 0)
                    {
                        if (FailureStatusTypeComboBox.SelectedValue != null)
                            FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID((int)FailureStatusTypeComboBox.SelectedValue, (byte)DB.Failure117AvalibilityStatus.Saloon);
                        else
                            FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID(_FailureStatusTypeID, (byte)DB.Failure117AvalibilityStatus.Saloon);
                    }
                    else
                        FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID((int)FailureStatusTypeComboBox.SelectedValue, (byte)DB.Failure117AvalibilityStatus.Saloon);
                    break;

                case (int)DB.RequestStepFailure117.Cable:
                    if (_FailureStatusTypeID != 0)
                    {
                        if (FailureStatusTypeComboBox.SelectedValue != null)
                            FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID((int)FailureStatusTypeComboBox.SelectedValue, (byte)DB.Failure117AvalibilityStatus.Cable);
                        else
                            FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID(_FailureStatusTypeID, (byte)DB.Failure117AvalibilityStatus.Cable);
                    }
                    else
                        FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID((int)FailureStatusTypeComboBox.SelectedValue, (byte)DB.Failure117AvalibilityStatus.Cable);
                    break;

                case (int)DB.RequestStepFailure117.MDFConfirm:
                    if (_FailureStatusTypeID != 0)
                    {
                        if (FailureStatusTypeComboBox.SelectedValue != null)
                            FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID((int)FailureStatusTypeComboBox.SelectedValue, (byte)DB.Failure117AvalibilityStatus.MDFConfirm);
                        else
                            FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID(_FailureStatusTypeID, (byte)DB.Failure117AvalibilityStatus.MDFConfirm);
                    }
                    else
                        FailureStatusComboBox.ItemsSource = Failure117DB.GetFailureStatusByParentID((int)FailureStatusTypeComboBox.SelectedValue, (byte)DB.Failure117AvalibilityStatus.MDFConfirm);
                    break;
            }
        }

        private void LineStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LineStatusComboBox.SelectedValue != null)
            {
                if (Convert.ToInt32(LineStatusComboBox.SelectedValue) == Failure117DB.GetLineStatusByTitle("همشنوایی").ID)
                {
                    HearingTelephoneNoLabel.Visibility = Visibility.Visible;
                    HearingTelephoneNoTextBox.Visibility = Visibility.Visible;
                }
                else
                {
                    HearingTelephoneNoLabel.Visibility = Visibility.Collapsed;
                    HearingTelephoneNoTextBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ActionStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ActionStatusComboBox.SelectedValue != null)
            {
                if (RequestID != 0)
                {
                    if (Convert.ToInt32(ActionStatusComboBox.SelectedValue) == (int)DB.Failure117ActionStatus.RemovalFailure)
                    {
                        switch (_RequestInfo.StepID)
                        {
                            case (int)DB.RequestStepFailure117.MDFAnalysis:
                                ResultLabel.Visibility = Visibility.Visible;
                                ResultListBox.Visibility = Visibility.Visible;
                                ResultListBox.SelectedIndex = 0;
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        if (_RequestInfo.StepID != (int)DB.RequestStepFailure117.MDFConfirm)
                        {
                            ResultLabel.Visibility = Visibility.Collapsed;
                            ResultListBox.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                if (RequestIDs != null && RequestIDs.Count != 0)
                {
                    if (Convert.ToInt32(ActionStatusComboBox.SelectedValue) == (int)DB.Failure117ActionStatus.RemovalFailure)
                    {
                        switch (_RequestInfoList[0].StepID)
                        {
                            case (int)DB.RequestStepFailure117.MDFAnalysis:
                                ResultLabel.Visibility = Visibility.Visible;
                                ResultListBox.Visibility = Visibility.Visible;
                                ResultListBox.SelectedIndex = 0;
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        if (_RequestInfoList[0].StepID != (int)DB.RequestStepFailure117.MDFConfirm)
                        {
                            ResultLabel.Visibility = Visibility.Collapsed;
                            ResultListBox.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void ShowFormLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Failure117NetworkForm form = new Failure117NetworkForm(RequestID);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        private void PlaySound_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SoundMessageLabel.Visibility = Visibility.Collapsed;

            System.Data.Linq.Binary sound = Data.Failure117DB.GetFailureRequestByID(RequestID).RecordeSound;

            if (sound == null || sound.Length == 0)
            {
                SoundMessageLabel.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                string tempPath = System.IO.Path.GetTempPath();
                string fileName = System.IO.Path.Combine(tempPath, "CRM_" + (RequestID) + ".wav");

                if (!System.IO.File.Exists(fileName))
                {
                    byte[] content = sound.ToArray();
                    File.WriteAllBytes(fileName, ConvertMsg2Wave(content));
                }

                System.Diagnostics.Process.Start(fileName);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion

        private void BtnSendSms_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Enterprise.Logger.WriteInfo("Step 1");
                ir.tcsem.twbs.Service1 service = new ir.tcsem.twbs.Service1();
                string userName = "SmsPendar#20117";
                string passWord = "Sms#$18795?5";
                string MobileNo = "";
                string Message = SmsContentTextBox1.Text.Trim() + 
                       ( !string.IsNullOrEmpty( SimColor.Text.Trim() )?  " رنگ سیم : " +SimColor.Text.Trim() :"");
                string PhoneNo = TelephoneNoTextBox.Text.Trim();
                DBHelper dbh = new DBHelper("172.24.2.33", "sa", "pendar@tci", "CRM");
                Enterprise.Logger.WriteInfo("tep 2");
                Enterprise.Logger.WriteInfo(PhoneNo);
                //Enterprise.Logger.WriteInfo(dbh.c);
                object mobileNumebr = dbh.ExecuteScalar(string.Format(@"SELECT   
                                                 TOP(1)[MobileNo]
                                              FROM[CRM].[dbo].[Failure117SMS]
                                              WHERE PhoneNo =  '{0}' 
                                              AND MobileNo IS NOT NULL
                                              AND MobileNo <> ''
                                              ORDER BY ID DESC ", PhoneNo));


                if (mobileNumebr == null)
                {
                    Enterprise.Logger.WriteInfo("IS   NULL ");
                    MessageBox.Show("هیچ شماره موبایلی برای تلفن مشترک ثبت نشده است");
                    return;
                }
                else
                {
                    MobileNo = mobileNumebr.ToString();
                    Enterprise.Logger.WriteInfo("IS NOT NULL ");
                }
                service.SendSMSV117(userName, passWord, MobileNo, Message);
                MessageBox.Show("پیامک با موفقیت ارسال شد");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString(), "خطا ");
            }
        }
    }
}
