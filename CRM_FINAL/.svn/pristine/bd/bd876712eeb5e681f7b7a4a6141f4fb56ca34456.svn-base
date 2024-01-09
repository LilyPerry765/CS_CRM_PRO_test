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
using System.Transactions;
using System.Collections;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ExchangeCabinetInputForm.xaml
    /// </summary>
    public partial class ExchangeCabinetInputForm : Local.RequestFormBase
    {

        #region Properties
        List<Bucht> _oldBuchtList { get; set; }
        List<Bucht> _newBuchtList { get; set; }

        List<Bucht> _pastOldBuchtList { get; set; }
        List<Bucht> _pastNewBuchtList { get; set; }

        List<CabinetInput> _newCabinetInputs { get; set; }
        List<CabinetInput> _oldCabinetInputs { get; set; }

        List<Post> _oldPosts { get; set; }
        List<Post> _newPosts { get; set; }

        List<CheckableItem> _pastCabinet { get; set; }
        List<PostContact> _newConnectionID { get; set; }

        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }
        ExchangeCabinetInput _exchangeCabinetInput { get; set; }
        Request _reqeust { get; set; }
        int pastNewCabinetID = 0 ;
        int pastOldCabinetID = 0 ;
        private int _RequestType;
        private long _ID;
        int _centerID = 0;
        #endregion

        #region cunstractor && Method load

        public ExchangeCabinetInputForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ExchangeCabinetInputForm(long exchangeCabinetInputID):this()
        {
            _ID = exchangeCabinetInputID;
        }

        public ExchangeCabinetInputForm(int requestTypeID): this()
        {
            this._RequestType = requestTypeID;
        }

        public void Initialize()
        {

            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Cancelation };
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_ID);
            _exchangeRequestInfo.RequestType = this._RequestType;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;

            StatusComboBox.ItemsSource = DB.GetStepStatus((int)DB.RequestType.ExchangeCabinetInput, this.currentStep);
            StatusComboBox.SelectedValue = this.currentStat;
            AccomplishmentGroupBox.Visibility = Visibility.Collapsed;
            if (_ID == 0)
            {
                _exchangeCabinetInput = new ExchangeCabinetInput();


            }
            else
            {
                _reqeust = RequestDB.GetRequestByID(_ID);
                _exchangeCabinetInput = Data.ExchangeCenralCableCabinetDB.GetExchangeCabinetInput(_ID);
                _pastCabinet = new List<CheckableItem>() { 
                                                            Data.CabinetDB.GetCheckableItemCabinetByID(_exchangeCabinetInput.NewCabinetID),
                                                            Data.CabinetDB.GetCheckableItemCabinetByID(_exchangeCabinetInput.OldCabinetID)
                                                         };

                if (Data.StatusDB.IsFinalStep(this.currentStat))
                {
                    AccomplishmentDateLabel.Visibility = Visibility.Visible;
                    AccomplishmentDate.Visibility = Visibility.Visible;

                    AccomplishmentTimeLabel.Visibility = Visibility.Visible;
                    AccomplishmentTime.Visibility = Visibility.Visible;

                    if (_exchangeCabinetInput.MDFAccomplishmentTime == null)
                    {
                        DateTime currentDateTime = DB.GetServerDate();
                        _exchangeCabinetInput.MDFAccomplishmentTime = currentDateTime.ToShortTimeString();
                        _exchangeCabinetInput.MDFAccomplishmentDate = currentDateTime;
                    }

                }

                if (_exchangeCabinetInput.IsChangePost == true)
                {
                    WithChangePostRadioButton.IsChecked = true;
                }
                else
                {
                    TransferSamePostsRadioButton.IsChecked = true;
                }
                pastNewCabinetID = _exchangeCabinetInput.NewCabinetID;
                pastOldCabinetID = _exchangeCabinetInput.OldCabinetID;
                OldCabinetComboBox.SelectedValue = _exchangeCabinetInput.OldCabinetID;
                OldCabinetComboBox_SelectionChanged(null, null);

                NewCabinetComboBox.SelectedValue = _exchangeCabinetInput.NewCabinetID;
                NewCabinetComboBox_SelectionChanged(null, null);

            }
            this.DataContext = _exchangeCabinetInput;
        }

        #endregion

        #region Event

        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            _centerID = centerID;

            if (_exchangeCabinetInput.ID == 0)
            {
                OldCabinetComboBox.ItemsSource = NewCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(centerID);
            }
            else
            {

                int requestCenterID = Data.RequestDB.GetCenterIDByRequestID(_exchangeCabinetInput.ID);

                if (requestCenterID == _centerID)
                {
                    OldCabinetComboBox.ItemsSource = NewCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(centerID).Union(_pastCabinet);
                }
                else
                {
                    OldCabinetComboBox.ItemsSource = NewCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(centerID);
                }
            }
        }

        private void OldCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OldCabinetComboBox.SelectedValue != null)
            {
                ToOldCabinetInputComboBox.ItemsSource = FromOldCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)OldCabinetComboBox.SelectedValue);
            }
        }

        private void NewCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewCabinetComboBox.SelectedValue != null)
            {
                ToNewCabinetInputComboBox.ItemsSource = FromNewCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)NewCabinetComboBox.SelectedValue);
                FirstPostComboBox.ItemsSource = Data.PostDB.GetAllpostInCabinet((int)NewCabinetComboBox.SelectedValue).Select(t=> new CheckableItem{ ID = t.ID , Name = t.Number.ToString() , IsChecked = false});
            }
        }
        private void TransferSamePostsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (TransferSamePostsRadioButton.IsChecked == true)
            {
                FromNewPostGroupBox.Visibility = Visibility.Collapsed;
                OldCabinetGroupBox.Visibility = Visibility.Visible;
                NewCabinetGroupBox.Visibility = Visibility.Visible;
                this.ResizeWindow();
            }
        }
        private void WithChangePostRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (WithChangePostRadioButton.IsChecked == true)
            {
                FromNewPostGroupBox.Visibility = Visibility.Visible;
                OldCabinetGroupBox.Visibility = Visibility.Visible;
                NewCabinetGroupBox.Visibility = Visibility.Visible;
                this.ResizeWindow();
            }
        }

        private void wiringButtom_Click(object sender, RoutedEventArgs e)
        {
            Folder.MessageBox.ShowInfo("این گزارش در دست تهیه می باشد");
        }

        private void ToNewCabinetInputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ToOldCabinetInputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        #endregion

        #region method
        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {


                           _reqeust = _exchangeRequestInfo.Request;
                           _exchangeCabinetInput = this.DataContext as ExchangeCabinetInput;

                           // Verify data
                           VerifyData(_exchangeCabinetInput);

                            Status Status = Data.StatusDB.GetStatueByStatusID(_reqeust.StatusID);

                           // Save reqeust
                            if (_ID == 0)
                            {
                                _reqeust.ID = DB.GenerateRequestID();
                                _reqeust.RequestPaymentTypeID = 0;
                                _reqeust.IsViewed = false;
                                _reqeust.InsertDate = DB.GetServerDate();
                                _reqeust.StatusID = DB.GetStatus(_RequestType, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                                _reqeust.Detach();
                                DB.Save(_reqeust, true);

                                _exchangeCabinetInput.ID = _reqeust.ID;
                                _exchangeCabinetInput.InsertDate = DB.GetServerDate();
                                _exchangeCabinetInput.CompletionDate = DB.ServerDate();

                                _exchangeCabinetInput.Detach();
                                DB.Save(_exchangeCabinetInput,true);
                            }
                            else if (Status.StatusType != (byte)DB.RequestStatusType.Start)
                            {
                                if (StatusComboBox.SelectedValue == null) throw new Exception("وضعیت انتخاب نشده است");
                                _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                                if (Data.StatusDB.GetStatueByStatusID((int)StatusComboBox.SelectedValue).StatusType == (byte)DB.RequestStatusType.End)
                                {
                                    throw new Exception("امکان ذخیره در خواست دراین مرحله نیست برای اعمال تغییرات از وضعیت اعمال تغییرات استفاده کنید.");
                                }
                                _reqeust.Detach();
                                DB.Save(_reqeust, false);
                            }
                            if (_exchangeCabinetInput.CompletionDate != null)
                            {
                                throw new Exception("در درخواست قبلا اعمال تغییرات صورت گرفته است");
                            }
                   
                            if(TransferSamePostsRadioButton.IsChecked == true)
                            {
                                _exchangeCabinetInput.IsChangePost = false;
                            }
                            else if (WithChangePostRadioButton.IsChecked == true)
	                        {
                                _exchangeCabinetInput.IsChangePost = true;
	                        }
                            

                        Status = Data.StatusDB.GetStatueByStatusID((int)StatusComboBox.SelectedValue);
                        // اگر در وضعیت تغییرات نیست
                        if (_ID == 0 || Status.StatusType != (byte)DB.RequestStatusType.Changes)
                        {
                            ReserveCabinet(_exchangeCabinetInput.NewCabinetID, _exchangeCabinetInput.OldCabinetID, pastNewCabinetID, pastOldCabinetID);
                        }
                        else if (Status.StatusType == (byte)DB.RequestStatusType.Changes)
                        {

                            // ابتدا مرکزی های انتخاب شده از کافو قبل برگردان به کافو بعد برگردان منتقل می شود
                            // سپس اگر حالت انتقال پست انتخاب شده باشد پست ها با تغییر کافو به کافو جدید انتقال میابد
                            // اگر حالت تبدیل پست انتخاب شود هر اتصالی بسته به پی سی ام یا عادی یودن به پست جدید انتقال میابد
                            // 

                            _exchangeCabinetInput.CompletionDate = DB.ServerDate();

                            // exit cabinet from reserve
                            ExitReserveCabinet(_exchangeCabinetInput.NewCabinetID, _exchangeCabinetInput.OldCabinetID);

                                for (int i = 0; i < _oldBuchtList.Count(); i++)
                                {
                                    // transform bucht else PCM
                                    if (_oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtList[i].Status == (int)DB.BuchtStatus.Connection && _oldBuchtList[i].BuchtIDConnectedOtherBucht == null)
                                    {
                                        _newBuchtList[i].Status = _oldBuchtList[i].Status;
                                        _oldBuchtList[i].Status = (int)DB.BuchtStatus.Free;

                                        _newBuchtList[i].ConnectionID = _oldBuchtList[i].ConnectionID;
                                        _oldBuchtList[i].ConnectionID = null;

                                        _newBuchtList[i].SwitchPortID = _oldBuchtList[i].SwitchPortID;
                                        _oldBuchtList[i].SwitchPortID = null;

                                        //_newBuchtList[i].ADSLPortID = _oldBuchtList[i].ADSLPortID;
                                        //_oldBuchtList[i].ADSLPortID = null;


                                        _newBuchtList[i].ADSLStatus = _oldBuchtList[i].ADSLStatus;
                                        _oldBuchtList[i].ADSLStatus = false;

                                        //_newBuchtList[i].ADSLType = _oldBuchtList[i].ADSLType;
                                        //_oldBuchtList[i].ADSLType = null;

                                    }
                                     // transform PCM bucht
                                    else if (_oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtList[i].Status == (int)DB.BuchtStatus.AllocatedToInlinePCM)
                                    {
         
                                        _newBuchtList[i].Status = _oldBuchtList[i].Status;
                                        _oldBuchtList[i].Status = (int)DB.BuchtStatus.Free;
                                     
                                        _newBuchtList[i].BuchtIDConnectedOtherBucht = _oldBuchtList[i].BuchtIDConnectedOtherBucht;
                                        _newBuchtList[i].ConnectionID = _oldBuchtList[i].ConnectionID;

                                        
                                        _oldBuchtList.Where(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).SingleOrDefault().BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                        _oldBuchtList[i].BuchtIDConnectedOtherBucht = null;
                                       
                                        _oldBuchtList[i].ConnectionID = null;


                                        _oldBuchtList.Where(t => t.CabinetInputID == _oldBuchtList[i].CabinetInputID && (t.BuchtTypeID == (int)DB.BuchtType.InLine || t.BuchtTypeID == (int)DB.BuchtType.OutLine))
                                                     .ToList()
                                                     .ForEach(item => item.CabinetInputID = _newBuchtList[i].CabinetInputID);

                                    }
                                    // if bucht is PrivateWire change
                                    else if (_oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtList[i].Status == (int)DB.BuchtStatus.Connection && _oldBuchtList[i].BuchtIDConnectedOtherBucht != null && Data.ExchangeCabinetInputDB.IsSpecialWire(_oldBuchtList[i].ID))
                                    {
                                        _newBuchtList[i].Status = _oldBuchtList[i].Status;
                                        _oldBuchtList[i].Status = (int)DB.BuchtStatus.Free;

                                        _newBuchtList[i].ConnectionID = _oldBuchtList[i].ConnectionID;
                                        _oldBuchtList[i].ConnectionID = null;

                                        _newBuchtList[i].SwitchPortID = _oldBuchtList[i].SwitchPortID;
                                        _oldBuchtList[i].SwitchPortID = null;

                                        //_newBuchtList[i].ADSLPortID = _oldBuchtList[i].ADSLPortID;
                                        //_oldBuchtList[i].ADSLPortID = null;


                                        _newBuchtList[i].ADSLStatus = _oldBuchtList[i].ADSLStatus;
                                        _oldBuchtList[i].ADSLStatus = false;

                                        //_newBuchtList[i].ADSLType = _oldBuchtList[i].ADSLType;
                                        //_oldBuchtList[i].ADSLType = null;

                                        int newBuchtType = _newBuchtList[i].BuchtTypeID;
                                        _newBuchtList[i].BuchtTypeID = _oldBuchtList[i].BuchtTypeID;
                                        _oldBuchtList[i].BuchtTypeID = newBuchtType;


                                        Bucht buchtPrivateWire = Data.BuchtDB.GetBuchetByID(_oldBuchtList[i].BuchtIDConnectedOtherBucht);
                                        buchtPrivateWire.BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                        if (_oldBuchtList.Any(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht)) _oldBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                        if (_newBuchtList.Any(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht)) _newBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                        buchtPrivateWire.Detach();
                                        DB.Save(buchtPrivateWire, false);

                                        _newBuchtList[i].BuchtIDConnectedOtherBucht = buchtPrivateWire.ID;
                                        _oldBuchtList[i].BuchtIDConnectedOtherBucht = null;

                                    }
                                    else if ((_oldBuchtList[i].Status == (int)DB.BuchtStatus.Connection && !( _oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.InLine || _oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.OutLine)))
                                    {
                                        throw new Exception("در میان بوخت ها نوع بوخت نا مشخصی وجود دارد لطفا با مدیر سیستم تماس بگیرید");
                                    }
                                 
                                }

                              
                                _oldBuchtList.ForEach(item => item.Detach());
                                DB.UpdateAll(_oldBuchtList);
                              
                                _newBuchtList.ForEach(item => item.Detach());
                                DB.UpdateAll(_newBuchtList);
                               

                            if (TransferSamePostsRadioButton.IsChecked == true)
                            {
                                // transform old post to new cabinet
                                _oldPosts.ForEach(item => { item.CabinetID = _exchangeCabinetInput.NewCabinetID; item.Detach(); });
                                DB.UpdateAll(_oldPosts);
                            }
                            else if (WithChangePostRadioButton.IsChecked == true)
                            {
                               
                                for (int j = 0; j < _oldPosts.Count(); j++)
                                {
                                    
                                    List<PostContact> allOldPostContactList = Data.PostContactDB.GetPostContactByPostID(_oldPosts[j].ID);
                                    List<PostContact> oldPostContactList = allOldPostContactList.Where(t => t.ConnectionType != (int)DB.PostContactConnectionType.PCMNormal).ToList();
                                   
                                    List<PostContact> newPostContactList = Data.PostContactDB.GetPostContactByPostID(_newPosts[j].ID);
                                    for (int i = 0; i < oldPostContactList.Count(); i++)
                                    {
                                        // transform post contact to new post of new cabinet else PCM
                                        if (oldPostContactList[i].ConnectionType != (int)DB.PostContactConnectionType.PCMRemote && oldPostContactList[i].Status == (int)DB.PostStatus.Dayer)
                                        {
                                            // اتصالی های غیره پی سی ام با تغییر پست انها به پست جدید منتقل می شوند
                                            int newPostID = newPostContactList[i].PostID;
                                            int connectionNo = newPostContactList[i].ConnectionNo;

                                            newPostContactList[i].PostID = oldPostContactList[i].PostID;
                                            newPostContactList[i].ConnectionNo = oldPostContactList[i].ConnectionNo;
                                            newPostContactList[i].Status = (int)DB.PostContactStatus.Free;

                                            oldPostContactList[i].PostID = newPostID;
                                            oldPostContactList[i].ConnectionNo = connectionNo;
                                            oldPostContactList[i].Status = (int)DB.PostContactStatus.CableConnection;


                                            newPostContactList[i].Detach();
                                            DB.Save(newPostContactList[i]);


                                            oldPostContactList[i].Detach();
                                            DB.Save(oldPostContactList[i]);


                                        }
                                        else if (oldPostContactList[i].ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                                        {
                                           // transform pcm post contact to new post of new cabinet
                                            newPostContactList[i].Status = (byte)DB.PostContactStatus.NoCableConnection;
                                            newPostContactList[i].ConnectionType = (byte)DB.PostContactConnectionType.PCMRemote;

                                            oldPostContactList[i].Status = (byte)DB.PostContactStatus.Free;
                                            oldPostContactList[i].ConnectionType = (byte)DB.PostContactConnectionType.Noraml;

                                            List<PostContact> pastOldPostContactPCMOutputList = allOldPostContactList.Where(t => t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal && t.ConnectionNo == oldPostContactList[i].ConnectionNo).ToList();
                                            foreach (PostContact PostContactPCM in pastOldPostContactPCMOutputList)
                                            {
                                                PostContactPCM.PostID = newPostContactList[i].PostID;
                                                PostContactPCM.ConnectionNo = newPostContactList[i].ConnectionNo;
                                                PostContactPCM.Detach();
                                            }

                                            List<Bucht> buchtsConnectToPCM = _oldBuchtList.Where(t => t.ConnectionID == oldPostContactList[i].ID).ToList().Union(_newBuchtList.Where(t => t.ConnectionID == oldPostContactList[i].ID).ToList()).ToList();
                                            buchtsConnectToPCM.ForEach(item => { item.ConnectionID = newPostContactList[i].ID; item.Detach(); });
                                            DB.UpdateAll(buchtsConnectToPCM);


                                            oldPostContactList[i].Detach();
                                            DB.Save(oldPostContactList[i]);

                                            newPostContactList[i].Detach();
                                            DB.Save(newPostContactList[i]);

                                            pastOldPostContactPCMOutputList.ForEach(item => item.Detach());
                                            DB.UpdateAll(pastOldPostContactPCMOutputList);
                                        }

                                    }
                                  
                                }
                            }
                        }

                        _exchangeCabinetInput.Detach();
                        DB.Save(_exchangeCabinetInput);
                    
                        ts.Complete();
                    }

                IsSaveSuccess = true;
                   
                }
            catch(Exception ex)
            {
                ShowErrorMessage("برگردان ورودی کافو انجام نشد",ex);
            }

            return IsSaveSuccess;
        }

        public override bool Cancel()
        {
            using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
            {


                Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(_exchangeCabinetInput.OldCabinetID);
                oldCabinet.Status = (int)DB.CabinetStatus.Install;
                oldCabinet.Detach();
                DB.Save(oldCabinet);

                Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(_exchangeCabinetInput.NewCabinetID);
                newCabinet.Status = (int)DB.CabinetStatus.Install;
                newCabinet.Detach();
                DB.Save(newCabinet);



                Data.CancelationRequestList cancelationRequest = new CancelationRequestList();
                cancelationRequest.ID = _reqeust.ID;
                cancelationRequest.EntryDate = DB.GetServerDate();
                cancelationRequest.UserID = Folder.User.Current.ID;
                cancelationRequest.Detach();
                DB.Save(cancelationRequest, true);

                _reqeust.IsCancelation = true;
                _reqeust.Detach();
                DB.Save(_reqeust);

                IsCancelSuccess = true;

                ts3.Complete();
            }

            return true;
        }

        private void ExitReserveCabinet(int newCabinetID, int oldCabinetID)
        {
            Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(oldCabinetID);
            oldCabinet.Status = (int)DB.CabinetStatus.Install;
            oldCabinet.Detach();

            Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(newCabinetID);
            newCabinet.Status = (int)DB.CabinetStatus.Install;
            newCabinet.Detach();

            DB.UpdateAll(new List<Cabinet> { oldCabinet, newCabinet });
        }
        private void ReserveCabinet(int newCabinetID, int oldCabinetID, int pastNewCabinetID, int pastOldCabinetID)
        {

            Cabinet pastOldCabinet = Data.CabinetDB.GetCabinetByID(pastNewCabinetID);
            if (pastOldCabinet != null)
            {
                pastOldCabinet.Status = (int)DB.CabinetStatus.Install;
                pastOldCabinet.Detach();
                DB.Save(pastOldCabinet);
            }

            Cabinet pastNewCabinet = Data.CabinetDB.GetCabinetByID(pastOldCabinetID);
            if (pastNewCabinet != null)
            {
                pastNewCabinet.Status = (int)DB.CabinetStatus.Install;
                pastNewCabinet.Detach();
                DB.Save(pastNewCabinet);
            }
            

            Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(oldCabinetID);
            oldCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
            oldCabinet.Detach();

            Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(newCabinetID);
            newCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
            newCabinet.Detach();

            DB.UpdateAll(new List<Cabinet> { oldCabinet, newCabinet });
        }

        private void VerifyData(ExchangeCabinetInput exchangeCabinetInput)
        {
            Cabinet OldCabinet = Data.CabinetDB.GetCabinetByID(_exchangeCabinetInput.OldCabinetID);
            Cabinet NewCabinet = Data.CabinetDB.GetCabinetByID(_exchangeCabinetInput.NewCabinetID);

            if ((OldCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet && NewCabinet.CabinetUsageType != (byte)DB.CabinetUsageType.OpticalCabinet)
                 || (NewCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet && OldCabinet.CabinetUsageType != (byte)DB.CabinetUsageType.OpticalCabinet))
                throw new Exception("امکان برگردان از کافو نوری به کافو غیر از نوری نیست");



              // number new cabinetInput
             _oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(exchangeCabinetInput.FromOldCabinetInputID ?? 0, exchangeCabinetInput.ToOldCabinetInputID ?? 0);
             _oldBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(_oldCabinetInputs.Select(t => t.ID).ToList());


             if (_oldBuchtList.Any(t => t.Status != (int)DB.BuchtStatus.Free &&
                                       t.Status != (int)DB.BuchtStatus.AllocatedToInlinePCM &&
                                       t.Status != (int)DB.BuchtStatus.Connection &&
                                       t.Status != (int)DB.BuchtStatus.Destroy &&
                                       t.BuchtTypeID != (int)DB.BuchtType.InLine && 
                                       t.BuchtTypeID != (int)DB.BuchtType.OutLine)) 
                                { 
                                    throw new Exception("از میان بوخت ها بوخت غیر آزاد یا متصل وجود دارد");
                                }

            // number old cabinetInput
             _newCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(exchangeCabinetInput.FromNewCabinetInputID ?? 0, exchangeCabinetInput.ToNewCabinetInputID ?? 0);
             _newBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(_newCabinetInputs.Select(t => t.ID).ToList());

            if (_newCabinetInputs.Count() != _oldCabinetInputs.Count()) { throw new Exception("تعداد مرکزی های انتخاب شده برابر نمی باشد."); }

            if (_oldBuchtList.Where(t=>t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).Count() != _newBuchtList.Count()) { throw new Exception("تعداد بوخت های متصل انتخاب شده برابر نمی باشد."); }
            
            if(_newBuchtList.Any(t=>t.Status != (Byte)DB.BuchtStatus.Free)){ throw new Exception("همه بوخت های متصل به ورودی های بعد از برگردان انتخاب شد در وضعیت آزاد قرار ندارند.");}

            _oldPosts = Data.PostDB.GetAllPostsByPostContactList(_oldBuchtList.Where(t=>t.ConnectionID != null).Select(t=>t.ConnectionID ?? 0).ToList());

            List<Bucht> AllBuchtConnectToOldPosts = Data.BuchtDB.GetBuchtByPostIDs(_oldPosts.Select(t => t.ID).ToList());
            if (AllBuchtConnectToOldPosts.Any(t => !_oldBuchtList.Select(t2 => t2.ID).Contains(t.ID))) { throw new Exception("از میان اتصالی  پست های متصل به مرکزی های انتخاب شده اتصالی متصل به خارج از محدوده انتخاب شده وجود دارد"); }

            if (TransferSamePostsRadioButton.IsChecked == true)
            {
                List<Post> allPostsInNewCabinet = Data.PostDB.GetAllpostInCabinet(_newCabinetInputs.Take(1).SingleOrDefault().CabinetID);
                if (_oldPosts.Any(t => allPostsInNewCabinet.Select(p => p.Number).Contains(t.Number))) { throw new Exception("پست با شماره انتخاب شده در کافو جدید موجود می باشد."); }
            }
            List<PostContact> resultPostContactsOldPost = Data.PostContactDB.GetPostContactByStatus(_oldPosts, new List<byte> { (byte)DB.PostContactStatus.CableConnection, (byte)DB.PostContactStatus.NoCableConnection, (byte)DB.PostContactStatus.Free, (byte)DB.PostContactStatus.PermanentBroken }, false);

            if (resultPostContactsOldPost.Count() > 0)
            {
                string errorPostContactStatus = string.Empty;

                resultPostContactsOldPost.ForEach(item => { errorPostContactStatus = errorPostContactStatus + Helper.GetEnumDescriptionByValue(typeof(DB.PostContactStatus) , item.Status) + " "; });

                throw new Exception("وضعیت های " + errorPostContactStatus + " قابل برگردان نیستن");
            }

           

            if (WithChangePostRadioButton.IsChecked == true)
            {
                _newPosts = Data.PostDB.GetTheNumberPostByStartID((int)exchangeCabinetInput.FromNewPostID, _oldPosts.Count());

                if (_newPosts.Count() != _oldPosts.Count) { throw new Exception("تعداد پست های جدید برابر تعداد پست های قبل برگردان نمی باشد."); };

                List<PostContact> resultPostContactsNewPost = Data.PostContactDB.GetPostContactByStatus(_newPosts, new List<byte> { (byte)DB.PostContactStatus.Free }, false);
                if(resultPostContactsNewPost.Count() > 0)
                throw new Exception("اتصالی های پست های جدید شامل اتصالی غیر ازاد می باشد");
            }
        }
        public override bool Forward()
        {
            Save();
            this.RequestID = _reqeust.ID;
            if (IsSaveSuccess == true)
                IsForwardSuccess = true;
            return IsForwardSuccess;
        }
        #endregion


       
    }
}
