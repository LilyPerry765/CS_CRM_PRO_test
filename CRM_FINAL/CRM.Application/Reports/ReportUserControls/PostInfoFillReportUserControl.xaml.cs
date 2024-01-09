using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PostInfoReserveReportUserControl.xaml
    /// </summary>
    public partial class PostInfoFillReportUserControl : Local.ReportBase
    {
        public PostInfoFillReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            CityComboBox.SelectedIndex = 0;
            PCMTypeComboBox.ItemsSource = Data.PCMTypeDB.GetPCMTypeCheckable();
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
            title = "گزارش اطلاعات فنی پست";


            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();



            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("result", "result", result);

   
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public List<PostInfoFill> LoadData()
        {



            List<PostInfoFill> result = ReportDB.GetPostInfoFill(new List<int> { (int)CenterComboBox.SelectedValue },
                                                     CabinetComboBox.SelectedIDs,
                                                     string.IsNullOrEmpty(PostContactLessThanTextBox.Text.Trim()) ? (int?)null : int.Parse(PostContactLessThanTextBox.Text.Trim()),
                                                     string.IsNullOrEmpty(PostContactMoreThanTextBox.Text.Trim()) ? (int?)null : int.Parse(PostContactMoreThanTextBox.Text.Trim()),
                                                     string.IsNullOrEmpty(PortLessThanTextBox.Text.Trim()) ? (int?)null : int.Parse(PortLessThanTextBox.Text.Trim()),
                                                     string.IsNullOrEmpty(PortMoreThanTextBox.Text.Trim()) ? (int?)null : int.Parse(PortMoreThanTextBox.Text.Trim()),
                                                     PCMTypeComboBox.SelectedIDs);



           
            return result;

        }

        
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableByCityId((int)CityComboBox.SelectedValue);
            CenterComboBox.SelectedIndex = 0;
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(new List<int> { (int)CenterComboBox.SelectedValue });
        }


        



    }
}
