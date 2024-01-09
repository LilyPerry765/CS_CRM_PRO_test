using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using CRM.Data;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ExchangePostForm.xaml
    /// </summary>
    public partial class ExchangePostForm : Local.RequestFormBase
    {

        #region Properties && Fielde

        List<Bucht> oldBuchtList;
        List<Bucht> newBuchtList;
        private long _ID;

        private int _RequestType;
        int newPostGroupID = 0;
        int? oldPostGroupID = 0;
        List<PostContact> newPostContactList;
        List<PostContact> pastNewPostContactList;
        PostContact partialPastNewPostContact;
        List<PostContact> oldPostContactList;
        List<PostContact> pastOldPostContactList;
        PostContact partialPastOldPostContact;
        CabinetInput PartialPassNewCabinetInput;
        List<CheckableItem> cabinetInputList;
        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }
        Post pastOldPost;
        Post pastNewPost;

        ExchangePost _exchangePost { get; set; }
        Request _reqeust { get; set; }

        int _centerID = 0;
        private int CityID = 0;
        #endregion

        #region constractor
        public ExchangePostForm()
        {
            InitializeComponent();
            Initialize();

        }
        public ExchangePostForm(long exchangeID)
            : this()
        {
            _ID = exchangeID;

        }
        public ExchangePostForm(int requestType)
            : this()
        {
            this._RequestType = requestType;
        }
        #endregion

        #region event

        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            _centerID = centerID;

            AfterCabinetNumberComboBox.ItemsSource = BeforCabinetNumberComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(_centerID);
        }

        private void WithoutChangeCabinetInputRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (WithoutChangeCabinetInputRadioButton.IsChecked == true)
            {
                ExchangeFromGroupBox.Visibility = Visibility.Visible;
                ExchangeToGroupBox.Visibility = Visibility.Visible;
                ExchangeDetailGroupBox.Visibility = Visibility.Visible;
                AfterCabinetNumberComboBox.IsEnabled = false;
                ToCabinetInputID.Visibility = Visibility.Collapsed;
                ToCabinetInputLabel.Visibility = Visibility.Collapsed;
                FirstInputAfterTransferLabel.Visibility = Visibility.Collapsed;
                FirstInputAfterTransferComboBox.Visibility = Visibility.Collapsed;
                this.ResizeWindow();
            }
        }

        private void WithChangeCabinetInputRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (WithChangeCabinetInputRadioButton.IsChecked == true)
            {
                ExchangeFromGroupBox.Visibility = Visibility.Visible;
                ExchangeToGroupBox.Visibility = Visibility.Visible;
                ExchangeDetailGroupBox.Visibility = Visibility.Visible;
                AfterCabinetNumberComboBox.IsEnabled = true;
                ToCabinetInputID.Visibility = Visibility.Visible;
                ToCabinetInputLabel.Visibility = Visibility.Visible;
                FirstInputAfterTransferLabel.Visibility = Visibility.Visible;
                FirstInputAfterTransferComboBox.Visibility = Visibility.Visible;
                this.ResizeWindow();
            }

        }

        #endregion

        #region Methods
        private void Initialize()
        {
            newPostContactList = new List<PostContact>();
            pastNewPostContactList = new List<PostContact>();
            oldPostContactList = new List<PostContact>();
            cabinetInputList = new List<CheckableItem>();
            pastOldPostContactList = new List<PostContact>();
            oldBuchtList = new List<Bucht>();
            newBuchtList = new List<Bucht>();

            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
        }



        private void Load()
        {

            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_ID);
            _exchangeRequestInfo.RequestType = this._RequestType;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;

            StatusComboBox.ItemsSource = DB.GetStepStatus((int)DB.RequestType.ExchangePost, this.currentStep);
            StatusComboBox.SelectedValue = this.currentStat;



            if (_ID == 0)
            {

                _exchangePost = new ExchangePost();
                AccomplishmentGroupBox.Visibility = Visibility.Collapsed;
            }
            else
            {

                _exchangePost = Data.ExchangePostDB.GetExchangePostByID(_ID);
                _reqeust = Data.RequestDB.GetRequestByID(_ID);


                if (Data.StatusDB.IsFinalStep(this.currentStat))
                {
                    AccomplishmentDateLabel.Visibility = Visibility.Visible;
                    AccomplishmentDate.Visibility = Visibility.Visible;

                    AccomplishmentTimeLabel.Visibility = Visibility.Visible;
                    AccomplishmentTime.Visibility = Visibility.Visible;

                    if (_exchangePost.AccomplishmentTime == null)
                    {
                        DateTime currentDateTime = DB.GetServerDate();
                        _exchangePost.AccomplishmentTime = currentDateTime.ToShortTimeString();
                        _exchangePost.AccomplishmentDate = currentDateTime;
                    }

                }

                Status Status = Data.StatusDB.GetStatueByStatusID(_reqeust.StatusID);
                if (Status.StatusType == (byte)DB.RequestStatusType.Start)
                {
                    StatusComboBox.Visibility = Visibility.Collapsed;
                    wiringButtom.Visibility = Visibility.Collapsed;
                    StatusComboBoxLabel.Visibility = Visibility.Collapsed;
                }



                pastOldPost = Data.PostDB.GetPostByID(_exchangePost.OldPostID);
                pastNewPost = Data.PostDB.GetPostByID(_exchangePost.NewPostID);

                if (_exchangePost.OverallTransfer == true)
                {
                    AllTransferButton.IsSelected = true;
                }
                else
                {
                    PartialTransferButtom.IsSelected = true;
                    partialPastOldPostContact = Data.PostContactDB.GetPostContactByID((long)_exchangePost.FromOldConnectionID);
                    partialPastNewPostContact = Data.PostContactDB.GetPostContactByID((long)_exchangePost.ToOldConnectionID);
                    PartialPassNewCabinetInput = Data.CabinetInputDB.GetCabinetInputByID(_exchangePost.ToCabinetInputID ?? 0);

                }
                if (_exchangePost.WithChangeCabinetInput == true)
                {
                    WithChangeCabinetInputRadioButton.IsChecked = true;
                    WithoutChangeCabinetInputRadioButton.Visibility = Visibility.Collapsed;
                    WithChangeCabinetInputRadioButton_Checked(null, null);
                }
                else
                {
                    WithoutChangeCabinetInputRadioButton.IsChecked = true;
                    WithChangeCabinetInputRadioButton.Visibility = Visibility.Collapsed;
                    WithoutChangeCabinetInputRadioButton_Checked(null, null);
                }


                BeforCabinetNumberComboBox.SelectedValue = _exchangePost.OldCabinetID;
                AfterCabinetNumberComboBox.SelectedValue = _exchangePost.NewCabinetID;


                BeforCabinetNumberComboBox_SelectionChanged(null, null);
                AfterCabinetNumberComboBox_SelectionChanged(null, null);


                BeforPostComboBox.SelectedValue = _exchangePost.OldPostID;
                pastOldPostContactList = Data.PostContactDB.GetPostContactByPostID(_exchangePost.OldPostID).ToList();


                AfterPostComboBox.SelectedValue = _exchangePost.NewPostID;
                pastNewPostContactList = Data.PostContactDB.GetPostContactByPostID(_exchangePost.NewPostID).Where(t => (t.Status == (byte)DB.PostContactStatus.Free) && !(t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).OrderBy(t => t.ConnectionNo).ToList();

                BeforPostComboBox_SelectionChanged(null, null);
                AfterPostComboBox_SelectionChanged(null, null);


            }
            this.DataContext = _exchangePost;
        }
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {

        }

        private void NewItem(object sender, RoutedEventArgs e)
        {


        }

        private void PartialTransfer_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void AllTransfer_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void BeforCabinetNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (BeforCabinetNumberComboBox.SelectedValue != null)
                {
                    BeforPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)BeforCabinetNumberComboBox.SelectedValue);

                    BeforGroupPostNumberTextBox.Text = oldPostGroupID == 0 ? string.Empty : oldPostGroupID.ToString();
                    FromCabinetInputID.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)BeforCabinetNumberComboBox.SelectedValue);
                    if (WithoutChangeCabinetInputRadioButton.IsChecked == true)
                    {
                        AfterCabinetNumberComboBox.SelectedValue = (int)BeforCabinetNumberComboBox.SelectedValue;
                        AfterCabinetNumberComboBox_SelectionChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در دریافت اطلاعات", ex);
            }
        }

        private void AfterCabinetNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AfterCabinetNumberComboBox.SelectedValue != null)
            {
                AfterPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)AfterCabinetNumberComboBox.SelectedValue);
                ToCabinetInputID.ItemsSource = FirstInputAfterTransferComboBox.ItemsSource = cabinetInputList = Data.CabinetDB.GetFreeCabinetInputByCabinetID((int)AfterCabinetNumberComboBox.SelectedValue);
            }

        }

        private void BeforPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BeforPostComboBox.SelectedValue != null)
            {

                // اتصالی هایی که متصل هستند و پی سی ام هستند را استخراج می کند

                oldPostContactList = Data.PostContactDB.GetPostContactByPostID((int)BeforPostComboBox.SelectedValue).Where(t => t.Status == (byte)DB.PostContactStatus.CableConnection || t.Status == (byte)DB.PostContactStatus.FullBooking || (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).ToList();

                // بوخت های اتصالی های پست را استخراج میکند
                oldBuchtList = Data.BuchtDB.GetBuchtByListConnectionID(oldPostContactList.Select(t => t.ID).ToList());

                //FromOldConnectionNo.ItemsSource = Data.PostContactDB.GetPostContactCheckableByPostID((int)BeforPostComboBox.SelectedValue).Where(t => oldBuchtList.Select(p => p.ConnectionID).Contains(t.LongID));
                List<PostContact> partialPastOldPostContactList = new List<PostContact>();
                if (partialPastOldPostContact != null)
                    partialPastOldPostContactList.Add(partialPastOldPostContact);
                if (partialPastOldPostContactList.Count > 0)
                    FromOldConnectionNo.ItemsSource = Data.PostContactDB.GetPostContactByPostID((int)BeforPostComboBox.SelectedValue).Where(t => (t.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal && t.Status == (byte)DB.PostContactStatus.CableConnection) || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote).Union(partialPastOldPostContactList).ToList().Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false, Description = (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote) ? "پی سی ام" : " " }).ToList();
                else
                    FromOldConnectionNo.ItemsSource = Data.PostContactDB.GetPostContactByPostID((int)BeforPostComboBox.SelectedValue).Where(t => (t.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal && t.Status == (byte)DB.PostContactStatus.CableConnection) || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote).Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false, Description = (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote) ? "پی سی ام" : " " }).ToList();


                ConnectionDataGrid.ItemsSource = DB.GetAllInformationByPostIDAndWithOutpostContactType((int)BeforPostComboBox.SelectedValue, (byte)DB.PostContactConnectionType.PCMRemote);
                ConnectionDataGrid.Visibility = Visibility.Visible;

                //HighNumberOfConnectionsTextBox.Text = oldPostContactList.Where(t => t.Status == (byte)DB.PostContactStatus.CableConnection).Count().ToString();
            }

        }

        private void AfterPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AfterPostComboBox.SelectedValue != null)
            {
                if (_exchangePost.ToOldConnectionID != null)
                {
                    List<CheckableItem> ToOldConnectionCheckableItem = new List<CheckableItem> { Data.PostContactDB.GetPostContactCheckableItemByID((long)_exchangePost.ToOldConnectionID) };
                    newPostContactList = Data.PostContactDB.GetPostContactByPostID((int)AfterPostComboBox.SelectedValue).OrderBy(t => t.ConnectionNo).ToList();
                    ToOldConnectionNo.ItemsSource = newPostContactList.Where(t => t.Status == (byte)DB.PostContactStatus.Free && !(t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false }).Union(ToOldConnectionCheckableItem).ToList();


                }
                else
                {
                    newPostContactList = Data.PostContactDB.GetPostContactByPostID((int)AfterPostComboBox.SelectedValue).OrderBy(t => t.ConnectionNo).ToList();
                    ToOldConnectionNo.ItemsSource = newPostContactList.Where(t => t.Status == (byte)DB.PostContactStatus.Free && !(t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).Select(t => new CheckableItem { LongID = t.ID, Name = t.ConnectionNo.ToString(), IsChecked = false }).ToList();

                }

                NewConnectionDataGrid.ItemsSource = DB.GetAllInformationByPostIDAndWithOutpostContactType((int)AfterPostComboBox.SelectedValue, (byte)DB.PostContactConnectionType.PCMRemote);
                NewConnectionDataGrid.Visibility = Visibility.Visible;
                //AfterNumberVacantBinding.Text = newPostContactList.Where(t => t.Status == (byte)DB.PostContactStatus.Free && !(t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).Count().ToString();

            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PartialTransferGrid != null)
            {
                if (AllTransferButton.IsSelected == true)
                {
                    PartialTransferGrid.Visibility = Visibility.Hidden;
                    FullTransferGrid.Visibility = Visibility.Visible;
                }
                else if (PartialTransferButtom.IsSelected == true)
                {
                    FullTransferGrid.Visibility = Visibility.Hidden;
                    PartialTransferGrid.Visibility = Visibility.Visible;
                }
            }
        }

        private void AfterGroupPostNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void FirstInputAfterTransferComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (FirstInputAfterTransferComboBox.SelectedValue != null)
            {
            }
        }

        private void FromOldConnectionNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FromOldConnectionNo.SelectedValue != null)
                FromCabinetInputID.SelectedValue = oldBuchtList.Where(t => t.ConnectionID == (long)FromOldConnectionNo.SelectedValue && (t.BuchtTypeID != (int)DB.BuchtType.OutLine && t.BuchtTypeID != (int)DB.BuchtType.InLine)).Select(t => t.CabinetInputID).SingleOrDefault();
        }

        private void ToOldConnectionNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void wiringButtom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    IssueWiring issueWiring = new IssueWiring();
                    issueWiring.InsertDate = DB.GetServerDate();
                    issueWiring.RequestID = _reqeust.ID;
                    issueWiring.WiringNo = _reqeust.ID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                    issueWiring.WiringTypeID = (byte)DB.WiringType.CableBack;
                    issueWiring.WiringIssueDate = DB.GetServerDate();
                    issueWiring.PrintCount = 0;
                    issueWiring.IsPrinted = false;
                    issueWiring.Status = _reqeust.StatusID;
                    issueWiring.Detach();
                    DB.Save(issueWiring);


                    if (oldPostContactList.Where(t => !(t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal) && t.Status == (byte)DB.PostContactStatus.CableConnection).ToList().Count <= newPostContactList.Count)
                    {

                        if (AllTransferButton.IsSelected == true)
                        {
                            int i = 0;
                            List<Wiring> wiringList = new List<Wiring>();
                            for (i = 0; i < oldBuchtList.Count; i++)
                            {
                                Wiring wiring = new Wiring();
                                wiring.OldConnectionID = oldBuchtList[i].ConnectionID;
                                wiring.OldBuchtID = oldBuchtList[i].ID;
                                wiring.OldBuchtType = (byte)oldBuchtList[i].BuchtTypeID;
                                wiring.ConnectionID = newPostContactList[i].ID;
                                wiring.OldConnectionType = (byte)oldBuchtList[i].BuchtTypeID;
                                wiring.IssueWiringID = issueWiring.ID;
                                wiring.RequestID = _reqeust.ID;
                                wiring.Status = _reqeust.StatusID;
                                wiring.Detach();
                                wiringList.Add(wiring);
                            }

                            DB.SaveAll(wiringList);

                        }
                        else if (PartialTransferButtom.IsSelected == true)
                        {

                            Wiring wiring = new Wiring();

                            Bucht bucht = Data.BuchtDB.GetBuchtByConnectionID((long)FromOldConnectionNo.SelectedValue);
                            wiring.OldConnectionID = bucht.ConnectionID;
                            wiring.OldBuchtID = bucht.ID;
                            wiring.OldBuchtType = (byte)bucht.BuchtTypeID;
                            wiring.ConnectionID = (long)ToOldConnectionNo.SelectedValue;
                            wiring.IssueWiringID = issueWiring.ID;
                            wiring.RequestID = _reqeust.ID;
                            wiring.Status = _reqeust.StatusID;
                            wiring.Detach();
                            DB.Save(wiring);
                        }
                        ts.Complete();

                        ShowSuccessMessage("ذخیره سیم بندی انجام شد");
                    }
                    else
                    {
                        throw new Exception("تعداد اتصالی های جدید از اتصالی های قدیم کمتر است");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("ذخیره سیم بندی انجام نشد", ex);
            }
        }
        #endregion

        #region ActionMethod
        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(5)))
                {

                    _reqeust = _exchangeRequestInfo.Request;

                    Status Status = Data.StatusDB.GetStatueByStatusID(_reqeust.StatusID);
                    _reqeust.RequestPaymentTypeID = 0;
                    _reqeust.IsViewed = false;
                    _reqeust.InsertDate = DB.GetServerDate();

                    if (_ID == 0)
                    {
                        _reqeust.ID = DB.GenerateRequestID();
                        _reqeust.StatusID = DB.GetStatus(_RequestType, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                        _reqeust.Detach();
                        DB.Save(_reqeust, true);
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
                    if (_exchangePost.Completion == true)
                    {
                        throw new Exception("در درخواست قبلا اعمال تغییرات صورت گرفته است");
                    }

                    if (WithChangeCabinetInputRadioButton.IsChecked == true)
                    {
                        _exchangePost.WithChangeCabinetInput = true;
                    }
                    else if (WithoutChangeCabinetInputRadioButton.IsChecked == true)
                    {
                        _exchangePost.WithChangeCabinetInput = false;
                    }

                    _exchangePost = this.DataContext as ExchangePost;


                    if (WithChangeCabinetInputRadioButton.IsChecked == true)
                    {
                        #region WithChangeCabinetInput

                        // Verify data 

                        VerifyDataByWithChangeCabinetInput(_exchangePost);



                        Status = Data.StatusDB.GetStatueByStatusID((int)StatusComboBox.SelectedValue);
                        // اگر در وضعیت تغییرات نیست
                        if (_ID == 0 || Status.StatusType != (byte)DB.RequestStatusType.Changes)
                        {
                            _exchangePost.ID = _reqeust.ID;
                            _exchangePost.InsertDate = DB.GetServerDate();
                            if (AllTransferButton.IsSelected == true)
                            {
                                _exchangePost.OverallTransfer = true;

                            }
                            else
                                _exchangePost.OverallTransfer = false;

                            _exchangePost.Detach();
                            if (_ID == 0)
                                DB.Save(_exchangePost, true);
                            else
                                DB.Save(_exchangePost, false);

                            #region Reserve
                            if (AllTransferButton.IsSelected == true)
                            {

                                if (pastOldPost != null)
                                {
                                    pastOldPost.Status = (byte)DB.PostStatus.Dayer;
                                    pastOldPost.Detach();
                                    DB.Save(pastOldPost);
                                }
                                if (pastNewPost != null)
                                {
                                    pastNewPost.Status = (byte)DB.PostStatus.Dayer;
                                    pastNewPost.Detach();
                                    DB.Save(pastNewPost);
                                }

                                Post OldPost = Data.PostDB.GetPostByID(_exchangePost.OldPostID);
                                Post NewPost = Data.PostDB.GetPostByID(_exchangePost.NewPostID);


                                OldPost.Status = (byte)DB.PostStatus.ReserveForExchange;
                                NewPost.Status = (byte)DB.PostStatus.ReserveForExchange;
                                OldPost.Detach();
                                DB.Save(OldPost, false);
                                NewPost.Detach();
                                DB.Save(NewPost, false);
                            }
                            else if (PartialTransferButtom.IsSelected == true)
                            {

                                if (partialPastOldPostContact != null)
                                {
                                    if (partialPastOldPostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote)
                                    {
                                        partialPastOldPostContact.Status = (int)DB.PostContactStatus.CableConnection;
                                    }
                                    else
                                    {
                                        partialPastOldPostContact.Status = (int)DB.PostContactStatus.NoCableConnection;
                                    }

                                    partialPastOldPostContact.Detach();
                                    DB.Save(partialPastOldPostContact);

                                    Bucht FromPassPartialBucht;
                                    if (partialPastOldPostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote)
                                    {
                                        FromPassPartialBucht = Data.BuchtDB.GetBuchtByConnectionID(partialPastOldPostContact.ID);
                                        FromPassPartialBucht.Status = (int)DB.BuchtStatus.Connection;
                                    }
                                    else
                                    {
                                        FromPassPartialBucht = Data.BuchtDB.GetBuchtByConnectionIDAndBuchtType(partialPastOldPostContact.ID, (int)DB.BuchtType.CustomerSide);
                                        FromPassPartialBucht.Status = (int)DB.BuchtStatus.AllocatedToInlinePCM;
                                    }
                                    FromPassPartialBucht.Detach();
                                    DB.Save(FromPassPartialBucht);
                                }

                                if (partialPastNewPostContact != null)
                                {

                                    partialPastNewPostContact.Status = (int)DB.PostContactStatus.Free;
                                    partialPastNewPostContact.Detach();
                                    DB.Save(partialPastNewPostContact);


                                }
                                if (PartialPassNewCabinetInput != null)
                                {
                                    Bucht FromPassPartialBucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(new List<long> { PartialPassNewCabinetInput.ID }).SingleOrDefault();
                                    FromPassPartialBucht.Status = (int)DB.BuchtStatus.Free;
                                    FromPassPartialBucht.Detach();
                                    DB.Save(FromPassPartialBucht);
                                }

                                PostContact FromPartialPostContact = Data.PostContactDB.GetPostContactByID((long)_exchangePost.FromOldConnectionID);
                                FromPartialPostContact.Status = (int)DB.PostContactStatus.FullBooking;
                                FromPartialPostContact.Detach();
                                DB.Save(FromPartialPostContact);
                                Bucht FromPartialBucht;
                                if (FromPartialPostContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                                {
                                    FromPartialBucht = Data.BuchtDB.GetBuchtByConnectionIDAndBuchtType((long)_exchangePost.FromOldConnectionID, (int)DB.BuchtType.CustomerSide);
                                }
                                else
                                {
                                    FromPartialBucht = Data.BuchtDB.GetBuchtByConnectionID((long)_exchangePost.FromOldConnectionID);
                                }
                                FromPartialBucht.Status = (int)DB.BuchtStatus.ExchangePostContact;
                                FromPartialBucht.Detach();
                                DB.Save(FromPartialBucht);

                                PostContact ToPartialPostContact = Data.PostContactDB.GetPostContactByID((long)_exchangePost.ToOldConnectionID);
                                ToPartialPostContact.Status = (int)DB.PostContactStatus.FullBooking;
                                ToPartialPostContact.Detach();
                                DB.Save(ToPartialPostContact);

                                Bucht ToPartialBucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(new List<long> { (long)_exchangePost.ToCabinetInputID }).SingleOrDefault();
                                ToPartialBucht.Status = (int)DB.BuchtStatus.ExchangePostContact;
                                ToPartialBucht.Detach();
                                DB.Save(ToPartialBucht);
                            }
                            #endregion Reserve

                            ShowSuccessMessage("ذخیره کابل برگردان انجام شد");
                        }
                        // اگر وضعیت اعمال تغییرات باشد
                        else if (Status.StatusType == (byte)DB.RequestStatusType.Changes)
                        {
                            #region UndoFromReserve
                            if (PartialTransferButtom.IsSelected == true)
                            {

                                PostContact FromPartialPostContact = Data.PostContactDB.GetPostContactByID((long)_exchangePost.FromOldConnectionID);
                                if (FromPartialPostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote)
                                {
                                    FromPartialPostContact.Status = (int)DB.PostContactStatus.CableConnection;
                                }
                                else
                                {
                                    FromPartialPostContact.Status = (int)DB.PostContactStatus.NoCableConnection;
                                }
                                FromPartialPostContact.Detach();
                                DB.Save(FromPartialPostContact);


                                if (FromPartialPostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote)
                                {
                                    oldBuchtList[0].Status = (int)DB.BuchtStatus.Connection;
                                }
                                else
                                {
                                    oldBuchtList[0].Status = (int)DB.BuchtStatus.AllocatedToInlinePCM;
                                }

                                oldBuchtList[0].Detach();
                                DB.Save(oldBuchtList[0]);

                                PostContact ToPartialPostContact = Data.PostContactDB.GetPostContactByID((long)_exchangePost.ToOldConnectionID);
                                ToPartialPostContact.Status = (int)DB.PostContactStatus.Free;
                                ToPartialPostContact.Detach();
                                DB.Save(ToPartialPostContact);


                                newBuchtList[0].Status = (int)DB.BuchtStatus.Free;
                                newBuchtList[0].Detach();
                                DB.Save(newBuchtList[0]);


                            }
                            else
                            {
                                Post OldPost = Data.PostDB.GetPostByID(_exchangePost.OldPostID);
                                Post NewPost = Data.PostDB.GetPostByID(_exchangePost.NewPostID);


                                OldPost.Status = (byte)DB.PostStatus.Dayer;
                                NewPost.Status = (byte)DB.PostStatus.Dayer;
                                OldPost.Detach();
                                DB.Save(OldPost, false);
                                NewPost.Detach();
                                DB.Save(NewPost, false);

                                _reqeust.InsertDate = DB.GetServerDate();
                                _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                                _reqeust.Detach();
                                DB.Save(_reqeust);
                            }
                            #endregion UndoFromReserve

                            if (oldPostContactList.Count() <= newPostContactList.Count)
                            {
                                _exchangePost.Completion = true;
                                _exchangePost.Detach();
                                DB.Save(_exchangePost);

                                _reqeust.InsertDate = DB.GetServerDate();
                                _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                                _reqeust.Detach();
                                DB.Save(_reqeust);
                                PostContact newPostContact;
                                for (int i = 0; i < oldPostContactList.Count(); i++)
                                {

                                    // برای هر اتصالی بررسی می شود که اتصالی پی سی ام می باشد یا خیر
                                    // در صورت پی سی ام نبوده اطلاعات از بوخت قدیم به بوخت جدید انتقال پیدا می کند
                                    // در صورت پی سی ام بودن اتصالی های اضافه شده برای پی سی ام با تغییر نوع پست به پست جدید منتقل می شود ورودی تنظیم می شود و سپس اطلاعات بوخت پی سی ام اتقال میابد

                                    if (PartialTransferButtom.IsSelected == true)
                                    {
                                        newPostContact = newPostContactList[i];
                                    }
                                    else
                                    {
                                        newPostContact = newPostContactList.SingleOrDefault(t => t.ConnectionNo == oldPostContactList[i].ConnectionNo);
                                    }

                                    if (oldPostContactList[i].ConnectionType != (int)DB.PostContactConnectionType.PCMRemote)
                                    {
                                        #region ExchangeNormalPostContact
                                        Bucht oldBucht = oldBuchtList.Where(t => t.ConnectionID == oldPostContactList[i].ID && (t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine)).SingleOrDefault();

                                        newBuchtList[i].ConnectionID = newPostContact.ID;
                                        oldBucht.ConnectionID = null;

                                        newBuchtList[i].SwitchPortID = oldBucht.SwitchPortID;
                                        oldBucht.SwitchPortID = null;

                                        newBuchtList[i].Status = oldBucht.Status;
                                        oldBucht.Status = (int)DB.BuchtStatus.Free;

                                        //newBuchtList[i].ADSLPortID = oldBucht.ADSLPortID;
                                        //oldBucht.ADSLPortID = null;

                                        //newBuchtList[i].ADSLType = oldBucht.ADSLType;
                                        //oldBucht.ADSLType = null;


                                        newBuchtList[i].ADSLStatus = oldBucht.ADSLStatus;
                                        oldBucht.ADSLStatus = false;

                                        int newBuchtType = newBuchtList[i].BuchtTypeID;
                                        newBuchtList[i].BuchtTypeID = oldBucht.BuchtTypeID;
                                        oldBucht.BuchtTypeID = newBuchtType;

                                        if (oldBucht.BuchtIDConnectedOtherBucht != null)
                                        {
                                            Bucht buchtPrivateWire = Data.BuchtDB.GetBuchetByID(oldBucht.BuchtIDConnectedOtherBucht);
                                            buchtPrivateWire.BuchtIDConnectedOtherBucht = newBuchtList[i].ID;
                                            buchtPrivateWire.Detach();
                                            DB.Save(buchtPrivateWire, false);

                                            if (oldBuchtList.Any(b => b.ID == oldBucht.BuchtIDConnectedOtherBucht)) oldBuchtList.SingleOrDefault(b => b.ID == oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = newBuchtList[i].ID;
                                            if (newBuchtList.Any(b => b.ID == oldBucht.BuchtIDConnectedOtherBucht)) newBuchtList.SingleOrDefault(b => b.ID == oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = newBuchtList[i].ID;


                                            newBuchtList[i].BuchtIDConnectedOtherBucht = buchtPrivateWire.ID;
                                            oldBucht.BuchtIDConnectedOtherBucht = null;
                                        }
                                        newPostContact.Status = (int)DB.PostContactStatus.CableConnection;
                                        newPostContact.ConnectionType = (int)DB.PostContactConnectionType.Noraml;

                                        oldPostContactList[i].Status = (byte)DB.PostContactStatus.Free;
                                        oldPostContactList[i].ConnectionType = (int)DB.PostContactConnectionType.Noraml;




                                        oldBucht.Detach();
                                        newBuchtList[i].Detach();
                                        DB.Save(oldBucht);
                                        DB.Save(newBuchtList[i]);

                                        newPostContact.Detach();
                                        oldPostContactList[i].Detach();
                                        DB.Save(newPostContact);
                                        DB.Save(oldPostContactList[i]);
                                        #endregion ExchangeNormalPostContact
                                    }
                                    else if (oldPostContactList[i].ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                                    {

                                        // برای انتقال پی سی ام ابتدا اتصالی های ریموت پی سی ام با تغییر پست به پست جدید منتقل می شود شماره اتصالی با شماره اتصالی جدید مقدار می گیرد
                                        // سپس مرکزی بوخت ها متصل به اتصالی های ریموت پی سی ام تغییر می کند
                                        // سپس بوخت طرف مشترک پی سی ام روی ام دی اف تغییر می کند
                                        //
                                        #region ExchnagePCMPostContect
                                        newPostContact.Status = (byte)DB.PostContactStatus.NoCableConnection;
                                        newPostContact.ConnectionType = (byte)DB.PostContactConnectionType.PCMRemote;

                                        oldPostContactList[i].Status = (byte)DB.PostContactStatus.Free;
                                        oldPostContactList[i].ConnectionType = (byte)DB.PostContactConnectionType.Noraml;

                                        List<PostContact> pastOldPostContactPCMOutputList = pastOldPostContactList.Where(t => t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal && t.ConnectionNo == oldPostContactList[i].ConnectionNo).ToList();
                                        foreach (PostContact PostContactPCM in pastOldPostContactPCMOutputList)
                                        {
                                            PostContactPCM.PostID = _exchangePost.NewPostID;
                                            PostContactPCM.ConnectionNo = newPostContact.ConnectionNo;
                                            PostContactPCM.Detach();
                                        }


                                        List<Bucht> oldBuchtPCMListTemp = oldBuchtList.Where(t => pastOldPostContactPCMOutputList.Select(p => p.ID).Contains(t.ConnectionID ?? 0)).ToList();
                                        foreach (Bucht buchtPcm in oldBuchtPCMListTemp)
                                        {
                                            buchtPcm.CabinetInputID = cabinetInputList[i].LongID;
                                            buchtPcm.Detach();
                                        }
                                        DB.UpdateAll(oldBuchtPCMListTemp);


                                        Bucht oldBuchtOutLinePCMRemote = oldBuchtList.Where(t => oldPostContactList[i].ID == t.ConnectionID && t.BuchtTypeID == (int)DB.BuchtType.OutLine).SingleOrDefault();
                                        Bucht newBuchtOutLinePCMRemote = newBuchtList.Where(t => t.CabinetInputID == cabinetInputList[i].LongID).SingleOrDefault();
                                        Bucht oldBuchtCustomerSidePcm = oldBuchtList.Where(t => oldPostContactList[i].ID == t.ConnectionID && t.BuchtTypeID == (int)DB.BuchtType.CustomerSide).SingleOrDefault();

                                        oldBuchtOutLinePCMRemote.BuchtIDConnectedOtherBucht = newBuchtOutLinePCMRemote.ID;
                                        oldBuchtOutLinePCMRemote.ConnectionID = newPostContact.ID;
                                        oldBuchtOutLinePCMRemote.CabinetInputID = cabinetInputList[i].LongID;
                                        oldBuchtOutLinePCMRemote.Detach();
                                        DB.Save(oldBuchtOutLinePCMRemote);

                                        newBuchtOutLinePCMRemote.BuchtIDConnectedOtherBucht = oldBuchtOutLinePCMRemote.ID;
                                        newBuchtOutLinePCMRemote.Status = (int)DB.BuchtStatus.AllocatedToInlinePCM;
                                        newBuchtOutLinePCMRemote.ConnectionID = newPostContact.ID;
                                        newBuchtOutLinePCMRemote.Detach();
                                        DB.Save(newBuchtOutLinePCMRemote);

                                        oldBuchtCustomerSidePcm.BuchtIDConnectedOtherBucht = null;
                                        oldBuchtCustomerSidePcm.ConnectionID = null;
                                        oldBuchtCustomerSidePcm.Status = (int)DB.BuchtStatus.Free;
                                        oldBuchtCustomerSidePcm.Detach();
                                        DB.Save(oldBuchtCustomerSidePcm);


                                        oldPostContactList[i].Detach();
                                        DB.Save(oldPostContactList[i]);

                                        newPostContact.Detach();
                                        DB.Save(newPostContact);

                                        pastOldPostContactPCMOutputList.ForEach(item => item.Detach());
                                        DB.UpdateAll(pastOldPostContactPCMOutputList);

                                        #endregion ExchnagePCMPostContect
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("وضعیت نامشخص می باشد");
                        }

                        #endregion WithChangeCabinetInput
                    }
                    else if (WithoutChangeCabinetInputRadioButton.IsChecked == true)
                    {
                        #region WithotChangeCabinetInputSave

                        // Verify data 

                        VerifyWithoutChangeCabinetInput(_exchangePost);



                        Status = Data.StatusDB.GetStatueByStatusID((int)StatusComboBox.SelectedValue);
                        // اگر در وضعیت تغییرات نیست
                        if (_ID == 0 || Status.StatusType != (byte)DB.RequestStatusType.Changes)
                        {
                            _exchangePost.ID = _reqeust.ID;
                            _exchangePost.InsertDate = DB.GetServerDate();
                            if (AllTransferButton.IsSelected == true)
                            {
                                _exchangePost.OverallTransfer = true;
                            }
                            else
                                _exchangePost.OverallTransfer = false;

                            _exchangePost.Detach();
                            if (_ID == 0)
                                DB.Save(_exchangePost, true);
                            else
                                DB.Save(_exchangePost, false);

                            #region Reserve
                            if (AllTransferButton.IsSelected == true)
                            {

                                if (pastOldPost != null)
                                {
                                    pastOldPost.Status = (byte)DB.PostStatus.Dayer;
                                    pastOldPost.Detach();
                                    DB.Save(pastOldPost);
                                }
                                if (pastNewPost != null)
                                {
                                    pastNewPost.Status = (byte)DB.PostStatus.Dayer;
                                    pastNewPost.Detach();
                                    DB.Save(pastNewPost);
                                }

                                Post OldPost = Data.PostDB.GetPostByID(_exchangePost.OldPostID);
                                Post NewPost = Data.PostDB.GetPostByID(_exchangePost.NewPostID);


                                OldPost.Status = (byte)DB.PostStatus.ReserveForExchange;
                                NewPost.Status = (byte)DB.PostStatus.ReserveForExchange;
                                OldPost.Detach();
                                DB.Save(OldPost, false);
                                NewPost.Detach();
                                DB.Save(NewPost, false);
                            }
                            else if (PartialTransferButtom.IsSelected == true)
                            {

                                if (partialPastOldPostContact != null)
                                {
                                    if (partialPastOldPostContact.ConnectionType != (int)DB.PostContactConnectionType.PCMRemote)
                                    {
                                        partialPastOldPostContact.Status = (int)DB.PostContactStatus.CableConnection;
                                    }
                                    else
                                    {
                                        partialPastOldPostContact.Status = (int)DB.PostContactStatus.NoCableConnection;
                                    }

                                    partialPastOldPostContact.Detach();
                                    DB.Save(partialPastOldPostContact);
                                }

                                if (partialPastNewPostContact != null)
                                {

                                    partialPastNewPostContact.Status = (int)DB.PostContactStatus.Free;
                                    partialPastNewPostContact.Detach();
                                    DB.Save(partialPastNewPostContact);
                                }

                                PostContact FromPartialPostContact = Data.PostContactDB.GetPostContactByID((long)_exchangePost.FromOldConnectionID);
                                FromPartialPostContact.Status = (int)DB.PostContactStatus.FullBooking;
                                FromPartialPostContact.Detach();
                                DB.Save(FromPartialPostContact);

                                PostContact ToPartialPostContact = Data.PostContactDB.GetPostContactByID((long)_exchangePost.ToOldConnectionID);
                                ToPartialPostContact.Status = (int)DB.PostContactStatus.FullBooking;
                                ToPartialPostContact.Detach();
                                DB.Save(ToPartialPostContact);
                            }
                            #endregion Reserve

                            ShowSuccessMessage("ذخیره کابل برگردان انجام شد");
                        }
                        // اگر وضعیت اعمال تغییرات باشد
                        else if (Status.StatusType == (byte)DB.RequestStatusType.Changes)
                        {
                            _exchangePost.Completion = true;
                            _exchangePost.Detach();
                            DB.Save(_exchangePost);

                            #region UndoFromReserve
                            if (PartialTransferButtom.IsSelected == true)
                            {
                                // not need. be change in exchange of postContact
                            }
                            else
                            {
                                Post OldPost = Data.PostDB.GetPostByID(_exchangePost.OldPostID);
                                Post NewPost = Data.PostDB.GetPostByID(_exchangePost.NewPostID);


                                OldPost.Status = (byte)DB.PostStatus.Dayer;
                                NewPost.Status = (byte)DB.PostStatus.Dayer;
                                OldPost.Detach();
                                DB.Save(OldPost, false);
                                NewPost.Detach();
                                DB.Save(NewPost, false);

                                _reqeust.InsertDate = DB.GetServerDate();
                                _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                                _reqeust.Detach();
                                DB.Save(_reqeust);
                            }
                            #endregion UndoFromReserve

                            PostContact newPostContact;
                            for (int i = 0; i < oldPostContactList.Count(); i++)
                            {

                                if (PartialTransferButtom.IsSelected == true)
                                {
                                    newPostContact = newPostContactList[i];
                                }
                                else
                                {
                                    newPostContact = newPostContactList.SingleOrDefault(t => t.ConnectionNo == oldPostContactList[i].ConnectionNo);
                                }

                                if (oldPostContactList[i].ConnectionType != (int)DB.PostContactConnectionType.PCMRemote)
                                {
                                    // اتصالی های غیره پی سی ام با تغییر پست انها به پست جدید منتقل می شوند
                                    int newPostID = newPostContact.PostID;
                                    int connectionNo = newPostContact.ConnectionNo;


                                    newPostContact.PostID = oldPostContactList[i].PostID;
                                    newPostContact.ConnectionNo = oldPostContactList[i].ConnectionNo;
                                    newPostContact.Status = (int)DB.PostContactStatus.Free;

                                    oldPostContactList[i].PostID = newPostID;
                                    oldPostContactList[i].ConnectionNo = connectionNo;
                                    oldPostContactList[i].Status = (int)DB.PostContactStatus.CableConnection;


                                    oldPostContactList[i].Detach();
                                    DB.Save(oldPostContactList[i]);

                                    newPostContact.Detach();
                                    DB.Save(newPostContact);


                                }
                                else if (oldPostContactList[i].ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                                {
                                    // اتصالی های پی سی ام با تغییر

                                    newPostContact.Status = (byte)DB.PostContactStatus.NoCableConnection;
                                    newPostContact.ConnectionType = (byte)DB.PostContactConnectionType.PCMRemote;

                                    oldPostContactList[i].Status = (byte)DB.PostContactStatus.Free;
                                    oldPostContactList[i].ConnectionType = (byte)DB.PostContactConnectionType.Noraml;

                                    List<PostContact> pastOldPostContactPCMOutputList = pastOldPostContactList.Where(t => t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal && t.ConnectionNo == oldPostContactList[i].ConnectionNo).ToList();
                                    foreach (PostContact PostContactPCM in pastOldPostContactPCMOutputList)
                                    {
                                        PostContactPCM.PostID = _exchangePost.NewPostID;
                                        PostContactPCM.ConnectionNo = newPostContact.ConnectionNo;
                                        PostContactPCM.Detach();
                                    }

                                    oldPostContactList[i].Detach();
                                    DB.Save(oldPostContactList[i]);

                                    newPostContact.Detach();
                                    DB.Save(newPostContact);

                                    List<Bucht> buchtsConnectToPCM = oldBuchtList.Where(t => t.ConnectionID == oldPostContactList[i].ID).ToList();
                                    buchtsConnectToPCM.ForEach(item => { item.ConnectionID = newPostContact.ID; item.Detach(); });
                                    DB.UpdateAll(buchtsConnectToPCM);


                                    pastOldPostContactPCMOutputList.ForEach(item => item.Detach());
                                    DB.UpdateAll(pastOldPostContactPCMOutputList);
                                }
                            }

                        }

                        #endregion WithotChangeCabinetInputSave
                    }
                    ts.Complete();

                    ShowSuccessMessage("ذخیره کابل برگردان انجام شد");
                    IsSaveSuccess = true;
                }
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("ذخیره کابل برگردان انجام نشد", ex);
            }


            return IsSaveSuccess;
        }

        private void VerifyWithoutChangeCabinetInput(ExchangePost exchangePost)
        {

            Cabinet OldCabinet = Data.CabinetDB.GetCabinetByID(_exchangePost.OldCabinetID);
            Cabinet NewCabinet = Data.CabinetDB.GetCabinetByID(_exchangePost.NewCabinetID);

            if ((OldCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet && NewCabinet.CabinetUsageType != (byte)DB.CabinetUsageType.OpticalCabinet)
                 || (NewCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet && OldCabinet.CabinetUsageType != (byte)DB.CabinetUsageType.OpticalCabinet))
                throw new Exception("امکان برگردان از کافو نوری به کافو غیر از نوری نیست");

            if (WithChangeCabinetInputRadioButton.IsChecked == true && (OldCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet || NewCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet))
                throw new Exception("امکان برگردان با تغییر مرکزی برای کافو نوری نمی باشد");


            if (AllTransferButton.IsSelected == true)
            {
                oldPostContactList = oldPostContactList.Where(t => t.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal).ToList();
                if (oldPostContactList.Any(t => t.Status == (byte)DB.PostContactStatus.FullBooking))
                {
                    throw new Exception("پست دارای اتصالی رزرو میباشد");
                }
                //Separate free postContact from new postContect
                newPostContactList = newPostContactList.Where(t => t.Status == (int)DB.PostContactStatus.Free && t.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal && t.ConnectionType != (byte)DB.PostContactConnectionType.PCMRemote).ToList();

                if (oldPostContactList.Count() > newPostContactList.Count)
                    throw new Exception("تعداد اتصال های آزاد پست جدید کم تر از تعداد اتصالهای پست قبل برگردان است");

                if (oldPostContactList.Any(t => !newPostContactList.Select(t2 => t2.ConnectionNo).Contains(t.ConnectionNo)))
                    throw new Exception("باید همه شماره اتصالی های متصل در پست قدیم در پست جدید آزاد باشد. ");
            }
            else if (PartialTransferButtom.IsSelected == true)
            {
                oldPostContactList = new List<PostContact> { Data.PostContactDB.GetPostContactByID((long)exchangePost.FromOldConnectionID) };
                if (oldPostContactList.Any(t => t.Status == (byte)DB.PostContactStatus.FullBooking))
                {
                    throw new Exception("اتصالی قدیم دارای وضعیت رزرو می باشد امکان انتخاب ان نیست");
                }

                newPostContactList = new List<PostContact> { Data.PostContactDB.GetPostContactByID((long)exchangePost.ToOldConnectionID) };
                if (newPostContactList.Any((t => t.Status != (byte)DB.PostContactStatus.Free && t.Status != (byte)DB.PostContactStatus.FullBooking)))
                {
                    throw new Exception("اتصالی جدید آزاد نمی باشد.");
                }
            }
        }

        private void VerifyDataByWithChangeCabinetInput(ExchangePost exchangePost)
        {
            Cabinet OldCabinet = Data.CabinetDB.GetCabinetByID(_exchangePost.OldCabinetID);
            Cabinet NewCabinet = Data.CabinetDB.GetCabinetByID(_exchangePost.NewCabinetID);

            if ((OldCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet && NewCabinet.CabinetUsageType != (byte)DB.CabinetUsageType.OpticalCabinet)
                 || (NewCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet && OldCabinet.CabinetUsageType != (byte)DB.CabinetUsageType.OpticalCabinet))
                throw new Exception("امکان برگردان از کافو نوری به کافو غیر از نوری نیست");

            if (WithChangeCabinetInputRadioButton.IsChecked == true && (OldCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet || NewCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet))
                throw new Exception("امکان برگردان با تغییر مرکزی برای کافو نوری نمی باشد");


            if (AllTransferButton.IsSelected == true)
            {

                oldPostContactList = oldPostContactList.Where(t => t.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal).ToList();
                if (oldPostContactList.Any(t => t.Status == (byte)DB.PostContactStatus.FullBooking))
                {
                    throw new Exception("پست دارای اتصالی رزرو میباشد");
                }
           
                // چون برای انتقال پی سی ام به پست جدید فقط به یک انصالی نیاز است اثصالی های پی سی ام در تعداد در نظر نگرفتم
                if (oldPostContactList.Count() > newPostContactList.Count)
                    throw new Exception("تعداد اتصال های پست جدید کم تر از تعداد اتصالهای پست قبل برگردان است");

                //int freePostContactCount = newPostContactList.Where(t => t.Status == (byte)DB.PostContactStatus.Free && !(t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).Count();

                //if (oldPostContactList.Count() > freePostContactCount)
                //    throw new Exception("تعداد اتصال های  خالی پست جدید کم تر از تعداد اتصالهای  پر پست قبل برگردان است");


                // به تعداد ااتصالی های پست قدیم از ورودی های جدید جدا می کند.
                CabinetInput cabinetInput = Data.CabinetInputDB.GetCabinetInputByID((long)exchangePost.ToCabinetInputID);
                cabinetInputList = cabinetInputList.Where(t => Convert.ToInt32(t.Name) >= cabinetInput.InputNumber).OrderBy(t => Convert.ToInt32(t.Name)).Take(oldPostContactList.Count()).ToList();

                if (oldPostContactList.Count() > cabinetInputList.Count)
                    throw new Exception("تعداد ورودی های انتخاب شده از تعداد اتصالی های قدیم کمتر است");


                // بوخت های متصل به ورودی های جدید را جدا میکند
                newBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(cabinetInputList.Select(t => (long)t.LongID).ToList());

                if (cabinetInputList.Count() != newBuchtList.Count())
                    throw new Exception("تعداد ورودی های جدید برابر با تعداد بوخت های جدید نیست.");

                if (oldPostContactList.Any(t => !newPostContactList.Where(t2 => t2.Status == (int)DB.PostContactStatus.Free && t2.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal && t2.ConnectionType != (byte)DB.PostContactConnectionType.PCMRemote).Select(t2 => t2.ConnectionNo).Contains(t.ConnectionNo)))
                    throw new Exception("باید همه شماره اتصالی های متصل در پست قدیم در پست جدید آزاد باشد. ");
            }
            else if (PartialTransferButtom.IsSelected == true)
            {
                oldPostContactList = new List<PostContact> { Data.PostContactDB.GetPostContactByID((long)exchangePost.FromOldConnectionID) };
                if (oldPostContactList.Any(t => t.Status == (byte)DB.PostContactStatus.FullBooking))
                {
                    throw new Exception("این اتصالی دارای وضعیت رزرو می باشد امکان انتخاب ان نیست");
                }

                newPostContactList = new List<PostContact> { Data.PostContactDB.GetPostContactByID((long)exchangePost.ToOldConnectionID) };
                if (oldPostContactList.Any(t => t.Status == (byte)DB.PostContactStatus.FullBooking))
                {
                    throw new Exception("این اتصالی دارای وضعیت رزرو می باشد امکان انتخاب ان نیست");
                }

                newBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(new List<long> { (long)exchangePost.ToCabinetInputID });
                if (newBuchtList.Count() != 1)
                {
                    throw new Exception("بوخت ورودی یافت نشد.");
                }
                oldBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(new List<long> { (long)exchangePost.FromCabinetInputID });
                cabinetInputList = Data.CabinetInputDB.GetCabinetInputChechableByID((long)exchangePost.ToCabinetInputID);
                if (cabinetInputList.Count() != 1)
                {
                    throw new Exception("مرکزی یافت نشد.");
                }

            }
        }

        public override bool Forward()
        {
            if (_exchangePost.Completion != true)
            {
                Save();
            }
            else
            {
                IsForwardSuccess = true;
            }

            this.RequestID = _reqeust.ID;
            if (IsSaveSuccess == true)
                IsForwardSuccess = true;
            return IsForwardSuccess;
        }
        #endregion



    }
}
