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
    public partial class Title118 : UserControl
    {
        #region Properties

        private long _RequestID = 0;
        private byte _Mode = 0;

        public long TelephoneNo { get; set; }
        private Data.TitleIn118 _Title118 { get; set; }
        private Data.Request _Request { get; set; }
        private Data.Customer _Customer { get; set; }
        private long _Telephone { get; set; }

        private long RequestType { get; set; }

        #endregion

        #region Costructors

        public Title118()
        {
            InitializeComponent();
            Initialize();
        }

        public Title118(long requestID, long telephone, int _requestType)
            : this()
        {
            _RequestID = requestID;
            RequestType = _requestType;
            _Telephone = telephone;

        }

        #endregion

        #region Methods

        private void Initialize()
        {
        }

        #endregion

        #region Event HAndlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData(null, null);
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            if (_RequestID == 0)
            {

                _Title118 = new TitleIn118();

            }
            else
            {
                _Title118 = Data.TitleIn118DB.GetTitleIn118ByRequestID(_RequestID);

            }

            switch (RequestType)
            {


                case (int)DB.RequestType.RemoveTitleIn118:
                    {

                        TitleGroupBox.Visibility = Visibility.Collapsed; 

                    }
                    break;

                case (int)DB.RequestType.ChangeTitleIn118:

                    break;

                  

                default:
                    break;
            }
            TitleIn118 titleIn118 = Data.TitleIn118DB.GetLastTitlein118ByTelephone(_Telephone);
            if (titleIn118 != null)
            {
                LastTitleGroupBox.Visibility = Visibility.Visible;
                LastTitleGroupBox.DataContext = titleIn118;
            }

            //Customer Customer = Data.CustomerDB.GetCustomerByTelephone(_Telephone);
            //if (Customer != null && Customer.PersonType == (int)DB.PersonType.Company)
            //{

            //    NameTitleAt118Label.Visibility = Visibility.Collapsed;
            //    NameTitleAt118TextBox.Visibility = Visibility.Collapsed;

            //    LastNameAt118Label.Visibility = Visibility.Collapsed;
            //    LastNameAt118TextBox.Visibility = Visibility.Collapsed;

            //    TitleAt118Label.Visibility = Visibility.Visible;
            //    TitleAt118TextBox.Visibility = Visibility.Visible;
            //}
            //else if (Customer != null && Customer.PersonType == (int)DB.PersonType.Person)
            //{
            //    TitleAt118Label.Visibility = Visibility.Collapsed;
            //    TitleAt118TextBox.Visibility = Visibility.Collapsed;

            //    NameTitleAt118Label.Visibility = Visibility.Visible;
            //    NameTitleAt118TextBox.Visibility = Visibility.Visible;

            //    LastNameAt118Label.Visibility = Visibility.Visible;
            //    LastNameAt118TextBox.Visibility = Visibility.Visible;
            //}
            TitleGroupBox.DataContext = _Title118;
        }

        #endregion


    }
}
