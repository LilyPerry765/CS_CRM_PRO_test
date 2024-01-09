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

namespace CRM.Application.UserControls
{
    /// <summary>
    /// این یوزر کنترل به منظور آگاه سازی کاربر با استفاده از عکس وپیغام مورد نظر ایجاد شده است
    /// به طور مثال میتوان با استفاده از عکس هشدار و پیغام هشداردهنده به کاربر اعلام هشدار کرد
    /// </summary>
    public partial class AnouncementUserControl : UserControl
    {
        #region Properties and Fields

        /// <summary>
        /// پیغام
        /// </summary>
        public string AnouncementMessage
        {
            set
            {
                AnouncementMessageTextBlock.Text = value;
            }
        }

        /// <summary>
        /// عکس
        /// </summary>
        public ImageSource AnouncementImageSource
        {
            set
            {
                AnouncementImage.Source = value;
            }
        }

        /// <summary>
        /// رنگ قلم پیغام
        /// </summary>
        public Brush AnouncementMessageForeground
        {
            set
            {
                AnouncementMessageTextBlock.Foreground = value;
            }
        }
        #endregion
        
        #region Constructors

        public AnouncementUserControl()
        {
            InitializeComponent();
        }

        #endregion
    }
}
