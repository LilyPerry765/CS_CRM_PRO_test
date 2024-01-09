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
using CRM.Data;
using CRM.Application.UserControls;
using System.Collections;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for TotalStatisticPCMPortsReportUserControl.xaml
    /// </summary>
    public partial class TotalStatisticPCMPortsReportUserControl : Local.ReportBase
    {
        public TotalStatisticPCMPortsReportUserControl()
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
            int? CenterIDs = (int?)CenterComboBox.SelectedValue;
            int? CityIDs = (int?)CityComboBox.SelectedValue;

            stiReport.Dictionary.Variables["CenterName"].Value = CenterComboBox.Text;
            stiReport.Dictionary.Variables["Region"].Value = CityComboBox.Text;
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
            List<PCMStatisticDetails> result = PCMDB.GetTotalStatisticPCMPorts( FromDate.SelectedDate,ToDate.SelectedDate,
                                                                                (int?)CenterComboBox.SelectedValue,
                                                                                (int?)PCMUC.PCMBrandComboBox.SelectedValue,
                                                                                (int?)PCMUC.RockComboBox.SelectedValue,
                                                                                (int?)PCMUC.ShelfComboBox.SelectedValue,
                                                                                (int?)PCMUC.CardComboBox.SelectedValue);
            List<EnumItem> ps = Helper.GetEnumItem(typeof(DB.PCMStatus));
            foreach (PCMStatisticDetails item in result)
            {
                item.InsertDate_Time = Helper.GetPersianDate(item.InsertDate, Helper.DateStringType.Time).ToString();
                item.InsertDate_Date = Helper.GetPersianDate(item.InsertDate, Helper.DateStringType.Short).ToString();
                item.Telno = (item.SwitchPortID!= null)? TelephoneDB.GetTelephoneNoBySwitchPortId((int)item.SwitchPortID): "";

            }

            return result;
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                UpdateElemans();
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityComboBox.SelectedValue != null)
            {
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableByCityId((int)CityComboBox.SelectedValue);
                CenterComboBox.SelectedIndex = 0;
            }
        }

        private void UpdateElemans()
        {
            if (CenterComboBox.SelectedValue != null)
                PCMUC.Rock_Update((int)CenterComboBox.SelectedValue);
        }
    }
}
