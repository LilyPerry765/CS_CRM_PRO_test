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
using System.Collections.ObjectModel;
using Enterprise;
using CRM.Application.UserControls;
using CRM.Application.Views;
using System.Transactions;
using CRM.Data.Schema;
using System.ComponentModel;
using System.Net;
using System.Web.Services.Protocols;
using System.IO;
using System.Diagnostics;


namespace CRM.Application.Views
{
    public partial class InvestigatePossibilityForm : Local.RequestFormBase
    {
        # region Properties And Fields

        private long _id = 0;

        private long? _subID;
        int buchtType = 0;
        CRM.Data.E1Link _e1Link { get; set; }
        public InstallRequest _installRequest { get; set; }
        public Request _request { get; set; }
        CRM.Data.ChangeLocation changeLocation;
        CRM.Data.E1 _e1;
        CRM.Data.SpecialWire _specialWire;
        CRM.Data.SpaceAndPower _spaceAndPower { get; set; }
        private Bucht _oldOtherBucht { get; set; }
        private Bucht _oldSecondOtherBucht { get; set; }

        CRM.Data.ChangeLocationSpecialWire _changeLocationSpecialWire;

        public static EnumItem sourceItem;
        private long oldPostContactID = 0;
        public string _UserName;
        bool Mode = false;
        private Guid _FileID { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        /// <summary>
        /// .مقدار این پراپرتی تعیین میکند که آیا کاربر فایلی را انتخاب کرده است یا خیر و پیرو آن دکمه ذخیره فایل فعال یا غیر فعال خواهد شد
        /// </summary>
        public bool FileIsSelected { get; set; }

        List<CableDesignOffice> _cableDesignOffice = new List<CableDesignOffice>();

        public InvestigatePossibility _InvestigatePossibility { get; set; }

        public List<SugesstionPossibility> _SugesstionPossibilityList { get; set; }

        public List<WaitingList> _WaitingList { get; set; }
        public List<Request> _RelatedRequest { get; set; }

        public List<VisitAddress> _VisitInfoList { get; set; }

        public static List<PostContact> AllpostContactList { get; set; }
        public static List<CabinetInput> centralCableList { get; set; }
        public static List<BuchtInfo> pcmChannelList { get; set; }
        public BuchtInfo buchtInfo { get; set; }

        public static List<Cabinet> cabinetList { get; set; }
        public static List<BuchtInfo> buchtList { get; set; }
        public static List<Post> postList { get; set; }
        public static List<PostContact> postContactList { get; set; }
        public static List<PCMInfo> pcmList { get; set; }
        List<CRM.Data.AssignmentInfo> assingmentInfos;
        CRM.Data.AssignmentInfo assingmentInfo;
        public Bucht bucht { get; set; }
        public Cabinet cabinet { get; set; }
        public PostContact oldPostContact { get; set; }
        public Bucht oldBucht { get; set; }
        public Bucht oldTargetBucht { get; set; }
        public Post oldPost { get; set; }
        public Cabinet oldCabinet { get; set; }
        private SpecialCondition _specialCondition { get; set; }
        CRM.Data.AssignmentInfo oldAssignmentInfo { get; set; }
        CRM.Data.AssignmentInfo oldTargetAssignmentInfo { get; set; }

        CRM.Data.AssignmentInfo OldTelephoneAssignmentInfo { get; set; }

        // if open new InvestigatePossibilityForm. the property IsNewInvestigatePossibility is be true;
        private bool IsNewInvestigatePossibility { get; set; }

        // if open new InvestigatePossibilityForm and select save. the property SelectSaveNewInvestigatePossibility is be true;
        private static bool SelectSaveNewInvestigatePossibility { get; set; }


        //public UserControls.UserInfoSummary     _userInfoSummary     { get; set; }
        public UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        public UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        public UserControls.InstallInfoSummary _installInfoSummary { get; set; }

        public UserControls.ProposaledCabinetAndPost _proposaledCabinetAndPost { get; set; }
        public UserControls.E1InfoSummary _E1InfoSummary { get; set; }
        public UserControls.V2SpaceAndPowerInfoSummary _V2SpaceAndPowerInfoSummary { get; set; }

        #endregion

        #region Constructors

        public InvestigatePossibilityForm()
        {
            InitializeComponent();
        }

        public InvestigatePossibilityForm(long requestID, long? subID)
            : this()
        {
            _subID = subID;
            _request = Data.RequestDB.GetRequestByID(requestID);
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByType(_request.CenterID);

            _installRequest = Data.InstallRequestDB.GetInstallRequestByRequestID(_request.ID);
            Initialize();
        }

        #endregion

        #region Methods

        private void FillInput()
        {
            CheckableItem cabinetInput = new CheckableItem();

            if (IsNewInvestigatePossibility != true)
            {
                // ورودی که قبلا رزرو شده است را برای بار
                if (oldAssignmentInfo != null && (int)CabinetComboBox.SelectedValue == oldAssignmentInfo.CabinetID)
                {
                    cabinetInput = Data.CabinetInputDB.GetCabinetInputChechableByID(oldAssignmentInfo.InputNumberID ?? 0).SingleOrDefault();
                    InputComboBox.ItemsSource = Data.CabinetDB.GetFreeCabinetInputByCabinetIDWithTelephon((int)CabinetComboBox.SelectedValue).Union(new List<CheckableItem> { cabinetInput }).ToList();
                }
                else
                {
                    InputComboBox.ItemsSource = Data.CabinetDB.GetFreeCabinetInputByCabinetIDWithTelephon((int)CabinetComboBox.SelectedValue);
                }

            }
            else
            {

                if (oldTargetAssignmentInfo != null && (int)CabinetComboBox.SelectedValue == oldTargetAssignmentInfo.CabinetID)
                {
                    cabinetInput = Data.CabinetInputDB.GetCabinetInputChechableByID(oldTargetAssignmentInfo.InputNumberID ?? 0).SingleOrDefault();
                    InputComboBox.ItemsSource = Data.CabinetDB.GetFreeCabinetInputByCabinetID((int)CabinetComboBox.SelectedValue).Union(new List<CheckableItem> { cabinetInput }).ToList();
                }
                else
                {
                    InputComboBox.ItemsSource = Data.CabinetDB.GetFreeCabinetInputByCabinetID((int)CabinetComboBox.SelectedValue);
                }

            }
        }

        private void Resetcomboboxes()
        {


        }

        private void BuchtInfo(long? cabintInput)
        {
            BuchtNoTextBox.Text = string.Empty;
            MDFNoTextBox.Text = string.Empty;

            bucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(new List<long> { cabintInput ?? -1 }).Take(1).SingleOrDefault();
            if (bucht != null)
            {
                BuchtNoTextBox.Text = DB.GetConnectionByBuchtID(bucht.ID);
                MDFNoTextBox.Text = Data.MDFDB.GetMDFBtBuchtID(bucht.ID);
            }

        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
            base.RequestID = _request.ID;

            _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
            _customerInfoSummary.Mode = true;
            CustomerInfoUC.Content = _customerInfoSummary;
            CustomerInfoUC.DataContext = _customerInfoSummary;

            _requestInfoSummary = new RequestInfoSummary(_request.ID);
            _requestInfoSummary.CentercomboBox.IsEnabled = true;
            RequestInfoUC.Content = _requestInfoSummary;
            RequestInfoUC.DataContext = _requestInfoSummary;
            SourceTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.SourceType));
            SourceTypeColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.SourceType));
            ConnectionTypeColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.PostContactConnectionType));

            //PCMStatusComboBoxColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.PCMStatus));
            //PCMPortStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PCMPortStatus));
            ReasonColumn.ItemsSource = Data.WaitingListReasonDB.GetWaitingListReasonCheckableByRequestTypeID();

            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    {
                        ProposaledCabinetAndPostUserControl.Visibility = Visibility.Visible;
                        ProposaledCabinetAndPostUserControl.AddressInfoExpander.IsExpanded = true;
                        ProposaledCabinetAndPostUserControl = new ProposaledCabinetAndPost(_request.ID, _installRequest.NearestTelephon);

                        InstallInfoUC.Visibility = Visibility.Visible;
                        _installInfoSummary = new InstallInfoSummary(_request.ID);
                        InstallInfoUC.Content = _installInfoSummary;
                        InstallInfoUC.DataContext = _installInfoSummary;


                    }
                    break;

                case (int)DB.RequestType.ChangeLocationCenterInside:
                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                    {
                        NewCustomerAddressInfo.Visibility = Visibility.Visible;
                        ProposaledCabinetAndPostUserControl.Visibility = Visibility.Visible;
                        ProposaledCabinetAndPostUserControl.AddressInfoExpander.IsExpanded = true;
                        NewCustomerAddressInfo = new UserControls.CustomerAddressUserControl(_request.ID);

                        changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(_request.ID);
                        ProposaledCabinetAndPostUserControl = new ProposaledCabinetAndPost(_request.ID, changeLocation.NearestTelephon);

                        _requestInfoSummary.CentercomboBox.IsEnabled = false;

                    }
                    break;

                case (int)DB.RequestType.E1:
                case (int)DB.RequestType.E1Link:
                    {
                        _E1InfoSummary = new E1InfoSummary(_request.ID, _subID);
                        _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
                        E1InfoSummaryUC.Content = _E1InfoSummary;
                        E1InfoSummaryUC.DataContext = _E1InfoSummary;
                        E1InfoSummaryUC.Visibility = Visibility.Visible;

                        ProposaledCabinetAndPostUserControl.Visibility = Visibility.Visible;
                        ProposaledCabinetAndPostUserControl.AddressInfoExpander.IsExpanded = true;
                        ProposaledCabinetAndPostUserControl = new ProposaledCabinetAndPost(_request.ID, 0);

                        if (_subID != null)
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };

                    }
                    break;
                case (int)DB.RequestType.SpaceandPower:
                    {
                        //مقداردهی کنترل های مربوط به اطلاعات رکورد فضا و پاور
                        _V2SpaceAndPowerInfoSummary = new V2SpaceAndPowerInfoSummary(_request.ID);
                        _V2SpaceAndPowerInfoSummary.SpaceAndPowerInfoSummaryExpander.IsExpanded = true;
                        V2SpaceAndPowerInfoSummaryUC.Content = _V2SpaceAndPowerInfoSummary;
                        V2SpaceAndPowerInfoSummaryUC.DataContext = _V2SpaceAndPowerInfoSummary;
                        V2SpaceAndPowerInfoSummaryUC.Visibility = Visibility.Visible;

                        //مقداردهی عملیات های مربوطه - با توجه به وضعیت های چرخه کاری
                        ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                    }
                    break;
                case (int)DB.RequestType.SpecialWire:
                case (int)DB.RequestType.SpecialWireOtherPoint:
                    {
                        _requestInfoSummary.CentercomboBox.IsEnabled = false;

                        ProposaledCabinetAndPostUserControl.Visibility = Visibility.Visible;
                        ProposaledCabinetAndPostUserControl.AddressInfoExpander.IsExpanded = true;
                        ProposaledCabinetAndPostUserControl = new ProposaledCabinetAndPost(_request.ID, 0);

                        SpecialWireUserControl specialWireUserControl = new UserControls.SpecialWireUserControl(this.RequestID);
                        specialWireUserControl.CenterID = _request.CenterID;
                        specialWireUserControl.IsEnabled = false;
                        SpecialWireInfoSummaryUC.Content = specialWireUserControl;
                        SpecialWireInfoSummaryUC.DataContext = specialWireUserControl;
                        SpecialWireInfoSummaryUC.Visibility = Visibility.Visible;
                    }
                    break;
                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    {
                        _requestInfoSummary.CentercomboBox.IsEnabled = false;
                        ProposaledCabinetAndPostUserControl.Visibility = Visibility.Visible;
                        ProposaledCabinetAndPostUserControl.AddressInfoExpander.IsExpanded = true;
                        ProposaledCabinetAndPostUserControl = new ProposaledCabinetAndPost(_request.ID, 0);


                        //SpecialWireUserControl specialWireUserControl = new UserControls.SpecialWireUserControl(this.RequestID);
                        //specialWireUserControl.CenterID = _request.CenterID;
                        //specialWireUserControl.IsEnabled = false;
                        //SpecialWireInfoSummaryUC.Content = specialWireUserControl;
                        //SpecialWireInfoSummaryUC.DataContext = specialWireUserControl;
                        //SpecialWireInfoSummaryUC.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        public void Load()
        {
            if (!(_request.RequestTypeID == (int)DB.RequestType.E1 || _request.RequestTypeID == (int)DB.RequestType.E1Link) && _request.RequestTypeID != (byte)DB.RequestType.SpaceandPower)
            {
                _InvestigatePossibility = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_request.ID).Take(1).SingleOrDefault();
                if (_InvestigatePossibility != null)
                {
                    _id = _InvestigatePossibility.ID;
                    oldBucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID ?? 0);
                    oldPostContact = PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                    oldPostContactID = _InvestigatePossibility.PostContactID ?? 0;
                }
                else
                {
                    _InvestigatePossibility = new InvestigatePossibility();
                }
            }
            else
            {
                // in switch case handeled
            }
            _specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_request.ID);

            if (_specialCondition != null && _specialCondition.ReturnedFromWiring == true)
            {
                InputComboBox.IsEnabled = false;
                CabinetComboBox.IsEnabled = false;
            }
            InvestigateStatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            InvestigateStatusComboBox.SelectedValue = this.currentStat;

            if (Data.StatusDB.GetStatueByStatusID(this.currentStat).StatusType == (byte)DB.RequestStatusType.Changes)
            {
                InvestigateStatusComboBox.IsEnabled = false;
            }

            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    {
                        if (oldBucht != null)
                        {
                            oldPost = Data.PostDB.GetPostByID(oldPostContact.PostID);
                            oldCabinet = Data.CabinetDB.GetCabinetByID(oldPost.CabinetID);

                            CabinetComboBox.SelectedValue = oldCabinet.ID;
                            CabinetComboBox_SelectionChanged(null, null);
                            PostComboBox.SelectedValue = oldPost.ID;
                            PostComboBox_SelectionChanged(null, null);

                            ConnectionDataGrid.ItemsSource = DB.GetAllInformationByPostIDAndWithOutpostContactType(oldPost.ID, (byte)DB.PostContactConnectionType.PCMRemote);
                            ConnectionDataGrid.SelectedValue = oldPostContact.ID;
                            oldAssignmentInfo = ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;

                            InputComboBox.SelectedValue = oldCabinet.ID;
                            bucht = Data.BuchtDB.GetBuchtByCabinetIDANDPostContact(new List<int>() { oldCabinet.ID }, oldPostContact.ID).SingleOrDefault();
                            if (bucht != null)
                            {
                                BuchtNoTextBox.Text = DB.GetConnectionByBuchtID(bucht.ID);
                                MDFNoTextBox.Text = Data.MDFDB.GetMDFBtBuchtID(bucht.ID);
                            }

                            CommentTextBox.Text = _InvestigatePossibility.Comment;
                        }

                        _SugesstionPossibilityList = Data.SugesstionPossibilityDB.GetSugesstionPossibilityByInvestigatePossibilityID(_InvestigatePossibility.ID).ToList();
                        _WaitingList = Data.WaitingListDB.GetWaitingListByRequestID(_request.ID).ToList();
                        _RelatedRequest = Data.RequestDB.GetRelatedRequestByID(_request.ID).ToList();

                        SuggestGrid.ItemsSource = _SugesstionPossibilityList;
                        SuggestGrid.DataContext = _SugesstionPossibilityList;

                        WaitingGrid.ItemsSource = _WaitingList;
                        WaitingGrid.DataContext = _WaitingList;

                        RelatedRequestsGrid.ItemsSource = _RelatedRequest;
                        RelatedRequestsGrid.DataContext = _RelatedRequest;
                    }
                    break;
                case (int)DB.RequestType.ChangeLocationCenterInside:
                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                    {
                        CommentTextBox.Text = _InvestigatePossibility.Comment;
                        _request = Data.RequestDB.GetRequestByID(_request.ID);
                        OldTelephoneAssignmentInfo = DB.GetAllInformationByTelephoneNo(changeLocation.OldTelephone ?? 0);
                        if (_InvestigatePossibility.BuchtID != null)
                        {
                            oldAssignmentInfo = DB.GetAllInformationByBuchtID(_InvestigatePossibility.BuchtID ?? 0);
                            bucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID);
                            if (bucht != null)
                            {
                                BuchtNoTextBox.Text = DB.GetConnectionByBuchtID(bucht.ID);
                                MDFNoTextBox.Text = Data.MDFDB.GetMDFBtBuchtID(bucht.ID);
                                InputComboBox.SelectedValue = bucht.CabinetInputID;
                            }

                            if (oldAssignmentInfo != null)
                            {
                                CabinetComboBox.SelectedValue = oldAssignmentInfo.CabinetID;
                                CabinetComboBox_SelectionChanged(null, null);
                                PostComboBox.SelectedValue = oldAssignmentInfo.PostID;
                                PostComboBox_SelectionChanged(null, null);

                            }

                            if (changeLocation.ChangeLocationTypeID == (byte)DB.ChangeLocationCenterType.itself)
                            {
                                ConnectionDataGrid.SelectedValue = _InvestigatePossibility.PostContactID;
                            }
                            else
                            {
                                ConnectionDataGrid.SelectedValue = _InvestigatePossibility.PostContactID;
                            }

                            SourceTypeLable.Visibility = Visibility.Collapsed;
                            SourceTypecomboBox.Visibility = Visibility.Collapsed;
                            Mode = true;
                        }
                    }
                    break;
                case (int)DB.RequestType.SpaceandPower:
                    {
                        _spaceAndPower = SpaceAndPowerDB.GetSpaceAndPowerByRequestId(_request.ID);
                        CableDesignFileDescriptionLabel.Visibility = Visibility.Visible;
                        CableDesignFileDescriptionTextBox.Visibility = Visibility.Visible;
                        CableDesignDatePicker.Visibility = Visibility.Visible;
                        CableDesignDateLabel.Visibility = Visibility.Visible;
                        CableDesignFileGroupBox.Visibility = Visibility.Visible;
                        CableDesignFileDescriptionTextBox.Text = _spaceAndPower.CableAndNetworkDesignOfficeComment;
                        CableDesignDatePicker.SelectedDate = _spaceAndPower.CableAndNetworkDesignOfficeDate;

                        //کنترل های مربوط به اطلاعات غیر از فضا و پاور نباید نمایش داده شوند
                        InvestigateInfo.Visibility = Visibility.Collapsed;
                        RequestDetail.Visibility = Visibility.Collapsed;
                        InvestigateInfo.Visibility = Visibility.Collapsed;
                        InputLabel.Visibility = Visibility.Collapsed;
                        InputComboBox.Visibility = Visibility.Collapsed;
                        SourceTypeLable.Visibility = Visibility.Collapsed;
                        SourceTypecomboBox.Visibility = Visibility.Collapsed;
                        MDFNoLabel.Visibility = Visibility.Collapsed;
                        MDFNoTextBox.Visibility = Visibility.Collapsed;
                        BuchtNoLabel.Visibility = Visibility.Collapsed;
                        BuchtNoTextBox.Visibility = Visibility.Collapsed;
                        CommentLabel.Visibility = Visibility.Collapsed;
                        CommentTextBox.Visibility = Visibility.Collapsed;

                        RefreshItemsDataGrid();
                    }
                    break;
                case (int)DB.RequestType.E1:
                case (int)DB.RequestType.E1Link:
                    {
                        buchtType = (int)DB.BuchtType.E1;
                        _e1 = Data.E1DB.GetE1ByRequestID(_request.ID);

                        if (_subID != null)
                        {
                            _e1Link = Data.E1LinkDB.GetE1LinkByID(_subID ?? 0);

                            _InvestigatePossibility = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByID(_e1Link.InvestigatePossibilityID ?? 0);
                            if (_InvestigatePossibility != null)
                            {
                                _id = _InvestigatePossibility.ID;
                                oldBucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID ?? 0);
                                oldPostContact = PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                                oldPostContactID = _InvestigatePossibility.PostContactID ?? 0;
                            }
                            else
                            {
                                _InvestigatePossibility = new InvestigatePossibility();
                            }

                            _oldOtherBucht = Data.BuchtDB.GetBuchetByID(_e1Link.OtherBuchtID);

                            RequestDetail.Visibility = Visibility.Collapsed;
                            ////
                            MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(_request.CenterID);
                            ////

                            if (_oldOtherBucht != null)
                            {
                                ConnectionInfo connectionInfo = DB.GetConnectionInfoByBuchtID(_oldOtherBucht.ID);

                                MDFComboBox.SelectedValue = connectionInfo.MDFID;
                                MDFComboBox_SelectionChanged(null, null);

                                ConnectionColumnComboBox.SelectedValue = connectionInfo.VerticalColumnID;
                                ConnectionColumnComboBox_SelectionChanged(null, null);

                                ConnectionRowComboBox.SelectedValue = connectionInfo.VerticalRowID;
                                ConnectionRowComboBox_SelectionChanged(null, null);

                                ConnectionBuchtComboBox.SelectedValue = connectionInfo.BuchtID;
                            }

                            OtherBuchtGroupBox.Visibility = Visibility.Visible;
                            BuchtTypeTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.BuchtType), (int)DB.BuchtType.E1);
                            InvestigateStatusLabel.Visibility = Visibility.Collapsed;
                            InvestigateStatusComboBox.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            CableDesignFileGroupBox.Visibility = Visibility.Visible;
                            //if (_e1.CableDesignFileID.HasValue)
                            //    CableDesignTypeListBox.SelectedIndex = 0;
                            InvestigateInfo.Visibility = Visibility.Collapsed;
                            InputLabel.Visibility = Visibility.Collapsed;
                            InputComboBox.Visibility = Visibility.Collapsed;
                            SourceTypeLable.Visibility = Visibility.Collapsed;
                            SourceTypecomboBox.Visibility = Visibility.Collapsed;
                            MDFNoLabel.Visibility = Visibility.Collapsed;
                            MDFNoTextBox.Visibility = Visibility.Collapsed;
                            BuchtNoLabel.Visibility = Visibility.Collapsed;
                            BuchtNoTextBox.Visibility = Visibility.Collapsed;
                            CommentLabel.Visibility = Visibility.Collapsed;
                            CommentTextBox.Visibility = Visibility.Collapsed;
                            FillVisitInfo();

                            RefreshItemsDataGrid();
                            return;
                        }

                        oldAssignmentInfo = DB.GetAllInformationByBuchtID(_InvestigatePossibility.BuchtID ?? 0);
                        if (oldBucht != null)
                        {
                            BuchtNoTextBox.Text = DB.GetConnectionByBuchtID(oldBucht.ID);
                            MDFNoTextBox.Text = Data.MDFDB.GetMDFBtBuchtID(oldBucht.ID);
                        }
                        if (oldAssignmentInfo != null)
                        {
                            CabinetComboBox.SelectedValue = oldAssignmentInfo.CabinetID;
                            CabinetComboBox_SelectionChanged(null, null);
                            PostComboBox.SelectedValue = oldAssignmentInfo.PostID;
                            PostComboBox_SelectionChanged(null, null);
                            ConnectionDataGrid.SelectedValue = oldAssignmentInfo.PostContactID;
                            InputComboBox.SelectedValue = oldAssignmentInfo.InputNumberID;
                            SourceTypeLable.Visibility = Visibility.Collapsed;
                            SourceTypecomboBox.Visibility = Visibility.Collapsed;
                        }
                    }
                    break;
                case (int)DB.RequestType.SpecialWire:
                case (int)DB.RequestType.SpecialWireOtherPoint:
                    {

                        _specialWire = Data.SpecialWireDB.GetSpecialWireByRequestID(_request.ID);
                        buchtType = _specialWire.BuchtType;
                        SecondMDFComboBox.ItemsSource = MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(_request.CenterID);

                        _oldOtherBucht = Data.BuchtDB.GetBuchetByID(_specialWire.OtherBuchtID);
                        if (_oldOtherBucht != null)
                        {
                            ConnectionInfo connectionInfo = DB.GetConnectionInfoByBuchtID(_oldOtherBucht.ID);

                            MDFComboBox.SelectedValue = connectionInfo.MDFID;
                            MDFComboBox_SelectionChanged(null, null);

                            ConnectionColumnComboBox.SelectedValue = connectionInfo.VerticalColumnID;
                            ConnectionColumnComboBox_SelectionChanged(null, null);

                            ConnectionRowComboBox.SelectedValue = connectionInfo.VerticalRowID;
                            ConnectionRowComboBox_SelectionChanged(null, null);

                            ConnectionBuchtComboBox.SelectedValue = connectionInfo.BuchtID;
                        }

                        if (_specialWire.SpecialWireType == (int)DB.SpecialWireType.General)
                        {
                            oldBucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID ?? 0);
                            if (oldBucht != null)
                            {
                                oldAssignmentInfo = DB.GetAllInformationByBuchtID(oldBucht.ID);

                                BuchtNoTextBox.Text = DB.GetConnectionByBuchtID(oldBucht.ID);
                                MDFNoTextBox.Text = Data.MDFDB.GetMDFBtBuchtID(oldBucht.ID);
                            }
                            if (oldAssignmentInfo != null)
                            {
                                CabinetComboBox.SelectedValue = oldAssignmentInfo.CabinetID;
                                CabinetComboBox_SelectionChanged(null, null);
                                PostComboBox.SelectedValue = oldAssignmentInfo.PostID;
                                PostComboBox_SelectionChanged(null, null);
                                ConnectionDataGrid.SelectedValue = oldAssignmentInfo.PostContactID;
                                InputComboBox.SelectedValue = oldAssignmentInfo.InputNumberID;
                                SourceTypeLable.Visibility = Visibility.Collapsed;
                                SourceTypecomboBox.Visibility = Visibility.Collapsed;
                            }
                        }
                        else if (_specialWire.SpecialWireType == (int)DB.SpecialWireType.Middle)
                        {

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Cancelation };
                            _oldSecondOtherBucht = Data.BuchtDB.GetBuchetByID(_specialWire.SecondOtherBuchtID);
                            if (_oldSecondOtherBucht != null)
                            {
                                ConnectionInfo SecondconnectionInfo = DB.GetConnectionInfoByBuchtID(_oldSecondOtherBucht.ID);

                                SecondMDFComboBox.SelectedValue = SecondconnectionInfo.MDFID;
                                SecondMDFComboBox_SelectionChanged(null, null);

                                SecondConnectionColumnComboBox.SelectedValue = SecondconnectionInfo.VerticalColumnID;
                                SecondConnectionColumnComboBox_SelectionChanged(null, null);

                                SecondConnectionRowComboBox.SelectedValue = SecondconnectionInfo.VerticalRowID;
                                SecondConnectionRowComboBox_SelectionChanged(null, null);

                                SecondConnectionBuchtComboBox.SelectedValue = SecondconnectionInfo.BuchtID;
                            }

                            InputLabel.Visibility = Visibility.Collapsed;
                            InputComboBox.Visibility = Visibility.Collapsed;
                            SourceTypeLable.Visibility = Visibility.Collapsed;
                            SourceTypecomboBox.Visibility = Visibility.Collapsed;
                            MDFNoLabel.Visibility = Visibility.Collapsed;
                            MDFNoTextBox.Visibility = Visibility.Collapsed;
                            BuchtNoLabel.Visibility = Visibility.Collapsed;
                            BuchtNoTextBox.Visibility = Visibility.Collapsed;
                            //InvestigateStatusLabel
                            //InvestigateStatusComboBox
                            CommentLabel.Visibility = Visibility.Collapsed;
                            CommentTextBox.Visibility = Visibility.Collapsed;

                            InvestigateInfo.Visibility = Visibility.Collapsed;

                            SecondBuchtTypeLabel.Visibility = Visibility.Visible;
                            SecondBuchtTypeTextBox.Visibility = Visibility.Visible;

                            SecondMDFLabel.Visibility = Visibility.Visible;
                            SecondMDFComboBox.Visibility = Visibility.Visible;

                            SecondColumnLable.Visibility = Visibility.Visible;
                            SecondConnectionColumnComboBox.Visibility = Visibility.Visible;

                            SecondRowLable.Visibility = Visibility.Visible;
                            SecondConnectionRowComboBox.Visibility = Visibility.Visible;

                            SecondConnectionLable.Visibility = Visibility.Visible;
                            SecondConnectionBuchtComboBox.Visibility = Visibility.Visible;

                        }
                        OtherBuchtGroupBox.Visibility = Visibility.Visible;
                        SecondBuchtTypeTextBox.Text = BuchtTypeTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.BuchtType), _specialWire.BuchtType);

                    }
                    break;
                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    {
                        BuchtTypeComboBox.ItemsSource = Data.BuchtTypeDB.GetSubBuchtTypeCheckable((int)DB.BuchtType.PrivateWire);
                        MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(_request.CenterID);
                        OtherBuchtGroupBox.Visibility = Visibility.Visible;
                        _changeLocationSpecialWire = Data.ChangeLocationSpecialWireDB.GetChangeLocationWireByRequestID(_request.ID);

                        oldBucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID ?? 0);

                        if (oldBucht != null)
                        {
                            oldAssignmentInfo = DB.GetAllInformationByBuchtID(oldBucht.ID);

                            BuchtNoTextBox.Text = DB.GetConnectionByBuchtID(oldBucht.ID);
                            MDFNoTextBox.Text = Data.MDFDB.GetMDFBtBuchtID(oldBucht.ID);
                        }

                        if (oldAssignmentInfo != null)
                        {

                            CabinetComboBox.SelectedValue = oldAssignmentInfo.CabinetID;
                            CabinetComboBox_SelectionChanged(null, null);
                            PostComboBox.SelectedValue = oldAssignmentInfo.PostID;
                            PostComboBox_SelectionChanged(null, null);
                            ConnectionDataGrid.SelectedValue = _InvestigatePossibility.PostContactID;
                            InputComboBox.SelectedValue = oldAssignmentInfo.InputNumberID;
                            InputComboBox_SelectionChanged(null, null);
                            SourceTypeLable.Visibility = Visibility.Collapsed;
                            SourceTypecomboBox.Visibility = Visibility.Collapsed;
                        }

                        if (!(_changeLocationSpecialWire.OldOtherBuchtID == null || _changeLocationSpecialWire.OldOtherBuchtID == 0))
                        {

                            _oldOtherBucht = Data.BuchtDB.GetBuchetByID(_changeLocationSpecialWire.OldOtherBuchtID);
                            BuchtTypeTextBox.Visibility = Visibility.Collapsed;
                            BuchtTypeComboBox.Visibility = Visibility.Visible;
                            BuchtTypeComboBox.SelectedValue = _oldOtherBucht.BuchtTypeID;
                            BuchtTypeComboBox_SelectionChanged(null, null);
                            BuchtTypeComboBox.IsEnabled = false;

                            ConnectionInfo connectionInfo = DB.GetConnectionInfoByBuchtID(_oldOtherBucht.ID);

                            MDFComboBox.SelectedValue = connectionInfo.MDFID;
                            MDFComboBox_SelectionChanged(null, null);

                            ConnectionColumnComboBox.SelectedValue = connectionInfo.VerticalColumnID;
                            ConnectionColumnComboBox_SelectionChanged(null, null);

                            ConnectionRowComboBox.SelectedValue = connectionInfo.VerticalRowID;
                            ConnectionRowComboBox_SelectionChanged(null, null);

                            ConnectionBuchtComboBox.SelectedValue = connectionInfo.BuchtID;
                        }
                        else
                        {
                            BuchtTypeTextBox.Visibility = Visibility.Collapsed;
                            BuchtTypeComboBox.Visibility = Visibility.Visible;
                        }
                    }
                    break;
            }

            FillVisitInfo();
        }

        private void FillVisitInfo()
        {
            long? addressID = -1;
            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    addressID = _installRequest.InstallAddressID;
                    break;

                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                case (int)DB.RequestType.ChangeLocationCenterInside:
                    addressID = changeLocation.NewInstallAddressID;
                    break;

                case (int)DB.RequestType.E1:
                case (int)DB.RequestType.E1Link:
                    addressID = _e1.InstallAddressID;
                    break;
                case (int)DB.RequestType.SpecialWire:
                case (int)DB.RequestType.SpecialWireOtherPoint:
                    addressID = _specialWire.InstallAddressID;
                    if (_specialWire.SpecialWireType == (int)DB.SpecialWireType.Middle)
                        return;
                    break;
                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    addressID = _changeLocationSpecialWire.InstallAddressID;
                    if (_changeLocationSpecialWire.SpecialWireTypeID == (int)DB.SpecialWireType.Middle)
                        return;
                    break;
                case (int)DB.RequestType.SpaceandPower:
                    {
                        //آدرسی با شناسه صفر وجود ندارد
                        addressID = 0;
                    }
                    break;
            }


            if (!(addressID == -1 || addressID == null))
            {
                _VisitInfoList = Data.VisitAddressDB.GetVisitAddressByRequestID(_request.ID, (long)addressID).OrderByDescending(t => t.ID).ToList();

                if (_VisitInfoList.Count > 0 && (_VisitInfoList.Take(1).SingleOrDefault().OutBoundMeter ?? 0) > 0)
                {
                    Status status = Data.StatusDB.GetStatusInCurrentStepByStatusType(this.currentStat, DB.RequestStatusType.OutBound);
                    if (status != null)
                        InvestigateStatusComboBox.SelectedValue = status.ID;
                }
                //CrossPostComboBoxColumn.ItemsSource = Data.PostDB.GetPostCheckable();
                //if (visitAddress.CrossPostID != null)
                //{
                //    Cabinet cabinet = Data.CabinetDB.GetCabinetByPostID((int)visitAddress.CrossPostID);
                //    CrossCabinetComboBox.SelectedValue = cabinet.ID;
                //    CrossCabinetComboBox_SelectionChanged(null, null);
                //    CrossPostComboBox.SelectedValue = visitAddress.CrossPostID;
                //}
                VisitInfoGrid.ItemsSource = _VisitInfoList;
                VisitInfo.DataContext = _VisitInfoList;

            }
            else
            {

                MessageBox.Show("آدرس برای این روال  یافت نشد لطفا با مدیر سیستم تماس بگیرید.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RefreshItemsDataGrid()
        {
            _cableDesignOffice = CableDesignOfficeDB.GetCableDesignOfficeByRequestID(_request.ID);
            ItemsDataGrid.ItemsSource = null;
            ItemsDataGrid.ItemsSource = _cableDesignOffice;
        }

        # endregion

        #region Actions

        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }


            try
            {
                if (_request.RequestTypeID != (int)DB.RequestType.SpaceandPower)
                {
                    // if saved on New InvestigatePossibilityForm. information other point save on _privateWirie object.
                    if (IsNewInvestigatePossibility == true)
                    {

                        SelectSaveNewInvestigatePossibility = true;
                        this.Close();
                    }
                    else
                    {
                        Status Status = Data.StatusDB.GetStatueByStatusID((int)InvestigateStatusComboBox.SelectedValue);
                        if (Status.StatusType == (byte)DB.RequestStatusType.WaitingList)
                        {
                            Folder.MessageBox.ShowInfo("امکان ذخیره در وضعیت های لیست انتظار نمی باشد. می توانید از ذخیره وارجاع استفاده کنید.");
                            IsSaveSuccess = false;
                            return false;
                        }

                        if (_requestInfoSummary.CentercomboBox.SelectedValue != null)
                        {
                            if (_request.CenterID != (int)_requestInfoSummary.CentercomboBox.SelectedValue)
                            {
                                if (MessageBox.Show("شما مرکز مربوط به درخواست را تغییر داده اید. اطلاعات مربوط به تغییر مرکز ذخیره شود؟", "تغییر در مرکز", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                {
                                    _request.CenterID = (int)_requestInfoSummary.CentercomboBox.SelectedValue;
                                    _request.Detach();
                                    DB.Save(_request);



                                    throw new Exception("اطلاعات ذخیره نشد. فقط مرکز تغییر داده شد.");
                                }
                            }

                        }
                        if (_specialCondition != null && _specialCondition.ReturnedFromWiring == true && ConnectionDataGrid.SelectedItem != null)
                        {
                            CRM.Data.AssignmentInfo item = ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;
                            if (oldAssignmentInfo.PostContactID != item.PostContactID && item.PostContactType == (int)DB.PostContactConnectionType.PCMNormal)
                            {
                                throw new Exception("در وضعیتی که درخواست از شبکه هوایی برگشت داده شده باشد بعلت اینکه باانتخاب پی سی ام نیاز به تغییر بوخت می باشد امکان انتخاب  پی سی ام نیست.");
                            }
                        }

                        long? telephone = null;
                        if (Data.RequestDocumnetDB.CheckTelephoneBeRound(_request, out telephone) && !Data.TelephoneDB.CheckTelephonNoInOpticalCabinet(telephone, cabinet))
                        {
                            throw new Exception("برای درخواست تلفن رند ثبت شده است. این تلفن در رنج کافو نوری انتخاب شده نمی باشد");
                        }



                        if (cabinet != null && cabinet.ApplyQuota && Data.InvestigatePossibilityDB.CheckCabinetShare(cabinet) && Status.StatusType != (byte)DB.RequestStatusType.ChangeTheLocationItself && InputComboBox.SelectedValue != null)
                        {
                            throw new Exception("مرکزی های باقی مانده کافو سهمیه رزرو می باشد");
                        }

                    }
                }
                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.Dayri:
                    case (int)DB.RequestType.Reinstall:
                        DayriSave();
                        break;

                    case (int)DB.RequestType.ChangeLocationCenterInside:
                    case (int)DB.RequestType.ChangeLocationCenterToCenter:
                        ChangeLocationSave();
                        break;

                    case (int)DB.RequestType.E1:
                    case (int)DB.RequestType.E1Link:
                        E1Save();
                        break;
                    case (int)DB.RequestType.SpecialWire:
                    case (int)DB.RequestType.SpecialWireOtherPoint:
                        SpecialWireSave();
                        break;
                    case (int)DB.RequestType.ChangeLocationSpecialWire:
                        ChangeLocationSpecialWireSave();
                        break;
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            SpaceAndPowerSave();
                        }
                        break;
                    default:
                        MessageBox.Show("در خواست یافت نشد");
                        break;
                }
                IsSaveSuccess = true;
                ShowSuccessMessage("ذخیره انجام شد");


            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);

            }

            return IsSaveSuccess;

        }

        public override bool Forward()
        {
            try
            {
                if (InvestigateStatusComboBox.SelectedValue == null)
                {
                    throw new Exception("وضعیت انتخاب نشده است.");
                }

                // اگر وضعیت انتخاب شد از وضعیت های لیست انتظار باشد با وضعیت انتخاب شده در جدول لیست انتظار دخیره میشود
                // و درخواست به وضعیت فعلی  برگردانده میشود
                // تا بعد از خروج از لیست انتظار از وضعیت فعلی ادامه پیدا کند
                int PreStatus = _request.StatusID;
                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;

                Status Status = Data.StatusDB.GetStatueByStatusID(_request.StatusID);
                if (Status.StatusType == (byte)DB.RequestStatusType.WaitingList)
                {
                    //if (CabinetAndPostUserControl.CabinetID == 0 || CabinetAndPostUserControl.PostID == 0)
                    //{
                    //    Folder.MessageBox.ShowInfo("لطفا کافو و پستی که مایل هستید در لیست انتظار قرار دهید را انتخاب نمایید");
                    //    IsForwardSuccess = false;
                    //    return false;
                    //}
                    WaitingList waitingList = new WaitingList();

                    InvestigatePossibilityWaitinglist investigatePossibilityWaitinglist = new InvestigatePossibilityWaitinglist();

                    waitingList.RequestID = _request.ID;
                    waitingList.CreatorUserID = DB.CurrentUser.ID;
                    waitingList.StatusID = _request.StatusID;
                    waitingList.WaitingListType = (byte)DB.WatingListType.investigatePossibility;
                    waitingList.Status = false;

                    if (_request.RequestTypeID == (int)DB.RequestType.Dayri || _request.RequestTypeID == (int)DB.RequestType.Reinstall)
                    {
                        investigatePossibilityWaitinglist.InstallAdressID = _installInfoSummary._InstallAdrressID;
                    }
                    else
                    {
                        investigatePossibilityWaitinglist.InstallAdressID = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo).InstallAddressID;
                    }

                    if (PostComboBox.SelectedValue != null)
                    {
                        investigatePossibilityWaitinglist.PostID = (int)PostComboBox.SelectedValue;
                    }
                    else
                    {
                        investigatePossibilityWaitinglist.PostID = null;
                    }

                    if (CabinetComboBox.SelectedValue != null)
                    {
                        investigatePossibilityWaitinglist.CabinetID = (int)CabinetComboBox.SelectedValue;
                    }
                    else
                    {
                        investigatePossibilityWaitinglist.CabinetID = null;
                    }

                    investigatePossibilityWaitinglist.CustomerID = _request.CustomerID;


                    _request.StatusID = PreStatus;
                    Data.WaitingPossibilityDB.investigatePossibilityWaitingListSave(_request, waitingList, investigatePossibilityWaitinglist);
                    this.Close();
                }

                 // در حالت بازدید از محل اطلاعاتی ذخیره نمی شود و فقط به مرحله بعدی ارجاع میشود
                else if (Status.StatusType != (byte)DB.RequestStatusType.VisitPlaces)
                {
                    if (Status.StatusType == (byte)DB.RequestStatusType.OutBound)
                    {
                        if (_VisitInfoList.Count() == 1)
                        {
                            VisitAddress visitAddress = _VisitInfoList.Single();
                            visitAddress.ConfirmInvestigatePossibility = true;
                            visitAddress.Detach();
                            DB.Save(visitAddress);
                            IsForwardSuccess = true;
                        }
                        else if (_VisitInfoList.Count() == 0)
                        {
                            throw new Exception("اطلاعات خارج از مرز یافت نشد امکان ارجاع نمی باشد");
                        }
                        else
                        {
                            throw new Exception("چند رکورد اطلاعات خارج از مرز یافت شد. امکان ارجاع نمی باشد");

                        }
                    }
                    Save();

                    if (IsSaveSuccess == true)
                    {
                        Status = Data.StatusDB.GetStatuesByStatusID(_request.StatusID).Where(t => t.StatusType == (byte)DB.RequestStatusType.Completed).SingleOrDefault();
                        if (Status == null) { throw new Exception("وضعیت انجام شد مشخص نیست"); }

                        // در روال ها نیاز است از شبکه هوایی در صورت عدم امکان برقراری اتصال از شبکه هوایی به بررسی امکانات بازگردد و در این قسمت با اختصاص امکانات جدید به شبکه هوایی بازگردد
                        if (_request.RequestTypeID == (int)DB.RequestType.Reinstall)
                        {
                            // در روال دایری مجدد اگر تلفن قبلی آزاد یا تخلیه باشد نیاز به تعیین شماره نیست و به مرحله ام دی اف میرود
                            // اگر تلفن آزاد باشد و پورت آن شناور بوده و پورت همکنون ازاد نیست باید برای تعیین پورت به تعیین شماره برود
                            // Happen special condition

                            AssignmentInfo oldAssignmetInfo = DB.GetAllInformationByTelephoneNo(_installInfoSummary.InstallRequest.PassTelephone ?? 0);


                            long? telephone = null;
                            if (!InvestigatePossibilityDB.CheckFreePassTelephonForAutoForward(_request, bucht, _InvestigatePossibility, GetType().FullName, Title, _UserName, cabinet) && !Data.RequestDocumnetDB.CheckTelephoneBeRound(_request, out telephone))
                            {
                                if (_specialCondition == null)
                                {
                                    _specialCondition = new SpecialCondition();
                                    _specialCondition.RequestID = _request.ID;
                                    _specialCondition.BeFreeOldPhone = true;
                                    _specialCondition.Detach();
                                    DB.Save(_specialCondition, true);
                                }
                                else
                                {
                                    _specialCondition.RequestID = _request.ID;
                                    _specialCondition.BeFreeOldPhone = true;
                                    _specialCondition.Detach();
                                    DB.Save(_specialCondition, false);
                                }

                                IsForwardSuccess = true;
                            }
                            else
                            {
                                if (_specialCondition == null)
                                {
                                    _specialCondition = new SpecialCondition();
                                    _specialCondition.RequestID = _request.ID;
                                    _specialCondition.BeFreeOldPhone = false;
                                    _specialCondition.Detach();
                                    DB.Save(_specialCondition, true);
                                }
                                else
                                {
                                    _specialCondition.RequestID = _request.ID;
                                    _specialCondition.BeFreeOldPhone = false;
                                    _specialCondition.Detach();
                                    DB.Save(_specialCondition, false);
                                }
                                IsForwardSuccess = true;
                            }

                            /////
                        }
                        else if (_request.RequestTypeID == (int)DB.RequestType.SpecialWireOtherPoint)
                        {
                            // در حالت نقاط دیگر سیم خصوصی درخواست تا زمان انجام محاسبات مربوط به آن در مرکز مبدا قابلیت دیده شدن را ندارد وبعد از انجام محاسبات روال ادامه پیدا می کند
                            _request.Detach();
                            _request.WaitForToBeCalculate = true;
                            DB.Save(_request, false);
                            IsForwardSuccess = true;
                        }
                        else if (_request.RequestTypeID == (int)DB.RequestType.E1
                                 || (_request.RequestTypeID == (int)DB.RequestType.E1Link))
                        {
                            if (_e1.E1Type == (byte)DB.E1Type.Wire && !Data.E1LinkDB.CheckALLInvestigatePossibility(_e1.RequestID))
                            {
                                Folder.MessageBox.ShowError("همه لینک ها بررسی امکانات نشده اند");
                                IsForwardSuccess = false;

                            }
                            else
                            {
                                _request.Detach();
                                DB.Save(_request, false);

                                IsForwardSuccess = true;
                            }


                        }
                        else if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside && changeLocation.ChangeLocationTypeID == (int)DB.ChangeLocationCenterType.InSideCenter)
                        {
                            // در روال تغییر مکان داخل مرکز اگر نوع بوخت عوض نشود نیاز به تعیین شماره نیست
                            // که اگر نوع بوخت تغییر نکند از ارجاع با تایید استفاده و اگر نوع بوخت عوض شود از ارجاع اتوماتیک استفاده می شود
                            // اگر در برای درخواست تلفن رند ثبت شده باشد تعیین شماره می رود
                            long? telephone = null;
                            if (!Data.RequestDocumnetDB.CheckTelephoneBeRound(_request, out telephone))
                            {
                                if (InvestigatePossibilityDB.CheckChangeCabinetForAutoForward(_request, changeLocation, bucht, _specialCondition, cabinet))
                                {
                                    IsForwardSuccess = true;
                                }
                            }
                            else
                            {
                                IsForwardSuccess = true;
                            }
                        }
                        else
                        {

                            // اگر تلفن آزاد نباشد به تعیین شماره میرود
                            IsForwardSuccess = true;
                        }
                    }
                }
                else
                {

                    _request.Detach();
                    DB.Save(_request, false);
                    IsForwardSuccess = true;
                }
            }

            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ثبت اطلاعات", ex);

            }

            return IsForwardSuccess;

        }

        public override bool Deny()
        {
            try
            {
                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.Dayri:
                    case (int)DB.RequestType.Reinstall:
                        DayriDelete();
                        break;

                    case (int)DB.RequestType.ChangeLocationCenterInside:
                    case (int)DB.RequestType.ChangeLocationCenterToCenter:
                        ChangeLocationDelete();
                        break;
                    case (int)DB.RequestType.SpecialWire:
                        SpecialWireDelete();
                        break;
                    case (int)DB.RequestType.ChangeLocationSpecialWire:
                        ChangeLocationSpecialWireDelete();
                        break;
                    case (int)DB.RequestType.E1:
                        E1Reject();
                        break;
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            SpaceAndPowerReject();
                        }
                        break;
                }
                IsRejectSuccess = true;

            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در رد درخواست", ex);
            }
            base.Deny();
            return IsRejectSuccess;

        }

        public override bool Cancel()
        {

            try
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    _request.IsCancelation = true;
                    Data.CancelationRequestList cancelationRequest = new CancelationRequestList();
                    cancelationRequest.ID = RequestID;
                    cancelationRequest.EntryDate = DB.GetServerDate();
                    cancelationRequest.UserID = Folder.User.Current.ID;
                    cancelationRequest.Detach();
                    DB.Save(cancelationRequest, true);

                    switch (_request.RequestTypeID)
                    {
                        case (int)DB.RequestType.SpecialWireOtherPoint:
                            {
                                if (_specialWire.OtherBuchtID != null)
                                {
                                    Bucht otherBucht = Data.BuchtDB.GetBuchtByID((long)_specialWire.OtherBuchtID);
                                    otherBucht.Status = (byte)DB.BuchtStatus.Free;
                                    otherBucht.Detach();
                                    DB.Save(otherBucht);


                                    _specialWire.OtherBuchtID = null;
                                    _specialWire.Detach();
                                    DB.Save(_specialWire);
                                }

                                if (_specialWire.SecondOtherBuchtID != null)
                                {
                                    Bucht SecondOtherBucht = Data.BuchtDB.GetBuchtByID((long)_specialWire.SecondOtherBuchtID);

                                    SecondOtherBucht.Status = (byte)DB.BuchtStatus.Free;
                                    SecondOtherBucht.Detach();
                                    DB.Save(SecondOtherBucht);

                                    _specialWire.SecondOtherBuchtID = null;
                                    _specialWire.Detach();
                                    DB.Save(_specialWire);

                                }
                            }
                            break;
                    }

                    _request.Detach();
                    DB.Save(_request);

                    ts.Complete();
                    IsCancelSuccess = true;
                }
            }
            catch (Exception ex)
            {
                IsCancelSuccess = false;
                ShowErrorMessage("خطا در ابطال درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }

            return IsCancelSuccess;

        }

        #endregion

        #region SaveMethods

        private void SpaceAndPowerSave()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request, false);
                _spaceAndPower.CableAndNetworkDesignOfficeComment = CableDesignFileDescriptionTextBox.Text;
                _spaceAndPower.CableAndNetworkDesignOfficeDate = (CableDesignDatePicker.SelectedDate.HasValue) ? CableDesignDatePicker.SelectedDate : DB.GetServerDate();
                _spaceAndPower.Detach();
                DB.Save(_spaceAndPower, false);
                ts.Complete();
            }
        }

        private void SpecialWireSave()
        {

            using (TransactionScope ts = new TransactionScope())
            {

                if (_oldOtherBucht != null)
                {
                    _oldOtherBucht.Status = (byte)DB.BuchtStatus.Free;
                    _oldOtherBucht.Detach();
                    DB.Save(_oldOtherBucht);
                }

                if (_oldSecondOtherBucht != null)
                {
                    _oldSecondOtherBucht.Status = (byte)DB.BuchtStatus.Free;
                    _oldSecondOtherBucht.Detach();
                    DB.Save(_oldSecondOtherBucht);
                }

                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;
                Bucht otherBucht = Data.BuchtDB.GetBuchtByID((long)ConnectionBuchtComboBox.SelectedValue);
                otherBucht.Status = (byte)DB.BuchtStatus.Reserve;
                otherBucht.Detach();
                DB.Save(otherBucht);
                _specialWire.OtherBuchtID = otherBucht.ID;

                if (_specialWire.SpecialWireType == (int)DB.SpecialWireType.Middle)
                {


                    Bucht SecondOtherBucht = Data.BuchtDB.GetBuchtByID((long)SecondConnectionBuchtComboBox.SelectedValue);

                    if (SecondOtherBucht.ID == otherBucht.ID)
                        throw new Exception("بوخت های دیگر نمی توانند یکسان باشند");
                    SecondOtherBucht.Status = (byte)DB.BuchtStatus.Reserve;
                    SecondOtherBucht.Detach();
                    DB.Save(SecondOtherBucht);

                    _specialWire.SecondOtherBuchtID = SecondOtherBucht.ID;
                }
                else
                {

                    assingmentInfo = ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;
                    if (assingmentInfo == null)
                    {
                        throw new Exception("لطفا یک اتصالی را انتخاب کنید");
                    }
                    if (oldAssignmentInfo != null && oldAssignmentInfo.PostContactID != assingmentInfo.PostContactID)
                    {
                        if (!(assingmentInfo.PostContactStatus == (Byte)DB.PostContactStatus.Free))
                            throw new Exception("لطفا یک اتصالی خالی را انتخاب کنید");
                    }
                    if (assingmentInfo.PostContactType == (byte)DB.PostContactConnectionType.PCMNormal)
                    {
                        throw new Exception("امکان برقراری اتصال بر روی اتصالی پی سی ام نیست");
                    }
                    if (bucht == null)
                    {
                        throw new Exception("لطفا ورودی را انتخاب کنید");
                    }
                    if (bucht.BuchtTypeID != (int)DB.BuchtType.CustomerSide)
                    {
                        throw new Exception("فقط نوع بوخت طرف مشترک قابلیت اتصال سیم خصوصی دارد");
                    }


                    _InvestigatePossibility.PostContactID = assingmentInfo.PostContactID;
                    _InvestigatePossibility.BuchtID = bucht.ID;


                    //                // Free Source reserv Bucht
                    if (oldBucht != null)
                    {
                        // آزاد سازی اتصالی پست
                        PostContact Contact = Data.PostContactDB.GetPostContactByID(oldAssignmentInfo.PostContactID ?? 0);
                        Contact.Status = (byte)DB.PostContactStatus.Free;
                        Contact.Detach();
                        DB.Save(Contact);

                        // آزاد سازی بوخت  
                        oldBucht.Status = (byte)DB.BuchtStatus.Free;
                        oldBucht.ConnectionID = null;
                        oldBucht.Detach();
                        DB.Save(oldBucht);
                    }

                    // reserve postcontact
                    PostContact ContactItem = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                    ContactItem.Status = (byte)DB.PostContactStatus.FullBooking;
                    ContactItem.Detach();
                    DB.Save(ContactItem);

                    // reserve bucht
                    Bucht buchtItem = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID);
                    buchtItem.Status = (byte)DB.BuchtStatus.Reserve;
                    buchtItem.ConnectionID = _InvestigatePossibility.PostContactID;
                    buchtItem.Detach();
                    DB.Save(buchtItem);



                    _InvestigatePossibility.ConnectionReserveDate = DB.GetServerDate();
                    _InvestigatePossibility.Comment = CommentTextBox.Text;
                    _InvestigatePossibility.RequestID = _request.ID;
                    _InvestigatePossibility.Detach();
                    DB.Save(_InvestigatePossibility);
                }

                _request.Detach();
                DB.Save(_request);

                _specialWire.Detach();
                DB.Save(_specialWire);

                ts.Complete();
            }
        }

        private void ChangeLocationSpecialWireSave()
        {

            using (TransactionScope ts = new TransactionScope())
            {

                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;



                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;
                assingmentInfo = ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;

                if (assingmentInfo == null)
                {
                    throw new Exception("لطفا یک اتصالی را انتخاب کنید");
                }

                if (assingmentInfo.PostContactType == (byte)DB.PostContactConnectionType.PCMNormal)
                {
                    throw new Exception("امکان برقراری اتصال بر روی اتصالی پی سی ام نیست");
                }


                if (ConnectionBuchtComboBox.SelectedValue != null)
                {
                    _changeLocationSpecialWire.NewOtherBuchtID = (long)ConnectionBuchtComboBox.SelectedValue;
                }
                else
                {
                    _changeLocationSpecialWire.NewOtherBuchtID = null;
                }


                if (_changeLocationSpecialWire.OldOtherBuchtID != _changeLocationSpecialWire.NewOtherBuchtID)
                {
                    if (_changeLocationSpecialWire.OldOtherBuchtID != null)
                    {
                        Bucht oldOtherBucht = Data.BuchtDB.GetBuchetByID(_changeLocationSpecialWire.OldOtherBuchtID);
                        oldOtherBucht.Status = (byte)DB.BuchtStatus.Free;
                        oldOtherBucht.Detach();
                        DB.Save(oldOtherBucht);
                    }


                    if (_changeLocationSpecialWire.NewOtherBuchtID != null)
                    {
                        Bucht newOtherBucht = Data.BuchtDB.GetBuchetByID(_changeLocationSpecialWire.NewOtherBuchtID);
                        newOtherBucht.Status = (byte)DB.BuchtStatus.Reserve;
                        newOtherBucht.Detach();
                        DB.Save(newOtherBucht);


                    }


                }


                // Free Source reserv Bucht
                if (oldBucht != null && _specialCondition != null && _specialCondition.ChangeLocationInsider != true)
                {
                    // آزاد سازی اتصالی پست
                    PostContact Contact = Data.PostContactDB.GetPostContactByID(oldAssignmentInfo.PostContactID ?? 0);
                    Contact.Status = (byte)DB.PostContactStatus.Free;
                    Contact.Detach();
                    DB.Save(Contact);

                    // آزاد سازی بوخت  
                    oldBucht.Status = (byte)DB.BuchtStatus.Free;
                    oldBucht.ConnectionID = null;
                    oldBucht.BuchtIDConnectedOtherBucht = null;
                    oldBucht.Detach();
                    DB.Save(oldBucht);
                }

                Status Status = Data.StatusDB.GetStatueByStatusID((int)InvestigateStatusComboBox.SelectedValue);
                if (Status.StatusType == (byte)DB.RequestStatusType.ChangeTheLocationItself)
                {


                    if (_specialCondition == null)
                    {
                        _specialCondition = new SpecialCondition();
                        _specialCondition.RequestID = _request.ID;
                        _specialCondition.ChangeLocationInsider = true;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, true);
                    }
                    else
                    {
                        _specialCondition.RequestID = _request.ID;
                        _specialCondition.ChangeLocationInsider = true;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, false);
                    }

                    // اگر کافو عوض نشود و مرکزی عوض نشوند امکان تغییر مکان خودی می باشد. اگر تغییر مکان خودی انتخاب شود مرکزی قبلی برای دخواست درنظر گرفته می شود
                    AssignmentInfo itselfAssignmentInfo = DB.GetAllInformationByTelephoneNo(_request.TelephoneNo ?? 0);

                    if (itselfAssignmentInfo.PostContactID != assingmentInfo.PostContactID)
                    {
                        if (_InvestigatePossibility == null && _InvestigatePossibility.PostContactID == null)
                        {
                            if (!(assingmentInfo.PostContactStatus == (Byte)DB.PostContactStatus.Free))
                                throw new Exception("لطفا یک اتصالی آزاد را انتخاب کنید");
                        }
                        else
                        {
                            if ((assingmentInfo.PostContactStatus != (Byte)DB.PostContactStatus.Free && _InvestigatePossibility.PostContactID != assingmentInfo.PostContactID))
                                throw new Exception("لطفا یک اتصالی آزاد را انتخاب کنید");
                        }
                    }

                    if (cabinet.ID == itselfAssignmentInfo.CabinetID)
                    {
                        if (_InvestigatePossibility.PostContactID != null)
                        {
                            // reserve postcontact
                            PostContact ContactItem = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                            ContactItem.Status = (byte)DB.PostContactStatus.Free;
                            ContactItem.Detach();
                            DB.Save(ContactItem);
                        }
                        _InvestigatePossibility.PostContactID = assingmentInfo.PostContactID;
                        _InvestigatePossibility.BuchtID = itselfAssignmentInfo.BuchtID;

                        // تغییر مکان خودی برای مرکز یکسان با تغییر اتصالی پست و بدون تغییر اتصالی پست امکان پذیر است. 
                        // اگر اتصالی پست عوض شود اتصالی پست به حالت رزرو می رود
                        if (assingmentInfo.PostContactID != itselfAssignmentInfo.PostContactID)
                        {
                            // reserve postcontact
                            PostContact ContactItem = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                            ContactItem.Status = (byte)DB.PostContactStatus.FullBooking;
                            ContactItem.Detach();
                            DB.Save(ContactItem);
                        }

                    }
                    else
                    {
                        throw new Exception("برای تغییر مکان خودی باید اتصالی همان تلفن را انتخاب کنید");
                    }



                }
                else
                {
                    if (oldAssignmentInfo != null && oldAssignmentInfo.PostContactID != assingmentInfo.PostContactID)
                    {
                        if (!(assingmentInfo.PostContactStatus == (Byte)DB.PostContactStatus.Free))
                            throw new Exception("لطفا یک اتصالی خالی را انتخاب کنید");
                    }


                    if (_specialCondition == null)
                    {
                        _specialCondition = new SpecialCondition();
                        _specialCondition.RequestID = _request.ID;
                        _specialCondition.ChangeLocationInsider = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, true);
                    }
                    else
                    {
                        _specialCondition.RequestID = _request.ID;
                        _specialCondition.ChangeLocationInsider = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, false);
                    }

                    if (bucht == null)
                    {
                        throw new Exception("لطفا ورودی را انتخاب کنید");
                    }

                    if (bucht.BuchtTypeID != (int)DB.BuchtType.CustomerSide)
                    {
                        throw new Exception("فقط نوع بوخت طرف مشترک قابلیت اتصال سیم خصوصی دارد");
                    }


                    _InvestigatePossibility.PostContactID = assingmentInfo.PostContactID;
                    _InvestigatePossibility.BuchtID = bucht.ID;

                    // reserve postcontact
                    PostContact ContactItem = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                    ContactItem.Status = (byte)DB.PostContactStatus.FullBooking;
                    ContactItem.Detach();
                    DB.Save(ContactItem);

                    // reserve bucht
                    Bucht buchtItem = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID);
                    buchtItem.Status = (byte)DB.BuchtStatus.Reserve;
                    buchtItem.BuchtIDConnectedOtherBucht = _changeLocationSpecialWire.NewOtherBuchtID;
                    buchtItem.ConnectionID = _InvestigatePossibility.PostContactID;
                    buchtItem.Detach();
                    DB.Save(buchtItem);
                }
                _request.Detach();
                DB.Save(_request);


                _changeLocationSpecialWire.Detach();
                DB.Save(_changeLocationSpecialWire);



                _InvestigatePossibility.ConnectionReserveDate = DB.GetServerDate();
                _InvestigatePossibility.Comment = CommentTextBox.Text;
                _InvestigatePossibility.RequestID = _request.ID;
                _InvestigatePossibility.Detach();
                DB.Save(_InvestigatePossibility);

                ts.Complete();
            }
        }

        void DayriSave()
        {
            if (InvestigateStatusComboBox.SelectedValue == null)
            {
                throw new Exception("وضعیت انتخاب نشده است.");
            }

            if (ConnectionDataGrid.SelectedItem != null)
            {
                // Get abone information
                CRM.Data.AssignmentInfo item = ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;

                // Check correctness inforamtion
                if (_InvestigatePossibility.ID != 0)
                {
                    if (_InvestigatePossibility.PostContactID != item.PostContactID)
                    {
                        if (!(item.PostContactStatus == (Byte)DB.PostContactStatus.Free))
                            throw new Exception("لطفا یک اتصالی آزاد را انتخاب کنید");
                    }
                }
                else
                {

                    if (!(item.PostContactStatus == (Byte)DB.PostContactStatus.Free))
                        throw new Exception("لطفا یک اتصالی آزاد را انتخاب کنید");
                }

                // Verify markazi information
                if (CabinetComboBox.SelectedValue != null && item.PostContactType != (byte)DB.PostContactConnectionType.PCMNormal)
                    item.CabinetID = (int)CabinetComboBox.SelectedValue;


                // Verify Be PCM postContact
                if (item.PostContactType == (byte)DB.PostContactConnectionType.PCMNormal)
                {
                    _InvestigatePossibility.BuchtID = item.BuchtID;
                }
                else
                {
                    if (bucht == null)
                    {
                        throw new Exception("لطفا ورودی را انتخاب کنید");
                    }
                    _InvestigatePossibility.BuchtID = bucht.ID;
                }

                _InvestigatePossibility.PostContactID = item.PostContactID;

                if (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet || cabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL)
                {
                    if (_specialCondition == null)
                    {
                        _specialCondition = new SpecialCondition();
                        _specialCondition.RequestID = _request.ID;
                        _specialCondition.IsOpticalCabinet = true;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, true);
                    }
                    else
                    {
                        _specialCondition.RequestID = _request.ID;
                        _specialCondition.IsOpticalCabinet = true;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, false);
                    }
                }
                else
                {
                    if (_specialCondition == null)
                    {
                        _specialCondition = new SpecialCondition();
                        _specialCondition.RequestID = _request.ID;
                        _specialCondition.IsOpticalCabinet = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, true);
                    }
                    else
                    {
                        _specialCondition.RequestID = _request.ID;
                        _specialCondition.IsOpticalCabinet = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, false);
                    }
                }


                _InvestigatePossibility.ConnectionReserveDate = DB.GetServerDate();
                _InvestigatePossibility.Comment = CommentTextBox.Text;
                _InvestigatePossibility.RequestID = _request.ID;
                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;


                if (_id == 0)
                {
                    InvestigatePossibilityDB.SaveInvestigate(_request, _InvestigatePossibility, bucht, item);

                    _id = _InvestigatePossibility.ID;
                }

                else if (_id != 0)
                {
                    InvestigatePossibilityDB.UpdateInvestigate(_request, _InvestigatePossibility, bucht, oldPostContact, oldBucht, item, oldAssignmentInfo, false);
                }

                Load();



            }
            else
            {
                Folder.MessageBox.ShowInfo("هیچ اتصالی از پست انتخاب نشده است");
                throw new Exception("هیچ اتصالی از پست انتخاب نشده است");
            }

        }

        private void E1Save()
        {
            if (_e1Link != null)
            {


                assingmentInfo = ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;
                Bucht otherBucht = new Bucht();
                if (assingmentInfo == null)
                {
                    throw new Exception("لطفا یک اتصالی را انتخاب کنید");
                }

                if (oldAssignmentInfo != null)
                {
                    if (oldAssignmentInfo.PostContactID != assingmentInfo.PostContactID)
                    {
                        if (!(assingmentInfo.PostContactStatus == (Byte)DB.PostContactStatus.Free))
                            throw new Exception("لطفا یک اتصالی آزاد را انتخاب کنید");
                    }
                }
                else
                {

                    if (!(assingmentInfo.PostContactStatus == (Byte)DB.PostContactStatus.Free))
                        throw new Exception("لطفا یک اتصالی آزاد را انتخاب کنید");
                }

                //if (ConnectionBuchtComboBox.SelectedValue == null)
                //{
                //    throw new Exception("لطفا بوخت دیگر در انتخاب نمایید");
                //}

                if (bucht == null)
                {
                    throw new Exception("لطفا مرکزی را انتخاب کنید ");
                }



                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;
                if (assingmentInfo.PostContactType == (byte)DB.PostContactConnectionType.PCMNormal)
                {
                    throw new Exception("امکان برقراری اتصال بر روی اتصالی پی سی ام نیست");
                }

                if (cabinet.CabinetTypeID == (byte)DB.CabinetUsageType.OpticalCabinet)
                {
                    throw new Exception("امکان برقراری اتصال بر روی کافو نوری نیست");
                }

                if (ConnectionBuchtComboBox.SelectedValue != null)
                {
                    otherBucht = Data.BuchtDB.GetBuchtByID((long)ConnectionBuchtComboBox.SelectedValue);
                }
                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;

                _InvestigatePossibility.BuchtID = bucht.ID;
                _InvestigatePossibility.PostContactID = assingmentInfo.PostContactID;
                _InvestigatePossibility.ConnectionReserveDate = DB.GetServerDate();
                _InvestigatePossibility.Comment = CommentTextBox.Text;
                _InvestigatePossibility.RequestID = _request.ID;




                if (oldAssignmentInfo != null)
                {
                    InvestigatePossibilityDB.E1Update(oldAssignmentInfo, assingmentInfo, _e1, _request, bucht, oldBucht, _e1Link, _oldOtherBucht, otherBucht, _InvestigatePossibility);
                }
                else
                {
                    InvestigatePossibilityDB.E1Save(assingmentInfo, _e1, _request, bucht, _e1Link, otherBucht, _InvestigatePossibility);
                }


            }
            else
            {
                //if (_e1.CableDesignFileID != null && _FileID != Guid.Empty && _e1.CableDesignFileID != _FileID)
                //{
                //    Guid temp = (Guid)_e1.CableDesignFileID;
                //    _e1.CableDesignFileID = null;
                //    _e1.Detach();
                //    DB.Save(_e1, false);

                //    DocumentsFileDB.DeleteDocumentsFileTable(temp);
                //    _e1.CableDesignFileID = _FileID;

                //}
                //else if (_FileID != Guid.Empty)
                //{
                //    _e1.CableDesignFileID = _FileID;
                //}

                _e1.Detach();
                DB.Save(_e1);
            }

        }

        void ChangeLocationSave()
        {

            using (TransactionScope ts = new TransactionScope())
            {


                SpecialCondition specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_request.ID);
                if (ConnectionDataGrid.SelectedItem != null)
                {
                    if (InvestigateStatusComboBox.SelectedValue != null)
                    {
                        Bucht reserveBucht = new Bucht();

                        Telephone OldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(changeLocation.OldTelephone ?? 0);
                        Bucht Oldbucht = Data.BuchtDB.GetBuchetByID(changeLocation.OldBuchtID);
                        assingmentInfo = ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;
                        _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;
                        byte passChangeLocationType = (byte)changeLocation.ChangeLocationTypeID;

                        if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside)
                        {
                            changeLocation.ChangeLocationTypeID = (byte?)DB.ChangeLocationCenterType.InSideCenter;
                        }
                        else if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterToCenter)
                        {
                            changeLocation.ChangeLocationTypeID = (byte?)DB.ChangeLocationCenterType.CenterToCenter;
                        }


                        // varify itself change location
                        // if change location is itself then or not will change post contact or just will change post contact
                        Status Status = Data.StatusDB.GetStatueByStatusID((int)InvestigateStatusComboBox.SelectedValue);
                        if (Status.StatusType == (byte)DB.RequestStatusType.ChangeTheLocationItself)
                        {


                            AssignmentInfo OldTelephoneAssignmentInfo = DB.GetAllInformationByTelephoneNo(changeLocation.OldTelephone ?? 0);
                            if ((assingmentInfo.PostContactID != OldTelephoneAssignmentInfo.PostContactID) && (assingmentInfo.PostContactType == (byte)DB.PostContactConnectionType.PCMNormal || OldTelephoneAssignmentInfo.PostContactType == (byte)DB.PostContactConnectionType.PCMNormal))
                            {
                                throw new Exception("چون در حالت اتصال به پی سی ام جابجایی رانژه تلفن به بوخت پی سی ام نیاز می باشد تغییر مکان خودی برای پی سی ام امکانپذیر نیست.");
                            }
                            if (cabinet.ID != OldTelephoneAssignmentInfo.CabinetID)
                            {
                                throw new Exception("در تغییر مکان خودی امکان تغییر کافو نیست.");
                            }

                            // realase reserved post contact and bucht
                            if (_InvestigatePossibility.ID != 0 && _InvestigatePossibility.PostContactID != null && _InvestigatePossibility.PostContactID != changeLocation.OldPostContactID)
                            {
                                PostContact Contact = Data.PostContactDB.GetPostContactByID((long)_InvestigatePossibility.PostContactID);

                                if (Contact != null)
                                {

                                    Contact.Status = (byte)DB.PostContactStatus.Free;
                                    Contact.Detach();
                                    DB.Save(Contact);

                                }
                            }

                            if (_InvestigatePossibility.ID != 0 && _InvestigatePossibility.BuchtID != null && _InvestigatePossibility.BuchtID != changeLocation.OldBuchtID)
                            {
                                Bucht bucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
                                if (bucht != null)
                                {
                                    if (bucht.BuchtTypeID == (byte)DB.BuchtType.OutLine || bucht.BuchtTypeID == (byte)DB.BuchtType.InLine)
                                    {
                                        bucht.Status = (byte)DB.BuchtStatus.Free;
                                        bucht.Detach();
                                        DB.Save(bucht);
                                    }
                                    else
                                    {
                                        bucht.Status = (byte)DB.BuchtStatus.Free;
                                        bucht.ConnectionID = null;
                                        bucht.Detach();
                                        DB.Save(bucht);
                                    }
                                }

                            }

                            if (OldTelephoneAssignmentInfo.PostContactID == assingmentInfo.PostContactID)
                            {
                                _InvestigatePossibility.PostContactID = OldTelephoneAssignmentInfo.PostContactID;
                            }
                            else
                            {
                                _InvestigatePossibility.PostContactID = assingmentInfo.PostContactID;

                                // reseve post contact
                                PostContact Contact = Data.PostContactDB.GetPostContactByID((long)assingmentInfo.PostContactID);

                                if (Contact != null)
                                {

                                    Contact.Status = (byte)DB.PostContactStatus.FullBooking;
                                    Contact.Detach();
                                    DB.Save(Contact);

                                }
                                else
                                {
                                    throw new Exception("اتصالی پست یافت نشد.");
                                }
                            }

                            // if changed post contect save new information with old information and save new post contect
                            _InvestigatePossibility.BuchtID = OldTelephoneAssignmentInfo.BuchtID;

                            changeLocation.ChangeLocationTypeID = (int)DB.ChangeLocationCenterType.itself;
                            changeLocation.Detach();
                            DB.Save(changeLocation);



                            _request.Detach();
                            DB.Save(_request, false);

                            _InvestigatePossibility.ConnectionReserveDate = DB.GetServerDate();
                            _InvestigatePossibility.Comment = CommentTextBox.Text;
                            _InvestigatePossibility.RequestID = _request.ID;
                            _InvestigatePossibility.Detach();
                            DB.Save(_InvestigatePossibility);

                            if (specialCondition == null)
                            {
                                specialCondition = new SpecialCondition();
                                specialCondition.RequestID = _request.ID;
                                specialCondition.ChangeLocationInsider = true;
                                specialCondition.Detach();
                                DB.Save(specialCondition, true);
                            }
                            else
                            {
                                specialCondition.RequestID = _request.ID;
                                specialCondition.ChangeLocationInsider = true;
                                specialCondition.Detach();
                                DB.Save(specialCondition, false);
                            }

                            ts.Complete();
                            return;
                        }

                        if (specialCondition == null)
                        {
                            specialCondition = new SpecialCondition();
                            specialCondition.RequestID = _request.ID;
                            specialCondition.ChangeLocationInsider = false;
                            specialCondition.Detach();
                            DB.Save(specialCondition, true);
                        }
                        else
                        {
                            specialCondition.RequestID = _request.ID;
                            specialCondition.ChangeLocationInsider = false;
                            specialCondition.Detach();
                            DB.Save(specialCondition, false);
                        }


                        if (oldAssignmentInfo != null)
                        {
                            if (oldAssignmentInfo.PostContactID != assingmentInfo.PostContactID)
                            {
                                if (!(assingmentInfo.PostContactStatus == (Byte)DB.PostContactStatus.Free))
                                    throw new Exception("لطفا یک اتصالی آزاد را انتخاب کنید");
                            }
                        }
                        else
                        {

                            if (!(assingmentInfo.PostContactStatus == (Byte)DB.PostContactStatus.Free))
                                throw new Exception("لطفا یک اتصالی آزاد را انتخاب کنید");
                        }


                        //if (oldAssignmentInfo != null && oldAssignmentInfo.PostContactID != assingmentInfo.PostContactID && !(assingmentInfo.PostContactStatus == (Byte)DB.PostContactStatus.Free))
                        //{
                        //    throw new Exception("لطفا یک اتصالی خالی را انتخاب کنید");
                        //}

                        // bucht of pcm is Knows
                        if (assingmentInfo.PostContactType == (byte)DB.PostContactConnectionType.PCMNormal)
                        {
                            bucht = Data.BuchtDB.GetBuchetByID(assingmentInfo.BuchtID);
                        }
                        else
                        {
                            if (bucht == null)
                            {
                                throw new Exception("لطفا ورودی را انتخاب کنید");
                            }
                            assingmentInfo.BuchtID = bucht.ID;
                        }

                        if ((bucht.Status != (byte)DB.BuchtStatus.Free) && (bucht.Status != (byte)DB.BuchtStatus.ConnectedToPCM) && (oldAssignmentInfo != null && oldAssignmentInfo.BuchtID != assingmentInfo.BuchtID))
                        {
                            throw new Exception("امکان استفاده از بوخت نمی باشد");
                        }

                        //if ((byte?)SourceTypecomboBox.SelectedValue == (byte)DB.SourceType.SpecialCables)
                        //    bucht.ConnectionIDBucht = bucht.ID;

                        _InvestigatePossibility.BuchtID = bucht.ID;
                        _InvestigatePossibility.PostContactID = assingmentInfo.PostContactID;
                        _InvestigatePossibility.ConnectionReserveDate = DB.GetServerDate();
                        _InvestigatePossibility.Comment = CommentTextBox.Text;
                        _InvestigatePossibility.RequestID = _request.ID;


                        if (assingmentInfo.BuchtStatus != (byte)DB.BuchtStatus.Free || assingmentInfo.BuchtStatus != (byte)DB.BuchtStatus.ConnectedToPCM)
                        {
                            if (Mode == false)
                            {
                                AssignmentDB.Save(assingmentInfo, changeLocation, _request, bucht, _InvestigatePossibility);
                            }
                            else
                            {
                                AssignmentDB.Updata(oldAssignmentInfo, assingmentInfo, changeLocation, _request, passChangeLocationType, _InvestigatePossibility);
                            }

                            Load();



                        }
                        else
                        {

                            throw new Exception("لطفا یک اتصالی خالی را انتخاب کنید");
                        }
                    }
                    else
                    {

                        throw new Exception("وضعیت انتخاب نشده است");
                    }
                }
                else
                {
                    throw new Exception("اتصالی انتخاب نشده است");
                }
                ts.Complete();


                ShowSuccessMessage("ذخیره انجام شد");
            }


        }

        #endregion

        #region RejectMethods

        void DayriDelete()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                // item use to parameter UpdateInvestigate not change data with it
                CRM.Data.AssignmentInfo item = ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;
                if (oldAssignmentInfo != null)
                {

                    Telephone telItem = Data.TelephoneDB.GetTelephoneNoBySwitchPortID(bucht.SwitchPortID ?? 0);

                    if (telItem != null)
                    {
                        // reserve telephone
                        telItem.Status = (byte)DB.TelephoneStatus.Free;
                        telItem.InstallAddressID = null;
                        telItem.CorrespondenceAddressID = null;
                        telItem.CustomerID = null;
                        telItem.Detach();
                        DB.Save(telItem);
                    }

                    if (oldBucht != null)
                    {
                        oldBucht.SwitchPortID = null;
                    }


                    if (_specialCondition == null)
                    {
                        _specialCondition = new SpecialCondition();
                        _specialCondition.RequestID = _request.ID;
                        _specialCondition.IsOpticalCabinet = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, true);
                    }
                    else
                    {
                        _specialCondition.RequestID = _request.ID;
                        _specialCondition.IsOpticalCabinet = false;
                        _specialCondition.Detach();
                        DB.Save(_specialCondition, false);
                    }


                    _request.TelephoneNo = null;



                    _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;
                    _request.TelephoneNo = null;
                    _request.Detach();
                    DB.Save(_request);
                    InvestigatePossibilityDB.UpdateInvestigate(_request, _InvestigatePossibility, bucht, oldPostContact, oldBucht, item, oldAssignmentInfo, true);

                }

                IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByRequestID(_request.ID);
                if (issueWiring != null)
                {
                    Data.InstallLineDB.DeleteInstallLineByissueWiringID(issueWiring.ID);

                    Data.WiringDB.DeleteWiringByissueWiringID(issueWiring.ID);

                    DB.Delete<IssueWiring>(issueWiring.ID);
                }

                if (_InvestigatePossibility.ID != 0)
                    DB.Delete<InvestigatePossibility>(_InvestigatePossibility.ID);
                ts.Complete();
            }

        }

        private void ChangeLocationDelete()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {

                if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterToCenter)
                    _request.CenterID = (int)changeLocation.SourceCenter;
                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;

                _request.Detach();
                DB.Save(_request);

                if (changeLocation.ChangeLocationTypeID != (byte)DB.ChangeLocationCenterType.itself)
                {
                    // Release Bucht
                    if (_InvestigatePossibility.BuchtID != null)
                    {
                        Bucht reservebucht = Data.BuchtDB.GetBuchetByID(_InvestigatePossibility.BuchtID);
                        reservebucht.Status = (byte)DB.BuchtStatus.Free;
                        reservebucht.ConnectionID = null;
                        reservebucht.Detach();
                        DB.Save(reservebucht);

                        _InvestigatePossibility.BuchtID = null;
                    }

                    if (_InvestigatePossibility.PostContactID != null)
                    {
                        PostContact oldPostContact = Data.PostContactDB.GetPostContactByID((long)_InvestigatePossibility.PostContactID);
                        oldPostContact.Status = (byte)DB.PostContactStatus.Free;
                        oldPostContact.Detach();
                        DB.Save(oldPostContact);
                        _InvestigatePossibility.PostContactID = null;
                    }

                }
                else
                {
                    AssignmentInfo OldTelephoneAssignmentInfo = DB.GetAllInformationByTelephoneNo(changeLocation.OldTelephone ?? 0);
                    if (OldTelephoneAssignmentInfo.PostContactID == _InvestigatePossibility.PostContactID)
                    {
                        _InvestigatePossibility.PostContactID = null;
                    }
                    else
                    {


                        // reseve post contact
                        PostContact Contact = Data.PostContactDB.GetPostContactByID((long)_InvestigatePossibility.PostContactID);

                        if (Contact != null)
                        {

                            Contact.Status = (byte)DB.PostContactStatus.Free;
                            Contact.Detach();
                            DB.Save(Contact);

                        }
                        _InvestigatePossibility.PostContactID = null;
                    }
                    _InvestigatePossibility.BuchtID = null;
                }

                if (_request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterInside)
                {
                    changeLocation.ChangeLocationTypeID = (int)DB.ChangeLocationCenterType.InSideCenter;
                }
                else if (_request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterToCenter)
                {
                    changeLocation.ChangeLocationTypeID = (int)DB.ChangeLocationCenterType.CenterToCenter;
                }

                changeLocation.Detach();
                DB.Save(changeLocation);

                SpecialCondition specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(_request.ID);
                if (specialCondition != null)
                {
                    specialCondition.RequestID = _request.ID;
                    specialCondition.NotEqualityOfBuchtType = false;
                    specialCondition.EqualityOfBuchtTypeCusromerSide = false;
                    specialCondition.EqualityOfBuchtTypeOptical = false;
                    specialCondition.NotEqualityOfBuchtTypeOptical = false;
                    specialCondition.Detach();
                    DB.Save(specialCondition, false);
                }

                if (_InvestigatePossibility.ID != 0)
                    DB.Delete<InvestigatePossibility>(_InvestigatePossibility.ID);
                ts.Complete();
            }

        }

        private void ChangeLocationSpecialWireDelete()
        {
            using (TransactionScope parentTransactionScope = new TransactionScope())
            {


                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;

                _InvestigatePossibility.PostContactID = null;
                _InvestigatePossibility.BuchtID = null;

                if (_changeLocationSpecialWire.OldOtherBuchtID != null)
                {
                    Bucht oldOtherBucht = Data.BuchtDB.GetBuchetByID(_changeLocationSpecialWire.OldOtherBuchtID);
                    oldOtherBucht.Status = (byte)DB.BuchtStatus.Connection;
                    oldOtherBucht.Detach();
                    DB.Save(oldOtherBucht);
                }
                if (_changeLocationSpecialWire.NewOtherBuchtID != null)
                {
                    Bucht newOtherBucht = Data.BuchtDB.GetBuchetByID(_changeLocationSpecialWire.NewOtherBuchtID);
                    newOtherBucht.Status = (byte)DB.BuchtStatus.Free;
                    newOtherBucht.Detach();
                    DB.Save(newOtherBucht);

                }

                //


                // Free Source reserv Bucht
                if (oldBucht != null)
                {
                    // آزاد سازی اتصالی پست
                    PostContact Contact = Data.PostContactDB.GetPostContactByID(oldAssignmentInfo.PostContactID ?? 0);
                    Contact.Status = (byte)DB.PostContactStatus.Free;
                    Contact.Detach();
                    DB.Save(Contact);

                    // آزاد سازی بوخت  
                    oldBucht.Status = (byte)DB.BuchtStatus.Free;
                    oldBucht.ConnectionID = null;
                    oldBucht.BuchtIDConnectedOtherBucht = _changeLocationSpecialWire.OldOtherBuchtID;
                    oldBucht.Detach();
                    DB.Save(oldBucht);
                }

                _InvestigatePossibility.Detach();
                DB.Save(_InvestigatePossibility);
                parentTransactionScope.Complete();
            }

            //    using (TransactionScope parentTransactionScope = new TransactionScope())
            //    {

            //        _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;

            //        // اگر مرکز مبدا و مقصد یک مرکز باشند
            //        if (_privateWire.TargetCenter == null || _privateWire.SourceCenter == _privateWire.TargetCenter)
            //        {

            //            // اطلاعات ابونه نقطه مقصد توسط باز شدن همین فرم دریافت میشود
            //            _privateWire.SourcePostContactID  = null;
            //            _privateWire.SourceCabinetInputID = null;
            //            _privateWire.SourceBuchtID = null;
            //            _privateWire.TargetBuchtID = null;
            //            //


            //            // Free Source reserv Bucht
            //            if (oldBucht != null)
            //            {
            //                // آزاد سازی اتصالی پست
            //                PostContact Contact = Data.PostContactDB.GetPostContactByID(oldAssignmentInfo.PostContactID ?? 0);
            //                Contact.Status = (byte)DB.PostContactStatus.Free;
            //                Contact.Detach();
            //                DB.Save(Contact);

            //                // آزاد سازی بوخت  
            //                oldBucht.Status = (byte)DB.BuchtStatus.Free;
            //                oldBucht.ConnectionID = null;
            //                oldBucht.Detach();
            //                DB.Save(oldBucht);
            //            }

            //            // Free target reserv bucht
            //            if (oldTargetBucht != null)
            //            {
            //                // آزاد سازی اتصالی پست
            //                PostContact Contact = Data.PostContactDB.GetPostContactByID(oldTargetAssignmentInfo.PostContactID ?? 0);
            //                Contact.Status = (byte)DB.PostContactStatus.Free;
            //                Contact.Detach();
            //                DB.Save(Contact);

            //                // آزاد سازی بوخت  
            //                oldTargetBucht.Status = (byte)DB.BuchtStatus.Free;
            //                oldTargetBucht.ConnectionID = null;
            //                oldTargetBucht.Detach();
            //                DB.Save(oldTargetBucht);
            //            }

            //            _request.Detach();
            //            DB.Save(_request);


            //            _privateWire.Detach();
            //            DB.Save(_privateWire);



            //        }
            //        // اگر درخواست در مبدا است
            //        else if (_request.CenterID == _privateWire.SourceCenter)
            //        {
            //            _privateWire.SourcePostContactID = null ;
            //            _privateWire.SourceCabinetInputID = null;
            //            _privateWire.SourceBuchtID = null;
            //            if (oldBucht != null)
            //            {
            //                // آزاد سازی اتصالی پست
            //                PostContact Contact = Data.PostContactDB.GetPostContactByID(oldAssignmentInfo.PostContactID ?? 0);
            //                Contact.Status = (byte)DB.PostContactStatus.Free;
            //                Contact.Detach();
            //                DB.Save(Contact);

            //                // آزاد سازی بوخت  
            //                oldBucht.Status = (byte)DB.BuchtStatus.Free;
            //                oldBucht.ConnectionID = null;
            //                oldBucht.Detach();
            //                DB.Save(oldBucht);
            //            }
            //        }
            //        // اگر درخواست در مقصد
            //        else if (_request.CenterID == _privateWire.TargetCenter)
            //        {

            //            _privateWire.TargetPostContactID = null;
            //            _privateWire.TargetCabinetInputID = null;
            //            _privateWire.TargetBuchtID = null;

            //            if (oldBucht != null)
            //            {
            //                // آزاد سازی اتصالی پست
            //                PostContact Contact = Data.PostContactDB.GetPostContactByID(oldAssignmentInfo.PostContactID ?? 0);
            //                Contact.Status = (byte)DB.PostContactStatus.Free;
            //                Contact.Detach();
            //                DB.Save(Contact);

            //                // آزاد سازی بوخت  
            //                oldBucht.Status = (byte)DB.BuchtStatus.Free;
            //                oldBucht.ConnectionID = null;
            //                oldBucht.Detach();
            //                DB.Save(oldBucht);

            //            }

            //        }

            //        parentTransactionScope.Complete();




            //    }


        }

        private void SpecialWireDelete()
        {
            using (TransactionScope parentTransactionScope = new TransactionScope())
            {

                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;

                if (_specialWire.OtherBuchtID != null)
                {
                    Bucht otherBucht = BuchtDB.GetBuchtByID((long)_specialWire.OtherBuchtID);
                    otherBucht.Status = (byte)DB.BuchtStatus.Free;
                    otherBucht.Detach();
                    DB.Save(otherBucht);
                }


                if (_specialWire.SecondOtherBuchtID != null)
                {
                    Bucht SecondOtherBuchtID = BuchtDB.GetBuchtByID((long)_specialWire.SecondOtherBuchtID);
                    SecondOtherBuchtID.Status = (byte)DB.BuchtStatus.Free;
                    SecondOtherBuchtID.Detach();
                    DB.Save(SecondOtherBuchtID);
                }

                _InvestigatePossibility.PostContactID = null;
                _InvestigatePossibility.BuchtID = null;

                _specialWire.SecondOtherBuchtID = null;
                _specialWire.OtherBuchtID = null;
                _specialWire.Detach();
                DB.Save(_specialWire);
                //




                // Free Source reserv Bucht
                if (oldBucht != null)
                {
                    // آزاد سازی اتصالی پست
                    PostContact Contact = Data.PostContactDB.GetPostContactByID(oldAssignmentInfo.PostContactID ?? 0);
                    Contact.Status = (byte)DB.PostContactStatus.Free;
                    Contact.Detach();
                    DB.Save(Contact);

                    // آزاد سازی بوخت  
                    oldBucht.Status = (byte)DB.BuchtStatus.Free;
                    oldBucht.ConnectionID = null;
                    oldBucht.Detach();
                    DB.Save(oldBucht);
                }

                _InvestigatePossibility.Detach();
                DB.Save(_InvestigatePossibility);

                //if (_InvestigatePossibility.ID != 0)
                //    DB.Delete<InvestigatePossibility>(_InvestigatePossibility.ID);
                parentTransactionScope.Complete();
            }

        }

        private void SpaceAndPowerReject()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request, false);

                _spaceAndPower.CableAndNetworkDesignOfficeComment = string.Empty;
                _spaceAndPower.CableAndNetworkDesignOfficeDate = null;
                _spaceAndPower.Detach();
                DB.Save(_spaceAndPower, false);


                //بعد از رد درخواست در این مرحله چنانچه فایلی برای آن ذخیره شده باشد باید حذف گردد
                //if (_cableDesignOffice != null && _cableDesignOffice.Count != 0)
                //{
                //    List<long> cableDesignOfficeIds = new List<long>();
                //    foreach (var item in _cableDesignOffice)
                //    {
                //        cableDesignOfficeIds.Add(item.ID);
                //    }
                //    if (cableDesignOfficeIds.Count != 0)
                //    {
                //        DB.DeleteAll<CableDesignOffice>(cableDesignOfficeIds);
                //    }
                //}
            }
        }

        private void E1Reject()
        {

            _request.StatusID = (int)InvestigateStatusComboBox.SelectedValue;


            List<InvestigatePossibility> investigatePossibilitys = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_request.ID);

            if (investigatePossibilitys.Count() != 0)
            {

                using (TransactionScope Subts = new TransactionScope(TransactionScopeOption.Required))
                {

                    investigatePossibilitys.ForEach(t =>
                    {
                        // رزرو اتصالی پست
                        PostContact Contact = Data.PostContactDB.GetPostContactByID(t.PostContactID ?? 0);
                        Bucht BuchtItem = Data.BuchtDB.GetBuchetByID(t.BuchtID ?? 0);
                        if (Contact.Status == (byte)DB.PostContactStatus.FullBooking)
                        {
                            Contact.Status = (byte)DB.PostContactStatus.Free;
                            Contact.Detach();
                            DB.Save(Contact);

                            // رزرو بوخت  
                            BuchtItem.Status = (byte)DB.BuchtStatus.Free;
                            BuchtItem.ConnectionID = null;
                            BuchtItem.Detach();
                            DB.Save(BuchtItem);

                            _e1.BuchtID = null;
                            _e1.ConnectionID = null;
                            _e1.Detach();
                            DB.Save(_e1, false);


                            t.PostContactID = null;
                            t.BuchtID = null;
                            t.Detach();
                            DB.Save(t);
                        }
                    });

                    DB.UpdateAll(investigatePossibilitys);

                    Subts.Complete();
                }
            }
        }

        #endregion

        #region EventHandlers

        private void RegVisitPlace(object sender, RoutedEventArgs e)
        {
            VisitPlacesForm windows = new VisitPlacesForm(_request.ID);
            windows._openedInInvestigateForm = true;
            windows.ShowDialog();
            Load();
        }

        private void SecondMDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecondMDFComboBox.SelectedValue != null)
                SecondConnectionColumnComboBox.ItemsSource = DB.GetConnectionColumnInfo((int)SecondMDFComboBox.SelectedValue);
        }

        private void SecondConnectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecondConnectionColumnComboBox.SelectedValue != null)
                SecondConnectionRowComboBox.ItemsSource = DB.GetConnectionRowInfo((int)SecondConnectionColumnComboBox.SelectedValue);
        }

        private void SecondConnectionRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecondConnectionRowComboBox.SelectedValue != null)
            {
                if (_oldSecondOtherBucht != null)

                    SecondConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)SecondConnectionRowComboBox.SelectedValue, true, buchtType).Union(new List<CheckableItem> { new CheckableItem { LongID = _oldSecondOtherBucht.ID, Name = _oldSecondOtherBucht.BuchtNo.ToString(), IsChecked = false } });
                    
                else

                    SecondConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)SecondConnectionRowComboBox.SelectedValue, true, buchtType);
            }
        }

        private void BuchtTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BuchtTypeComboBox.SelectedValue != null)
            {
                buchtType = (int)BuchtTypeComboBox.SelectedValue;
            }
        }

        private void ViewPost_Click(object sender, RoutedEventArgs e)
        {
            if (PostComboBox.SelectedValue != null)
            {
                CRM.Application.Views.PostForm postForm = new Views.PostForm((int)PostComboBox.SelectedValue);
                postForm.IsEnabled = false;
                postForm.ShowDialog();
            }
        }

        private void SourceTypecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Resetcomboboxes();
        }

        private void RemoveFromReserve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ConnectionDataGrid.SelectedItem != null)
                {
                    CRM.Data.AssignmentInfo item = ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;
                    PostContact postContect = Data.PostContactDB.GetPostContactByID(item.PostContactID ?? 0);


                    if (postContect.Status == (byte)DB.PostContactStatus.FullBooking)
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {

                            if (item.BuchtType == (byte)DB.BuchtType.OutLine || item.BuchtType == (byte)DB.BuchtType.InLine)
                            {
                                // رزرو اتصالی پست
                                PostContact Contact = Data.PostContactDB.GetPostContactByID(item.PostContactID ?? 0);
                                Contact.Status = (byte)DB.PostContactStatus.Free;
                                Contact.Detach();
                                DB.Save(Contact);

                                // رزرو بوخت. پی سی ام نیاز به تنظیم اتصالی پست ندارد  
                                Bucht buchtPCM = Data.BuchtDB.GetBuchetByID(item.BuchtID);
                                buchtPCM.Status = (byte)DB.BuchtStatus.Free;
                                buchtPCM.Detach();
                                DB.Save(buchtPCM);

                                PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByID(item.PCMPortIDInBuchtTable ?? 0);
                                PCMPort.Status = (byte)DB.PCMPortStatus.Empty;
                                PCMPort.Detach();
                                DB.Save(PCMPort);
                            }
                            else
                            {

                                if (_request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterInside && changeLocation.ChangeLocationTypeID == (byte)DB.ChangeLocationCenterType.itself)
                                {
                                    // رزرو اتصالی پست
                                    PostContact Contact = Data.PostContactDB.GetPostContactByID(item.PostContactID ?? 0);
                                    Contact.Status = (byte)DB.PostContactStatus.CableConnection;
                                    Contact.Detach();
                                    DB.Save(Contact);

                                    // رزرو بوخت  
                                    bucht.Status = (byte)DB.BuchtStatus.Connection;
                                    bucht.Detach();
                                    DB.Save(bucht);
                                }
                                else
                                {
                                    // رزرو اتصالی پست
                                    PostContact Contact = Data.PostContactDB.GetPostContactByID(item.PostContactID ?? 0);
                                    Contact.Status = (byte)DB.PostContactStatus.Free;
                                    Contact.Detach();
                                    DB.Save(Contact);

                                    // رزرو بوخت  
                                    bucht.Status = (byte)DB.BuchtStatus.Free;
                                    bucht.ConnectionID = null;
                                    bucht.Detach();
                                    DB.Save(bucht);
                                }
                            }

                            if (_InvestigatePossibility != null)
                            {
                                _InvestigatePossibility.BuchtID = null;
                                _InvestigatePossibility.PostContactID = null;
                                _InvestigatePossibility.Detach();
                                DB.Save(_InvestigatePossibility);
                            }

                            if (changeLocation != null)
                            {
                                changeLocation.Detach();
                                DB.Save(changeLocation);
                            }
                            ts.Complete();
                        }

                    }

                    Load();
                    //assingmentInfos = DB.GetAllInformationByPostIDAndWithOutpostContactType(0, (byte)DB.PostContactConnectionType.PCMRemote);
                    //ConnectionDataGrid.ItemsSource = assingmentInfos;
                    ConnectionDataGrid.ItemsSource = DB.GetAllInformationByPostIDAndWithOutpostContactType((int)PostComboBox.SelectedValue, (byte)DB.PostContactConnectionType.PCMRemote);

                    InputComboBox.SelectedIndex = -1;
                    MDFNoTextBox.Text = string.Empty;
                    BuchtNoTextBox.Text = string.Empty;

                }

                ShowSuccessMessage("حذف انجام شد");

            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف اطلاعات", ex);

            }

        }

        private void BoxNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
            this.ResizeWindow();
        }

        private void PostNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //    //EnumItem item = SourceTypecomboBox.SelectedItem as EnumItem;
            //    //Cabinet cabItem = BoxNoComboBox.SelectedItem as Cabinet;
            //    //if (cabItem == null) return;
            //    //Post postItem = PostNoComboBox.SelectedItem as Post;
            //    if (postItem == null) return;
            //    buchtList = AllbuchtListInfo.Where(b => b.BuchtTypeID == item.ID && b.CabinetID == cabItem.ID).ToList();
            //    List<int> cabIds = buchtList.Select(c => c.CabinetID??-1).ToList();
            //    cabinetList = AllcabinetList.Where(cab => cabIds.Contains(cabItem.ID)).ToList();
            //    postList = PostDB.GetPostsByCabinetID(cabIds);
            //    List<int> postIds = postList.Where(p => p.ID == postItem.ID).Select(p => p.ID).ToList();
            //    postContactList = AllpostContactList.Where(pc => postIds.Contains(pc.PostID) && pc.ConnectionType == item.ID).ToList();
            //}
        }

        private void PCMDeviceIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //PCMInfo item = PCMDeviceIDComboBox.SelectedItem as PCMInfo;
            //if (item != null)
            //{
            //    var x = DB.SearchByPropertyName<PCMDevice>("MUID", item.MUID).ToList();
            //    List<long> Ids = x.Select(p => p.BuchtID ?? -1).ToList();
            //    pcmChannelList = AllbuchtListInfo.Where(b => b.BuchtTypeID == 4 && Ids.Contains(b.BuchtID)).ToList();
            //}

        }

        private void ConnectionNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ConnectionDataGrid.SelectedItem != null)
                {
                    if (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL || cabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet)
                    {
                        Folder.MessageBox.ShowInfo("برقراری پی سی ام بر روی کافو نوری امکان پذیر نیست");
                    }
                    else
                    {

                        CRM.Data.AssignmentInfo item = ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;
                        if (CabinetComboBox.SelectedValue != null)

                            if ((item.PostContactStatus == (byte)DB.PostContactStatus.Free || item.PostContactStatus == (byte)DB.PostContactStatus.CableConnection) && (item.PostContactType != (byte)DB.PostContactConnectionType.PCMNormal) || (item.PostContactType != (byte)DB.PostContactConnectionType.PCMNormal))
                            {
                                if (CabinetComboBox.SelectedValue == null) { Folder.MessageBox.ShowInfo("لطفا یک ورودی انتخاب کنید"); return; }
                                item.CabinetID = (int)CabinetComboBox.SelectedValue;
                                if (item.PostContactStatus == (byte)DB.PostContactStatus.CableConnection)
                                {
                                    if (CRM.Data.ADSLPAPPortDB.HasADSLbyTelephoneNo((long)item.TelePhoneNo))
                                        throw new Exception("امکان نسب تلفن روی تلفن ADSL  نمی باشد.");
                                }
                                PCMAssignment window = new PCMAssignment(item, _request.CenterID);
                                window.ShowDialog();
                                if (window.DialogResult == true)
                                {

                                    CabinetComboBox_SelectionChanged(null, null);
                                    PostComboBox.SelectedValue = item.PostID;
                                    PostComboBox_SelectionChanged(null, null);
                                }
                            }
                            else
                            {
                                Folder.MessageBox.ShowInfo("اتصالی انتخاب شده صحیح نیست");
                            }
                    }

                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در انتساب", ex);
            }
        }

        private void InputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InputComboBox.SelectedValue != null)
            {

                BuchtInfo((long)InputComboBox.SelectedValue);
            }
        }

        #endregion

        # region PostInfo

        private void PostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (PostComboBox.SelectedValue != null)
            {
                assingmentInfos = DB.GetAllInformationByPostIDAndWithOutpostContactType((int)PostComboBox.SelectedValue, (byte)DB.PostContactConnectionType.PCMRemote);

                List<string> telephoneListOnPost = assingmentInfos.Where(t => t.TelePhoneNo != null).Select(t => t.TelePhoneNo.ToString()).ToList();

                List<CRM.Data.BillingServiceReference.DebtInfo> DebtList = CRM.Application.Codes.ServiceReference.GetDebtInfo(telephoneListOnPost);

                DebtList.ForEach(Item =>
                {
                    assingmentInfos.Where(t => t.TelePhoneNo == Convert.ToInt64(Item.PhoneNo)).SingleOrDefault().Debt = Item.DebtAmount;
                    assingmentInfos.Where(t => t.TelePhoneNo == Convert.ToInt64(Item.PhoneNo)).SingleOrDefault().LastPaidBillDate = Item.LastPaidBillDate;
                });

                AdjacentPostLabel.Visibility = Visibility.Visible;
                AdjacentPostTextBox.Visibility = Visibility.Visible;
                //WaittingImage.Visibility = Visibility.Collapsed;
                ConnectionDataGrid.Visibility = Visibility.Visible;
                AdjacentPostTextBox.Text = Data.PostDB.GetAdjacentPost((int)PostComboBox.SelectedValue);
                ConnectionDataGrid.ItemsSource = assingmentInfos;
            }
            else
            {
                if (assingmentInfos != null)
                    assingmentInfos.Clear();
            }
        }

        private void CabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CabinetComboBox.SelectedValue != null)
            {
                PostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)CabinetComboBox.SelectedValue);

                cabinet = Data.CabinetDB.GetCabinetByID((int)CabinetComboBox.SelectedValue);

                RemainedQuotaReservationLabel.Visibility = Visibility.Visible;
                RemainedQuotaReservationTextBox.Visibility = Visibility.Visible;

                RemainedQuotaReservationTextBox.Text = Data.CabinetDB.GetRemainedQuotaReservationByCabinetID((int)CabinetComboBox.SelectedValue).ToString();

                FillInput();

                if (assingmentInfos != null)
                {
                    assingmentInfos.Clear();
                    ConnectionDataGrid.ItemsSource = null;
                }
            }
            else
            {
                RemainedQuotaReservationLabel.Visibility = Visibility.Collapsed;
                RemainedQuotaReservationTextBox.Visibility = Visibility.Collapsed;
            }

            if (oldAssignmentInfo != null)
                InputComboBox.SelectedValue = oldAssignmentInfo.InputNumberID;
        }

        //private void CabinetChange(int? CabinetID)
        //{
        //    if (CabinetID != null)
        //    {
        //        cabinet = Data.CabinetDB.GetCabinetByID((int)CabinetID);

        //        CabinetAndPostUserControl.RemainedQuotaReservationLabel.Visibility = Visibility.Visible;
        //        CabinetAndPostUserControl.RemainedQuotaReservationTextBox.Visibility = Visibility.Visible;

        //        CabinetAndPostUserControl.RemainedQuotaReservationTextBox.Text = Data.CabinetDB.GetRemainedQuotaReservationByCabinetID((int)CabinetID).ToString();

        //        FillInput();

        //        if (assingmentInfos != null)
        //        {
        //            assingmentInfos.Clear();
        //            ConnectionDataGrid.ItemsSource = assingmentInfos;
        //        }
        //    }
        //    else
        //    {
        //        CabinetAndPostUserControl.RemainedQuotaReservationLabel.Visibility = Visibility.Collapsed;
        //        CabinetAndPostUserControl.RemainedQuotaReservationTextBox.Visibility = Visibility.Collapsed;
        //    }

        //    if (oldAssignmentInfo != null)
        //        InputComboBox.SelectedValue = oldAssignmentInfo.InputNumberID;
        //}

        //private void WorkerDoWork(int postID)
        //{

        //    if (postID == 0)
        //    {
        //        if (assingmentInfos != null)
        //                  assingmentInfos.Clear();
        //    }
        //    else
        //    {
        //        assingmentInfos = DB.GetAllInformationByPostIDAndWithOutpostContactType(postID, (byte)DB.PostContactConnectionType.PCMRemote);



        //        List<string> telephoneListOnPost = assingmentInfos.Where(t => t.TelePhoneNo != null).Select(t => t.TelePhoneNo.ToString()).ToList();

        //        List<CRM.Data.BillingServiceReference.DebtInfo> DebtList = CRM.Application.Codes.ServiceReference.GetDebtInfo(telephoneListOnPost);

        //        DebtList.ForEach(Item =>
        //          {
        //              assingmentInfos.Where(t => t.TelePhoneNo == Convert.ToInt64(Item.PhoneNo)).SingleOrDefault().Debt = Item.DebtAmount;
        //              assingmentInfos.Where(t => t.TelePhoneNo == Convert.ToInt64(Item.PhoneNo)).SingleOrDefault().LastPaidBillDate = Item.LastPaidBillDate;
        //          });
        //    }
        //}

        //private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{

        //    ConnectionDataGrid.ItemsSource = assingmentInfos;

        //    if (oldContact != null)
        //        ConnectionDataGrid.SelectedValue = oldContact.ID;

        //    WaittingImage.Visibility = Visibility.Collapsed;
        //    ConnectionDataGrid.Visibility = Visibility.Visible;


        //    if (oldAssignmentInfo != null)
        //        InputComboBox.SelectedValue = oldAssignmentInfo.InputNumberID;

        //    this.ResizeWindow();
        //}

        #endregion

        #region VisitInfo

        private void EditVisitInfo(object sender, RoutedEventArgs e)
        {
            if (VisitInfoGrid.SelectedItem != null)
            {
                VisitAddress visitaddress = VisitInfoGrid.SelectedItem as VisitAddress;
                //    VisitAddressForm window = new VisitAddressForm(investigate.ID, visitaddress.ID);
                //   window.ShowDialog();
                Load();

            }

        }

        private void InsertVisitInfo(object sender, RoutedEventArgs e)
        {
            if (_InvestigatePossibility != null && _InvestigatePossibility.ID != 0)
            {
                //  VisitAddressForm window = new VisitAddressForm(investigate.ID, 0);
                //   window.ShowDialog();
                Load();
            }
        }

        private void DeleteVisitInfo(object sender, RoutedEventArgs e)
        {
            //if (VisitInfoGrid.SelectedIndex < 0 || VisitInfoGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            //try
            //{
            //    MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //    if (result == MessageBoxResult.Yes)
            //    {
            //        VisitAddress visitaddress = VisitInfoGrid.SelectedItem as VisitAddress;

            //        DB.Delete<Data.VisitAddress>(visitaddress.ID);
            //        ShowSuccessMessage("بانک مورد نظر حذف شد");
            //        Load();
            //    }
            //}

            //catch (System.Data.SqlClient.SqlException ex)
            //{
            //    ShowErrorMessage(" مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            //}

            //catch (Exception ex)
            //{
            //    ShowErrorMessage("خطا در حذف بانک", ex);
            //}

        }

        #endregion

        #region Suggest

        private void SuggestEdit(object sender, RoutedEventArgs e)
        {
            if (SuggestGrid.SelectedItem != null)
            {
                SugesstionPossibility sugesstionPossibility = SuggestGrid.SelectedItem as SugesstionPossibility;
                SugesstionPossibilityForm window = new SugesstionPossibilityForm(_InvestigatePossibility.ID, sugesstionPossibility.ID);
                window.ShowDialog();
                Load();

            }
        }

        private void SuggestInsert(object sender, RoutedEventArgs e)
        {
            if (_InvestigatePossibility != null && _InvestigatePossibility.ID != 0)
            {
                SugesstionPossibilityForm window = new SugesstionPossibilityForm(_InvestigatePossibility.ID, 0);
                window.ShowDialog();
                Load();
            }
        }

        private void SuggestDelete(object sender, RoutedEventArgs e)
        {
            if (VisitInfoGrid.SelectedIndex < 0 || VisitInfoGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    SugesstionPossibility sugesstionPossibility = SuggestGrid.SelectedItem as SugesstionPossibility;

                    DB.Delete<Data.SugesstionPossibility>(sugesstionPossibility.ID);
                    ShowSuccessMessage("بانک مورد نظر حذف شد");
                    Load();
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage(" مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }

            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف بانک", ex);
            }
        }

        #endregion

        #region Waiting

        private void EditWaiting(object sender, RoutedEventArgs e)
        {
            if (WaitingGrid.SelectedItem != null)
            {
                WaitingList waitingList = WaitingGrid.SelectedItem as WaitingList;
                InvestigatePossibilityFolder.WaitingPossibilityForm item = new InvestigatePossibilityFolder.WaitingPossibilityForm(_request.ID, _InvestigatePossibility.ID, waitingList.ID);
                item.ShowDialog();

            }

        }

        private void InsertWaiting(object sender, RoutedEventArgs e)
        {
            InvestigatePossibilityFolder.WaitingPossibilityForm item = new InvestigatePossibilityFolder.WaitingPossibilityForm(_request.ID, _InvestigatePossibility.ID, 0);
            item.ShowDialog();
        }

        private void DeleteWaiting(object sender, RoutedEventArgs e)
        {
            if (WaitingGrid.SelectedIndex < 0 || WaitingGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    WaitingList waitingList = WaitingGrid.SelectedItem as WaitingList;

                    DB.Delete<Data.WaitingList>(waitingList.ID);
                    ShowSuccessMessage("بانک مورد نظر حذف شد");
                    Load();
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage(" مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }

            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف بانک", ex);
            }

        }

        #endregion Waiting

        //private void ConfirmInvestigatePossibility(object sender, RoutedEventArgs e)
        //{

        //}

        //private void ConfirmOutBound(object sender, RoutedEventArgs e)
        //{
        //    if (VisitInfoGrid.SelectedItem != null)
        //    {
        //        VisitAddress visitAddress = VisitInfoGrid.SelectedItem as VisitAddress;
        //        if (visitAddress.ConfirmInvestigatePossibility == false)
        //        {
        //            visitAddress.ConfirmInvestigatePossibility = true;
        //            visitAddress.Detach();
        //            DB.Save(visitAddress);
        //        }

        //    }
        //}

        //private void NotConfirmOutBound(object sender, RoutedEventArgs e)
        //{
        //    if (VisitInfoGrid.SelectedItem != null)
        //    {
        //        VisitAddress visitAddress = VisitInfoGrid.SelectedItem as VisitAddress;
        //        if (visitAddress.ConfirmInvestigatePossibility == true)
        //        {
        //            visitAddress.ConfirmInvestigatePossibility = false;
        //            visitAddress.Detach();
        //            DB.Save(visitAddress);
        //        }

        //    }
        //}

        # region OtherBucht

        private void MDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MDFComboBox.SelectedValue != null)
                ConnectionColumnComboBox.ItemsSource = DB.GetConnectionColumnInfo((int)MDFComboBox.SelectedValue);
        }

        private void ConnectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionColumnComboBox.SelectedValue != null)
                ConnectionRowComboBox.ItemsSource = DB.GetConnectionRowInfo((int)ConnectionColumnComboBox.SelectedValue);
        }

        private void ConnectionRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ConnectionRowComboBox.SelectedValue != null)
            {

                if (_oldOtherBucht != null)

                    ConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)ConnectionRowComboBox.SelectedValue, false, buchtType).Union(new List<CheckableItem> { new CheckableItem { LongID = _oldOtherBucht.ID, Name = _oldOtherBucht.BuchtNo.ToString(), IsChecked = false } });

                else

                    ConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)ConnectionRowComboBox.SelectedValue, false, buchtType);
            }
        }

        #endregion OtherBucht

        #region File

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                CableDesignOffice cableDesignOfficeItem = ItemsDataGrid.SelectedItem as CableDesignOffice;
                if (cableDesignOfficeItem != null)
                {

                    _FileID = (Guid)cableDesignOfficeItem.CableDesignFileID;
                    CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable(_FileID);
                    FileBytes = fileInfo.Content;
                    Extension = "." + fileInfo.FileType;
                    if (FileBytes != null && Extension != string.Empty)
                    {
                        string path = System.IO.Path.GetTempFileName() + Extension;
                        try
                        {
                            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                            {
                                writer.Write(FileBytes);
                            }

                            Process p = System.Diagnostics.Process.Start(path);
                            p.WaitForExit();
                        }
                        finally
                        {

                            int result = DocumentsFileDB.UpdateFileInDocumentsFile(_FileID, File.ReadAllBytes(path));
                            if (result <= 0)
                            {
                                Folder.MessageBox.ShowError("فایل بروز رسانی نشد");
                            }
                            else
                            {
                                File.Delete(path);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("فایل موجود نمی باشد !");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

        }

        private void DeleteImage_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CableDesignOffice cableDesignOfficeItem = ItemsDataGrid.SelectedItem as CableDesignOffice;
                    if (cableDesignOfficeItem != null)
                    {

                        Guid tempFileID = cableDesignOfficeItem.CableDesignFileID;
                        DB.Delete<CableDesignOffice>(cableDesignOfficeItem.ID);

                        DocumentsFileDB.DeleteDocumentsFileTable(tempFileID);

                        RefreshItemsDataGrid();

                        switch (_request.RequestTypeID)
                        {
                            case (int)DB.RequestType.E1:
                                {
                                    //باید دیتا گرید موجود در اطلاعات ایوان از نو پر شود
                                    _E1InfoSummary.RefreshFiles(_request.ID, false);
                                }
                                break;
                            case (int)DB.RequestType.SpaceandPower:
                                {
                                    //باید دیتا گرید موجود در اطلاعات فضا و پاور از نو پر شود
                                    _V2SpaceAndPowerInfoSummary.RefreshFiles(_request.ID, false);
                                }
                                break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف", ex);
            }


        }

        private void ChangeFileSelected(object sender, RoutedEventArgs e)
        {
            if (FromFileRadioButton.IsChecked == true)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Filter = "All files (*.*)|*.*";

                if (dlg.ShowDialog() == true)
                {
                    FileBytes = System.IO.File.ReadAllBytes(dlg.FileName);
                    Extension = System.IO.Path.GetExtension(dlg.FileName);
                }
                else
                {
                    FromFileRadioButton.IsChecked = false;
                }
            }
            else if (FromScanerRadioButton.IsChecked == true)
            {
                Scanner oScanner = new Scanner();
                string extension;

                FileBytes = oScanner.ScannWithExtension(out extension);
                Extension = extension;
                if (string.IsNullOrEmpty(Extension))
                {
                    FromScanerRadioButton.IsChecked = false;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FileBytes != null && Extension != string.Empty)
            {
                DateTime currentDateTime = DB.GetServerDate();
                _FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
                CableDesignOffice cableDesignOffice = new CableDesignOffice();
                cableDesignOffice.RequestID = _request.ID;
                cableDesignOffice.CableDesignFileID = _FileID;
                cableDesignOffice.InsertDate = currentDateTime;
                cableDesignOffice.Detach();
                DB.Save(cableDesignOffice);

                RefreshItemsDataGrid();

                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.E1:
                        {
                            //باید دیتا گرید موجود در اطلاعات ایوان از نو پر شود
                            _E1InfoSummary.RefreshFiles(_request.ID, false);
                        }
                        break;
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            //باید دیتا گرید موجود در اطلاعات فضا و پاور از نو پر شود
                            _V2SpaceAndPowerInfoSummary.RefreshFiles(_request.ID, false);
                        }
                        break;
                }

                //بعد از عملیات ذخیره فایل تعیین شده ، باید محتوای دو فیلد زیر خالی شود
                this.Extension = string.Empty;
                this.FileBytes = null;

                FromFileRadioButton.IsChecked = false;
                FromScanerRadioButton.IsChecked = false;
            }
            else
            {
                Folder.MessageBox.ShowInfo("فایل انتخاب نشده است");
            }
        }

        #endregion

        //#region File

        //private void CableDesignFileLisBoxItem_Selected(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        //    dlg.Filter = "All files (*.*)|*.*";

        //    if (dlg.ShowDialog() == true)
        //    {
        //        FileBytes = System.IO.File.ReadAllBytes(dlg.FileName);
        //        Extension = System.IO.Path.GetExtension(dlg.FileName);
        //    }

        //    if (FileBytes != null && Extension != string.Empty)
        //    {
        //        _FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
        //    }
        //}

        //private void CableDesignScannerLisBox_Selected(object sender, RoutedEventArgs e)
        //{
        //    Scanner oScanner = new Scanner();
        //    string extension;

        //    FileBytes = oScanner.ScannWithExtension(out extension);
        //    Extension = extension;
        //}

        //private void CableDesignImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{

        //    switch (_request.RequestTypeID)
        //    {
        //        case (int)DB.RequestType.E1Link:
        //        case (int)DB.RequestType.E1:
        //            {
        //                try
        //                {

        //                    if (_e1.CableDesignFileID != null)
        //                    {
        //                        _FileID = (Guid)_e1.CableDesignFileID;
        //                        CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable(_FileID);
        //                        FileBytes = fileInfo.Content;
        //                        Extension = "." + fileInfo.FileType;
        //                    }
        //                    if (FileBytes != null && Extension != string.Empty)
        //                    {
        //                        string path = System.IO.Path.GetTempFileName() + Extension;
        //                        try
        //                        {
        //                            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        //                            {
        //                                writer.Write(FileBytes);
        //                            }

        //                            Process p = System.Diagnostics.Process.Start(path);
        //                            p.WaitForExit();
        //                        }
        //                        finally
        //                        {


        //                            int result = DocumentsFileDB.UpdateFileInDocumentsFile(_FileID, File.ReadAllBytes(path));
        //                            if (result <= 0)
        //                            {
        //                                Folder.MessageBox.ShowError("فایل بروز رسانی نشد");
        //                            }
        //                            else
        //                            {
        //                                File.Delete(path);
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("فایل موجود نمی باشد !");
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    ShowErrorMessage(ex.Message, ex);
        //                }
        //            }
        //            break;
        //    }

        //}
        //#endregion File

    }
}