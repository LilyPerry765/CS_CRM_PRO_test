using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for PCMTransferForm.xaml
    /// </summary>
    public partial class PCMTransferForm : Local.PopupWindow
    {
        #region Propertise
        BackgroundWorker backgrountWorker;
        List<PCMCardInfo> PCMCardInfos;
        int beforPostID = 0;

        private Data.Schema.PCMTransfer actionLogPCMTransfer = new Data.Schema.PCMTransfer();


        #endregion

        #region Constructor
        public PCMTransferForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            backgrountWorker = new BackgroundWorker();

            backgrountWorker.DoWork += new DoWorkEventHandler(backgroundWorkerDoWork);
            backgrountWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerComleted);

            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();

        }

        private void backgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            PCMCardInfos = Data.PCMTransferFormDB.Search(beforPostID);
        }

        private void backgroundWorkerComleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = PCMCardInfos;
        }
        private void BeforPostChange(int postID, int CabinetID)
        {

        }
        private void AfterPostChange(int postID, int CabinetID)
        {
          
        }
        #endregion
        
        #region Methode
        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            // by change PostID transfer PCM from post to other post
            PCMCardInfo transferPCMCardInfo = ItemsDataGrid.SelectedItem as PCMCardInfo;
            AssignmentInfo assignmentInfo = ConnectionDataGrid.SelectedItem as AssignmentInfo;
            if (transferPCMCardInfo != null && assignmentInfo != null)
            {

                List<PostContact> BeforPostContacts = Data.PostContactDB.GetPCMPostContactByPostContactID((long)transferPCMCardInfo.PostContactID);
                PostContact BeforPostContactsInline = BeforPostContacts.Where(t=>t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote).SingleOrDefault();
                List<PostContact> BeforPostContactsOutLine = BeforPostContacts.Where(t => t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal).ToList();
                PostContact AfterPostContact = Data.PostContactDB.GetPostContactByID(assignmentInfo.PostContactID ?? 0);
                if (AfterPostContact.Status != (byte)DB.PostContactStatus.Free || AfterPostContact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || AfterPostContact.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote) { MessageBox.Show("اتصالی انتخاب شده آزاد نمی باشد."); return; }

                BeforPostContactsOutLine.ForEach((PostContact item) => { item.PostID = AfterPostContact.PostID; item.ConnectionNo = AfterPostContact.ConnectionNo; item.Detach(); });

                var tempConnectionType = BeforPostContactsInline.ConnectionNo;
                var tempPostID = BeforPostContactsInline.PostID;
                BeforPostContactsInline.ConnectionNo = AfterPostContact.ConnectionNo;
                BeforPostContactsInline.PostID = AfterPostContact.PostID;
                BeforPostContactsInline.Detach();

                AfterPostContact.ConnectionNo = tempConnectionType;
                AfterPostContact.PostID = tempPostID;
                AfterPostContact.Detach();

                BeforPostContactsOutLine.Add(BeforPostContactsInline);
                BeforPostContactsOutLine.Add(AfterPostContact);
                using (TransactionScope ts = new TransactionScope())
                {
                    DB.UpdateAll(BeforPostContactsOutLine);

                    actionLogPCMTransfer.CenterID = transferPCMCardInfo.CenterID;
                    actionLogPCMTransfer.Center = transferPCMCardInfo.CenterName;
                    actionLogPCMTransfer.Rock = transferPCMCardInfo.RockNumber.ToString();
                    actionLogPCMTransfer.Shelf = transferPCMCardInfo.ShelfNumber.ToString();
                    actionLogPCMTransfer.Card = transferPCMCardInfo.Card.ToString();
                    actionLogPCMTransfer.Type = transferPCMCardInfo.PCMBrandName + " " + transferPCMCardInfo.PCMTypeName;
                    actionLogPCMTransfer.FromPostContact = transferPCMCardInfo.ConnectionNo.ToString();
                    actionLogPCMTransfer.FromCabinet = transferPCMCardInfo.CabinetNumber.ToString();
                    actionLogPCMTransfer.FromPost = transferPCMCardInfo.PostNumber.ToString();

                    actionLogPCMTransfer.ToPostContact = assignmentInfo.PostContact.ToString();
                    actionLogPCMTransfer.ToCabinet = assignmentInfo.CabinetName.ToString();
                    actionLogPCMTransfer.ToPost = assignmentInfo.PostName.ToString();


                    ActionLog actionLog = new ActionLog();
                    actionLog.ActionID = (byte)DB.ActionLog.PCMTransfer;
                    actionLog.UserName = Folder.User.Current.Username;
                    actionLogPCMTransfer.Date = actionLog.Date = DB.GetServerDate();
                    actionLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.PCMTransfer>(actionLogPCMTransfer, true));
                    actionLog.Detach();
                    DB.Save(actionLog);

                    ts.Complete();
                }
                ShowSuccessMessage("عملیات انجام شد.");
            }
            else
            {
                MessageBox.Show("لطفا اتصالی و پست را انتخاب کنید!","خطا",MessageBoxButton.OK,MessageBoxImage.Warning);
            }

        }

        #endregion

        #region Event
        private void PostComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (PostComboBox.SelectedValue != null)
            {
                beforPostID = (int)PostComboBox.SelectedValue;
                if (!backgrountWorker.IsBusy)
                    backgrountWorker.RunWorkerAsync();
            }
        }

        private void CabinetComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CabinetComboBox.SelectedValue != null)
                PostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)CabinetComboBox.SelectedValue);

        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
        }

        private void CenterComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                AfterCabinetComboBox.ItemsSource = CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID((int)CenterComboBox.SelectedValue);
            }
        }

        private void AfterCabinetComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (AfterCabinetComboBox.SelectedValue != null)
                AfterPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)CabinetComboBox.SelectedValue);
        }

        private void AfterPostComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (AfterPostComboBox.SelectedValue != null)
                ConnectionDataGrid.ItemsSource = DB.GetAllInformationByPostIDAndWithOutpostContactType((int)AfterPostComboBox.SelectedValue , (byte)DB.PostContactConnectionType.PCMRemote);
        }

        #endregion Event



    }
}
