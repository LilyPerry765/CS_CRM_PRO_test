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
using CRM.Application.Views;
using CRM.Data;

namespace CRM.Application.UserControls
{
    public partial class DashboardCounterDetails : UserControl
    {
        #region Properties

        private ImageBrush _BackgroundImage = new ImageBrush(Helper.GetBitmapImage("Counter_Grid_Middle_01.png"));

        public string Header
        {
            set { HeaderTextBox.Text = value; }
            get { return HeaderTextBox.Text; }
        }

        public int Count
        {
            set { CountTextBox.Text = value.ToString(); }
        }

        public DateTime? LastItemDate
        {
            set { DateTextBox.Text = Helper.GetPersianDate((DateTime?)value, Helper.DateStringType.DateTime).ToString(); }
        }
        public int ColorNumber { get; set; }
        
        #endregion

        #region Constructors

        public DashboardCounterDetails()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
        }

        #endregion

        #region Methods

        #endregion

        #region Event Handlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ColorNumber == 0) ColorNumber = 1;
            _BackgroundImage = new ImageBrush(Helper.GetBitmapImage("Counter_Grid_Middle_0" + (((int)ColorNumber % 6) + 1) + ".png"));
            _BackgroundImage.Opacity = 0.8;
            ContainerBorder.Background = _BackgroundImage;
        }

        private void ContainerBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            (ContainerBorder.Background as ImageBrush).Opacity = 1;
        }

        private void ContainerBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            (ContainerBorder.Background as ImageBrush).Opacity = 0.8;
        }

        #endregion
    }
}
