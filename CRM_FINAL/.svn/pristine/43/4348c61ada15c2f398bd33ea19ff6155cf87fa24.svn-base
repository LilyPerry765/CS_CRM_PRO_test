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
    public partial class CauseOfChangeNoForm : Local.PopupWindow
    {
        private int _ID = 0;

        public CauseOfChangeNoForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CauseOfChangeNoForm(int id)
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
            CauseOfChangeNo item = new CauseOfChangeNo();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.CauseOfChangeNoDB.GetCauseOfChangeNoID(_ID);

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
                CauseOfChangeNo item = this.DataContext as CauseOfChangeNo;

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
