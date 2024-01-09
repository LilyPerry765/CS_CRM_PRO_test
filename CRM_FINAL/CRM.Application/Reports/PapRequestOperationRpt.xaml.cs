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
using System.Windows.Shapes;
using CRM.Data;
using Stimulsoft.Report;
using System.Data.Linq.SqlClient;

namespace CRM.Application.Reports
{
    /// <summary>
    /// Interaction logic for PapRequestOperationRpt.xaml
    /// </summary>
    public partial class PapRequestOperationRpt : Local.TabWindow
    {
        public PapRequestOperationRpt()
        {
            InitializeComponent();
            Initialize();
        }
        public void Initialize()
        {
            InstallDateTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPAPInstalTimeOut));

            List<Region> lstRegion = Data.RegionDB.GetRegions();
            RegionIdcomboBox.ItemsSource = lstRegion;
            RegionIdcomboBox.SelectedValuePath = "ID";
            RegionIdcomboBox.DisplayMemberPath = "Title";

     
            List<EnumItem> DS = Helper.GetEnumItem(typeof(DB.DayeriStatus));
            DayeriTypeComboBox.ItemsSource = DS;
            DayeriTypeComboBox.SelectedValuePath = "ID";
            DayeriTypeComboBox.DisplayMemberPath = "Name";

        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            List<ADSLPAPRequestInfo> result = LoadData();



            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();



            stiReport.Load(@"D:\\Project\\CRM.Application\\Reports\\PapRequestOperation_report.mrt");
            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["TelephoneNO"].Value = txtTelephoneNo.Text.Trim();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
           

            if (RegionIdcomboBox.Text != string.Empty)

                stiReport.Dictionary.Variables["Region"].Value = RegionIdcomboBox.Text;
            if (CenterIdComboBox.Text != string.Empty)
                stiReport.Dictionary.Variables["CenterName"].Value = CenterIdComboBox.Text;

           
            if (DayeriTypeComboBox.Text != string.Empty)
                stiReport.Dictionary.Variables["DayeriStatus"].Value = DayeriTypeComboBox.Text;

            

            string _Title = string.Empty;
            _Title = "گزارش عملکرد درخواستهای شرکتهای Pap ";
            stiReport.Dictionary.Variables["Header"].Value = _Title;


            stiReport.RegData("Result", "Result", result);
            stiReport.Show();
        }

        private List<ADSLPAPRequestInfo> LoadData()
        {
            List<ADSLPAPRequestInfo> result =
                     ReportDB.GetPapRequestsOperation(FromDate.SelectedDate,
                                                     ToDate.SelectedDate,
                                                     InstallDateTypeComboBox.SelectedIDs,
                                                     string.IsNullOrEmpty(txtRequestNO.Text.Trim()) ? (int?)null : int.Parse(txtRequestNO.Text),
                                                     string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString()));

            if (!string.IsNullOrEmpty(txtTelephoneNo.Text.Trim()))
            {
                result = result.Where
                    (t => (txtTelephoneNo.Text.Trim() == string.Empty
                        || t.TelephoneNo.ToString() == txtTelephoneNo.Text.Trim()))
                        .ToList();
            }
            List<EnumItem> lstRT = Helper.GetEnumItem(typeof(DB.RequestType));
            List<EnumItem> lstI = Helper.GetEnumItem(typeof(DB.ADSLPAPInstalTimeOut));
            List<EnumItem> lstCS = Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));

            foreach (ADSLPAPRequestInfo i in result)
            {
                i.EndDate = string.IsNullOrEmpty(i.EndDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.EndDate), Helper.DateStringType.DateTime);

                i.RequestDate = string.IsNullOrEmpty(i.RequestDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.RequestDate), Helper.DateStringType.DateTime);

                int Var = 0;
                switch (int.Parse(i.InstalTimeOut))
                {
                    case (int)DB.ADSLPAPInstalTimeOut.OneDay:
                        {
                            Var = 24;
                            i.DateDiff = Var - i.DateDiff;
                            break;
                        }
                    case (int)DB.ADSLPAPInstalTimeOut.ThreeDay:
                        {
                            Var = 72;
                            i.DateDiff = Var - i.DateDiff;
                            break;
                        }
                    case (int)DB.ADSLPAPInstalTimeOut.TwoDay:
                        {
                            Var = 48;
                            i.DateDiff = Var - i.DateDiff;
                            break;
                        }
                    case (int)DB.ADSLPAPInstalTimeOut.NoLimitation:
                        {
                            Var = 0;
                            break;
                        }
                }



                if (Var != 0)
                    if (i.DateDiff >= 0)
                    {
                        i.Color = (int)DB.Color.Green;
                        i.strDateDiff = "+" + i.DateDiff.ToString();
                    }
                    else
                    {
                        i.Color = (int)DB.Color.Red;
                        i.strDateDiff = i.DateDiff.ToString();
                    }
                else
                {
                    i.Color = (int)DB.Color.Black;
                    i.strDateDiff = i.DateDiff.ToString();
                }

                i.InstalTimeOut = lstI.Find(item => item.ID == byte.Parse(i.InstalTimeOut)).Name;
                i.RequestType = lstRT.Find(item => item.ID == byte.Parse(i.RequestType)).Name;



               
               

            }
            int? DayeriStatus = string.IsNullOrEmpty(DayeriTypeComboBox.Text) ? (int?)null : int.Parse(DayeriTypeComboBox.SelectedValue.ToString());
            
            switch (DayeriStatus)
            {
                case (int)DB.DayeriStatus.DayeriOnTime:
                    {
                        
                        result = result.Where(t => (t.DateDiff >= 0)).ToList();
                        break;
                    }
                case (int)DB.DayeriStatus.DayeriWithDelay:
                    {
                        result = result.Where(t => (t.DateDiff < 0)).ToList();
                        break;
                    }
                
            }
           
            return result;
            
        }

        private void RegionIdcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Center> lstCenter = Data.CenterDB.GetCenterByRegionId(int.Parse(RegionIdcomboBox.SelectedValue.ToString()));
            CenterIdComboBox.ItemsSource = lstCenter;
            CenterIdComboBox.SelectedValuePath = "ID";
            CenterIdComboBox.DisplayMemberPath = "CenterName";
        }
    }
}
