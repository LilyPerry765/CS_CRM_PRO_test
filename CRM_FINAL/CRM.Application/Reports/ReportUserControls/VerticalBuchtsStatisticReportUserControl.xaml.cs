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
using System.Collections;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for VerticalBuchtsStatisticReportUserControl.xaml
    /// </summary>
    public partial class VerticalBuchtsStatisticReportUserControl : Local.ReportBase
    {
        public VerticalBuchtsStatisticReportUserControl()
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
                BuchtTypeComboBox.ItemsSource = BuchtTypeDB.GetBuchtTypeCheckable();
                BuchtTypeComboBox.SelectedIndex = 0;
            }
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

            string CenterIds = string.Empty;
            string RegionIds = string.Empty;
            List<Center> centerList = CenterDB.GetAllCenter();


            stiReport.Dictionary.Variables["CenterName"].Value = CenterComboBox.Text;
            stiReport.Dictionary.Variables["Region"].Value = RegionIds;
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public IEnumerable LoadData()
        {
            List<VerticalBuchtReportInfo> result =
            MDFDB.GetVerticalBuchtStatistic((int)CenterComboBox.SelectedValue,
                                             (int)BuchtTypeComboBox.SelectedValue,
                                              MDFComboBox.SelectedValue.ToString(),
                                              ConnectionColumnComboBox.SelectedValue.ToString(),
                                              ConnectionRowComboBox.SelectedValue.ToString());

            switch (UserControlID)
            {
                case (int)DB.UserControlNames.VerticalBuchtEmpty:
                    result = result.Where(t => t.Status == (int)DB.BuchtStatus.Free).ToList();
                    break;
                case (int)DB.UserControlNames.VerticalBuchtFail:
                    result.Clear();
                    result = result.Where(t => t.Status == (int)DB.BuchtStatus.Destroy).ToList();

                    break;
                case (int)DB.UserControlNames.VerticalBuchtFill:
                    result = result.Where(t => t.Status == (int)DB.BuchtStatus.Connection).ToList();
                    break;
                case (int)DB.UserControlNames.VerticalBuchtTotal:

                    break;
                case (int)DB.UserControlNames.VerticalBuchtReserve:
                    result = result.Where(t => t.Status == (int)DB.BuchtStatus.Reserve).ToList();
                    break;
            }

            List<EnumItem> status = Helper.GetEnumItem(typeof(DB.BuchtStatus));
            List<EnumItem> BuchtType = Helper.GetEnumItem(typeof(DB.BuchtType));
            foreach (VerticalBuchtReportInfo item in result)
            {
                item.StatusName = (!item.Status.HasValue) ? "" : status.Find(i => i.ID == item.Status).Name;
            }

            return result;
        }
        #region Event
        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityComboBox.SelectedValue != null)
            {
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(new List<int> { (int)CityComboBox.SelectedValue });
                CenterComboBox.SelectedIndex = 0;
            }
        }

        private void CenterComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID((int)CenterComboBox.SelectedValue);
                MDFComboBox.SelectedIndex = 0;
            }
        }

        private void MDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MDFComboBox.SelectedValue != null)
            {
                ConnectionColumnComboBox.ItemsSource = DB.GetConnectionColumnInfo((int)MDFComboBox.SelectedValue);
                ConnectionColumnComboBox.SelectedIndex = 0;
            }
        }

        private void ConnectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionColumnComboBox.SelectedValue != null)
            {
                ConnectionRowComboBox.ItemsSource = DB.GetConnectionRowInfo((int)ConnectionColumnComboBox.SelectedValue);
                ConnectionRowComboBox.SelectedIndex = 0;
            }
        }
        #endregion
    }
}
