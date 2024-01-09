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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Enterprise;
using Stimulsoft.Report.Dictionary;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for CenterCabinetSubsetReportUserControl.xaml
    /// </summary>
    public partial class CenterCabinetSubsetReportUserControl : Local.ReportBase
    {
        #region Constructor

        public CenterCabinetSubsetReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion


        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            ReportSortingTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ReportSortingType));
        }

        //TODO:rad
        public override void Search()
        {
            try
            {
                //آیا یک مقدار عددی برای شماره کافو تعیین شده است یا خیر
                //long? cabinetNumber = -1;
                //bool cabinetNumberIsValid = true;
                //if (!string.IsNullOrEmpty(CabinetNumberTextBox.Text.Trim()))
                //{
                //    cabinetNumberIsValid = Helper.CheckDigitDataEntry(CabinetNumberTextBox, out cabinetNumber);
                //}
                //if (!cabinetNumberIsValid)
                //{
                //    MessageBox.Show(".برای تعیین شماره کافو فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    CabinetNumberTextBox.Focus();
                //    CabinetNumberTextBox.SelectAll();
                //    return;
                //}

                //آیا یک مقدار عددی برای تعداد اتصالی آزاد تعیین شده است یا خیر
                long? freePostContact = -1;
                bool freePostContactIsValid = true;
                if (!string.IsNullOrEmpty(FreePostContactTextBox.Text.Trim()))
                {
                    freePostContactIsValid = Helper.CheckDigitDataEntry(FreePostContactTextBox, out freePostContact);
                }
                if (!freePostContactIsValid)
                {
                    MessageBox.Show(".برای تعیین اتصالی آزاد فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    FreePostContactTextBox.Focus();
                    FreePostContactTextBox.SelectAll();
                    return;
                }

                int reportSortingType = (ReportSortingTypeComboBox.SelectedValue != null) ? Convert.ToInt32(ReportSortingTypeComboBox.SelectedValue) : -1;

                var primaryResult = ReportDB.GetCenterCabinet_Subset( 
                     CityComboBox.SelectedIDs,
                     CenterComboBox.SelectedIDs,
                      CabinetComboBox.SelectedIDs,
                     (int)freePostContact,
                     reportSortingType);

                if (primaryResult.Count != 0)
                {

                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                    StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.CenterCabinet_Subset, dateVariable, timeVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex, "خطا در لیست گزارش - گزارش کافو و پست  ");
            }

        }

        #endregion

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

    }
}
