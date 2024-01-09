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
    public partial class ADSLSetServiceForm : Local.PopupWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public ADSLSetServiceForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            TelephoneNoTextBox.Focus();
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string telephoneNo =TelephoneNoTextBox.Text.Trim();

            CRMWebService.IbsngInputInfo ibsngInputInfo = new CRMWebService.IbsngInputInfo();
            CRMWebService.IBSngUserInfo ibsngUserInfo = new CRMWebService.IBSngUserInfo();
            CRMWebService.CRMWebService webService = new CRMWebService.CRMWebService();

            ibsngInputInfo.NormalUsername = telephoneNo;
            ibsngUserInfo = webService.GetUserInfo(ibsngInputInfo);

            if (ibsngUserInfo != null)
            {
                if (!string.IsNullOrWhiteSpace(ibsngUserInfo.IBSngGroupName))
                {
                    IBSngNameTextBox.Text = ibsngUserInfo.IBSngGroupName;

                    List<ADSLService> serviceList = ADSLServiceDB.GetADSLServicebyIBSngName(ibsngUserInfo.IBSngGroupName);

                    if (serviceList != null && serviceList.Count != 0)
                        ItemsDataGrid.ItemsSource = serviceList;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLService item = ItemsDataGrid.SelectedItem as ADSLService;

                if (item == null)
                    return;                

                ADSL aDSl = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(TelephoneNoTextBox.Text.Trim()));

                aDSl.TariffID = item.ID;

                aDSl.Detach();
                DB.Save(aDSl);
            }
        }

        private void TelephoneNoTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {            
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion
    }
}
