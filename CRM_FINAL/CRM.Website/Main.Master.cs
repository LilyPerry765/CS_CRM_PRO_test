using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI;
using CRM.Data;
using Telerik.Web.UI;
using CRM.Website.Viewes;
using CRM.Data;


namespace CRM.Website
{
    public partial class Main : System.Web.UI.MasterPage
    {
        #region Properties & Fields

        public OfficeRibbon MainRibbon { get { return this.OfficeRibbon; } }

        public Manager MainManager { get { return this.RibbonManager; } }

        public List<CRM.Data.ExtendedPair> TabPanels
        {
            get
            {
                if (HttpContext.Current.Session["CRMFooterTabPanels"] == null)
                    HttpContext.Current.Session["CRMFooterTabPanels"] = new List<CRM.Data.ExtendedPair>();
                return HttpContext.Current.Session["CRMFooterTabPanels"] as List<CRM.Data.ExtendedPair>;

            }
            set
            {
                HttpContext.Current.Session["CRMFooterTabPanels"] = value;
            }
        }

        private static List<string> _tabWindowClassNames = new List<string>() { "RequestsInbox", "RequestDashboard" };

        private byte _requestTypeID;

        private byte _mode;

        private long _telephoneNo;

        #endregion

        #region Events

        protected void ShowRequestButton_Click(object sender, EventArgs e)
        {
            try
            {
                byte.TryParse(Request.QueryString["Mode"], out _mode);
                long.TryParse(PhoneNoTextBox.Text.Trim().Replace(" ", "").TrimStart('0').ToString(), out _telephoneNo);
                byte.TryParse(HttpContext.Current.Session["CRMRequestTypeID"].ToString(), out _requestTypeID);
                //// check user access to center of telephone
                //AssignmentInfo assingmentInfo = DB.GetAllInformationByTelephoneNo(_telephoneNo);
                //if (assingmentInfo != null && !DB.CurrentUser.CenterIDs.Contains(assingmentInfo.CenterID))
                //    throw new Exception("تلفن متعلق به مرکز " + assingmentInfo.CenterName + "می باشد. دسترسی شما شامل این مرکز نمی باشد . ");

                //// check to exist telephone on other request
                //if (_requestTypeID != (byte)DB.RequestType.ADSLChangeService && _requestTypeID != (byte)DB.RequestType.ADSL && _requestTypeID != (byte)DB.RequestType.Failure117)
                //{
                //    string requestName = Data.RequestDB.GetOpenRequestNameTelephone(_telephoneNo);
                //    if (!string.IsNullOrWhiteSpace(requestName))
                //        throw new Exception("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                //}

                if (_requestTypeID != (byte)DB.RequestType.ADSLChangeService && _requestTypeID != (byte)DB.RequestType.ADSL && _requestTypeID != (byte)DB.RequestType.Failure117 && _requestTypeID != (byte)DB.RequestType.ADSLChangeIP)
                {
                    // check user access to center of telephone
                    AssignmentInfo assingmentInfo = DB.GetAllInformationByTelephoneNo(_telephoneNo);
                    if (assingmentInfo != null && !DB.CurrentUser.CenterIDs.Contains(assingmentInfo.CenterID))
                        throw new Exception("تلفن متعلق به مرکز " + assingmentInfo.CenterName + "می باشد. دسترسی شما شامل این مرکز نمی باشد . ");

                    // check to exist telephone on other request
                    string requestName = Data.RequestDB.GetOpenRequestNameTelephone(_telephoneNo);
                    if (!string.IsNullOrWhiteSpace(requestName))
                        throw new Exception("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                }


                switch (_requestTypeID)
                {
                    case (byte)DB.RequestType.ADSL:
                    case (byte)DB.RequestType.ADSLChangeService:
                        CheckConditions();
                        PhoneNoTextBox.Text = string.Empty;
                        DialogWindow.Style.Add("display", "none");
                        Page page = HttpContext.Current.CurrentHandler as Page;
                        ScriptManager.RegisterStartupScript(page, page.GetType(), "OpenModalDialog", string.Format("<script type=text/javascript>window.showModalDialog('/Viewes/RequestForm.aspx?RequestTypeID={0}&Mode={1}&TelephoneNo={2}', null, 'dialogHeight:750px;dialogWidth:780px;status:no;center:yes'); </script>", _requestTypeID, _mode, _telephoneNo), false);
                        break;
                }
            }

            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                Response.Write(string.Format("<script>alert('{0}');</script>", message));
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AddTab("داشبورد درخواست");
                AddPageView(true, "RequestDashboard" + "PageView");

                //MasterRadTabStrip.Tabs.Add(new RadTab() { Text = string.Empty });

                //OfficeWebUI.Ribbon.RibbonTab ribbonTab = OfficeRibbon.Contexts[1].Tabs.Where(t => t.Text == "مشترکین").SingleOrDefault();
                //OfficeWebUI.Ribbon.RibbonGroup ribbonGroup = ribbonTab.Groups.Where(t => t.Text == "کارتابل").SingleOrDefault();
                //OfficeWebUI.Ribbon.GroupZone groupZone = ribbonGroup.Zones[0];
                //OfficeWebUI.Ribbon.LargeItem largeItem = groupZone.Content.Where(t => t.GetType() == typeof(OfficeWebUI.Ribbon.LargeItem) && (t as OfficeWebUI.Ribbon.LargeItem).ID == "RequestDashboard").SingleOrDefault() as OfficeWebUI.Ribbon.LargeItem;
                //item_Click(largeItem, null);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DialogWindow.Style.Clear();
            TabsHeaderTemplate tabTemplate = new TabsHeaderTemplate();
            foreach (RadTab tab in MasterRadTabStrip.Tabs)
            {
                if (tab.Text != "داشبورد درخواست")
                    tabTemplate.InstantiateIn(tab);
            }

            foreach (RadPageView pageview in MasterRadMultiPage.PageViews)
            {
                CreatePageViewControls(pageview, pageview.ID);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            //List<FolderMenuInfo> selectedMenues = SecurityDB.GetFolderMenuInfoesCheckableForWeb(4).Where(t => t.IsChecked == true).ToList();
            List<FolderMenuInfo> selectedMenues = SecurityDB.GetFolderMenuInfoesCheckableForWeb(DB.CurrentUser.RoleID).ToList();

            OfficeWebUI.Ribbon.RibbonContext lNewContext = new OfficeWebUI.Ribbon.RibbonContext();
            OfficeRibbon.Contexts.Add(lNewContext);

            foreach (FolderMenuInfo folderMenuItem in selectedMenues)
            {
                /* Get or create menu tab
                 -----------------------------------------------------------------------------*/
                OfficeWebUI.Ribbon.RibbonTab tab;
                if (!lNewContext.Tabs.Select(t => t.Text).Contains(folderMenuItem.Name))
                {
                    tab = new OfficeWebUI.Ribbon.RibbonTab();
                    tab.ID = folderMenuItem.Name;
                    tab.Text = folderMenuItem.Name;
                    lNewContext.Tabs.Add(tab);
                }
                else
                {
                    tab = lNewContext.Tabs.Where(t => t.Text.Trim() == folderMenuItem.Name.Trim()).FirstOrDefault();
                }

                /* Get or create menu group
                -----------------------------------------------------------------------------*/
                OfficeWebUI.Ribbon.RibbonGroup group;
                OfficeWebUI.Ribbon.GroupZone zone;
                if (!tab.Groups.Select(t => t.Text).Contains(folderMenuItem.GroupName))
                {
                    group = new OfficeWebUI.Ribbon.RibbonGroup();
                    group.Text = folderMenuItem.GroupName;
                    tab.Groups.Add(group);

                    zone = new OfficeWebUI.Ribbon.GroupZone();
                    group.Zones.Add(zone);
                }
                else
                {
                    group = tab.Groups.Where(t => t.Text.Trim() == folderMenuItem.GroupName.Trim()).FirstOrDefault();
                    zone = group.Zones.FirstOrDefault();
                }

                /* Get or create menu item
                ----------------------------------------------------------------------------*/
                OfficeWebUI.Ribbon.LargeItem item;
                if (!tab.Groups.Select(t => t.Text).Contains(folderMenuItem.Header))
                {
                    item = new OfficeWebUI.Ribbon.LargeItem();
                    item.Text = folderMenuItem.Header;
                    item.Tooltip = folderMenuItem.Header;
                    item.ID = folderMenuItem.ClassName;
                    item.ImageUrl = folderMenuItem.Icon;
                    item.HasTabWindow = _tabWindowClassNames.Contains(folderMenuItem.ClassName);
                    if (_tabWindowClassNames.Contains(folderMenuItem.ClassName))
                        item.Click += new EventHandler(item_Click);
                    else
                        item.Click += new EventHandler(ShowDialogWindow);
                    zone.Content.Add(item);
                }
            }

            base.OnInit(e);
        }

        protected void item_Click(object sender, EventArgs e)
        {
            OfficeWebUI.Ribbon.LargeItem ribbonItem = sender as OfficeWebUI.Ribbon.LargeItem;
            foreach (RadTab item in MasterRadTabStrip.Tabs)
            {
                if (item.Text == ribbonItem.Text)
                {
                    MasterRadTabStrip.SelectedIndex = item.Index;
                    MasterRadMultiPage.SelectedIndex = item.PageView.Index;
                    UserControl.RequestsInbox requestInboxControl = item.PageView.FindControl("RequestsInboxPageView_userControl") as UserControl.RequestsInbox;
                    if (requestInboxControl != null)
                        requestInboxControl.Search();

                    return;
                }
            }

            AddTab(ribbonItem.Text);
            AddPageView(true, ribbonItem.ID + "PageView");
        }

        protected void ShowDialogWindow(object sender, EventArgs e)
        {
            switch ((sender as OfficeWebUI.Ribbon.LargeItem).ID)
            {
                case "ADSLTelephoneNoInput":
                    HttpContext.Current.Session["CRMRequestTypeID"] = (byte)DB.RequestType.ADSL;
                    break;

                case "ADSLChangeServiceTelephoneNoInput":
                    HttpContext.Current.Session["CRMRequestTypeID"] = (byte)DB.RequestType.ADSLChangeService;
                    break;

                default:
                    break;
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "PopItUp", "PopItUp();", true);
        }

        //public void OnRibbonLargeItemClick(string itemText)
        //{
        //    OfficeWebUI.Ribbon.LargeItem ribbonItem = new OfficeWebUI.Ribbon.LargeItem();
        //    ribbonItem.Text = itemText;
        //    item_Click(ribbonItem, null);
        //}

        #endregion

        #region Methods

        private void AddTab(string title)
        {
            TabsHeaderTemplate tabTemplate = new TabsHeaderTemplate();
            RadTab tab = new RadTab() { Text = title };
            tabTemplate.InstantiateIn(tab);
            //mas MasterRadTabStrip.Tabs.Insert((MasterRadTabStrip.Tabs.Count - 1), tab);
            MasterRadTabStrip.Tabs.Insert((MasterRadTabStrip.Tabs.Count), tab);
            MasterRadTabStrip.DataBind();
            MasterRadTabStrip.SelectedIndex = tab.Index;
        }

        private void AddPageView(bool isNew, string pageViewID)
        {
            RadPageView pageView = new RadPageView();

            if (isNew)
                CreatePageViewControls(pageView, pageViewID);
            else
                pageView.ID = pageViewID;

            MasterRadMultiPage.PageViews.Add(pageView);
            MasterRadMultiPage.SelectedIndex = pageView.Index;
        }

        private RadPageView CreatePageViewControls(RadPageView pageView, string pageViewID)
        {
            pageView.ID = pageViewID;
            pageView.CssClass = "pageview";
            switch (pageViewID)
            {
                case "RequestsInboxPageView":
                    UserControl.RequestsInbox requestsInbox = LoadControl("~/UserControl/RequestsInbox.ascx") as UserControl.RequestsInbox;
                    requestsInbox.ID = pageView.ID + "_userControl";
                    if (!string.IsNullOrEmpty(RequestStepIDDummyHidden.Value))
                    {
                        int requestStepID = 0;
                        int.TryParse(RequestStepIDDummyHidden.Value, out requestStepID);
                        requestsInbox.SelectedStepID = requestStepID;
                        RequestStepIDDummyHidden.Value = string.Empty;
                    }
                    pageView.Controls.Add(requestsInbox);
                    break;

                case "RequestDashboardPageView":
                    pageView.Controls.Clear();
                    UserControl.RequestDashboard requestDashboard = LoadControl("~/UserControl/RequestDashboard.ascx") as UserControl.RequestDashboard;
                    requestDashboard.ID = pageView.ID + "_userControl";
                    pageView.Controls.Add(requestDashboard);
                    break;

                default:
                    break;

            }
            return pageView;
        }

        private void CheckConditions()
        {
            switch (_requestTypeID)
            {
                case (byte)DB.RequestType.ADSL:
                    CheckADSLConditions();
                    break;

                case (byte)DB.RequestType.ADSLChangeService:
                    CheckADSLChangeServiceConditions();
                    break;

                default:
                    break;
            }
        }

        private void CheckADSLConditions()
        {
            Service1 aDSLService = new Service1();

            if (!aDSLService.Is_Phone_Exist(_telephoneNo.ToString()))
                throw new Exception("* شماره وارد شده موجود نمی باشد !");

            if (aDSLService.TelDissectionStatus(_telephoneNo.ToString()))
                throw new Exception("* شماره وارد شده قطع می باشد !");

            if (aDSLService.Tel_Have_ADSl_Port(_telephoneNo.ToString()))
                throw new Exception("* شماره وارد شده دارای ADSL می باشد !");


            List<Request> aDSLRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_telephoneNo, (int)DB.RequestType.ADSL).Where(t => t.EndDate == null).ToList();
            if (aDSLRequests.Count != 0)
                throw new Exception("برای این شماره در حال حاضر درخواست ADSL موجود می باشد !");

            System.Data.DataTable telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", _telephoneNo.ToString());

            string centerCode = telephoneInfo.Rows[0]["CENTERCODE"].ToString();
            int centerID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(centerCode));
            int cabinetNo = Convert.ToInt32(telephoneInfo.Rows[0]["KAFU_NUM"].ToString());

            if (ADSLPAPCabinetAccuracyDB.CheckCabinetAccuracy(cabinetNo, centerID))
                throw new Exception("این شماره در کافویی است که امکان تخصیص ADSL ندارد !");

        }

        private void CheckADSLChangeServiceConditions()
        {

            ADSL ADSL = ADSLDB.GetADSLByTelephoneNo(_telephoneNo);

            if (ADSL == null)
                throw new Exception("این شماره دارای سرویس ADSL نمی باشد !");
            else
            {
                if (ADSL.PAPInfoID != null)
                    throw new Exception("سرویس ADSL این شماره از شرکت PAP گرفته شده است !");

                switch (ADSL.Status)
                {
                    case (byte)DB.ADSLStatus.Cut:
                        throw new Exception("سرویس ADSL برای این شماره قطع موقت می باشد !");

                    case (byte)DB.ADSLStatus.Discharge:
                        throw new Exception("سرویس ADSL برای این شماره تخلیه شده است !");

                    default:
                        break;
                }
            }

        }

        #endregion

        protected void DummyLink_Click(object sender, EventArgs e)
        {
            OfficeWebUI.Ribbon.LargeItem ribbonItem = new OfficeWebUI.Ribbon.LargeItem();
            ribbonItem.Text = RibbonItemTextDummyHidden.Value;
            ribbonItem.ID = RibbonItemIDDummyHidden.Value;
            item_Click(ribbonItem, null);

        }
    }

