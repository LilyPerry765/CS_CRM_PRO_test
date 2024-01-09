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
using CRM.Data;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for PCMsStatisticReportUserControl.xaml
    /// </summary>
    public partial class PCMUserControl : UserControl
    {
        public PCMUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            PCMBrandComboBox.ItemsSource = PCMBrandDB.GetPCMBrandCheckable();
            PCMTypeComboBox.ItemsSource = PCMTypeDB.GetPCMTypeCheckable();
            RockComboBox.ItemsSource = PCMRockDB.GetPCMRockCheckable();
            //RockComboBox.SelectedIndex = 0;
        }

        public void Rock_Update(int centerID)
        {
            RockComboBox.ItemsSource = PCMRockDB.GetPCMRockCheckablebyCenterId(new List<int>{centerID});
            RockComboBox.SelectedIndex = 0;
        }

        private void RockComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShelfComboBox.ItemsSource = null;
            if ((sender as ComboBox).SelectedValue != null)
            {
                ShelfComboBox.ItemsSource = PCMShelfDB.GetCheckableItemPCMShelfByRockIDs(new List<int> { (int)(sender as ComboBox).SelectedValue });
                ShelfComboBox.SelectedIndex = 0;
            }
        }

        private void ShelfComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CardComboBox.ItemsSource = null;
            if ((sender as ComboBox).SelectedValue != null)
            {
                CardComboBox.ItemsSource = PCMDB.GetCheckableItemPCMCardInfoByShelfID(new List<int> { (int)(sender as ComboBox).SelectedValue });
                CardComboBox.SelectedIndex = 0;
            }
        }

       
    }
}
