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
using System.Collections.ObjectModel;
using CRM.Data;
using System.Transactions;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ExchangeCentralCableMDF.xaml
    /// </summary> 
    public partial class ExchangeCentralCableMDFForm : Local.RequestFormBase
    {
        # region Initialize && Fildes
        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }
        CRM.Data.ExchangeCentralCableMDF _exchangeCentralCableMDF { get; set; }
        Cabinet _pastCabinet { get; set; }
        Cable _pastCable { get; set; }
        Request _reqeust { get; set; }

        List<CabinetInput> _oldCabinetInputs { get; set; }

        List<CablePair> _newCablePairs { get; set; }
        List<CablePair> _oldCablePairs { get; set; }

        List<Bucht> _oldBuchtList { get; set; }
        List<Bucht> _newBuchtList { get; set; }

        private long _requestID;

        private int _RequestType;
        int _centerID = 0;
        private int CityID = 0;
        public ExchangeCentralCableMDFForm()
        {
            InitializeComponent();

        }
        public ExchangeCentralCableMDFForm(int requestType)
            : this()
        {
            this._RequestType = requestType;
            Initialize();
        }
        public ExchangeCentralCableMDFForm(long id)
            : this()
        {

            _requestID = id;
            Initialize();
        }


        public void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
        }
        #endregion

        #region Load
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {

            Load();
        }
        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {

            _centerID = centerID;

            if (_exchangeCentralCableMDF.ID == 0)
            {
                OldCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(_centerID);
                CableComboBox.ItemsSource = Data.CableDB.GetCableCheckableByCenterID(new List<int> { _centerID });
            }
            else
            {
                int requestCenterID = Data.RequestDB.GetCenterIDByRequestID(_exchangeCentralCableMDF.ID);

                if (requestCenterID == _centerID)
                {
                   // OldCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(_centerID).Union(Data.CabinetDB.GetCabinetCheckableByID(_exchangeCentralCableMDF.CabinetID));
                   // CableComboBox.ItemsSource = Data.CableDB.GetCableCheckableByCenterID(new List<int> { _centerID }).Union(Data.CableDB.GetCableCheckableByID(_exchangeCentralCableMDF.CableID));
                }
                else
                {
                    OldCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(_centerID);
                    CableComboBox.ItemsSource = Data.CableDB.GetCableCheckableByCenterID(new List<int> { _centerID });
                }
            }
        }
        private void Load()
        {

            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_requestID);
            _exchangeRequestInfo.RequestType = this._RequestType;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;

            StatusComboBox.ItemsSource = DB.GetStepStatus((int)DB.RequestType.ExchangeCenralCableMDF, this.currentStep);
            StatusComboBox.SelectedValue = this.currentStat;

            if (_requestID == 0)
            {

                _exchangeCentralCableMDF = new ExchangeCentralCableMDF();
                AccomplishmentGroupBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                _reqeust = Data.RequestDB.GetRequestByID(_requestID);
                _exchangeCentralCableMDF = Data.ExchangeCenralCableMDFDB.GetExchangeCentralCableByID(_requestID);

                ExchangeCenralCableMDFTechnicaliInfo exchangeCenralCableMDFTechnicaliInfo = Data.ExchangeCenralCableMDFDB.GetExchangeCenralCableMDFTechnicaliInfo(_exchangeCentralCableMDF);
                FromOldCabinetInputComboBox.ItemsSource = ToOldCabinetInputComboBox.ItemsSource = exchangeCenralCableMDFTechnicaliInfo.CabinetInputs;
                FromCablePairComboBox.ItemsSource = ToCablePairComboBox.ItemsSource = exchangeCenralCableMDFTechnicaliInfo.CablePairs;

                if (Data.StatusDB.IsFinalStep(this.currentStat))
                {
                    AccomplishmentDateLabel.Visibility = Visibility.Visible;
                    AccomplishmentDate.Visibility = Visibility.Visible;

                    AccomplishmentTimeLabel.Visibility = Visibility.Visible;
                    AccomplishmentTime.Visibility = Visibility.Visible;

                    if (_exchangeCentralCableMDF.MDFAccomplishmentTime == null)
                    {
                        DateTime currentDateTime = DB.GetServerDate();
                        _exchangeCentralCableMDF.MDFAccomplishmentTime = currentDateTime.ToShortTimeString();
                        _exchangeCentralCableMDF.MDFAccomplishmentDate = currentDateTime;
                    }

                }


                Status Status = Data.StatusDB.GetStatueByStatusID(_reqeust.StatusID);
                if (Status.StatusType == (byte)DB.RequestStatusType.Start)
                {
                    StatusComboBox.Visibility = Visibility.Collapsed;
                    wiringButtom.Visibility = Visibility.Collapsed;
                    StatusComboBoxLabel.Visibility = Visibility.Collapsed;
                }

               // _pastCabinet = Data.CabinetDB.GetCabinetByID((int)_exchangeCentralCableMDF.CabinetID);
              //  _pastCable = Data.CableDB.GetCableByID((int)_exchangeCentralCableMDF.CableID);

            }
            this.DataContext = _exchangeCentralCableMDF;
        }
        #endregion

        # region save
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
                    _exchangeCentralCableMDF = this.DataContext as ExchangeCentralCableMDF;

                    // Verify data
                    VerifyData(_exchangeCentralCableMDF);

                    Status Status = Data.StatusDB.GetStatueByStatusID(_reqeust.StatusID);
                    _reqeust.RequestPaymentTypeID = 0;
                    _reqeust.IsViewed = false;
                    _reqeust.InsertDate = DB.GetServerDate();

                    if (_requestID == 0)
                    {
                        _reqeust.ID = DB.GenerateRequestID();
                        _reqeust.StatusID = DB.GetStatus(_RequestType, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                        _reqeust.Detach();
                        DB.Save(_reqeust, true);

                        _exchangeCentralCableMDF.ID = _reqeust.ID;
                        _exchangeCentralCableMDF.InsertDate = DB.GetServerDate();
                        _exchangeCentralCableMDF.CompletionDate = DB.GetServerDate();

                        _exchangeCentralCableMDF.Detach();
                        DB.Save(_exchangeCentralCableMDF, true);

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

                        _exchangeCentralCableMDF.Detach();
                        DB.Save(_exchangeCentralCableMDF, false);
                    }
                    if (_exchangeCentralCableMDF.CompletionDate != null)
                    {
                        throw new Exception("در درخواست قبلا اعمال تغییرات صورت گرفته است");
                    }

                    Status = Data.StatusDB.GetStatueByStatusID((int)(StatusComboBox.SelectedValue ?? 0));
                    // اگر در وضعیت تغییرات نیست
                    if (_requestID == 0 || Status.StatusType != (byte)DB.RequestStatusType.Changes)
                    {
                        if (_requestID == 0)
                        {
                            // Reservations the cabinet
                            //Cabinet currentCabinet = Data.CabinetDB.GetCabinetByID(_exchangeCentralCableMDF.CabinetID);
                            //if (currentCabinet != null)
                            //{
                            //    currentCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                            //    currentCabinet.Detach();
                            //    DB.Save(currentCabinet);
                            //}
                            //else
                            //{
                            //    throw new Exception("کافو یافت نشد.");
                            //}

                            // Reservations the cable
                            //Cable currentCable = Data.CableDB.GetCableByID(_exchangeCentralCableMDF.CableID);
                            //if (currentCable != null)
                            //{
                            //    currentCable.Status = (int)DB.CableStatus.Exchange;
                            //    currentCable.Detach();
                            //    DB.Save(currentCable);
                            //}
                            //else
                            //{
                            //    throw new Exception("کابل یافت نشد.");
                            //}
                        }
                        else
                        {
                            _exchangeCentralCableMDF.Detach();
                            DB.Save(_exchangeCentralCableMDF, false);

                            // Out Reservations the past cabinet
                            if (_pastCabinet != null)
                            {
                                _pastCabinet.Status = (int)DB.CabinetStatus.Install;
                                _pastCabinet.Detach();
                                DB.Save(_pastCabinet);
                            }

                            // Out Reservations the past cable
                            if (_pastCable != null)
                            {
                                _pastCable.Status = (int)DB.CableStatus.CableConnection;
                                _pastCable.Detach();
                                DB.Save(_pastCable);
                            }

                            //// Reservations the new cabinet
                            //Cabinet currentCabinet = Data.CabinetDB.GetCabinetByID(_exchangeCentralCableMDF.CabinetID);
                            //if (currentCabinet != null)
                            //{
                            //    currentCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                            //    currentCabinet.Detach();
                            //    DB.Save(currentCabinet);
                            //}
                            //else
                            //{
                            //    throw new Exception("کافو یافت نشد.");
                            //}


                            // Reservations the cable
                         //   Cable currentCable = Data.CableDB.GetCableByID(_exchangeCentralCableMDF.CableID);
                            //if (currentCable != null)
                            //{
                            //    currentCable.Status = (int)DB.CableStatus.Exchange;
                            //    currentCable.Detach();
                            //    DB.Save(currentCable);
                            //}
                            //else
                            //{
                            //    throw new Exception("کابل یافت نشد.");
                            //}

                        }

                    }
                    else if (Status.StatusType == (byte)DB.RequestStatusType.Changes)
                    {

                        _exchangeCentralCableMDF.CompletionDate = DB.GetServerDate();


                        // Out Reservations the past cabinet
                        if (_pastCabinet != null)
                        {
                            _pastCabinet.Status = (int)DB.CabinetStatus.Install;
                            _pastCabinet.Detach();
                            DB.Save(_pastCabinet);
                        }
                        // Out Reservations the past cable
                        if (_pastCable != null)
                        {
                            _pastCable.Status = (int)DB.CableStatus.CableConnection;
                            _pastCable.Detach();
                            DB.Save(_pastCable);
                        }

                        for (int i = 0; i < _oldBuchtList.Count(); i++)
                        {
                            // transform bucht else PCM
                            if (_oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtList[i].Status == (int)DB.BuchtStatus.Connection && _oldBuchtList[i].BuchtIDConnectedOtherBucht == null)
                            {
                                _newBuchtList[i].Status = _oldBuchtList[i].Status;
                                _oldBuchtList[i].Status = (int)DB.BuchtStatus.Free;


                                //_newBuchtList[i].ADSLPortID = _oldBuchtList[i].ADSLPortID;
                                //_oldBuchtList[i].ADSLPortID = null;


                                _newBuchtList[i].ADSLStatus = _oldBuchtList[i].ADSLStatus;
                                _oldBuchtList[i].ADSLStatus = false;

                                //_newBuchtList[i].ADSLType = _oldBuchtList[i].ADSLType;
                                //_oldBuchtList[i].ADSLType = null;


                                _newBuchtList[i].ConnectionID = _oldBuchtList[i].ConnectionID;
                                _oldBuchtList[i].ConnectionID = null;

                                int newBuchtType = _newBuchtList[i].BuchtTypeID;
                                _newBuchtList[i].BuchtTypeID = _oldBuchtList[i].BuchtTypeID;
                                _oldBuchtList[i].BuchtTypeID = newBuchtType;


                                _newBuchtList[i].SwitchPortID = _oldBuchtList[i].SwitchPortID;
                                _oldBuchtList[i].SwitchPortID = null;

                                _oldCablePairs.Where(t => t.CabinetInputID == _oldBuchtList[i].CabinetInputID).SingleOrDefault().CabinetInputID = null;
                                _newCablePairs[i].CabinetInputID = (long)_oldCabinetInputs[i].ID;
                                _newBuchtList[i].CabinetInputID = (long)_oldCabinetInputs[i].ID;
                                _oldBuchtList[i].CabinetInputID = null;

                            }
                            // transform PCM bucht
                            else if (_oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtList[i].Status == (int)DB.BuchtStatus.AllocatedToInlinePCM)
                            {

                                _newBuchtList[i].Status = _oldBuchtList[i].Status;
                                _oldBuchtList[i].Status = (int)DB.BuchtStatus.Free;

                                _newBuchtList[i].BuchtIDConnectedOtherBucht = _oldBuchtList[i].BuchtIDConnectedOtherBucht;
                                _newBuchtList[i].ConnectionID = _oldBuchtList[i].ConnectionID;

                                _oldCablePairs.Where(t => t.CabinetInputID == _oldBuchtList[i].CabinetInputID).SingleOrDefault().CabinetInputID = null;
                                _newCablePairs[i].CabinetInputID = _oldCabinetInputs[i].ID;



                                Bucht outPutBucht = Data.BuchtDB.GetBuchetByID(_oldBuchtList[i].BuchtIDConnectedOtherBucht);
                                outPutBucht.BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                outPutBucht.Detach();
                                DB.Save(outPutBucht, false);
                                if (_oldBuchtList.Any(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht)) _oldBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;

                                _oldBuchtList[i].BuchtIDConnectedOtherBucht = null;

                                _oldBuchtList[i].ConnectionID = null;


                                //List<Bucht> _oldPCMBuchtList  = Data.BuchtDB.GetPCMBuchtByCabinetInputID(_oldBuchtList[i].CabinetInputID ?? 0);
                                //_oldPCMBuchtList.ForEach(item => { item.CabinetInputID = _newBuchtList[i].CabinetInputID; item.Detach(); });
                                //DB.UpdateAll(_oldPCMBuchtList);

                                //  _oldBuchtList.Where(t => t.CabinetInputID == _oldBuchtList[i].CabinetInputID && (t.BuchtTypeID == (int)DB.BuchtType.InLine || t.BuchtTypeID == (int)DB.BuchtType.OutLine)).ToList().ForEach(item => item.CabinetInputID = _newBuchtList[i].CabinetInputID);


                                _newBuchtList[i].CabinetInputID = _oldCabinetInputs[i].ID;
                                _oldBuchtList[i].CabinetInputID = null;


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

                                _oldCablePairs.Where(t => t.CabinetInputID == _oldBuchtList[i].CabinetInputID).SingleOrDefault().CabinetInputID = null;
                                _newCablePairs[i].CabinetInputID = _oldCabinetInputs[i].ID;

                                _newBuchtList[i].CabinetInputID = _oldCabinetInputs[i].ID;
                                _oldBuchtList[i].CabinetInputID = null;
                            }
                            // if bucht is free only change the cabinet input it.
                            else if (_oldBuchtList[i].Status == (int)DB.BuchtStatus.Free)
                            {
                                _oldCablePairs.Where(t => t.CabinetInputID == _oldBuchtList[i].CabinetInputID).SingleOrDefault().CabinetInputID = null;
                                _newCablePairs[i].CabinetInputID = _oldCabinetInputs[i].ID;

                                _newBuchtList[i].CabinetInputID = _oldCabinetInputs[i].ID;
                                _oldBuchtList[i].CabinetInputID = null;
                            }
                        }



                        _oldBuchtList.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldBuchtList);

                        _oldCablePairs.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldCablePairs);


                        _newCablePairs.ForEach(t => t.Detach());
                        DB.UpdateAll(_newCablePairs);

                        _newBuchtList.ForEach(t => t.Detach());
                        DB.UpdateAll(_newBuchtList);


                    }

                    _exchangeCentralCableMDF.Detach();
                    DB.Save(_exchangeCentralCableMDF, false);
                    ts.Complete();
                }
                IsSaveSuccess = true;
                ShowSuccessMessage("برگردان انجام شد");
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("برگردان انجام نشد", ex);
            }

            return IsSaveSuccess;

        }

        private void VerifyData(ExchangeCentralCableMDF exchangeCentralCableMDF)
        {
            
           // _oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(exchangeCentralCableMDF.FromCabinetInputID, exchangeCentralCableMDF.ToCabinetInputID);
           // _oldCablePairs = Data.CablePairDB.GetCablePairByCabinetInputs(_oldCabinetInputs.Select(t => t.ID).ToList());
           // _oldBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(_oldCabinetInputs.Select(t => t.ID).ToList()).Where(t => t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).ToList();

            _oldBuchtList = Data.BuchtDB.GetAllBuchtFromBuchtIDToBuchtID(exchangeCentralCableMDF.FromNewBuchtID, exchangeCentralCableMDF.ToNewBuchtID);
            if (_oldBuchtList.Any(t => t.Status != (int)DB.BuchtStatus.Free &&
                                      t.Status != (int)DB.BuchtStatus.AllocatedToInlinePCM &&
                                      t.Status != (int)DB.BuchtStatus.Connection &&
                                      t.Status != (int)DB.BuchtStatus.Destroy &&
                                      t.BuchtTypeID != (int)DB.BuchtType.InLine &&
                                      t.BuchtTypeID != (int)DB.BuchtType.OutLine))
            {
                throw new Exception("در میان بوخت ها قبل برگردان بوخت رزرو وجود دارد");
            }

            //_newBuchtList = Data.BuchtDB.GetBuchtFromCablePairIDToCablePairID(exchangeCentralCableMDF.FromCablePairID, exchangeCentralCableMDF.ToCablePairID);
            //_newCablePairs = Data.CablePairDB.GetCablePairByCablePairIDToCablePairID(exchangeCentralCableMDF.FromCablePairID, exchangeCentralCableMDF.ToCablePairID);

            if (_newBuchtList.Any(t => t.Status != (int)DB.BuchtStatus.Free))
            {
                throw new Exception("در میان بوخت ها بعداز برگردان بوخت غیر آزاد وجود دارد");
            }

            if (_newBuchtList.Any(t => t.CabinetInputID != null))
            {
                throw new Exception("در میان بوخت ها بعداز برگردان بوخت متصل به مرکزی وجود دارد");
            }


            if (_oldBuchtList.Count() != _newBuchtList.Count()) { throw new Exception("تعداد بوخت های متصل انتخاب شده برابر نمی باشد."); }


        }

        private void wiringButtom_Click(object sender, RoutedEventArgs e)
        {
            Folder.MessageBox.ShowInfo("گزارش در دست تهیه می باشد.");
        }
        #endregion

        #region action
        public override bool Forward()
        {
            if (_exchangeCentralCableMDF.CompletionDate == null)
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
        #endregion action

        #region Events
        private void CableComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CableComboBox.SelectedValue != null)
            {
                FromCablePairComboBox.ItemsSource = ToCablePairComboBox.ItemsSource = Data.CablePairDB.GetCablePairCheckableByCableID((long)CableComboBox.SelectedValue, true);
            }

        }

        private void OldCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OldCabinetComboBox.SelectedValue != null)
            {
                FromOldCabinetInputComboBox.ItemsSource = ToOldCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)OldCabinetComboBox.SelectedValue);
            }
        }
        #endregion Events


    }
}
