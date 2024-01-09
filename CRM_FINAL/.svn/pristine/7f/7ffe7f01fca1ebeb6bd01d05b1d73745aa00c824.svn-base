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
    /// Interaction logic for ExchangePCMCardFrom.xaml
    /// </summary>
    public partial class ExchangePCMCardFrom : Local.RequestFormBase
    {
        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }
        private long _requestID = 0;
        int _centerID = 0;
        Request _reqeust { get; set; }

        List<PCMBuchtTelephonInfo> pCMBuchtTelephonInfo { get; set; }
        List<PCMBuchtTelephonInfo> newPCMBuchtTelephonInfo { get; set; }

        ExchangeBrokenPCM BrokenPCM { get; set; }
        public ExchangePCMCardFrom()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
        }
        public ExchangePCMCardFrom(long requestID)
            : this()
        {
            _requestID = requestID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_requestID);
            _exchangeRequestInfo.RequestType = (int)DB.RequestType.BrokenPCM;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;

            if(_requestID == 0)
            {
                _reqeust = new Request();
            }
            else
            {
                throw new Exception();
            }
        }

        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            if (centerID != null)
            {
                OldRockComboBox.ItemsSource = Data.PCMRockDB.GetPCMRockCheckableByCenterIDs(new List<int>{centerID});
                NewRockComboBox.ItemsSource = Data.PCMRockDB.GetPCMRockCheckableByCenterIDs(new List<int>{centerID});
            }
        }


        #region Old PCM
        private void OldRockComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OldRockComboBox.SelectedValue != null)
            {
                OldShelfComboBox.ItemsSource = Data.PCMShelfDB.GetPCMShelfByRockID((int)OldRockComboBox.SelectedValue).Select(t => new CheckableItem { ID = t.ID, Name = t.Number.ToString(), IsChecked = false }).ToList();
            }
        }

        private void OldShelfComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(OldShelfComboBox.SelectedValue != null)
            {
                OldCardComboBox.ItemsSource = Data.PCMDB.GetPCMByShelfID((int)OldShelfComboBox.SelectedValue, (int)DB.PCMStatus.Connection);
            }
        }

        #endregion


        #region New PCM
        private void NewRockComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewRockComboBox.SelectedValue != null)
            {
                NewShelfComboBox.ItemsSource = Data.PCMShelfDB.GetPCMShelfByRockID((int)NewRockComboBox.SelectedValue).Select(t => new CheckableItem { ID = t.ID, Name = t.Number.ToString(), IsChecked = false }).ToList();
            }
        }

        private void NewShelfComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewShelfComboBox.SelectedValue != null)
            {
                NewCardComboBox.ItemsSource = Data.PCMDB.GetPCMByShelfID((int)NewShelfComboBox.SelectedValue  , (int)DB.PCMStatus.Install);
            }
        }

        #endregion Old PCM


        public override bool Forward()
        {
            try
            {
                this.RequestID = _requestID;
                Validation();
                using (TransactionScope ts1 = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(5)))
                {
                    _reqeust = _exchangeRequestInfo.Request;

                    if (_requestID == 0)
                    {
                        _reqeust.ID = DB.GenerateRequestID();
                        _reqeust.RequestPaymentTypeID = 0;
                        _reqeust.IsViewed = false;
                        _reqeust.InsertDate = DB.GetServerDate();
                        _reqeust.StatusID = DB.GetStatus((int)DB.RequestType.BrokenPCM, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                        _reqeust.Detach();
                        DB.Save(_reqeust, true);


                        List<ExchangeBrokenPCM> BrokenPCMs = new List<Data.ExchangeBrokenPCM>();

                        pCMBuchtTelephonInfo.Where(t => t.Bucht.BuchtTypeID == (int)DB.BuchtType.InLine).ToList().ForEach(t =>
                        {
                            BrokenPCMs.Add(new ExchangeBrokenPCM 
                            {
                                 ID = 0,
                                 RequestID = _reqeust.ID,
                                 OldBuchtID = t.Bucht.ID,
                                 OldBuchtStatus = t.Bucht.Status,
                                 OldPostContactStatus = t.PostContact.Status,
                                 TelephoneNo = (t.Telephone == null) ? default(long?) : t.Telephone.TelephoneNo,
                                 OldPCMID = t.PCMID,
                                 NewPCMID = (int)NewCardComboBox.SelectedValue,
                                 NewBuchtID = newPCMBuchtTelephonInfo.Where(x1 => x1.Bucht.BuchtTypeID == (int)DB.BuchtType.InLine && x1.PortNo == t.PortNo).SingleOrDefault().Bucht.ID,
                            });
                        });


                        BrokenPCMs.Add(new ExchangeBrokenPCM
                        {
                            ID = 0,
                            RequestID = _reqeust.ID,
                            OldBuchtID = pCMBuchtTelephonInfo.Where(t => t.Bucht.BuchtTypeID == (int)DB.BuchtType.OutLine).Take(1).SingleOrDefault().Bucht.ID,
                            OldBuchtStatus = pCMBuchtTelephonInfo.Where(t => t.Bucht.BuchtTypeID == (int)DB.BuchtType.OutLine).Take(1).SingleOrDefault().Bucht.Status,
                            OldPostContactStatus = pCMBuchtTelephonInfo.Where(t => t.Bucht.BuchtTypeID == (int)DB.BuchtType.OutLine).Take(1).SingleOrDefault().PostContact.Status,
                            TelephoneNo =  default(long?),
                            OldPCMID = pCMBuchtTelephonInfo.Where(t => t.Bucht.BuchtTypeID == (int)DB.BuchtType.OutLine).Take(1).SingleOrDefault().PCMID,
                            NewPCMID = (int)NewCardComboBox.SelectedValue,
                            NewBuchtID = newPCMBuchtTelephonInfo.Where(t => t.Bucht.BuchtTypeID == (int)DB.BuchtType.OutLine).Take(1).SingleOrDefault().Bucht.ID
                        });

                        DB.SaveAll(BrokenPCMs);

                        PCM oldPCM = PCMDB.GetPCMByID(BrokenPCMs.Take(1).SingleOrDefault().OldPCMID);
                        oldPCM.Status = (int)DB.PCMStatus.Reserve;
                        oldPCM.Detach();
                        DB.Save(oldPCM);

                        PCM newPCM = PCMDB.GetPCMByID(BrokenPCMs.Take(1).SingleOrDefault().NewPCMID);
                        newPCM.Status = (int)DB.PCMStatus.Reserve;
                        newPCM.Detach();
                        DB.Save(newPCM);


                        List<Bucht> updateBucht = pCMBuchtTelephonInfo.Where(t => t.Bucht.BuchtTypeID == (int)DB.BuchtType.InLine).Select(t => t.Bucht).ToList();
                        updateBucht.ForEach(t => { t.Status = (int)DB.BuchtStatus.Reserve; t.Detach(); });
                        DB.UpdateAll(updateBucht);


                        List<PostContact> updatePostContact = pCMBuchtTelephonInfo.Where(t => t.PostContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal).Select(t => t.PostContact).ToList();
                        updatePostContact.ForEach(t => { t.Status = (int)DB.PostContactStatus.FullBooking; t.Detach(); });
                        DB.UpdateAll(updatePostContact);

                        _requestID = _reqeust.ID;
                        this.RequestID = _reqeust.ID;
                    }
                    else
                    {
                        throw new Exception();
                    }

                    IsForwardSuccess = true;
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

        private void Validation()
        {
            string errorMessage = string.Empty;

            if (OldCardComboBox.SelectedValue == null)
                errorMessage += "لطفا کارت خراب را انتخاب کنید/n";

            if (NewCardComboBox.SelectedValue == null)
                errorMessage += "لطفا کارت جدید را انتخاب کنید/n";

            pCMBuchtTelephonInfo = PCMDB.GetAllTelephoneByPCMID((int)OldCardComboBox.SelectedValue);
           // pCMBuchtTelephonInfo.ForEach(t => { System.Diagnostics.Debug.WriteLine(t.Bucht.ID); System.Diagnostics.Debug.WriteLine(t.Bucht.BuchtTypeID); });
            newPCMBuchtTelephonInfo = PCMDB.GetAllTelephoneByPCMID((int)NewCardComboBox.SelectedValue);
           // newPCMBuchtTelephonInfo.ForEach(t => { System.Diagnostics.Debug.WriteLine(t.Bucht.ID); System.Diagnostics.Debug.WriteLine(t.Bucht.BuchtTypeID); });
            bool inWaitingList = false;
            string requestName = Data.RequestDB.GetOpenRequestNameTelephone(pCMBuchtTelephonInfo.Where(t=>t.Telephone != null).Select(t=>t.Telephone.TelephoneNo).ToList(), out inWaitingList);
            if (!string.IsNullOrEmpty(requestName))
            {
                  errorMessage += "این تلفن در روال " + requestName + " در حال پیگیری می باشد.";
            }

            if (pCMBuchtTelephonInfo.Where(t=>t.Bucht.SwitchPortID != null && t.Bucht.Status != (int)DB.BuchtStatus.Connection).Any())
                errorMessage += string.Format("بوخت متصل به تلفن ها در وضعیت متصل نمی باشند");



            if (pCMBuchtTelephonInfo.Any(t => !(t.Bucht.Status == (int)DB.BuchtStatus.Connection || t.Bucht.Status == (int)DB.BuchtStatus.Free || t.Bucht.Status == (int)DB.BuchtStatus.ConnectedToPCM)))
                errorMessage += string.Format("وضعیت بوخت رزرو می باشد");



            if (pCMBuchtTelephonInfo.Where(t => t.Bucht != null).Count() != newPCMBuchtTelephonInfo.Where(t => t.Bucht != null).Count())
                errorMessage += string.Format("تعداد بوخت ها برابر نمی باشد");


            if (!string.IsNullOrEmpty(errorMessage))
                throw new Exception(errorMessage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PCMForm windows = new PCMForm();
            windows.ShowDialog();
        }

    }
}
