using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using CDAL;

namespace Client.Admin
{
    public partial class District : System.Web.UI.Page
    {
        List<District_CDAL> listDistrict_CDAL = new List<District_CDAL>();
        List<State_CDAL> listState_CDAL = new List<State_CDAL>();
        int dcode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["adminid"] != null)
                {
                    lblUser.Text = Session["UserName"].ToString();
                    pnlGrid.Visible = true;
                    pnlControl.Visible = false;
                    LoadDistrict();
                    LoadState();
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Login to View this Page');window.location='/Admin/Adminlogin.aspx';</script>");
                }
            }
        }

        private void LoadState()
        {
            State_BAL lState_BAL = new State_BAL();
            lState_BAL.lState_CDAL = new State_CDAL();
            listState_CDAL = lState_BAL.GetState();
            if (listState_CDAL.Count > 0)
            {
                ddlstate.DataSource = listState_CDAL;
                ddlstate.DataValueField = "StateID";
                ddlstate.DataTextField = "stateName";
                ddlstate.DataBind();
                ddlstate.Items.Insert(0, new ListItem("<--Select State-->", "0"));
            }
        }

        private void LoadDistrict()
        {
            listDistrict_CDAL = new List<District_CDAL>();
            District_BAL lDistrict_BAL = new District_BAL();
            listDistrict_CDAL = lDistrict_BAL.GetDistrict();
            GridDistrict.DataSource = listDistrict_CDAL;
            GridDistrict.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.InnerHtml == "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save")
                {
                    InsUptDel("Insert", 0);
                    pnlGrid.Visible = true;
                    pnlControl.Visible = false;
                    LoadDistrict();
                }
                else
                {
                    InsUptDel("Update", Convert.ToInt32(txtdistrictid.Text));
                    pnlGrid.Visible = true;
                    pnlControl.Visible = false;
                    LoadDistrict();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                btnClear_Click(sender, e);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in pnlControl.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    ((TextBox)(ctrl)).Text = string.Empty;
                }
                else if (ctrl.GetType() == typeof(DropDownList))
                {
                    ((DropDownList)(ctrl)).SelectedIndex = 0;
                    ((DropDownList)(ctrl)).Enabled = true;
                }
            }
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
        }

        // INSERT UPDATE DELETE Common Method//

        protected void InsUptDel(string flg, int did)
        {
            int dd;
            dcode = Convert.ToInt32(did);
            District_BAL lDistrict_BAL = new District_BAL();
            lDistrict_BAL.lDistrict_CDAL = new District_CDAL();
            lDistrict_BAL.lDistrict_CDAL.DistrictID = dcode;
            lDistrict_BAL.lDistrict_CDAL.StateID = Convert.ToInt32(ddlstate.SelectedValue);
            lDistrict_BAL.lDistrict_CDAL.DistrictName = txtdistrictname.Text.Trim();

            dd = lDistrict_BAL.InsUptDel(flg);
            if (dd > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + flg + " Successfully')", true);
                LoadDistrict();
                dcode = 0;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Try Again')", true);
            }

        }

        // INSERT UPDATE DELETE Common Method//
        protected void Edit(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            pnlGrid.Visible = false;
            pnlControl.Visible = true;
            District_BAL lDistrict_BAL = new District_BAL();
            dcode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            lDistrict_BAL.lDistrict_CDAL = new District_CDAL();
            lDistrict_BAL.lDistrict_CDAL.DistrictID = dcode;
            listDistrict_CDAL = lDistrict_BAL.GetDistrict();
            if (listDistrict_CDAL.Count > 0)
            {
                btnSave.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                txtdistrictid.Text = Convert.ToString(listDistrict_CDAL[0].DistrictID);
                ddlstate.Text = Convert.ToString(listDistrict_CDAL[0].StateID);
                txtdistrictname.Text = listDistrict_CDAL[0].DistrictName;
            }
        }

        protected void Delete(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            InsUptDel("Delete", Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString()));
            LoadDistrict();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlControl.Visible = true;
            pnlGrid.Visible = false;
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            pnlGrid.Visible = true;
            pnlControl.Visible = false;
            LoadDistrict();
        }
    }
}