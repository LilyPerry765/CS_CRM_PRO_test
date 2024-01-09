using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
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

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for CustomerOfficeReport.xaml
    /// </summary>
    public partial class CustomerOfficeReport : Local.ReportBase
    {
        public CustomerOfficeReport()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            RequestTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.RequestType));
            RoleComboBox.ItemsSource = RoleDB.GetRolesCheckable();
        }

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void RequestTypeComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void RoleComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        public override void Search()
        {
            List<CustomerOfficeReportInfo> result = LoadData();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            title = " دفاتر خدماتی  ";
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();



            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }


        private List<CustomerOfficeReportInfo> LoadData()
        {

            List<CustomerOfficeReportInfo> result =
            ReportDB.GetCustomerOfficeInfo(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, RequestTypeComboBox.SelectedIDs, RoleComboBox.SelectedIDs);

            result.ForEach(t =>
            {
                t.InsertDatePersian = (string.IsNullOrEmpty(t.InsertDate.ToString())) ? "" : Helper.GetPersianDate(t.InsertDate, Helper.DateStringType.Short);
                t.EndDatePersian = (string.IsNullOrEmpty(t.EndDate.ToString())) ? "" : Helper.GetPersianDate(t.EndDate, Helper.DateStringType.Short);
            }
);
            return result;
        }
    }
}
