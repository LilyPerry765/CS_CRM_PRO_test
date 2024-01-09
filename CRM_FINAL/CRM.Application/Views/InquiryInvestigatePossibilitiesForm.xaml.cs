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
    /// Interaction logic for InquiryInvestigatePossibilitiesForm.xaml
    /// </summary>
    public partial class InquiryInvestigatePossibilitiesForm : Local.PopupWindow
    {
        private string Telephone = string.Empty;

        public InquiryInvestigatePossibilitiesForm()
        {
            InitializeComponent();
        }

        public InquiryInvestigatePossibilitiesForm(string Telephone)
        {
            InitializeComponent();
            this.Telephone = Telephone; 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TelephoneTextBox.Text = Telephone;
            TelephoneSearchButton_Click(null, null);
        }

        private void TelephoneSearchButton_Click(object sender, RoutedEventArgs e)
        {

            long teleohone = 0 ;

            Telephone = TelephoneTextBox.Text.Trim();
            if( long.TryParse(Telephone ,out teleohone))
            {
            AssignmentInfo assingmentInfo = DB.GetAllInformationByTelephoneNo(teleohone);
            if (assingmentInfo != null)
            {
                DetailTelephone.DataContext = assingmentInfo;
                if (assingmentInfo.isOutBoundCabinet == true)
                {
                    
                    OutBoundCabinetLabel.Content = "کافو خارج از مرز می باشد";
                    OutBoundCabinetLabel.Visibility = Visibility.Visible;
                }

                if (assingmentInfo.isOutBoundPost == true)
                {
                    OutBountPostLabel.Content =  "پست خارج از مرز می باشد";
                    OutBountPostLabel.Visibility = Visibility.Visible;
                }
                ConnectionDataGrid.ItemsSource = DB.GetAllInformationInGroupPostByPostIDAndWithOutpostContactType(assingmentInfo.PostID ?? 0, (byte)DB.PostContactConnectionType.PCMRemote);

            }
            else
            {
                Folder.MessageBox.ShowInfo("اطلاعاتی برای تلفن یافت نشد.");
            }
            }
            else
            {
            }
        }


    }
}
