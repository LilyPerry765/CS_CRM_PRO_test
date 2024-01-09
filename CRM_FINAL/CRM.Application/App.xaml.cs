using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Globalization;
using System.Threading;

namespace CRM.Application
{
    public partial class App : System.Windows.Application
    {
        public App()
        {
            CultureInfo farsiCultureInfo = new CultureInfo("fa-IR");
            farsiCultureInfo.NumberFormat.NumberGroupSeparator = "،";
            Thread.CurrentThread.CurrentCulture = farsiCultureInfo;
        }
    }
}
