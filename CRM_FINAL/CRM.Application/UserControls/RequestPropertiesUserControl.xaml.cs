using System;
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
using CRM.Data;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for RequestPeopertiesUserControl.xaml
    /// </summary>
    public partial class RequestPropertiesUserControl : UserControl
    {
        public RequestPropertiesUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            }
        }
        private void CityComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterCheckableComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds((sender as CheckableComboBox).SelectedIDs);

        }
    }
}
