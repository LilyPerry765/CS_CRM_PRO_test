using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections;
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
    public partial class CertificateTranslationOpticalCabinetToNormalUserControl : ReportBase
    {
        #region Constructor

        public CertificateTranslationOpticalCabinetToNormalUserControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public override void Search()
        {
           
            try
            {
                DateTime? fromDate = (FromDatePicker.SelectedDate.HasValue) ? FromDatePicker.SelectedDate : null;
                DateTime? toDate = (ToDatePicker.SelectedDate.HasValue) ? ToDatePicker.SelectedDate : null;

                //آیا یک مقدار عددی برای شناسه درخواست مشخص شده است یا خیر
                long? requestId = null;
                bool requestIdIsValid = Helper.CheckDigitDataEntry(RequestIdTextBox, out requestId);

                if (!requestIdIsValid)
                {
                    MessageBox.Show(".برای تعیین شناسه درخواست فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    RequestIdTextBox.Focus();
                    return;
                }

                IEnumerable result 
                    = ReportDB.GetChangeLocationSpecialwireCertificateInfo(fromDate,toDate,CityCenterComboBox.CityComboBox.SelectedIDs,CityCenterComboBox.CenterComboBox.SelectedIDs,new List<long?>{requestId});
                               
                if (result != null)
                {
                    StiVariable vari = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                    ReportBase.SendToPrint(result, (int)DB.UserControlNames.ChangeLocationSpecialWireCertificate, vari);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex,"خطا در لیست گزارش ها - گزارش درخواست کافو نوری به معمولی");
            }
        }

        #endregion
    }
}
