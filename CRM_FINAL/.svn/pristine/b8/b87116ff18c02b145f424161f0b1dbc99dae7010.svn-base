using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for FailureTimeTableReportUserControl.xaml
    /// </summary>
    public partial class FailureTimeTableReportUserControl : Local.ReportBase
    {
        public FailureTimeTableReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CityCenterUC.CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();

                DurationCombobox.ItemsSource = Helper.GetEnumItem(typeof(DB.TimeTable));
                DurationCombobox.SelectedValue = (int)DB.TimeTable.All;

                FailureStatusCombobox.ItemsSource = Helper.GetEnumItem(typeof(DB.RequestStepFailure117));
                FailureStatusCombobox.SelectedValue = (int)DB.RequestStepFailure117.All;
               
            }
        }
        public override void Search()
        {
            List<FailureFormRowInfo> result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            string CenterIds = string.Empty;
            string RegionIds = string.Empty;
            List<Center> centerList = CenterDB.GetAllCenter();
            
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        private List<FailureFormRowInfo> LoadData()
        {
            DateTime? ToDate = null;
            if (toDate.SelectedDate.HasValue)
            {
                ToDate = toDate.SelectedDate.Value.AddDays(1);
            }

            List<FailureFormRowInfo> result = ReportDB.GetFailureTimeTableRequest(CityCenterUC.CenterCheckableComboBox.SelectedIDs,
                byte.Parse(DurationCombobox.SelectedValue.ToString()), int.Parse(FailureStatusCombobox.SelectedValue.ToString()), fromDate.SelectedDate, ToDate);
            //foreach (FailureFormRowInfo item in result)
            //{
            //    item.Step = RequestDB.GetRequestInfoByID(item.RequestID).CurrentStep;
            //    if (item.EndMDFDateString != null)
            //    {
            //        double compareResult = (DateTime.Parse(item.EndMDFDateString) - DateTime.Parse(item.InsertDateString)).TotalMinutes;
            //        item.EliminateFailureDate = string.Format("{1} : {0}", Math.Round(compareResult % 60, 2).ToString(), Math.Round(compareResult / 60).ToString());
            //        //item.InsertDate = Date.GetPersianDate(DateTime.Parse(item.InsertDate), Date.DateStringType.DateTime);
            //       // item.EndMDFDate = Date.GetPersianDate(DateTime.Parse(item.EndMDFDate), Date.DateStringType.DateTime);
            //    }
            //    else
            //    {
            //       // item.InsertDate = Date.GetPersianDate(DateTime.Parse(item.InsertDate), Date.DateStringType.DateTime);
            //        item.EliminateFailureDate = "-";
            //    }

            //}

            return result;
                                                                   

        }
    }
}
