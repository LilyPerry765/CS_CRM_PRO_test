﻿using CRM.Application.Local;
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
    /// Interaction logic for RefundDepositCertificateReportUserControl.xaml
    /// </summary>
    public partial class RefundDepositCertificateReportUserControl : ReportBase
    {
        #region Constructor

        public RefundDepositCertificateReportUserControl()
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
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            CauseOfRefundDepositComboBox.ItemsSource = RefundDepositDB.GetCauseOfRefundDepositCheckableItem();
        }

        //TODO:rad
        public override void Search()
        {
            try
            {
                //آیا یک مقدار عددی برای تلفن مشخص شده است یا خیر
                long? telephoneNo = -1;
                if (!string.IsNullOrEmpty(TelephoneNoTextBox.Text))
                {
                    bool telephoneNoIsValid = Helper.CheckDigitDataEntry(TelephoneNoTextBox, out telephoneNo);
                    if (!telephoneNoIsValid)
                    {
                        MessageBox.Show(".برای تعیین «شماره تلفن» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = ReportDB.GetRefundDepositInfos(FromDatePicker.SelectedDate, ToDatePicker.SelectedDate, CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CauseOfRefundDepositComboBox.SelectedIDs, telephoneNo.Value, -1);

                if (primaryResult.Count() > 0)
                {
                    //تنظیمات برای نمایش گزارش
                    ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.RefundDepositCertificateReport);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش گواهی - گواهی استرداد ودیعه تلفن ثابت");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}