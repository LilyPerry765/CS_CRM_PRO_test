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
    public partial class SwapPCMFrom : Local.RequestFormBase
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

      

        Telephone _passToTelephoneItem { get; set; }
        Telephone _passFromTelephoneItem { get; set; }

        long _requestID = 0;
        Request _reqeust { get; set; }
        CRM.Data.SwapPCM _swapPCM { get; set; }

        #endregion

        #region Constructor

        public SwapPCMFrom()
        {
            InitializeComponent();
        }
        public SwapPCMFrom(long requestID)
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
        public void LoadData()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
            if (_requestID == 0)
            {
                _reqeust = new Request();
                _swapPCM = new Data.SwapPCM();
            }
            else
            {
                _reqeust = RequestDB.GetRequestByID(_requestID);
                _swapPCM = SwapPCMDB.GetSwapPCMByID(_requestID);

                _passFromTelephoneItem = TelephoneDB.GetTelephoneByTelephoneNo(_swapPCM.FromTelephoneNo);
                _passToTelephoneItem = TelephoneDB.GetTelephoneByTelephoneNo(_swapPCM.ToTelephoneNo);

                FromTelephonTextBox.Text = _swapPCM.FromTelephoneNo.ToString();
                FromSearchButton_Click(null, null);

                ToTelephonTextBox.Text = _swapPCM.ToTelephoneNo.ToString();
                ToSearchButton_Click(null, null);


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
                FromSearchButton_Click(null, null);
                ToSearchButton_Click(null, null);

                if (FromTelephoneItem == null)
                {
                    throw new Exception("از تلفن پیدا نشد");
                }

                if (ToTelephoneItem == null)
                {
                    throw new Exception("به تلفن پیدا نشد");
                }

                if (FromAssignmentInfo == null)
                {
                    throw new Exception("اطلاعات فنی از تلفن پیدا نشد");
                }


                if (ToAssignmentInfo == null)
                {
                    throw new Exception("اطلاعات فنی به تلفن پیدا نشد");
                }

                if (!(FromAssignmentInfo.BuchtTypeID == (int)DB.BuchtType.InLine || FromAssignmentInfo.BuchtTypeID == (int)DB.BuchtType.OutLine))
                {
                    throw new Exception("از تلفن باید پی سی ام باشد");
                }

                if ((ToAssignmentInfo.BuchtTypeID == (int)DB.BuchtType.InLine || ToAssignmentInfo.BuchtTypeID == (int)DB.BuchtType.OutLine))
                {
                    throw new Exception("به تلفن نباید پی سی ام باشد");
                }

                if (ToTelephoneItem.CenterID != FromTelephoneItem.CenterID)
                    throw new Exception(" تعویض پی سی ام فقط برای تلفن های یک مرکز امکان پذیر می باشد");

                if (!(ToAssignmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.Normal && FromAssignmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.Normal))
                    throw new Exception(" تعویض پی سی ام فقط برای کافو معمولی به معمولی امکان پذیر می باشد");

                DateTime currentDateTime = DB.GetServerDate();

                using (TransactionScope ts = new TransactionScope())
                {


                    if (_requestID == 0)
                    {
                                            

                        string message = string.Empty;
                        bool inWaitingList = false;
                        if (DB.HasRestrictionsTelphone(FromTelephoneItem.TelephoneNo, out message, out inWaitingList))
                        {
                            throw new Exception(message);
                        }


                        string message2 = string.Empty;
                        bool inWaitingList2 = false;
                        if (DB.HasRestrictionsTelphone(ToTelephoneItem.TelephoneNo, out message2, out inWaitingList2))
                        {
                            throw new Exception(message2);
                        }

                        _reqeust.ID = DB.GenerateRequestID();
                        _reqeust.RequestTypeID = (int)DB.RequestType.SwapPCM;
                        _reqeust.StatusID = DB.GetStatus((int)DB.RequestType.SwapPCM, (int)DB.RequestStatusType.Start).ID;
                        _reqeust.TelephoneNo = FromAssignmentInfo.TelePhoneNo;
                        _reqeust.CenterID = (int)FromAssignmentInfo.CenterID;
                        _reqeust.CustomerID = FromAssignmentInfo.Customer.ID;
                        _reqeust.RelatedRequestID = null;
                        _reqeust.CreatorUserID = DB.currentUser.ID;
                        _reqeust.InsertDate = currentDateTime;
                        _reqeust.RequestDate = currentDateTime;
                        _reqeust.IsVisible = true;
                        _reqeust.Detach();
                        DB.Save(_reqeust, true);

                        _swapPCM.RequestID = _reqeust.ID;
                        _swapPCM.FromTelephoneNo = (long)FromTelephoneItem.TelephoneNo;
                        _swapPCM.ToTelephoneNo = (long)ToTelephoneItem.TelephoneNo;
                        _swapPCM.Detach();
                        DB.Save(_swapPCM, true);

                        FromTelephoneItem.Status = (int)DB.TelephoneStatus.Reserv;
                        FromTelephoneItem.Detach();
                        DB.Save(FromTelephoneItem);

                        ToTelephoneItem.Status = (int)DB.TelephoneStatus.Reserv;
                        ToTelephoneItem.Detach();
                        DB.Save(ToTelephoneItem);

                        _requestID = _reqeust.ID;
                    }
                    else
                    {

                        if (_passFromTelephoneItem.TelephoneNo != FromTelephoneItem.TelephoneNo)
                        {
                            if (_passFromTelephoneItem.CutDate.HasValue && !_passFromTelephoneItem.ConnectDate.HasValue)
                            {
                                _passFromTelephoneItem.Status = (int)DB.TelephoneStatus.Cut;
                                _passFromTelephoneItem.Detach();
                                DB.Save(_passFromTelephoneItem);
                            }
                            else
                            {
                                _passFromTelephoneItem.Status = (int)DB.TelephoneStatus.Connecting;
                                _passFromTelephoneItem.Detach();
                                DB.Save(_passFromTelephoneItem);

                            }

                            FromTelephoneItem.Status = (int)DB.TelephoneStatus.Reserv;
                            FromTelephoneItem.Detach();
                            DB.Save(FromTelephoneItem);

                        }

                        if (_passToTelephoneItem.TelephoneNo != ToTelephoneItem.TelephoneNo)
                        {
                            if (_passToTelephoneItem.CutDate.HasValue && !_passToTelephoneItem.ConnectDate.HasValue)
                            {
                                _passToTelephoneItem.Status = (int)DB.TelephoneStatus.Cut;
                                _passToTelephoneItem.Detach();
                                DB.Save(_passToTelephoneItem);
                            }
                            else
                            {

                                _passToTelephoneItem.Status = (int)DB.TelephoneStatus.Connecting;
                                _passToTelephoneItem.Detach();
                                DB.Save(_passToTelephoneItem);
                            }

                            ToTelephoneItem.Status = (int)DB.TelephoneStatus.Reserv;
                            ToTelephoneItem.Detach();
                            DB.Save(ToTelephoneItem);
                        }


                        _reqeust.TelephoneNo = FromAssignmentInfo.TelePhoneNo;
                        _reqeust.ModifyDate = currentDateTime;
                        _reqeust.Detach();
                        DB.Save(_reqeust);

                        _swapPCM.FromTelephoneNo = (long)FromTelephoneItem.TelephoneNo;
                        _swapPCM.ToTelephoneNo = (long)ToTelephoneItem.TelephoneNo;
                        _swapPCM.Detach();
                        DB.Save(_swapPCM);

                    }


                    ts.Complete();
                    IsSaveSuccess = true;

                }

                LoadData();
                ShowSuccessMessage("عملیات انجام شد");
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
               
                Save();
                this.RequestID = _requestID;
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

        public override bool Cancel()
        {

            try
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    if (_passFromTelephoneItem.CutDate.HasValue && !_passFromTelephoneItem.ConnectDate.HasValue)
                    {
                        _passFromTelephoneItem.Status = (int)DB.TelephoneStatus.Cut;
                        _passFromTelephoneItem.Detach();
                        DB.Save(_passFromTelephoneItem);
                    }
                    else
                    {
                        _passFromTelephoneItem.Status = (int)DB.TelephoneStatus.Connecting;
                        _passFromTelephoneItem.Detach();
                        DB.Save(_passFromTelephoneItem);

                    }



                    if (_passToTelephoneItem.CutDate.HasValue && !_passToTelephoneItem.ConnectDate.HasValue)
                    {
                        _passToTelephoneItem.Status = (int)DB.TelephoneStatus.Cut;
                        _passToTelephoneItem.Detach();
                        DB.Save(_passToTelephoneItem);
                    }
                    else
                    {

                        _passToTelephoneItem.Status = (int)DB.TelephoneStatus.Connecting;
                        _passToTelephoneItem.Detach();
                        DB.Save(_passToTelephoneItem);
                    }

                    _reqeust.IsCancelation = true;
                    _reqeust.Detach();
                    DB.Save(_reqeust);

                    Data.CancelationRequestList cancelationRequest = new CancelationRequestList();

                    cancelationRequest.ID = RequestID;
                    cancelationRequest.EntryDate = DB.GetServerDate();
                    cancelationRequest.UserID = Folder.User.Current.ID;

                    cancelationRequest.Detach();
                    DB.Save(cancelationRequest, true);

                    IsCancelSuccess = true;
                    ts.Complete();
                }
            }
            catch
            {
                IsCancelSuccess = false;
            }

            return IsCancelSuccess;

        }

        #endregion

    }
}
