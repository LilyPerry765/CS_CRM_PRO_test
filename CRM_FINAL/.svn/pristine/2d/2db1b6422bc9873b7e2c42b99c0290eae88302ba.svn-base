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
using Stimulsoft.Report;
using Stimulsoft.Base;
using System.Reflection;
using CRM.Application.Reports.Viewer;
using CRM.Data;

namespace CRM.Application.Reports.ReportUserControls
{    
    public partial class ADSLServiceReportUserControl : Local.ReportBase
    {
        #region Properties
        #endregion Properties

        #region Constructor

        public ADSLServiceReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Consructor

        #region Initializer

        private void Initialize()
        {
            TitleComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceType));
            GroupNameComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            BandwidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWidthCheckable();
            TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckable();
            DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationCheckable();
            PaymentTypeComboBox.ItemsSource =Helper.GetEnumCheckable(typeof(DB.PaymentType));
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<ADSLServiceInfo> Result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);

            stiReport.Dictionary.Variables["StartFromDate"].Value = Helper.GetPersianDate(StartFromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["StartToDate"].Value = Helper.GetPersianDate(StartToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["EndFromDate"].Value = Helper.GetPersianDate(EndFromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["EndToDate"].Value = Helper.GetPersianDate(EndToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            foreach (ADSLServiceInfo adslserviceInfo in Result)
            {
                if(adslserviceInfo.IsOnlineRegister==true)
                    adslserviceInfo.IsOnlineRegisterString="بلی";
                else
                    adslserviceInfo.IsOnlineRegisterString = "خیر";

                if (adslserviceInfo.IsActive == true)
                    adslserviceInfo.IsActiveString = "بلی";
                else
                    adslserviceInfo.IsActiveString = "خیر";

                if (adslserviceInfo.IsSpecial == true)
                    adslserviceInfo.IsSpecialString = "بلی";
                else
                    adslserviceInfo.IsSpecialString = "خیر";

                adslserviceInfo.StartDateString =(adslserviceInfo.StartDate.HasValue) ? Helper.GetPersianDate(adslserviceInfo.StartDate, Helper.DateStringType.Short) : "";
                adslserviceInfo.EndDateString = (adslserviceInfo.EndDate.HasValue) ? Helper.GetPersianDate(adslserviceInfo.StartDate, Helper.DateStringType.Short) : "";

            }

            title = "ADSL سرویس های ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        public List<ADSLServiceInfo> LoadData()
        {
            DateTime? toStartDate = null;
            if (StartToDate.SelectedDate.HasValue)
            {
                toStartDate = StartToDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? toEndDate = null;
            if (EndToDate.SelectedDate.HasValue)
            {
                toEndDate = EndToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLServiceInfo> result = ReportDB.GetADSLServiceInfo
                                                    (StartFromDate.SelectedDate, toStartDate,
                                                    EndFromDate.SelectedDate, toEndDate,
                                                     TitleComboBox.SelectedIDs,
                                                     GroupNameComboBox.SelectedIDs,
                                                     BandwidthComboBox.SelectedIDs,
                                                     TrafficComboBox.SelectedIDs,
                                                     DurationComboBox.SelectedIDs,
                                                     PaymentTypeComboBox.SelectedIDs,
                                                     IsSpecialCheckBox.IsChecked,
                                                     IsActiveCheckBox.IsChecked,
                                                     IsOnlineRegisterCheckBox.IsChecked);
            return result; 
        }

        #endregion Methods
    }
}
