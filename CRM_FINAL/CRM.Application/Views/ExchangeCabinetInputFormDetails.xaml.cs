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
using System.Transactions;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ExchangeCabinetInputDetails.xaml
    /// </summary>
    public partial class ExchangeCabinetInputDetails : Local.PopupWindow
    {

        List<Bucht> OldBucht;
        List<CheckableItem> newCabinetInputIDs;
        List<PostContact> newConnectionID;
        ExchangeCabinetInput item;
        private long p;
        private long _requestID = 0;
        public ExchangeCabinetInputDetails()
        {
            InitializeComponent();
            Initialize();
        }

        public ExchangeCabinetInputDetails(long requestID):this()
        {

            _requestID = requestID;
            Initialize();
        }

        private void Initialize()
        {
            OldCabinetTypeComboBox.ItemsSource = Data.CabinetTypeDB.GetCabinetTypeCheckable();
            NewCabinetTypeComboBox.ItemsSource = Data.CabinetTypeDB.GetCabinetTypeCheckable();
            OldBucht = new List<Bucht>();
            newCabinetInputIDs = new List<CheckableItem>();
            newConnectionID = new List<PostContact>();
        }


     
        private void OldCabinetTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OldCabinetTypeComboBox.SelectedValue != null)
            {
                IEnumerable<int> cabinet = Data.CabinetDB.GetCabinetListByTypeID((int)OldCabinetTypeComboBox.SelectedValue).Select(t => t.ID);
                OldCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckable().Where(t => cabinet.Contains(t.ID));
            }
        }

        private void NewCabinetTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewCabinetTypeComboBox.SelectedValue != null)
            {
                IEnumerable<int> cabinet = Data.CabinetDB.GetCabinetListByTypeID((int)NewCabinetTypeComboBox.SelectedValue).Select(t => t.ID);
                NewCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckable().Where(t => cabinet.Contains(t.ID));
            }
        }

        private void OldCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OldCabinetComboBox.SelectedValue != null)
            {
               
                List<int> postID = Data.PostDB.GetPostCheckableByCabinetID((int)OldCabinetComboBox.SelectedValue).Select(t=>t.ID).ToList();
                List<CheckableItem> cabinetInputIDs = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)OldCabinetComboBox.SelectedValue);
                List<PostContact> ConnectionID = Data.PostContactDB.GetPostContactByPostID(postID).ToList();
                OldBucht = Data.BuchtDB.GetBuchtByListConnectionID(ConnectionID.Select(t=>t.ID).ToList()).Where(t => cabinetInputIDs.Select(i => i.LongID).Contains(t.CabinetInputID)).ToList();
                ToOldCabinetInputComboBox.ItemsSource = FromOldCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)OldCabinetComboBox.SelectedValue).Where(t => OldBucht.Select(b=>b.CabinetInputID).Contains(t.LongID));
             }
        }

        private void NewCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewCabinetComboBox.SelectedValue != null)
            {
                ToNewCabinetInputComboBox.ItemsSource = FromNewCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)NewCabinetComboBox.SelectedValue);
                List<int> postID = Data.PostDB.GetPostCheckableByCabinetID((int)NewCabinetComboBox.SelectedValue).Select(t => t.ID).ToList();
                newCabinetInputIDs = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)NewCabinetComboBox.SelectedValue);
                newConnectionID = Data.PostContactDB.GetPostContactByPostID(postID);
            }
        }
        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    if (OldBucht.Count == 0)
                    {
                        ShowErrorMessage("کافو انتخاب شده اتصالی ندارد", new Exception());
                        return;
                    }

                    List<Wiring> wiringList = new List<Wiring>();
                    Request request = Data.RequestDB.GetRequestByID(_requestID);
                    IssueWiring issueWiring = new IssueWiring();

                    issueWiring.InsertDate = DB.GetServerDate();
                    issueWiring.RequestID = _requestID;
                    issueWiring.WiringNo = _requestID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                    issueWiring.WiringTypeID = 0;
                    issueWiring.WiringIssueDate = DB.GetServerDate();
                    issueWiring.PrintCount = 0;
                    issueWiring.IsPrinted = false;
                    issueWiring.Status = 0;
                    issueWiring.Detach();
                    DB.Save(issueWiring);


                    for (int i = 0; i <= OldBucht.Count - 1; i++)
                    {
                        OldBucht[i].ConnectionID = newConnectionID[i].ID;
                        OldBucht[i].CabinetInputID = newCabinetInputIDs[i].LongID;
                        OldBucht[i].Detach();

                        Wiring wiring = new Wiring();
                        wiring.OldConnectionID = OldBucht[i].ConnectionID;
                        wiring.OldBuchtID = OldBucht[i].ID;
                        wiring.OldBuchtType = (byte)OldBucht[i].BuchtTypeID;
                        wiring.ConnectionID = newConnectionID[i].ID;
                        wiring.OldConnectionType = (byte)OldBucht[i].BuchtTypeID;
                        wiring.IssueWiringID = issueWiring.ID;
                        wiring.RequestID = _requestID;
                        wiring.Status = request.StatusID;
                        wiring.Detach();
                        wiringList.Add(wiring);


                    }
                    DB.UpdateAll(OldBucht);

                    item = this.DataContext as ExchangeCabinetInput;
                 

                    item.InsertDate = DB.GetServerDate();
                    item.ID = _requestID;
                    item.Detach();
                    DB.Save(item);

                    ts.Complete();
                    ShowSuccessMessage("عملیات برگردان انجام شد");
                }
            }
            catch(Exception ex)
            {
                ShowErrorMessage("برگردان انجام نشد", ex);
            }

        }

        private void ToOldCabinetInputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FromOldCabinetInputComboBox.SelectedValue != null)
            OldBucht = OldBucht.Where(t=>t.CabinetInputID >= (long)FromOldCabinetInputComboBox.SelectedValue && t.CabinetInputID <= (long)ToOldCabinetInputComboBox.SelectedValue).ToList();
        }
        private void ToNewCabinetInputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FromNewCabinetInputComboBox.SelectedValue != null )
            newCabinetInputIDs = newCabinetInputIDs.Where(t => t.LongID >= (long)FromNewCabinetInputComboBox.SelectedValue && t.LongID <= (long)ToNewCabinetInputComboBox.SelectedValue).ToList();
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }
        private void Load()
        {
            item = new ExchangeCabinetInput();
            this.DataContext = item;
        }
    }
}
