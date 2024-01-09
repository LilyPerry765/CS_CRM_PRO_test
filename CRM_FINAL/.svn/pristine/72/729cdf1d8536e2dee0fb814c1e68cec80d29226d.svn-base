using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using CRM.Data;


namespace CRM.Website.Viewes
{
    public partial class DocumentViewForm : System.Web.UI.Page
    {
        private byte[] _fileBytes;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Session["FileBytes"] != null)
                    _fileBytes = HttpContext.Current.Session["FileBytes"] as byte[];
                else
                    return;

                if (_fileBytes != null)
                {
                    long requestID = 0;
                    long customerID = 0;
                    long documentRequestTypeID = 0;
                    byte typeID = 0;
                    long announceID = 0;
                    long documentTypeID = 0;
                    string fileType = string.Empty;

                    long.TryParse(Request.QueryString["RequestID"], out requestID);
                    long.TryParse(Request.QueryString["CustomerID"], out customerID);
                    long.TryParse(Request.QueryString["DocumentRequestTypeID"], out documentRequestTypeID);
                    byte.TryParse(Request.QueryString["TypeID"], out typeID);
                    long.TryParse(Request.QueryString["AnnounceID "], out announceID);
                    long.TryParse(Request.QueryString["DocumentTypeID"], out documentTypeID);
                    fileType = Request.QueryString["FileType"];

                    string tempPath;
                    string filePath;

                    tempPath = Server.MapPath("~/Files/RequestDocuments");
                    string fileName = string.Format("CRM_ {0}_ {1}_ {2}_ {3}_ {4}_ {5}.{6}", requestID, customerID, documentRequestTypeID, typeID, announceID, documentTypeID, fileType);
                    filePath = System.IO.Path.Combine(tempPath, fileName);

                    if (!System.IO.File.Exists(filePath))
                    {
                        Enterprise.Logger.WriteInfo("Attached file is loading to : '{0}'", filePath);
                        File.WriteAllBytes(filePath, _fileBytes);
                    }
                    Response.Redirect("/Files/RequestDocuments/" + fileName, false);
                    //System.Diagnostics.Process.Start(fileName);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('خطا : " + message + "'); window.close(); ", true);
            }
        }
    }
}