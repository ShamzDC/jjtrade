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
using DAL;

namespace Client.Admin
{
    public partial class RptCollection : System.Web.UI.Page
    {

        int scode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 
                    lblUser.Text = Session["UserName"].ToString();
                     
                   
            }
        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(ddlReportType.SelectedValue.ToString()+ "_Billing_ddlFill", "ddlFill");

        }
        private void LoadData(string flg, string contoltype)
        {
            try
            {
                DataSet ds = new DataSet();
                string fromdt = "", Todt = "";
                
                DataTable dt = new DataTable();
                GrdBilling.DataSource = dt;
                GrdBilling.DataBind();

                TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
                lTransaction_BAL.lTransactionNew_CDAL.flag = flg;
                if(contoltype== "GrdFill")
                {
                    lTransaction_BAL.lTransactionNew_CDAL.Report_DdlValue = ddlSelectValue.SelectedValue.ToString();
                    fromdt = txtFromDate.Text.Trim();
                    Todt = txtToDate.Text.Trim();
                    if (fromdt.Length == 10 && Todt.Length == 10)
                    {
                        fromdt = fromdt.ToString().Substring(3, 3) + fromdt.ToString().Substring(0, 3) + fromdt.ToString().Substring(6, 4);
                        lTransaction_BAL.lTransactionNew_CDAL.FromDate = fromdt;
                        Todt = Todt.ToString().Substring(3, 3) + Todt.ToString().Substring(0, 3) + Todt.ToString().Substring(6, 4);
                        lTransaction_BAL.lTransactionNew_CDAL.ToDate = Todt;

                    }
                    else {
                        lTransaction_BAL.lTransactionNew_CDAL.FromDate = ""; lTransaction_BAL.lTransactionNew_CDAL.ToDate = "";


                    }

                }

                ds = lTransaction_BAL.GetCustomerBill_RPT();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (contoltype == "ddlFill")
                        {
                            ddlSelectValue.DataSource = ds.Tables[0];
                            ddlSelectValue.DataTextField = "KeyValue";
                            ddlSelectValue.DataValueField = "KeyID";
                            ddlSelectValue.DataBind();
                        }
                        else if (contoltype == "GrdFill")
                        {
                            ViewState["dtdata"] = ds.Tables[0];
                            GrdBilling.DataSource = ds.Tables[0];
                            GrdBilling.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         
 
        protected void btnClear_Click(object sender, EventArgs e)
        {
             ddlReportType.SelectedIndex = 0;
            ddlSelectValue.Items.Clear();
            DataTable dt = new DataTable();
            GrdBilling.DataSource = dt;
            GrdBilling.DataBind();
           
        }
        
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            
        }


        protected void btnModify_Click(object sender, EventArgs e)
        {
            if ((ddlReportType.SelectedValue.ToString() == "CustomerWise" || ddlReportType.SelectedValue.ToString() == "PackageNoWise"
                || ddlReportType.SelectedValue.ToString() == "ProductNameWise") && ddlSelectValue.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select report criteria')", true);
            }
            else if (ddlReportType.SelectedValue.ToString() == "DateWise" && (txtFromDate.Text=="" || txtToDate.Text=="") )
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter From & To date')", true);
            }
            else
            {
                if (ddlReportType.SelectedValue.ToString() == "CustomerWise")
                {
                    LoadData("Billing_CustomerBased", "GrdFill");
                }
                else if (ddlReportType.SelectedValue.ToString() == "PackageNoWise")
                {
                    LoadData("Billing_PackageNoBased", "GrdFill");
                }
                else if (ddlReportType.SelectedValue.ToString() == "ProductNameWise")
                {
                    LoadData("Billing_ProductNameBased", "GrdFill");
                }
                else if (ddlReportType.SelectedValue.ToString() == "ProductNameWise")
                {
                    LoadData("Billing_ProductNameBased", "GrdFill");
                }
                else if (ddlReportType.SelectedValue.ToString() == "DateWise")
                {
                    LoadData("Billing_DateWise", "GrdFill");
                }
                else if (ddlReportType.SelectedValue.ToString() == "AllData")
                {
                    LoadData("Billing_AllData", "GrdFill");
                }

            }

        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {

            ExportExcel("Billing");
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