    public class TabsHeaderTemplate : ITemplate
    {
        public void InstantiateIn(System.Web.UI.Control container)
        {
            Label tabLabel = new Label();
            tabLabel.ID = "tabLabel";
            tabLabel.CssClass = "tablabel";
            tabLabel.Text = (container as RadTab).Text;
            container.Controls.Add(tabLabel);

            if (tabLabel.Text != string.Empty && tabLabel.Text != "داشبورد درخواست")
            {
                ImageButton closeImage = new ImageButton();
                closeImage.ID = "closeImage";
                closeImage.Width = Unit.Pixel(9);
                closeImage.Height = Unit.Pixel(9);
                closeImage.Click += closeImage_Click;
                closeImage.Attributes["value"] = "";
                closeImage.ImageUrl = "~/Images/Delete - Copy.png";
                closeImage.Style.Add("margin-top", "8px");
                closeImage.CssClass = "imageButton";
                container.Controls.Add(closeImage);
            }
        }

        void closeImage_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton image = (ImageButton)sender;
            RadTab tab = image.Parent as RadTab;

            if (tab.Selected)
            {
                tab.TabStrip.SelectedIndex = tab.Index - 1;
                tab.PageView.MultiPage.SelectedIndex = tab.Index - 1;
            }

            tab.PageView.MultiPage.PageViews.Remove(tab.PageView);
            tab.TabStrip.Tabs.Remove(tab);
        }
    }
}