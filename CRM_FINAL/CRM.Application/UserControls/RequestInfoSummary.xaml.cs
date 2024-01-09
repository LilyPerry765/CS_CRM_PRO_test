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
using System.Collections.ObjectModel;
using CRM.Application.Views;

namespace CRM.Application.UserControls
{
    public partial class RequestInfoSummary : Local.UserControlBase
    {
        #region Properties and Fields

        public static Request _request { get; set; }

        public Customer Customer { get; set; }
        //  public  List<UsedDocs> refDocs { get; set; }

        public List<UsedDocs> refDocs
        {
            get { return (List<UsedDocs>)GetValue(refDocsProperty); }
            set { SetValue(refDocsProperty, value); }
        }

        public static readonly DependencyProperty refDocsProperty = DependencyProperty.Register("refDocs", typeof(List<UsedDocs>), typeof(UserControl));

        private byte[] FileBytes { get; set; }

        #endregion

        #region Constructors

        public RequestInfoSummary()
        {
            InitializeComponent();
        }

        public RequestInfoSummary(long id)
            : this()
        {
            _request = Data.RequestDB.GetRequestByID(id);
            if (_request.CustomerID != null)
            {
                Customer = Data.CustomerDB.GetCustomerByID(_request.CustomerID ?? 0);
                refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.CustomerID == Customer.ID).ToList();
            }
            this.DataContext = _request;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            RequestTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.RequestType));
            CentercomboBox.ItemsSource = Data.CenterDB.GetCenterCheckable().Where(t => DB.CurrentUser.CenterIDs.Contains(t.ID));
            RequestTypecomboBox.SelectedValue = _request.RequestTypeID;
            CentercomboBox.SelectedValue = _request.CenterID;

            if (Customer != null)
            {
                var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_request.RequestTypeID, _request.RequestDate, Customer.PersonType);
                RequestDocGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 1).ToList();
                RequestPermissionGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 2).ToList();
                RequestContractGrid.ItemsSource = NeededDocs.Where(t => t.TypeID == 3).ToList();
            }
            RelatedRequestsGrid.ItemsSource = Data.RequestDB.GetRelatedRequestByID(_request.ID);
            InstallmentsGrid.ItemsSource = Data.InstallmentDB.GetInstallmentByRequestID(_request.ID);
            RequestPaymentDataGrid.ItemsSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(_request.ID);

            Validation.ClearInvalid(RequestTypecomboBox.GetBindingExpression(ComboBox.SelectedValueProperty));
            Validation.ClearInvalid(CentercomboBox.GetBindingExpression(ComboBox.SelectedValueProperty));

            if (_request.RequestTypeID == (int)DB.RequestType.SpaceandPower)
            {
                RequestTelecomminucationServiceTabItem.Visibility = Visibility.Visible;

                //پر کردن کمبو باکس مربوط به کالا و خدمات مخابرات که مربوط به درخواست فضاپاور هستند
                this._TelecomminucationServiceInfos = TelecomminucationServiceDB.GetTelecomminucationServiceInfosByRequestTypeID((int)DB.RequestType.SpaceandPower);

                this._TelecomminucationServicePaymentInfos = TelecomminucationServicePaymentDB.GetTelecomminucationServicePaymentInfos(_request.ID);

                TelecomminucationServiceColumn.ItemsSource = this._TelecomminucationServiceInfos;
                RequestTelecomminucationServiceDataGrid.ItemsSource = this._TelecomminucationServicePaymentInfos;
            }
        }

        #endregion

        #region EventHandlers

        private void RequestInfoExpander_Expanded(object sender, RoutedEventArgs e)
        {
            RequestTypecomboBox.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();
            CentercomboBox.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();
        }

        private void RequestInfoExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            Validation.ClearInvalid(RequestTypecomboBox.GetBindingExpression(ComboBox.SelectedValueProperty));
            Validation.ClearInvalid(CentercomboBox.GetBindingExpression(ComboBox.SelectedValueProperty));
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (RequestDocGrid.SelectedItem != null)
            {
                DocumentsByCustomer RequestDocGridItem = RequestDocGrid.SelectedItem as DocumentsByCustomer;

                RequestDocument requestDocument = Data.RequestDocumnetDB.GetRequestDocument(_request.ID, RequestDocGridItem.DocumentRequestTypeID);

                if (requestDocument != null)
                {
                    FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable((Guid)requestDocument.DocumentsFileID);
                    FileBytes = fileInfo.Content;
                    CRM.Application.Views.DocumentViewForm window = new DocumentViewForm();
                    window.FileBytes = FileBytes;
                    window.FileType = fileInfo.FileType;
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show(".فایلی وجود ندارد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region TelecommunicationService

        public List<TelecomminucationServiceInfo> _TelecomminucationServiceInfos { get; set; }

        public List<TelecomminucationServicePaymentInfo> _TelecomminucationServicePaymentInfos { get; set; }

        #endregion

    }
}
