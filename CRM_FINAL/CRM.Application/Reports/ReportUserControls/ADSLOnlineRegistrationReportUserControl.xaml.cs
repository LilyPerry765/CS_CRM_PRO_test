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
    /// Interaction logic for ADSLOnlineRegistrationReportUserControl.xaml
    /// </summary>
    public partial class ADSLOnlineRegistrationReportUserControl : Local.ReportBase
    {
         #region properties
        #endregion

        #region Constructor

        public ADSLOnlineRegistrationReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Consructor

        #region Initializer

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            CityComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ItemsComboBox_DropDownClosed);
            PaymentTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPaymentType));
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceType));
            GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            GroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupItemsComboBox_DropDownClosed);
        }

        void ItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (CityComboBox.SelectedIDs.Count > 0)
                AllChecked = true;

            CenterComboBox.ItemsSource = Data.CenterDB.GetCentersCheckable(AllChecked, CityComboBox.SelectedIDs);
        }


        void GroupItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (GroupComboBox.SelectedIDs.Count > 0)
                AllChecked = true;

            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupIDsAndTypeIds(GroupComboBox.SelectedIDs, PaymentTypeCombBox.SelectedIDs);
            BandWidthComboBox.ItemsComboBox.DropDownClosed += new EventHandler(BandWidthItemsComboBox_DropDownClosed);
        }
        void BandWidthItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (BandWidthComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationByBandwidtIDs(BandWidthComboBox.SelectedIDs);
            DurationComboBox.ItemsComboBox.DropDownClosed += new EventHandler(DurationItemsComboBox_DropDownClosed);
        }

        void DurationItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (DurationComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckablebyDurationIDs(DurationComboBox.SelectedIDs);
            TrafficComboBox.ItemsComboBox.DropDownClosed += new EventHandler(TrafficItemsComboBox_DropDownClosed);
        }
        void TrafficItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (TrafficComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableByTrafficIDs(TrafficComboBox.SelectedIDs, GroupComboBox.SelectedIDs, DurationComboBox.SelectedIDs, BandWidthComboBox.SelectedIDs);
        }

        #endregion Intitializer

        #region Methods

        public override void Search()
        {
            List<ADSLRequestInfo> Result1 = LoadData();
            List<ADSLRequestInfo> Result2 = LoadData2();
            List<ADSLRequestInfo> Result3 = LoadData3();
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

            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            //foreach (ADSLRequestInfo info in Result)
            //{
            //    RequestPaymentInfo amountSum = ReportDB.GetAmountSum(info.ID);
            //    info.Cost = amountSum.AmountSum;
            //    info.AMountSUm = (info.Cost * info.NumberOfSaledADSLService).ToString();
            //}
            for (int i = 0; i < Result2.Count; i++)
            {
                for (int k = i + 1; k < Result2.Count; k++)
                {
                    if (Result2[i].CityName == Result2[k].CityName)
                    {
                        Result2[i].NumberOfSold = Result2[i].NumberOfSold + Result2[k].NumberOfSold;
                        Result2[i].Cost = Result2[i].Cost + Result2[k].Cost;
                        Result2.Remove(Result2[k]);
                        k--;
                    }
                }
            }

            for (int i = 0; i < Result3.Count; i++)
            {
                for (int k = i + 1; k < Result3.Count; k++)
                {
                    if (Result3[i].Center == Result3[k].Center)
                    {
                        Result3[i].NumberOfSaledADSLService = Result3[i].NumberOfSaledADSLService + Result3[k].NumberOfSaledADSLService;
                        Result3[i].Cost = Result3[i].Cost + Result3[k].Cost;
                        Result3.Remove(Result3[k]);
                        k--;
                    }
                }
            }
            foreach (ADSLRequestInfo info in Result1)
            {
                if (info.IsIBSNG == true)
                {
                    info.IsIBSNGString = "اعمال شده";
                }
                else
                    info.IsIBSNGString = "اعمال نشده";
            }

                title = "ليست ثبت نام های  اينترنتي ";
             stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result1);
            stiReport.RegData("Result1", "Result1", Result2);
            stiReport.RegData("Result2", "Result2", Result3);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLRequestInfo> LoadData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetADSLOnlineRegistrationInfo( FromDate.SelectedDate, toDate
                                                                                        , CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        PaymentTypeCombBox.SelectedIDs);

            return result;
            
        }

        private List<ADSLRequestInfo> LoadData2()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result1 = ReportDB.GetADSLOnlineRegistrationBaseOnCity( FromDate.SelectedDate, toDate
                                                                                        , CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        PaymentTypeCombBox.SelectedIDs);

            return result1;

        }

        private List<ADSLRequestInfo> LoadData3()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result2 = ReportDB.GetADSLOnlineRegistrationBaseOnCenter( FromDate.SelectedDate, toDate
                                                                                        , CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        PaymentTypeCombBox.SelectedIDs);

            return result2;

        }
        #endregion Methods
    }
}
