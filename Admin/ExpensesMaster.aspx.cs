using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDAL;
using BAL;
using System.Data;
using System.Drawing;
using System.IO;
using DAL;

namespace Client.Admin
{

    public partial class ExpensesMaster : System.Web.UI.Page
    {
        int scode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Session["UserID"].ToString() == "")
                {
                    Response.Write("<script language='javascript'>window.alert('Login to View this Page');window.location='/Admin/Index.aspx';</script>");

                }
                else
                {
                    lblUser.Text = Session["UserName"].ToString();
                    pnlGrid.Visible = true;
                    pnlControl.Visible = false;
                    LoadData("SelectSavedExpenses");

                }
                 
            }
        }

        private void LoadData(string flagname)
        {
            try
            {
                DataSet ds = new DataSet();
                ExpensesMaster_BAL lExpensesMaster_BAL = new ExpensesMaster_BAL();
                lExpensesMaster_BAL.lExpensesMaster_CDAL = new ExpensesMaster_CDAL();
                lExpensesMaster_BAL.lExpensesMaster_CDAL.flag = flagname;
                ds = lExpensesMaster_BAL.GetExpensesMaster();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (flagname == "SelectSavedExpenses")
                        {
                            ViewState["dtdata"] = ds.Tables[0];
                            GridExpenses.DataSource = ds.Tables[0];
                            GridExpenses.DataBind();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.InnerHtml == "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save")
                {
                    DML("Insert", 0);
                    pnlGrid.Visible = true;
                    pnlControl.Visible = false;
                    LoadData("SelectSavedExpenses");
                }
                else
                {
                    DML("Update", Convert.ToInt32(hdnExpId.Value));
                    pnlGrid.Visible = true;
                    pnlControl.Visible = false;
                    LoadData("SelectSavedExpenses");
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in pnlControl.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    ((TextBox)(ctrl)).Text = string.Empty;
                }
            }
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
        }

        protected void Edit(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            LinkButton lbtn = (LinkButton)sender;
            pnlControl.Visible = true;
            pnlGrid.Visible = false;
            ExpensesMaster_BAL lExpensesMaster_BAL = new ExpensesMaster_BAL();
            scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            lExpensesMaster_BAL.lExpensesMaster_CDAL = new ExpensesMaster_CDAL();
            lExpensesMaster_BAL.lExpensesMaster_CDAL.ExpTypeID = scode;
            lExpensesMaster_BAL.lExpensesMaster_CDAL.flag = "SelectSavedExpenses";

            ds = lExpensesMaster_BAL.GetExpensesMaster();
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnSave.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                hdnExpId.Value = Convert.ToString(ds.Tables[0].Rows[0]["ExpTypeID"].ToString());
                txtExpensesName.Text = ds.Tables[0].Rows[0]["ExpType"].ToString();
            }
        }

        protected void Delete(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            DML("Delete", Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString()));
            LoadData("SelectSavedExpenses");
        }

        private void DML(string flg, int bid)
        {
            int se;
            DataSet ds = new DataSet();
            scode = Convert.ToInt32(bid);
            ExpensesMaster_BAL lExpensesMaster_BAL = new ExpensesMaster_BAL();
            lExpensesMaster_BAL.lExpensesMaster_CDAL = new ExpensesMaster_CDAL();
            lExpensesMaster_BAL.lExpensesMaster_CDAL.ExpTypeID = scode;
            lExpensesMaster_BAL.lExpensesMaster_CDAL.ExpType = txtExpensesName.Text.Trim();

            lExpensesMaster_BAL.lExpensesMaster_CDAL.flag = flg;
            // ds = lExpensesMaster_BAL.GetExpensesMaster();


            se = lExpensesMaster_BAL.InsUptDel(flg);
            if (se > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + flg + "Successfully')", true);
                LoadData("SelectSavedExpenses");
                scode = 0;
            }
            else
            {
                if (se == -1 && flg == "Delete")
                { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Expenses type Exists in Debit/Credit Note. So delete Debit note first')", true); }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Try Again')", true);
            }
        

        /*
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
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record already Exists. Pls enter new Shop name')", true);

                            }
                            else if (ds.Tables[0].Rows[0][0].ToString() == "-2")
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Shop Name already exists in trxn. Pls delete that first')", true);

                            }
                        }
                    }
        */
    }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlControl.Visible = true;
            pnlGrid.Visible = false;
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            pnlGrid.Visible = true;
            pnlControl.Visible = false;
            LoadData("SelectSavedExpenses");
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            
            ExportExcel( "ExpensesMaster");
        }

        public void ExportExcel( string filename)
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