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
using System.Collections;
using System.ComponentModel;
using CRM.Data;
using CRM.Application.Reports.Viewer;


namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for InstallAndDisChargePapCompanyReportUserControl.xaml
    /// </summary>
    public partial class SpaceAndPowerPapCompanyReportUserControl : Local.ReportBase
    {
        public SpaceAndPowerPapCompanyReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            PapCompanyComboBox.ItemsSource = PAPInfoDB.GetPapInfo();
        }

        public override void Search()
        {
            if (PapCompanyComboBox.SelectedIndex < 0)
                MessageBox.Show("لطفا شرکت PAP مورد نظر را انتخاب بفرمایید.");
            else
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

                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
                stiReport.CacheAllData = true;
                stiReport.RegData("result", "result", result);
 
                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
            }
        
        public IEnumerable LoadData()
        {
            List<SpaceAndPowerPapCompany> result = PAPInfoDB.GetSpaceAndpowerPAPCompanyReport(CityCenterUC.CenterCheckableComboBox.SelectedIDs,
                                                                    FromDate.SelectedDate,
                                                                    ToDate.SelectedDate,
                                                                    (CycleIDComboBox.SelectedValue != null) ? int.Parse(CycleIDComboBox.Text) : (int?)null,
                                                                    (PapCompanyComboBox.SelectedValue != null) ? int.Parse(PapCompanyComboBox.SelectedValue.ToString()) : (int?)null);

            double PapInfoCostValue = double.Parse(PAPInfoCostDB.GetPAPInfoCostById(1).Value);
            foreach (SpaceAndPowerPapCompany item in result)
            {
                item.PersianStartDate = (!item.StartDate.HasValue) ? "ندارد" : Helper.GetPersianDate(item.StartDate, Helper.DateStringType.Short);
            }

            return result;
        }
    }
}
