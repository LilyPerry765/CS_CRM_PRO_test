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
    public partial class ADSLServiceNetworkForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLServiceNetworkForm()
        {
            InitializeComponent();
        }

        public ADSLServiceNetworkForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            ADSLServiceNetwork aDSLServiceNetwork = new ADSLServiceNetwork();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                aDSLServiceNetwork = ADSLServiceDB.GetADSLServiceNetworkByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = aDSLServiceNetwork;
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
                ADSLServiceNetwork aDSLServiceNetwork = this.DataContext as ADSLServiceNetwork;

                aDSLServiceNetwork.Detach();
                Save(aDSLServiceNetwork);
                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره نوع شبکه ADSL", ex);
            }
        }

        #endregion
    }
}
