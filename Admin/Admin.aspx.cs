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
    public partial class Admin : System.Web.UI.Page
    {
        List<Admin_CDAL> listAdmin_CDAL = new List<Admin_CDAL>();
        int mcode;
        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["adminid"] != null)
                {
                    lblUser.Text = Session["UserName"].ToString();
                    PnlControl.Visible = false;
                    PnlGrid.Visible = true;
                    LoadAdmin();
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Login to View this Page');window.location='/Admin/Adminlogin.aspx';</script>");
                }
            }
        }
        #endregion

        #region Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.InnerHtml == "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save")
                {
                    DML("Insert", 0);
                    PnlControl.Visible = false;
                    PnlGrid.Visible = true;
                    LoadAdmin();
                }
                else
                {
                    DML("Update", Convert.ToInt32(txtadminid.Text));
                    PnlControl.Visible = false;
                    PnlGrid.Visible = true;
                    LoadAdmin();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                btnClear_Click(sender, e);
            }
        }
        #endregion

        #region DML
        private void DML(string flg, int mid)
        {
            int mm;
            mcode = Convert.ToInt32(mid);
            Admin_BAL lAdmin_BAL = new Admin_BAL();
            lAdmin_BAL.lAdmin_CDAL = new Admin_CDAL();
            lAdmin_BAL.lAdmin_CDAL.adminid = mcode;
            lAdmin_BAL.lAdmin_CDAL.adminname = txtadminname.Text.Trim();
            lAdmin_BAL.lAdmin_CDAL.username = txtusername.Text.Trim();
            lAdmin_BAL.lAdmin_CDAL.password = txtpassword.Text.Trim();
            lAdmin_BAL.lAdmin_CDAL.emailid = txtemail.Text.Trim();
            lAdmin_BAL.lAdmin_CDAL.phone = txtphone.Text.Trim();
            mm = lAdmin_BAL.Ins_Upd_Del(flg);
            if (mm > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmessage", "alert('" + flg + " Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmessage", "alert(Try again)", true);
            }
        }
        #endregion

        #region Clear
        protected void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in PnlControl.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    ((TextBox)(ctrl)).Text = string.Empty;
                }

            }
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
        }
        #endregion

        #region loaddata
        protected void LoadAdmin()
        {
            Admin_BAL lAdmin_BAL = new Admin_BAL();
            listAdmin_CDAL = lAdmin_BAL.LoadAdmin();
            GridAdmin.DataSource = listAdmin_CDAL;
            GridAdmin.DataBind();
        }
        #endregion

        #region Add
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PnlControl.Visible = true;
            PnlGrid.Visible = false;
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
        }
        #endregion

        #region Modify
        protected void btnModify_Click(object sender, EventArgs e)
        {
            PnlControl.Visible = false;
            PnlGrid.Visible = true;
            LoadAdmin();
        }
        #endregion

        #region Edit
        protected void Edit(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            PnlControl.Visible = true;
            PnlGrid.Visible = false;
            mcode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            listAdmin_CDAL = new List<Admin_CDAL>();
            Admin_BAL lAdmin_BAL = new Admin_BAL();
            lAdmin_BAL.lAdmin_CDAL = new Admin_CDAL();
            lAdmin_BAL.lAdmin_CDAL.adminid = mcode;
            listAdmin_CDAL = lAdmin_BAL.LoadAdmin();
            if (listAdmin_CDAL.Count > 0)
            {
                btnSave.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                txtadminid.Text = listAdmin_CDAL[0].adminid.ToString();
                txtadminname.Text = listAdmin_CDAL[0].adminname;
                txtusername.Text = listAdmin_CDAL[0].username;
                txtpassword.Text = listAdmin_CDAL[0].password;
                txtemail.Text = listAdmin_CDAL[0].emailid;
                txtphone.Text = listAdmin_CDAL[0].phone;
            }
        }
        #endregion

        #region Delete
        protected void Delete(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            DML("Delete", Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString()));
            LoadAdmin();
        }
        #endregion
    }
}