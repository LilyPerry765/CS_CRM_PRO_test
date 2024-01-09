using CRM.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media;

namespace CRM.Website.UserControl
{
    public partial class DashboardCounter : System.Web.UI.UserControl
    {
        public string Header
        {
            set
            {
                HeaderLabel.Text = value;
            }
            get
            {
                return HeaderLabel.Text;
            }

        }
        public int RequestCount
        {
            set
            {
                RequestCountLabel.Text = value.ToString();
            }
        }

        public List<UserControl.DashboardCounterDetails> MainControls
        {
            set
            {
                //GridMiddlePanel.Controls.Clear();
                foreach (UserControl.DashboardCounterDetails control in value)
                {
                    GridMiddlePanel.Controls.Add(control); 
                }
            }
        }
        public int ColorNumber { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            GridHeaderBorderPanel.Style.Add("background-image", string.Format("url('../Images/Counter_Grid_Header_0{0}.png')", (((int)ColorNumber % 6) + 1)));
            GridFooterBorderPanel.Style.Add("background-image", string.Format("url('../Images/Counter_Grid_Footer_0{0}.png')", (((int)ColorNumber % 6) + 1)));
        }
    }
}