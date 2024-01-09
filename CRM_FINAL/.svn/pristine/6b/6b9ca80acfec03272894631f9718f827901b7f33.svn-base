using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Application.Codes.Validation
{
	[System.Xml.Serialization.XmlRoot("XmlValidation")]
	public class XmlValidation
	{
		[System.Xml.Serialization.XmlElement("Element")]
		public System.Collections.ObjectModel.ObservableCollection<Element> elements
			= new System.Collections.ObjectModel.ObservableCollection<Element>();
	}

	public class Element
	{
		[System.Xml.Serialization.XmlAttribute("Name")]
		public string Name { get; set; }

		[System.Xml.Serialization.XmlElement("Control")]
		public System.Collections.ObjectModel.ObservableCollection<Control> Controls
			= new System.Collections.ObjectModel.ObservableCollection<Control>();
	}

	[System.Xml.Serialization.XmlType]
	public class Control
	{
		[System.Xml.Serialization.XmlAttribute("Name")]
		public string Name { get; set; }

		[System.Xml.Serialization.XmlAttribute("IsRequire")]
		public bool IsRequire { get; set; }

		[System.Xml.Serialization.XmlAttribute("RegularExpression")]
		public string RegularExpression { get; set; }
	}
}
