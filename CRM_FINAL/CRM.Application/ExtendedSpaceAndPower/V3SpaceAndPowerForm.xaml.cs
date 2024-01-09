using CRM.Application.Local;
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

namespace CRM.Application.ExtendedSpaceAndPower
{
    /// <summary>
    /// Interaction logic for V3SpaceAndPowerForm.xaml
    /// </summary>
    public partial class V3SpaceAndPowerForm : PopupWindow
    {
        #region properties and Fields

        public long ID
        {
            get;
            set;
        }

        public CRM.Data.DB.BasicEquipmentType SpaceAndPowerEquipmentType
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public V3SpaceAndPowerForm()
        {
            InitializeComponent();
            Initialize();
        }

        public V3SpaceAndPowerForm(long id)
            : this()
        {
            this.ID = id;
        }

        public V3SpaceAndPowerForm(long id, CRM.Data.DB.BasicEquipmentType spaceAndPowerEquipmentType)
            : this(id)
        {
            this.SpaceAndPowerEquipmentType = spaceAndPowerEquipmentType;
        }

        #endregion

        #region EventHandlers

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BasicEquipmentTypesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Methods

        private void Initialize()
        {
            BasicEquipmentTypesComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.BasicEquipmentType));
        }

        private void Load()
        {
            if (this.ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                //Fibre table = New Fibre();
                //Cable Table    "
                //Space Table    "
                //Antenna Table  "
                //Power Table    "
                SaveButton.Content = "بروزرسانی";
            }
            //EquipmentPropertiesExpander.DataContext=
        }

        #endregion

    }
}
