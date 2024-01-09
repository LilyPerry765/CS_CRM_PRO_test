using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TranslationOpticalCabinetToNormalForm.xaml
    /// </summary>
    public partial class ExchangeCabinetForm : Local.RequestFormBase
    {
        #region Properties and Fields

        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }

        private long _requestID;

        int _centerID = 0;


        int _pastNewCabinetID = 0;

        int _pastOldCabinetID = 0;

        //  List<ExchangeCabinetConnectionInfo> cabinetInputsList = new List<ExchangeCabinetConnectionInfo>();

        public ObservableCollection<CheckableItem> _FromPost { get; set; }

        List<CRM.Data.ExchangeCabinetInputConncetion> _exchangeCabinetInputConncetion = new List<ExchangeCabinetInputConncetion>();

        public ObservableCollection<CheckableItem> _FromPostConntact { get; set; }

        public ObservableCollection<CheckableItem> _ToPost { get; set; }

        public ObservableCollection<CheckableItem> _ToPostConntact { get; set; }

        Request _reqeust { get; set; }

        ExchangeCabinetInput _exchangeCabinetInput { get; set; }

        ObservableCollection<CRM.Data.ExchangeCabinetInputConnectionInfo> _exchangeCabinetInputConncetionInfo { get; set; }
        List<CRM.Data.ExchangeCabinetInputConnectionInfo> _cloneOfExchangeCabinetInputConncetionInfo { get; set; }

        ObservableCollection<CRM.Data.TranslationOpticalCabinetToNormalPost> _exchangeCabinetInputConncetionPost { get; set; }

        #endregion

        #region Constructor

        public ExchangeCabinetForm()
        {
            InitializeComponent();
            Initialize();
        }
        public ExchangeCabinetForm(long requestID)
            : this()
        {
            _requestID = requestID;
        }

        #endregion

        #region Methods

        public void Initialize()
        {

        }

        public void LoadData()
        {
            if (Data.StatusDB.IsFinalStep(this.currentStat))
            {
                ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
            }
            else
            {
                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
            }

            _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_requestID);
            _exchangeRequestInfo.RequestType = (int)DB.RequestType.ExchangeCabinetInput;
            _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
            ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
            ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;
            _exchangeCabinetInputConncetionPost = new ObservableCollection<TranslationOpticalCabinetToNormalPost>();

            if (_requestID == 0)
            {
                _exchangeCabinetInput = new ExchangeCabinetInput();
                _exchangeCabinetInputConncetionInfo = new ObservableCollection<ExchangeCabinetInputConnectionInfo>();
                _cloneOfExchangeCabinetInputConncetionInfo = new List<ExchangeCabinetInputConnectionInfo>();
            }
            else
            {


                _exchangeCabinetInput = Data.ExchangeCabinetInputDB.GetExchangeCabinetInputByRequestID(_requestID);
                _reqeust = Data.RequestDB.GetRequestByID(_requestID);

                _exchangeCabinetInputConncetionInfo = new ObservableCollection<ExchangeCabinetInputConnectionInfo>(Data.ExchangeCabinetInputDB.GetExchangeCabinetInpuByRequestID(_requestID));
                _cloneOfExchangeCabinetInputConncetionInfo = _exchangeCabinetInputConncetionInfo.Select(t => (ExchangeCabinetInputConnectionInfo)t.Clone()).ToList();

                if (Data.StatusDB.IsFinalStep(this.currentStat))
                {
                    TelephoneDetailGroupBox.Visibility = Visibility.Visible;
                    TranslationTypeDetailGroupBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    _pastNewCabinetID = _exchangeCabinetInput.NewCabinetID;
                    _pastOldCabinetID = _exchangeCabinetInput.OldCabinetID;


                    OldCabinetComboBox.SelectedValue = _exchangeCabinetInput.OldCabinetID;
                    OldCabinetComboBox_SelectionChanged(null, null);

                    NewCabinetComboBox.SelectedValue = _exchangeCabinetInput.NewCabinetID;
                    NewCabinetComboBox_SelectionChanged(null, null);


                    if (_exchangeCabinetInput.Type == (byte)DB.ExchangeCabinetInputType.General)
                    {
                        GeneralTranslationRadioButton.IsChecked = true;
                    }
                    else if (_exchangeCabinetInput.Type == (byte)DB.ExchangeCabinetInputType.Slight)
                    {
                        SlightTranslationRadioButton.IsChecked = true;
                    }
                    else if (_exchangeCabinetInput.Type == (byte)DB.ExchangeCabinetInputType.Post)
                    {
                        PostTranslationRadioButton.IsChecked = true;
                        SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;
                    }
                    else if (_exchangeCabinetInput.Type == (byte)DB.ExchangeCabinetInputType.CabinetInput)
                    {
                        CabinetInputTranslationRadioButton.IsChecked = true;
                    }
                }
            }

            if (_exchangeCabinetInput.IsChangePost)
            {
                IsChangePostCheckBox_Checked(null, null);
            }
            else
            {
                IsChangePostCheckBox_Unchecked(null, null);
            }
            this.DataContext = _exchangeCabinetInput;
            SlightTranslationDataGrid.ItemsSource = _exchangeCabinetInputConncetionInfo;
            PostSelectorDataGrid.ItemsSource = _exchangeCabinetInputConncetionPost;
        }

        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            _centerID = centerID;
            List<CheckableItem> newCabinetInputList = Data.CabinetDB.GetCabinetCheckableByType(_centerID);
            List<CheckableItem> oldCabinetInputList = Data.CabinetDB.GetCabinetCheckableByType(_centerID);
            if (_pastNewCabinetID != 0)
            {
                Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(_pastNewCabinetID);
                newCabinetInputList.Add(new CheckableItem { ID = newCabinet.ID, Name = newCabinet.CabinetNumber.ToString(), IsChecked = false });
            }
            NewCabinetComboBox.ItemsSource = newCabinetInputList;

            if (_pastOldCabinetID != 0)
            {
                Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(_pastNewCabinetID);
                oldCabinetInputList.Add(new CheckableItem { ID = oldCabinet.ID, Name = oldCabinet.CabinetNumber.ToString(), IsChecked = false });
            }

            OldCabinetComboBox.ItemsSource = oldCabinetInputList;

        }

        private void SelectTranslationType()
        {
            if (PostTranslationRadioButton.IsChecked == true)
            {
                OldCabinetGroupBox.Visibility = Visibility.Visible;
                OldCabinetComboBox.Visibility = Visibility.Visible;
                OldCabinetLabel.Visibility = Visibility.Visible;

                NewCabinetGroupBox.Visibility = Visibility.Visible;
                NewCabinetComboBox.Visibility = Visibility.Visible;
                NewCabinetLabel.Visibility = Visibility.Visible;
                NewCabinetInputCountLabel.Visibility = Visibility.Visible;
                NewCabinetInputCountTextBox.Visibility = Visibility.Visible;


                OldCabinetInputCountLabel.Visibility = Visibility.Visible;
                OldCabinetInputCountTextBox.Visibility = Visibility.Visible;

                TransferWaitingListLabel.Visibility = Visibility.Visible;
                TransferWaitingListLabelCheckBox.Visibility = Visibility.Visible;


                SelectCabinetInput.Visibility = Visibility.Collapsed;
                PCMToNormalLabel.Visibility = Visibility.Collapsed;
                PCMToNormalCheckBox.Visibility = Visibility.Collapsed;
                TelephoneSearchGroupBox.Visibility = Visibility.Collapsed;
                PostSelectorGroupBox.Visibility = Visibility.Visible;

                FromOldCabinetInputLabel.Visibility = Visibility.Collapsed;
                FromOldCabinetInputComboBox.Visibility = Visibility.Collapsed;

                ToOldCabinetInputLabel.Visibility = Visibility.Collapsed;
                ToOldCabinetInputComboBox.Visibility = Visibility.Collapsed;


                FromNewCabinetInputLabel.Visibility = Visibility.Collapsed;
                FromNewCabinetInputComboBox.Visibility = Visibility.Collapsed;

                ToNewCabinetInputLabel.Visibility = Visibility.Collapsed;
                ToNewCabinetInputComboBox.Visibility = Visibility.Collapsed;
            }

            else if (SlightTranslationRadioButton.IsChecked == true)
            {
                OldCabinetGroupBox.Visibility = Visibility.Visible;
                OldCabinetComboBox.Visibility = Visibility.Visible;
                OldCabinetLabel.Visibility = Visibility.Visible;
                SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;
                PostSelectorGroupBox.Visibility = Visibility.Collapsed;


                NewCabinetGroupBox.Visibility = Visibility.Visible;
                NewCabinetComboBox.Visibility = Visibility.Visible;
                NewCabinetLabel.Visibility = Visibility.Visible;

                OldCabinetInputCountLabel.Visibility = Visibility.Visible;
                OldCabinetInputCountTextBox.Visibility = Visibility.Visible;


                TransferWaitingListLabel.Visibility = Visibility.Collapsed;
                TransferWaitingListLabelCheckBox.Visibility = Visibility.Collapsed;


                NewCabinetGroupBox.Visibility = Visibility.Visible;
                OldCabinetGroupBox.Visibility = Visibility.Collapsed;
                PCMToNormalLabel.Visibility = Visibility.Collapsed;
                PCMToNormalCheckBox.Visibility = Visibility.Collapsed;


                SelectCabinetInput.Visibility = Visibility.Collapsed;
                PCMToNormalLabel.Visibility = Visibility.Collapsed;
                PCMToNormalCheckBox.Visibility = Visibility.Collapsed;
                TelephoneSearchGroupBox.Visibility = Visibility.Visible;

                FromOldCabinetInputLabel.Visibility = Visibility.Collapsed;
                FromOldCabinetInputComboBox.Visibility = Visibility.Collapsed;

                ToOldCabinetInputLabel.Visibility = Visibility.Collapsed;
                ToOldCabinetInputComboBox.Visibility = Visibility.Collapsed;


                FromNewCabinetInputLabel.Visibility = Visibility.Collapsed;
                FromNewCabinetInputComboBox.Visibility = Visibility.Collapsed;

                ToNewCabinetInputLabel.Visibility = Visibility.Collapsed;
                ToNewCabinetInputComboBox.Visibility = Visibility.Collapsed;
            }
            else if (GeneralTranslationRadioButton.IsChecked == true)
            {
                #region OldCabinetGroupBox Children

                OldCabinetGroupBox.Visibility = Visibility.Visible;

                OldCabinetLabel.Visibility = Visibility.Visible;
                OldCabinetComboBox.Visibility = Visibility.Visible;

                OldCabinetInputCountLabel.Visibility = Visibility.Visible;
                OldCabinetInputCountTextBox.Visibility = Visibility.Visible;

                FromOldCabinetInputLabel.Visibility = Visibility.Collapsed;
                FromOldCabinetInputComboBox.Visibility = Visibility.Collapsed;

                TransferWaitingListLabel.Visibility = Visibility.Visible;
                TransferWaitingListLabelCheckBox.Visibility = Visibility.Visible;

                ToOldCabinetInputLabel.Visibility = Visibility.Collapsed;
                ToOldCabinetInputComboBox.Visibility = Visibility.Collapsed;

                #endregion

                #region NewCabinetGroupBox Children

                NewCabinetGroupBox.Visibility = Visibility.Visible;

                NewCabinetLabel.Visibility = Visibility.Visible;
                NewCabinetComboBox.Visibility = Visibility.Visible;

                NewCabinetInputCountLabel.Visibility = Visibility.Visible;
                NewCabinetInputCountTextBox.Visibility = Visibility.Visible;

                FromNewCabinetInputLabel.Visibility = Visibility.Collapsed;
                FromNewCabinetInputComboBox.Visibility = Visibility.Collapsed;

                ToNewCabinetInputLabel.Visibility = Visibility.Collapsed;
                ToNewCabinetInputComboBox.Visibility = Visibility.Collapsed;

                PCMToNormalLabel.Visibility = Visibility.Visible;
                PCMToNormalCheckBox.Visibility = Visibility.Visible;

                SelectCabinetInput.Visibility = Visibility.Visible;

                #endregion

                SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;
                TelephoneSearchGroupBox.Visibility = Visibility.Collapsed;
                PostSelectorGroupBox.Visibility = Visibility.Collapsed;
            }
            else if (CabinetInputTranslationRadioButton.IsChecked == true)
            {
                OldCabinetGroupBox.Visibility = Visibility.Visible;
                OldCabinetComboBox.Visibility = Visibility.Visible;
                OldCabinetLabel.Visibility = Visibility.Visible;

                NewCabinetGroupBox.Visibility = Visibility.Visible;
                NewCabinetComboBox.Visibility = Visibility.Visible;
                NewCabinetLabel.Visibility = Visibility.Visible;
                NewCabinetInputCountLabel.Visibility = Visibility.Visible;
                NewCabinetInputCountTextBox.Visibility = Visibility.Visible;


                OldCabinetInputCountLabel.Visibility = Visibility.Visible;
                OldCabinetInputCountTextBox.Visibility = Visibility.Visible;

                TransferWaitingListLabel.Visibility = Visibility.Visible;
                TransferWaitingListLabelCheckBox.Visibility = Visibility.Visible;


                FromOldCabinetInputLabel.Visibility = Visibility.Visible;
                FromOldCabinetInputComboBox.Visibility = Visibility.Visible;

                ToOldCabinetInputLabel.Visibility = Visibility.Visible;
                ToOldCabinetInputComboBox.Visibility = Visibility.Visible;


                FromNewCabinetInputLabel.Visibility = Visibility.Visible;
                FromNewCabinetInputComboBox.Visibility = Visibility.Visible;

                ToNewCabinetInputLabel.Visibility = Visibility.Visible;
                ToNewCabinetInputComboBox.Visibility = Visibility.Visible;


                SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;


                PCMToNormalLabel.Visibility = Visibility.Visible;
                PCMToNormalCheckBox.Visibility = Visibility.Visible;


                SelectCabinetInput.Visibility = Visibility.Visible;
                TelephoneSearchGroupBox.Visibility = Visibility.Collapsed;
                PostSelectorGroupBox.Visibility = Visibility.Collapsed;


            }

            this.ResizeWindow();
        }

        //private void ReserveCabinet(TranslationOpticalCabinetToNormal translationOpticalCabinetToNormal)
        //{
        //    using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
        //    {

        //        Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(translationOpticalCabinetToNormal.OldCabinetID);
        //        oldCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
        //        oldCabinet.Detach();

        //        Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(translationOpticalCabinetToNormal.NewCabinetID);
        //        newCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
        //        newCabinet.Detach();

        //        DB.UpdateAll(new List<Cabinet> { oldCabinet, newCabinet });

        //        ts3.Complete();
        //    }
        //}
        
        private string VerifyData(ExchangeCabinetInput exchangeCabinetInput)
        {

            string error = string.Empty;
            if (!exchangeCabinetInput.IsChangePost && exchangeCabinetInput.Type == (int)DB.ExchangeCabinetInputType.Slight)
            {
                error += string.Format("حالت جزئی بدون تغییر پست امکان پذیر نمی باشد");
            }

            if (exchangeCabinetInput.TransferWaitingList && exchangeCabinetInput.OldCabinetID == exchangeCabinetInput.NewCabinetID && !exchangeCabinetInput.IsChangePost)
            {
                error += string.Format("انتقال عدم امکانات فقط با تغییر کافو و تغییر پست صحیح می باشد");
            }


            if (exchangeCabinetInput.TransferBrokenPostContact && !exchangeCabinetInput.IsChangePost)
            {
                error += string.Format("در حالت بدون تغییر پست اتصالی های خراب ، روی پست باقی می مانند.");
            }


            Cabinet cabinet = CabinetDB.GetCabinetByID(exchangeCabinetInput.OldCabinetID);

            Cabinet NewCabinet = CabinetDB.GetCabinetByID(exchangeCabinetInput.NewCabinetID);

            if (!(cabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal && NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal))
            {
                if (cabinet.ID != NewCabinet.ID)
                {
                    error += string.Format("برگردان از کافو  " + Helper.GetEnumDescriptionByValue(typeof(DB.CabinetUsageType), cabinet.CabinetUsageType) + " به کافو " + Helper.GetEnumDescriptionByValue(typeof(DB.CabinetUsageType), NewCabinet.CabinetUsageType) + " امکان پذیر نمی باشد ");
                }
            }


            //if ((cabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal && (NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet || NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL))
            // || (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet && (NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal || NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL))
            // || (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL && (NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet || NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.Normal))
            // || (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet && NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet))
            //{
            //    error += string.Format("برگردان از کافو  " + Helper.GetEnumDescriptionByValue(typeof(DB.CabinetUsageType), cabinet.CabinetUsageType) + " به کافو " + Helper.GetEnumDescriptionByValue(typeof(DB.CabinetUsageType), NewCabinet.CabinetUsageType) + " امکان پذیر نمی باشد ");
            //}




            List<CRM.Data.ExchangeCabinetInputConnectionInfo> inputlist = (SlightTranslationDataGrid.ItemsSource as ObservableCollection<CRM.Data.ExchangeCabinetInputConnectionInfo>).ToList();

            if (inputlist.Count == 0)
            {
                error += string.Format("هیچ اتصالی انتخاب نشده است");
            }
            if (!exchangeCabinetInput.IsChangePost && exchangeCabinetInput.OldCabinetID != exchangeCabinetInput.NewCabinetID)
            {
                List<Bucht> AllBuchtConnectToOldPosts = Data.BuchtDB.GetBuchtByPostIDs(inputlist.Select(t => (int)t.FromPostID).Distinct().ToList());
                if (AllBuchtConnectToOldPosts.Any(t => !inputlist.Select(t2 => t2.FromPostContactID).Contains(t.ConnectionID)))
                {
                    error += string.Format("از میان اتصالی  پست های متصل به مرکزی های انتخاب شده اتصالی متصل به خارج از محدوده انتخاب شده وجود دارد");
                }
            }


            if (!exchangeCabinetInput.IsChangePost && exchangeCabinetInput.OldCabinetID != exchangeCabinetInput.NewCabinetID)
            {
                string returnedError = CabinetDB.ExistPostNumberInCabinet(inputlist.Select(t => (int)t.FromPostNumber).Distinct().ToList(), (int)exchangeCabinetInput.NewCabinetID);
                if (!string.IsNullOrEmpty(returnedError))
                {
                    error += string.Format("پست های {0} در کافو جدید وجود دارد", returnedError);
                }
            }

            if (inputlist.Any(t => t.ToCabinetInputID == null))
                error += string.Format("ورودی خالی نمی توان وارد کرد");

            if (exchangeCabinetInput.IsChangePost)
            {
                if (inputlist.Any(t => t.ToPostID == null))
                    error += string.Format("پست جدید خالی نمی توان وارد کرد");

                if (inputlist.Any(t => t.ToPostConntactID == null))
                    error += string.Format("اتصالی پست خالی نمی توان وارد کرد");


                if (inputlist.GroupBy(t => t.ToPostConntactID).Any(g => g.Count() > 1))
                    error += string.Format("اتصالی پست تکراری نمی توان وارد کرد");

            }



            if (inputlist.GroupBy(t => t.ToCabinetInputID).Any(g => g.Count() > 1))
                error += string.Format("ورودی تکراری نمی توان وارد کرد");

            if (inputlist.GroupBy(t => t.FromPostContactID).Any(g => g.Count() > 1))
                error += string.Format("اتصالی پست تکراری نمی توان وارد کرد");


            if (exchangeCabinetInput.ID == 0)
            {
                if (Data.TranslationOpticalCabinetToNormalDB.ExistPostCntactReserve(inputlist.Select(t => (long)t.FromPostContactID).ToList()))
                    error += string.Format("اتصالی پست رزرو می باشد");
            }

            return error;
        }

        #endregion

        #region Load Controls
        ComboBox FromPostComboBox;
        private void FromPostComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            FromPostComboBox = sender as ComboBox;
        }

        ComboBox FromPostConntactComboBox;
        private void FromPostConntactComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            FromPostConntactComboBox = sender as ComboBox;
        }

        ComboBox ToPostComboBox;
        private void ToPostComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ToPostComboBox = sender as ComboBox;
        }
        ComboBox ToPostConntactComboBox;
        private void ToPostConntactComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ToPostConntactComboBox = sender as ComboBox;
        }
        #endregion Load Controls

        #region EventHandlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SlightTranslationDelete(object sender, RoutedEventArgs e)
        {
            if (SlightTranslationDataGrid.SelectedItem != null)
            {
                _exchangeCabinetInputConncetionInfo.Remove(_exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex]);
            }
        }
        private void PostTranslationDetailDelete(object sender, RoutedEventArgs e)
        {
            if (PostSelectorDataGrid.SelectedItem != null)
            {
                (PostSelectorDataGrid.ItemsSource as ObservableCollection<TranslationOpticalCabinetToNormalPost>).Remove((PostSelectorDataGrid.ItemsSource as ObservableCollection<TranslationOpticalCabinetToNormalPost>)[PostSelectorDataGrid.SelectedIndex]);
            }
        }

        private void FromPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FromPostComboBox != null && FromPostComboBox.SelectedValue != null)
            {
                _FromPostConntact = new ObservableCollection<CheckableItem>(Data.PostContactDB.GetConnectionPostContactCheckableByPostID((int)FromPostComboBox.SelectedValue));

                _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].FromPostConntactNumber = null;
                _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].FromPostContactID = null;

                if (FromPostComboBox.SelectedItem != null)
                {

                    _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].FromPostNumber = Convert.ToInt32((FromPostComboBox.SelectedItem as CheckableItem).Name);
                    _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].FromPostID = Convert.ToInt32((FromPostComboBox.SelectedItem as CheckableItem).ID);
                    _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].FromAorBTypeName = Convert.ToString((FromPostComboBox.SelectedItem as CheckableItem).Description);

                }
            }
        }

        private void ToPostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToPostComboBox != null && ToPostComboBox.SelectedValue != null)
            {

                _ToPostConntact = new ObservableCollection<CheckableItem>(Data.PostContactDB.GetFreePostContactCheckableByPostID((int)ToPostComboBox.SelectedValue));
                _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].ToPostConntactNumber = null;
                _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].ToPostConntactID = null;

                if (ToPostComboBox.SelectedItem != null)
                {

                    _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].ToPostNumber = Convert.ToInt32((ToPostComboBox.SelectedItem as CheckableItem).Name);
                    _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].ToPostID = Convert.ToInt32((ToPostComboBox.SelectedItem as CheckableItem).ID);
                    _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].ToAorBTypeName = Convert.ToString((ToPostComboBox.SelectedItem as CheckableItem).Description);
                }
            }
        }

        private void FromPostConntactComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FromPostConntactComboBox.SelectedItem != null && FromPostConntactComboBox.SelectedValue != null)
            {

                _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].FromPostConntactNumber = Convert.ToInt32((FromPostConntactComboBox.SelectedItem as CheckableItem).Name);
                _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].FromPostContactID = Convert.ToInt32((FromPostConntactComboBox.SelectedItem as CheckableItem).LongID);
            }

        }

        private void ToPostConntactComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToPostConntactComboBox.SelectedItem != null && ToPostConntactComboBox.SelectedValue != null)
            {


                _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].ToPostConntactNumber = Convert.ToInt32((ToPostConntactComboBox.SelectedItem as CheckableItem).Name);
                _exchangeCabinetInputConncetionInfo[SlightTranslationDataGrid.SelectedIndex].ToPostConntactID = Convert.ToInt32((ToPostConntactComboBox.SelectedItem as CheckableItem).LongID);

            }

        }

        private void OldCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OldCabinetComboBox.SelectedValue != null)
            {

                ToOldCabinetInputComboBox.ItemsSource = FromOldCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)OldCabinetComboBox.SelectedValue);
                OldCabinetInputCountTextBox.Text = Data.CabinetInputDB.GetCabinetInputCountByCabinetID((int)OldCabinetComboBox.SelectedValue).ToString();

                _FromPost = new ObservableCollection<CheckableItem>(Data.PostDB.GetPostCheckableByCabinetID((int)OldCabinetComboBox.SelectedValue));
                FromPostColumn.ItemsSource = _FromPost;

                ClearPostSelectorDataGrid();
            }
        }

        private void ClearPostSelectorDataGrid()
        {
            PostSelectorDataGrid.ItemsSource = new ObservableCollection<TranslationOpticalCabinetToNormalPost>();
        }

        private void NewCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewCabinetComboBox.SelectedValue != null)
            {
                ToNewCabinetInputComboBox.ItemsSource = FromNewCabinetInputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID((int)NewCabinetComboBox.SelectedValue);

                NewCabinetInputCountTextBox.Text = Data.CabinetInputDB.GetCabinetInputCountByCabinetID((int)NewCabinetComboBox.SelectedValue).ToString();

                List<CheckableItem> CabinetInputs = Data.CabinetDB.GetFreeCabinetInputByCabinetID((int)NewCabinetComboBox.SelectedValue);
                if (_exchangeCabinetInputConncetionInfo.Count != 0)
                {
                    List<CheckableItem> cabinetInputs = CabinetInputDB.GetCabinetInputChechableByID(_exchangeCabinetInputConncetionInfo.Select(t => t.ToCabinetInputID ?? 0).ToList());
                    CabinetInputs.AddRange(cabinetInputs);
                }

                ToCabinetInputColumn.ItemsSource = CabinetInputs;

                _ToPost = new ObservableCollection<CheckableItem>(Data.PostDB.GetPostCheckableByCabinetID((int)NewCabinetComboBox.SelectedValue));

                ToPostColumn.ItemsSource = _ToPost;

                ClearPostSelectorDataGrid();
            }
        }

        private void GeneralTranslationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectTranslationType();
        }

        private void SlightTranslationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectTranslationType();
        }

        private void PostTranslationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectTranslationType();
        }

        #endregion

        #region ActionuserControl Methods :Save,Print,Forward,...

        public override bool Print()
        {
            try
            {
                if (this._requestID >= 0)
                {
                    List<long> requestsId = new List<long> { this._requestID };

                    var result = ReportDB.GetTranslationOpticalCabinetToNormalReport(requestsId);

                    StiVariable dateVarible = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));

                    ReportBase.SendToPrint(result, (int)DB.UserControlNames.TranslationOpticalCabinetToNormalReport, dateVarible);
                }
                IsPrintSuccess = true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در گزارش چاپ گواهی برای برگردان کافو نوری به کافو معمولی");
                MessageBox.Show("خطا در چاپ", "", MessageBoxButton.OK, MessageBoxImage.Error);
                IsPrintSuccess = false;
            }

            return IsPrintSuccess;
        }

        public override bool Save()
        {
            //if (!Codes.Validation.WindowIsValid.IsValid(this))
            //{
            //    IsSaveSuccess = false;
            //    return false;
            //}
            try
            {


                if (Data.StatusDB.IsFinalStep(this.currentStat))
                {
                    IsSaveSuccess = true;

                }
                else
                {
                    _reqeust = _exchangeRequestInfo.Request;
                    _exchangeCabinetInput = this.DataContext as ExchangeCabinetInput;

                    if (GeneralTranslationRadioButton.IsChecked == true)
                    {
                        _exchangeCabinetInput.Type = (byte)DB.ExchangeCabinetInputType.General;
                        _exchangeCabinetInput.FromNewCabinetInputID = null;
                        _exchangeCabinetInput.FromOldCabinetInputID = null;
                        _exchangeCabinetInput.ToNewCabinetInputID = null;
                        _exchangeCabinetInput.ToOldCabinetInputID = null;
                    }
                    else if (SlightTranslationRadioButton.IsChecked == true)
                    {
                        _exchangeCabinetInput.Type = (byte)DB.ExchangeCabinetInputType.Slight;
                        _exchangeCabinetInput.FromNewCabinetInputID = null;
                        _exchangeCabinetInput.FromOldCabinetInputID = null;
                        _exchangeCabinetInput.ToNewCabinetInputID = null;
                        _exchangeCabinetInput.ToOldCabinetInputID = null;
                        _exchangeCabinetInput.TransferWaitingList = false;
                    }
                    else if (PostTranslationRadioButton.IsChecked == true)
                    {
                        _exchangeCabinetInput.Type = (byte)DB.ExchangeCabinetInputType.Post;
                    }
                    else if (CabinetInputTranslationRadioButton.IsChecked == true)
                    {
                        _exchangeCabinetInput.Type = (byte)DB.ExchangeCabinetInputType.CabinetInput;
                    }


                    // Verify data
                    string error = VerifyData(_exchangeCabinetInput);

                    if (string.IsNullOrEmpty(error))
                    {

                        List<AssignmentInfo> asignmentInfos = DB.GetAllInformationPostContactIDs(_exchangeCabinetInputConncetionInfo.Select(t => (long)t.FromPostContactID).ToList())
                                                               .Where(t => t.BuchtType != (int)DB.BuchtType.InLine && t.BuchtType != (int)DB.BuchtType.OutLine).ToList();
                        List<Bucht> cabinetInputBucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(_exchangeCabinetInputConncetionInfo.Select(t => (long)t.ToCabinetInputID).ToList());

                        using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
                        {

                            if (_exchangeCabinetInputConncetionInfo.Count() == 1)
                            {

                                AssignmentInfo asignmentInfo = asignmentInfos[0];

                                if (_reqeust.TelephoneNo != asignmentInfo.TelePhoneNo)
                                {

                                    bool inWaitingList = false;
                                    string requestName = Data.RequestDB.GetOpenRequestNameTelephone(asignmentInfos.Select(t => (long)t.TelePhoneNo).ToList(), out inWaitingList);
                                    if (!string.IsNullOrEmpty(requestName))
                                    {
                                        Folder.MessageBox.ShowError("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                                        throw new Exception("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                                    }
                                    else
                                    {
                                        _reqeust.TelephoneNo = asignmentInfo.TelePhoneNo;
                                    }
                                }


                            }
                            else
                            {

                                List<long> newTelephones = asignmentInfos.Where(t => !_cloneOfExchangeCabinetInputConncetionInfo.Select(t2 => t2.FromPostContactID).Contains(t.PostContactID)).Select(t => t.TelePhoneNo ?? 0).ToList();
                                newTelephones = newTelephones.Where(t => t != 0).ToList();
                                bool inWaitingList = false;
                                string requestName = Data.RequestDB.GetOpenRequestNameTelephone(newTelephones, out inWaitingList);
                                if (!string.IsNullOrEmpty(requestName))
                                {
                                    Folder.MessageBox.ShowError("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                                    throw new Exception("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                                }

                                _reqeust.TelephoneNo = null;
                            }

                            // cabinetInputsList = Data.TranslationOpticalCabinetToNormalDB.GetEquivalentCabinetInputs(_translationOpticalCabinetToNormal);
                            // Save reqeust
                            if (_requestID == 0)
                            {
                                // save telephone in reqeust when one Connection selected



                                _reqeust.ID = DB.GenerateRequestID();
                                _reqeust.RequestPaymentTypeID = 0;
                                _reqeust.IsViewed = false;
                                _reqeust.InsertDate = DB.GetServerDate();
                                _reqeust.StatusID = DB.GetStatus((int)DB.RequestType.ExchangeCabinetInput, (int)DB.RequestStatusType.Start).ID; // Get Start Status
                                _reqeust.Detach();
                                DB.Save(_reqeust, true);

                                _exchangeCabinetInput.ID = _reqeust.ID;

                                _exchangeCabinetInput.Detach();
                                DB.Save(_exchangeCabinetInput, true);

                                _requestID = _reqeust.ID;
                            }
                            else
                            {



                                if (_cloneOfExchangeCabinetInputConncetionInfo != null)
                                {
                                    List<Bucht> buchts = BuchtDB.GetBuchtByCabinetInputIDs(_cloneOfExchangeCabinetInputConncetionInfo.Select(t => (long)t.ToCabinetInputID).ToList());
                                    buchts.ForEach(t => { t.Status = (int)DB.BuchtStatus.Free; t.Detach(); });
                                    DB.UpdateAll(buchts);
                                }


                                if (_cloneOfExchangeCabinetInputConncetionInfo != null)
                                {
                                    List<PostContact> toPostContacts = PostContactDB.GetPostContactByIDs(_cloneOfExchangeCabinetInputConncetionInfo.Where(t => t.ToPostConntactID != null).Select(t => (long)t.ToPostConntactID).ToList());
                                    toPostContacts.ForEach(t => { t.Status = (int)DB.PostContactStatus.Free; t.Detach(); });
                                    DB.UpdateAll(toPostContacts);
                                }


                                Data.ExchangeCabinetInputDB.DeleteExchangeCabinetInputByRequestID(_exchangeCabinetInput.ID);

                                _reqeust.Detach();
                                DB.Save(_reqeust, false);

                                _exchangeCabinetInput.Detach();
                                DB.Save(_exchangeCabinetInput, false);
                            }



                            if (GeneralTranslationRadioButton.IsChecked == true)
                            {
                                if (_pastOldCabinetID != 0 && _pastOldCabinetID != _exchangeCabinetInput.OldCabinetID)
                                {
                                    Cabinet oldPastCabinet = Data.CabinetDB.GetCabinetByID(_pastOldCabinetID);
                                    oldPastCabinet.Status = (int)DB.CabinetStatus.Install;
                                    oldPastCabinet.Detach();
                                }
                                Cabinet oldCabinet = Data.CabinetDB.GetCabinetByID(_exchangeCabinetInput.OldCabinetID);
                                oldCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                                oldCabinet.Detach();


                                if (_pastNewCabinetID != 0 && _pastNewCabinetID != _exchangeCabinetInput.NewCabinetID)
                                {
                                    Cabinet newPastCabinet = Data.CabinetDB.GetCabinetByID(_pastOldCabinetID);
                                    newPastCabinet.Status = (int)DB.CabinetStatus.Install;
                                    newPastCabinet.Detach();
                                }
                                Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(_exchangeCabinetInput.NewCabinetID);
                                newCabinet.Status = (int)DB.CabinetStatus.ExchangeCabinetInput;
                                newCabinet.Detach();

                                DB.UpdateAll(new List<Cabinet> { oldCabinet, newCabinet });

                            }

                            _exchangeCabinetInputConncetionInfo.ToList().ForEach(item =>
                            {
                                CRM.Data.ExchangeCabinetInputConncetion exchangeCabinetInputConncetion = new ExchangeCabinetInputConncetion();
                                AssignmentInfo asignmentInfo = asignmentInfos.Find(t => t.PostContactID == item.FromPostContactID);
                                if (asignmentInfo == null) throw new Exception("اطلاعات مشترک یافت نشد");
                                exchangeCabinetInputConncetion.FromTelephoneNo = asignmentInfo.TelePhoneNo;
                                exchangeCabinetInputConncetion.FromCabinetInputID = asignmentInfo.InputNumberID;
                                exchangeCabinetInputConncetion.FromBucht = asignmentInfo.BuchtID;
                                if (asignmentInfo.Customer != null)
                                {
                                    exchangeCabinetInputConncetion.CustomerID = asignmentInfo.Customer.ID;
                                }
                                if (asignmentInfo.InstallAddress != null)
                                {
                                    exchangeCabinetInputConncetion.InstallAddressID = asignmentInfo.InstallAddress.ID;
                                }
                                if (asignmentInfo.CorrespondenceAddress != null)
                                {
                                    exchangeCabinetInputConncetion.CorrespondenceAddressID = asignmentInfo.CorrespondenceAddress.ID;
                                }

                                exchangeCabinetInputConncetion.RequestID = _reqeust.ID;
                                exchangeCabinetInputConncetion.FromPostID = (int)item.FromPostID;
                                exchangeCabinetInputConncetion.FromPostContactID = (long)item.FromPostContactID;
                                if (_exchangeCabinetInput.IsChangePost)
                                {
                                    exchangeCabinetInputConncetion.ToPostID = (int)item.ToPostID;
                                    exchangeCabinetInputConncetion.ToPostConntactID = (long)item.ToPostConntactID;
                                    exchangeCabinetInputConncetion.ToCabinetInputID = (long)item.ToCabinetInputID;
                                }

                                exchangeCabinetInputConncetion.ToCabinetInputID = (long)item.ToCabinetInputID;
                                exchangeCabinetInputConncetion.ToBucht = cabinetInputBucht.Find(t => t.CabinetInputID == (long)item.ToCabinetInputID && t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine).ID;
                                exchangeCabinetInputConncetion.Detach();
                                _exchangeCabinetInputConncetion.Add(exchangeCabinetInputConncetion);

                            });



                            cabinetInputBucht.ForEach(t => { t.Status = (int)DB.BuchtStatus.Reserve; t.Detach(); });
                            DB.UpdateAll(cabinetInputBucht);

                            if (_exchangeCabinetInput.IsChangePost)
                            {
                                List<PostContact> ReserveToPostContacts = PostContactDB.GetPostContactByIDs(_exchangeCabinetInputConncetion.Select(t => (long)t.ToPostConntactID).ToList());
                                if (ReserveToPostContacts.Any(t => t.Status == (int)DB.PostContactStatus.FullBooking))
                                {
                                    throw new Exception("اتصالی پست رزرو می باشد");
                                }
                                ReserveToPostContacts.ForEach(t => { t.Status = (int)DB.PostContactStatus.FullBooking; t.Detach(); });
                                DB.UpdateAll(ReserveToPostContacts);
                            }

                            DB.SaveAll(_exchangeCabinetInputConncetion);
                            ts2.Complete();
                            IsSaveSuccess = true;
                            ShowSuccessMessage("ذخیره انجام شد");

                        }

                    }
                    else
                    {
                        IsSaveSuccess = false;
                        throw new Exception(error);
                    }
                }
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("ذخیره برگردان کافو انجام نشد .", ex);
            }
            return IsSaveSuccess;
        }

        public override bool Forward()
        {
            //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
            Save();

            this.RequestID = _reqeust.ID;

            if (IsSaveSuccess == true)
            {
                IsForwardSuccess = true;
            }

            //    ts.Complete();
            //}
            return IsForwardSuccess;
        }

        public override bool Cancel()
        {
            try
            {

                using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (_cloneOfExchangeCabinetInputConncetionInfo != null)
                    {
                        List<Bucht> buchts = BuchtDB.GetBuchtByCabinetInputIDs(_cloneOfExchangeCabinetInputConncetionInfo.Select(t => (long)t.ToCabinetInputID).ToList());
                        buchts.ForEach(t => { t.Status = (int)DB.BuchtStatus.Free; t.Detach(); });
                        DB.UpdateAll(buchts);
                    }


                    if (_cloneOfExchangeCabinetInputConncetionInfo != null)
                    {
                        List<PostContact> toPostContacts = PostContactDB.GetPostContactByIDs(_cloneOfExchangeCabinetInputConncetionInfo.Select(t => (long)t.ToPostConntactID).ToList());
                        toPostContacts.ForEach(t => { t.Status = (int)DB.PostContactStatus.Free; t.Detach(); });
                        DB.UpdateAll(toPostContacts);
                    }



                    _reqeust.IsCancelation = true;

                    Data.CancelationRequestList cancelationRequest = new CancelationRequestList();

                    cancelationRequest.ID = _reqeust.ID;
                    cancelationRequest.EntryDate = DB.GetServerDate();
                    cancelationRequest.UserID = Folder.User.Current.ID;

                    cancelationRequest.Detach();
                    DB.Save(cancelationRequest, true);

                    _reqeust.Detach();
                    DB.Save(_reqeust);

                    ts3.Complete();

                }
                IsCancelSuccess = true;

                ShowSuccessMessage("ابطال درخواست انجام شد");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در رد درخواست، " + ex.Message + " !", ex);
            }

            return IsCancelSuccess;
        }

        #endregion

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExchangeCabinetInputConnectionInfo tempExchangeCabinetInputConnectionInfo = new ExchangeCabinetInputConnectionInfo();
                if (!string.IsNullOrEmpty(TelephonTextBox.Text.Trim()))
                {
                    long telephonNo = 0;
                    if (!long.TryParse(TelephonTextBox.Text.Trim(), out telephonNo))
                        throw new Exception("تلفن وارد شده صحیح نمی باشد");


                    string message = string.Empty;
                    bool inWaitingList = false;
                    if (DB.HasRestrictionsTelphone(telephonNo, out message, out inWaitingList))
                        throw new Exception(message);


                    Telephone telephoneItem = Data.TelephoneDB.GetTelephoneByTelephoneNo(telephonNo);
                    if (telephoneItem != null)
                    {

                        AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo(telephonNo);

                        if (assignmentInfo != null)
                        {
                            if (assignmentInfo.CenterID != (int)_exchangeRequestInfo.CenterComboBox.SelectedValue)
                            {
                                Folder.MessageBox.ShowError(".تلفن وارد شده، مربوط به این مرکز نمی باشد");
                            }
                            else
                            {

                                OldCabinetComboBox.SelectedValue = assignmentInfo.CabinetID;
                                OldCabinetComboBox_SelectionChanged(null, null);
                                tempExchangeCabinetInputConnectionInfo.FromPostID = assignmentInfo.PostID;
                                tempExchangeCabinetInputConnectionInfo.FromPostNumber = assignmentInfo.PostName;
                                tempExchangeCabinetInputConnectionInfo.FromPostContactID = assignmentInfo.PostContactID;
                                tempExchangeCabinetInputConnectionInfo.FromPostConntactNumber = assignmentInfo.PostContact;
                                tempExchangeCabinetInputConnectionInfo.FromAorBTypeName = assignmentInfo.AorBType;
                                tempExchangeCabinetInputConnectionInfo.FromAorBType = assignmentInfo.AorBTypeID;
                                tempExchangeCabinetInputConnectionInfo.FromConnectiontype = (assignmentInfo.PCMPortIDInBuchtTable != null ? "پی سی ام" : "معمولی");
                                _exchangeCabinetInputConncetionInfo = new ObservableCollection<ExchangeCabinetInputConnectionInfo>(new List<ExchangeCabinetInputConnectionInfo> { tempExchangeCabinetInputConnectionInfo });
                                SlightTranslationDataGrid.ItemsSource = _exchangeCabinetInputConncetionInfo;
                            }

                        }
                        else
                        {
                            Folder.MessageBox.ShowError(string.Format("{2} {1} {0}", "موجود نیست", TelephonTextBox.Text.Trim(), ". اطلاعات شماره تلفن"));
                        }
                    }
                    else
                    {
                        Folder.MessageBox.ShowError(".تلفن وارد شده، موجود نیست");
                    }
                }
                else
                {

                    Folder.MessageBox.ShowError(".لطفاً فیلد شماره تلفن را پر نمائید");
                    TelephonTextBox.Focus();
                }



            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در جستجوی تلفن - اصلاح مشخصات - بخش برگردان");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
        private void SelectCabinetInput_Click(object sender, RoutedEventArgs e)
        {

            List<CRM.Data.ExchangeCabinetInputConnectionInfo> tempExchangeCabinetInputConnectionInfo = new List<ExchangeCabinetInputConnectionInfo>();
            List<CabinetInput> freeCabinetInput = new List<CabinetInput>();
            List<PostContactInfo> freePostContact = new List<PostContactInfo>();
            if (GeneralTranslationRadioButton.IsChecked == true)
            {
                tempExchangeCabinetInputConnectionInfo = TelephoneDB.GetExchangeCabinetInputConnectionInfoTelephoneByCabinetID(_exchangeCabinetInput.OldCabinetID, false, new List<int> { }, (bool)PCMToNormalCheckBox.IsChecked);

                freeCabinetInput = CabinetInputDB.GetFreeCabinetInputByCabinetID(_exchangeCabinetInput.NewCabinetID);
                freePostContact = PostContactDB.GetFreePostContactInfoByCabinetID(_exchangeCabinetInput.NewCabinetID);

            }
            else if (CabinetInputTranslationRadioButton.IsChecked == true)
            {
                tempExchangeCabinetInputConnectionInfo = TelephoneDB.GetExchangeCabinetInputConnectionInfoTelephoneByCabinetID(_exchangeCabinetInput.OldCabinetID, false, new List<int> { }, (bool)PCMToNormalCheckBox.IsChecked, (long)_exchangeCabinetInput.FromOldCabinetInputID, (long)_exchangeCabinetInput.ToOldCabinetInputID) as List<CRM.Data.ExchangeCabinetInputConnectionInfo>;

                freeCabinetInput = CabinetInputDB.GetFreeCabinetInputByCabinetInputID(_exchangeCabinetInput.NewCabinetID, (long)_exchangeCabinetInput.FromNewCabinetInputID, (long)_exchangeCabinetInput.ToNewCabinetInputID);
                freePostContact = PostContactDB.GetFreePostContactInfoByCabinetID(_exchangeCabinetInput.NewCabinetID);
            }
            int count = tempExchangeCabinetInputConnectionInfo.Count();
            int postContactCount = freePostContact.Count();
            int freeCabinetInputCount = freeCabinetInput.Count();

            for (int i = 0; i < count; i++)
            {

                if ((bool)PCMToNormalCheckBox.IsChecked)
                {
                    List<PostContactInfo> PostContactInfos = freePostContact.Where(t2 => t2.ABType == tempExchangeCabinetInputConnectionInfo[i].FromAorBType && t2.PostNumber == tempExchangeCabinetInputConnectionInfo[i].FromPostNumber).OrderBy(t => t.ConnectionNo).ToList();

                    if (i < PostContactInfos.Count())
                    {
                        tempExchangeCabinetInputConnectionInfo[i].ToPostConntactID = PostContactInfos[i].ID;
                        tempExchangeCabinetInputConnectionInfo[i].ToPostConntactNumber = PostContactInfos[i].ConnectionNo;
                        tempExchangeCabinetInputConnectionInfo[i].ToPostID = PostContactInfos[i].PostID;
                        tempExchangeCabinetInputConnectionInfo[i].ToPostNumber = PostContactInfos[i].PostNumber;
                        tempExchangeCabinetInputConnectionInfo[i].ToAorBTypeName = PostContactInfos[i].ABTypeName;
                    }
                }
                else
                {
                    PostContactInfo item = freePostContact.Where(t => t.ABType == tempExchangeCabinetInputConnectionInfo[i].FromAorBType && t.PostNumber == tempExchangeCabinetInputConnectionInfo[i].FromPostNumber && t.ConnectionNo == tempExchangeCabinetInputConnectionInfo[i].FromPostConntactNumber).Take(1).SingleOrDefault();
                    if (item != null)
                    {
                        tempExchangeCabinetInputConnectionInfo[i].ToPostConntactID = item.ID;
                        tempExchangeCabinetInputConnectionInfo[i].ToPostConntactNumber = item.ConnectionNo;
                        tempExchangeCabinetInputConnectionInfo[i].ToPostID = item.PostID;
                        tempExchangeCabinetInputConnectionInfo[i].ToPostNumber = item.PostNumber;
                        tempExchangeCabinetInputConnectionInfo[i].ToAorBTypeName = item.ABTypeName;
                    }
                }

            }


            for (int i = 0; i < count && i < freeCabinetInputCount; i++)
            {
                tempExchangeCabinetInputConnectionInfo[i].ToCabinetInputID = freeCabinetInput[i].ID;
            }

            _exchangeCabinetInputConncetionInfo = new ObservableCollection<ExchangeCabinetInputConnectionInfo>(tempExchangeCabinetInputConnectionInfo);
            SlightTranslationDataGrid.ItemsSource = _exchangeCabinetInputConncetionInfo;
        }
        private void AddPost_Click(object sender, RoutedEventArgs e)
        {
            List<CRM.Data.ExchangeCabinetInputConnectionInfo> tempTranslationOpticalCabinetToNormalConncetion = new List<ExchangeCabinetInputConnectionInfo>();

            List<TranslationOpticalCabinetToNormalPost> items = new List<TranslationOpticalCabinetToNormalPost>(PostSelectorDataGrid.ItemsSource as ObservableCollection<TranslationOpticalCabinetToNormalPost>);


            tempTranslationOpticalCabinetToNormalConncetion = TelephoneDB.GetExchangeCabinetInputConnectionInfoTelephoneByCabinetID(_exchangeCabinetInput.OldCabinetID, true, items.Where(t => t.PCMToNormal == true).Select(t => t.FromPostID).ToList(), true);

            tempTranslationOpticalCabinetToNormalConncetion = tempTranslationOpticalCabinetToNormalConncetion.Union(TelephoneDB.GetExchangeCabinetInputConnectionInfoTelephoneByCabinetID(_exchangeCabinetInput.OldCabinetID, true, items.Where(t => t.PCMToNormal == false).Select(t => t.FromPostID).ToList(), false)).ToList();
            List<CabinetInput> freeCabinetInput = CabinetInputDB.GetFreeCabinetInputByCabinetID(_exchangeCabinetInput.NewCabinetID);
            List<PostContactInfo> freePostContact = PostContactDB.GetFreePostContactInfoByCabinetID(_exchangeCabinetInput.NewCabinetID, items.Select(t => t.ToPostID).ToList());
            int count = tempTranslationOpticalCabinetToNormalConncetion.Count();
            int postContactCount = freePostContact.Count();
            int freeCabinetInputCount = freeCabinetInput.Count();

            for (int i = 0; i < count; i++)
            {

                TranslationOpticalCabinetToNormalPost item = items.Where(t => t.FromPostID == (int)tempTranslationOpticalCabinetToNormalConncetion[i].FromPostID).SingleOrDefault();
                if (item != null)
                {

                    if (!item.PCMToNormal)
                    {
                        PostContactInfo postContactInfoItem = freePostContact.Find(t2 => t2.PostID == item.ToPostID && t2.ConnectionNo == tempTranslationOpticalCabinetToNormalConncetion[i].FromPostConntactNumber);

                        if (postContactInfoItem != null)
                        {
                            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactID = postContactInfoItem.ID;
                            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactNumber = postContactInfoItem.ConnectionNo;
                            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID = postContactInfoItem.PostID;
                            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostNumber = postContactInfoItem.PostNumber;
                            tempTranslationOpticalCabinetToNormalConncetion[i].ToAorBTypeName = postContactInfoItem.ABTypeName;
                        }
                    }
                    else
                    {
                        List<PostContactInfo> PostContactInfos = freePostContact.Where(t2 => t2.PostID == item.ToPostID).OrderBy(t => t.ConnectionNo).ToList();

                        if (i < PostContactInfos.Count())
                        {
                            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactID = PostContactInfos[i].ID;
                            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostConntactNumber = PostContactInfos[i].ConnectionNo;
                            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostID = PostContactInfos[i].PostID;
                            tempTranslationOpticalCabinetToNormalConncetion[i].ToPostNumber = PostContactInfos[i].PostNumber;
                            tempTranslationOpticalCabinetToNormalConncetion[i].ToAorBTypeName = PostContactInfos[i].ABTypeName;
                        }
                    }
                }

            };

            for (int i = 0; i < count && i < freeCabinetInputCount; i++)
            {
                tempTranslationOpticalCabinetToNormalConncetion[i].ToCabinetInputID = freeCabinetInput[i].ID;
            }


            _exchangeCabinetInputConncetionInfo = new ObservableCollection<ExchangeCabinetInputConnectionInfo>(tempTranslationOpticalCabinetToNormalConncetion);
            SlightTranslationDataGrid.ItemsSource = _exchangeCabinetInputConncetionInfo;
            SlightTranslationDetailGroupBox.Visibility = Visibility.Visible;
        }

        private void SlightTranslationDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (SlightTranslationDataGrid.SelectedItem != null)
            {
                ExchangeCabinetInputConnectionInfo item = SlightTranslationDataGrid.SelectedItem as ExchangeCabinetInputConnectionInfo;
                if (item != null)
                {
                    if (item.ToPostID != null)
                    {
                        _ToPostConntact = new ObservableCollection<CheckableItem>(Data.PostContactDB.GetFreePostContactCheckableByPostID((int)item.ToPostID));
                    }

                    if (item.FromPostID != null)
                    {
                        _FromPostConntact = new ObservableCollection<CheckableItem>(Data.PostContactDB.GetConnectionPostContactWithRemotPCMCheckableByPostID((int)item.FromPostID, true));
                    }
                }
            }

        }

        private void WithoutPostTranslationRadioButton_Checked(object sender, RoutedEventArgs e)
        {

            SelectTranslationType();
        }

        private void IsChangePostCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ToPostColumn.Visibility = Visibility.Visible;
            ToPostConntactTemplateColumn.Visibility = Visibility.Visible;
            ToAorBTypeNameColumn.Visibility = Visibility.Visible;
            ToPostTemplateColumn.Visibility = Visibility.Visible;
        }

        private void IsChangePostCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ToPostColumn.Visibility = Visibility.Collapsed;
            ToPostConntactTemplateColumn.Visibility = Visibility.Collapsed;
            ToAorBTypeNameColumn.Visibility = Visibility.Collapsed;
            ToPostTemplateColumn.Visibility = Visibility.Collapsed;
        }

        private void CabinetInputTranslationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SelectTranslationType();
        }

    }
}


