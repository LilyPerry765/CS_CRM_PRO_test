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
using CRM.Data;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for ConnectionOfPost.xaml
    /// </summary>
    public partial class ConnectionOfPost : UserControl
    {
        
        public  CRM.Data.AssignmentInfo pastAssingmentInfo;
        List<CRM.Data.AssignmentInfo> assingmentInfo;
        Data.ChangeLocation changeLocation;
        public InvestigatePossibility _Investigate { get; set; }

        long _RequestID = 0;
        int? postID;
        bool mode = false;

        public int PostID
        {
            get
            {
                return postID ?? -1;
            }
            set
            {
                postID = value;
            }
        }
        public bool Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        public long RequestID
        {
            get
            {
                return _RequestID;
            }
            set
            {
                _RequestID = value;
            }
        }

        public List<CRM.Data.AssignmentInfo> AssingmentInfo
        {
            get
            {
                return assingmentInfo;
            }
            set
            {
                assingmentInfo = value;
            }
        }
        public ConnectionOfPost()
        {
            InitializeComponent();
            Initialize();
            
        }

        //TODO : 

        //public ConnectionOfPost(long request)
        //    : this()
        //{
        //    //_RequestID = request;
        //    //Initialize();
        //}
        private void Initialize()
        {
            
            changeLocation = new Data.ChangeLocation();
            List<EnumItem> postContactStatus = Helper.GetEnumItem(typeof(DB.PostContactStatus));
            postContactStatus.RemoveAll(t => t.ID == (int)DB.PostContactStatus.Deleted || t.ID == (int)DB.PostContactStatus.NoCableConnection);
            StatusColumn.ItemsSource = postContactStatus;       

          //  BuchtTypeColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.BuchtType));
        }

      
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BuchtTypeColumn.ItemsSource = Data.BuchtTypeDB.GetBuchtTypeCheckable();
            changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(_RequestID);
            _Investigate = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_RequestID).Take(1).SingleOrDefault();

            if (changeLocation != null && _Investigate.BuchtID != null)
            {
                pastAssingmentInfo = AssignmentDB.SearchByBuchtID(_Investigate.BuchtID);
                postID = pastAssingmentInfo.PostID ?? -1;
                InputComboBox.SelectedValue = (long)pastAssingmentInfo.InputNumberID;
                InputComboBox_SelectionChanged(null, null);
                ConnectionDataGrid.SelectedValue = pastAssingmentInfo.BuchtID;
                if (pastAssingmentInfo.InputNumberID != null)
                    InputComboBox.SelectedValue = (long)pastAssingmentInfo.InputNumberID;
                mode = true;
            }
        }
        
        private void InputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InputComboBox.SelectedValue != null && assingmentInfo != null)
            {
                ConnectionDataGrid.ItemsSource = assingmentInfo.Where(t => t.InputNumberID == (long)InputComboBox.SelectedValue).ToList();
            }
             
        }
    }
}
