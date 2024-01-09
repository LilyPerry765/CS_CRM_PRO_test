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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using System.Collections;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PCMsStatisticReportUserControl.xaml
    /// </summary>
    public partial class PCMsStatisticReportUserControl : Local.ReportBase
    {
        //private bool update;
        //public bool Update
        //{
        //    set 
        //    {
        //        update = value;
        //        UpdateElemans();
        //    }
        //    get
        //    {
        //        return update;
        //    }
        //}
        public PCMsStatisticReportUserControl()
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
                PCMUC.PCMTypelbl.Visibility = System.Windows.Visibility.Collapsed;
                PCMUC.PCMTypeComboBox.Visibility = System.Windows.Visibility.Collapsed;

                List<CheckableItem> Temp = Helper.GetEnumCheckable(typeof(DB.PCMPortStatus));
                Temp.AddRange(Helper.GetEnumCheckable(typeof(DB.selectAll)).ToList());
                StatusComboBox.ItemsSource = Temp;//Helper.GetEnumCheckable(typeof(DB.PCMPortStatus)).AddRange();
                StatusComboBox.SelectedIndex = 4;
                UpdateElemans();
            }
        }
        public void UpdateElemans()
        {
            PCMUC.Rock_Update((int)CenterComboBox.SelectedValue);
        }
        public override void Search()
        {
            List<PCMStatisticDetails> result = LoadData();
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
        private List<PCMStatisticDetails> LoadData()
        {

            List<PCMStatisticDetails> result = ReportDB.GetPCMStatisticDetails((int?)CityComboBox.SelectedValue, 
                                                                                (int?)CenterComboBox.SelectedValue,
                                                                                (int?)PCMUC.PCMBrandComboBox.SelectedValue,
                                                                                (int?)PCMUC.RockComboBox.SelectedValue,
                                                                                (int?)PCMUC.ShelfComboBox.SelectedValue,
                                                                                (int?)PCMUC.CardComboBox.SelectedValue,
                                                                                CabinetComboBox.SelectedIDs,
                                                                                (int)StatusComboBox.SelectedValue);
            List<EnumItem> ps = Helper.GetEnumItem(typeof(DB.PCMStatus));
            foreach (PCMStatisticDetails item in result)
            {
                if (item.BuchtType == (int)DB.BuchtType.OutLine) // ورودی
                    item.InputBucht = DB.GetConnectionByBuchtID(item.BuchtID);
                else if (item.BuchtType == (int)DB.BuchtType.InLine) // خروجی
                {
                    string OutputBucht = DB.GetConnectionByBuchtID(item.BuchtID);

                    string ConnectionIDBucht = string.IsNullOrEmpty(item.ConnectionIDBucht) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(item.ConnectionIDBucht));
                    List<PCMStatisticDetails> Temp = result.Where(t => (t.RockID == item.RockID && t.ShelfID == item.ShelfID && t.CardID == item.CardID && t.BuchtType == (int)DB.BuchtType.OutLine)).ToList();
                    foreach (PCMStatisticDetails i in Temp)
                    {
                        i.OutputBucht = OutputBucht;
                        i.ConnectionIDBucht = ConnectionIDBucht;
                    }


                }
                item.PCMStatus = string.IsNullOrEmpty(item.PCMStatus) ? (string)null : ps.Find(t => t.ID == int.Parse(item.PCMStatus)).Name;

            }

            return result.Where(t => t.BuchtType == (int)DB.BuchtType.CustomerSide || t.BuchtType == (int)DB.BuchtType.OutLine).ToList();
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                UpdateElemans();
                CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID((int)CenterComboBox.SelectedValue);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityComboBox.SelectedValue != null)
            {
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableByCityId((int)CityComboBox.SelectedValue);
                CenterComboBox.SelectedIndex = 0;
            }
        }
        
    }
}

