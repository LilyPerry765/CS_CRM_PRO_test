using CRM.Application.Reports.Viewer;
using CRM.Application.UserControls;
using CRM.Data;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for CenterToCenterTranslationChooseNumberInfoFrom.xaml
    /// </summary>
    public partial class CenterToCenterTranslatioNetworkForm : Local.RequestFormBase
    {
        private long requestID = 0;
        BackgroundWorker worker;
        private Request request { get; set; }
        CRM.Application.UserControls.CenterToCenterTranslationInfo _centerToCenterTranslationInfo;
        Data.CenterToCenterTranslation _centerToCenterTranslation { get; set; }
        ObservableCollection<CenterToCenterTranslationChooseNumberInfo> telphonNumbers;

        public ObservableCollection<CheckableItem> _preCodes { get; set; }
        public ObservableCollection<Telephone> _newTelephons { get; set; }

        List<CabinetInput> _newCabinetInputs { get; set; }
        List<CabinetInput> _oldCabinetInputs { get; set; }

        List<Bucht> _oldBuchtList { get; set; }
        List<ConnectionInfo> _newBuchtInfoList { get; set; }
        List<Bucht> _newBuchtList { get; set; }

        List<Telephone> _newTelephones { get; set; }
        List<Telephone> _oldTelephones { get; set; }

        List<Post> _oldPosts { get; set; }
        List<Post> _newPosts { get; set; }

        List<PostContact> _oldPostContactList { get; set; }
        List<PostContact> _newPostContactList { get; set; }
        List<ConnectionInfo> _oldBuchtInfoList { get; set; }

        public CenterToCenterTranslatioNetworkForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            
        }
        public CenterToCenterTranslatioNetworkForm(long ID)
            : this()
        {
            this.requestID = ID;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

            _centerToCenterTranslationInfo = new CRM.Application.UserControls.CenterToCenterTranslationInfo(requestID);
            TranslationInfo.DataContext = _centerToCenterTranslationInfo;
            TranslationInfo.Content = _centerToCenterTranslationInfo;

            _centerToCenterTranslation = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslation(requestID);
            request = Data.RequestDB.GetRequestByID(requestID);
            if (request.CenterID == _centerToCenterTranslation.TargetCenterID && _centerToCenterTranslation.IsCompletion == false)
            {
                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward , (byte)DB.NewAction.Exit };
            }
            else
            {
                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.ConfirmEnd , (byte)DB.NewAction.Exit };
            }

            StatusComboBox.ItemsSource = DB.GetStepStatus(request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = request.StatusID;


            telphonNumbers = new ObservableCollection<CenterToCenterTranslationChooseNumberInfo>(Data.CenterToCenterTranslationDB.GetTelephones(_centerToCenterTranslation));
            _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_centerToCenterTranslation.TargetCenterID));

            List<CenterToCenterTranslationTelephone> centerToCenterTranslationTelephones = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslationTelephon(requestID).ToList();
            centerToCenterTranslationTelephones.ForEach(item =>
                                                           {
                                                               if (telphonNumbers.Any(t => t.TelephonNo == item.TelephoneNo))
                                                               {
                                                                   telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewTelephonNo = item.NewTelephoneNo;
                                                                   telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeID = item.NewSwitchPrecodeID;
                                                                   telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == item.NewSwitchPrecodeID).SingleOrDefault().Name));
                                                               }
                                                           }
                                                           );


            if (request.CenterID == _centerToCenterTranslation.TargetCenterID)
            {
                CabinetInputNumberTextcolumn.Visibility = Visibility.Collapsed;
                TelephonNoTextColumn.Visibility = Visibility.Collapsed;
                
                DetailGroupBox.Header = "جزئیات دایری اتصالی";
                PCMGroupBox.Header = "جزئیات دایری پی سی ام";
                if (Data.CenterToCenterTranslationDB.CheckExistCenterToCenterTranslationPCM(request.ID))
                {
                    PCMGroupBox.Visibility = Visibility.Visible;
                    PCMDataGrid.ItemsSource = Data.CenterToCenterTranslationDB.GetCenterToCenterPCMs(request.ID);

                }
            }
            else if (request.CenterID == _centerToCenterTranslation.SourceCenterID)
            {
                NewCabinetInputNumberTextcolumn.Visibility = Visibility.Collapsed;
                NewTelephonNoTextColumn.Visibility = Visibility.Collapsed;

                if (Data.CenterToCenterTranslationDB.CheckExistCenterToCenterTranslationPCM(request.ID))
                {
                    PCMGroupBox.Visibility = Visibility.Visible;
                    PCMDataGrid.ItemsSource = Data.CenterToCenterTranslationDB.GetCenterToCenterPCMs(request.ID);

                }

            }


            _oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_centerToCenterTranslation.FromOldCabinetInputID, _centerToCenterTranslation.ToOldCabinetInputID);
            // _oldBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDsOrderByInputNumber(_oldCabinetInputs.Select(t => t.ID).ToList());

            _newCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_centerToCenterTranslation.FromNewCabinetInputID, _centerToCenterTranslation.ToNewCabinetInputID);
            _newBuchtInfoList = Data.BuchtDB.GetBuchtInfoByCabinetInputIDsOrderByInputNumber(_newCabinetInputs.Select(t => t.ID).ToList());


            for (int i = 0; i < _oldCabinetInputs.Count(); i++)
            {

                if (telphonNumbers.Any(t => t.CabinetInputID == _oldCabinetInputs[i].ID))
                {
                    telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewCabinetInputID = _newCabinetInputs[i].ID;
                    telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewCabinetInputNumber = _newCabinetInputs[i].InputNumber;

                    telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewVerticalCloumnNo = _newBuchtInfoList[i].VerticalColumnNo;
                    telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewVerticalRowNo = _newBuchtInfoList[i].VerticalRowNo;
                    telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewBuchtNo = _newBuchtInfoList[i].BuchtNo;
                }

            }


            TelItemsDataGrid.DataContext = telphonNumbers;

        }


        private void NewPreCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewPreCodeComboBox != null && NewPreCodeComboBox.SelectedValue != null)
            {
                _newTelephons = new ObservableCollection<Telephone>(Data.SwitchPrecodeDB.GetTelephonesByPreCodes(new List<int> { (int)NewPreCodeComboBox.SelectedValue }, telphonNumbers.Count()));
                if (NewPreCodeComboBox.SelectedItem != null)
                {
                    CenterToCenterTranslationChooseNumberInfo CenterToCenterTranslationChooseNumberInfo = TelItemsDataGrid.SelectedItem as CenterToCenterTranslationChooseNumberInfo;
                    if (CenterToCenterTranslationChooseNumberInfo != null)
                    {
                        CenterToCenterTranslationChooseNumberInfo.NewPreCodeNumber = Convert.ToInt64((NewPreCodeComboBox.SelectedItem as CheckableItem).Name);
                        CenterToCenterTranslationChooseNumberInfo.NewTelephonNo = null;

                    }
                }
            }
        }


        private void NewTelephonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewTelephonComboBox != null && NewTelephonComboBox.SelectedValue != null && TelItemsDataGrid.SelectedItem != null)
            {
                CenterToCenterTranslationChooseNumberInfo CenterToCenterTranslationChooseNumberInfo = TelItemsDataGrid.SelectedItem as CenterToCenterTranslationChooseNumberInfo;
                if (CenterToCenterTranslationChooseNumberInfo != null)
                    CenterToCenterTranslationChooseNumberInfo.NewTelephonNo = (long)NewTelephonComboBox.SelectedValue;
            }
        }

        public override bool Save()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    request.StatusID = (int)StatusComboBox.SelectedValue;
                    request.Detach();
                    DB.Save(request, false);

                    ts.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage("ذخیره انجام شد");
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
                using (TransactionScope ts1 = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(0)))
                {

                    Save();
                    this.RequestID = requestID;
                    if (IsSaveSuccess)
                    {
                        IsForwardSuccess = true;
                        Status Status = Data.StatusDB.GetStatueByStatusID(request.StatusID);
                        if (request.CenterID == _centerToCenterTranslation.SourceCenterID && Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter)
                        {

                            request.CenterID = _centerToCenterTranslation.TargetCenterID;
                            request.Detach();
                            DB.Save(request, false);

                        }
                        else if(request.CenterID == _centerToCenterTranslation.TargetCenterID && Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter)
                        {

                            request.CenterID = _centerToCenterTranslation.SourceCenterID;
                            request.Detach();
                            DB.Save(request, false);

                        }
                    }
                    

                    ts1.Complete();

                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
                IsForwardSuccess = false;
            }
            return IsForwardSuccess;

        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ShowSuccessMessage(e.ProgressPercentage.ToString() + "%");
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            if (_centerToCenterTranslation.IsCompletion == true)
            {
                ShowSuccessMessage("برگردان انجام شد.");
                this.IsEnabled = true;
                _Action.InternalForward(null, null);
            }
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            if (_centerToCenterTranslation.IsCompletion != true)
            {
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
                {
                    _oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_centerToCenterTranslation.FromOldCabinetInputID, _centerToCenterTranslation.ToOldCabinetInputID);
                    _oldBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDsOrderByInputNumber(_oldCabinetInputs.Select(t => t.ID).ToList());
                    _oldTelephones = Data.TelephoneDB.GetTelephones(telphonNumbers.Select(t => t.TelephonNo).ToList());
                    _oldPosts = Data.PostDB.GetAllPostsByPostContactList(_oldBuchtList.Where(t => t.ConnectionID != null).Select(t => t.ConnectionID ?? 0).ToList());
                    _oldPostContactList = Data.PostContactDB.GetPostContactByListID(_oldBuchtList.Where(t => t.ConnectionID != null).Select(t => t.ConnectionID ?? 0).ToList());

                    _newCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_centerToCenterTranslation.FromNewCabinetInputID, _centerToCenterTranslation.ToNewCabinetInputID);
                    _newBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDsOrderByInputNumber(_newCabinetInputs.Select(t => t.ID).ToList());
                    _newTelephones = Data.TelephoneDB.GetTelephones(telphonNumbers.Select(t => (long)t.NewTelephonNo).ToList());
                    _newPosts = Data.PostDB.GetTheNumberPostByStartID(_newCabinetInputs.Take(1).SingleOrDefault().CabinetID, (int)_oldPosts.OrderBy(t => t.Number).Take(1).SingleOrDefault().ID, _oldPosts.Count());
                    _newPostContactList = Data.PostContactDB.GetPostContactByPostIDs(_newPosts.Select(t => t.ID).ToList());
                    _oldBuchtList = _oldBuchtList.Where(t => t.BuchtTypeID != (int)DB.BuchtType.InLine).ToList();
                    int buchtCount = _oldBuchtList.Count();

                    for (int i = 0; i < buchtCount; i++)
                    {
                        // transform bucht else PCM
                        if (_oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtList[i].Status == (int)DB.BuchtStatus.Connection && _oldBuchtList[i].BuchtIDConnectedOtherBucht == null)
                        {
                            _newBuchtList[i].Status = _oldBuchtList[i].Status;
                            _oldBuchtList[i].Status = (int)DB.BuchtStatus.Free;

                            PostContact oldPostContact = _oldPostContactList.Where(t => t.ID == _oldBuchtList[i].ConnectionID).SingleOrDefault();
                            Post oldPost = _oldPosts.Where(t => t.ID == oldPostContact.PostID).SingleOrDefault();

                            Post newPost = _newPosts.Where(t => t.Number == oldPost.Number).SingleOrDefault();
                            PostContact newPostContact = _newPostContactList.Where(t => t.PostID == newPost.ID && t.ConnectionNo == oldPostContact.ConnectionNo).SingleOrDefault();


                            oldPostContact.Status = (byte)DB.PostContactStatus.Free;
                            newPostContact.Status = (byte)DB.PostContactStatus.CableConnection;

                            _oldBuchtList[i].ConnectionID = null;
                            _newBuchtList[i].ConnectionID = newPostContact.ID;



                            Telephone oldTelephone = _oldTelephones.Where(t => t.SwitchPortID == _oldBuchtList[i].SwitchPortID).SingleOrDefault();
                            Telephone newTelephonTelephone = _newTelephones.Where(t => t.TelephoneNo == telphonNumbers.Where(t2 => t2.TelephonNo == oldTelephone.TelephoneNo).SingleOrDefault().NewTelephonNo).SingleOrDefault();

                            newTelephonTelephone.Status = (byte)DB.TelephoneStatus.Connecting;
                            newTelephonTelephone.CustomerID = oldTelephone.CustomerID;
                            newTelephonTelephone.InstallAddressID = oldTelephone.InstallAddressID;
                            newTelephonTelephone.CorrespondenceAddressID = oldTelephone.CorrespondenceAddressID;
                            newTelephonTelephone.ClassTelephone = oldTelephone.ClassTelephone;


                            oldTelephone.Status = (byte)DB.TelephoneStatus.Discharge;
                            oldTelephone.CustomerID = null;
                            oldTelephone.InstallAddressID = null;
                            oldTelephone.CorrespondenceAddressID = null;
                            oldTelephone.ClassTelephone = (byte)DB.ClassTelephone.LimitLess;


                            _newBuchtList[i].SwitchPortID = newTelephonTelephone.SwitchPortID;
                            _oldBuchtList[i].SwitchPortID = null;

                            //_newBuchtList[i].ADSLPortID = _oldBuchtList[i].ADSLPortID;
                            //_oldBuchtList[i].ADSLPortID = null;


                            _newBuchtList[i].ADSLStatus = _oldBuchtList[i].ADSLStatus;
                            _oldBuchtList[i].ADSLStatus = false;

                            //_newBuchtList[i].ADSLType = _oldBuchtList[i].ADSLType;
                            //_oldBuchtList[i].ADSLType = null;

                            _newBuchtList[i].Detach();
                            DB.Save(_newBuchtList[i], false);

                            _oldBuchtList[i].Detach();
                            DB.Save(_oldBuchtList[i], false);

                            oldTelephone.Detach();
                            DB.Save(oldTelephone, false);

                            newTelephonTelephone.Detach();
                            DB.Save(newTelephonTelephone, false);

                            oldPostContact.Detach();
                            DB.Save(oldPostContact, false);

                            newPostContact.Detach();
                            DB.Save(newPostContact, false);


                            // Transfer telephone feature
                            Data.TelephoneSpecialServiceTypeDB.ExchangeTelephoneNoFeature(request, DB.RequestType.CenterToCenterTranslation, DB.LogType.TransforFeature, oldTelephone.TelephoneNo, newTelephonTelephone.TelephoneNo, false);

                        }
                        // transform PCM bucht
                        else if (_oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtList[i].Status == (int)DB.BuchtStatus.AllocatedToInlinePCM && _oldBuchtList[i].BuchtIDConnectedOtherBucht != null)
                        {
                            CabinetInput cabinetInput = Data.CabinetInputDB.GetCabinetInputByNumber(_centerToCenterTranslation.OldCabinetID, _centerToCenterTranslation.NewCabinetID, (long)_oldBuchtList[i].CabinetInputID);
                            Bucht newPCMConnecterBucht = _newBuchtList.Where(t => t.CabinetInputID == cabinetInput.ID).SingleOrDefault(); ;

                            PCM oldPCM = Data.PCMDB.GetPCMByPortID((int)_oldBuchtList.Where(t => t.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).SingleOrDefault().PCMPortID);
                            List<Bucht> OldPCMBucht = Data.BuchtDB.GetBuchtByCabinetID((long)_oldBuchtList[i].CabinetInputID).Where(t => t.BuchtTypeID != (int)DB.BuchtType.CustomerSide).OrderBy(t => t.BuchtTypeID).OrderBy(t => t.PortNo).ToList();

                            PCM newPCM = Data.CenterToCenterTranslationDB.GetNewPCMByOldPCMID(request.ID, oldPCM.ID);
                            List<Bucht> newPCMBucht = Data.BuchtDB.GetBuchtByPCMID(newPCM.ID).OrderBy(t => t.BuchtTypeID).OrderBy(t => t.PortNo).ToList();


                            // انتقال اتصالی های پست قدیم به پست جدید
                            // اتصالی های مربوط به پی سی ام را با تعویض پست آنها به پست جدید انتقال می دهد
                            List<PostContact> BeforPostContacts = Data.PostContactDB.GetPCMPostContactByPostContactID((long)_oldBuchtList[i].ConnectionID);

                            Post newPost = _newPosts.Where(t => _oldPosts.Where(t2 => t2.ID == BeforPostContacts.Take(1).SingleOrDefault().PostID).SingleOrDefault().Number == t.Number).SingleOrDefault();
                            PostContact AfterPostContactsInline = Data.PostContactDB.GetPostContact(newPost.ID, BeforPostContacts.Take(1).SingleOrDefault().ConnectionNo);
                            int BeforPostID = BeforPostContacts.Take(1).SingleOrDefault().PostID;

                            BeforPostContacts.ForEach((PostContact item) => { item.PostID = AfterPostContactsInline.PostID; item.Detach(); });
                            AfterPostContactsInline.PostID = BeforPostID;


                            DB.UpdateAll(BeforPostContacts);
                            AfterPostContactsInline.Detach();
                            DB.Save(AfterPostContactsInline, false);




                            // انتقال اطلاعات بوخت ها از پی سی ام قدیم به جدید
                            for (int j = 0; j < newPCMBucht.Count(); j++)
                            {
                                newPCMBucht[j].ConnectionID = OldPCMBucht[j].ConnectionID;
                                newPCMBucht[j].Status = OldPCMBucht[j].Status;
                                OldPCMBucht[j].Status = (int)DB.BuchtStatus.Free;

                                // تعویض شماره
                                if (OldPCMBucht[j].SwitchPortID != null)
                                {
                                    Telephone oldTelephone = _oldTelephones.Where(t => t.SwitchPortID == OldPCMBucht[j].SwitchPortID).SingleOrDefault();
                                    Telephone newTelephonTelephone = _newTelephones.Where(t => t.TelephoneNo == telphonNumbers.Where(t2 => t2.TelephonNo == oldTelephone.TelephoneNo).SingleOrDefault().NewTelephonNo).SingleOrDefault();

                                    newTelephonTelephone.Status = (byte)DB.TelephoneStatus.Connecting;
                                    newTelephonTelephone.CustomerID = oldTelephone.CustomerID;
                                    newTelephonTelephone.InstallAddressID = oldTelephone.InstallAddressID;
                                    newTelephonTelephone.CorrespondenceAddressID = oldTelephone.CorrespondenceAddressID;
                                    newTelephonTelephone.ClassTelephone = oldTelephone.ClassTelephone;


                                    oldTelephone.Status = (byte)DB.TelephoneStatus.Discharge;
                                    oldTelephone.CustomerID = null;
                                    oldTelephone.InstallAddressID = null;
                                    oldTelephone.CorrespondenceAddressID = null;
                                    oldTelephone.ClassTelephone = (byte)DB.ClassTelephone.LimitLess;


                                    newPCMBucht[j].SwitchPortID = newTelephonTelephone.SwitchPortID;
                                    OldPCMBucht[j].SwitchPortID = null;

                                    oldTelephone.Detach();
                                    DB.Save(oldTelephone, false);

                                    newTelephonTelephone.Detach();
                                    DB.Save(newTelephonTelephone, false);

                                    // Transfer telephone feature
                                    Data.TelephoneSpecialServiceTypeDB.ExchangeTelephoneNoFeature(request, DB.RequestType.CenterToCenterTranslation, DB.LogType.TransforFeature, oldTelephone.TelephoneNo, newTelephonTelephone.TelephoneNo, false);

                                }
                                // تعویض بوخت طرف مشترک
                                if (OldPCMBucht[j].BuchtIDConnectedOtherBucht != null)
                                {


                                    if (newPCMBucht[j].BuchtTypeID != (int)DB.BuchtType.OutLine)
                                        throw new Exception("خطا در نوع بوخت پی سی ام خروجی");

                                    newPCMBucht[j].BuchtIDConnectedOtherBucht = newPCMConnecterBucht.ID;
                                    newPCMConnecterBucht.BuchtIDConnectedOtherBucht = newPCMBucht[j].ID;

                                    newPCMConnecterBucht.Status = _oldBuchtList[i].Status;
                                    newPCMConnecterBucht.ConnectionID = _oldBuchtList[i].ConnectionID;
                                    newPCMBucht[j].ConnectionID = OldPCMBucht[j].ConnectionID;

                                    _oldBuchtList[i].Status = (int)DB.BuchtStatus.Free;
                                    _oldBuchtList[i].BuchtIDConnectedOtherBucht = null;
                                    _oldBuchtList[i].ConnectionID = null;
                                    _oldBuchtList[i].Detach();
                                    DB.Save(_oldBuchtList[i], false);

                                    newPCMConnecterBucht.Detach();
                                    DB.Save(newPCMConnecterBucht, false);

                                }

                                newPCMBucht[j].CabinetInputID = newPCMConnecterBucht.CabinetInputID;
                                newPCMBucht[j].Detach();
                                DB.Save(newPCMBucht[j], false);



                                OldPCMBucht[j].SwitchPortID = null;
                                OldPCMBucht[j].CabinetInputID = null;
                                OldPCMBucht[j].ConnectionID = null;
                                OldPCMBucht[j].BuchtIDConnectedOtherBucht = null;
                                OldPCMBucht[j].Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                                OldPCMBucht[j].Detach();
                                DB.Save(OldPCMBucht[j], false);


                            }

                            newPCM.InstallAddress = oldPCM.InstallAddress;
                            newPCM.InstallPostCode = oldPCM.InstallPostCode;

                            oldPCM.InstallAddress = null;
                            oldPCM.InstallPostCode = null;

                            oldPCM.Status = (byte)DB.PCMStatus.Install;
                            newPCM.Status = (byte)DB.PCMStatus.Connection;

                            oldPCM.Detach();
                            DB.Save(oldPCM, false);

                            newPCM.Detach();
                            DB.Save(newPCM, false);

                         

                        }
                        // if bucht is PrivateWire change
                        else if (_oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtList[i].Status == (int)DB.BuchtStatus.Connection && _oldBuchtList[i].BuchtIDConnectedOtherBucht != null && Data.ExchangeCabinetInputDB.IsSpecialWire(_oldBuchtList[i].ID))
                        {
                            throw new Exception("خطا");
                            //_newBuchtList[i].Status = _oldBuchtList[i].Status;
                            //_oldBuchtList[i].Status = (int)DB.BuchtStatus.Free;

                            //_newBuchtList[i].ConnectionID = _oldBuchtList[i].ConnectionID;
                            //_oldBuchtList[i].ConnectionID = null;

                            //_newBuchtList[i].SwitchPortID = _oldBuchtList[i].SwitchPortID;
                            //_oldBuchtList[i].SwitchPortID = null;

                            //_newBuchtList[i].ADSLPortID = _oldBuchtList[i].ADSLPortID;
                            //_oldBuchtList[i].ADSLPortID = null;


                            //_newBuchtList[i].ADSLStatus = _oldBuchtList[i].ADSLStatus;
                            //_oldBuchtList[i].ADSLStatus = false;

                            //_newBuchtList[i].ADSLType = _oldBuchtList[i].ADSLType;
                            //_oldBuchtList[i].ADSLType = null;

                            //int newBuchtType = _newBuchtList[i].BuchtTypeID;
                            //_newBuchtList[i].BuchtTypeID = _oldBuchtList[i].BuchtTypeID;
                            //_oldBuchtList[i].BuchtTypeID = newBuchtType;


                            //Bucht buchtPrivateWire = Data.BuchtDB.GetBuchetByID(_oldBuchtList[i].BuchtIDConnectedOtherBucht);
                            //buchtPrivateWire.BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                            //if (_oldBuchtList.Any(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht)) _oldBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                            //if (_newBuchtList.Any(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht)) _newBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                            //buchtPrivateWire.Detach();
                            //DB.Save(buchtPrivateWire, false);

                            //_newBuchtList[i].BuchtIDConnectedOtherBucht = buchtPrivateWire.ID;
                            //_oldBuchtList[i].BuchtIDConnectedOtherBucht = null;

                        }
                        else if ((_oldBuchtList[i].Status == (int)DB.BuchtStatus.Connection && !(_oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.InLine || _oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.OutLine)))
                        {
                            throw new Exception("در میان بوخت ها نوع بوخت نا مشخصی وجود دارد لطفا با مدیر سیستم تماس بگیرید");
                        }

                        worker.ReportProgress(i * 100 / buchtCount);

                    }


                    Cabinet pastOldCabinet = Data.CabinetDB.GetCabinetByID(_centerToCenterTranslation.NewCabinetID);
                    if (pastOldCabinet != null)
                    {
                        pastOldCabinet.Status = (int)DB.CabinetStatus.Install;
                        pastOldCabinet.Detach();
                        DB.Save(pastOldCabinet);
                    }

                    Cabinet pastNewCabinet = Data.CabinetDB.GetCabinetByID(_centerToCenterTranslation.OldCabinetID);
                    if (pastNewCabinet != null)
                    {
                        pastNewCabinet.Status = (int)DB.CabinetStatus.Install;
                        pastNewCabinet.Detach();
                        DB.Save(pastNewCabinet);
                    }

                    _centerToCenterTranslation.IsCompletion = true;
                    _centerToCenterTranslation.Detach();
                    DB.Save(_centerToCenterTranslation, false);

                    ts2.Complete();
                }
            }
        }

        #region Load Control
        ComboBox NewPreCodeComboBox;
        private void NewPreCodeComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            NewPreCodeComboBox = sender as ComboBox;
        }

        ComboBox NewTelephonComboBox;
        private void NewTelephonComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            NewTelephonComboBox = sender as ComboBox;
        }
        #endregion

        private void FilterTelephonNoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        public void ApplyFilters()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(TelItemsDataGrid.ItemsSource);
            if (view != null)
            {
                view.Filter = new Predicate<object>(PredicateFilters);
            }
        }

        private bool PredicateFilters(object obj)
        {
            CenterToCenterTranslationChooseNumberInfo checkableObject = obj as CenterToCenterTranslationChooseNumberInfo;
            return checkableObject.TelephonNo.ToString().Contains(FilterTelephonNoTextBox.Text.Trim());
        }

        private void TelItemsDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        public override bool ConfirmEnd()
        {

                worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += new DoWorkEventHandler(WorkerDoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerCompleted);
                worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);

                if (!worker.IsBusy)
                {
                    worker.RunWorkerAsync();
                    this.IsEnabled = false;
                }

            IsConfirmEndSuccess = false;
            return IsConfirmEndSuccess;
        }

        private void Print_Switch(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            ObservableCollection<CenterToCenterTranslationChooseNumberInfo> Result = new ObservableCollection<CenterToCenterTranslationChooseNumberInfo>();
            request = Data.RequestDB.GetRequestByID(this.requestID);
            Status StatusPlace = StatusDB.GetStatusByID(request.StatusID);
            RequestStep requestStep = RequestStepDB.GetRequestStepByID(StatusPlace.RequestStepID);

            if (requestStep.StepTitle.Contains("شبکه هوایی - دایری"))
            {
                _centerToCenterTranslationInfo = new CRM.Application.UserControls.CenterToCenterTranslationInfo(requestID);

                _centerToCenterTranslation = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslation(requestID);


                telphonNumbers = new ObservableCollection<CenterToCenterTranslationChooseNumberInfo>(Data.CenterToCenterTranslationDB.GetTelephones(_centerToCenterTranslation));
                _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_centerToCenterTranslation.TargetCenterID));

                List<CenterToCenterTranslationTelephone> centerToCenterTranslationTelephones = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslationTelephon(requestID).ToList();
                centerToCenterTranslationTelephones.ForEach(item =>
                                                               {
                                                                   if (telphonNumbers.Any(t => t.TelephonNo == item.TelephoneNo))
                                                                   {
                                                                       telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewTelephonNo = item.NewTelephoneNo;
                                                                       telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeID = item.NewSwitchPrecodeID;
                                                                       telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == item.NewSwitchPrecodeID).SingleOrDefault().Name));
                                                                   }
                                                               }
                                                               );


                _oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_centerToCenterTranslation.FromOldCabinetInputID, _centerToCenterTranslation.ToOldCabinetInputID);
                // _oldBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDsOrderByInputNumber(_oldCabinetInputs.Select(t => t.ID).ToList());

                _newCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_centerToCenterTranslation.FromNewCabinetInputID, _centerToCenterTranslation.ToNewCabinetInputID);
                _newBuchtInfoList = Data.BuchtDB.GetBuchtInfoByCabinetInputIDsOrderByInputNumber(_newCabinetInputs.Select(t => t.ID).ToList());


                for (int i = 0; i < _oldCabinetInputs.Count(); i++)
                {

                    if (telphonNumbers.Any(t => t.CabinetInputID == _oldCabinetInputs[i].ID))
                    {
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewCabinetInputID = _newCabinetInputs[i].ID;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewCabinetInputNumber = _newCabinetInputs[i].InputNumber;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewCabinetNumber = CabinetDB.GetCabinetNumberByCabinetInputID(_newCabinetInputs[i].ID);

                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewVerticalCloumnNo = _newBuchtInfoList[i].VerticalColumnNo;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewVerticalRowNo = _newBuchtInfoList[i].VerticalRowNo;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewBuchtNo = _newBuchtInfoList[i].BuchtNo;
                    }

                }


                Result = telphonNumbers;
                SendToPrintDayeri(Result);
            }

            if (requestStep.StepTitle.Contains("شبکه هوایی - تخلیه"))
            {
                _centerToCenterTranslationInfo = new CRM.Application.UserControls.CenterToCenterTranslationInfo(requestID);

                _centerToCenterTranslation = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslation(requestID);


                telphonNumbers = new ObservableCollection<CenterToCenterTranslationChooseNumberInfo>(Data.CenterToCenterTranslationDB.GetTelephones(_centerToCenterTranslation));
                _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_centerToCenterTranslation.SourceCenterID));

                List<CenterToCenterTranslationTelephone> centerToCenterTranslationTelephones = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslationTelephon(requestID).ToList();
                centerToCenterTranslationTelephones.ForEach(item =>
                {
                    if (telphonNumbers.Any(t => t.TelephonNo == item.TelephoneNo))
                    {
                        telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().TelephonNo = item.TelephoneNo;
                    }
                }
                                                               );


                _oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_centerToCenterTranslation.FromOldCabinetInputID, _centerToCenterTranslation.ToOldCabinetInputID);
                _oldBuchtInfoList = Data.BuchtDB.GetBuchtInfoByCabinetInputIDsOrderByInputNumber(_oldCabinetInputs.Select(t => t.ID).ToList());

                for (int i = 0; i < _oldCabinetInputs.Count(); i++)
                {

                    if (telphonNumbers.Any(t => t.CabinetInputID == _oldCabinetInputs[i].ID))
                    {

                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().CabinetInputNumber = _oldCabinetInputs[i].InputNumber;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().CabinetNumber = CabinetDB.GetCabinetNumberByCabinetInputID(_oldCabinetInputs[i].ID);

                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().VerticalCloumnNo = _oldBuchtInfoList[i].VerticalColumnNo;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().VerticalRowNo = _oldBuchtInfoList[i].OldVerticalRowNo;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().BuchtNo = _oldBuchtInfoList[i].BuchtNo;
                    }

                }


                Result = telphonNumbers;
                SendToPrintDischarge(Result);
            }
        }

        private void SendToPrintDayeri(ObservableCollection<CenterToCenterTranslationChooseNumberInfo> Result)
        {

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.CenterToCenterTranslationNetworkingDayeri);
            stiReport.Load(path);
            stiReport.CacheAllData = true; stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintDischarge(ObservableCollection<CenterToCenterTranslationChooseNumberInfo> Result)
        {

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.CenterToCenterTranslationNetworkingDischarge);
            stiReport.Load(path);
            stiReport.CacheAllData = true; stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
    }
}


