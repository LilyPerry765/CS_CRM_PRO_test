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
    public partial class ADSLChangePortReasonForm : Local.PopupWindow
    {
        #region Properties

        private  long _ID = 0;

        #endregion

        #region Constructors

        public ADSLChangePortReasonForm()
        {
            InitializeComponent();
        }

        public ADSLChangePortReasonForm(long id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            ADSLChangePortReason _ADSLChangePortReason = new ADSLChangePortReason();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                _ADSLChangePortReason = ADSLChangePortDB.GetADSLChangePortReasonByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = _ADSLChangePortReason;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                ADSLChangePortReason _ADSLChangePortReason = this.DataContext as ADSLChangePortReason;

                _ADSLChangePortReason.Detach();
                Save(_ADSLChangePortReason);
                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره علت تعویض پورت ADSL", ex);
            }
        }

        #endregion
    }
}
