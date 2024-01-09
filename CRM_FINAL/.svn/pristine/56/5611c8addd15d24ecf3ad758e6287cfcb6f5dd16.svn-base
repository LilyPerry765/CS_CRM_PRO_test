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
    /// Interaction logic for FilledCabinetReportUserControl.xaml
    /// </summary>
    public partial class FilledCabinetReportUserControl : ReportBase
    {
        #region Constructor

        public FilledCabinetReportUserControl()
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
                ////آیا یک مقدار عددی برای «تعداد زوج خالی کمتر از» مشخص شده است یا خیر
                long? emptyCount = -1;
                if (!string.IsNullOrEmpty(EmptyCountTextBox.Text))
                {
                    bool emptyCountIsValid = Helper.CheckDigitDataEntry(EmptyCountTextBox, out emptyCount);
                    if (!emptyCountIsValid)
                    {
                        MessageBox.Show(".برای تعیین «تعداد زوج خالی کمتر از» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                        EmptyCountTextBox.Focus();
                        return;
                    }
                }

                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = ReportDB.GetFilledCabinet(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CabinetComboBox.SelectedIDs, (int)emptyCount);

                if (primaryResult.Count() > 0)
                {
                    //StiVariables
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
                    StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));
                    StiVariable cityVariable = new StiVariable("City", "City", DB.PersianCity);

                    //تنظیمات برای نمایش
                    ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.FilledCabinetReport, true, dateVariable, timeVariable, cityVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش کافوهای مرکز - گزارش لیست کافوهای پر شده");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region EventHandlers

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        #endregion

    }
}
