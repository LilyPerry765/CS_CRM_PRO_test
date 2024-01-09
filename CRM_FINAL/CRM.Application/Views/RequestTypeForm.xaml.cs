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

    public partial class RequestTypeForm : Local.PopupWindow
    {
        private int _id = 0;

        public RequestTypeForm()
        {
            InitializeComponent();
        }

        public RequestTypeForm(int id):this()
        {
            _id = id;
        }

       
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
           Save();
        }

        private void Save()
        {
            RequestType requestType = new RequestType();

            if (requestTitleTextBox.Text.Trim() != string.Empty)
            {
                requestType.Title = requestTitleTextBox.Text;

                if ((bool)isSpecialService.IsChecked)
                    requestType.IsSpecialService =true;
                else
                    requestType.IsSpecialService = false;

                requestType.InsertDate = DB.GetServerDate();

                Save(requestType);
                MessageBox.Show("با موفقیت ذخیره شد");
                requestTitleTextBox.Text = string.Empty;
                isSpecialService.IsChecked = false;
                workUnitCheckBox.ItemsSource = null;
               



            }
            else
            {
                MessageBox.Show("مقدار وارد شده برای عنوان معتبر نمی باشد");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           LoadData();
        }

        private void LoadData()
        {
             RequestType requestType ;
             _id = 11;
                      
            if (_id == 0)
            {
                 requestType = new RequestType();
                saveButton.Content = "ذخیره";
                Title = "ثبت انواع درخواستها";

                workUnitCheckBox.ItemsSource = DB.GetAllEntity<WorkUnit>().Select(item => new CheckableItem()
                {
                    ID = item.ID,
                    Name = item.WorkUnitName,
                    IsChecked = false
                }).ToList();
               
            }
            else
            {
                saveButton.Content = "بروز رسانی";
                Title = "ویرایش انواع درخواستها";
                requestType = DB.GetEntitybyIntID<RequestType>(_id);
                requestTitleTextBox.Text = requestType.Title;

                if (requestType.IsSpecialService.HasValue)

                   isSpecialService.IsChecked =requestType.IsSpecialService;
                     

                workUnitCheckBox.ItemsSource = DB.GetAllEntity<WorkUnit>();        



            }
        }


    
      
    }
}
