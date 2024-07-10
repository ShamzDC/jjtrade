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
    public partial class Index : System.Web.UI.Page
    {
        List<Login_CDAL> listLogin_CDAL = new List<Login_CDAL>();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtusername.Text.ToLower() == "admin" && txtpassword.Text.ToLower() == "admin")
            {
                Session["IsAdmin"] = "1";
                Session["adminid"] = "0";
                Session["UserID"] = "0";
                Session["UserName"] = "Admin";
                Session["Pwd"] = "Admin";
                Session["DashboardAgentID"] = "0";
                Session["EmpID"] = "0";
                Response.Redirect("BillingDashboard.aspx");

               
            }
            else
            {
                DataSet ds = new DataSet();
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
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["DashboardAgentID"] = "0";
                             
                            Session["adminid"] = ds.Tables[0].Rows[0]["UserID"].ToString();
                            Session["UserID"] = ds.Tables[0].Rows[0]["UserID"].ToString();

                            Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
                            Session["IsAdmin"] = ds.Tables[0].Rows[0]["IsAdmin"].ToString();
                            Session["EmpID"] = ds.Tables[0].Rows[0]["EmpID"].ToString();
                            Session["Pwd"] = ds.Tables[0].Rows[0]["password"].ToString();

                            Response.Redirect("BillingDashboard.aspx");
                            //Response.Write("<script language='javascript'>window.location=BillingDashboard.aspx';</script>");

                            /*
                            if (ds.Tables[0].Rows[0]["IsAgent_Admin"].ToString() == "Admin")
                            {                          
                                Response.Write("<script language='javascript'>window.location='AdminDashboard.aspx';</script>");
                            }
                            else
                            {                          
                                Response.Write("<script language='javascript'>window.location='TransactionPosting.aspx?From_AdminAgent=Agent&AgntID=" + Session["FromAgent"] + "&AgntPrmCurr=" + Session["FromCurrency"] + " ';</script>");

                            }
                            */

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
    }
}