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
    /// Interaction logic for DischargePrintCertificationReportUserControl.xaml
    /// </summary>
    public partial class DischargeCertificateReportUserControl : ReportBase
    {
        #region Properties

        //TODO:rad باید کلیه جاهای که فیلتر تکتست باکس چند تا دارند از طریق روش این فرم پیاده سازی شوند
        /// <summary>
        /// .اگر این ویژگی درست باشد به معنای آن است که کاربر مقادیر عددی فیلتر ها را به درستی وارد کرده است در غیر این صورت مقدار ان نادرست است
        /// </summary>
        private bool FiltersValueIsNumeric { get; set; }

        #endregion

        #region Constructor

        public DischargeCertificateReportUserControl()
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
        }

        //TODO:rad
        public override void Search()
        {
            try
            {
                if (this.FiltersValueIsNumeric)
                {
                    //به علت جلوگیری از ایجاد گزارش کند ، تعیین بازه تاریخ اجباری است
                    if (!FromDatePicker.SelectedDate.HasValue || !ToDatePicker.SelectedDate.HasValue)
                    {
                        MessageBox.Show(".برای مشاهده گزارش حتماً باید مقادیر «از تاریخ» و «تا تاریخ» را تعیین نمائید", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    List<long?> requestNos = new List<long?>();
                    long? requestId = (!string.IsNullOrEmpty(RequestIdTextBox.Text.Trim())) ? System.Convert.ToInt64(RequestIdTextBox.Text.Trim()) : default(long?);
                    if (requestId.HasValue)
                    {
                        requestNos.Add(requestId);
                    }

                    long telephoneNo = (!string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim())) ? System.Convert.ToInt64(TelephoneNoTextBox.Text.Trim()) : 0;

                    //دیتای مورد نیاز برای ایجاد گزارش
                    var primaryResult = ReportDB.GetTakePossessionInfo(
                                                                        CityComboBox.SelectedIDs,
                                                                        CenterComboBox.SelectedIDs,
                                                                        FromDatePicker.SelectedDate,
                                                                        ToDatePicker.SelectedDate, telephoneNo, telephoneNo, new List<int>(), new List<int>(), new List<int>(), requestNos
                                                                       );

                    if (primaryResult.Count() > 0)
                    {
                        //تنظیمات برای نمایش گزارش
                        ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.DischargeCertificateReport);
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
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش گواهی - گزارش گواهی تخلیه");
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
