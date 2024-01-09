using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TranslationPCMToNormalNetworkForm.xaml
    /// </summary>
    public partial class TranslationPCMToNormalNetworkForm : Local.RequestFormBase
    {
        TranslationPCMToNormal _translationPCMToNormal { get; set; }
        Request _reqeust { get; set; }

        private long _RequestID;

        PostContact postContact   { get; set; }
        Post post      { get; set; }           
        CabinetInput cabinetInput { get; set; }
        Cabinet cabinet { get; set; }

        bool error = false;

        AssignmentInfo oldAssingmentInfo { get; set; }
        public TranslationPCMToNormalNetworkReportInfo translationPCMToNormalNetworkReportInfo = new TranslationPCMToNormalNetworkReportInfo();

        public TranslationPCMToNormalNetworkForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }

        public TranslationPCMToNormalNetworkForm(long requestID) : this()
        {
            this._RequestID = requestID;
        }


        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            _translationPCMToNormal = Data.TranslationPCMToNormalDB.GetTranslationPCMToNormalByID(_RequestID);
            translationPCMToNormalNetworkReportInfo.Type = TypeTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.TranslationPCMToNormalType), _translationPCMToNormal.Type);

            if (_translationPCMToNormal.NetworkAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _translationPCMToNormal.NetworkAccomplishmentDate = dateTime.Date;
                _translationPCMToNormal.NetworkAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }

            _reqeust = Data.RequestDB.GetRequestByID(_RequestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(_reqeust.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = _reqeust.StatusID;

            TelephoneTextBox.Text = _reqeust.TelephoneNo.ToString();
            oldAssingmentInfo = DB.GetAllInformationByTelephoneNo((long)_reqeust.TelephoneNo);

            translationPCMToNormalNetworkReportInfo.CustomerName = oldAssingmentInfo.CustomerName.ToString();
            translationPCMToNormalNetworkReportInfo.CustomerAddress = oldAssingmentInfo.Address.ToString();
            translationPCMToNormalNetworkReportInfo.PostalCode = oldAssingmentInfo.PostallCode.ToString();
            translationPCMToNormalNetworkReportInfo.Telephone = oldAssingmentInfo.TelePhoneNo;
            

            try
            { 
            if (_translationPCMToNormal.Type == (byte)DB.TranslationPCMToNormalType.PCMToNormal)
            {


                translationPCMToNormalNetworkReportInfo.BeforCabinet = BeforCabinetTextBox.Text = oldAssingmentInfo.CabinetName.ToString();
                translationPCMToNormalNetworkReportInfo.BeforCabinetInput = BeforCabinetInputTextBox.Text = oldAssingmentInfo.InputNumber.ToString();
                translationPCMToNormalNetworkReportInfo.BeforPost = BeforPostTextBox.Text = oldAssingmentInfo.PostName.ToString();
                translationPCMToNormalNetworkReportInfo.BeforPostContact = BeforPostContactTextBox.Text = oldAssingmentInfo.PostContact.ToString();
                translationPCMToNormalNetworkReportInfo.BeforMUID = BeforMUIDTextBox.Text = oldAssingmentInfo.MUID.ToString();

               postContact  = Data.PostContactDB.GetPostContactByID(_translationPCMToNormal.NewPostContactID);
               post   = Data.PostDB.GetPostByID(postContact.PostID);

               cabinetInput = Data.CabinetInputDB.GetCabinetInputByID((long)_translationPCMToNormal.CabinetInputID);
               cabinet  = Data.CabinetDB.GetCabinetByID(cabinetInput.CabinetID);


               translationPCMToNormalNetworkReportInfo.AfterCabinet = AfterCabinetTextBox.Text = cabinet.CabinetNumber.ToString();
               translationPCMToNormalNetworkReportInfo.AfterCabinetInput = AfterCabinetInputTextBox.Text = cabinetInput.InputNumber.ToString();
               translationPCMToNormalNetworkReportInfo.AfterPost = AfterPostTextBox.Text = post.Number.ToString();
               translationPCMToNormalNetworkReportInfo.AfterPostContact = AfterPostContactTextBox.Text = postContact.ConnectionNo.ToString();

               AfterMUIDLabel.Visibility = Visibility.Collapsed;
               AfterMUIDTextBox.Visibility = Visibility.Collapsed;
                
            }
            else if (_translationPCMToNormal.Type == (byte)DB.TranslationPCMToNormalType.NormalToPCM)
            {

                translationPCMToNormalNetworkReportInfo.BeforCabinet = BeforCabinetTextBox.Text = oldAssingmentInfo.CabinetName.ToString();
                translationPCMToNormalNetworkReportInfo.BeforCabinetInput = BeforCabinetInputTextBox.Text = oldAssingmentInfo.InputNumber.ToString();
                translationPCMToNormalNetworkReportInfo.BeforPost = BeforPostTextBox.Text = oldAssingmentInfo.PostName.ToString();
                translationPCMToNormalNetworkReportInfo.BeforPostContact = BeforPostContactTextBox.Text = oldAssingmentInfo.PostContact.ToString();
                BeforMUIDLabel.Visibility = Visibility.Collapsed;
                BeforMUIDTextBox.Visibility = Visibility.Collapsed;


                postContact = Data.PostContactDB.GetPostContactByID(_translationPCMToNormal.NewPostContactID);
                post = Data.PostDB.GetPostByID(postContact.PostID);




                Bucht bucht = Data.BuchtDB.GetBuchtByConnectionID(_translationPCMToNormal.NewPostContactID);
                cabinetInput = Data.CabinetInputDB.GetCabinetInputByID((long)bucht.CabinetInputID);
                cabinet = Data.CabinetDB.GetCabinetByID(cabinetInput.CabinetID);

                translationPCMToNormalNetworkReportInfo.AfterCabinet = AfterCabinetTextBox.Text = cabinet.CabinetNumber.ToString();
                translationPCMToNormalNetworkReportInfo.AfterCabinetInput = AfterCabinetInputTextBox.Text = cabinetInput.InputNumber.ToString();
                translationPCMToNormalNetworkReportInfo.AfterPost = AfterPostTextBox.Text = post.Number.ToString();
                translationPCMToNormalNetworkReportInfo.AfterPostContact = AfterPostContactTextBox.Text = postContact.ConnectionNo.ToString();


                AssignmentInfo newAssingmentInfo = DB.GetAllInformationPostContactID((long)_translationPCMToNormal.NewPostContactID, (byte)DB.BuchtType.InLine);
                translationPCMToNormalNetworkReportInfo.AfterMUID = AfterMUIDTextBox.Text = newAssingmentInfo.MUID;

            }
            else if (_translationPCMToNormal.Type == (byte)DB.TranslationPCMToNormalType.PCMToPCM)
            {
                translationPCMToNormalNetworkReportInfo.BeforCabinet = BeforCabinetTextBox.Text = oldAssingmentInfo.CabinetName.ToString();
                translationPCMToNormalNetworkReportInfo.BeforCabinetInput = BeforCabinetInputTextBox.Text = oldAssingmentInfo.InputNumber.ToString();
                translationPCMToNormalNetworkReportInfo.BeforPost = BeforPostTextBox.Text = oldAssingmentInfo.PostName.ToString();
                translationPCMToNormalNetworkReportInfo.BeforPostContact = BeforPostContactTextBox.Text = oldAssingmentInfo.PostContact.ToString();
                translationPCMToNormalNetworkReportInfo.BeforMUID = BeforMUIDTextBox.Text = oldAssingmentInfo.MUID.ToString();


                postContact = Data.PostContactDB.GetPostContactByID(_translationPCMToNormal.NewPostContactID);
                post = Data.PostDB.GetPostByID(postContact.PostID);

                Bucht bucht = Data.BuchtDB.GetBuchtByConnectionID(_translationPCMToNormal.NewPostContactID);
                cabinetInput = Data.CabinetInputDB.GetCabinetInputByID((long)bucht.CabinetInputID);
                cabinet = Data.CabinetDB.GetCabinetByID(cabinetInput.CabinetID);

                translationPCMToNormalNetworkReportInfo.AfterCabinet = AfterCabinetTextBox.Text = cabinet.CabinetNumber.ToString();
                translationPCMToNormalNetworkReportInfo.AfterCabinetInput = AfterCabinetInputTextBox.Text = cabinetInput.InputNumber.ToString();
                translationPCMToNormalNetworkReportInfo.AfterPost = AfterPostTextBox.Text = post.Number.ToString();
                translationPCMToNormalNetworkReportInfo.AfterPostContact = AfterPostContactTextBox.Text = postContact.ConnectionNo.ToString();

                AssignmentInfo newAssingmentInfo = DB.GetAllInformationPostContactID((long)_translationPCMToNormal.NewPostContactID, (byte)DB.BuchtType.InLine);
                translationPCMToNormalNetworkReportInfo.AfterMUID = AfterMUIDTextBox.Text = newAssingmentInfo.MUID;

            }
            }
            catch (Exception ex)
            {
                error = true;
                Folder.MessageBox.ShowError("در بازیابی اطلاعات خطای رخ داد. لطفا با پشتیبان سیستم تماس بگیرید");
            }

            AccomplishmentGroupBox.DataContext = _translationPCMToNormal;

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
                if (error == true)
                    throw new Exception("اطلاعات پی سی ام دارای خطا می باشد");
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {
                    _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                    _reqeust.Detach();
                    DB.Save(_reqeust, false);

                    _translationPCMToNormal = AccomplishmentGroupBox.DataContext as TranslationPCMToNormal;
                    _translationPCMToNormal.Detach();
                    DB.Save(_translationPCMToNormal, false);

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

        public override bool Forward()
        {

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    Save();
                    this.RequestID = _reqeust.ID;
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

        private void DoWork()
        {
          using (TransactionScope tsi = new TransactionScope(TransactionScopeOption.Required))
           {

           Status Status = Data.StatusDB.GetStatueByStatusID(_reqeust.StatusID);
           if (Status != null && Status.StatusType == (byte)DB.RequestStatusType.Completed && _translationPCMToNormal.CompletionDate == null)
           {
               _translationPCMToNormal.CompletionDate = DB.GetServerDate();

               RequestLog requestLog = new RequestLog();
               requestLog.IsReject = false;
               requestLog.RequestID = _reqeust.ID;
               requestLog.RequestTypeID = (int)DB.RequestType.PCMToNormal;
               requestLog.TelephoneNo = _reqeust.TelephoneNo;
               requestLog.CustomerID = oldAssingmentInfo.Customer.CustomerID;
               requestLog.UserID = DB.currentUser.ID;
               requestLog.Date = DB.GetServerDate();

               Data.Schema.PCMToNormal PCMToNormalSchema = new Data.Schema.PCMToNormal();
               PCMToNormalSchema.Type = TypeTextBox.Text;

               if (_translationPCMToNormal.Type == (byte)DB.TranslationPCMToNormalType.PCMToNormal)
               {

                   Bucht newBucht = Data.BuchtDB.GetBuchtByCabinetInputID((long)cabinetInput.ID);
                   if (newBucht == null || newBucht.ID == 0)
                       throw new Exception("بوخت مرکزی جدید یافت نشد");

                   PCMToNormalSchema.NewBuchtID = newBucht.ID;
                   PCMToNormalSchema.NewConnectionNo = DB.GetConnectionByBuchtID(newBucht.ID);

                   Bucht oldBucht = Data.BuchtDB.GetBuchtByID((long)oldAssingmentInfo.BuchtID);
                   if (oldBucht == null || oldBucht.ID == 0)
                       throw new Exception("بوخت قدیم یافت نشد");

                   PCMToNormalSchema.OldBuchtID = oldBucht.ID;
                   PCMToNormalSchema.OldConnectionNo = DB.GetConnectionByBuchtID(oldBucht.ID);

                   newBucht.SwitchPortID = oldBucht.SwitchPortID;
                   newBucht.ConnectionID = postContact.ID;
                   newBucht.Status = (byte)DB.BuchtStatus.Connection;
                   newBucht.Detach();
                   DB.Save(newBucht);

                   oldBucht.Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                   oldBucht.SwitchPortID = null;
                   oldBucht.Detach();
                   DB.Save(oldBucht);

                   cabinetInput.Status = (byte)DB.CabinetInputStatus.healthy;


                   postContact.Status = (byte)DB.PostContactStatus.CableConnection;

                   PostContact oldPostContact = Data.PostContactDB.GetPostContactByID((long)oldAssingmentInfo.PostContactID);
                   oldPostContact.Status = (byte)DB.PostContactStatus.Free;

                   PCMToNormalSchema.OldMUID = oldAssingmentInfo.MUID;

                   PCMToNormalSchema.OldCabinet = oldAssingmentInfo.CabinetName ?? 0;
                   PCMToNormalSchema.OldCabinetID = oldAssingmentInfo.CabinetID ?? 0;

                   PCMToNormalSchema.OldCabinetInput = oldAssingmentInfo.InputNumber ?? 0;
                   PCMToNormalSchema.OldCabinetInputID = oldAssingmentInfo.CabinetInputID ?? 0;

                   PCMToNormalSchema.OldPost = oldAssingmentInfo.PostName ?? 0;
                   PCMToNormalSchema.OldPostID = oldAssingmentInfo.PostID ?? 0;

                   PCMToNormalSchema.OldPostContactID = oldAssingmentInfo.PostContactID ?? 0;
                   PCMToNormalSchema.OldPostContact = oldAssingmentInfo.PostContact ?? 0;

                   PCMToNormalSchema.NewCabinet = cabinet.CabinetNumber;
                   PCMToNormalSchema.NewCabinetID = cabinet.ID;

                   PCMToNormalSchema.NewCabinetInput =  cabinetInput.InputNumber;
                   PCMToNormalSchema.NewCabinetInputID = cabinetInput.ID;

                   PCMToNormalSchema.NewPost = post.Number;
                   PCMToNormalSchema.NewPostID = post.ID;

                   PCMToNormalSchema.NewPostContactID = postContact.ID;
                   PCMToNormalSchema.NewPostContact = postContact.ConnectionNo;

                   oldPostContact.Detach();
                   DB.Save(oldPostContact);

                   postContact.Detach();
                   DB.Save(postContact);

                 

                   _translationPCMToNormal.Detach();
                   DB.Save(_translationPCMToNormal);

                   cabinetInput.Detach();
                   DB.Save(cabinetInput);


               }
               else if (_translationPCMToNormal.Type == (byte)DB.TranslationPCMToNormalType.NormalToPCM)
               {

                   Bucht newBucht = Data.BuchtDB.GetBuchtByConnectionID(_translationPCMToNormal.NewPostContactID);
                   Bucht oldBucht = Data.BuchtDB.GetBuchtByID((long)oldAssingmentInfo.BuchtID);
                   PostContact oldPostContact = Data.PostContactDB.GetPostContactByID((long)_translationPCMToNormal.OldPostContactID);

                   newBucht.SwitchPortID = oldBucht.SwitchPortID;
                   newBucht.Status = (byte)DB.BuchtStatus.Connection;
                   newBucht.Detach();
                   DB.Save(newBucht);

                   oldBucht.ConnectionID = null;
                   oldBucht.ADSLStatus = false;
                   oldBucht.Status = (byte)DB.BuchtStatus.Free;
                   oldBucht.SwitchPortID = null;
                   oldBucht.Detach();
                   DB.Save(oldBucht);

                   postContact.Status = (byte)DB.PostContactStatus.CableConnection;
                   oldPostContact.Status = (byte)DB.PostContactStatus.Free;


                   PCMToNormalSchema.NewMUID = AfterMUIDTextBox.Text;

                   PCMToNormalSchema.OldCabinet = oldAssingmentInfo.CabinetName ?? 0;
                   PCMToNormalSchema.OldCabinetID = oldAssingmentInfo.CabinetID ?? 0;

                   PCMToNormalSchema.OldCabinetInput = oldAssingmentInfo.InputNumber ?? 0;
                   PCMToNormalSchema.OldCabinetInputID = oldAssingmentInfo.CabinetInputID ?? 0;

                   PCMToNormalSchema.OldPost = oldAssingmentInfo.PostName ?? 0;
                   PCMToNormalSchema.OldPostID = oldAssingmentInfo.PostID ?? 0;

                   PCMToNormalSchema.OldPostContactID = oldAssingmentInfo.PostContactID ?? 0;
                   PCMToNormalSchema.OldPostContact = oldAssingmentInfo.PostContact ?? 0;

                   PCMToNormalSchema.NewCabinet = cabinet.CabinetNumber;
                   PCMToNormalSchema.NewCabinetID = cabinet.ID;

                   PCMToNormalSchema.NewCabinetInput = cabinetInput.InputNumber;
                   PCMToNormalSchema.NewCabinetInputID = cabinetInput.ID;

                   PCMToNormalSchema.NewPost = post.Number;
                   PCMToNormalSchema.NewPostID = post.ID;

                   PCMToNormalSchema.NewPostContactID = postContact.ID;
                   PCMToNormalSchema.NewPostContact = postContact.ConnectionNo;

                   oldPostContact.Detach();
                   DB.Save(oldPostContact);

                   postContact.Detach();
                   DB.Save(postContact);
               }
               else if (_translationPCMToNormal.Type == (byte)DB.TranslationPCMToNormalType.PCMToPCM)
               {
                   Bucht newBucht = Data.BuchtDB.GetBuchtByConnectionID(_translationPCMToNormal.NewPostContactID);
                   Bucht oldBucht = Data.BuchtDB.GetBuchtByID((long)oldAssingmentInfo.BuchtID);
                   PostContact oldPostContact = Data.PostContactDB.GetPostContactByID((long)_translationPCMToNormal.OldPostContactID);

                   newBucht.SwitchPortID = oldBucht.SwitchPortID;
                   newBucht.Status = (byte)DB.BuchtStatus.Connection;
                   newBucht.Detach();
                   DB.Save(newBucht);

                   oldBucht.Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                   oldBucht.SwitchPortID = null;
                   oldBucht.Detach();
                   DB.Save(oldBucht);

                   postContact.Status = (byte)DB.PostContactStatus.CableConnection;
                   oldPostContact.Status = (byte)DB.PostContactStatus.Free;

                   PCMToNormalSchema.OldMUID = BeforMUIDTextBox.Text;
                   PCMToNormalSchema.NewMUID = AfterMUIDTextBox.Text;

                   PCMToNormalSchema.OldCabinet = oldAssingmentInfo.CabinetName ?? 0;
                   PCMToNormalSchema.OldCabinetID = oldAssingmentInfo.CabinetID ?? 0;

                   PCMToNormalSchema.OldCabinetInput = oldAssingmentInfo.InputNumber ?? 0;
                   PCMToNormalSchema.OldCabinetInputID = oldAssingmentInfo.CabinetInputID ?? 0;

                   PCMToNormalSchema.OldPost = oldAssingmentInfo.PostName ?? 0;
                   PCMToNormalSchema.OldPostID = oldAssingmentInfo.PostID ?? 0;

                   PCMToNormalSchema.OldPostContactID = oldAssingmentInfo.PostContactID ?? 0;
                   PCMToNormalSchema.OldPostContact = oldAssingmentInfo.PostContact ?? 0;

                   PCMToNormalSchema.NewCabinet = cabinet.CabinetNumber;
                   PCMToNormalSchema.NewCabinetID = cabinet.ID;

                   PCMToNormalSchema.NewCabinetInput = cabinetInput.InputNumber;
                   PCMToNormalSchema.NewCabinetInputID = cabinetInput.ID;

                   PCMToNormalSchema.NewPost = post.Number;
                   PCMToNormalSchema.NewPostID = post.ID;

                   PCMToNormalSchema.NewPostContactID = postContact.ID;
                   PCMToNormalSchema.NewPostContact = postContact.ConnectionNo;


                   oldPostContact.Detach();
                   DB.Save(oldPostContact);

                   postContact.Detach();
                   DB.Save(postContact);

               }

               requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.PCMToNormal>(PCMToNormalSchema, true));
               requestLog.Date = DB.GetServerDate();
               requestLog.Detach();
               DB.Save(requestLog, true);

           }
           tsi.Complete();
         }
        }

        public override bool Print()
        {
        
            try
            {
                ObservableCollection<TranslationPCMToNormalNetworkReportInfo> resulat = new ObservableCollection<TranslationPCMToNormalNetworkReportInfo>(new List<TranslationPCMToNormalNetworkReportInfo> { translationPCMToNormalNetworkReportInfo });
                SendToPrintTranslationPCMToNormallNetworkReport(resulat);
                IsPrintSuccess = true;
            }
            catch
            {
                IsPrintSuccess = false;

            }

            return IsPrintSuccess;
        }


        private void SendToPrintTranslationPCMToNormallNetworkReport(ObservableCollection<TranslationPCMToNormalNetworkReportInfo> Result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.TranslationPCMToNormallNetworkReport);
            stiReport.Load(path);

            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        public override bool Deny()
        {

            try
            {
                base.RequestID = _reqeust.ID;
                if (_translationPCMToNormal.CompletionDate == null)
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
    }
}
