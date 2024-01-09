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
using System.Reflection;
using CRM.Application.Reports.Viewer;
namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLTrafficSaleReportUserControl.xaml
    /// </summary>
    public partial class ADSLTrafficSaleReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ADSLTrafficSaleReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor 

        #region Initializer

        private void Initialize()
        {
            TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckable();
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<ADSLServiceInfo> ResultSellTraffic = LoadADSLSellTrafficData();
            List<ADSLServiceInfo> ResultRequest = LoadADSLRequestData();
            List<ADSLServiceInfo> Result = ResultSellTraffic.Union(ResultRequest).ToList();
                string title = string.Empty;
                string path;
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                path = ReportDB.GetReportPath(UserControlID);
                stiReport.Load(path);
                stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

                if (fromDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(fromDate.SelectedDate, Helper.DateStringType.Short).ToString();
                if (toDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(toDate.SelectedDate, Helper.DateStringType.Short).ToString();

                foreach (ADSLServiceInfo info in Result)
                {
                    if (info.IsPaid == true)
                    {
                        info.IsPaidString = "پرداخت شده";
                    }
                    else
                        info.IsPaidString = "پرداخت نشده";
                }

                title = "خرید ترافیک";
                stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("Result", "Result", Result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            
        }

        public List<ADSLServiceInfo> LoadADSLSellTrafficData()
        {
            DateTime? ToDate = null;
            if (toDate.SelectedDate.HasValue)
            {
                ToDate = toDate.SelectedDate.Value.AddDays(1);
            }
            //هزینه ها رو بر 0/06تقسیم کردم تا خالص به دست بیاد  sumprice in reportdb
            List<ADSLServiceInfo> Result = ReportDB.GetADSLTrafficSaleInfo(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                            CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                             TrafficComboBox.SelectedIDs,
                                                                            fromDate.SelectedDate,
                                                                             ToDate);

            return Result;
        }

        public List<ADSLServiceInfo> LoadADSLRequestData()
        {
            DateTime? ToDate = null;
            if (toDate.SelectedDate.HasValue)
            {
                ToDate = toDate.SelectedDate.Value.AddDays(1);
            }
            //هزینه ها رو بر 0/06تقسیم کردم تا خالص به دست بیاد  sumprice in reportdb
            List<ADSLServiceInfo> Result = ReportDB.GetADSLRequestTrafficSaleInfo(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                            CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                            TrafficComboBox.SelectedIDs,
                                                                            fromDate.SelectedDate,
                                                                             ToDate);

            return Result;
        }
       

        #endregion Methods
    }
}
