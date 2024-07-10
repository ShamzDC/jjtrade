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

namespace Client.Admin
{
    public partial class TransactionLedger : System.Web.UI.Page
    {
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["adminid"] = "1"; Session["UserName"] = "Admin";
                if (Session["adminid"] != null)
                {
                    lblUser.Text = Session["UserName"].ToString();
                    LoadToAgent();
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Login to View this Page');window.location='/Admin/Adminlogin.aspx';</script>");
                }
            }
        }

        //private void LoadTransaction()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    DataSet ds = new DataSet();
        //    TransactionTransferBAL lTransaction_BAL = new TransactionTransferBAL();
        //    lTransaction_BAL.lTransactionTransfer_CDAL = new TransactionTransfer_CDAL();
        //    lTransaction_BAL.lTransactionTransfer_CDAL.To_Agent = ddlToAgent.SelectedValue.ToString();
        //    lTransaction_BAL.lTransactionTransfer_CDAL.flag = "ClosingBalanceView";
        //    ds = lTransaction_BAL.GetTransactionTransfer();
        //    if (ds.Tables.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            double opbal = 0, clbal = 0, cr=0,dr=0;
        //            int sno = 0;
        //            sb.Append("<table width='100%' style='vertical-align:top; font-family:Verdana ;font-size:9px;' border='1' cellpadding='1' cellspacing='1'>");
        //            sb.Append("<tr style='text-align:left; font-weight:bold; font-size:12px'><td>S.no</td><td>Trxn ID</td><td>To Agent Name</td><td>Effective Date</td><td>Processed Date</td>");
        //            sb.Append("<td>Op.Balance</td><td>Debit</td><td>Credit</td><td>Cl.Balance</td><td>Notes</td><td>Status</td></tr>");
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                sno = sno + 1;
        //                sb.Append("<tr style='text-align:left; font-size:10px'><td > " + sno  + " </td><td > " + ds.Tables[0].Rows[i]["TrxnID"] + " </td>");
        //                sb.Append("<td > " + ds.Tables[0].Rows[i]["AgentName"] + " </td> <td > " + ds.Tables[0].Rows[i]["EffectiveDate"] + " </td> ");
        //                sb.Append("<td > " + ds.Tables[0].Rows[i]["ProcessingDate"] + " </td> <td > " + opbal + " </td> ");
        //                //sb.Append("<td > " + ds.Tables[0].Rows[i]["DebitUnit"] + " </td> <td > " + ds.Tables[0].Rows[i]["CreditUnit"] + " </td> ");
        //                cr = 0;dr = 0;
        //                if (ds.Tables[0].Rows[i]["Cr_DR"].ToString()== "Debit")
        //                {
        //                    dr = Convert.ToDouble(ds.Tables[0].Rows[i]["Txrn_Amt"]);
        //                    cr = 0;
        //                }
        //                else
        //                    if (ds.Tables[0].Rows[i]["Cr_DR"].ToString() == "Credit")
        //                {
        //                    cr = Convert.ToDouble(ds.Tables[0].Rows[i]["Txrn_Amt"]);
        //                    dr = 0;
        //                }
        //                sb.Append("<td > " + dr + " </td> <td > " + cr + " </td> ");
        //                clbal = opbal + cr - dr;
        //                opbal = opbal + clbal;
        //                sb.Append("<td > " + clbal + " </td> <td > " + ds.Tables[0].Rows[i]["Notes"] + " </td> <td > " + ds.Tables[0].Rows[i]["Trxn_Status"] + " </td> ");
        //                sb.Append("</tr>");
        //            }

        //            sb.Append("</table>");
        //           // HttpResponse Response = HttpContext.Current.Response;
        //            lbl.Text = sb.ToString();
        //        }
        //    }
        //}
        private void LoadToAgent()
        {    
 
            AgentMaster_BAL lAgent_BAL = new AgentMaster_BAL();
            lAgent_BAL.lAgentMaster_CDAL = new AgentMaster_CDAL();
            lAgent_BAL.lAgentMaster_CDAL.flag = "Select";          

            DataSet ds = new DataSet();
            ds = lAgent_BAL.lAgentMaster_CDAL.GetAgent_ds();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlToAgent.DataSource = ds.Tables[0];
                    ddlToAgent.DataTextField = "AgentNameNew";
                    ddlToAgent.DataValueField = "AgentID";
                    ddlToAgent.DataBind();
                    ddlToAgent.Items.Insert(0, new ListItem("--Select Agent--", "0"));
                }
            }

             
        }
         
       
        protected void btnModify_Click(object sender, EventArgs e)
        {
            
            LoadTransaction();
        }

        private void LoadTransaction()
        {
           
            DataTable dt = new DataTable();
            GvStatusLedger.DataSource = dt;
            GvStatusLedger.DataBind();

            DataSet ds = new DataSet();
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
           
            lTransaction_BAL.lTransactionNew_CDAL.From_Agent = ddlToAgent.SelectedValue.ToString();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "Admin_Report";

            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["dtdata"] = ds.Tables[0];
                    GvStatusLedger.DataSource = ds.Tables[0];
                    GvStatusLedger.DataBind();
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            ExportExcel("Transaction-Details");
        }

        public void ExportExcel(string filename)
        {

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["dtdata"];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                { 
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

    }
    }