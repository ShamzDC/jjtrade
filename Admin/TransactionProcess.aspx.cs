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
using System.Drawing;
using System.IO;

namespace Client.Admin
{
    public partial class TransactionProcess : System.Web.UI.Page
    {
        int scode;
        public DataSet dsToAgent = new DataSet();
        public DataSet ds_CrDr = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
 
                if (Session["IsAdmin"].ToString() == "0")
                {
                    Response.Write("<script language='javascript'>window.alert('You should Login as Admin');window.location='/Admin/Index.aspx';</script>");
                }
                else
                { 
                    if (Session["UserID"] != null)
                    {
                        pnlProcess.Visible = true;
                        pnlSearch.Visible = false;

                        lblUser.Text = "Agent - " + Session["UserName"].ToString();
                        LoadToAgent();
                        ddlToAgentSearch.DataSource = dsToAgent.Tables[0];
                        ddlToAgentSearch.DataTextField = "AgentName";
                        ddlToAgentSearch.DataValueField = "AgentID";
                        ddlToAgentSearch.DataBind(); 
                        ddlToAgentSearch.Items.Insert(0, new ListItem("--Select Agent--", "0"));

                        if (Session["DashboardAgentID"].ToString() != "0")
                        {
                            ddlToAgentSearch.SelectedValue = Session["DashboardAgentID"].ToString();
                            ddlToAgentSearch.Enabled = false;


                            DataSet dsname = new DataSet();
                            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
                            lTransaction_BAL.lTransactionNew_CDAL.flag = "SelectAgentCurrency";
                            lTransaction_BAL.lTransactionNew_CDAL.To_Agent = Session["DashboardAgentID"].ToString();
                            dsname = lTransaction_BAL.GetTransactionTransfer_Post();
                            if (dsname.Tables.Count > 0)
                            {
                                lblUser.Text = "Admin - " + dsname.Tables[0].Rows[0]["AgentName_WithCurr"].ToString();
                            }
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
        private void LoadToAgent()
        {
            AgentMaster_BAL lAgent_BAL = new AgentMaster_BAL();
            lAgent_BAL.lAgentMaster_CDAL = new AgentMaster_CDAL();
            lAgent_BAL.lAgentMaster_CDAL.flag = "Select";
            dsToAgent = lAgent_BAL.lAgentMaster_CDAL.GetAgent_ds();
        }

        protected void ddlToAgentSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlProcess.Visible = true;
            pnlSearch.Visible = false;
            LoadTransaction();
        }
        

        protected void ddlTrxnStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlProcess.Visible = false;
            pnlSearch.Visible = true;
            LoadTransaction_Status();
        }

        private void LoadTransaction_Status()
        {
            DataTable dt = new DataTable();
            GvStatus.DataSource = dt;
            GvStatus.DataBind();

            DataSet ds = new DataSet();
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.From_Agent = ddlToAgentSearch.SelectedValue.ToString();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "SelectCompletedTrxn";
            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvStatus.DataSource = ds.Tables[0];
                    GvStatus.DataBind();
                }
            }
        }

