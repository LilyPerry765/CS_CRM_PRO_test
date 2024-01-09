using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
   public class DBbase
    {
    }
    public partial class CheckableItem
    {
        public int ID { get; set; }
        
        public long? LongID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? IsChecked { get; set; }

        public CheckableItem() { }

        public CheckableItem(int id, string name, string description, bool isChecked)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.IsChecked = isChecked;
        }
    }

    public partial class CheckableItemNullable
    {
        public int? ID { get; set; }

        public long? LongID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? IsChecked { get; set; }

        public CheckableItemNullable() { }

        public CheckableItemNullable(int? id, string name, string description, bool isChecked)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.IsChecked = isChecked;
        }
    }

    public partial class LongItem
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public bool? IsChecked { get; set; }

        public LongItem() { }

        public LongItem(long id, string name, bool isChecked)
        {
            this.ID = id;
            this.Name = name;
            this.IsChecked = isChecked;
        }
    }

    public partial class EnumItem
    {
        public byte ID { get; set; }     

        public string Name { get; set; }

        public string OriginalName { get; set; }

        public bool? IsChecked { get; set; }

        public int EnumValue { get; set; }

        public EnumItem() { }

        public EnumItem(byte id, string name, bool isChecked)
        {
            this.ID = id;
            this.Name = name;
            this.IsChecked = isChecked;

        }
    }

    public partial class CheckableObject
    {
        public object Object { get; set; }

        public bool? IsChecked { get; set; }
    }

    public partial class Pair
    {
        public Pair()
        {
        }

        public Pair(object name, object value)
        {
            Name = name;
            Value = value;
        }

        public object Name { get; set; }

        public object Value { get; set; }
    }

    public partial class ExtendedPair
    {
        public ExtendedPair()
        {
        }

        public ExtendedPair(object key, object value, object extention)
        {
            Key = key;
            Value = value;
            Extention = extention;
        }

        public object Key { get; set; }
        public object Value { get; set; }
        public object Extention { get; set; }
    }
}
