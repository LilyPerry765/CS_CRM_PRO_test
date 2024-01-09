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
    /// Interaction logic for ADSLAdditionalServiceSaleReportUserControl.xaml
    /// </summary>
    public partial class ADSLAdditionalServiceSaleReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ADSLAdditionalServiceSaleReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckable();
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<ADSLServiceInfo> Result = LoadData();
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
            stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplainationTextBox.Text;

            if (fromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(fromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (toDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(toDate.SelectedDate, Helper.DateStringType.Short).ToString();

            foreach (ADSLServiceInfo info in Result)
            {
                if (info.SaleWayByte == 1 || info.SaleWayByte == 2)
                {
                    info.SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 1);
                }
                if (info.SaleWayByte == 3)
                    info.SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 2);

                if (info.SaleWayByte == 4)
                    info.SaleWay = DB.GetEnumDescriptionByValue(typeof(DB.ADSLSellChanellLimited), 3);

            }
            for (int i = Result.Count - 1; i > 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (Result[i].SaleWay == Result[j].SaleWay  && Result[i].ID == Result[j].ID)
                    {
                        Result[i].NumberOfSoldTraffic = (Convert.ToInt32(Result[i].NumberOfSoldTraffic) + Convert.ToInt32(Result[j].NumberOfSoldTraffic)).ToString();
                        Result[i].SoldTraffic = (Convert.ToInt32(Result[j].SoldTraffic) + Convert.ToInt32(Result[i].SoldTraffic)).ToString();
                        Result.Remove(Result[j]);
                    }

                }
            }

            title = " تعداد و ميزان ترافيک اضافي فروخته شده به تفکيک سرويس ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ADSLServiceInfo> LoadData()
        {
            DateTime? ToDate = null;
            if (toDate.SelectedDate.HasValue)
            {
                ToDate = toDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLServiceInfo> result = ReportDB.GetADSLAdditinalTrafficSaleInfo(ServiceComboBox.SelectedIDs,
                                                                                 fromDate.SelectedDate,
                                                                                 ToDate,
                                                                                 CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                 CityCenterComboBox.CenterComboBox.SelectedIDs);
            return result;
        }

        #endregion Methods
    }
}
