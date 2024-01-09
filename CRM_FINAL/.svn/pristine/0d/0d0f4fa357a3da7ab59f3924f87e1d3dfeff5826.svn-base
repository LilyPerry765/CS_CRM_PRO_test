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
    /// Interaction logic for TelephoneRequestLogReportUserControl.xaml
    /// </summary>
    public partial class TelephoneRequestLogReportUserControl : Local.ReportBase
    {
        #region Constructor

        public TelephoneRequestLogReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<TelephoneRequestLog> Result = LoadData();
            
            
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            stiReport.PreviewMode = StiPreviewMode.DotMatrix;

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            title = " سوابق تلفن";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<TelephoneRequestLog> LoadData()
        {
            List<TelephoneRequestLog> NewResult = new List<TelephoneRequestLog>();
            long? TelephoneNo = string.IsNullOrEmpty(TelNoTextBox.Text) ? -1 : Convert.ToInt64(TelNoTextBox.Text);
            List<TelephoneRequestLog> result = ReportDB.GetTelephoneRequestLogInfo(TelephoneNo, InternationalCodeTextBox.Text, CustomerNameTextBox.Text, NewResult, CustomerIDTextBox.Text);

            for (int i = 0; i < result.Count;i++ )
            {
                for(int j=0;j<result.Count;j++)
                {
                    if(i!=j && result[i].TelephoneNo==result[j].TelephoneNo && result[i].ToTelephoneNo==result[j].ToTelephoneNo
                        && result[i].RequestType==result[j].RequestType && result[i].CustomerName==result[j].CustomerName
                        && result[i].Date==result[j].Date)
                    {
                        result.Remove(result[j]);
                        j--;
                    }
                }
            }
                return result.Distinct().OrderBy(t => t.TelephoneNo).ToList();
        }

        #endregion Methods
    }
}
