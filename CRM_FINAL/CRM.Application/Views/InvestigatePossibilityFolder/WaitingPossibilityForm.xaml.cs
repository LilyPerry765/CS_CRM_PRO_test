using CRM.Data;
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

namespace CRM.Application.Views.InvestigatePossibilityFolder
{
    /// <summary>
    /// Interaction logic for WaitingPossibilityForm.xaml
    /// </summary>
    public partial class WaitingPossibilityForm : Local.PopupWindow
    {
        private long _RequstID;
        private long _InvestigatPossibilityID;
        private long _WaitingListID;

        WaitingList _WaitingList;
        Request _Request;

        public WaitingPossibilityForm()
        {
            InitializeComponent();
        }

        public WaitingPossibilityForm(long requstID, long investigatPossibilityID, long waitingListID):this()
        {
            this._RequstID = requstID;
            this._InvestigatPossibilityID = investigatPossibilityID;
            this._WaitingListID = waitingListID;
            Initialize();
        }


        private void Initialize()
        {
            _WaitingList = new WaitingList();
            _Request = new Request();
            _Request = Data.RequestDB.GetRequestByID( _RequstID);
            ReasonComboBox.ItemsSource = Data.WaitingListReasonDB.GetWaitingListReasonCheckableByRequestTypeID(_Request.RequestTypeID);
            _WaitingList.InsertDate = DB.GetServerDate();
        }

        private void PopupWindow_Loaded_1(object sender, RoutedEventArgs e)
        {


            if (_WaitingListID != 0)
            {
                _WaitingList = DB.SearchByPropertyName<WaitingList>("ID", _WaitingListID).SingleOrDefault();
            }

            this.DataContext = _WaitingList;


        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            _WaitingList.RequestID = _RequstID;
            _WaitingList.CreatorUserID = DB.CurrentUser.ID;
            _WaitingList.Status = false;
            Data.WaitingPossibilityDB.WaitingPossibilitySave(_Request, _WaitingList);
        }
    }
}
