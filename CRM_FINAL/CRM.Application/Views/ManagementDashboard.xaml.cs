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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using CRM.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ManagementDashboard.xaml
    /// </summary>
    public partial class ManagementDashboard : Local.TabWindow
    {

        #region Properties
        List<RequestTitleInfo> _RequestsInfos;
        List<RequestTitleInfo> _RequestsInfosDetails;
        DateTime _TodayDate;
        List<RequestWaitTimeInfo> _RequestWaitTimeInfo;
        List<RequestsTime> _RequestsTime;
        CheckableItemNullable request;
        List<RequestTitleInfo> requestsInfosForCancelationPieChart;
        List<RequestTitleInfo> requestsInfosForRejectionPieChart;

        private BackgroundWorker workerAllRequest;
        private BackgroundWorker workerRequestWaitTimeInfo;
        private BackgroundWorker workerRequestsTime;

        private BackgroundWorker workerRequestsInfos;
        private BackgroundWorker workerRequestsInfosForRejection;
        private BackgroundWorker workerRequestsInfosForCancelation;


        #endregion

        #region Constructor & Loading

        public ManagementDashboard()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            RequestCityComboBox.ItemsSource = RequestWaitTimeCityComboBox.ItemsSource = WaitTimeCityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            RequestWaitTimeTypeComboBox.ItemsSource = TimeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TimeType));


            //Define BackgroundWorker events
            workerAllRequest = new BackgroundWorker();
            workerAllRequest.DoWork += new DoWorkEventHandler(workerAllRequest_DoWork);
            workerAllRequest.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerAllRequest_RunWorkerCompleted);

            workerRequestWaitTimeInfo = new BackgroundWorker();
            workerRequestWaitTimeInfo.DoWork += new DoWorkEventHandler(workerRequestWaitTimeInfo_DoWork);
            workerRequestWaitTimeInfo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerRequestWaitTimeInfo_RunWorkerCompleted);


            workerRequestsTime = new BackgroundWorker();
            workerRequestsTime.DoWork += new DoWorkEventHandler(workerRequestsTime_DoWork);
            workerRequestsTime.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerRequestsTime_RunWorkerCompleted);


            workerRequestsInfos = new BackgroundWorker();
            workerRequestsInfos.DoWork += new DoWorkEventHandler(workerRequestsInfos_DoWork);
            workerRequestsInfos.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerRequestsInfos_RunWorkerCompleted);

            workerRequestsInfosForRejection = new BackgroundWorker();
            workerRequestsInfosForRejection.DoWork += new DoWorkEventHandler(workerRequestsInfosForRejection_DoWork);
            workerRequestsInfosForRejection.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerRequestsInfosForRejection_RunWorkerCompleted);

            workerRequestsInfosForCancelation = new BackgroundWorker();
            workerRequestsInfosForCancelation.DoWork += new DoWorkEventHandler(workerRequestsInfosForCancelation_DoWork);
            workerRequestsInfosForCancelation.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerRequestsInfosForCancelation_RunWorkerCompleted);

            //
            RequestWaitTimeRequestComboBox.ItemsSource = WaitTimeRequestComboBox.ItemsSource = RequestRequestComboBox.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckableNullable().Union(new List<CheckableItemNullable> { new CheckableItemNullable { ID = null } }).ToList();

            request = new CheckableItemNullable();
            requestsInfosForCancelationPieChart = new List<RequestTitleInfo>();
            requestsInfosForRejectionPieChart = new List<RequestTitleInfo>();

            _TodayDate = ((DateTime)DB.GetServerDate()).Date;
        }


        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {

            RequestCounter();
            RequestCityComboBox.SelectedIndex = 0;
            RequestCityComboBox_LostFocus(null, null);
            RequestCenterComboBox.SelectedIndex = 0;
            RequestRequestComboBox.SelectedIndex = 0;
            RequestSearch_Click(null, null);

            RequestWaitTimeCityComboBox.SelectedIndex = 0;
            RequestWaitTimeCityComboBox_LostFocus(null, null);
            RequestWaitTimeCenterComboBox.SelectedIndex = 0;
           // RequestWaitTimeSearch_Click(null, null);

            WaitTimeCityComboBox.SelectedIndex = 0;
            WaitTimeCityComboBox_LostFocus(null, null);
            WaitTimeCenterComboBox.SelectedIndex = 0;
          //  WaitTimeSearch_Click(null, null);

        }
        #endregion

        #region Workers

        //AllRequest



        #region Request
        private void RequestSearch_Click(object sender, RoutedEventArgs e)
        {
            RequestCounter();

            DateTime? a = RequestFromDateTextBox.SelectedDate;
            RequestSearch.Content = "در حال جستجو";
            RequestSearch.IsEnabled = false;
            List<object> arguments = new List<object>();
            arguments.Add((List<int>)RequestCityComboBox.SelectedIDs);
            arguments.Add((List<int>)RequestCenterComboBox.SelectedIDs);
            arguments.Add(RequestFromDateTextBox.SelectedDate);
            arguments.Add(RequestToDateTextBox.SelectedDate);
            arguments.Add((int?)RequestRequestComboBox.SelectedValue);
            arguments.Add(null);

            if (workerAllRequest.IsBusy == false)
                workerAllRequest.RunWorkerAsync(arguments);

            // create RequestBarChart
            if (workerRequestsInfos.IsBusy == false)
                workerRequestsInfos.RunWorkerAsync(arguments);


            // create CancelationBarChart
            if (workerRequestsInfosForRejection.IsBusy == false)
                workerRequestsInfosForRejection.RunWorkerAsync(arguments);

            // create RejectionBarChart
            if (workerRequestsInfosForCancelation.IsBusy == false)
                workerRequestsInfosForCancelation.RunWorkerAsync(arguments);


        }

        private void RequestCounter()
        {

             int RecordedRequestToday   = 0;
             int FinishedRequestToday   = 0;
             int OpenRequestToday       = 0;
             int RejectedRequestToday   = 0;
             int CanceledRequestToday   = 0;
             int WatingListRequestToday = 0;
             int RecordedRequest        = 0;
             int RejectedRequest        = 0;
             int CanceledRequest        = 0;
             int WatingListRequest      = 0;

             RequestDB.GetRequestByInsertDate(RequestCityComboBox.SelectedIDs,
                                              RequestCenterComboBox.SelectedIDs,
                                              RequestFromDateTextBox.SelectedDate,
                                              RequestToDateTextBox.SelectedDate,
                                              (int?)RequestRequestComboBox.SelectedValue,
                                     out RecordedRequestToday,
                                     out FinishedRequestToday,
                                     out OpenRequestToday,
                                     out RejectedRequestToday,
                                     out CanceledRequestToday,
                                     out WatingListRequestToday,
                                     out RecordedRequest,
                                     out RejectedRequest,
                                     out CanceledRequest,
                                     out WatingListRequest
                                     );

            // Set Values SevenSegments
             CountersRecordedRequestToday.Value = RecordedRequestToday;
             CountersFinishedRequestToday.Value = FinishedRequestToday;
            CountersOpenRequestToday.Value = OpenRequestToday;
            CountersRejectedRequestToday.Value = RejectedRequestToday;
            CountersCanceledRequestToday.Value = CanceledRequestToday;
            CountersWatingListRequestToday.Value = WatingListRequestToday;
            CountersRecordedRequest.Value = RecordedRequest;
            CountersRejectedRequest.Value = RejectedRequest;
            CountersCanceledRequest.Value = CanceledRequest;
            CountersWatingListRequest.Value = WatingListRequest;
        }

        private void workerAllRequest_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> arguments = e.Argument as List<object>;
            _RequestsInfos = RequestDB.GetAllRequestInfo(false, null, (List<int>)arguments[0], (List<int>)arguments[1], (DateTime?)arguments[2], (DateTime?)arguments[3], null, (TimeSpan?)arguments[5]);

        }

        private void workerAllRequest_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            piePlotter.DataContext = new MainViewModel(_RequestsInfos.GroupBy(t => t.RequestTitle).Select(t => new ItemChartClass {  Category = t.Key.ToString(),  Number = t.Sum(s => s.RequestDetails.Count) }).OrderBy(t => t.Number).ToList());

            RequestSearch.Content = "جستجو";
            RequestSearch.IsEnabled = true;
        }
        private void RequestCityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            RequestCenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(RequestCityComboBox.SelectedIDs);
        }
        private void RequestCenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
        private void RequestRequestComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RequestRequestComboBox.SelectedItem != null)
            {
                request = RequestRequestComboBox.SelectedItem as CheckableItemNullable;
            }
        }
        //RequestsInfos
        private void workerRequestsInfos_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> arguments = e.Argument as List<object>;
            _RequestsInfosDetails = RequestDB.GetAllRequestInfo(false, null, (List<int>)arguments[0], (List<int>)arguments[1], (DateTime?)arguments[2], (DateTime?)arguments[3], (int?)arguments[4], (TimeSpan?)arguments[5]);
        }

        private void workerRequestsInfos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RequestBarChart.DataContext = new MainViewModel(_RequestsInfosDetails.Where(t => t.RequestTitle == request.Name).Select(t => new ItemChartClass { Category = t.RequestDetails.StepTitle, Number = t.RequestDetails.Count }).ToList());
        }

        //RequestsInfosForCancelation
        private void workerRequestsInfosForCancelation_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> arguments = e.Argument as List<object>;
            requestsInfosForCancelationPieChart = RequestDB.GetAllRequestInfo(true, null, (List<int>)arguments[0], (List<int>)arguments[1], (DateTime?)arguments[2], (DateTime?)arguments[3], null, (TimeSpan?)arguments[5]);

        }

        private void workerRequestsInfosForCancelation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CancelationBarChart.DataContext = new MainViewModel(requestsInfosForCancelationPieChart.GroupBy(t => t.RequestTitle).Select(t => new ItemChartClass { Category = t.Key.ToString(), Number = t.Sum(s => s.RequestDetails.Count) }).OrderBy(t => t.Number).ToList());
        }

        //RequestsInfosForRejection
        private void workerRequestsInfosForRejection_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> arguments = e.Argument as List<object>;
            requestsInfosForRejectionPieChart = RequestDB.GetAllRequestInfo(false, DB.Action.Reject, (List<int>)arguments[0], (List<int>)arguments[1], (DateTime?)arguments[2], (DateTime?)arguments[3], (int?)arguments[4], (TimeSpan?)arguments[5]);
        }

        private void workerRequestsInfosForRejection_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RejectionBarChart.DataContext = new MainViewModel(requestsInfosForRejectionPieChart.Where(t => t.RequestTitle == request.Name).Select(t => new ItemChartClass {  Category = t.RequestDetails.StepTitle,  Number = t.RequestDetails.Count }).ToList());
        }
        #endregion

        private void RequestWaitTimeCityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            RequestWaitTimeCenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(RequestWaitTimeCityComboBox.SelectedIDs);
        }

        private void RequestWaitTimeCenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region WaitTime

        private void workerRequestsTime_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> arguments = e.Argument as List<object>;
            _RequestsTime = RequestDB.GetRequestTime((List<int>)arguments[0], (List<int>)arguments[1], (DateTime?)arguments[2], (DateTime?)arguments[3], (int?)arguments[4], (TimeSpan?)arguments[5]);

        }

        private void workerRequestsTime_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            RoutineGrid.ItemsSource = _RequestsTime;
            NoWaitTimeTextBox.Text = _RequestsTime.Count().ToString();

            WaitTimeSearch.Content = "جستجو";
            WaitTimeSearch.IsEnabled = true;

        }

        private void WaitTimeSearch_Click(object sender, RoutedEventArgs e)
        {
            if (workerRequestsTime.IsBusy == false)
            {
                WaitTimeSearch.Content = "در حال جستجو";
                WaitTimeSearch.IsEnabled = false;
                List<object> arguments = new List<object>();
                arguments.Add((List<int>)WaitTimeCityComboBox.SelectedIDs);
                arguments.Add((List<int>)WaitTimeCenterComboBox.SelectedIDs);
                arguments.Add(WaitTimeFromDateTextBox.SelectedDate);
                arguments.Add(WaitTimeToDateTextBox.SelectedDate);
                arguments.Add((int?)WaitTimeRequestComboBox.SelectedValue);

                if (TimeTypeComboBox.SelectedValue != null)
                {
                    TimeSpan timeSpan = new TimeSpan();
                    double time = MaxTimeTextBox.Text.Trim() != string.Empty ? Convert.ToInt64(MaxTimeTextBox.Text.Trim()) : 0;
                    if (Convert.ToInt16(TimeTypeComboBox.SelectedValue) == (byte)DB.TimeType.Hour)
                    {
                        timeSpan = TimeSpan.FromHours(time);
                    }
                    else if (Convert.ToInt16(TimeTypeComboBox.SelectedValue) == (byte)DB.TimeType.Day)
                    {
                        timeSpan = TimeSpan.FromDays(time);
                    }
                    else if (Convert.ToInt16(TimeTypeComboBox.SelectedValue) == (byte)DB.TimeType.Month)
                    {
                        timeSpan = TimeSpan.FromDays(time * 30);
                    }

                    arguments.Add(timeSpan);
                }
                else
                {
                    arguments.Add(null);
                }

                workerRequestsTime.RunWorkerAsync(arguments);
            }
        }

        private void WaitTimeCityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            WaitTimeCenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(WaitTimeCityComboBox.SelectedIDs);
        }
        private void WaitTimeCenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
        #endregion WaitTime

        #region RequestWaitTime

        private void workerRequestWaitTimeInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> arguments = e.Argument as List<object>;
            _RequestWaitTimeInfo = RequestDB.GetRequestWaitTime((List<int>)arguments[0], (List<int>)arguments[1], (DateTime?)arguments[2], (DateTime?)arguments[3], (int?)arguments[4], (TimeSpan?)arguments[5]);
        }

        private void workerRequestWaitTimeInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RequestWaitTimeGrid.ItemsSource = _RequestWaitTimeInfo;
            NoRequestWaitTimeTextBox.Text = _RequestWaitTimeInfo.Count().ToString();
            RequestWaitTimeSearch.Content = "جستجو";
            RequestWaitTimeSearch.IsEnabled = true;

        }

       
        private void RequestWaitTimeSearch_Click(object sender, RoutedEventArgs e)
        {
            if (workerRequestWaitTimeInfo.IsBusy == false)
            {
                RequestWaitTimeSearch.Content = "در حال جستجو";
                RequestWaitTimeSearch.IsEnabled = false;
                List<object> arguments = new List<object>();
                arguments.Add((List<int>)RequestWaitTimeCityComboBox.SelectedIDs);
                arguments.Add((List<int>)RequestWaitTimeCenterComboBox.SelectedIDs);
                arguments.Add(RequestWaitTimeFromDateTextBox.SelectedDate);
                arguments.Add(RequestWaitTimeToDateTextBox.SelectedDate);
                arguments.Add((int?)RequestWaitTimeRequestComboBox.SelectedValue);

                if (RequestWaitTimeTypeComboBox.SelectedValue != null)
                {
                    TimeSpan timeSpan = new TimeSpan();
                    double time = RequestWaitTimeTextBox.Text.Trim() != string.Empty ? Convert.ToInt64(RequestWaitTimeTextBox.Text.Trim()) : 0;
                    if (Convert.ToInt16(RequestWaitTimeTypeComboBox.SelectedValue) == (byte)DB.TimeType.Hour)
                    {
                        timeSpan = TimeSpan.FromHours(time);
                    }
                    else if (Convert.ToInt16(RequestWaitTimeTypeComboBox.SelectedValue) == (byte)DB.TimeType.Day)
                    {
                        timeSpan = TimeSpan.FromDays(time);
                    }
                    else if (Convert.ToInt16(RequestWaitTimeTypeComboBox.SelectedValue) == (byte)DB.TimeType.Month)
                    {
                        timeSpan = TimeSpan.FromDays(time * 30);
                    }

                    arguments.Add(timeSpan);
                }
                else
                {
                    arguments.Add(null);
                }

                workerRequestWaitTimeInfo.RunWorkerAsync(arguments);
            }

        }

        #endregion reqeustWateTime

        private void RequestWaitTimeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void MaxTimeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

    }
}

