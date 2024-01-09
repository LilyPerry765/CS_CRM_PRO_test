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
using System.Collections;
using System.ComponentModel;
using CRM.Application.Reports.Viewer;


namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for failureNotCorrectedPostContactsReportUserControl.xaml
    /// </summary>
    public partial class failureNotCorrectedPostContactsReportUserControl : Local.ReportBase
    {
        public failureNotCorrectedPostContactsReportUserControl()
        {
            InitializeComponent();
            Initialize();

        }
        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CityCenterUC.CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            }
        }

        public override void Search()
        {
            IEnumerable result = LoadData();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);

            string CenterIds = string.Empty;
            string RegionIds = string.Empty;
            List<Center> centerList = CenterDB.GetAllCenter();
            List<int> CenterIDs = CityCenterUC.CenterCheckableComboBox.SelectedIDs;
            List<int> CityIDs = CityCenterUC.CityComboBox.SelectedIDs;
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

            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
  
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public IEnumerable LoadData()
        {
            List<PostContactMalfunction> result =
            PostContactDB.GetFailureNotCorrectPostContactStatistic(CityCenterUC.CenterCheckableComboBox.SelectedIDs,
                                                FromDate.SelectedDate, ToDate.SelectedDate);

            List<EnumItem> PCMMalfuctionType = Helper.GetEnumItem(typeof(DB.PCMMalfuctionType));

            foreach (PostContactMalfunction item in result)
            {
                item.PersianCorrect_Date = Helper.GetPersianDate(item.Correct_Date, Helper.DateStringType.Short);
                item.PersianFailure_Date = Helper.GetPersianDate(item.Failure_Date, Helper.DateStringType.Short);
                item.TypeMalfactionName = (item.TypeMalfaction != null) ? (PCMMalfuctionType.Find(i => i.ID == item.TypeMalfaction) != null) ? PCMMalfuctionType.Find(i => i.ID == item.TypeMalfaction).Name : "" : "";
            }


            return result;
        }
    }
}
