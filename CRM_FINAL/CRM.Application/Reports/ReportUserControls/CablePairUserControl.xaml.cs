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
using System.Collections;
using Stimulsoft.Report;
using System.ComponentModel;
using CRM.Application.UserControls;
using System.Collections.ObjectModel;
using CRM.Application.Reports.Viewer;
using System.Xml.Linq;


namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for CablePairUserControl.xaml
    /// </summary>
    public partial class CablePairUserControl : Local.ReportBase
    {
        public CablePairUserControl()
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

                //CRM.Data.Schema.CablePairStatus t = new Data.Schema.CablePairStatus();
                //ActionLog a = new ActionLog();
                //a.ActionID = 19;
                //a.Date = DB.GetServerDate();
                //a.UserName = "1";
                //t.CablePairID = 2801;
                //t.CenterID = 1;
                //t.LogDateTime = DB.GetServerDate();
                //a.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.CablePairStatus>(t, true));


                //a.Detach();
                //DB.Save(a);


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


            List<Center> centerList = CenterDB.GetAllCenter();
            int CenterID = (int)CenterComboBox.SelectedValue;
            int CityID = (int)CityComboBox.SelectedValue;




            stiReport.Dictionary.Variables["CenterName"].Value = CenterComboBox.Text;
            stiReport.Dictionary.Variables["Region"].Value = CityComboBox.Text;
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            //stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public IEnumerable LoadData()
        {
            List<CablePairReport> result = new List<CablePairReport>();
            List<CablePairActionLogXml> resultLog = new List<CablePairActionLogXml>();
            switch (UserControlID)
            {
                case (int)DB.UserControlNames.CablePairEmpty:
                    result = CableDB.GetCablePairReport((int)CenterComboBox.SelectedValue, (long)CableNoComboBox.SelectedValue, (int)DB.BuchtStatus.Free);
                    resultLog = CableDB.GetCablePairLog((int)DB.ActionLog.CablePairEmpty, (int)CenterComboBox.SelectedValue, (long)CableNoComboBox.SelectedValue, result.Select(x => x.CablePairID).ToList());
                    break;
                case (int)DB.UserControlNames.CablePairFail:
                    result = CableDB.GetCablePairReport((int)CenterComboBox.SelectedValue, (long)CableNoComboBox.SelectedValue, (int)DB.BuchtStatus.Destroy);
                    resultLog = CableDB.GetCablePairLog((int)DB.ActionLog.CablePairFail, (int)CenterComboBox.SelectedValue, (long)CableNoComboBox.SelectedValue, result.Select(x => x.CablePairID).ToList());
                    break;
                case (int)DB.UserControlNames.CablePairFill:
                    result = CableDB.GetCablePairReport((int)CenterComboBox.SelectedValue, (long)CableNoComboBox.SelectedValue, (int)DB.BuchtStatus.Connection);
                    resultLog = CableDB.GetCablePairLog((int)DB.ActionLog.CablePairFill, (int)CenterComboBox.SelectedValue, (long)CableNoComboBox.SelectedValue, result.Select(x => x.CablePairID).ToList());
                    break;
                case (int)DB.UserControlNames.CablePairReserve:
                    result = CableDB.GetCablePairReport((int)CenterComboBox.SelectedValue, (long)CableNoComboBox.SelectedValue, (int)DB.BuchtStatus.Reserve);
                    resultLog = CableDB.GetCablePairLog((int)DB.ActionLog.CablePairReserve, (int)CenterComboBox.SelectedValue, (long)CableNoComboBox.SelectedValue, result.Select(x => x.CablePairID).ToList());
                    break;
                case (int)DB.UserControlNames.CablePairTotal:
                    result = CableDB.GetCablePairReport((int)CenterComboBox.SelectedValue, (long)CableNoComboBox.SelectedValue, null);
                    break;

            }

            List<EnumItem> CablePairStatus = Helper.GetEnumItem(typeof(DB.CablePairStatus));

            List<EnumItem> buchtStatus = Helper.GetEnumItem(typeof(DB.BuchtStatus));
            foreach (CablePairReport item in result)
            {
                item.CablePairStatusName = string.IsNullOrEmpty(item.CablePairStatusID.ToString()) ? "" : CablePairStatus.Find(i => i.ID == item.CablePairStatusID).Name;
                DateTime? TempDT = resultLog.Where(t => t.CablePairID == item.CablePairID).Select(t => t.LogDateTime).FirstOrDefault();
                item.CablePairDate = string.IsNullOrEmpty(TempDT.ToString()) ? "" : Helper.GetPersianDate(TempDT, Helper.DateStringType.Short);
                item.CablePairTime = string.IsNullOrEmpty(TempDT.ToString()) ? "" : Helper.GetPersianDate(TempDT, Helper.DateStringType.Time);
                item.BuchtStatus = string.IsNullOrEmpty(item.BuchtStatus) ? "" : buchtStatus.Find(t => t.ID == int.Parse(item.BuchtStatus)).Name;
            }


            return result;
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
            if (CenterComboBox.SelectedValue != null)
            {
                CableNoComboBox.ItemsSource =
                    CableDB.GetCableCheckableByCenterID(new List<int> { (int)CenterComboBox.SelectedValue });
                CableNoComboBox.SelectedIndex = 0;
            }
        }
    }
}
