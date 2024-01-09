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
    /// Interaction logic for ExchangePostNetworkForm.xaml
    /// </summary>
    public partial class TranslationPostInputInvestigateForm : Local.RequestFormBase
    {

        CRM.Application.UserControls.TranslationPostInputInfo _translationPostInputInfo;
        private long _requestID = 0;
        private Request request { get; set; }
        private CRM.Data.TranslationPostInput _translationPostInput { get; set; }

        BackgroundWorker worker;

        public TranslationPostInputInvestigateForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Exit }; 
        }
        public TranslationPostInputInvestigateForm(long reqeustID)
            : this()
        {
            _requestID = reqeustID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {



            _translationPostInput = Data.TranslationPostInputDB.GetTranslationPostInputByID(_requestID);

            request = Data.RequestDB.GetRequestByID(_requestID);

            _translationPostInputInfo = new UserControls.TranslationPostInputInfo(_requestID);
            TranslationInfo.Content = _translationPostInputInfo;
            TranslationInfo.DataContext = _translationPostInputInfo;


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
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {

                    request.Detach();
                    DB.Save(request, false);

                    _translationPostInput.Detach();
                    DB.Save(_translationPostInput, false);

                    IsSaveSuccess = true;
                    ts2.Complete();
                }

            }
            catch(Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات" , ex);
            }

            return IsSaveSuccess;
        }

        public override bool Forward()
        {

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    Save();
                    this.RequestID = request.ID;
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
            try
            {
                if (_translationPostInput.DateOfFinal == null)
                {
                    DateTime currentDataTime = DB.GetServerDate();
                    using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
                    {

                        worker.ReportProgress(0);

                        List<TranslationPostInputConnection> translationPostInputConnection = Data.TranslationPostInputDB.GetTranslationPostInputConectionByRequestID(request.ID);

                        Post fromPost = Data.PostDB.GetPostByID(_translationPostInput.FromPostID);
                        Post toPost = Data.PostDB.GetPostByID(_translationPostInput.ToPostID);

                        Cabinet fromCabinet = Data.CabinetDB.GetCabinetByID(_translationPostInput.FromCabinetID);
                        Cabinet toCabinet = Data.CabinetDB.GetCabinetByID(_translationPostInput.ToCabinetID);

                        List<PostContact> oldPostContact = Data.PostContactDB.GetPostContactByIDs(translationPostInputConnection.Select(t => t.ConnectionID).ToList());
                        List<Bucht> oldBucht = Data.BuchtDB.GetBuchtByConnectionIDs(translationPostInputConnection.Select(t => t.ConnectionID).ToList());
                        //List<CabinetInput> oldCabinetInput = Data.CabinetInputDB.GetCabinetInputByIDs(oldBucht.Select(t=>(long)t.CabinetInputID).ToList());

                        
                        List<PostContact> newPostContact = Data.PostContactDB.GetPostContactConnctionNo(oldPostContact.Select(t => t.ConnectionNo).ToList(), toPost.ID);
                        List<Bucht> newBucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(translationPostInputConnection.Select(t => t.CabinetInputID).ToList());

                        int count = translationPostInputConnection.Count;
                        if (fromPost.ID == toPost.ID)
                        {

                            for (int i = 0; i < count; i++)
                                {
                                    if (oldPostContact.Where(t => t.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                                    {

                                        newBucht.Where(t => t.CabinetInputID == translationPostInputConnection[i].CabinetInputID).ToList().ForEach(t =>
                                        {
                                            t.SwitchPortID = oldBucht.Where(t2 => t2.ConnectionID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().SwitchPortID;
                                            t.BuchtIDConnectedOtherBucht = oldBucht.Where(t2 => t2.ConnectionID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().BuchtIDConnectedOtherBucht;
                                            t.Status = (int)DB.BuchtStatus.Connection;
                                            t.ConnectionID = translationPostInputConnection[i].ConnectionID;
                                        });
                         
                                        oldBucht.Where(t => t.ConnectionID == translationPostInputConnection[i].ConnectionID).ToList().ForEach(t => 
                                        {
                                            t.ConnectionID = null;
                                            t.Status = (int)DB.BuchtStatus.Free;
                                            t.ADSLStatus = false;
                                            t.SwitchPortID = null;
                                            t.BuchtIDConnectedOtherBucht = null;
                                        });

                                    }
                                    else if (oldPostContact.Where(t => t.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)
                                    {

                                        newBucht.Where(t => t.CabinetInputID == translationPostInputConnection[i].CabinetInputID).ToList().ForEach(t =>
                                        {
                                            t.Status = (byte)DB.BuchtStatus.AllocatedToInlinePCM;
                                            t.ConnectionID = oldBucht.Where(t2 => t2.ConnectionID == translationPostInputConnection[i].ConnectionID && t2.Status == (byte)DB.BuchtStatus.AllocatedToInlinePCM).SingleOrDefault().ConnectionID;
                                            t.BuchtIDConnectedOtherBucht = oldBucht.Where(t3 => t3.ConnectionID == translationPostInputConnection[i].ConnectionID && t3.Status == (byte)DB.BuchtStatus.ConnectedToPCM).SingleOrDefault().ID;
                                        });

                                        List<Bucht> oldPcmBucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(new List<long> { (long)oldBucht.Where(t => t.ConnectionID == translationPostInputConnection[i].ConnectionID).Take(1).SingleOrDefault().CabinetInputID }).Where(t => t.BuchtTypeID == (byte)DB.BuchtType.InLine).ToList();
                                        oldPcmBucht.ForEach(t => 
                                        {
                                            t.CabinetInputID = translationPostInputConnection[i].CabinetInputID;
                                            t.Detach();
                                        });
                                        DB.UpdateAll(oldPcmBucht);

                                        oldBucht.Where(t => t.ConnectionID == translationPostInputConnection[i].ConnectionID && t.Status == (byte)DB.BuchtStatus.ConnectedToPCM).ToList().ForEach(t =>
                                            {
                                                t.BuchtIDConnectedOtherBucht = newBucht.Where(t2 => t2.CabinetInputID == translationPostInputConnection[i].CabinetInputID).SingleOrDefault().ID;
                                                t.CabinetInputID = translationPostInputConnection[i].CabinetInputID;
                                            });
                                  
                                        oldBucht.Where(t => t.ConnectionID == translationPostInputConnection[i].ConnectionID && t.Status == (byte)DB.BuchtStatus.AllocatedToInlinePCM).ToList().ForEach(t => 
                                        {
                                            t.BuchtIDConnectedOtherBucht = null;
                                            t.Status = (byte)DB.BuchtStatus.Free;
                                            t.ConnectionID = null;
                                        });
                                    }

                                    worker.ReportProgress((i + 1) * 100 / count);
                                };

                        }
                        else
                        {

                            for (int i = 0; i < count; i++)
                            {
                                if (oldPostContact.Where(t => t.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                                {

                                    newBucht.Where(t => t.CabinetInputID == translationPostInputConnection[i].CabinetInputID).ToList().ForEach(t=>
                                    {
                                        t.SwitchPortID = oldBucht.Where(t2 => t2.ConnectionID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().SwitchPortID;
                                        t.BuchtIDConnectedOtherBucht = oldBucht.Where(t2 => t2.ConnectionID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().BuchtIDConnectedOtherBucht;
                                        t.Status = (int)DB.BuchtStatus.Connection;
                                        t.ConnectionID = newPostContact.Where(t3 => t3.ConnectionNo == oldPostContact.Where(t4 => t4.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionNo).SingleOrDefault().ID;
                                    });

                                    newPostContact.Where(t => t.ConnectionNo == oldPostContact.Where(t2 => t2.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionNo).SingleOrDefault().Status = oldPostContact.Where(t2 => t2.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().Status;

                                    oldPostContact.Where(t2 => t2.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().Status = (byte)DB.PostContactStatus.Free;

                                    oldBucht.Where(t => t.ConnectionID == translationPostInputConnection[i].ConnectionID).ToList().ForEach(t => 
                                    {
                                        t.Status = (byte)DB.BuchtStatus.Free;
                                        t.ADSLStatus = false;
                                        t.SwitchPortID = null;
                                        t.ConnectionID = null;
                                        t.BuchtIDConnectedOtherBucht = null;
                                    });
                                }
                                else if (oldPostContact.Where(t => t.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)
                                {
                                    List<PostContact> pastOldPostContactPCMOutputList = new List<PostContact>();
                                    pastOldPostContactPCMOutputList = Data.PostContactDB.GetPostContactByPostID(fromPost.ID, oldPostContact.Where(t2 => t2.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionNo).Where(t => t.ConnectionType != (byte)DB.PostContactConnectionType.PCMRemote).ToList();
                                    pastOldPostContactPCMOutputList.ForEach(item => { item.PostID = toPost.ID; item.Detach(); });

                                    oldPostContact.Where(t => t.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().PostID = toPost.ID;

                                    newPostContact.Where(t => t.ConnectionNo == oldPostContact.Where(t2 => t2.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionNo).SingleOrDefault().PostID = fromPost.ID;

                                    List<Bucht> oldPcmBucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(new List<long> { (long)oldBucht.Where(t => t.ConnectionID == translationPostInputConnection[i].ConnectionID).Take(1).SingleOrDefault().CabinetInputID}).Where(t => t.BuchtTypeID == (byte)DB.BuchtType.InLine).ToList();
                                    oldPcmBucht.ForEach(t => { t.CabinetInputID = translationPostInputConnection[i].CabinetInputID; t.Detach(); });


                                    newBucht.Where(t => t.CabinetInputID == translationPostInputConnection[i].CabinetInputID).ToList().ForEach(t=>
                                    {
                                        t.Status = (byte)DB.BuchtStatus.AllocatedToInlinePCM;
                                        t.ConnectionID = oldBucht.Where(t2 => t2.ConnectionID == translationPostInputConnection[i].ConnectionID && t2.Status == (byte)DB.BuchtStatus.AllocatedToInlinePCM).SingleOrDefault().ConnectionID;
                                        t.BuchtIDConnectedOtherBucht = oldBucht.Where(t2 => t2.ConnectionID == translationPostInputConnection[i].ConnectionID && t2.Status == (byte)DB.BuchtStatus.ConnectedToPCM).SingleOrDefault().ID;
                                    });

                                    oldBucht.Where(t => t.ConnectionID == translationPostInputConnection[i].ConnectionID && t.Status == (byte)DB.BuchtStatus.ConnectedToPCM).ToList().ForEach(t =>
                                    {
                                        t.BuchtIDConnectedOtherBucht = newBucht.Where(t2 => t2.CabinetInputID == translationPostInputConnection[i].CabinetInputID).SingleOrDefault().ID;
                                        t.CabinetInputID = translationPostInputConnection[i].CabinetInputID;
                                    });


                                    oldBucht.Where(t => t.ConnectionID == translationPostInputConnection[i].ConnectionID && t.Status == (byte)DB.BuchtStatus.AllocatedToInlinePCM).ToList().ForEach(t => { t.BuchtIDConnectedOtherBucht = null; t.Status = (byte)DB.BuchtStatus.Free; t.ConnectionID = null; });
                             
                                    DB.UpdateAll(oldPcmBucht);

                                    DB.UpdateAll(pastOldPostContactPCMOutputList);
                                }

                                worker.ReportProgress((i+1) * 100 / count);
                            };
                        }

                        oldPostContact.ForEach(t => t.Detach());
                        DB.UpdateAll(oldPostContact);

                        newPostContact.ForEach(t => t.Detach());
                        DB.UpdateAll(newPostContact);

                        oldBucht.ForEach(t => t.Detach());
                        DB.UpdateAll(oldBucht);

                        newBucht.ForEach(t => t.Detach());
                        DB.UpdateAll(newBucht);

                        _translationPostInput.DateOfFinal = currentDataTime;
                        _translationPostInput.Detach();
                        DB.Save(_translationPostInput);

                       fromCabinet.Status = (byte)DB.CabinetStatus.Install;
                       fromCabinet.Detach();
                       DB.Save(fromCabinet, false);

                       toCabinet.Status = (byte)DB.CabinetStatus.Install;
                       toCabinet.Detach();
                       DB.Save(toCabinet, false);


                        ts3.Complete();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("خطا در عملیات برگردان\n" + ex.Message);
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ShowSuccessMessage(e.ProgressPercentage.ToString() + "%");
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_translationPostInput.DateOfFinal != null)
            {
                ShowSuccessMessage("اتمام عملیات برگردان");
                this.IsEnabled = true;
                _Action.InternalForward(null, null);
            }
        }

        private void wiringButtom_Click(object sender, RoutedEventArgs e)
        {
            Folder.MessageBox.ShowInfo("این گزارش در دست تهیه می باشد");
        }
    }
}



