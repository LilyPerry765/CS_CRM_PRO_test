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

namespace CRM.Application.Views
{
    public partial class IssueWiringForm : Local.RequestFormBase
    {

        private bool Mode = false;


        public Request _request { get; set; }


        public InvestigatePossibility investigate { get; set; }

        public static List<Switch> switchList { get; set; }
        public static List<Telephone> telList { get; set; }
        public static List<BuchtInfo> buchtList { get; set; }
        public static Wiring wiring { get; set; }
        public static SwitchCodeInfo switchCode { get; set; }

        public UserControls.UserInfoSummary _userInfoSummary { get; set; }
        public UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        public UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        public UserControls.InstallInfoSummary _installInfoSummary { get; set; }
        public UserControls.InvestigateInfoSummary _investigateInfoSummary { get; set; }

        public static IssueWiring _issueWiring { get; set; }

        public IssueWiringForm()
        {
            InitializeComponent();
        }

        public IssueWiringForm(long id)
            : this()
        {

            _request = Data.RequestDB.GetRequestByID(id);

            switchList = Data.SwitchDB.GetSwitchByCenterID(_request.CenterID);
            Initialize();


        }


        private void Initialize()
        {
            base.RequestID = _request.ID;


            wiring = new Wiring();
            _issueWiring = new IssueWiring();



            _userInfoSummary = new UserInfoSummary();
            _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
            _requestInfoSummary = new RequestInfoSummary(_request.ID);

            if (_request.RequestTypeID == (int)DB.RequestType.Dayri || _request.RequestTypeID == (int)DB.RequestType.Reinstall)
            {
                _installInfoSummary = new InstallInfoSummary(_request.ID);
                _investigateInfoSummary = new InvestigateInfoSummary(_request.ID);
            }

            WiringTypeComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.WiringType));

            if (_issueWiring.ID == 0)
            {
                if (_request.RequestTypeID == (int)DB.RequestType.Dayri || _request.RequestTypeID == (int)DB.RequestType.Reinstall)
                {
                    string investigateID = string.Empty;
                    if (_investigateInfoSummary != null && _investigateInfoSummary.investigate != null)
                    {
                        investigateID = _investigateInfoSummary.investigate.ID.ToString();
                        wiring.InvestigatePossibilityID = _investigateInfoSummary.investigate.ID;
                    }

                    _issueWiring.WiringNo = investigateID + "-" + _request.CustomerID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                    //_issueWiring.WiringNo = _request.ID.ToString() + "-" + _request.CustomerID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                    //if (_investigateInfoSummary != null && _investigateInfoSummary.investigate != null)
                    
                }

                else if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterInside || _request.RequestTypeID == (int)DB.RequestType.ChangeLocationCenterToCenter)
                {
                    // TO DO : شماره سیم بندی اصلاح شود
                    _issueWiring.WiringNo = _request.ID.ToString() + "-" + _request.CustomerID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                }

