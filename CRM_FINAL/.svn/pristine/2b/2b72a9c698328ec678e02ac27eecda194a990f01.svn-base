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
    public partial class Failure117FailureStatusForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public Failure117FailureStatusForm()
        {
            InitializeComponent();
            Initialize();
        }

        public Failure117FailureStatusForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            FailureStatusTypeComboBox.ItemsSource = Failure117DB.GetParentFailureStatus((byte)DB.Failure117AvalibilityStatus.MDFAnalysis);
            //AvailablityComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Failure117AvalibilityStatus));
        }

        private void LoadData()
        {
            Failure117FailureStatus failureStatus = new Failure117FailureStatus();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                failureStatus = Data.Failure117DB.GetFailureStatusByID(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = failureStatus;

            if (failureStatus.ParentID != null)
            {
                FailureStatusTypeComboBox.SelectedValue = failureStatus.ParentID;
            }

            List<int> list = new List<int>();
            string[] availableList = new string[5];
            
            if (!string.IsNullOrEmpty(failureStatus.Availablity))
                availableList = failureStatus.Availablity.Split(',');

            foreach (string availableChar in availableList)
            {
                if (availableChar != "")
                    list.Add(Convert.ToInt32(availableChar));
            }

            UIElement container = AvalibilityGrid as UIElement;
            List<System.Windows.Controls.Control> controlsList = Helper.FindVisualChildren<System.Windows.Controls.Control>(container).ToList();

            foreach (System.Windows.Controls.Control control in controlsList.Where(t => t.GetType() == typeof(CheckBox)).ToList())
            {
                CheckBox currentControl = control as CheckBox;
                foreach (int index in list)
                {
                    if (index == Convert.ToInt32(currentControl.Tag))
                        currentControl.IsChecked = true;
                }
            }
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
                Failure117FailureStatus failureStatus = this.DataContext as Failure117FailureStatus;
                failureStatus.Availablity = "";

                if (string.IsNullOrEmpty(ArchivedTimeTextBox.Text))
                    failureStatus.ArchivedTime = 0;

                List<int> list = new List<int>();
                UIElement container = AvalibilityGrid as UIElement;
                List<System.Windows.Controls.Control> controlsList = Helper.FindVisualChildren<System.Windows.Controls.Control>(container).ToList();

                foreach (System.Windows.Controls.Control control in controlsList.Where(t => t.GetType() == typeof(CheckBox)).ToList())
                {
                    CheckBox currentControl = control as CheckBox;

                    if ((bool)currentControl.IsChecked)
                        list.Add(Convert.ToInt32(currentControl.Tag));
                }

                foreach (int index in list)
                {
                    failureStatus.Availablity = failureStatus.Availablity + index.ToString() + ",";
                }

                failureStatus.Detach();
                Save(failureStatus);

                ShowSuccessMessage("وضعیت خرابی ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره وضعیت خرابی", ex);
            }
        }

        #endregion
    }
}
