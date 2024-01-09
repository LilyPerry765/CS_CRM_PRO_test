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
    /// Interaction logic for GeneralRequestPaymentsDividedByCityCenterBaseCostReportUserControl.xaml
    /// </summary>
    public partial class GeneralRequestPaymentsDividedByCityCenterBaseCostReportUserControl : ReportBase
    {
        #region Constructor

        public GeneralRequestPaymentsDividedByCityCenterBaseCostReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region EventHandlers

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            BaseCostComboBox.ItemsSource = RequestPaymentDB.GetCostTypeCheckable();
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        public override void Search()
        {
            try
            {

                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = ReportDB.GetGeneralRequestPaymentInfo(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, FromDatePicker.SelectedDate, ToDatePicker.SelectedDate, BaseCostComboBox.SelectedIDs);
                if (primaryResult.Count() > 0)
                {
                    //StiVariables
                    StiVariable reportDateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
                    StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));
                    StiVariable fromDateVariable = new StiVariable();
                    fromDateVariable.Name = "FromDate";
                    fromDateVariable.Category = "FromDate";
                    fromDateVariable.ValueObject = (FromDatePicker.SelectedDate.HasValue) ? FromDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short) : "-----";
                    StiVariable toDateVariable = new StiVariable();
                    toDateVariable.Name = "ToDate";
                    toDateVariable.Category = "ToDate";
                    toDateVariable.ValueObject = (ToDatePicker.SelectedDate.HasValue) ? ToDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short) : "-----";
                    StiVariable cityVariable = new StiVariable("CityName", "CityName", DB.PersianCity);

                    //تنظیمات برای نمایش
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.GeneralRequestPaymentsDividedByCityCenterBaseCostReport, true, cityVariable, timeVariable, reportDateVariable, fromDateVariable, toDateVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش امور مشترکین - گزارش کلی هزینه ها");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}
