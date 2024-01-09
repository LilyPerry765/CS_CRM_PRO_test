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
using System.Xml.Linq;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ExchangePostNetworkForm.xaml
    /// </summary>
    public partial class TranslationPostInvertigateForm : Local.RequestFormBase
    {

        CRM.Application.UserControls.TranslationPostInfo _translationPostInfo;
        CRM.Data.TranslationPost _translationPost { get; set; }
        private long _requestID = 0;
        private Request request { get; set; }
        List<PostContact> oldPostContactList;
        List<PostContact> oldPostContactListWithOutPCMRemote;
        List<PostContact> newPostContactList;
        BackgroundWorker worker;

        public TranslationPostInvertigateForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Exit }; 
        }
        public TranslationPostInvertigateForm(long reqeustID)
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

            request = Data.RequestDB.GetRequestByID(_requestID);
            _translationPost = Data.TranslationPostDB.GetTranslationPostByID(_requestID);

            _translationPostInfo = new UserControls.TranslationPostInfo(_requestID);
            TranslationInfo.Content = _translationPostInfo;
            _translationPostInfo.DataContext = _translationPostInfo;

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

                    
                    IsSaveSuccess = true;
                    ts2.Complete();
                }

            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            return IsSaveSuccess;
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
            if (_translationPost.CompletionDate != null)
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
                if (_translationPost.CompletionDate == null)
                {
                    
                    DateTime currentDataTime = DB.GetServerDate();
                    using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
                    {

                        Data.TranslationPostDB.VerifyData(_translationPost , this.currentStat);
                        if (_translationPost.OverallTransfer == true)
                        {
                            oldPostContactList = Data.PostContactDB.GetPostContactByPostID(_translationPost.OldPostID);
                            newPostContactList = Data.PostContactDB.GetPostContactByPostID(_translationPost.NewPostID);

                            Post OldPost = Data.PostDB.GetPostByID(_translationPost.OldPostID);
                            Post NewPost = Data.PostDB.GetPostByID(_translationPost.NewPostID);


                            OldPost.Status = (byte)DB.PostStatus.Dayer;
                            OldPost.Detach();
                            DB.Save(OldPost, false);

                            NewPost.Status = (byte)DB.PostStatus.Dayer;
                            NewPost.Detach();
                            DB.Save(NewPost, false);
                        }
                        else
                        {
                            oldPostContactList = new List<PostContact> { Data.PostContactDB.GetPostContactByID((long)_translationPost.OldPostContactID) };
                            newPostContactList = new List<PostContact> { Data.PostContactDB.GetPostContactByID((long)_translationPost.NewPostContactID) };

                            if (oldPostContactList.SingleOrDefault().ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                            {
                                oldPostContactList.SingleOrDefault().Status = (int)DB.PostContactStatus.NoCableConnection;
                            }
                            else
                            {
                                oldPostContactList.SingleOrDefault().Status = (int)DB.PostContactStatus.CableConnection;
                            }
                            oldPostContactList.SingleOrDefault().Detach();
                            DB.Save(oldPostContactList.SingleOrDefault());

                            newPostContactList.SingleOrDefault().Status = (int)DB.PostContactStatus.Free;
                            newPostContactList.SingleOrDefault().Detach();
                            DB.Save(newPostContactList.SingleOrDefault());
                        }

                        oldPostContactListWithOutPCMRemote = oldPostContactList.Where(t => t.ConnectionType != (int)DB.PostContactConnectionType.PCMNormal).OrderBy(t => t.ConnectionNo).ToList();
                        int count = oldPostContactListWithOutPCMRemote.Count();

                        for (int i = 0; i < count; i++)
                        {


                            
                            if (oldPostContactListWithOutPCMRemote[i].ConnectionType == (int)DB.PostContactConnectionType.Noraml && oldPostContactListWithOutPCMRemote[i].Status == (int)DB.PostContactStatus.CableConnection)
                            {

                             



                                // اتصالی های غیره پی سی ام با تغییر پست انها به پست جدید منتقل می شوند
                                int newPostID = newPostContactList[i].PostID;
                                int connectionNo = newPostContactList[i].ConnectionNo;

                                newPostContactList[i].PostID = oldPostContactListWithOutPCMRemote[i].PostID;
                                newPostContactList[i].ConnectionNo = oldPostContactListWithOutPCMRemote[i].ConnectionNo;

                                oldPostContactListWithOutPCMRemote[i].PostID = newPostID;
                                oldPostContactListWithOutPCMRemote[i].ConnectionNo = connectionNo;

                                oldPostContactListWithOutPCMRemote[i].Detach();
                                DB.Save(oldPostContactListWithOutPCMRemote[i]);

                                newPostContactList[i].Detach();
                                DB.Save(newPostContactList[i]);

                                //
                     

                            }
                            else if (oldPostContactListWithOutPCMRemote[i].ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                            {
                                // اتصالی های پی سی ام با تغییر پست به پست جدید منتقل می شوند
                                List<PostContact> pastOldPostContactPCMOutputList = new List<PostContact>();
                                if (_translationPost.OverallTransfer == true)
                                {
                                   pastOldPostContactPCMOutputList = oldPostContactList.Where(t => t.ConnectionNo == oldPostContactListWithOutPCMRemote[i].ConnectionNo).ToList();
                                }
                                else
                                {
                                    pastOldPostContactPCMOutputList = Data.PostContactDB.GetPostContactByPostID(oldPostContactListWithOutPCMRemote.SingleOrDefault().PostID, oldPostContactListWithOutPCMRemote.SingleOrDefault().ConnectionNo);
                                    pastOldPostContactPCMOutputList.ForEach(t => t.ConnectionNo = newPostContactList[i].ConnectionNo);
                                    newPostContactList[i].ConnectionNo = oldPostContactListWithOutPCMRemote.SingleOrDefault().ConnectionNo;
                                }

                                pastOldPostContactPCMOutputList.ForEach(item => 
                                {
                                   

                                    item.PostID = _translationPost.NewPostID; 
                                    item.Detach();
                                });

                                newPostContactList[i].PostID = _translationPost.OldPostID;
                                newPostContactList[i].Detach();
                                DB.Save(newPostContactList[i]);

                                DB.UpdateAll(pastOldPostContactPCMOutputList);
                            }

                            worker.ReportProgress(i * 100 / count);
                        }

                        _translationPost.CompletionDate = currentDataTime;
                        _translationPost.Detach();
                        DB.Save(_translationPost, false);

                        ts3.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
              MessageBox.Show("خطا در عملیات برگردان" + ex.ToString());
            }
        }


        private void wiringButtom_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

