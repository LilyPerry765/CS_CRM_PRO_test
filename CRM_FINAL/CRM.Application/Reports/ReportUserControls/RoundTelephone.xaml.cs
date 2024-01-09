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
    /// Interaction logic for RoundTelephone.xaml
    /// </summary>
    public partial class RoundTelephone : Local.ReportBase
    {
        public RoundTelephone()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TelephoneStatus));
            RoundTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.RoundType));
        }

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
            PreCodeComboBox.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        public override void Search()
        {
            List<RoundTelephoneReportInfo> result = LoadData();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            title = " تلفن های رند  ";
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();



            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }


        private List<RoundTelephoneReportInfo> LoadData()
        {

            List<RoundTelephoneReportInfo> result =
            ReportDB.GetRoundTelephoneInfo(
            CityComboBox.SelectedIDs,
            CenterComboBox.SelectedIDs,
            CabinetComboBox.SelectedIDs,
            PreCodeComboBox.SelectedIDs,
            StatusComboBox.SelectedIDs,
            RoundTypeComboBox.SelectedIDs
            );

            result.ForEach(t =>
            {
                t.Status = t.Status != null ? Helper.GetEnumDescriptionByValue(typeof(DB.TelephoneStatus) , int.Parse(t.Status) ) : "";
                t.RoundType = t.RoundType != null ? Helper.GetEnumDescriptionByValue(typeof(DB.RoundType) , int.Parse(t.RoundType)) : "";
            });
            return result;
        }
    }
}
