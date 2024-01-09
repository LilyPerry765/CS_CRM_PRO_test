using CRM.Application.Local;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for WorkingTelephoneStatisticsBySwitchReportUserControl.xaml
    /// </summary>
    public partial class WorkingTelephoneStatisticsBySwitchReportUserControl : ReportBase
    {
        #region Constructor

        public WorkingTelephoneStatisticsBySwitchReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            SwitchTypesComboBox.ItemsSource = SwitchTypeDB.GetSwitchCheckable();
        }

        #endregion

        #region EventHandlers

        private void CitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CitiesComboBox.SelectedValue != null)
            {
                CentersComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(new List<int> { (int)CitiesComboBox.SelectedValue });
                CentersComboBox.SelectedIndex = 0;
                CentersComboBox.Items.Refresh();
            }
        }

        #endregion
    }
}
