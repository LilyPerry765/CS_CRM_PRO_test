using CRM.Data;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ChangeGSMSimCardForm.xaml
    /// </summary>
    public partial class ChangeGSMSimCardForm : Local.PopupWindow
    {
        GSMSimCard GSMSimCardItem;
        Telephone  telephone;
        public ChangeGSMSimCardForm()
        {
            InitializeComponent();
        }
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void SaveForm(object sender, RoutedEventArgs e)
        {
            if (telephone != null)
            {
                if (GSMSimCardItem != null && GSMSimCardItem.TelephoneNo != 0)
                {
                    GSMSimCardItem.Code = GSMCodeTextBox.Text;
                    GSMSimCardItem.Detach();
                    DB.Save(GSMSimCardItem, false);
                }
                else
                {
                    GSMSimCardItem = new GSMSimCard();
                    GSMSimCardItem.TelephoneNo = telephone.TelephoneNo;
                    GSMSimCardItem.Code = GSMCodeTextBox.Text.Trim();
                    DB.Save(GSMSimCardItem, true);
                }
            }
            else
            {
                MessageBox.Show("لطفا تلفن را صحیح را وارد کنید.");
            }
        }

        private void TelephoneSearchButton_Click(object sender, RoutedEventArgs e)
        {
            telephone = TelephoneDB.GetTelephoneByTelephoneNo(Convert.ToInt64(TelephoneTextBox.Text.Trim()));

            if (telephone != null)
            {


                GSMSimCardItem = GSMSimCardDB.GetGSMSimCardByTelephone(telephone.TelephoneNo);

                if (GSMSimCardItem != null)
                {
                    GSMCodeTextBox.Text = GSMSimCardItem.Code;
                    GSMSimCardItem.Detach();
                    DB.Save(GSMSimCardItem, false);
                }
            }
            else
            {
                MessageBox.Show("لطفا تلفن را صحیح را وارد کنید.");
            }
        }
    }
}
