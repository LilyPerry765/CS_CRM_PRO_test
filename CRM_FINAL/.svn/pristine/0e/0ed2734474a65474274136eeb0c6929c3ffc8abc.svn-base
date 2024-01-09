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
using System.ComponentModel;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for RegionCenterUserControl.xaml
    /// </summary>
    public partial class RegionCenterUserControl : UserControl
    {
        #region Constructor

        public RegionCenterUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion  Constructor

        #region Initializer

        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                RegionComboBox.ItemsSource = RegionDB.GetRegions();
            }
        }

        #endregion Initializer

        #region Event Handlers
        private void RegionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterByCityId((int)(sender as ComboBox).SelectedValue);
        }

        #endregion  Event Handlers
       
    }
}
