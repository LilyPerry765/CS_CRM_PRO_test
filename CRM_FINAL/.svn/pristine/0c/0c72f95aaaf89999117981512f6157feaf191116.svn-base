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
    /// Interaction logic for ChangeNoUserControl.xaml
    /// </summary>
    public partial class ChangeNoUserControl : Local.UserControlBase
    {
        #region Properties and Fields

        private long _RequestID;
        private long _TelephoneNo;
        private ChangeNo _ChangeNo { get; set; } 

        #endregion

        #region Constructors

        public ChangeNoUserControl()
        {
            InitializeComponent();
        }

        public ChangeNoUserControl(long requestId, long telephoneNo)
            : this()
        {
            this._RequestID = requestId;
            this._TelephoneNo = telephoneNo;
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CauseOfChangeNoComboBox.ItemsSource = CauseOfChangeNoDB.GetCauseOfChangeNoCheckableItem();
            _ChangeNo = new ChangeNo();
        }

        #endregion

        #region EventHandlers

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            _ChangeNo.OldTelephoneNo = _TelephoneNo;

            if (_RequestID != 0)
            {
                Request request = Data.RequestDB.GetRequestByID((long)_RequestID);
                _ChangeNo = Data.ChangeNoDB.GetChangeNoDBByID((long)_RequestID);
                if (Data.StatusDB.IsFinalStep(request.StatusID))
                {
                    NewTelephoneNoLabel.Visibility = Visibility.Visible;
                    NewTelephoneNoTextBox.Visibility = Visibility.Visible;
                }

            }

            this.DataContext = _ChangeNo;


        } 

        #endregion
    }
}
