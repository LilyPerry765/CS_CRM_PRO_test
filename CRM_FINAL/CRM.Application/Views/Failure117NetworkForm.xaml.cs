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
using Stimulsoft.Report;
using System.IO;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Views
{
    public partial class Failure117NetworkForm : Local.RequestFormBase
    {
        #region Properties

        private Request _Request = new Request();
        private Data.Failure117 _Failure117 = new Data.Failure117();
        private Data.FailureForm _Form = new FailureForm();
        private FailureFormInfo _FormInfo = new FailureFormInfo();
        private RequestInfo _RequestInfo = new RequestInfo();
        private byte _LineStatusTypeID = 0;
        private int _FailureStatusTypeID = 0;
        private int UserControlID = 0;
        public List<long> RequestIDs = new List<long>();

        private TelephoneInfoForRequest telephoneInfo;
        private TechnicalInfoFailure117 technicalInfo;

        private bool _IsSearch = false;

        #endregion

        #region Constructors

        public Failure117NetworkForm(long requestID)
        {
            RequestID = requestID;

            InitializeComponent();

            Initialize();
            LoadData();
        }

        public Failure117NetworkForm(List<long> requestIDs)
        {
            RequestIDs = requestIDs;

            InitializeComponent();

            Initialize();
            LoadData();
        }

        public Failure117NetworkForm(long requestID, bool isShearch)
        {
            _IsSearch = isShearch;
            RequestID = requestID;

            InitializeComponent();

            Initialize();
            LoadData();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            Color1ComboBox.ItemsSource = DB.GetAllEntity<CableColor>();
            Color2ComboBox.ItemsSource = DB.GetAllEntity<CableColor>();
            CableTypeComboBox.ItemsSource = DB.GetAllEntity<Failure117CableType>();
            NetworkOfficerComboBox.ItemsSource = Failure117NetworkContractorDB.GetContractorOfficerName();

            //  if (DB.City == "semnan")
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };

            //if (DB.City == "kermanshah")
            //{
            //    if (_IsSearch == false)
            //        ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
            //    else
            //        ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
            //}
        }

        public void LoadData()
        {
            try
            {

                if (DB.City == "semnan")
                {
                    _Request = Data.RequestDB.GetRequestByID(RequestID);
                    _RequestInfo = RequestDB.GetRequestInfoByID(RequestID);
                    _Failure117 = Failure117DB.GetFailureRequestByID(RequestID);
                    _FormInfo = Data.Failure117DB.GetFailureFormInfo(RequestID);

                    Service1 service = new Service1();
                    System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());

                    RequestNoTextBox.Text = _Request.ID.ToString();
                    CallingNoTextBox.Text = _Failure117.CallingNo.ToString();
                    TelephoneNoTextBox.Text = _Request.TelephoneNo.ToString();

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
                        MobileNoTextBox.Text = telephoneInfo.Rows[0]["MOBILE"].ToString();
                    }
                    catch (Exception ex)
                    {
                        MobileNoTextBox.Text = string.Empty;
                    }
                    try
                    {
                        CustomerNameTextBox.Text = telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                    }
                    catch (Exception ex)
                    {
                        CustomerNameTextBox.Text = string.Empty;
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
                    try
                    {
                        RadifBuchtTextBox.Text = telephoneInfo.Rows[0]["RADIF"].ToString();
                    }
                    catch (Exception ex)
                    {
                        RadifBuchtTextBox.Text = string.Empty;
                    }
                    try
                    {
                        TabagheBuchtTextBox.Text = telephoneInfo.Rows[0]["TABAGHE"].ToString();
                    }
                    catch (Exception ex)
                    {
                        TabagheBuchtTextBox.Text = string.Empty;
                    }
                    try
                    {
                        EtesaliBuchtTextBox.Text = telephoneInfo.Rows[0]["ETESALII"].ToString();
                    }
                    catch (Exception ex)
                    {
                        EtesaliBuchtTextBox.Text = string.Empty;
                    }

                    if (Convert.ToInt16(telephoneInfo.Rows[0]["PCM_STATUS"]) == 1)
                    {
                        PCMCheckBox.IsChecked = true;

                        System.Data.DataTable pCMInfo = service.GetPCMInformation("Admin", "alibaba123", _Request.TelephoneNo.ToString());

                        if (pCMInfo.Rows.Count != 0)
                        {
                            PCMTechnicalInfo.Visibility = Visibility.Visible;
                            try
                            {
                                PortPCMTextBox.Text = pCMInfo.Rows[0]["PORT"].ToString();
                            }
                            catch (Exception ex)
                            {
                                PortPCMTextBox.Text = string.Empty;
                            }
                            try
                            {
                                ModelPCMTextBox.Text = pCMInfo.Rows[0]["PCM_MARK_NAME"].ToString();
                            }
                            catch (Exception ex)
                            {
                                ModelPCMTextBox.Text = string.Empty;
                            }
                            try
                            {
                                TypePCMTextBox.Text = pCMInfo.Rows[0]["PCM_TYPE_NAME"].ToString();
                            }
                            catch (Exception ex)
                            {
                                TypePCMTextBox.Text = string.Empty;
                            }
                            try
                            {
                                RockPCMTextBox.Text = pCMInfo.Rows[0]["ROCK"].ToString();
                            }
                            catch (Exception ex)
                            {
                                RockPCMTextBox.Text = string.Empty;
                            }
                            try
                            {
                                ShelfPCMTextBox.Text = pCMInfo.Rows[0]["SHELF"].ToString();
                            }
                            catch (Exception ex)
                            {
                                ShelfPCMTextBox.Text = string.Empty;
                            }
                            try
                            {
                                CardPCMTextBox.Text = pCMInfo.Rows[0]["CARD"].ToString();
                            }
                            catch (Exception ex)
                            {
                                CardPCMTextBox.Text = string.Empty;
                            }
                            try
                            {
                                RadifInputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMI_RADIF"].ToString();
                            }
                            catch (Exception ex)
                            {
                                RadifInputBuchtTextBox.Text = string.Empty;
                            }
                            try
                            {
                                TabagheInputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMI_TABAGHE"].ToString();
                            }
                            catch (Exception ex)
                            {
                                TabagheInputBuchtTextBox.Text = string.Empty;
                            }
                            try
                            {
                                EtesaliInputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMI_ETESALI"].ToString();
                            }
                            catch (Exception ex)
                            {
                                EtesaliInputBuchtTextBox.Text = string.Empty;
                            }
                            try
                            {
                                RadifOutputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMO_RADIF"].ToString();
                            }
                            catch (Exception ex)
                            {
                                RadifOutputBuchtTextBox.Text = string.Empty;
                            }
                            try
                            {
                                TabagheOutputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMO_TABAGHE"].ToString();
                            }
                            catch (Exception ex)
                            {
                                TabagheOutputBuchtTextBox.Text = string.Empty;
                            }
                            try
                            {
                                EtesaliOutputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMO_ETESALI"].ToString();
                            }
                            catch (Exception ex)
                            {
                                EtesaliOutputBuchtTextBox.Text = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        PCMCheckBox.IsChecked = false;
                        PCMTechnicalInfo.Visibility = Visibility.Collapsed;
                    }

                    if (_Failure117.LineStatusID != null)
                    {
                        if (_Failure117.LineStatusID != 0)
                        {
                            Failure117LineStatus lineStatus = DB.SearchByPropertyName<Failure117LineStatus>("ID", (int)_Failure117.LineStatusID).SingleOrDefault();
                            LineStatusTextBox.Text = lineStatus.Title;
                            LineStatusTypeTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.Failure117LineStatus), lineStatus.Type);

                            if (string.Equals(lineStatus.Title, "همشنوایی"))
                            {
                                HearingTelephoneNoLabel.Visibility = Visibility.Visible;
                                HearingTelephoneNoTextBox.Text = _Failure117.HearingTelephoneNo.ToString();
                                HearingTelephoneNoPanel.Visibility = Visibility.Visible;
                            }
                        }
                    }

                    RequestDate1TextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                    if (_Failure117.MDFPesonnelID != null)
                        MDFUserTextBox.Text = DB.SearchByPropertyName<MDFPersonnel>("ID", _Failure117.MDFPesonnelID).SingleOrDefault().FirstName + " " + DB.SearchByPropertyName<MDFPersonnel>("ID", _Failure117.MDFPesonnelID).SingleOrDefault().LastName; //UserDB.GetUserFullName(_Failure117.MDFPesonnelID);
                    MDFDateTextBox.Text = Helper.GetPersianDate(_Failure117.MDFDate, Helper.DateStringType.DateTime);
                    AdjacentTelephoneNoTextBox.Text = _Failure117.AdjacentTelephoneNo.ToString();

                    double compareResult = ((DateTime)_Failure117.MDFDate - _Request.InsertDate).TotalMinutes;
                    double hour = (compareResult < 60) ? 0 : Math.Round(compareResult / 60);
                    double min = Math.Round(compareResult % 60, 2);
                    MDFSpeedMinTextBox.Text = string.Format("{0} : {1}", (min >= 10) ? min.ToString() : "0" + min.ToString(), (hour >= 10) ? hour.ToString() : "0" + hour.ToString());
                    MDFCommnetTextBox.Text = _Failure117.MDFCommnet;

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

                    if (_FormInfo != null)
                    {
                        this.DataContext = _FormInfo;

                        UIElement container = NetworkInfo as UIElement;
                        List<System.Windows.Controls.Control> controlsList = Helper.FindVisualChildren<System.Windows.Controls.Control>(container).ToList();

                        foreach (System.Windows.Controls.Control control in controlsList.Where(t => t.GetType() == typeof(RadioButton)).ToList())
                        {
                            RadioButton currentControl = control as RadioButton;
                            if (currentControl.GroupName == "FailureStatus")
                            {
                                if (_FormInfo.FailureStatusID == Convert.ToInt32(currentControl.Tag))
                                    currentControl.IsChecked = true;
                            }
                        }

                        foreach (System.Windows.Controls.Control control in controlsList.Where(t => t.GetType() == typeof(RadioButton)).ToList())
                        {
                            RadioButton currentControl = control as RadioButton;
                            if (currentControl.GroupName == "FailureSpeed")
                            {
                                if (_FormInfo.FailureSpeed == Convert.ToInt32(currentControl.Tag))
                                    currentControl.IsChecked = true;
                            }
                        }
                    }

                    switch (_RequestInfo.StepID)
                    {
                        case (int)DB.RequestStepFailure117.MDFConfirm:
                        case (int)DB.RequestStepFailure117.Archived:
                            UIElement container = NetworkInfo as UIElement;
                            List<System.Windows.Controls.Control> controlsList = Helper.FindVisualChildren<System.Windows.Controls.Control>(container).ToList();

                            foreach (System.Windows.Controls.Control control in controlsList.Where(t => t.GetType() == typeof(RadioButton) || t.GetType() == typeof(TextBox) || t.GetType() == typeof(ComboBox) || t.GetType() == typeof(ListBox)).ToList())
                            {
                                if (control as RadioButton != null || control as ComboBox != null || control as ListBox != null)
                                    control.IsEnabled = false;
                                if (control as TextBox != null)
                                {
                                    TextBox currentControl = control as TextBox;
                                    currentControl.IsReadOnly = true;
                                }
                            }

                            GetNetworkFormDate.IsEnabled = false;
                            GiveNetworkFormDate.IsEnabled = false;
                            SendToCabelDate.IsEnabled = false;
                            SendToCabelTimeTextBox.IsReadOnly = true;
                            CabelDate.IsEnabled = false;
                            CabelTimeTextBox.IsReadOnly = true;

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };

                            break;
                    }

                    ResizeWindow();
                }

                if (DB.City == "kermanshah")
                {
                    if (_IsSearch == true)
                    {
                        GiveNetworkFormDateImage.IsEnabled = false;
                        GiveNetworkFormDate.IsEnabled = false;
                        GiveNetworkFormTimeImage.IsEnabled = false;
                        GiveNetworkFormTimeTextBox.IsEnabled = false;
                    }

                    SendToCabelDateGrid.IsEnabled = false;
                    SendToCabelTimeGrid.IsEnabled = false;
                    CabelDateGrid.IsEnabled = false;
                    CabelTimeGrid.IsEnabled = false;

                    if (RequestID != null && RequestID != 0)
                    {
                        NooriWLLLabel.Content = "کافو نوری / WLL : ";
                        CheckBox27.Visibility = Visibility.Visible;
                        CheckBox28.Visibility = Visibility.Visible;
                        CheckBox29.Visibility = Visibility.Visible;

                        _Request = Data.RequestDB.GetRequestByID(RequestID);
                        _RequestInfo = RequestDB.GetRequestInfoByID(RequestID);
                        _Failure117 = Failure117DB.GetFailureRequestByID(RequestID);
                        _FormInfo = Data.Failure117DB.GetFailureFormInfo(RequestID);

                        RequestNoTextBox.Text = _Request.ID.ToString();
                        CallingNoTextBox.Text = _Failure117.CallingNo.ToString();
                        TelephoneNoTextBox.Text = _Request.TelephoneNo.ToString();

                        telephoneInfo = TelephoneDB.GetTelephoneInfoForFailure((long)_Request.TelephoneNo);

                        if (telephoneInfo != null)
                        {
                            CenterTextBox.Text = telephoneInfo.Center;
                            PostalCodeTextBox.Text = telephoneInfo.PostalCode;
                            AddressTextBox.Text = telephoneInfo.Address;
                            MobileNoTextBox.Text = telephoneInfo.Mobile;
                            CustomerNameTextBox.Text = telephoneInfo.FirstName + " " + telephoneInfo.LastName;
                        }

                        technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo((long)_Request.TelephoneNo);
                        if (technicalInfo != null)
                        {
                            CabinetNoTextBox.Text = technicalInfo.CabinetNo;
                            CabinetinputNoTextBox.Text = technicalInfo.CabinetInputNumber;
                            PostNoTextBox.Text = technicalInfo.PostNo;
                            PostEtesaliNoTextBox.Text = technicalInfo.ConnectionNo;
                            RadifBuchtTextBox.Text = technicalInfo.RADIF;
                            TabagheBuchtTextBox.Text = technicalInfo.TABAGHE;
                            EtesaliBuchtTextBox.Text = technicalInfo.ETESALII;

                            if (technicalInfo.IsPCM)
                            {
                                PCMCheckBox.IsChecked = true;

                                PCMTechnicalInfo.Visibility = Visibility.Visible;

                                PortPCMTextBox.Text = technicalInfo.PCMPort;
                                ModelPCMTextBox.Text = technicalInfo.PCMModel;
                                TypePCMTextBox.Text = technicalInfo.PCMType;
                                RockPCMTextBox.Text = technicalInfo.PCMRock;
                                ShelfPCMTextBox.Text = technicalInfo.PCMShelf;
                                CardPCMTextBox.Text = technicalInfo.PCMCard;
                                RadifInputBuchtTextBox.Text = technicalInfo.PCMInRadif;
                                TabagheInputBuchtTextBox.Text = technicalInfo.PCMInTabaghe;
                                EtesaliInputBuchtTextBox.Text = technicalInfo.PCMInEtesali;
                                RadifOutputBuchtTextBox.Text = technicalInfo.PCMOutRadif;
                                TabagheOutputBuchtTextBox.Text = technicalInfo.PCMOutTabaghe;
                                EtesaliOutputBuchtTextBox.Text = technicalInfo.PCMOutEtesali;
                            }
                            else
                            {
                                PCMCheckBox.IsChecked = false;
                                PCMTechnicalInfo.Visibility = Visibility.Collapsed;
                            }

                            TechnicalInfoFailure117 aDSLInfo = Failure117DB.ADSLPAPbyTelephoneNo((long)_Request.TelephoneNo);

                            if (aDSLInfo != null)
                            {
                                ADSLTechnicalInfo.Visibility = Visibility.Visible;

                                RadifADSLTextBox.Text = aDSLInfo.ADSLRadif;
                                TabagheADSLTextBox.Text = aDSLInfo.ADSLTabaghe;
                                EtesaliADSLTextBox.Text = aDSLInfo.ADSLEtesali;
                            }

                            Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo((long)_Request.TelephoneNo);
                            if (telephone != null)
                            {
                                if (telephone.UsageType != null)
                                {
                                    if (telephone.UsageType == (byte)DB.TelephoneUsageType.PrivateWire)
                                    {
                                        SpecialWireTechnicalInfo.Visibility = Visibility.Visible;

                                        TechnicalInfoFailure117 specilaInfo = Failure117DB.GetSpecilaWireBuchtbySwitchPortID(technicalInfo.SwitchPortID);

                                        if (specilaInfo != null)
                                        {
                                            RadifSpecialTextBox.Text = specilaInfo.RADIF;
                                            TabagheSpecialTextBox.Text = specilaInfo.TABAGHE;
                                            EtesaliSpecialTextBox.Text = specilaInfo.ETESALII;
                                        }
                                    }
                                    if (telephone.UsageType == (byte)DB.TelephoneUsageType.E1)
                                    {
                                        E1TechnicalInfo.Visibility = Visibility.Visible;

                                        TechnicalInfoFailure117 E1Info = Failure117DB.GetSpecilaWireBuchtbySwitchPortID(technicalInfo.SwitchPortID);

                                        if (E1Info != null)
                                        {
                                            RadifE1TextBox.Text = E1Info.RADIF;
                                            TabagheE1TextBox.Text = E1Info.TABAGHE;
                                            EtesaliE1TextBox.Text = E1Info.ETESALII;
                                        }
                                    }
                                }
                            }
                        }

                        if (_Failure117.LineStatusID != null)
                        {
                            if (_Failure117.LineStatusID != 0)
                            {
                                Failure117LineStatus lineStatus = DB.SearchByPropertyName<Failure117LineStatus>("ID", (int)_Failure117.LineStatusID).SingleOrDefault();
                                LineStatusTextBox.Text = lineStatus.Title;
                                LineStatusTypeTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.Failure117LineStatus), lineStatus.Type);

                                if (string.Equals(lineStatus.Title, "همشنوایی"))
                                {
                                    HearingTelephoneNoLabel.Visibility = Visibility.Visible;
                                    HearingTelephoneNoTextBox.Text = _Failure117.HearingTelephoneNo.ToString();
                                    HearingTelephoneNoPanel.Visibility = Visibility.Visible;
                                }
                            }
                        }

                        RequestDate1TextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                        if (_Failure117.MDFPesonnelID != null)
                            MDFUserTextBox.Text = UserDB.GetUserFullName(_Failure117.MDFPesonnelID);
                        MDFDateTextBox.Text = Helper.GetPersianDate(_Failure117.MDFDate, Helper.DateStringType.DateTime);
                        AdjacentTelephoneNoTextBox.Text = _Failure117.AdjacentTelephoneNo.ToString();

                        double compareResult = ((DateTime)_Failure117.MDFDate - _Request.InsertDate).TotalMinutes;
                        double hour = (compareResult < 60) ? 0 : Math.Round(compareResult / 60);
                        double min = Math.Round(compareResult % 60, 2);
                        MDFSpeedMinTextBox.Text = string.Format("{0} : {1}", (min >= 10) ? min.ToString() : "0" + min.ToString(), (hour >= 10) ? hour.ToString() : "0" + hour.ToString());
                        MDFCommnetTextBox.Text = _Failure117.MDFCommnet;

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

                        if (_FormInfo != null)
                        {
                            this.DataContext = _FormInfo;

                            UIElement container = NetworkInfo as UIElement;
                            List<System.Windows.Controls.Control> controlsList = Helper.FindVisualChildren<System.Windows.Controls.Control>(container).ToList();

                            foreach (System.Windows.Controls.Control control in controlsList.Where(t => t.GetType() == typeof(RadioButton)).ToList())
                            {
                                RadioButton currentControl = control as RadioButton;
                                if (currentControl.GroupName == "FailureStatus")
                                {
                                    if (_FormInfo.FailureStatusID == Convert.ToInt32(currentControl.Tag))
                                        currentControl.IsChecked = true;
                                }
                            }

                            foreach (System.Windows.Controls.Control control in controlsList.Where(t => t.GetType() == typeof(RadioButton)).ToList())
                            {
                                RadioButton currentControl = control as RadioButton;
                                if (currentControl.GroupName == "FailureSpeed")
                                {
                                    if (_FormInfo.FailureSpeed == Convert.ToInt32(currentControl.Tag))
                                        currentControl.IsChecked = true;
                                }
                            }
                        }

                        switch (_RequestInfo.StepID)
                        {
                            case (int)DB.RequestStepFailure117.MDFConfirm:
                            case (int)DB.RequestStepFailure117.Archived:
                                UIElement container = NetworkInfo as UIElement;
                                List<System.Windows.Controls.Control> controlsList = Helper.FindVisualChildren<System.Windows.Controls.Control>(container).ToList();

                                foreach (System.Windows.Controls.Control control in controlsList.Where(t => t.GetType() == typeof(RadioButton) || t.GetType() == typeof(TextBox) || t.GetType() == typeof(ComboBox) || t.GetType() == typeof(ListBox)).ToList())
                                {
                                    if (/*control as RadioButton != null ||*/ control as ComboBox != null || control as ListBox != null)
                                        control.IsEnabled = false;
                                    if (control as TextBox != null)
                                    {
                                        TextBox currentControl = control as TextBox;
                                        currentControl.IsReadOnly = true;
                                    }
                                }

                                GetNetworkFormTimeTextBox.IsReadOnly = false;
                                GiveNetworkFormTimeTextBox.IsReadOnly = false;
                                //GetNetworkFormDate.IsEnabled = false;
                                //GiveNetworkFormDate.IsEnabled = false;
                                SendToCabelDate.IsEnabled = false;
                                SendToCabelTimeTextBox.IsReadOnly = true;
                                CabelDate.IsEnabled = false;
                                CabelTimeTextBox.IsReadOnly = true;

                                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };

                                break;
                        }

                        ResizeWindow();
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در نمایش اطلاعات، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در نمایش اطلاعات، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در نمایش اطلاعات، " + ex.Message + " !", ex);
            }
        }

        public override bool Save()
        {
            try
            {
                _Form = DB.SearchByPropertyName<FailureForm>("FailureRequestID", RequestID).SingleOrDefault();

                if (_Form == null)
                    _Form = new FailureForm();

                _Form.FailureRequestID = RequestID;

                if (Color1ComboBox.SelectedValue != null)
                    _Form.CableColor1 = (byte)Convert.ToInt16(Color1ComboBox.SelectedValue);
                else
                    _Form.CableColor1 = null;
                if (Color2ComboBox.SelectedValue != null)
                    _Form.CableColor2 = (byte)Convert.ToInt16(Color2ComboBox.SelectedValue);
                else
                    _Form.CableColor2 = null;
                _Form.CableTypeID = (byte)Convert.ToInt16(CableTypeComboBox.SelectedValue);
                if (NetworkOfficerComboBox.SelectedValue != null)
                    _Form.Failure117NetworkContractorOfficerID = (int)NetworkOfficerComboBox.SelectedValue;

                _Form.GiveNetworkFormTime = GiveNetworkFormTimeTextBox.Text;
                _Form.GetNetworkFormTime = GetNetworkFormTimeTextBox.Text;

                if (GiveNetworkFormDate.SelectedDate > DB.GetServerDate())
                    throw new Exception("تاریخ تحویل فرم به مامور صحیح نمی باشد");

                if (!string.IsNullOrEmpty(GiveNetworkFormTimeTextBox.Text))
                {
                    if (GiveNetworkFormDate.SelectedDate == null)
                        throw new Exception("لطفا قبل از تعیین ساعت تحویل فرم، تاریخ را تعیین نمایید");
                    else
                    {
                        string[] giveTime = GiveNetworkFormTimeTextBox.Text.Trim().Split(':');
                        if (giveTime.Count() == 2)
                        {
                            int value;
                            if (int.TryParse(giveTime[0], out value))
                            {
                                if (value >= 24)
                                    throw new Exception("ساعت تحویل فرم معتبر نمی باشد");

                                if (int.TryParse(giveTime[1], out value))
                                {
                                    if (value >= 60)
                                        throw new Exception("ساعت تحویل فرم معتبر نمی باشد");
                                    else
                                        _Form.GiveNetworkFormDate = new DateTime(GiveNetworkFormDate.SelectedDate.Value.Year, GiveNetworkFormDate.SelectedDate.Value.Month, GiveNetworkFormDate.SelectedDate.Value.Day, Convert.ToInt32(giveTime[0]), Convert.ToInt32(giveTime[1]), 0);
                                }
                                else
                                    throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                            }
                            else
                                throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                        }
                        else
                            throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                    }
                }
                else
                    _Form.GiveNetworkFormDate = GiveNetworkFormDate.SelectedDate;

                if (GetNetworkFormDate.SelectedDate > DB.GetServerDate())
                    throw new Exception("تاریخ بازگشت فرم صحیح نمی باشد");

                if (!string.IsNullOrEmpty(GetNetworkFormTimeTextBox.Text))
                {
                    if (GetNetworkFormDate.SelectedDate == null)
                        throw new Exception("لطفا قبل از تعیین ساعت بازگشت فرم، تاریخ را تعیین نمایید");
                    else
                    {
                        string[] getTime = GetNetworkFormTimeTextBox.Text.Trim().Split(':');
                        if (getTime.Count() == 2)
                        {
                            int value;
                            if (int.TryParse(getTime[0], out value))
                            {
                                if (value >= 24)
                                    throw new Exception("ساعت بازگشت فرم معتبر نمی باشد");

                                if (int.TryParse(getTime[1], out value))
                                {
                                    if (value >= 60)
                                        throw new Exception("ساعت بازگشت فرم معتبر نمی باشد");
                                    else
                                        _Form.GetNetworkFormDate = new DateTime(GetNetworkFormDate.SelectedDate.Value.Year, GetNetworkFormDate.SelectedDate.Value.Month, GetNetworkFormDate.SelectedDate.Value.Day, Convert.ToInt32(getTime[0]), Convert.ToInt32(getTime[1]), 0);
                                }
                                else
                                    throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                            }
                            else
                                throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                        }
                        else
                            throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                    }
                }
                else
                    _Form.GetNetworkFormDate = GetNetworkFormDate.SelectedDate;

                if (_Form.GetNetworkFormDate < _Form.GiveNetworkFormDate)
                    throw new Exception("تاریخ برگشت مامور قبل از تاریخ تحویل فرم به وی می باشد");

                _Form.SendToCabelTime = SendToCabelTimeTextBox.Text;
                _Form.CabelTime = CabelTimeTextBox.Text;

                if (SendToCabelDate.SelectedDate > DB.GetServerDate())
                    throw new Exception("تاریخ ارجاع به کابل صحیح نمی باشد");

                if (!string.IsNullOrEmpty(SendToCabelTimeTextBox.Text))
                {
                    if (SendToCabelDate.SelectedDate == null)
                        throw new Exception("لطفا قبل از تعیین ساعت ارجاع به کابل، تاریخ را تعیین نمایید");
                    else
                    {
                        string[] sendTime = SendToCabelTimeTextBox.Text.Trim().Split(':');
                        if (sendTime.Count() == 2)
                        {
                            int value;
                            if (int.TryParse(sendTime[0], out value))
                            {
                                if (value >= 24)
                                    throw new Exception("ساعت ارجاع به کابل معتبر نمی باشد");

                                if (int.TryParse(sendTime[1], out value))
                                {
                                    if (value >= 60)
                                        throw new Exception("ساعت ارجاع به کابل معتبر نمی باشد");
                                    else
                                        _Form.SendToCabelDate = new DateTime(SendToCabelDate.SelectedDate.Value.Year, SendToCabelDate.SelectedDate.Value.Month, SendToCabelDate.SelectedDate.Value.Day, Convert.ToInt32(sendTime[0]), Convert.ToInt32(sendTime[1]), 0);
                                }
                                else
                                    throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                            }
                            else
                                throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                        }
                        else
                            throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                    }
                }
                else
                    _Form.SendToCabelDate = SendToCabelDate.SelectedDate;

                if (CabelDate.SelectedDate > DB.GetServerDate())
                    throw new Exception("تاریخ اصلاح در کابل صحیح نمی باشد");

                if (!string.IsNullOrEmpty(CabelTimeTextBox.Text))
                {
                    if (CabelDate.SelectedDate == null)
                        throw new Exception("لطفا قبل از تعیین ساعت اصلاح در کابل، تاریخ را تعیین نمایید");
                    else
                    {
                        string[] cabelTime = CabelTimeTextBox.Text.Trim().Split(':');
                        if (cabelTime.Count() == 2)
                        {
                            int value;
                            if (int.TryParse(cabelTime[0], out value))
                            {
                                if (value >= 24)
                                    throw new Exception("ساعت اصلاح در کابل معتبر نمی باشد");

                                if (int.TryParse(cabelTime[1], out value))
                                {
                                    if (value >= 60)
                                        throw new Exception("ساعت اصلاح در کابل معتبر نمی باشد");
                                    else
                                        _Form.CabelDate = new DateTime(CabelDate.SelectedDate.Value.Year, CabelDate.SelectedDate.Value.Month, CabelDate.SelectedDate.Value.Day, Convert.ToInt32(cabelTime[0]), Convert.ToInt32(cabelTime[1]), 0);
                                }
                                else
                                    throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                            }
                            else
                                throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                        }
                        else
                            throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                    }
                }
                else
                    _Form.CabelDate = CabelDate.SelectedDate;

                if (_Form.CabelDate < _Form.SendToCabelDate)
                    throw new Exception("تاریخ اصلاح در کابل قبل از تاریخ ارجاع به کابل می باشد");

                _Form.Description = NetworkCommentTextBox.Text;

                _Failure117.NetworkComment = NetworkCommentTextBox.Text;

                UIElement container = this.Content as UIElement;
                List<System.Windows.Controls.Control> controlsList = Helper.FindVisualChildren<System.Windows.Controls.Control>(container).ToList();

                foreach (System.Windows.Controls.Control control in controlsList.Where(t => t.GetType() == typeof(RadioButton)).ToList())
                {
                    RadioButton currentControl = control as RadioButton;
                    if (currentControl.GroupName == "FailureStatus")
                    {
                        if ((bool)currentControl.IsChecked)
                        {
                            _Form.FailureStatusID = Convert.ToInt32(currentControl.Tag);
                            _Failure117.FailureStatusID = Convert.ToInt32(currentControl.Tag);
                        }
                    }
                }

                //if (FailureSpeedComboBox.SelectedValue == null)
                //    _Form.FailureSpeed = null;
                //else
                //    _Form.FailureSpeed = (byte)Convert.ToInt16(FailureSpeedComboBox.SelectedValue);

                if (_Form.ID != 0)
                    RequestForFailure117.SaveFailureForm(_Form, false);
                else
                    RequestForFailure117.SaveFailureForm(_Form, true);

                RequestForFailure117.SaveFailureActions(_Failure117);

                ShowSuccessMessage("ذخیره با موفقیت انجام شد.");

            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره، " + ex.Message + "!", ex);
            }

            return IsSaveSuccess;
        }

        public override bool Print()
        {
            Save();

            if (RepeatCheckBox.IsChecked == null || (bool)RepeatCheckBox.IsChecked == false)
            {
                FailureForm form = Failure117DB.GetFailureForm(_Request.ID);

                form.GiveNetworkFormDate = DB.GetServerDate();
                form.GiveNetworkFormTime = form.GiveNetworkFormDate.Value.Hour.ToString() + ":" + form.GiveNetworkFormDate.Value.Minute.ToString();

                form.Detach();
                DB.Save(form);

                GiveNetworkFormDate.SelectedDate = form.GiveNetworkFormDate;
                GiveNetworkFormTimeTextBox.Text = form.GiveNetworkFormTime;
            }

            Failure117NetworkReport RecordTemp = new Failure117NetworkReport();
            List<Failure117NetworkReport> result = new List<Failure117NetworkReport>();
            RecordTemp.MobileNo = MobileNoTextBox.Text;
            RecordTemp.CallingNo = CallingNoTextBox.Text;
            RecordTemp.Address = AddressTextBox.Text;
            RecordTemp.Radif = RowTextBox.Text;
            RecordTemp.CabinetinputNo = CabinetinputNoTextBox.Text;
            RecordTemp.CabinetNo = CabinetNoTextBox.Text;
            RecordTemp.CardPCM = CardPCMTextBox.Text;
            RecordTemp.Center = CenterTextBox.Text;
            RecordTemp.CustomerName = CustomerNameTextBox.Text;
            RecordTemp.EtesaliBucht = EtesaliBuchtTextBox.Text;
            RecordTemp.EtesaliInputBucht = EtesaliInputBuchtTextBox.Text;
            RecordTemp.EtesaliOutputBucht = EtesaliOutputBuchtTextBox.Text;
            RecordTemp.MDFDate = MDFDateTextBox.Text;
            RecordTemp.MDFUser = MDFUserTextBox.Text;
            RecordTemp.ModelPCM = ModelPCMTextBox.Text;
            RecordTemp.PhoneNo = TelephoneNoTextBox.Text;
            RecordTemp.PortPCM = PortPCMTextBox.Text;
            RecordTemp.PostalCode = PostalCodeTextBox.Text;
            RecordTemp.PostEtesaliNo = PostEtesaliNoTextBox.Text;
            RecordTemp.PostNo = PostNoTextBox.Text;
            RecordTemp.RadifBucht = RadifBuchtTextBox.Text;
            RecordTemp.RadifInputBucht = RadifInputBuchtTextBox.Text;
            RecordTemp.RadifOutputBucht = RadifOutputBuchtTextBox.Text;
            //RecordTemp.RequestDate = RequestDateTextBox.Text;
            RecordTemp.RequestDate1 = RequestDate1TextBox.Text;
            RecordTemp.RequestNo = long.Parse(RequestNoTextBox.Text);
            RecordTemp.RockPCM = RockPCMTextBox.Text;
            RecordTemp.ShelfPCM = ShelfPCMTextBox.Text;
            RecordTemp.TabagheBucht = TabagheBuchtTextBox.Text;
            RecordTemp.TabagheInputBucht = TabagheInputBuchtTextBox.Text;
            RecordTemp.TabagheOutputBucht = TabagheOutputBuchtTextBox.Text;
            RecordTemp.TypePCM = TypePCMTextBox.Text;
            RecordTemp.LineStatus = LineStatusTextBox.Text + "_" + LineStatusTypeTextBox.Text;
            RecordTemp.IsPCM = ((bool)PCMCheckBox.IsChecked) ? "دارد" : "ندارد";
            RecordTemp.ColorCable = Color1ComboBox.Text + " - " + Color2ComboBox.Text;
            RecordTemp.CableType = CableTypeComboBox.Text;
            RecordTemp.Description = NetworkCommentTextBox.Text;
            RecordTemp.AdjacentTelephoneNo = AdjacentTelephoneNoTextBox.Text;
            RecordTemp.EndMDFComment = MDFCommnetTextBox.Text;
            RecordTemp.SendToCabelDate = Helper.GetPersianDate(SendToCabelDate.SelectedDate, Helper.DateStringType.Short);
            RecordTemp.CabelDate = Helper.GetPersianDate(CabelDate.SelectedDate, Helper.DateStringType.Short);
            RecordTemp.MDFSpeedMin = MDFSpeedMinTextBox.Text;
            RecordTemp.NetworkOfficer = NetworkOfficerComboBox.Text;
            RecordTemp.GiveNetworkFormDate = Helper.GetPersianDate(GiveNetworkFormDate.SelectedDate, Helper.DateStringType.Short);
            RecordTemp.GiveNetworkFormTime = GiveNetworkFormTimeTextBox.Text;
            RecordTemp.GetNetworkFormDate = Helper.GetPersianDate(GetNetworkFormDate.SelectedDate, Helper.DateStringType.Short);
            RecordTemp.GetNetworkFormTime = GetNetworkFormTimeTextBox.Text;
            RecordTemp.ADSLRadif = RadifADSLTextBox.Text;
            RecordTemp.ADSLTabaghe = TabagheADSLTextBox.Text;
            RecordTemp.ADSLEtesali = EtesaliADSLTextBox.Text;
            RecordTemp.SpecialRadif = RadifSpecialTextBox.Text;
            RecordTemp.SpecialTabaghe = TabagheSpecialTextBox.Text;
            RecordTemp.SpecialEtesali = EtesaliSpecialTextBox.Text;
            RecordTemp.E1Radif = RadifE1TextBox.Text;
            RecordTemp.E1Tabaghe = TabagheE1TextBox.Text;
            RecordTemp.E1Etesali = EtesaliE1TextBox.Text;
            //RecordTemp.ResultAfterReturn = (resultListBox.SelectedValue != null) ? ((ListBoxItem)resultListBox.SelectedItem).Content.ToString() : "";

            RecordTemp.CheckBox1 = ((bool)CheckBox1.IsChecked) ? "*" : "";
            RecordTemp.CheckBox2 = ((bool)CheckBox2.IsChecked) ? "*" : "";
            RecordTemp.CheckBox3 = ((bool)CheckBox3.IsChecked) ? "*" : "";
            RecordTemp.CheckBox4 = ((bool)CheckBox4.IsChecked) ? "*" : "";
            RecordTemp.CheckBox5 = ((bool)CheckBox5.IsChecked) ? "*" : "";
            RecordTemp.CheckBox6 = ((bool)CheckBox6.IsChecked) ? "*" : "";
            RecordTemp.CheckBox7 = ((bool)CheckBox7.IsChecked) ? "*" : "";
            RecordTemp.CheckBox8 = ((bool)CheckBox8.IsChecked) ? "*" : "";
            RecordTemp.CheckBox9 = ((bool)CheckBox9.IsChecked) ? "*" : "";
            RecordTemp.CheckBox10 = ((bool)CheckBox10.IsChecked) ? "*" : "";

            RecordTemp.CheckBox11 = ((bool)CheckBox11.IsChecked) ? "*" : "";
            RecordTemp.CheckBox12 = ((bool)CheckBox12.IsChecked) ? "*" : "";
            RecordTemp.CheckBox13 = ((bool)CheckBox13.IsChecked) ? "*" : "";
            RecordTemp.CheckBox14 = ((bool)CheckBox14.IsChecked) ? "*" : "";
            RecordTemp.CheckBox15 = ((bool)CheckBox15.IsChecked) ? "*" : "";
            RecordTemp.CheckBox16 = ((bool)CheckBox16.IsChecked) ? "*" : "";
            RecordTemp.CheckBox17 = ((bool)CheckBox17.IsChecked) ? "*" : "";
            RecordTemp.CheckBox18 = ((bool)CheckBox18.IsChecked) ? "*" : "";
            RecordTemp.CheckBox19 = ((bool)CheckBox19.IsChecked) ? "*" : "";
            RecordTemp.CheckBox20 = ((bool)CheckBox20.IsChecked) ? "*" : "";

            RecordTemp.CheckBox21 = ((bool)CheckBox21.IsChecked) ? "*" : "";
            RecordTemp.CheckBox22 = ((bool)CheckBox22.IsChecked) ? "*" : "";
            RecordTemp.CheckBox23 = ((bool)CheckBox23.IsChecked) ? "*" : "";
            RecordTemp.CheckBox24 = ((bool)CheckBox24.IsChecked) ? "*" : "";
            RecordTemp.CheckBox25 = ((bool)CheckBox25.IsChecked) ? "*" : "";

            result.Add(RecordTemp);
            List<FailureHistoryInfo> historyListTemp = new List<FailureHistoryInfo>();
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

            historyList1 = historyList1.OrderByDescending(t => t.ID).Take(2).ToList();
            RecordTemp.Rowno = Failure117DB.GetFailureFormInfo(RequestID).RowNo.ToString();
            historyListTemp.AddRange(historyList1);

            string title = string.Empty;
            string path;

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath((int)DB.UserControlNames.Failure117Network);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

            stiReport.RegData("xx", "xx", result);
            stiReport.RegData("result", "historyList", historyListTemp);
            stiReport.CacheAllData = true;


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

            return IsPrintSuccess;
        }

        public override bool Forward()
        {
            try
            {
                _Form = Failure117DB.GetFailureForm(RequestID);// DB.SearchByPropertyName<FailureForm>("FailureRequestID", RequestID).SingleOrDefault();

                if (_Form == null)
                    _Form = new FailureForm();

                _Form.FailureRequestID = RequestID;

                if (Color1ComboBox.SelectedValue != null)
                    _Form.CableColor1 = (byte)Convert.ToInt16(Color1ComboBox.SelectedValue);
                else
                    _Form.CableColor1 = null;
                if (Color2ComboBox.SelectedValue != null)
                    _Form.CableColor2 = (byte)Convert.ToInt16(Color2ComboBox.SelectedValue);
                else
                    _Form.CableColor2 = null;
                _Form.CableTypeID = (byte)Convert.ToInt16(CableTypeComboBox.SelectedValue);
                if (NetworkOfficerComboBox.SelectedValue != null)
                    _Form.Failure117NetworkContractorOfficerID = (int)NetworkOfficerComboBox.SelectedValue;
                _Form.GiveNetworkFormTime = GiveNetworkFormTimeTextBox.Text;
                _Form.GetNetworkFormTime = GetNetworkFormTimeTextBox.Text;

                if (GiveNetworkFormDate.SelectedDate > DB.GetServerDate())
                    throw new Exception("تاریخ تحویل فرم به مامور صحیح نمی باشد");

                if (!string.IsNullOrEmpty(GiveNetworkFormTimeTextBox.Text))
                {
                    if (GiveNetworkFormDate.SelectedDate == null)
                        throw new Exception("لطفا قبل از تعیین ساعت، تاریخ را تعیین نمایید");
                    else
                    {
                        string[] giveTime = GiveNetworkFormTimeTextBox.Text.Trim().Split(':');
                        if (giveTime.Count() == 2)
                        {
                            int value;
                            if (int.TryParse(giveTime[0], out value))
                            {
                                if (value >= 24)
                                    throw new Exception("ساعت تحویل فرم معتبر نمی باشد");

                                if (int.TryParse(giveTime[1], out value))
                                {
                                    if (value >= 60)
                                        throw new Exception("ساعت تحویل فرم معتبر نمی باشد");
                                    else
                                        _Form.GiveNetworkFormDate = new DateTime(GiveNetworkFormDate.SelectedDate.Value.Year, GiveNetworkFormDate.SelectedDate.Value.Month, GiveNetworkFormDate.SelectedDate.Value.Day, Convert.ToInt32(giveTime[0]), Convert.ToInt32(giveTime[1]), 0);
                                }
                                else
                                    throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                            }
                            else
                                throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                        }
                        else
                            throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                    }
                }
                else
                    _Form.GiveNetworkFormDate = GiveNetworkFormDate.SelectedDate;

                if (GetNetworkFormDate.SelectedDate > DB.GetServerDate())
                    throw new Exception("تاریخ بازگشت فرم صحیح نمی باشد");

                if (!string.IsNullOrEmpty(GetNetworkFormTimeTextBox.Text))
                {
                    if (GetNetworkFormDate.SelectedDate == null)
                        throw new Exception("لطفا قبل از تعیین ساعت، تاریخ را تعیین نمایید");
                    else
                    {
                        string[] getTime = GetNetworkFormTimeTextBox.Text.Trim().Split(':');
                        if (getTime.Count() == 2)
                        {
                            int value;
                            if (int.TryParse(getTime[0], out value))
                            {
                                if (value >= 24)
                                    throw new Exception("ساعت بازگشت فرم معتبر نمی باشد");

                                if (int.TryParse(getTime[1], out value))
                                {
                                    if (value >= 60)
                                        throw new Exception("ساعت بازگشت فرم معتبر نمی باشد");
                                    else
                                        _Form.GetNetworkFormDate = new DateTime(GetNetworkFormDate.SelectedDate.Value.Year, GetNetworkFormDate.SelectedDate.Value.Month, GetNetworkFormDate.SelectedDate.Value.Day, Convert.ToInt32(getTime[0]), Convert.ToInt32(getTime[1]), 0);
                                }
                                else
                                    throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                            }
                            else
                                throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                        }
                        else
                            throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                    }
                }
                else
                    _Form.GetNetworkFormDate = GetNetworkFormDate.SelectedDate;

                if (_Form.GetNetworkFormDate < _Form.GiveNetworkFormDate)
                    throw new Exception("تاریخ برگشت مامور قبل از تاریخ تحویل فرم به وی می باشد");

                _Form.SendToCabelTime = SendToCabelTimeTextBox.Text;
                _Form.CabelTime = CabelTimeTextBox.Text;

                if (SendToCabelDate.SelectedDate > DB.GetServerDate())
                    throw new Exception("تاریخ ارجاع به کابل صحیح نمی باشد");

                if (!string.IsNullOrEmpty(SendToCabelTimeTextBox.Text))
                {
                    if (SendToCabelDate.SelectedDate == null)
                        throw new Exception("لطفا قبل از تعیین ساعت ارجاع به کابل، تاریخ را تعیین نمایید");
                    else
                    {
                        string[] sendTime = SendToCabelTimeTextBox.Text.Trim().Split(':');
                        if (sendTime.Count() == 2)
                        {
                            int value;
                            if (int.TryParse(sendTime[0], out value))
                            {
                                if (value >= 24)
                                    throw new Exception("ساعت ارجاع به کابل معتبر نمی باشد");

                                if (int.TryParse(sendTime[1], out value))
                                {
                                    if (value >= 60)
                                        throw new Exception("ساعت ارجاع به کابل معتبر نمی باشد");
                                    else
                                        _Form.SendToCabelDate = new DateTime(SendToCabelDate.SelectedDate.Value.Year, SendToCabelDate.SelectedDate.Value.Month, SendToCabelDate.SelectedDate.Value.Day, Convert.ToInt32(sendTime[0]), Convert.ToInt32(sendTime[1]), 0);
                                }
                                else
                                    throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                            }
                            else
                                throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                        }
                        else
                            throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                    }
                }
                else
                    _Form.SendToCabelDate = SendToCabelDate.SelectedDate;

                if (CabelDate.SelectedDate > DB.GetServerDate())
                    throw new Exception("تاریخ اصلاح در کابل صحیح نمی باشد");

                if (!string.IsNullOrEmpty(CabelTimeTextBox.Text))
                {
                    if (CabelDate.SelectedDate == null)
                        throw new Exception("لطفا قبل از تعیین ساعت اصلاح در کابل، تاریخ را تعیین نمایید");
                    else
                    {
                        string[] cabelTime = CabelTimeTextBox.Text.Trim().Split(':');
                        if (cabelTime.Count() == 2)
                        {
                            int value;
                            if (int.TryParse(cabelTime[0], out value))
                            {
                                if (value >= 24)
                                    throw new Exception("ساعت اصلاح در کابل معتبر نمی باشد");

                                if (int.TryParse(cabelTime[1], out value))
                                {
                                    if (value >= 60)
                                        throw new Exception("ساعت اصلاح در کابل معتبر نمی باشد");
                                    else
                                        _Form.CabelDate = new DateTime(CabelDate.SelectedDate.Value.Year, CabelDate.SelectedDate.Value.Month, CabelDate.SelectedDate.Value.Day, Convert.ToInt32(cabelTime[0]), Convert.ToInt32(cabelTime[1]), 0);
                                }
                                else
                                    throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                            }
                            else
                                throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                        }
                        else
                            throw new Exception("ساعت وارد شده معتبر نمی باشد، لطفا با فرمت 00:00 وارد نمایید");
                    }
                }
                else
                    _Form.CabelDate = CabelDate.SelectedDate;

                if (_Form.CabelDate < _Form.SendToCabelDate)
                    throw new Exception("تاریخ اصلاح در کابل قبل از تاریخ ارجاع به کابل می باشد");

                _Form.Description = NetworkCommentTextBox.Text;

                _Failure117.NetworkComment = NetworkCommentTextBox.Text;

                UIElement container = this.Content as UIElement;
                List<System.Windows.Controls.Control> controlsList = Helper.FindVisualChildren<System.Windows.Controls.Control>(container).ToList();

                foreach (System.Windows.Controls.Control control in controlsList.Where(t => t.GetType() == typeof(RadioButton)).ToList())
                {
                    RadioButton currentControl = control as RadioButton;
                    if (currentControl.GroupName == "FailureStatus")
                    {
                        if ((bool)currentControl.IsChecked)
                        {
                            _Form.FailureStatusID = Convert.ToInt32(currentControl.Tag);
                            _Failure117.FailureStatusID = Convert.ToInt32(currentControl.Tag);
                        }
                    }
                }

                if (_Form.ID != 0)
                    RequestForFailure117.SaveFailureForm(_Form, false);
                else
                    RequestForFailure117.SaveFailureForm(_Form, true);

                _Failure117.ResultAfterReturn = 1;
                RequestForFailure117.SaveFailureActions(_Failure117);

                switch (_RequestInfo.StepID)
                {
                    case (int)DB.RequestStepFailure117.Network:
                        if (_Form.FailureStatusID == null)
                            throw new Exception("لطفا وضعیت خرابی را تعیین نمایید");
                        if (_Form.CableColor1 == 0)
                            throw new Exception("لطفا رنگ کابل را تعیین نمایید");
                        if (_Form.CableColor2 == 0)
                            throw new Exception("لطفا رنگ کابل را تعیین نمایید");
                        if (DB.City == "semnan")
                        {
                            if (_Form.CableTypeID == 0)
                                throw new Exception("لطفا نوع کابل را تعیین نمایید");
                        }
                        if (_Form.Failure117NetworkContractorOfficerID == null || _Form.Failure117NetworkContractorOfficerID == 0)
                            throw new Exception("لطفا نام مامور را تعیین نمایید");
                        if (_Form.GiveNetworkFormDate == null)
                            throw new Exception("لطفا تاریخ تحویل فرم را تعیین نمایید");
                        if (string.IsNullOrEmpty(_Form.GiveNetworkFormTime))
                            throw new Exception("لطفا ساعت تحویل فرم را تعیین نمایید");
                        if (_Form.GetNetworkFormDate == null)
                            throw new Exception("لطفا تاریخ برگشت مامور را تعیین نمایید");
                        if (string.IsNullOrEmpty(_Form.GetNetworkFormTime))
                            throw new Exception("لطفا ساعت برگشت مامور را تعیین نمایید");

                        _Failure117.NetworkUserID = DB.CurrentUser.ID;
                        _Failure117.NetworkDate = DB.GetServerDate();

                        _Request.StatusID = 1367;
                        break;
                }

                _Request.PreviousAction = (byte)DB.Action.Confirm;
                _Request.ModifyDate = DB.GetServerDate();
                _Request.ModifyUserID = DB.CurrentUser.ID;
                if (_Request.EndDate == null)
                    _Request.IsViewed = false;
                else
                    _Request.IsViewed = true;

                if (DB.City == "kermanshah")
                {
                    _Failure117.ActionStatusID = (byte)DB.Failure117ActionStatus.RemovalFailure;
                    _Failure117.ResultAfterReturn = 1;

                    _Request.StatusID = 1368;
                    _Request.PreviousAction = (byte)DB.Action.Confirm;
                    _Request.ModifyDate = DB.GetServerDate();
                    _Request.ModifyUserID = DB.CurrentUser.ID;
                    _Request.IsViewed = true;

                    if (_Failure117.FailureStatusID != null)
                    {
                        Failure117FailureStatus status = DB.SearchByPropertyName<Failure117FailureStatus>("ID", _Failure117.FailureStatusID).SingleOrDefault();
                        _Request.EndDate = DB.GetServerDate().AddHours((int)status.ArchivedTime);
                    }
                    else
                        _Request.EndDate = DB.GetServerDate().AddHours(24);

                    _Failure117.EndMDFDate = DB.GetServerDate();
                }

                RequestForFailure117.SaveFailureRequest(_Request, _Failure117, false);

                IsForwardSuccess = false;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ارجاع، " + ex.Message + "!", ex);
            }

            return IsForwardSuccess;
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

        private void ShowTelephoneInfo(long telephoneNo)
        {
            TelephoneInfoForm form = new TelephoneInfoForm(telephoneNo);
            form.ShowDialog();
        }

        #endregion

        #region Event Handlers

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

        private void AdjacentTelephoneNo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_Failure117.AdjacentTelephoneNo != null && _Failure117.AdjacentTelephoneNo != 0)
                ShowTelephoneInfo((long)_Failure117.AdjacentTelephoneNo);
        }

        private void HearingTelephoneNo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_Failure117.HearingTelephoneNo != null && _Failure117.HearingTelephoneNo != 0)
                ShowTelephoneInfo((long)_Failure117.HearingTelephoneNo);
        }

        private void MouseLeftButtonDownImage(object sender, MouseButtonEventArgs e)
        {
            Image star = sender as Image;

            if (star != null)
            {
                switch (star.Name)
                {
                    case "GiveNetworkFormDateImage":
                        GiveNetworkFormDate.SelectedDate = DB.GetServerDate();
                        break;

                    case "GiveNetworkFormTimeImage":
                        GiveNetworkFormTimeTextBox.Text = ((DB.GetServerDate().Hour >= 10) ? DB.GetServerDate().Hour.ToString() : "0" + DB.GetServerDate().Hour.ToString()) + ":" + ((DB.GetServerDate().Minute >= 10) ? DB.GetServerDate().Minute.ToString() : "0" + DB.GetServerDate().Minute.ToString());
                        break;

                    case "GetNetworkFormDateImage":
                        GetNetworkFormDate.SelectedDate = DB.GetServerDate();
                        break;

                    case "GetNetworkFormTimeImage":
                        GetNetworkFormTimeTextBox.Text = ((DB.GetServerDate().Hour >= 10) ? DB.GetServerDate().Hour.ToString() : "0" + DB.GetServerDate().Hour.ToString()) + ":" + ((DB.GetServerDate().Minute >= 10) ? DB.GetServerDate().Minute.ToString() : "0" + DB.GetServerDate().Minute.ToString());
                        break;

                    case "SendToCabelDateImage":
                        SendToCabelDate.SelectedDate = DB.GetServerDate();
                        break;

                    case "SendToCabelTimeImage":
                        SendToCabelTimeTextBox.Text = ((DB.GetServerDate().Hour >= 10) ? DB.GetServerDate().Hour.ToString() : "0" + DB.GetServerDate().Hour.ToString()) + ":" + ((DB.GetServerDate().Minute >= 10) ? DB.GetServerDate().Minute.ToString() : "0" + DB.GetServerDate().Minute.ToString());
                        break;

                    case "CabelDateImage":
                        CabelDate.SelectedDate = DB.GetServerDate();
                        break;

                    case "CabelTimeImage":
                        CabelTimeTextBox.Text = ((DB.GetServerDate().Hour >= 10) ? DB.GetServerDate().Hour.ToString() : "0" + DB.GetServerDate().Hour.ToString()) + ":" + ((DB.GetServerDate().Minute >= 10) ? DB.GetServerDate().Minute.ToString() : "0" + DB.GetServerDate().Minute.ToString());
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion
    }
}

