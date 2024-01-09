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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Base;
using Stimulsoft.Report;


namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ChangeTitleIn118ReportUserControl.xaml
    /// </summary>
    public partial class ChangeTitleIn118ReportUserControl : Local.ReportBase
    {
        public ChangeTitleIn118ReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {

            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            TitleStatusComboBox.ItemsSource = new List<CheckableItem> { new CheckableItem { ID = (int)DB.RequestType.TitleIn118, Description = "ثبت عنوان" }, new CheckableItem { ID = (int)DB.RequestType.RemoveTitleIn118, Description = "حذف عنوان" }, new CheckableItem { ID = (int)DB.RequestType.ChangeTitleIn118, Description = "تغییر عنوان" } };
        }

        
         #region Event Handlers
        
        #endregion  Event Handlers

        #region Methods

        public override void Search()
        {
            List<TitleIn118Info> result = LoadData();
            string title = string.Empty;
            string path;

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["TelephoneNO"].Value =PhoneNoTextBox.Text.Trim();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            //if (RegionIdComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["Region"].Value = RegionIdComboBox.Text;
            //}

            //if (CenterIdComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["CenterName"].Value = CenterIdComboBox.Text;
            //}

            title = "گزارش تغییر عنوان در 118 ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", result);
           
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<TitleIn118Info> LoadData()
        {
            List<TitleIn118Info> result = ReportDB.GetChangeTitleIn118Info(
                                                                 CityComboBox.SelectedIDs,
                                                                 CenterComboBox.SelectedIDs,
                                                                 FromDate.SelectedDate,
                                                                 ToDate.SelectedDate,
                                                                 string.IsNullOrEmpty(RequestNoTextBox.Text.Trim()) ? (long?)null : long.Parse(RequestNoTextBox.Text),
                                                                 TitleStatusComboBox.SelectedIDs ,
                                                                 string.IsNullOrEmpty(PhoneNoTextBox.Text.Trim()) ? (string)null : PhoneNoTextBox.Text.Trim(),
                                                                 string.IsNullOrEmpty(NationalIdTextBox.Text.Trim()) ? (string)null : NationalIdTextBox.Text.Trim());
            List<EnumItem> TitleIn118RequestMode = new List<EnumItem> { new EnumItem { ID = (int)DB.RequestType.TitleIn118, Name = "ثبت عنوان" }, new EnumItem { ID = (int)DB.RequestType.RemoveTitleIn118, Name = "حذف عنوان" }, new EnumItem { ID = (int)DB.RequestType.ChangeTitleIn118,  Name = "تغییر عنوان" } };

            foreach (TitleIn118Info TI118 in result)
            {
                TI118.ChangeTitleStatus = TitleIn118RequestMode.Find(item => item.ID == int.Parse(TI118.ChangeTitleStatus)).Name;
            }

            return result;
        }
        #endregion  Methods

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
 