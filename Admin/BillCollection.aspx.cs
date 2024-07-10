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
using Razorpay.Api;
using System.Data.SqlClient;
using DAL;

namespace Client.Admin
{
    public partial class BillCollection : System.Web.UI.Page
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
                    LoadData("SelectCustomer");
                    LoadData("SelectSavedBilling");
                    LoadData("SelectEmployee");
                }

                 

                if (Session["IsAdmin"].ToString() != "1")
                {
                    ddlPayTo.SelectedValue = Session["EmpID"].ToString();
                    ddlPayTo.Enabled = false;
                }
                
            }
        }
        private void LoadData(string flagname)
        {
            try
            {
                DataSet ds = new DataSet();
                BillCollection_BAL lBillCollection_BAL = new BillCollection_BAL();
                lBillCollection_BAL.lBillCollection_CDAL = new BillCollection_CDAL();
                lBillCollection_BAL.lBillCollection_CDAL.flag = flagname;
                lBillCollection_BAL.lBillCollection_CDAL.IsAdmin = Session["IsAdmin"].ToString();
                lBillCollection_BAL.lBillCollection_CDAL.UserID = Session["UserID"].ToString();

                if (flagname == "SelectPendingBills")
                {
                    lBillCollection_BAL.lBillCollection_CDAL.CustomerID = ddlCustomer.SelectedValue.ToString();
                }
                ds = lBillCollection_BAL.GetBillReceipt();
                if (flagname == "SelectCustomer")
                {

                    ddlCustomer.DataSource = ds.Tables[0];
                    ddlCustomer.DataTextField = "CUSTOMER_NAME";
                    ddlCustomer.DataValueField = "CUSTOMER_ID";
                    ddlCustomer.DataBind();
                    ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Customer--", "0"));
                }
                else if (flagname == "SelectEmployee")
                {

                    ddlPayTo.DataSource = ds.Tables[0];
                    ddlPayTo.DataTextField = "Empname";
                    ddlPayTo.DataValueField = "Empid";
                    ddlPayTo.DataBind();
                    ddlPayTo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Employee--", "0"));
                }
                else if (flagname == "SelectSavedBilling")
                {
                    ViewState["dtdata"] = ds.Tables[0];
                    GridAllCollection.DataSource = ds.Tables[0];
                    GridAllCollection.DataBind();
                }
                else if (flagname == "AutoBillNo")
                {
                    txtReceiptNo.Text = ds.Tables[0].Rows[0][0].ToString();
                }
                else if (flagname == "SelectPendingBills")
                {
                    ViewState["dtdata"] = ds.Tables[0];
                    GrdCollection.DataSource = ds.Tables[0];
                    GrdCollection.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

       
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {  
            LoadData("SelectPendingBills"); 
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
            DataTable dt = new DataTable();
            GrdCollection.DataSource = dt;
            GrdCollection.DataBind();
            pnlGrid.Visible = true;
            pnlControl.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int isok = 1;
            try
            {
                if (ddlCustomer.SelectedIndex > 0 && ddlPayTo.SelectedIndex > 0)
                {
                    if (btnSave.InnerHtml == "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save")
                    {
                        DML("Insert", 0);
                        pnlGrid.Visible = true;
                        pnlControl.Visible = false;
                        LoadData("SelectSavedBilling");
                    }
                    else
                    {
                        DML("Update", Convert.ToInt32(hdnBillingId.Value));
                        pnlGrid.Visible = true;
                        pnlControl.Visible = false;
                        LoadData("SelectSavedBilling");
                    }
                }
                else
                {
                    isok = 0;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Either Customer or Pay to is not selected')", true);
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
        protected void Edit(object sender, EventArgs e)
        {

            LoadData("SelectCustomer");
            LoadData("SelectEmployee");
            DataSet ds = new DataSet();
            LinkButton lbtn = (LinkButton)sender;
            pnlControl.Visible = true;
            pnlGrid.Visible = false;
            
            scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
 

            
            BillCollection_BAL lBillCollection_BAL = new BillCollection_BAL();
            lBillCollection_BAL.lBillCollection_CDAL = new BillCollection_CDAL();
            lBillCollection_BAL.lBillCollection_CDAL.flag = "SelectSavedBilling_ID";
             
                lBillCollection_BAL.lBillCollection_CDAL.CHID = scode;
             
            ds = lBillCollection_BAL.GetBillReceipt();


            
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnSave.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                hdnBillingId.Value = Convert.ToString(ds.Tables[0].Rows[0]["CHID"].ToString());
                ddlCustomer.SelectedValue = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                txtReceiptNo.Text = ds.Tables[0].Rows[0]["Receipt_No"].ToString();
                txtEffectiveDate.Text = ds.Tables[0].Rows[0]["Receipt_date"].ToString();
                ddlPayTo.SelectedValue = ds.Tables[0].Rows[0]["PaytoEmpID"].ToString();
                txtModeofPay.Text = ds.Tables[0].Rows[0]["ModeofPay"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txttotAmount.Text = ds.Tables[0].Rows[0]["ReceivedAmt"].ToString();

                ViewState["dtdata"] = ds.Tables[0];

                GrdCollection.DataSource = ds.Tables[0];
                GrdCollection.DataBind();

            }


        }

        protected void GrdCollection_Add_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (scode > 0)
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                { 
                    CheckBox chk1 = (e.Row.FindControl("chk_Select") as CheckBox);
                    chk1.Checked = true;
                }

            }
        }
        protected void Delete(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            DML("Delete", Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString()));
            LoadData("SelectSavedBilling");
        }

        private void DML(string flg, int bid)
        {
            
            DataSet ds = new DataSet();
            DataRow dr;
            scode = Convert.ToInt32(bid);
        

            BillCollection_BAL lBillCollection_BAL = new BillCollection_BAL();
            lBillCollection_BAL.lBillCollection_CDAL = new BillCollection_CDAL();
            lBillCollection_BAL.lBillCollection_CDAL.flag = flg;
            lBillCollection_BAL.lBillCollection_CDAL.CHID = scode;

            if (flg != "Delete")
            {
               
                if (flg == "Insert" || flg == "Update")
                {
                    lBillCollection_BAL.lBillCollection_CDAL.flag = "EmptyBillDataSet";
                }
                

                ds = lBillCollection_BAL.GetBillReceipt();
                int sno = 0;
                double   totAmt = 0;

                //if (btnSave.InnerHtml == "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save")
                //{

                foreach (GridViewRow row in GrdCollection.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk_Select");


                    if (chk.Checked == true)
                    {

                        HiddenField hdnBHID1 = (HiddenField)row.FindControl("hdnBHID");
                       

                        Label LblBIll_no1 = (Label)row.FindControl("LblBIll_no");
                         
                        TextBox txtAmtPaid1 = (TextBox)row.FindControl("txtAmtPaid");


                        dr = ds.Tables[0].NewRow();
                        dr["Sno"] = sno + 1;
                        dr["BHID"] = hdnBHID1.Value;
                        dr["Bill_No"] = LblBIll_no1.Text; 
                        dr["CollectedAmt"] = txtAmtPaid1.Text; 
                        totAmt = totAmt + Convert.ToDouble(txtAmtPaid1.Text);

                        ds.Tables[0].Rows.Add(dr);

                    }
                }

                string effdt = txtEffectiveDate.Text.Trim();
                effdt = effdt.ToString().Substring(3, 3) + effdt.ToString().Substring(0, 3) + effdt.ToString().Substring(6, 4);

            

                lBillCollection_BAL.lBillCollection_CDAL.flag = flg;
                lBillCollection_BAL.lBillCollection_CDAL.Receipt_No = txtReceiptNo.Text;
                lBillCollection_BAL.lBillCollection_CDAL.EffectiveDate = effdt;
                lBillCollection_BAL.lBillCollection_CDAL.CustomerID = ddlCustomer.SelectedValue.ToString();
                lBillCollection_BAL.lBillCollection_CDAL.Payto = ddlPayTo.SelectedValue.ToString();
                lBillCollection_BAL.lBillCollection_CDAL.ModeofPay = txtModeofPay.Text;
                lBillCollection_BAL.lBillCollection_CDAL.Total_Amount = totAmt.ToString();
                lBillCollection_BAL.lBillCollection_CDAL.Remarks = txtRemarks.Text;
                lBillCollection_BAL.lBillCollection_CDAL.UserID = Session["UserID"].ToString();
                lBillCollection_BAL.lBillCollection_CDAL.xmlvalue = ds.GetXml();

               
            }
             
            int se = lBillCollection_BAL.InsUptDel();
            if (se > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + flg + "Successfully')", true);
                 
                scode = 0;
            }
            else
            {
                
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Try Again')", true);
            }

            //DML("AdminBulkUpdate", 0);
            //if (ds.Tables.Count > 0)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        //if (ds.Tables[0].Rows[0][0].ToString() == "1")
            //        //{
            //        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Processed Successfully')", true);
            //        //    LoadPendingBIlls();

            //        //}
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ds.Tables[0].Rows[0][1].ToString() + "')", true);
            //    }
            //}

          



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
            LoadData("SelectSavedBilling");
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