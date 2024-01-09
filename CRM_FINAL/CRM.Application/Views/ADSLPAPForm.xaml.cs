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
using System.Xml.Linq;

namespace CRM.Application.Views
{
    public partial class ADSLPAPForm : Local.RequestFormBase
    {
        #region Properties

        private Request _Request { get; set; }
        private Data.ADSLPAPRequest _ADSLPAPRequest { get; set; }
        private PAPInfo _PAPInfo { get; set; }
        private Center _Center { get; set; }
        private Address _CustomerAddress { get; set; }
        private ADSL _ADSL { get; set; }
        private Telephone _Telephone { get; set; }
        private RequestLog _RequestLog { get; set; }
        private bool isShowResult = false;
        private Service1 aDSLService { get; set; }

        #endregion

        #region Constructors

        public ADSLPAPForm(long requestID)
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
            aDSLService = new Service1();
        }

        public void LoadData()
        {
            _Request = Data.RequestDB.GetRequestByID(RequestID);
            _ADSLPAPRequest =  DB.SearchByPropertyName<Data.ADSLPAPRequest>("ID", RequestID).SingleOrDefault();

            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            {
                _PAPInfo = DB.SearchByPropertyName<PAPInfo>("ID", _ADSLPAPRequest.PAPInfoID).SingleOrDefault();
                if (_PAPInfo == null)
                {
                    PAPInfoLabel.Visibility = Visibility.Visible;
                    PAPInfoLabel.Content = " * شرکت PAP درخواست دهنده در سیستم موجود نمی باشد !";
                    isShowResult = true;
                    PAPInfoNameTextBox.Text = " * ";
                    PAPInfoNameTextBox.Foreground = Brushes.Red;
                }
                else
                {
                    if ((bool)!_PAPInfo.LoginStatus)
                    {
                        PAPInfoLabel.Visibility = Visibility.Visible;
                        PAPInfoLabel.Content = " * شرکت PAP درخواست دهنده غیر فعال می باشد !";
                        isShowResult = true;
                        PAPInfoNameTextBox.Text = _PAPInfo.Title;
                        PAPInfoNameTextBox.Foreground = Brushes.Red;
                    }
                    else
                        PAPInfoNameTextBox.Text = _PAPInfo.Title;

                    //_Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo( _ADSLPAPRequest.TelephoneNo).SingleOrDefault();
                    //if (_Telephone == null)


                    if (!aDSLService.Is_Phone_Exist(_ADSLPAPRequest.TelephoneNo.ToString()))
                        ShowErrorLabel("* شماره تلفن ثبت شده در سیستم موجود نمی باشد !");
                    else
                    {
                        if (aDSLService.Tel_Have_ADSl_Port(_ADSLPAPRequest.TelephoneNo.ToString()))
                            ShowErrorLabel("* شماره وارد شده دارای ADSL می باشد !");
                        else
                        {
                            if (aDSLService.Phone_Is_PCM(_ADSLPAPRequest.TelephoneNo.ToString()))
                                ShowErrorLabel("* امکان تخصیص ADSL به این شماره وجود ندارد !");
                            else
                            {
                                if (aDSLService.TelDissectionStatus(_ADSLPAPRequest.TelephoneNo.ToString()))
                                    ShowErrorLabel("* شماره وارد شده قطع می باشد !");
                                else
                                {
                                    System.Data.DataTable addressDateTable = aDSLService.Get_Costumer_Address(_ADSLPAPRequest.TelephoneNo.ToString());
                                    PostalCodeTextBox.Text = addressDateTable.Rows[0]["PostCode"].ToString();
                                    AddressTextBox.Text = addressDateTable.Rows[0]["Address"].ToString();
                                    //_CustomerAddress = DB.SearchByPropertyName<CustomerAddress>("ID", _Telephone.CustomerAddressID).SingleOrDefault();
                                    TelephoneNoTextBox.Text = _ADSLPAPRequest.TelephoneNo.ToString();
                                    //PostalCodeTextBox.Text = _CustomerAddress.PostalCode;
                                    //AddressTextBox.Text = _CustomerAddress.Address;
                                }
                            }
                        }
                    }
                }

                _Center = Data.CenterDB.GetCenterById(  _Request.CenterID);
                CenterTextBox.Text = _Center.CenterName;

                CustomerTextBox.Text = _ADSLPAPRequest.Customer;
                CustomerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSLPAPRequest.CustomerStatus);
                RequestDateTextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                InstalDateTextBox.Text = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPInstalTimeOut), (int)_ADSLPAPRequest.InstalTimeOut);
                SplitorBuchtTextBox.Text = _ADSLPAPRequest.SplitorBucht;
                LineBuchtTextBox.Text = _ADSLPAPRequest.LineBucht;

                CommentCustomersTextBox.Text = _ADSLPAPRequest.CommentCustomers;
                CommentsGroupBox.Visibility = Visibility.Visible;

                MDFCommentLabel.Visibility = Visibility.Collapsed;
                MDFCommentTextBox.Visibility = Visibility.Collapsed;
                FinalCommentLabel.Visibility = Visibility.Collapsed;
                FinalCommentTextBox.Visibility = Visibility.Collapsed;

                if (_Request.PreviousAction == (byte)DB.Action.Reject)
                {
                    MDFCommentTextBox.Text = _ADSLPAPRequest.MDFComment;
                    MDFCommentTextBox.IsReadOnly = true;
                    MDFCommentLabel.Visibility = Visibility.Visible;
                    MDFCommentTextBox.Visibility = Visibility.Visible;
                }

                if (isShowResult)
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
                else
                {
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
                    NoProblemLabel.Visibility = Visibility.Visible;
                    NoProblemLabel.Content = " * اطلاعات صحیح می باشد.";
                }
            }

            if ((_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID) || (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID))
            {
                _PAPInfo = DB.SearchByPropertyName<PAPInfo>("ID", _ADSLPAPRequest.PAPInfoID).SingleOrDefault();
                //_Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo( _ADSLPAPRequest.TelephoneNo).SingleOrDefault();
                //_CustomerAddress = DB.SearchByPropertyName<CustomerAddress>("ID", _Telephone.CustomerAddressID).SingleOrDefault();
                _Center = Data.CenterDB.GetCenterById(  _Request.CenterID);

                PAPInfoNameTextBox.Text = _PAPInfo.Title;
                //TelephoneNoTextBox.Text = _Telephone.TelephoneNo.ToString();
                TelephoneNoTextBox.Text = _ADSLPAPRequest.TelephoneNo.ToString();
                //PostalCodeTextBox.Text = _CustomerAddress.PostalCode;
                //AddressTextBox.Text = _CustomerAddress.Address;
                System.Data.DataTable addressDateTable = aDSLService.Get_Costumer_Address(_ADSLPAPRequest.TelephoneNo.ToString());
                PostalCodeTextBox.Text = addressDateTable.Rows[0]["PostCode"].ToString();
                AddressTextBox.Text = addressDateTable.Rows[0]["Address"].ToString();
                CenterTextBox.Text = _Center.CenterName;
                CustomerTextBox.Text = _ADSLPAPRequest.Customer;
                CustomerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSLPAPRequest.CustomerStatus);
                RequestDateTextBox.Text = Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.DateTime);
                InstalDateTextBox.Text = DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPInstalTimeOut), (int)_ADSLPAPRequest.InstalTimeOut);
                SplitorBuchtTextBox.Text = _ADSLPAPRequest.SplitorBucht;
                LineBuchtTextBox.Text = _ADSLPAPRequest.LineBucht;

                ResultGroupBox.Visibility = Visibility.Collapsed;
                CommentCustomersTextBox.Text = _ADSLPAPRequest.CommentCustomers;
                CommentCustomersTextBox.IsReadOnly = true;

                MDFCommentTextBox.Text = _ADSLPAPRequest.MDFComment;
                MDFCommentTextBox.IsReadOnly = true;
                MDFCommentLabel.Visibility = Visibility.Visible;
                MDFCommentTextBox.Visibility = Visibility.Visible;

                FinalCommentTextBox.Text = _ADSLPAPRequest.FinalComment;

                CommentRejectLabel.Visibility = Visibility.Collapsed;
                CommentRejectListBox.Visibility = Visibility.Collapsed;
                CommentsGroupBox.Visibility = Visibility.Visible;

                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID)
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };

                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID)
                {
                    FinalCommentTextBox.IsReadOnly = true;
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };
                }
            }
        }

        public override bool Confirm()
        {
            try
            {
                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                {
                    _ADSLPAPRequest.CommentCustomers = CommentCustomersTextBox.Text;

                    _ADSLPAPRequest.Detach();
                    DB.Save(_ADSLPAPRequest);
                }

                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID)
                {
                    _ADSLPAPRequest.Status = (byte)DB.ADSLPAPRequestStatus.Completed;
                    _ADSLPAPRequest.FinalUserID = DB.CurrentUser.ID;
                    _ADSLPAPRequest.FinalDate = DB.GetServerDate();
                    _ADSLPAPRequest.FinalComment = FinalCommentTextBox.Text;

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

                    RequestForADSL.SaveADSLPAPRequest(_Request, _ADSLPAPRequest, null/*_ADSL*/, null, null, null, _RequestLog, false);
                }

                IsConfirmSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            return IsConfirmSuccess;
        }

        public override bool Deny()
        {
            try
            {
                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                {
                    _ADSLPAPRequest.CommentCustomers = CommentCustomersTextBox.Text;
                    if (CommentRejectListBox.SelectedValue != null)
                        _ADSLPAPRequest.CommnetReject = CommentCustomersTextBox.Text + " ، " + Helper.GetEnumDescriptionByValue(typeof(DB.ADSLPAPRejectCommnet), Convert.ToInt32(CommentRejectListBox.SelectedValue));
                    else
                        _ADSLPAPRequest.CommnetReject = CommentCustomersTextBox.Text;
                    _ADSLPAPRequest.Status = (byte)DB.ADSLPAPRequestStatus.Reject;

                    _ADSLPAPRequest.Detach();
                    DB.Save(_ADSLPAPRequest);
                }

                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            return IsRejectSuccess;
        }

        private void ShowErrorLabel(string message)
        {
            TelephoneNoLabel.Visibility = Visibility.Visible;
            TelephoneNoLabel.Content = message;
            isShowResult = true;
            TelephoneNoTextBox.Text = _ADSLPAPRequest.TelephoneNo.ToString();
            TelephoneNoTextBox.Foreground = Brushes.Red;
        }

        #endregion
    }
}
