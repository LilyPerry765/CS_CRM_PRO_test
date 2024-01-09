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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CRMConvertApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Center center { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            TelephoneNoTextBox.Focus();
        }

        private void Click_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Service1 aDSLService = new Service1();

                System.Data.DataTable telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", TelephoneNoTextBox.Text.Trim().ToString());
                string centerCode = telephoneInfo.Rows[0]["CENTERCODE"].ToString();
                CenterCodeLabel.Content = "Center Code in Elka : " + centerCode;
                center = CenterDB.GetCenterByCenterCode(Convert.ToInt32(centerCode));

                if (center != null)
                {
                    CenterIDLabel.Content = "CenterID in CRM : " + center.ID.ToString();
                    CenterNameLabel.Content = "CenterID in CRM : " + center.CenterName;
                }

                ErrorLabel.Content = string.Empty;
                SuccessLabel.Content = string.Empty;
            }
            catch (Exception ex)
            {
                CenterCodeLabel.Content = string.Empty;
                CenterIDLabel.Content = string.Empty;
                CenterNameLabel.Content = string.Empty;
                ErrorLabel.Content = "خطا، " + ex.Message;
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MDF1 mdf = new MDF1();
            mdf.CenterID = center.ID;
            mdf.Type = 0;
            mdf.Number = 1;
            mdf.Description = "ADSl";

            mdf.Detach();
            DB.Save(mdf);

            SuccessLabel.Content = "ذخیره MDF با موفقیت انجام شد.";
        }

        private void CheckCenterButton_Click(object sender, RoutedEventArgs e)
        {
            List<ADSL> aDSlList = ADSLDB.GetADSLList();
            Telephone telephone = null;
            Service1 aDSLService = new Service1();
            System.Data.DataTable telephoneInfo = null;
            int centerID = 0;
            
            foreach (ADSL currentADSL in aDSlList)
            {
                telephone = ADSLDB.GetTelephonebyTelephoneNo(currentADSL.TelephoneNo);

                if (telephone != null)
                {
                    telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", currentADSL.TelephoneNo.ToString());

                    if (telephoneInfo.Rows.Count != 0)
                    {
                        string centerCode = telephoneInfo.Rows[0]["CENTERCODE"].ToString();
                        centerID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(centerCode));

                        if (centerID != 0 && telephone.CenterID != centerID)
                        {
                            telephone.CenterID = centerID;

                            telephone.Detach();
                            DB.Save(telephone);
                        }
                    }
                }
            }

        }
    }
}
