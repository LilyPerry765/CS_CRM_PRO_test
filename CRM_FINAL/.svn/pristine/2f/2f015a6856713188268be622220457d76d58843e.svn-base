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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Enterprise;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PostContactsReportUserControl.xaml
    /// </summary>
    public partial class PostContactsStatisticsReportUserControl : Local.ReportBase
    {
        #region Properties and Fields

        List<int> centerInfoList = new List<int>();

        #endregion

        #region Constructor
        public PostContactsStatisticsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            CityComboBox.SelectedIndex = 0;
        }

        //TODO:rad
        public override void Search()
        {
            try
            {
                int postId = (PostComboBox.SelectedIndex >= 0) ? (int)PostComboBox.SelectedValue : -1;
                List<PostContactsReport> result = new List<PostContactsReport>();
                switch (this.UserControlID)
                {
                    case (int)DB.UserControlNames.PostContactTotal:
                        result = ReportDB.GetPostContactTotal((int)CenterComboBox.SelectedValue, (int)CabinetComboBox.SelectedValue, postId, null);
                        break;
                    case (int)DB.UserControlNames.PostContactReserve:
                        result = ReportDB.GetPostContactTotal((int)CenterComboBox.SelectedValue, (int)CabinetComboBox.SelectedValue, postId, (int)DB.PostContactStatus.FullBooking);
                        break;
                    case (int)DB.UserControlNames.PostContactFill:
                        result = ReportDB.GetPostContactTotal((int)CenterComboBox.SelectedValue, (int)CabinetComboBox.SelectedValue, postId, (int)DB.PostContactStatus.CableConnection);
                        break;
                    case (int)DB.UserControlNames.PostContactFail:
                        result = ReportDB.GetPostContactTotal((int)CenterComboBox.SelectedValue, (int)CabinetComboBox.SelectedValue, postId, (int)DB.PostContactStatus.PermanentBroken);
                        break;
                    case (int)DB.UserControlNames.PostContactEmpty:
                        result = ReportDB.GetPostContactTotal((int)CenterComboBox.SelectedValue, (int)CabinetComboBox.SelectedValue, postId, (int)DB.PostContactStatus.Free);
                        break;
                }

                if (result.Count() > 0)
                {
                    foreach (PostContactsReport item in result)
                    {
                        item.TelephoneDatePersian = (item.TelephoneDate.HasValue) ? item.TelephoneDate.Value.ToPersian(Date.DateStringType.Short) : "-----";
                    }

                    //StiVariables
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
                    StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));

                    CRM.Application.Local.ReportBase.SendToPrint(result, this.UserControlID, dateVariable, timeVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - آمار اتصالی ");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //before rad
        //private List<PostContactsReport> LoadData(ref List<PCMDetails> pcmDetails)
        //{
        //    pcmDetails.Clear();
        //    List<PostContact> Temp = new List<PostContact>();
        //    List<EnumItem> Status = Helper.GetEnumItem(typeof(DB.PostContactStatus));
        //    List<PostContactsReport> result = new List<PostContactsReport>();
        //    int postId = (PostComboBox.SelectedIndex >= 0) ? (int)PostComboBox.SelectedValue : -1;

        //    switch (UserControlID)
        //    {
        //        case (int)DB.UserControlNames.PostContactTotal:
        //            result = ReportDB.GetPostContactTotal((int)CenterComboBox.SelectedValue, (int)CabinetComboBox.SelectedValue, postId, null);
        //            break;
        //        case (int)DB.UserControlNames.PostContactReserve:
        //            result = ReportDB.GetPostContactTotal((int)CenterComboBox.SelectedValue, (int)CabinetComboBox.SelectedValue, (int)PostComboBox.SelectedValue, (int)DB.PostContactStatus.FullBooking);
        //            break;
        //        case (int)DB.UserControlNames.PostContactFill:
        //            result = ReportDB.GetPostContactTotal((int)CenterComboBox.SelectedValue, (int)CabinetComboBox.SelectedValue, (int)PostComboBox.SelectedValue, (int)DB.PostContactStatus.CableConnection);
        //            break;
        //        case (int)DB.UserControlNames.PostContactFail:
        //            result = ReportDB.GetPostContactTotal((int)CenterComboBox.SelectedValue, (int)CabinetComboBox.SelectedValue, (int)PostComboBox.SelectedValue, (int)DB.PostContactStatus.PermanentBroken);
        //            break;
        //        case (int)DB.UserControlNames.PostContactEmpty:
        //            result = ReportDB.GetPostContactTotal((int)CenterComboBox.SelectedValue, (int)CabinetComboBox.SelectedValue, (int)PostComboBox.SelectedValue, (int)DB.PostContactStatus.Free);
        //            break;
        //    }
        //    if (result.Count() > 0)
        //    {
        //        foreach (PostContactsReport item in result)
        //        {
        //            item.TelephoneDatePersian = !item.TelephoneDate.HasValue ? string.Empty : Helper.GetPersianDate(item.TelephoneDate, Helper.DateStringType.Short);
        //        }
        //    }

        //    return result;
        //}

        //before rad
        //public override void Search()
        //{
        //    List<PCMDetails> pcmDetails = new List<PCMDetails>();
        //    List<PostContactsReport> result = LoadData(ref pcmDetails);
        //    StiReport stiReport = new StiReport();
        //    stiReport.Dictionary.DataStore.Clear();
        //    stiReport.Dictionary.Databases.Clear();
        //    stiReport.Dictionary.RemoveUnusedData();
        //    string title = string.Empty;
        //    string path = ReportDB.GetReportPath(UserControlID);
        //    stiReport.Load(path);



        //    //StiVariables
        //    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
        //    StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));



        //    stiReport.RegData("result", "result", result);
        //    //stiReport.RegData("Details", "Details", pcmDetails);

        //    ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
        //    reportViewerForm.ShowDialog();
        //}

        #endregion

        #region EventHandlers

        private void CenterComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(new List<int> { (int)CenterComboBox.SelectedValue });
                CabinetComboBox.SelectedIndex = 0;
            }
        }

        private void CabinetComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (CabinetComboBox.SelectedValue != null)
            {
                PostComboBox.ItemsSource = PostDB.GetPostCheckableByCabinetID(new List<int> { (int)CabinetComboBox.SelectedValue });
                PostComboBox.SelectedIndex = -1;
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityComboBox.SelectedValue != null)
            {
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(new List<int> { (int)CityComboBox.SelectedValue });
                CenterComboBox.SelectedIndex = 0;
            }
        }

        #endregion EventHandlers

    }
}
