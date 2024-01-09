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
using CookComputing.XmlRpc;
using CRM.Data.Services;

namespace CRMConvertApplication
{
    /// <summary>
    /// Interaction logic for ADSLServiceForm.xaml
    /// </summary>
    public partial class ADSLServiceForm : Window
    {
        public ADSLServiceForm()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }

        #region SaveItem

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            GroupNameLabel.Content = GetGroupComment(IBSTitleTextBox.Text.Trim());

            SuccessLabel.Visibility = Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ADSLService service = ADSLServiceDB.GetADSLServiecNamebyIBSngName(IBSTitleTextBox.Text.Trim());

            if (service != null)
            {
                service.Title = GroupNameLabel.Content.ToString();

                service.Detach();
                DB.Save(service);
            }

            SuccessLabel.Visibility = Visibility.Visible;
        }

        private string GetGroupComment(string GroupName)
        {
            //try
            //{
            //    IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            //    XmlRpcStruct arguments = new XmlRpcStruct();
            //    XmlRpcStruct conds = new XmlRpcStruct();
            //    arguments.Add("auth_name", "pendar");
            //    arguments.Add("auth_pass", "Pendar#!$^");
            //    arguments.Add("auth_type", "ADMIN");

            //    arguments.Add("group_name", GroupName);

            //    XmlRpcStruct result = ibsngService.GetGroupInfo(arguments);
            //    string comment = result["comment"].ToString();

            //    return comment;

            //}
            //catch (Exception ex)
            //{
            //    return "";
            //    throw ex;
            //}

            return "";
        }
        
        #endregion

        #region SavePrice

        private void SaveCostButton_Click(object sender, RoutedEventArgs e)
        {
            ADSLService service = ADSLServiceDB.GetADSLServiecNamebyIBSngName(IBSTextBox.Text);

            if (service != null)
            {
                service.Price = Convert.ToInt64(CostTextBox.Text.Trim());
                service.PriceSum = service.Price;

                service.Detach();
                DB.Save(service);
            }

            CostTextBox.Text = string.Empty;
            DurationTextBox.Text = string.Empty;
            GhestTextBox.Text = string.Empty;
            PriceTextBox.Text = string.Empty;
        }

        private void SaveInstalmentButton_Click(object sender, RoutedEventArgs e)
        {
            ADSLService service = ADSLServiceDB.GetADSLServiecNamebyIBSngName(IBSTextBox.Text);

            if (service != null)
            {
                DurationTextBox.Text = service.DurationID.ToString();

                service.Price = Convert.ToInt64(service.DurationID) * Convert.ToInt64(GhestTextBox.Text);
                service.PriceSum = service.Price;
                service.IsInstalment = true;
                service.MAXInstallmentCount = service.DurationID;

                service.Detach();
                DB.Save(service);
            }

            CostTextBox.Text = string.Empty;
            DurationTextBox.Text = string.Empty;
            GhestTextBox.Text = string.Empty;
            PriceTextBox.Text = string.Empty;
        }
        
        #endregion
    }
}
