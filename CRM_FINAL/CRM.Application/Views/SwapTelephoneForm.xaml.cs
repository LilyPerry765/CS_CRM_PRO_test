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
    public partial class SwapTelephoneForm : Local.RequestFormBase
    {
        #region Properties and Fields

        long ToTelephonNo = 0;

        long FromTelephonNo = 0;

        Telephone FromTelephoneItem { get; set; }

        Telephone _passFromTelephoneItem { get; set; }

        Bucht FromBucht { get; set; }

        AssignmentInfo FromAssignmentInfo { get; set; }

        Telephone ToTelephoneItem { get; set; }
        Telephone _passToTelephoneItem { get; set; }

        Bucht ToBucht { get; set; }

        AssignmentInfo ToAssignmentInfo { get; set; }

        long _requestID = 0;

        Request _reqeust { get; set; }
        CRM.Data.SwapTelephone _swapTelephone { get; set; }


        //public bool HasSuccessfulSave
        //{
        //    get;
        //    set;
        //}

        #endregion

        #region Constructor

        public SwapTelephoneForm()
        {
            InitializeComponent();
        }
        public SwapTelephoneForm(long requestID):this()
        {
            _requestID = requestID;
            Initialize();
        }

        #endregion

        #region Methods
        private void Initialize()
        {
           
            this.ToAssignmentInfo = new AssignmentInfo();
            this.FromAssignmentInfo = new AssignmentInfo();
        }
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
                _swapTelephone = new Data.SwapTelephone();
            }
            else
            {
                _reqeust = RequestDB.GetRequestByID(_requestID);
                _swapTelephone = SwapTelephoneDB.GetSwapTelephoneByID(_requestID);

                _passFromTelephoneItem = TelephoneDB.GetTelephoneByTelephoneNo(_swapTelephone.FromTelephoneNo);
                _passToTelephoneItem = TelephoneDB.GetTelephoneByTelephoneNo(_swapTelephone.ToTelephoneNo);

                FromTelephonTextBox.Text = _swapTelephone.FromTelephoneNo.ToString();
                FromSearchButton_Click(null, null);

                ToTelephonTextBox.Text = _swapTelephone.ToTelephoneNo.ToString();
                ToSearchButton_Click(null, null);


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

                if (ToTelephoneItem.CenterID != FromTelephoneItem.CenterID)
                    throw new Exception(" تعویض تلفن فقط برای تلفن های یک مرکز امکان پذیر می باشد");

                if (!(ToAssignmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.Normal && FromAssignmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.Normal))
                {
                    if ((FromAssignmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.OpticalCabinet || FromAssignmentInfo.CabinetUsageTypeID != (int)DB.CabinetUsageType.WLL))
                    {
                        if (FromAssignmentInfo.CabinetID != ToAssignmentInfo.CabinetID)
                            throw new Exception(" تعویض تلفن نوری فقط درون همان کافو  امکان پذیر می باشد");
                    }
                    else
                    {
                        throw new Exception(" تعویض تلفن فقط برای کافو معمولی به معمولی امکان پذیر می باشد");
                    }
                }

                DateTime currentDateTime = DB.GetServerDate();

                using (TransactionScope ts = new TransactionScope())
                {
                    if(_requestID == 0)
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
                        _reqeust.RequestTypeID = (int)DB.RequestType.SwapTelephone;
                        _reqeust.StatusID = DB.GetStatus((int)DB.RequestType.SwapTelephone, (int)DB.RequestStatusType.Start).ID;
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

                        _swapTelephone.RequestID = _reqeust.ID;
                        _swapTelephone.FromTelephoneNo = (long)FromTelephoneItem.TelephoneNo;
                        _swapTelephone.ToTelephoneNo = (long)ToTelephoneItem.TelephoneNo;
                        _swapTelephone.Detach();
                        DB.Save(_swapTelephone, true);

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
                        
                        if( _passFromTelephoneItem.TelephoneNo != FromTelephoneItem.TelephoneNo)
                        { 
                            if(_passFromTelephoneItem.CutDate.HasValue && !_passFromTelephoneItem.ConnectDate.HasValue)
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

                        _swapTelephone.FromTelephoneNo = (long)FromTelephoneItem.TelephoneNo;
                        _swapTelephone.ToTelephoneNo = (long)ToTelephoneItem.TelephoneNo;
                        _swapTelephone.Detach();
                        DB.Save(_swapTelephone);

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

                    cancelationRequest.ID = _reqeust.ID;
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
                        FromExchangeFromGroupBox.DataContext = null;
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
                        ToExchangeFromGroupBox.DataContext = null;
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


        #endregion


    }
}
