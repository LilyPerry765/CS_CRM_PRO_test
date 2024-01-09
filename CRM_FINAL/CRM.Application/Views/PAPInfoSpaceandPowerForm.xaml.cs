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

namespace CRM.Application.Views
{
    public partial class PAPInfoSpaceandPowerForm : Local.PopupWindow
    {
        #region Properties

        private int _ID;
        private int _PAPInfoID;
        private int _CenterID;

        #endregion

        #region Constructors

        public PAPInfoSpaceandPowerForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PAPInfoSpaceandPowerForm(int papInfoID)
            : this()
        {
            _PAPInfoID = papInfoID;
        }

        public PAPInfoSpaceandPowerForm(int id, int papInfoID, int centerID)
            : this()
        {
            _ID = id;
            _PAPInfoID = papInfoID;
            _CenterID = centerID;
        }

        #endregion

        #region Methods

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenters();
        }

        private void LoadData()
        {
            PAPInfoSpaceandPower item = new PAPInfoSpaceandPower();

            if (_ID == 0)
            {
                PAPInfoTextBox.Text = DB.SearchByPropertyName<PAPInfo>("ID", _PAPInfoID).SingleOrDefault().Title;
                SaveButton.Content = "ذخیره";
            }
            else
            {
                CenterComboBox.IsEnabled = false;

                item = PAPInfoDB.GetPAPInfoSpaceandPower(_ID);
                PAPInfoTextBox.Text = PAPInfoDB.GetPAPInfoByID(item.PAPInfoID).Title;

                item.ACPower = null;
                item.DCPower = null;
                item.Space = null;
                item.FromDate = null;

                SaveButton.Content = "بروزرسانی";
            }

            using (MainDataContext context = new MainDataContext())
            {
                ACTextox.Text = context.PAPInfoSpaceandPowers.Where(t => t.PAPInfoID == _PAPInfoID && t.EndDate == null).Sum(t => t.ACPower).ToString();
                DCTextox.Text = context.PAPInfoSpaceandPowers.Where(t => t.PAPInfoID == _PAPInfoID && t.EndDate == null).Sum(t => t.DCPower).ToString();
                SpaceTextox.Text = context.PAPInfoSpaceandPowers.Where(t => t.PAPInfoID == _PAPInfoID && t.EndDate == null).Sum(t => t.Space).ToString();
            }

            Search(null, null);

            this.DataContext = item;
        }

        #endregion

        #region Event Handlers

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = PAPInfoDB.SearchPAPInfoSpaceByCenterID(_PAPInfoID, _CenterID);
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                PAPInfoSpaceandPower item = this.DataContext as PAPInfoSpaceandPower;                

                if (item.CenterID == 0)
                    throw new Exception("لطفا مرکز را تعیین نمایید");
                if (item.ACPower == null)
                    throw new Exception("لطفا مقدار برق AC را تعیین نمایید");
                if (item.DCPower == null)
                    throw new Exception("لطفا مقدار برق DC را تعیین نمایید");
                if (item.Space == null)
                    throw new Exception("لطفا مقدار فضا را تعیین نمایید");
                if (item.FromDate == null)
                    throw new Exception("لطفا تاریخ شروع تغییرات را تعیین نمایید");
                
                PAPInfoSpaceandPower lastSAPSpace = PAPInfoDB.GetLastSpaceandPowerwithCenterID(_PAPInfoID, item.CenterID);

                if (lastSAPSpace != null)
                {
                    DateTime fromDate = (DateTime)item.FromDate;
                    lastSAPSpace.EndDate = fromDate.AddDays(-1);

                    lastSAPSpace.Detach();
                    Save(lastSAPSpace);
                }

                item.PAPInfoID = _PAPInfoID;

                item.Detach();
                Save(item, true);

                using (MainDataContext context = new MainDataContext())
                {
                    ACTextox.Text = context.PAPInfoSpaceandPowers.Where(t => t.PAPInfoID == _PAPInfoID && t.EndDate == null).Sum(t => t.ACPower).ToString();
                    DCTextox.Text = context.PAPInfoSpaceandPowers.Where(t => t.PAPInfoID == _PAPInfoID && t.EndDate == null).Sum(t => t.DCPower).ToString();
                    SpaceTextox.Text = context.PAPInfoSpaceandPowers.Where(t => t.PAPInfoID == _PAPInfoID && t.EndDate == null).Sum(t => t.Space).ToString();
                }

                CenterComboBox.IsEnabled = false;

                Search(null, null);
                ShowSuccessMessage("اطلاعات فضا و پاور شرکت PAP ذخیره شد.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات فضا و پاور شرکت PAP" + " ، " + ex.Message + "!", ex);
            }
        }
                
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion
    }
}
