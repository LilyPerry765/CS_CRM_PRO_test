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
using CRM.Application.UserControls;
using CRM.Application.Reports.Viewer;
using System.Text.RegularExpressions;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for CenterCableReportUserControl.xaml
    /// </summary>
    public partial class CenterCableReportUserControl : Local.ReportBase
    {
        bool _mode = false;
        public CenterCableReportUserControl()
        {
            InitializeComponent(); 

        }
        public CenterCableReportUserControl(bool mode):this()
        {
            _mode = mode;
        }
        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CityCenterUC.CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
                PhysicalTypeCheckableCombobox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CablePhysicalType));
                UsingTypeCheckableCombobox.ItemsSource = CableUsedChannelDB.GetCableUsedChannelCheckable();
                StatusCombobox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CableStatus));
            }
        }
        private void ReportBase_Loaded(object sender, RoutedEventArgs e)
        {
            if(_mode)
            {

                PhysicalTypeTextBlock.Visibility = Visibility.Collapsed;
                PhysicalTypeCheckableCombobox.Visibility = Visibility.Collapsed;

                UsingTypeTextBlock.Visibility = Visibility.Collapsed;
                UsingTypeCheckableCombobox.Visibility = Visibility.Collapsed;

                StatusTextBlock.Visibility = Visibility.Collapsed;
                StatusCombobox.Visibility = Visibility.Collapsed;

                FromDateTextBlock.Visibility = Visibility.Collapsed;
                FromDate.Visibility = Visibility.Collapsed;

                ToDateTextBlock.Visibility = Visibility.Collapsed;
                ToDate.Visibility = Visibility.Collapsed;
            }
        }
        private void CityComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CityCenterUC.CenterCheckableComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds((sender as CheckableComboBox).SelectedIDs);

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
            
            
            List<string> strCityList = new List<string>();
            List<string> strCenterList = new List<string>();
            if (UserControlID == (int)DB.UserControlNames.CenterCablesTotal)
            {
                strCityList = CityDB.GetCityNameByIDs(CityIDs); 
                strCenterList = CenterDB.GetCenterNameByIDs(CenterIDs);
            }
             else if (UserControlID == (int)DB.UserControlNames.CenterCablesDetails)
            {
                strCityList = CityDB.GetCityNameByIDs(CityIDs); 
                 strCenterList = CenterDB.GetCenterNameByIDs(CenterIDs);
            }






            stiReport.Dictionary.Variables["CenterName"].Value = (CenterIDs.Count != 0 && CenterIDs.Count <= 2) ? string.Join("،", strCenterList) : "چندین مرکز";
           stiReport.Dictionary.Variables["Region"].Value = (CityIDs.Count != 0 && CityIDs.Count <= 2) ? string.Join("،", strCityList) : "چندین شهر"; 

            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

            stiReport.Dictionary.Variables["FromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ToDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
           
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public IEnumerable LoadData()
        {
            List<CableInfo> resultTotal = new List<CableInfo>();
            List<GroupingCabinetInput> resultDetails = new List<GroupingCabinetInput>();


            if (UserControlID == (int)DB.UserControlNames.CenterCablesTotal)
            {
                resultTotal = CableDB.GetCenterCables(FromDate.SelectedDate, ToDate.SelectedDate
                                                                        , CityCenterUC.CenterCheckableComboBox.SelectedIDs
                                                                        , string.IsNullOrEmpty(CableNo.Text.Trim()) ? -1 : int.Parse(CableNo.Text.Trim())
                                                                        , PhysicalTypeCheckableCombobox.SelectedIDs
                                                                        , UsingTypeCheckableCombobox.SelectedIDs
                                                                        , string.IsNullOrEmpty(StatusCombobox.Text) ? "" : StatusCombobox.SelectedValue.ToString());
                List<EnumItem> cs = Helper.GetEnumItem(typeof(DB.CableStatus));
                List<EnumItem> PTC = Helper.GetEnumItem(typeof(DB.CablePhysicalType));
               
                foreach (CableInfo item in resultTotal)
                {
                    item.StatusName = cs.Find(t => t.ID == item.Status).Name;
                    item.PhysicalTypeName = (string.IsNullOrEmpty(item.PhysicalType.ToString())) ? "" : PTC.Find(t => t.ID == (byte)item.PhysicalType).Name;
                }
                return resultTotal;
            }
            else if (UserControlID == (int)DB.UserControlNames.CenterCablesDetails)
            {
                resultDetails = CabinetInputDB.GetGroupingCableDetallReport(CityCenterUC.CityComboBox.SelectedIDs, CityCenterUC.CenterCheckableComboBox.SelectedIDs, new List<int>(), (string.IsNullOrEmpty(CableNo.Text.Trim()) ? -1 : int.Parse(CableNo.Text.Trim())));
                 List<Cable> CableTemp = CableDB.GetCablesByCenterIDs(CityCenterUC.CenterCheckableComboBox.SelectedIDs);
                foreach (GroupingCabinetInput item in resultDetails)
                {
                    item.Capacity = CableTemp.Find(t => t.ID == item.CableID).ToCablePairNumber - CableTemp.Find(t => t.ID == item.CableID).FromCablePairNumber;
                    item.Diameter = CableTemp.Find(t => t.ID == item.CableID).CableDiameter;
                    item.CabinetNumber = CableTemp.Find(t => t.ID == item.CableID).CableNumber;
                    
                }
                return resultDetails;
            }
            return null;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


    }
}
