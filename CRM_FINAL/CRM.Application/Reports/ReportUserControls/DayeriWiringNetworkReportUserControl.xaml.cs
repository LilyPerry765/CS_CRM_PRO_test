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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ResignationLinesStatisticReportUserControl.xaml
    /// </summary>
    public partial class DayeriWiringNetworkReportUserControl : Local.ReportBase
    {
        public DayeriWiringNetworkReportUserControl()
        {
            InitializeComponent();
            Initialize();

        }
        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CityCenterUC.CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
                ReasonTypeCkeckableComboBox.ItemsSource = WaitingListReasonDB.GetWaitingListReasonCheckableByRequestTypeID();
            }
        }
        


    }
}
