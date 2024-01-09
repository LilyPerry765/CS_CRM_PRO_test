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
    /// Interaction logic for IbsngForm.xaml
    /// </summary>
    public partial class IbsngForm : Window
    {
        public IbsngForm()
        {
            InitializeComponent();


        }

        private void ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeService();
        }

        public void ChangeService()
        {
            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            userAuthentication.Clear();

            userAuthentication.Add("user_id", "37559");

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            userAuthentication.Add("deposit", "3072");
            userAuthentication.Add("is_absolute_change", false);
            userAuthentication.Add("deposit_type", "renew");
            userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Extension Service Request (Renew)");

            ibsngService.changeDeposit(userAuthentication);

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            userAuthentication.Add("user_id", "37559");

            userInfo.Add("renew_next_group", "PRA-512K-3M-3G");
            userInfo.Add("renew_remove_user_exp_dates", "1");

            userAuthentication.Add("attrs", userInfo);
            userAuthentication.Add("to_del_attrs", "");

            ibsngService.UpdateUserAttrs(userAuthentication);
        }

        private void TrafficButton_Click(object sender, RoutedEventArgs e)
        {
            SellTraffic();
        }

        private void SellTraffic()
        {
            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            userAuthentication.Clear();

            userAuthentication.Add("user_id", "6393");
            userAuthentication.Add("deposit", "3072");
            userAuthentication.Add("is_absolute_change", false);
            userAuthentication.Add("deposit_type", "recharge");
            userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Sell Traffice Request (Recharge)");

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            try
            {
                ibsngService.changeDeposit(userAuthentication);
            }
            catch (Exception ex)
            {
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUser();
        }

        private void CreateUser()
        {
            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();
            object[] ids = new object[1];
            bool isAddable = false;

            userAuthentication.Clear();
            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            try
            {
                userAuthentication.Add("normal_username", "2313388246");
                userInfos = ibsngService.GetUserInfo(userAuthentication);
            }
            catch (Exception ex)
            {
                isAddable = true;
            }

            if (isAddable)
            {
                userAuthentication.Clear();

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                userAuthentication.Add("count", 1);
                userAuthentication.Add("credit", "12288");
                userAuthentication.Add("isp_name", "Main");
                userAuthentication.Add("group_name", "OSS-1024K-12M-12G");
                userAuthentication.Add("credit_comment", "");

                ids = ibsngService.AddNewUsers(userAuthentication);
                if (ids.Count() == 0)
                    throw new Exception("ذخیره کاربر در سیستم IBSng با مشکل مواجه شد");
            }
            else
            {
                ids[0] = "1";

                userAuthentication.Clear();

                userAuthentication.Add("auth_name", "pendar");
                userAuthentication.Add("auth_pass", "Pendar#!$^");
                userAuthentication.Add("auth_type", "ADMIN");

                XmlRpcStruct list = new XmlRpcStruct();
                list.Add("to_del_attrs", "lock");

                userAuthentication.Add("user_id", ids[0].ToString());
                userAuthentication.Add("attrs", userInfo);
                userAuthentication.Add("to_del_attrs", list);

                ibsngService.UpdateUserAttrs(userAuthentication);
            }

            XmlRpcStruct dictionary = new XmlRpcStruct();

            dictionary.Add("normal_username", "2313388246");
            dictionary.Add("normal_password", "");


            userInfo.Add("normal_user_spec", dictionary);

            userInfo.Add("normal_password_bind_on_login", "");

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            userAuthentication.Add("user_id", ids[0].ToString());
            userAuthentication.Add("attrs", userInfo);
            userAuthentication.Add("to_del_attrs", "");

            ibsngService.UpdateUserAttrs(userAuthentication);

            userAuthentication.Clear();

            userAuthentication.Add("user_id", ids[0].ToString());
            userAuthentication.Add("deposit", "12288");
            userAuthentication.Add("is_absolute_change", false);
            userAuthentication.Add("deposit_type", "recharge");
            userAuthentication.Add("deposit_comment", "Change by Pendar_CRM Sell ADSL Request");

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            try
            {
                ibsngService.changeDeposit(userAuthentication);
            }
            catch (Exception ex)
            { }
        }
    }
}
