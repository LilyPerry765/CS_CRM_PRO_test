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
    public partial class CauseOfTakePossessionForm : Local.PopupWindow
    {
        private int _ID = 0;

        public CauseOfTakePossessionForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CauseOfTakePossessionForm(int id)
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
            CauseOfTakePossession item = new CauseOfTakePossession();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.CauseOfTakePossessionDB.GetCauseOfTakePossessionID(_ID);

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
                CauseOfTakePossession item = this.DataContext as CauseOfTakePossession;

                item.Detach();
                DB.Save(item);

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
