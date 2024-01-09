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
    public partial class TranslationCabinetForm : Local.RequestFormBase
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
        int pastNewCabinetID = 0;
        int pastOldCabinetID = 0;
        private int _RequestType;
        private long _ID;
        int _centerID = 0;
        #endregion

        #region cunstractor && Method load

        public TranslationCabinetForm()
        {
            InitializeComponent();
            Initialize();
        }

        public TranslationCabinetForm(long exchangeCabinetInputID)
            : this()
        {
            _ID = exchangeCabinetInputID;
        }

        public TranslationCabinetForm(int requestTypeID)
            : this()
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

            if (_ID == 0)
            {
                _exchangeCabinetInput = new ExchangeCabinetInput();

                AccomplishmentGroupBox.Visibility = Visibility.Collapsed;
            }
            else
            {

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
                FirstPostComboBox.ItemsSource = Data.PostDB.GetAllpostInCabinet((int)NewCabinetComboBox.SelectedValue).Select(t => new CheckableItem { ID = t.ID, Name = t.Number.ToString(), IsChecked = false });
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
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {
                    _reqeust = _exchangeRequestInfo.Request;
                    _exchangeCabinetInput = this.DataContext as ExchangeCabinetInput;

                    if (TransferSamePostsRadioButton.IsChecked == true)
                        _exchangeCabinetInput.IsChangePost = false;
                     else if (WithChangePostRadioButton.IsChecked == true)
                        _exchangeCabinetInput.IsChangePost = true;
                    

                    // Verify data
                    VerifyData(_exchangeCabinetInput);


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

                        _exchangeCabinetInput.Detach();
                        DB.Save(_exchangeCabinetInput, true);
                    }
                    else
                    {
                    
                        _reqeust.Detach();
                        DB.Save(_reqeust, true);

                        _exchangeCabinetInput.Detach();
                        DB.Save(_exchangeCabinetInput, true);
                    }

                    ts2.Complete();
                    IsSaveSuccess = true;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("ذخیره برگردان کافو انجام نشد .", ex);
            }

            return IsSaveSuccess;
        }

        private void ReserveCabinet(ExchangeCabinetInput exchangeCabinetInput)
        {
            using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
            {

                Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(exchangeCabinetInput.OldCabinetID);
                oldCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                oldCabinet.Detach();
                DB.Save(oldCabinet);

                Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(exchangeCabinetInput.NewCabinetID);
                newCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                newCabinet.Detach();
                DB.Save(newCabinet);

                ts3.Complete();
            }
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

                ReserveGroupBox.Visibility = Visibility.Visible;
                BuchtStatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.BuchtStatus));
               ReserveItemsDataGrid.ItemsSource = DB.GetAllInformationByBuchtIDs(_oldBuchtList.Where(t => t.Status != (int)DB.BuchtStatus.Free &&
                                      t.Status != (int)DB.BuchtStatus.AllocatedToInlinePCM &&
                                      t.Status != (int)DB.BuchtStatus.Connection &&
                                      t.Status != (int)DB.BuchtStatus.Destroy &&
                                      t.BuchtTypeID != (int)DB.BuchtType.InLine &&
                                      t.BuchtTypeID != (int)DB.BuchtType.OutLine).Select(t=>t.ID).ToList());
                throw new Exception("از میان بوخت ها قدیم بوخت غیر آزاد یا متصل وجود دارد");
            }

            // number old cabinetInput
            _newCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(exchangeCabinetInput.FromNewCabinetInputID ?? 0, exchangeCabinetInput.ToNewCabinetInputID ?? 0);
            _newBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(_newCabinetInputs.Select(t => t.ID).ToList());

            if (_newCabinetInputs.Count() != _oldCabinetInputs.Count()) { throw new Exception("تعداد مرکزی های انتخاب شده برابر نمی باشد."); }

            if (_oldBuchtList.Where(t => t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).Count() != _newBuchtList.Count()) { throw new Exception("تعداد بوخت های متصل انتخاب شده برابر نمی باشد."); }

            if (_newBuchtList.Any(t => t.Status != (Byte)DB.BuchtStatus.Free)) { throw new Exception("همه بوخت های متصل به ورودی های بعد از برگردان انتخاب شد در وضعیت آزاد قرار ندارند."); }

            _oldPosts = Data.PostDB.GetAllPostsByPostContactList(_oldBuchtList.Where(t => t.ConnectionID != null).Select(t => t.ConnectionID ?? 0).ToList());


            if (TransferSamePostsRadioButton.IsChecked == true && exchangeCabinetInput.OldCabinetID != exchangeCabinetInput.NewCabinetID)
            {
                List<Post> allPostsInNewCabinet = Data.PostDB.GetAllpostInCabinet(_newCabinetInputs.Take(1).SingleOrDefault().CabinetID);
                if (_oldPosts.Any(t => allPostsInNewCabinet.Select(p => p.Number).Contains(t.Number))) { throw new Exception("پست با شماره انتخاب شده در کافو جدید موجود می باشد."); }
            }

            List<PostContact> resultPostContactsOldPost = Data.PostContactDB.GetPostContactByStatus(_oldPosts, new List<byte> { (byte)DB.PostContactStatus.CableConnection, (byte)DB.PostContactStatus.NoCableConnection, (byte)DB.PostContactStatus.Free, (byte)DB.PostContactStatus.PermanentBroken }, false);

            if (resultPostContactsOldPost.Count() > 0)
            {
                string errorPostContactStatus = string.Empty;

                resultPostContactsOldPost.ForEach(item => { errorPostContactStatus = errorPostContactStatus + Helper.GetEnumDescriptionByValue(typeof(DB.PostContactStatus), item.Status) + " "; });

                throw new Exception("وضعیت های " + errorPostContactStatus + " قابل برگردان نیستن");
            }



            if (WithChangePostRadioButton.IsChecked == true)
            {
                List<Bucht> AllBuchtConnectToOldPosts = Data.BuchtDB.GetBuchtByPostIDs(_oldPosts.Select(t => t.ID).ToList());
                if (AllBuchtConnectToOldPosts.Any(t => !_oldBuchtList.Select(t2 => t2.ID).Contains(t.ID))) { throw new Exception("از میان اتصالی  پست های متصل به مرکزی های انتخاب شده اتصالی متصل به خارج از محدوده انتخاب شده وجود دارد"); }

                _newPosts = Data.PostDB.GetTheNumberPostByStartID((int)exchangeCabinetInput.FromNewPostID, _oldPosts.Count());

                if (_newPosts.Count() != _oldPosts.Count) { throw new Exception("تعداد پست های جدید برابر تعداد پست های قبل برگردان نمی باشد."); };

                List<PostContact> resultPostContactsNewPost = Data.PostContactDB.GetPostContactByStatus(_newPosts, new List<byte> { (byte)DB.PostContactStatus.Free }, false);
                if (resultPostContactsNewPost.Count() > 0)
                    throw new Exception("اتصالی های پست های جدید شامل اتصالی غیر ازاد می باشد");
            }
        }

        public override bool Forward()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                Save();

                this.RequestID = _reqeust.ID;

                if (IsSaveSuccess == true)
                {
                    ReserveCabinet(_exchangeCabinetInput);

                    IsForwardSuccess = true;
                }

                ts.Complete();
            }
            return IsForwardSuccess;
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

        #endregion



    }
}

