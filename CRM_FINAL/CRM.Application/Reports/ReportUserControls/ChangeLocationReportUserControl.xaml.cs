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
using Stimulsoft.Report;
using Stimulsoft.Base;
using CRM.Application.Reports.Viewer;


namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ChangeLocationReportUserControl.xaml
    /// </summary>
    public partial class ChangeLocationReportUserControl : Local.ReportBase
    {
        public ChangeLocationReportUserControl()
        {
            InitializeComponent(); Initialize();
        }
        private void Initialize()
        {

            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            ChangeCenterTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ChangeLocationCenterType));
            ChangeNumberComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ChangeNumberType));
        }
        #region Event Handlers

        #endregion Event Handlers

        #region Methods

        public override void Search()
        {
            List<ChangeLocationProcessInfo> result = LoadData();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            title = "گزارش تغییر مکان";

            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            //if (regionCenterUserControl.RegionComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["Region"].Value = regionCenterUserControl.RegionComboBox.Text;
            //}
            //if (regionCenterUserControl.CenterComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["CenterName"].Value = regionCenterUserControl.CenterComboBox.Text;
            //}


            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }


        private List<ChangeLocationProcessInfo> LoadData()
        {

            using (MainDataContext context = new MainDataContext())
            {
                List<ChangeLocationProcessInfo> result =
                ReportDB.GetChangeLocationProcessInfo(
                                         CityComboBox.SelectedIDs,
                                         CenterComboBox.SelectedIDs,
                                         FromDate.SelectedDate,
                                         ToDate.SelectedDate,
                                         string.IsNullOrEmpty(txtRequestNo.Text.Trim()) ? (long?)null : long.Parse(txtRequestNo.Text),
                                         string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim(),
                                         ChangeCenterTypeComboBox.SelectedIDs,
                                         ChangeNumberComboBox.SelectedIDs,
                                         string.IsNullOrEmpty(CustomerNationalIDTextBox.Text.Trim()) ? (string)null : CustomerNationalIDTextBox.Text,
                                         ChangeNameCheckBox.IsChecked
                                         );
                //result = FilterByChangeNumber(ChangeNumberComboBox.SelectedIDs, result);
                //List<CounterLastInfo> CLI = new List<CounterLastInfo>();
                //var q = from t in context.Counters
                //        join countertemp in
                //            (
                //                (from t0 in
                //                     (
                //                         (from t01 in context.Counters
                //                          group t01 by new
                //                          {
                //                              t01.TelephoneNo
                //                          } into g
                //                          select new
                //                          {
                //                              TelNo = (System.Int64?)g.Key.TelephoneNo,
                //                              DateTimeRead = g.Max(p => p.CounterReadDate)
                //                          }))
                //                 select new
                //                 {
                //                     t0.TelNo,
                //                     t0.DateTimeRead
                //                 })) on new { TelNo = t.TelephoneNo } equals new { TelNo = Convert.ToInt64(countertemp.TelNo) }
                //        where
                //          Convert.ToDateTime(countertemp.DateTimeRead) == t.CounterReadDate
                //        select new CounterLastInfo
                //        {
                //            CounterNo = t.CounterNo,
                //            TelephonNo = t.TelephoneNo
                //        };

                //CLI = q.ToList();


                List<EnumItem> ChangeLocationCenterTypeList = Helper.GetEnumItem(typeof(DB.ChangeLocationCenterType));

                foreach (ChangeLocationProcessInfo changeLocationInfo in result)
                {
                                       //if (!String.IsNullOrEmpty(changeLocationInfo.OldTelephone))
                    //{
                    //    CounterLastInfo Temp = CLI.Find(item => item.TelephonNo == long.Parse(changeLocationInfo.OldTelephone));
                    //    if (Temp != null)
                    //        changeLocationInfo.OldCounterNo = Temp.CounterNo;
                    //}
                    //if (!String.IsNullOrEmpty(changeLocationInfo.NewTelephone))
                    //{
                    //    CounterLastInfo Temp = CLI.Find(item => item.TelephonNo == long.Parse(changeLocationInfo.NewTelephone));
                    //    if (Temp != null)
                    //        changeLocationInfo.NewCounterNo = Temp.CounterNo;
                    //}
                    //if (string.IsNullOrEmpty(changeLocationInfo.NewTelephone))
                    //    changeLocationInfo.NewTelephone = changeLocationInfo.OldTelephone;
                    changeLocationInfo.ChangeLocationType = ChangeLocationCenterTypeList.Find(item => item.ID == int.Parse(changeLocationInfo.ChangeLocationType)).Name;
                    changeLocationInfo.RequestLetterDate = string.IsNullOrEmpty(changeLocationInfo.RequestLetterDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(changeLocationInfo.RequestLetterDate), Helper.DateStringType.Short);


                }
                return result;
            }
        }

        private List<ChangeLocationProcessInfo> FilterByChangeNumber(List<int> ChangeNoType, List<ChangeLocationProcessInfo> result)
        {
           foreach(ChangeLocationProcessInfo i in result )
            {
               
                if ((ChangeNoType.Contains((int)DB.ChangeNumberType.ChangeNumber)
                    && !ChangeNoType.Contains((int)DB.ChangeNumberType.UnChangeNumber)))
                    result = result.Where(t => t.NewTelephone != null).ToList();

                else if ((!ChangeNoType.Contains((int)DB.ChangeNumberType.ChangeNumber)
                    && ChangeNoType.Contains((int)DB.ChangeNumberType.UnChangeNumber)))
                    result = result.Where(t => t.NewTelephone == null).ToList();   
           }
           return result;
        }

       
        #endregion Method

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}