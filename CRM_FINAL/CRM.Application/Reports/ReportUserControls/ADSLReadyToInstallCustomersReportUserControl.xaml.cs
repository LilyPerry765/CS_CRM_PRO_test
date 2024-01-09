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
    /// Interaction logic for ADSLReadyToInstallCustomersReportUserControl.xaml
    /// </summary>
    public partial class ADSLReadyToInstallCustomersReportUserControl : Local.ReportBase
    {
        #region Prperties

        #endregion

        #region Constructor

        public ADSLReadyToInstallCustomersReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        
        #endregion Consructor

        #region Initializer

        private void Initialize()
        {
            //CenterComboBox.ItemsSource = CenterDB.GetCenterCheckable();
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<ADSLRequestInfo> Result = LoadData(); 
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
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            title = "ليست مشترکين آماده به نصب و ارسال جهت راه اندازي و دايري";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        public List<ADSLRequestInfo> LoadData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetReadyToInstallCustomers(CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                               CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                FromDate.SelectedDate,
                                                                                toDate);
            return result;
        }

       

        #endregion Methods
    }
}
