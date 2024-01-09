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
    public partial class TranslationCabinetInvestigateFrom : Local.RequestFormBase
    {
        List<Bucht> _oldBuchtList { get; set; }
        List<Bucht> _oldBuchtListWithoutPCM { get; set; }
        List<Bucht> _oldBuchtListPCM { get; set; }
        List<Bucht> _newBuchtList { get; set; }

        // List<Bucht> _pastOldBuchtList { get; set; }
        // List<Bucht> _pastNewBuchtList { get; set; }

        List<CabinetInput> _newCabinetInputs { get; set; }
        List<CabinetInput> _oldCabinetInputs { get; set; }

        List<Post> _oldPosts { get; set; }
        List<Post> _newPosts { get; set; }

        //List<CheckableItem> _pastCabinet { get; set; }
        //List<PostContact> _newConnectionID { get; set; }

        CRM.Application.UserControls.TranslationCabinetInfo _translationCabinetInfo;
        private Request request { get; set; }
        Data.ExchangeCabinetInput _exchangeCabinetInput { get; set; }
        private long _requestID = 0;
        BackgroundWorker worker;

        public TranslationCabinetInvestigateFrom()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
        }
        public TranslationCabinetInvestigateFrom(long requestID)
            : this()
        {
            this._requestID = requestID;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _translationCabinetInfo = new CRM.Application.UserControls.TranslationCabinetInfo(_requestID);
            TranslationInfo.DataContext = _translationCabinetInfo;
            TranslationInfo.Content = _translationCabinetInfo;

            _exchangeCabinetInput = Data.TranslationCabinetDB.GetTranslationCabinetByID(_requestID);
            request = Data.RequestDB.GetRequestByID(_requestID);

        }


        public override bool Save()
        {
            try
            {
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {

                    _exchangeCabinetInput.Detach();
                    DB.Save(_exchangeCabinetInput, false);

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

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ShowSuccessMessage(e.ProgressPercentage.ToString() + "%");
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_exchangeCabinetInput.CompletionDate != null)
            {
                ShowSuccessMessage("اتمام عملیات برگردان");
                this.IsEnabled = true;
                _Action.InternalForward(null, null);
            }
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (_exchangeCabinetInput.CompletionDate == null)
                {
                    return;
                }
                DateTime currentDataTime = DB.GetServerDate();

                using (TransactionScope ts = new TransactionScope())
                {

                    // ابتدا مرکزی های انتخاب شده از کافو قبل برگردان به کافو بعد برگردان منتقل می شود
                    // سپس اگر حالت انتقال پست انتخاب شده باشد پست ها با تغییر کافو به کافو جدید انتقال میابد
                    // اگر حالت تبدیل پست انتخاب شود هر اتصالی بسته به پی سی ام یا عادی یودن به پست جدید انتقال میابد
                    // 
                    // exit cabinet from reserve
                     ExitReserveCabinet(_exchangeCabinetInput.NewCabinetID, _exchangeCabinetInput.OldCabinetID);

                    // number old cabinetInput
                    _oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_exchangeCabinetInput.FromOldCabinetInputID ?? 0 , _exchangeCabinetInput.ToOldCabinetInputID ?? 0);
                    _oldBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(_oldCabinetInputs.Select(t => t.ID).ToList());

                    _oldBuchtListWithoutPCM = _oldBuchtList.Where(t => !(t.BuchtTypeID == (int)DB.BuchtType.InLine || t.BuchtTypeID == (int)DB.BuchtType.OutLine)).ToList();
                    _oldBuchtListPCM = _oldBuchtList.Where(t => t.BuchtTypeID == (int)DB.BuchtType.InLine || t.BuchtTypeID == (int)DB.BuchtType.OutLine).ToList();

                    // number new cabinetInput
                    _newCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_exchangeCabinetInput.FromNewCabinetInputID ?? 0, _exchangeCabinetInput.ToNewCabinetInputID ?? 0);
                    _newBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDs(_newCabinetInputs.Select(t => t.ID).ToList());

                    _oldPosts = Data.PostDB.GetAllPostsByPostContactList(_oldBuchtList.Where(t => t.ConnectionID != null).Select(t => t.ConnectionID ?? 0).ToList());


                    if (_exchangeCabinetInput.IsChangePost == true)
                    {
                        _newPosts = Data.PostDB.GetTheNumberPostByStartID((int)_exchangeCabinetInput.FromNewPostID, _oldPosts.Count());
                    }

                    for (int i = 0; i < _oldBuchtListWithoutPCM.Count(); i++)
                    {
                        // transform bucht else PCM
                        if (_oldBuchtListWithoutPCM[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtListWithoutPCM[i].Status == (int)DB.BuchtStatus.Connection && _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht == null)
                        {
                            _newBuchtList[i].Status = _oldBuchtListWithoutPCM[i].Status;
                            _oldBuchtListWithoutPCM[i].Status = (int)DB.BuchtStatus.Free;

                            _newBuchtList[i].ConnectionID = _oldBuchtListWithoutPCM[i].ConnectionID;
                            _oldBuchtListWithoutPCM[i].ConnectionID = null;

                            _newBuchtList[i].SwitchPortID = _oldBuchtListWithoutPCM[i].SwitchPortID;
                            _oldBuchtListWithoutPCM[i].SwitchPortID = null;

                            //_newBuchtList[i].ADSLPortID = _oldBuchtList[i].ADSLPortID;
                            //_oldBuchtList[i].ADSLPortID = null;


                            //_newBuchtList[i].ADSLStatus = _oldBuchtList[i].ADSLStatus;
                            //_oldBuchtList[i].ADSLStatus = false;

                            //_newBuchtList[i].ADSLType = _oldBuchtList[i].ADSLType;
                            //_oldBuchtList[i].ADSLType = null;

                        }
                        // transform PCM bucht
                        else if (_oldBuchtListWithoutPCM[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtListWithoutPCM[i].Status == (int)DB.BuchtStatus.AllocatedToInlinePCM)
                        {

                            _newBuchtList[i].Status = _oldBuchtListWithoutPCM[i].Status;
                            _oldBuchtListWithoutPCM[i].Status = (int)DB.BuchtStatus.Free;

                            _newBuchtList[i].BuchtIDConnectedOtherBucht = _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht;
                            _newBuchtList[i].ConnectionID = _oldBuchtListWithoutPCM[i].ConnectionID;


                            _oldBuchtListPCM.Where(b => b.ID == _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht).SingleOrDefault().BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                             _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht = null;

                            _oldBuchtListWithoutPCM[i].ConnectionID = null;


                            _oldBuchtListPCM.Where(t => t.CabinetInputID == _oldBuchtListWithoutPCM[i].CabinetInputID).ToList().ForEach(item => item.CabinetInputID = _newBuchtList[i].CabinetInputID);

                        }
                        // if bucht is PrivateWire change
                        else if (_oldBuchtListWithoutPCM[i].BuchtTypeID == (int)DB.BuchtType.CustomerSide && _oldBuchtListWithoutPCM[i].Status == (int)DB.BuchtStatus.Connection && _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht != null && Data.ExchangeCabinetInputDB.IsSpecialWire(_oldBuchtListWithoutPCM[i].ID))
                        {
                            _newBuchtList[i].Status = _oldBuchtListWithoutPCM[i].Status;
                            _oldBuchtListWithoutPCM[i].Status = (int)DB.BuchtStatus.Free;

                            _newBuchtList[i].ConnectionID = _oldBuchtListWithoutPCM[i].ConnectionID;
                            _oldBuchtListWithoutPCM[i].ConnectionID = null;

                            _newBuchtList[i].SwitchPortID = _oldBuchtListWithoutPCM[i].SwitchPortID;
                            _oldBuchtListWithoutPCM[i].SwitchPortID = null;

                            //_newBuchtList[i].ADSLPortID = _oldBuchtList[i].ADSLPortID;
                            //_oldBuchtList[i].ADSLPortID = null;


                            //_newBuchtList[i].ADSLStatus = _oldBuchtList[i].ADSLStatus;
                            //_oldBuchtList[i].ADSLStatus = false;

                            //_newBuchtList[i].ADSLType = _oldBuchtList[i].ADSLType;
                            //_oldBuchtList[i].ADSLType = null;

                            int newBuchtType = _newBuchtList[i].BuchtTypeID;
                            _newBuchtList[i].BuchtTypeID = _oldBuchtListWithoutPCM[i].BuchtTypeID;
                            _oldBuchtListWithoutPCM[i].BuchtTypeID = newBuchtType;


                            Bucht buchtPrivateWire = Data.BuchtDB.GetBuchetByID(_oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht);
                            buchtPrivateWire.BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                            if (_oldBuchtListWithoutPCM.Any(b => b.ID == _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht)) _oldBuchtListWithoutPCM.SingleOrDefault(b => b.ID == _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                            if (_newBuchtList.Any(b => b.ID == _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht)) _newBuchtList.SingleOrDefault(b => b.ID == _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht).BuchtIDConnectedOtherBucht = _newBuchtList[i].ID;
                            buchtPrivateWire.Detach();
                            DB.Save(buchtPrivateWire, false);

                            _newBuchtList[i].BuchtIDConnectedOtherBucht = buchtPrivateWire.ID;
                            _oldBuchtListWithoutPCM[i].BuchtIDConnectedOtherBucht = null;

                        }
                        else if ((_oldBuchtListWithoutPCM[i].Status == (int)DB.BuchtStatus.Connection && !(_oldBuchtListWithoutPCM[i].BuchtTypeID == (int)DB.BuchtType.InLine || _oldBuchtListWithoutPCM[i].BuchtTypeID == (int)DB.BuchtType.OutLine)))
                        {
                            throw new Exception("در میان بوخت ها نوع بوخت نا مشخصی وجود دارد لطفا با مدیر سیستم تماس بگیرید");
                        }

                        worker.ReportProgress(i * 100 / _oldBuchtList.Count());
                    }


                    _oldBuchtListWithoutPCM.ForEach(item => item.Detach());
                    DB.UpdateAll(_oldBuchtListWithoutPCM);

                    _oldBuchtListPCM.ForEach(item => item.Detach());
                    DB.UpdateAll(_oldBuchtListPCM);
                    
                    _newBuchtList.ForEach(item => item.Detach());
                    DB.UpdateAll(_newBuchtList);


                    if (_exchangeCabinetInput.IsChangePost == false)
                    {
                        // transform old post to new cabinet
                        _oldPosts.ForEach(item => { item.CabinetID = _exchangeCabinetInput.NewCabinetID; item.Detach(); });
                        DB.UpdateAll(_oldPosts);
                    }
                    else
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

                            worker.ReportProgress(j * 100 / _oldPosts.Count());
                        }
                    }

                    _exchangeCabinetInput.CompletionDate = currentDataTime;
                    _exchangeCabinetInput.Detach();
                    DB.Save(_exchangeCabinetInput);

                    ts.Complete();
                    IsSaveSuccess = true;
                }



            }
            catch (Exception ex)
            {
                ShowErrorMessage("برگردان ورودی کافو انجام نشد", ex);
            }
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
    }
}


