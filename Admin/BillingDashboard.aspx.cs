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
    public partial class BillingDashboard : System.Web.UI.Page
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
                    LoadCustomers();
                    LoadSavedBilling();
                }

                Control list = this.Page.Master.FindControl("LiMaster");
                list.Visible = true;
                 
                LoadCustomers();
            }
        }
        private void LoadCustomers()
        {
            try
            {
                DataSet ds = new DataSet();
                TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
                lTransaction_BAL.lTransactionNew_CDAL.flag = "SelectCustomer";
                ds = lTransaction_BAL.GetCustomerBill();

                ddlCustomer.DataSource = ds.Tables[0];
                ddlCustomer.DataTextField = "CUSTOMER_NAME";
                ddlCustomer.DataValueField = "CUSTOMER_ID";
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Customer--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadSavedBilling()
        {
            DataSet ds = new DataSet();           

            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "SelectSavedBilling";
            lTransaction_BAL.lTransactionNew_CDAL.IsAdmin = Session["IsAdmin"].ToString();
            lTransaction_BAL.lTransactionNew_CDAL.UserID = Session["UserID"].ToString();
            ds = lTransaction_BAL.GetCustomerBill();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["dtdata"] = ds.Tables[0];
                    GridAllBilling.DataSource = ds.Tables[0];
                    GridAllBilling.DataBind();
                }


            }
        }

        private void AutoBill()
        {
            DataSet ds = new DataSet();

            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "AutoBillNo";
            ds = lTransaction_BAL.GetCustomerBill();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtBillNo.Text = ds.Tables[0].Rows[0][0].ToString();
                }


            }
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPendingBIlls();

        }

        private void LoadPendingBIlls()
        {
            try
            {
                DataSet ds = new DataSet();
                TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
                lTransaction_BAL.lTransactionNew_CDAL.Customer_ID = ddlCustomer.SelectedValue.ToString();
                lTransaction_BAL.lTransactionNew_CDAL.flag = "SelectPendingBills";
                ds = lTransaction_BAL.GetCustomerBill();


                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["dtdata"] = ds.Tables[1];

                        GrdBilling.DataSource = ds.Tables[0];
                        GrdBilling.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            protected void GrdBilling_Add_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["dtdata"];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddlShopName = (e.Row.FindControl("ddlShopName") as DropDownList);
                        ddlShopName.DataSource = dt;
                        ddlShopName.DataTextField = "ShopName";
                        ddlShopName.DataValueField = "ShopID";
                        ddlShopName.DataBind();
                        ddlShopName.Items.Insert(0, new ListItem("--Select ShopName--", "0"));

                        if (scode > 0)
                        {
                            HiddenField hdnshp = (e.Row.FindControl("hdnShopID") as HiddenField);
                            ddlShopName.SelectedValue = hdnshp.Value;
                            // < asp:CheckBox ID = "chk_Select" runat = "server" HeaderText = "Select" Width = "10px" />
                            CheckBox chk1 = (e.Row.FindControl("chk_Select") as CheckBox);
                            chk1.Checked = true;
                        }

                    }
                    
                }
            }

        }

        protected void txtNewRate_TextChanged(object sender, EventArgs e)
        {
            //double totamt = 0;
            //totAmount.Text = "";
            TextBox txt1 = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt1.NamingContainer;
            TextBox txtNewRate1 = (TextBox)row.FindControl("txtNewRate");
            TextBox txtGridAmt1 = (TextBox)row.FindControl("txtGridAmt");
            Label LblQty1 = (Label)row.FindControl("LblQty");
            txtGridAmt1.Text = Convert.ToString(Convert.ToDouble(txtNewRate1.Text) * Convert.ToDouble(LblQty1.Text));

            //foreach (GridViewRow row1 in GrdBilling.Rows)
            //{
            //    CheckBox chk = (CheckBox)row1.FindControl("chk_Select");


            //    if (chk.Checked == true)
            //    {
            //        totamt = totamt + Convert.ToDouble(txtGridAmt1.Text);
                    
            //    }
            //}
            //totAmount.Text= totamt.ToString();
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
            DataTable dt = new DataTable();
            GrdBilling.DataSource = dt;
            GrdBilling.DataBind();
            pnlGrid.Visible = true;
            pnlControl.Visible = false;
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
                    LoadSavedBilling();
                }
                else
                {
                    DML("Update", Convert.ToInt32(hdnBillingId.Value));
                    pnlGrid.Visible = true;
                    pnlControl.Visible = false;
                    LoadSavedBilling();
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
        protected void Edit(object sender, EventArgs e)
        {

            LoadCustomers();
            DataSet ds = new DataSet();
            LinkButton lbtn = (LinkButton)sender;
            pnlControl.Visible = true;
            pnlGrid.Visible = false;
            
            scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());

            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();

            lTransaction_BAL.lTransactionNew_CDAL.flag = "SelectSavedBilling_ID";
            lTransaction_BAL.lTransactionNew_CDAL.BHID = scode;
          


            ds = lTransaction_BAL.GetCustomerBill();
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnSave.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                hdnBillingId.Value = Convert.ToString(ds.Tables[0].Rows[0]["ShopID"].ToString());
                ddlCustomer.SelectedValue = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                txtBillNo.Text = ds.Tables[0].Rows[0]["bill_no"].ToString();
                txtShipmentNo.Text = ds.Tables[0].Rows[0]["Shipment_No"].ToString();
                txtToPay.Text = ds.Tables[0].Rows[0]["To_pay"].ToString();
                txtEffectiveDate.Text = ds.Tables[0].Rows[0]["Bill_date"].ToString();
                //totAmount.Text = ds.Tables[0].Rows[0]["Total_Amount"].ToString();

                ViewState["dtdata"] = ds.Tables[0];

                GrdBilling.DataSource = ds.Tables[0];
                GrdBilling.DataBind();

            }


        }

        protected void Delete(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            DML("Delete", Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString()));
            LoadSavedBilling();
        }

        private void DML(string flg, int bid)
        {
            
            DataSet ds = new DataSet();
            DataRow dr;
            scode = Convert.ToInt32(bid);
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();

            
            lTransaction_BAL.lTransactionNew_CDAL.BHID = scode;
            
            if (flg != "Delete")
            {
               
                if (flg == "Insert" || flg == "Update")
                {
                    lTransaction_BAL.lTransactionNew_CDAL.flag = "EmptyBillDataSet";
                }
                

                ds = lTransaction_BAL.GetCustomerBill();
                int sno = 0;
                double totqty = 0, totweight = 0, totrate = 0, totAmt = 0;

                //if (btnSave.InnerHtml == "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save")
                //{

                foreach (GridViewRow row in GrdBilling.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk_Select");


                    if (chk.Checked == true)
                    {

                        HiddenField hdnGrdPackageNo1 = (HiddenField)row.FindControl("hdnGrdPackageNo");
                        HiddenField hdnFinalPackID1 = (HiddenField)row.FindControl("hdnFinalPackID");

                        Label LblRate1 = (Label)row.FindControl("LblRate");
                        Label LblQty1 = (Label)row.FindControl("LblQty");
                        Label LblWeight1 = (Label)row.FindControl("LblWeight");

                        DropDownList ddlShopName1 = (DropDownList)row.FindControl("ddlShopName");

                        TextBox txtNewRate1 = (TextBox)row.FindControl("txtNewRate");
                        TextBox txtGridAmt1 = (TextBox)row.FindControl("txtGridAmt");


                        dr = ds.Tables[0].NewRow();
                        dr["Sno"] = sno + 1;
                        dr["Package_No"] = hdnGrdPackageNo1.Value;
                        dr["FINAL_PACKING_DETAIL_ID"] = hdnFinalPackID1.Value;

                        dr["ShopID"] = ddlShopName1.SelectedValue.ToString();
                        dr["InvoiceRate"] = LblRate1.Text;
                        //dr["InvoiceRate"] = txtNewRate1.Text;

                        dr["Current_Rate"] = txtNewRate1.Text;

                        dr["Bill_amt"] = txtGridAmt1.Text;

                        totqty = totqty + Convert.ToDouble(LblQty1.Text);
                        totweight = totweight + Convert.ToDouble(LblWeight1.Text);
                        totrate = totrate + Convert.ToDouble(txtNewRate1.Text);
                        totAmt = totAmt + Convert.ToDouble(txtGridAmt1.Text);

                        ds.Tables[0].Rows.Add(dr);

                    }
                }

                string effdt = txtEffectiveDate.Text.Trim();
                effdt = effdt.ToString().Substring(3, 3) + effdt.ToString().Substring(0, 3) + effdt.ToString().Substring(6, 4);

               
                lTransaction_BAL.lTransactionNew_CDAL.Bill_no = txtBillNo.Text;
                lTransaction_BAL.lTransactionNew_CDAL.EffectiveDate = effdt;
                lTransaction_BAL.lTransactionNew_CDAL.Customer_ID = ddlCustomer.SelectedValue.ToString();
                lTransaction_BAL.lTransactionNew_CDAL.Shipment_No = txtShipmentNo.Text;
                lTransaction_BAL.lTransactionNew_CDAL.Total_Qty = totqty.ToString();
                lTransaction_BAL.lTransactionNew_CDAL.Total_Weight = totweight.ToString();
                lTransaction_BAL.lTransactionNew_CDAL.Total_Rate = totrate.ToString();
                lTransaction_BAL.lTransactionNew_CDAL.ToPay = txtToPay.Text;
                lTransaction_BAL.lTransactionNew_CDAL.Total_Amt = totAmt.ToString();
                lTransaction_BAL.lTransactionNew_CDAL.UserID = Session["UserID"].ToString();
                lTransaction_BAL.lTransactionNew_CDAL.xmlvalue = ds.GetXml();               
            }

            lTransaction_BAL.lTransactionNew_CDAL.flag = flg;
            int se = lTransaction_BAL.InsUptDel();
            if (se > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + flg + "Successfully')", true);
                scode = 0;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Try Again')", true);
            }

             

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlControl.Visible = true;
            pnlGrid.Visible = false;
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
            AutoBill();
        }


        protected void btnModify_Click(object sender, EventArgs e)
        {
            pnlGrid.Visible = true;
            pnlControl.Visible = false;
            LoadSavedBilling();
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