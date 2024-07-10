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
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using DAL;

namespace Client.Admin
{
     
    public partial class DebitNote : System.Web.UI.Page
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
                    LoadData("ShipmentNo");
                    LoadData("ExpensesType");
                    LoadData("SelectSaved_DRCR");
                    LoadData("SelectEmployee");
                }
                

                if (Session["IsAdmin"].ToString() != "1")
                {
                    ddlEmp.SelectedValue = Session["EmpID"].ToString();
                    ddlEmp.Enabled = false;
                }
            }
        }

        private void LoadData(string flagname)
        {
            //try
            //{
            DataSet ds = new DataSet();
            DebitNote_BAL lDebitNote_BAL = new DebitNote_BAL();
            lDebitNote_BAL.lDebitNote_CDAL = new DebitNote_CDAL();
            lDebitNote_BAL.lDebitNote_CDAL.flag = flagname;
            lDebitNote_BAL.lDebitNote_CDAL.IsAdmin = Session["IsAdmin"].ToString();
            lDebitNote_BAL.lDebitNote_CDAL.UserID = Session["UserID"].ToString();

            ds = lDebitNote_BAL.GetDebitCreditNote();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (flagname == "ShipmentNo")
                    {
                        ddlShipmentNo.DataSource = ds.Tables[0];
                        ddlShipmentNo.DataTextField = "Shipment_No";
                        ddlShipmentNo.DataValueField = "Shipment_No";
                        ddlShipmentNo.DataBind();
                        ddlShipmentNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select ShipmentNo--", "0"));
                    }
                    else if (flagname == "ExpensesType")
                    {
                        ddlExpType.DataSource = ds.Tables[0];
                        ddlExpType.DataTextField = "ExpType";
                        ddlExpType.DataValueField = "ExpTypeID";
                        ddlExpType.DataBind();
                        ddlExpType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Expenses Type--", "0"));
                    }
                    else if (flagname == "SelectSaved_DRCR")
                    {
                        ViewState["dtdata"] = ds.Tables[0];
                        GridDrCRNote.DataSource = ds.Tables[0];
                        GridDrCRNote.DataBind();
                    }
                    else if (flagname == "AutoBillNo")
                    {
                        txtDebitCreditNo.Text = ds.Tables[0].Rows[0][0].ToString();
                    }
                    else if (flagname == "SelectEmployee")
                    {

                        ddlEmp.DataSource = ds.Tables[0];
                        ddlEmp.DataTextField = "Empname";
                        ddlEmp.DataValueField = "Empid";
                        ddlEmp.DataBind();
                        ddlEmp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Employee--", "0"));
                    }
                    //txtBillNo.Text = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            int isok = 1;
            try
            {
                if (ddlTrxnType.SelectedIndex>0 && ddlExpType.SelectedIndex > 0 && ddlShipmentNo.SelectedIndex > 0 && ddlEmp.SelectedIndex>0)
                {
                    if (btnSave.InnerHtml == "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save")
                    {
                        DML("Insert", 0);
                        pnlGrid.Visible = true;
                        pnlControl.Visible = false;
                        LoadData("SelectSaved_DRCR");
                    }
                    else
                    {
                        DML("Update", Convert.ToInt32(hdnDRCR_ID.Value));
                        pnlGrid.Visible = true;
                        pnlControl.Visible = false;
                        LoadData("SelectSaved_DRCR");
                    }
                }
                else
                {
                    isok = 0;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Either Trxn Type / Expenses Type / Shipment / Employee is not selected')", true);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (isok == 1)
                {
                    btnClear_Click(sender, e);
                }
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
                else if (ctrl.GetType() == typeof(DropDownList))
                {
                    ((DropDownList)(ctrl)).SelectedIndex = 0;
                    ((DropDownList)(ctrl)).Enabled = true;
                }
            }
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
            LoadData("AutoBillNo");
        }

        protected void Edit(object sender, EventArgs e)
        {
            LoadData("CargoNo");
            LoadData("ExpensesType");
            LoadData("SelectEmployee");

            DataSet ds = new DataSet();
            LinkButton lbtn = (LinkButton)sender;
            pnlControl.Visible = true;
            pnlGrid.Visible = false;
             
            
            DebitNote_BAL lDebitNote_BAL = new DebitNote_BAL();
            lDebitNote_BAL.lDebitNote_CDAL = new DebitNote_CDAL();
            lDebitNote_BAL.lDebitNote_CDAL.DRCR_ID = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            lDebitNote_BAL.lDebitNote_CDAL.flag = "SelectSaved_DRCR_ID";
            ds = lDebitNote_BAL.GetDebitCreditNote();

             
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnSave.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                hdnDRCR_ID.Value = Convert.ToString(ds.Tables[0].Rows[0]["DRCR_ID"].ToString());
                txtDebitCreditNo.Text = ds.Tables[0].Rows[0]["DRCR_No"].ToString();
                ddlTrxnType.SelectedValue = ds.Tables[0].Rows[0]["TrxnType"].ToString();
                txtEffectiveDate.Text = ds.Tables[0].Rows[0]["TrxnDate"].ToString();
                ddlExpType.SelectedValue = ds.Tables[0].Rows[0]["ExpTypeID"].ToString();
                ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["EmpID"].ToString();
                ddlShipmentNo.SelectedValue = ds.Tables[0].Rows[0]["Shipment_No"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["TrxnAmt"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

            }
        }

        protected void Delete(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            DML("Delete", Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString()));
            LoadData("SelectSaved_DRCR");
        }

        private void DML(string flg, int bid)
        {
            //int se;
            DataSet ds = new DataSet();
            scode = Convert.ToInt32(bid);
            DebitNote_BAL lDebitNote_BAL = new DebitNote_BAL();
            lDebitNote_BAL.lDebitNote_CDAL = new DebitNote_CDAL();
            lDebitNote_BAL.lDebitNote_CDAL.DRCR_ID = scode;
            lDebitNote_BAL.lDebitNote_CDAL.flag = flg;

            if (flg != "Delete")
            {
                string effdt = txtEffectiveDate.Text.Trim();
                effdt = effdt.ToString().Substring(3, 3) + effdt.ToString().Substring(0, 3) + effdt.ToString().Substring(6, 4);               
                lDebitNote_BAL.lDebitNote_CDAL.DRCR_No = txtDebitCreditNo.Text;
                lDebitNote_BAL.lDebitNote_CDAL.TrxnType = ddlTrxnType.SelectedValue.ToString();
                lDebitNote_BAL.lDebitNote_CDAL.TrxnDate = effdt;
                lDebitNote_BAL.lDebitNote_CDAL.ExpTypeID = ddlExpType.SelectedValue.ToString();
                lDebitNote_BAL.lDebitNote_CDAL.ExpType = ddlExpType.SelectedItem.ToString();
                lDebitNote_BAL.lDebitNote_CDAL.EmpID = ddlEmp.SelectedValue.ToString();
                lDebitNote_BAL.lDebitNote_CDAL.Shipment_No = ddlShipmentNo.SelectedValue.ToString();
                lDebitNote_BAL.lDebitNote_CDAL.TrxnAmt = txtAmount.Text;
                lDebitNote_BAL.lDebitNote_CDAL.Remarks = txtRemarks.Text;
                lDebitNote_BAL.lDebitNote_CDAL.UserID = Session["UserID"].ToString();                
            }

            int se = lDebitNote_BAL.InsUptDel();
            if (se > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + flg + "Successfully')", true);
                scode = 0;
            }
            else
            {
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
            LoadData("AutoBillNo");
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            pnlGrid.Visible = true;
            pnlControl.Visible = false;
            LoadData("SelectSaved_DRCR");
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            
            ExportExcel( "DebitCreditNote");
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