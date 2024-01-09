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

    public partial class DashboardCounter : UserControl
    {
        private ImageBrush _BackgroundImageHeader = new ImageBrush(Helper.GetBitmapImage("Counter_Grid_Header_01.png"));
        private ImageBrush _BackgroundImageFooter = new ImageBrush(Helper.GetBitmapImage("Counter_Grid_Footer_01.png"));
        public string Header
        {
            set
            {
                HeaderTextBox.Text = value;
            }
            get
            {
                return HeaderTextBox.Text;
            }

        }
        public int RequestCount
        {
            set
            {
                RequestCountTextBox.Text = value.ToString();
            }
        }
        public int ColorNumber { get; set; }

        public DashboardCounter()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ColorNumber == 0) ColorNumber = 1;
            _BackgroundImageHeader = new ImageBrush(Helper.GetBitmapImage("Counter_Grid_Header_0" + (((int)ColorNumber % 6) + 1) + ".png"));
            _BackgroundImageFooter = new ImageBrush(Helper.GetBitmapImage("Counter_Grid_Footer_0" + (((int)ColorNumber % 6) + 1) + ".png"));

            _BackgroundImageHeader.Opacity = 0.8;
            _BackgroundImageFooter.Opacity = 0.8;
            GridHeaderBorder.Background = _BackgroundImageHeader;
            GridFooterBorder.Background = _BackgroundImageFooter;
        }
    }
}
