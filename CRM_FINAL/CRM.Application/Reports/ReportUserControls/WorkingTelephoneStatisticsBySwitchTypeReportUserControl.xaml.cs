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
    /// Interaction logic for WorkingTelephoneStatisticsBySwitchReportUserControl.xaml
    /// </summary>
    public partial class WorkingTelephoneStatisticsBySwitchTypeReportUserControl : ReportBase
    {
        #region Constructor

        public WorkingTelephoneStatisticsBySwitchTypeReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            SwitchTypesComboBox.ItemsSource = SwitchTypeDB.GetSwitchCheckable();
        }

        //TODO:rad
        public override void Search()
        {
            try
            {
                int cityId = (CitiesComboBox.SelectedValue != null) ? Convert.ToInt32(CitiesComboBox.SelectedValue) : -1;
                int centerId = (CentersComboBox.SelectedValue != null) ? Convert.ToInt32(CentersComboBox.SelectedValue) : -1;
                int switchTypeId = (SwitchTypesComboBox.SelectedValue != null) ? Convert.ToInt32(SwitchTypesComboBox.SelectedValue) : -1;

                //تعیین فیلتر های تاریخ اجباری است
                if (FromDatePicker.SelectedDate == null || ToDatePicker.SelectedDate == null)
                {
                    MessageBox.Show(".برای ایجاد گزارش حتماً باید مقادیر «از تاریخ» و «تا تاریخ» را تعیین نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = ReportDB.GetWorkingTelephoneStatisticsBySwitchType(cityId, centerId, switchTypeId, FromDatePicker.SelectedDate, ToDatePicker.SelectedDate);

                if (primaryResult.Count() > 0)
                {
                    //StiVariables 
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                    StiVariable cityVariable = new StiVariable("CityName", "CityName", DB.PersianCity);

                    //تنظیمات برای نمایش گزارش
                    ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.WorkingTelephoneStatisticsBySwitchTypeReport, true, dateVariable, cityVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش گزارش اطلاعات فنی - گزارش آمار تلفن های مشغول به کار بر اساس نوع سوئیچ");
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
