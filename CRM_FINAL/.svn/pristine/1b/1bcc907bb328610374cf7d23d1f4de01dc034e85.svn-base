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
using System.ComponentModel;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class CauseOfBuchtSwitchingForm : Local.PopupWindow
    {
        private int _ID = 0;

        public CauseOfBuchtSwitchingForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CauseOfBuchtSwitchingForm(int id)
            : this()
        {
            _ID = id;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {

        }

        private void LoadData()
        {
            CauseBuchtSwitching item = new CauseBuchtSwitching();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.CauseBuchtSwitchingDB.GetCauseBuchtSwitchingByID(_ID);

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                CauseBuchtSwitching item = this.DataContext as CauseBuchtSwitching;
                if (item.IsReadOnly == true)
                {
                    throw new Exception("این گزینه قابل تغییر نمی باشد");
                }
                else
                {

                    if (item.ID == 0)
                        item.IsReadOnly = false;

                    item.Detach();
                    DB.Save(item);
                }

                ShowSuccessMessage("علت تعویض شماره");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("علت تعویض شماره", ex);
            }
        }
    }
}
