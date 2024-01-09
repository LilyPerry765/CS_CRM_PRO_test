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
    /// Interaction logic for ADSLChangeServiceReportUserControl.xaml
    /// </summary>
    public partial class ADSLChangeServiceReportUserControl : Local.ReportBase
    {
       
        #region Constructor

        public ADSLChangeServiceReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            SaleChannelComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSellChanellLimited));
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<ADSLChangeServiceInfo> Result = LoadData();
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

            foreach (ADSLChangeServiceInfo info in Result)
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
                for (int j = i - 1; j >=0 ; j--)
                {
                    if (Result[i].SaleWay == Result[j].SaleWay && Result[i].NewServiceTitle == Result[j].NewServiceTitle)
                    {
                        Result[i].NumberOfRequest = (Convert.ToInt32(Result[i].NumberOfRequest) + Convert.ToInt32(Result[j].NumberOfRequest)).ToString();
                        Result.Remove(Result[j]);
                        i--;
                    }

                }
            }
            

            title = " تعداد درخواست هاي تمديد  سرويس به تفکيک کانال فروش در بازه زماني مشخص ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ADSLChangeServiceInfo> LoadData()
        {
            DateTime? ToDate = null;
            if (toDate.SelectedDate.HasValue)
            {
                ToDate = toDate.SelectedDate.Value.AddDays(1);
            }
            List<int> SaleChanell = new List<int>();
            //if (SaleChannelComboBox.SelectedIDs.Contains((int)DB.ADSLSellChanellLimited.Person))
            //{
            //    SaleChanell.Add(1);
            //    SaleChanell.Add(2);
            //}

            //if (SaleChannelComboBox.SelectedIDs.Contains((int)(DB.ADSLSellChanellLimited.Internet)))
            //{
            //    SaleChanell.Add(3);
            //}

            //if (SaleChannelComboBox.SelectedIDs.Contains((int)(DB.ADSLSellChanellLimited.NonAttendance)))
            //{
            //    SaleChanell.Add(4);
            //}

            List<ADSLChangeServiceInfo> result = ReportDB.GetADSLChangeServiceInfo(SaleChanell,
                                                                                 fromDate.SelectedDate,
                                                                                 ToDate);
            return result;
        }

        #endregion Methods
    }
}