        private void LoadTransaction()
        {
            LoadToAgent();
            DataTable dt = new DataTable();
            grd_Add.DataSource = dt;
            grd_Add.DataBind();

            DataSet ds = new DataSet();
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.From_Agent = ddlToAgentSearch.SelectedValue.ToString();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "SelectAll_ForProcess";
            
            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            //if (ds.Tables.Count > 0)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        grd_Add.DataSource = ds.Tables[0];
            //        grd_Add.DataBind();
            //    }
            //}

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    pgsource.DataSource = ds.Tables[0].DefaultView;
                    pgsource.AllowPaging = true;
                    ViewState["dtdata"] = ds.Tables[0];

                    pgsource.PageSize = pageSize;
                    pgsource.CurrentPageIndex = CurrentPage;
                    ViewState["TotalPages"] = pgsource.PageCount;
                    lbPrevious.Enabled = !pgsource.IsFirstPage;
                    lbNext.Enabled = !pgsource.IsLastPage;
                    lbFirst.Enabled = !pgsource.IsFirstPage;
                    lbLast.Enabled = !pgsource.IsLastPage;

                    grd_Add.DataSource = pgsource;
                    grd_Add.DataBind();

                    HandlePaging();
                }
                else
                {
                    lbPrevious.Visible = false;
                    lbNext.Visible = false;
                    lbFirst.Visible = false;
                    lbLast.Visible = false;
                    rptPaging.DataSource = null;
                    rptPaging.DataBind();
                }

            }

        }
        protected void ddlToAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            // GridViewRow row1 = grd_Add.Rows[e.];


            DropDownList ddlto = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlto.NamingContainer;
            Label lbleff = (Label)row.FindControl("lblAgentEffectiveDate");
            HiddenField hdnFromCurrency = (HiddenField)row.FindControl("hdnGrdFromAgentCurrency");
            HiddenField hdnToAgentCurr = (HiddenField)row.FindControl("hdnGrdToAgentCurrency");
            HiddenField hdnToAgentPerUnt = (HiddenField)row.FindControl("hdnGrdToAgentPerUnit");

            TextBox txtToAgentCurrency1 = (TextBox)row.FindControl("txtToAgentCurrencyPerUnit");

            Label lblConvertedTrxnAmt1 = (Label)row.FindControl("lblConvertedTrxnAmt");
            Label lblTrxnCr_Dr1 = (Label)row.FindControl("lblTrxnCr_Dr");
            Label lblTrxnAmt1 = (Label)row.FindControl("lblTrxnAmt");
            Label lblToAgentCurrency1 = (Label)row.FindControl("lblToAgentCurrency");
            Label lblIsValid1 = (Label)row.FindControl("lblIsValid");

            string effdt = "";
            effdt = lbleff.Text.ToString().Substring(3, 3) + lbleff.Text.ToString().Substring(0, 3) + lbleff.Text.ToString().Substring(6, 4);

            DataSet ds = new DataSet();
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.From_Currency = hdnFromCurrency.Value;
            lTransaction_BAL.lTransactionNew_CDAL.EffectiveDate = effdt;
            lTransaction_BAL.lTransactionNew_CDAL.To_Agent = ddlto.SelectedValue.ToString();

            lTransaction_BAL.lTransactionNew_CDAL.flag = "SelectCurrencyUnitNew";

            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnToAgentCurr.Value = ds.Tables[0].Rows[0]["ToCurrencyCode"].ToString();
                    hdnToAgentPerUnt.Value = ds.Tables[0].Rows[0]["CurrencyValuePerUnit"].ToString();

                    txtToAgentCurrency1.Text = ds.Tables[0].Rows[0]["CurrencyValuePerUnit"].ToString();

                    lblToAgentCurrency1.Text= ds.Tables[0].Rows[0]["ToCurrencyCode"].ToString() +" / "+ ds.Tables[0].Rows[0]["CurrencyValuePerUnit"].ToString();
                    if (ds.Tables[0].Rows[0]["ToCurrencyCode"].ToString() != "" && ds.Tables[0].Rows[0]["CurrencyValuePerUnit"].ToString() != "")
                    {
                        lblIsValid1.Text = "Y";
                    }
                    lblConvertedTrxnAmt1.Text =Convert.ToString( Convert.ToDouble(lblTrxnAmt1.Text) * Convert.ToDouble(hdnToAgentPerUnt.Value));
                }
            }

        }

        protected void txtToAgentCurrency_TextChanged(object sender, EventArgs e)
        {

            TextBox txt1 = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt1.NamingContainer;
            TextBox txtToAgentCurrency1 = (TextBox)row.FindControl("txtToAgentCurrencyPerUnit");
            Label lblConvertedTrxnAmt1 = (Label)row.FindControl("lblConvertedTrxnAmt");
            Label lblTrxnAmt1 = (Label)row.FindControl("lblTrxnAmt");
            if (txtToAgentCurrency1.Text == "")
            {
                txtToAgentCurrency1.Text= "0";
            }

            lblConvertedTrxnAmt1.Text = Convert.ToString(Convert.ToDouble(lblTrxnAmt1.Text) * Convert.ToDouble(txtToAgentCurrency1.Text));

        }

        protected void grd_Add_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddltoAgnt = (e.Row.FindControl("ddlToAgent") as DropDownList);
                ddltoAgnt.DataSource = dsToAgent.Tables[0];
                ddltoAgnt.DataTextField = "AgentName";
                ddltoAgnt.DataValueField = "AgentID";
                ddltoAgnt.DataBind();
                ddltoAgnt.Items.Insert(0, new ListItem("--Select Agent--", "0"));

                /*
                HiddenField hdnGrdIsProcessed1 = (e.Row.FindControl("hdnGrdIsProcessed") as HiddenField);
                LinkButton Imgedit1 = (e.Row.FindControl("Imgedit") as LinkButton);
                LinkButton Imgdelete1 = (e.Row.FindControl("Imgdelete") as LinkButton);
                CheckBox chk = (e.Row.FindControl("chk_Select") as CheckBox);   

                if (hdnGrdIsProcessed1.Value == "Y")
                {
                    Imgedit1.Visible = false; Imgdelete1.Visible = false;
                    ddltoAgnt.Enabled = false;
                    chk.Enabled = false;
                }
                */

            }

        }
        
        protected void GvStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            pnlProcess.Visible = true;
            pnlSearch.Visible = false;
            txtEffectiveDate.Text = "";
            btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
            //LoadToAgent();
            LoadTransaction();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                pnlProcess.Visible = true;
                pnlSearch.Visible = false;
                DataSet ds = new DataSet();
                DataRow dr;

                TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
                lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
                lTransaction_BAL.lTransactionNew_CDAL.flag = "EmptyDataSet";

                ds = lTransaction_BAL.GetTransactionTransfer_Post();


                if (btnSave.InnerHtml == "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save")
                {

                    foreach (GridViewRow row in grd_Add.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("chk_Select");
                        

                        if (chk.Checked == true)
                        {
                            HiddenField hdnGrdTrxnID1=(HiddenField)row.FindControl("hdnGrdTrxnID");
                            HiddenField hdnFromCurrency = (HiddenField)row.FindControl("hdnGrdFromAgentCurrency");
                            HiddenField hdnToAgentCurr = (HiddenField)row.FindControl("hdnGrdToAgentCurrency");
                            HiddenField hdnToAgentPerUnt = (HiddenField)row.FindControl("hdnGrdToAgentPerUnit");
                            Label lblConvertedTrxnAmt1 = (Label)row.FindControl("lblConvertedTrxnAmt");
                            Label lblTrxnCr_Dr1 = (Label)row.FindControl("lblTrxnCr_Dr");
                            Label lblTrxnAmt1 = (Label)row.FindControl("lblTrxnAmt");
                            Label lblToAgentCurrency1 = (Label)row.FindControl("lblToAgentCurrency");
                            Label lblIsValid1 = (Label)row.FindControl("lblIsValid");
                            DropDownList ddlToAgent1=(DropDownList)row.FindControl("ddlToAgent");

                            TextBox txtToAgentCurrency1 = (TextBox)row.FindControl("txtToAgentCurrencyPerUnit");


                            if (lblIsValid1.Text == "Y")
                            {
                                dr = ds.Tables[0].NewRow();
                                dr["TrxnID"] = hdnGrdTrxnID1.Value;

                                dr["FromCurrency_PerUnit"] = ""; dr["To_Agent"] = ddlToAgent1.SelectedValue.ToString(); dr["To_Currency"] = hdnToAgentCurr.Value;
                                //dr["ToCurrency_PerUnit"] = hdnToAgentPerUnt.Value;
                                dr["ToCurrency_PerUnit"] = txtToAgentCurrency1.Text;
                                if (lblTrxnCr_Dr1.Text == "Debit")
                                {
                                    dr["ToAgent_CR_DR"] = "Credit";
                                    dr["CreditUnit"] = lblConvertedTrxnAmt1.Text;
                                    dr["Credit_Amt"] = lblConvertedTrxnAmt1.Text;

                                    dr["DebitUnit"] = lblTrxnAmt1.Text;
                                    dr["Debit_Amt"] = lblTrxnAmt1.Text;
                                }
                                else
                                {
                                    dr["ToAgent_CR_DR"] = "Debit";
                                    dr["DebitUnit"] = lblConvertedTrxnAmt1.Text;
                                    dr["Debit_Amt"] = lblConvertedTrxnAmt1.Text;

                                    dr["CreditUnit"] = lblTrxnAmt1.Text;
                                    dr["Credit_Amt"] = lblTrxnAmt1.Text;
                                }


                                ds.Tables[0].Rows.Add(dr);
                            }
                        }
                    }

                    string effdt = txtEffectiveDate.Text.Trim();
                    effdt = effdt.ToString().Substring(3, 3) + effdt.ToString().Substring(0, 3) + effdt.ToString().Substring(6, 4);
                    lTransaction_BAL.lTransactionNew_CDAL.IsAdmin_Agent = "Admin";
                    lTransaction_BAL.lTransactionNew_CDAL.EffectiveDate = effdt;
                    lTransaction_BAL.lTransactionNew_CDAL.AdminUserID = Session["UserID"].ToString();
                    lTransaction_BAL.lTransactionNew_CDAL.flag = "AdminBulkUpdate"; //@Agent_EffectiveDate
                    lTransaction_BAL.lTransactionNew_CDAL.xmlvalue = ds.GetXml();
                    ds = lTransaction_BAL.GetTransactionTransfer_Post();
                    //DML("AdminBulkUpdate", 0);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "1")
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Processed Successfully')", true);
                                btnClear_Click(sender, e);
                            }
                            
                        }
                    }

                }
                else
                {
                   // DML("AgentUpdate", Convert.ToInt32(hdnTrxnId.Value));
                    
                    //LoadTransaction();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               
            }
        }
        protected void EditStatus(object sender, EventArgs e)
        {
            // ******** moving into pending status again ************//
            DataSet ds = new DataSet();
            LinkButton lbtn = (LinkButton)sender;
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();
            lTransaction_BAL.lTransactionNew_CDAL.TrxnID = scode;
            lTransaction_BAL.lTransactionNew_CDAL.IsAdmin_Agent = Session["IsAgent_Admin"].ToString();
            lTransaction_BAL.lTransactionNew_CDAL.flag = "Move_To_Process";

            
            ds = lTransaction_BAL.GetTransactionTransfer_Post();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sucessfully Moved for Process')", true);

                    }

                     
                }
            }

            LoadTransaction();
            LoadTransaction_Status();
        }
        
        protected void Delete1(object sender, EventArgs e)
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
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction Exists for this Agent. So delete the transactions first')", true);

                    }
                }
            }

            LoadTransaction();
            LoadTransaction_Status();
        }
        protected void Edit1(object sender, EventArgs e)
        {
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
                txtEffectiveDate.Text = ds.Tables[0].Rows[0]["EffectiveDate"].ToString();
                 
            }
        }

        private void DML(string flg, int bid)
        {
            DataSet ds = new DataSet();

            string effdt = txtEffectiveDate.Text.Trim();
            effdt = effdt.ToString().Substring(3, 3) + effdt.ToString().Substring(0, 3) + effdt.ToString().Substring(6, 4);


            scode = Convert.ToInt32(bid);
            TransactionNew_BAL lTransaction_BAL = new TransactionNew_BAL();
            lTransaction_BAL.lTransactionNew_CDAL = new TransactionNew_CDAL();

            lTransaction_BAL.lTransactionNew_CDAL.flag = flg;

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

        }

        #region Pagination
        readonly PagedDataSource pgsource = new PagedDataSource();
        int firstIndex, lastIndex;
        private int pageSize = 5;
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["CurrentPage"]);
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        private void HandlePaging()
        {
            var dt = new DataTable();
            dt.Columns.Add("PageIndex"); //Start from 0
            dt.Columns.Add("PageText"); //Start from 1

            firstIndex = CurrentPage - 3;
            if (CurrentPage > 3)
                lastIndex = CurrentPage + 3;
            else
                lastIndex = 6;
            // Check last page is greater than total page then reduced it 
            // to total no. of page is last index
            if (lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                firstIndex = lastIndex - 6;
            }
            if (firstIndex < 0)
                firstIndex = 0;
            // Now creating page number based on above first and last page index
            for (var i = firstIndex; i < lastIndex; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }
            rptPaging.DataSource = dt;
            rptPaging.DataBind();
        }
        private void BindDataIntoRepeater()
        {
            LoadTransaction();
        }
        protected void lbFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataIntoRepeater();
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindDataIntoRepeater();
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindDataIntoRepeater();
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindDataIntoRepeater();
        }
        protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindDataIntoRepeater();
        }
        protected void rptPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            lnkPage.Enabled = false;
            lnkPage.BackColor = Color.FromName("#60bb46");
        }
        #endregion

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
                    dt.Columns.Remove("Agent_ProcessingDate");
                    dt.Columns.Remove("From_Agent");
                    dt.Columns.Remove("From_Currency");
                    dt.Columns.Remove("FromAgent_CR_DR");
                    dt.Columns.Remove("CreditUnit");
                    dt.Columns.Remove("DebitUnit");  				
                    dt.Columns.Remove("Trxn_Amt");
                    dt.Columns.Remove("convertedTrxn_Amt");
                    dt.Columns.Remove("ToAgentCurrency");
                    dt.Columns.Remove("IsValid");
                    dt.Columns.Remove("IsProcessed");


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