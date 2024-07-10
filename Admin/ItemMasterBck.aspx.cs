using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDAL;
using BAL;

namespace KingoClient.Admin
{
    public partial class ItemMasterBck : System.Web.UI.Page
    {
        List<ItemMaster_CDAL> listItemMaster_CDAL = new List<ItemMaster_CDAL>();
        int icode;
        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["adminid"] != null)
                {
                    lblUser.Text = Session["UserName"].ToString();
                    PnlGrid.Visible = true;
                    PnlControl.Visible = false;
                    loaditemdata();
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
                    PnlGrid.Visible = true;
                    PnlControl.Visible = false;
                    loaditemdata();
                }
                else
                {
                    DML("Update", Convert.ToInt32(txtItemId.Text));
                    PnlGrid.Visible = true;
                    PnlControl.Visible = false;
                    loaditemdata();
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
        private void DML(string flg, int itid)
        {
            int it;
            icode = Convert.ToInt32(itid);
            ItemMaster_BAL lItemMaster_BAL = new ItemMaster_BAL();
            lItemMaster_BAL.lItemMaster_CDAL = new ItemMaster_CDAL();
            lItemMaster_BAL.lItemMaster_CDAL.ItemId = icode;
            lItemMaster_BAL.lItemMaster_CDAL.ItemCode = txtitemcode.Text;
            lItemMaster_BAL.lItemMaster_CDAL.ItemName = txtItemName.Text.Trim();
            lItemMaster_BAL.lItemMaster_CDAL.Description = txtdescription.Text;
            lItemMaster_BAL.lItemMaster_CDAL.HourBefore = txtHr.Text;
            if (chkStatus.Checked == true)
                lItemMaster_BAL.lItemMaster_CDAL.Isactive = "Y";
            else
                lItemMaster_BAL.lItemMaster_CDAL.Isactive = "N";
          

            it = lItemMaster_BAL.Ins_Upd_Del(flg);
            if (it > 0)
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
                //else if (ctrl.GetType() == typeof(DropDownList))
                //{
                //    ((DropDownList)(ctrl)).SelectedIndex = 0;
                //}
            }
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
        }
        #endregion

        #region loaddata
        protected void loaditemdata()
        {
            ItemMaster_BAL lItemMaster_BAL = new ItemMaster_BAL();
            listItemMaster_CDAL = lItemMaster_BAL.LoadItem();
            GridItem.DataSource = listItemMaster_CDAL;
            GridItem.DataBind();
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
            loaditemdata();
        }
        #endregion

        #region Edit
        protected void Edit(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            PnlControl.Visible = true;
            PnlGrid.Visible = false;
            icode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            listItemMaster_CDAL = new List<ItemMaster_CDAL>();
            ItemMaster_BAL lItemMaster_BAL = new ItemMaster_BAL();
            lItemMaster_BAL.lItemMaster_CDAL = new ItemMaster_CDAL();
            lItemMaster_BAL.lItemMaster_CDAL.ItemId = icode;
            listItemMaster_CDAL = lItemMaster_BAL.LoadItem();
            if (listItemMaster_CDAL.Count > 0)
            {
                btnSave.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                txtItemId.Text = listItemMaster_CDAL[0].ItemId.ToString();
                txtitemcode.Text = listItemMaster_CDAL[0].ItemCode;
                txtItemName.Text = listItemMaster_CDAL[0].ItemName;
                txtdescription.Text = listItemMaster_CDAL[0].Description;
                chkStatus.Text = listItemMaster_CDAL[0].Isactive;
                txtHr.Text = listItemMaster_CDAL[0].HourBefore;
            }
        }
        #endregion

        #region Delete
        protected void Delete(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            DML("Delete", Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString()));
            loaditemdata();
        }
        #endregion
    }
}