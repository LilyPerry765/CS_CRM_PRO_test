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
    /// Interaction logic for RequestRejectDescriptionForm.xaml
    /// </summary>
    public partial class RequestRejectDescriptionForm : Local.PopupWindow
    {
        private int _requestStatusID = 0;
        public string Description 
        { 
            get 
            {
               return DescriptionTextBox.Text.Trim();
            }
        }
        public int RequestRejectReason
        {
            get
            {
                return ((int?)RequestRejectReasonComboBox.SelectedValue) ?? -1;
            }
        }
        public RequestRejectDescriptionForm()
        {
            InitializeComponent();
        }
        public RequestRejectDescriptionForm(int requestStatusID) : this()
        {
            _requestStatusID = requestStatusID;
        }
       

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

            RequestRejectReasonComboBox.ItemsSource = Data.RequestRejectReasonDB.GetRequestReasonCheckableByRequestStatusID(_requestStatusID);
        }


        private void SaveForm(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

    }
}
