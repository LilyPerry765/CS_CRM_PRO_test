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
    /// Interaction logic for EmptyTelephoneReportUserControl.xaml
    /// </summary>
    public partial class EmptyTelephoneReportUserControl : Local.ReportBase
    {
        public EmptyTelephoneReportUserControl()
        {
            InitializeComponent();
            Initialize();

        }
        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            }
        }

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PreCodeComboBox.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableByCenterIDs(CenterComboBox.SelectedIDs);
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
            List<int> CenterIDs = CenterComboBox.SelectedIDs;
            List<int> CityIDs = CityComboBox.SelectedIDs;
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
            List<EmptyTelephoneReport> result_DisCharge = new List<EmptyTelephoneReport>();
            List<EmptyTelephoneReport> result_Empty = new List<EmptyTelephoneReport>();

            List<EmptyTelephoneReport> result = new List<EmptyTelephoneReport>();
            if ((bool)EmptyCheckBox.IsChecked)
               result_Empty= TelephoneDB.GetEmptyTelephoneStatistic(CityComboBox.SelectedIDs , CenterComboBox.SelectedIDs , PreCodeComboBox.SelectedIDs);

            if ((bool)DisChargeCheckBox.IsChecked)
                result_DisCharge = TelephoneDB.GetDisChargeTelephoneStatistic(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, PreCodeComboBox.SelectedIDs);

            result = result_DisCharge.Union(result_Empty).ToList();
            foreach (EmptyTelephoneReport item in result)
            {
                item.PersianDayeriDT = (string.IsNullOrEmpty (item.Dayeri.ToString()))?"": Helper.GetPersianDate(item.Dayeri, Helper.DateStringType.Short);
                item.PersianUnIstallDT = (string.IsNullOrEmpty(item.DisCharge.ToString())) ? "" : Helper.GetPersianDate(item.DisCharge, Helper.DateStringType.Short);
                item.PersianReDayeri = (string.IsNullOrEmpty(item.ReDayeri.ToString())) ? "" : Helper.GetPersianDate(item.ReDayeri, Helper.DateStringType.Short);
            }

            return result;
        }

        private void CityComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            PreCodeComboBox.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }


    }
}
