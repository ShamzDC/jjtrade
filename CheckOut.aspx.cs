using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DB_Utility;
using CDAL;
using BAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Text;
using System.IO;
using DAL;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using System.Drawing;

namespace Client
{
    public partial class CheckOut : System.Web.UI.Page
    {
        List<AgentMaster_CDAL> listCountry_CDAL = new List<AgentMaster_CDAL>();
        List<State_CDAL> listState_CDAL = new List<State_CDAL>();
        List<District_CDAL> listDistrict_CDAL = new List<District_CDAL>();
        List<City_CDAL> listCity_CDAL = new List<City_CDAL>();
        List<Location_CDAL> listLocation_CDAL = new List<Location_CDAL>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Ckm"] != null && Request.QueryString["Ckm"] == "Buy" && Request.QueryString["I"] != null)
                {
                    string Item = Request.QueryString["I"].ToString().Replace("-", " ");
                    string query = "select ItemID from tbl_ItemMaster where ItemName='" + Item + "'";
                    DataSet ds = DB_Con.GetDS(query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int ItmID = Convert.ToInt32(ds.Tables[0].Rows[0]["ItemID"].ToString());
                        ViewState["ItemId"] = ItmID;
                    }
                }
                if (Request.QueryString["Ckm"] != null && Request.QueryString["Ckm"] == "AddCart" && Request.QueryString["I"] != null)
                {
                    string Item = Request.QueryString["I"].ToString().Replace("-", " ");
                    string DecreptItem = DecreptQuerystring(Item);
                    int ItmID = Convert.ToInt32(DecreptItem);
                    ViewState["ItemId"] = ItmID;
                }
                if (Session["U_Name"] != null)
                {
                    collapse_payment_address.Attributes.Add("class", "panel-collapse collapse in");
                    collapse_payment_address.Attributes.Add("aria-expanded", "true");
                    collapse_checkout_option.Attributes.Add("class", collapse_checkout_option.Attributes["class"].ToString().Replace("panel-collapse collapse in", "panel-collapse collapse"));
                    collapse_checkout_option.Attributes.Add("aria-expanded", "false");
                    LoadSelectedEventChange();
                }
                //address_existing.Visible = false;
                //address_new.Visible = true;

                if (Session["Type"].ToString() == "booking")
                {
                    DivSchAddr.Visible = true;
                    DivOrderNowAddr.Visible = false;
                }
                else
                {
                    DivSchAddr.Visible = false;
                    DivOrderNowAddr.Visible = true;
                }

