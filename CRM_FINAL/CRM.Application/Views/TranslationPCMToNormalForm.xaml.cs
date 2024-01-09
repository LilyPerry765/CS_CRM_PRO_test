using CRM.Data;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TranslationPCMToNormalForm.xaml
    /// </summary>
    public partial class TranslationPCMToNormalForm : Local.RequestFormBase
    {
        private long _RequestID;
        int _centerID = 0;

        CRM.Data.TranslationPCMToNormal _translationPCMToNormal { get; set; }
        Request _reqeust { get; set; }

        PostContact  passOldPostContact;
        PostContact  passNewPostContact;
        CabinetInput passCabinetInput;

        public CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }
        public TranslationPCMToNormalForm()
        {
            InitializeComponent();
            Initialize();
        }


         public TranslationPCMToNormalForm(long reqeustID)
            : this()
        {
            this._RequestID = reqeustID;

        }
        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Cancelation };
        }
        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            _centerID = centerID;
            AfterCabinetNumberComboBox.ItemsSource = BeforCabinetNumberComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDByCabinetUsageType(_centerID, new List<int> { (int)DB.CabinetUsageType.Cable, (int)DB.CabinetUsageType.Normal, (int)DB.CabinetUsageType.WithoutCabinet});
            
        }
        private void PCMToNormalRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButtonChecked();
        }

        private void NormalToPCMRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButtonChecked();
        }
        private void PCMToPCMRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButtonChecked();
        }
        private void RadioButtonChecked()
        {
          if(PCMToNormalRadioButton.IsChecked == true)
          {
              ExchangeDetailGroupBox.Visibility = Visibility.Visible;
          }
          else if (NormalToPCMRadioButton.IsChecked == true)
          {
              ExchangeDetailGroupBox.Visibility = Visibility.Collapsed;
          }
          else if (PCMToPCMRadioButton.IsChecked == true)
          {
              ExchangeDetailGroupBox.Visibility = Visibility.Collapsed;
          }

        }

        private void BeforCabinetNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BeforCabinetNumberComboBox.SelectedValue != null)
            {
              BeforPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)BeforCabinetNumberComboBox.SelectedValue);
            }

        }

        private void BeforPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BeforPostComboBox.SelectedValue != null)
            {
                ConnectionDataGrid.ItemsSource = DB.GetAllInformationByPostIDAndWithOutpostContactType((int)BeforPostComboBox.SelectedValue, (byte)DB.PostContactConnectionType.PCMRemote);
                ConnectionDataGrid.Visibility = Visibility.Visible;
                this.ResizeWindow();
            }
        }

        private void AfterCabinetNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AfterCabinetNumberComboBox.SelectedValue != null)
            {

                AfterPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)AfterCabinetNumberComboBox.SelectedValue);

                CabinetInput.ItemsSource = Data.CabinetDB.GetFreeCabinetInputByCabinetIDWithTelephon((int)AfterCabinetNumberComboBox.SelectedValue);
            }
        }

        private void AfterPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AfterPostComboBox.SelectedValue != null)
            {
                NewConnectionDataGrid.ItemsSource = DB.GetAllInformationByPostIDAndWithOutpostContactType((int)AfterPostComboBox.SelectedValue, (byte)DB.PostContactConnectionType.PCMRemote);
                NewConnectionDataGrid.Visibility = Visibility.Visible;
                this.ResizeWindow();
            }
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_RequestID);
            _exchangeRequestInfo.RequestType = (int)DB.RequestType.PCMToNormal;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;


            if (_RequestID == 0)
            {

                _translationPCMToNormal = new CRM.Data.TranslationPCMToNormal();
                _translationPCMToNormal.Type = (byte)DB.TranslationPCMToNormalType.PCMToNormal;
            }
            else
            {

                _translationPCMToNormal = Data.TranslationPCMToNormalDB.GetTranslationPCMToNormalByID(_RequestID);
                _reqeust = Data.RequestDB.GetRequestByID(_RequestID);

                passOldPostContact = Data.PostContactDB.GetPostContactByID(_translationPCMToNormal.OldPostContactID);
                passNewPostContact = Data.PostContactDB.GetPostContactByID(_translationPCMToNormal.NewPostContactID);
                passCabinetInput = Data.CabinetInputDB.GetCabinetInputByID(_translationPCMToNormal.CabinetInputID ?? 0);
           
                BeforCabinetNumberComboBox.SelectedValue = _translationPCMToNormal.OldCabinetID;
                BeforCabinetNumberComboBox_SelectionChanged(null, null);

                BeforPostComboBox.SelectedValue = _translationPCMToNormal.OldPostID;
                BeforPostComboBox_SelectionChanged(null, null);


                AfterCabinetNumberComboBox.SelectedValue = _translationPCMToNormal.NewCabinetID;
                AfterCabinetNumberComboBox_SelectionChanged(null, null);

                AfterPostComboBox.SelectedValue = _translationPCMToNormal.NewPostID;
                AfterPostComboBox_SelectionChanged(null, null);

                if (Data.StatusDB.IsFinalStep(_reqeust.StatusID))
                {
                    ExchangeRequestInfoUserControl.IsEnabled = false;
                    TranslationTypeDetailGroupBox.IsEnabled = false;

                     BeforCabinetNumberComboBox .IsEnabled = false;
                     BeforPostComboBox.IsEnabled = false;

                     AfterCabinetNumberComboBox.IsEnabled = false;
                     AfterPostComboBox.IsEnabled = false;


                    ExchangeDetailGroupBox.IsEnabled = false;

                    ExchangeDetailGroupBox.Visibility = Visibility.Collapsed;
                

                }
                else
                {
                    ConnectionDataGrid.SelectedValue = _translationPCMToNormal.OldPostContactID;
                    NewConnectionDataGrid.SelectedValue = _translationPCMToNormal.NewPostContactID;
                }
            }
            this.DataContext = _translationPCMToNormal;
            if (_translationPCMToNormal.Type == (byte)DB.TranslationPCMToNormalType.NormalToPCM) NormalToPCMRadioButton.IsChecked = true; else PCMToNormalRadioButton.IsChecked = true;
        }

        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }
            try
            {
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {

                    _reqeust = _exchangeRequestInfo.Request;

                    if (_reqeust.ID != 0 && Data.StatusDB.IsFinalStep(_reqeust.StatusID))
                    {

                        _reqeust.Detach();
                        DB.Save(_reqeust, false);
                    }
                    else
                    {
                        _translationPCMToNormal = this.DataContext as CRM.Data.TranslationPCMToNormal;


                        if (passOldPostContact != null)
                        {
                            passOldPostContact.Status = (byte)DB.PostContactStatus.CableConnection;
                            passOldPostContact.Detach();
                            DB.Save(passOldPostContact);
                        }
                        if (passNewPostContact != null)
                        {
                            passNewPostContact.Status = (byte)DB.PostContactStatus.Free;
                            passNewPostContact.Detach();
                            DB.Save(passNewPostContact);
                        }

                        if (passCabinetInput != null)
                        {
                            passCabinetInput.Status = (byte)DB.CabinetInputStatus.healthy;
                            passCabinetInput.Detach();
                            DB.Save(passCabinetInput);
                        }

                        // verify data
                        if ((ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo) == null)
                            throw new Exception("لطفا اتصالی پست را انتخاب کنید");

                        CRM.Data.AssignmentInfo oldConnection = ConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;
                        if ((NewConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo) == null)
                            throw new Exception("لطفا اتصالی پست را انتخاب کنید");

                        if (oldConnection.TelePhoneNo == null || oldConnection.TelePhoneNo == 0)
                            throw new Exception("تلفن یافت نشد");
                        _reqeust.TelephoneNo = oldConnection.TelePhoneNo;




                        CRM.Data.AssignmentInfo newConnection = NewConnectionDataGrid.SelectedItem as CRM.Data.AssignmentInfo;

                        PostContact oldPostContact = Data.PostContactDB.GetPostContactByID(oldConnection.PostContactID ?? 0);

                        PostContact newPostContact = Data.PostContactDB.GetPostContactByID(newConnection.PostContactID ?? 0);


                        _translationPCMToNormal.OldPostContactID = oldConnection.PostContactID ?? 0;
                        _translationPCMToNormal.NewPostContactID = newConnection.PostContactID ?? 0;

                        if (NormalToPCMRadioButton.IsChecked == true)
                        {

                            _translationPCMToNormal.Type = (byte)DB.TranslationPCMToNormalType.NormalToPCM;

                            if (oldPostContact.ConnectionType != (byte)DB.PostContactConnectionType.Noraml)
                                throw new Exception("اتصالی معمولی صحیح انتخاب نشده است");


                            if (passOldPostContact  == null) 
                            {
                               if(oldPostContact.Status != (byte)DB.PostContactStatus.CableConnection)
                                 throw new Exception("لطفا یک اتصالی متصل انتخاب کنید");
                            }
                            else if(oldPostContact.ID != passOldPostContact.ID)
                            {
                                if(oldPostContact.Status != (byte)DB.PostContactStatus.CableConnection)
                                  throw new Exception("لطفا یک اتصالی متصل انتخاب کنید");
                            }

                            if (newPostContact.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal)
                                throw new Exception("اتصالی پی سی ام صحیح انتخاب نشده است");

                            if (passOldPostContact == null)
                            {
                                if (newPostContact.Status != (byte)DB.PostContactStatus.Free)
                                {
                                    throw new Exception("لطفا یک اتصال آزاد انتخاب کنید");
                                }
                            }
                            else if (newPostContact.ID != passNewPostContact.ID)
                            {
                                if (newPostContact.Status != (byte)DB.PostContactStatus.Free)
                                {
                                    throw new Exception("لطفا یک اتصال آزاد انتخاب کنید");
                                }
                            }

                        }
                        else if (PCMToNormalRadioButton.IsChecked == true)
                        {
                             _translationPCMToNormal.Type = (byte)DB.TranslationPCMToNormalType.PCMToNormal;

                            if (oldPostContact.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal)
                                throw new Exception("اتصالی پی سی ام صحیح انتخاب نشده است");

                            if (passOldPostContact == null  )
                            {
                                if(oldPostContact.Status != (byte)DB.PostContactStatus.CableConnection)
                                throw new Exception("لطفا یک اتصال متصل انتخاب کنید");
                            }
                            else if (oldPostContact.ID != passOldPostContact.ID)
                            {
                                if (oldPostContact.Status != (byte)DB.PostContactStatus.CableConnection)
                                    throw new Exception("لطفا یک اتصال متصل انتخاب کنید");
                            }


                            if (newPostContact.ConnectionType != (byte)DB.PostContactConnectionType.Noraml)
                                throw new Exception("اتصالی معمولی صحیح انتخاب نشده است");


                            if (passNewPostContact == null )
                            {
                                if(newPostContact.Status != (byte)DB.PostContactStatus.Free)
                                throw new Exception("لطفا یک اتصالی آزاد انتخاب کنید");
                            }
                            else if(newPostContact.ID != passNewPostContact.ID) 
                            {
                                if (newPostContact.Status != (byte)DB.PostContactStatus.Free)
                                    throw new Exception("لطفا یک اتصالی آزاد انتخاب کنید");
                            }


                            if (CabinetInput.SelectedValue == null)
                                throw new Exception("لطفا مرکزی را انتخاب کنید");



                            CabinetInput cabinetInput = Data.CabinetInputDB.GetCabinetInputByID(_translationPCMToNormal.CabinetInputID ?? 0);
                            cabinetInput.Status = (byte)DB.CabinetInputStatus.Exchange;
                            cabinetInput.Detach();
                            DB.Save(cabinetInput);

                        }
                        else if (PCMToPCMRadioButton.IsChecked == true)
                        {
                            _translationPCMToNormal.Type = (byte)DB.TranslationPCMToNormalType.PCMToPCM;


                            if (oldPostContact.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal)
                                throw new Exception("اتصالی پی سی ام صحیح انتخاب نشده است");


                            if (passOldPostContact == null )
                            {
                                if (oldPostContact.Status != (byte)DB.PostContactStatus.CableConnection)
                                    throw new Exception("لطفا یک اتصالی متصل انتخاب کنید");
                            }
                            else if( oldPostContact.ID != passOldPostContact.ID)
                            {
                                                if (oldPostContact.Status != (byte)DB.PostContactStatus.CableConnection)
                                    throw new Exception("لطفا یک اتصالی متصل انتخاب کنید");
                            }

                            if (newPostContact.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal)
                                throw new Exception("اتصالی پی سی ام صحیح انتخاب نشده است");

                            if (passOldPostContact == null )
                            {
                                if (newPostContact.Status != (byte)DB.PostContactStatus.Free)
                                {
                                    throw new Exception("لطفا یک اتصال آزاد انتخاب کنید");
                                }
                            }
                            else if(newPostContact.ID != passNewPostContact.ID)
                            {
                                if (newPostContact.Status != (byte)DB.PostContactStatus.Free)
                                {
                                    throw new Exception("لطفا یک اتصال آزاد انتخاب کنید");
                                }
                            }
                        }


                        oldPostContact.Status = (byte)DB.PostContactStatus.FullBooking;
                        oldPostContact.Detach();
                        DB.Save(oldPostContact);

                        newPostContact.Status = (byte)DB.PostContactStatus.FullBooking;
                        newPostContact.Detach();
                        DB.Save(newPostContact);


                        // 

                        if (_RequestID == 0)
                        {
                            _reqeust.ID = DB.GenerateRequestID();
                            _reqeust.RequestPaymentTypeID = 0;
                            _reqeust.IsViewed = false;
                            _reqeust.InsertDate = DB.GetServerDate();
                            _reqeust.StatusID = DB.GetStatus((int)DB.RequestType.PCMToNormal, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                            _reqeust.Detach();
                            DB.Save(_reqeust, true);

                            _translationPCMToNormal.ID = _reqeust.ID;
                            _translationPCMToNormal.InsertDate = DB.GetServerDate();
                            _translationPCMToNormal.Detach();
                            DB.Save(_translationPCMToNormal, true);
                        }
                        else
                        {
                            _reqeust.Detach();
                            DB.Save(_reqeust, false);

                            _translationPCMToNormal.Detach();
                            DB.Save(_translationPCMToNormal, false);
                        }

                    }
                    ts2.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage("ذخیره اطلاعات انجام شد.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            return IsSaveSuccess;
        }


        public override bool Forward()
        {
            try
            {
                using (TransactionScope ts1 = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(5)))
                {
                    Save();
                    this.RequestID = _reqeust.ID;
                    if (IsSaveSuccess)
                    {
                        IsForwardSuccess = true;
                    }
                    ts1.Complete();
                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ارجاع درخواست", ex);
            }

            return IsForwardSuccess;
        }

        public override bool Cancel()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    RequestID = _reqeust.ID;

                    List<RequestPayment> paymentList = RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                    List<InstallmentRequestPayment> instalments = new List<InstallmentRequestPayment>();

                    if (paymentList != null)
                        if (paymentList.Count != 0)
                            foreach (RequestPayment currentPayment in paymentList)
                            {
                                if (currentPayment.IsPaid == true)
                                    throw new Exception("پرداخت نقدی صورت گرفته است !");

                                if (currentPayment.PaymentType == (byte)DB.PaymentType.Instalment)
                                {
                                    instalments = InstalmentRequestPaymentDB.GetInstalmentRequestPaymentbyPaymentID(currentPayment.ID);
                                    DB.DeleteAll<InstallmentRequestPayment>(instalments.Select(t => t.ID).ToList());
                                }
                            }

                    _reqeust.IsCancelation = true;



                    if (passOldPostContact != null)
                    {
                        passOldPostContact.Status = (byte)DB.PostContactStatus.CableConnection;
                        passOldPostContact.Detach();
                        DB.Save(passOldPostContact);
                    }
                    if (passNewPostContact != null)
                    {
                        passNewPostContact.Status = (byte)DB.PostContactStatus.Free;
                        passNewPostContact.Detach();
                        DB.Save(passNewPostContact);
                    }

                    if (passCabinetInput != null)
                    {
                        passCabinetInput.Status = (byte)DB.CabinetInputStatus.healthy;
                        passCabinetInput.Detach();
                        DB.Save(passCabinetInput);
                    }

                    Data.CancelationRequestList cancelationRequest = new CancelationRequestList();

                    cancelationRequest.ID = RequestID;
                    cancelationRequest.EntryDate = DB.GetServerDate();
                    cancelationRequest.UserID = Folder.User.Current.ID;

                    cancelationRequest.Detach();
                    DB.Save(cancelationRequest, true);


                    _reqeust.Detach();
                    DB.Save(_reqeust);
                    ts.Complete();
                    IsCancelSuccess = true;

                }
            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در ابطال درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در ابطال درخواست، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ابطال درخواست، " + ex.Message, ex);
                IsCancelSuccess = false;
            }

            return IsCancelSuccess;
        }



    }
}
