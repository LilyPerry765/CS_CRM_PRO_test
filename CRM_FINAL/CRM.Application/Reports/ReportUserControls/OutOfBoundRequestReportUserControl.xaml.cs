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
    /// Interaction logic for OutOfBoundRequestReportUserControl.xaml
    /// </summary>
    public partial class OutOfBoundRequestReportUserControl : ReportBase
    {
        #region Constructor
        public OutOfBoundRequestReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            RequestCompletionStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.RequestCompletionStatus));

            //به خاطر مرتب کردن بر اساس عنوان نوع درخواست لیست زیر را تعریف کردم
            List<EnumItem> requestTypes = Helper.GetEnumItem(typeof(DB.RequestType));
            RequestTypesComboBox.ItemsSource = requestTypes.OrderBy(l => l.Name).ToList();
        }

        public override void Search()
        {
            try
            {
                int requestTypeId = (RequestTypesComboBox.SelectedValue != null) ? Convert.ToInt32(RequestTypesComboBox.SelectedValue) : -1;
                int cityId = (CitiesComboBox.SelectedValue != null) ? Convert.ToInt32(CitiesComboBox.SelectedValue) : -1;
                int centerId = (CentersComboBox.SelectedValue != null) ? Convert.ToInt32(CentersComboBox.SelectedValue) : -1;
                int requestCompletionStatus = (RequestCompletionStatusComboBox.SelectedValue != null) ? Convert.ToInt32(RequestCompletionStatusComboBox.SelectedValue) : -1;

                //آیا یک مقدار عددی برای متراژ خارج از مرز بزرگتر  ، تعیین شده است یا خیر
                long? outBoundMetersGreaterThan = -1;
                bool outBoundMetersGreaterThanIsValid = true;
                if (!string.IsNullOrEmpty(OutOfBoundMeterTextBox.Text.Trim()))
                {
                    outBoundMetersGreaterThanIsValid = Helper.CheckDigitDataEntry(OutOfBoundMeterTextBox, out outBoundMetersGreaterThan);
                }
                if (!outBoundMetersGreaterThanIsValid)
                {
                    MessageBox.Show(".برای تعیین 'متراژ خارج از مرز بیشتر از' فقط از اعداد استفاده نمائید", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    OutOfBoundMeterTextBox.Focus();
                    OutOfBoundMeterTextBox.SelectAll();
                    return;
                }

                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = ReportDB.GetOutOfBoundRequest(requestTypeId, cityId, centerId, requestCompletionStatus, (int)outBoundMetersGreaterThan, FromDatePicker.SelectedDate, ToDatePicker.SelectedDate);

                if (primaryResult.Count() > 0)
                {
                    //StiVariables 
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                    StiVariable cityVariable = new StiVariable("CityName", "CityName", DB.PersianCity);

                    //تنظیمات برای نمایش گزارش
                    ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.OutOfBoundRequestReport, true, cityVariable, dateVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش گزارش اطلاعات فنی - گزارش درخواست های خارج از مرز");
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
