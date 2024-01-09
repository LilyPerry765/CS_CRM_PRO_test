using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CRM.Application.Codes.CustomControls
{
    //TODO:rad ExtendedMenuItem
    /// <summary>
    /// .این کنترل قابلیت اضافه کردن عکس در خود را به طور مستقیم دارا میباشد ، همچنین چنانچه متن وارد شده برای تکست این کنترل فضای کافی را نداشته باشد "رپ" میشود
    /// usage: <ExtendedMenuItem Image="pack://application:,,,/CRM.Application;component/AuditReportIcon.png" ImageHeight="24" ImageWidth="24"/>
    /// </summary>
    public class ExtendedMenuItem : MenuItem
    {
        #region Properties

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(ExtendedMenuItem), new PropertyMetadata(null));
        //*****************************************************************

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(ExtendedMenuItem), new PropertyMetadata(Double.NaN));
        //*****************************************************************


        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(ExtendedMenuItem), new PropertyMetadata(Double.NaN));
        //*****************************************************************

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ExtendedMenuItem), new UIPropertyMetadata(string.Empty));

        #endregion

        #region Constructors

        static ExtendedMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedMenuItem), new FrameworkPropertyMetadata(typeof(ExtendedMenuItem)));
        }

        #endregion
    }
}

