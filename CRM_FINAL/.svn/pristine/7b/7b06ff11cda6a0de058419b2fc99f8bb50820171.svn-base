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
using CRM.Data;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PostStatusReportUserControl.xaml
    /// </summary>
    public partial class PostStatusStatisticReportUserControl : Local.ReportBase
    {
        public PostStatusStatisticReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckable();
        }
        private void CabinetComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CabinetComboBox.SelectedIDs.Count != 0)
            {
                List<Post> Postlst = PostDB.GetPostsByCabinetID(CabinetComboBox.SelectedIDs);
                PostComboBox.ItemsSource = PostDB.GetPostCheckableByCabinetID(CabinetComboBox.SelectedIDs);
            }
        }
        public override void Search()
        {
            IEnumerable result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            string CenterIds = string.Empty;
            string RegionIds = string.Empty;
            List<Center> centerList = CenterDB.GetAllCenter();

            stiReport.Dictionary.Variables["CenterName"].Value = CenterIds;
            stiReport.Dictionary.Variables["Region"].Value = RegionIds;
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(RequestPropertisUC.FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(RequestPropertisUC.ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["TelephoneNO"].Value = RequestPropertisUC.TelephoneNo.Text.Trim();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();




            stiReport.RegData("Result", "Result", result);
            stiReport.CompileStandaloneReport("", StiStandaloneReportType.ShowWithWpf);
            stiReport.Render(true);
            stiReport.ShowWithWpf(true);
        }
        private IEnumerable LoadData()
        {
            IEnumerable result = null;// ReportDB.GetCabinetInputFailure(RequestPropertisUC.FromDate.SelectedDate
                                      //                         , RequestPropertisUC.ToDate.SelectedDate
                                      //                         , string.IsNullOrEmpty(RequestPropertisUC.RequestIdFrom.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.RequestIdFrom.Text.Trim())
                                      //                         , string.IsNullOrEmpty(RequestPropertisUC.RequestIdTo.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.RequestIdTo.Text.Trim())
                                      //                         , RequestPropertisUC.CenterCheckableComboBox.SelectedIDs
                                      //                         , string.IsNullOrEmpty(RequestPropertisUC.TelephoneNo.Text.Trim()) ? (long?)null : long.Parse(RequestPropertisUC.TelephoneNo.Text.Trim())
                                      //                         , LineStatusCheckableCombobox.SelectedIDs
                                      //                         , FailureStatusCheckableCombobox.SelectedIDs
                                      //                         , int.Parse(FailureCombobox.SelectedValue.ToString())
                                      //                         , CabinetComboBox.SelectedIDs
                                      //                         , PostComboBox.SelectedIDs
                                      //                         , string.IsNullOrEmpty(RequestPropertisUC.IdentityId.Text.Trim()) ? null : RequestPropertisUC.IdentityId.Text.Trim());





            return result;
        }

    }
}
