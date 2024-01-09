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

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for CenterUserControl.xaml
    /// </summary>
    public partial class CenterUserControl : UserControl
    {
        public delegate void  CenterComboBoxLostFocus();
        public  event CenterComboBoxLostFocus DoCenterComboBoxLostFocus;
        private void OnDoCenterComboBoxLostFocus()
        {
           if(DoCenterComboBoxLostFocus != null)
                     DoCenterComboBoxLostFocus();
        }
        public CenterUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        public void Reset()
        {
             CityComboBox.Reset();
             CenterComboBox.Reset();
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            OnDoCenterComboBoxLostFocus();
        }
    }
}
