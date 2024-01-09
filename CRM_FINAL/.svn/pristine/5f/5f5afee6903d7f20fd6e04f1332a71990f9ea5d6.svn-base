using System;
using System.Collections;
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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Base;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for CenterCabinetSubsetReportUserControl.xaml
    /// </summary>
    public partial class CenterCabinet_CabinetSyndeticOrderReportUserControl : Local.ReportBase
    {
        public CenterCabinet_CabinetSyndeticOrderReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAllCity();
        }
        public override void Search()
        {

            IEnumerable result = LoadData();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);


            stiReport.Dictionary.Variables["City_Center"].Value = CenterComboBox.Text;
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();


            stiReport.RegData("result", "result", result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public List<GroupingCabinetInput> LoadData()
        {
            List<GroupingCabinetInput> result = new List<GroupingCabinetInput>();
            if ((bool)WithBuchtTypeNameCheckBox.IsChecked)
                result = CabinetInputDB.GetGroupingCabinetInputWithBuchtType(new List<int>{(int)CityComboBox.SelectedValue}, 
                                                                                       new List<int>{(int)CenterComboBox.SelectedValue},
                                                                                       new List<int>{(int)CabinetComboBox.SelectedValue});
            else
             result = CabinetInputDB.GetGroupingCabinetInput(new List<int>{(int)CityComboBox.SelectedValue}, 
                                                                                       new List<int>{(int)CenterComboBox.SelectedValue},
                                                                                       new List<int>{(int)CabinetComboBox.SelectedValue});
            
            return result;

        }

        

        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterByCityId((int)CityComboBox.SelectedValue);
        }
        private void Center_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                CabinetComboBox.ItemsSource = CabinetDB.GetCabinetByCenterID((int)CenterComboBox.SelectedValue);
                CabinetComboBox.SelectedIndex = 0;
            }
        }


    }
}
