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
    /// Interaction logic for E1CertificateReportUserControl.xaml
    /// </summary>
    public partial class E1CertificateReportUserControl : ReportBase
    {
        #region Constructor
        public E1CertificateReportUserControl()
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
                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = ReportDB.SearchE1Certificate(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, NationalCodeOrRecordNoTextBox.Text.Trim(), FromDatePicker.SelectedDate, ToDatePicker.SelectedDate);

                if (primaryResult.Count() > 0)
                {
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", DB.GetServerDate().ToPersian(Date.DateStringType.Short));
                    //تنظیمات برای نمایش گزارش
                    ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.E1Certificate, dateVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش گواهی - گواهی ایوان");
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
