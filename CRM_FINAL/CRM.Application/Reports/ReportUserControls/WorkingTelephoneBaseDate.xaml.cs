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
    /// Interaction logic for WorkingTelephone.xaml
    /// </summary>
    public partial class WorkingTelephoneBaseDate : Local.ReportBase
    {
        public WorkingTelephoneBaseDate()
        {
            InitializeComponent();
            Initialize();
        }

        #region Initializer

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }
        #endregion  Initializer

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        public override void Search()
        {
            List<WorkingTelephoneBaseDateReport> result = LoadData();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            title = " تلفن های مشغول به کار  ";
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();



            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }


        private List<WorkingTelephoneBaseDateReport> LoadData()
        {

            List<WorkingTelephoneBaseDateReport> result =
            ReportDB.GetWorkingTelephonBaseDate(CityComboBox.SelectedIDs,
                                        CenterComboBox.SelectedIDs,
                                        FromDate.SelectedDate ,
                                        ToDate.SelectedDate);

            result.ForEach(t => t.InstallationDatePersian = t.InstallationDate.ToPersian(Date.DateStringType.Short));
            return result;
        }


    }
}
