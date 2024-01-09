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
    /// Interaction logic for ADSLCustomerInfoReportUserControl.xaml
    /// </summary>
    public partial class ADSLCustomerInfoReportUserControl : Local.ReportBase
    {
        #region Properties
        #endregion Properties

        #region Constructor

        public ADSLCustomerInfoReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion Consructor

        #region Initializer

        private void Initialize()
        {
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableNew();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLStatus));
            SellerAgentComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentCheckable();
            ADSLPaymentTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPaymentType));
           
            GroupNameComboBox.ItemsSource = ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();

        }

        #endregion Intitializer

        #region Methods

        public override void Search()
        {
            List<ADSLInfo> Result = LoadData();
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
            stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplaianationTextBox.Text;
            title = "اطلاعات مشترکین ADSL ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLInfo> LoadData()
        {
            long TelNumber = !string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text) ? Convert.ToInt64(TelephoneNoTextBox.Text) : -1;

            List<ADSLInfo> result = ReportDB.GetADSLCustomerInfo
                                                    //( string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? null : (TelephoneNoTextBox.Text),
                                                     (TelNumber,
                                                      string.IsNullOrEmpty(CustomerNameTextBox.Text.Trim()) ? null : (CustomerNameTextBox.Text),
                                                      string.IsNullOrEmpty(UserNameTextBox.Text.Trim()) ? null : (UserNameTextBox.Text),
                                                      ServiceComboBox.SelectedIDs,
                                                      StatusComboBox.SelectedIDs,
                                                      CityCenterComboBox.CityComboBox.SelectedIDs,
                                                      CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                      ADSLPaymentTypeComboBox.SelectedIDs,
                                                      SellerAgentComboBox.SelectedIDs,
                                                      GroupNameComboBox.SelectedIDs);
            return result;
        }
        #endregion Methods
    }
}
