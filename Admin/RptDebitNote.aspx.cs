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
    public partial class RptDebitNote : System.Web.UI.Page
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
            LoadData(ddlReportType.SelectedValue.ToString()+ "_DebitNote_ddlFill", "ddlFill");

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

                DebitNote_BAL lDebitNote_BAL = new DebitNote_BAL();
                lDebitNote_BAL.lDebitNote_CDAL = new DebitNote_CDAL();
                lDebitNote_BAL.lDebitNote_CDAL.flag = flg;
                lDebitNote_BAL.lDebitNote_CDAL.IsAdmin = Session["IsAdmin"].ToString();
                lDebitNote_BAL.lDebitNote_CDAL.UserID = Session["UserID"].ToString();

                if (contoltype== "GrdFill")
                {
                    lDebitNote_BAL.lDebitNote_CDAL.Report_DdlValue = ddlSelectValue.SelectedValue.ToString();
                    fromdt = txtFromDate.Text.Trim();
                    Todt = txtToDate.Text.Trim();
                    if (fromdt.Length == 10 && Todt.Length == 10)
                    {
                        fromdt = fromdt.ToString().Substring(3, 3) + fromdt.ToString().Substring(0, 3) + fromdt.ToString().Substring(6, 4);
                        lDebitNote_BAL.lDebitNote_CDAL.FromDate = fromdt;
                        Todt = Todt.ToString().Substring(3, 3) + Todt.ToString().Substring(0, 3) + Todt.ToString().Substring(6, 4);
                        lDebitNote_BAL.lDebitNote_CDAL.ToDate = Todt;

                    }
                    else {
                        lDebitNote_BAL.lDebitNote_CDAL.FromDate = ""; lDebitNote_BAL.lDebitNote_CDAL.ToDate = "";
                    }

                }

                ds = lDebitNote_BAL.GetDebitCreditNote_RPT();
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
        
        

        protected void btnModify_Click(object sender, EventArgs e)
        {
            if ((ddlReportType.SelectedValue.ToString() == "EmployeeWise" || ddlReportType.SelectedValue.ToString() == "ShipmentNoWise"
                ) && ddlSelectValue.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select report criteria')", true);
            }
            else if (ddlReportType.SelectedValue.ToString() == "DateWise" && (txtFromDate.Text=="" || txtToDate.Text=="") )
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter From & To date')", true);
            }
            else
            {
                if (ddlReportType.SelectedValue.ToString() == "EmployeeWise")
                {
                    LoadData("Debitnote_EmpBased", "GrdFill");
                }
                else if (ddlReportType.SelectedValue.ToString() == "ShipmentNoWise")
                {
                    LoadData("Debitnote_ShipmentBased", "GrdFill");
                }
               
                else if (ddlReportType.SelectedValue.ToString() == "DateWise")
                {
                    LoadData("Debitnote_DateWise", "GrdFill");
                }
                else if (ddlReportType.SelectedValue.ToString() == "AllData")
                {
                    LoadData("Debitnote_AllData", "GrdFill");
                }

            }

        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {

            ExportExcel("DebitNote");
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