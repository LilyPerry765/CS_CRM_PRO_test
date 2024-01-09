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
    /// Interaction logic for PapRequestOperationReportUserControl.xaml
    /// </summary>
    public partial class PapRequestOperationReportUserControl : Local.ReportBase
    {
        #region Constructor

        public PapRequestOperationReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion  Constructor

        #region Initializer

        private void Initialize()
        {
            DayeriStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.DayeriStatus));            
            DayeriTimeOutComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPAPInstalTimeOut));
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

            if (DayeriStatusComboBox.Text != string.Empty)
            {
                stiReport.Dictionary.Variables["DayeriStatus"].Value = DayeriStatusComboBox.Text;
            }

            title = "گزارش عملکرد درخواسته ADSL شرکت های Pap ";
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
            List<ADSLPAPRequestInfo> result = ReportDB.GetPapRequestsOperation(FromDate.SelectedDate,
                                                     toDate,
                                                     DayeriTimeOutComboBox.SelectedIDs,
                                                     string.IsNullOrEmpty(RequestNoTextBox.Text.Trim()) ? (long?)null : long.Parse(RequestNoTextBox.Text.Trim()),
                                                     string.IsNullOrEmpty(regionCenterUserControl.CenterComboBox.Text) ? (int?)null : int.Parse(regionCenterUserControl.CenterComboBox.SelectedValue.ToString()),
                                                     string.IsNullOrEmpty(PhoneNoTextBox.Text.Trim()) ? (long?)null : long.Parse(PhoneNoTextBox.Text));

            
            List<EnumItem> requestTypeList = Helper.GetEnumItem(typeof(DB.RequestType));
            List<EnumItem> adslPapInstallTimeOutList = Helper.GetEnumItem(typeof(DB.ADSLPAPInstalTimeOut));
            List<EnumItem> adslOwnerStatusList = Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));

            foreach (ADSLPAPRequestInfo adslPapRequestInfo in result)
            {
                adslPapRequestInfo.EndDate = string.IsNullOrEmpty(adslPapRequestInfo.EndDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(adslPapRequestInfo.EndDate), Helper.DateStringType.DateTime);

                adslPapRequestInfo.RequestDate = string.IsNullOrEmpty(adslPapRequestInfo.RequestDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(adslPapRequestInfo.RequestDate), Helper.DateStringType.DateTime);

                int hour = 0;
                switch (int.Parse(adslPapRequestInfo.InstalTimeOut))
                {
                    case (int)DB.ADSLPAPInstalTimeOut.OneDay:
                        {
                            hour = 24;
                            adslPapRequestInfo.DateDiff = hour - adslPapRequestInfo.DateDiff;
                            break;
                        }
                    case (int)DB.ADSLPAPInstalTimeOut.ThreeDay:
                        {
                            hour = 72;
                            adslPapRequestInfo.DateDiff = hour - adslPapRequestInfo.DateDiff;
                            break;
                        }
                    case (int)DB.ADSLPAPInstalTimeOut.TwoDay:
                        {
                            hour = 48;
                            adslPapRequestInfo.DateDiff = hour - adslPapRequestInfo.DateDiff;
                            break;
                        }
                    case (int)DB.ADSLPAPInstalTimeOut.NoLimitation:
                        {
                            hour = 0;
                            break;
                        }
                }



                if (hour != 0)
                    if (adslPapRequestInfo.DateDiff >= 0)
                    {
                        adslPapRequestInfo.Color = (int)DB.Color.Green;
                        adslPapRequestInfo.strDateDiff = "+" + adslPapRequestInfo.DateDiff.ToString();
                    }
                    else
                    {
                        adslPapRequestInfo.Color = (int)DB.Color.Red;
                        adslPapRequestInfo.strDateDiff = adslPapRequestInfo.DateDiff.ToString();
                    }
                else
                {
                    adslPapRequestInfo.Color = (int)DB.Color.Black;
                    adslPapRequestInfo.strDateDiff = adslPapRequestInfo.DateDiff.ToString();
                }

                adslPapRequestInfo.InstalTimeOut = adslPapInstallTimeOutList.Find(item => item.ID == byte.Parse(adslPapRequestInfo.InstalTimeOut)).Name;
                adslPapRequestInfo.RequestType = requestTypeList.Find(item => item.ID == byte.Parse(adslPapRequestInfo.RequestType)).Name;

            }

            int? DayeriStatus = string.IsNullOrEmpty(DayeriStatusComboBox.Text) ? (int?)null : int.Parse(DayeriStatusComboBox.SelectedValue.ToString());

            switch (DayeriStatus)
            {
                case (int)DB.DayeriStatus.DayeriOnTime:
                    {

                        result = result.Where(t => (t.DateDiff >= 0)).ToList();
                        break;
                    }
                case (int)DB.DayeriStatus.DayeriWithDelay:
                    {
                        result = result.Where(t => (t.DateDiff < 0)).ToList();
                        break;
                    }

            }

            return result;

        }

        #endregion  Methods

    }
}
