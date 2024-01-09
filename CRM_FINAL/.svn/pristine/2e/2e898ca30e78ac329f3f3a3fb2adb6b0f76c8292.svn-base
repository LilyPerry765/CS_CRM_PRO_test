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
    /// <summary>
    /// Interaction logic for ADSLHistoryReportUserControl.xaml
    /// </summary>
    public partial class ADSLHistoryReportUserControl : Local.ReportBase
    {
        #region Properties

        #endregion Peroperties

        #region Constructor

        public ADSLHistoryReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckable();
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<ADSLHistoryInfo> Result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);

            stiReport.Dictionary.Variables["FromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ToDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            //foreach (ADSLHistoryInfo adslhistoryinfo in Result)
            //{
            //    adslhistoryinfo.InsertDate = Helper.GetPersianDate(adslhistoryinfo.InsertDate, Helper.DateStringType.Short) ;
            //}

            title = "تاریخچه مشترکین ADSL";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        public List<ADSLHistoryInfo> LoadData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            //long TelNo=!string.IsNullOrEmpty(TelNoTextBox.Text) ? Convert.ToInt64(TelNoTextBox.Text):-1;
            List<ADSLHistoryInfo> result = ReportDB.GetADSLHitoryInfo(ServiceComboBox.SelectedIDs,
                                                                       TelNoTextBox.Text,
                                                                       FromDate.SelectedDate,
                                                                       toDate);
                                                            
            return result;
        }

        #endregion Methods

    }
}
