//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Copyright 2008, Tenta Corporation, Version 1.5
namespace CRM.Data.Schema {
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using System.IO;
    
    
    public partial class SpecialServiceUtility {
        
        public const string SchemaNamespace = "http://tempuri.org/SpecialService.xsd";
        
        public static string Serialize<T>(object o, bool indented)
         {
            StringWriter writer = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(writer);
            if (indented) {
                xmlWriter.Formatting = Formatting.Indented;
            }
            else {
                xmlWriter.Formatting = Formatting.None;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(xmlWriter, o);
            return writer.ToString();
        }
        
        public static T Deserialize<T>(string objectXml)
         {
            StringReader reader = new StringReader(objectXml);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T classObject = ((T)(serializer.Deserialize(reader)));
            reader.Close();
            return classObject;
        }
        
        public static T DeserializeFile<T>(string path)
         {
            string xml = File.ReadAllText(path);
            return SpecialServiceUtility.Deserialize<T>(xml);
        }
    }
    
    [Serializable()]
    [XmlRoot(ElementName="SpecialServices", Namespace=SpecialServiceUtility.SchemaNamespace, IsNullable=false)]
    public partial class SpecialServices : INotifyPropertyChanged {
        
        [XmlElement(ElementName="FirstZeroStatus")]
        public byte _FirstZeroStatus;
        
        public const string FirstZeroStatusProperty = "FirstZeroStatus";
        
        [XmlElement(ElementName="SecondZeroStatus")]
        public byte _SecondZeroStatus;
        
        public const string SecondZeroStatusProperty = "SecondZeroStatus";
        
        [XmlIgnore()]
        public byte FirstZeroStatus {
            get {
                return _FirstZeroStatus;
            }
            set {
                if ((_FirstZeroStatus != value)) {
                    this._FirstZeroStatus = value;
                    this.SendPropertyChanged("FirstZeroStatus");
                }
            }
        }
        
        [XmlIgnore()]
        public byte SecondZeroStatus {
            get {
                return _SecondZeroStatus;
            }
            set {
                if ((_SecondZeroStatus != value)) {
                    this._SecondZeroStatus = value;
                    this.SendPropertyChanged("SecondZeroStatus");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void SendPropertyChanged(string propertyName) {
            if ((PropertyChanged != null)) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}