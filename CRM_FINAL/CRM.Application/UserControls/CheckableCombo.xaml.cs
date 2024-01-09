using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Input;
//using Common;
using System.Linq;
using CRM.Data;

namespace CRM.Application.UserControls
{   
    public partial class CheckableComboBox : UserControl
    {

        public CheckableComboBox()
        {
            InitializeComponent();
        }

        public List<int> SelectedIDs
        {
            get
            {
                List<int> ids = new List<int>();
                foreach (CheckableItem item in ItemsComboBox.Items)
                {
                    if (item.IsChecked == true)
                        ids.Add(item.ID);
                };
                return ids;
            }
        }

        public List<string> SelectedIDs_S
        {
            get
            {
                List<string> ids = new List<string>();
                foreach (CheckableItem item in ItemsComboBox.Items)
                {
                    if (item.IsChecked == true)
                        ids.Add(item.Name);
                };
                return ids;
            }
            //set
            //{
            //    foreach (int index in value)
            //    {
            //        ItemsComboBox.Items.Add(index);                    
            //    }
            //}
        }

        public List<long> SelectedIDs_l
        {
            get
            {
                List<long> ids = new List<long>();
                foreach (CheckableItem item in ItemsComboBox.Items)
                {
                    if (item.IsChecked == true)
                        ids.Add((long)item.LongID);
                };
                return ids;
            }
        }

        public List<byte> SelectedIDs_b
        {
            get
            {
                List<byte> ids = new List<byte>();
                foreach (EnumItem item in ItemsComboBox.Items)
                {
                    if (item.IsChecked == true)
                        ids.Add(item.ID);
                };
                return ids;
            }
        }
                
        public string SelectedIdsToString(List<int> Ids)
        {
            if (Ids.Count != 0)
                return string.Join(",", Ids.ToArray());
            else
                return null;      
        }
        public List<int> SelectedIdsToInt(string Ids)
        {
            if (Ids != string.Empty)
                return Ids.Split(',').Select(s => int.Parse(s)).ToList();
            else
                return null;            
        }

        public List<CheckableItem> SelectedItems
        {
            get
            {
                List<CheckableItem> items = new List<CheckableItem>();
                foreach (CheckableItem item in ItemsComboBox.Items)
                {
                    if (item.IsChecked == true)
                        items.Add(item);
                };
                return items;
            }
        }

        public IEnumerable ItemsSource
        {
            get
            {
                return ItemsComboBox.ItemsSource;
            }
            set
            {
                ItemsComboBox.ItemsSource = value;
            }
        }

        public ItemCollection Items
        {
            get
            {
                return ItemsComboBox.Items;
            }
        }

        public int  SelectedIndex
        {
            get
            {
                return ItemsComboBox.SelectedIndex  ;
            }
            set
            {
                ItemsComboBox.SelectedIndex = value;
            }
        }
        private void ItemsComboBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SelectedIDs.Count == 0)
            {
                ItemsComboBox.SelectedIndex = -1;
            }
            else if (SelectedIDs.Count == 1)
            {
                ItemsComboBox.SelectedItem = (ItemsComboBox.ItemsSource as List<CheckableItem>).Where(t => t.IsChecked == true).SingleOrDefault();
            }
        }
        //private void ItemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (ItemsComboBox.SelectedItem != null)
        //    {
        //        (ItemsComboBox.SelectedItem as CheckableItem).IsChecked = true;
        //        ItemsComboBox.Items.Refresh();
        //    }
        //}

        public void CheckableItemsComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                switch (e.Key)
                {
                    case Key.A:

                        foreach (CheckableItem item in comboBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = true;
                        comboBox.Items.Refresh();
                        break;

                    case Key.N:
                        foreach (CheckableItem item in comboBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = false;
                        comboBox.Items.Refresh();
                        break;

                    case Key.R:
                        foreach (CheckableItem item in comboBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = !item.IsChecked;
                        comboBox.Items.Refresh();
                        break;

                    default:
                        break;
                }
            }
        }

        public void Reset()
        {
            if (ItemsComboBox.ItemsSource != null)
            {
                foreach (CheckableItem item in ItemsComboBox.ItemsSource as List<CheckableItem>)
                    item.IsChecked = false;
                ItemsComboBox.Items.Refresh();
                ItemsComboBox.SelectedIndex = -1;
            }
        }

        private void ItemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemsComboBox.SelectedItem != null)
            {
                (ItemsComboBox.SelectedItem as CheckableItem).IsChecked = true;
                ItemsComboBox.Items.Refresh();
            }
        }

        

    }
}