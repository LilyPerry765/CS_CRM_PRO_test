using CRM.Data;
using CRM.Data.Schema;
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
using System.Xml.Linq;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for SwapTelephoneForm.xaml
    /// </summary>
    public partial class SwapTelephoneMDFForm : Local.RequestFormBase
    {
        #region Properties and Fields

        long ToTelephonNo = 0;

        long FromTelephonNo = 0;

        Telephone FromTelephoneItem { get; set; }

        Bucht FromBucht { get; set; }

        AssignmentInfo FromAssignmentInfo { get; set; }

        Telephone ToTelephoneItem { get; set; }

        Bucht ToBucht { get; set; }

        AssignmentInfo ToAssignmentInfo { get; set; }

        long _requestID { get; set; }

        Request _reqeust { get; set; }

        CRM.Data.SwapTelephone _swapTelephone { get; set; }

        public SwapTelephoneRequestLog SwapTelephoneRequestLogReportInfo { get; set; }

        //public bool HasSuccessfulSave
        //{
        //    get;
        //    set;
        //}

        #endregion

        #region Constructor

        public SwapTelephoneMDFForm()
        {
            InitializeComponent();
            this.SwapTelephoneRequestLogReportInfo = new SwapTelephoneRequestLog();
            Initialize();
        }
        public SwapTelephoneMDFForm(long requestID)
            : this()
        {
            _requestID = requestID;

        }
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }


        public void LoadData()
        {

            if (_requestID == 0)
            {
                _reqeust = new Request();
                _swapTelephone = new Data.SwapTelephone();
            }
            else
            {
                _reqeust = RequestDB.GetRequestByID(_requestID);
                _swapTelephone = SwapTelephoneDB.GetSwapTelephoneByID(_requestID);

                FromTelephonTextBox.Text = _swapTelephone.FromTelephoneNo.ToString();
                FromSearchButton_Click(null, null);

                ToTelephonTextBox.Text = _swapTelephone.ToTelephoneNo.ToString();
                ToSearchButton_Click(null, null);

                //بخش زیر به خاطر چاپ به صورت گروهی در RequestsInbox
                //اضافه شد
                //البته باید یک متد نوشته شود که با استفاده از شناسه های درخواست ، دیتای مرود نیاز برای گزارش فراهم کند به طور مثال
                //TODO: Must define ReportDB.GetSwapTelephonesByRequestsId(List<long> requestsId)
                SwapTelephoneRequestLogReportInfo.UserName = DB.currentUser.UserName;
                SwapTelephoneRequestLogReportInfo.TelephoneNo = FromTelephoneItem.TelephoneNo.ToString(); 
                SwapTelephoneRequestLogReportInfo.ToTelephoneNo = ToTelephoneItem.TelephoneNo.ToString();
                SwapTelephoneRequestLogReportInfo.Date = DB.GetServerDate().ToPersian(Date.DateStringType.Short);

                SwapTelephoneRequestLogReportInfo.FromVerticalColumnNo = FromAssignmentInfo.VerticalColumnNo;
                SwapTelephoneRequestLogReportInfo.FromVerticalRowNo = FromAssignmentInfo.VerticalRowNo;
                SwapTelephoneRequestLogReportInfo.FromBuchtNo = FromAssignmentInfo.BuchtNo;
                SwapTelephoneRequestLogReportInfo.FromPcmCabinetInputColumnNo = FromAssignmentInfo.PCMCabinetInputColumnNo;
                SwapTelephoneRequestLogReportInfo.FromPcmCabinetInputRowNo = FromAssignmentInfo.PCMCabinetInputRowNo;
                SwapTelephoneRequestLogReportInfo.FromPcmCabinetInputBuchtNo = FromAssignmentInfo.PCMCabinetInputBuchtNo;
                SwapTelephoneRequestLogReportInfo.FromBuchtID = FromAssignmentInfo.BuchtID.ToString();
                SwapTelephoneRequestLogReportInfo.FromCabinet = FromAssignmentInfo.CabinetName.ToString();
                SwapTelephoneRequestLogReportInfo.FromCabinetID = FromAssignmentInfo.CabinetID.ToString();
                SwapTelephoneRequestLogReportInfo.FromCabinetInput = FromAssignmentInfo.InputNumber.ToString();
                SwapTelephoneRequestLogReportInfo.FromCabinetInputID = FromAssignmentInfo.CabinetInputID.ToString();
                SwapTelephoneRequestLogReportInfo.FromConnectionNo = FromAssignmentInfo.Connection;
                SwapTelephoneRequestLogReportInfo.FromCustomerID = FromAssignmentInfo.Customer.ID.ToString();
                SwapTelephoneRequestLogReportInfo.FromCustomerName = FromAssignmentInfo.Customer.FirstNameOrTitle ?? "" + " " + FromAssignmentInfo.Customer.LastName ?? "";
                SwapTelephoneRequestLogReportInfo.FromPost = FromAssignmentInfo.PostName.ToString();
                SwapTelephoneRequestLogReportInfo.FromPostContact = FromAssignmentInfo.PostContact.ToString();
                SwapTelephoneRequestLogReportInfo.FromPostContactID = FromAssignmentInfo.PostContactID.ToString();
                SwapTelephoneRequestLogReportInfo.FromPostID = FromAssignmentInfo.PostID.ToString();
                SwapTelephoneRequestLogReportInfo.FromMUID = FromAssignmentInfo.MUID;

                SwapTelephoneRequestLogReportInfo.ToVerticalColumnNo = ToAssignmentInfo.VerticalColumnNo;
                SwapTelephoneRequestLogReportInfo.ToVerticalRowNo = ToAssignmentInfo.VerticalRowNo;
                SwapTelephoneRequestLogReportInfo.ToBuchtNo = ToAssignmentInfo.BuchtNo;
                SwapTelephoneRequestLogReportInfo.ToPcmCabinetInputColumnNo = ToAssignmentInfo.PCMCabinetInputColumnNo;
                SwapTelephoneRequestLogReportInfo.ToPcmCabinetInputRowNo = ToAssignmentInfo.PCMCabinetInputRowNo;
                SwapTelephoneRequestLogReportInfo.ToPcmCabinetInputBuchtNo = ToAssignmentInfo.PCMCabinetInputBuchtNo;
                SwapTelephoneRequestLogReportInfo.ToBuchtID = ToAssignmentInfo.BuchtID.ToString();
                SwapTelephoneRequestLogReportInfo.ToCabinet = ToAssignmentInfo.CabinetName.ToString();
                SwapTelephoneRequestLogReportInfo.ToCabinetID = ToAssignmentInfo.CabinetID.ToString();
                SwapTelephoneRequestLogReportInfo.ToCabinetInput = ToAssignmentInfo.InputNumber.ToString();
                SwapTelephoneRequestLogReportInfo.ToCabinetInputID = ToAssignmentInfo.CabinetInputID.ToString();
                SwapTelephoneRequestLogReportInfo.ToConnectionNo = ToAssignmentInfo.Connection;
                SwapTelephoneRequestLogReportInfo.ToCustomerID = ToAssignmentInfo.Customer.ID.ToString();
                SwapTelephoneRequestLogReportInfo.ToCustomerName = ToAssignmentInfo.Customer.FirstNameOrTitle ?? "" + " " + FromAssignmentInfo.Customer.LastName ?? "";
                SwapTelephoneRequestLogReportInfo.ToPost = ToAssignmentInfo.PostName.ToString();
                SwapTelephoneRequestLogReportInfo.ToPostContact = ToAssignmentInfo.PostContact.ToString();
                SwapTelephoneRequestLogReportInfo.ToPostContactID = ToAssignmentInfo.PostContactID.ToString();
                SwapTelephoneRequestLogReportInfo.ToPostID = ToAssignmentInfo.PostID.ToString();
                SwapTelephoneRequestLogReportInfo.ToMUID = ToAssignmentInfo.MUID;
            }
        }

        #endregion

        #region Methods
        private void Initialize()
        {
            this.ToAssignmentInfo = new AssignmentInfo();
            this.FromAssignmentInfo = new AssignmentInfo();
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
        }

        public override bool Save()
        {

            try
            {

                if (!_swapTelephone.CompletionDate.HasValue)
                {

                    using (TransactionScope ts = new TransactionScope())
                    {

                        ToBucht = Data.BuchtDB.GetBuchetByID(ToAssignmentInfo.BuchtID);
                        FromBucht = Data.BuchtDB.GetBuchetByID(FromAssignmentInfo.BuchtID);

                        RequestLog requestLog = new RequestLog();
                        requestLog.IsReject = false;
                        requestLog.RequestTypeID = (int)DB.RequestType.SwapTelephone;
                        requestLog.TelephoneNo = (long)FromTelephoneItem.TelephoneNo;
                        requestLog.ToTelephoneNo = (long)ToTelephoneItem.TelephoneNo;
                        if (FromAssignmentInfo != null && FromAssignmentInfo.Customer != null)
                            requestLog.CustomerID = FromAssignmentInfo.Customer.CustomerID;
                        requestLog.UserID = DB.currentUser.ID;
                        requestLog.Date = DB.GetServerDate();


                        Data.Schema.SwapTelephone swapTelephone = new Data.Schema.SwapTelephone();
                        swapTelephone.Date = DB.GetServerDate();
                        swapTelephone.FromCabinetID = FromAssignmentInfo.CabinetID ?? 0;
                        swapTelephone.FromCabinet = FromAssignmentInfo.CabinetName ?? 0;
                        swapTelephone.FromCabinetInputID = FromAssignmentInfo.CabinetInputID ?? 0;
                        swapTelephone.FromCabinetInput = FromAssignmentInfo.InputNumber ?? 0;
                        swapTelephone.FromPostContactID = (long)FromAssignmentInfo.PostContactID;
                        swapTelephone.FromPostContact = (int)FromAssignmentInfo.PostContact;
                        swapTelephone.FromPostID = (int)FromAssignmentInfo.PostID;
                        swapTelephone.FromPost = (int)FromAssignmentInfo.PostName;
                        swapTelephone.FromBuchtID = (long)FromAssignmentInfo.BuchtID;
                        swapTelephone.FromConnectionNo = FromAssignmentInfo.Connection;
                        swapTelephone.FromCustomerID = FromAssignmentInfo.Customer.ID;
                        swapTelephone.FromCustomerName = FromAssignmentInfo.CustomerName;

                        swapTelephone.ToCabinetID = ToAssignmentInfo.CabinetID ?? 0;
                        swapTelephone.ToCabinet = ToAssignmentInfo.CabinetName ?? 0;
                        swapTelephone.ToCabinetInputID = ToAssignmentInfo.CabinetInputID ?? 0;
                        swapTelephone.ToCabinetInput = ToAssignmentInfo.InputNumber ?? 0;
                        swapTelephone.ToPostContactID = (long)ToAssignmentInfo.PostContactID;
                        swapTelephone.ToPostContact = (int)ToAssignmentInfo.PostContact;
                        swapTelephone.ToPostID = (int)ToAssignmentInfo.PostID;
                        swapTelephone.ToPost = (int)ToAssignmentInfo.PostName;
                        swapTelephone.ToBuchtID = (long)ToAssignmentInfo.BuchtID;
                        swapTelephone.ToConnectionNo = ToAssignmentInfo.Connection;
                        swapTelephone.ToCustomerID = ToAssignmentInfo.Customer.ID;
                        swapTelephone.ToCustomerName = ToAssignmentInfo.CustomerName;

                        int switchPortID = (int)ToBucht.SwitchPortID;
                        ToBucht.SwitchPortID = (int)FromBucht.SwitchPortID;
                        FromBucht.SwitchPortID = switchPortID;



                        ToBucht.Detach();
                        FromBucht.Detach();
                        DB.UpdateAll<Bucht>(new List<Bucht> { ToBucht, FromBucht });


                        requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.SwapTelephone>(swapTelephone, true));
                        requestLog.Date = DB.GetServerDate();
                        requestLog.Detach();
                        DB.Save(requestLog, true);




                        if (FromTelephoneItem.CutDate.HasValue && !FromTelephoneItem.ConnectDate.HasValue)
                        {
                            FromTelephoneItem.Status = (int)DB.TelephoneStatus.Cut;
                            FromTelephoneItem.Detach();
                            DB.Save(FromTelephoneItem);
                        }
                        else
                        {
                            FromTelephoneItem.Status = (int)DB.TelephoneStatus.Connecting;
                            FromTelephoneItem.Detach();
                            DB.Save(FromTelephoneItem);

                        }


                        if (ToTelephoneItem.CutDate.HasValue && !ToTelephoneItem.ConnectDate.HasValue)
                        {
                            ToTelephoneItem.Status = (int)DB.TelephoneStatus.Cut;
                            ToTelephoneItem.Detach();
                            DB.Save(ToTelephoneItem);
                        }
                        else
                        {
                            ToTelephoneItem.Status = (int)DB.TelephoneStatus.Connecting;
                            ToTelephoneItem.Detach();
                            DB.Save(ToTelephoneItem);

                        }

                        _swapTelephone.CompletionDate = DB.GetServerDate();
                        _swapTelephone.Detach();
                        DB.Save(_swapTelephone);

                        ts.Complete();
                        ShowSuccessMessage("عملیات انجام شد");
                    }
                }
                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            return IsSaveSuccess;
        }

        #endregion

        #region EventHandlers

        private void FromSearchButton_Click(object sender, RoutedEventArgs e)
        {
            //this.HasSuccessfulSave = false;

            try
            {
                if (!string.IsNullOrEmpty(FromTelephonTextBox.Text.Trim()))
                {
                    if (!long.TryParse(FromTelephonTextBox.Text.Trim(), out FromTelephonNo))
                        throw new Exception("تلفن وارد شده صحیح نمی باشد");

                    FromTelephoneItem = Data.TelephoneDB.GetTelephoneByTelephoneNo(FromTelephonNo);

                    FromAssignmentInfo = DB.GetAllInformationByTelephoneNo(FromTelephonNo);

                    if (FromAssignmentInfo != null)
                    {

                        if (FromAssignmentInfo.MUID != null)
                        {
                            FromPCMInfoLabel.Visibility = Visibility.Visible;
                            FromPCMInfoTextBox.Visibility = Visibility.Visible;
                        }

                        FromExchangeFromGroupBox.DataContext = FromAssignmentInfo;
                    }
                    else
                    {
                        this.FromAssignmentInfo = new AssignmentInfo();
                        Folder.MessageBox.ShowError("اطلاعات فنی تلفن موجود نمی باشد");
                    }
                }
                else
                {
                    FromTelephonTextBox.Focus();
                    MessageBox.Show(".لطفاً فیلد شماره تلفن را پر نمائید", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی تلفن - تعویض شماره - بخش برگردان");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ToSearchButton_Click(object sender, RoutedEventArgs e)
        {
            //this.HasSuccessfulSave = false;

            try
            {
                if (!string.IsNullOrEmpty(ToTelephonTextBox.Text.Trim()))
                {
                    if (!long.TryParse(ToTelephonTextBox.Text.Trim(), out ToTelephonNo))
                        throw new Exception("تلفن وارد شده صحیح نمی باشد");

                    ToTelephoneItem = Data.TelephoneDB.GetTelephoneByTelephoneNo(ToTelephonNo);

                    ToAssignmentInfo = DB.GetAllInformationByTelephoneNo(ToTelephonNo);

                    if (ToAssignmentInfo != null)
                    {

                        if (ToAssignmentInfo.MUID != null)
                        {
                            ToPCMInfoLabel.Visibility = Visibility.Visible;
                            ToPCMInfoTextBox.Visibility = Visibility.Visible;
                        }

                        ToExchangeFromGroupBox.DataContext = ToAssignmentInfo;
                    }
                    else
                    {
                        this.ToAssignmentInfo = new AssignmentInfo();
                        Folder.MessageBox.ShowError("اطلاعات فنی تلفن موجود نمی باشد");
                    }
                }
                else
                {
                    ToTelephonTextBox.Focus();
                    MessageBox.Show(".لطفاً فیلد شماره تلفن را پر نمائید", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی تلفم - تعویض شماره - بخش برگردان");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public override bool Print()
        {
            try
            {
                IsPrintSuccess = false;

                SwapTelephoneRequestLog dataForPrint = new SwapTelephoneRequestLog();
                dataForPrint.UserName = DB.currentUser.UserName;
                dataForPrint.TelephoneNo = FromTelephoneItem.TelephoneNo.ToString(); // (requestLogReport.TelephoneNo.HasValue) ? requestLogReport.TelephoneNo.Value.ToString() : string.Empty;
                dataForPrint.ToTelephoneNo = ToTelephoneItem.TelephoneNo.ToString(); //(requestLogReport.ToTelephone.HasValue) ? requestLogReport.ToTelephone.Value.ToString() : string.Empty;
                CRM.Data.Schema.SwapTelephone swapTelephoneDetails = new CRM.Data.Schema.SwapTelephone();

                dataForPrint.Date = DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                
                dataForPrint.FromAdslColumnNo = FromAssignmentInfo.ADSLVerticalColumnNo;
                dataForPrint.FromAdslRowNo = FromAssignmentInfo.ADSLVerticalRowNo;
                dataForPrint.FromAdslBuchtNo = FromAssignmentInfo.ADSLBuchtNo;
                dataForPrint.FromVerticalColumnNo = FromAssignmentInfo.VerticalColumnNo;
                dataForPrint.FromVerticalRowNo = FromAssignmentInfo.VerticalRowNo;
                dataForPrint.FromBuchtNo = FromAssignmentInfo.BuchtNo;
                dataForPrint.FromPcmCabinetInputColumnNo = FromAssignmentInfo.PCMCabinetInputColumnNo;
                dataForPrint.FromPcmCabinetInputRowNo = FromAssignmentInfo.PCMCabinetInputRowNo;
                dataForPrint.FromPcmCabinetInputBuchtNo = FromAssignmentInfo.PCMCabinetInputBuchtNo;
                dataForPrint.FromBuchtID = FromAssignmentInfo.BuchtID.ToString();
                dataForPrint.FromCabinet = FromAssignmentInfo.CabinetName.ToString();
                dataForPrint.FromCabinetID = FromAssignmentInfo.CabinetID.ToString();
                dataForPrint.FromCabinetInput = FromAssignmentInfo.InputNumber.ToString();
                dataForPrint.FromCabinetInputID = FromAssignmentInfo.CabinetInputID.ToString();
                dataForPrint.FromConnectionNo = FromAssignmentInfo.Connection;
                dataForPrint.FromCustomerID = FromAssignmentInfo.Customer.ID.ToString();
                dataForPrint.FromCustomerName = FromAssignmentInfo.Customer.FirstNameOrTitle ?? "" + " " + FromAssignmentInfo.Customer.LastName ?? "";
                dataForPrint.FromPost = FromAssignmentInfo.PostName.ToString();
                dataForPrint.FromPostContact = FromAssignmentInfo.PostContact.ToString();
                dataForPrint.FromPostContactID = FromAssignmentInfo.PostContactID.ToString();
                dataForPrint.FromPostID = FromAssignmentInfo.PostID.ToString();
                dataForPrint.FromMUID = FromAssignmentInfo.MUID;

                dataForPrint.ToAdslColumNo = ToAssignmentInfo.ADSLVerticalColumnNo;
                dataForPrint.ToAdslRowNo = ToAssignmentInfo.ADSLVerticalRowNo;
                dataForPrint.ToAdslBuchtNo = ToAssignmentInfo.ADSLBuchtNo;
                dataForPrint.ToVerticalColumnNo = ToAssignmentInfo.VerticalColumnNo;
                dataForPrint.ToVerticalRowNo = ToAssignmentInfo.VerticalRowNo;
                dataForPrint.ToBuchtNo = ToAssignmentInfo.BuchtNo;
                dataForPrint.ToPcmCabinetInputColumnNo = ToAssignmentInfo.PCMCabinetInputColumnNo;
                dataForPrint.ToPcmCabinetInputRowNo = ToAssignmentInfo.PCMCabinetInputRowNo;
                dataForPrint.ToPcmCabinetInputBuchtNo = ToAssignmentInfo.PCMCabinetInputBuchtNo;
                dataForPrint.ToBuchtID = ToAssignmentInfo.BuchtID.ToString();
                dataForPrint.ToCabinet = ToAssignmentInfo.CabinetName.ToString();
                dataForPrint.ToCabinetID = ToAssignmentInfo.CabinetID.ToString();
                dataForPrint.ToCabinetInput = ToAssignmentInfo.InputNumber.ToString();
                dataForPrint.ToCabinetInputID = ToAssignmentInfo.CabinetInputID.ToString();
                dataForPrint.ToConnectionNo = ToAssignmentInfo.Connection;
                dataForPrint.ToCustomerID = ToAssignmentInfo.Customer.ID.ToString();
                dataForPrint.ToCustomerName = ToAssignmentInfo.Customer.FirstNameOrTitle ?? "" + " " + FromAssignmentInfo.Customer.LastName ?? "";
                dataForPrint.ToPost = ToAssignmentInfo.PostName.ToString();
                dataForPrint.ToPostContact = ToAssignmentInfo.PostContact.ToString();
                dataForPrint.ToPostContactID = ToAssignmentInfo.PostContactID.ToString();
                dataForPrint.ToPostID = ToAssignmentInfo.PostID.ToString();
                dataForPrint.ToMUID = ToAssignmentInfo.MUID;

                //تنظیمات برای نمایش 
                DateTime now = DB.GetServerDate();
                StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(now, Helper.DateStringType.Short));
                StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(now, Helper.DateStringType.Time));
                StiVariable cityVariable = new StiVariable("City", "City", DB.PersianCity);
                List<SwapTelephoneRequestLog> listForPrint = new List<SwapTelephoneRequestLog>() { dataForPrint };
                CRM.Application.Local.ReportBase.SendToPrint(listForPrint, (int)DB.UserControlNames.SwapTelephoneMDFWiringReport, true, dateVariable, timeVariable, cityVariable);


                //if (
                //     !string.IsNullOrEmpty(FromTelephonTextBox.Text.Trim())
                //     &&
                //     Helper.AllCharacterIsNumber(FromTelephonTextBox.Text.Trim())
                //     &&
                //     !string.IsNullOrEmpty(ToTelephonTextBox.Text.Trim())
                //     &&
                //     Helper.AllCharacterIsNumber(ToTelephonTextBox.Text.Trim())
                //   )
                //{
                //لاگ ثبت شده برای تلفن ها را گرفته  تا عملیات هایی را بر روی آن انجام دهیم
                //var result = RequestLogDB.GetRequestLogReport(long.Parse(FromTelephonTextBox.Text.Trim()), long.Parse(ToTelephonTextBox.Text.Trim()), DB.RequestType.SwapTelephone);

                //if (result != null)
                //{
                //    RequestLogReport requestLogReport = result as RequestLogReport;

                //    // حالا با استفاده از لاگ ثبت شده، دیتای مورد نظر برای چاپ را ایجاد می نماییم
                //    SwapTelephoneRequestLog dataForPrint = new SwapTelephoneRequestLog();
                //    dataForPrint.UserName = requestLogReport.UserName;
                //    dataForPrint.TelephoneNo = (requestLogReport.TelephoneNo.HasValue) ? requestLogReport.TelephoneNo.Value.ToString() : string.Empty;
                //    dataForPrint.ToTelephoneNo = (requestLogReport.ToTelephone.HasValue) ? requestLogReport.ToTelephone.Value.ToString() : string.Empty;

                //    //حالا باید ریز اطلاعات تعویض شماره را بدست بیاوریم
                //    if (requestLogReport.LogEntityDetails != null && requestLogReport.LogEntityDetails is CRM.Data.Schema.SwapTelephone)
                //    {
                //        CRM.Data.Schema.SwapTelephone swapTelephoneDetails = new CRM.Data.Schema.SwapTelephone();
                //        swapTelephoneDetails = requestLogReport.LogEntityDetails as CRM.Data.Schema.SwapTelephone;
                //        dataForPrint.Date = swapTelephoneDetails.Date.ToPersian(Date.DateStringType.Short);
                //        dataForPrint.FromBuchtID = swapTelephoneDetails.FromBuchtID.ToString();
                //        dataForPrint.FromCabinet = swapTelephoneDetails.FromCabinet.ToString();
                //        dataForPrint.FromCabinetID = swapTelephoneDetails.FromCabinetID.ToString();
                //        dataForPrint.FromCabinetInput = swapTelephoneDetails.FromCabinetInput.ToString();
                //        dataForPrint.FromCabinetInputID = swapTelephoneDetails.FromCabinetInputID.ToString();
                //        dataForPrint.FromConnectionNo = swapTelephoneDetails.FromConnectionNo;
                //        dataForPrint.FromCustomerID = swapTelephoneDetails.FromCustomerID.ToString();
                //        dataForPrint.FromCustomerName = swapTelephoneDetails.FromCustomerName;
                //        dataForPrint.FromPost = swapTelephoneDetails.FromPost.ToString();
                //        dataForPrint.FromPostContact = swapTelephoneDetails.FromPostContact.ToString();
                //        dataForPrint.FromPostContactID = swapTelephoneDetails.FromPostContactID.ToString();
                //        dataForPrint.FromPostID = swapTelephoneDetails.FromPostID.ToString();

                //        dataForPrint.ToBuchtID = swapTelephoneDetails.ToBuchtID.ToString();
                //        dataForPrint.ToCabinet = swapTelephoneDetails.ToCabinet.ToString();
                //        dataForPrint.ToCabinetID = swapTelephoneDetails.ToCabinetID.ToString();
                //        dataForPrint.ToCabinetInput = swapTelephoneDetails.ToCabinetInput.ToString();
                //        dataForPrint.ToCabinetInputID = swapTelephoneDetails.ToCabinetInputID.ToString();
                //        dataForPrint.ToConnectionNo = swapTelephoneDetails.ToConnectionNo;
                //        dataForPrint.ToCustomerID = swapTelephoneDetails.ToCustomerID.ToString();
                //        dataForPrint.ToCustomerName = swapTelephoneDetails.ToCustomerName;
                //        dataForPrint.ToPost = swapTelephoneDetails.ToPost.ToString();
                //        dataForPrint.ToPostContact = swapTelephoneDetails.ToPostContact.ToString();
                //        dataForPrint.ToPostContactID = swapTelephoneDetails.ToPostContactID.ToString();
                //        dataForPrint.ToPostID = swapTelephoneDetails.ToPostID.ToString();

                //        //تنظیمات برای نمایش 
                //        StiVariable variable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                //        List<SwapTelephoneRequestLog> listForPrint = new List<SwapTelephoneRequestLog>() { dataForPrint };
                //        CRM.Application.Local.ReportBase.SendToPrint(listForPrint, (int)DB.UserControlNames.SwapTelephoneReport, variable);

                //        IsPrintSuccess = true;
                //    }
                //    else
                //    {
                //        MessageBox.Show("عدم دسترسی به جزئیات تعویض شماره", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    }

                //}
                //else
                //{
                //    MessageBox.Show(".مابین این دو شماره تلفن عملیات تعویض شماره صورت نگرفته است", "", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                //}
                //else
                //{
                //    MessageBox.Show(".لطفاً شماره تلفن ها را با دقت پر نمائید", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                //}



            }
            catch (Exception ex)
            {
                IsPrintSuccess = false;
                Logger.Write(ex, "خطا در کامند چاپ - تعویض شماره - بخش برگردان");
                MessageBox.Show("خطا در چاپ", "", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            return IsPrintSuccess;
        }

        public override bool Forward()
        {
            try
            {
                this.RequestID = _requestID;
                Save();
                if (IsSaveSuccess)
                {
                    IsForwardSuccess = true;
                }
                else
                {
                    IsForwardSuccess = false;
                }
            }
            catch
            {
                IsForwardSuccess = false;
            }

            return IsForwardSuccess;
        }

        public override bool Deny()
        {
            this.RequestID = _requestID;
            IsDeleteSuccess = false;
            if (_swapTelephone.CompletionDate.HasValue)
            {
                Folder.MessageBox.ShowError("بعد از ثبت عملیات امکان رد نمی باشد");
            }
            else
            {

                IsDeleteSuccess = true;
            }

            return IsDeleteSuccess;
        }



        #endregion


    }
}

