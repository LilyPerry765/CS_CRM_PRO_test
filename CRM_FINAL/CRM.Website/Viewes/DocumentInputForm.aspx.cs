using CRM.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.Website.Viewes
{
    public partial class DocumentInputForm : System.Web.UI.Page
    {
        #region Properties & Fields

        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session["FileBytes"] = string.Empty;
            HttpContext.Current.Session["Extension"] = string.Empty;
        }

        protected void FromFile_Click(object sender, EventArgs e)
        {
            AttachmentFileUpload.Style.Add("display","block");
            SaveButton.Style.Add("display", "block");
        }

        protected void FromScanner_Click(object sender, EventArgs e)
        {
            Scanner oScanner = new Scanner();
            string extension;

            HttpContext.Current.Session["FileBytes"] = oScanner.ScannWithExtension(out extension);
            HttpContext.Current.Session["Extension"] = extension;
            Response.Write("<script> window.close(); </script>");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (AttachmentFileUpload.HasFile)
            {
                HttpContext.Current.Session["FileBytes"] = AttachmentFileUpload.FileBytes;
                HttpContext.Current.Session["Extension"] = System.IO.Path.GetExtension(AttachmentFileUpload.FileName);
            }
            Response.Write("<script> window.close(); </script>");
        }
    }
}