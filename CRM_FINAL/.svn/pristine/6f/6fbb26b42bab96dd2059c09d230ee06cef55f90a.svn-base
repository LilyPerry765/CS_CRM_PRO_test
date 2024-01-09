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
using System.Reflection;
using CRM.Application.Reports.Viewer;


namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for RegisterReportUserControl.xaml
    /// </summary>
    public partial class RegisterReportUserControl : Local.ReportBase
    {

        #region Constructor

        public RegisterReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            ChargingTypecomboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ChargingGroup));
            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();
            PosessionTypecomboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PossessionType));
            OrderTypecomboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.OrderType));
            TelephoneTypeGroupComboBox.ItemsSource = Data.CustomerGroupDB.GetCustomerGroupsCheckable();
            ReportTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.InstallRequestType));
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }


        #endregion Initializer

        #region Methods

        public override void Search()
        {
            if (ReportTypeComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("لطفا نوع گزارش مورد نظر خود را انتخاب بفرمایید");
            }
            else
            {
                List<RegisterInfo> Result = LoadData();
                string title = string.Empty;
                string path;
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                path = ReportDB.GetReportPath(UserControlID);
                stiReport.Load(path);
                stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

                if (FromDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
                if (ToDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();


                ff.ItemsSource = Result;
                title = "ثبت نام";
                stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("Result", "Result", Result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
        }

        private List<RegisterInfo> LoadData()
        {
            long FromtelNo = (!string.IsNullOrEmpty(FromTelNoTextBox.Text)) ? Convert.ToInt64(FromTelNoTextBox.Text) : -1;
            long ToTelNo = (!string.IsNullOrEmpty(ToTelNoTextBox.Text)) ? Convert.ToInt64(ToTelNoTextBox.Text) : -1;

            List<CounterLastInfo> CounterLastInfo = ReportDB.GetCounterLast();

            List<RegisterInfo> Result = ReportDB.GetRegisterRequestInfo(FromDate.SelectedDate, ToDate.SelectedDate,
                                                                                    FromtelNo, ToTelNo,
                                                                                     (int?)TelephoneTypeComboBox.SelectedValue,
                                                                                     (int?)TelephoneTypeGroupComboBox.SelectedValue,
                                                                                     (int?)ChargingTypecomboBox.SelectedValue,
                                                                                     (int?)OrderTypecomboBox.SelectedValue,
                                                                                     (int?)PosessionTypecomboBox.SelectedValue,
                                                                                     (int?)ReportTypeComboBox.SelectedValue, CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs);

            return Result;
        }

        #endregion Methods

        #region EventHandlers

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        #endregion

    }
}
