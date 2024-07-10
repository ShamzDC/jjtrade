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
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace Client
{
    public partial class ViewScreen : System.Web.UI.MasterPage
    {
       // List<User_CDAL> listUser_CDAL = new List<User_CDAL>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadChildMenu();
                //hmyaccount.Visible = false;
                if (Session["U_Name"] != null)
                {
                    //((Label)Master.FindControl("lblInvalidMsg")).Text = "Invalid Selection";

                    lblUsername.Text = "Welcome " + Session["U_Name"].ToString() + "";
                    A1.Visible = false;
                    A2.InnerHtml = "Logout";
                    A3.Visible = true;
                    div_newsletter.Visible = false;
                }
                else
                {
                    div_newsletter.Visible = true;
                }
                if (Session["dtItem"] == null)
                {
                    lblitemcount.Text = "0";
                }
                else
                {
                    if (Session["Type"]  != null)
                    {
                        DataTable dtItem = (DataTable)Session["dtItem"];
                        lblitemcount.Text = dtItem.Rows.Count.ToString();
                    }
                    else
                    {
                        Session["dtItem"] = null;
                        lblitemcount.Text = "0";
                    }
                }
            }
        }

   
        protected void rptMainMenu_DataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Repeater rptSubMenu = e.Item.FindControl("rptSubMenu") as Repeater;
                    DataTable dt = new DataTable();
                    string query = @"SELECT SubID,SubMenu,Main_MenuID FROM tbl_SubMenu where Main_MenuID=" + ((System.Data.DataRowView)(e.Item.DataItem)).Row[0];
                    DataSet ds = DB_Con.GetDS(query);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        rptSubMenu.DataSource = dt;
                        rptSubMenu.DataBind();                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rptSubMenu_DataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Repeater rptSubChildMenu = e.Item.FindControl("rptSubChildMenu") as Repeater;
                    DataTable dt = new DataTable();
                    string query = @"SELECT ChildID,ChildMenu,Sub_SubID,Main_MenuID FROM tbl_SubChildMenu where  Sub_SubID=" + ((System.Data.DataRowView)(e.Item.DataItem)).Row[0];
                    DataSet ds = DB_Con.GetDS(query);
                    dt = ds.Tables[0];
                    rptSubChildMenu.DataSource = dt;
                    rptSubChildMenu.DataBind();                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadChildMenu()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT ChildID,ChildMenu,Sub_SubID,Main_MenuID FROM tbl_SubChildMenu";
            DataSet ds = DB_Con.GetDS(query);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                rptSubChildMenu.DataSource = dt;
                rptSubChildMenu.DataBind();                
            }
        }
        public void hrefdrop_serverclick(object sender, EventArgs e)
        {
            HtmlAnchor href = sender as HtmlAnchor;
            RepeaterItem item = href.NamingContainer as RepeaterItem;
            HiddenField hdnMenu = item.FindControl("hdnMenu") as HiddenField;
            string Menu = hdnMenu.Value;
            string EncreptMenu = HttpUtility.UrlEncode(Menu).Replace("+", "-");
            //string EncreptMenu = Menu.Replace(" ", "-");
            Response.Redirect("/Product/?Fet=" + EncreptMenu + "");
        }
      

        protected void btnViewCart_Click(object sender, EventArgs e)
        {
            if (Session["dtItem"] != null)
            {
                if (Session["Type"] != null)
                {
                    Response.Redirect("~/ViewCart.aspx?val=" + Session["Type"].ToString() + "");
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
               
            }
            else
            {
                 //Response.Write("<script type=\"text/javascript\">alert('Your Cart is Empty. Add one or more Products to Proceed!');</script>");
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtmail.Text.Trim() != "")
                {
                    //lblvalimsg.Text = "";
                    string query = @"select * from tbl_User where EmailID='" + txtmail.Text.Trim() + "';";
                    DataSet ds = DB_Con.GetDS(query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblvalimsg.Text = "Thank-you for join Spicymix";
                        lblvalimsg.ForeColor = System.Drawing.ColorTranslator.FromHtml("#e01b22");
                        MailSend();
                    }
                    else
                    {
                        DML("Insert", 0);
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
            finally
            {

          }
        }

        private void DML(string flg, int scid)
        {
            //int sm;
            //User_BAL lUser_BAL = new User_BAL();
            //lUser_BAL.lUser_CDAL = new User_CDAL();
            //lUser_BAL.lUser_CDAL.EmailID = txtmail.Text;
            //lUser_BAL.lUser_CDAL.Unsubscribe = "Y";
            //sm = lUser_BAL.Ins_Upd_Del(flg);
            //if (sm > 0)
            //{
            //    txtmail.Text = "";
            //    lblvalimsg.Text = "Thank-you for join Spicymix";
            //    lblvalimsg.ForeColor = System.Drawing.ColorTranslator.FromHtml("#e01b22");
            //    MailSend();
            //}
        }

        private string Encrepttext(string EmailID)
        {
            //Encryptdecrypt_BAL lEncryptdecrypt_BAL = new Encryptdecrypt_BAL();
            //string EncreptOrderNumber = HttpUtility.UrlDecode(lEncryptdecrypt_BAL.Encrypt(EmailID));
            string EncreptOrderNumber = "";
            return EncreptOrderNumber;
        }
        private void MailSend()
        {
            string EmailID = Encrepttext(txtmail.Text);
            MailMessage mail = new MailMessage();
            MailAddress ma = new MailAddress("hello@spicymix.in", "Spicy Mix | Homemade Foods order Online | Taste of South India");
            if (txtmail.Text != null)
            {
                string stremailTo = txtmail.Text;
                mail.Subject = "Spicy Mix | Homemade Foods order Online | Taste of South India";
                if (stremailTo.Length > 0)
                {
                    mail.From = ma;
                    mail.To.Add(stremailTo);

                    mail.Body = mail.Body + @"<!doctype html><html><head>
            <meta charset='utf-8'><title></title>
   <meta name='theme-color' content='#666' />
   <meta name='viewport'content='user-scalable=no, initial-scale=1, maximum-scale=1, minimum-scale=1, height=device-height, width=device-width, target-densitydpi=medium-dpi' />
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
               <table width='600' border='0' bordercolor='#ffffff' cellspacing='0' cellpadding='0' bgcolor='#f2f2f2'
                  style='text-align:left'>
                  <tbody>
                     <tr>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                        <td align='top' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                           <p
                              style='font-weight: bold;text-align: center;color:#000;font-size:11px;line-height:15px;margin-top:14px;margin-bottom:4px;'>
                              some contents of this email may not display and/or behave properly. <a href='http://spicymix.in/index.aspx'>Learn more...</a>
                              <br>
                              if you are unable to see the message below, <a href='http://spicymix.in/View.aspx'>click here</a> to view.
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
               <table width='600' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff' style='text-align:left'>
                  <tbody>
                     <tr>
                        <td height='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                     <tr>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                        <td width='48' valign='top'
                           style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                           <img src='http://spicymix.in//image/main-assert/logo.png' border='0' class='CToWUd'></td>
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
               <table width='600' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff' style='text-align:left'>
                  <tbody>
                     <tr>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                        <td align='top' style='border:none;'>
                           <a href='http://spicymix.in/index.aspx' target='_blank'>
                              <img width='auto' height='auto' src='http://spicymix.in/image/SpicyMix_Advt1.jpg' class='CToWUd'></a>
                        </td>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff' style='text-align:left'>
                  <tbody>
                     <tr>
                        <td height='7' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                     <tr>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                        <td align='top' style='border:none;'>
                           <a href='http://spicymix.in/index.aspx' target='_blank'>
                              <img width='auto' height='auto' src='http://spicymix.in/image/SpicyMix_Advt2.jpg' class='CToWUd'></a>
                        </td>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                     <tr>
                        <td height='7' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff' style='text-align:left'>
                  <tbody>
                     <tr>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                        <td align='top' style='border:none;'>
                           <a href='http://spicymix.in/index.aspx' target='_blank'>
                              <img width='auto' height='auto' src='http://spicymix.in/image/SpicyMix_Advt3.jpg' class='CToWUd'></a>
                        </td>
                        <td width='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
         <tr>
            <td align='center'>
               <table width='600' border='0' cellspacing='0' cellpadding='0' bgcolor='#e01b22' style='text-align:left'>
                  <tbody>
                     <tr>
                        <td height='20' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
                        </td>
                     </tr>
                     <tr>
                        <td width='40' style='border-top:none;border-bottom:none;border-left:none;border-right:none'>
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
                  style='text-align:left'>
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
                              | All rights
                              reserved |<a href='http://spicymix.in/Unsubscribe.aspx?U=" + EmailID + "'>Unsubscribe</a>";
                    mail.Body = mail.Body + @"</p>
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
      </tbody>
   </table>
</body>

</html>";


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
                    smtp.Send(mail);
                    //txtmail.Text = "";
                }
                mail.To.Clear();
                txtmail.Text = "";
            }
            //Insert NewsLetter Starts//

            //User_BAL lUser_BAL = new User_BAL();
            //lUser_BAL.lUser_CDAL = new User_CDAL();
            //lUser_BAL.lUser_CDAL.NewsLetter = mail.Body;
            //int nl = lUser_BAL.InsertNewsLetter();

            //Insert NewsLetter Ends//
        }


    }
}