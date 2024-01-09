using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Application.Local
{
    public class TimeConsumingOperation
    {
        #region Properties and Fields
        /// <summary>
        /// عملیات اصلی - عملیاتی که ممکن است برای اجرا زمان زیادی لازم داشته باشد
        /// </summary>
        public Action MainOperationAction { get; set; }

        /// <summary>
        /// به طور مثال در طول اجرای عملیات اصلی ما میخواهیم یک پراگرس بار به کاربر نمایش دهیم
        /// MainExtendedStatusBar.ShowProgressBar = true;
        /// MainExtendedStatusBar.MessageLabel.Text = "درحال بارگذاری...";
        /// </summary>
        public Action DuringOperationAction { get; set; }

        /// <summary>
        /// اگر در طول اجرای عملیات اصلی تغییراتی در ظاهر برنامه داشته باشیم ، میبایست بعد از اتمام عملیات اصلی کلیه موارد به حالت پیش فرض برگردند
        /// به طور مثال 
        /// MainExtendedStatusBar.ShowProgressBar = false;
        /// MainExtendedStatusBar.MessageLabel.Text = string.Empty;
        /// </summary>
        public Action AfterOperationAction { get; set; }

        #endregion
    }

    public class TimeConsumingResult
    {
        /// <summary>
        /// اگر در حین اجرای عملیات سنگین ، سیستم با خطا موجه میشد در ایونت زیر  
        /// V2BackgroundWorker_RunWorkerCompleted
        /// به 
        /// AfterOperationAction
        /// دسترسی نداشتیم و سیستم در حال بارگذاری باقی میماند
        /// </summary>
        public Exception Error { get; set; }
        public Action AfterOperationAction { get; set; }
    }
}
