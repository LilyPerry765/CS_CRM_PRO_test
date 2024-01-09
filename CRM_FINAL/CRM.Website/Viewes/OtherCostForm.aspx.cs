using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using CRM.Application;

namespace CRM.Website.Viewes
{
    public partial class OtherCostForm : System.Web.UI.Page
    {
        #region Properties & Fields

        private int _ID = 0;
        OtherCost otherCost { get; set; }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadData();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            //{
            //    return;
            //}
            try
            {
                byte result;
                long longResult;
                otherCost.WorkUnit = byte.TryParse(WorkUnitDropDownList.SelectedValue, out result) ?(byte?) byte.Parse(WorkUnitDropDownList.SelectedValue) : null;
                otherCost.IsActive = byte.Parse(IsActiveDropDownList.SelectedValue);
                otherCost.InsertDate = Helper.PersianToGregorian(InsertDateTextBox.Text).HasValue ? Helper.PersianToGregorian(InsertDateTextBox.Text).Value : DB.GetServerDate(); 
                otherCost.RequestID = long.TryParse(RequestTextBox.Text, out longResult) ? (long?)long.Parse(RequestTextBox.Text) : null;
                otherCost.BasePrice = long.Parse(BasePriceTextBox.Text);
                otherCost.CostTitle = CostTitleTextBox.Text;
                otherCost.Reason = ReasonTextBox.Text;

                otherCost.Detach();
                DB.Save(otherCost);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "EndAddingOtherCostScript", string.Format("alert('هزینه متفرقه ذخیره شد'); window.returnValue = {0};  window.close();", otherCost.ID), true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                {
                    string message = ex.Message.Replace("\'", "");
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + " مقادیر وارد شده در پایگاه داده وجود دارد " + message + " !" + "');", true);
                }

                else
                {
                    string message = ex.Message.Replace("\'", "");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + " خطا در ذخیره هزینه متفرقه " + message + " !" + "');", true);
                }
            }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            WorkUnitDropDownList.DataSource = Helper.GetEnumItem(typeof(DB.WorkUnit));
            WorkUnitDropDownList.DataBind();
            IsActiveDropDownList.DataSource = Helper.GetEnumItem(typeof(DB.IsActive));
            IsActiveDropDownList.DataBind();

            otherCost = new OtherCost();
            otherCost.InsertDate = DB.GetServerDate();

            InsertDateTextBox.Text = Date.GetPersianDate(otherCost.InsertDate, Date.DateStringType.Short);
        }

        private void LoadData()
        {
            if (_ID == 0)
            {
                Initialize();
                SaveButton.Text = "ذخیره";
            }
            else
            {
                SaveButton.Text = "بروزرسانی";
                otherCost = Data.OtherCostDB.GetOtherCostByID(_ID);
            }
        }

        #endregion

    }
}