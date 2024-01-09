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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for DDFUserControl.xaml
    /// </summary>
    public partial class DDFUserControl : Local.UserControlBase
    {

        private byte _DDFType = 0;
        private int _E1NumberID = 0;
        public int E1Number { get; set; }
        public int CenterID { get; set; }

        E1Number _e1Number { get; set; }  
        E1Position _e1Position { get; set; }
        E1Bay _e1Bay        { get; set; }
        E1DDF _e1DDF { get; set; }      
        public DDFUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        public DDFUserControl(byte DDFType)
            : this()
        {
            this._DDFType = DDFType;
        }
        public DDFUserControl(int E1NumberID)
            : this()
        {
            this._E1NumberID = E1NumberID;
        }
        private void Initialize()
        {
        }
        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (_E1NumberID == 0)
            {
                DDFComboBox.ItemsSource = Data.E1DDFDB.GetDDFCheckableByTypeAndCenterID(_DDFType , CenterID);
            }
            else
            {
                 _e1Number        = Data.E1NumberDB.GetE1NumberByID(_E1NumberID);
                 _e1Position      = Data.E1PositionDB.GetE1PositionByID(_e1Number.PositionID);
                 _e1Bay           = Data.E1BayDB.GetE1BayByID(_e1Position.BayID);
                 _e1DDF           = Data.E1DDFDB.GetE1DDFByID(_e1Bay.DDFID);


                 DDFComboBox.ItemsSource = Data.E1DDFDB.GetDDFCheckableByTypeAndCenterID(_e1DDF.DDFType, CenterID);
                DDFComboBox.SelectedValue = _e1DDF.ID;
                DDFComboBox_SelectionChanged(null, null);

                BayComboBox.SelectedValue = _e1Bay.ID;
                BayComboBox_SelectionChanged(null, null);

                PositionComboBox.SelectedValue = _e1Position.ID;
                PositionComboBox_SelectionChanged(null, null);

                NumberComboBox.SelectedValue = _e1Number.ID;
                NumberComboBox_SelectionChanged(null, null);

            }

        }
        private void DDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DDFComboBox.SelectedValue != null)
                BayComboBox.ItemsSource = Data.E1BayDB.GetBayCheckableByDDFID((int)DDFComboBox.SelectedValue);
        }

        private void BayComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BayComboBox.SelectedValue != null)
                PositionComboBox.ItemsSource = Data.E1PositionDB.GetPositionCheckableByBayIDs(new List<int> { (int)BayComboBox.SelectedValue });
        }

        private void PositionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PositionComboBox.SelectedValue != null)
            {
                if (_e1Number == null)
                    NumberComboBox.ItemsSource = Data.E1NumberDB.GetE1NumberCheckableByPositionID((int)PositionComboBox.SelectedValue);
                else
                    NumberComboBox.ItemsSource = Data.E1NumberDB.GetE1NumberCheckableByPositionID((int)PositionComboBox.SelectedValue).Union(new List<CheckableItem> { new CheckableItem { ID = _e1Number.ID, Name = _e1Number.Number.ToString(), IsChecked = false } });

            }
        }

        private void NumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            E1Number = (int?)NumberComboBox.SelectedValue ?? 0;
        }


    }
}
