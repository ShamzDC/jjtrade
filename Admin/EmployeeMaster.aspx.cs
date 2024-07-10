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

    public partial class EmployeeMaster : System.Web.UI.Page
    {
        int scode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 
                if (Session["IsAdmin"].ToString() == "1")
                {
                    lblUser.Text = Session["UserName"].ToString();
                    pnlGrid.Visible = true;
                    pnlControl.Visible = false;
                    LoadData("SelectSavedEmployee");
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Login to View this Page');window.location='/Admin/Index.aspx';</script>");
                }

                 
            }
        }

        private void LoadData(string flagname)
        {
            try
            {
                DataSet ds = new DataSet();
                EmployeeMaster_BAL lEmployeeMaster_BAL = new EmployeeMaster_BAL();
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL = new EmployeeMaster_CDAL();
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.flag = flagname;
                ds = lEmployeeMaster_BAL.GetEmployeeMaster();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (flagname == "SelectSavedEmployee")
                        {
                            ViewState["dtdata"] = ds.Tables[0];
                            GridEmployee.DataSource = ds.Tables[0];
                            GridEmployee.DataBind();
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
                    LoadData("SelectSavedEmployee");
                }
                else
                {
                    DML("Update", Convert.ToInt32(hdnEmpId.Value));
                    pnlGrid.Visible = true;
                    pnlControl.Visible = false;
                    LoadData("SelectSavedEmployee");
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
            EmployeeMaster_BAL lEmployeeMaster_BAL = new EmployeeMaster_BAL();
            scode = Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString());
            lEmployeeMaster_BAL.lEmployeeMaster_CDAL = new EmployeeMaster_CDAL();
            lEmployeeMaster_BAL.lEmployeeMaster_CDAL.Empid = scode;
            lEmployeeMaster_BAL.lEmployeeMaster_CDAL.flag = "SelectSavedEmployee_ID";

            ds = lEmployeeMaster_BAL.GetEmployeeMaster();
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnSave.InnerHtml = "<i class='fa fa-arrow-up' aria-hidden='true'></i> Update";
                hdnEmpId.Value = Convert.ToString(ds.Tables[0].Rows[0]["EmpID"].ToString());
                txtEmpname.Text = ds.Tables[0].Rows[0]["Empname"].ToString();
                txtAddressLine1.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                txtAddressLine1.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                txtAddressLine1.Text = ds.Tables[0].Rows[0]["AddressLine3"].ToString();
                txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                txtDOH.Text = ds.Tables[0].Rows[0]["DOH"].ToString();
                txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                txtState.Text = ds.Tables[0].Rows[0]["State"].ToString();
                txtCountry.Text = ds.Tables[0].Rows[0]["Country"].ToString();
                txtPincode.Text = ds.Tables[0].Rows[0]["Pincode"].ToString();
                txtPrimaryContactNumber.Text = ds.Tables[0].Rows[0]["PrimaryContactNumber"].ToString();
                txtSecondaryContactNumber.Text = ds.Tables[0].Rows[0]["SecondaryContactNumber"].ToString();
                txtEmpCompanyEmailID.Text = ds.Tables[0].Rows[0]["EmpCompanyEmailID"].ToString();
                txtEmpPersonalEmailID.Text = ds.Tables[0].Rows[0]["EmpPersonalEmailID"].ToString();
                txtUsername.Text = ds.Tables[0].Rows[0]["Username"].ToString();
                txtPwd.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                if (ds.Tables[0].Rows[0]["IsAdmin"].ToString()=="Y")
                {
                    RD_IsAdmin.SelectedIndex = 0;
                }
                else
                {
                    RD_IsAdmin.SelectedIndex = 1;
                }
                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "Y")
                {
                    RD_IsActive.SelectedIndex = 0;
                }
                else
                {
                    RD_IsActive.SelectedIndex = 1;
                }
            }
        }

        protected void Delete(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            DML("Delete", Convert.ToInt32(lbtn.Attributes["EventArgument"].ToString()));
            LoadData("SelectSavedEmployee");
        }

        private void DML(string flg, int bid)
        {
            int se;
            DataSet ds = new DataSet();
            scode = Convert.ToInt32(bid); 
            EmployeeMaster_BAL lEmployeeMaster_BAL = new EmployeeMaster_BAL();            
            lEmployeeMaster_BAL.lEmployeeMaster_CDAL = new EmployeeMaster_CDAL();
            lEmployeeMaster_BAL.lEmployeeMaster_CDAL.Empid = scode;
            if (flg != "Delete")
            {
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.Empname = txtEmpname.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.AddressLine1 = txtAddressLine1.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.AddressLine2 = txtAddressLine2.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.AddressLine3 = txtAddressLine3.Text;

                string effdt = txtDOB.Text.Trim();
                effdt = effdt.ToString().Substring(3, 3) + effdt.ToString().Substring(0, 3) + effdt.ToString().Substring(6, 4);
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.DOB = effdt;

                effdt = txtDOH.Text.Trim();
                effdt = effdt.ToString().Substring(3, 3) + effdt.ToString().Substring(0, 3) + effdt.ToString().Substring(6, 4);
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.DOH = effdt;

                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.City = txtCity.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.State = txtState.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.Country = txtCountry.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.Pincode = txtPincode.Text;

                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.PrimaryContactNumber = txtPrimaryContactNumber.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.SecondaryContactNumber = txtSecondaryContactNumber.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.EmpCompanyEmailID = txtEmpCompanyEmailID.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.EmpPersonalEmailID = txtEmpPersonalEmailID.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.Isactive = RD_IsActive.SelectedValue.ToString();
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.UserName = txtUsername.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.Pwd = txtPwd.Text;
                lEmployeeMaster_BAL.lEmployeeMaster_CDAL.IsAdmin = RD_IsAdmin.SelectedValue.ToString();
            }
            

            lEmployeeMaster_BAL.lEmployeeMaster_CDAL.flag = flg;
            se = lEmployeeMaster_BAL.InsUptDel(flg);
            if (se > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + flg + "Successfully')", true);
                LoadData("SelectSavedEmployee");
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
            LoadData("SelectSavedEmployee");
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            
            ExportExcel( "EmployeeMaster");
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