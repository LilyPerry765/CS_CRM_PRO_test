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

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for BuchtSwitchingUserControl.xaml
    /// </summary>
    public partial class BuchtSwitchingUserControl : Local.UserControlBase
    {
        private long _requestID = 0;
        AssignmentInfo assignmentInfo = new AssignmentInfo();
        ConnectionInfo connectionInfo = new ConnectionInfo();
        private BuchtSwitching buchtSwitching { get; set; }
        public BuchtSwitchingUserControl()
        {
            InitializeComponent();
        }

        public BuchtSwitchingUserControl(long requestID)
            :this()
        {
            this._requestID = requestID;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

            buchtSwitching = Data.BuchtSwitchingDB.GetBuchtSwitchingByID(_requestID);
            Request reqeust = Data.RequestDB.GetRequestByID(_requestID);
            TelephoneTextBox.Text = reqeust.TelephoneNo.ToString();

            BuchtInformation.DataContext = new 
            {
                oldBucht = DB.GetBuchtInfoByID(buchtSwitching.OldBuchtID),
                newBucht = DB.GetBuchtInfoByID(buchtSwitching.NewBuchtID ?? 0),
            };


            OtherBuchtInformation.DataContext = new
            {
                OtherBucht = DB.GetBuchtInfoByID(buchtSwitching.OtherBuchtID ?? 0)
            };

            AboneInformation.DataContext = new
            {
                oldAbone = DB.GetAboneInfoByBuchtID(buchtSwitching.OldBuchtID),
                newAbone = DB.GetAboneInfoByBuchtID(buchtSwitching.NewBuchtID ?? 0),
                newPost = DB.GetAboneInfoByPostContact(buchtSwitching.PostContactID ?? 0),
            };


        }
    }
}
