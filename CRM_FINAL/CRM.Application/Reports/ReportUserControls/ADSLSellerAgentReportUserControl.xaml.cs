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
using Stimulsoft.Base;
using System.Reflection;
using CRM.Application.Reports.Viewer;
using CRM.Data;
namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLSellerAgentReportUserControl.xaml
    /// </summary>
    public partial class ADSLSellerAgentReportUserControl : Local.ReportBase
    {
        #region Properties

        #endregion Properties

        #region Constructor

        public ADSLSellerAgentReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initilize

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            CityComboBox.ItemsComboBox.DropDownClosed += new EventHandler(CityComboBox_DropDownClosed);
            
            //GroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupCityComboBox_DropDownClosed);
        }

        void GroupCityComboBox_DropDownClosed(object sender, EventArgs e)
        {
           
        }

        void CityComboBox_DropDownClosed(object sender, EventArgs e)
        {
            //GroupComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentCheckablebyCityIDs(CityComboBox.SelectedIDs);
            ADSLSellerAgentComboBox.ItemsSource = Data.ADSLSellerGroupDB.GetADSLSellerAgentCheckablebyCityIDs(CityComboBox.SelectedIDs);
        }

     #endregion Initialize

     #region Methods

        public override void Search()
        {
            List<ADSLSellerAgentInfo> Result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);

            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            foreach (ADSLSellerAgentInfo info in Result)
            {
                if (info.ISSellModem == true)
                    info.SellModem = "دارد";
                else if (info.ISSellModem == false)
                    info.SellModem = "ندارد";
            }

            title = "اعتبار نمایندگان فروش ADSL";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLSellerAgentInfo> LoadData()
        {
            List<ADSLSellerAgentInfo> result = ReportDB.GetADSLSellerAgentInfo(ADSLSellerAgentComboBox.SelectedIDs_S,
                                                                               CityComboBox.SelectedIDs);
            return result;
        }

     #endregion Methods
    }
}
