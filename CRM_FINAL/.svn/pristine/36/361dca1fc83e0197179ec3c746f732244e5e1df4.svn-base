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
using CRM.Data;
namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TEST.xaml
    /// </summary>
    public partial class TEST : Window
    {
        public TEST()
        {
            InitializeComponent();           
           
           
            RequestInfo = new Request();
            InstallInfo = new InstallRequest();
     
            this.B.DataContext = RequestInfo;
            this.C.DataContext = InstallInfo;
        }
        public Request RequestInfo { get; set; }
        public InstallRequest InstallInfo { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RequestInfo.RequestDate = DB.GetServerDate();
            RequestInfo.RepresentitiveNo = "123uu";

            DB.Save(RequestInfo);
            DB.Save(InstallInfo);
            
                     

        }
    }
}
