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
    /// Interaction logic for ADSLPortsReportUserControl.xaml
    /// </summary>
    public partial class ADSLPortsReportUserControl : Local.ReportBase
    {
        #region properties
        #endregion

        #region Constructor
        public ADSLPortsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        
        #region Methods
        public void Initialize()
        {
            PortTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPortStatus));
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (CityComboBox.SelectedIDs.Count != 0)
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (CenterComboBox.SelectedIDs.Count != 0)
                MDFComboBox.ItemsSource = ADSLMDFDB.GetMDFCheckablebyCenterIDs(CenterComboBox.SelectedIDs);
        }
        public override void Search()
        {
            List<ADSLPortsInfo> Result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            title = "لیست پورت های ADSL ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
            
        }

        private List<ADSLPortsInfo> LoadData()
        {
            List<int> radif=new List<int>();
            List<int> tabaghe=new List<int>();
            List<int> ettesali=new List<int>();
            List<ADSLPortsInfo> result = ReportDB.GetADSLPortsInfo(CityComboBox.SelectedIDs,
                                                                   CenterComboBox.SelectedIDs,
                                                                    PortTypeComboBox.SelectedIDs,
                                                                     MDFComboBox.SelectedIDs,
                                                                     radif,
                                                                     tabaghe,
                                                                     ettesali,
                                                                     null);
            return result;
          
        }
        #endregion Methods
    }
}
