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
    public partial class TranslationPostInputNetworkForm : Local.RequestFormBase
    {

        #region properties
        CRM.Application.UserControls.TranslationPostInputInfo _translationPostInputInfo;
        private long _requestID = 0;
        private Request request { get; set; }
        private CRM.Data.TranslationPostInput _translationPostInput { get; set; }

        #endregion properties

        #region Constractor
        public TranslationPostInputNetworkForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Print, (byte)DB.NewAction.Deny };
        }
        public TranslationPostInputNetworkForm(long reqeustID)
            : this()
        {
            _requestID = reqeustID;
        }

        #endregion Constractor

        #region Method

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {



            _translationPostInput = Data.TranslationPostInputDB.GetTranslationPostInputByID(_requestID);
            if (_translationPostInput.NetworkAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _translationPostInput.NetworkAccomplishmentDate = dateTime.Date;
                _translationPostInput.NetworkAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }

            request = Data.RequestDB.GetRequestByID(_requestID);

            //StatusComboBox.ItemsSource = DB.GetStepStatus(request.RequestTypeID, this.currentStep);
            //StatusComboBox.SelectedValue = request.StatusID;

            _translationPostInputInfo = new UserControls.TranslationPostInputInfo(_requestID);
            TranslationInfo.Content = _translationPostInputInfo;
            TranslationInfo.DataContext = _translationPostInputInfo;


            AccomplishmentGroupBox.DataContext = _translationPostInput;
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
                if (_translationPostInput.DateOfFinal == null)
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
            try
            {
                if (_translationPostInput.DateOfFinal == null)
                {
                    List<RequestLog> requestLogs = new List<RequestLog>();
                    DateTime currentDataTime = DB.GetServerDate();
                    using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
                    {

                        RequestLog requestLog = new RequestLog();
                        requestLog.IsReject = false;
                        requestLog.RequestID = request.ID;
                        requestLog.RequestTypeID = request.RequestTypeID;
                        requestLog.UserID = DB.CurrentUser.ID;


                        List<TranslationPostInputConnection> translationPostInputConnection = Data.TranslationPostInputDB.GetTranslationPostInputConectionByRequestID(request.ID);
                        LeaveReserv(translationPostInputConnection);

                        Post fromPost = Data.PostDB.GetPostByID(_translationPostInput.FromPostID);
                        Post toPost = Data.PostDB.GetPostByID(_translationPostInput.ToPostID);

                        Cabinet fromCabinet = Data.CabinetDB.GetCabinetByID(_translationPostInput.FromCabinetID);
                        Cabinet toCabinet = Data.CabinetDB.GetCabinetByID(_translationPostInput.ToCabinetID);

                        List<PostContact> oldPostContact = Data.PostContactDB.GetPostContactByIDs(translationPostInputConnection.Select(t => t.ConnectionID).ToList());
                        List<Bucht> oldBucht = Data.BuchtDB.GetBuchtByConnectionIDs(translationPostInputConnection.Select(t => t.ConnectionID).ToList());
                        //List<CabinetInput> oldCabinetInput = Data.CabinetInputDB.GetCabinetInputByIDs(oldBucht.Select(t=>(long)t.CabinetInputID).ToList());


                        List<PostContact> newPostContact = Data.PostContactDB.GetPostContactByIDs(translationPostInputConnection.Select(t => t.NewConnectionID).ToList());
                        List<Bucht> newBucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(translationPostInputConnection.Select(t => t.CabinetInputID).ToList());

                        int count = translationPostInputConnection.Count;
                        if (fromPost.ID == toPost.ID && translationPostInputConnection.All(t=> t.NewConnectionID == 0))
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

                            };

                        }
                        else
                        {

                            for (int i = 0; i < count; i++)
                            {
                                if (oldPostContact.Where(t => t.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                                {
                                    // save log

                                    AssignmentInfo assingnmentInfo = DB.GetAllInformationPostContactID(translationPostInputConnection[i].ConnectionID , (byte)DB.BuchtType.CustomerSide);
                                    requestLog.TelephoneNo = assingnmentInfo.TelePhoneNo;
                                    requestLog.CustomerID = assingnmentInfo.Customer.CustomerID;

                                    Data.Schema.TranslationPostInput translationPostInput = new Data.Schema.TranslationPostInput();

                                    translationPostInput.OldCabinet = assingnmentInfo.CabinetName ?? 0;
                                    
                                    translationPostInput.OldPost = assingnmentInfo.PostName ?? 0;
                                    translationPostInput.OldPostContact = oldPostContact.Where(t2 => t2.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionNo; ;


                                    translationPostInput.NewCabinet = assingnmentInfo.CabinetName ?? 0;
                                          translationPostInput.NewPost = Data.PostDB.GetPostByID(newPostContact.Where(t => t.ID == translationPostInputConnection[i].NewConnectionID).SingleOrDefault().PostID).Number;
                                    translationPostInput.NewPostContact = newPostContact.Where(t => t.ID == translationPostInputConnection[i].NewConnectionID).SingleOrDefault().ConnectionNo;

                                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.TranslationPostInput>(translationPostInput, true));
                                    requestLog.Date = currentDataTime;
                                    requestLog.Detach();

                            

                                    //

                                    newBucht.Where(t => t.CabinetInputID == translationPostInputConnection[i].CabinetInputID).ToList().ForEach(t =>
                                    {
                                        translationPostInput.NewCabinetInput = Data.CabinetDB.GetCabinetNumberByCabinetInputID((long)t.CabinetInputID);
                             
                                        t.SwitchPortID = oldBucht.Where(t2 => t2.ConnectionID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().SwitchPortID;
                                        t.BuchtIDConnectedOtherBucht = oldBucht.Where(t2 => t2.ConnectionID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().BuchtIDConnectedOtherBucht;
                                        t.Status = (int)DB.BuchtStatus.Connection;
                                        t.ConnectionID = translationPostInputConnection[i].NewConnectionID;
                                      //  t.ConnectionID = newPostContact.Where(t3 => t3.ConnectionNo == oldPostContact.Where(t4 => t4.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionNo).SingleOrDefault().ID;
                                    });

                                    newPostContact.Where(t => t.ID == translationPostInputConnection[i].NewConnectionID ).SingleOrDefault().Status = oldPostContact.Where(t2 => t2.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().Status;

                                    oldPostContact.Where(t2 => t2.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().Status = (byte)DB.PostContactStatus.Free;

                                    oldBucht.Where(t => t.ConnectionID == translationPostInputConnection[i].ConnectionID).ToList().ForEach(t =>
                                    {
                                        translationPostInput.OldCabinetInput = Data.CabinetDB.GetCabinetNumberByCabinetInputID((long)t.CabinetInputID);

                                        t.Status = (byte)DB.BuchtStatus.Free;
                                        t.ADSLStatus = false;
                                        t.SwitchPortID = null;
                                        t.ConnectionID = null;
                                        t.BuchtIDConnectedOtherBucht = null;
                                    });


                                    requestLogs.Add(requestLog);
                                }
                                else if (oldPostContact.Where(t => t.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)
                                {
                                    List<PostContact> pastOldPostContactPCMOutputList = new List<PostContact>();
                                    pastOldPostContactPCMOutputList = Data.PostContactDB.GetPostContactByPostID(fromPost.ID, oldPostContact.Where(t2 => t2.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionNo).Where(t => t.ConnectionType != (byte)DB.PostContactConnectionType.PCMRemote).ToList();
                                    pastOldPostContactPCMOutputList.ForEach(item =>
                                    {

                                        AssignmentInfo assingnmentInfo = DB.GetAllInformationPostContactID(item.ID , (byte)DB.BuchtType.InLine);
                                        if (assingnmentInfo.TelePhoneNo != null)
                                        {
                                            requestLog.TelephoneNo = assingnmentInfo.TelePhoneNo;
                                            requestLog.CustomerID = assingnmentInfo.Customer.CustomerID;

                                            Data.Schema.TranslationPostInput translationPostInput = new Data.Schema.TranslationPostInput();

                                            translationPostInput.OldCabinet = assingnmentInfo.CabinetName ?? 0;
                                            translationPostInput.OldPost = assingnmentInfo.PostName ?? 0;
                                            translationPostInput.OldPostContact = item.ConnectionNo;


                                            translationPostInput.NewCabinet = assingnmentInfo.CabinetName ?? 0;
                                            translationPostInput.NewPost = Data.PostDB.GetPostByID(newPostContact.Where(t => t.ID == translationPostInputConnection[i].NewConnectionID).SingleOrDefault().PostID).Number;
                                            translationPostInput.NewPostContact = newPostContact.Where(t => t.ID == translationPostInputConnection[i].NewConnectionID).SingleOrDefault().ConnectionNo;

                                            requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.TranslationPostInput>(translationPostInput, true));

                                            requestLog.Date = currentDataTime;
                                            requestLog.Detach();

                                            requestLogs.Add(requestLog);
                                        }

                                        item.PostID = toPost.ID;
                                        item.ConnectionNo = newPostContact.Where(t => t.ID == translationPostInputConnection[i].NewConnectionID).SingleOrDefault().ConnectionNo;
                                        item.Detach();
                                    });


                                    int oldConnectionNo = oldPostContact.Where(t => t.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionNo;

                                    oldPostContact.Where(t => t.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().PostID = toPost.ID;
                                    oldPostContact.Where(t => t.ID == translationPostInputConnection[i].ConnectionID).SingleOrDefault().ConnectionNo = newPostContact.Where(t => t.ID == translationPostInputConnection[i].NewConnectionID).SingleOrDefault().ConnectionNo;

                                    newPostContact.Where(t => t.ID == translationPostInputConnection[i].NewConnectionID).SingleOrDefault().PostID = fromPost.ID;
                                    newPostContact.Where(t => t.ID == translationPostInputConnection[i].NewConnectionID).SingleOrDefault().ConnectionNo = oldConnectionNo;

                                    List<Bucht> oldPcmBucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(new List<long> { (long)oldBucht.Where(t => t.ConnectionID == translationPostInputConnection[i].ConnectionID).Take(1).SingleOrDefault().CabinetInputID }).Where(t => t.BuchtTypeID == (byte)DB.BuchtType.InLine).ToList();
                                    oldPcmBucht.ForEach(t => { t.CabinetInputID = translationPostInputConnection[i].CabinetInputID; t.Detach(); });


                                    newBucht.Where(t => t.CabinetInputID == translationPostInputConnection[i].CabinetInputID).ToList().ForEach(t =>
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

        private void LeaveReserv(List<TranslationPostInputConnection> translationPostInputConnection)
        {
            using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
            {

                List<CabinetInput> cabinetInputs = Data.CabinetInputDB.GetCabinetInputByIDs(translationPostInputConnection.Select(t => t.CabinetInputID).ToList());
                cabinetInputs.ForEach(t => { t.Status = (byte)DB.CabinetInputStatus.healthy; t.Detach(); });
                DB.UpdateAll(cabinetInputs);

                List<PostContact> postContact = Data.PostContactDB.GetPostContactByIDs(translationPostInputConnection.Select(t => t.ConnectionID).ToList());
                postContact.ForEach(t => { t.Status = (byte)DB.PostContactStatus.CableConnection; t.Detach(); });
                DB.UpdateAll(postContact);

                List<PostContact> newPostContact = Data.PostContactDB.GetPostContactByIDs(translationPostInputConnection.Select(t => t.NewConnectionID).ToList());
                newPostContact.ForEach(t => { t.Status = (byte)DB.PostContactStatus.Free; t.Detach(); });
                DB.UpdateAll(newPostContact);

                ts3.Complete();
            }
        }

        #endregion Method

        #region Print
        public override bool Print()
        {
            List<uspReportNetworkWireExchangeCentralPostResult> Result = ReportDB.GetNetworkWireExchangeCentralPost(new List<long> { _requestID });
            SendToPrint(Result);
            return true;
        }
        private void SendToPrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.NetworkWireExchangeCentralPostReport);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        #endregion Print

   

    }
}

