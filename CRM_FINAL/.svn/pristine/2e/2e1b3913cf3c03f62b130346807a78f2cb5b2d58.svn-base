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
    /// Interaction logic for CabinetAndPost.xaml
    /// </summary>
    public partial class CabinetAndPost : Local.UserControlBase
    {

        #region Fildes && events
        private int centerID = 0;

        private int _postID = 0;

        private int _cabinetID = 0;

        public int CenterID
        {
            get { return centerID; }
            set { centerID = value; }
        }

        public int PostID
        {
            get { return _postID; }
            set { _postID = value; }
        }

        public int CabinetID
        {
            get { return _cabinetID; }
            set { _cabinetID = value; }
        }
        public CabinetAndPost()
        {
            InitializeComponent();
        }

        public CabinetAndPost(int centerID)
            : this()
        {
            CenterID = centerID;
            Initialize();
        }

        public delegate void PostChange(int postID);
        public event PostChange DoPostChange;
        void OnDoPostChange(int postID)
        {
            if (DoPostChange != null)
                DoPostChange(postID);
        }


        public delegate void CabinetChange(int? CabinetID);
        public event CabinetChange DoCabinetChange;
        void OnDoCabinetChange(int? CabinetID)
        {
            if (DoCabinetChange != null)
                DoCabinetChange(CabinetID);

        }
        #endregion

        #region Methode
        private void Initialize()
        {

        }

        public void CabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            if (CabinetComboBox.SelectedValue != null)
            {
                PostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)CabinetComboBox.SelectedValue);
                CabinetID = (int)CabinetComboBox.SelectedValue;
                OnDoCabinetChange((int?)CabinetComboBox.SelectedValue);
            }
        }
        public void PostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostComboBox.SelectedValue != null)
            {
                
                PostID = (int)PostComboBox.SelectedValue;
                OnDoPostChange((int)PostComboBox.SelectedValue);
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {

            if(_IsLoaded)
                return ;
            else
               _IsLoaded = false;

            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByType(centerID);
            CabinetComboBox.SelectedValue = _cabinetID;
            CabinetComboBox_SelectionChanged(null, null);
            PostComboBox.SelectedValue = _postID;

        }
        #endregion

        private void ViewPost_Click(object sender, RoutedEventArgs e)
        {

            if (PostID != 0)
            {
                CRM.Application.Views.PostForm postForm = new Views.PostForm((int)PostComboBox.SelectedValue);
                postForm.IsEnabled = false;
                postForm.ShowDialog();
            }

        }


    }
}
