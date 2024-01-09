using CRM.Data;
using Enterprise;
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
    /// Interaction logic for ChangeLocationCenterInsideCertificateReportUserControl.xaml
    /// </summary>
    public partial class ChangeLocationCenterInsideCertificateReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ChangeLocationCenterInsideCertificateReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        //TODO:rad
        public override void Search()
        {
            try
            {
                //آیا یک مقدار عددی برای شماره تلفن مشخص شده است یا خیر
                long? telephoneNo = -1;
                if (!string.IsNullOrEmpty(TelephoneNoTextBox.Text))
                {
                    bool isTelephoneNoValid = Helper.CheckDigitDataEntry(TelephoneNoTextBox, out telephoneNo);
                    if (!isTelephoneNoValid)
                    {
                        MessageBox.Show(".برای تعیین «شماره تلفن» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                        TelephoneNoTextBox.Focus();
                        return;
                    }
                }

                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = ReportDB.GetChangeLocationCenterInsideInfos(FromDatePicker.SelectedDate, ToDatePicker.SelectedDate, CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, false, (long)telephoneNo);

                if (primaryResult.Count() > 0)
                {
                    //تنظیمات برای نمایش گزارش
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.ChangeLocationCenterInsideCertificateReport);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش گواهی - گواهی تغییر مکان داخل مرکز");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region EventHandlers

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        #endregion
    }
}
