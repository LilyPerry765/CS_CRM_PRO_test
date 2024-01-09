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
using CRM.Data;
using System.Reflection;
using CRM.Application.Reports.Viewer;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSCityCenterDailyReportUserControl.xaml
    /// </summary>
    public partial class ADSCityCenterDailyReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ADSCityCenterDailyReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor 

        #region Initializer

        private void Initialize()
        {
            PaymentTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            //if (PaymentTypeComboBox.SelectedIndex < 0)
            //{
            //    MessageBox.Show("لطفا نحوه پرداخت را انتخاب کنید");
            //}
           // else
           //{
                List<ADSLCityCenterBandwidthDailyInfo> Result = LoadData();
                //List<ADSLCityCenterBandwidthDailyInfo> ResultADSLChangeService = LoadADSLChangeServiceData();
                List<ADSLCityCenterBandwidthDailyInfo> ResultDischarge = LoadDataDischarge();

                #region ADSLChangeService
                //for (int m = 0; m < ResultADSLChangeService.Count(); m++)
                //{

                //    for (int j = m + 1; j < ResultADSLChangeService.Count(); j++)
                //    {
                //        if (ResultADSLChangeService[m].CityName == ResultADSLChangeService[j].CityName && ResultADSLChangeService[m].CenterName == ResultADSLChangeService[j].CenterName)
                //        {
                //            if (ResultADSLChangeService[j]._64Friday != null && ResultADSLChangeService[m]._64Friday == null)
                //                ResultADSLChangeService[m]._64Friday = ResultADSLChangeService[j]._64Friday;

                //            else if (ResultADSLChangeService[j]._64Friday != null && ResultADSLChangeService[m]._64Friday != null)
                //                ResultADSLChangeService[m]._64Friday = ResultADSLChangeService[j]._64Friday + ResultADSLChangeService[m]._64Friday;

                //            if (ResultADSLChangeService[j]._64Monday != null && ResultADSLChangeService[m]._64Monday == null)
                //                ResultADSLChangeService[m]._64Monday = ResultADSLChangeService[j]._64Monday;

                //            else if (ResultADSLChangeService[j]._64Monday != null && ResultADSLChangeService[m]._64Monday != null)
                //                ResultADSLChangeService[m]._64Monday = ResultADSLChangeService[j]._64Monday + ResultADSLChangeService[m]._64Monday;

                //            if (ResultADSLChangeService[j]._64Saturday != null && ResultADSLChangeService[m]._64Saturday == null)
                //                ResultADSLChangeService[m]._64Saturday = ResultADSLChangeService[j]._64Saturday;

                //            else if (ResultADSLChangeService[j]._64Saturday != null && ResultADSLChangeService[m]._64Saturday != null)
                //                ResultADSLChangeService[m]._64Saturday = ResultADSLChangeService[j]._64Saturday + ResultADSLChangeService[m]._64Saturday;

                //            if (ResultADSLChangeService[j]._64Sunday != null && ResultADSLChangeService[m]._64Sunday == null)
                //                ResultADSLChangeService[m]._64Sunday = ResultADSLChangeService[j]._64Sunday;

                //            else if (ResultADSLChangeService[j]._64Sunday != null && ResultADSLChangeService[m]._64Sunday != null)
                //                ResultADSLChangeService[m]._64Sunday = ResultADSLChangeService[j]._64Sunday + ResultADSLChangeService[m]._64Sunday;

                //            if (ResultADSLChangeService[j]._64Thursday != null && ResultADSLChangeService[m]._64Thursday == null)
                //                ResultADSLChangeService[m]._64Thursday = ResultADSLChangeService[j]._64Thursday;

                //            else if (ResultADSLChangeService[j]._64Thursday != null && ResultADSLChangeService[m]._64Thursday != null)
                //                ResultADSLChangeService[m]._64Thursday = ResultADSLChangeService[j]._64Thursday + ResultADSLChangeService[m]._64Thursday;

                //            if (ResultADSLChangeService[j]._64Tuesday != null && ResultADSLChangeService[m]._64Tuesday == null)
                //                ResultADSLChangeService[m]._64Tuesday = ResultADSLChangeService[j]._64Tuesday;

                //            else if (ResultADSLChangeService[j]._64Tuesday != null && ResultADSLChangeService[m]._64Tuesday != null)
                //                ResultADSLChangeService[m]._64Tuesday = ResultADSLChangeService[j]._64Tuesday + ResultADSLChangeService[m]._64Tuesday;

                //            if (ResultADSLChangeService[j]._64Wednesday != null && ResultADSLChangeService[m]._64Wednesday == null)
                //                ResultADSLChangeService[m]._64Wednesday = ResultADSLChangeService[j]._64Wednesday;

                //            else if (ResultADSLChangeService[j]._64Wednesday != null && ResultADSLChangeService[m]._64Wednesday != null)
                //                ResultADSLChangeService[m]._64Wednesday = ResultADSLChangeService[j]._64Wednesday + ResultADSLChangeService[m]._64Wednesday;

                //            if (ResultADSLChangeService[j]._1024Friday != null && ResultADSLChangeService[m]._1024Friday == null)
                //                ResultADSLChangeService[m]._1024Friday = ResultADSLChangeService[j]._1024Friday;

                //            else if (ResultADSLChangeService[j]._1024Friday != null && ResultADSLChangeService[m]._1024Friday != null)
                //                ResultADSLChangeService[m]._1024Friday = (ResultADSLChangeService[j]._1024Friday) + ResultADSLChangeService[m]._1024Friday;

                //            if (ResultADSLChangeService[j]._1024Monday != null && ResultADSLChangeService[m]._1024Monday == null)
                //                ResultADSLChangeService[m]._1024Monday = ResultADSLChangeService[j]._1024Monday;

                //            else if (ResultADSLChangeService[j]._1024Monday != null && ResultADSLChangeService[m]._1024Monday != null)
                //                ResultADSLChangeService[m]._1024Monday = ResultADSLChangeService[j]._1024Monday + ResultADSLChangeService[m]._1024Monday;

                //            if (ResultADSLChangeService[j]._1024Saturday != null && ResultADSLChangeService[m]._1024Saturday == null)
                //                ResultADSLChangeService[m]._1024Saturday = ResultADSLChangeService[j]._1024Saturday;

                //            else if (ResultADSLChangeService[j]._1024Saturday != null && ResultADSLChangeService[m]._1024Saturday != null)
                //                ResultADSLChangeService[m]._1024Saturday = ResultADSLChangeService[j]._1024Saturday + ResultADSLChangeService[m]._1024Saturday;

                //            if (ResultADSLChangeService[j]._1024Sunday != null && ResultADSLChangeService[m]._1024Sunday == null)
                //                ResultADSLChangeService[m]._1024Sunday = ResultADSLChangeService[j]._1024Sunday;

                //            else if (ResultADSLChangeService[j]._1024Sunday != null && ResultADSLChangeService[m]._1024Sunday != null)
                //                ResultADSLChangeService[m]._1024Sunday = ResultADSLChangeService[j]._1024Sunday + ResultADSLChangeService[m]._1024Sunday;

                //            if (ResultADSLChangeService[j]._1024Thursday != null && ResultADSLChangeService[m]._1024Thursday == null)
                //                ResultADSLChangeService[m]._1024Thursday = ResultADSLChangeService[j]._1024Thursday;

                //            else if (ResultADSLChangeService[j]._1024Thursday != null && ResultADSLChangeService[m]._1024Thursday != null)
                //                ResultADSLChangeService[m]._1024Thursday = ResultADSLChangeService[j]._1024Thursday + ResultADSLChangeService[m]._1024Thursday;

                //            if (ResultADSLChangeService[j]._1024Tuesday != null && ResultADSLChangeService[m]._1024Tuesday == null)
                //                ResultADSLChangeService[m]._1024Tuesday = ResultADSLChangeService[j]._1024Tuesday;

                //            else if (ResultADSLChangeService[j]._1024Tuesday != null && ResultADSLChangeService[m]._1024Tuesday != null)
                //                ResultADSLChangeService[m]._1024Tuesday = ResultADSLChangeService[j]._1024Tuesday + ResultADSLChangeService[m]._1024Tuesday;

                //            if (ResultADSLChangeService[j]._1024Wednesday != null && ResultADSLChangeService[m]._1024Wednesday == null)
                //                ResultADSLChangeService[m]._1024Wednesday = ResultADSLChangeService[j]._1024Wednesday;

                //            else if (ResultADSLChangeService[j]._1024Wednesday != null && ResultADSLChangeService[m]._1024Wednesday != null)
                //                ResultADSLChangeService[m]._1024Wednesday = ResultADSLChangeService[j]._1024Wednesday + ResultADSLChangeService[m]._1024Wednesday;

                //            if (ResultADSLChangeService[j]._128Friday != null && ResultADSLChangeService[m]._128Friday == null)
                //                ResultADSLChangeService[m]._128Friday = ResultADSLChangeService[j]._128Friday;

                //            else if (ResultADSLChangeService[j]._128Friday != null && ResultADSLChangeService[m]._128Friday != null)
                //                ResultADSLChangeService[m]._128Friday = ResultADSLChangeService[j]._128Friday + ResultADSLChangeService[m]._128Friday;

                //            if (ResultADSLChangeService[j]._128Monday != null && ResultADSLChangeService[m]._128Monday == null)
                //                ResultADSLChangeService[m]._128Monday = ResultADSLChangeService[j]._128Monday;

                //            else if (ResultADSLChangeService[j]._128Monday != null && ResultADSLChangeService[m]._128Monday != null)
                //                ResultADSLChangeService[m]._128Monday = ResultADSLChangeService[j]._128Monday + ResultADSLChangeService[m]._128Monday;

                //            if (ResultADSLChangeService[j]._128Saturday != null && ResultADSLChangeService[m]._128Saturday == null)
                //                ResultADSLChangeService[m]._128Saturday = ResultADSLChangeService[j]._128Saturday;

                //            else if (ResultADSLChangeService[j]._128Saturday != null && ResultADSLChangeService[m]._128Saturday != null)
                //                ResultADSLChangeService[m]._128Saturday = ResultADSLChangeService[j]._128Saturday + ResultADSLChangeService[m]._128Saturday;

                //            if (ResultADSLChangeService[j]._128Sunday != null && ResultADSLChangeService[m]._128Sunday == null)
                //                ResultADSLChangeService[m]._128Sunday = ResultADSLChangeService[j]._128Sunday;

                //            else if (ResultADSLChangeService[j]._128Sunday != null && ResultADSLChangeService[m]._128Sunday != null)
                //                ResultADSLChangeService[m]._128Sunday = ResultADSLChangeService[j]._128Sunday + Result[m]._128Sunday;

                //            if (ResultADSLChangeService[j]._128Thursday != null && ResultADSLChangeService[m]._128Thursday == null)
                //                ResultADSLChangeService[m]._128Thursday = ResultADSLChangeService[j]._128Thursday;

                //            else if (ResultADSLChangeService[j]._128Thursday != null && ResultADSLChangeService[m]._128Thursday != null)
                //                ResultADSLChangeService[m]._128Thursday = ResultADSLChangeService[j]._128Thursday + ResultADSLChangeService[m]._128Thursday;

                //            if (ResultADSLChangeService[j]._128Tuesday != null && ResultADSLChangeService[m]._128Tuesday == null)
                //                ResultADSLChangeService[m]._128Tuesday = ResultADSLChangeService[j]._128Tuesday;

                //            else if (ResultADSLChangeService[j]._128Tuesday != null && ResultADSLChangeService[m]._128Tuesday != null)
                //                ResultADSLChangeService[m]._128Tuesday = ResultADSLChangeService[j]._128Tuesday + ResultADSLChangeService[m]._128Tuesday;

                //            if (ResultADSLChangeService[j]._128Wednesday != null && ResultADSLChangeService[m]._128Wednesday == null)
                //                ResultADSLChangeService[m]._128Wednesday = ResultADSLChangeService[j]._128Wednesday;

                //            else if (ResultADSLChangeService[j]._128Wednesday != null && ResultADSLChangeService[m]._128Wednesday != null)
                //                ResultADSLChangeService[m]._128Wednesday = ResultADSLChangeService[j]._128Wednesday + ResultADSLChangeService[m]._128Wednesday;

                //            if (ResultADSLChangeService[j]._2048Friday != null && ResultADSLChangeService[m]._2048Friday == null)
                //                ResultADSLChangeService[m]._2048Friday = ResultADSLChangeService[j]._2048Friday;

                //            else if (ResultADSLChangeService[j]._2048Friday != null && ResultADSLChangeService[m]._2048Friday != null)
                //                ResultADSLChangeService[m]._2048Friday = ResultADSLChangeService[j]._2048Friday + ResultADSLChangeService[m]._2048Friday;

                //            if (ResultADSLChangeService[j]._2048Monday != null && ResultADSLChangeService[m]._2048Monday == null)
                //                ResultADSLChangeService[m]._2048Monday = ResultADSLChangeService[j]._2048Monday;

                //            else if (ResultADSLChangeService[j]._2048Monday != null && ResultADSLChangeService[m]._2048Monday != null)
                //                ResultADSLChangeService[m]._2048Monday = ResultADSLChangeService[j]._2048Monday + ResultADSLChangeService[m]._2048Monday;

                //            if (ResultADSLChangeService[j]._2048Saturday != null && ResultADSLChangeService[m]._2048Saturday == null)
                //                ResultADSLChangeService[m]._2048Saturday = ResultADSLChangeService[j]._2048Saturday;

                //            else if (ResultADSLChangeService[j]._2048Saturday != null && ResultADSLChangeService[m]._2048Saturday != null)
                //                ResultADSLChangeService[m]._2048Saturday = ResultADSLChangeService[j]._2048Saturday + ResultADSLChangeService[m]._2048Saturday;

                //            if (ResultADSLChangeService[j]._2048Sunday != null && ResultADSLChangeService[m]._2048Sunday == null)
                //                ResultADSLChangeService[m]._2048Sunday = ResultADSLChangeService[j]._2048Sunday;

                //            else if (ResultADSLChangeService[j]._2048Sunday != null && ResultADSLChangeService[m]._2048Sunday != null)
                //                ResultADSLChangeService[m]._2048Sunday = ResultADSLChangeService[j]._2048Sunday + ResultADSLChangeService[m]._2048Sunday;

                //            if (ResultADSLChangeService[j]._2048Thursday != null && ResultADSLChangeService[m]._2048Thursday == null)
                //                ResultADSLChangeService[m]._2048Thursday = ResultADSLChangeService[j]._2048Thursday;

                //            else if (ResultADSLChangeService[j]._2048Thursday != null && ResultADSLChangeService[m]._2048Thursday != null)
                //                ResultADSLChangeService[m]._2048Thursday = ResultADSLChangeService[j]._2048Thursday + ResultADSLChangeService[m]._2048Thursday;

                //            if (ResultADSLChangeService[j]._2048Tuesday != null && ResultADSLChangeService[m]._2048Tuesday == null)
                //                ResultADSLChangeService[m]._2048Tuesday = ResultADSLChangeService[j]._2048Tuesday;

                //            else if (ResultADSLChangeService[j]._2048Tuesday != null && ResultADSLChangeService[m]._2048Tuesday != null)
                //                ResultADSLChangeService[m]._2048Tuesday = ResultADSLChangeService[j]._2048Tuesday + ResultADSLChangeService[m]._2048Tuesday;

                //            if (ResultADSLChangeService[j]._2048Wednesday != null && ResultADSLChangeService[m]._2048Wednesday == null)
                //                ResultADSLChangeService[m]._2048Wednesday = ResultADSLChangeService[j]._2048Wednesday;

                //            else if (ResultADSLChangeService[j]._2048Wednesday != null && ResultADSLChangeService[m]._2048Wednesday != null)
                //                ResultADSLChangeService[m]._2048Wednesday = ResultADSLChangeService[j]._2048Wednesday + ResultADSLChangeService[m]._2048Wednesday;

                //            if (ResultADSLChangeService[j]._256Friday != null && ResultADSLChangeService[m]._256Friday == null)
                //                ResultADSLChangeService[m]._256Friday = ResultADSLChangeService[j]._256Friday;

                //            else if (ResultADSLChangeService[j]._256Friday != null && ResultADSLChangeService[m]._256Friday != null)
                //                ResultADSLChangeService[m]._256Friday = Result[j]._256Friday + ResultADSLChangeService[m]._256Friday;

                //            if (ResultADSLChangeService[j]._256Monday != null && ResultADSLChangeService[m]._256Monday == null)
                //                ResultADSLChangeService[m]._256Monday = ResultADSLChangeService[j]._256Monday;

                //            else if (ResultADSLChangeService[j]._256Monday != null && ResultADSLChangeService[m]._256Monday != null)
                //                ResultADSLChangeService[m]._256Monday = ResultADSLChangeService[j]._256Monday + ResultADSLChangeService[m]._256Monday;

                //            if (ResultADSLChangeService[j]._256Saturday != null && ResultADSLChangeService[m]._256Saturday == null)
                //                ResultADSLChangeService[m]._256Saturday = ResultADSLChangeService[j]._256Saturday;

                //            else if (ResultADSLChangeService[j]._256Saturday != null && ResultADSLChangeService[m]._256Saturday != null)
                //                ResultADSLChangeService[m]._256Saturday = ResultADSLChangeService[j]._256Saturday + ResultADSLChangeService[m]._256Saturday;

                //            if (ResultADSLChangeService[j]._256Sunday != null && ResultADSLChangeService[m]._256Sunday == null)
                //                ResultADSLChangeService[m]._256Sunday = ResultADSLChangeService[j]._256Sunday;

                //            else if (ResultADSLChangeService[j]._256Sunday != null && ResultADSLChangeService[m]._256Sunday != null)
                //                ResultADSLChangeService[m]._256Sunday = ResultADSLChangeService[j]._256Sunday + ResultADSLChangeService[m]._256Sunday;

                //            if (ResultADSLChangeService[j]._256Thursday != null && ResultADSLChangeService[m]._256Thursday == null)
                //                ResultADSLChangeService[m]._256Thursday = ResultADSLChangeService[j]._256Thursday;

                //            else if (ResultADSLChangeService[j]._256Thursday != null && ResultADSLChangeService[m]._256Thursday != null)
                //                ResultADSLChangeService[m]._256Thursday = ResultADSLChangeService[j]._256Thursday + ResultADSLChangeService[m]._256Thursday;

                //            if (ResultADSLChangeService[j]._256Tuesday != null && ResultADSLChangeService[m]._256Tuesday == null)
                //                ResultADSLChangeService[m]._256Tuesday = ResultADSLChangeService[j]._256Tuesday;

                //            else if (ResultADSLChangeService[j]._256Tuesday != null && ResultADSLChangeService[m]._256Tuesday != null)
                //                ResultADSLChangeService[m]._256Tuesday = ResultADSLChangeService[j]._256Tuesday + ResultADSLChangeService[m]._256Tuesday;

                //            if (ResultADSLChangeService[j]._256Wednesday != null && ResultADSLChangeService[m]._256Wednesday == null)
                //                ResultADSLChangeService[m]._256Wednesday = ResultADSLChangeService[j]._256Wednesday;

                //            else if (ResultADSLChangeService[j]._256Wednesday != null && ResultADSLChangeService[m]._256Wednesday != null)
                //                ResultADSLChangeService[m]._256Wednesday = ResultADSLChangeService[j]._256Wednesday + ResultADSLChangeService[m]._256Wednesday;

                //            if (ResultADSLChangeService[j]._512Friday != null && ResultADSLChangeService[m]._512Friday == null)
                //                ResultADSLChangeService[m]._512Friday = ResultADSLChangeService[j]._512Friday;

                //            else if (ResultADSLChangeService[j]._512Friday != null && ResultADSLChangeService[m]._512Friday != null)
                //                ResultADSLChangeService[m]._512Friday = ResultADSLChangeService[j]._512Friday + ResultADSLChangeService[m]._512Friday;

                //            if (ResultADSLChangeService[j]._512Monday != null && ResultADSLChangeService[m]._512Monday == null)
                //                ResultADSLChangeService[m]._512Monday = ResultADSLChangeService[j]._512Monday;

                //            else if (ResultADSLChangeService[j]._512Monday != null && ResultADSLChangeService[m]._512Monday != null)
                //                ResultADSLChangeService[m]._512Monday = Result[j]._512Monday + ResultADSLChangeService[m]._512Monday;

                //            if (ResultADSLChangeService[j]._512Saturday != null && ResultADSLChangeService[m]._512Saturday == null)
                //                ResultADSLChangeService[m]._512Saturday = ResultADSLChangeService[j]._512Saturday;

                //            else if (ResultADSLChangeService[j]._512Saturday != null && ResultADSLChangeService[m]._512Saturday != null)
                //                ResultADSLChangeService[m]._512Saturday = ResultADSLChangeService[j]._512Saturday + ResultADSLChangeService[m]._512Saturday;

                //            if (ResultADSLChangeService[j]._512Sunday != null && ResultADSLChangeService[m]._512Sunday == null)
                //                ResultADSLChangeService[m]._512Sunday = ResultADSLChangeService[j]._512Sunday;

                //            else if (ResultADSLChangeService[j]._512Sunday != null && ResultADSLChangeService[m]._512Sunday != null)
                //                ResultADSLChangeService[m]._512Sunday = ResultADSLChangeService[j]._512Sunday + ResultADSLChangeService[m]._512Sunday;

                //            if (ResultADSLChangeService[j]._512Thursday != null && ResultADSLChangeService[m]._512Thursday == null)
                //                ResultADSLChangeService[m]._512Thursday = ResultADSLChangeService[j]._512Thursday;

                //            else if (ResultADSLChangeService[j]._512Thursday != null && ResultADSLChangeService[m]._512Thursday != null)
                //                ResultADSLChangeService[m]._512Thursday = ResultADSLChangeService[j]._512Thursday + ResultADSLChangeService[m]._512Thursday;

                //            if (ResultADSLChangeService[j]._512Tuesday != null && ResultADSLChangeService[m]._512Tuesday == null)
                //                ResultADSLChangeService[m]._512Tuesday = ResultADSLChangeService[j]._512Tuesday;

                //            else if (ResultADSLChangeService[j]._512Tuesday != null && ResultADSLChangeService[m]._512Tuesday != null)
                //                ResultADSLChangeService[m]._512Tuesday = ResultADSLChangeService[j]._512Tuesday + ResultADSLChangeService[m]._512Tuesday;

                //            if (ResultADSLChangeService[j]._512Wednesday != null && ResultADSLChangeService[m]._512Wednesday == null)
                //                ResultADSLChangeService[m]._512Wednesday = ResultADSLChangeService[j]._512Wednesday;

                //            else if (ResultADSLChangeService[j]._512Wednesday != null && ResultADSLChangeService[m]._512Wednesday != null)
                //                ResultADSLChangeService[m]._512Wednesday = ResultADSLChangeService[j]._512Wednesday + ResultADSLChangeService[m]._512Wednesday;

                //            ResultADSLChangeService.Remove(ResultADSLChangeService[j]);
                            
                //            j--;
                //        }
                //    }
                //}
                
                #endregion


                #region ForAdslRequest
                for (int m = 0; m < Result.Count(); m++)
                {

                    for (int j = m+1; j < Result.Count(); j++)
                    {
                        if (Result[m].CityName == Result[j].CityName && Result[m].CenterName == Result[j].CenterName)
                        {
                            if (Result[j]._64Friday != null && Result[m]._64Friday == null)
                                Result[m]._64Friday = Result[j]._64Friday;

                            else if (Result[j]._64Friday != null && Result[m]._64Friday != null)
                                Result[m]._64Friday = Result[j]._64Friday + Result[m]._64Friday;

                            if (Result[j]._64Monday != null && Result[m]._64Monday == null)
                                Result[m]._64Monday = Result[j]._64Monday;

                            else if (Result[j]._64Monday != null && Result[m]._64Monday != null)
                                Result[m]._64Monday = Result[j]._64Monday + Result[m]._64Monday;

                            if (Result[j]._64Saturday != null && Result[m]._64Saturday == null)
                                Result[m]._64Saturday = Result[j]._64Saturday;

                            else if (Result[j]._64Saturday != null && Result[m]._64Saturday != null)
                                Result[m]._64Saturday = Result[j]._64Saturday + Result[m]._64Saturday;

                            if (Result[j]._64Sunday != null && Result[m]._64Sunday == null)
                                Result[m]._64Sunday = Result[j]._64Sunday;

                            else if (Result[j]._64Sunday != null && Result[m]._64Sunday != null)
                                Result[m]._64Sunday = Result[j]._64Sunday + Result[m]._64Sunday;

                            if (Result[j]._64Thursday != null && Result[m]._64Thursday == null)
                                Result[m]._64Thursday = Result[j]._64Thursday;

                            else if (Result[j]._64Thursday != null && Result[m]._64Thursday != null)
                                Result[m]._64Thursday = Result[j]._64Thursday + Result[m]._64Thursday;

                            if (Result[j]._64Tuesday != null && Result[m]._64Tuesday == null)
                                Result[m]._64Tuesday = Result[j]._64Tuesday;

                            else if (Result[j]._64Tuesday != null && Result[m]._64Tuesday != null)
                                Result[m]._64Tuesday = Result[j]._64Tuesday + Result[m]._64Tuesday;

                            if (Result[j]._64Wednesday != null && Result[m]._64Wednesday == null)
                                Result[m]._64Wednesday = Result[j]._64Wednesday;

                            else if (Result[j]._64Wednesday != null && Result[m]._64Wednesday != null)
                                Result[m]._64Wednesday = Result[j]._64Wednesday + Result[m]._64Wednesday;

                            if (Result[j]._1024Friday != null && Result[m]._1024Friday==null)
                            Result[m]._1024Friday = Result[j]._1024Friday;

                            else if (Result[j]._1024Friday != null && Result[m]._1024Friday!=null)
                                Result[m]._1024Friday = (Result[j]._1024Friday) + Result[m]._1024Friday;

                            if (Result[j]._1024Monday != null && Result[m]._1024Monday == null)
                            Result[m]._1024Monday = Result[j]._1024Monday;

                            else if (Result[j]._1024Monday != null && Result[m]._1024Monday != null)
                                Result[m]._1024Monday = Result[j]._1024Monday + Result[m]._1024Monday;

                            if (Result[j]._1024Saturday != null && Result[m]._1024Saturday==null)
                            Result[m]._1024Saturday = Result[j]._1024Saturday;

                            else if (Result[j]._1024Saturday != null && Result[m]._1024Saturday != null)
                                Result[m]._1024Saturday = Result[j]._1024Saturday + Result[m]._1024Saturday;

                            if (Result[j]._1024Sunday != null && Result[m]._1024Sunday==null)
                            Result[m]._1024Sunday = Result[j]._1024Sunday;

                            else if (Result[j]._1024Sunday != null && Result[m]._1024Sunday != null)
                                Result[m]._1024Sunday =Result[j]._1024Sunday + Result[m]._1024Sunday;

                            if (Result[j]._1024Thursday != null && Result[m]._1024Thursday==null)
                            Result[m]._1024Thursday = Result[j]._1024Thursday;

                            else if (Result[j]._1024Thursday != null && Result[m]._1024Thursday != null)
                                Result[m]._1024Thursday = Result[j]._1024Thursday + Result[m]._1024Thursday;

                            if (Result[j]._1024Tuesday != null && Result[m]._1024Tuesday == null)
                                Result[m]._1024Tuesday = Result[j]._1024Tuesday;

                            else if (Result[j]._1024Tuesday != null && Result[m]._1024Tuesday != null)
                                Result[m]._1024Tuesday = Result[j]._1024Tuesday + Result[m]._1024Tuesday;

                            if (Result[j]._1024Wednesday != null && Result[m]._1024Wednesday==null)
                            Result[m]._1024Wednesday = Result[j]._1024Wednesday;

                            else if (Result[j]._1024Wednesday != null && Result[m]._1024Wednesday != null)
                                Result[m]._1024Wednesday = Result[j]._1024Wednesday +Result[m]._1024Wednesday;

                            if (Result[j]._128Friday != null && Result[m]._128Friday==null)
                            Result[m]._128Friday = Result[j]._128Friday;

                            else if (Result[j]._128Friday != null && Result[m]._128Friday != null)
                                Result[m]._128Friday = Result[j]._128Friday + Result[m]._128Friday;

                            if (Result[j]._128Monday != null && Result[m]._128Monday==null)
                            Result[m]._128Monday = Result[j]._128Monday;

                            else if (Result[j]._128Monday != null && Result[m]._128Monday != null)
                                Result[m]._128Monday = Result[j]._128Monday +Result[m]._128Monday;

                            if (Result[j]._128Saturday != null && Result[m]._128Saturday==null)
                            Result[m]._128Saturday = Result[j]._128Saturday;

                            else if (Result[j]._128Saturday != null && Result[m]._128Saturday != null)
                                Result[m]._128Saturday =Result[j]._128Saturday + Result[m]._128Saturday;

                            if (Result[j]._128Sunday != null && Result[m]._128Sunday==null)
                            Result[m]._128Sunday = Result[j]._128Sunday;

                            else if (Result[j]._128Sunday != null && Result[m]._128Sunday != null)
                                Result[m]._128Sunday =Result[j]._128Sunday + Result[m]._128Sunday;

                            if (Result[j]._128Thursday != null && Result[m]._128Thursday==null)
                            Result[m]._128Thursday = Result[j]._128Thursday;

                            else if (Result[j]._128Thursday != null && Result[m]._128Thursday != null)
                                Result[m]._128Thursday =Result[j]._128Thursday + Result[m]._128Thursday;

                            if (Result[j]._128Tuesday != null && Result[m]._128Tuesday==null)
                            Result[m]._128Tuesday = Result[j]._128Tuesday;

                            else if (Result[j]._128Tuesday != null && Result[m]._128Tuesday != null)
                                Result[m]._128Tuesday =Result[j]._128Tuesday + Result[m]._128Tuesday;

                            if (Result[j]._128Wednesday != null && Result[m]._128Wednesday==null)
                            Result[m]._128Wednesday = Result[j]._128Wednesday;

                            else if (Result[j]._128Wednesday != null && Result[m]._128Wednesday != null)
                                Result[m]._128Wednesday = Result[j]._128Wednesday + Result[m]._128Wednesday;

                            if (Result[j]._2048Friday != null && Result[m]._2048Friday==null)
                            Result[m]._2048Friday = Result[j]._2048Friday;

                            else if (Result[j]._2048Friday != null && Result[m]._2048Friday != null)
                                Result[m]._2048Friday = Result[j]._2048Friday+Result[m]._2048Friday;

                            if (Result[j]._2048Monday != null && Result[m]._2048Monday==null)
                            Result[m]._2048Monday = Result[j]._2048Monday;

                            else if (Result[j]._2048Monday != null && Result[m]._2048Monday != null)
                                Result[m]._2048Monday = Result[j]._2048Monday +Result[m]._2048Monday;

                            if (Result[j]._2048Saturday != null && Result[m]._2048Saturday==null)
                            Result[m]._2048Saturday = Result[j]._2048Saturday;

                            else if (Result[j]._2048Saturday != null && Result[m]._2048Saturday != null)
                                Result[m]._2048Saturday =Result[j]._2048Saturday+ Result[m]._2048Saturday;

                            if (Result[j]._2048Sunday != null && Result[m]._2048Sunday==null)
                            Result[m]._2048Sunday = Result[j]._2048Sunday;

                            else if (Result[j]._2048Sunday != null && Result[m]._2048Sunday != null)
                                Result[m]._2048Sunday = Result[j]._2048Sunday + Result[m]._2048Sunday;

                            if (Result[j]._2048Thursday != null && Result[m]._2048Thursday==null)
                            Result[m]._2048Thursday = Result[j]._2048Thursday;

                            else if (Result[j]._2048Thursday != null && Result[m]._2048Thursday != null)
                                Result[m]._2048Thursday = Result[j]._2048Thursday +Result[m]._2048Thursday;

                            if (Result[j]._2048Tuesday != null && Result[m]._2048Tuesday==null)
                            Result[m]._2048Tuesday = Result[j]._2048Tuesday;

                            else if (Result[j]._2048Tuesday != null && Result[m]._2048Tuesday != null)
                                Result[m]._2048Tuesday =Result[j]._2048Tuesday + Result[m]._2048Tuesday;

                            if (Result[j]._2048Wednesday != null && Result[m]._2048Wednesday==null)
                            Result[m]._2048Wednesday = Result[j]._2048Wednesday;

                            else if (Result[j]._2048Wednesday != null && Result[m]._2048Wednesday != null)
                                Result[m]._2048Wednesday = Result[j]._2048Wednesday +Result[m]._2048Wednesday;

                            if (Result[j]._256Friday != null && Result[m]._256Friday==null)
                            Result[m]._256Friday = Result[j]._256Friday;

                            else if (Result[j]._256Friday != null && Result[m]._256Friday != null)
                                Result[m]._256Friday = Result[j]._256Friday + Result[m]._256Friday;

                            if (Result[j]._256Monday != null && Result[m]._256Monday==null)
                            Result[m]._256Monday = Result[j]._256Monday;

                            else if (Result[j]._256Monday != null && Result[m]._256Monday != null)
                                Result[m]._256Monday =Result[j]._256Monday +Result[m]._256Monday;

                            if (Result[j]._256Saturday != null && Result[m]._256Saturday==null)
                            Result[m]._256Saturday = Result[j]._256Saturday;

                            else if (Result[j]._256Saturday != null && Result[m]._256Saturday != null)
                                Result[m]._256Saturday =Result[j]._256Saturday +Result[m]._256Saturday;

                            if (Result[j]._256Sunday != null && Result[m]._256Sunday==null)
                            Result[m]._256Sunday = Result[j]._256Sunday;

                            else if (Result[j]._256Sunday != null && Result[m]._256Sunday != null)
                                Result[m]._256Sunday = Result[j]._256Sunday + Result[m]._256Sunday;

                            if (Result[j]._256Thursday != null && Result[m]._256Thursday==null)
                            Result[m]._256Thursday = Result[j]._256Thursday;

                            else if (Result[j]._256Thursday != null && Result[m]._256Thursday != null)
                                Result[m]._256Thursday = Result[j]._256Thursday + Result[m]._256Thursday;

                            if (Result[j]._256Tuesday != null && Result[m]._256Tuesday==null)
                            Result[m]._256Tuesday = Result[j]._256Tuesday;

                            else if (Result[j]._256Tuesday != null && Result[m]._256Tuesday != null)
                                Result[m]._256Tuesday = Result[j]._256Tuesday+ Result[m]._256Tuesday;

                            if (Result[j]._256Wednesday != null && Result[m]._256Wednesday==null)
                            Result[m]._256Wednesday = Result[j]._256Wednesday;

                            else if (Result[j]._256Wednesday != null && Result[m]._256Wednesday != null)
                                Result[m]._256Wednesday = Result[j]._256Wednesday + Result[m]._256Wednesday;

                            if (Result[j]._512Friday != null && Result[m]._512Friday==null)
                            Result[m]._512Friday = Result[j]._512Friday;

                            else if (Result[j]._512Friday != null && Result[m]._512Friday != null)
                                Result[m]._512Friday = Result[j]._512Friday +Result[m]._512Friday;

                            if (Result[j]._512Monday != null && Result[m]._512Monday==null)
                            Result[m]._512Monday = Result[j]._512Monday;

                            else if (Result[j]._512Monday != null && Result[m]._512Monday != null)
                                Result[m]._512Monday = Result[j]._512Monday +Result[m]._512Monday;

                            if (Result[j]._512Saturday != null && Result[m]._512Saturday==null)
                            Result[m]._512Saturday = Result[j]._512Saturday;

                            else if (Result[j]._512Saturday != null && Result[m]._512Saturday != null)
                                Result[m]._512Saturday =Result[j]._512Saturday + Result[m]._512Saturday;

                            if (Result[j]._512Sunday != null && Result[m]._512Sunday==null)
                            Result[m]._512Sunday = Result[j]._512Sunday;

                            else if (Result[j]._512Sunday != null && Result[m]._512Sunday != null)
                                Result[m]._512Sunday = Result[j]._512Sunday + Result[m]._512Sunday;

                            if (Result[j]._512Thursday != null && Result[m]._512Thursday==null)
                            Result[m]._512Thursday = Result[j]._512Thursday;

                            else if (Result[j]._512Thursday != null && Result[m]._512Thursday != null)
                                Result[m]._512Thursday =Result[j]._512Thursday + Result[m]._512Thursday;

                            if (Result[j]._512Tuesday != null && Result[m]._512Tuesday==null)
                            Result[m]._512Tuesday = Result[j]._512Tuesday;

                            else if (Result[j]._512Tuesday != null && Result[m]._512Tuesday != null)
                                Result[m]._512Tuesday =Result[j]._512Tuesday+  Result[m]._512Tuesday;

                            if (Result[j]._512Wednesday != null && Result[m]._512Wednesday==null)
                            Result[m]._512Wednesday = Result[j]._512Wednesday;

                            else if (Result[j]._512Wednesday != null && Result[m]._512Wednesday != null)
                                Result[m]._512Wednesday =Result[j]._512Wednesday+Result[m]._512Wednesday;

                            if (Result[j]._5120Saturday != null && Result[m]._5120Saturday == null)
                                Result[m]._5120Saturday = Result[j]._5120Saturday;

                            else if (Result[j]._5120Saturday != null && Result[m]._5120Saturday != null)
                                Result[m]._5120Saturday = Result[j]._5120Saturday + Result[m]._5120Saturday;

                            if (Result[j]._5120Sunday != null && Result[m]._5120Sunday == null)
                                Result[m]._5120Sunday = Result[j]._5120Sunday;

                            else if (Result[j]._5120Sunday != null && Result[m]._5120Sunday != null)
                                Result[m]._5120Sunday = Result[j]._5120Sunday + Result[m]._5120Sunday;

                            if (Result[j]._5120Monday != null && Result[m]._5120Monday == null)
                                Result[m]._5120Monday = Result[j]._5120Monday;

                            else if (Result[j]._5120Monday != null && Result[m]._5120Monday != null)
                                Result[m]._5120Monday = Result[j]._5120Monday + Result[m]._5120Monday;

                            if (Result[j]._5120Tuesday != null && Result[m]._5120Tuesday == null)
                                Result[m]._5120Tuesday = Result[j]._5120Tuesday;

                            else if (Result[j]._5120Tuesday != null && Result[m]._5120Tuesday != null)
                                Result[m]._5120Tuesday = Result[j]._5120Tuesday + Result[m]._5120Tuesday;

                            if (Result[j]._5120Wednesday != null && Result[m]._5120Wednesday == null)
                                Result[m]._5120Wednesday = Result[j]._5120Wednesday;

                            else if (Result[j]._5120Wednesday != null && Result[m]._5120Wednesday != null)
                                Result[m]._5120Wednesday = Result[j]._5120Wednesday + Result[m]._5120Wednesday;

                            if (Result[j]._5120Thursday != null && Result[m]._5120Thursday == null)
                                Result[m]._5120Thursday = Result[j]._5120Thursday;

                            else if (Result[j]._5120Thursday != null && Result[m]._5120Thursday != null)
                                Result[m]._5120Thursday = Result[j]._5120Thursday + Result[m]._5120Thursday;

                            if (Result[j]._5120Friday != null && Result[m]._5120Friday == null)
                                Result[m]._5120Friday = Result[j]._5120Friday;

                            else if (Result[j]._5120Friday != null && Result[m]._5120Friday != null)
                                Result[m]._5120Friday = Result[j]._5120Friday + Result[m]._5120Friday;

                            if (Result[j]._10240Saturday != null && Result[m]._10240Saturday == null)
                                Result[m]._10240Saturday = Result[j]._10240Saturday;

                            else if (Result[j]._10240Saturday != null && Result[m]._10240Saturday != null)
                                Result[m]._10240Saturday = Result[j]._10240Saturday + Result[m]._10240Saturday;

                            if (Result[j]._10240Sunday != null && Result[m]._10240Sunday == null)
                                Result[m]._10240Sunday = Result[j]._10240Sunday;

                            else if (Result[j]._10240Sunday != null && Result[m]._10240Sunday != null)
                                Result[m]._10240Sunday = Result[j]._10240Sunday + Result[m]._10240Sunday;

                            if (Result[j]._10240Monday != null && Result[m]._10240Monday == null)
                                Result[m]._10240Monday = Result[j]._10240Monday;

                            else if (Result[j]._10240Monday != null && Result[m]._10240Monday != null)
                                Result[m]._10240Monday = Result[j]._10240Monday + Result[m]._10240Monday;

                            if (Result[j]._10240Tuesday != null && Result[m]._10240Tuesday == null)
                                Result[m]._10240Tuesday = Result[j]._10240Tuesday;

                            else if (Result[j]._10240Tuesday != null && Result[m]._10240Tuesday != null)
                                Result[m]._10240Tuesday = Result[j]._10240Tuesday + Result[m]._10240Tuesday;

                            if (Result[j]._10240Wednesday != null && Result[m]._10240Wednesday == null)
                                Result[m]._10240Wednesday = Result[j]._10240Wednesday;

                            else if (Result[j]._10240Wednesday != null && Result[m]._10240Wednesday != null)
                                Result[m]._10240Wednesday = Result[j]._10240Wednesday + Result[m]._10240Wednesday;

                            if (Result[j]._10240Thursday != null && Result[m]._10240Thursday == null)
                                Result[m]._10240Thursday = Result[j]._10240Thursday;

                            else if (Result[j]._10240Thursday != null && Result[m]._10240Thursday != null)
                                Result[m]._10240Thursday = Result[j]._10240Thursday + Result[m]._10240Thursday;

                            if (Result[j]._10240Friday != null && Result[m]._10240Friday == null)
                                Result[m]._10240Friday = Result[j]._10240Friday;

                            else if (Result[j]._10240Friday != null && Result[m]._10240Friday != null)
                                Result[m]._10240Friday = Result[j]._10240Friday + Result[m]._10240Friday;

                                Result.Remove(Result[j]);
                            j--;
                        }
                    }
                }

#endregion

                #region ADSLChangeServiceADSLRequest

                
                //for (int j = 0; j < ResultADSLChangeService.Count(); j++)
                //{
                //    bool AddADSLChangeService=false;

                //    for (int m = 0; m < Result.Count(); m++)
                //    {
                //        if (Result[m].CityName == ResultADSLChangeService[j].CityName && Result[m].CenterName == ResultADSLChangeService[j].CenterName)
                //        {
                //            if (ResultADSLChangeService[j]._64Friday != null && Result[m]._64Friday == null)
                //                Result[m]._64Friday = ResultADSLChangeService[j]._64Friday;

                //            else if (ResultADSLChangeService[j]._64Friday != null && Result[m]._64Friday != null)
                //                Result[m]._64Friday = ResultADSLChangeService[j]._64Friday + Result[m]._64Friday;

                //            if (ResultADSLChangeService[j]._64Monday != null && Result[m]._64Monday == null)
                //                Result[m]._64Monday = ResultADSLChangeService[j]._64Monday;

                //            else if (ResultADSLChangeService[j]._64Monday != null && Result[m]._64Monday != null)
                //                Result[m]._64Monday = ResultADSLChangeService[j]._64Monday + Result[m]._64Monday;

                //            if (ResultADSLChangeService[j]._64Saturday != null && Result[m]._64Saturday == null)
                //                Result[m]._64Saturday = ResultADSLChangeService[j]._64Saturday;

                //            else if (ResultADSLChangeService[j]._64Saturday != null && Result[m]._64Saturday != null)
                //                Result[m]._64Saturday = ResultADSLChangeService[j]._64Saturday + Result[m]._64Saturday;

                //            if (ResultADSLChangeService[j]._64Sunday != null && Result[m]._64Sunday == null)
                //                Result[m]._64Sunday = ResultADSLChangeService[j]._64Sunday;

                //            else if (ResultADSLChangeService[j]._64Sunday != null && Result[m]._64Sunday != null)
                //                Result[m]._64Sunday = ResultADSLChangeService[j]._64Sunday + Result[m]._64Sunday;

                //            if (ResultADSLChangeService[j]._64Thursday != null && Result[m]._64Thursday == null)
                //                Result[m]._64Thursday = ResultADSLChangeService[j]._64Thursday;

                //            else if (ResultADSLChangeService[j]._64Thursday != null && Result[m]._64Thursday != null)
                //                Result[m]._64Thursday = ResultADSLChangeService[j]._64Thursday + Result[m]._64Thursday;

                //            if (ResultADSLChangeService[j]._64Tuesday != null && Result[m]._64Tuesday == null)
                //                Result[m]._64Tuesday = ResultADSLChangeService[j]._64Tuesday;

                //            else if (ResultADSLChangeService[j]._64Tuesday != null && Result[m]._64Tuesday != null)
                //                Result[m]._64Tuesday = ResultADSLChangeService[j]._64Tuesday + Result[m]._64Tuesday;

                //            if (ResultADSLChangeService[j]._64Wednesday != null && Result[m]._64Wednesday == null)
                //                Result[m]._64Wednesday = ResultADSLChangeService[j]._64Wednesday;

                //            else if (ResultADSLChangeService[j]._64Wednesday != null && Result[m]._64Wednesday != null)
                //                Result[m]._64Wednesday = ResultADSLChangeService[j]._64Wednesday + Result[m]._64Wednesday;

                //            if (ResultADSLChangeService[j]._1024Friday != null && Result[m]._1024Friday==null)
                //            Result[m]._1024Friday = ResultADSLChangeService[j]._1024Friday;

                //            else if (ResultADSLChangeService[j]._1024Friday != null && Result[m]._1024Friday!=null)
                //                Result[m]._1024Friday = (ResultADSLChangeService[j]._1024Friday) + Result[m]._1024Friday;

                //            if (ResultADSLChangeService[j]._1024Monday != null && Result[m]._1024Monday == null)
                //            Result[m]._1024Monday = ResultADSLChangeService[j]._1024Monday;

                //            else if (ResultADSLChangeService[j]._1024Monday != null && Result[m]._1024Monday != null)
                //                Result[m]._1024Monday = ResultADSLChangeService[j]._1024Monday + Result[m]._1024Monday;

                //            if (ResultADSLChangeService[j]._1024Saturday != null && Result[m]._1024Saturday==null)
                //            Result[m]._1024Saturday = ResultADSLChangeService[j]._1024Saturday;

                //            else if (ResultADSLChangeService[j]._1024Saturday != null && Result[m]._1024Saturday != null)
                //                Result[m]._1024Saturday = ResultADSLChangeService[j]._1024Saturday + Result[m]._1024Saturday;

                //            if (ResultADSLChangeService[j]._1024Sunday != null && Result[m]._1024Sunday==null)
                //            Result[m]._1024Sunday = ResultADSLChangeService[j]._1024Sunday;

                //            else if (ResultADSLChangeService[j]._1024Sunday != null && Result[m]._1024Sunday != null)
                //                Result[m]._1024Sunday =ResultADSLChangeService[j]._1024Sunday + Result[m]._1024Sunday;

                //            if (ResultADSLChangeService[j]._1024Thursday != null && Result[m]._1024Thursday==null)
                //            Result[m]._1024Thursday = ResultADSLChangeService[j]._1024Thursday;

                //            else if (ResultADSLChangeService[j]._1024Thursday != null && Result[m]._1024Thursday != null)
                //                Result[m]._1024Thursday = ResultADSLChangeService[j]._1024Thursday + Result[m]._1024Thursday;

                //            if (ResultADSLChangeService[j]._1024Tuesday != null && Result[m]._1024Tuesday == null)
                //                Result[m]._1024Tuesday = ResultADSLChangeService[j]._1024Tuesday;

                //            else if (ResultADSLChangeService[j]._1024Tuesday != null && Result[m]._1024Tuesday != null)
                //                Result[m]._1024Tuesday = ResultADSLChangeService[j]._1024Tuesday + Result[m]._1024Tuesday;

                //            if (ResultADSLChangeService[j]._1024Wednesday != null && Result[m]._1024Wednesday==null)
                //            Result[m]._1024Wednesday = ResultADSLChangeService[j]._1024Wednesday;

                //            else if (ResultADSLChangeService[j]._1024Wednesday != null && Result[m]._1024Wednesday != null)
                //                Result[m]._1024Wednesday = ResultADSLChangeService[j]._1024Wednesday +Result[m]._1024Wednesday;

                //            if (ResultADSLChangeService[j]._128Friday != null && Result[m]._128Friday==null)
                //            Result[m]._128Friday = ResultADSLChangeService[j]._128Friday;

                //            else if (ResultADSLChangeService[j]._128Friday != null && Result[m]._128Friday != null)
                //                Result[m]._128Friday = ResultADSLChangeService[j]._128Friday + Result[m]._128Friday;

                //            if (ResultADSLChangeService[j]._128Monday != null && Result[m]._128Monday==null)
                //            Result[m]._128Monday = ResultADSLChangeService[j]._128Monday;

                //            else if (ResultADSLChangeService[j]._128Monday != null && Result[m]._128Monday != null)
                //                Result[m]._128Monday = ResultADSLChangeService[j]._128Monday +Result[m]._128Monday;

                //            if (ResultADSLChangeService[j]._128Saturday != null && Result[m]._128Saturday==null)
                //            Result[m]._128Saturday = ResultADSLChangeService[j]._128Saturday;

                //            else if (ResultADSLChangeService[j]._128Saturday != null && Result[m]._128Saturday != null)
                //                Result[m]._128Saturday =ResultADSLChangeService[j]._128Saturday + Result[m]._128Saturday;

                //            if (ResultADSLChangeService[j]._128Sunday != null && Result[m]._128Sunday==null)
                //            Result[m]._128Sunday = ResultADSLChangeService[j]._128Sunday;

                //            else if (ResultADSLChangeService[j]._128Sunday != null && Result[m]._128Sunday != null)
                //                Result[m]._128Sunday =ResultADSLChangeService[j]._128Sunday + Result[m]._128Sunday;

                //            if (ResultADSLChangeService[j]._128Thursday != null && Result[m]._128Thursday==null)
                //            Result[m]._128Thursday = ResultADSLChangeService[j]._128Thursday;

                //            else if (ResultADSLChangeService[j]._128Thursday != null && Result[m]._128Thursday != null)
                //                Result[m]._128Thursday =ResultADSLChangeService[j]._128Thursday + Result[m]._128Thursday;

                //            if (ResultADSLChangeService[j]._128Tuesday != null && Result[m]._128Tuesday==null)
                //            Result[m]._128Tuesday = ResultADSLChangeService[j]._128Tuesday;

                //            else if (ResultADSLChangeService[j]._128Tuesday != null && Result[m]._128Tuesday != null)
                //                Result[m]._128Tuesday =ResultADSLChangeService[j]._128Tuesday + Result[m]._128Tuesday;

                //            if (ResultADSLChangeService[j]._128Wednesday != null && Result[m]._128Wednesday==null)
                //            Result[m]._128Wednesday = ResultADSLChangeService[j]._128Wednesday;

                //            else if (ResultADSLChangeService[j]._128Wednesday != null && Result[m]._128Wednesday != null)
                //                Result[m]._128Wednesday = ResultADSLChangeService[j]._128Wednesday + Result[m]._128Wednesday;

                //            if (ResultADSLChangeService[j]._2048Friday != null && Result[m]._2048Friday==null)
                //            Result[m]._2048Friday = ResultADSLChangeService[j]._2048Friday;

                //            else if (ResultADSLChangeService[j]._2048Friday != null && Result[m]._2048Friday != null)
                //                Result[m]._2048Friday = ResultADSLChangeService[j]._2048Friday+Result[m]._2048Friday;

                //            if (ResultADSLChangeService[j]._2048Monday != null && Result[m]._2048Monday==null)
                //            Result[m]._2048Monday = ResultADSLChangeService[j]._2048Monday;

                //            else if (ResultADSLChangeService[j]._2048Monday != null && Result[m]._2048Monday != null)
                //                Result[m]._2048Monday = ResultADSLChangeService[j]._2048Monday +Result[m]._2048Monday;

                //            if (ResultADSLChangeService[j]._2048Saturday != null && Result[m]._2048Saturday==null)
                //            Result[m]._2048Saturday = ResultADSLChangeService[j]._2048Saturday;

                //            else if (ResultADSLChangeService[j]._2048Saturday != null && Result[m]._2048Saturday != null)
                //                Result[m]._2048Saturday =ResultADSLChangeService[j]._2048Saturday+ Result[m]._2048Saturday;

                //            if (ResultADSLChangeService[j]._2048Sunday != null && Result[m]._2048Sunday==null)
                //            Result[m]._2048Sunday = ResultADSLChangeService[j]._2048Sunday;

                //            else if (ResultADSLChangeService[j]._2048Sunday != null && Result[m]._2048Sunday != null)
                //                Result[m]._2048Sunday = ResultADSLChangeService[j]._2048Sunday + Result[m]._2048Sunday;

                //            if (ResultADSLChangeService[j]._2048Thursday != null && Result[m]._2048Thursday==null)
                //            Result[m]._2048Thursday = ResultADSLChangeService[j]._2048Thursday;

                //            else if (ResultADSLChangeService[j]._2048Thursday != null && Result[m]._2048Thursday != null)
                //                Result[m]._2048Thursday = ResultADSLChangeService[j]._2048Thursday +Result[m]._2048Thursday;

                //            if (ResultADSLChangeService[j]._2048Tuesday != null && Result[m]._2048Tuesday==null)
                //            Result[m]._2048Tuesday = ResultADSLChangeService[j]._2048Tuesday;

                //            else if (ResultADSLChangeService[j]._2048Tuesday != null && Result[m]._2048Tuesday != null)
                //                Result[m]._2048Tuesday =ResultADSLChangeService[j]._2048Tuesday + Result[m]._2048Tuesday;

                //            if (ResultADSLChangeService[j]._2048Wednesday != null && Result[m]._2048Wednesday==null)
                //            Result[m]._2048Wednesday = ResultADSLChangeService[j]._2048Wednesday;

                //            else if (ResultADSLChangeService[j]._2048Wednesday != null && Result[m]._2048Wednesday != null)
                //                Result[m]._2048Wednesday = ResultADSLChangeService[j]._2048Wednesday +Result[m]._2048Wednesday;

                //            if (ResultADSLChangeService[j]._256Friday != null && Result[m]._256Friday==null)
                //            Result[m]._256Friday = ResultADSLChangeService[j]._256Friday;

                //            else if (ResultADSLChangeService[j]._256Friday != null && Result[m]._256Friday != null)
                //                Result[m]._256Friday = ResultADSLChangeService[j]._256Friday + Result[m]._256Friday;

                //            if (ResultADSLChangeService[j]._256Monday != null && Result[m]._256Monday==null)
                //            Result[m]._256Monday = ResultADSLChangeService[j]._256Monday;

                //            else if (ResultADSLChangeService[j]._256Monday != null && Result[m]._256Monday != null)
                //                Result[m]._256Monday =ResultADSLChangeService[j]._256Monday +Result[m]._256Monday;

                //            if (ResultADSLChangeService[j]._256Saturday != null && Result[m]._256Saturday==null)
                //            Result[m]._256Saturday = ResultADSLChangeService[j]._256Saturday;

                //            else if (ResultADSLChangeService[j]._256Saturday != null && Result[m]._256Saturday != null)
                //                Result[m]._256Saturday =ResultADSLChangeService[j]._256Saturday +Result[m]._256Saturday;

                //            if (ResultADSLChangeService[j]._256Sunday != null && Result[m]._256Sunday==null)
                //            Result[m]._256Sunday = ResultADSLChangeService[j]._256Sunday;

                //            else if (ResultADSLChangeService[j]._256Sunday != null && Result[m]._256Sunday != null)
                //                Result[m]._256Sunday = ResultADSLChangeService[j]._256Sunday + Result[m]._256Sunday;

                //            if (ResultADSLChangeService[j]._256Thursday != null && Result[m]._256Thursday==null)
                //            Result[m]._256Thursday = ResultADSLChangeService[j]._256Thursday;

                //            else if (ResultADSLChangeService[j]._256Thursday != null && Result[m]._256Thursday != null)
                //                Result[m]._256Thursday = ResultADSLChangeService[j]._256Thursday + Result[m]._256Thursday;

                //            if (ResultADSLChangeService[j]._256Tuesday != null && Result[m]._256Tuesday==null)
                //            Result[m]._256Tuesday = ResultADSLChangeService[j]._256Tuesday;

                //            else if (ResultADSLChangeService[j]._256Tuesday != null && Result[m]._256Tuesday != null)
                //                Result[m]._256Tuesday = ResultADSLChangeService[j]._256Tuesday+ Result[m]._256Tuesday;

                //            if (ResultADSLChangeService[j]._256Wednesday != null && Result[m]._256Wednesday==null)
                //            Result[m]._256Wednesday = ResultADSLChangeService[j]._256Wednesday;

                //            else if (ResultADSLChangeService[j]._256Wednesday != null && Result[m]._256Wednesday != null)
                //                Result[m]._256Wednesday = ResultADSLChangeService[j]._256Wednesday + Result[m]._256Wednesday;

                //            if (ResultADSLChangeService[j]._512Friday != null && Result[m]._512Friday==null)
                //            Result[m]._512Friday = ResultADSLChangeService[j]._512Friday;

                //            else if (ResultADSLChangeService[j]._512Friday != null && Result[m]._512Friday != null)
                //                Result[m]._512Friday = ResultADSLChangeService[j]._512Friday +Result[m]._512Friday;

                //            if (ResultADSLChangeService[j]._512Monday != null && Result[m]._512Monday==null)
                //            Result[m]._512Monday = ResultADSLChangeService[j]._512Monday;

                //            else if (ResultADSLChangeService[j]._512Monday != null && Result[m]._512Monday != null)
                //                Result[m]._512Monday = ResultADSLChangeService[j]._512Monday +Result[m]._512Monday;

                //            if (ResultADSLChangeService[j]._512Saturday != null && Result[m]._512Saturday==null)
                //            Result[m]._512Saturday = ResultADSLChangeService[j]._512Saturday;

                //            else if (ResultADSLChangeService[j]._512Saturday != null && Result[m]._512Saturday != null)
                //                Result[m]._512Saturday =ResultADSLChangeService[j]._512Saturday + Result[m]._512Saturday;

                //            if (ResultADSLChangeService[j]._512Sunday != null && Result[m]._512Sunday==null)
                //            Result[m]._512Sunday = ResultADSLChangeService[j]._512Sunday;

                //            else if (ResultADSLChangeService[j]._512Sunday != null && Result[m]._512Sunday != null)
                //                Result[m]._512Sunday = ResultADSLChangeService[j]._512Sunday + Result[m]._512Sunday;

                //            if (ResultADSLChangeService[j]._512Thursday != null && Result[m]._512Thursday==null)
                //            Result[m]._512Thursday = ResultADSLChangeService[j]._512Thursday;

                //            else if (ResultADSLChangeService[j]._512Thursday != null && Result[m]._512Thursday != null)
                //                Result[m]._512Thursday =ResultADSLChangeService[j]._512Thursday + Result[m]._512Thursday;

                //            if (ResultADSLChangeService[j]._512Tuesday != null && Result[m]._512Tuesday==null)
                //            Result[m]._512Tuesday = ResultADSLChangeService[j]._512Tuesday;

                //            else if (ResultADSLChangeService[j]._512Tuesday != null && Result[m]._512Tuesday != null)
                //                Result[m]._512Tuesday =ResultADSLChangeService[j]._512Tuesday+  Result[m]._512Tuesday;

                //            if (ResultADSLChangeService[j]._512Wednesday != null && Result[m]._512Wednesday==null)
                //            Result[m]._512Wednesday = ResultADSLChangeService[j]._512Wednesday;

                //            else if (ResultADSLChangeService[j]._512Wednesday != null && Result[m]._512Wednesday != null)
                //                Result[m]._512Wednesday =ResultADSLChangeService[j]._512Wednesday+Result[m]._512Wednesday;


                //            AddADSLChangeService=true;
                //        }
                //    }

                //    if(AddADSLChangeService==false)
                //    {
                //        Result.Add(ResultADSLChangeService[j]);
                //    }
                //}

         #endregion

                #region ADSLDischarge

                for (int i = 0; i < ResultDischarge.Count(); i++)
                {
                      bool add = false;
                    for (int m = 0; m < Result.Count(); m++)
                    {

                        if (Result[m].CityName == ResultDischarge[i].CityName && Result[m].CenterName == ResultDischarge[i].CenterName)
                        {
                            if (Result[m].SaturdayDischarge == null && ResultDischarge[i].SaturdayDischarge!=null)
                            Result[m].SaturdayDischarge = ResultDischarge[i].SaturdayDischarge;
                            if (Result[m].SundayDischarge == null && ResultDischarge[i].SundayDischarge!=null)
                            Result[m].SundayDischarge = ResultDischarge[i].SundayDischarge;
                            if (Result[m].MondayDischarge == null && ResultDischarge[i].MondayDischarge!=null)
                            Result[m].MondayDischarge = ResultDischarge[i].MondayDischarge;
                            if (Result[m].TuesdayDischarge == null && ResultDischarge[i].TuesdayDischarge!=null)
                            Result[m].TuesdayDischarge = ResultDischarge[i].TuesdayDischarge;
                            if (Result[m].WednesdayDischarge == null && ResultDischarge[i].WednesdayDischarge!=null)
                            Result[m].WednesdayDischarge = ResultDischarge[i].WednesdayDischarge;
                            if (Result[m].ThursdayDischarge == null && ResultDischarge[i].ThursdayDischarge!=null)
                            Result[m].ThursdayDischarge = ResultDischarge[i].ThursdayDischarge;
                            if (Result[m].FridayDischarge == null && ResultDischarge[i].FridayDischarge!=null)
                            Result[m].FridayDischarge = ResultDischarge[i].FridayDischarge;
                           
                            add = true;
                            break;
                        }

                    }
                    if (add == false)
                    {
                        Result.Add(ResultDischarge[i]);
                        break;
                    }
                }

                #endregion

                string title = string.Empty;
                string path;
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                path = ReportDB.GetReportPath(UserControlID);
                stiReport.Load(path);
                stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

                if (fromDayeriDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["fromDayeriDate"].Value = Helper.GetPersianDate(fromDayeriDate.SelectedDate, Helper.DateStringType.Short).ToString();
                if (toDayeriDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["toDayeriDate"].Value = Helper.GetPersianDate(toDayeriDate.SelectedDate, Helper.DateStringType.Short).ToString();

                stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplainationTextBox.Text;

                if (fromDischargeDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["fromDischargeDate"].Value = Helper.GetPersianDate(fromDischargeDate.SelectedDate, Helper.DateStringType.Short).ToString();
                if (toDischargeDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["toDischargeDate"].Value = Helper.GetPersianDate(toDischargeDate.SelectedDate, Helper.DateStringType.Short).ToString();



                title = " آمار ثبت نامی های جدید روزانه شرکت مخابرات ";
                stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("Result", "Result", Result);


                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            
        }
    

        private List<ADSLCityCenterBandwidthDailyInfo> LoadData()
        {
            DateTime? toDate = null;
            if (toDayeriDate.SelectedDate.HasValue)
            {
                toDate = toDayeriDate.SelectedDate.Value.AddDays(1);
            }

            List<ADSLCityCenterBandwidthDailyInfo> result = ReportDB.GetADSLCityCenterBandwidthDayeriDailyInfo(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                 CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                                 fromDayeriDate.SelectedDate,
                                                                                 toDate, PaymentTypeComboBox.SelectedIDs);
            foreach (ADSLCityCenterBandwidthDailyInfo Info in result)
            {
                if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth == "64")
                {
                    Info._64Saturday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "64")
                {
                    Info._64Sunday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "64")
                {
                    Info._64Monday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "64")
                {
                    Info._64Tuesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "64")
                {
                    Info._64Wednesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "64")
                {
                    Info._64Thursday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "64")
                {
                    Info._64Friday = Info.NumberOfDayeri;
                }

                if (Info.DayName==DayOfWeek.Saturday && Info.BandWidth=="128")
                {
                    Info._128Saturday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "128")
                {
                    Info._128Sunday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "128")
                {
                    Info._128Monday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "128")
                {
                    Info._128Tuesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "128")
                {
                    Info._128Wednesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "128")
                {
                    Info._128Thursday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "128")
                {
                    Info._128Friday = Info.NumberOfDayeri;
                }

                if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth=="256")
                {
                    Info._256Saturday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "256")
                {
                    Info._256Sunday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "256")
                {
                    Info._256Monday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "256")
                {
                    Info._256Tuesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "256")
                {
                    Info._256Wednesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "256")
                {
                    Info._256Thursday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "256")
                {
                    Info._256Friday = Info.NumberOfDayeri;
                }

                if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth=="512")
                {
                    Info._512Saturday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "512")
                {
                    Info._512Sunday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "512")
                {
                    Info._512Monday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "512")
                {
                    Info._512Tuesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "512")
                {
                    Info._512Wednesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "512")
                {
                    Info._512Thursday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "512")
                {
                    Info._512Friday = Info.NumberOfDayeri;
                }

                if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth=="1024")
                {
                    Info._1024Saturday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "1024")
                {
                    Info._1024Sunday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "1024")
                {
                    Info._1024Monday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "1024")
                {
                    Info._1024Tuesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "1024")
                {
                    Info._1024Wednesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "1024")
                {
                    Info._1024Thursday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "1024")
                {
                    Info._1024Friday = Info.NumberOfDayeri;
                }

                if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth == "2048")
                {
                    Info._2048Saturday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "2048")
                {
                    Info._2048Sunday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "2048")
                {
                    Info._2048Monday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "2048")
                {
                    Info._2048Tuesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "2048")
                {
                    Info._2048Wednesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "2048")
                {
                    Info._2048Thursday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "2048")
                {
                    Info._2048Friday = Info.NumberOfDayeri;
                }

                if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth == "5120")
                {
                    Info._5120Saturday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "5120")
                {
                    Info._5120Sunday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "5120")
                {
                    Info._5120Monday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "5120")
                {
                    Info._5120Tuesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "5120")
                {
                    Info._5120Wednesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "5120")
                {
                    Info._5120Thursday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "5120")
                {
                    Info._5120Friday = Info.NumberOfDayeri;
                }

                if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth == "10240")
                {
                    Info._10240Saturday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "10240")
                {
                    Info._10240Sunday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "10240")
                {
                    Info._10240Monday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "10240")
                {
                    Info._10240Tuesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "10240")
                {
                    Info._10240Wednesday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "10240")
                {
                    Info._10240Thursday = Info.NumberOfDayeri;
                }
                if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "10240")
                {
                    Info._10240Friday = Info.NumberOfDayeri;
                }

                //Info.SumOfDayeriWeek = (!Info._128Saturday.HasValue?0:Info._128Saturday) + (!Info._128Sunday.HasValue?0:Info._128Sunday) + (!Info._128Monday.HasValue?0:Info._128Monday)
                //    +(! Info._128Tuesday.HasValue?0:Info._128Tuesday) +(! Info._128Wednesday.HasValue?0:Info._128Wednesday) +( !Info._128Thursday.HasValue?0:Info._128Thursday)
                //    +(! Info._128Friday.HasValue?0:Info._128Friday) +
                //    (!Info._256Saturday.HasValue?0:Info._256Saturday) + (!Info._256Sunday.HasValue?0:Info._256Sunday) + (!Info._256Monday.HasValue?0:Info._256Monday)
                //    + (!Info._256Tuesday.HasValue?0:Info._256Tuesday) + (!Info._256Wednesday.HasValue?0:Info._256Wednesday) + (!Info._256Thursday.HasValue?0:Info._256Thursday)
                //    + (!Info._256Friday.HasValue?0:Info._256Friday) +
                //    (!Info._512Saturday.HasValue?0:Info._512Saturday) + (!Info._512Sunday.HasValue?0:Info._512Sunday) + (!Info._512Monday.HasValue?0:Info._512Monday)
                //    + (!Info._512Thursday.HasValue?0:Info._512Thursday) + (!Info._512Wednesday.HasValue?0:Info._512Wednesday) + (!Info._512Thursday.HasValue?0:Info._512Thursday)
                //    + (!Info._512Friday.HasValue?0:Info._512Friday) +
                //    (!Info._1024Saturday.HasValue?0:Info._1024Saturday) + (!Info._1024Sunday.HasValue?0:Info._1024Sunday) + (!Info._1024Monday.HasValue?0:Info._1024Monday)
                //    + (!Info._1024Tuesday.HasValue?0:Info._1024Tuesday) + (!Info._1024Wednesday.HasValue?0:Info._1024Wednesday) + (!Info._1024Thursday.HasValue?0:Info._1024Thursday)
                //    + (!Info._1024Friday.HasValue?0:Info._1024Friday) +
                //    (!Info._2048Saturday.HasValue?0:Info._2048Saturday) + (!Info._2048Sunday.HasValue?0:Info._2048Sunday) + (!Info._2048Monday.HasValue?0:Info._2048Monday)
                //    + (!Info._2048Tuesday.HasValue?0:Info._2048Tuesday) + (!Info._2048Wednesday.HasValue?0:Info._2048Wednesday) + (!Info._2048Thursday.HasValue?0:Info._2048Thursday)
                //    + (!Info._2048Friday.HasValue?0:Info._2048Friday);
            }

            return result;
        }

        //private List<ADSLCityCenterBandwidthDailyInfo> LoadADSLChangeServiceData()
        //{
        //    DateTime? toDate = null;
        //    if (toDayeriDate.SelectedDate.HasValue)
        //    {
        //        toDate = toDayeriDate.SelectedDate.Value.AddDays(1);
        //    }

        //    List<ADSLCityCenterBandwidthDailyInfo> result = ReportDB.GetADSLChangeSErviceCityCenterBandwidthDayeriDailyInfo(CityCenterComboBox.CityComboBox.SelectedIDs,
        //                                                                         CityCenterComboBox.CenterComboBox.SelectedIDs,
        //                                                                         fromDayeriDate.SelectedDate,
        //                                                                         toDayeriDate.SelectedDate, PaymentTypeComboBox.SelectedIDs);
        //    foreach (ADSLCityCenterBandwidthDailyInfo Info in result)
        //    {
        //        if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth == "64")
        //        {
        //            Info._64Saturday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "64")
        //        {
        //            Info._64Sunday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "64")
        //        {
        //            Info._64Monday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "64")
        //        {
        //            Info._64Tuesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "64")
        //        {
        //            Info._64Wednesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "64")
        //        {
        //            Info._64Thursday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "64")
        //        {
        //            Info._64Friday = Info.NumberOfDayeri;
        //        }

        //        if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth == "128")
        //        {
        //            Info._128Saturday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "128")
        //        {
        //            Info._128Sunday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "128")
        //        {
        //            Info._128Monday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "128")
        //        {
        //            Info._128Tuesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "128")
        //        {
        //            Info._128Wednesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "128")
        //        {
        //            Info._128Thursday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "128")
        //        {
        //            Info._128Friday = Info.NumberOfDayeri;
        //        }

        //        if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth == "256")
        //        {
        //            Info._256Saturday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "256")
        //        {
        //            Info._256Sunday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "256")
        //        {
        //            Info._256Monday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "256")
        //        {
        //            Info._256Tuesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "256")
        //        {
        //            Info._256Wednesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "256")
        //        {
        //            Info._256Thursday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "256")
        //        {
        //            Info._256Friday = Info.NumberOfDayeri;
        //        }

        //        if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth == "512")
        //        {
        //            Info._512Saturday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "512")
        //        {
        //            Info._512Sunday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "512")
        //        {
        //            Info._512Monday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "512")
        //        {
        //            Info._512Tuesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "512")
        //        {
        //            Info._512Wednesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "512")
        //        {
        //            Info._512Thursday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "512")
        //        {
        //            Info._512Friday = Info.NumberOfDayeri;
        //        }

        //        if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth == "1024")
        //        {
        //            Info._1024Saturday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "1024")
        //        {
        //            Info._1024Sunday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "1024")
        //        {
        //            Info._1024Monday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "1024")
        //        {
        //            Info._1024Tuesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "1024")
        //        {
        //            Info._1024Wednesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "1024")
        //        {
        //            Info._1024Thursday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "1024")
        //        {
        //            Info._1024Friday = Info.NumberOfDayeri;
        //        }

        //        if (Info.DayName == DayOfWeek.Saturday && Info.BandWidth == "2048")
        //        {
        //            Info._2048Saturday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Sunday && Info.BandWidth == "2048")
        //        {
        //            Info._2048Sunday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Monday && Info.BandWidth == "2048")
        //        {
        //            Info._2048Monday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Tuesday && Info.BandWidth == "2048")
        //        {
        //            Info._2048Tuesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Wednesday && Info.BandWidth == "2048")
        //        {
        //            Info._2048Wednesday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Thursday && Info.BandWidth == "2048")
        //        {
        //            Info._2048Thursday = Info.NumberOfDayeri;
        //        }
        //        if (Info.DayName == DayOfWeek.Friday && Info.BandWidth == "2048")
        //        {
        //            Info._2048Friday = Info.NumberOfDayeri;
        //        }

               
        //    }

        //    return result;
        //}

        private List<ADSLCityCenterBandwidthDailyInfo> LoadDataDischarge()
        {

            DateTime? toDate = null;
            if (toDischargeDate.SelectedDate.HasValue)
            {
                toDate = toDischargeDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLCityCenterBandwidthDailyInfo> resultDisharge = ReportDB.GetADSLCityCenterDischargeDailyInfo(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                                                CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                                                                fromDischargeDate.SelectedDate, toDate, PaymentTypeComboBox.SelectedIDs);

            foreach (ADSLCityCenterBandwidthDailyInfo Info in resultDisharge)
            {
                if (Info.DayName == DayOfWeek.Saturday)
                {
                    Info.SaturdayDischarge = Info.NumberOfDischarge;
                }
                if (Info.DayName == DayOfWeek.Sunday)
                {
                    Info.SundayDischarge = Info.NumberOfDischarge;
                }
                if (Info.DayName == DayOfWeek.Monday)
                {
                    Info.MondayDischarge = Info.NumberOfDischarge;
                }
                if (Info.DayName == DayOfWeek.Tuesday)
                {
                    Info.TuesdayDischarge = Info.NumberOfDischarge;
                }
                if (Info.DayName == DayOfWeek.Wednesday)
                {
                    Info.WednesdayDischarge = Info.NumberOfDischarge;
                }
                if (Info.DayName == DayOfWeek.Thursday)
                {
                    Info.ThursdayDischarge = Info.NumberOfDischarge;
                }
                if (Info.DayName == DayOfWeek.Friday)
                {
                    Info.FridayDischarge = Info.NumberOfDischarge;
                }
                Info.SumOfDischargeWeek = (!Info.SaturdayDischarge.HasValue ?0:Info.SaturdayDischarge)
                    +( !Info.SundayDischarge.HasValue ?0:Info.SundayDischarge)
                    + (!Info.MondayDischarge.HasValue?0:Info.MondayDischarge)
                    +(! Info.TuesdayDischarge.HasValue ?0:Info.TuesdayDischarge)
                    + (!Info.WednesdayDischarge.HasValue?0:Info.WednesdayDischarge)
                    + (!Info.ThursdayDischarge.HasValue?0:Info.ThursdayDischarge)
                    + (!Info.FridayDischarge.HasValue?0:Info.FridayDischarge);
            }

            return resultDisharge;
        }

        #endregion Methods
    }
}
