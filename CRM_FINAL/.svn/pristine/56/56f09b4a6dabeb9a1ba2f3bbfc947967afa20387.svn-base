using CRM.Data;
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
using CRM.Application.Reports.Viewer;
using CRM.Application.Views;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for ChangeLocationMDFWirigUserControl.xaml
    /// </summary>
    public partial class ChangeLocationMDFWirigUserControl : Local.UserControlBase
    {


        #region fields && properties
        ChangeLocation changeLocation = new ChangeLocation();
        TakePossession takePossession = new TakePossession();
        Telephone oldTelephone = new Telephone();
        Bucht bucht = new Bucht();
        static long _Request = 0;
        static Request request = new Request();
        InvestigatePossibility _InvestigatePossibility { get; set; }
        public Request _request { get; set; }
        public CRM.Data.SpecialWire _SpecialWire { get; set; }
        public CRM.Data.E1 _e1 { get; set; }
        public CRM.Data.VacateSpecialWire _vacateSpecialWire { get; set; }

        public long? subID = 0;
        E1Link _e1Link { get; set; }
        private CRM.Data.CutAndEstablish _cutAndEstablish { get; set; }
        public long Request
        {
            get
            {
                return _Request;
            }
            set
            {
                _Request = value;
            }
        }

        #endregion

        #region constractor && Load

        public ChangeLocationMDFWirigUserControl()
        {
            InitializeComponent(); 
            Initialize();
        }

        public ChangeLocationMDFWirigUserControl(long request)
            :this()
        {
            _Request = request;

        }

        private void Initialize()
        {
           
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        #endregion

        #region method
        public void LoadData()
        {
            if (_IsLoaded)
                return;
            else
                _IsLoaded = true;

            request = Data.RequestDB.GetRequestByID(_Request);
            _InvestigatePossibility = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_Request).Take(1).SingleOrDefault();

            switch (request.RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    DayariLoad();
                    break;
                case (int)DB.RequestType.ChangeLocationCenterInside:
                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                    ChangeLocationLoad();
                    break;
                case (int)DB.RequestType.Dischargin:
                    DischarginLoad();
                    break;
                case (int)DB.RequestType.ChangeNo:
                    ChangeNoLoad();
                    break;
                case (int)DB.RequestType.SpecialWire:
                case (int)DB.RequestType.SpecialWireOtherPoint:
                    SpecialWireLoad();
                    break;
                case (int)DB.RequestType.VacateSpecialWire:
                    VacateSpecialWireLoad();
                    break;
                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    ChangeLocationSpecialWireLoad();
                    break;
                case (int)DB.RequestType.RefundDeposit:
                    RefundDepositLoad();
                    break;
                case (int)DB.RequestType.E1:
                case (int)DB.RequestType.E1Link:
                case (int)DB.RequestType.VacateE1:
                    E1Load();
                    break;
                case (int)DB.RequestType.Connect:
                    Connect();
                    break;
                case (int)DB.RequestType.CutAndEstablish:
                    CutAndEstablishLoad();
                    break;
            }
        }

        private void Connect()
        {
            CauseOfCutTextBox.Visibility = Visibility.Visible;
            CauseOfCutLable.Visibility = Visibility.Visible;
            OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
            PostInfo.Visibility = Visibility.Collapsed;
            PCMGroupBox.Visibility = Visibility.Collapsed;
            NewTelTextBox.Text = request.TelephoneNo.ToString();

            AssignmentInfo assingnmentInfo = DB.GetAllInformationByTelephoneNo(request.TelephoneNo ?? 0);
            // information bucht
            NewTelephoneLabel.Content = "مشخصات بوخت";
            NewCabinetLable.Content = ":کافو";
            NewCabinetInputLable.Content = ":ورودی";
            NewPostLable.Content = ":پست";
            NewConnectionLable.Content = ":اتصالی";

            if (assingnmentInfo != null)
            {
                ConnectionInfo connectionInfo = DB.GetBuchtInfoByID(assingnmentInfo.BuchtID ?? 0);

                NewRowTextBox.Text = connectionInfo.VerticalRowNo.ToString();
                NewColumnTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
                NewBuchtTextBox.Text = connectionInfo.BuchtNo.ToString();
                NewMDFONUInfoTextBox.Text = connectionInfo.MDF;
                CauseOfCutTextBox.Text = assingnmentInfo.CauseOfCut;
            }
        }
        private void ChangeNoLoad()
        {
            CRM.Data.ChangeNo _changeNo = Data.ChangeNoDB.GetChangeNoDBByID(request.ID);

            ConnectionInfo oldconnectionInfo = DB.GetBuchtInfoByID((long)_changeNo.OldBuchtID);
            OldTelTextBox.Text = _changeNo.OldTelephoneNo.ToString();
            OldRowTextBox.Text = oldconnectionInfo.VerticalRowNo.ToString();
            OldColumnTextBox.Text = oldconnectionInfo.VerticalColumnNo.ToString();
            OldBuchtTextBox.Text = oldconnectionInfo.BuchtNo.ToString();
            OldMDFONUInfoTextBox.Text = oldconnectionInfo.MDF;

            NewCabinetComboBox.Visibility = Visibility.Collapsed;
            NewPostComboBox.Visibility = Visibility.Collapsed;
            NewCabinetInputComboBox.Visibility = Visibility.Collapsed;
            NewConnectionTextBox.Visibility = Visibility.Collapsed;

            NewCabinetLable.Visibility = Visibility.Collapsed;
            NewPostLable.Visibility = Visibility.Collapsed;
            NewCabinetInputLable.Visibility = Visibility.Collapsed;
            NewConnectionLable.Visibility = Visibility.Collapsed;

            Bucht bucht = DB.GetBuchtIDByTelephonNo((long)_changeNo.NewTelephoneNo);
            if (bucht != null)
            {
                ConnectionInfo newconnectionInfo = DB.GetBuchtInfoByID((long)bucht.ID);
                NewTelTextBox.Text = _changeNo.NewTelephoneNo.ToString();
                NewRowTextBox.Text = newconnectionInfo.VerticalRowNo.ToString();
                NewColumnTextBox.Text = newconnectionInfo.VerticalColumnNo.ToString();
                NewBuchtTextBox.Text = newconnectionInfo.BuchtNo.ToString();
                NewMDFONUInfoTextBox.Text = newconnectionInfo.MDF;
            }

            AboneInfo OldAboneInfo = Data.PostContactDB.GetAboneInfoByPostContactID((long)_changeNo.OldPostContactID);
            PostInfo.DataContext = new {  OldConnection = OldAboneInfo };

        }

        private void ChangeLocationSpecialWireLoad()
        {

            CRM.Data.ChangeLocationSpecialWire _changeLocationSpecialWire = Data.ChangeLocationSpecialWireDB.GetChangeLocationWireByRequestID(request.ID);

            ConnectionInfo oldconnectionInfo = DB.GetBuchtInfoByID((long)_changeLocationSpecialWire.OldBuchtID);
            OldRowTextBox.Text = oldconnectionInfo.VerticalRowNo.ToString();
            OldColumnTextBox.Text = oldconnectionInfo.VerticalColumnNo.ToString();
            OldBuchtTextBox.Text = oldconnectionInfo.BuchtNo.ToString();
            OldMDFONUInfoTextBox.Text = oldconnectionInfo.MDF;

            ConnectionInfo newconnectionInfo = DB.GetBuchtInfoByID((long)_InvestigatePossibility.BuchtID);
            NewRowTextBox.Text = newconnectionInfo.VerticalRowNo.ToString();
            NewColumnTextBox.Text = newconnectionInfo.VerticalColumnNo.ToString();
            NewBuchtTextBox.Text = newconnectionInfo.BuchtNo.ToString();
            NewMDFONUInfoTextBox.Text = newconnectionInfo.MDF;

            AboneInfo NewAboneInfo = Data.PostContactDB.GetAboneInfoByPostContactID((long)_InvestigatePossibility.PostContactID);
            AboneInfo OldAboneInfo = Data.PostContactDB.GetAboneInfoByPostContactID(_changeLocationSpecialWire.OldPostContactID ?? 0);

            PostInfo.DataContext = new { NewConnection = NewAboneInfo, OldConnection = OldAboneInfo };



        }

        private void VacateSpecialWireLoad()
        {
            OldCabinetComboBox.Visibility = Visibility.Collapsed;
            OldPostComboBox.Visibility = Visibility.Collapsed;
            OldCabinetInputComboBox.Visibility = Visibility.Collapsed;
            OldConnectionTextBox.Visibility = Visibility.Collapsed;

            OldCabinetLable.Visibility = Visibility.Collapsed;
            OldPostLable.Visibility = Visibility.Collapsed;
            OldCabinetInputLable.Visibility = Visibility.Collapsed;
            OldConnectionLable.Visibility = Visibility.Collapsed;

            OldTelephoneGroupBox.Visibility = Visibility.Collapsed;

            _vacateSpecialWire = Data.VacateSpecialWireDB.GetVacateSpecialWireByRequestID(request.ID);
            NewTelephoneLabel.Content = "مشخصات بوخت ";
            NewCabinetLable.Content = "کافو ";
            NewCabinetInputLable.Content = "ورودی ";
            NewPostLable.Content = "پست ";
            NewConnectionLable.Content = "اتصالی ";

            NewTelTextBox.Text = request.TelephoneNo.ToString();

            ConnectionInfo connectionInfo = DB.GetBuchtInfoByID(_vacateSpecialWire.BuchtID);
            NewRowTextBox.Text = connectionInfo.VerticalRowNo.ToString();
            NewColumnTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
            NewBuchtTextBox.Text = connectionInfo.BuchtNo.ToString();
            NewMDFONUInfoTextBox.Text = connectionInfo.MDF;


            List<CRM.Data.AssignmentDB.NearestTelephonInfo> TelephonInfoByBuchtID = Data.AssignmentDB.GetTelephonInfoByBuchtID(_vacateSpecialWire.BuchtID).ToList();
            PostInfo.DataContext = new { NewConnection = TelephonInfoByBuchtID.Take(1).SingleOrDefault() };
        }

        private void PostContactEquipmentButton_Click(object sender, RoutedEventArgs e)
        {

            //  CRM.Data.AssignmentDB.NearestTelephonInfo nearestTelephonInfo = PostInfo.DataContext as CRM.Data.AssignmentDB.NearestTelephonInfo;
            Button button = sender as Button;
            if (button.Tag != null)
            {
                PostContactUsedEquinmentForm window = new PostContactUsedEquinmentForm((long)button.Tag);
                window.ShowDialog();
            }
        }


        #endregion

        # region LoadMethod

        private void DayariLoad()
        {

            OldTelephoneGroupBox.Visibility = Visibility.Collapsed;

            // information bucht
            NewTelephoneLabel.Content = "مشخصات بوخت";
            NewCabinetLable.Content = ":کافو";
            NewCabinetInputLable.Content = ":ورودی";
            NewPostLable.Content = ":پست";
            NewConnectionLable.Content = ":اتصالی";


            ConnectionInfo connectionInfo = DB.GetBuchtInfoByID(_InvestigatePossibility.BuchtID ?? 0);
            NewTelTextBox.Text = request.TelephoneNo.ToString();
            NewRowTextBox.Text = connectionInfo.VerticalRowNo.ToString();
            NewColumnTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
            NewBuchtTextBox.Text = connectionInfo.BuchtNo.ToString();
            NewMDFONUInfoTextBox.Text = connectionInfo.MDF;


            List<CRM.Data.AssignmentDB.NearestTelephonInfo> TelephonInfoByBuchtID = Data.AssignmentDB.GetTelephonInfoByBuchtID(_InvestigatePossibility.BuchtID).ToList();
            PostInfo.DataContext = new { NewConnection = TelephonInfoByBuchtID.Take(1).SingleOrDefault() };

            OldCabinetComboBox.Visibility = Visibility.Collapsed;
            OldPostComboBox.Visibility = Visibility.Collapsed;
            OldCabinetInputComboBox.Visibility = Visibility.Collapsed;
            OldConnectionTextBox.Visibility = Visibility.Collapsed;

            OldCabinetLable.Visibility = Visibility.Collapsed;
            OldPostLable.Visibility = Visibility.Collapsed;
            OldCabinetInputLable.Visibility = Visibility.Collapsed;
            OldConnectionLable.Visibility = Visibility.Collapsed;

            // نمایش اطلاعات پی سی ام
            bucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
            if (bucht != null)
            {
                PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                if ( postContact!= null && ( postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote))
                {
                    PCMGroupBox.Visibility = Visibility.Visible;
                    LoadPCMInfo(bucht);
                }
            }
        }

        private void DischarginLoad()
        {
            OldTelephoneGroupBox.Visibility = Visibility.Collapsed;

            takePossession = Data.TakePossessionDB.GetTakePossessionByID(request.ID);
            // information bucht
            NewTelephoneLabel.Content = "مشخصات بوخت";
            NewCabinetLable.Content = ":کافو";
            NewCabinetInputLable.Content = ":ورودی"; 
            NewPostLable.Content = ":پست";
            NewConnectionLable.Content = ":اتصالی";


            ConnectionInfo connectionInfo = DB.GetBuchtInfoByID(takePossession.BuchtID ?? 0);
            NewTelTextBox.Text = request.TelephoneNo.ToString();
            NewRowTextBox.Text = connectionInfo.VerticalRowNo.ToString();
            NewColumnTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
            NewBuchtTextBox.Text = connectionInfo.BuchtNo.ToString();
            NewMDFONUInfoTextBox.Text = connectionInfo.MDF;


            List<CRM.Data.AssignmentDB.NearestTelephonInfo> TelephonInfoByBuchtID = Data.AssignmentDB.GetTelephonInfoByBuchtID(takePossession.BuchtID).ToList();
            PostInfo.DataContext = new { NewConnection = TelephonInfoByBuchtID.Take(1).SingleOrDefault() };

            OldCabinetComboBox.Visibility = Visibility.Collapsed;
            OldPostComboBox.Visibility = Visibility.Collapsed;
            OldCabinetInputComboBox.Visibility = Visibility.Collapsed;
            OldConnectionTextBox.Visibility = Visibility.Collapsed;

            OldCabinetLable.Visibility = Visibility.Collapsed;
            OldPostLable.Visibility = Visibility.Collapsed;
            OldCabinetInputLable.Visibility = Visibility.Collapsed;
            OldConnectionLable.Visibility = Visibility.Collapsed;

            // نمایش اطلاعات پی سی ام
            bucht = Data.BuchtDB.GetBuchtByID((long)takePossession.BuchtID);
            if (bucht != null)
            {
                PostContact postContact = Data.PostContactDB.GetPostContactByID(takePossession.PostContactID ?? 0);
                // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                {
                    PCMGroupBox.Visibility = Visibility.Visible;
                    LoadPCMInfo(bucht);
                }
            }
        }

        private void ChangeLocationLoad()
        {
            PostInfo.Visibility = Visibility.Collapsed;

            changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID((long)request.ID);
            oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
            bucht = Data.BuchtDB.GetBuchtByID((long)changeLocation.OldBuchtID);

            Bucht ReservBucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);



            if (_InvestigatePossibility.BuchtID != null)
            {
                ConnectionInfo connectionInfo = DB.GetBuchtInfoByID((long)_InvestigatePossibility.BuchtID);
                NewRowTextBox.Text = connectionInfo.VerticalRowNo.ToString();
                NewColumnTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
                NewBuchtTextBox.Text = connectionInfo.BuchtNo.ToString();
                NewMDFONUInfoTextBox.Text = connectionInfo.MDF;

            }
            else
            {
                Folder.MessageBox.ShowInfo("بوخت رزرو یافت نشد");

            }
            if (changeLocation.SourceCenter != null && changeLocation.SourceCenter == request.CenterID)
            {
                NewTelephoneGroupBox.Visibility = Visibility.Collapsed;
                PostInfo.Visibility = Visibility.Visible;
                OldTelephoneGroupBox.Header = "تلفن تخلیه";
                List<CRM.Data.AssignmentDB.NearestTelephonInfo> TelephonInfoByBuchtID = Data.AssignmentDB.GetTelephonInfoByBuchtID(changeLocation.OldBuchtID).ToList();
                PostInfo.DataContext = new { OldConnection = TelephonInfoByBuchtID.Take(1).SingleOrDefault() };

                NewCabinetComboBox.Visibility = Visibility.Collapsed;
                NewPostComboBox.Visibility = Visibility.Collapsed;
                NewCabinetInputComboBox.Visibility = Visibility.Collapsed;
                NewConnectionTextBox.Visibility = Visibility.Collapsed;

                NewCabinetLable.Visibility = Visibility.Collapsed;
                NewPostLable.Visibility = Visibility.Collapsed;
                NewCabinetInputLable.Visibility = Visibility.Collapsed;
                NewConnectionLable.Visibility = Visibility.Collapsed;


                // نمایش اطلاعات پی سی ام
                bucht = Data.BuchtDB.GetBuchtByID((long)changeLocation.OldBuchtID);
                if (bucht != null)
                {
                    PostContact postContact = Data.PostContactDB.GetPostContactByID(changeLocation.OldPostContactID ?? 0);
                    // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                    if (postContact != null && (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote))
                    {
                        PCMGroupBox.Visibility = Visibility.Visible;
                        LoadPCMInfo(bucht);
                    }
                }

            }
            else if (changeLocation.SourceCenter != null && changeLocation.TargetCenter == request.CenterID)
            {

                OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
                NewTelephoneGroupBox.Header = "تلفن دایر";

                PostInfo.Visibility = Visibility.Visible;

                List<CRM.Data.AssignmentDB.NearestTelephonInfo> TelephonInfoByBuchtID = Data.AssignmentDB.GetTelephonInfoByBuchtID(_InvestigatePossibility.BuchtID).ToList();
                PostInfo.DataContext = new { NewConnection = TelephonInfoByBuchtID.Take(1).SingleOrDefault() };


                OldCabinetComboBox.Visibility = Visibility.Collapsed;
                OldPostComboBox.Visibility = Visibility.Collapsed;
                OldCabinetInputComboBox.Visibility = Visibility.Collapsed;
                OldConnectionTextBox.Visibility = Visibility.Collapsed;

                OldCabinetLable.Visibility = Visibility.Collapsed;
                OldPostLable.Visibility = Visibility.Collapsed;
                OldCabinetInputLable.Visibility = Visibility.Collapsed;
                OldConnectionLable.Visibility = Visibility.Collapsed;

                // نمایش اطلاعات پی سی ام
                bucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
                if (bucht != null)
                {
                    PostContact postContact = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0);
                    // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                    if (postContact != null && (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote))
                    {
                        PCMGroupBox.Visibility = Visibility.Visible;
                        LoadPCMInfo(bucht);
                    }
                }
            }
            else
            {
                PostInfo.Visibility = Visibility.Visible;
                List<CRM.Data.AssignmentDB.NearestTelephonInfo> NearestTelephonInfoByBuchtIDReserv = Data.AssignmentDB.GetTelephonInfoByBuchtID(_InvestigatePossibility.BuchtID).ToList();

                if (changeLocation.ChangeLocationTypeID == (int)DB.ChangeLocationCenterType.itself && changeLocation.OldPostContactID != _InvestigatePossibility.PostContactID)
                {
                    NearestTelephonInfoByBuchtIDReserv.SingleOrDefault().ConnectionNo = Data.PostContactDB.GetPostContactByID(_InvestigatePossibility.PostContactID ?? 0).ConnectionNo;
                    NearestTelephonInfoByBuchtIDReserv.SingleOrDefault().PostContactID = _InvestigatePossibility.PostContactID;
                    Post post = Data.PostDB.GetPosByPostContactID(_InvestigatePossibility.PostContactID ?? 0);
                    NearestTelephonInfoByBuchtIDReserv.SingleOrDefault().PostNumber = post.Number.ToString();
                    NearestTelephonInfoByBuchtIDReserv.SingleOrDefault().PostID = post.ID;
                }
                List<CRM.Data.AssignmentDB.NearestTelephonInfo> TelephonInfoByBuchtID = Data.AssignmentDB.GetTelephonInfoByBuchtID(changeLocation.OldBuchtID).ToList();
                if (changeLocation.ChangeLocationTypeID == (int)DB.ChangeLocationCenterType.itself && changeLocation.OldPostContactID != _InvestigatePossibility.PostContactID)
                {
                    TelephonInfoByBuchtID.SingleOrDefault().ConnectionNo = Data.PostContactDB.GetPostContactByID(changeLocation.OldPostContactID ?? 0).ConnectionNo;
                    TelephonInfoByBuchtID.SingleOrDefault().PostContactID = changeLocation.OldPostContactID;
                }

                PostInfo.DataContext = new { OldConnection = TelephonInfoByBuchtID.Take(1).SingleOrDefault(), NewConnection = NearestTelephonInfoByBuchtIDReserv.Take(1).SingleOrDefault() };

                // نمایش اطلاعات پی سی ام
                bucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
                if (bucht != null)
                {
                    PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                    // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                    if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                    {
                        PCMGroupBox.Visibility = Visibility.Visible;
                        LoadPCMInfo(bucht);
                    }
                }
            }

            OldTelTextBox.Text = changeLocation.OldTelephone.ToString();
            NewTelTextBox.Text = changeLocation.NewTelephone != null ? changeLocation.NewTelephone.ToString() : changeLocation.OldTelephone.ToString();

            ConnectionInfo oldconnectionInfo = DB.GetBuchtInfoByID((long)changeLocation.OldBuchtID);

            OldRowTextBox.Text = oldconnectionInfo.VerticalRowNo.ToString();
            OldColumnTextBox.Text = oldconnectionInfo.VerticalColumnNo.ToString();
            OldBuchtTextBox.Text = oldconnectionInfo.BuchtNo.ToString();
            OldMDFONUInfoTextBox.Text = oldconnectionInfo.MDF;

        }

        private void SpecialWireLoad()
        {
                

               OldCabinetComboBox.Visibility = Visibility.Collapsed;
               OldPostComboBox.Visibility = Visibility.Collapsed;
               OldCabinetInputComboBox.Visibility = Visibility.Collapsed;
               OldConnectionTextBox.Visibility = Visibility.Collapsed;

               OldCabinetLable.Visibility = Visibility.Collapsed;
               OldPostLable.Visibility = Visibility.Collapsed;
               OldCabinetInputLable.Visibility = Visibility.Collapsed;
               OldConnectionLable.Visibility = Visibility.Collapsed;

               OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
            
               _SpecialWire = Data.SpecialWireDB.GetSpecialWireByRequestID(request.ID);
               NewTelephoneLabel.Content = "مشخصات بوخت ";
               NewCabinetLable.Content = "کافو ";
               NewCabinetInputLable.Content = "ورودی ";
               NewPostLable.Content = "پست ";
               NewConnectionLable.Content = "اتصالی ";
               if (_SpecialWire != null)
               {
                   NewTelTextBox.Text = _SpecialWire.TelephoneNo.ToString();

                   if (_SpecialWire.SpecialWireType != (int)DB.SpecialWireType.Middle)
                   {

                       ConnectionInfo connectionInfo = DB.GetBuchtInfoByID(_InvestigatePossibility.BuchtID ?? 0);
                       NewRowTextBox.Text = connectionInfo.VerticalRowNo.ToString();
                       NewColumnTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
                       NewBuchtTextBox.Text = connectionInfo.BuchtNo.ToString();
                       NewMDFONUInfoTextBox.Text = connectionInfo.MDF;
                       List<CRM.Data.AssignmentDB.NearestTelephonInfo> TelephonInfoByBuchtID = Data.AssignmentDB.GetTelephonInfoByBuchtID(_InvestigatePossibility.BuchtID).ToList();
                       PostInfo.DataContext = new { NewConnection = TelephonInfoByBuchtID.Take(1).SingleOrDefault() };
                   }
               }


               
       
        }

        private void RefundDepositLoad()
        {
            OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
            CRM.Data.RefundDeposit refundDeposit = Data.RefundDepositDB.GetRefundDepositByID(request.ID);

            // information bucht
            NewTelephoneLabel.Content = "مشخصات بوخت";
            NewCabinetLable.Content = ":کافو";
            NewCabinetInputLable.Content = ":ورودی";
            NewPostLable.Content = ":پست";
            NewConnectionLable.Content = ":اتصالی";

            ConnectionInfo connectionInfo = DB.GetBuchtInfoByID(refundDeposit.BuchtID ?? 0);
            NewTelTextBox.Text = request.TelephoneNo.ToString();
            NewRowTextBox.Text = connectionInfo.VerticalRowNo.ToString();
            NewColumnTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
            NewBuchtTextBox.Text = connectionInfo.BuchtNo.ToString();
            NewMDFONUInfoTextBox.Text = connectionInfo.MDF;

            List<CRM.Data.AssignmentDB.NearestTelephonInfo> TelephonInfoByBuchtID = Data.AssignmentDB.GetTelephonInfoByBuchtID(refundDeposit.BuchtID).ToList();
            PostInfo.DataContext = new { NewConnection = TelephonInfoByBuchtID.Take(1).SingleOrDefault() };

            OldCabinetComboBox.Visibility = Visibility.Collapsed;
            OldPostComboBox.Visibility = Visibility.Collapsed;
            OldCabinetInputComboBox.Visibility = Visibility.Collapsed;
            OldConnectionTextBox.Visibility = Visibility.Collapsed;

            OldCabinetLable.Visibility = Visibility.Collapsed;
            OldPostLable.Visibility = Visibility.Collapsed;
            OldCabinetInputLable.Visibility = Visibility.Collapsed;
            OldConnectionLable.Visibility = Visibility.Collapsed;

            // نمایش اطلاعات پی سی ام
            bucht = Data.BuchtDB.GetBuchtByID((long)refundDeposit.BuchtID);
            if (bucht != null)
            {
                PostContact postContact = Data.PostContactDB.GetPostContactByID(refundDeposit.PostContactID ?? 0);
                // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                {
                    PCMGroupBox.Visibility = Visibility.Visible;
                    LoadPCMInfo(bucht);
                }
            }
        }

        private void E1Load()
        {
            OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
            CRM.Data.E1 _e1 = Data.E1DB.GetE1ByRequestID(request.ID);
            _e1Link = Data.E1LinkDB.GetE1LinkByID(subID ?? 0);

            // information bucht
            NewTelephoneLabel.Content = "مشخصات بوخت";
            NewCabinetLable.Content = ":کافو";
            NewCabinetInputLable.Content = ":ورودی";
            NewPostLable.Content = ":پست";
            NewConnectionLable.Content = ":اتصالی";

            ConnectionInfo connectionInfo = DB.GetBuchtInfoByID((long)_InvestigatePossibility.BuchtID);
            NewTelTextBox.Text = request.TelephoneNo.ToString();
            NewRowTextBox.Text = connectionInfo.VerticalRowNo.ToString();
            NewColumnTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
            NewBuchtTextBox.Text = connectionInfo.BuchtNo.ToString();
            NewMDFONUInfoTextBox.Text = connectionInfo.MDF;


            if (_e1Link.OtherBuchtID != null)
            {
                ConnectionInfo OtherBuchtconnectionInfo = DB.GetBuchtInfoByID((long)_e1Link.OtherBuchtID);
                OtherBuchtRowTextBox.Text = OtherBuchtconnectionInfo.VerticalRowNo.ToString();
                OtherBuchtColumnTextBox.Text = OtherBuchtconnectionInfo.VerticalColumnNo.ToString();
                OtherBuchtTextBox.Text = OtherBuchtconnectionInfo.BuchtNo.ToString();
                OtherBuchtMDFONUInfoTextBox.Text = OtherBuchtconnectionInfo.MDF;
            }


            if (_e1Link.AcessBuchtID != null)
            {
                ConnectionInfo AcessBuchtconnectionInfo = DB.GetBuchtInfoByID((long)_e1Link.AcessBuchtID);
                AcessBuchtRowTextBox.Text = AcessBuchtconnectionInfo.VerticalRowNo.ToString();
                AcessBuchtColumnTextBox.Text = AcessBuchtconnectionInfo.VerticalColumnNo.ToString();
                AcessBuchtTextBox.Text = AcessBuchtconnectionInfo.BuchtNo.ToString();
                AcessBuchtMDFONUInfoTextBox.Text = AcessBuchtconnectionInfo.MDF;
            }

            List<CRM.Data.AssignmentDB.NearestTelephonInfo> TelephonInfoByBuchtID = Data.AssignmentDB.GetTelephonInfoByBuchtID(_InvestigatePossibility.BuchtID).ToList();
            PostInfo.DataContext = new { NewConnection = TelephonInfoByBuchtID.Take(1).SingleOrDefault() };

            OldCabinetComboBox.Visibility = Visibility.Collapsed;
            OldPostComboBox.Visibility = Visibility.Collapsed;
            OldCabinetInputComboBox.Visibility = Visibility.Collapsed;
            OldConnectionTextBox.Visibility = Visibility.Collapsed;

            OldCabinetLable.Visibility = Visibility.Collapsed;
            OldPostLable.Visibility = Visibility.Collapsed;
            OldCabinetInputLable.Visibility = Visibility.Collapsed;
            OldConnectionLable.Visibility = Visibility.Collapsed;
        }

        private void LoadPCMInfo(Bucht bucht)
        {
            if (bucht.PCMPortID != null)
            {
                
                StatusColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.PostContactStatus));
                BuchtTypeColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.BuchtType));
                PCMDataGrid.Visibility = Visibility.Visible;

                PCMPort pCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
                List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(pCMPort.PCMID).ToList();
                List<Bucht> buchtList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();

                List<AssignmentInfo> assingmentInfo = DB.GetAllInformationByBuchtIDs(buchtList.Select(b => b.ID).ToList(), (byte)DB.BuchtStatus.AllocatedToInlinePCM);

                PCMDataGrid.ItemsSource = assingmentInfo;

                BuchtNoInputLable.Visibility = Visibility.Visible;
                BuchtNoInputTextBox.Visibility = Visibility.Visible;

                BuchtNoInputPCMTextBox.Visibility = Visibility.Visible;
                BuchtNoPCMInputLable.Visibility = Visibility.Visible;

                Bucht buchtConnectToInputCabinet = Data.BuchtDB.GetBuchtByID((long)buchtList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().BuchtIDConnectedOtherBucht);
                BuchtNoInputTextBox.Text = DB.GetConnectionByBuchtID(buchtConnectToInputCabinet.ID);
                BuchtNoInputPCMTextBox.Text = DB.GetConnectionByBuchtID(buchtList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().ID);
            }
            else
            {
                Folder.MessageBox.ShowError("خطا در دریافت اطلاعات پی سی ام لطفا اطلاعات صحیح پی سی ام را به پشتیبان ارائه دهید");
            }
        }


        private void CutAndEstablishLoad()
        {


            OldTelephoneGroupBox.Visibility = Visibility.Collapsed;
            PostInfo.Visibility = Visibility.Collapsed;
            PCMGroupBox.Visibility = Visibility.Collapsed;
            NewTelTextBox.Text = request.TelephoneNo.ToString();

            AssignmentInfo assingnmentInfo = DB.GetAllInformationByTelephoneNo(request.TelephoneNo ?? 0);
            // information bucht
            NewTelephoneLabel.Content = "مشخصات بوخت";
            NewCabinetLable.Content = ":کافو";
            NewCabinetInputLable.Content = ":ورودی";
            NewPostLable.Content = ":پست";
            NewConnectionLable.Content = ":اتصالی";

            if (assingnmentInfo != null)
            {
                ConnectionInfo connectionInfo = DB.GetBuchtInfoByID(assingnmentInfo.BuchtID ?? 0);

                NewRowTextBox.Text = connectionInfo.VerticalRowNo.ToString();
                NewColumnTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
                NewBuchtTextBox.Text = connectionInfo.BuchtNo.ToString();
                NewMDFONUInfoTextBox.Text = connectionInfo.MDF;
            }
        }

        #endregion


    }
}
