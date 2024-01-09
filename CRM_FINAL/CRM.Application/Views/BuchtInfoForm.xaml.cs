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
    /// Interaction logic for BuchtInfoForm.xaml
    /// </summary>
    public partial class BuchtInfoForm : Local.PopupWindow
    {
        long ID = 0;

        public BuchtInfoForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            StatusComboBox.ItemsSource = new List<CheckableItem>
           { 
               new CheckableItem{ ID = (int)DB.BuchtStatus.Destroy , Name =  Helper.GetEnumDescriptionByValue(typeof(DB.BuchtStatus) , (int)DB.BuchtStatus.Destroy) , IsChecked = false } ,
               new CheckableItem{ ID = (int)DB.BuchtStatus.Free , Name =  Helper.GetEnumDescriptionByValue(typeof(DB.BuchtStatus) , (int)DB.BuchtStatus.Free) , IsChecked = false }
           };


        }

        public BuchtInfoForm(long id)
            :this()
        {
            ID = id;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionInfo connectionInfo = DB.GetBuchtInfoByID(ID);

            VerticalRowTextBox.Text = connectionInfo.VerticalRowNo.ToString();
            VerticalTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
            BuchtTextBox.Text = connectionInfo.BuchtNo.ToString();

            if(!(connectionInfo.BuchtStatus == (int)DB.BuchtStatus.Destroy || connectionInfo.BuchtStatus == (int)DB.BuchtStatus.Free))
            {
                StatusComboBox.IsEnabled = false;
            }
            else
            {
                StatusComboBox.SelectedValue = connectionInfo.BuchtStatus;
            }

        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((StatusComboBox.SelectedValue != null) && ((int)StatusComboBox.SelectedValue == (int)DB.BuchtStatus.Destroy || (int)StatusComboBox.SelectedValue == (int)DB.BuchtStatus.Free))
                {

                    int status = (int)StatusComboBox.SelectedValue;
                    Bucht bucht = BuchtDB.GetBuchtByID(ID);
                    bucht.Status = (byte)status;
                    bucht.Detach();
                    DB.Save(bucht);

                    this.DialogResult = true;
                    ShowSuccessMessage("ذخیره انجام شد");
                }
                else
                {
                    this.DialogResult = false;
                    throw new Exception("لطفا وضعیت را انتخاب کنید");
                }
            }
            catch(Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره" , ex);
            }
        }
    }
}
