using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using System;
using System.Collections;
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
    /// Interaction logic for ExchangePostNetworkForm.xaml
    /// </summary>
    public partial class TranslationPostInputMDFForm : Local.RequestFormBase
    {

        CRM.Application.UserControls.TranslationPostInputInfo _translationPostInputInfo;
        private long _requestID = 0;
        private Request request { get; set; }
        private CRM.Data.TranslationPostInput _translationPostInput { get; set; }

        public TranslationPostInputMDFForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Print, (byte)DB.NewAction.Deny };
        }
        public TranslationPostInputMDFForm(long reqeustID)
            : this()
        {
            _requestID = reqeustID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {



            _translationPostInput = Data.TranslationPostInputDB.GetTranslationPostInputByID(_requestID);
            if (_translationPostInput.MDFAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _translationPostInput.MDFAccomplishmentDate = dateTime.Date;
                _translationPostInput.MDFAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }
           
            request = Data.RequestDB.GetRequestByID(_requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = request.StatusID;

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

                    request.StatusID = (int)StatusComboBox.SelectedValue;
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

        public override bool Print()
        {
           List< uspReportMDFExchangeCabinuteInputCentralResult> Result = ReportDB.GetTranslationPostInputMDF(new List<long>{_requestID});
           SendToPrint(Result);
           return true;
        }
        private void SendToPrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.MDFTranslationPostInput);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", result);
 
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
    }
}


