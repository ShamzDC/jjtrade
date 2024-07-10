using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDAL;
using BAL;
using System.Data;
using System.Text;

namespace Client.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Control list = this.Page.Master.FindControl("LiMaster");
                list.Visible = true;
                Control listadmin = this.Page.Master.FindControl("LiAdminTrxn");
                listadmin.Visible = true;
                Control listAgent = this.Page.Master.FindControl("LiAgentTrxn");
                listAgent.Visible = false;
                LoadTransaction();
            }
        }
       
        private void LoadTransaction()
        {
            DataTable dt = new DataTable();
            GridCurrency.DataSource = dt;
            GridCurrency.DataBind();

            DataSet ds = new DataSet();
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "Admin_Dashboard";
            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridCurrency.DataSource = ds.Tables[0];
                    GridCurrency.DataBind();
                }
            }
            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    LblTotalClosingBal.Text = "Total Closing Balance : " +  ds.Tables[1].Rows[0]["ClosingBal"].ToString();
                }
            }
            //LblTotalClosingBal
            // GetRuningTotal();
        }

        protected void btnaud_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "AUD";
            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridCurrency.DataSource = ds.Tables[0];
                    GridCurrency.DataBind();
                }
            }
        }

        protected void btninr_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "INR";
            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridCurrency.DataSource = ds.Tables[0];
                    GridCurrency.DataBind();
                }
            }
        }

        protected void btnlkr_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "LKR";
            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridCurrency.DataSource = ds.Tables[0];
                    GridCurrency.DataBind();
                }
            }
        }

        protected void btnusd_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "USD";
            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridCurrency.DataSource = ds.Tables[0];
                    GridCurrency.DataBind();
                }
            }
        }
        protected void btnall_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "Admin_Dashboard";
            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridCurrency.DataSource = ds.Tables[0];
                    GridCurrency.DataBind();
                }
            }
        }

        public void GetRuningTotal()
        {
            //double opbal = 0, clbal = 0, cr = 0, dr = 0, GndCr = 0, GndDr = 0;
            double GndCr = 0, GndDr = 0;

            double Closingbalance = 0;
            foreach (GridViewRow row in GridCurrency.Rows)
            {
                Label LblopBal1 = (Label)row.FindControl("LblopBal");
                Label LblDebitAmt1 = (Label)row.FindControl("LblDebitAmt");
                Label LblCreditAmt1 = (Label)row.FindControl("LblCreditAmt");
                Label LblClosingBalance1 = (Label)row.FindControl("LblClosingBalance");

                LinkButton Imgedit1 = (LinkButton)row.FindControl("Imgedit");
                LinkButton Imgdelete1 = (LinkButton)row.FindControl("Imgdelete");
                HiddenField hdnGrdIsProcessed1 = (HiddenField)row.FindControl("hdnGrdIsProcessed");
                HiddenField hdnGrdAgentTime1 = (HiddenField)row.FindControl("hdnGrdAgentTime");

                if (hdnGrdIsProcessed1.Value == "Y")
                {
                    Imgedit1.Visible = false; Imgdelete1.Visible = false;
                }
                else
                {
                    int processtime = Convert.ToInt32(hdnGrdAgentTime1.Value.ToString());
                    if (processtime < 1)
                    { Imgedit1.Visible = false; Imgdelete1.Visible = false; }
                }

                LblopBal1.Text = Closingbalance.ToString();               
                Closingbalance = (Convert.ToDouble(LblopBal1.Text) + (Convert.ToDouble(LblCreditAmt1.Text) - Convert.ToDouble(LblDebitAmt1.Text)));
                LblClosingBalance1.Text = Closingbalance.ToString();


                GndDr = GndDr + Convert.ToDouble(LblDebitAmt1.Text); GndCr = GndCr + Convert.ToDouble(LblCreditAmt1.Text);
            }

            //lblCredit.Text = "Total Payments : " + GndCr.ToString(); lblDebit.Text = "Total Receipts : " + GndDr.ToString();

        }
        
        
        protected void Edit1(object sender, EventArgs e)
        {
            //int scode;            
            LinkButton lbtn = (LinkButton)sender; 

            string[] commandArgs = lbtn.Attributes["EventArgument"].ToString().Split(new char[] { '@' });
            string AgnID = commandArgs[0];
            string PrmCurr = commandArgs[1];

            //scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            if (AgnID != "0")
            {
                Session["DashboardAgentID"] = AgnID;
                Response.Write("<script language='javascript'>window.location='TransactionPosting.aspx?From_AdminAgent=Admin&AgntID=" + AgnID + " &AgntPrmCurr=" + PrmCurr + " ';</script>");
            }
            else
            {
                Session["DashboardAgentID"] = "0" ;
                Response.Write("<script language='javascript'>window.location='TransactionProcess.aspx?From_AdminAgent=Admin&AgntID=" + AgnID + " &AgntPrmCurr=" + PrmCurr + " ';</script>");
            }
        }      
         
    }
}