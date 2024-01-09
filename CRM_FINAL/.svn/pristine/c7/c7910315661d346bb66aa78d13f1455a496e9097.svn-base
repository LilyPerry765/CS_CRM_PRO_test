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
    public partial class CutAndEstablish : Local.UserControlBase
    {
        #region Properties

        private long _RequestID = 0;
        private long _CustomerID = 0;
        private int _RequestType = 0;

        private Data.CutAndEstablish _CutAndEstablish { get; set; }
        private Request _Request { get; set; }
        private Telephone _Telephone { get; set; }

        public long TelephoneNo { get; set; }

        #endregion

        #region Constructors

        public CutAndEstablish()
        {
            InitializeComponent();
            Initialize();
        }

        public CutAndEstablish(long requestID, long customerID, long telephoneNo, int requestType)
            : this()
        {
            _RequestID = requestID;
            _CustomerID = customerID;
            TelephoneNo = telephoneNo;
            _RequestType = requestType;

            LoadData(null, null);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CutTypeComboBox.ItemsSource = Data.CauseOfCutDB.GetCauseOfCutCheckableItem();
            TypesColumn.ItemsSource = Data.CauseOfCutDB.GetCauseOfCutCheckableItem();
        }

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            if (_RequestID == 0)
            {
                _CutAndEstablish = new CRM.Data.CutAndEstablish();
                _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(TelephoneNo);

                switch (_RequestType)
                {
                    case (int)DB.RequestType.Connect:
                        {
                            CutGroupBox.Visibility = Visibility.Collapsed;
                            EstablishGroupBox.Visibility = Visibility.Visible;
                            MessageLabel.Visibility = Visibility.Collapsed;
                            _CutAndEstablish.ActionEstablishDueDate = DB.GetServerDate();
                            EstablishInfo.DataContext = _CutAndEstablish;
                            List<CRM.Data.CutAndEstablish> cutRequestList = Data.CutAndEstablishDB.GetCutAndEstablishByTelephoneNo(TelephoneNo, (byte)DB.RequestType.CutAndEstablish).ToList();
                            CutReasonDataGrid.DataContext = cutRequestList;
                        }
                        break;
                    case (int)DB.RequestType.CutAndEstablish:
                        {
                            CutGroupBox.Visibility = Visibility.Visible;
                            EstablishGroupBox.Visibility = Visibility.Collapsed;
                            MessageLabel.Visibility = Visibility.Collapsed;
                            _CutAndEstablish.ActionCutDueDate = DB.GetServerDate();
                            CutInfo.DataContext = _CutAndEstablish;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID(_RequestID);
                _CutAndEstablish = Data.CutAndEstablishDB.GetCutAndEstablishByRequestID(_RequestID);

                switch (_RequestType)
                {
                    case (int)DB.RequestType.CutAndEstablish:
                        {
                            CutGroupBox.Visibility = Visibility.Visible;
                            EstablishGroupBox.Visibility = Visibility.Collapsed;
                            MessageLabel.Visibility = Visibility.Collapsed;
                            CutTypeComboBox.SelectedValue = _CutAndEstablish.CutType;
                            CutInfo.DataContext = _CutAndEstablish;
                        }
                        break;
                    case (int)DB.RequestType.Connect:
                        {
                            CutGroupBox.Visibility = Visibility.Collapsed;
                            EstablishGroupBox.Visibility = Visibility.Visible;
                            MessageLabel.Visibility = Visibility.Collapsed;
                            EstablishInfo.DataContext = _CutAndEstablish;
                            List<CRM.Data.CutAndEstablish> cutRequestList = Data.CutAndEstablishDB.GetCutAndEstablishByTelephoneNo(TelephoneNo, (int)DB.RequestType.CutAndEstablish);
                            CutReasonDataGrid.DataContext = cutRequestList;
                        }
                        break;
                    default:
                        break;
                }

                //if ((_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Pending).ID) || (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID))
                //{
                //    EstablishGroupBox.IsEnabled = false;
                //    CutGroupBox.IsEnabled = false;

                //    CounterTextBox.Text = _CutAndEstablish.Counter;
                //    CutSaloonDate.SelectedDate = _CutAndEstablish.CutDate;

                //    SaloonInfo.Visibility = Visibility.Visible;
                //}
            }
        }

        private void CutTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CutTypeComboBox.SelectedValue != null)
            {
                if ((int)CutTypeComboBox.SelectedValue == (int)DB.CauseOfCut.PerturbedThird)
                {
                    WiringIllegalLabel.Visibility = Visibility.Collapsed;
                    WiringIllegalTextBox.Visibility = Visibility.Collapsed;

                    VacateLabel.Visibility = Visibility.Visible;
                    VacateCheckBox.Visibility = Visibility.Visible;
                }
                else if ((int)CutTypeComboBox.SelectedValue == (int)DB.CauseOfCut.WiringIllegal)
                {
                    VacateLabel.Visibility = Visibility.Collapsed;
                    VacateCheckBox.Visibility = Visibility.Collapsed;

                    WiringIllegalLabel.Visibility = Visibility.Visible;
                    WiringIllegalTextBox.Visibility = Visibility.Visible;
                }
                else
                {
                    VacateLabel.Visibility = Visibility.Collapsed;
                    VacateCheckBox.Visibility = Visibility.Collapsed;
                    WiringIllegalLabel.Visibility = Visibility.Collapsed;
                    WiringIllegalTextBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        #endregion
    }
}
