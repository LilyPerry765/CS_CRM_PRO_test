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
    public partial class DocumentForRequestForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;
        public DocumentRequestType doc;
        private int _AnnounceID = 0;

        #endregion

        #region Constructors

        public DocumentForRequestForm()
        {
            InitializeComponent();
            Initialize();
        }

        public DocumentForRequestForm(long id)
            : this()
        {
            _ID = id;
        }

        public DocumentForRequestForm(long id, int announceID)
            : this()
        {
            _ID = id;
            _AnnounceID = announceID;
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            doc = new DocumentRequestType();

            doc.AnnounceID = _AnnounceID;

            this.DataContext = doc;

            DocumentTypeIDcomboBox.ItemsSource = Data.DocumentTypeDB.GetAllEntity();
            RequestTypeIDcomboBox.ItemsSource = Data.RequestTypeDB.GetAllEntity();

            ChargingTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ChargingGroup));
            TelephoneTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.TelephoneType));
            PosessionTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PossessionType));
            OrderTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.OrderType));
        }

        private void LoadData()
        {
            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";

                ChargingTypecomboBox.SelectedValue = (byte)DB.ChargingGroup.Normal;
                PosessionTypecomboBox.SelectedValue = (byte)DB.PossessionType.Normal;
                OrderTypecomboBox.SelectedValue = (byte)DB.OrderType.Normal;
            }
            else
            {
                SaveButton.Content = "بروز رسانی";                
                doc = DB.GetEntitybyID<DocumentRequestType>(_ID);
                this.DataContext = doc;   
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
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            doc.AnnounceID = _AnnounceID;

            if (doc.AnnounceID == 0)
            {
                Folder.MessageBox.ShowInfo("آیین نامه ای برای مدرک مورد نظر یافت نشد.");
                return;

            }
            
            doc.Detach();
            DB.Save(doc);

            this.DialogResult = true;
        }

        private void RequestTypeIDcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion
    }
}
