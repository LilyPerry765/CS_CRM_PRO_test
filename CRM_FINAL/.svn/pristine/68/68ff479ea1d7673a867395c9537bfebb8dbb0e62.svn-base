using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ModifyProfileFrom.xaml
    /// </summary>
    public partial class ModifyProfileFrom : Local.PopupWindow
    {
        #region Properties and Fields

        AssignmentInfo assignmentInfo = new AssignmentInfo();

        public static List<SwitchPrecode> switchPrecodeList { get; set; }

        public static List<SwitchPort> portList { get; set; }

        public static List<Telephone> telList { get; set; }

        public Customer Customer { get; set; }

        public Address InstallAddress { get; set; }

        public Address CorrespondenceAddress { get; set; }

        Telephone telephoneItem { get; set; }

        long telephonNo = 0;

        #endregion

        #region Constructor

        public ModifyProfileFrom()
        {
            InitializeComponent();
            Initialize();

        }

        #endregion

        #region Methods
        
        private void ResetForm()
        {
            TelephonTextBox.Text = string.Empty;
            NewInstallAddressTextBox.Text = string.Empty;
            NewInstallPostalCodeTextBox.Text = string.Empty;
            NewCorrespondenceAddressTextBox.Text = string.Empty;
            NewCorrespondencePostalCodeTextBox.Text = string.Empty;
            CorrespondenceAddress = null;
            InstallAddress = null;

            NationalCodeTextBox.Text = string.Empty;
            CustomerNameTextBox.Text = string.Empty;
            Customer = null;

            telephoneItem = null;
            assignmentInfo = null;
            TechInformationGroupBox.DataContext = null;
            TelephoneNoComboBox.SelectedItem = null;
            PortComboBox.SelectedItem = null;
            NewCabinetComboBox.ItemsSource = null;
            NewPostContactComboBox.ItemsSource = null;
            NewPostComboBox.ItemsSource = null;
            NewCabinetInputComboBox.ItemsSource = null;
            NewPostComboBox.ItemsSource = null;
            PreCodeTypeComboBox.SelectedIndex = -1;
            SwitchPreCodeComboBox.ItemsSource = null;

            ShowRoundTelephoneCheckBox.IsChecked = false;
        }

        private void Initialize()
        {
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
        }

        private void LoadData()
        {

        }

        private void RadioButtonChecked()
        {
            if (WithChangeCabinetInputRadioButton.IsChecked ?? false)
            {
                NewNameInformationGroupbox.Visibility = Visibility.Collapsed;
                NewTelephoneInformationGroupbox.Visibility = Visibility.Collapsed;
                NewTechInformationGroupBox.Visibility = Visibility.Visible;

                NewCabinetLabel.Visibility = Visibility.Visible;
                NewCabinetComboBox.Visibility = Visibility.Visible;

                NewCabinetInputLabel.Visibility = Visibility.Visible;
                NewCabinetInputComboBox.Visibility = Visibility.Visible;

                NewPostLabel.Visibility = Visibility.Visible;
                NewPostComboBox.Visibility = Visibility.Visible;

                NewPostContactLabel.Visibility = Visibility.Visible;
                NewPostContactComboBox.Visibility = Visibility.Visible;
                NewAddressInformationGroupbox.Visibility = Visibility.Collapsed;
            }
            else if (WithoutChangeCabinetInputRadioButton.IsChecked ?? false)
            {
                NewNameInformationGroupbox.Visibility = Visibility.Collapsed;
                NewTelephoneInformationGroupbox.Visibility = Visibility.Collapsed;
                NewTechInformationGroupBox.Visibility = Visibility.Visible;

                NewCabinetLabel.Visibility = Visibility.Collapsed;
                NewCabinetComboBox.Visibility = Visibility.Collapsed;

                NewCabinetInputLabel.Visibility = Visibility.Collapsed;
                NewCabinetInputComboBox.Visibility = Visibility.Collapsed;

                NewPostLabel.Visibility = Visibility.Visible;
                NewPostComboBox.Visibility = Visibility.Visible;

                NewPostContactLabel.Visibility = Visibility.Visible;
                NewPostContactComboBox.Visibility = Visibility.Visible;
                NewAddressInformationGroupbox.Visibility = Visibility.Collapsed;


                if (assignmentInfo != null && assignmentInfo.CabinetID != null)
                {
                    NewPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)assignmentInfo.CabinetID);
                }
            }
            else if (NameRadioButton.IsChecked ?? false)
            {
                NewTechInformationGroupBox.Visibility = Visibility.Collapsed;
                NewTelephoneInformationGroupbox.Visibility = Visibility.Collapsed;
                NewNameInformationGroupbox.Visibility = Visibility.Visible;
                NewAddressInformationGroupbox.Visibility = Visibility.Collapsed;
            }
            else if (AddressRadioButton.IsChecked ?? false)
            {
                NewTechInformationGroupBox.Visibility = Visibility.Collapsed;
                NewNameInformationGroupbox.Visibility = Visibility.Collapsed;
                NewTelephoneInformationGroupbox.Visibility = Visibility.Collapsed;
                NewAddressInformationGroupbox.Visibility = Visibility.Visible;
            }
            else if (TelephoneRadioButton.IsChecked ?? false)
            {
                NewTechInformationGroupBox.Visibility = Visibility.Collapsed;
                NewNameInformationGroupbox.Visibility = Visibility.Collapsed;
                NewAddressInformationGroupbox.Visibility = Visibility.Collapsed;
                NewTelephoneInformationGroupbox.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region EventHandlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SearchTelephone(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TelephonTextBox.Text.Trim()))
                {
                    if (!long.TryParse(TelephonTextBox.Text.Trim(), out telephonNo))
                        throw new Exception("تلفن وارد شده صحیح نمی باشد");

                    bool inWaitingList = false;
                    string requestName = Data.RequestDB.GetOpenRequestNameTelephone(new List<long>() { (long)telephonNo}, out inWaitingList);
                    if (!string.IsNullOrEmpty(requestName))
                    {
                        Folder.MessageBox.ShowError("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                    }
                    else
                    {
                        this.ResetForm();
                        TelephonTextBox.Text = telephonNo.ToString();
                        telephoneItem = Data.TelephoneDB.GetTelephoneByTelephoneNo(telephonNo);
                        if (telephoneItem != null && telephoneItem.Status == (int)DB.TelephoneStatus.Connecting)
                        {
                            assignmentInfo = DB.GetAllInformationByTelephoneNo(telephonNo);
                            if (assignmentInfo != null && !Helper.AllPropertyIsEmpty(assignmentInfo))
                            {
                                if (assignmentInfo.MUID != null)
                                {
                                    PCMInfoLabel.Visibility = Visibility.Visible;
                                    PCMInfoTextBox.Visibility = Visibility.Visible;
                                }

                                TechInformationGroupBox.DataContext = assignmentInfo;

                                NewCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(assignmentInfo.CenterID);
                            }
                            else
                            {
                                string warningMessage = string.Format("{2} {1} {0}", "موجود نیست", TelephonTextBox.Text.Trim(), ". اطلاعات شماره تلفن");
                                MessageBox.Show(warningMessage, "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                        }
                        else if (telephoneItem != null && telephoneItem.Status != (int)DB.TelephoneStatus.Connecting)
                        {
                            MessageBox.Show(".تلفن وارد شده، دایر نمی باشد", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show(".تلفن وارد شده، موجود نیست", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(".لطفاً فیلد شماره تلفن را پر نمائید", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    TelephonTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی تلفن - اصلاح مشخصات - بخش برگردان");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WithChangeCabinetInputRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButtonChecked();
        }

        private void WithoutChangeCabinetInputRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButtonChecked();
        }

        private void NameRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButtonChecked();
        }

        private void AddressRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButtonChecked();
        }

        private void TelephoneRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButtonChecked();
        }

        private void NewCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewCabinetComboBox.SelectedValue != null)
            {
                NewPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)NewCabinetComboBox.SelectedValue);
                NewCabinetInputComboBox.ItemsSource = Data.CabinetDB.GetFreeCabinetInputByCabinetID((int)NewCabinetComboBox.SelectedValue);
            }
        }

        private void NewPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewPostComboBox.SelectedValue != null)
            {
                NewPostContactComboBox.ItemsSource = Data.PostContactDB.GetFreePostContactByPostID((int)NewPostComboBox.SelectedValue);
            }
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    string message = string.Empty;
                    bool inWaitingList = false;
                    if (DB.HasRestrictionsTelphone(telephoneItem.TelephoneNo, out message, out inWaitingList))
                    {
                        throw new Exception(message);
                    }

                    if (DateDatePicker.SelectedDate == null)
                        throw new Exception("لطفا تاریخ را انتخاب کنید");

                    DateTime currentDateTime = DB.GetServerDate();


                    RequestLog requestLog = new RequestLog();
                    requestLog.IsReject = false;
                    requestLog.RequestTypeID = (int)DB.RequestType.ModifyProfile;
                    requestLog.TelephoneNo = (long)telephoneItem.TelephoneNo;

                    if (assignmentInfo != null && assignmentInfo.Customer != null)
                        requestLog.CustomerID = assignmentInfo.Customer.CustomerID;

                    requestLog.UserID = DB.currentUser.ID;
                    requestLog.Date = currentDateTime;

                    Data.Schema.ModifyProfile modifyProfile = new Data.Schema.ModifyProfile();
                    modifyProfile.Date = (DateTime)DateDatePicker.SelectedDate;

                    if (WithChangeCabinetInputRadioButton.IsChecked ?? false)
                    {

                        PostContact oldPostContact = Data.PostContactDB.GetPostContactByID((long)assignmentInfo.PostContactID);
                        Post oldPost = Data.PostDB.GetPostByID(oldPostContact.PostID);

                        PostContact NewPostContact = Data.PostContactDB.GetPostContactByID((long)NewPostContactComboBox.SelectedValue);
                        Post NewPost = Data.PostDB.GetPostByID(NewPostContact.PostID);


                        modifyProfile.OldCabinetID = assignmentInfo.CabinetID ?? 0;
                        modifyProfile.OldCabinet = assignmentInfo.CabinetName ?? 0;

                        modifyProfile.OldCabinetInputID = assignmentInfo.CabinetInputID ?? 0;
                        modifyProfile.OldCabinetInput = assignmentInfo.InputNumber ?? 0;

                        modifyProfile.OldPostContactID = oldPostContact.ID;
                        modifyProfile.OldPostContact = oldPostContact.ConnectionNo;
                        modifyProfile.OldPostID = oldPostContact.PostID;
                        modifyProfile.OldPost = oldPost.Number;

                        modifyProfile.NewPostContactID = NewPostContact.ID;
                        modifyProfile.NewPostContact = NewPostContact.ConnectionNo;
                        modifyProfile.NewPostID = NewPostContact.PostID;
                        modifyProfile.NewPost = NewPost.Number;

                        modifyProfile.OldBuchtID = (long)assignmentInfo.BuchtID;
                        modifyProfile.OldConnectionNo = assignmentInfo.Connection;

                        modifyProfile.OldCustomerID = assignmentInfo.Customer.ID;
                        modifyProfile.OldCustomerName = assignmentInfo.CustomerName;

                        modifyProfile.OldInstallAddressID = assignmentInfo.InstallAddress.ID;
                        modifyProfile.OldInstallContactAddress = assignmentInfo.InstallAddress.AddressContent;
                        modifyProfile.OldInstallPostalCodeInstall = assignmentInfo.InstallAddress.PostalCode;

                        modifyProfile.OldCorrespondenceAddressID = assignmentInfo.CorrespondenceAddress.ID;
                        modifyProfile.OldCorrespondenceContactAddress = assignmentInfo.CorrespondenceAddress.AddressContent;
                        modifyProfile.OldCorrespondencePostalCodeInstall = assignmentInfo.CorrespondenceAddress.PostalCode;

                        Bucht oldBucht = Data.BuchtDB.GetBuchetByID(assignmentInfo.BuchtID);


                        if (Data.ExchangeCabinetInputDB.IsSpecialWire(oldBucht.ID) && NewPostContact.ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                            throw new Exception("انتقال سیم خصوصی تنها بروی اتصالی معمولی امکان پذیر است.");

                        if (oldPostContact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal && NewPostContact.ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                        {

                            if (NewCabinetInputComboBox.SelectedValue == null) throw new Exception("مرکزی یافت نشد.");

                            Bucht newBucht = Data.BuchtDB.GetBuchtByCabinetInputID((long)NewCabinetInputComboBox.SelectedValue);



                            newBucht.SwitchPortID = oldBucht.SwitchPortID;
                            newBucht.ConnectionID = NewPostContact.ID;
                            newBucht.Status = (byte)DB.BuchtStatus.Connection;
                            newBucht.Detach();
                            DB.Save(newBucht);

                            modifyProfile.NewBuchtID = newBucht.ID;
                            modifyProfile.NewConnectionNo = DB.GetConnectionByBuchtID(newBucht.ID);
                            modifyProfile.NewCabinetInputID = (long)NewCabinetInputComboBox.SelectedValue;
                            modifyProfile.NewCabinetInput = Convert.ToInt64((NewCabinetInputComboBox.SelectedItem as CheckableItem).Name);
                            modifyProfile.NewCabinetID = (int)NewCabinetComboBox.SelectedValue;
                            modifyProfile.NewCabinet = Convert.ToInt32((NewCabinetComboBox.SelectedItem as CheckableItem).Name);
                            modifyProfile.NewPostID = (int)NewPostComboBox.SelectedValue;
                            modifyProfile.NewPost = Convert.ToInt32((NewPostComboBox.SelectedItem as CheckableItem).Name);
                            modifyProfile.NewPostContactID = (long)NewPostContactComboBox.SelectedValue;
                            modifyProfile.NewPostContact = Convert.ToInt32((NewPostContactComboBox.SelectedItem as CheckableItem).Name);

                            oldBucht.Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                            oldBucht.SwitchPortID = null;
                            oldBucht.Detach();
                            DB.Save(oldBucht);

                            NewPostContact.Status = oldPostContact.Status;
                            oldPostContact.Status = (byte)DB.PostContactStatus.Free;

                            oldPostContact.Detach();
                            DB.Save(oldPostContact);

                            NewPostContact.Detach();
                            DB.Save(NewPostContact);
                        }
                        else if (oldPostContact.ConnectionType == (byte)DB.PostContactConnectionType.Noraml && NewPostContact.ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                        {
                            if (NewCabinetInputComboBox.SelectedValue == null) throw new Exception("مرکزی یافت نشد.");

                            Bucht newBucht = Data.BuchtDB.GetBuchtByCabinetInputID((long)NewCabinetInputComboBox.SelectedValue);
                            modifyProfile.NewBuchtID = newBucht.ID;
                            modifyProfile.NewConnectionNo = DB.GetConnectionByBuchtID(newBucht.ID);
                            modifyProfile.NewCabinetInputID = (long)NewCabinetInputComboBox.SelectedValue;
                            modifyProfile.NewCabinetInput = Convert.ToInt64((NewCabinetInputComboBox.SelectedItem as CheckableItem).Name);
                            modifyProfile.NewCabinetID = (int)NewCabinetComboBox.SelectedValue;
                            modifyProfile.NewCabinet = Convert.ToInt32((NewCabinetComboBox.SelectedItem as CheckableItem).Name);
                            modifyProfile.NewPostID = (int)NewPostComboBox.SelectedValue;
                            modifyProfile.NewPost = Convert.ToInt32((NewPostComboBox.SelectedItem as CheckableItem).Name);
                            modifyProfile.NewPostContactID = (long)NewPostContactComboBox.SelectedValue;
                            modifyProfile.NewPostContact = Convert.ToInt32((NewPostContactComboBox.SelectedItem as CheckableItem).Name);

                            int switchPortID = (int)oldBucht.SwitchPortID;
                            bool aDSLStatus = oldBucht.ADSLStatus;

                            if (oldBucht.BuchtIDConnectedOtherBucht != null)
                            {
                                Bucht otherBucht = Data.BuchtDB.GetBuchetByID(oldBucht.BuchtIDConnectedOtherBucht);
                                otherBucht.BuchtIDConnectedOtherBucht = newBucht.ID;
                                otherBucht.Detach();
                                DB.Save(otherBucht);

                                newBucht.BuchtIDConnectedOtherBucht = otherBucht.ID;
                                oldBucht.BuchtIDConnectedOtherBucht = null;
                            }

                            oldBucht.Status = (byte)DB.BuchtStatus.Free;
                            oldBucht.ADSLStatus = false;
                            oldBucht.SwitchPortID = null;
                            oldBucht.ConnectionID = null;

                            oldBucht.Detach();
                            DB.Save(oldBucht);

                            newBucht.SwitchPortID = switchPortID;
                            newBucht.ConnectionID = NewPostContact.ID;
                            newBucht.ADSLStatus = aDSLStatus;
                            newBucht.Status = (byte)DB.BuchtStatus.Connection;
                            newBucht.Detach();
                            DB.Save(newBucht);

                            NewPostContact.Status = oldPostContact.Status;
                            oldPostContact.Status = (byte)DB.PostContactStatus.Free;

                            oldPostContact.Detach();
                            DB.Save(oldPostContact);

                            NewPostContact.Detach();
                            DB.Save(NewPostContact);
                        }
                        else if (oldPostContact.ConnectionType == (byte)DB.PostContactConnectionType.Noraml && NewPostContact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal)
                        {
                            throw new Exception("انتقال از اتصالی معمولی به پی سی ام بدون تغییر مرکزی صورت می گیرد");
                        }
                        else if (oldPostContact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal && NewPostContact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal)
                        {
                            throw new Exception("انتقال از پی سی ام به پی سی ام بدون تغییر مرکزی صورت می گیرد");
                        }


                    }
                    else if (WithoutChangeCabinetInputRadioButton.IsChecked ?? false)
                    {

                        PostContact oldPostContact = Data.PostContactDB.GetPostContactByID((long)assignmentInfo.PostContactID);
                        Post oldPost = Data.PostDB.GetPostByID(oldPostContact.PostID);

                        PostContact NewPostContact = Data.PostContactDB.GetPostContactByID((long)NewPostContactComboBox.SelectedValue);
                        Post NewPost = Data.PostDB.GetPostByID(NewPostContact.PostID);


                        modifyProfile.OldCabinetID = assignmentInfo.CabinetID ?? 0;
                        modifyProfile.OldCabinet = assignmentInfo.CabinetName ?? 0;

                        modifyProfile.OldCabinetInputID = assignmentInfo.CabinetInputID ?? 0;
                        modifyProfile.OldCabinetInput = assignmentInfo.InputNumber ?? 0;

                        modifyProfile.OldPostContactID = oldPostContact.ID;
                        modifyProfile.OldPostContact = oldPostContact.ConnectionNo;
                        modifyProfile.OldPostID = oldPostContact.PostID;
                        modifyProfile.OldPost = oldPost.Number;

                        modifyProfile.NewPostContactID = NewPostContact.ID;
                        modifyProfile.NewPostContact = NewPostContact.ConnectionNo;
                        modifyProfile.NewPostID = NewPostContact.PostID;
                        modifyProfile.NewPost = NewPost.Number;

                        modifyProfile.OldBuchtID = (long)assignmentInfo.BuchtID;
                        modifyProfile.OldConnectionNo = assignmentInfo.Connection;

                        modifyProfile.OldCustomerID = assignmentInfo.Customer.ID;
                        modifyProfile.OldCustomerName = assignmentInfo.CustomerName;

                        modifyProfile.OldInstallAddressID = assignmentInfo.InstallAddress.ID;
                        modifyProfile.OldInstallContactAddress = assignmentInfo.InstallAddress.AddressContent;
                        modifyProfile.OldInstallPostalCodeInstall = assignmentInfo.InstallAddress.PostalCode;

                        modifyProfile.OldCorrespondenceAddressID = assignmentInfo.CorrespondenceAddress.ID;
                        modifyProfile.OldCorrespondenceContactAddress = assignmentInfo.CorrespondenceAddress.AddressContent;
                        modifyProfile.OldCorrespondencePostalCodeInstall = assignmentInfo.CorrespondenceAddress.PostalCode;

                        Bucht oldBucht = Data.BuchtDB.GetBuchetByID(assignmentInfo.BuchtID);


                        if (Data.ExchangeCabinetInputDB.IsSpecialWire(oldBucht.ID) && NewPostContact.ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                            throw new Exception("انتقال سیم خصوصی تنها بروی اتصالی معمولی امکان پذیر است.");

                        if (oldPostContact.ConnectionType == (byte)DB.PostContactConnectionType.Noraml && NewPostContact.ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                        {

                            modifyProfile.NewPostID = (int)NewPostComboBox.SelectedValue;
                            modifyProfile.NewPost = Convert.ToInt32((NewPostComboBox.SelectedItem as CheckableItem).Name);
                            modifyProfile.NewPostContactID = (long)NewPostContactComboBox.SelectedValue;
                            modifyProfile.NewPostContact = Convert.ToInt32((NewPostContactComboBox.SelectedItem as CheckableItem).Name);

                            NewPostContact.Status = oldPostContact.Status;
                            oldPostContact.Status = (byte)DB.PostContactStatus.Free;

                            oldBucht.ConnectionID = NewPostContact.ID;

                            oldPostContact.Detach();
                            DB.Save(oldPostContact);

                            NewPostContact.Detach();
                            DB.Save(NewPostContact);

                            oldBucht.Detach();
                            DB.Save(oldBucht);
                        }
                        else if (oldPostContact.ConnectionType == (byte)DB.PostContactConnectionType.Noraml && NewPostContact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal)
                        {
                            Bucht newBucht = Data.BuchtDB.GetBuchtByConnectionID(NewPostContact.ID);

                            modifyProfile.NewBuchtID = newBucht.ID;
                            modifyProfile.NewConnectionNo = DB.GetConnectionByBuchtID(newBucht.ID);
                            modifyProfile.NewPostID = (int)NewPostComboBox.SelectedValue;
                            modifyProfile.NewPost = Convert.ToInt32((NewPostComboBox.SelectedItem as CheckableItem).Name);
                            modifyProfile.NewPostContactID = (long)NewPostContactComboBox.SelectedValue;
                            modifyProfile.NewPostContact = Convert.ToInt32((NewPostContactComboBox.SelectedItem as CheckableItem).Name);

                            newBucht.SwitchPortID = oldBucht.SwitchPortID;
                            newBucht.Status = oldBucht.Status;
                            newBucht.Detach();
                            DB.Save(newBucht);

                            oldBucht.ConnectionID = null;
                            oldBucht.ADSLStatus = false;
                            oldBucht.Status = (byte)DB.BuchtStatus.Free;
                            oldBucht.SwitchPortID = null;
                            oldBucht.Detach();
                            DB.Save(oldBucht);

                            NewPostContact.Status = oldPostContact.Status;
                            oldPostContact.Status = (byte)DB.PostContactStatus.Free;

                            oldPostContact.Detach();
                            DB.Save(oldPostContact);

                            NewPostContact.Detach();
                            DB.Save(NewPostContact);
                        }
                        else if (oldPostContact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal && NewPostContact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal)
                        {

                            Bucht newBucht = Data.BuchtDB.GetBuchtByConnectionID(NewPostContact.ID);

                            modifyProfile.NewBuchtID = newBucht.ID;
                            modifyProfile.NewConnectionNo = DB.GetConnectionByBuchtID(newBucht.ID);
                            modifyProfile.NewPostID = (int)NewPostComboBox.SelectedValue;
                            modifyProfile.NewPost = Convert.ToInt32((NewPostComboBox.SelectedItem as CheckableItem).Name);
                            modifyProfile.NewPostContactID = (long)NewPostContactComboBox.SelectedValue;
                            modifyProfile.NewPostContact = Convert.ToInt32((NewPostContactComboBox.SelectedItem as CheckableItem).Name);

                            newBucht.SwitchPortID = oldBucht.SwitchPortID;
                            newBucht.Status = oldBucht.Status;
                            newBucht.Detach();
                            DB.Save(newBucht);

                            oldBucht.Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                            oldBucht.SwitchPortID = null;
                            oldBucht.Detach();
                            DB.Save(oldBucht);

                            NewPostContact.Status = oldPostContact.Status;
                            oldPostContact.Status = (byte)DB.PostContactStatus.Free;

                            oldPostContact.Detach();
                            DB.Save(oldPostContact);

                            NewPostContact.Detach();
                            DB.Save(NewPostContact);

                        }
                        else if (oldPostContact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal && NewPostContact.ConnectionType == (byte)DB.PostContactConnectionType.Noraml)
                        {
                            throw new Exception("انتقال از پی سی ام به اتصالی معمولی فقط با تعیین ورودی امکان پذیر است");
                        }

                    }
                    else if (NameRadioButton.IsChecked ?? false)
                    {


                        if (telephoneItem == null)
                            throw new Exception("تلفن یافت نشد");

                        if (Customer == null)
                            throw new Exception("مشترک یافت نشد");


                        modifyProfile.NewCustomerID = Customer.ID;
                        modifyProfile.NewCustomerName = (Customer.FirstNameOrTitle ?? string.Empty) + " " + (Customer.LastName ?? string.Empty);

                        telephoneItem.CustomerID = Customer.ID;
                        telephoneItem.Detach();
                        DB.Save(telephoneItem);
                    }
                    else if (AddressRadioButton.IsChecked ?? false)
                    {


                        if (telephoneItem == null)
                            throw new Exception("تلفن یافت نشد");

                        if (InstallAddress != null)
                        {
                            telephoneItem.InstallAddressID = InstallAddress.ID;
                            modifyProfile.NewInstallAddressID = InstallAddress.ID;
                            modifyProfile.NewInstallContactAddress = InstallAddress.AddressContent;
                            modifyProfile.NewInstallPostalCodeInstall = InstallAddress.PostalCode;
                        }


                        if (CorrespondenceAddress != null)
                        {
                            telephoneItem.CorrespondenceAddressID = CorrespondenceAddress.ID;
                            modifyProfile.NewCorrespondenceAddressID = CorrespondenceAddress.ID;
                            modifyProfile.NewCorrespondenceContactAddress = CorrespondenceAddress.AddressContent;
                            modifyProfile.NewCorrespondencePostalCodeInstall = CorrespondenceAddress.PostalCode;
                        }
                        telephoneItem.Detach();
                        DB.Save(telephoneItem);
                    }
                    else if (TelephoneRadioButton.IsChecked ?? false)
                    {

                        if (telephoneItem == null)
                            throw new Exception("تلفن یافت نشد");


                        Telephone telItem = TelephoneNoComboBox.SelectedItem as Telephone;

                        if (telItem == null)
                            throw new Exception("تلفن یافت نشد");

                        SwitchPort portItem = PortComboBox.SelectedItem as SwitchPort;

                        if (portItem == null)
                            throw new Exception("پورت یافت نشد");


                        if (telItem.SwitchPortID != null && telItem.SwitchPortID != portItem.ID)
                        {
                            SwitchPort teleSwitchPort = Data.SwitchPortDB.GetSwitchPortByID((int)telItem.SwitchPortID);
                            teleSwitchPort.Status = (byte)DB.SwitchPortStatus.Free;
                            teleSwitchPort.Detach();
                            DB.Save(teleSwitchPort, false);

                            portItem.Status = (byte)DB.SwitchPortStatus.Install;
                            portItem.Detach();
                            DB.Save(portItem, false);

                            telItem.SwitchPortID = portItem.ID;
                            telItem.Detach();
                            DB.Save(telItem, false);
                        }


                        Telephone newTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)telItem.TelephoneNo);
                        Telephone oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)assignmentInfo.TelePhoneNo);

                        modifyProfile.OldTelephonNo = oldTelephone.TelephoneNo;
                        modifyProfile.NewTelephonNo = newTelephone.TelephoneNo;

                        List<Bucht> oldBuchts = Data.BuchtDB.GetBuchtsBySwitchPortID((int)oldTelephone.SwitchPortID);
                        oldBuchts.ForEach(t =>
                        {
                            t.SwitchPortID = newTelephone.SwitchPortID;
                            t.Detach();
                        });
                        DB.UpdateAll(oldBuchts);

                        //newTelephone.Status = (byte)DB.TelephoneStatus.Connecting;
                        //newTelephone.InstallationDate = currentDateTime;
                        //newTelephone.InitialInstallationDate = oldTelephone.InitialInstallationDate;
                        //newTelephone.CustomerID = oldTelephone.CustomerID;
                        //newTelephone.InstallAddressID = oldTelephone.InstallAddressID;
                        //newTelephone.CorrespondenceAddressID = oldTelephone.CorrespondenceAddressID;
                        //newTelephone.Detach();
                        //DB.Save(newTelephone);

                        //////////

                        //oldTelephone.Status = (byte)DB.TelephoneStatus.Discharge;
                        //oldTelephone.DischargeDate = currentDateTime;
                        //oldTelephone.CustomerID = null;
                        //oldTelephone.InstallAddressID = null;
                        //oldTelephone.CorrespondenceAddressID = null;
                        //oldTelephone.Detach();
                        //DB.Save(oldTelephone);
                        //////////

                        ///////////////////////
                        newTelephone.CustomerID = oldTelephone.CustomerID;
                        newTelephone.InstallationDate = currentDateTime;
                        newTelephone.InitialInstallationDate = oldTelephone.InitialInstallationDate;
                        newTelephone.DischargeDate = null;
                        newTelephone.CauseOfTakePossessionID = null; 
                        newTelephone.Status = oldTelephone.Status;
                        newTelephone.CauseOfCutID = oldTelephone.CauseOfCutID;
                        newTelephone.CustomerTypeID = oldTelephone.CustomerTypeID;
                        newTelephone.CustomerGroupID = oldTelephone.CustomerGroupID;
                        newTelephone.ChargingType = oldTelephone.ChargingType;
                        newTelephone.PosessionType = oldTelephone.PosessionType;
                        newTelephone.CutDate = oldTelephone.CutDate;
                        newTelephone.InstallAddressID = oldTelephone.InstallAddressID;
                        newTelephone.CorrespondenceAddressID = oldTelephone.CorrespondenceAddressID;
                        newTelephone.CustomerID = oldTelephone.CustomerID;
                        newTelephone.ClassTelephone = oldTelephone.ClassTelephone;
                        newTelephone.Detach();
                        DB.Save(newTelephone);
                        ///////////////////////
                        oldTelephone.DischargeDate = currentDateTime;
                        oldTelephone.Status = (int)DB.TelephoneStatus.Discharge;
                        oldTelephone.CustomerTypeID = null;
                        oldTelephone.InitialInstallationDate = null;
                        oldTelephone.CustomerGroupID = null;
                        oldTelephone.ChargingType = null;
                        oldTelephone.PosessionType = null;
                        oldTelephone.CauseOfCutID = null;
                        oldTelephone.InstallAddressID = null;
                        oldTelephone.CorrespondenceAddressID = null;
                        oldTelephone.CustomerID = null;
                        oldTelephone.ClassTelephone = (byte)DB.ClassTelephone.LimitLess;
                        oldTelephone.Detach();
                        DB.Save(oldTelephone);
                        ///////////////////////////

                        requestLog.ToTelephoneNo = newTelephone.TelephoneNo;

                    }

                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ModifyProfile>(modifyProfile, true));
                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);

                    ts.Complete();

                    ShowSuccessMessage("اصلاح مشخصات انجام شد");
                    SaveButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.ResetForm();
            SaveButton.IsEnabled = true;
        }

        private void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CustomerNameTextBox.Text = string.Empty;

                Customer = Data.CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
                if (Customer != null)
                {
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                }

                else
                {
                    CustomerForm customerForm = new CustomerForm();
                    customerForm.ShowDialog();
                    if (customerForm.DialogResult ?? false)
                    {
                        Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                    }
                }
            }
            else
            {
                CustomerForm customerForm = new CustomerForm();
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {
                    Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                }
            }
        }

        private void SearchInstallAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewInstallPostalCodeTextBox.Text.Trim()))
            {
                NewInstallAddressTextBox.Text = string.Empty;

                if (Data.AddressDB.GetAddressByPostalCodeCount(NewInstallPostalCodeTextBox.Text.Trim()) != 0)
                {
                    InstallAddress = Data.AddressDB.GetAddressByPostalCode(NewInstallPostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                    NewInstallAddressTextBox.Text = string.Empty;
                    NewInstallAddressTextBox.Text = InstallAddress.AddressContent;
                }

                else
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = NewInstallPostalCodeTextBox.Text.Trim();
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        InstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);

                        NewInstallPostalCodeTextBox.Text = string.Empty;
                        NewInstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                        NewInstallAddressTextBox.Text = string.Empty;
                        NewInstallAddressTextBox.Text = InstallAddress.AddressContent;
                    }
                }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = NewInstallPostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    InstallAddress = Data.AddressDB.GetAddressByID(customerAddressForm.ID);

                    NewInstallPostalCodeTextBox.Text = string.Empty;
                    NewInstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                    NewInstallAddressTextBox.Text = string.Empty;
                    NewInstallAddressTextBox.Text = InstallAddress.AddressContent;
                }
            }

        }

        private void SearchCorrespondenceAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewCorrespondencePostalCodeTextBox.Text.Trim()))
            {
                NewCorrespondenceAddressTextBox.Text = string.Empty;

                if (Data.AddressDB.GetAddressByPostalCodeCount(NewCorrespondencePostalCodeTextBox.Text.Trim()) != 0)
                {
                    CorrespondenceAddress = Data.AddressDB.GetAddressByPostalCode(NewCorrespondencePostalCodeTextBox.Text.Trim()).Take(1).SingleOrDefault();

                    NewCorrespondenceAddressTextBox.Text = string.Empty;
                    NewCorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                }

                else
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = NewCorrespondencePostalCodeTextBox.Text.Trim();
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        CorrespondenceAddress = Data.AddressDB.GetAddressByID(CorrespondenceAddress.ID);

                        NewCorrespondencePostalCodeTextBox.Text = string.Empty;
                        NewCorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                        NewCorrespondenceAddressTextBox.Text = string.Empty;
                        NewCorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                    }
                }
            }
            else
            {
                CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                customerAddressForm.PostallCode = NewCorrespondencePostalCodeTextBox.Text.Trim();
                customerAddressForm.ShowDialog();
                if (customerAddressForm.DialogResult ?? false)
                {
                    CorrespondenceAddress = Data.AddressDB.GetAddressByID(CorrespondenceAddress.ID);

                    NewCorrespondencePostalCodeTextBox.Text = string.Empty;
                    NewCorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                    NewCorrespondenceAddressTextBox.Text = string.Empty;
                    NewCorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;
                }
            }
        }

        //TODO:rad
        private void PrintCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!Helper.AllPropertyIsEmpty(this.assignmentInfo))
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void PrintCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TelephonTextBox.Text.Trim()) && Helper.AllCharacterIsNumber(TelephonTextBox.Text.Trim()))
                {
                    //در ابتدا باید لاگ ثبت شده مربوط به اصلاح مشخصات تلفن وارد شده را بدست بیاوریم
                    var result = RequestLogDB.GetRequestLogReport(long.Parse(TelephonTextBox.Text.Trim()), null, DB.RequestType.ModifyProfile);

                    if (result != null)
                    {
                        RequestLogReport requestLogReport = result as RequestLogReport;

                        //حالا با استفاده از لاگ ثبت شده ، دیتای مورد نیاز برای چاپ را ایجاد می نماییم
                        ModifyProfileRequestLog dataForPrint = new ModifyProfileRequestLog();
                        dataForPrint.OldTelephoneNo = (requestLogReport.TelephoneNo.HasValue) ? requestLogReport.TelephoneNo.Value.ToString() : string.Empty;
                        dataForPrint.NewTelephoneNo = (requestLogReport.ToTelephone.HasValue) ? requestLogReport.ToTelephone.Value.ToString() : string.Empty;
                        dataForPrint.UserName = requestLogReport.UserName;

                        //حالا باید ریز اطلاعات ، اصلاح مشخصات را بدست بیاوریم
                        if (requestLogReport.LogEntityDetails != null && requestLogReport.LogEntityDetails is Data.Schema.ModifyProfile)
                        {
                            Data.Schema.ModifyProfile modifyProfileDetails = requestLogReport.LogEntityDetails as Data.Schema.ModifyProfile;
                            dataForPrint.Date = modifyProfileDetails.Date.ToPersian(Date.DateStringType.Short);

                            //Old
                            //dataForPrint.OldBuchtID = modifyProfileDetails.OldBuchtID.ToString();
                            dataForPrint.OldAorBType = PostDB.GetAorBTypeByPostID(modifyProfileDetails.OldPostID);
                            dataForPrint.OldCabinet = modifyProfileDetails.OldCabinet.ToString();
                            //dataForPrint.OldCabinetID = modifyProfileDetails.OldCabinetID.ToString();
                            dataForPrint.OldCabinetInput = modifyProfileDetails.OldCabinetInput.ToString();
                            //dataForPrint.OldCabinetInputID = modifyProfileDetails.OldCabinetInputID.ToString();
                            dataForPrint.OldConnectionNo = modifyProfileDetails.OldConnectionNo;
                            //dataForPrint.OldCorrespondenceAddressID = modifyProfileDetails.OldCorrespondenceAddressID.ToString();
                            dataForPrint.OldCorrespondenceContactAddress = modifyProfileDetails.OldCorrespondenceContactAddress;
                            dataForPrint.OldCorrespondencePostalCodeInstall = modifyProfileDetails.OldCorrespondencePostalCodeInstall;
                            //dataForPrint.OldCustomerID = modifyProfileDetails.OldCustomerID.ToString();
                            dataForPrint.OldCustomerName = modifyProfileDetails.OldCustomerName;
                            //dataForPrint.OldInstallAddressID = modifyProfileDetails.OldInstallAddressID.ToString();
                            dataForPrint.OldInstallContactAddress = modifyProfileDetails.OldInstallContactAddress;
                            dataForPrint.OldInstallPostalCodeInstall = modifyProfileDetails.OldInstallPostalCodeInstall;
                            dataForPrint.OldPost = modifyProfileDetails.OldPost.ToString();
                            dataForPrint.OldPostContact = modifyProfileDetails.OldPostContact.ToString();
                            //dataForPrint.OldPostContactID = modifyProfileDetails.OldPostContactID.ToString();
                            //dataForPrint.OldPostID = modifyProfileDetails.OldPostID.ToString();

                            //New
                            //dataForPrint.NewBuchtID = modifyProfileDetails.NewBuchtID.ToString();
                            dataForPrint.NewAorBType = PostDB.GetAorBTypeByPostID(modifyProfileDetails.NewPostID);
                            dataForPrint.NewCabinet = modifyProfileDetails.NewCabinet.ToString();
                            //dataForPrint.NewCabinetID = modifyProfileDetails.NewCabinetID.ToString();
                            dataForPrint.NewCabinetInput = modifyProfileDetails.NewCabinetInput.ToString();
                            //dataForPrint.NewCabinetInputID = modifyProfileDetails.NewCabinetInputID.ToString();
                            dataForPrint.NewConnectionNo = modifyProfileDetails.NewConnectionNo;
                            //dataForPrint.NewCorrespondenceAddressID = modifyProfileDetails.NewCorrespondenceAddressID.ToString();
                            dataForPrint.NewCorrespondenceContactAddress = modifyProfileDetails.NewCorrespondenceContactAddress;
                            dataForPrint.NewCorrespondencePostalCodeInstall = modifyProfileDetails.NewCorrespondencePostalCodeInstall;
                            //dataForPrint.NewCustomerID = modifyProfileDetails.NewCustomerID.ToString();
                            dataForPrint.NewCustomerName = modifyProfileDetails.NewCustomerName;
                            //dataForPrint.NewInstallAddressID = modifyProfileDetails.NewInstallAddressID.ToString();
                            dataForPrint.NewInstallContactAddress = modifyProfileDetails.NewInstallContactAddress;
                            dataForPrint.NewInstallPostalCodeInstall = modifyProfileDetails.NewInstallPostalCodeInstall;
                            dataForPrint.NewPost = modifyProfileDetails.NewPost.ToString();
                            dataForPrint.NewPostContact = modifyProfileDetails.NewPostContact.ToString();
                            //dataForPrint.NewPostContactID = modifyProfileDetails.NewPostContactID.ToString();
                            dataForPrint.NewPostID = modifyProfileDetails.NewPostID.ToString();

                            //Check for null and empty values.
                            dataForPrint.CheckMembersValue();

                            //TODO:rad نیاز به اصلاح دارد
                            //در فکس 2 صفحه ای 13940210 خواسته بودند تا 
                            //A or B
                            //بودن پست مشخص شود  البته بهتر است این فیلد برای گزارش جداگانه ارسال شود نه این که همراه با شماره پست ارسال شود
                            dataForPrint.NewPost = string.Format("{0} ({1})", dataForPrint.NewPost, dataForPrint.NewAorBType);
                            dataForPrint.OldPost = string.Format("{0} ({1})", dataForPrint.OldPost, dataForPrint.OldAorBType);

                            //تنظیمات برای نمایش
                            StiVariable variable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                            List<ModifyProfileRequestLog> listForPrint = new List<ModifyProfileRequestLog> { dataForPrint };
                            ReportBase.SendToPrint(listForPrint, (int)DB.UserControlNames.ModifyProfileReport, variable);
                        }
                        else
                        {
                            MessageBox.Show("عدم دسترسی به جزئیات اصلاح مشخصات", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(".برای تلفن وارد شده هیچ گونه اصلاح مشخصاتی صورت نگرفته است", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(".لطفاً شماره تلفن را با دقت پر نمائید", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در کامند چاپ - اصلاح مشخصات - بخش برگردان");
                MessageBox.Show("خطا در چاپ", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region changeNumber

        private void PreCodeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PreCodeTypeComboBox.SelectedValue != null)
            {
                if (assignmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.OpticalCabinet)
                {
                    switchPrecodeList = Data.SwitchPrecodeDB.GetOpticalCabinetSwitchPrecodeByCenterIDAndType(assignmentInfo.CenterID, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), assignmentInfo.CabinetSwitchID);
                }
                else if (assignmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.WLL)
                {
                    switchPrecodeList = Data.SwitchPrecodeDB.GetOpticalCabinetSwitchPrecodeByCenterIDAndType(assignmentInfo.CenterID, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), assignmentInfo.CabinetSwitchID);
                }
                else
                {
                    if (telephoneItem.UsageType == null) throw new Exception();
                    switchPrecodeList = Data.SwitchPrecodeDB.GetSwitchPrecodeByCenterIDAndType(assignmentInfo.CenterID, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue) , telephoneItem.UsageType ?? -1);
                }
                SwitchPreCodeComboBox.ItemsSource = switchPrecodeList;
            }
            else
            {
                SwitchPreCodeComboBox.ItemsSource = null;
            }
        }

        private void SwitchPreCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SwitchPreCodeComboBox.SelectedValue != null)
            {

                SwitchPrecode switchPrecodeItem = switchPrecodeList.Where(s => s.ID == (int)SwitchPreCodeComboBox.SelectedValue).SingleOrDefault();
                if (switchPrecodeItem != null)
                {
                    TelephoneList();
                    // لیست پورت ها مربوط به سوییچ را تهیه میکند
                    portList = SwitchPortDB.GetSwitchPortsFreeOfOpticalBuchtBySwitch(switchPrecodeItem);
                    PortComboBox.ItemsSource = portList;
                }
            }
            else
            {
                TelephoneNoComboBox.ItemsSource = null;
                PortComboBox.ItemsSource = null;
            }
        }

        private void TelephoneList()
        {
            if (PreCodeTypeComboBox.SelectedValue != null && SwitchPreCodeComboBox.SelectedValue != null)
            {
                SwitchPrecode switchPrecodeItem = switchPrecodeList.Where(s => s.ID == (int)SwitchPreCodeComboBox.SelectedValue).SingleOrDefault();
                if (assignmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.OpticalCabinet)
                {
                    telList = TelephoneDB.GetOpticalCabinetFreeTelephoneBySwitchPreCodeNo(switchPrecodeItem, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), assignmentInfo.CabinetSwitchID, (bool)ShowRoundTelephoneCheckBox.IsChecked);
                }
                else if (assignmentInfo.CabinetUsageTypeID == (int)DB.CabinetUsageType.WLL)
                {
                    telList = TelephoneDB.GetOpticalCabinetFreeTelephoneBySwitchPreCodeNo(switchPrecodeItem, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), assignmentInfo.CabinetSwitchID, (bool)ShowRoundTelephoneCheckBox.IsChecked);
                }
                else
                {
                    telList = TelephoneDB.GetFreeTelephoneBySwitchPreCodeNoWithoutOptiacalBucht(switchPrecodeItem, Convert.ToInt32(PreCodeTypeComboBox.SelectedValue), telephoneItem.UsageType ?? -1, (bool)ShowRoundTelephoneCheckBox.IsChecked);
                }
                TelephoneNoComboBox.ItemsSource = telList;
            }
            else
            {
                TelephoneNoComboBox.ItemsSource = null;
            }
        }

        private void TelephoneNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TelephoneNoComboBox.SelectedValue != null)
            {
                Telephone telephone = telList.Where(t => t.TelephoneNo == (long)TelephoneNoComboBox.SelectedValue).Take(1).SingleOrDefault();
                // برسی ثابت یا شناور بودن پورت
                if (telephone != null)
                {
                    SwitchPort switchPort = Data.SwitchPortDB.GetSwitchPortByID((int)telephone.SwitchPortID);
                    if (switchPort != null)
                    {
                        // تازه سازی لیست پورت ها
                        portList = portList.Where(t => t.Status == (byte)DB.SwitchPortStatus.Free).ToList();
                        if (!portList.Any(t => t.ID == switchPort.ID)) portList.Add(switchPort);
                        PortComboBox.ItemsSource = portList;

                        this.PortComboBox.SelectedValue = switchPort.ID;
                        if (switchPort.Type == true)
                        {
                            PortComboBox.IsEnabled = false;
                        }
                        else if (switchPort.Type == false)
                        {
                            this.PortComboBox.SelectedValue = switchPort.ID;
                        }
                    }

                }
            }
        }

        #endregion changeNumber

    }
}
