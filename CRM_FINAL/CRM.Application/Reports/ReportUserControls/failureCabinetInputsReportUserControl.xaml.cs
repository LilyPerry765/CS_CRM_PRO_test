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
using Stimulsoft.Report;
using System.Collections;
using System.ComponentModel;
using CRM.Data;
using CRM.Application.Reports.Viewer;
using Stimulsoft.Report.Dictionary;
using Enterprise;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for failureCabinetInputsReportUserControl.xaml
    /// </summary>
    public partial class failureCabinetInputsReportUserControl : Local.ReportBase
    {
        #region Constructor
        public failureCabinetInputsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CityCenterUC.CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            }
        }

        //TODO:rad
        public override void Search()
        {
            try
            {
                //آیا یک مقدار عددی برای شماره کافو تعیین شده است یا خیر
                long? cabinetNumber = -1;
                bool cabinetNumberIsValid = true;
                if (!string.IsNullOrEmpty(CabinetNumberTextBox.Text.Trim()))
                {
                    cabinetNumberIsValid = Helper.CheckDigitDataEntry(CabinetNumberTextBox, out cabinetNumber);
                }

                if (!cabinetNumberIsValid)
                {
                    MessageBox.Show(".برای تعیین شماره کافو فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CabinetNumberTextBox.Focus();
                    CabinetNumberTextBox.SelectAll();
                    return;
                }

                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = CabinetInputDB.GetFailureCabinetInputStatistic(CityCenterUC.CenterCheckableComboBox.SelectedIDs, FromDate.SelectedDate, ToDate.SelectedDate, (int)cabinetNumber);

                if (primaryResult.Count != 0)
                {
                    List<EnumItem> PCMMalfuctionType = Helper.GetEnumItem(typeof(DB.PCMMalfuctionType));

                    foreach (CabinetInputMalfunction item in primaryResult)
                    {
                        item.PersianCorrect_Date = Helper.GetPersianDate(item.Correct_Date, Helper.DateStringType.Short);
                        item.PersianFailure_Date = Helper.GetPersianDate(item.Failure_Date, Helper.DateStringType.Short);
                        item.TypeMalfactionName = (item.TypeMalfaction != null) ? (PCMMalfuctionType.Find(i => i.ID == item.TypeMalfaction) != null) ? PCMMalfuctionType.Find(i => i.ID == item.TypeMalfaction).Name : "" : "";
                    }

                    string centersName = string.Empty;
                    string regionsName = string.Empty;

                    List<Center> centerList = CenterDB.GetAllCenter();
                    List<int> centersId = CityCenterUC.CenterCheckableComboBox.SelectedIDs;
                    List<int> citiesId = CityCenterUC.CityComboBox.SelectedIDs;

                    if (centersId.Count != 0)
                    {
                        foreach (int _centerIds in centersId)
                        {
                            centersName += centerList.Find(item => item.ID == _centerIds).CenterName + " ,";
                        }
                        centersName = centersName.Substring(0, centersName.Length - 1);
                    }


                    List<City> cityList = CityDB.GetAllCity();
                    if (citiesId.Count != 0)
                    {
                        foreach (int _cityrIds in citiesId)
                        {
                            regionsName += cityList.Find(item => item.ID == _cityrIds).Name + " ,";
                        }
                        regionsName = regionsName.Substring(0, regionsName.Length - 1);
                    }

                    //Stivariables
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
                    StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));
                    StiVariable centersNameVariable = new StiVariable("CentersName", "CentersName", centersName);
                    StiVariable regionsNameVariable = new StiVariable("RegionsName", "RegionsName", regionsName);
                    StiVariable fromDateVariable = new StiVariable("FromDate", "FromDate", (FromDate.SelectedDate.HasValue) ? Helper.GetPersianDate(FromDate.SelectedDate.Value, Helper.DateStringType.Short) : "-----");
                    StiVariable toDateVariable = new StiVariable("ToDate", "ToDate", (ToDate.SelectedDate.HasValue) ? Helper.GetPersianDate(ToDate.SelectedDate.Value, Helper.DateStringType.Short) : "-----");

                    //نمایش گزارش
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.failureCabinetInputReport, dateVariable, timeVariable, centersNameVariable, regionsNameVariable, fromDateVariable, toDateVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex, "خطا در لیست گزارش ها - گزارش کلی خرابی مرکزی ها");
            }
        }

        #endregion
    }
}
