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
    /// Interaction logic for ChangeNameReportUserControl.xaml
    /// </summary>
    public partial class ChangeNameReportUserControl :Local.ReportBase
    {
        #region Constructor
        
        public ChangeNameReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        #endregion  Constructor

        #region Methods

        public override void Search()
        {
            List<ChangeNameInfo> result = LoadData();
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
            stiReport.Dictionary.Variables["TelephoneNO"].Value = PhoneNoTextBox.Text.Trim();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            //if (regionCenterUserControl.RegionComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["Region"].Value = regionCenterUserControl.RegionComboBox.Text;
            //}

            //if (regionCenterUserControl.CenterComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["CenterName"].Value = regionCenterUserControl.CenterComboBox.Text;
            //}

            title = "گزارش تغییر نام مالک تلفن ";
            stiReport.Dictionary.Variables["Header"].Value = title;

            stiReport.RegData("Result", "Result", result);
 
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ChangeNameInfo> LoadData()
        {
            List<ChangeNameInfo> result = ReportDB.GetChangeNameInfo(CityComboBox.SelectedIDs,
                                                                     CenterComboBox.SelectedIDs,
                                                                    FromDate.SelectedDate
                                                                    , ToDate.SelectedDate
                                                                    , RequestNoTextBox.Text == string.Empty ? (long?)null : long.Parse(RequestNoTextBox.Text.Trim())
                                                                    , string.IsNullOrEmpty(PhoneNoTextBox.Text) ? (long?)null : long.Parse(PhoneNoTextBox.Text.ToString()));
            
            foreach (ChangeNameInfo changeNameInfo in result)
            {
                if (changeNameInfo.OldCustomerNationalCode != null)
                {
                    Customer cutstomerID = Data.CustomerDB.GetCustomerByNationalCode(changeNameInfo.OldCustomerNationalCode);
                    changeNameInfo.OldFirstNameOrTitle = cutstomerID.FirstNameOrTitle + " " + cutstomerID.LastName;
                    changeNameInfo.OldCustomerNationalCode = cutstomerID.NationalCodeOrRecordNo;
                }

                if (changeNameInfo.NewCustomerNationalCode != null)
                {
                    Customer cNew = Data.CustomerDB.GetCustomerByNationalCode(changeNameInfo.NewCustomerNationalCode);
                    changeNameInfo.NewFirstNameOrTitle = cNew.FirstNameOrTitle + " " + cNew.LastName;
                    changeNameInfo.NewCustomerNationalCode = cNew.NationalCodeOrRecordNo;
                }

                changeNameInfo.RequestDate = changeNameInfo.RequestDate;
                changeNameInfo.RequestLetterDate = string.IsNullOrEmpty(changeNameInfo.RequestLetterDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(changeNameInfo.RequestLetterDate), Helper.DateStringType.Short);
    
            }

            //if (PhoneNoTextBox.Text.Trim() != string.Empty)
            //{
            //    long TelNo = long.Parse(PhoneNoTextBox.Text.Trim());
            //    result = result.Where(t => (t.TelephoneNo == TelNo.to)).ToList();
            //}

            if (NationalIDTextBox.Text.Trim() != string.Empty)
            {
                result = result.Where(t => (t.OldCustomerNationalCode == NationalIDTextBox.Text.Trim() || t.NewCustomerNationalCode == NationalIDTextBox.Text.Trim())).ToList();
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
