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
    public partial class ADSLSetupContactInformationForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private long _RequestID = 0;

        #endregion

        #region Constructors

        public ADSLSetupContactInformationForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLSetupContactInformationForm(int id, long requestID)
            : this()
        {
            _ID = id;
            _RequestID = requestID;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
        }

        private void LoadData()
        {
            ADSLSetupContactInformation contact = new ADSLSetupContactInformation();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                contact = Data.ADSLSetupContactInformationDB.GetContactInformationById(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = contact;
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
                ADSLSetupContactInformation contactInformation = this.DataContext as ADSLSetupContactInformation;

                if (contactInformation.Date == null)
                    throw new Exception("لطفا تاریخ تماس را وارد نمایید");

                if (string.IsNullOrWhiteSpace(ContactTimeTextBox.Text))
                    throw new Exception("لطفا ساعت تماس را وارد نمایید");

                if (contactInformation.Description == null)
                    throw new Exception("لطفا توضیحات را وارد نمایید");

                contactInformation.UserID = DB.CurrentUser.ID;
                contactInformation.Time = ContactTimeTextBox.Text;
                contactInformation.RequestID = _RequestID;

                contactInformation.Detach();
                Save(contactInformation);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات تماس، " + ex.Message + " !", ex);
            }
        }

        private void MouseLeftButtonDownImage(object sender, MouseButtonEventArgs e)
        {
            Image star = sender as Image;

            if (star != null)
            {
                switch (star.Name)
                {
                    case "DateImage":
                        ContactDate.SelectedDate = DB.GetServerDate();
                        break;

                    case "TimeImage":
                        ContactTimeTextBox.Text = ((DB.GetServerDate().Hour >= 10) ? DB.GetServerDate().Hour.ToString() : "0" + DB.GetServerDate().Hour.ToString()) + ":" + ((DB.GetServerDate().Minute >= 10) ? DB.GetServerDate().Minute.ToString() : "0" + DB.GetServerDate().Minute.ToString());
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
