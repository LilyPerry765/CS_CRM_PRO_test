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
using Enterprise;

namespace CRM.Application.Views
{
    public partial class UserForm : Local.PopupWindow
    {
         #region Properties

        private int _UserID = 0;
        private static List<CheckableItem> _CenterIDs;

        #endregion

        #region Constructors

        public UserForm()
        {
            InitializeComponent();
            Initialize();
        }

        public UserForm(int id)
            : this()
        {
            _UserID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RoleComboBox.ItemsSource = Data.SecurityDB.GetRolesInfo();
            CenterListBox.ItemsSource = _CenterIDs = Data.CenterDB.GetCenterCheckable();
        }

        private void LoadData()
        {
            User user = new User();

            if (_UserID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                user = UserDB.GetUserByID(_UserID);

                CenterListBox.ItemsSource = null;

                List<UserCenter> userCenters = UserDB.GetUserCentersByUserId(_UserID);
                foreach (UserCenter userCenter in userCenters)
                {
                    _CenterIDs.Where(t => (int)t.ID == userCenter.CenterID).SingleOrDefault().IsChecked = true;
                }

                CenterListBox.ItemsSource = _CenterIDs;

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = user;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        
        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                if (RoleComboBox.SelectedItem == null || RoleComboBox.SelectedValue.ToString() == "-1") throw new Exception("نقش انتخاب نشده است");

                List<int> selectedCenterIds = CenterListBox.Items.Cast<CheckableItem>().ToList().Where(t => t.IsChecked == true).Select(t => (int)t.ID).ToList();

                User user = this.DataContext as User;

                user.TimeStamp = "000000000000000";

                SecurityDB.SaveUser(user, selectedCenterIds, PasswordTextBox.Password.Trim(), (RoleComboBox.SelectedItem as RoleInfo).Name);
                user.Detach();
                DB.Save(user);
                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex, "Save User Error");
            }
        }

        private void ListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                switch (e.Key)
                {
                    case Key.A:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = true;
                        listBox.Items.Refresh();
                        break;

                    case Key.N:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = false;
                        listBox.Items.Refresh();
                        break;

                    case Key.R:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = !item.IsChecked;
                        listBox.Items.Refresh();
                        break;

                    default:
                        break;
                }
            }
        }

        private void CenterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CenterListBox.ItemsSource = _CenterIDs.Where(t => t.Name.Contains(CenterTextBox.Text)).ToList();
        }

        #endregion
    }
}
