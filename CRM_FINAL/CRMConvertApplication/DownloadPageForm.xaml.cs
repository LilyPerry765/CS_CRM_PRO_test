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
using System.Net;

namespace CRMConvertApplication
{
    /// <summary>
    /// Interaction logic for DownloadPageForm.xaml
    /// </summary>
    public partial class DownloadPageForm : Window
    {
        public DownloadPageForm()
        {
            InitializeComponent();

            WebClient Client = new WebClient();
            Client.DownloadFile("http://78.39.252.109:81/images/3.jpg", @"D:\117\3.png");
        }
    }
}
