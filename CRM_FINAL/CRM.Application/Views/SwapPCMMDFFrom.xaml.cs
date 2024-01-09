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
    /// Interaction logic for SwapPCMFrom.xaml
    /// </summary>
    public partial class SwapPCMMDFFrom : Local.RequestFormBase
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

        long _requestID = 0;
        Request _reqeust { get; set; }
        CRM.Data.SwapPCM _swapPCM { get; set; }

        public SwapPCMRequestLog SwapPCMRequestLogReportInfo { get; set; }

        #endregion

        #region Constructor

        public SwapPCMMDFFrom()
        {
            InitializeComponent();
        }
        public SwapPCMMDFFrom(long requestID)
            : this()
        {
            _requestID = requestID;
            Initialize();
            this.SwapPCMRequestLogReportInfo = new SwapPCMRequestLog();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            this.FromAssignmentInfo = new AssignmentInfo();
            this.ToAssignmentInfo = new AssignmentInfo();
        }

        #endregion

        #region EventHandlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();

        }
        public void LoadData()
        {

            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };

            if (_requestID == 0)
            {
                _reqeust = new Request();
                _swapPCM = new Data.SwapPCM();
            }
            else
            {
                _reqeust = RequestDB.GetRequestByID(_requestID);
                _swapPCM = SwapPCMDB.GetSwapPCMByID(_requestID);


                FromTelephonTextBox.Text = _swapPCM.FromTelephoneNo.ToString();
                FromSearchButton_Click(null, null);

                ToTelephonTextBox.Text = _swapPCM.ToTelephoneNo.ToString();
                ToSearchButton_Click(null, null);

                //بخش زیر به خاطر چاپ به صورت گروهی در RequestsInbox
                //اضافه شد
                //البته باید یک متد نوشته شود که با استفاده از شناسه های درخواست ، دیتای مرود نیاز برای گزارش فراهم کند به طور مثال
                //TODO: Must define ReportDB.GetSwapPCMsByRequestsId(List<long> requestsId)
                SwapPCMRequestLogReportInfo.ToTelephoneNo = ToTelephoneItem.TelephoneNo.ToString();
                SwapPCMRequestLogReportInfo.FromTelephoneNo = FromTelephoneItem.TelephoneNo.ToString();
                SwapPCMRequestLogReportInfo.FromVerticalColumnNo = FromAssignmentInfo.VerticalColumnNo;
                SwapPCMRequestLogReportInfo.FromVerticalRowNo = FromAssignmentInfo.VerticalRowNo;
                SwapPCMRequestLogReportInfo.FromBuchtNo = FromAssignmentInfo.BuchtNo;
                SwapPCMRequestLogReportInfo.FromPcmCabinetInputColumnNo = FromAssignmentInfo.PCMCabinetInputColumnNo;
                SwapPCMRequestLogReportInfo.FromPcmCabinetInputRowNo = FromAssignmentInfo.PCMCabinetInputRowNo;
                SwapPCMRequestLogReportInfo.FromPcmCabinetInputBuchtNo = FromAssignmentInfo.PCMCabinetInputBuchtNo;
                SwapPCMRequestLogReportInfo.ToVerticalColumnNo = ToAssignmentInfo.VerticalColumnNo;
                SwapPCMRequestLogReportInfo.ToVerticalRowNo = ToAssignmentInfo.VerticalRowNo;
                SwapPCMRequestLogReportInfo.ToBuchtNo = ToAssignmentInfo.BuchtNo;
                SwapPCMRequestLogReportInfo.ToPcmCabinetInputColumnNo = ToAssignmentInfo.PCMCabinetInputColumnNo;
                SwapPCMRequestLogReportInfo.ToPcmCabinetInputRowNo = ToAssignmentInfo.PCMCabinetInputRowNo;
                SwapPCMRequestLogReportInfo.ToPcmCabinetInputBuchtNo = ToAssignmentInfo.PCMCabinetInputBuchtNo;
                SwapPCMRequestLogReportInfo.MdfDescription = string.Empty;
            }
        }

        private void FromSearchButton_Click(object sender, RoutedEventArgs e)
        {
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
                Logger.Write(ex, "خطا در جستجوی تلفن - تعویض پی سی ام - بخش برگردان");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ToSearchButton_Click(object sender, RoutedEventArgs e)
        {
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

                        if (ToAssignmentInfo.MUID == null)
                        {
                            ToExchangeFromGroupBox.DataContext = ToAssignmentInfo;
                        }
                        else
                        {
                            this.ToAssignmentInfo = new AssignmentInfo();
                            Folder.MessageBox.ShowError("این تلفن بر روی پی سی ام قرار دارد ");
                        }
                    }
                    else
                    {
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
                Logger.Write(ex, "خطا در جستجوی تلفن - تعویض پی سی ام - بخش برگردان");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public override bool Save()
        {
            try
            {

                using (TransactionScope ts = new TransactionScope())
                {

                    _swapPCM.MDFDate = DB.GetServerDate();
                    _swapPCM.Detach();
                    DB.Save(_swapPCM);

                    ts.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage("عملیات انجام شد");
                }


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

        public override bool Print()
        {
            try
            {

                // حالا با استفاده از لاگ ثبت شده، دیتای مورد نظر برای چاپ را ایجاد می نماییم
                SwapPCMRequestLog dataForPrint = new SwapPCMRequestLog();

                //old reportTemplate - DB.UserControlNames.SwapPCMReport
                //dataForPrint.UserName = DB.currentUser.UserName;
                //if (_swapPCM.MDFDate.HasValue)
                //    dataForPrint.Date = _swapPCM.MDFDate.Value.ToPersian(Date.DateStringType.Short);
                //else
                //    dataForPrint.Date = DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                //dataForPrint.FromBucht = FromAssignmentInfo.Connection;
                //dataForPrint.FromMUID = FromAssignmentInfo.MUID;
                //dataForPrint.FromPCMBucht = FromAssignmentInfo.Connection;
                //dataForPrint.ToBucht = ToAssignmentInfo.Connection;
                //old reportTemplate - end

                //new reportTemplate - DB.UserControlNames.SwapPCMMDFWiringReport
                dataForPrint.ToTelephoneNo = ToTelephoneItem.TelephoneNo.ToString();
                dataForPrint.FromTelephoneNo = FromTelephoneItem.TelephoneNo.ToString();
                dataForPrint.FromVerticalColumnNo = FromAssignmentInfo.VerticalColumnNo;
                dataForPrint.FromVerticalRowNo = FromAssignmentInfo.VerticalRowNo;
                dataForPrint.FromBuchtNo = FromAssignmentInfo.BuchtNo;
                dataForPrint.FromPcmCabinetInputColumnNo = FromAssignmentInfo.PCMCabinetInputColumnNo;
                dataForPrint.FromPcmCabinetInputRowNo = FromAssignmentInfo.PCMCabinetInputRowNo;
                dataForPrint.FromPcmCabinetInputBuchtNo = FromAssignmentInfo.PCMCabinetInputBuchtNo;
                dataForPrint.ToVerticalColumnNo = ToAssignmentInfo.VerticalColumnNo;
                dataForPrint.ToVerticalRowNo = ToAssignmentInfo.VerticalRowNo;
                dataForPrint.ToBuchtNo = ToAssignmentInfo.BuchtNo;
                dataForPrint.ToPcmCabinetInputColumnNo = ToAssignmentInfo.PCMCabinetInputColumnNo;
                dataForPrint.ToPcmCabinetInputRowNo = ToAssignmentInfo.PCMCabinetInputRowNo;
                dataForPrint.ToPcmCabinetInputBuchtNo = ToAssignmentInfo.PCMCabinetInputBuchtNo;
                dataForPrint.MdfDescription = string.Empty;

                //تنظیمات برای نمایش
                DateTime now = DB.GetServerDate();
                StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(now, Helper.DateStringType.Short));
                StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(now, Helper.DateStringType.Time));
                StiVariable cityVariable = new StiVariable("City", "City", DB.PersianCity);
                List<SwapPCMRequestLog> listForPrint = new List<SwapPCMRequestLog> { dataForPrint };
                CRM.Application.Local.ReportBase.SendToPrint(listForPrint, (int)DB.UserControlNames.SwapPCMMDFWiringReport, true, dateVariable, timeVariable, cityVariable);
                IsPrintSuccess = true;

                //if (
                //    !string.IsNullOrEmpty(FromTelephonTextBox.Text.Trim())
                //    &&
                //    Helper.AllCharacterIsNumber(FromTelephonTextBox.Text.Trim())
                //    &&
                //    !string.IsNullOrEmpty(ToTelephonTextBox.Text.Trim())
                //    &&
                //    Helper.AllCharacterIsNumber(ToTelephonTextBox.Text.Trim())
                //   )
                //{
                //    //در ابتدا باید لاگ ثبت  شده مربوط به تعویض پی سی ام مابین دو تلفن را بدست بیاوریم
                //    var result = RequestLogDB.GetRequestLogReport(long.Parse(FromTelephonTextBox.Text.Trim()), long.Parse(ToTelephonTextBox.Text.Trim()), DB.RequestType.SwapPCM);

                //    if (result != null)
                //    {
                //        RequestLogReport requestLogReport = result as RequestLogReport;

                //        // حالا با استفاده از لاگ ثبت شده، دیتای مورد نظر برای چاپ را ایجاد می نماییم
                //        SwapPCMRequestLog dataForPrint = new SwapPCMRequestLog();
                //        dataForPrint.ToTelephoneNo = (requestLogReport.ToTelephone.HasValue) ? requestLogReport.ToTelephone.Value.ToString() : string.Empty;
                //        dataForPrint.FromTelephoneNo = (requestLogReport.TelephoneNo.HasValue) ? requestLogReport.TelephoneNo.Value.ToString() : string.Empty;
                //        dataForPrint.UserName = requestLogReport.UserName;

                //        //حالا باید ریز اطلاعات تعویض پی سی ام را بدست بیاوریم
                //        if (requestLogReport.LogEntityDetails != null && requestLogReport.LogEntityDetails is SwapPCM)
                //        {
                //            SwapPCM swapPcmDetails = new SwapPCM();
                //            swapPcmDetails = requestLogReport.LogEntityDetails as SwapPCM;

                //            dataForPrint.Date = swapPcmDetails.Date.ToPersian(Date.DateStringType.Short);
                //            dataForPrint.FromBucht = swapPcmDetails.FromBucht;
                //            dataForPrint.FromMUID = swapPcmDetails.FromMUID;
                //            dataForPrint.FromPCMBucht = swapPcmDetails.FromPCMBucht;
                //            dataForPrint.ToBucht = swapPcmDetails.ToBucht;

                //            //تنظیمات برای نمایش
                //            StiVariable variable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                //            List<SwapPCMRequestLog> listForPrint = new List<SwapPCMRequestLog> { dataForPrint };
                //            CRM.Application.Local.ReportBase.SendToPrint(listForPrint, (int)DB.UserControlNames.SwapPCMReport, variable);
                //        }
                //        else
                //        {
                //            MessageBox.Show("عدم دسترسی به جزئیات تعویض پی سی ام", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                //        }

                //    }
                //    else
                //    {
                //        MessageBox.Show(".مابین این دو تلفن عملیات تعویض پی سی ام صورت نگرفته است", "", MessageBoxButton.OK, MessageBoxImage.Information);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show(".لطفاً شماره تلفن ها را با دقت پر نمائید", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                //}
            }
            catch (Exception ex)
            {
                IsPrintSuccess = false;
                MessageBox.Show("خطا در چاپ", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return IsPrintSuccess;
        }


        public override bool Deny()
        {

            this.RequestID = _requestID;

            try
            {
                _swapPCM.MDFDate = null;
                _swapPCM.Detach();
                DB.Save(_swapPCM);

                IsDeleteSuccess = true;
            }
            catch
            {
                IsDeleteSuccess = false;
            }
            return IsDeleteSuccess;
        }

        #endregion

    }
}

