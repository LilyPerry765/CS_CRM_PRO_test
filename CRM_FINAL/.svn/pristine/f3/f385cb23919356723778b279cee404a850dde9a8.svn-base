using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report.Dictionary;
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

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for MalfuctionHistoryReportUserControl.xaml
    /// </summary>
    public partial class MalfuctionHistoryReportUserControl : ReportBase
    {
        #region Constructor
        public MalfuctionHistoryReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            MalfuctionTypesComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.MalfuctionType));
        }

        //TODO:rad
        public override void Search()
        {
            try
            {
                int malfuctionType = (MalfuctionTypesComboBox.SelectedValue != null) ? Convert.ToInt16(MalfuctionTypesComboBox.SelectedValue) : -1;
                int cityId = (CitiesComboBox.SelectedValue != null) ? Convert.ToInt16(CitiesComboBox.SelectedValue) : -1;
                int centerId = (CentersComboBox.SelectedValue != null) ? Convert.ToInt16(CentersComboBox.SelectedValue) : -1;

                if (malfuctionType != -1)
                {
                    //دیتای مورد نیاز برای ایجاد گزارش
                    var primaryResult = ReportDB.GetMalfuctionHistoryReport(malfuctionType, FromDatePicker.SelectedDate, ToDatePicker.SelectedDate, cityId, centerId);

                    if (!primaryResult.IsNullOrEmpty())
                    {
                        //StiVariables
                        StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                        StiVariable malfuctionTypeVariable = new StiVariable("MalfuctionType", "MalfuctionType", Helpers.GetEnumDescription(malfuctionType, typeof(DB.MalfuctionType)));

                        //تنظیمات برای نمایش گزارش
                        ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.MalfuctionHistoryReport, dateVariable, malfuctionTypeVariable);
                    }
                    else
                    {
                        MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(".برای ایجاد این گزارش تعیین نوع خرابی ضروری است", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    MalfuctionTypesComboBox.Focus();
                    MalfuctionTypesComboBox.IsDropDownOpen = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش گزارش اطلاعات فنی - گزارش تاریخچه خرابی ها");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region EventHandlers

        private void CitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CitiesComboBox.SelectedValue != null)
            {
                CentersComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(new List<int> { (int)CitiesComboBox.SelectedValue });
                CentersComboBox.SelectedIndex = 0;
                CentersComboBox.Items.Refresh();
            }
        }

        #endregion
    }
}
