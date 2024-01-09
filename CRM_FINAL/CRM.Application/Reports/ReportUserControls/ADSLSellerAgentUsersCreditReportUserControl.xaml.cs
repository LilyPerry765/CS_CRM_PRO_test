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
using System.Windows.Shapes;
using CRM.Data;
using System.Collections;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;
namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLSellerAgentUsersCreditReportUserControl.xaml
    /// </summary>
    public partial class ADSLSellerAgentUsersCreditReportUserControl : Local.ReportBase
    {
       
       #region Properties

        #endregion Properties

        #region Constructor

        public ADSLSellerAgentUsersCreditReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initilize

        #region Initializer

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            CityComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ItemsComboBox_DropDownClosed);
            

        }

        #endregion Intitializer

        #region EventHAndler

        void ItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (CityComboBox.SelectedIDs.Count > 0)
                AllChecked = true;

            //CenterComboBox.ItemsSource = Data.CenterDB.GetCentersCheckable(AllChecked, CityComboBox.SelectedIDs);
            //GroupComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentCheckablebyCityIDs(CityComboBox.SelectedIDs);
            ADSLSellerAgentComboBox.ItemsSource = Data.ADSLSellerGroupDB.GetADSLSellerAgentCheckablebyCityIDs(CityComboBox.SelectedIDs);
            ADSLSellerAgentComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ADSLSellerAgentItemsComboBox_DropDownClosed);
        }

        void ADSLSellerAgentItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            ADSLSellerAgentUserComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentUsersCheckableByADSlSellerAgentID(ADSLSellerAgentComboBox.SelectedIDs);
        }

        #endregion

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
            title = "اعتبارکاربران نمایندگان فروش ADSL";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLSellerAgentInfo> LoadData()
        {
            List<ADSLSellerAgentInfo> result = ReportDB.GetADSLSellerAgentUserCreditInfo( CityComboBox.SelectedIDs,
                                                                               ADSLSellerAgentComboBox.SelectedIDs,
                                                                               ADSLSellerAgentUserComboBox.SelectedIDs);
            return result;
        }

       

     #endregion Methods
    }
}
