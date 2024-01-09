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
using System.Collections;
using System.ComponentModel;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for CityCenterUserControl.xaml
    /// </summary>
    public partial class CityCenterUserControl : UserControl
    {
        private double _CityTextBlockWidth = 0;
        private double _CityComboBoxWidth = 0;
        private double _CenterTextBlockWidth = 0;
        private double _CenterComboBoxWidth = 0;

        public double CityTextBlockWidth { get { return _CityTextBlockWidth; } set { _CityTextBlockWidth = value; } }
        public double CityComboBoxWidth { get { return _CityComboBoxWidth; } set { _CityComboBoxWidth = value; } }
        public double CenterTextBlockWidth { get { return _CenterTextBlockWidth; } set { _CenterTextBlockWidth = value; } }
        public double CenterComboBoxWidth { get { return _CenterComboBoxWidth; } set { _CenterComboBoxWidth = value; } }

        //This Enum type is defined for using in orientation dependency property
        public enum State
        {
            Vertical, Horizontal
        }

        //This Property is used to set Orientation of CityCenterUserControl.
        public State Orientation
        {
            get { return (State)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(State), typeof(CityCenterUserControl), new UIPropertyMetadata(State.Vertical));

        public void Reset()
        {

            CenterComboBox.SelectedIndex = -1;
            CityComboBox.SelectedIndex = -1;
        }

        public List<int> SelectedCenterIDs
        {
            get
            {
                return CenterComboBox.SelectedIDs;
            }
        }

        public IEnumerable CityItemSource
        {
            set
            {
                CityComboBox.ItemsSource = value;
            }
        }

        public CityCenterUserControl()
        {
            InitializeComponent();
            CityComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ItemsComboBox_DropDownClosed);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this) && CityComboBox.Items.Count == 0)
            {
                CityItemSource = Data.CityDB.GetAvailableCityCheckable();
            }

            if (Orientation.ToString() == State.Horizontal.ToString())
            {
                Grid.SetRow(CenterTextBlock, 0);
                Grid.SetRow(CenterComboBox, 0);
                Grid.SetColumn(CenterTextBlock, 3);
                Grid.SetColumn(CenterComboBox, 4);
            }
            else
            {
                if (_CityTextBlockWidth != 0)
                    CityTextBlock.Width = _CityTextBlockWidth;

                if (_CityComboBoxWidth != 0)
                    CityComboBox.Width = _CityComboBoxWidth;


                if (_CenterTextBlockWidth != 0)
                    CenterTextBlock.Width = _CenterTextBlockWidth;

                if (_CenterComboBoxWidth != 0)
                    CenterComboBox.Width = _CityComboBoxWidth;
            }
        }

        void ItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (CityComboBox.SelectedIDs.Count > 0)
                AllChecked = true;

            CenterComboBox.ItemsSource = Data.CenterDB.GetCentersCheckable(AllChecked, CityComboBox.SelectedIDs);
        }
    }
}
