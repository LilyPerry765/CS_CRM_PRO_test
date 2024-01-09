using CRM.Application.Reports.Viewer;
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
    /// Interaction logic for CenterToCenterTranslationForm.xaml
    /// </summary>
    public partial class CenterToCenterTranslationForm : Local.RequestFormBase
    {
        private long ID = 0;
        private int _RequestType;
        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }
        CenterToCenterTranslation _centerToCenterTranslation { get; set; }
        int _centerID = 0;
        Request _reqeust { get; set; }
        int pastNewCabinetID = 0;
        int pastOldCabinetID = 0;

        List<CabinetInput> _newCabinetInputs { get; set; }
        List<CabinetInput> _oldCabinetInputs { get; set; }

        List<Bucht> _oldBuchtList { get; set; }
        List<Bucht> _newBuchtList { get; set; }

        List<Post> _oldPosts { get; set; }
        List<Post> _newPosts { get; set; }
        public ObservableCollection<CheckableItem> _NewPCM { get; set; }
        List<CenterToCenterTranslationPCMInfo> centerToCenterTranslationPCM { get; set; }

        public CenterToCenterTranslationForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }

        public CenterToCenterTranslationForm(long ID)
            : this()
        {
            this.ID = ID;
        }
        public CenterToCenterTranslationForm(int requestTypeID)
            : this()
        {
            this._RequestType = requestTypeID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(ID);
            _exchangeRequestInfo.RequestType = this._RequestType;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;

            //   StatusComboBox.ItemsSource = DB.GetStepStatus((int)DB.RequestType.ExchangeCabinetInputID, this.currentStep);
            //   StatusComboBox.SelectedValue = this.currentStat;


            if (ID == 0)
            {
                _centerToCenterTranslation = new CenterToCenterTranslation();

                //   AccomplishmentGroupBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                _centerToCenterTranslation = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslation(ID);
                _reqeust = Data.RequestDB.GetRequestByID(ID);



                pastNewCabinetID = _centerToCenterTranslation.NewCabinetID;
                pastOldCabinetID = _centerToCenterTranslation.OldCabinetID;

                OldCabinetComboBox.SelectedValue = _centerToCenterTranslation.OldCabinetID;
                OldCabinetComboBox_SelectionChanged(null, null);


                TargetCenterComboBox.SelectedValue = _centerToCenterTranslation.TargetCenterID;
                TargetCenterComboBox_SelectionChanged(null, null);


                NewCabinetComboBox.SelectedValue = _centerToCenterTranslation.NewCabinetID;
                NewCabinetComboBox_SelectionChanged(null, null);



                if (Data.StatusDB.IsFinalStep(this.currentStat))
                {
                    //    AccomplishmentDateLabel.Visibility = Visibility.Visible;
                    //    AccomplishmentDate.Visibility = Visibility.Visible;

                    //  AccomplishmentTimeLabel.Visibility = Visibility.Visible;
                    //   AccomplishmentTime.Visibility = Visibility.Visible;

                    ExchangeRequestInfoUserControl.IsEnabled = false;
                    OldCabinetGroupBox.IsEnabled = false;
                    NewCabinetGroupBox.IsEnabled = false;

                    //   StatusComboBox.ItemsSource = DB.GetStepStatus(_reqeust.RequestTypeID, this.currentStep);
                    //   StatusComboBox.SelectedValue = _reqeust.StatusID;

                    if (_centerToCenterTranslation.AccomplishmentTime == null)
                    {
                        DateTime currentDateTime = DB.GetServerDate();
                        _centerToCenterTranslation.AccomplishmentTime = currentDateTime.ToShortTimeString();
                        _centerToCenterTranslation.AccomplishmentDate = currentDateTime;
                    }
                }

                //if (Data.StatusDB.IsStartStep(this.currentStat))
                //{
                //    AccomplishmentGroupBox.Visibility = Visibility.Collapsed;
                //}
            }

            this.DataContext = _centerToCenterTranslation;
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
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (Data.StatusDB.IsFinalStep(this.currentStat))
                    {
                        // _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                        _reqeust.Detach();
                        DB.Save(_reqeust, false);

                    }
                    else
                    {
                        _reqeust = _exchangeRequestInfo.Request;
                        _centerToCenterTranslation = this.DataContext as CenterToCenterTranslation;

                        // Verify data
                        VerifyData(_centerToCenterTranslation);

                        Status Status = Data.StatusDB.GetStatueByStatusID(_reqeust.StatusID);

                        // Save reqeust
                        if (ID == 0)
                        {
                            _reqeust.ID = DB.GenerateRequestID();
                            _reqeust.RequestPaymentTypeID = 0;
                            _reqeust.IsViewed = false;
                            _reqeust.InsertDate = DB.GetServerDate();
                            _reqeust.StatusID = DB.GetStatus(_RequestType, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                            _reqeust.Detach();
                            DB.Save(_reqeust, true);

                            _centerToCenterTranslation.ID = _reqeust.ID;
                            _centerToCenterTranslation.InsertDate = DB.GetServerDate();
                            _centerToCenterTranslation.SourceCenterID = _reqeust.CenterID;
                            _centerToCenterTranslation.TargetCenterID = (int)TargetCenterComboBox.SelectedValue;
                            _centerToCenterTranslation.IsCompletion = false;

                            _centerToCenterTranslation.Detach();
                            DB.Save(_centerToCenterTranslation, true);
                        }
                        else
                        {
                            //  if (StatusComboBox.SelectedValue == null) throw new Exception("وضعیت انتخاب نشده است");
                            //  _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                            _reqeust.Detach();
                            DB.Save(_reqeust, false);

                        }

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


                        Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(_centerToCenterTranslation.OldCabinetID);
                        oldCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                        oldCabinet.Detach();

                        Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(_centerToCenterTranslation.NewCabinetID);
                        newCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                        newCabinet.Detach();

                        DB.UpdateAll(new List<Cabinet> { oldCabinet, newCabinet });


                    }
                    ts.Complete();
                }



                IsSaveSuccess = true;

                ShowSuccessMessage("دخیره اطلاعات انجام شد");

            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            return IsSaveSuccess;
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

        private void CabinetReserve(CenterToCenterTranslation centerToCenterTranslation, int pastNewCabinetID, int pastOldCabinetID)
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


            Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(centerToCenterTranslation.OldCabinetID);
            oldCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
            oldCabinet.Detach();

            Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(centerToCenterTranslation.NewCabinetID);
            newCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
            newCabinet.Detach();

            DB.UpdateAll(new List<Cabinet> { oldCabinet, newCabinet });
        }


        private void VerifyData(CenterToCenterTranslation centerToCenterTranslation)
        {
            Cabinet OldCabinet = Data.CabinetDB.GetCabinetByID(centerToCenterTranslation.OldCabinetID);
            Cabinet NewCabinet = Data.CabinetDB.GetCabinetByID(centerToCenterTranslation.NewCabinetID);

            if ((OldCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet && NewCabinet.CabinetUsageType != (byte)DB.CabinetUsageType.OpticalCabinet)
                 || (NewCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet && OldCabinet.CabinetUsageType != (byte)DB.CabinetUsageType.OpticalCabinet))
                throw new Exception("امکان برگردان از کافو نوری به کافو غیر از نوری نیست");



            // number new cabinetInput
            _oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(centerToCenterTranslation.FromOldCabinetInputID, centerToCenterTranslation.ToOldCabinetInputID);
            _oldBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(_oldCabinetInputs.Select(t => t.ID).ToList());

            List<Bucht> otherBuchts = _oldBuchtList.Where(t => t.BuchtIDConnectedOtherBucht != null && t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).ToList();

            otherBuchts.ForEach(item => { if (Data.ExchangeCabinetInputDB.IsSpecialWire(item.ID))throw new Exception("در میان بوخت ها بوخت متصل به سیم خصوصی وجود دارد، سیم خصوصی را ابتدا از طریق تغییر مکان انتقال دهید."); });

            if (_oldBuchtList.Any(t => t.Status != (int)DB.BuchtStatus.Free &&
                                      t.Status != (int)DB.BuchtStatus.AllocatedToInlinePCM &&
                                      t.Status != (int)DB.BuchtStatus.Connection &&
                                      t.Status != (int)DB.BuchtStatus.Destroy &&
                                      t.BuchtTypeID != (int)DB.BuchtType.InLine &&
                                      t.BuchtTypeID != (int)DB.BuchtType.OutLine))
            {
                throw new Exception("از میان بوخت ها بوخت غیر آزاد یا متصل وجود دارد");
            }



            if (Data.CenterToCenterTranslationDB.CheckExistPCM(_centerToCenterTranslation))
            {
                if (centerToCenterTranslationPCM.Any(t => t.NewPCMID == 0))
                    throw new Exception("لطفا اطلاعات پی سی ام های جدید را کامل وارد کنید.");

                if (centerToCenterTranslationPCM.GroupBy(t => t.NewPCMID).Any(t => t.Count() > 1))
                    throw new Exception("پی سی ام تکراری نمی توان وارد کرد.");

            }

            // number old cabinetInput
            _newCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(centerToCenterTranslation.FromNewCabinetInputID, centerToCenterTranslation.ToNewCabinetInputID);
            _newBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(_newCabinetInputs.Select(t => t.ID).ToList());

            if (_newCabinetInputs.Count() != _oldCabinetInputs.Count()) { throw new Exception("تعداد مرکزی های انتخاب شده برابر نمی باشد."); }

            if (_oldBuchtList.Where(t => t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).Count() != _newBuchtList.Count()) { throw new Exception("تعداد بوخت های متصل انتخاب شده برابر نمی باشد."); }

            if (_newBuchtList.Any(t => t.Status != (Byte)DB.BuchtStatus.Free)) { throw new Exception("همه بوخت های متصل به ورودی های بعد از برگردان انتخاب شد در وضعیت آزاد قرار ندارند."); }

            _oldPosts = Data.PostDB.GetAllPostsByPostContactList(_oldBuchtList.Where(t => t.ConnectionID != null).Select(t => t.ConnectionID ?? 0).ToList());

            if (_oldPosts.Count() == 0) throw new Exception("هیچ پستی در کافو قدیم یافت نشد"); 

            List<Bucht> AllBuchtConnectToOldPosts = Data.BuchtDB.GetBuchtByPostIDs(_oldPosts.Select(t => t.ID).ToList());
            if (AllBuchtConnectToOldPosts.Any(t => !_oldBuchtList.Select(t2 => t2.ID).Contains(t.ID))) { throw new Exception("از میان اتصالی  پست های متصل به مرکزی های انتخاب شده اتصالی متصل به خارج از محدوده انتخاب شده وجود دارد"); }

            _newPosts = Data.PostDB.GetTheNumberPostByStartID(NewCabinet.ID, (int)_oldPosts.OrderBy(t => t.Number).Take(1).SingleOrDefault().ID, _oldPosts.Count());

            if (_newPosts.Count() != _oldPosts.Count) { throw new Exception("تعداد پست های جدید برابر تعداد پست های قبل برگردان نمی باشد."); };


            if (_oldPosts.Any(t => !_newPosts.Select(t2 => t2.Number).ToList().Contains(t.Number)))
                throw new Exception("همه پست های کافو قدیم در کافو جدید نمی باشد.");

            _oldPosts.ForEach(item => { if (!CenterToCenterTranslationDB.CheckEqualityPostContact(item, _newPosts.Where(t => t.Number == item.Number).SingleOrDefault())) throw new Exception("اتصالی های پست ها برابر نمی باشند"); });



            List<PostContact> resultPostContactsNewPost = Data.PostContactDB.GetPostContactByStatus(_newPosts, new List<byte> { (byte)DB.PostContactStatus.Free }, false);
            if (resultPostContactsNewPost.Count() > 0)
                throw new Exception("اتصالی های پست های جدید شامل اتصالی غیر ازاد می باشد");

            //  List<Post> allPostsInNewCabinet = Data.PostDB.GetAllpostInCabinet(_newCabinetInputs.Take(1).SingleOrDefault().CabinetID);
            // if (_oldPosts.Any(t => allPostsInNewCabinet.Select(p => p.Number).Contains(t.Number))) { throw new Exception("پست با شماره انتخاب شده در کافو جدید موجود می باشد."); }


            List<PostContact> resultPostContactsOldPost = Data.PostContactDB.GetPostContactByStatus(_oldPosts, new List<byte> { (byte)DB.PostContactStatus.CableConnection, (byte)DB.PostContactStatus.NoCableConnection, (byte)DB.PostContactStatus.Free, (byte)DB.PostContactStatus.PermanentBroken }, false);

            if (resultPostContactsOldPost.Count() > 0)
            {
                string errorPostContactStatus = string.Empty;

                resultPostContactsOldPost.ForEach(item => { errorPostContactStatus = errorPostContactStatus + Helper.GetEnumDescriptionByValue(typeof(DB.PostContactStatus), item.Status) + " "; });

                throw new Exception("وضعیت های " + errorPostContactStatus + " قابل برگردان نیستن");
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
            }
        }

        private void wiringButtom_Click(object sender, RoutedEventArgs e)
        {

        }



        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            _centerID = centerID;

            OldCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(centerID);

            TargetCenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(Data.CityDB.GetCityByCenterID(_centerID).ID);

            if (_centerToCenterTranslation.ID == 0)
            {
                OldCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(centerID);
            }
            else
            {

                int requestCenterID = Data.RequestDB.GetCenterIDByRequestID(_centerToCenterTranslation.ID);

                if (requestCenterID == _centerID)
                {
                    OldCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(centerID).Union(new List<CheckableItem> { Data.CabinetDB.GetCheckableItemCabinetByID(_centerToCenterTranslation.OldCabinetID) });
                }
                else
                {
                    OldCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(centerID);
                }
            }
        }

        private void TargetCenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TargetCenterComboBox.SelectedValue != null)
            {

                NewPCMComboBoxColumn.ItemsSource = new ObservableCollection<CheckableItem>(Data.PCMDB.GetPCMCheckable((int)TargetCenterComboBox.SelectedValue, (byte)DB.PCMStatus.Install));

                if (_centerToCenterTranslation.ID == 0)
                {
                    NewCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID((int)TargetCenterComboBox.SelectedValue);
                }
                else
                {
                    if (_centerToCenterTranslation.TargetCenterID == (int)TargetCenterComboBox.SelectedValue)
                    {
                        NewCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID((int)TargetCenterComboBox.SelectedValue).Union(new List<CheckableItem> { Data.CabinetDB.GetCheckableItemCabinetByID(_centerToCenterTranslation.NewCabinetID) });
                    }
                    else
                    {
                        NewCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID((int)TargetCenterComboBox.SelectedValue);
                    }
                }
            }
        }

        public override bool Forward()
        {

            using (TransactionScope ts1 = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                Save();
                this.RequestID = _reqeust.ID;
                if (IsSaveSuccess)
                {

                    IsForwardSuccess = true;
                    if (Data.StatusDB.IsStartStep(_reqeust.StatusID))
                    {
                        List<CenterToCenterTranslationPCM> centerToCenterTranslationPCMs = new List<CenterToCenterTranslationPCM>();
                        if (centerToCenterTranslationPCM != null && centerToCenterTranslationPCM.Count > 0)
                        {

                            List<PCM> pcms = Data.PCMDB.GetPCMByIDs(centerToCenterTranslationPCM.Select(t => t.NewPCMID).ToList());
                            pcms.ForEach(item => { item.Status = (int)DB.PCMStatus.Reserve; item.Detach(); });
                            DB.UpdateAll(pcms);

                            pcms = Data.PCMDB.GetPCMByIDs(centerToCenterTranslationPCM.Select(t => t.OldPCMID).ToList());
                            pcms.ForEach(item => { item.Status = (int)DB.PCMStatus.Reserve; item.Detach(); });
                            DB.UpdateAll(pcms);

                            centerToCenterTranslationPCM.ForEach(item =>
                            {
                                CenterToCenterTranslationPCM centerToCenterTranslationPCMItem = new CenterToCenterTranslationPCM();
                                centerToCenterTranslationPCMItem.RequestID = _reqeust.ID;
                                centerToCenterTranslationPCMItem.NewPCMID = item.NewPCMID;
                                centerToCenterTranslationPCMItem.OldPCMID = item.OldPCMID;
                                centerToCenterTranslationPCMs.Add(centerToCenterTranslationPCMItem);
                            });

                            DB.SaveAll(centerToCenterTranslationPCMs);

                        }

                        // change request center 
                        _reqeust.CenterID = _centerToCenterTranslation.TargetCenterID;
                        _reqeust.Detach();
                        DB.Save(_reqeust, false);

                        //
                    }
                  }
                

                ts1.Complete();
            }
            return IsForwardSuccess;
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }



        private void ToOldCabinetInputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            _centerToCenterTranslation = this.DataContext as CenterToCenterTranslation;

            if (Data.CenterToCenterTranslationDB.CheckExistPCM(_centerToCenterTranslation))
            {
                PCMGroupBox.Visibility = Visibility.Visible;
                centerToCenterTranslationPCM = Data.CenterToCenterTranslationDB.GetCabinetInputsPCM(_centerToCenterTranslation);
                DataGrid.ItemsSource = centerToCenterTranslationPCM;

            }
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                CenterToCenterTranslationPCMInfo item = DataGrid.SelectedItem as CenterToCenterTranslationPCMInfo;
                var newPCMItem = new List<CheckableItem>(NewPCMComboBoxColumn.ItemsSource as ObservableCollection<CheckableItem>).Where(t => t.ID == item.NewPCMID).SingleOrDefault();
                if (newPCMItem != null && newPCMItem.Description != item.OldPCMTypeID.ToString())
                {
                    (DataGrid.SelectedItem as CenterToCenterTranslationPCM).NewPCMID = 0;
                }
            }
        }

        public override bool Print()
        {
            List<CenterToCenterTranslationChooseNumberInfo> Result = ReportDB.GetCenterToCenterTranslationCustomerInfo(_reqeust.ID);
            SendToPrint(Result);
            return true;
        }

        private void SendToPrint(List<CenterToCenterTranslationChooseNumberInfo> Result)
        {

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.CenterToCenterTranslationPrintCertification);
            stiReport.Load(path);
            stiReport.CacheAllData = true; stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

    }

}
