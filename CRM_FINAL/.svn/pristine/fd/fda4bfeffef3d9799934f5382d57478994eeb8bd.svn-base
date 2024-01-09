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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for EmptyPCMsReportUserControl.xaml
    /// </summary>
    public partial class EmptyPCMsReportUserControl : Local.ReportBase
    {
        public EmptyPCMsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            PCMUC.PCMTypelbl.Visibility = System.Windows.Visibility.Collapsed;
            PCMUC.PCMTypeComboBox.Visibility = System.Windows.Visibility.Collapsed;
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            CityComboBox.SelectedIndex = 0;
            UpdateElemans();
        }
        public void UpdateElemans()
        {
            PCMUC.Rock_Update((int)CenterComboBox.SelectedValue);
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

            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public IEnumerable LoadData()
        {
            List<PCMStatisticDetails> result = PCMDB.GetEmptyPCMs((int?)CenterComboBox.SelectedValue,
                                                                                (int?)PCMUC.PCMBrandComboBox.SelectedValue,
                                                                                (int?)PCMUC.RockComboBox.SelectedValue,
                                                                                (int?)PCMUC.ShelfComboBox.SelectedValue,
                                                                                (int?)PCMUC.CardComboBox.SelectedValue
                                                                                , (bool)InstallRadioButton.IsChecked);
            List<EnumItem> ps = Helper.GetEnumItem(typeof(DB.PCMStatus));
            foreach (PCMStatisticDetails item in result)
            {
                
                if (item.BuchtType == (int)DB.BuchtType.InLine) // ورودی
                    item.InputBucht = DB.GetConnectionByBuchtID(item.BuchtID);
                else if (item.BuchtType == (int)DB.BuchtType.OutLine) // خروجی
                {
                    string OutputBucht = DB.GetConnectionByBuchtID(item.BuchtID);

                    string ConnectionIDBucht = string.IsNullOrEmpty(item.ConnectionIDBucht) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(item.ConnectionIDBucht));
                    List<PCMStatisticDetails> Temp = result.Where(t => (t.RockID == item.RockID && t.ShelfID == item.ShelfID && t.CardID == item.CardID && t.BuchtType == (int)DB.BuchtType.InLine)).ToList();
                    foreach (PCMStatisticDetails i in Temp)
                    {
                        i.OutputBucht = OutputBucht;
                        i.ConnectionIDBucht = ConnectionIDBucht;
                    }
                }
                

                item.PCMStatus = string.IsNullOrEmpty(item.PCMStatus) ? (string)null : ps.Find(t => t.ID == int.Parse(item.PCMStatus)).Name;

            }

            return result.Where(t => t.BuchtType == (int)DB.BuchtType.CustomerSide || t.BuchtType == (int)DB.BuchtType.InLine).ToList();
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityComboBox.SelectedValue != null)
            {
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableByCityId((int)CityComboBox.SelectedValue);
                CenterComboBox.SelectedIndex = 0;
            }
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null) UpdateElemans();
        }
    }
}
