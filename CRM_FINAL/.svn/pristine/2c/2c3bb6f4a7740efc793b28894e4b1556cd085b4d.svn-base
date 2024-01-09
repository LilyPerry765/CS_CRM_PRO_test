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
    /// Interaction logic for ADSLOnlineDailyCitySaleReportUserControl.xaml
    /// </summary>
    public partial class ADSLOnlineDailyCitySaleReportUserControl : Local.ReportBase
    {
        #region Properties
        #endregion

        #region Constructor

        public ADSLOnlineDailyCitySaleReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion Consructor

        #region Initializer

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        #endregion Intitializer

        #region Methods

        public override void Search()
        {
            if (CityComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("لطفا شهرستان مورد نظر خود را انتخاب کنید");
            }
            else
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
                stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

                title = "درآمد روزانه آنلاين شهرستان";
                stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("Result", "Result", Result);


                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
        }

        private List<ADSLRequestInfo> LoadData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetCityOnlineSaleStatistics((int) CityComboBox.SelectedValue,
                                                                                FromDate.SelectedDate,
                                                                                toDate);
            return result;
        }
        #endregion Methods
    }
}
