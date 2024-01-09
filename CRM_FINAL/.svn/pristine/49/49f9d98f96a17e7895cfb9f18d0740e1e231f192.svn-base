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
using CRM.Data;
using Stimulsoft.Report;
using Stimulsoft.Base;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PapADSLRequestReportUserControl.xaml
    /// </summary>
    public partial class PapADSLRequestReportUserControl : Local.ReportBase
    {
        #region Constructor
        
        public PapADSLRequestReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion  Constructor

        #region Initializer

        private void Initialize()
        {
            RequestTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PAPRequestType));
        }

        #endregion  Initializer

        #region Event Handlers
        #endregion  Event Handlers

        #region Methods

        public override void Search()
        {
            List<ADSLPAPRequestInfo> result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["TelephoneNO"].Value = PhoneNoTextBox.Text.Trim();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            if (regionCenterUserControl.RegionComboBox.Text != string.Empty)
            {
                stiReport.Dictionary.Variables["Region"].Value = regionCenterUserControl.RegionComboBox.Text;
            }
            if (regionCenterUserControl.CenterComboBox.Text != string.Empty)
            {
                stiReport.Dictionary.Variables["CenterName"].Value = regionCenterUserControl.CenterComboBox.Text;
            }
            
            title = "گزارش درخواستهای شرکتهای PAP ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ADSLPAPRequestInfo> LoadData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLPAPRequestInfo> result =
                     ReportDB.GetADSLPapRequests(FromDate.SelectedDate,
                                                     toDate,
                                                     RequestTypeComboBox.SelectedIDs,
                                                     string.IsNullOrEmpty(RequestNoTextBox.Text.Trim()) ? (long?)null : long.Parse(RequestNoTextBox.Text.Trim()),
                                                     string.IsNullOrEmpty(regionCenterUserControl.CenterComboBox.Text) ? (int?)null : int.Parse(regionCenterUserControl.CenterComboBox.SelectedValue.ToString()),
                                                     NationalIdTextBox.Text.Trim(),
                                                     string.IsNullOrEmpty(PhoneNoTextBox.Text.Trim()) ? (long?)null : long.Parse(PhoneNoTextBox.Text.Trim()));




            
            List<EnumItem> requestTypeList = Helper.GetEnumItem(typeof(DB.RequestType));
            List<EnumItem> adslPapInstallTimeOutList = Helper.GetEnumItem(typeof(DB.ADSLPAPInstalTimeOut));
            List<EnumItem> adslOwnerStatueList = Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));

            foreach (ADSLPAPRequestInfo adslPapRequestInfo in result)
            {
                adslPapRequestInfo.EndDate = string.IsNullOrEmpty(adslPapRequestInfo.EndDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(adslPapRequestInfo.EndDate), Helper.DateStringType.Short);
                adslPapRequestInfo.InsertDate = string.IsNullOrEmpty(adslPapRequestInfo.InsertDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(adslPapRequestInfo.InsertDate), Helper.DateStringType.Short);
                adslPapRequestInfo.RequestDate = string.IsNullOrEmpty(adslPapRequestInfo.RequestDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(adslPapRequestInfo.RequestDate), Helper.DateStringType.Short);
                adslPapRequestInfo.PapInfoStatus = (adslPapRequestInfo.PapInfoStatus == "True") ? "فعال" : "غیر فعال";
                adslPapRequestInfo.InstalTimeOut = adslPapInstallTimeOutList.Find(item => item.ID == byte.Parse(adslPapRequestInfo.InstalTimeOut)).Name;
                adslPapRequestInfo.RequestType = requestTypeList.Find(item => item.ID == byte.Parse(adslPapRequestInfo.RequestType)).Name;
                adslPapRequestInfo.CustomerStatus = adslOwnerStatueList.Find(item => item.ID == byte.Parse(adslPapRequestInfo.CustomerStatus)).Name;

            }

            return result;

        }

        #endregion Methods
    }
}
