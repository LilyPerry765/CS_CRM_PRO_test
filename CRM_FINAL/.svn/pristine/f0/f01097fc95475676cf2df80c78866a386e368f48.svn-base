using CRM.Data;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for Charts.xaml
    /// </summary>
    public partial class Charts : Local.TabWindow
    {
        List<RequestTitleInfo> _RequestsInfos;
        DateTime? fromDate = new DateTime();
        DateTime? toDate = new DateTime();
        public Charts()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ChartListBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Charts));

        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ChartListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dictionary<string, int> dictionaryRequestColumnChart = new Dictionary<string, int>();
            _RequestsInfos = RequestDB.GetRequestsStepsGroupInfo(false, false, null, null, null, true).ToList(); 
            dictionaryRequestColumnChart = _RequestsInfos.GroupBy(t => t.RequestTitle).ToDictionary(t => t.Key.ToString(), t => t.Sum(s => s.RequestDetails.Count));
            ChartColumnSeries.ItemsSource = dictionaryRequestColumnChart;

        }
    }
}
