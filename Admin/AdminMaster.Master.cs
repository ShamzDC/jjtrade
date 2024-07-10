using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["IsAdmin"].ToString() == "1")
                //{
                //    LiAgentTrxn.Visible = false;
                //}
                //else
                //{
                //    LiAgentTrxn.Visible = true;
                //}

               
                if (Session["IsAdmin"].ToString() == "1")
                {
                     
                }
                else
                {
                    //LiMaster.Visible = false;
                    //LiAdminTrxn.Visible = false;
                    //LiTrxnReport.Visible = false;
                    LiEmployee.Visible = false;
                }
                 
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["adminid"] = null;
            Server.Transfer("Index.aspx");
        }
        protected void TrxnPost_ServerClick(object sender, EventArgs e)
        {

            if (Session["IsAdmin"].ToString()=="1")
            {
                Response.Write("<script language='javascript'>window.location='AdminDashboard.aspx';</script>");
            }
            else
            {
                Response.Write("<script language='javascript'>window.location='TransactionPosting.aspx?From_AdminAgent=Agent&AgntID=" + Session["FromAgent"] + "&AgntPrmCurr=" + Session["FromCurrency"] + " ';</script>");

            }

        }

    }
}