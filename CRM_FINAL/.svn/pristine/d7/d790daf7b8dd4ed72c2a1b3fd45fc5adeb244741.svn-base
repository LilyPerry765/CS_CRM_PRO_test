using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Base;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLOfficialDelayReportUserControl.xaml
    /// </summary>
    public partial class ADSLOfficialDelayReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ADSLOfficialDelayReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            List<EnumItem> adslRequestTypeList = Helper.GetEnumItem(typeof(DB.RequestType));
            adslRequestTypeList = GetAdslRequest(adslRequestTypeList);
            RequestTypeComboBox.ItemsSource = adslRequestTypeList;
        }

        #endregion Initializer

        #region Event Handlers

        private void RequestTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DelayStatusComboBox.ItemsSource = RequestStepDB.GetRequestStepByRequestTypeID(Convert.ToInt32((sender as ComboBox).SelectedValue));
        }

        #endregion  Event Handlers

        #region Mehtods

        public override void Search()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            string title;
            string path;
            List<RequestInfo> result = ReportDB.GetAdslDelay(FromDate.SelectedDate, toDate
                , string.IsNullOrEmpty(PhoneNoTextBox.Text) ? (string)null : PhoneNoTextBox.Text.Trim()
                , string.IsNullOrEmpty(regionCenterUserControl.RegionComboBox.Text) ? (int?)null : int.Parse(regionCenterUserControl.RegionComboBox.SelectedValue.ToString())
                , string.IsNullOrEmpty(regionCenterUserControl.CenterComboBox.Text) ? (int?)null : int.Parse(regionCenterUserControl.CenterComboBox.SelectedValue.ToString())
                , string.IsNullOrEmpty(RequestTypeComboBox.Text) ? (int?)null : int.Parse(RequestTypeComboBox.SelectedValue.ToString())
                , string.IsNullOrEmpty(DelayStatusComboBox.Text) ? (int?)null : int.Parse(DelayStatusComboBox.SelectedValue.ToString())
                , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text);

            List<EnumItem> PaymentType = Helper.GetEnumItem(typeof(DB.PaymentType));
            //FillPhoneNo(result);
            foreach(RequestInfo requestInfo in result)
            {
                requestInfo.RequestPaymentTypeName = PaymentType.Find(t => t.ID == requestInfo.RequestPaymentTypeID).Name;
                requestInfo.InsertDate=Helper.GetPersianDate(DateTime.Parse(requestInfo.InsertDate),Helper.DateStringType.Short).ToString();
                requestInfo.ModifyDate = Helper.GetPersianDate(DateTime.Parse(requestInfo.ModifyDate), Helper.DateStringType.Short).ToString();
                //requestInfo.strRequestDate = Helper.GetPersianDate(requestInfo.RequestDate, Helper.DateStringType.Short).ToString();
                //requestInfo.strRequestLetterDate = Helper.GetPersianDate(requestInfo.RequestLetterDate, Helper.DateStringType.Short);
                //requestInfo.strRequestDate = Helper.GetPersianDate(requestInfo.RequestDate, Helper.DateStringType.Short);

                requestInfo.strRequestDate = requestInfo.RequestDate;
                requestInfo.strRequestLetterDate = requestInfo.RequestLetterDate;
                requestInfo.strRequestDate = requestInfo.RequestDate;


            }

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            title = "گزارش تاخیر اداری ADSL ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("result", "result", result);

              
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        public static List<RequestInfo> FillPhoneNo(List<RequestInfo> result)
        {
            List<EnumItem> adslStatusList = Helper.GetEnumItem(typeof(DB.ADSLStatus));
            List<Status> statusList = StatusDB.GetStatus();
            List<ADSLRequest> adslRequestList = DB.GetAllEntity<ADSLRequest>().ToList();
            List<ADSLChangePort1> adslChangePortList = DB.GetAllEntity<ADSLChangePort1>().ToList();
            List<ADSLChangeService> adslChangeTariffList = DB.GetAllEntity<ADSLChangeService>().ToList();
            List<ADSLPAPRequest> adslPapRequestList = DB.GetAllEntity<ADSLPAPRequest>().ToList();
            List<ADSLCutTemporary> adslCutTemporaryList = DB.GetAllEntity<ADSLCutTemporary>().ToList();
            List<ADSLDischarge> adlsDischargeList = DB.GetAllEntity<ADSLDischarge>().ToList();

            //foreach (RequestInfo requestInfo in result)
            //{
            //    switch (requestInfo.RequestTypeID)
            //    {
            //        case (int)DB.RequestType.ADSL:
            //            requestInfo.TelephoneNo = adslRequestList.Find(t => t.ID == requestInfo.ID).TelephoneNo.ToString();
            //            break;



            //        case (int)DB.RequestType.ChangePortADSL:
            //            requestInfo.TelephoneNo = adslChangePortList.Find(t => t.ID == requestInfo.ID).TelephoneNo.ToString();
            //            break;

            //        case(int)DB.RequestType.ADSLChangeServiceType:
            //            requestInfo.TelephoneNo = adslChangeServiceList.Find(t => t.ID == requestInfo.ID).TelephoneNo.ToString();
            //            break;

            //        case(int) DB.RequestType.ADSLChangeTariff:
            //            requestInfo.TelephoneNo = adslChangeTariffList.Find(t => t.ID == requestInfo.ID).TelephoneNo.ToString();
            //            break;

            //        case(int)DB.RequestType.ADSLInstalPAPCompany:
            //            requestInfo.TelephoneNo=adslPapRequestList.Find(t=>t.ID==requestInfo.ID).TelephoneNo.ToString();
            //            break;

            //        case(int)DB.RequestType.ADSLCutTemporary:
            //            requestInfo.TelephoneNo = adslCutTemporaryList.Find(t => t.ID == requestInfo.ID).TelephoneNo.ToString();
            //            break;

            //        case(int)DB.RequestType.DischargeADSL:
            //            requestInfo.TelephoneNo = adlsDischargeList.Find(t => t.ID == requestInfo.ID).TelephoneNo.ToString();
            //            break;
            //    }
            //}


            return result;
        }

        private List<EnumItem> GetAdslRequest(List<EnumItem> adslRequestTypeList)
        {
            return adslRequestTypeList.Where(t => (t.ID == (byte)DB.RequestType.ADSL)
                             || (t.ID == (byte)DB.RequestType.ADSLChangeService)
                             || (t.ID == (byte)DB.RequestType.ADSLCutTemporary)
                             || (t.ID == (byte)DB.RequestType.ADSLInstalPAPCompany)
                             || (t.ID == (byte)DB.RequestType.ADSLDischarge)
                             || (t.ID == (byte)DB.RequestType.ADSLChangePort)
                             ).Distinct().ToList();
        }

        #endregion  Mehtods

    }
}
