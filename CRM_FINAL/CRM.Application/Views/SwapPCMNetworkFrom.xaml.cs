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
    public partial class SwapPCMNetworkFrom : Local.RequestFormBase
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

        #endregion

        #region Constructor

        public SwapPCMNetworkFrom()
        {
            InitializeComponent();
        }
        public SwapPCMNetworkFrom(long requestID)
            : this()
        {
            _requestID = requestID;
            Initialize();
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

        private void LoadData()
        {
            //milad doran
            //ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };

            //TODO:rad 13950611
            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
            if (_requestID == 0)
            {
                _reqeust = new Request();
                _swapPCM = new Data.SwapPCM();
            }
            else
            {
                _reqeust = RequestDB.GetRequestByID(_requestID);
                _swapPCM = SwapPCMDB.GetSwapPCMByID(_requestID);

                if (_swapPCM.CompletionDate.HasValue)
                {
                    MessageBox.Show("عملیات جابجایی انجام شده است", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    FromTelephonTextBox.Text = _swapPCM.FromTelephoneNo.ToString();
                    FromSearchButton_Click(null, null);

                    ToTelephonTextBox.Text = _swapPCM.ToTelephoneNo.ToString();
                    ToSearchButton_Click(null, null);
                }
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
                        MessageBox.Show("اطلاعات فنی تلفن موجود نمی باشد", "", MessageBoxButton.OK, MessageBoxImage.Error);
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
                            MessageBox.Show("این تلفن بر روی پی سی ام قرار دارد ", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("اطلاعات فنی تلفن موجود نمی باشد", "", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (!_swapPCM.CompletionDate.HasValue)
                {

                    DateTime currentDataTime = DB.GetServerDate();

                    using (TransactionScope ts = new TransactionScope())
                    {

                        FromBucht = Data.BuchtDB.GetBuchetByID(FromAssignmentInfo.BuchtID);

                        ToBucht = Data.BuchtDB.GetBuchetByID(ToAssignmentInfo.BuchtID);

                        RequestLog requestLog = new RequestLog();
                        requestLog.IsReject = false;
                        requestLog.RequestTypeID = (int)DB.RequestType.SwapPCM;
                        requestLog.TelephoneNo = (long)FromTelephoneItem.TelephoneNo;
                        requestLog.ToTelephoneNo = (long)ToTelephoneItem.TelephoneNo;

                        if (FromAssignmentInfo != null && FromAssignmentInfo.Customer != null)
                            requestLog.CustomerID = FromAssignmentInfo.Customer.CustomerID;
                        requestLog.UserID = DB.currentUser.ID;
                        requestLog.Date = currentDataTime;


                        Data.Schema.SwapPCM swapPCM = new Data.Schema.SwapPCM();
                        swapPCM.Date = currentDataTime;
                        swapPCM.FromPCMBucht = FromAssignmentInfo.Connection;
                        swapPCM.FromTelephonNo = (long)FromTelephoneItem.TelephoneNo;
                        swapPCM.FromMUID = FromAssignmentInfo.MUID;

                        swapPCM.ToTelephonNo = (long)ToTelephoneItem.TelephoneNo;
                        swapPCM.ToBucht = ToAssignmentInfo.Connection;




                        List<Bucht> AllPCMBucht = BuchtDB.GetBuchtByCabinetInputIDs(new List<long> { (long)FromBucht.CabinetInputID });

                        List<Bucht> AllPCMPortBucht = AllPCMBucht.Where(t => t.BuchtTypeID == (int)DB.BuchtType.InLine || t.BuchtTypeID == (int)DB.BuchtType.OutLine).ToList();
                        AllPCMPortBucht.ForEach(t => t.CabinetInputID = (int)ToBucht.CabinetInputID);
                        List<PostContact> oldPostcontact = PostContactDB.GetPostContactByIDs(AllPCMPortBucht.Where(t => t.BuchtTypeID == (int)DB.BuchtType.InLine).Select(t => (long)t.ConnectionID).ToList());

                        Bucht AllPCMCabinetInputBucht = AllPCMBucht.Where(t => t.BuchtTypeID == (int)DB.BuchtType.CustomerSide).SingleOrDefault();

                        swapPCM.FromBucht = BuchtDB.GetConnectionByBuchtID(AllPCMCabinetInputBucht.ID);


                        AllPCMPortBucht.Where(t => t.BuchtTypeID == (int)DB.BuchtType.OutLine).SingleOrDefault().BuchtIDConnectedOtherBucht = ToBucht.ID;
                        AllPCMPortBucht.Where(t => t.BuchtTypeID == (int)DB.BuchtType.OutLine).SingleOrDefault().ConnectionID = ToBucht.ConnectionID;
                        ToBucht.BuchtIDConnectedOtherBucht = AllPCMCabinetInputBucht.BuchtIDConnectedOtherBucht;
                        AllPCMCabinetInputBucht.BuchtIDConnectedOtherBucht = null;
                        AllPCMCabinetInputBucht.SwitchPortID = FromBucht.SwitchPortID;
                        AllPCMCabinetInputBucht.Status = (int)DB.BuchtStatus.Connection;


                        AllPCMPortBucht.Where(t => t.ID == FromBucht.ID).SingleOrDefault().SwitchPortID = ToBucht.SwitchPortID;
                        ToBucht.SwitchPortID = null;
                        ToBucht.Status = (int)DB.BuchtStatus.AllocatedToInlinePCM;



                        PostContact newPostcontact = PostContactDB.GetPostContactByID((long)ToBucht.ConnectionID);

                        oldPostcontact.ForEach(t => { t.PostID = newPostcontact.PostID; t.ConnectionNo = newPostcontact.ConnectionNo; t.Detach(); });

                        newPostcontact.ConnectionType = (int)DB.PostContactConnectionType.PCMRemote;
                        newPostcontact.Status = (int)DB.PostContactStatus.NoCableConnection;
                        newPostcontact.Detach();
                        DB.Save(newPostcontact);

                        PostContact FromPostcontact = PostContactDB.GetPostContactByID((long)AllPCMCabinetInputBucht.ConnectionID);
                        FromPostcontact.ConnectionType = (int)DB.PostContactConnectionType.Noraml;
                        FromPostcontact.Status = (int)DB.PostContactStatus.CableConnection;
                        FromPostcontact.Detach();
                        DB.Save(FromPostcontact);



                        ToBucht.Detach();
                        AllPCMCabinetInputBucht.Detach();
                        DB.UpdateAll<Bucht>(new List<Bucht> { ToBucht, AllPCMCabinetInputBucht });

                        oldPostcontact.ForEach(t => t.Detach());
                        DB.UpdateAll<PostContact>(oldPostcontact);

                        oldPostcontact.ForEach(t => t.Detach());
                        DB.UpdateAll<PostContact>(oldPostcontact);

                        AllPCMPortBucht.ForEach(t => t.Detach());
                        DB.UpdateAll<Bucht>(AllPCMPortBucht);


                        requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.SwapPCM>(swapPCM, true));
                        requestLog.Date = currentDataTime;
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

                        _swapPCM.CompletionDate = currentDataTime;
                        _swapPCM.Detach();
                        DB.Save(_swapPCM);


                        ts.Complete();
                        IsSaveSuccess = true;
                        ShowSuccessMessage("عملیات انجام شد");
                    }
                }
                else
                {
                    IsSaveSuccess = true;
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
                DateTime currentDateTime = DB.GetServerDate();
                // حالا با استفاده از لاگ ثبت شده، دیتای مورد نظر برای چاپ را ایجاد می نماییم
                SwapPCMRequestLog dataForPrint = new SwapPCMRequestLog();
                dataForPrint.ToTelephoneNo = ToTelephoneItem.TelephoneNo.ToString();
                dataForPrint.FromTelephoneNo = FromTelephoneItem.TelephoneNo.ToString();
                dataForPrint.UserName = DB.currentUser.UserName;

                if (_swapPCM.MDFDate.HasValue)
                    dataForPrint.Date = _swapPCM.MDFDate.Value.ToPersian(Date.DateStringType.Short);
                else
                    dataForPrint.Date = currentDateTime.ToPersian(Date.DateStringType.Short);

                dataForPrint.FromBucht = FromAssignmentInfo.Connection;
                dataForPrint.FromMUID = FromAssignmentInfo.MUID;
                dataForPrint.FromPCMBucht = FromAssignmentInfo.Connection;
                dataForPrint.ToBucht = ToAssignmentInfo.Connection;

                //تنظیمات برای نمایش
                StiVariable variable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(currentDateTime));
                List<SwapPCMRequestLog> listForPrint = new List<SwapPCMRequestLog> { dataForPrint };
                CRM.Application.Local.ReportBase.SendToPrint(listForPrint, (int)DB.UserControlNames.SwapPCMReport, variable);
                IsPrintSuccess = true;
            }
            catch (Exception ex)
            {
                IsPrintSuccess = false;
                MessageBox.Show("خطا در چاپ", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return IsPrintSuccess;
        }

        //private void PrintCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    if ((!Helper.AllPropertyIsEmpty(this.FromAssignmentInfo) && !Helper.AllPropertyIsEmpty(this.ToAssignmentInfo)))
        //    {
        //        e.CanExecute = true;
        //    }
        //    else
        //    {
        //        e.CanExecute = false;
        //    }
        //}

        ////TOOD:rad
        //private void PrintCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (
        //            !string.IsNullOrEmpty(FromTelephonTextBox.Text.Trim())
        //            &&
        //            Helper.AllCharacterIsNumber(FromTelephonTextBox.Text.Trim())
        //            &&
        //            !string.IsNullOrEmpty(ToTelephonTextBox.Text.Trim())
        //            &&
        //            Helper.AllCharacterIsNumber(ToTelephonTextBox.Text.Trim())
        //           )
        //        {
        //            //در ابتدا باید لاگ ثبت  شده مربوط به تعویض پی سی ام مابین دو تلفن را بدست بیاوریم
        //            var result = RequestLogDB.GetRequestLogReport(long.Parse(FromTelephonTextBox.Text.Trim()), long.Parse(ToTelephonTextBox.Text.Trim()), DB.RequestType.SwapPCM);

        //            if (result != null)
        //            {
        //                RequestLogReport requestLogReport = result as RequestLogReport;

        //                // حالا با استفاده از لاگ ثبت شده، دیتای مورد نظر برای چاپ را ایجاد می نماییم
        //                SwapPCMRequestLog dataForPrint = new SwapPCMRequestLog();
        //                dataForPrint.ToTelephoneNo = (requestLogReport.ToTelephone.HasValue) ? requestLogReport.ToTelephone.Value.ToString() : string.Empty;
        //                dataForPrint.FromTelephoneNo = (requestLogReport.TelephoneNo.HasValue) ? requestLogReport.TelephoneNo.Value.ToString() : string.Empty;
        //                dataForPrint.UserName = requestLogReport.UserName;

        //                //حالا باید ریز اطلاعات تعویض پی سی ام را بدست بیاوریم
        //                if (requestLogReport.LogEntityDetails != null && requestLogReport.LogEntityDetails is SwapPCM)
        //                {
        //                    SwapPCM swapPcmDetails = new SwapPCM();
        //                    swapPcmDetails = requestLogReport.LogEntityDetails as SwapPCM;

        //                    dataForPrint.Date = swapPcmDetails.Date.ToPersian(Date.DateStringType.Short);
        //                    dataForPrint.FromBucht = swapPcmDetails.FromBucht;
        //                    dataForPrint.FromMUID = swapPcmDetails.FromMUID;
        //                    dataForPrint.FromPCMBucht = swapPcmDetails.FromPCMBucht;
        //                    dataForPrint.ToBucht = swapPcmDetails.ToBucht;

        //                    //تنظیمات برای نمایش
        //                    StiVariable variable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
        //                    List<SwapPCMRequestLog> listForPrint = new List<SwapPCMRequestLog> { dataForPrint };
        //                    CRM.Application.Local.ReportBase.SendToPrint(listForPrint, (int)DB.UserControlNames.SwapPCMReport, variable);
        //                }
        //                else
        //                {
        //                    MessageBox.Show("عدم دسترسی به جزئیات تعویض پی سی ام", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                }

        //            }
        //            else
        //            {
        //                MessageBox.Show(".مابین این دو تلفن عملیات تعویض پی سی ام صورت نگرفته است", "", MessageBoxButton.OK, MessageBoxImage.Information);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show(".لطفاً شماره تلفن ها را با دقت پر نمائید", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(ex, "خطا در کامند چاپ - تعویض پی سی ام - بخش برگردان");
        //        MessageBox.Show("خطا در چاپ", "", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        public override bool Deny()
        {
            try
            {
                this.RequestID = _requestID;
                IsDeleteSuccess = false;
                if (_swapPCM.CompletionDate.HasValue)
                {
                    MessageBox.Show("بعد از ثبت عملیات امکان رد نمی باشد", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {

                    IsDeleteSuccess = true;
                }

                return IsDeleteSuccess;
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

