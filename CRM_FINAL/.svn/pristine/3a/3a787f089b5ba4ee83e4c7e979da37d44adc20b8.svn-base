using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Test.CRM.ServiceHost
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CultureInfo farsiCultureInfo = new CultureInfo("fa-IR");
            farsiCultureInfo.NumberFormat.NumberGroupSeparator = "،";
            Thread.CurrentThread.CurrentCulture = farsiCultureInfo;
        }
    }
}
