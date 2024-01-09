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
    /// Interaction logic for TranslationCabinetMDFFrom.xaml
    /// </summary>
    public partial class TranslationCabinetMDFFrom : Local.RequestFormBase
    {
        CRM.Application.UserControls.TranslationCabinetInfo _translationCabinetInfo;
        private Request request { get; set; }
        Data.ExchangeCabinetInput _exchangeCabinetInput { get; set; }
        private long _requestID = 0;

        public TranslationCabinetMDFFrom()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }
        public TranslationCabinetMDFFrom(long requestID) : this()
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

            StatusComboBox.ItemsSource = DB.GetStepStatus(request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = request.StatusID;

            if (_exchangeCabinetInput.MDFAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _exchangeCabinetInput.MDFAccomplishmentDate = dateTime.Date;
                _exchangeCabinetInput.MDFAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }

            AccomplishmentGroupBox.DataContext = _exchangeCabinetInput;
        }


        public override bool Save()
        {
            try
            {
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {
                    request.StatusID = (int)StatusComboBox.SelectedValue;
                    request.Detach();
                    DB.Save(request, false);


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

        public override bool Deny()
        {

            try
            {
                base.RequestID = _requestID;
                if (_exchangeCabinetInput.CompletionDate == null)
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
            //List<TranslationPostInputMDFInfo> FromOldResult = ReportDB.GetFromOldExchangeCabinetInputInfo(_requestID);
            //List<TranslationPostInputMDFInfo> ToOldResult = ReportDB.GetToOldExchangeCabinetInputInfo(_requestID);
            //List<TranslationPostInputMDFInfo> OldResult = FromOldResult.Union(ToOldResult).ToList();

            //List<TranslationPostInputMDFInfo> FromNewResult = ReportDB.GetFromNewExchangeCabinetInputInfo(_requestID);
            //List<TranslationPostInputMDFInfo> ToNewResult = ReportDB.GetToNewExchangeCabinetInputInfo(_requestID);
            //List<TranslationPostInputMDFInfo> NewResult = FromNewResult.Union(ToNewResult).ToList();

            //for (int i = 0; i < OldResult.Count(); i++)
            //{

            //    OldResult[i].NewRadif = NewResult[i].NewRadif;
            //    OldResult[i].NewTabaghe = NewResult[i].NewTabaghe;
            //    OldResult[i].NewEttesali = NewResult[i].NewEttesali;
            //    OldResult[i].NewMDF = NewResult[i].NewMDF;

            //}
            //List<TranslationPostInputMDFInfo> Result = new List<TranslationPostInputMDFInfo>();
            List<uspReportMDFExchangeCabinuteInputResult> result = ReportDB.GetMDFExchangeCabinuteInput(new List<long>{_requestID});
            
            SendToPrint(result);
            return true;
        }
       
        private void SendToPrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.MDFExchangeCabinuteInput);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }


    }
}
