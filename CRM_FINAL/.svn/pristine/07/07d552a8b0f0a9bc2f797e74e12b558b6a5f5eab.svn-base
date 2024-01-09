using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for PostContactsReportUserControl.xaml
    /// </summary>
    public partial class PostContactsReportUserControl : ReportBase
    {
        #region Properties and Fields

        //TODO:rad must implement backgroundroker. 13931022
        //BackgroundWorker backWorker;

        //از این لیست به عنوان دیتای مورد نیاز برای ایجاد گزارش استفاده میکنیم
        //this property must used when BackgroundWorker in this form
        //private List<PostContactInfo> _result = new List<PostContactInfo>();
        //private List<PostContactInfo> Result
        //{
        //    get { return _result; }
        //    set { _result = value; }
        //}

        #endregion

        #region Constructor

        public PostContactsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            //backWorker = new BackgroundWorker();
            //backWorker.DoWork += backWorker_DoWork;
            //backWorker.RunWorkerCompleted += backWorker_RunWorkerCompleted;
        }

        //TODO:rad
        public override void Search()
        {
            try
            {
                if (CitiesComboBox.SelectedValue != null && CentersComboBox.SelectedValue != null)
                {
                    //انجام عملیات جستجو به طوری فرم لاک نشود
                    //backWorker.RunWorkerAsync();
                    long postContactID = (PostContactsComboBox.SelectedValue != null) ? Convert.ToInt64(PostContactsComboBox.SelectedValue) : -1;
                    int postID = (PostsComboBox.SelectedValue != null) ? Convert.ToInt32(PostsComboBox.SelectedValue) : -1;
                    int centerID = (CentersComboBox.SelectedValue != null) ? Convert.ToInt32(CentersComboBox.SelectedValue) : -1;
                    int cabinetID = (CabinetsComboBox.SelectedValue != null) ? Convert.ToInt32(CabinetsComboBox.SelectedValue) : -1;
                    int cityID = Convert.ToInt32(CitiesComboBox.SelectedValue);

                    //دیتای مورد نیاز برای ایجاد گزارش
                    var primaryResult = PostContactDB.GetPostContactsInfoReport(cityID, centerID, cabinetID, postID, postContactID);

                    //تنظیمات برای نمایش گزارش
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));

                    if (primaryResult.Count != 0)
                    {
                        //Helper.CollapseReportGeneratingInfo(this);
                        ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.PostContacts, dateVariable);
                    }
                    else
                    {
                        MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(".برای ایجاد گزارش حتماً باید شهر و مرکز را مشخص نمائید", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - گزارش اتصالی پست");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        #endregion

        #region EventHandlers

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CitiesComboBox.SelectedValue != null)
            {
                List<int> selectedCitiesId = new List<int>() { (int)CitiesComboBox.SelectedValue };
                CentersComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(selectedCitiesId);
            }
        }

        private void CentersComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CentersComboBox.SelectedValue != null)
            {
                List<int> selectedCentersId = new List<int>() { (int)CentersComboBox.SelectedValue };
                CabinetsComboBox.ItemsSource = CabinetDB.GetCabinetCheckableByCenterIDs(selectedCentersId);
            }
        }

        private void CabinetsComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CabinetsComboBox.SelectedValue != null)
            {
                List<int> selectedCabinetsId = new List<int>() { (int)CabinetsComboBox.SelectedValue };
                PostsComboBox.ItemsSource = PostDB.GetPostCheckableByCabinetID(selectedCabinetsId);
            }
        }

        private void PostsComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PostsComboBox.SelectedValue != null)
            {
                int selectedPostId = Convert.ToInt32(PostsComboBox.SelectedValue);
                PostContactsComboBox.ItemsSource = PostContactDB.GetPostContactCheckableByPostID(selectedPostId);
            }
        }

        //private void backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    //تنظیمات برای نمایش گزارش
        //    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));

        //    if (this.Result.Count != 0)
        //    {
        //        Helper.CollapseReportGeneratingInfo(this);
        //        ReportBase.SendToPrint(this.Result, (int)DB.UserControlNames.PostContacts, dateVariable);
        //    }
        //    else
        //    {
        //        MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    this.IsEnabled = true;
        //}

        //private void backWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    long postContactID = 0;
        //    int postID = 0;
        //    int centerID = 0;
        //    int cabinetID = 0;
        //    int cityID = 0;
        //    this.Dispatcher.Invoke(new Action(() =>
        //    {
        //        postContactID = (PostContactsComboBox.SelectedValue != null) ? Convert.ToInt64(PostContactsComboBox.SelectedValue) : -1;
        //        postID = (PostsComboBox.SelectedValue != null) ? Convert.ToInt32(PostsComboBox.SelectedValue) : -1;
        //        centerID = (CentersComboBox.SelectedValue != null) ? Convert.ToInt32(CentersComboBox.SelectedValue) : -1;
        //        cabinetID = (CabinetsComboBox.SelectedValue != null) ? Convert.ToInt32(CabinetsComboBox.SelectedValue) : -1;
        //        cityID = Convert.ToInt32(CitiesComboBox.SelectedValue);
        //        this.IsEnabled = false;
        //        Helper.ShowReportGeneratingInfo(this);

        //    }), System.Windows.Threading.DispatcherPriority.Normal);

        //    //دیتای مورد نیاز برای ایجاد گزارش
        //    this.Result = PostContactDB.GetPostContactsInfoReport(cityID, centerID, cabinetID, postID, postContactID);
        //}

        #endregion
    }
}
