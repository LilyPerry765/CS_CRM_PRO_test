using CRM.Data;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for TranslationCabinetMDFFrom.xaml
    /// </summary>
    public partial class TranslationCentralCableMDFInvestigateForm : Local.RequestFormBase
    {
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

        public TranslationCentralCableMDFInvestigateForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
        }
        public TranslationCentralCableMDFInvestigateForm(long requestID) : this()
        {
            this._requestID = requestID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _translationCentralCableMDFInfo = new CRM.Application.UserControls.TranslationCentralCableMDFInfo(_requestID);
            TranslationInfo.DataContext = _translationCentralCableMDFInfo; ;
            TranslationInfo.Content = _translationCentralCableMDFInfo; ;

            _exchangeCentralCableMDF = Data.TranslationCentralCableMDFDB.GetTranslationVentralCableMDFByID(_requestID);
            request = Data.RequestDB.GetRequestByID(_requestID);

//            _pastCabinet = Data.CabinetDB.GetCabinetByID((int)_exchangeCentralCableMDF.CabinetID);

        }


        public override bool Save()
        {
            try
            {
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {
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

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            // در این برگردان یک کابل جدید راه اندازی می کنند و مشترکینی که روی کابلی هستن که پر شده است به این کابل بدون تغییر ورودی منتقل می شوند
            try
            {
                if (_exchangeCentralCableMDF.CompletionDate == null)
                {

                    using (TransactionScope ts2 = new TransactionScope())
                    {
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

                        throw new Exception("خطا بوخت");
                        _oldCabinetInputs       = Data.CabinetInputDB.GetCabinetInputFromIDToID(1, 1);
                        _oldCablePairs          = Data.CablePairDB.GetCablePairByCabinetInputs(_oldCabinetInputs.Select(t => t.ID).ToList());
                        _oldBuchtList           = Data.BuchtDB.GetBuchtByCabinetInputIDs(_oldCabinetInputs.Select(t => t.ID).ToList());
                        _oldBuchtListWithPCM    = _oldBuchtList.Where(t => t.BuchtTypeID == (int)DB.BuchtType.InLine || t.BuchtTypeID == (int)DB.BuchtType.OutLine).ToList();
                        _oldBuchtListWithoutPCM = _oldBuchtList.Where(t => t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).ToList();

                      //  _newBuchtList = Data.BuchtDB.GetBuchtFromCablePairIDToCablePairID(_exchangeCentralCableMDF.FromCablePairID, _exchangeCentralCableMDF.ToCablePairID);
                      //  _newCablePairs = Data.CablePairDB.GetCablePairByCablePairIDToCablePairID(_exchangeCentralCableMDF.FromCablePairID, _exchangeCentralCableMDF.ToCablePairID);
                        int count = _oldBuchtListWithoutPCM.Count();
                        for (int i = 0; i < count; i++)
                        {
                            // transform bucht else PCM
                            if (_oldBuchtListWithoutPCM[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtListWithoutPCM[i].Status == (int)DB.BuchtStatus.Connection && _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht == null)
                            {
                                _newBuchtList[i].Status = _oldBuchtListWithoutPCM[i].Status;
                                _oldBuchtListWithoutPCM[i].Status = (int)DB.BuchtStatus.Free;

                                //_newBuchtList[i].ADSLPortID = _oldBuchtList[i].ADSLPortID;
                                //_oldBuchtList[i].ADSLPortID = null;


                                //_newBuchtList[i].ADSLStatus = _oldBuchtList[i].ADSLStatus;
                                //_oldBuchtList[i].ADSLStatus = false;

                                //_newBuchtList[i].ADSLType = _oldBuchtList[i].ADSLType;
                                //_oldBuchtList[i].ADSLType = null;

                                _newBuchtList[i].ConnectionID = _oldBuchtListWithoutPCM[i].ConnectionID;
                                _oldBuchtListWithoutPCM[i].ConnectionID = null;

                                int newBuchtType = _newBuchtList[i].BuchtTypeID;
                                _newBuchtList[i].BuchtTypeID = _oldBuchtListWithoutPCM[i].BuchtTypeID;
                                _oldBuchtListWithoutPCM[i].BuchtTypeID = newBuchtType;


                                _newBuchtList[i].SwitchPortID = _oldBuchtListWithoutPCM[i].SwitchPortID;
                                _oldBuchtListWithoutPCM[i].SwitchPortID = null;

                                _oldCablePairs.Where(t => t.CabinetInputID == _oldBuchtListWithoutPCM[i].CabinetInputID).SingleOrDefault().CabinetInputID = null;
                                _newCablePairs[i].CabinetInputID = (long)_oldCabinetInputs[i].ID;
                                _newBuchtList[i].CabinetInputID  = (long)_oldCabinetInputs[i].ID;
                                _oldBuchtListWithoutPCM[i].CabinetInputID = null;

                            }
                            // transform PCM bucht
                            else if (_oldBuchtListWithoutPCM[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtListWithoutPCM[i].Status == (int)DB.BuchtStatus.AllocatedToInlinePCM)
                            {

                                _newBuchtList[i].Status = _oldBuchtListWithoutPCM[i].Status;
                                _oldBuchtListWithoutPCM[i].Status = (int)DB.BuchtStatus.Free;

                                _oldBuchtListWithPCM.Where(t => t.ID == _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht).SingleOrDefault().BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;

                                _newBuchtList[i].BuchtIDConnectedOtherBucht = _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht;
                                _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht = null;

                                _newBuchtList[i].ConnectionID = _oldBuchtListWithoutPCM[i].ConnectionID;
                                 _oldBuchtListWithoutPCM[i].ConnectionID = null;


                                _oldCablePairs.Where(t => t.CabinetInputID == _oldBuchtListWithoutPCM[i].CabinetInputID).SingleOrDefault().CabinetInputID = null;
                                _newCablePairs[i].CabinetInputID = _oldCabinetInputs[i].ID;

                                _newBuchtList[i].CabinetInputID = _oldBuchtListWithoutPCM[i].CabinetInputID;
                                _oldBuchtListWithoutPCM[i].CabinetInputID = null;

                               
                              //  _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht = null;
                              //  _oldBuchtListWithoutPCM[i].ConnectionID = null;

                                //_oldBuchtListWithPCM.Where(t => t.CabinetInputID == _oldBuchtListWithoutPCM[i].CabinetInputID).ToList().ForEach(t => t.CabinetInputID = _newBuchtList[i].CabinetInputID);
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
                            worker.ReportProgress(i * 100 / count);
                        }



                        _oldBuchtList.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldBuchtList);

                        _oldCablePairs.ForEach(t => t.Detach());
                        DB.UpdateAll(_oldCablePairs);


                        _newCablePairs.ForEach(t => t.Detach());
                        DB.UpdateAll(_newCablePairs);

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
                MessageBox.Show("خطا در فرایند انجام برگردان لطفا مشخصات خطا و درخواست را به مدیر سیستم انتقال دهید\n" + ex.Message);
            }

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



    }
}


