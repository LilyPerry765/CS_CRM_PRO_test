using System;
using System.Collections.Generic;
using System.ComponentModel;
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



using CRM.Application.UserControls;
using System.Collections;
using CRM.Application.Reports.Viewer;
using System.Text.RegularExpressions;
namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for InputStatisticReportUserControl.xaml
    /// </summary>
    public partial class InputStatisticReportUserControl : Local.ReportBase
    {
        public InputStatisticReportUserControl()
        {
            InitializeComponent();
            Initialize();

        }
        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
                CityComboBox.SelectedIndex = 0;
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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



            stiReport.Dictionary.Variables["CenterName"].Value = CenterComboBox.Text;
            stiReport.Dictionary.Variables["Region"].Value = CityComboBox.Text;
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public IEnumerable LoadData()
        {
            List<InputInfo> result =
                CabinetDB.GetInputStatistic((int)CenterComboBox.SelectedValue,
                                            CabinetComboBox.SelectedIDs,
                                             string.IsNullOrEmpty(InputCabinetText.Text.Trim()) ? -1 : int.Parse(InputCabinetText.Text.Trim()));

            List<EnumItem> status = Helper.GetEnumItem(typeof(DB.BuchtStatus));
            List<EnumItem> BuchtType = Helper.GetEnumItem(typeof(DB.BuchtType));

            switch (UserControlID)
            {
                case (int)DB.UserControlNames.CabinetInputEmpty:
                    result = result.Where(t => t.BuchtStatus == (int)DB.BuchtStatus.Free).ToList();
                    break;
                case (int)DB.UserControlNames.CabinetInputFail:
                    result.Clear();
                    result = ReportDB.GetCabinetInputFail((int)CenterComboBox.SelectedValue,
                                            CabinetComboBox.SelectedIDs,
                                             string.IsNullOrEmpty(InputCabinetText.Text.Trim()) ? -1 : int.Parse(InputCabinetText.Text.Trim()));

                    break;
                case (int)DB.UserControlNames.CabinetInputFill:
                    result = result.Where(t => t.BuchtStatus == (int)DB.BuchtStatus.Connection).ToList();
                    break;
                case (int)DB.UserControlNames.CabinetInputTotal:
                    foreach (InputInfo item in result)
                    {
                        item.BuchtStatusName = status.Find(i => i.ID == item.BuchtStatus).Name;
                    }
                    break;
                case (int)DB.UserControlNames.CabinetInputReserve:
                    result = result.Where(t => t.BuchtStatus == (int)DB.BuchtStatus.Reserve).ToList();
                    break;
            }




            return result;
        }
        private void CenterComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(new List<int> { (int)CenterComboBox.SelectedValue });
                CabinetComboBox.SelectedIndex = 0;
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityComboBox.SelectedValue != null)
            {
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(new List<int> { (int)CityComboBox.SelectedValue });
                CenterComboBox.SelectedIndex = 0;
            }
        }

    }
}