                else if (_request.RequestTypeID == (int)DB.RequestType.Dischargin)
                {
                    // TO DO : شماره سیم بندی اصلاح شود
                    _issueWiring.WiringNo = _request.ID.ToString() + "-" + _request.CustomerID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                }
                else if (_request.RequestTypeID == (int)DB.RequestType.RefundDeposit)
                {
                    // TO DO : شماره سیم بندی اصلاح شود
                    _issueWiring.WiringNo = _request.ID.ToString() + "-" + _request.CustomerID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                }
                else if (_request.RequestTypeID == (int)DB.RequestType.ChangeNo)
                {
                    // TO DO : شماره سیم بندی اصلاح شود
                    _issueWiring.WiringNo = _request.ID.ToString() + "-" + _request.CustomerID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                }
                else if (_request.RequestTypeID == (int)DB.RequestType.E1 || _request.RequestTypeID == (int)DB.RequestType.E1Link || _request.RequestTypeID == (int)DB.RequestType.VacateE1)
                {
                    // TO DO : شماره سیم بندی اصلاح شود
                    _issueWiring.WiringNo = _request.ID.ToString() + "-" + _request.CustomerID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                }
                else if (_request.RequestTypeID == (int)DB.RequestType.E1Fiber)
                {
                    // TO DO : شماره سیم بندی اصلاح شود
                    _issueWiring.WiringNo = _request.ID.ToString() + "-" + _request.CustomerID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                }
                else if (_request.RequestTypeID == (int)DB.RequestType.SpecialWire || _request.RequestTypeID == (int)DB.RequestType.SpecialWireOtherPoint)
                {
                    // TO DO : شماره سیم بندی اصلاح شود
                    _issueWiring.WiringNo = _request.ID.ToString() + "-" + _request.CustomerID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                }
                else if (_request.RequestTypeID == (int)DB.RequestType.VacateSpecialWire)
                {
                    // TO DO : شماره سیم بندی اصلاح شود
                    _issueWiring.WiringNo = _request.ID.ToString() + "-" + _request.CustomerID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                }
                else if (_request.RequestTypeID == (int)DB.RequestType.ChangeLocationSpecialWire)
                {
                    // TO DO : شماره سیم بندی اصلاح شود
                    _issueWiring.WiringNo = _request.ID.ToString() + "-" + _request.CustomerID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                }
                wiring.RequestID = _request.ID;


            }


            WiringIssueInfo.Visibility = Visibility.Visible;



            this.currentStep = DB.GetCurrentStepByRequest(_request);
            WiringStatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            WiringStatusComboBox.SelectedValue = _request.StatusID;

            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        public void Load()
        {

            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                    {
                        ChangeLocation changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID((long)_request.ID);
                        if (changeLocation != null && changeLocation.TargetCenter != null && changeLocation.TargetCenter == _request.CenterID)
                        {
                            //اگر در خواست در مقصد باشد و قبلا برای ان فرم سیم بند زخیره نشده باشد سیم بندی جدید ایجاد میشود اگر قبلا ایجاد شده باشد رکورد دوم استفاده میشود جون اول متعلق به مبدا میباشد
                            List<IssueWiring> IssueWirings = Data.IssueWiringDB.GetIssueWiringByRequestIDByStatus(_request.ID, (int)DB.StatusIssueWiring.Issued);
                            if (IssueWirings.Count >= 2)
                            {
                                IssueWiring newIssueWiring = Data.IssueWiringDB.GetIssueWiringByRequestIDByStatus(_request.ID, (int)DB.StatusIssueWiring.Issued).LastOrDefault();
                                if (newIssueWiring != null)
                                    _issueWiring = newIssueWiring;
                            }
                        }
                        else
                        {
                            IssueWiring newIssueWiring = Data.IssueWiringDB.GetIssueWiringByRequestIDByStatus(_request.ID, (int)DB.StatusIssueWiring.Issued).LastOrDefault();
                            if (newIssueWiring != null)
                                _issueWiring = newIssueWiring;
                        }
                    }
                    break;
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    {
                        IssueWiring newIssueWiring = Data.IssueWiringDB.GetIssueWiringByRequestIDByStatus(_request.ID, (int)DB.StatusIssueWiring.Issued).LastOrDefault();
                        if (newIssueWiring != null)
                            _issueWiring = newIssueWiring;
                    }
                    break;
                case (int)DB.RequestType.ChangeLocationCenterInside:
                    {
                        IssueWiring newIssueWiring = Data.IssueWiringDB.GetIssueWiringByRequestIDByStatus(_request.ID, (int)DB.StatusIssueWiring.Issued).LastOrDefault();
                        if (newIssueWiring != null)
                            _issueWiring = newIssueWiring;
                    }
                    break;
                case (int)DB.RequestType.SpecialWire:
                case (int)DB.RequestType.SpecialWireOtherPoint:
                    {

                        IssueWiring newIssueWiring = Data.IssueWiringDB.GetIssueWiringByRequestIDByStatus(_request.ID, (int)DB.StatusIssueWiring.Issued).LastOrDefault();
                        if (newIssueWiring != null)
                            _issueWiring = newIssueWiring;
                    }
                    break;
                case (int)DB.RequestType.VacateSpecialWire:
                    {

                        IssueWiring newIssueWiring = Data.IssueWiringDB.GetIssueWiringByRequestIDByStatus(_request.ID, (int)DB.StatusIssueWiring.Issued).LastOrDefault();
                        if (newIssueWiring != null)
                            _issueWiring = newIssueWiring;
                    }
                    break;
                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    {

                        IssueWiring newIssueWiring = Data.IssueWiringDB.GetIssueWiringByRequestIDByStatus(_request.ID, (int)DB.StatusIssueWiring.Issued).LastOrDefault();
                        if (newIssueWiring != null)
                            _issueWiring = newIssueWiring;
                    }
                    break;
            }
            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                    _issueWiring.WiringTypeID = (byte)DB.WiringType.Open;
                    break;
                case (int)DB.RequestType.ChangeLocationCenterInside:

                    _issueWiring.WiringTypeID = (byte)DB.WiringType.ChangeLocation;
                    break;
                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                    _issueWiring.WiringTypeID = (byte)DB.WiringType.ChangeLocation;
                    break;
                case (int)DB.RequestType.Dischargin:
                    _issueWiring.WiringTypeID = (byte)DB.WiringType.Discharge;
                    break;
                case (int)DB.RequestType.RefundDeposit:
                    _issueWiring.WiringTypeID = (byte)DB.WiringType.RefundDeposit;
                    break;
                case (int)DB.RequestType.Reinstall:
                    _issueWiring.WiringTypeID = (byte)DB.WiringType.Reinstall;
                    break;
                case (int)DB.RequestType.E1:
                case (int)DB.RequestType.E1Link:
                case (int)DB.RequestType.VacateE1:
                    _issueWiring.WiringTypeID = (byte)DB.WiringType.E1;
                    break;
                case (int)DB.RequestType.E1Fiber:
                    _issueWiring.WiringTypeID = (byte)DB.WiringType.E1Fiber;
                    break;
                case (int)DB.RequestType.SpecialWire:
                    _issueWiring.WiringTypeID = (byte)DB.WiringType.PrivateWire;
                    break;
                case (int)DB.RequestType.VacateSpecialWire:
                    _issueWiring.WiringTypeID = (byte)DB.WiringType.VacateSpecialWire;
                    break;
                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    _issueWiring.WiringTypeID = (byte)DB.WiringType.ChangeLocationSpecialWire;
                    break;
            }

            if (_issueWiring.ID == 0)
            {
                _issueWiring.WiringIssueDate = DB.GetServerDate();
                Mode = false;
            }
            else
            {
                Mode = true;
            }

            WiringIssueInfo.DataContext = _issueWiring;

        }

        #region Action
        public override bool Save()
        {
            try
            {
                _issueWiring.InsertDate = DB.GetServerDate();
                _issueWiring.RequestID = _request.ID;
                _request.StatusID = (int)WiringStatusComboBox.SelectedValue;

                switch (_request.RequestTypeID)
                {
                    case (byte)DB.RequestType.Dayri:
                    case (byte)DB.RequestType.Reinstall:
                        {
                            wiring.InvestigatePossibilityID = _investigateInfoSummary.investigate.ID;
                            wiring.RequestID = _request.ID;
                            wiring.WiringInsertDate = DB.GetServerDate();
                            wiring.Status = (int)WiringStatusComboBox.SelectedValue;
                            WiringDB.SaveWiringIssue(_request, _issueWiring, wiring, Mode);
                        }
                        break;
                    case (byte)DB.RequestType.ChangeLocationCenterInside:
                    case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                        {
                            _issueWiring.WiringIssueDate = WiringIssueDate.SelectedDate == null ? DB.GetServerDate() : (DateTime)WiringIssueDate.SelectedDate;
                            _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                            wiring.RequestID = _request.ID;
                            wiring.WiringInsertDate = DB.GetServerDate();
                            wiring.Status = (int)WiringStatusComboBox.SelectedValue;
                            WiringDB.SaveWiringIssue(_request, _issueWiring, wiring);
                        }
                        break;
                    case (byte)DB.RequestType.Dischargin:
                        {
                            _issueWiring.WiringIssueDate = WiringIssueDate.SelectedDate == null ? DB.GetServerDate() : (DateTime)WiringIssueDate.SelectedDate;
                            _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                            wiring.RequestID = _request.ID;
                            wiring.WiringInsertDate = DB.GetServerDate();
                            wiring.Status = (int)WiringStatusComboBox.SelectedValue;

                            WiringDB.SaveWiringIssue(_request, _issueWiring, wiring);
                        }
                        break;
                    case (byte)DB.RequestType.RefundDeposit:
                        {
                            _issueWiring.WiringIssueDate = WiringIssueDate.SelectedDate == null ? DB.GetServerDate() : (DateTime)WiringIssueDate.SelectedDate;
                            _request.StatusID = (int)WiringStatusComboBox.SelectedValue;

                            wiring.RequestID = _request.ID;
                            wiring.WiringInsertDate = DB.GetServerDate();
                            wiring.Status = (int)WiringStatusComboBox.SelectedValue;

                            WiringDB.SaveWiringIssue(_request, _issueWiring, wiring);
                        }
                        break;
                    case (byte)DB.RequestType.ChangeNo:
                        {
                            _issueWiring.WiringIssueDate = WiringIssueDate.SelectedDate == null ? DB.GetServerDate() : (DateTime)WiringIssueDate.SelectedDate;
                            _request.StatusID = (int)WiringStatusComboBox.SelectedValue;

                            wiring.RequestID = _request.ID;
                            wiring.WiringInsertDate = DB.GetServerDate();
                            wiring.Status = (int)WiringStatusComboBox.SelectedValue;

                            WiringDB.SaveWiringIssue(_request, _issueWiring, wiring);
                        }
                        break;
                    case (byte)DB.RequestType.E1:
                    case (byte)DB.RequestType.E1Link:
                    case (byte)DB.RequestType.VacateE1:
                        {
                            _issueWiring.WiringIssueDate = WiringIssueDate.SelectedDate == null ? DB.GetServerDate() : (DateTime)WiringIssueDate.SelectedDate;
                            _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                            wiring.RequestID = _request.ID;
                            wiring.WiringInsertDate = DB.GetServerDate();
                            wiring.Status = (int)WiringStatusComboBox.SelectedValue;
                            WiringDB.SaveWiringIssue(_request, _issueWiring, wiring);
                        }
                        break;
                    case (byte)DB.RequestType.SpecialWire:
                    case (byte)DB.RequestType.SpecialWireOtherPoint:
                        {
                            _issueWiring.WiringIssueDate = WiringIssueDate.SelectedDate == null ? DB.GetServerDate() : (DateTime)WiringIssueDate.SelectedDate;
                            _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                            wiring.RequestID = _request.ID;
                            wiring.WiringInsertDate = DB.GetServerDate();
                            wiring.Status = (int)WiringStatusComboBox.SelectedValue;
                            WiringDB.SaveWiringIssue(_request, _issueWiring, wiring);
                        }
                        break;
                    case (byte)DB.RequestType.VacateSpecialWire:
                        {
                            _issueWiring.WiringIssueDate = WiringIssueDate.SelectedDate == null ? DB.GetServerDate() : (DateTime)WiringIssueDate.SelectedDate;
                            _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                            wiring.RequestID = _request.ID;
                            wiring.WiringInsertDate = DB.GetServerDate();
                            wiring.Status = (int)WiringStatusComboBox.SelectedValue;
                            WiringDB.SaveWiringIssue(_request, _issueWiring, wiring);
                        }
                        break;
                    case (byte)DB.RequestType.ChangeLocationSpecialWire:
                        {
                            _issueWiring.WiringIssueDate = WiringIssueDate.SelectedDate == null ? DB.GetServerDate() : (DateTime)WiringIssueDate.SelectedDate;
                            _request.StatusID = (int)WiringStatusComboBox.SelectedValue;
                            wiring.RequestID = _request.ID;
                            wiring.WiringInsertDate = DB.GetServerDate();
                            wiring.Status = (int)WiringStatusComboBox.SelectedValue;
                            WiringDB.SaveWiringIssue(_request, _issueWiring, wiring);
                        }
                        break;
                }

                ShowSuccessMessage("ذخیره سیم بندی انجام شد");


                Load();
                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("ذخیره سیم بندی انجام نشد", ex);
            }

            base.Confirm();

            return IsSaveSuccess;
        }
        public override bool Deny()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    base.RequestID = _request.ID;
                    _request.ModifyDate = DB.GetServerDate();
                    _request.Detach();
                    DB.Save(_request);

                    Wiring wiring = Data.WiringDB.GetInfoWiringByInvestigatePossibility(_investigateInfoSummary.investigate.ID);
                    if (wiring != null && wiring.IssueWiringID != null)
                    {
                        IssueWiring issueWiring = Data.IssueWiringDB.GetIssueWiringByID((int)wiring.IssueWiringID);
                        if (issueWiring != null)
                        {
                            DB.Delete<Wiring>(wiring.ID);

                            DB.Delete<IssueWiring>(issueWiring.ID);
                        }


                    }
                    ts.Complete();
                    IsRejectSuccess = true;
                }

            }
            catch (Exception ex)
            {

                ShowErrorMessage("خطا در رد درخواست", ex);
            }

            base.Deny();
            return IsRejectSuccess;
        }
        public override bool Forward()
        {

            Save();
            if (IsSaveSuccess == true)
                IsForwardSuccess = true;
            return IsForwardSuccess;
        }
        #endregion
    }
}







