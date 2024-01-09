using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Application.Local
{
    public class ReportBase : System.Windows.Controls.UserControl
    {
        public virtual void Search() { }

        public int UserControlID { get; set; }

        //TODO :rad
        /// <summary>
        /// لیستی  از دیتا را گرفته و با استفاده از الگوی از قبل ایجاد شده یک گزارش را نمایش میدهد
        /// </summary>
        /// <param name="result">لیست از دیتا برای گزارش</param>
        /// <param name="usercontrolName">نوع گزارش-البته مقدارش برابر شناسه گزارش در جدول الگوی گزارشهاست</param>
        public static void SendToPrint(IEnumerable dataForReport, int usercontrolName)
        {
            try
            {
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                string path = ReportDB.GetReportPath(usercontrolName);
                if (!string.IsNullOrEmpty(path))
                {
                    stiReport.Load(path);
                    stiReport.CacheAllData = true;
                    stiReport.RegData("Result", "Result", dataForReport);
                    if (stiReport != null)
                    {
                        ReportViewerForm reportViewer = new ReportViewerForm(stiReport);
                        reportViewer.ShowDialog();
                    }
                }
                else
                {
                    throw new Exception("!الگوی برای این گزارش یافت نشد");
                }
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex);
                throw;
            }
        }

        public static void SendToPrint(IEnumerable dataForReport, int usercontrolName, params StiVariable[] variables)
        {
            try
            {
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                string path = ReportDB.GetReportPath(usercontrolName);
                if (!string.IsNullOrEmpty(path))
                {
                    stiReport.Load(path);
                    stiReport.CacheAllData = true;
                    stiReport.RegData("Result", "Result", dataForReport);
                    if (variables.Length > 0)
                    {
                        foreach (StiVariable vari in variables)
                        {
                            stiReport.Dictionary.Variables.Add(vari);
                        }
                    }
                    if (stiReport != null)
                    {
                        ReportViewerForm reportViewer = new ReportViewerForm(stiReport);
                        reportViewer.ShowDialog();
                    }
                }
                else
                {
                    throw new Exception("!الگوی برای این گزارش یافت نشد");
                }
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex);
                throw;
            }
        }

        /// <summary>
        /// .به عنوان پارامتر یک دیکشنری میگیرد که کلید اجزای آن نام دیتا سورس گزارش و مقدار آن دیتای متناظر است
        /// </summary>
        /// <param name="userControlName"></param>
        /// <param name="nameWithDataPair"></param>
        /// <param name="variables"></param>
        public static void SendToPrint(int userControlName, Dictionary<string, IEnumerable> nameWithDataPair, params StiVariable[] variables)
        {
            try
            {
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                string path = ReportDB.GetReportPath(userControlName);
                if (!string.IsNullOrEmpty(path))
                {
                    stiReport.Load(path);
                    stiReport.CacheAllData = true;
                    if (nameWithDataPair.Count != 0)
                    {
                        foreach (KeyValuePair<string, IEnumerable> dicItem in nameWithDataPair)
                        {
                            stiReport.RegData(dicItem.Key, dicItem.Key, dicItem.Value);
                        }
                    }
                    if (variables.Length > 0)
                    {
                        foreach (StiVariable item in variables)
                        {
                            stiReport.Dictionary.Variables.Add(item);
                        }
                    }
                    if (stiReport != null)
                    {
                        ReportViewerForm reportViewer = new ReportViewerForm(stiReport);
                        reportViewer.ShowDialog();
                    }
                }
                else
                {
                    throw new Exception("!الگوی برای این گزارش یافت نشد");
                }
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex);
                throw;
            }
        }

        public static void SendToPrint(IEnumerable dataForReport, int usercontrolName, bool hasDefaultTitle, params StiVariable[] variables)
        {
            try
            {
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                string path = ReportDB.GetReportPath(usercontrolName);
                if (!string.IsNullOrEmpty(path))
                {
                    stiReport.Load(path);
                    stiReport.CacheAllData = true;
                    stiReport.RegData("Result", "Result", dataForReport);
                    if (variables.Length > 0)
                    {
                        foreach (StiVariable vari in variables)
                        {
                            stiReport.Dictionary.Variables.Add(vari);
                        }
                    }

                    if (hasDefaultTitle)
                    {
                        string reportTitle = ReportDB.GetReportTitleByReportTemplateID(usercontrolName);
                        StiVariable titleVariable = new StiVariable("ReportTitle", "ReportTitle", reportTitle);
                        stiReport.Dictionary.Variables.Add(titleVariable);
                    }

                    if (stiReport != null)
                    {
                        ReportViewerForm reportViewer = new ReportViewerForm(stiReport);
                        reportViewer.ShowDialog();
                    }
                }
                else
                {
                    throw new Exception("!الگوی برای این گزارش یافت نشد");
                }
            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex);
                throw;
            }
        }
    }
}
