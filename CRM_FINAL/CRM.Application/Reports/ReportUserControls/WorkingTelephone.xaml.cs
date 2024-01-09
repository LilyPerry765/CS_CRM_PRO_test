using CRM.Application.Reports.Viewer;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report;
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
    /// Interaction logic for WorkingTelephone.xaml
    /// </summary>
    public partial class WorkingTelephone : Local.ReportBase
    {
        #region Constructor
        public WorkingTelephone()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Initializer

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        #endregion  Initializer

        #region EventHandlers

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
            PreCodeComboBox.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        private void CabinetComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PostsComboBox.ItemsSource = PostDB.GetPostCheckableByCabinetID(CabinetComboBox.SelectedIDs);
            CabinetInputComboBox.ItemsSource = CabinetInputDB.GetCheckableCabinetInputByCabinetIDs(CabinetComboBox.SelectedIDs);
        }

        #endregion

        #region Methods

        public override void Search()
        {
            //TODO:rad
            try
            {
                var primaryResult = ReportDB.GetWorkingTelephon(CityComboBox.SelectedIDs,
                                                                CenterComboBox.SelectedIDs,
                                                                CabinetComboBox.SelectedIDs,
                                                                CabinetInputComboBox.SelectedIDs_l,
                                                                PreCodeComboBox.SelectedIDs,
                                                                PostsComboBox.SelectedIDs);

                if (primaryResult.Count() > 0)
                {

                    //StiVariables
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));

                    //تنظیمات برای نمایش
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.WorkingTelephone, dateVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - تلفن مشغول به کار براساس کافو");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private List<WorkingTelephoneReport> LoadData()
        //{

        //    List<WorkingTelephoneReport> result =
        //    ReportDB.GetWorkingTelephon(CityComboBox.SelectedIDs,
        //                                CenterComboBox.SelectedIDs,
        //                                CabinetComboBox.SelectedIDs,
        //                                PreCodeComboBox.SelectedIDs,
        //                                PostsComboBox.SelectedIDs);
        //    return result;
        //}

        #endregion

    }
}
