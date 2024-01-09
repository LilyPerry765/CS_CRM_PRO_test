using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PostInfoReserveReportUserControl.xaml
    /// </summary>
    public partial class PostInfoReserveReportUserControl : Local.ReportBase
    {
        
        #region Constructor
        public PostInfoReserveReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PostTypeComboBox.ItemsSource = Data.PostTypeDB.GetPostTypeCheckable();
        }

        public override void Search()
        {
            try
            {
                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = ReportDB.GetPostInfoReserve(UCUserControl.CenterCheckableComboBox.SelectedIDs,
                                                                CabinetComboBox.SelectedIDs,
                                                                PostsCheckableCombobox.SelectedIDs,
                                                                PostTypeComboBox.SelectedIDs);

                if (primaryResult.Count() > 0)
                {
                    //StiVariables
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
                    StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));

                    //تنظیمات برای نمایش گزارش 
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.PostInfoReserve, timeVariable, dateVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - گروه پست های مرکز - گزارش رزرو");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region EventHandlers

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as UserControls.CenterCityUserControl).CenterCheckableComboBox.ItemsComboBox.IsDropDownOpen)
            {
                CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(UCUserControl.CenterCheckableComboBox.SelectedIDs);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CabinetComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PostsCheckableCombobox.ItemsSource = PostDB.GetPostCheckableByCabinetID(CabinetComboBox.SelectedIDs);
        }

        #endregion

    }
}
