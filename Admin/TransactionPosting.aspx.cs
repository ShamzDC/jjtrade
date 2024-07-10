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
using System.IO;
using System.Drawing;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace Client.Admin
{
    public partial class TransactionPosting : System.Web.UI.Page
    {
        int scode;
        public DataSet dsToAgent = new DataSet();
        public DataSet ds_CrDr = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    pnlControl_Post.Visible = true;
                    pnlControl_Post.Enabled = true;
                    pnlControl_Process.Visible = false;
                    pnlButton_Post.Visible = true;
                    pnlButton_Process.Visible = false;
                    pnlGrid.Visible = true;
                    pnlReport.Visible = false;
                    PnlStatusReportControl.Visible = false;

                    txtEffectiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtTID.Visible = true;
                    lblAgentEffDate.Visible = true;
                    lblAgentEffDate.Text = "Effective Date";

                    btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
                    btnSave_Process.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
                    //Response.Write("<script language='javascript'>window.location='TransactionPosting.aspx?From_AdminAgent=Admin&AgntID=" + AgnID + " &AgntPrmCurr=" + Session["PrmCurr"] + " ';</script>");
                    if (Request.QueryString["From_AdminAgent"] == "Agent" || Session["IsAdmin"].ToString() == "0")  // - it means its agent 
                    {
                        Control list = this.Page.Master.FindControl("LiMaster");
                        list.Visible = false;
                        Control listadmin = this.Page.Master.FindControl("LiAdminTrxn");
                        listadmin.Visible = false;
                        if (Session["FromAgent"].ToString() == "0")
                        {
                            Response.Write("<script language='javascript'>window.alert('You should Login as Agent');window.location='/Admin/Index.aspx';</script>");
                        }
                        else
                        {

                            if (Session["UserID"] != null)
                            {
                                lblUser.Text = "User - " + Session["AgentName"].ToString() + " - ( " + Session["FromCurrency"].ToString() + " )";
                                LoadTransaction();

                            }
                            else
                            {
                                Response.Write("<script language='javascript'>window.alert('Login to View this Page');window.location='/Admin/Index.aspx';</script>");
                            }
                        }
                    }
                    else if (Request.QueryString["From_AdminAgent"] == "Admin" || Session["DashboardAgentID"].ToString() != null)
                    {
                        //Control listAgent = this.Page.Master.FindControl("LiAgentTrxn");
                        //listAgent.Visible = true;

                        if (Session["DashboardAgentID"].ToString() == "0")
                        {
                            //Control listAgent = this.Page.Master.FindControl("LiAgentTrxn");
                            //listAgent.Visible = false;

                            Response.Write("<script language='javascript'>window.location='AdminDashboard.aspx';</script>");
                        }
                        else
                        {
                            if (Session["UserID"] != null)
                            {
                                lblUser.Text = "Admin - " + Session["UserName"].ToString();

                                DataSet dsname = new DataSet();
                                TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                                lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
                                lTransaction_BAL.lTransactionNew_CDAL.flag = "SelectAgentCurrency";
                                lTransaction_BAL.lTransactionNew_CDAL.To_Agent = Request.QueryString["AgntID"].ToString();
                                dsname = lTransaction_BAL.GetTransactionTransfer_Post();
                                if (dsname.Tables.Count > 0)
                                {
                                    lblUser.Text = "Admin - " + dsname.Tables[0].Rows[0]["AgentName_WithCurr"].ToString();
                                }
                                LoadTransaction();

                            }
                            else
                            {
                                Response.Write("<script language='javascript'>window.alert('Login to View this Page');window.location='/Admin/Index.aspx';</script>");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private void LoadTransaction()
        {
            try
            { 
            int isagent = 0;
            DataSet ds = new DataSet();
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "Select";
            lTransaction_BAL.lTransactionNew_CDAL.TrxnID = 0;
            if (Request.QueryString["From_AdminAgent"] == "Agent" || (Session["IsAdmin"].ToString()=="0" && Session["FromAgent"].ToString() != null)  )
            {
                lTransaction_BAL.lTransactionNew_CDAL.From_Agent = Session["FromAgent"].ToString();
                isagent = 1;
            }
            else if (Request.QueryString["From_AdminAgent"] == "Admin")
            {
                lTransaction_BAL.lTransactionNew_CDAL.From_Agent = Request.QueryString["AgntID"].ToString(); 
            }
                ds = lTransaction_BAL.GetTransactionTransfer_Post();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {  
                    ViewState["dtdata"] = ds.Tables[0]; 
                    GridCurrency.DataSource = ds.Tables[0];
                    GridCurrency.DataBind(); 
                    if(isagent==1)
                    {
                        GridCurrency.Columns[2].Visible = false;
                        GridCurrency.Columns[3].Visible = false;
                    }
                }
                
            }

            
            GetRuningTotal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetRuningTotal()
        {
            try
            { 
            double GndCr = 0, GndDr = 0; double Closingbalance = 0; double opBalNew = 0;

            for (int i = GridCurrency.Rows.Count-1 ; i >=0  ; i--)
            //for (int i = 0; i <= GridCurrency.Rows.Count - 1; i++)
            {

                LinkButton Imgedit1 = (LinkButton)GridCurrency.Rows[i].FindControl("Imgedit");
                LinkButton Imgdelete1 = (LinkButton)GridCurrency.Rows[i].FindControl("Imgdelete");
                LinkButton ImgProcess1 = (LinkButton)GridCurrency.Rows[i].FindControl("ImgProcess");
                LinkButton ImgProcessEdit1 = (LinkButton)GridCurrency.Rows[i].FindControl("ImgProcessEdit");
                LinkButton ImgPendingMove1 = (LinkButton)GridCurrency.Rows[i].FindControl("ImgPendingMove");
                HiddenField hdnGrdIsProcessed1 = (HiddenField)GridCurrency.Rows[i].FindControl("hdnGrdIsProcessed");
                HiddenField hdnGrdAgentTime1 = (HiddenField)GridCurrency.Rows[i].FindControl("hdnGrdAgentTime");

                if (Request.QueryString["From_AdminAgent"] == "Agent" || (Session["IsAdmin"].ToString() == "0" && Session["FromAgent"].ToString() != null))
                {
                    ImgProcess1.Visible = false; ImgProcessEdit1.Visible = false; ImgPendingMove1.Visible = false;

                    if (hdnGrdIsProcessed1.Value == "Y")
                    {
                        Imgdelete1.Visible = false; Imgedit1.Visible = false;
                    }
                    else
                    {
                        int processtime = Convert.ToInt32(hdnGrdAgentTime1.Value.ToString());
                        if (processtime < 1)
                        { Imgedit1.Visible = false; Imgdelete1.Visible = false; }
                    }
                }
                else
                {
                    if (hdnGrdIsProcessed1.Value == "Y")
                    {
                        Imgedit1.Visible = false; ImgProcess1.Visible = false; ImgProcessEdit1.Visible = false; ImgPendingMove1.Visible = true;
                    }
                    else
                    {
                        ImgProcessEdit1.Visible = false; ImgPendingMove1.Visible = false;
                    }
                }
                
             

                Label LblDebitAmt1 = (Label)GridCurrency.Rows[i].FindControl("LblDebitAmt");
                Label LblCreditAmt1 = (Label)GridCurrency.Rows[i].FindControl("LblCreditAmt");
                Label LblClosingBalance1 = (Label)GridCurrency.Rows[i].FindControl("LblClosingBalance");
                opBalNew = Closingbalance;
                Closingbalance = (Convert.ToDouble(opBalNew) + (Convert.ToDouble(LblCreditAmt1.Text) - Convert.ToDouble(LblDebitAmt1.Text)));
                LblClosingBalance1.Text = Closingbalance.ToString();
                GndDr = GndDr + Convert.ToDouble(LblDebitAmt1.Text);
                GndCr = GndCr + Convert.ToDouble(LblCreditAmt1.Text);
            }

            lblCredit.Text = "Total Payments : " + GndCr.ToString(); lblDebit.Text = "Total Receipts : " + GndDr.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetRuningTotal_Bck()
        {
            
            double GndCr = 0, GndDr = 0; double Closingbalance = 0; double opBalNew = 0;

            //for (int i = GridCurrency.Rows.Count-1 ; i >=0  ; i--)
            for (int i = 0; i <= GridCurrency.Rows.Count - 1; i++)
            {

                LinkButton Imgedit1 = (LinkButton)GridCurrency.Rows[i].FindControl("Imgedit");
                LinkButton Imgdelete1 = (LinkButton)GridCurrency.Rows[i].FindControl("Imgdelete");
                LinkButton ImgProcess1 = (LinkButton)GridCurrency.Rows[i].FindControl("ImgProcess");
                LinkButton ImgProcessEdit1 = (LinkButton)GridCurrency.Rows[i].FindControl("ImgProcessEdit");
                HiddenField hdnGrdIsProcessed1 = (HiddenField)GridCurrency.Rows[i].FindControl("hdnGrdIsProcessed");
                HiddenField hdnGrdAgentTime1 = (HiddenField)GridCurrency.Rows[i].FindControl("hdnGrdAgentTime");

                if (Request.QueryString["From_AdminAgent"] == "Agent" || (Session["IsAdmin"].ToString() == "0" && Session["FromAgent"].ToString() != null))
                {
                    ImgProcess1.Visible = false; ImgProcessEdit1.Visible = false;

                    if (hdnGrdIsProcessed1.Value == "Y")
                    {
                        Imgdelete1.Visible = false; Imgedit1.Visible = false;
                    }
                    else
                    {
                        int processtime = Convert.ToInt32(hdnGrdAgentTime1.Value.ToString());
                        if (processtime < 1)
                        { Imgedit1.Visible = false; Imgdelete1.Visible = false; }
                    }
                }
                else
                {
                    if (hdnGrdIsProcessed1.Value == "Y")
                    {
                        Imgedit1.Visible = false; ImgProcess1.Visible = false; ImgProcessEdit1.Visible = false;
                    }
                    else
                    {
                        ImgProcessEdit1.Visible = false;
                    }
                }
                /*
                Label LblopBal1 = (Label)GridCurrency.Rows[i].FindControl("LblopBal");
                Label LblDebitAmt1 = (Label)GridCurrency.Rows[i].FindControl("LblDebitAmt");
                Label LblCreditAmt1 = (Label)GridCurrency.Rows[i].FindControl("LblCreditAmt");
                Label LblClosingBalance1 = (Label)GridCurrency.Rows[i].FindControl("LblClosingBalance");
                LblopBal1.Text = Closingbalance.ToString();
                Closingbalance = (Convert.ToDouble(LblopBal1.Text) + (Convert.ToDouble(LblCreditAmt1.Text) - Convert.ToDouble(LblDebitAmt1.Text)));
                LblClosingBalance1.Text = Closingbalance.ToString();
                GndDr = GndDr + Convert.ToDouble(LblDebitAmt1.Text);
                GndCr = GndCr + Convert.ToDouble(LblCreditAmt1.Text);
                */
                // ********  avoiding op balance
                 
                Label LblDebitAmt1 = (Label)GridCurrency.Rows[i].FindControl("LblDebitAmt");
                Label LblCreditAmt1 = (Label)GridCurrency.Rows[i].FindControl("LblCreditAmt");
                Label LblClosingBalance1 = (Label)GridCurrency.Rows[i].FindControl("LblClosingBalance");
                opBalNew = Closingbalance ;
                Closingbalance = (Convert.ToDouble(opBalNew) + (Convert.ToDouble(LblCreditAmt1.Text) - Convert.ToDouble(LblDebitAmt1.Text)));
                LblClosingBalance1.Text = Closingbalance.ToString();
                GndDr = GndDr + Convert.ToDouble(LblDebitAmt1.Text);
                GndCr = GndCr + Convert.ToDouble(LblCreditAmt1.Text);
            }

            lblCredit.Text = "Total Payments : " + GndCr.ToString();          lblDebit.Text = "Total Receipts : " + GndDr.ToString();
        }
        


        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtTID.Text = "";
            txtCustomerName.Text = "";
            txtNotes.Text = "";
            ddlCredit_Debit.SelectedIndex = 0;
            txtCr_Dr_unit.Text = "";
            txtEffectiveDate.Text = "";
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";

            txtTID.Visible = true;
            lblTrxnIDNew.Visible = true;
            lblAgentEffDate.Text = "Effective Date";
            pnlButton_Post.Visible = true;            
            pnlButton_Process.Visible = false;
            pnlGrid.Visible = true;
            pnlReport.Visible = false;
            PnlStatusReportControl.Visible = false;
            pnlControl_Post.Visible = true;
            pnlControl_Process.Visible = false;
            txtEffectiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }

        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.InnerHtml == "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save")
                {
                    DML("AgentInsert", 0);
                    //LoadTransaction();
                }
                else
                {
                    DML("AgentUpdate", Convert.ToInt32(hdnTrxnId.Value));
                    
                    //LoadTransaction();
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
        protected void Delete1(object sender, EventArgs e)
        {
            try
            { 
            DataSet ds = new DataSet();
            LinkButton lbtn = (LinkButton)sender;
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.TrxnID = scode;
            lTransaction_BAL.lTransactionNew_CDAL.IsAdmin_Agent = Session["IsAgent_Admin"].ToString();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "Delete";

            //int se;
            //se = lTransaction_BAL.InsUptDel("Delete");
            //if (se > 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Deleted Successfully')", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Try Again')", true);
            //}

            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Deleted Successfully')", true);

                    }
                   
                    else if (ds.Tables[0].Rows[0][0].ToString() == "-2")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction Exists for this user. So delete the transactions first')", true);

                    }
                }
            }
            btnClear_Click(sender, e);
            LoadTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Edit1(object sender, EventArgs e)
        {
            try
            { 
            pnlControl_Post.Visible = true;
            pnlControl_Post.Enabled = true;
            pnlControl_Process.Visible = false;
            pnlButton_Post.Visible = true;
            pnlButton_Process.Visible = false;
            pnlGrid.Visible = true;
            pnlReport.Visible = false;
            PnlStatusReportControl.Visible = false;

            txtTID.Visible = true;
            lblTrxnIDNew.Visible = true;
            lblAgentEffDate.Text = "Effective Date";
           

            DataSet ds = new DataSet();
            LinkButton lbtn = (LinkButton)sender;
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.TrxnID = scode;
            lTransaction_BAL.lTransactionNew_CDAL.flag = "Select";

            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables[0].Rows.Count > 0)
            {
                 
                btnSave.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                txtTID.Text = scode.ToString();
                txtEffectiveDate.Text = ds.Tables[0].Rows[0]["EffectiveDate"].ToString();
                txtCustomerName.Text = ds.Tables[0].Rows[0]["Customer_Name"].ToString();
                ddlCredit_Debit.SelectedValue = ds.Tables[0].Rows[0]["Trxn_CR_DR"].ToString();

                txtNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
               // ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Trxn_Status"].ToString();
                hdnTrxnId.Value = scode.ToString();
                

                if (ddlCredit_Debit.SelectedValue.ToString() == "Credit")
                {
                    txtCr_Dr_unit.Text = ds.Tables[0].Rows[0]["CreditUnit"].ToString();
                }
                else
                {
                    txtCr_Dr_unit.Text = ds.Tables[0].Rows[0]["DebitUnit"].ToString();

                }
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DML(string flg, int bid)
        {
            try
            {
                DataSet ds = new DataSet();
                TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
                if (Session["FromCurrency"].ToString() == "" && Request.QueryString["From_AdminAgent"].ToString() == "Agent")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Looks like Currency code not correct')", true);
                }
                else
                {
                    //if (Request.QueryString["From_AdminAgent"].ToString() == "Admin" && (Request.QueryString["AgntPrmCurr"] == null || Request.QueryString["AgntPrmCurr"].ToString() == ""))
                    if (Request.QueryString["From_AdminAgent"] == "Admin" && (Request.QueryString["AgntPrmCurr"] == null || Request.QueryString["AgntPrmCurr"] == ""))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Looks like Currency code not correct')", true);
                    }
                    else
                    {

                        string effdt = txtEffectiveDate.Text.Trim();
                        effdt = effdt.ToString().Substring(3, 3) + effdt.ToString().Substring(0, 3) + effdt.ToString().Substring(6, 4);
                        string dr_unit = "", dr_amt = "", cr_unit = "", cr_amt = "";

                        if (ddlCredit_Debit.SelectedValue.ToString() == "Credit")
                        {
                            cr_unit = txtCr_Dr_unit.Text.Trim(); cr_amt = cr_unit; dr_unit = "0"; dr_amt = "0";
                        }
                        else if (ddlCredit_Debit.SelectedValue.ToString() == "Debit")
                        {
                            dr_unit = txtCr_Dr_unit.Text.Trim(); dr_amt = dr_unit; cr_unit = "0"; cr_amt = "0";
                        }


                        scode = Convert.ToInt32(bid);


                        lTransaction_BAL.lTransactionNew_CDAL.TrxnID = scode;
                        lTransaction_BAL.lTransactionNew_CDAL.UserID = Session["UserID"].ToString();
                        lTransaction_BAL.lTransactionNew_CDAL.EffectiveDate = effdt;
                        lTransaction_BAL.lTransactionNew_CDAL.IsAdmin_Agent = Session["IsAgent_Admin"].ToString();
                        if (Request.QueryString["From_AdminAgent"] == "Agent" || (Session["IsAdmin"].ToString() == "0" && Session["FromAgent"].ToString() != null))
                        {

                            lTransaction_BAL.lTransactionNew_CDAL.From_Agent = Session["FromAgent"].ToString();
                            lTransaction_BAL.lTransactionNew_CDAL.From_Currency = Session["FromCurrency"].ToString();
                        }
                        else
                        {

                            lTransaction_BAL.lTransactionNew_CDAL.From_Agent = Request.QueryString["AgntID"].ToString();
                            lTransaction_BAL.lTransactionNew_CDAL.From_Currency = Request.QueryString["AgntPrmCurr"].ToString();
                        }

                        lTransaction_BAL.lTransactionNew_CDAL.Customer_Name = txtCustomerName.Text.Trim();
                        lTransaction_BAL.lTransactionNew_CDAL.IsProcessed = "N";

                        lTransaction_BAL.lTransactionNew_CDAL.Trxn_CR_DR = ddlCredit_Debit.SelectedValue.ToString();
                        lTransaction_BAL.lTransactionNew_CDAL.CreditUnit = cr_unit;
                        lTransaction_BAL.lTransactionNew_CDAL.DebitUnit = dr_unit;
                        lTransaction_BAL.lTransactionNew_CDAL.Credit_Amt = cr_amt;
                        lTransaction_BAL.lTransactionNew_CDAL.Debit_Amt = dr_amt;

                        lTransaction_BAL.lTransactionNew_CDAL.Notes = txtNotes.Text;
                        lTransaction_BAL.lTransactionNew_CDAL.Trxn_Status = "Pending";

                        if (ddlCredit_Debit.SelectedValue.ToString() == "Debit")
                        {
                            lTransaction_BAL.lTransactionNew_CDAL.FromAgent_CR_DR = "Debit";
                            lTransaction_BAL.lTransactionNew_CDAL.ToAgent_CR_DR = "Credit";
                        }
                        else
                        {
                            lTransaction_BAL.lTransactionNew_CDAL.FromAgent_CR_DR = "Credit";
                            lTransaction_BAL.lTransactionNew_CDAL.ToAgent_CR_DR = "Debit";
                        }
                        lTransaction_BAL.lTransactionNew_CDAL.flag = flg;
                        lTransaction_BAL.lTransactionNew_CDAL.xmlvalue = "";

                        int se;
                        se = lTransaction_BAL.InsUptDel_Post(flg);
                        if (se > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + flg + "Successfully')", true);

                            scode = 0;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Try Again')", true);
                        }
                        LoadTransaction();

                        /*
                        ds = lTransaction_BAL.GetTransactionTransfer_Post();
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + flg + " Successfully')", true);

                                }
                                else if (ds.Tables[0].Rows[0][0].ToString() == "-1")
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record already Exists. Pls enter new Agent')", true);

                                }

                            }
                        }
                        */

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void txtToAgentCurrency_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtToAgentCurrencyPerUnit.Text == "")
                {
                    txtToAgentCurrencyPerUnit.Text = "0";
                }

                //txtConvertedTrxnAmt.Text = Convert.ToString(Convert.ToDouble(hdnTrxnVal.Value) * Convert.ToDouble(txtToAgentCurrencyPerUnit.Text));

                if (ViewState["ProcessMethod"].ToString() == "Multiply")
                {

                    txtConvertedTrxnAmt.Text = Convert.ToString(Math.Round((Convert.ToDouble(hdnTrxnVal.Value) * Convert.ToDouble(txtToAgentCurrencyPerUnit.Text)), 2));
                }
                else if (ViewState["ProcessMethod"].ToString() == "Divide")
                {

                    txtConvertedTrxnAmt.Text = Convert.ToString(Math.Round((Convert.ToDouble(hdnTrxnVal.Value) / Convert.ToDouble(txtToAgentCurrencyPerUnit.Text)), 2));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void txtConvertedTrxnAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //txtToAgentCurrencyPerUnit.Text = Convert.ToString(Convert.ToDouble(txtConvertedTrxnAmt.Text) / Convert.ToDouble(hdnTrxnVal.Value));
                if (ViewState["ProcessMethod"].ToString() == "Multiply")
                {
                    txtToAgentCurrencyPerUnit.Text = Convert.ToString(Math.Round((Convert.ToDouble(txtConvertedTrxnAmt.Text) / Convert.ToDouble(hdnTrxnVal.Value)), 2));
                }
                else if (ViewState["ProcessMethod"].ToString() == "Divide")
                {
                    txtToAgentCurrencyPerUnit.Text = Convert.ToString(Math.Round((Convert.ToDouble(hdnTrxnVal.Value) / Convert.ToDouble(txtConvertedTrxnAmt.Text)), 2));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Process(object sender, EventArgs e)
        {
            try
            {
                pnlControl_Post.Visible = true;
                pnlControl_Post.Enabled = false;
                pnlControl_Process.Visible = true;
                pnlButton_Post.Visible = false;
                pnlButton_Process.Visible = true;
                pnlGrid.Visible = true;
                pnlReport.Visible = false;
                PnlStatusReportControl.Visible = false;
                // btnClear_Process_ServerClick(sender, e);



                DataSet ds = new DataSet();
                LinkButton lbtn = (LinkButton)sender;

                //GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                //HiddenField hdnsno1 = (HiddenField)row.FindControl("hdnsno");
                //txtSno.Text = hdnsno1.Value;

                TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
                lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
                lTransaction_BAL.lTransactionNew_CDAL.TrxnID = scode;
                lTransaction_BAL.lTransactionNew_CDAL.flag = "Select";

                ds = lTransaction_BAL.GetTransactionTransfer_Post();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtProcessEffectiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        LoadToAgent();
                        btnSave.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                        txtTrxnID.Text = scode.ToString();
                        hdnFromCurrency.Value = ds.Tables[0].Rows[0]["From_Currency"].ToString();
                        hdnOrigTrxnType.Value = ds.Tables[0].Rows[0]["Trxn_CR_DR"].ToString();
                        if (ds.Tables[0].Rows[0]["Trxn_CR_DR"].ToString() == "Debit")
                        {
                            hdnTrxnVal.Value = ds.Tables[0].Rows[0]["Debit_Amt"].ToString();
                        }
                        else { hdnTrxnVal.Value = ds.Tables[0].Rows[0]["Credit_Amt"].ToString(); }


                        //*** starting for main values population *****//
                        txtTID.Visible = false;
                        lblTrxnIDNew.Visible = false;
                        lblAgentEffDate.Text = "Agent Eff.Date";
                        txtEffectiveDate.Text = ds.Tables[0].Rows[0]["EffectiveDate"].ToString();
                        txtCustomerName.Text = ds.Tables[0].Rows[0]["Customer_Name"].ToString();
                        ddlCredit_Debit.SelectedValue = ds.Tables[0].Rows[0]["Trxn_CR_DR"].ToString();
                        txtNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
                        if (ddlCredit_Debit.SelectedValue.ToString() == "Credit")
                        {
                            txtCr_Dr_unit.Text = ds.Tables[0].Rows[0]["CreditUnit"].ToString();
                        }
                        else
                        {
                            txtCr_Dr_unit.Text = ds.Tables[0].Rows[0]["DebitUnit"].ToString();
                        }

                        //*** ending for main values population *****//


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void EditProcess(object sender, EventArgs e)
        {
            try
            {
                pnlControl_Post.Visible = false;
                pnlControl_Process.Visible = true;
                pnlButton_Post.Visible = false;
                pnlButton_Process.Visible = true;
                pnlGrid.Visible = true;
                pnlReport.Visible = false;
                PnlStatusReportControl.Visible = false;

                DataSet ds = new DataSet();
                LinkButton lbtn = (LinkButton)sender;
                TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
                lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
                lTransaction_BAL.lTransactionNew_CDAL.TrxnID = scode;
                lTransaction_BAL.lTransactionNew_CDAL.flag = "Select";


                //GridViewRow row = (GridViewRow)lbtn.NamingContainer;
                //HiddenField hdnsno1 = (HiddenField)row.FindControl("hdnsno");
                //txtSno.Text = hdnsno1.Value;

                ds = lTransaction_BAL.GetTransactionTransfer_Post();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    LoadToAgent();
                    btnSave_Process.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                    txtProcessEffectiveDate.Text = ds.Tables[0].Rows[0]["Admin_EffectiveDate"].ToString();

                    ddlToAgentSearch.SelectedValue = ds.Tables[0].Rows[0]["To_Agent"].ToString();

                    txtToAgentCurrencyPerUnit_DB.Text = ds.Tables[0].Rows[0]["ToCurrency_PerUnit"].ToString();
                    txtToAgentCurrencyPerUnit.Text = ds.Tables[0].Rows[0]["ToCurrency_PerUnit"].ToString();
                    txtTrxnID.Text = scode.ToString();
                    hdnTrxnId.Value = scode.ToString();


                    if (ds.Tables[0].Rows[0]["ToAgent_CR_DR"].ToString() == "Credit")
                    {
                        txtConvertedTrxnAmt.Text = ds.Tables[0].Rows[0]["Credit_Amt"].ToString();
                        hdnTrxnVal.Value = ds.Tables[0].Rows[0]["Credit_Amt"].ToString();
                    }
                    else
                    {
                        txtConvertedTrxnAmt.Text = ds.Tables[0].Rows[0]["Debit_Amt"].ToString();
                        hdnTrxnVal.Value = ds.Tables[0].Rows[0]["Debit_Amt"].ToString();
                    }

                    //*** starting for main values population *****//
                    txtTID.Visible = false;

                    txtEffectiveDate.Text = ds.Tables[0].Rows[0]["EffectiveDate"].ToString();
                    txtCustomerName.Text = ds.Tables[0].Rows[0]["Customer_Name"].ToString();
                    ddlCredit_Debit.SelectedValue = ds.Tables[0].Rows[0]["Trxn_CR_DR"].ToString();
                    txtNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
                    if (ddlCredit_Debit.SelectedValue.ToString() == "Credit")
                    {
                        txtCr_Dr_unit.Text = ds.Tables[0].Rows[0]["CreditUnit"].ToString();
                    }
                    else
                    {
                        txtCr_Dr_unit.Text = ds.Tables[0].Rows[0]["DebitUnit"].ToString();
                    }

                    //*** ending for main values population *****//
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ReportProcess(object sender, EventArgs e)
        {

            pnlControl_Post.Visible = false;
            pnlControl_Process.Visible = false;
            pnlButton_Post.Visible = false;
            pnlButton_Process.Visible = false;
            pnlGrid.Visible = false;
            pnlReport.Visible = true;
            PnlStatusReportControl.Visible = false;

            btnProcessExcel.Visible = true;
            btnProcessPDF.Visible = true;

            DataSet ds = new DataSet();
            LinkButton lbtn = (LinkButton)sender;
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.TrxnID = scode;
            lTransaction_BAL.lTransactionNew_CDAL.flag = "Trxn_Report";
           
            //  *** hided on 1-Sep-22 due to show the alues in lable
            /*
            DataTable dt = new DataTable();
            GvStatus.DataSource = dt;
            GvStatus.DataBind();
            */
           
            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["dtdataStatusRpt"] = ds.Tables[0];
                    /*
                   GvStatus.DataSource = ds.Tables[0];
                   GvStatus.DataBind();
                    */
                    GvStatus.Visible = false;
                    divreportcontrols.Visible = true;

                    lblTrxnIDRptVal.Text = ds.Tables[0].Rows[0]["TrxnID"].ToString();
                    lblAgentTrxnDateRptVal.Text = ds.Tables[0].Rows[0]["Agent_EffectiveDate"].ToString();
                    lblFromAgentRptVal.Text = ds.Tables[0].Rows[0]["FromAgentName"].ToString();
                    lblCustomerNameRptVal.Text = ds.Tables[0].Rows[0]["Customer_Name"].ToString();
                    lblTrxnCr_DrRptVal.Text = ds.Tables[0].Rows[0]["Trxn_CR_DR"].ToString();
                    lbllblTrxnAmtRptVal.Text = ds.Tables[0].Rows[0]["FromAmt"].ToString();
                    lblToAgentRptVal.Text = ds.Tables[0].Rows[0]["ToAgentName"].ToString();
                    lblToAgentCurrencyRptVal.Text = ds.Tables[0].Rows[0]["To_Currency"].ToString();
                    lblToCurrency_PerUnitRptVal.Text = ds.Tables[0].Rows[0]["ToCurrency_PerUnit"].ToString();
                    lblConvertedAmtRptVal.Text = ds.Tables[0].Rows[0]["ToAmt"].ToString();
                    lblProcessedDateByAdminRptVal.Text = ds.Tables[0].Rows[0]["ProcessedDateByAdmin"].ToString();
                    lblStatusRptVal.Text = ds.Tables[0].Rows[0]["Trxn_Status"].ToString();
                    lblNotesRptVal.Text = ds.Tables[0].Rows[0]["Notes"].ToString();

                    
                    //if (Request.QueryString["From_AdminAgent"] == "Agent" || (Session["IsAdmin"].ToString() == "0" && Session["FromAgent"].ToString() != null))
                    //{
                    //    lblFromAgentRpt.Visible = false; lblFromAgentRptVal.Visible = false; lblToAgentRpt.Visible = false; lblToAgentRptVal.Visible = false;
                    //    lbllblTrxnAmtRpt.Visible = false; lbllblTrxnAmtRptVal.Visible = false; lblToCurrency_PerUnitRpt.Visible = false; lblToCurrency_PerUnitRptVal.Visible = false;
                    //}
                    //else
                    //{
                    //    lblFromAgentRpt.Visible = true; lblFromAgentRptVal.Visible = true; lblToAgentRpt.Visible = true; lblToAgentRptVal.Visible = true;
                    //    lbllblTrxnAmtRpt.Visible = true; lbllblTrxnAmtRptVal.Visible = true; lblToCurrency_PerUnitRpt.Visible = true; lblToCurrency_PerUnitRptVal.Visible = true;
                    //}


                    if (Request.QueryString["From_AdminAgent"] == "Agent" || (Session["IsAdmin"].ToString() == "0" && Session["FromAgent"].ToString() != null))
                    {
                         lblFromAgentRptVal.Text = "";  lblToAgentRptVal.Text = "";
                          lbllblTrxnAmtRptVal.Text = "";  lblToCurrency_PerUnitRptVal.Text = "";
                    }
                    //else
                    //{
                    //    lblFromAgentRpt.Visible = true; lblFromAgentRptVal.Visible = true; lblToAgentRpt.Visible = true; lblToAgentRptVal.Visible = true;
                    //    lbllblTrxnAmtRpt.Visible = true; lbllblTrxnAmtRptVal.Visible = true; lblToCurrency_PerUnitRpt.Visible = true; lblToCurrency_PerUnitRptVal.Visible = true;
                    //}

                }
           }

       }

        protected void MoveToPending(object sender, EventArgs e)
        {
            try
            { 
            //pnlControl_Post.Visible = false;
            //pnlControl_Process.Visible = true;
            //pnlButton_Post.Visible = false;
            //pnlButton_Process.Visible = true;
            //pnlGrid.Visible = true;
            //pnlReport.Visible = false;
            //PnlStatusReportControl.Visible = false;

            DataSet ds = new DataSet();
            LinkButton lbtn = (LinkButton)sender;
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.TrxnID = scode;
            lTransaction_BAL.lTransactionNew_CDAL.flag = "Move_To_Process";
             
            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Moved Successfully')", true);

                    }

                }
            }
            btnClear_Process_ServerClick(sender, e);
            LoadTransaction();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private void LoadToAgent()
       {
            try
            { 
           AgentMaster_BAL lAgent_BAL = new AgentMaster_BAL();
           lAgent_BAL.lAgentMaster_CDAL = new AgentMaster_CDAL();
           lAgent_BAL.lAgentMaster_CDAL.flag = "Select";
           dsToAgent = lAgent_BAL.lAgentMaster_CDAL.GetAgent_ds();
           ddlToAgentSearch.DataSource = dsToAgent.Tables[0];
           ddlToAgentSearch.DataTextField = "AgentNameNew";
           ddlToAgentSearch.DataValueField = "AgentID";
           ddlToAgentSearch.DataBind();
           ddlToAgentSearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Agent--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       protected void btnDownload_Click(object sender, EventArgs e)
       {
           ExportExcel("Transaction-Details");
       }

        protected void ddlToAgentSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtProcessEffectiveDate.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Process date')", true);
                    txtProcessEffectiveDate.Focus();
                }
                else
                {
                    string effdt;

                    effdt = txtProcessEffectiveDate.Text.ToString().Substring(3, 3) + txtProcessEffectiveDate.Text.ToString().Substring(0, 3) + txtProcessEffectiveDate.Text.ToString().Substring(6, 4);

                    DataSet ds = new DataSet();
                    TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                    lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
                    lTransaction_BAL.lTransactionNew_CDAL.From_Currency = hdnFromCurrency.Value;
                    lTransaction_BAL.lTransactionNew_CDAL.EffectiveDate = effdt;
                    lTransaction_BAL.lTransactionNew_CDAL.To_Agent = ddlToAgentSearch.SelectedValue.ToString();

                    lTransaction_BAL.lTransactionNew_CDAL.flag = "SelectCurrencyUnitNew";

                    ds = lTransaction_BAL.GetTransactionTransfer_Post();
                    if (ds.Tables.Count > 1)
                    {
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                        {
                            double From_To_currency = 0, To_From_Currency = 0;
                            From_To_currency = Convert.ToDouble(ds.Tables[0].Rows[0]["CurrencyValuePerUnit"].ToString());
                            To_From_Currency = Convert.ToDouble(ds.Tables[1].Rows[0]["CurrencyValuePerUnit"].ToString());

                            if (From_To_currency >= To_From_Currency)
                            {
                                ViewState["ProcessMethod"] = "Multiply";
                                hdnToAgentCurr.Value = ds.Tables[0].Rows[0]["ToCurrencyCode"].ToString();
                                hdnToAgentPerUnt.Value = ds.Tables[0].Rows[0]["CurrencyValuePerUnit"].ToString();

                                txtToAgentCurrencyPerUnit.Text = ds.Tables[0].Rows[0]["CurrencyValuePerUnit"].ToString();
                                txtToAgentCurrencyPerUnit_DB.Text = ds.Tables[0].Rows[0]["CurrencyValuePerUnit"].ToString();

                                //lblToAgentCurrency1.Text = ds.Tables[0].Rows[0]["ToCurrencyCode"].ToString() + " / " + ds.Tables[0].Rows[0]["CurrencyValuePerUnit"].ToString();
                                //if (ds.Tables[0].Rows[0]["ToCurrencyCode"].ToString() != "" && ds.Tables[0].Rows[0]["CurrencyValuePerUnit"].ToString() != "")
                                //{
                                //    lblIsValid1.Text = "Y";
                                //}

                                txtConvertedTrxnAmt.Text = Convert.ToString(Math.Round((Convert.ToDouble(hdnTrxnVal.Value) * Convert.ToDouble(txtToAgentCurrencyPerUnit_DB.Text)), 2));
                            }
                            else
                            {
                                ViewState["ProcessMethod"] = "Divide";
                                hdnToAgentCurr.Value = ds.Tables[0].Rows[0]["ToCurrencyCode"].ToString();

                                txtConvertedTrxnAmt.Text = Convert.ToString(Math.Round((Convert.ToDouble(hdnTrxnVal.Value) / Convert.ToDouble(To_From_Currency)), 2));


                                hdnToAgentPerUnt.Value = ds.Tables[1].Rows[0]["CurrencyValuePerUnit"].ToString();
                                txtToAgentCurrencyPerUnit.Text = ds.Tables[1].Rows[0]["CurrencyValuePerUnit"].ToString();
                                txtToAgentCurrencyPerUnit_DB.Text = ds.Tables[1].Rows[0]["CurrencyValuePerUnit"].ToString();


                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Looks like Currency code not found')", true);
                            txtConvertedTrxnAmt.Text = "0";
                            txtToAgentCurrencyPerUnit_DB.Text = "0";
                            txtToAgentCurrencyPerUnit.Text = "0";
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Looks like Currency code not found')", true);
                        txtConvertedTrxnAmt.Text = "0";
                        txtToAgentCurrencyPerUnit_DB.Text = "0";
                        txtToAgentCurrencyPerUnit.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       protected void btnSave_Process_ServerClick(object sender, EventArgs e)
       {
           try
           {
               if (btnSave_Process.InnerHtml == "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save")
               {
                   DML_Process("AdminSingleProcess", Convert.ToInt32(txtTrxnID.Text));
                   //LoadTransaction();
               }
               else
               {
                   DML_Process("AdminSingleProcess", Convert.ToInt32(txtTrxnID.Text));

                   //LoadTransaction();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               btnClear_Process_ServerClick(sender, e);
               LoadTransaction();
           }
       }

        private void DML_Process(string flg, int bid)
        {
            try
            {
                // flag should be AdminSingleUpdate 
                DataSet ds = new DataSet();
                TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();

                string effdt = txtProcessEffectiveDate.Text.Trim();
                effdt = effdt.ToString().Substring(3, 3) + effdt.ToString().Substring(0, 3) + effdt.ToString().Substring(6, 4);
                lTransaction_BAL.lTransactionNew_CDAL.To_Agent = ddlToAgentSearch.SelectedValue.ToString();
                lTransaction_BAL.lTransactionNew_CDAL.To_Currency = hdnToAgentCurr.Value;
                lTransaction_BAL.lTransactionNew_CDAL.ToCurrency_PerUnit = txtToAgentCurrencyPerUnit.Text;
                if (hdnOrigTrxnType.Value == "Debit")
                {

                    lTransaction_BAL.lTransactionNew_CDAL.ToAgent_CR_DR = "Credit";
                    lTransaction_BAL.lTransactionNew_CDAL.CreditUnit = txtConvertedTrxnAmt.Text;
                    lTransaction_BAL.lTransactionNew_CDAL.Credit_Amt = txtConvertedTrxnAmt.Text;
                    lTransaction_BAL.lTransactionNew_CDAL.DebitUnit = hdnTrxnVal.Value;
                    lTransaction_BAL.lTransactionNew_CDAL.Debit_Amt = hdnTrxnVal.Value;
                }
                else
                {

                    lTransaction_BAL.lTransactionNew_CDAL.ToAgent_CR_DR = "Debit";
                    lTransaction_BAL.lTransactionNew_CDAL.DebitUnit = txtConvertedTrxnAmt.Text;
                    lTransaction_BAL.lTransactionNew_CDAL.Debit_Amt = txtConvertedTrxnAmt.Text;
                    lTransaction_BAL.lTransactionNew_CDAL.CreditUnit = hdnTrxnVal.Value;
                    lTransaction_BAL.lTransactionNew_CDAL.Credit_Amt = hdnTrxnVal.Value;
                }


                lTransaction_BAL.lTransactionNew_CDAL.EffectiveDate = effdt;
                lTransaction_BAL.lTransactionNew_CDAL.AdminUserID = Session["UserID"].ToString();
                lTransaction_BAL.lTransactionNew_CDAL.flag = flg;
                lTransaction_BAL.lTransactionNew_CDAL.TrxnID = bid;

                ds = lTransaction_BAL.GetTransactionTransfer_Post();
                //DML("AdminBulkUpdate", 0);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Processed Successfully')", true);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       protected void btnClear_Process_ServerClick(object sender, EventArgs e)
       {
           txtSno.Text = "";
           txtTrxnID.Text = "";
           txtProcessEffectiveDate.Text = "";
           ddlToAgentSearch.SelectedIndex = -1;
           txtToAgentCurrencyPerUnit_DB.Text = "";
           txtToAgentCurrencyPerUnit.Text = "";
           txtConvertedTrxnAmt.Text = "";
           btnSave_Process.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
           txtTID.Text = "";
           txtCustomerName.Text = "";
           txtNotes.Text = "";
           ddlCredit_Debit.SelectedIndex = 0;
           txtCr_Dr_unit.Text = "";
           txtEffectiveDate.Text = "";
           txtProcessEffectiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

           txtTID.Text = "";
           txtCustomerName.Text = "";
           txtNotes.Text = "";
           ddlCredit_Debit.SelectedIndex = 0;
           txtCr_Dr_unit.Text = "";
           txtEffectiveDate.Text = "";
           btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";

           txtTID.Visible = true;
           lblTrxnIDNew.Visible = true;



           pnlControl_Post.Visible = true;
           pnlControl_Post.Enabled = true;
           pnlControl_Process.Visible = false;
           pnlButton_Post.Visible = true;
           pnlButton_Process.Visible = false;
           pnlGrid.Visible = true;
           pnlReport.Visible = false;
           PnlStatusReportControl.Visible = false;

           txtEffectiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
           txtTID.Visible = true;
           lblAgentEffDate.Visible = true;
           lblAgentEffDate.Text = "Effective Date";

       }

        public void ExportExcel(string filename)
        {
            //try
            //{
                
                DataSet ds = new DataSet();
                TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
                lTransaction_BAL.lTransactionNew_CDAL.flag = "Trxn_Report_Download";
                
                if (Request.QueryString["From_AdminAgent"] == "Agent" || (Session["IsAdmin"].ToString() == "0" && Session["FromAgent"].ToString() != null))
                {
                    lTransaction_BAL.lTransactionNew_CDAL.From_Agent = Session["FromAgent"].ToString();
                    
                }
                else if (Request.QueryString["From_AdminAgent"] == "Admin")
                {
                    lTransaction_BAL.lTransactionNew_CDAL.From_Agent = Request.QueryString["AgntID"].ToString();
                }
                ds = lTransaction_BAL.GetTransactionTransfer_Post();


                if (ds.Tables.Count>0)
                {
                    if (ds.Tables[0].Rows.Count>0)
                    {
 

                        HttpResponse Response = HttpContext.Current.Response;
                        GridView grd = new GridView();
                        grd.DataSource = ds.Tables[0];
                        grd.DataBind();
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename= " + filename + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            grd.RenderControl(hw);
                            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                            Response.Write(style);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                    else
                    { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Records to Download')", true); }
                }
                else
                { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Records to Download')", true); }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public void ExportExcel_oldBck(string filename)
        {
            try
            {
                DataTable dt = new DataTable();
                if (filename == "Transaction-Details")
                {
                    dt = (DataTable)ViewState["dtdata"];
                }
                else
                {
                    dt = (DataTable)ViewState["dtdataStatusRpt"];
                }
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (filename == "Transaction-Details")
                        {
                            dt.Columns.Remove("Agent_ProcessingDate_PlusHrs");
                            dt.Columns.Remove("DiffHrs");
                            dt.Columns.Remove("IsProcessed");
                        }

                        HttpResponse Response = HttpContext.Current.Response;
                        GridView grd = new GridView();
                        grd.DataSource = dt;
                        grd.DataBind();
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename= " + filename + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            grd.RenderControl(hw);
                            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                            Response.Write(style);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                    else
                    { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Records to Download')", true); }
                }
                else
                { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Records to Download')", true); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       protected void btnDownloadStatusRpt_Click(object sender, EventArgs e)
       {

           ExportExcel("Transaction Status Report");
       }

       protected void btnGotoProcess_Click(object sender, EventArgs e)
       {
           pnlControl_Post.Visible = true;
           pnlControl_Post.Enabled = true;
           pnlControl_Process.Visible = false;
           pnlButton_Post.Visible = true;
           pnlButton_Process.Visible = false;
           pnlGrid.Visible = true;
           pnlReport.Visible = false;
           PnlStatusReportControl.Visible = false;

       }

       protected void btnStatusReport_Click(object sender, EventArgs e)
       {
           PnlStatusReportControl.Visible = true;
           pnlControl_Post.Visible = false;
           pnlControl_Post.Enabled = false;
           pnlControl_Process.Visible = false;
           pnlButton_Post.Visible = false;
           pnlButton_Process.Visible = false;
           pnlGrid.Visible = false;
           pnlReport.Visible = true;

           btnProcessExcel.Visible = false;
           btnProcessPDF.Visible = false;

       }

       protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
       {
           DataTable dt = new DataTable();
           GvStatus.DataSource = dt;
           GvStatus.DataBind();
            GvStatus.Visible = true;
            divreportcontrols.Visible = false;

            DataSet ds = new DataSet();
           TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
           lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();

           lTransaction_BAL.lTransactionNew_CDAL.From_Agent = Request.QueryString["AgntID"].ToString();
           lTransaction_BAL.lTransactionNew_CDAL.Trxn_Status = ddlStatus.SelectedValue.ToString();
           lTransaction_BAL.lTransactionNew_CDAL.flag = "Admin_Report";

           ds = lTransaction_BAL.GetTransactionTransfer_Post();
           if (ds.Tables.Count > 0)
           {
               if (ds.Tables[0].Rows.Count > 0)
               {
                   ViewState["dtdataStatusRpt"] = ds.Tables[0];
                   GvStatus.DataSource = ds.Tables[0];
                   GvStatus.DataBind();
               }
           }
       }

       protected void btnProcessExcel_Click(object sender, EventArgs e)
       {
           ExportExcel("Transaction Status Report");
       }

       protected void btnProcessPDF_Click(object sender, EventArgs e)
       {
           Export_PDF("Transaction Status Report");
       }

       public void Export_PDF(  string Name)
       {
           DataTable dt = new DataTable();
           if (Name == "Transaction-Details")
           {
               dt = (DataTable)ViewState["dtdata"];
           }
           else
           {
               dt = (DataTable)ViewState["dtdataStatusRpt"];
           }
           if (dt != null)
           {
               if (dt.Rows.Count > 0)
               {
                   /*
                   HttpResponse Response = HttpContext.Current.Response;
                   GridView GridView2 = new GridView();

                   GridView2.AllowPaging = false;

                   GridView2.HeaderStyle.BackColor = System.Drawing.Color.Magenta;

                   GridView2.DataSource = dt;

                   GridView2.DataBind();

                   Response.ContentType = "application/pdf";

                   Response.AddHeader("content-disposition", "attachment;filename= " + Name + ".pdf");

                   Response.Cache.SetCacheability(HttpCacheability.NoCache);

                   StringWriter sw = new StringWriter();

                   HtmlTextWriter hw = new HtmlTextWriter(sw);

                   GridView2.RenderControl(hw);

                   StringReader sr = new StringReader(sw.ToString());

                   Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

                   HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                   PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

                   pdfDoc.Open();

                   htmlparser.Parse(sr);

                   pdfDoc.Close();

                   Response.Write(pdfDoc);

                   Response.End();
                   */
                    Name =   Name + ".pdf";
                    string Reportfolder =  Server.MapPath("../Admin/Temp_Reports/");

                    if (Directory.Exists(Reportfolder) == false)
                    {
                        Directory.CreateDirectory(Reportfolder);
                    }
                    else
                    {
                        foreach (FileInfo fileInformation in new DirectoryInfo(Reportfolder).GetFiles())  // delete all the files inside  Reportfolder - folder
                        {
                            File.Delete(fileInformation.FullName);
                        }
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<html><table width='100%' style='vertical-align:top; text-align:left; font-family:Verdana ;font-size:9px; ' cellpadding='0' cellspacing='0'>");
                    sb.Append("<tr><td colspan='4'><br/><br/><br/><br/></td></tr>");
                    for (int i = 0; i <= dt.Rows.Count-1; i++)
                    {
                         sb.Append("<trFWB@123><td>Transaction ID</td><td>: " + dt.Rows[0]["TrxnID"] + " </td><td>Customer Name</td><td>: " + dt.Rows[0]["Customer_Name"] + " </td></tr>");
                        sb.Append("<trFWB@123><td>From User</td><td>: " + dt.Rows[0]["FromAgentName"] + " </td><td>To User</td><td>: " + dt.Rows[0]["ToAgentName"] + " </td>  </tr>");
                        sb.Append("<trFWB@123><td>User-Trxn Date</td><td>: " + dt.Rows[0]["Agent_EffectiveDate"] + " </td><td>Admin Process Date</td><td>: " + dt.Rows[0]["ProcessedDateByAdmin"] + " </td></tr>");
                        sb.Append("<trFWB@123><td>Cr / Dr</td><td>: " + dt.Rows[0]["Trxn_CR_DR"] + " </td> <td>Trxn Status</td><td>: " + dt.Rows[0]["Trxn_Status"] + " </td> </tr>");
                        sb.Append("<trFWB@123><td>TrxnAmt</td><td>: " + dt.Rows[0]["FromAmt"] + " </td><td>Per Unit</td><td>: " + dt.Rows[0]["ToCurrency_PerUnit"] + " </td></tr>");
                        sb.Append("<trFWB@123><td>Processed Amt</td><td>: " + dt.Rows[0]["ToAmt"] + " </td><td>To Currency</td><td>: " + dt.Rows[0]["To_Currency"] + " </td></tr>");
                        sb.Append("<trFWB@123><td>Notes</td><td colspan='3'>: " + dt.Rows[0]["Notes"] + " </td></tr>");
                    }


                    sb.Append("</table></html>");
                    HttpResponse Response = HttpContext.Current.Response;

                    Literal lbl = new Literal();

                    lbl.Text = sb.ToString().Replace("<trFWB@123>", "<tr style='text-align:Left; font-weight:bold; font-size:12px'>");
                    //lbl.Text = lbl.Text.Replace("<html><table width='100%' style='vertical-align:top; font-family:Verdana ;font-size:9px; ' cellpadding='0' cellspacing='0'>", "<html><table width='100%' style='vertical-align:top; font-family:Courier New ;font-size:9px; line-height:8' cellpadding='0' cellspacing='0'>").Replace("style='line-height:0'", "style='line-height:4'");
                    //divcheck.InnerHtml = sb.ToString().Replace("<tdFWB@123>", "<td style='font-weight:bold'>").Replace("<td2FWB@123>", "<td colspan='2' style='font-weight:bold'>").Replace("<td3FWB@123>", "<td colspan='3' style='font-weight:bold'>").Replace("<td5FWB@123>", "<td colspan='5' style='font-weight:bold'>").Replace("<td7FWB@123>", "<td colspan='7' style='font-weight:bold'>").Replace("<trFWB@123>", "<tr style='font-weight:bold'>").Replace("<td10FWB@123>", "<td colspan='10' style='font-weight:bold'>");


                    
                    StringWriter sw = new StringWriter();

                    HtmlTextWriter hw = new HtmlTextWriter(sw);


                    lbl.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());

                    
                    Document pdfDoc = new Document(PageSize.A4, 40f, 10f, 50f, 1f);

                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, new FileStream(Reportfolder + Name, FileMode.Create));
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    Response.Write(pdfDoc); 
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "inline;filename=" + Name + "");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    using (StringWriter sw2 = new StringWriter())
                    {
                        HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                        lbl.RenderControl(hw2);
                        StringReader sr2 = new StringReader(sw2.ToString());
                        Document pdfDoc2 = new Document(PageSize.A4, 40f, 10f, 5f, 5f);
                        HTMLWorker htmlparser2 = new HTMLWorker(pdfDoc2);
                        PdfWriter.GetInstance(pdfDoc2, Response.OutputStream);
                        pdfDoc2.Open();
                        htmlparser2.Parse(sr2);
                        pdfDoc2.Close();
                        Response.Write(pdfDoc2);
                        Response.End();
                    }


                }
                else
                { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Records to Print')", true); }
            }
            else
            { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Records to Print')", true); }

        }

         
    }
}