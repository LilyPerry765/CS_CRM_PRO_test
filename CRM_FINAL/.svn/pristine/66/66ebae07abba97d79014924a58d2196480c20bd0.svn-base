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
using Stimulsoft.Report;
using Enterprise;
using Stimulsoft.Report.Dictionary;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PCMContactsPostStatisticReportUserControl.xaml
    /// </summary>
    public partial class PCMContactsPostStatisticReportUserControl : Local.ReportBase
    {
        #region Constructor

        public PCMContactsPostStatisticReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        //TODO:rad
        public override void Search()
        {
            try
            {
                var primaryResult = ReportDB.GetPCMPostInfo(CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, CabinetsComboBox.SelectedIDs, PostsComboBox.SelectedIDs);

                if (primaryResult.Count() > 0)
                {
                    //StiVariables
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));

                    //تنظیمات برای نمایش
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.PCMContactsPostStatistic, dateVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش گزارشات اطلاعات فنی پی سی ام - گزارش آمار پست های دارای پی سی ام");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //public override void Search()
        //{
        //    List<PCMPostInfo> result = LoadData();
        //    StiReport stiReport = new StiReport();
        //    stiReport.Dictionary.DataStore.Clear();
        //    stiReport.Dictionary.Databases.Clear();
        //    stiReport.Dictionary.RemoveUnusedData();
        //    string title = string.Empty;
        //    string path = ReportDB.GetReportPath(UserControlID);
        //    stiReport.Load(path);
        //    //title = "گزارش اطلاعات فنی مرکزی های کافو";
        //
        //
        //    stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
        //    stiReport.CacheAllData = true;
        //
        //
        //    //stiReport.Dictionary.Variables["Header"].Value = title;
        //    stiReport.RegData("result", "result", result);
        //

        //    ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
        //    reportViewerForm.ShowDialog();
        //}

        //private List<PCMPostInfo> LoadData()
        // {
        //List<PostContactInfo> result = ReportDB.GetPostContactInfo(CityCenterUC.CenterCheckableComboBox.SelectedIDs);
        //result = result.Where(t => t.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote).ToList();


        //List<PCMPostInfo> result = ReportDB.GetPCMPostInfo(CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs);

        //return result;
        // }

        private void Initialize()
        {
            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        #endregion

        #region EventHandlers

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        private void CentersComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CabinetsComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CentersComboBox.SelectedIDs);
        }

        private void CabinetsComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PostsComboBox.ItemsSource = PostDB.GetPostCheckableByCabinetID(CabinetsComboBox.SelectedIDs);
        }

        #endregion

    }
}
