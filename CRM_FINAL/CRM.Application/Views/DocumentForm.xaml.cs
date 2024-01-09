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
using System.IO;
using Microsoft.Win32;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class DocumentForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        public DocumentRequestType doc;

        private long _id = 0;
        private int _announceID = 0;

        #endregion

        #region Constructors

        public DocumentForm()
        {
            InitializeComponent();
            Initialize();

        }

        public DocumentForm(int id)
            : this()
        {
            _ID = id;
        }

        public DocumentForm(long id, int announceID)
            : this()
        {
            _id = id;
            _announceID = announceID;
            Initialize();

        }

        #endregion

        #region Methods        

        private void Initialize()
        {
            doc = new DocumentRequestType();

            doc.AnnounceID = _announceID;

            this.DataContext = doc;

            DocumentTypeIDcomboBox.ItemsSource = DB.GetAllEntity<DocumentType>().OrderBy(t => t.TypeID);//.Where(t => t.TypeID == 1);

            RequestTypeIDcomboBox.ItemsSource = DB.GetAllEntity<RequestType>();

            ChargingTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ChargingGroup)); 

            TelephoneTypecomboBox.ItemsSource = EnumTypeNameHelper.TelephoneTypeEnumValues;

            PosessionTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PossessionType)); 

            OrderTypecomboBox.ItemsSource = EnumTypeNameHelper.OrderTypeEnumValues;

            Title = "ثبت مدارک مورد نیاز درخواستها ";

        }

        #endregion

        #region Event Handlers

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            doc.InsertDate = DB.GetServerDate();
            doc.Detach();
            DB.Save(doc);

            if (_id != 0)
            {
                this.Close();
            }
            else
            {
                _id = 0;
                LoadData();
            }

        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (_id == 0)
            {
                Initialize();
            }
            else
            {
                SaveButton.Content = "بروز رسانی";
                Title = "بروز رسانی نوع مدرک ";
                doc = DB.GetEntitybyID<DocumentRequestType>(_id);
                this.DataContext = doc;
            }
        }

        private void RequestTypeIDcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion
    }
}
