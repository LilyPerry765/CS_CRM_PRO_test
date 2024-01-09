using System;
using System.Collections;
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
using System.Xml.Linq;
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class ChangeNameForm : Local.RequestFormBase
    {
        #region Properties

        private Data.Request _Request { get; set; }
        private Data.Center _Center { get; set; }
        private Data.Customer _OldCustomer { get; set; }
        private Data.Customer _NewCustomer { get; set; }
        private Data.Telephone _Telephone { get; set; }
        private Data.ChangeName _ChangeName { get; set; }
        private Data.Inquiry _Inquiry { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        string city = string.Empty;

        #endregion

        #region Constructors

        public ChangeNameForm(long requestID)
        {
            RequestID = requestID;

            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _Request = Data.RequestDB.GetRequestByID(RequestID);
            _ChangeName = Data.ChangeNameDB.GetChangeNameByID((long) RequestID);
            _OldCustomer = Data.CustomerDB.GetCustomerByID(_ChangeName.OldCustomerID);
            _NewCustomer = Data.CustomerDB.GetCustomerByID(_ChangeName.NewCustomerID);
            _Center = Data.CenterDB.GetCenterById(  _Request.CenterID);

            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID)
                InquiryInfo.Visibility = Visibility.Collapsed;

            TelephoneInfo.DataContext = DB.GetTelephoneInfoForRequest(_Request.TelephoneNo);

            OldNationalCodeTextBox.Text = _OldCustomer.NationalCodeOrRecordNo;
            OldCustomerNameTextBox.Text = _OldCustomer.FirstNameOrTitle + " " + _OldCustomer.LastName;

            NewNationalCodeNoTextBox.Text = _NewCustomer.NationalCodeOrRecordNo;
            NewCustomerNameTextBox.Text = _NewCustomer.FirstNameOrTitle + " " + _NewCustomer.LastName;
                        
            RequestInfo.DataContext = _Request;
            CenterNameTextBox.Text = _Center.CenterName;
            RequestDateTextBox.Text =Helper.GetPersianDate(_Request.RequestDate, Helper.DateStringType.Short);
            RequestLetterDateTextBox.Text = Helper.GetPersianDate(_Request.RequestLetterDate, Helper.DateStringType.Short);

            if (_ChangeName.HasCourtLetter)
            {
                CourtInfo.Visibility = Visibility.Visible;
                CourtInfo.DataContext = _ChangeName;
                CourtVerdictDateTextBox.Text = Helper.GetPersianDate(_ChangeName.CourtVerdictDate, Helper.DateStringType.Short);
                CourtInfo.Height = 80;

                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID)
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };
                else
                    ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
            }
            else
            {
                _Inquiry = Data.InquiryDB.GetInquiryByRequestID(RequestID);

                if (_Inquiry != null)
                {
                    if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID)
                    {
                        InquiryInfo.DataContext = _Inquiry;
                        InquiryGrid.Visibility = Visibility.Visible;
                        InquiryGrid.Margin = new System.Windows.Thickness { Bottom = 10 };

                        if (_Inquiry.Status == (byte)DB.InquiryStatus.Accept)
                        {
                            StatusLabel.Content = "بلامانع است.";
                            StatusLabel.Foreground = Brushes.Green;
                            StatusLabel.FontWeight = FontWeights.Bold;
                        }
                        else
                        {
                            StatusLabel.Content = "منع دارد!";
                            StatusLabel.Foreground = Brushes.Red;
                            StatusLabel.FontWeight = FontWeights.Bold;
                        }

                        ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };
                    }
                    else
                    {
                        if (DB.GetServerDate() < _Inquiry.InquiryResponseDate.Value.AddHours(24))
                        {
                            InquiryInfo.DataContext = _Inquiry;
                            InquiryGrid.Visibility = Visibility.Visible;
                            InquiryGrid.Margin = new System.Windows.Thickness { Bottom = 10 };

                            if (_Inquiry.Status == (byte)DB.InquiryStatus.Accept)
                            {
                                StatusLabel.Content = "بلامانع است.";
                                StatusLabel.Foreground = Brushes.Green;
                                StatusLabel.FontWeight = FontWeights.Bold;

                                ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
                            }
                            else
                            {
                                StatusLabel.Content = "منع دارد!";
                                StatusLabel.Foreground = Brushes.Red;
                                StatusLabel.FontWeight = FontWeights.Bold;

                                ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };
                            }
                        }
                        else
                        {
                            InquirytimeOutGrid.Visibility = Visibility.Visible;
                            InquiryInfo.IsEnabled = true;

                            ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };
                        }
                    }
                }

                InquiryInfo.Visibility = Visibility.Visible;
            }
        }

        public override bool Forward()
        {
            try
            {
                _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long) _Request.TelephoneNo);
                _Telephone.CustomerID = _ChangeName.NewCustomerID;
                
                using (TransactionScope MainTransactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (city == "semnan")
                    {

                        if (_ChangeName.DocumentsFileID == null && (FileBytes == null || Extension == string.Empty))
                        {
                            throw new Exception("اسکن سند اجباری می باشد.");
                        }
                        else
                        {
                            _ChangeName.DocumentsFileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
                        }
                    }
                    

                    _ChangeName.Detach();
                    DB.Save(_ChangeName);

                    RequestForChangeNameDB.SaveChangeNameRequest(_Request, null, null, _Telephone, false);

                    MainTransactionScope.Complete();
                 }

                IsForwardSuccess = true;
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("تغییر نام انجام نشد !", ex);
            }

            return IsForwardSuccess;
        }

        #endregion

        #region Event Handlers

        private void InquiryButton_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Status status = DB.GetStatus((int)DB.RequestType.ChangeName, (int)DB.RequestStatusType.Problem);
                _Request.StatusID = status.ID;

                RequestForChangeNameDB.SaveChangeNameRequest(_Request, _ChangeName, null, null, false);

                Local.PopupWindow.ShowSuccessMessage("درخواست استعلام مجدد ارسال شد.");
                InquiryButton.IsEnabled = false;

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                Local.PopupWindow.ShowErrorMessage("خطا در درخواست استعلام", ex);
            }
        }
        private void PrintCertificate_Click(object sender, RoutedEventArgs e)
        {
           
            IEnumerable result;
            result = ReportDB.GetChangeNameInfo(new List<int> { }, new List<int> { }, null, null, RequestID, null);
            foreach (ChangeNameInfo  item in result)
            {
                item.NewCustomer_BirthDateOrRecordDate = string.IsNullOrEmpty(item.NewCustomer_BirthDateOrRecordDate) ? "": Helper.GetPersianDate(DateTime.Parse(item.NewCustomer_BirthDateOrRecordDate), Helper.DateStringType.Short);
                item.OldCustomer_BirthDateOrRecordDate = string.IsNullOrEmpty(item.OldCustomer_BirthDateOrRecordDate) ? "": Helper.GetPersianDate(DateTime.Parse(item.OldCustomer_BirthDateOrRecordDate), Helper.DateStringType.Short);
                item.OldCustomer_Buyername = (item.OldCustomer_HasCountLetter) ? "امضاء نماینده دادگستری" : "امضاء فروشنده";
                if (item.NewCustomer_PersonType == 1)
                {
                      item.NewCustomer_BirthCertificateID = string.Empty;
                      item.NewCustomer_BirthDateOrRecordDate =string.Empty;
                      item.NewCustomer_FatherName = string.Empty;
                      item.NewCustomer_FirstName = string.Empty;
                      item.NewCustomer_IssuePlace =string.Empty;
                      item.NewCustomer_LastName = string.Empty;
                      item.NewCustomerNationalCode=string.Empty;
                }
                else
                {
                      item.NewFirstNameOrTitle = string.Empty;
                      item.NewCustomer_RecordNo =string.Empty;
                      item.NewCustomer_Agency = string.Empty;
                      item.NewCustomer_AgencyNumber = string.Empty;
                }

                if (item.OldCustomer_PersonType == 1)
                {
                    item.OldCustomer_BirthCertificateID = string.Empty;
                    item.OldCustomer_BirthDateOrRecordDate = string.Empty;
                    item.OldCustomer_FatherName = string.Empty;
                    item.OldCustomer_FirstName = string.Empty;
                    item.OldCustomer_IssuePlace = string.Empty;
                    item.OldCustomer_LastName = string.Empty;
                    item.OldCustomerNationalCode = string.Empty;
                }
                else
                {
                    item.OldFirstNameOrTitle = string.Empty;
                    item.OldCustomer_RecordNo = string.Empty;
                    item.OldCustomer_Agency = string.Empty;
                    item.OldCustomer_AgencyNumber = string.Empty;
                }
            }

            SendToPrint(result);
                    

        }
        private void SendToPrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeNameDocument);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
       

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        #endregion

        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            DocumentInputForm window = new DocumentInputForm();
            window.ShowDialog();

            FileBytes = window.FileBytes;
            Extension = window.Extension;

        }

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
        }
    }
}
