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
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for SpaceAndPowerReportUserControl.xaml
    /// </summary>
    public partial class SpaceAndPowerReportUserControl : Local.ReportBase
    {
        public SpaceAndPowerReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            EquipmentTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.EquipmentType));
            SpaceTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.SpaceType));
            PowerTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PowerType));
          //  CustomerTitleComboBox.ItemsSource = SpaceAndPowerCustomerDB.GetSpacePowerCustomerCheckable();
            //RegionIdComboBox.ItemsSource = RegionDB.GetRegions();
        }


        #region Event Handlers
        private void RegionIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CenterIdComboBox.ItemsSource = CenterDB.GetCenterByRegionId((int)((sender as ComboBox).SelectedValue));
        }
        #endregion Event Handlers

        #region Methods

        public override void Search()
        {
            List<SpaceAndPowerReportInfo> result = LoadData();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            title = "گزارش فضا و پاور";

            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            if (regionCenterUserControl.RegionComboBox.Text != string.Empty)
            {
                stiReport.Dictionary.Variables["Region"].Value = regionCenterUserControl.RegionComboBox.Text;
            }
            if (regionCenterUserControl.CenterComboBox.Text != string.Empty)
            {
                stiReport.Dictionary.Variables["CenterName"].Value = regionCenterUserControl.CenterComboBox.Text;
            }


            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", result);
 
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }


        private List<SpaceAndPowerReportInfo> LoadData()
        {
            List<SpaceAndPowerReportInfo> result =
            ReportDB.GetSpacePowerInfo(FromDate.SelectedDate,
                                     ToDate.SelectedDate,
                                     string.IsNullOrEmpty(txtRequestNo.Text.Trim()) ? (long?)null : long.Parse(txtRequestNo.Text),
                                     string.IsNullOrEmpty(regionCenterUserControl.CenterComboBox.Text) ? (int?)null : int.Parse(regionCenterUserControl.CenterComboBox.SelectedValue.ToString()),
                                     string.IsNullOrEmpty(txtSpaceSize.Text.Trim()) ? (string)null : txtSpaceSize.Text.Trim(),
                                     SpaceTypeComboBox.SelectedIDs,
                                     EquipmentTypeComboBox.SelectedIDs,
                                     PowerTypeComboBox.SelectedIDs
                                     );

            List<EnumItem> SpaceTypeList = Helper.GetEnumItem(typeof(DB.SpaceType));
            List<EnumItem> EquipmentTypeList = Helper.GetEnumItem(typeof(DB.EquipmentType));
            List<EnumItem> PowerTypeList = Helper.GetEnumItem(typeof(DB.PowerType));
            foreach (SpaceAndPowerReportInfo I in result)
            {
                I.PowerType = PowerTypeList.Find(item => item.ID == int.Parse(I.PowerType)).Name;
                I.SpaceType = SpaceTypeList.Find(item => item.ID == int.Parse(I.SpaceType)).Name;
                I.EquipmentType = EquipmentTypeList.Find(item => item.ID == int.Parse(I.EquipmentType)).Name;
                I.RequestDate = Helper.GetPersianDate(DateTime.Parse(I.RequestDate), Helper.DateStringType.Short);
                I.RequestLetterDate = string.IsNullOrEmpty(I.RequestLetterDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(I.RequestLetterDate), Helper.DateStringType.Short);
                
            }
            return result;
        }
        #endregion Method
    }
}
