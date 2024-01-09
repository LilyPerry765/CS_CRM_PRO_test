using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TranslationCabinetMDFFrom.xaml
    /// </summary>
    public partial class TranslationCentralCableMDFForMDFForm : Local.RequestFormBase
    {
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        CRM.Application.UserControls.TranslationCentralCableMDFInfo _translationCentralCableMDFInfo;
        private Request request { get; set; }
        Data.ExchangeCentralCableMDF _exchangeCentralCableMDF { get; set; }
        private long _requestID = 0;

        BackgroundWorker worker;
        Cabinet _pastCabinet { get; set; }
        Cable _pastCable { get; set; }

        List<CabinetInput> _oldCabinetInputs { get; set; }

        List<CablePair> _newCablePairs { get; set; }
        List<CablePair> _oldCablePairs { get; set; }

        List<Bucht> _oldBuchtList { get; set; }
        List<Bucht> _oldBuchtListWithoutPCM { get; set; }
        List<Bucht> _oldBuchtListWithPCM { get; set; }

        List<Bucht> _newBuchtList { get; set; }

        ObservableCollection<TranslationCentralCabinetInfo> cabinetInputsList;

        List<ExchangeCentralCableMDFConncetion> exchangeConncetion = new List<ExchangeCentralCableMDFConncetion>();

        public TranslationCentralCableMDFForMDFForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }
        public TranslationCentralCableMDFForMDFForm(long requestID) : this()
        {
            this._requestID = requestID;
        }

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }


        private void LoadData()
        {
            _translationCentralCableMDFInfo = new CRM.Application.UserControls.TranslationCentralCableMDFInfo(_requestID);

            _exchangeCentralCableMDF = Data.TranslationCentralCableMDFDB.GetTranslationVentralCableMDFByID(_requestID);
            request = Data.RequestDB.GetRequestByID(_requestID);
          //  _pastCabinet = Data.CabinetDB.GetCabinetByID((int)_exchangeCentralCableMDF.CabinetID);
//
            //StatusComboBox.ItemsSource = DB.GetStepStatus(request.RequestTypeID, this.currentStep);
            //StatusComboBox.SelectedValue = request.StatusID;

            if (_exchangeCentralCableMDF.MDFAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _exchangeCentralCableMDF.MDFAccomplishmentDate = dateTime.Date;
                _exchangeCentralCableMDF.MDFAccomplishmentTime = dateTime.ToShortTimeString();
            }


            cabinetInputsList = new ObservableCollection<TranslationCentralCabinetInfo>(Data.ExchangeCenralCableMDFDB.GetExchangeInfo(_exchangeCentralCableMDF));
            TelItemsDataGrid.DataContext = cabinetInputsList;

            AccomplishmentGroupBox.DataContext = _exchangeCentralCableMDF;
        }


        public override bool Save()
        {
            try
            {
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {
                   // request.StatusID = (int)StatusComboBox.SelectedValue;
                    request.Detach();
                    DB.Save(request, false);


                    _exchangeCentralCableMDF.Detach();
                    DB.Save(_exchangeCentralCableMDF, false);

                    ts2.Complete();
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
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(5)))
                {
                    Save();
                    this.RequestID = _requestID;
                    if (IsSaveSuccess)
                    {
                        DoWork();
                        IsForwardSuccess = true;
                    }
                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);

            }
            return IsForwardSuccess;

        }
         
        public override bool Deny()
        {
            try
            {
                base.RequestID = _requestID;
                if (_exchangeCentralCableMDF.CompletionDate == null)
                {
                    IsRejectSuccess = true;
                }
                else
                {
                    IsRejectSuccess = false;
                    Folder.MessageBox.ShowWarning("بعد از تایید نهایی امکان رد درخواست نمی باشد.");
                }
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("خطا در رد درخواست", ex);
            }

            return IsRejectSuccess;

        }

        private void DoWork()
        {
            // در این برگردان بوختهای متصل به یک کابل را از سمت ام دی اف جدا کرده و به محدوده دیگر از بوخت ها که از کابل آزاد هستن متصل می کند
            // در این حالت مرکزی و کابل تغییر نمی کند
            try
            {
                if (_exchangeCentralCableMDF.CompletionDate == null)
                {

                    using (TransactionScope ts2 = new TransactionScope())
                    {

                        _oldBuchtList = Data.BuchtDB.GetBuchtByIDs(cabinetInputsList.Select(t => (long)t.OldBuchtID).ToList());
                        _oldBuchtListWithPCM = _oldBuchtList.Where(t => t.BuchtTypeID == (int)DB.BuchtType.InLine || t.BuchtTypeID == (int)DB.BuchtType.OutLine).ToList();
                        _oldBuchtListWithoutPCM = _oldBuchtList.Where(t => t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).OrderBy(t=>t.BuchtNo).ToList();
                        exchangeConncetion = Data.ExchangeCenralCableMDFDB.GetExchangeCentralCableConnectionByID(_requestID);
                        _newBuchtList = Data.BuchtDB.GetBuchtByIDs(cabinetInputsList.Select(t => (long)t.NewBuchtID).ToList());

                        int count = _oldBuchtListWithoutPCM.Count();
                        for (int i = 0; i < count; i++)
                        {
                            // transform bucht else PCM
                            if (_oldBuchtListWithoutPCM[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtListWithoutPCM[i].Status != (int)DB.BuchtStatus.AllocatedToInlinePCM && _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht == null)
                            {
                                _newBuchtList[i].Status = exchangeConncetion.Where(t3 => t3.OldBuchtID == _oldBuchtListWithoutPCM[i].ID).SingleOrDefault().OldStatusID;
                                _oldBuchtListWithoutPCM[i].Status = (int)DB.BuchtStatus.Free;

                                _newBuchtList[i].CablePairID = _oldBuchtListWithoutPCM[i].CablePairID;
                                _oldBuchtListWithoutPCM[i].CablePairID = null;

                                _newBuchtList[i].CabinetInputID = _oldBuchtListWithoutPCM[i].CabinetInputID;
                                _oldBuchtListWithoutPCM[i].CabinetInputID = null;

                                _newBuchtList[i].ConnectionID = _oldBuchtListWithoutPCM[i].ConnectionID;
                                _oldBuchtListWithoutPCM[i].ConnectionID = null;

                                _newBuchtList[i].SwitchPortID = _oldBuchtListWithoutPCM[i].SwitchPortID;
                                _oldBuchtListWithoutPCM[i].SwitchPortID = null;
                            }
                            // transform PCM bucht
                            else if (_oldBuchtListWithoutPCM[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtListWithoutPCM[i].Status == (int)DB.BuchtStatus.AllocatedToInlinePCM)
                            {
                                _newBuchtList[i].Status = exchangeConncetion.Where(t3 => t3.OldBuchtID == _oldBuchtListWithoutPCM[i].ID).SingleOrDefault().OldStatusID;
                                _oldBuchtListWithoutPCM[i].Status = (int)DB.BuchtStatus.Free;

                                _oldBuchtListWithPCM.Where(t => t.ID == _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht).SingleOrDefault().BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;

                                _newBuchtList[i].BuchtIDConnectedOtherBucht = _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht;
                                _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht = null;

                                _newBuchtList[i].ConnectionID = _oldBuchtListWithoutPCM[i].ConnectionID;
                                _oldBuchtListWithoutPCM[i].ConnectionID = null;


                                _newBuchtList[i].CabinetInputID = _oldBuchtListWithoutPCM[i].CabinetInputID;
                                _oldBuchtListWithoutPCM[i].CabinetInputID = null;

                                _newBuchtList[i].CablePairID = _oldBuchtListWithoutPCM[i].CablePairID;
                                _oldBuchtListWithoutPCM[i].CablePairID = null;

                                _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht = null;
                                _oldBuchtListWithoutPCM[i].ConnectionID = null;

                                _oldBuchtListWithPCM.Where(t => t.CabinetInputID == _oldBuchtListWithoutPCM[i].CabinetInputID).ToList().ForEach(t => t.CabinetInputID = _newBuchtList[i].CabinetInputID);

                            }
                            else if (_oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtList[i].Status == (int)DB.BuchtStatus.Connection && _oldBuchtList[i].BuchtIDConnectedOtherBucht != null && Data.ExchangeCabinetInputDB.IsSpecialWire(_oldBuchtList[i].ID))
                            {
                                _newBuchtList[i].Status = _oldBuchtList[i].Status;
                                _oldBuchtList[i].Status = (int)DB.BuchtStatus.Free;

                                _newBuchtList[i].ConnectionID = _oldBuchtList[i].ConnectionID;
                                _oldBuchtList[i].ConnectionID = null;


                                Bucht buchtPrivateWire = Data.BuchtDB.GetBuchetByID(_oldBuchtList[i].BuchtIDConnectedOtherBucht);
                                buchtPrivateWire.BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                if (_oldBuchtList.Any(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht)) _oldBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                if (_newBuchtList.Any(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht)) _newBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                buchtPrivateWire.Detach();
                                DB.Save(buchtPrivateWire, false);

                                _newBuchtList[i].BuchtIDConnectedOtherBucht = buchtPrivateWire.ID;
                                _oldBuchtList[i].BuchtIDConnectedOtherBucht = null;


                                _newBuchtList[i].CabinetInputID = _oldBuchtList[i].CabinetInputID;
                                _oldBuchtList[i].CabinetInputID = null;

                                _newBuchtList[i].CablePairID = _oldBuchtList[i].CablePairID;
                                _oldBuchtList[i].CablePairID = null;
                            }
                            else
                            {

                                if (_oldBuchtList[i].BuchtIDConnectedOtherBucht != null)
                                {
                                    Bucht buchtPrivateWire = Data.BuchtDB.GetBuchetByID(_oldBuchtList[i].BuchtIDConnectedOtherBucht);
                                    if (buchtPrivateWire.BuchtIDConnectedOtherBucht == _oldBuchtList[i].ID)
                                    {
                                        buchtPrivateWire.BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                        if (_oldBuchtList.Any(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht)) _oldBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                        if (_newBuchtList.Any(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht)) _newBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                                        buchtPrivateWire.Detach();
                                        DB.Save(buchtPrivateWire, false);
                                    }
                                }

                                _newBuchtList[i].Status = exchangeConncetion.Where(t3 => t3.OldBuchtID == _oldBuchtListWithoutPCM[i].ID).SingleOrDefault().OldStatusID;
                                _oldBuchtListWithoutPCM[i].Status = (int)DB.BuchtStatus.Free;

                                _newBuchtList[i].CablePairID = _oldBuchtListWithoutPCM[i].CablePairID;
                                _oldBuchtListWithoutPCM[i].CablePairID = null;

                                _newBuchtList[i].CabinetInputID = _oldBuchtListWithoutPCM[i].CabinetInputID;
                                _oldBuchtListWithoutPCM[i].CabinetInputID = null;

                                _newBuchtList[i].ConnectionID = _oldBuchtListWithoutPCM[i].ConnectionID;
                                _oldBuchtListWithoutPCM[i].ConnectionID = null;

                                _newBuchtList[i].SwitchPortID = _oldBuchtListWithoutPCM[i].SwitchPortID;
                                _oldBuchtListWithoutPCM[i].SwitchPortID = null;
                            }

                            //worker.ReportProgress(i * 100 / count);
                        }

                        _oldBuchtListWithoutPCM.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldBuchtListWithoutPCM);

                        _newBuchtList.ForEach(t => t.Detach());
                        DB.UpdateAll(_newBuchtList);

                        _oldBuchtListWithPCM.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldBuchtListWithPCM);


                        _exchangeCentralCableMDF.CompletionDate = DB.GetServerDate();
                        _exchangeCentralCableMDF.Detach();
                        DB.Save(_exchangeCentralCableMDF, false);


                        ts2.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در فرایند انجام برگردان لطفا مشخصات خطا و درخواست را به مدیر سیستم گزارش دهید\n" + ex.Message);
            }

            //  قسمت کامنت شده هنگامی که یک کابل جدید راه اندازی می کنند و مشترکینی که روی کابلی هستن که پر شده است به این کابل بدون تغییر ورودی منتقل می شوند قابل استفاده است
            //try
            //{
            //    if (_exchangeCentralCableMDF.CompletionDate == null)
            //    {

            //        using (TransactionScope ts2 = new TransactionScope())
            //        {
            //            // Out Reservations the past cabinet
            //            if (_pastCabinet != null)
            //            {
            //                _pastCabinet.Status = (int)DB.CabinetStatus.Install;
            //                _pastCabinet.Detach();
            //                DB.Save(_pastCabinet);
            //            }
            //            // Out Reservations the past cable
            //            if (_pastCable != null)
            //            {
            //                _pastCable.Status = (int)DB.CableStatus.CableConnection;
            //                _pastCable.Detach();
            //                DB.Save(_pastCable);
            //            }


            //            _oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_exchangeCentralCableMDF.FromCabinetInputID, _exchangeCentralCableMDF.ToCabinetInputID);
            //            _oldCablePairs = Data.CablePairDB.GetCablePairByCabinetInputs(_oldCabinetInputs.Select(t => t.ID).ToList());
            //            _oldBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(_oldCabinetInputs.Select(t => t.ID).ToList());
            //            _oldBuchtListWithPCM = _oldBuchtList.Where(t => t.BuchtTypeID == (int)DB.BuchtType.InLine || t.BuchtTypeID == (int)DB.BuchtType.OutLine).ToList();
            //            _oldBuchtListWithoutPCM = _oldBuchtList.Where(t => t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).ToList();

            //             _newBuchtList = Data.BuchtDB.GetBuchtFromBuchtIDToBuchtID(_exchangeCentralCableMDF.FromBuchtID, _exchangeCentralCableMDF.ToBuchtID);
            //            //  _newCablePairs = Data.CablePairDB.GetCablePairByCablePairIDToCablePairID(_exchangeCentralCableMDF.FromCablePairID, _exchangeCentralCableMDF.ToCablePairID);
            //            int count = _oldBuchtListWithoutPCM.Count();
            //            for (int i = 0; i < count; i++)
            //            {
            //                // transform bucht else PCM
            //                if (_oldBuchtListWithoutPCM[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtListWithoutPCM[i].Status == (int)DB.BuchtStatus.Connection && _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht == null)
            //                {
            //                    _newBuchtList[i].Status = _oldBuchtListWithoutPCM[i].Status;
            //                    _oldBuchtListWithoutPCM[i].Status = (int)DB.BuchtStatus.Free;

            //                    //_newBuchtList[i].ADSLPortID = _oldBuchtList[i].ADSLPortID;
            //                    //_oldBuchtList[i].ADSLPortID = null;


            //                    //_newBuchtList[i].ADSLStatus = _oldBuchtList[i].ADSLStatus;
            //                    //_oldBuchtList[i].ADSLStatus = false;

            //                    //_newBuchtList[i].ADSLType = _oldBuchtList[i].ADSLType;
            //                    //_oldBuchtList[i].ADSLType = null;

            //                    _newBuchtList[i].ConnectionID = _oldBuchtListWithoutPCM[i].ConnectionID;
            //                    _oldBuchtListWithoutPCM[i].ConnectionID = null;

            //                    int newBuchtType = _newBuchtList[i].BuchtTypeID;
            //                    _newBuchtList[i].BuchtTypeID = _oldBuchtListWithoutPCM[i].BuchtTypeID;
            //                    _oldBuchtListWithoutPCM[i].BuchtTypeID = newBuchtType;


            //                    _newBuchtList[i].SwitchPortID = _oldBuchtListWithoutPCM[i].SwitchPortID;
            //                    _oldBuchtListWithoutPCM[i].SwitchPortID = null;

            //                    _oldCablePairs.Where(t => t.CabinetInputID == _oldBuchtListWithoutPCM[i].CabinetInputID).SingleOrDefault().CabinetInputID = null;
            //                    _newCablePairs[i].CabinetInputID = (long)_oldCabinetInputs[i].ID;
            //                    _newBuchtList[i].CabinetInputID = (long)_oldCabinetInputs[i].ID;
            //                    _oldBuchtListWithoutPCM[i].CabinetInputID = null;

            //                }
            //                // transform PCM bucht
            //                else if (_oldBuchtListWithoutPCM[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtListWithoutPCM[i].Status == (int)DB.BuchtStatus.AllocatedToInlinePCM)
            //                {

            //                    _newBuchtList[i].Status = _oldBuchtListWithoutPCM[i].Status;
            //                    _oldBuchtListWithoutPCM[i].Status = (int)DB.BuchtStatus.Free;

            //                    _oldBuchtListWithPCM.Where(t => t.ID == _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht).SingleOrDefault().BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;

            //                    _newBuchtList[i].BuchtIDConnectedOtherBucht = _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht;
            //                    _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht = null;

            //                    _newBuchtList[i].ConnectionID = _oldBuchtListWithoutPCM[i].ConnectionID;
            //                    _oldBuchtListWithoutPCM[i].ConnectionID = null;


            //                    _oldCablePairs.Where(t => t.CabinetInputID == _oldBuchtListWithoutPCM[i].CabinetInputID).SingleOrDefault().CabinetInputID = null;
            //                    _newCablePairs[i].CabinetInputID = _oldCabinetInputs[i].ID;

            //                    _newBuchtList[i].CabinetInputID = _oldBuchtListWithoutPCM[i].CabinetInputID;
            //                    _oldBuchtListWithoutPCM[i].CabinetInputID = null;


            //                    //  _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht = null;
            //                    //  _oldBuchtListWithoutPCM[i].ConnectionID = null;

            //                    //_oldBuchtListWithPCM.Where(t => t.CabinetInputID == _oldBuchtListWithoutPCM[i].CabinetInputID).ToList().ForEach(t => t.CabinetInputID = _newBuchtList[i].CabinetInputID);
            //                }
            //                // if bucht is PrivateWire change
            //                else if (_oldBuchtList[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtList[i].Status == (int)DB.BuchtStatus.Connection && _oldBuchtList[i].BuchtIDConnectedOtherBucht != null && Data.ExchangeCabinetInputDB.IsSpecialWire(_oldBuchtList[i].ID))
            //                {
            //                    _newBuchtList[i].Status = _oldBuchtList[i].Status;
            //                    _oldBuchtList[i].Status = (int)DB.BuchtStatus.Free;

            //                    _newBuchtList[i].ConnectionID = _oldBuchtList[i].ConnectionID;
            //                    _oldBuchtList[i].ConnectionID = null;

            //                    _newBuchtList[i].SwitchPortID = _oldBuchtList[i].SwitchPortID;
            //                    _oldBuchtList[i].SwitchPortID = null;

            //                    int newBuchtType = _newBuchtList[i].BuchtTypeID;
            //                    _newBuchtList[i].BuchtTypeID = _oldBuchtList[i].BuchtTypeID;
            //                    _oldBuchtList[i].BuchtTypeID = newBuchtType;


            //                    Bucht buchtPrivateWire = Data.BuchtDB.GetBuchetByID(_oldBuchtList[i].BuchtIDConnectedOtherBucht);
            //                    buchtPrivateWire.BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
            //                    if (_oldBuchtList.Any(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht)) _oldBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
            //                    if (_newBuchtList.Any(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht)) _newBuchtList.SingleOrDefault(b => b.ID == _oldBuchtList[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
            //                    buchtPrivateWire.Detach();
            //                    DB.Save(buchtPrivateWire, false);

            //                    _newBuchtList[i].BuchtIDConnectedOtherBucht = buchtPrivateWire.ID;
            //                    _oldBuchtList[i].BuchtIDConnectedOtherBucht = null;

            //                    _oldCablePairs.Where(t => t.CabinetInputID == _oldBuchtList[i].CabinetInputID).SingleOrDefault().CabinetInputID = null;
            //                    _newCablePairs[i].CabinetInputID = _oldCabinetInputs[i].ID;

            //                    _newBuchtList[i].CabinetInputID = _oldCabinetInputs[i].ID;
            //                    _oldBuchtList[i].CabinetInputID = null;
            //                }
            //                // if bucht is free only change the cabinet input it.
            //                else if (_oldBuchtList[i].Status == (int)DB.BuchtStatus.Free)
            //                {
            //                    _oldCablePairs.Where(t => t.CabinetInputID == _oldBuchtList[i].CabinetInputID).SingleOrDefault().CabinetInputID = null;
            //                    _newCablePairs[i].CabinetInputID = _oldCabinetInputs[i].ID;

            //                    _newBuchtList[i].CabinetInputID = _oldCabinetInputs[i].ID;
            //                    _oldBuchtList[i].CabinetInputID = null;
            //                }
            //                worker.ReportProgress(i * 100 / count);
            //            }



            //            _oldBuchtList.ForEach(t => t.Detach());
            //            DB.UpdateAll(_oldBuchtList);

            //            _oldCablePairs.ForEach(t => t.Detach());
            //            DB.UpdateAll(_oldCablePairs);


            //            _newCablePairs.ForEach(t => t.Detach());
            //            DB.UpdateAll(_newCablePairs);

            //            _newBuchtList.ForEach(t => t.Detach());
            //            DB.UpdateAll(_newBuchtList);

            //            _oldBuchtListWithPCM.ForEach(t => t.Detach());
            //            DB.UpdateAll(_oldBuchtListWithPCM);


            //            _exchangeCentralCableMDF.CompletionDate = DB.GetServerDate();
            //            _exchangeCentralCableMDF.Detach();
            //            DB.Save(_exchangeCentralCableMDF, false);

            //            ts2.Complete();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("خطا در فرایند انجام برگردان لطفا مشخصات خطا و درخواست را به مدیر سیستم انتقال دهید\n" + ex.Message);
            //}

        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ShowSuccessMessage(e.ProgressPercentage.ToString() + "%");
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_exchangeCentralCableMDF.CompletionDate != null)
            {
                ShowSuccessMessage("اتمام عملیات برگردان");
                this.IsEnabled = true;
                _Action.InternalForward(null, null);
            }
        }
        private void wiringButtom_Click(object sender, RoutedEventArgs e)
        
        {
            _exchangeCentralCableMDF = Data.TranslationCentralCableMDFDB.GetTranslationVentralCableMDFByID(_requestID);
        //    _oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_exchangeCentralCableMDF.FromCabinetInputID, _exchangeCentralCableMDF.ToCabinetInputID);
            
            List<TranslationPostInputMDFInfo> newBuchtList = new List<TranslationPostInputMDFInfo>();
            List<TranslationPostInputMDFInfo> OldBuchtList = new List<TranslationPostInputMDFInfo>();

          //  newBuchtList = ReportDB.GetBuchtFromBuchtIDToBuchtID(_exchangeCentralCableMDF.FromBuchtID, _exchangeCentralCableMDF.ToBuchtID, (byte)DB.BuchtStatus.ExchangeCentralCableMDF);
           // OldBuchtList = ReportDB.GetBuchtByCabinetInputIDs(_oldCabinetInputs.Select(t => t.ID).ToList());
          //  _oldBuchtList = Data.BuchtDB.GetAllBuchtFromBuchtIDToBuchtID(exchangeCentralCableMDF.FromNewBuchtID, exchangeCentralCableMDF.ToNewBuchtID);

            List<TranslationPostInputMDFInfo> Result = new List<TranslationPostInputMDFInfo>();

            for(int i=0;i<OldBuchtList.Count();i++)
            {
                TranslationPostInputMDFInfo Element = new TranslationPostInputMDFInfo();
               
                Element.OldRadif = OldBuchtList[i].OldRadif;
                Element.OldTabaghe = OldBuchtList[i].OldTabaghe;
                Element.OldEttesali = OldBuchtList[i].OldEttesali;

                Element.NewRadif = newBuchtList[i].NewRadif;
                Element.NewTabaghe = newBuchtList[i].NewTabaghe;
                Element.NewEttesali = newBuchtList[i].NewEttesali;

                Result.Add(Element);
            }
            SendToPrint(Result);
        }

        private void SendToPrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.MDfCentralTranslationForMDFReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = TelItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = CRM.Application.Codes.Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = TelItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = CRM.Application.Codes.Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void PrintItem_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            DataSet data = cabinetInputsList.ToDataSet("Result", TelItemsDataGrid);
            CRM.Application.Codes.Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn);

            this.Cursor = Cursors.Arrow;
        }
        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, TelItemsDataGrid.Name, TelItemsDataGrid.Columns);

        }

        #region Filters
        private bool PredicateFilters(object obj)
        {
            TranslationOpticalCabinetToNormalInfo checkableObject = obj as TranslationOpticalCabinetToNormalInfo;
            return checkableObject.OldTelephonNo.ToString().Contains(FilterTelephonNoTextBox.Text.Trim());
        }

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

        #endregion Filters

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(TelItemsDataGrid.Columns);
            ReportSettingForm reportSettingForm = new ReportSettingForm(dataGridColumn);
            reportSettingForm._title = _title;
            reportSettingForm._checkedList.Clear();
            reportSettingForm._checkedList = _groupingColumn;
            reportSettingForm._sumCheckedList = _sumColumn;
            reportSettingForm.ShowDialog();
            _sumColumn = reportSettingForm._sumCheckedList;
            _groupingColumn = reportSettingForm._checkedList;
            _title = reportSettingForm._title;

        }



    }
}

