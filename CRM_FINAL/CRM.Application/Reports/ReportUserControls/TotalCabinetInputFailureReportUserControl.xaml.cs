using System;
using System.Collections;
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

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for TotalCabinetInputFailureReportUserControl.xaml
    /// </summary>
    public partial class TotalCabinetInputFailureReportUserControl : Local.ReportBase
    {
        public TotalCabinetInputFailureReportUserControl()
        {
            InitializeComponent(); 
            Initialize();

        }
        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                FailureCombobox.ItemsSource = Helper.GetEnumItem(typeof(DB.RequestStepFailure117));
                FailureCombobox.SelectedValue = (int)DB.RequestStepFailure117.All;
            }

        }
        
        public override void Search()
        {
            IEnumerable result = LoadData();
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
            List<int> CenterIDs = RequestPropertisUC.CenterCheckableComboBox.SelectedIDs;
            List<int> CityIDs = RequestPropertisUC.CityComboBox.SelectedIDs;
            if (CenterIDs.Count != 0)
            {
                foreach (int _centerIds in CenterIDs)
                {
                    CenterIds += centerList.Find(item => item.ID == _centerIds).CenterName + " ,";
                }
                CenterIds = CenterIds.Substring(0, CenterIds.Length - 1);
            }
            

            List<City> Citylist = CityDB.GetAllCity();
            if (CityIDs.Count != 0)
            {
                foreach (int _cityrIds in CityIDs)
                {
                    RegionIds += Citylist.Find(item => item.ID == _cityrIds).Name + " ,";
                }
                RegionIds = RegionIds.Substring(0, RegionIds.Length - 1);
            }
            

            stiReport.Dictionary.Variables["CenterName"].Value = CenterIds;
            stiReport.Dictionary.Variables["Region"].Value = RegionIds;
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(RequestPropertisUC.FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(RequestPropertisUC.ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["TelephoneNO"].Value = RequestPropertisUC.TelephoneNo.Text.Trim();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            




            stiReport.RegData("Result", "Result", result);

              
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        private IEnumerable LoadData()
        {
            IEnumerable result = ReportDB.GetTotalCabinetInputFailure(RequestPropertisUC.FromDate.SelectedDate
                                                                    , RequestPropertisUC.ToDate.SelectedDate
                                                                    , string.IsNullOrEmpty(RequestPropertisUC.RequestIdFrom.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.RequestIdFrom.Text.Trim())
                                                                    , string.IsNullOrEmpty(RequestPropertisUC.RequestIdTo.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.RequestIdTo.Text.Trim())
                                                                    , RequestPropertisUC.CenterCheckableComboBox.SelectedIDs
                                                                    , string.IsNullOrEmpty(RequestPropertisUC.TelephoneNo.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.TelephoneNo.Text.Trim())
                                                                    , int.Parse(FailureCombobox.SelectedValue.ToString())
                                                                    , string.IsNullOrEmpty(CabinetIDFrom.Text.Trim()) ? null : CabinetIDFrom.Text.Trim()
                                                                    , string.IsNullOrEmpty(CabinetIDTo.Text.Trim()) ? null : CabinetIDTo.Text.Trim()
                                                                    , string.IsNullOrEmpty(RequestPropertisUC.IdentityId.Text.Trim()) ? null : RequestPropertisUC.IdentityId.Text.Trim());





            return result;
        }
    }
}
