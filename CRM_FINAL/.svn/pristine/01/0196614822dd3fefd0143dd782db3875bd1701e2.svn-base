using CRM.Application.Local;
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
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
    /// Interaction logic for BuchtSwitchingMDFForm.xaml
    /// </summary>
    public partial class BuchtSwitchingMDFForm : Local.RequestFormBase
    {
        #region Properties and Fields

        private long requestID;
        private Request request { get; set; }
        private CRM.Data.BuchtSwitching buchtSwitching { get; set; }
        private UserControls.BuchtSwitchingUserControl _buchtSwitchingUserControl { get; set; }

        #endregion

        #region Constructor
        public BuchtSwitchingMDFForm()
        {
            InitializeComponent();
            Initialize();
        }

        public BuchtSwitchingMDFForm(long requestID)
            : this()
        {
            this.requestID = requestID;
        }

        #endregion

        #region EventHandlers
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region Methods
        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Print, (byte)DB.NewAction.Deny };
        }
        public void LoadData()
        {
            buchtSwitching = Data.BuchtSwitchingDB.GetBuchtSwitchingByID(requestID);


            if (buchtSwitching.MDFAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                buchtSwitching.MDFAccomplishmentDate = dateTime.Date;
                buchtSwitching.MDFAccomplishmentTime = dateTime.ToShortTimeString();
            }

            request = Data.RequestDB.GetRequestByID(requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = request.StatusID;

            _buchtSwitchingUserControl = new UserControls.BuchtSwitchingUserControl(requestID);
            _buchtSwitchingUserControl.AboneInformation.Visibility = Visibility.Collapsed;
            TranslationInfo.Content = _buchtSwitchingUserControl;
            TranslationInfo.DataContext = _buchtSwitchingUserControl;


            AccomplishmentGroupBox.DataContext = buchtSwitching;
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

                    buchtSwitching.Detach();
                    DB.Save(buchtSwitching, false);

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
                base.RequestID = request.ID;
                if (buchtSwitching.DateOfFinal == null)
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
        //TODO:rad
        public override bool Print()
        {
            bool result = true;
            try
            {
                //List<long> requestsID = new List<long> { this.requestID };
                //List<TranslationPostInputMDFInfo> reportData = ReportDB.GetBuchtSwitchingMDFInfo(requestsID);
                //
                //reportData.ForEach(t=>
                //{
                //
                //    AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo((long)t.TelephoneNo);
                //    if (assignmentInfo != null)
                //    {
                //        t.ADSLBucht = assignmentInfo.ADSLBucht;
                //    }
                //});
                //
                //StiVariable variable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                //ReportBase.SendToPrint(reportData, (int)DB.UserControlNames.BuchtSwitchingMDF, variable);
                DateTime currentDateTime = DB.GetServerDate();
                StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(currentDateTime));
                StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time));
                IEnumerable _result = ReportDB.GetBuchtSwitchingMDF(new List<long> { request.ID });
                ReportBase.SendToPrint(_result, (int)DB.UserControlNames.BuchtSwitchingMDF, dateVariable, timeVariable);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در ایجاد گزارش - تعویض بوخت ام دی اف");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                result = false;
            }
            return result;
        }

        #endregion
    }
}
