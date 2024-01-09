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
    public partial class ADSLModemPropertyChangeCenterForm : Local.PopupWindow
    {
        #region Properties

        private static List<CheckableItem> _ModemIDs;

        #endregion

        #region Constructors

        public ADSLModemPropertyChangeCenterForm()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            OldCenterComboBox.ItemsSource = CenterDB.GetCenterCheckable();
            NewCenterComboBox.ItemsSource = CenterDB.GetCenterCheckable();
            ModemModelComboBox.ItemsSource = ADSLModemDB.GetModemMOdelsCheckable();

            SerialNoListBox.ItemsSource = _ModemIDs = ADSLModemPropertyDB.GetADSLModemPropertyNotSoldCheckable();
        }

        private void LoadData()
        {
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            List<int> selectedModemIds = SerialNoListBox.Items.Cast<CheckableItem>().ToList().Where(t => t.IsChecked == true).Select(t => (int)t.ID).ToList();

            foreach (int modemID in selectedModemIds)
            {
                ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById(modemID);

                modem.CenterID = (int)NewCenterComboBox.SelectedValue;

                modem.Detach();
                Save(modem);
            }

            this.DialogResult = true;
        }

        private void SerilaNoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SerialNoListBox.ItemsSource = _ModemIDs.Where(t => t.Name.Contains(SerilaNoTextBox.Text)).ToList();
        }

        private void SerialNoListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                switch (e.Key)
                {
                    case Key.A:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = true;
                        listBox.Items.Refresh();
                        break;

                    case Key.N:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = false;
                        listBox.Items.Refresh();
                        break;

                    case Key.R:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = !item.IsChecked;
                        listBox.Items.Refresh();
                        break;

                    default:
                        break;
                }
            }
        }

        private void ModemModelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SerialNoListBox.ItemsSource = _ModemIDs = ADSLModemPropertyDB.GetADSLModemPropertyNotSoldCheckablebyModemID((int)ModemModelComboBox.SelectedValue);
        }

        private void OldCenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SerialNoListBox.ItemsSource = _ModemIDs = ADSLModemPropertyDB.GetADSLModemPropertyNotSoldCheckablebyCenterID((int)OldCenterComboBox.SelectedValue);
        }

        #endregion
    }
}
