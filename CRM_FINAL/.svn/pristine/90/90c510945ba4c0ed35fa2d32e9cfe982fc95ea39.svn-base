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
using CRM.Data.Services;
using CookComputing.XmlRpc;

namespace CRMConvertApplication
{
    /// <summary>
    /// Interaction logic for TestApp.xaml
    /// </summary>
    public partial class TestApp : Window
    {
        public TestApp()
        {
            InitializeComponent();
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            ////// Delete Credit

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            userAuthentication.Add("user_id", "35982");

            userAuthentication.Add("credit", "0");
            userAuthentication.Add("is_absolute_change", true);            
            userAuthentication.Add("credit_comment", "Change by Pendar_CRM, Change Service Request");

            ibsngService.changeCredit(userAuthentication);

            ////// Delete Recharge Desposit

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            userAuthentication.Add("user_id", "35982");

            userAuthentication.Add("deposit", "1024");
            userAuthentication.Add("is_absolute_change", true);
            userAuthentication.Add("deposit_type", "recharge");
            userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Extension Service Request (Renew)");

            ibsngService.changeDeposit(userAuthentication);

            ////// Delete Renew Desposit

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            userAuthentication.Add("user_id", "35982");

            userAuthentication.Add("deposit", "49152");
            userAuthentication.Add("is_absolute_change", true);
            userAuthentication.Add("deposit_type", "renew");
            userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Extension Service Request (Renew)");

            try
            {
                ibsngService.changeDeposit(userAuthentication);
            }
            catch (Exception)
            {
                throw new Exception("تغییر اعتبار با موفقیت انجام نشد");
            }
        }
    }
}
