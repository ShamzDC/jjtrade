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

namespace Client.Admin
{
     
    public partial class ShopMaster : System.Web.UI.Page
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
                    LoadShop();
                }
                 
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
        private void LoadShop()
        {
            DataSet ds = new DataSet();
            ShopMaster_BAL lShopMaster_BAL = new ShopMaster_BAL();
            lShopMaster_BAL.lShopMaster_CDAL = new ShopMaster_CDAL();
            lShopMaster_BAL.lShopMaster_CDAL.flag = "Select";
            ds = lShopMaster_BAL.GetShop();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["dtdata"] = ds.Tables[0];
                    GridCurrency.DataSource = ds.Tables[0];
                    GridCurrency.DataBind();
                }


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
                    LoadShop();
                }
                else
                {
                    DML("Update", Convert.ToInt32(hdnShopId.Value));
                    pnlGrid.Visible = true;
                    pnlControl.Visible = false;
                    LoadShop();
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
            ShopMaster_BAL lShopMaster_BAL = new ShopMaster_BAL();
            scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            lShopMaster_BAL.lShopMaster_CDAL = new ShopMaster_CDAL();
            lShopMaster_BAL.lShopMaster_CDAL.ShopID = scode;
            lShopMaster_BAL.lShopMaster_CDAL.flag = "Select";

            ds = lShopMaster_BAL.GetShop();
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnSave.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                hdnShopId.Value = Convert.ToString(ds.Tables[0].Rows[0]["ShopID"].ToString());
                ddlCustomer.SelectedValue = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                txtShopName.Text = ds.Tables[0].Rows[0]["ShopName"].ToString();
                txtShopAddress.Text = ds.Tables[0].Rows[0]["ShopAddress"].ToString();
                             
            }
        }

        protected void Delete(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            DML("Delete", Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString()));
            LoadShop();
        }

        private void DML(string flg, int bid)
        {
            //int se;
            DataSet ds = new DataSet();
            scode = Convert.ToInt32(bid);
            ShopMaster_BAL lShopMaster_BAL = new ShopMaster_BAL();
            lShopMaster_BAL.lShopMaster_CDAL = new ShopMaster_CDAL();
            lShopMaster_BAL.lShopMaster_CDAL.ShopID = scode;
            lShopMaster_BAL.lShopMaster_CDAL.ShopName = txtShopName.Text.Trim();
            lShopMaster_BAL.lShopMaster_CDAL.ShopAddress = txtShopAddress.Text.Trim();

            lShopMaster_BAL.lShopMaster_CDAL.CustomerID = ddlCustomer.SelectedValue.ToString();
            lShopMaster_BAL.lShopMaster_CDAL.flag = flg;
            ds = lShopMaster_BAL.GetShop();
            /*
                       se = lShopMaster_BAL.InsUptDel(flg);
                       if (se > 0)
                       {
                           ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + flg + "Successfully')", true);
                           LoadCurrency();
                           scode = 0;
                       }
                       else
                       {
                           ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Try Again')", true);
                       }
            */
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
            LoadShop();
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            
            ExportExcel( "ShopMaster");
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