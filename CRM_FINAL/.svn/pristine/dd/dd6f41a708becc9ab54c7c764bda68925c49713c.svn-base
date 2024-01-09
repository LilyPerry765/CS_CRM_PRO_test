using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using System;
using System.Collections;
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
    public partial class TranslationPostNetworkForm : Local.RequestFormBase
    {

        CRM.Application.UserControls.TranslationPostInfo _translationPostInfo;
        private long _requestID = 0;
        private Request request { get; set; }

        List<PostContact> oldPostContactList;
        List<PostContact> oldPostContactListWithOutPCMRemote;
        List<PostContact> newPostContactList;

        private CRM.Data.TranslationPost _translationPost { get; set; }

        public TranslationPostNetworkForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Print, (byte)DB.NewAction.Deny };
        }
        public TranslationPostNetworkForm(long reqeustID):this()
        {
            _requestID = reqeustID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            _translationPost = Data.TranslationPostDB.GetTranslationPostByID(_requestID);
            if (_translationPost.AccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _translationPost.AccomplishmentDate = dateTime.Date;
                _translationPost.AccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }
           
            request = Data.RequestDB.GetRequestByID(_requestID);

            //StatusComboBox.ItemsSource = DB.GetStepStatus(request.RequestTypeID, this.currentStep);
            //StatusComboBox.SelectedValue = request.StatusID;

            _translationPostInfo = new UserControls.TranslationPostInfo(_requestID);
            TranslationInfo.Content = _translationPostInfo;
            _translationPostInfo.DataContext = _translationPostInfo;


            AccomplishmentGroupBox.DataContext = _translationPost;
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
                 
                   // request.StatusID = (int)StatusComboBox.SelectedValue;
                    request.Detach();
                    DB.Save(request, false);

                    _translationPost.Detach();
                    DB.Save(_translationPost , false);

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
                if (_translationPost.CompletionDate == null)
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

        public override bool Print()
        {
            //CRM.Data.TranslationPost _translationPost = Data.TranslationPostDB.GetTranslationPostByID(_requestID);

            //Post FromPost = Data.PostDB.GetPostByID(_translationPost.OldPostID);
            //Post ToPost = Data.PostDB.GetPostByID(_translationPost.NewPostID);

            //Cabinet FromCabinet = Data.CabinetDB.GetCabinetByID(_translationPost.OldCabinetID);
            ////Cabinet ToCabinet = Data.CabinetDB.GetCabinetByID(_translationPost.NewCabinetID);

            //List<TranslatoionPostWiring> Result = new List<TranslatoionPostWiring>();
            //TranslatoionPostWiring ResultFalse = new TranslatoionPostWiring();

            //ResultFalse.fromCabinet = FromCabinet.CabinetNumber.ToString();
            //ResultFalse.toCabinet = FromCabinet.CabinetNumber.ToString();
            //ResultFalse.fromPost = FromPost.Number.ToString();
            //ResultFalse.toPost = ToPost.Number.ToString();

            //if (_translationPost.OverallTransfer == false)
            //{
            //    ResultFalse.OldConnectionNo = Data.PostContactDB.GetPostContactByID((long)_translationPost.OldPostContactID).ConnectionNo;
            //    ResultFalse.NewConnectionNo = Data.PostContactDB.GetPostContactByID((long)_translationPost.NewPostContactID).ConnectionNo;
            //    Result.Add(ResultFalse);
            //}
            //else if (_translationPost.OverallTransfer == true)
            //{
            //    List<int> OldConnectionNoList = Data.PostContactDB.GetPostContactNoByPostID(_translationPost.OldPostID);
            //    List<int> NewConnectionNoList = Data.PostContactDB.GetPostContactNoByPostID(_translationPost.NewPostID);


            //    for (int i = 0; i < OldConnectionNoList.Count(); i++)
            //    {
            //        TranslatoionPostWiring Element = new TranslatoionPostWiring();
            //        Element.fromCabinet = FromCabinet.CabinetNumber.ToString();
            //        Element.toCabinet = FromCabinet.CabinetNumber.ToString();
            //        Element.fromPost = FromPost.Number.ToString();
            //        Element.toPost = ToPost.Number.ToString();
            //        Element.OldConnectionNo = OldConnectionNoList[i];
            //        AssignmentInfo assingnmentInfo = DB.GetAllInformationPostContactID(OldConnectionNoList[i]);
            //        if (assingnmentInfo != null)
            //            Element.TelephoneNo = assingnmentInfo.TelePhoneNo;
            //        Result.Add(Element);
            //    }

            //    for (int i = 0; i < NewConnectionNoList.Count(); i++)
            //    {

            //        Result[i].toPost = ToPost.Number.ToString();
            //        Result[i].NewConnectionNo = NewConnectionNoList[i];
            //        Result[i].toCabinet = FromCabinet.CabinetNumber.ToString();

            //        //TranslatoionPostWiring Element = new TranslatoionPostWiring();
            //        //Element.fromCabinet = FromCabinet.CabinetNumber.ToString();
            //        ////Element.toCabinet = ToCabinet.CabinetNumber.ToString();
            //        //Element.fromPost = FromPost.Number.ToString();
            //        //Element.toPost = ToPost.Number.ToString();
            //        //Element.NewConnectionNo = NewConnectionNoList[i];
            //        //Result.Add(Element);
            //    }
            //}
            List<uspReportTransferPostResult> result = ReportDB.GetTranslationPost(new List<long> { _requestID });
            SendToPrint(result);
            return IsPrintSuccess;
        }
        private void SendToPrint(List<uspReportTransferPostResult> Result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.PostTranslationWiring);
            stiReport.Load(path);

            stiReport.CacheAllData = true;
         
            stiReport.RegData("result", "result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }


        


        #region Worker
        private void DoWork()
        {
            try
            {
                if (_translationPost.CompletionDate == null)
                {
                    List<RequestLog> requestLogs = new List<RequestLog>();
                    DateTime currentDataTime = DB.GetServerDate();
                    using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
                    {

                        Data.TranslationPostDB.VerifyData(_translationPost, this.currentStat);



                        if (_translationPost.OverallTransfer == true)
                        {


                            // change waiting list
                            List<InvestigatePossibilityWaitngListChangeInfo> investigatePossibilityWaitngListChangeInfo = new List<InvestigatePossibilityWaitngListChangeInfo>();

                            if (_translationPost.TransferWaitingList)
                            {
                                investigatePossibilityWaitngListChangeInfo.Add(
                                        new InvestigatePossibilityWaitngListChangeInfo
                                        {
                                            newCabinetID = _translationPost.OldCabinetID,
                                            newPostID = _translationPost.NewPostID,
                                            oldCabinetID = _translationPost.OldCabinetID,
                                            oldPostID = _translationPost.OldPostID,
                                        });


                                InvestigatePossibilityDB.ChangePostInvestigatePossibilityWaitingList(investigatePossibilityWaitngListChangeInfo);
                            }

                            // Transfer Broken PostContact
                            if (_translationPost.TransferBrokenPostContact)
                            {

                                List<PostContactInfo> BrokenOldPostContactList = Data.PostContactDB.GetBrokenPostContactByCabinetID(_translationPost.OldCabinetID,new List<int>{ _translationPost.OldPostID});
                                List<PostContact> newBrokenPostContactList = Data.PostContactDB.GetFreePostContactByCabinetID(_translationPost.OldCabinetID, new List<int>{_translationPost.NewPostID});
                                BrokenOldPostContactList.ForEach(t =>
                                {
                                    int postID = _translationPost.NewPostID;
                                    if (newBrokenPostContactList.Any(t3 => t3.PostID == postID && t3.ConnectionNo == t.ConnectionNo))
                                    {
                                        newBrokenPostContactList.Where(t3 => t3.PostID == postID && t3.ConnectionNo == t.ConnectionNo).SingleOrDefault().Status = t.Status;
                                    }

                                });

                                newBrokenPostContactList.ForEach(t => t.Detach());
                                DB.UpdateAll(newBrokenPostContactList);
                            }
                            //

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

                            RequestLog requestLog = new RequestLog();
                            requestLog.IsReject = false;
                            requestLog.RequestID = request.ID;
                            requestLog.RequestTypeID = request.RequestTypeID;
                            requestLog.UserID = DB.CurrentUser.ID;



                            if (oldPostContactListWithOutPCMRemote[i].ConnectionType == (int)DB.PostContactConnectionType.Noraml && oldPostContactListWithOutPCMRemote[i].Status == (int)DB.PostContactStatus.CableConnection)
                            {
                                // save log
                                AssignmentInfo assingnmentInfo = DB.GetAllInformationWithoutPCMByPostContactID(oldPostContactListWithOutPCMRemote[i].ID);
                                requestLog.TelephoneNo = assingnmentInfo.TelePhoneNo;
                                requestLog.CustomerID = assingnmentInfo.Customer.CustomerID;

                                Data.Schema.TranslationPost translationPost = new Data.Schema.TranslationPost();

                                translationPost.OldCabinet = assingnmentInfo.CabinetName ?? 0;
                                translationPost.OldPost = assingnmentInfo.PostName ?? 0;
                                translationPost.OldPostContact = oldPostContactListWithOutPCMRemote[i].ConnectionNo;


                                translationPost.NewCabinet = assingnmentInfo.CabinetName ?? 0;
                                translationPost.NewPost = Data.PostDB.GetPostByID(newPostContactList[i].PostID).Number;
                                translationPost.NewPostContact = newPostContactList[i].ConnectionNo;

                                requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.TranslationPost>(translationPost, true));

                                requestLog.Date = currentDataTime;
                                requestLog.Detach();

                                requestLogs.Add(requestLog);

                                //

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
                                    // save log
                                    AssignmentInfo assingnmentInfo = DB.GetAllInformationPostContactID(item.ID  , (byte)DB.BuchtType.InLine);
                                    if (assingnmentInfo != null && assingnmentInfo.TelePhoneNo != null)
                                    {
                                        requestLog.TelephoneNo = assingnmentInfo.TelePhoneNo;
                                        requestLog.CustomerID = assingnmentInfo.Customer.CustomerID;

                                        Data.Schema.TranslationPost translationPost = new Data.Schema.TranslationPost();

                                        translationPost.OldCabinet = assingnmentInfo.CabinetName ?? 0;
                                        translationPost.OldPost = assingnmentInfo.PostName ?? 0;
                                        translationPost.OldPostContact = oldPostContactListWithOutPCMRemote[i].ConnectionNo;


                                        translationPost.NewCabinet = assingnmentInfo.CabinetName ?? 0;
                                        translationPost.NewPost = Data.PostDB.GetPostByID(newPostContactList[i].PostID).Number;
                                        translationPost.NewPostContact = newPostContactList[i].ConnectionNo;

                                        requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.TranslationPost>(translationPost, true));

                                        requestLog.Date = currentDataTime;
                                        requestLog.Detach();

                                        requestLogs.Add(requestLog);
                                    }
                                    //
                                    item.PostID = _translationPost.NewPostID;
                                    item.Detach(); 
                                });

                                newPostContactList[i].PostID = _translationPost.OldPostID;
                                newPostContactList[i].Detach();
                                DB.Save(newPostContactList[i]);



                                DB.UpdateAll(pastOldPostContactPCMOutputList);
                            }
                        }

                        _translationPost.CompletionDate = currentDataTime;
                        _translationPost.Detach();
                        DB.Save(_translationPost, false);

                        DB.SaveAll(requestLogs);

                        ts3.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
