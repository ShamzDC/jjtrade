using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDAL;
using BAL;
using System.Data;
using System.IO;
using System.Drawing;
using System.Security.Cryptography;

namespace Client.Admin
{
    public partial class UserDetails : System.Web.UI.Page
    {
        List<UserDetails_CDAL>  listUserDetails_CDAL  = new List<UserDetails_CDAL>();
        int tcode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                    lblUser.Text = Session["UserName"].ToString();
                    
                    LoadUsers();
                if (Session["IsAdmin"].ToString() != "1")
                {
                    ddlEmpID.SelectedValue = Session["EmpID"].ToString();
                    ddlEmpID.Enabled = false;
                    txtUserName.Text= Session["UserID"].ToString().Trim();
                    txtPassword.Text = Session["Pwd"].ToString().Trim();
                }

            }
        }

        private void LoadUsers()
        {
            DataSet ds = new DataSet();
            EmployeeMaster_BAL lEmployeeMaster_BAL = new EmployeeMaster_BAL();
            lEmployeeMaster_BAL.lEmployeeMaster_CDAL = new EmployeeMaster_CDAL();
            lEmployeeMaster_BAL.lEmployeeMaster_CDAL.flag = "SelectSavedEmployee";
            ds = lEmployeeMaster_BAL.GetEmployeeMaster();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlEmpID.DataSource = ds.Tables[0];
                    ddlEmpID.DataTextField = "Empname";
                    ddlEmpID.DataValueField = "Empid";
                    ddlEmpID.DataBind();
                    ddlEmpID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Employee--", "0"));

                }
            } 

        }

        protected void ddlEmpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            EmployeeMaster_BAL lEmployeeMaster_BAL = new EmployeeMaster_BAL();
            lEmployeeMaster_BAL.lEmployeeMaster_CDAL = new EmployeeMaster_CDAL();
            lEmployeeMaster_BAL.lEmployeeMaster_CDAL.flag = "SelectSavedEmployee_ID";
            lEmployeeMaster_BAL.lEmployeeMaster_CDAL.Empid=  Convert.ToInt32(ddlEmpID.SelectedValue.ToString());  
           ds = lEmployeeMaster_BAL.GetEmployeeMaster();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                { 
                    txtUserName.Text = ds.Tables[0].Rows[0]["Username"].ToString();   
                    txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();

                }
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int td;
                
                UserDetails_BAL lUserDetails_BAL = new UserDetails_BAL();
                lUserDetails_BAL.lUserDetails_CDAL = new UserDetails_CDAL();
                lUserDetails_BAL.lUserDetails_CDAL.UserID = ddlEmpID.SelectedValue.ToString();
                lUserDetails_BAL.lUserDetails_CDAL.UserName = txtUserName.Text.Trim();
                lUserDetails_BAL.lUserDetails_CDAL.Pwd = txtPassword.Text;
                 

                td = lUserDetails_BAL.InsUptDel("PasswordChange");
                if (td > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('PasswordChange Successfully')", true);
                     
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Try Again')", true);
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
            ddlEmpID.SelectedIndex = 0;
            txtUserName.Text = "";
            txtPassword.Text = "";
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
        }

         
        protected void btnAdd_Click(object sender, EventArgs e)
        {
             
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
             
            LoadUsers();
        }
        
         
        
         
    }
}