                LoadCountry();
                if (Request.QueryString["UI"] != null)
                {
                    string TempUserId = Request.QueryString["UI"].ToString();
                    BindAddress(TempUserId);
                }
            }
        }

        private string DecreptQuerystring(string DecreptValue)
        {
            Encryptdecrypt_BAL lEncryptdecrypt_BAL = new Encryptdecrypt_BAL();
            string value = lEncryptdecrypt_BAL.Decrypt(HttpUtility.UrlDecode(DecreptValue));
            return value;
        }
        private void LoadCountry()
        {
            listCountry_CDAL = new List<AgentMaster_CDAL>();
            AgentMaster_BAL lCountry_BAL = new AgentMaster_BAL();
            listCountry_CDAL = lCountry_BAL.GetCountry();
            ddlCountry.DataSource = listCountry_CDAL;
            ddlCountry.DataValueField = "CountryID";
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("<--Select Country-->", "0"));
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedIndex > 0)
            {
                if (ddlCountry.SelectedItem.Text == "India")
                {
                    if (Session["Type"].ToString() == "booking")
                    {
                        divdistrict.Visible = true;
                    }
                    lbltext.Text = "";
                    btncontinueaddress.Attributes.Remove("disabled");
                }
                else
                {
                    lbltext.Text = @"Sales temporarily not avaiable for outside India If you need any further information Give us a call at +91 999990000 or email us at kingo@gmail.com";
                    lbltext.ForeColor = System.Drawing.Color.Red;
                    //btncontinueaddress.Visible = false;
                    btncontinueaddress.Attributes.Add("Disabled", "Disabled");
                    divdistrict.Visible = false;
                }
            }
            Loadstate();
        }


       
        private void Loadstate()
        {
            listState_CDAL = new List<State_CDAL>();
            State_BAL lState_BAL = new State_BAL();
            lState_BAL.lState_CDAL = new State_CDAL();
            lState_BAL.lState_CDAL.CountryID = Convert.ToInt32(ddlCountry.SelectedItem.Value);
            listState_CDAL = lState_BAL.GetState();
            ddlState.DataSource = listState_CDAL;
            ddlState.DataValueField = "StateID";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("<--Select State-->", "0"));
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistrict();
        }
        private void LoadDistrict()
        {
            if (Session["Type"].ToString() == "booking")
            {
            listDistrict_CDAL = new List<District_CDAL>();
            District_BAL lDistrict_BAL = new District_BAL();
            lDistrict_BAL.lDistrict_CDAL = new District_CDAL();
            lDistrict_BAL.lDistrict_CDAL.StateID = Convert.ToInt32(ddlState.SelectedItem.Value);
            listDistrict_CDAL = lDistrict_BAL.GetDistrict();
            ddlDistrict.DataSource = listDistrict_CDAL;
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("<--Select District-->", "0"));
            }
            else
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Insert(0, new ListItem("<--Select District-->", "0"));
            }
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLocation();
        }
        private void LoadLocation()
        {

            Location_BAL ILocation_BAL = new Location_BAL();
            ILocation_BAL.lLocation_CDAL = new Location_CDAL();
            ILocation_BAL.lLocation_CDAL.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            listLocation_CDAL = ILocation_BAL.GetLocation();

            ddlLocation.DataSource = listLocation_CDAL;
            ddlLocation.DataValueField = "LocationID";
            ddlLocation.DataTextField = "LocationName";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("<--Select Location-->", "0"));

        }

        private void LoadPaymentMode()
        {
            List<PaymentMode_CDAL> listPaymentMode_CDAL = new List<PaymentMode_CDAL>();
            listPaymentMode_CDAL = new List<PaymentMode_CDAL>();
            PaymentMode_BAL lPaymentMode_BAL = new PaymentMode_BAL();
            listPaymentMode_CDAL = lPaymentMode_BAL.GetPaymentMode();
            rdoPaymentType.DataSource = listPaymentMode_CDAL;
            rdoPaymentType.DataValueField = "PaymentID";
            rdoPaymentType.DataTextField = "PaymentType";
            rdoPaymentType.DataBind();
            if (lblPayment.Text.Trim() == "Cash and Delivery Not Available Your Location")
            {
                rdoPaymentType.Items[0].Enabled = false;
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblloginmsg.Text = "";
                DataTable dt = new DataTable();
                string query = @"select UserID,UserName,ContactNo,EmailID,Address1 from tbl_User where ContactNo='" + txtMobile.Text.Trim() + "'";
                DataSet ds = DB_Con.GetDS(query);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                        Session["U_Name"] = dt.Rows[0]["EmailID"];
                        Session["UserID"] = dt.Rows[0]["UserID"];
                        Session["Address1"] = dt.Rows[0]["Address1"];
                        ((Label)Master.FindControl("lblUsername")).Text = "Welcome " + Session["U_Name"].ToString() + "";
                        HtmlAnchor a1 = (HtmlAnchor)Master.FindControl("A1");
                        a1.Visible = false;
                        HtmlAnchor a2 = (HtmlAnchor)Master.FindControl("A2");
                        a2.InnerHtml = "<i class='fa fa-bell-o' aria-hidden='true'></i> Logout";
                        HtmlAnchor a3 = (HtmlAnchor)Master.FindControl("A3");
                        a3.Visible = true;
                        //Response.Redirect(Request.RawUrl);
                        collapse_payment_address.Attributes.Add("class", "panel-collapse collapse in");
                        collapse_payment_address.Attributes.Add("aria-expanded", "true");
                        collapse_checkout_option.Attributes.Add("class", collapse_checkout_option.Attributes["class"].ToString().Replace("panel-collapse collapse in", "panel-collapse collapse"));
                        collapse_checkout_option.Attributes.Add("aria-expanded", "false");
                       
                        LoadSelectedEventChange();
                      
                }
                else
                {
                    lblloginmsg.Text = "Invalid Credentials";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void BindAddress(string TempUserId)
        {
            if (TempUserId == "0")
            {
                address_existing.Visible = true;
                address_new.Visible = false;
                Loaduserdetails();
            }
            else
            {
                LoadBindAddress(TempUserId);
            }
        }
        private void LoadBindAddress(string TempUserId)
        {
            DataTable dt = new DataTable();
            string query = "";
            if (TempUserId == "DefaultAddr" && Session["UserID"] != null)
            {
                query = @"select Fname,ContactNo,Address1,Country_CountryID as Country,State_StateID as State,District_DistrictID as District,City_CityID as City,
PINCode,Landmark,UserID, LocationID as location from tbl_User where UserID=" + Session["UserID"] + "";
            }
            else
            {
                query = @"select Fname,ContactNo,Address1,Country,State,District,City,PINCode,Landmark,UserTempId,User_UserID, location from tbl_UserTemp 
where UserTempId=" + TempUserId + "";
            }
            DataSet ds = DB_Con.GetDS(query);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                LoadCountry();
                Loadstate();
                LoadDistrict();
                LoadLocation();
                txtFullName.Text = dt.Rows[0]["Fname"].ToString();
                txtMobileNo.Text = dt.Rows[0]["ContactNo"].ToString();
                txtAddress1.Text = dt.Rows[0]["Address1"].ToString();
                if (TempUserId == "DefaultAddr")
                {
                    ddlCountry.ClearSelection();
                    ddlCountry.Items.FindByValue(dt.Rows[0]["Country"].ToString()).Selected = true;
                    ddlState.ClearSelection();
                    ddlState.Items.FindByValue(dt.Rows[0]["State"].ToString()).Selected = true;
                    if (ddlCountry.SelectedItem.Text == "India")
                    {
                         

                        divdistrict.Visible = true;
                        if (Session["Type"].ToString() == "booking")
                        {
                            ddlDistrict.ClearSelection();
                            ddlDistrict.Items.FindByValue(dt.Rows[0]["District"].ToString()).Selected = true;

                            //added location on 6/19/2021

                            ddlLocation.ClearSelection();
                            ddlLocation.Items.FindByValue(dt.Rows[0]["Location"].ToString()).Selected = true;
                            
                        }
                        else
                        {
                            txtCity.Text = dt.Rows[0]["City"].ToString();
                        
                        }

                    }
                    else
                    {
                        divdistrict.Visible = false;
                    }
                }
                else
                {
                    ddlCountry.ClearSelection();
                    ddlCountry.Items.FindByText(dt.Rows[0]["Country"].ToString()).Selected = true;
                    ddlState.ClearSelection();
                    ddlState.Items.FindByText(dt.Rows[0]["State"].ToString()).Selected = true;
                    if (ddlCountry.SelectedItem.Text == "India")
                    {
                        divdistrict.Visible = true;
                        ddlDistrict.ClearSelection();
                        ddlDistrict.Items.FindByText(dt.Rows[0]["District"].ToString()).Selected = true;

                        //added location on 6/19/2021
                        ddlLocation.ClearSelection();
                       ddlLocation.Items.FindByValue(dt.Rows[0]["Location"].ToString()).Selected = true;

                    }
                    else
                    {
                        divdistrict.Visible = false;
                    }
                }
                //txtLocation.Text = dt.Rows[0]["LocationID"].ToString();
                txtPostCode.Text = dt.Rows[0]["PINCode"].ToString();
                txtLandmark.Text = dt.Rows[0]["Landmark"].ToString();
            }
        }
        protected void btnPrevAddress_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["U_Name"] == null)
                {
                    collapse_checkout_option.Attributes.Add("class", "panel-collapse collapse in");
                    collapse_checkout_option.Attributes.Add("aria-expanded", "true");
                    collapse_payment_address.Attributes.Add("class", collapse_payment_address.Attributes["class"].ToString().Replace("panel-collapse collapse in", "panel-collapse collapse"));
                    collapse_payment_address.Attributes.Add("aria-expanded", "false");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadSelectedEventChange()
        {
            if( Session["Address1"] != null)
            {
                lbladdmsg.Text = "";
                Loaduserdetails();
            }
            else
            {
                lbladdmsg.Text = "";
                ViewState["Edit"] = null;
                address_existing.Visible = false;
                address_new.Visible = true;
            }
        }

        protected void btnEditAddress_Click(object sender, EventArgs e)
        {
            address_existing.Visible = false;
            address_new.Visible = true;
            ViewState["Edit"] = "Edit";
            LoadBindAddress("DefaultAddr");
        }
        protected void btnContinueAddress_Click(object sender, EventArgs e)
        {
            //try
            //{
            int isbookingflag = 0;

                if (Session["Type"].ToString() == "booking")
                {
                    if (ddlDistrict.SelectedIndex > 0 || ddlDistrict.SelectedIndex == 0 )
                    {
                        if (ddlDistrict.SelectedItem.Text != "Chennai")
                        {
                            isbookingflag = 1;
                            lblvalitmsg.Text = "Schedule Booking can deliver to Chennai only";
                        }
                    }
                    else  
                    {
                        
                        {
                            isbookingflag = 1;
                            lblvalitmsg.Text = "Please Click the Edit button and Select the District.";
                        }
                    }
                    
                }
                if (isbookingflag == 0)
                {

                    if (Session["Address1"] != null)
                    {
                        if (txtAddress1.Text.Trim() != "" && ddlCountry.SelectedIndex > 0 && txtPostCode.Text.Trim() != "")
                        {
                            int sm = 0;
                            ViewState["ChkAddresss"] = "User";
                            User_BAL lUser_BAL = new User_BAL();
                            lUser_BAL.lUser_CDAL = new User_CDAL();
                            lUser_BAL.lUser_CDAL.UserID = Convert.ToInt32(Session["UserID"].ToString());
                            lUser_BAL.lUser_CDAL.UserName = Session["U_Name"].ToString();
                            lUser_BAL.lUser_CDAL.Fname = txtFullName.Text;
                            lUser_BAL.lUser_CDAL.ContactNo = txtMobileNo.Text;
                            lUser_BAL.lUser_CDAL.Address1 = txtAddress1.Text.Trim();

                            lUser_BAL.lUser_CDAL.Address2 = txtAddress.Text.Trim();  // on 8/21/2021 by jayanthi

                            lUser_BAL.lUser_CDAL.Country_CountryID = Convert.ToInt32(ddlCountry.SelectedItem.Value);
                            lUser_BAL.lUser_CDAL.State_StateID = Convert.ToInt32(ddlState.SelectedItem.Value);
                            if (ddlCountry.SelectedItem.Text == "India")
                            {
                                lUser_BAL.lUser_CDAL.District_DistrictID = Convert.ToInt32(ddlDistrict.SelectedItem.Value);
                            }
                            else
                            {
                                lUser_BAL.lUser_CDAL.District_DistrictID = 0;
                            }

                            if (Session["Type"].ToString() == "ordernow")
                            {
                                lUser_BAL.lUser_CDAL.City_CityID = txtCity.Text;
                            }
                            else
                            {
                                if (ddlCountry.SelectedItem.Text == "India")
                                {
                                    lUser_BAL.lUser_CDAL.District_DistrictID = Convert.ToInt32(ddlDistrict.SelectedItem.Value);
                                }
                                else
                                {
                                    lUser_BAL.lUser_CDAL.District_DistrictID = 0;
                                }
                                lUser_BAL.lUser_CDAL.Location_locationID = Convert.ToInt32(ddlLocation.SelectedItem.Value);
                            }

                          
                            lUser_BAL.lUser_CDAL.PINCode = Convert.ToInt32(txtPostCode.Text.Trim());
                            if (txtLandmark.Text.Trim() != "")
                            {
                                lUser_BAL.lUser_CDAL.Landmark = txtLandmark.Text;
                            }

                            sm = lUser_BAL.Ins_Upd_Del("UpdateAddress");
                        }
                        else
                        {
                           
                        }
                    }

                    else
                    {
                        if (txtAddress1.Text.Trim() != "" && ddlCountry.SelectedIndex > 0 && txtPostCode.Text.Trim() != "" && ViewState["Edit"] != null
                            && Session["UserID"] != null)
                        {
                            int sm = 0;
                            ViewState["ChkAddresss"] = "User";
                            User_BAL lUser_BAL = new User_BAL();
                            lUser_BAL.lUser_CDAL = new User_CDAL();
                            lUser_BAL.lUser_CDAL.UserID = Convert.ToInt32(Session["UserID"].ToString());
                            lUser_BAL.lUser_CDAL.UserName = Session["U_Name"].ToString();
                            lUser_BAL.lUser_CDAL.Fname = txtFullName.Text;
                            lUser_BAL.lUser_CDAL.ContactNo = txtMobileNo.Text;
                            lUser_BAL.lUser_CDAL.Address1 = txtAddress1.Text.Trim();
                            
                            lUser_BAL.lUser_CDAL.Address2 = txtAddress.Text.Trim();  // on 8/21/2021 by jayanthi

                            lUser_BAL.lUser_CDAL.Country_CountryID = Convert.ToInt32(ddlCountry.SelectedItem.Value);
                            lUser_BAL.lUser_CDAL.State_StateID = Convert.ToInt32(ddlState.SelectedItem.Value);

                            if (Session["Type"].ToString() == "ordernow")
                            {
                                lUser_BAL.lUser_CDAL.City_CityID = txtCity.Text;
                            }
                            else
                            {
                                if (ddlCountry.SelectedItem.Text == "India")
                                {
                                    lUser_BAL.lUser_CDAL.District_DistrictID = Convert.ToInt32(ddlDistrict.SelectedItem.Value);
                                }
                                else
                                {
                                    lUser_BAL.lUser_CDAL.District_DistrictID = 0;
                                }
                                lUser_BAL.lUser_CDAL.Location_locationID = Convert.ToInt32(ddlLocation.SelectedItem.Value);
                            }


                            

                            lUser_BAL.lUser_CDAL.PINCode = Convert.ToInt32(txtPostCode.Text.Trim());
                            if (txtLandmark.Text.Trim() != "")
                            {
                                lUser_BAL.lUser_CDAL.Landmark = txtLandmark.Text;
                            }
                            sm = lUser_BAL.Ins_Upd_Del("UpdateAddress");
                            if (sm > 0)
                            {
                                Loaduserdetails();
                            }
                        }
                        collapse_checkout_confirm.Attributes.Add("class", "panel-collapse collapse in");
                        collapse_checkout_confirm.Attributes.Add("aria-expanded", "true");
                        collapse_payment_address.Attributes.Add("class", collapse_payment_address.Attributes["class"].ToString().Replace("panel-collapse collapse in", "panel-collapse collapse"));
                        collapse_payment_address.Attributes.Add("aria-expanded", "false");
                        //Loaduserdetails();

                    }
                    if (Request.QueryString["Ckm"] != null)
                    {
                        if (Request.QueryString["Ckm"].ToString() == "Buy")
                        {
                            int ItmID = 0;
                            if (ViewState["ItemId"] != null)
                            {
                                ItmID = Convert.ToInt32(ViewState["ItemId"].ToString());
                            }
                            string qty = Request.QueryString["Q"].ToString();
                            loadcheckoutgrid(qty, ItmID, "Address");
                            collapse_checkout_confirm.Attributes.Add("class", "panel-collapse collapse in");
                            collapse_checkout_confirm.Attributes.Add("aria-expanded", "true");
                            collapse_payment_address.Attributes.Add("class", collapse_payment_address.Attributes["class"].ToString().Replace("panel-collapse collapse in", "panel-collapse collapse"));
                            collapse_payment_address.Attributes.Add("aria-expanded", "false");
                        }
                        else if (Request.QueryString["Ckm"].ToString() == "AddCart")
                        {
                            FillCartDatatoGrid();
                            collapse_checkout_confirm.Attributes.Add("class", "panel-collapse collapse in");
                            collapse_checkout_confirm.Attributes.Add("aria-expanded", "true");
                            collapse_payment_address.Attributes.Add("class", collapse_payment_address.Attributes["class"].ToString().Replace("panel-collapse collapse in", "panel-collapse collapse"));
                            collapse_payment_address.Attributes.Add("aria-expanded", "false");
                        }
                    }
                }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void Loaduserdetails()
        {
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                //if (Session["Address1"] == ViewState["address"])
                //{
                ViewState["address"] = null;
                address_existing.Visible = true;
                address_new.Visible = false;
                string query = "";
                query = @"select CountryName from tbl_User a,tbl_Country b where a.Country_CountryID=b.CountryID 
and a.UserID=" + Session["UserID"] + "";
                DataSet ds1 = DB_Con.GetDS(query);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (Session["Type"].ToString() == "booking")
                    {
//                        query = @" select a.Fname,isnull(a.Fname,'')+','+Address1 +','+b.DistrictName +','+ a.City_CityID +','+d.StateName+','+ 
//cast(a.PINCode as varchar)+','+e.CountryName+'.'+isnull('Landmark'+'-'+a.Landmark,'') as Address,d.StateName,a.UserID,a.ContactNo,a.EmailID,e.CountryName,e.CountryCode,
//a.PINCode, b.DistrictName,l.LocationName, d.stateid, l.LocationId, b.DistrictID,e.CountryID from tbl_user a ,tbl_District b, tbl_State d, tbl_Country e , tbl_location l where a.District_DistrictID=b.DistrictID and a.State_StateID=d.StateID and 
//a.Country_CountryID=e.CountryID and a.LocationID=l.LocationID and a.UserID=" + Session["UserID"] + "";
                        query = @" select a.Fname,isnull(a.Fname,'')+','+isnull(Address1,'') +','+isnull(b.DistrictName,'') +','+ isnull(a.City_CityID,'')
  +','+isnull(d.StateName,'')+','+ 
cast(a.PINCode as varchar)+','+ isnull(e.CountryName,'')+ '.'+isnull('Landmark'+'-'+a.Landmark,'') as Address,d.StateName,a.UserID,a.ContactNo,a.EmailID,e.CountryName,e.CountryCode,
a.PINCode, b.DistrictName,l.LocationName, d.stateid, isnull(l.LocationId,'') LocationId , isnull( b.DistrictID,'') DistrictID ,e.CountryID 
from tbl_user a inner join tbl_Country e on a.Country_CountryID=e.CountryID
inner join  tbl_State d on a.State_StateID=d.StateID
left join  tbl_District b on a.District_DistrictID=b.DistrictID
left join  tbl_location l  on  a.LocationID=l.LocationID where a.UserID=" + Session["UserID"] + "";

                    }
                    else
                    {
                        query = @"select a.Fname,isnull(a.Fname,'')+','+isnull(Address1,'') +','+ isnull(a.City_CityID,'') +','+isnull(d.StateName,'')+','+ 
cast(a.PINCode as varchar)+','+ isnull(e.CountryName,'') +'.'+isnull('Landmark'+'-'+ isnull(a.Landmark,''),'') as Address,d.StateName,a.UserID,a.ContactNo,a.EmailID,e.CountryName,e.CountryCode,
a.PINCode, d.stateid,e.CountryID, isnull(l.LocationId,'') LocationId,l.LocationName from tbl_user a left join tbl_State d on  a.State_StateID=d.StateID left join tbl_Country e on a.Country_CountryID=e.CountryID
left join  tbl_location l  on  a.LocationID=l.LocationID 
where a.UserID=" + Session["UserID"] + "";
                    }
                    DataSet ds = DB_Con.GetDS(query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lbluseraddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        ViewState["address"] = lbluseraddress.Text;
                        ViewState["UserId"] = ds.Tables[0].Rows[0]["UserID"].ToString();
                        txtName.Text = ds.Tables[0].Rows[0]["Fname"].ToString();
                        txtMobileNo1.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                        txtEmail1.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                        //txtAddress.Text = lbluseraddress.Text;
                        txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        hdnpincode.Value = ds.Tables[0].Rows[0]["PINCode"].ToString();

                        //hdncountry.Value = ds.Tables[0].Rows[0]["CountryName"].ToString();
                        //hdncountrycode.Value = ds.Tables[0].Rows[0]["CountryCode"].ToString();
                        //hdnState.Value = ds.Tables[0].Rows[0]["StateName"].ToString();
                        //hdnDistrict.Value = ds.Tables[0].Rows[0]["DistrictName"].ToString();
                        //hdnLocation.Value = ds.Tables[0].Rows[0]["LocationName"].ToString();

                        hdncountry.Value = ds.Tables[0].Rows[0]["CountryName"].ToString();
                        hdncountrycode.Value = ds.Tables[0].Rows[0]["CountryCode"].ToString();
                        hdnState.Value = ds.Tables[0].Rows[0]["stateid"].ToString();

                        // ------------ ********** start adding the below two lines by jayanthi **** ---------//
                        if (ds.Tables[0].Rows[0]["stateid"].ToString() != "" && ds.Tables[0].Rows[0]["stateid"].ToString() != "0")
                        {
                            ddlState.Items.Insert(0, new ListItem(ds.Tables[0].Rows[0]["StateName"].ToString(), ds.Tables[0].Rows[0]["stateid"].ToString()));
                        }
                        if (ds.Tables[0].Rows[0]["LocationId"].ToString() != "" && ds.Tables[0].Rows[0]["LocationId"].ToString() != "0")
                        {
                            ddlLocation.Items.Insert(0, new ListItem(ds.Tables[0].Rows[0]["LocationName"].ToString(), ds.Tables[0].Rows[0]["LocationId"].ToString()));
                            
                        }

                        // ------------ ********** end adding the below two lines by jayanthi **** ---------//



                        if (Session["Type"].ToString() == "booking")
                        {
                            if (ds.Tables[0].Rows[0]["DistrictID"].ToString() != "" && ds.Tables[0].Rows[0]["DistrictID"].ToString() != "0")
                            {
                                hdnDistrict.Value = ds.Tables[0].Rows[0]["DistrictID"].ToString();
                                ddlDistrict.Items.Insert(0, new ListItem(ds.Tables[0].Rows[0]["DistrictName"].ToString(), ds.Tables[0].Rows[0]["DistrictID"].ToString()));
                            }
                            if (ds.Tables[0].Rows[0]["LocationId"].ToString() != "" && ds.Tables[0].Rows[0]["LocationId"].ToString() != "0")
                            {
                                hdnLocation.Value = ds.Tables[0].Rows[0]["LocationId"].ToString();
                            }
                        }

                        hdncountryID.Value = ds.Tables[0].Rows[0]["CountryID"].ToString();

                        hdnUserTempId.Value = "0";

                      



                        // ------------ ********** start adding the below two lines **** ---------//
                    }
                }
                else
                {
                    //rdoaddress.ClearSelection();
                    //rdoaddress.Items.FindByValue("1").Selected = true;
                    address_new.Visible = true;
                    address_existing.Visible = false;
                    //lbladdmsg.Text = "Existing address not found please enter new address";
                    //lbladdmsg.ForeColor = System.Drawing.Color.Red;
                    address_existing.Visible = false;
                    address_new.Visible = true;
                }

            }
            else
                address_existing.Visible = false;
        }

        private void Loaduserdetails_Bck()
        {
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                //if (Session["Address1"] == ViewState["address"])
                //{
                    ViewState["address"] = null;
                    address_existing.Visible = true;
                    address_new.Visible = false;
                    string query = "";
                    query = @"select CountryName from tbl_User a,tbl_Country b where a.Country_CountryID=b.CountryID 
and a.UserID=" + Session["UserID"] + "";
                    DataSet ds1 = DB_Con.GetDS(query);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["CountryName"].ToString() == "India")
                        {
                            query = @" select a.Fname,isnull(a.Fname,'')+','+Address1 +','+b.DistrictName +','+ a.City_CityID +','+d.StateName+','+ 
cast(a.PINCode as varchar)+','+e.CountryName+'.'+isnull('Landmark'+'-'+a.Landmark,'') as Address,d.StateName,a.UserID,a.ContactNo,a.EmailID,e.CountryName,e.CountryCode,
a.PINCode, b.DistrictName,l.LocationName, d.stateid, l.LocationId, b.DistrictID,e.CountryID from tbl_user a ,tbl_District b, tbl_State d, tbl_Country e , tbl_location l where a.District_DistrictID=b.DistrictID and a.State_StateID=d.StateID and 
a.Country_CountryID=e.CountryID and a.LocationID=l.LocationID and a.UserID=" + Session["UserID"] + "";
                        }
                        else
                        {
                            query = @" select a.Fname,isnull(a.Fname,'')+','+Address1+','+ a.City_CityID +','+d.StateName+','+ 
cast(a.PINCode as varchar)+','+e.CountryName+'.'+isnull('Landmark'+'-'+a.Landmark,'') as Address,d.StateName,a.UserID,a.ContactNo,a.EmailID,e.CountryName,e.CountryCode,
a.PINCode, '' DistrictName,'' LocationName,'' as stateid, '' as LocationId,e.CountryID from tbl_user a , tbl_State d, tbl_Country e where a.State_StateID=d.StateID and 
a.Country_CountryID=e.CountryID and a.UserID=" + Session["UserID"] + "";
                        }
                        DataSet ds = DB_Con.GetDS(query);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lbluseraddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                            ViewState["address"] = lbluseraddress.Text;
                            ViewState["UserId"] = ds.Tables[0].Rows[0]["UserID"].ToString();
                            txtName.Text = ds.Tables[0].Rows[0]["Fname"].ToString();
                            txtMobileNo1.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                            txtEmail1.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                            txtAddress.Text = lbluseraddress.Text;
                            hdnpincode.Value = ds.Tables[0].Rows[0]["PINCode"].ToString();

                            //hdncountry.Value = ds.Tables[0].Rows[0]["CountryName"].ToString();
                            //hdncountrycode.Value = ds.Tables[0].Rows[0]["CountryCode"].ToString();
                            //hdnState.Value = ds.Tables[0].Rows[0]["StateName"].ToString();
                            //hdnDistrict.Value = ds.Tables[0].Rows[0]["DistrictName"].ToString();
                            //hdnLocation.Value = ds.Tables[0].Rows[0]["LocationName"].ToString();

                            hdncountry.Value = ds.Tables[0].Rows[0]["CountryName"].ToString();
                            hdncountrycode.Value = ds.Tables[0].Rows[0]["CountryCode"].ToString();
                            hdnState.Value = ds.Tables[0].Rows[0]["stateid"].ToString();
                            hdnDistrict.Value = ds.Tables[0].Rows[0]["DistrictID"].ToString();
                            hdnLocation.Value = ds.Tables[0].Rows[0]["LocationId"].ToString();
                            hdncountryID.Value = ds.Tables[0].Rows[0]["CountryID"].ToString();

                            hdnUserTempId.Value = "0";

                            // ------------ ********** start adding the below two lines by jayanthi **** ---------//

                            ddlDistrict.Items.Insert(0, new ListItem(ds.Tables[0].Rows[0]["DistrictName"].ToString(), ds.Tables[0].Rows[0]["DistrictID"].ToString()));

                           
                            
                            // ------------ ********** start adding the below two lines **** ---------//
                        }
                    }
                    else
                    {
                        //rdoaddress.ClearSelection();
                        //rdoaddress.Items.FindByValue("1").Selected = true;
                        address_new.Visible = true;
                        address_existing.Visible = false;
                        //lbladdmsg.Text = "Existing address not found please enter new address";
                        //lbladdmsg.ForeColor = System.Drawing.Color.Red;
                        address_existing.Visible = false;
                        address_new.Visible = true;
                    }
 
            }
            else
                address_existing.Visible = false;
        }
        private void FillCartDatatoGrid()
        {
            if (Session["dtItem"] != null && Session["U_Name"] != null)
            {
                //loadcheckoutgrid("1", 1, "Address");
                DataTable dtm = (DataTable)Session["dtItem"];
                ViewState["dt"] = dtm;
                grdCheckOut.DataSource = dtm;
                grdCheckOut.DataBind();
                txtDiscount.Text = "0";
                string country = string.Empty;
                string query = @" select CAST(round((isnull(DiscPercentage,0)/100* " + dtm.Compute("SUM(Rate)", "") + "),2)AS DECIMAL(18,2))  from  tbl_DiscountSlabMaster where " + dtm.Compute("SUM(Rate)", "") + " ";
                query = query + " between AmtSlabfrom and AmtSlabTo  and isactive='Y' ;    ";
                query = query + " select a.Country_CountryID,b.CountryName from tbl_User a join tbl_Country b on a.Country_CountryID=b.CountryID where a.UserName='" + Session["U_Name"] + "'";
                DataSet ds1 = DB_Con.GetDS(query);
                if (ds1.Tables[0].Rows.Count > 0 || ds1.Tables[1].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        //txtDiscount.Text = ds1.Tables[0].Rows[0][0].ToString();
                        txtDiscount.Text = "0.0";
                    }
                    if (ds1.Tables[1].Rows.Count > 0)
                    {
                        country = ds1.Tables[1].Rows[0]["CountryName"].ToString();
                    }
                }
                if (dtm.Compute("SUM(Weight)", "") != DBNull.Value)
                {
                    hdnweight.Value = Convert.ToDouble(dtm.Compute("SUM(Weight)", "")).ToString();
                }
                object Totaltaxamt = 0.0;
                if (country == "India")
                {
                    Totaltaxamt = Convert.ToDouble(dtm.Compute("SUM(Tax1Amt)", "")) + Convert.ToDouble(dtm.Compute("SUM(Tax2Amt)", ""));
                }
                else
                {
                    Totaltaxamt = Convert.ToDouble(dtm.Compute("SUM(Tax3Amt)", ""));
                }
                txtTaxAmount.Text = Totaltaxamt.ToString();
                txtTotalamount.Text = ((Convert.ToDouble(dtm.Compute("SUM(Rate)", "")) + Convert.ToDouble(txtTaxAmount.Text)) - Convert.ToDouble(txtDiscount.Text)).ToString();
            }
        }

        protected void btnPrevConfirm_Click(object sender, EventArgs e)
        {
            //if (Session["dtItem"] != null)
            //{
            //    DataTable dth = (DataTable)Session["dtItem"];
            //    if (dth.Rows.Count > 0)
            //    {
                    collapse_payment_address.Attributes.Add("class", "panel-collapse collapse in");
                    collapse_payment_address.Attributes.Add("aria-expanded", "true");
                    collapse_checkout_confirm.Attributes.Add("class", collapse_checkout_confirm.Attributes["class"].ToString().Replace("panel-collapse collapse in", "panel-collapse collapse"));
                    collapse_checkout_confirm.Attributes.Add("aria-expanded", "false");
            //    }
            //    else
            //    {
            //        Response.Redirect("index.aspx");
            //    }
            //}
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdCheckOut.Rows.Count > 0)
                {
                    lblgridmsg.Text = "";
                    collapse_payment_method.Attributes.Add("class", "panel-collapse collapse in");
                    collapse_payment_method.Attributes.Add("aria-expanded", "true");
                    collapse_checkout_confirm.Attributes.Add("class", collapse_checkout_confirm.Attributes["class"].ToString().Replace("panel-collapse collapse in", "panel-collapse collapse"));
                    collapse_checkout_confirm.Attributes.Add("aria-expanded", "false");
                    LoadPaymentMode();
                    Loaduserdetails();
                }
                else
                {
                    lblgridmsg.Text = "No Data Found";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadcheckoutgrid(string qty, int ItmID, string value)
        {
            try
            {
                string SizeID = "0";
                if (Request.QueryString["S"] != null)
                {
                    SizeID = Request.QueryString["S"].ToString();
                }
                DataTable dt = new DataTable();
                DataTable dtSlab = new DataTable();
 

                string query = @"select *,CAST(Round(Price + Tax1Amt + Tax2Amt + Tax3Amt,2) AS  DECIMAL(18,0))as Rate, isnull(" + qty + "*CAST(Round(Price + Tax1Amt + Tax2Amt + Tax3Amt,2) AS  DECIMAL(18,0)),0) as Amount  from (select distinct a.itemid,a.ItemCode,a.ItemName," + qty + "  Quantity ,a.Price, ";
                query = query + " b.Tax1,b.Tax2,b.Tax3 , isnull( a.Price,0)* Tax1/100 as Tax1Amt, ";
                query = query + " isnull(a.Price,0)* Tax2/100 as Tax2Amt,isnull(a.Price,0)* Tax3/100 as Tax3Amt,d.ImageName," + SizeID + " as SizeID, isnull(NetWeight,0.0) as Weight from tbl_ItemMaster a,";
                query = query + " (select ItemID,isnull(min(cgst),0)Tax1,isnull(min(Sgst),0)Tax2, isnull(min(Igst),0)Tax3 from tbl_productmaster group by ItemID ) b, ";
                query = query + "  tbl_ItemImages d where   a.ItemID=" + ItmID + " and a.ItemID=b.ItemID and ";
                query = query + "a.ItemID=d.Item_ItemID and exists (select ItemID from tbl_CumulativeStock where ItemID=a.ItemID and Quantity>0))RA; ";

                


                DataSet ds = DB_Con.GetDS(query);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (value == "Address")
                    {
                        if (Request.QueryString["Ckm"] != null)
                        {
                            if (Request.QueryString["Ckm"].ToString() == "Buy")
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    double weight = Convert.ToDouble(ds.Tables[1].Rows[0]["Weight"].ToString());
                                    int Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"].ToString());
                                    hdnweight.Value = (weight * Quantity).ToString();
                                }
                                ViewState["dt"] = dt;
                                grdCheckOut.DataSource = dt;
                                grdCheckOut.DataBind();
                                Session["dtBuyItem"] = dt;
                                txtDiscount.Text = "0";
                                string country = string.Empty;
                                query = @" select CAST(round((isnull(DiscPercentage,0)/100* " + dt.Rows[0]["Amount"] + "),2)AS DECIMAL(18,2)) from  tbl_DiscountSlabMaster where " + dt.Rows[0]["Amount"] + " ";
                                query = query + " between AmtSlabfrom and AmtSlabTo  and isactive='Y' ;";
                                query = query + " select a.Country_CountryID,b.CountryName from tbl_User a join tbl_Country b on a.Country_CountryID=b.CountryID where a.UserName='" + Session["U_Name"] + "'";
                                DataSet ds1 = DB_Con.GetDS(query);
                                if (ds1.Tables[0].Rows.Count > 0 || ds1.Tables[1].Rows.Count > 0)
                                {
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        //txtDiscount.Text = ds1.Tables[0].Rows[0][0].ToString();
                                        txtDiscount.Text = "0.0";
                                    }
                                    if (ds1.Tables[1].Rows.Count > 0)
                                    {
                                        country = ds1.Tables[1].Rows[0]["CountryName"].ToString();
                                    }
                                }
                                object Totaltaxamt = 0.0;
                                if (country == "India")
                                {
                                    Totaltaxamt = Convert.ToDouble(dt.Rows[0]["Tax1amt"]) + Convert.ToDouble(dt.Rows[0]["Tax2amt"]);
                                }
                                else
                                {
                                    Totaltaxamt = Convert.ToDouble(dt.Rows[0]["Tax1amt"]) + Convert.ToDouble(dt.Rows[0]["Tax2amt"]) + Convert.ToDouble(dt.Rows[0]["Tax3amt"]);
                                }
                                txtTaxAmount.Text = Totaltaxamt.ToString();
                                //object finalamt = Convert.ToDouble(dt.Rows[0]["Rate"]) + Convert.ToDouble(txtTaxAmount.Text) - Convert.ToDouble(txtDiscount.Text);
                                object finalamt = Convert.ToDouble(dt.Rows[0]["Amount"])  - Convert.ToDouble(txtDiscount.Text);
                                txtTotalamount.Text = finalamt.ToString();
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnplus_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlButton btnplus = (sender as HtmlButton);
                int itemid = Convert.ToInt32(btnplus.Attributes["EventArgument"].ToString());
                //int SizeID = Convert.ToInt32(btnplus.Attributes["AlterEventArgument"].ToString());
                GridViewRow gvRow = (GridViewRow)btnplus.Parent.Parent;
                TextBox txtqty = (TextBox)gvRow.FindControl("txtqty");
                Label lblQty = (Label)gvRow.FindControl("lblQty");
                string query = @"select cast(quantity as int)quantity from tbl_CumulativeStock where  ItemID=" + itemid;
                DataSet ds = DB_Con.SelectCommand(query);
                int quantity = 0;
                lblQty.Text = "";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    quantity = Convert.ToInt32(ds.Tables[0].Rows[0]["quantity"].ToString());
                    //ViewState["quantity"] = quantity;
                    //ViewState["itemid"] = itemid;
                    //lblQty.Text = "Available Quantity " + quantity + "";
                    //lblQty.ForeColor = Color.Red;
                }
                if (Convert.ToInt32(txtqty.Text) < quantity)
                {
                    btnplus.Disabled = false;
                    txtqty.Text = (Convert.ToInt32(txtqty.Text) + 1).ToString();
                    ChangeQuantity(itemid, txtqty.Text);
                }
                else
                {
                    btnplus.Disabled = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnminus_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlButton btnminus = (sender as HtmlButton);
                int itemid = Convert.ToInt32(btnminus.Attributes["EventArgument"].ToString());
                //int SizeID = Convert.ToInt32(btnminus.Attributes["AlterEventArgument"].ToString());
                GridViewRow gvRow = (GridViewRow)btnminus.Parent.Parent;
                TextBox txtqty = (TextBox)gvRow.FindControl("txtqty");
                if (txtqty.Text != "1")
                {
                    txtqty.Text = (Convert.ToInt32(txtqty.Text) - 1).ToString();
                    //ViewState["quantity"] = null;
                    ChangeQuantity(itemid, txtqty.Text);
                }
                else
                {
                    btnminus.Disabled = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtqty_Textchanged(object sender, EventArgs e)
        {
            int itemid = 0;
            TextBox txtquantity = ((TextBox)(sender));
            GridViewRow gvRow = (GridViewRow)txtquantity.Parent.Parent;
            TextBox txtqty = (TextBox)gvRow.FindControl("txtqty");
            Label lblQty = (Label)gvRow.FindControl("lblQty");
            itemid = Convert.ToInt32(txtqty.Attributes["EventArgument"].ToString());
            //ChangeQuantity(itemid, txtqty.Text);
            int SizeID = Convert.ToInt32(txtqty.Attributes["AlterEventArgument"].ToString());
            //ChangeQuantity(itemid, txtqty.Text);
            string query = @"select cast(quantity as int)quantity from tbl_CumulativeStock where Size_SizeID=" + SizeID + " and ItemID=" + itemid;
            DataSet ds = DB_Con.SelectCommand(query);
            int quantity = 0;
            lblQty.Text = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                quantity = Convert.ToInt32(ds.Tables[0].Rows[0]["quantity"].ToString());
                //ViewState["itemid"] = itemid;
                //ViewState["quantity"] = quantity;
            }
            if (Convert.ToInt32(txtqty.Text) < quantity)
            {
                ChangeQuantity(itemid, txtqty.Text);
            }
            else
            {
                txtqty.Text = quantity.ToString();
                ChangeQuantity(itemid, txtqty.Text);
            }
        }
        private void ChangeQuantity(int itemid, string quantity)
        {
            if (Request.QueryString["Ckm"].ToString() == "Buy")
            {
                loadcheckoutgrid(quantity, itemid, "Address");
            }
            else if (Session["dtItem"] != null && Session["U_Name"] != null)
            {
                DataTable dtItem = (DataTable)Session["dtItem"];
                DataRow dr = dtItem.Select("itemid = " + itemid + "").FirstOrDefault();
                if (dr != null)
                {
                    dr["Quantity"] = quantity;
                    dr["Amount"] = Convert.ToDouble(dr["Rate"]) * Convert.ToDouble(dr["Quantity"]);
                    dr["Tax1Amt"] = Convert.ToDouble(dr["Quantity"]) * Convert.ToDouble(dr["Price"]) * Convert.ToDouble(dr["Tax1"]) / 100;
                    dr["Tax2Amt"] = Convert.ToDouble(dr["Quantity"]) * Convert.ToDouble(dr["Price"]) * Convert.ToDouble(dr["Tax2"]) / 100;
                    dr["Tax3Amt"] = Convert.ToDouble(dr["Quantity"]) * Convert.ToDouble(dr["Price"]) * Convert.ToDouble(dr["Tax3"]) / 100;
                    if (!string.IsNullOrEmpty(dr["SglWeight"].ToString()))
                    {
                        dr["Weight"] = Convert.ToDouble(quantity) * Convert.ToDouble(dr["SglWeight"]);
                    }
                    string country = string.Empty;
                    string query = @" select CAST(round((isnull(DiscPercentage,0)/100* " + dr["Amount"] + "),2)AS DECIMAL(18,2)),DiscPercentage  from  tbl_DiscountSlabMaster where " + dr["Amount"] + " ";
                    query = query + " between AmtSlabfrom and AmtSlabTo  and isactive='Y' ;    ";
                    query = query + " select a.Country_CountryID,b.CountryName from tbl_User a join tbl_Country b on a.Country_CountryID=b.CountryID where a.UserName='" + Session["U_Name"] + "'";
                    DataSet ds1 = DB_Con.GetDS(query);
                    if (ds1.Tables[0].Rows.Count > 0 || ds1.Tables[1].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            //dr["DiscAmt"] = ds1.Tables[0].Rows[0][0].ToString();
                            dr["DiscAmt"] = "0.0";
                        }
                        if (ds1.Tables[1].Rows.Count > 0)
                        {
                            country = ds1.Tables[1].Rows[0]["CountryName"].ToString();
                        }
                    }
                    if (country != "India")
                    {
                       // dr["TotAmount"] = Convert.ToDouble(dr["Amount"]) + Convert.ToDouble(dr["Tax3Amt"]) - Convert.ToDouble(dr["DiscAmt"]);
                        dr["TotAmount"] = Convert.ToDouble(dr["Amount"])  - Convert.ToDouble(dr["DiscAmt"]);
                    }
                    else
                    {
                        //dr["TotAmount"] = Convert.ToDouble(dr["Amount"]) + Convert.ToDouble(dr["Tax1Amt"]) + Convert.ToDouble(dr["Tax2Amt"]) - Convert.ToDouble(dr["DiscAmt"]);
                        dr["TotAmount"] = Convert.ToDouble(dr["Amount"])  - Convert.ToDouble(dr["DiscAmt"]);
                    }
                }
                Session["dtItem"] = dtItem;
                FillCartDatatoGrid();
            }
        }
        protected void grdCheckOut_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblQty = (Label)e.Row.FindControl("lblQty");
                Label Price = (Label)e.Row.FindControl("Price");
                Label Amount = (Label)e.Row.FindControl("Amount");
                Label lblSize = (Label)e.Row.FindControl("lblSize");
                HtmlButton btnplus = (HtmlButton)e.Row.FindControl("incr");
                string itemid = btnplus.Attributes["EventArgument"].ToString();
                string SizeID = btnplus.Attributes["AlterEventArgument"].ToString();
                string query = @"select cast(quantity as int)quantity from tbl_CumulativeStock where Size_SizeID=" + SizeID + " and ItemID=" + itemid + ";";
                query = query + "select SizeID,SizeDescription from tbl_Size where SizeID=" + SizeID + ";";
                DataSet ds = DB_Con.SelectCommand(query);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblQty.Text = "Available Quantity " + ds.Tables[0].Rows[0]["quantity"].ToString() + "";
                    lblQty.ForeColor = Color.Red;
                }
                else
                {
                    lblQty.Text = "";
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    lblSize.Text = ds.Tables[1].Rows[0]["SizeDescription"].ToString();
                }
                else
                {
                    lblSize.Text = "";
                }
                if (!string.IsNullOrEmpty(hdncountry.Value))
                {
                    if (hdncountry.Value != "India")
                    {
                        Currency_BAL lCurrency_BAL = new Currency_BAL();
                        string Dollerprice = lCurrency_BAL.GetDollerPrice(Price.Text);
                        string DollerAmount = lCurrency_BAL.GetDollerPrice(Amount.Text);
                        Price.Text = "$ " + Dollerprice;
                        Amount.Text = "$ " + DollerAmount;
                    }
                    else
                    {
                        Price.Text = "<i class='fa fa-inr'></i> " + Price.Text;
                        Amount.Text = "<i class='fa fa-inr'></i> " + Amount.Text;
                    }
                }
            }
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ViewState["dt"] != null)
            {
                DataTable dt = ViewState["dt"] as DataTable;
                if (Session["UserID"] != null)
                {
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_Spicy"].ConnectionString);
                    conn.Open();
                    string Query = @"delete from tbl_TempSession where userid=" + Session["UserID"].ToString() + " and ";
                    Query = Query + "itemid=" + dt.Rows[e.RowIndex]["itemid"] + " and SizeID=" + dt.Rows[e.RowIndex]["SizeID"] + "";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                }
                dt.Rows.RemoveAt(e.RowIndex);
                Session["dtItem"] = dt;
                grdCheckOut.DataSource = dt;
                grdCheckOut.DataBind();
                if (dt.Rows.Count > 0)
                {
                    ((Label)Master.FindControl("lblitemcount")).Text = dt.Rows.Count.ToString();
                    btnConfirm.Attributes.Remove("disabled");
                }
                else
                {

                    ((Label)Master.FindControl("lblitemcount")).Text = "0";
                    btnConfirm.Attributes.Add("disabled", "disabled");
                }
            }
        }
        protected void btnPrevPayment_Click(object sender, EventArgs e)
        {
            collapse_checkout_confirm.Attributes.Add("class", "panel-collapse collapse in");
            collapse_checkout_confirm.Attributes.Add("aria-expanded", "true");
            collapse_payment_method.Attributes.Add("class", collapse_payment_method.Attributes["class"].ToString().Replace("panel-collapse collapse in", "panel-collapse collapse"));
            collapse_payment_method.Attributes.Add("aria-expanded", "false");
        }
        protected void btnPreContinuePay_Click(object sender, EventArgs e)
        {
            if (rdoPaymentType.SelectedIndex >= 0 && chkagree.Checked == true)
            {
                string cod = "";
                if (rdoPaymentType.SelectedItem.Text == "Cash On Delivery")
                {
                    cod = "1";
                }
                else
                {
                    cod = "0";
                }
                lblcheck.Text = "";
                //string[] values = txtAddress.Text.Split(',');
                //values = values.Where((s, i) => i % 2 == 0).Zip(values.Where((s, i) => i % 2 == 1), (a, b) => a + ", " + b).ToArray();
                //List<User_DAL> listUser_DAL = new List<User_DAL>();
                //for (int i = 0; i < values.Length; i++)
                //{
                //    User_DAL lUser_DAL = new User_DAL();
                //    lUser_DAL.Address1 = values[i];
                //    listUser_DAL.Add(lUser_DAL);
                //}                
                string Ckm = "";
                string itemsize = "";
                if (Request.QueryString["Ckm"] != null)
                {
                    if (Request.QueryString["Ckm"].ToString() == "Buy")
                    {
                        Ckm = "Buy";
                        itemsize = Request.QueryString["S"].ToString();
                    }
                    else if (Request.QueryString["Ckm"].ToString() == "AddCart")
                    {
                        Ckm = "AddCart";
                        itemsize = "0";
                    }
                    if (hdnweight.Value == "")
                    {
                        hdnweight.Value = "0.5";
                    }


                    //Encryptdecrypt_BAL lEncryptdecrypt_BAL = new Encryptdecrypt_BAL();
                    //string Encreptcod = HttpUtility.UrlDecode(lEncryptdecrypt_BAL.Encrypt(cod));
                    //string Encreptweight = HttpUtility.UrlDecode(lEncryptdecrypt_BAL.Encrypt(hdnweight.Value));
                    //string Encreptpincode = HttpUtility.UrlDecode(lEncryptdecrypt_BAL.Encrypt(hdnpincode.Value));
                    //string EncreptPaymentType = HttpUtility.UrlDecode(lEncryptdecrypt_BAL.Encrypt(rdoPaymentType.SelectedValue));
                    //string EncreptCountryCode = HttpUtility.UrlDecode(lEncryptdecrypt_BAL.Encrypt(hdncountrycode.Value));
                    //string EncreptState = HttpUtility.UrlDecode(lEncryptdecrypt_BAL.Encrypt(hdnState.Value));

                    //string EncreptDistrict = HttpUtility.UrlDecode(lEncryptdecrypt_BAL.Encrypt(hdnDistrict.Value));
                    //string EncreptLocation = HttpUtility.UrlDecode(lEncryptdecrypt_BAL.Encrypt(hdnLocation.Value));

                    //string Url = "/PlaceOrder.aspx?C=" + Encreptcod + "&W=" + Encreptweight + "&PC=" + Encreptpincode + "&PM=" + EncreptPaymentType + "";
                    //Url = Url + "&Ckm=" + Ckm + "&S=" + itemsize + "&CC=" + EncreptCountryCode + "&UI=" + hdnUserTempId.Value + "&SN=" + EncreptState + " &Dis=" + EncreptDistrict + " &Loc=" + EncreptLocation + " ";
                   
                    //Response.Redirect("" + Url + "");


                    string Url = "/PlaceOrder.aspx?C=" + cod + "&W=" + hdnweight.Value + "&PC=" + hdnpincode.Value + "&PM=" + rdoPaymentType.SelectedValue + "";
                    Url = Url + "&Ckm=" + Ckm + "&S=" + itemsize + "&CC=" + hdncountrycode.Value + "&UI=" + hdnUserTempId.Value + "&SN=" + hdnState.Value + " &Dis=" + hdnDistrict.Value + " &Loc=" + hdnLocation.Value + " &CID=" + hdncountryID.Value + " &CntName=" + hdncountry.Value + "  ";

                    Response.Redirect("" + Url + "");

                }
            }
            else
            {
                if (chkagree.Checked == false)
                {
                    lblcheck.Text = "Select Terms of Conditions";
                }
                else
                {
                    lblcheck.Text = "Select PaymentMode";
                }
            }
        }

        private string Encrepttext(string OrderNumber)
        {
            Encryptdecrypt_BAL lEncryptdecrypt_BAL = new Encryptdecrypt_BAL();
            string EncreptOrderNumber = HttpUtility.UrlDecode(lEncryptdecrypt_BAL.Encrypt(OrderNumber));
            return EncreptOrderNumber;
        }
    }
}