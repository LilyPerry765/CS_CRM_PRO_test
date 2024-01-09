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
    /// Interaction logic for TranslationOpticalToNormalRequestUserControl.xaml
    /// </summary>
    public partial class TranslationOpticalCabinetToNormalRequestUserControl : ReportBase
    {

        #region Properties

        //TODO:rad باید کلیه جاهای که فیلتر تکتست باکس چند تا دارند از طریق روش این فرم پیاده سازی شوند
        /// <summary>
        /// .اگر این ویژگی درست باشد به معنای آن است که کاربر مقادیر عددی فیلتر ها را به درستی وارد کرده است در غیر این صورت مقدار ان نادرست است
        /// </summary>
        private bool FiltersValueIsNumeric { get; set; }

        #endregion

        #region Constructor

        public TranslationOpticalCabinetToNormalRequestUserControl()
        {
            InitializeComponent();
            this.FiltersValueIsNumeric = true;
        }

        #endregion

        #region Methods

        public override void Search()
        {

            try
            {
                if (this.FiltersValueIsNumeric) //در صورتی بلاک زیر اجرا میشود که برای تعیین شناسه درخواست و تلفن فقط از اعداد استفاده شده باشد
                {
                    DateTime? fromDate = (FromDatePicker.SelectedDate.HasValue) ? FromDatePicker.SelectedDate : null;
                    DateTime? toDate = (ToDatePicker.SelectedDate.HasValue) ? ToDatePicker.SelectedDate : null;

                    long? requestId = !string.IsNullOrEmpty(RequestIdTextBox.Text.Trim()) ? System.Convert.ToInt64(RequestIdTextBox.Text.Trim()) : default(long?);
                    long? telephoneNo = !string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? System.Convert.ToInt64(TelephoneNoTextBox.Text.Trim()) : default(long?);

                    var result = ReportDB.GetTranslationOpticalCabinetToNormalRequestReport(requestId, telephoneNo, CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs, fromDate, toDate);

                    if (result.Count != 0)
                    {
                        StiVariable vari = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                        ReportBase.SendToPrint(result, (int)DB.UserControlNames.TranslationOpticalCabinetToNormalRequestReport, vari);
                    }
                    else
                    {
                        MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(".برای تعیین مقادیر «تلفن» و «شناسه درخواست» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex, "خطا در لیست گزارش ها - گزارش درخواست کافو نوری به معمولی");
            }
        }

        #endregion

        #region EventHandlers

        private void FiltersTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;
            if (source != null)
            {
                //آیا یک مقدار عددی برای شناسه درخواست و تلفن مشخص شده است یا خیر
                long? inputValue = -1;
                bool inputValueIsValid = Helper.CheckDigitDataEntry(source, out inputValue);
                if (!inputValueIsValid)
                {
                    MessageBox.Show(".برای تعیین مقادیر «تلفن» و «شناسه درخواست» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
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
