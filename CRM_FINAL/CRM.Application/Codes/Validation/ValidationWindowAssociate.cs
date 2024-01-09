using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Application.Codes.Validation
{
	public class ValidationWindowAssociate
	{
		public string Fullname { get; set; }

		public string Title { get; set; }

		public Type Type { get; set; }

		public bool HasContent { get; set; }

		public bool HasValidation { get; set; }
	}
}
