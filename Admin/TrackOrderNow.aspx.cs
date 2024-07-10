using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DB_Utility;
using BAL;
using CDAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using DAL;

namespace Client.Admin
{
    public partial class TrackOrderNow : System.Web.UI.Page
    {
        List<TrackOrder_CDAL> listTrackOrder_CDAL = new List<TrackOrder_CDAL>();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["adminid"] != null)
                {
                    LoadOrderdetail();
                    LoadCounts();
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Login to View this Page');window.location='/Admin/Adminlogin.aspx';</script>");
                }
            }
        }

        private void LoadCounts()
        {
            LoadCount("Confirmed", lblcount);
            LoadCount("Processed", lblcount1);
            LoadCount("Dispatched", lblcount2);
            LoadCount("Delivered", lblcount3);
        }

        private void LoadCount(string OrderStatus, Label lbl)
        {
            string query = "";
            query = @"SELECT Count(*)as Count FROM tbl_OrderMain as om 
join (select OrderMain_OrderMainID,min(Item_ItemID)Item_ItemID,min(Quantity)Quantity,min(Price)Price,Type from 
tbl_OrderSub group by OrderMain_OrderMainID,Type) os on os.OrderMain_OrderMainID=om.OrderMainID
join tbl_ItemMaster as im on im.ItemID=os.Item_ItemID
join tbl_ItemImages as ii on ii.Item_ItemID=im.ItemID
join tbl_User as u on om.User_UserID=u.UserID where os.Type='ordernow' and OrderStatus='" + OrderStatus + "'";
            DataSet ds = DB_Con.GetDS(query);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                lbl.Text = dt.Rows[0]["Count"].ToString();
            }
        }

        private void LoadOrderdetail()
        {
            string query = "";
 
            query = @"SELECT distinct om.OrderMainID,om.User_UserID,ii.ImageName,CONVERT(VARCHAR(10), om.OrderDate, 103)as OrderDate,om.TotalAmount,
om.OrderNumber,om.ShipDate,om.OrderStatus,om.IsDelivered,os.Item_ItemID,os.Quantity,os.Price,im.ItemName,u.UserName, convert(varchar,OM.ExpectedDeliveryDate,103)  as ExpectedDeliveryDate
FROM tbl_OrderMain as om 
join (select OrderMain_OrderMainID,min(Item_ItemID)Item_ItemID,min(Quantity)Quantity,min(Price)Price,Type from 
tbl_OrderSub group by OrderMain_OrderMainID,Type) os on os.OrderMain_OrderMainID=om.OrderMainID
join tbl_ItemMaster as im on im.ItemID=os.Item_ItemID join tbl_ItemImages as ii on ii.Item_ItemID=im.ItemID
join tbl_User as u on om.User_UserID=u.UserID where os.Type='ordernow' and  OrderStatus='Confirmed' order by OrderDate desc";
            DataSet ds = DB_Con.GetDS(query);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                ViewState["UserId"] = dt.Rows[0]["User_UserID"];
                Gridordersub.DataSource = dt;
                Gridordersub.DataBind();
            }
        }
     
        #region Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int id;
                string orderno = "";
                string userid = "";
                TrackOrder_BAL lTrackOrder_BAL = new TrackOrder_BAL();
                lTrackOrder_BAL.lTrackOrder_CDAL = new TrackOrder_CDAL();
                if (ddlStatus.SelectedIndex > 0)
                {
                    string ChkStatus = "";
                    for (int i = 0; i < Gridordersub.Rows.Count; i++)
                    {
                        CheckBox ck = (CheckBox)Gridordersub.Rows[i].FindControl("chkorderstatus");
                        if (ck.Checked)
                        {
                            ChkStatus = "True";
                            Label ll = (Label)Gridordersub.Rows[i].Cells[1].FindControl("OrderMainID");
                            orderno += ll.Text + ",";
                            Label l2 = (Label)Gridordersub.Rows[i].Cells[7].FindControl("lblUser_UserID");
                            userid += l2.Text + ",";
                        }
                    }
                    if (ChkStatus == "True")
                    {
                        lTrackOrder_BAL.lTrackOrder_CDAL.OrderMainID = orderno.TrimEnd(',');
                        lTrackOrder_BAL.lTrackOrder_CDAL.User_UserID = userid.TrimEnd(',');
                        lTrackOrder_BAL.lTrackOrder_CDAL.OrderStatus = ddlStatus.SelectedItem.Text;
                         
                        if (txtdeliverydate.Text.Trim() != "")
                        {
                            string[] From = txtdeliverydate.Text.Trim().Split('/');
                            lTrackOrder_BAL.lTrackOrder_CDAL.ExpectedDeliveryDate = From[2] + "/" + From[1] + "/" + From[0];
                        }
                        else
                        {
                            lTrackOrder_BAL.lTrackOrder_CDAL.ExpectedDeliveryDate = "";
                        }
                        id = lTrackOrder_BAL.Ins_Upd_Del();
                        if (id > 0)
                        {
                            Response.Write("<Script> alert('Insert Successfully')</Script>");
                            LoadOrderdetail();
                            LoadCounts();
                           txtdeliverydate.Text = "";
                            if (ddlStatus.SelectedItem.Text == "Delivered")
                            {
                                SendEmail(lTrackOrder_BAL.lTrackOrder_CDAL.OrderMainID);
                            }
                        }
                        else
                        {
                            Response.Write("<Script> alert('Try Again')</Script>");
                        }
                    }
                    else
                    {
                        Response.Write("<Script> alert('Select Order')</Script>");
                    }
                }
                else
                {
                    Response.Write("<Script> alert('Select OrderStatus')</Script>");
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

        #endregion
        #region Clear
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ddlStatus.ClearSelection();
                btnSave.InnerHtml = "<i class='fa fa-floppy-o' aria-hidden='true'></i> Save";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        private void SendEmail(string OrderMainID)
        {
            List<TrackOrder_DAL> listTrackOrder_DAL = new List<TrackOrder_DAL>();
            List<string> OrderMinID = OrderMainID.Split(',').ToList();
            for (int i = 0; i < OrderMinID.Count; i++)
            {
                TrackOrder_DAL lTrackOrder_DAL = new TrackOrder_DAL();
                lTrackOrder_DAL.OrderMainID = OrderMinID[i];
                listTrackOrder_DAL.Add(lTrackOrder_DAL);
            }
            for (int j = 0; j < listTrackOrder_DAL.Count; j++)
            {
                string email = "";
                DataTable dt = new DataTable();
                
                string query = @"select  os.ordersubid,os.ordermain_ordermainid,om.ShippingAddress1,im.ItemName,im.ItemCode,ii.ImageName,om.OrderNumber,
om.User_UserID,u.Fname,u.EmailID,u.ContactNo,os.item_itemid,os.quantity,os.price,om.BillAmt,om.totalamount,om.ShippingAmt from tbl_ordersub as os 
join tbl_ordermain as om on os.ordermain_ordermainid=om.OrderMainID join tbl_ItemMaster as im on im.ItemID=os.item_itemid 
join tbl_ItemImages as ii on ii.Item_ItemID=im.itemid join tbl_User as u on u.UserID=om.User_UserID 
where om.OrderMainID=" + listTrackOrder_DAL[j].OrderMainID + " order by ordersubid desc";
                DataSet ds = DB_Con.SelectCommand(query);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    email = dt.Rows[0]["EmailID"].ToString();
                }
                MailMessage mail = new MailMessage();
                MailAddress ma = new MailAddress("hello@spicymix.in", "Spicy Mix | Homemade Foods order Online | Taste of South India");
                if (email != null)
                {
                    string stremailTo = email;
                    
                    mail.Subject = "Your SpicyMix Order has been delivered - successfully  OrderNo(" + dt.Rows[0]["OrderNumber"] + ")";
                    if (stremailTo.Length > 0)
                    {
                        mail.From = ma;
                        mail.To.Add(stremailTo);


                        mail.Body += @"<!doctype html><html><head>
   <meta charset='utf-8'>
   <title>Spicymix</title>
   <meta name='theme-color' content='#666' />
   <meta name='viewport'
      content='user-scalable=no, initial-scale=1, maximum-scale=1, minimum-scale=1, height=device-height, width=device-width, target-densitydpi=medium-dpi' />
   <style>
      .social {
         float: right;
         margin-top: 0;
      }

      .social li {
         margin-right: 10px;
         float: left;
         display: inline-block;
      }

      .social li a {
         border-radius: 30px;
         padding: 5px;
         font-size: 16px;
         color: #8c4299;
        
         text-decoration: none;
      }
   </style>

</head>
<body style='margin: auto; background:#fff;font-family: arial,sans-serif;'>
   <table bgcolor='#fafafa' width='100%' height='100%' align='center' style='border-collapse:collapse' border='0'
      cellspacing='0' cellpadding='0'>
      <tbody>
         <tr>
            <td align='center'>
               <table width='600' border='0' cellspacing='0' cellpadding='0' bgcolor='#f2f2f2' style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td height='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none;'>
                        </td>
                     </tr>
                     <tr>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                        <td width='48' valign='top'
                           style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                           <img src='http://spicymix.in/image/main-assert/logo.png' border='0' class='CToWUd'></td>
                        <td align='right'
                           style='font-size:12px;line-height:18px;color:#666;border-top:none;border-bottom:none;border-left:none;border-right:none'>
                             <p style='letter-spacing: 0.5px;font-size: 14px;color: #000;'>
                              <span style='font-weight:bold;'>Purely made from home</span>
                           </p>
                        </td>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                     <tr>
                        <td height='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff' style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td height='30' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                     <tr>
                        <td width='40' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                        <td align='top' style='border:none;'>
                           <p style='line-height: 30px;font-size: 14px;'>
                                 Hi <b>" + dt.Rows[0]["EmailID"] + "</b>";
                        mail.Body += @"<br>
                              <span style='font-size:24px;font-weight: bold;letter-spacing: 0.5px;'>Your Spicy Mix
                                Order is now delivered</span>
                              <br>
                              Thank you for shopping with us on spicymix.in!
                            
                           </p>
                        </td>
                        <td width='10' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
     
         <tr>
            <td align='center'>
               <table width='600' border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                  style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td width='7%' style='border:none;'>
                        </td>
                        <td width='22%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                           Order ID
                        </td>
                        <td width='58%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                            " + dt.Rows[0]["OrderNumber"] + "";
                        mail.Body += @"</td>
                        <td width='7%' style='border:none;'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
        </tr>";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            mail.Body += @"<tr>
            <td align='center'>
               <table width='600' border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                  style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td width='7%' style='border:none;'>
                        </td>
                        <td width='85%' valign='top' bgcolor='#f2f2f2'
                           style='color: #e01b22;padding: 10px;font-size: 14px;font-weight: bold;border: 1px solid #fff;'>
                           Order Details
                        </td>
                        <td width='7%' style='border:none;'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr><tr>
            <td rowspan='1' align='center'>
               <table width='600' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff' style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td width='7%' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                        <td width='20%' align='top' style='border:none;'>
                          <img src'http://spicymix.in/ItemImage/" + dt.Rows[i]["ImageName"].ToString() + "' width='140' height='200' />";
                            mail.Body += @"</td>
                        <td width='60%' bgcolor='#fff' style='border:none;'>
                           <table border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                              style='text-align:left;border: white;'>
                              <tbody>
                                 <tr>
                                    <td height='8' width='100%' valign='top' bgcolor='#f2f2f2'
                                       style='color: #8c4298;padding: 16px 185px;font-size: 14px;font-weight: bold;border: 1px solid #fff;'>
                                    </td>
                                 </tr>
                              </tbody>
                           </table>
                           <table border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                              style='text-align:left;border: white;'>
                              <tbody>
                                 <tr>
                                    <td width='20%' valign='top' bgcolor='#f2f2f2'
                                       style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                                       Item Code
                                    </td>
                                    <td width='70%' valign='top' bgcolor='#f2f2f2'
                                       style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                                      " + dt.Rows[i]["ItemCode"] + "";
                            mail.Body += @"</td>
                                 </tr>
                              </tbody>
                           </table>
                           <table border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                              style='text-align:left;border: white;'>
                              <tbody>
                                 <tr>
                                    <td width='20%' valign='top' bgcolor='#f2f2f2'
                                       style='color: #000;padding: 10px 9px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                                       Item Name
                                    </td>
                                    <td width='70%' valign='top' bgcolor='#f2f2f2'
                                       style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                                        " + dt.Rows[i]["ItemName"] + "";
                            mail.Body += @"</td>
                                 </tr>
                              </tbody>
                           </table>
                           <table border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                              style='text-align:left;border: white;'>
                              <tbody>
                                 <tr>
                                    <td width='20%' valign='top' bgcolor='#f2f2f2'
                                       style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                                       Item Price
                                    </td>
                                    <td width='60%' valign='top' bgcolor='#f2f2f2'
                                       style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                                       Rs." + dt.Rows[i]["price"] + "";
                            mail.Body += @"</td>
                                    <td width='10%' valign='top' bgcolor='#f2f2f2'
                                       style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                                     " + dt.Rows[i]["quantity"] + "";
                            mail.Body += @"</td>
                                 </tr>
                              </tbody>
                           </table>
                           <table border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                              style='text-align:left;border: white;'>
                              <tbody>
                                 <tr>
                                    <td height='8' width='100%' valign='top' bgcolor='#f2f2f2'
                                       style='color: #8c4298;padding: 16px 185px;font-size: 14px;font-weight: bold;border: 1px solid #fff;'>
                                    </td>
                                 </tr>
                              </tbody>
                           </table>
                        </td>
                        <td width='7%' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
        </tr>";
                        }
                        mail.Body += @" <tr>
            <td align='center'>
               <table width='600' border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                  style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td width='7%' style='border:none;'>
                        </td>
                        <td width='22%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                           Item Subtotal
                        </td>
                        <td width='58%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                           Rs. " + dt.Rows[0]["BillAmt"] + "";
                        mail.Body += @"</td>
                        <td width='7%' style='border:none;'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                  style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td width='7%' style='border:none;'>
                        </td>
                        <td width='22%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                           Shipping & Handling
                        </td>
                        <td width='58%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                           Rs. " + dt.Rows[0]["ShippingAmt"] + "";
                        mail.Body += @"</td>
                        <td width='7%' style='border:none;'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                  style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td width='7%' style='border:none;'>
                        </td>
                        <td width='22%' valign='top' bgcolor='#e01b22'
                           style='color: #fff;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                           Shipping Total (includes GST)
                        </td>
                        <td width='58%' valign='top' bgcolor='#e01b22'
                           style='color: #fff;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                            Rs. " + dt.Rows[0]["totalamount"] + "";
                        mail.Body += @"</td>
                        <td width='7%' style='border:none;'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                  style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td height='20' style='border:none;'>
                        </td>
                     </tr>
                     <tr>
                        <td width='7%' style='border:none;'>
                        </td>
                        <td width='85%' valign='top' bgcolor='#f2f2f2'
                           style='color: #e01b22;padding: 10px;font-size: 14px;font-weight: bold;border: 1px solid #fff;'>
                           Delivery Information
                        </td>
                        <td width='7%' style='border:none;'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                  style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td width='7%' style='border:none;'>
                        </td>
                        <td width='22%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                           Name
                        </td>
                        <td width='58%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                           " + dt.Rows[0]["Fname"] + "";
                        mail.Body += @"</td>
                        <td width='7%' style='border:none;'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                  style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td width='7%' style='border:none;'>
                        </td>
                        <td width='22%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                           Phone
                        </td>
                        <td width='58%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                          " + dt.Rows[0]["ContactNo"] + "";
                        mail.Body += @"</td>
                        <td width='7%' style='border:none;'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='1' bordercolor='#fff' cellspacing='0' cellpadding='0' bgcolor='#ffffff'
                  style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td width='7%' style='border:none;'>
                        </td>
                        <td width='22%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                           Address
                        </td>
                        <td width='58%' valign='top' bgcolor='#f2f2f2'
                           style='color: #000;padding: 10px;font-size: 12px;font-weight: bold;border: 1px solid #fff;'>
                          " + dt.Rows[0]["ShippingAddress1"] + "";
                        mail.Body += @"</td>
                        <td width='7%' style='border:none;'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff' style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td height='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                     <tr>
                        <td width='45' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                        <td align='top' style='border:none;'>
                           <p style='line-height: 22px;font-size: 14px;'>
                              *Package will be delivered between 09:00-19:00 from Monday to Saturday. There are no
                              deliveries on Sunday and Public holidays.
                           </p>
                        </td>
                        <td width='40' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                     <tr>
                        <td height='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='0' cellspacing='0' cellpadding='0' bgcolor='#e01b22' style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td height='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                     <tr>
                        <td width='45' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                        <td align='top' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                          <p><a href='http://spicymix.in/index.aspx'
                                 style='color: #fff;text-decoration: none;'>www.spicymix.in</a></p>
                        </td>
                        <td align='right'
                           style='font-size:12px;line-height:18px;color:#666;border-top:none;border-bottom:none;border-left:none;border-right:none'>
                           <p style='letter-spacing: 0.5px;font-size: 14px;color: #000;'>
                               <ul class='social'>
                                <li><a href='https://www.facebook.com/spicymixfoods'><img src='http://spicymix.in/image/fb.png' width='20' height='20' alt='' /></a></li>
                                 <li><a href='https://www.instagram.com/spicymixfoods'><img src='http://spicymix.in/image/insta.png' width='20' height='20' alt='' /></a></li>
                              </ul>
                           </p>
                           <br>
                        </td>
                        <td width='30' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                     <tr>
                        <td height='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='0' bordercolor='#ffffff' cellspacing='0' cellpadding='0' bgcolor='#f2f2f2'
                  style='text-align:left;border: white;'>
                  <tbody>
                     <tr>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                        <td align='top' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                           <p
                              style='font-weight: bold;text-align: center;color:#000;font-size:11px;line-height:15px;margin-top:14px;margin-bottom:4px;'>
                              Disclaimer: This is an auto-geneated email. Please do not reply.
                              <br>
                              Copyrights &#169; 2021 <span style='font-size: 13px;font-weight:bold;'>www.spicymix.in</span>
                              | All rights reserved
                           </p>
                        </td>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                     <tr>
                        <td height='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td></tr></tbody></table></td></tr></tbody></table></body></html>";

                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "relay-hosting.secureserver.net";
                        smtp.EnableSsl = false;
                        //smtp.Host = "smtp.gmail.com";
                        //smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("hello@spicymix.in", "1Homemade9#");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 25;
                        //smtp.Port = 587;
                        smtp.Send(mail);
                    }
                    mail.To.Clear();
                    //Response.Redirect("OrderConfirmationSuccess.aspx");
                }
            }
        }
        protected void btnconfirm_Click(object sender, EventArgs e)
        {
            
            fillgrid("Confirmed");
        }
        protected void btnship_Click(object sender, EventArgs e)
        {
            fillgrid("Processed");
        }
        protected void btndispatch_Click(object sender, EventArgs e)
        {
            fillgrid("Dispatched");
        }
        protected void btndeliver_Click(object sender, EventArgs e)
        {
            
            fillgrid("Delivered");
        }

        private void fillgrid(string ordstatus)
        {

            listTrackOrder_CDAL = new List<TrackOrder_CDAL>();
            TrackOrder_BAL lTrackOrder_BAL = new TrackOrder_BAL();
            lTrackOrder_BAL.lTrackOrder_CDAL = new TrackOrder_CDAL();
            lTrackOrder_BAL.lTrackOrder_CDAL.OrderStatus = ordstatus;
            lTrackOrder_BAL.lTrackOrder_CDAL.Type = "ordernow";
            listTrackOrder_CDAL = lTrackOrder_BAL.GetStatus();
            Gridordersub.DataSource = listTrackOrder_CDAL;
            Gridordersub.DataBind();
        }
        protected void View(object sender, CommandEventArgs e)
        {
            int orderno = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("ViewOrder.aspx?ordtype=ordernow&orderno=" + orderno);
        }

        

    }
}