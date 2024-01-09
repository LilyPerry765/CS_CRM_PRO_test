using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
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
    /// Interaction logic for ApplicationConfig.xaml
    /// </summary>
    public partial class ApplicationConfigForm : Local.PopupWindow
    {
        Setting RequestValidTimeSettring;
        Setting ApplyCabinetShareSettring;
        public ApplicationConfigForm()
        {
            InitializeComponent();
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            RequestValidTimeSettring = Data.SettingDB.GetSettingByKey("RequestValidTime");
            ApplyCabinetShareSettring = Data.SettingDB.GetSettingByKey("ApplyCabinetShare");

            RequestValidTimeTextBox.Text = RequestValidTimeSettring.Value;
            ApplyCabinetShareTextBox.Text = ApplyCabinetShareSettring.Value;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    /// requestValidTime
                    int requestValidTime = 0;
                    int.TryParse(RequestValidTimeTextBox.Text.Trim(), out requestValidTime);

                    if (requestValidTime == 0)
                        throw new Exception("مقدار زمان اعتبار درخواست (ماه) صحیح نمی باشد");

                    RequestValidTimeSettring.Value = requestValidTime.ToString();
                    RequestValidTimeSettring.Detach();
                    DB.Save(RequestValidTimeSettring, false);


                    /// ApplyCabinetShare
                    int ApplyCabinetShare = 0;
                    int.TryParse(ApplyCabinetShareTextBox.Text.Trim(), out ApplyCabinetShare);

                    if (ApplyCabinetShare == 0)
                        throw new Exception("مقدار سهمیه کافو صحیح نمی باشد");

                    ApplyCabinetShareSettring.Value = ApplyCabinetShare.ToString();
                    ApplyCabinetShareSettring.Detach();
                    DB.Save(ApplyCabinetShareSettring, false);

                    ///
                    ts.Complete();
                }
                ShowSuccessMessage("ذخیره تنظیمات انجام شد");

            }
            catch(Exception ex)
            {
                ShowErrorMessage("خطا در دخیره اطلاعات" , ex);
            }

        }

    }
}
