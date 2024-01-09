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
    /// Interaction logic for ADSLCityDailyIncomeReportUserControl.xaml
    /// </summary>
    public partial class ADSLCityDailyIncomeReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ADSLCityDailyIncomeReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            //ProvinceComboBox.ItemsSource = ProvinceDB.GetProvincesCheckable();
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {

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
            stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplainationTextBox.Text;

            stiReport.Dictionary.Variables["StartDate"].Value = Helper.GetPersianDate(fromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["EndDate"].Value = Helper.GetPersianDate(toDate.SelectedDate, Helper.DateStringType.Short).ToString();

                stiReport.Dictionary.Variables.Add("fromDate", ((!fromDate.SelectedDate.HasValue) ? "null" :"'"+ fromDate.SelectedDate.Value.Date.Date.ToShortDateString()+"'"));
                stiReport.Dictionary.Variables.Add("toDate", ((!toDate.SelectedDate.HasValue) ? "null" :"'"+ toDate.SelectedDate.Value.Date.Date.ToShortDateString()+"'"));
                //stiReport.Dictionary.Variables["ProvinceIDs"].Value = ProvinceComboBox.SelectedValue.ToString();


                title = " درآمد روزانه شهرستان ";
            stiReport.Dictionary.Variables["Header"].Value = title;

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        

        #endregion Methods
    }
}
