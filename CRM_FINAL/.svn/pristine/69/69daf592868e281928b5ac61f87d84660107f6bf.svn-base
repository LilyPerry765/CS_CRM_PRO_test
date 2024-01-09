using CRM.Application.Local;
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
    /// Interaction logic for ChangeNoCertificateReportUserControl.xaml
    /// </summary>
    public partial class ChangeNoCertificateReportUserControl : ReportBase
    {
        
        #region Properties

        //TODO:rad باید کلیه جاهای که فیلتر تکتست باکس چند تا دارند از طریق روش این فرم پیاده سازی شوند
        /// <summary>
        /// .اگر این ویژگی درست باشد به معنای آن است که کاربر مقادیر عددی فیلتر ها را به درستی وارد کرده است در غیر این صورت مقدار ان نادرست است
        /// </summary>
        private bool FiltersValueIsNumeric { get; set; }

        #endregion

        #region Constructor

        public ChangeNoCertificateReportUserControl()
        {
            InitializeComponent();
            Initialize();
            this.FiltersValueIsNumeric = true;
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            CauseOfChangeNoComboBox.ItemsSource = CauseOfChangeNoDB.GetCauseOfChangeNoCheckableItem();
        }

        //TODO:rad
        public override void Search()
        {
            try
            {
                if (FiltersValueIsNumeric)//در صورتی بلاک زیر اجرا شود که یک مقدار عددی برای شماره تلفن های جدید و قدیم مشخص شده باشد
                {
                    long oldTelephoneNo = !string.IsNullOrEmpty(OldTelephoneNoTextBox.Text) ? Convert.ToInt64(OldTelephoneNoTextBox.Text) : -1;
                    long newTelephoneNo = !string.IsNullOrEmpty(NewTelephoneNoTextBox.Text) ? Convert.ToInt64(NewTelephoneNoTextBox.Text) : -1;

                    //دیتای مورد نیاز برای ایجاد گزارش
                    var primaryResult = ReportDB.GetChangeNoInfos(FromDatePicker.SelectedDate, ToDatePicker.SelectedDate, CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CauseOfChangeNoComboBox.SelectedIDs, oldTelephoneNo, newTelephoneNo);

                    if (primaryResult.Count() > 0)
                    {
                        //تنظیمات برای نمایش گزارش
                        ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.ChangeNoCertificateReport);
                    }
                    else
                    {
                        MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(".برای تعیین مقادیر «تلفن قدیم» و «تلفن جدید» فقط از اعداد استفاده نمائید", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش گواهی - گواهی تعویض شماره");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region EventHandlers

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void FiltersTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;
            if (source != null)
            {
                //آیا یک مقدار عددی برای شماره تلفن های جدید و قدیم مشخص شده است یا خیر
                long? enteredTelephoneNo = -1;
                bool enteredTelephoneNoIsValid = Helper.CheckDigitDataEntry(source, out enteredTelephoneNo);
                if (!enteredTelephoneNoIsValid)
                {
                    MessageBox.Show(".برای تعیین مقادیر «تلفن قدیم» و «تلفن جدید» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.FiltersValueIsNumeric = false;
                    source.Focus();
                }
                else
                {
                    this.FiltersValueIsNumeric = true;
                }
            }
        }

        #endregion

    }
}
