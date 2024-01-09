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
using System.ComponentModel;
using CRM.Data;
using System.Collections.ObjectModel;
using CRM.Application.UserControls;
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class SwitchPortForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private static List<Telephone> TeleList { get; set; }
        private List<Telephone> oldtele;

        #endregion

        #region Constrauctor

        public SwitchPortForm()
        {
            InitializeComponent();
            Initialize();
        }

        public SwitchPortForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methode

        private void Initialize()
        {
            SwitchComboBox.ItemsSource = Data.SwitchDB.GetSwitchCheckable();
            Status.ItemsSource = Helper.GetEnumItem(typeof(DB.SwitchPortStatus));
        }

        private void LoadData()
        {
            SwitchPort item = new SwitchPort();
            item.Type = false;

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                item = Data.SwitchPortDB.GetSwitchPortByID(_ID);
                SwitchComboBox.SelectedValue = item.SwitchID;
                SwitchComboBox_SelectionChanged(null, null);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;


            if (!object.ReferenceEquals(item, null) && item.Type == true)
            {
                TeleList = Data.TelephoneDB.GetTelephoneListBySwitchID(item.SwitchID).Where(t => t.Status == (byte)DB.TelephoneStatus.Free).ToList();
                oldtele = Data.TelephoneDB.GetTelephonesNoBySwitchPortID(item.ID);
                if (oldtele.Count > 1) MessageBox.Show("خطا در بارگذاری اطلاعات دو تلفن با یک شماره پورت ثبت شده است");

                // اگر تلفن در لیست تلفنها موجود نباشد آن را اضافه میکند
                if (!object.Equals(oldtele, null))
                    if (!TeleList.Any(t => t.TelephoneNo == oldtele.Take(1).SingleOrDefault().TelephoneNo)) { TeleList = TeleList.Union(oldtele).ToList(); }

                CRM.Application.UserControls.AutoComplete.AutoCompleteArgs TelArg = new UserControls.AutoComplete.AutoCompleteArgs(oldtele.Take(1).SingleOrDefault().TelephoneNo.ToString());
                TelArg.DataSource = TeleList;
                this.TelephoneNoComboBox_PatternChanged(null, TelArg);
                this.TelephoneNoComboBox.ItemsSource = TelArg.DataSource;
                this.TelephoneNoComboBox.SelectedValue = (TelArg.DataSource).Cast<CRM.Data.Telephone>().FirstOrDefault().TelephoneNo;
                this.TelephoneNoComboBox.Text = TelArg.Pattern;
            }
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
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
                    SwitchPort item = this.DataContext as SwitchPort;

                    if (TypeCheckBox.IsChecked == false)
                    {
                        item.Detach();
                        Save(item);
                    }
                    else
                    {
                        // اگر پورت جدید باشد تلفن به آن نصبت داده میشود در غیر اینصورت تلفن قبلی پاک و تلفن جدید نصبت داده میشود
                        if (_ID == 0)
                        {

                            if (DB.ExistPortNo(item)) throw new Exception("این پورت موجود میباشد");
                            item.Detach();
                            Save(item);

                            Data.Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)TelephoneNoComboBox.SelectedValue);
                            telephone.SwitchPortID = item.ID;
                            telephone.Detach();
                            DB.Save(telephone);
                        }
                        else
                        {
                            item.Detach();
                            Save(item);
                            Data.Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)TelephoneNoComboBox.SelectedValue);

                            // اگر تلفن قبلی موجود باشد وبرابر با تلفن انتخاب شده فعلی نباشد تلفن قبلی را پاک میکند وجدید را ثبت میکند
                            if (!object.ReferenceEquals(oldtele, null) && oldtele.Take(1).SingleOrDefault().Equals(telephone))
                            {

                              //  oldtele.Take(1).SingleOrDefault().SwitchPortID = DB.GetSwitchPortIDTypeByTelephone(oldtele.Take(1).SingleOrDefault());
                                oldtele.Take(1).SingleOrDefault().Detach();
                                DB.Save(oldtele.Take(1).SingleOrDefault());

                            }

                            telephone.SwitchPortID = item.ID;
                            telephone.Detach();
                            DB.Save(telephone);
                        }
                    }

                    ts.Complete();
                }

                ShowSuccessMessage("پورت ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پورت", ex);
            }
        }

        private void SwitchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SwitchComboBox.SelectedValue != null)
            {
                TeleList = Data.TelephoneDB.GetTelephoneListBySwitchID((int)SwitchComboBox.SelectedValue).Where(t => t.Status == (byte)DB.TelephoneStatus.Free && t.SwitchPortID == null).ToList();
                //TelephoneNoComboBox.ItemsSource = TeleList;
            }
        }

        #endregion

        #region AutoComplate

        private void TelephoneNoComboBox_PatternChanged(object sender, AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
            {
                args.DataSource = SwitchPortForm.GetTelNo(args.Pattern);
                TextSearch.SetTextPath(this.TelephoneNoComboBox, "TelephoneNo");
            }
        }

        private static ObservableCollection<Telephone> GetTelNo(string Pattern)
        {
            if (TeleList == null) return null;

            return new ObservableCollection<Telephone>(TeleList.Where((tel, match) => tel.TelephoneNo.ToString().ToLower().Contains(Pattern.ToLower())));
        }

        #endregion
    }
}
