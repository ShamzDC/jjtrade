using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDAL;
using BAL;
using DAL;
using System.Data;

namespace Client.Admin
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        //List<Admin_CDAL> listAdmin_CDAL = new List<Admin_CDAL>();        
        List<Login_CDAL> listLogin_CDAL = new List<Login_CDAL>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            /*
            if (txtusername.Text.Trim() != "")
            {
                Admin_BAL lAdmin_BAL = new Admin_BAL();
                lAdmin_BAL.lAdmin_CDAL = new Admin_CDAL();
                lAdmin_BAL.lAdmin_CDAL.username = txtusername.Text;
                lAdmin_BAL.lAdmin_CDAL.password = txtpassword.Text;
                listAdmin_CDAL = lAdmin_BAL.AdminLogin();
                if (listAdmin_CDAL.Count > 0)
                {
                    Session["adminid"] = listAdmin_CDAL[0].adminid;
                    Session["username"] = listAdmin_CDAL[0].username;
                    Response.Write("<script language='javascript'>window.alert('Welcome " + listAdmin_CDAL[0].username + "');window.location='AgentMaster.aspx';</script>");
                    //Response.Write("<script>alert('User details saved sucessfully');window.location ='AddProducts.aspx',true</script>");
                    //Response.Redirect("AddProducts.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Incorrect UserName or Password')</script>");
                }
            }
            */

            if (txtusername.Text.Trim() != "")
            {
                listLogin_CDAL = new List<Login_CDAL>();
                Login_BAL lLogin_BAL = new Login_BAL();
                lLogin_BAL.lLogin_CDAL = new Login_CDAL();
                lLogin_BAL.lLogin_CDAL.Username = txtusername.Text;
                lLogin_BAL.lLogin_CDAL.Password = txtpassword.Text;

                lLogin_BAL.lLogin_CDAL.IsAgent_Admin = ddl_Admin_Agent.SelectedValue.ToString();
                lLogin_BAL.lLogin_CDAL.flag = "Select_UserName";                
              
                ds = lLogin_BAL.GetUser(); 

                if (ds.Tables.Count > 0)
                {
                    if(ddl_Admin_Agent.SelectedValue.ToString()=="Admin")
                    {
                        Session["IsAdmin"] = "1";
                    }
                    else
                    { 
                        Session["IsAdmin"] = "0"; 
                    }
                    Session["adminid"] = ds.Tables[0].Rows[0]["UserID"].ToString();
                    Session["UserID"] = ds.Tables[0].Rows[0]["UserID"].ToString();
                    Session["AgentName"] = ds.Tables[0].Rows[0]["AgentName"].ToString();
                    Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
                    Session["AgentID"] = ds.Tables[0].Rows[0]["AgentID"].ToString();
                    Session["IsAgent_Admin"] = ds.Tables[0].Rows[0]["IsAgent_Admin"].ToString();

                    if (ds.Tables[0].Rows[0]["IsAgent_Admin"].ToString() == "Admin")
                    {
                        //this.Page.MasterPageFile = "~/AdminMaster.Master";
                        Response.Write("<script language='javascript'>window.location='AgentMaster.aspx';</script>");
                    }
                    else
                    {
                        //this.Page.MasterPageFile = "~/AgentMaster.Master";
                        Response.Write("<script language='javascript'>window.location='AgentMaster.aspx';</script>");
                    }


                }
                else
                {
                    Response.Write("<script>alert('Incorrect UserName or Password')</script>");
                    //lblerror.Text = "Incorrect UserName or Password";
                }
            }
        }

    }
    
}