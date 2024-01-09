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
using CRM.Data.Schema;

namespace CRM.Application.UserControls
{
    public partial class OpenAndCloseZero : UserControl
    {
        #region Properties and Fields

        private long _RequestID = 0;   
        
        private ZeroStatus _ZeroStatus { get; set; }
        private Request _Request { get; set; }

        // On Dayri form is true for to change background of CheckBox
       // public bool IsDayeriForm { get; set; }

        private long _telephoneNo;

        #endregion

        #region Constructors

        public OpenAndCloseZero()
        {
            InitializeComponent();
            Initialize();
        }

        public OpenAndCloseZero(long requestID , long telephone)
            : this()
        {
            _RequestID = requestID;
            _telephoneNo = telephone;

            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            //ZeroStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ZeroStatus));
            ZeroBlockComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ClassTelephone));
        }

        #endregion

        #region Event Handlers

        private void ZeroStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(TelephoneNo);

            //if (_RequestID == 0)
            //{
            //    if (telephone.LastSpecialServiceStatus != null)
            //    {
            //        CRM.Data.Schema.SpecialServices services = SpecialServiceUtility.Deserialize<CRM.Data.Schema.SpecialServices>(telephone.LastSpecialServiceStatus.ToString());

            //        switch ((byte)ZeroStatusComboBox.SelectedValue)
            //        {
            //            case (byte)DB.ZeroStatus.FirstZero:
            //                if (services.FirstZeroStatus != 0)
            //                {
            //                    if (services.FirstZeroStatus == (byte)DB.BlockZeroStatus.Close)
            //                    {
            //                        OpenZeroGroup.Visibility = Visibility.Visible;
            //                        CloseZeroGroup.Visibility = Visibility.Collapsed;
            //                        _ZeroStatus.ActionDueCloseZeroDate = DB.GetServerDate();
            //                        OpenZeroGroup.DataContext = _ZeroStatus;
            //                    }
            //                    else
            //                    {
            //                        OpenZeroGroup.Visibility = Visibility.Collapsed;
            //                        CloseZeroGroup.Visibility = Visibility.Visible;
            //                        _ZeroStatus.ActionDueOpenZeroDate = DB.GetServerDate();
            //                        CloseZeroGroup.DataContext = _ZeroStatus;
            //                    }
            //                }
            //                else
            //                {
            //                    OpenZeroGroup.Visibility = Visibility.Collapsed;
            //                    CloseZeroGroup.Visibility = Visibility.Visible;
            //                    _ZeroStatus.ActionDueOpenZeroDate = DB.GetServerDate();
            //                    CloseZeroGroup.DataContext = _ZeroStatus;
            //                }
            //                break;

            //            case (byte)DB.ZeroStatus.SecondZero:
            //                if (services.SecondZeroStatus != 0)
            //                {
            //                    if (services.SecondZeroStatus == (byte)DB.BlockZeroStatus.Close)
            //                    {
            //                        OpenZeroGroup.Visibility = Visibility.Visible;
            //                        CloseZeroGroup.Visibility = Visibility.Collapsed;
            //                        _ZeroStatus.ActionDueCloseZeroDate = DB.GetServerDate();
            //                        OpenZeroGroup.DataContext = _ZeroStatus;
            //                    }
            //                    else
            //                    {
            //                        OpenZeroGroup.Visibility = Visibility.Collapsed;
            //                        CloseZeroGroup.Visibility = Visibility.Visible;
            //                        _ZeroStatus.ActionDueCloseZeroDate = DB.GetServerDate();
            //                        CloseZeroGroup.DataContext = _ZeroStatus;
            //                    }
            //                }
            //                else
            //                {
            //                    OpenZeroGroup.Visibility = Visibility.Collapsed;
            //                    CloseZeroGroup.Visibility = Visibility.Visible;
            //                    _ZeroStatus.ActionDueOpenZeroDate = DB.GetServerDate();
            //                    CloseZeroGroup.DataContext = _ZeroStatus;
            //                }
            //                break;

            //            default:
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        OpenZeroGroup.Visibility = Visibility.Collapsed;
            //        CloseZeroGroup.Visibility = Visibility.Visible;
            //        _ZeroStatus.ActionDueOpenZeroDate = DB.GetServerDate();
            //        CloseZeroGroup.DataContext = _ZeroStatus;
            //    }
            //}
            //else
            //{
            //    switch (_ZeroStatus.Status)
            //    {
            //        case (byte)DB.BlockZeroStatus.Close:
            //            OpenZeroGroup.Visibility = Visibility.Collapsed;
            //            CloseZeroGroup.Visibility = Visibility.Visible;
            //            CloseZeroGroup.DataContext = _ZeroStatus;
            //            if ((_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID) ||
            //                (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Pending).ID) ||
            //                (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID))
            //            {
            //                CloseZeroGroup.IsEnabled = false;
            //                ZeroStatusComboBox.IsEnabled = false;
            //            }
            //            break;

            //        case (byte)DB.BlockZeroStatus.Open:
            //            OpenZeroGroup.Visibility = Visibility.Visible;
            //            CloseZeroGroup.Visibility = Visibility.Collapsed;
            //            OpenZeroGroup.DataContext = _ZeroStatus;
            //            if ((_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID) ||
            //                (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Pending).ID) ||
            //                (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID))
            //            {
            //                OpenZeroGroup.IsEnabled = false;
            //                ZeroStatusComboBox.IsEnabled = false;
            //            }
            //            break;

            //        default:
            //            break;
            //    }
            //}
        }

        private void ZeroBlockComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ZeroBlockComboBox.SelectedValue != null)
            {
                if (Convert.ToInt16(ZeroBlockComboBox.SelectedValue) == (int)DB.ClassTelephone.SecondZeroBlock)
                {
                    SecondZeroBlockingWithCostCheckBox.Visibility = Visibility.Visible;
                    SecondZeroBlockingWithCostLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    SecondZeroBlockingWithCostCheckBox.Visibility = Visibility.Collapsed;
                    SecondZeroBlockingWithCostLabel.Visibility = Visibility.Collapsed;
                    SecondZeroBlockingWithCostCheckBox.IsChecked = false;
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_telephoneNo);

            if (_RequestID == 0)
            {
                _ZeroStatus = new ZeroStatus();
                _ZeroStatus.ClassTelephone = telephone.ClassTelephone;
            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID(_RequestID);
                _ZeroStatus = Data.ZeroStatusDB.GetZeroStatusByID(_RequestID);
                if (_ZeroStatus.ClassTelephone == (int)DB.ClassTelephone.SecondZeroBlock)
                {
                    SecondZeroBlockingWithCostCheckBox.Visibility = Visibility.Visible;
                    SecondZeroBlockingWithCostLabel.Visibility = Visibility.Visible;
                }
            }

            this.DataContext = _ZeroStatus;

            // if are dayri form. change Background CheckBox
            //if (IsDayeriForm == true)
            //{
            //    if (telephone.FirstZeroBlock == false && _ZeroStatus.FirstZeroBlock == true)
            //    {
            //        (FirstZeroBlockCheckBox.Template.FindName("BorderTemplate", FirstZeroBlockCheckBox) as Border).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#96FF96"));
            //    }
            //    else if (telephone.FirstZeroBlock == true && _ZeroStatus.FirstZeroBlock == false)
            //    {
            //        (FirstZeroBlockCheckBox.Template.FindName("BorderTemplate", FirstZeroBlockCheckBox) as Border).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF9696"));
            //    }


            //    if (telephone.SecondZeroBlock == false && _ZeroStatus.SecondZeroBlock == true)
            //    {
            //        (SecondZeroBlockCheckBox.Template.FindName("BorderTemplate", SecondZeroBlockCheckBox) as Border).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#96FF96"));
            //    }
            //    else if (telephone.SecondZeroBlock == true && _ZeroStatus.SecondZeroBlock == false)
            //    {
            //        (SecondZeroBlockCheckBox.Template.FindName("BorderTemplate", SecondZeroBlockCheckBox) as Border).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF9696"));
            //    }
            //}

        }

        #endregion

        

    }
}
