  <%@ Page Title="" Language="C#" MasterPageFile="~/ViewScreen.Master" AutoEventWireup="true"
    CodeBehind="CheckOut.aspx.cs" Inherits="Client.CheckOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <style>
        .address
        {
            text-overflow: ellipsis;
            overflow: hidden;
            height: auto;
        }
    </style>
    <div class="container">
        <ul class="breadcrumb">
            <li><a href="index.aspx">Home</a></li>
            <li><a href="Viewcart.aspx">Shopping Cart</a></li>
            <li><a href="#">Checkout</a></li>
        </ul>
        <div class="row">
            <div id="content" class="col-sm-12">
                <div class="heading-extra">
                    <h4>
                        Checkout</h4>
                </div>
                <asp:UpdatePanel ID="UptpnlCheckout" runat="server" RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnlogin" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="common-box">
                            <div class="panel-group" id="accordion">
                                <div class="panel panel-default" runat="server">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a>Step 1: Checkout Options <i class="fa fa-caret-down"></i></a>
                                        </h4>
                                    </div>
                                    <div class="panel-collapse collapse in" runat="server" id="collapse_checkout_option"
                                        aria-expanded="true">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <h2>
                                                        New Customer</h2>
                                                    <div class="radio">
                                                        <a href="#">
                                                            <p>
                                                                Register Account</p>
                                                        </a>
                                                    </div>
                                                    <p>
                                                        By creating an account you will be able to shop faster, be up to date on an order's
                                                        status, and keep track of the orders you have previously made.</p>
                                                    <a href="register.aspx" class="btn btn-primary">Continue</a>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="logincontainer">
                                                        <h2>
                                                            Returning Customer</h2>
                                                        <p>
                                                            I am a returning customer</p>
                                                        <div class="form-group">
                                                            <label class="control-label" for="input-email">
                                                                MobileNo (Ex:9999900000)
                                                            </label>
                                                            <asp:TextBox ID="txtMobile" runat="server" placeholder="MobileNo" class="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtMobile" ForeColor="Red"
                                                                SetFocusOnError="true" Display="Dynamic" ErrorMessage=" Restrict for special characters"
                                                                ID="rfvname" ValidationExpression="^[\sa-zA-Z0-9]*$"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <a href="ForgetPassword.aspx">Forgotten Password</a></div>
                                                    <asp:Button ID="btnlogin" runat="server" Text="Login" class="btn btn-primary" OnClick="btnLogin_Click"
                                                        OnClientClick="return validatLogin()" />
                                                    <asp:Label ID="lblloginmsg" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a>Step 2: Delivery Address <i class="fa fa-caret-down"></i></a>
                                    </h4>
                                </div>
                                <div runat="server" class="panel-collapse collapse" id="collapse_payment_address"
                                    aria-expanded="false">
                                    <div class="panel-body">
                                        <div class="form-horizontal">
                                            <div class="from-register">
                                              <%--  <div class="radio">
                                                    <label>
                                                        <asp:RadioButtonList ID="rdoaddress" runat="server" OnSelectedIndexChanged="rdoaddress_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                            <asp:ListItem Selected="True" Value="1">I want to use a new address</asp:ListItem>
                                                            <asp:ListItem Value="2">I want to use an existing address</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </label>
                                                </div>--%>
                                                <div runat="server" id="address_existing">
                                                    <asp:Label ID="lbluseraddress" class="form-control address" runat="server"></asp:Label>
                                                    <asp:Button ID="btnEditAddress" runat="server" class="btn btn-primary" Text="Edit"
                                                        OnClick="btnEditAddress_Click" />
                                                </div>
                                                <asp:Label ID="lbladdmsg" runat="server"></asp:Label>
                                            </div>
                                            <br />
                                            <div runat="server" id="address_new">
                                                <div class="form-group required">
                                                    <label class="col-sm-2 control-label" for="input-payment-Name">
                                                        FullName
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtFullName" runat="server" placeholder="FullName" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group required">
                                                    <label class="col-sm-2 control-label" for="input-payment-MobileNo">
                                                        MobileNo
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtMobileNo" runat="server" placeholder="MobileNo" class="form-control" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group required">
                                                    <label class="col-sm-2 control-label" for="input-payment-country">
                                                        Country
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <asp:DropDownList ID="ddlCountry" runat="server" class="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lbltext" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group required">
                                                    <label class="col-sm-2 control-label" for="input-payment-zone">
                                                        Region / State
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <asp:DropDownList ID="ddlState" runat="server" class="form-control" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div id="DivSchAddr" runat="server">
                                                <div id="divdistrict" runat="server" class="form-group required">
                                                    <label class="col-sm-2 control-label" for="input-payment-city">
                                                        District
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="form-control" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div id="divLocation" runat="server" class="form-group required">
                                                    <label class="col-sm-2 control-label" for="input-payment-city">
                                                        Location
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <asp:DropDownList ID="ddlLocation" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                        
                                                    </div>
                                                </div>

                                                <%--<div class="form-group required">
                                                    <label class="col-sm-2 control-label" for="input-payment-city">
                                                        City
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtCity" runat="server" placeholder="City" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>--%>
                                                </div>
                                                 <div id="DivOrderNowAddr" runat="server">
                                                <div class="form-group required">
                                                    <label class="col-sm-2 control-label" for="input-payment-city">
                                                        City
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtCity" runat="server" placeholder="City" class="form-control" autocomplete="off" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                </div>
                                                <div class="form-group required">
                                                    <label class="col-sm-2 control-label" for="input-payment-address-1">
                                                        Address
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtAddress1" runat="server" placeholder="Address" class="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group required">
                                                    <label class="col-sm-2 control-label" for="input-payment-postcode">
                                                        Post Code
                                                    </label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtPostCode" runat="server" placeholder="Post Code" class="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label" for="input-payment-Landmark">
                                                        Landmark</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtLandmark" runat="server" placeholder="Landmark" class="form-control" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            
                                            <%--<div class="buttons clearfix">
                                                <div class="pull-right">
                                                    <asp:Button ID="btncontinueaddress" runat="server" class="btn btn-primary" Text="Continue"
                                                        OnClick="btnContinueAddress_Click" OnClientClick="return validateAddress()" />
                                                    <asp:Label ID="lblvalitmsg" runat="server" Text="" ForeColor="Red" ></asp:Label>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                    <div class="buttons clearfix">
                                                <div class="pull-right">
                                                    <asp:Button ID="btncontinueaddress" runat="server" class="btn btn-primary" Text="Continue"
                                                        OnClick="btnContinueAddress_Click" OnClientClick="return validateAddress()" />
                                                    <asp:Label ID="lblvalitmsg" runat="server" Text="" ForeColor="Red" ></asp:Label>
                                                </div>
                                            </div>

                                </div>
                            </div>

                            <!-- test start-->
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a>Step 3: Confirm Order <i class="fa fa-caret-down"></i></a>
                                    </h4>
                                </div>
                                <div class="panel-collapse collapse" runat="server" id="collapse_checkout_confirm"
                                    aria-expanded="false">
                                    <div class="panel-body">
                                        <div class="table-responsive">
                                            <asp:HiddenField ID="hdnweight" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnpincode" runat="server" Value="" />
                                            <asp:HiddenField ID="hdncountry" runat="server" Value="" />
                                             <asp:HiddenField ID="hdncountryID" runat="server" Value="" />
                                            <asp:HiddenField ID="hdncountrycode" runat="server" Value="" />
                                             <asp:HiddenField ID="hdnState" runat="server" Value="" />
                                             <asp:HiddenField ID="hdnDistrict" runat="server" Value="" />
                                             <asp:HiddenField ID="hdnLocation" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnUserTempId" runat="server" Value="" />
                                            <asp:Label ID="lblgridmsg" runat="server" Text=""></asp:Label>
                                            <asp:GridView ID="grdCheckOut" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-responsive-stack"
                                                OnRowDeleting="OnRowDeleting" DataKeyNames="itemid" OnRowDataBound="grdCheckOut_RowDataBound"
                                                EmptyDataText="No Data Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Image">
                                                        <ItemTemplate>
                                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/ItemImage/"+Eval("ImageName") %>'
                                                                Width="70px"></asp:Image>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ItemCode" runat="server" Text='<%#Eval("ItemCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ItemName" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <button type="button" class="pull-left btn-number btnminus" eventargument='<%#Eval("itemid") %>'
                                                                altereventargument='<%#Eval("SizeID") %>' id="decr" runat="server" onserverclick="btnminus_Click"
                                                                causesvalidation="false">
                                                                <span class="glyphicon glyphicon-minus"></span>
                                                            </button>
                                                            <asp:TextBox ID="txtqty" Text='<%#Eval("Quantity") %>' EventArgument='<%#Eval("itemid") %>'
                                                                AlterEventArgument='<%#Eval("SizeID") %>' class="pull-left input-number" min="1"
                                                                max="10" runat="server" OnTextChanged="txtqty_Textchanged" AutoPostBack="true"
                                                                Style="width: 30px; text-align: center;" ReadOnly="true"></asp:TextBox>
                                                            <button type="button" class="pull-left btn-number btnplus" eventargument='<%#Eval("itemid") %>'
                                                                altereventargument='<%#Eval("SizeID") %>' id="incr" runat="server" onserverclick="btnplus_Click"
                                                                causesvalidation="false">
                                                                <span class="glyphicon glyphicon-plus"></span>
                                                            </button>
                                                            <br />
                                                            <br />
                                                            <asp:Label class="pull-left" ID="lblQty" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Price" runat="server" Text='<%#Eval("Rate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Amount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSize" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkdelete" class="btn btn-primary" runat="server" CommandName="Delete" causesvalidation="false">Delete</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Label ID="Label1" runat="server" Text="Tax" Visible="false"></asp:Label>
                                            <asp:Label ID="txtTaxAmount" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="Discount" Visible="false"></asp:Label>
                                            <asp:Label ID="txtDiscount" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="Label3" runat="server" Text="Total BillAmount" Visible="false"></asp:Label>
                                            <asp:Label ID="txtTotalamount" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <div class="buttons">
                                            <div class="pull-right">
                                                <button type="button" runat="server" class="btn btn-primary" id="btnConfirm" onserverclick="btnConfirm_Click">
                                                    Confirm Order</button>
                                            </div>
                                            <div class="pull-left">
                                                <button type="button" runat="server" class="btn btn-primary" id="btnPrevStep" causesvalidation="false" onserverclick="btnPrevConfirm_Click">
                                                    Prev-Step</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--test ends-->

                             <!-- orignial start-->
                            <%--<div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a>Step 3: Confirm Order <i class="fa fa-caret-down"></i></a>
                                    </h4>
                                </div>
                                <div class="panel-collapse collapse" runat="server" id="collapse_checkout_confirm"
                                    aria-expanded="false">
                                    <div class="panel-body">
                                        <div class="table-responsive">
                                            <asp:HiddenField ID="hdnweight" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnpincode" runat="server" Value="" />
                                            <asp:HiddenField ID="hdncountry" runat="server" Value="" />
                                             <asp:HiddenField ID="hdncountryID" runat="server" Value="" />
                                            <asp:HiddenField ID="hdncountrycode" runat="server" Value="" />
                                             <asp:HiddenField ID="hdnState" runat="server" Value="" />
                                             <asp:HiddenField ID="hdnDistrict" runat="server" Value="" />
                                             <asp:HiddenField ID="hdnLocation" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnUserTempId" runat="server" Value="" />
                                            <asp:Label ID="lblgridmsg" runat="server" Text=""></asp:Label>
                                            <asp:GridView ID="grdCheckOut" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                                OnRowDeleting="OnRowDeleting" DataKeyNames="itemid" OnRowDataBound="grdCheckOut_RowDataBound"
                                                EmptyDataText="No Data Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Image">
                                                        <ItemTemplate>
                                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/ItemImage/"+Eval("ImageName") %>'
                                                                Width="70px"></asp:Image>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ItemCode" runat="server" Text='<%#Eval("ItemCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ItemName" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <button type="button" class="pull-left btn-number btnminus" eventargument='<%#Eval("itemid") %>'
                                                                altereventargument='<%#Eval("SizeID") %>' id="decr" runat="server" onserverclick="btnminus_Click"
                                                                causesvalidation="false">
                                                                <span class="glyphicon glyphicon-minus"></span>
                                                            </button>
                                                            <asp:TextBox ID="txtqty" Text='<%#Eval("Quantity") %>' EventArgument='<%#Eval("itemid") %>'
                                                                AlterEventArgument='<%#Eval("SizeID") %>' class="pull-left input-number" min="1"
                                                                max="10" runat="server" OnTextChanged="txtqty_Textchanged" AutoPostBack="true"
                                                                Style="width: 30px; text-align: center;" ReadOnly="true"></asp:TextBox>
                                                            <button type="button" class="pull-left btn-number btnplus" eventargument='<%#Eval("itemid") %>'
                                                                altereventargument='<%#Eval("SizeID") %>' id="incr" runat="server" onserverclick="btnplus_Click"
                                                                causesvalidation="false">
                                                                <span class="glyphicon glyphicon-plus"></span>
                                                            </button>
                                                            <br />
                                                            <br />
                                                            <asp:Label class="pull-left" ID="lblQty" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Price" runat="server" Text='<%#Eval("Rate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Amount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSize" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkdelete" class="btn btn-primary" runat="server" CommandName="Delete" causesvalidation="false">Delete</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Label ID="Label1" runat="server" Text="Tax" Visible="false"></asp:Label>
                                            <asp:Label ID="txtTaxAmount" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="Discount" Visible="false"></asp:Label>
                                            <asp:Label ID="txtDiscount" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="Label3" runat="server" Text="Total BillAmount" Visible="false"></asp:Label>
                                            <asp:Label ID="txtTotalamount" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <div class="buttons">
                                            <div class="pull-right">
                                                <button type="button" runat="server" class="btn btn-primary" id="btnConfirm" onserverclick="btnConfirm_Click">
                                                    Confirm Order</button>
                                            </div>
                                            <div class="pull-left">
                                                <button type="button" runat="server" class="btn btn-primary" id="btnPrevStep" causesvalidation="false" onserverclick="btnPrevConfirm_Click">
                                                    Prev-Step</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                             <!-- orignial ends-->

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a>Step 4: Payment Method <i class="fa fa-caret-down"></i></a>
                                    </h4>
                                </div>
                                <div class="panel-collapse collapse" runat="server" id="collapse_payment_method"
                                    aria-expanded="false">
                                    <div class="panel-body">
                                        <p>
                                            <span style="color: Red;">*</span> Please select the preferred payment method to
                                            use on this order.</p>
                                        <div class="col-md-6">
                                            <div class="radio">
                                                <label>
                                                    <asp:RadioButtonList ID="rdoPaymentType" runat="server">
                                                    </asp:RadioButtonList>
                                                </label>
                                            </div>
                                            <asp:Label ID="lblPayment" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label" for="Your_Name">
                                                    Name</label>
                                                <asp:TextBox ID="txtName" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label" for="Mobile_Number">
                                                    Mobile Number</label>
                                                <asp:TextBox ID="txtMobileNo1" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label" for="Email">
                                                    Email</label>
                                                <asp:TextBox ID="txtEmail1" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label" for="Address">
                                                    Shipping Address</label>
                                                <asp:TextBox ID="txtAddress" Rows="5" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <span style="color: Red;">*</span><asp:CheckBox ID="chkagree" runat="server" />
                                            &nbsp; I have read and agree to the <b>Terms &amp; Conditions</b>
                                            <asp:Label ID="lblcheck" runat="server" Style="color: Red;"></asp:Label>
                                        </div>
                                        <div class="buttons">
                                            <div class="pull-right">
                                                <button type="button" runat="server" class="btn btn-primary" id="btnPreContinuePay"
                                                    onserverclick="btnPreContinuePay_Click">
                                                    Continue</button>
                                            </div>
                                            <div class="pull-left">
                                                <button type="button" runat="server" class="btn btn-primary" id="btnPrevpayment"
                                                    onserverclick="btnPrevPayment_Click">
                                                    Prev-Step</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!--content bottom full -->
    <!--over-->
    <script>
        $(document).ready(function () {
            var livecount = $('#BodyContent_grdCheckOut_txtqty').val();
            $('#incr').click(function () {
                livecount++;
                $('#BodyContent_grdCheckOut_txtqty').val(livecount);
            });
            $('#decr').click(function () {
                livecount--;
                $('#BodyContent_grdCheckOut_txtqty').val(livecount);
            });
        });
    </script>
    <style>
        .plholder::-webkit-input-placeholder
        {
            color: red;
        }
    </style>
    <script type="text/javascript">
        function validatLogin() {
            if (document.getElementById("<%=txtMobile.ClientID%>").value == "") {
                var inputtxt = document.getElementById("<%=txtMobile.ClientID%>");
                inputtxt.classList.add('plholder');
                document.getElementById("<%=txtMobile.ClientID%>").focus();
                return false;
            }
            var mob = /^[1-9]{1}[0-9]{9}$/;
            var txtMobile = document.getElementById("<%=txtMobile.ClientID%>");
            if (mob.test(txtMobile.value) == false) {
                document.getElementById('<%=lblloginmsg.ClientID%>').innerHTML = 'Not a valid MobileNo';
                document.getElementById('<%=lblloginmsg.ClientID%>').style.color = "red";
                txtMobile.focus();
                return false;
            }
            return true;

        }
    </script>
    <script>
        function validateAddress() {
            if (document.getElementById("<%=txtFullName.ClientID%>").value == "") {
                var inputtxt = document.getElementById("<%=txtFullName.ClientID%>");
                inputtxt.classList.add('plholder');
                document.getElementById("<%=txtFullName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtMobileNo.ClientID%>").value == "") {
                var inputtxt = document.getElementById("<%=txtMobileNo.ClientID%>");
                inputtxt.classList.add('plholder');
                document.getElementById("<%=txtMobileNo.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlCountry.ClientID%>").value == "0") {
                document.getElementById('<%=lblvalitmsg.ClientID%>').innerHTML = 'Select Country';
                document.getElementById('<%=lblvalitmsg.ClientID%>').style.color = "red";
                document.getElementById("<%=ddlCountry.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlState.ClientID%>").value == "0") {
                document.getElementById('<%=lblvalitmsg.ClientID%>').innerHTML = 'Select State';
                document.getElementById('<%=lblvalitmsg.ClientID%>').style.color = "red";
                document.getElementById("<%=ddlState.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlLocation.ClientID%>").value == "0") {
                document.getElementById('<%=lblvalitmsg.ClientID%>').innerHTML = 'Select Location';
                document.getElementById('<%=lblvalitmsg.ClientID%>').style.color = "red";
                document.getElementById("<%=ddlLocation.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=ddlCountry.ClientID%>").value == "0") {
                var Country = document.getElementById("<%=ddlCountry.ClientID%>");
                if (Country.value == "India") {
                    if (document.getElementById("<%=ddlDistrict.ClientID%>").value == "0") {
                        document.getElementById('<%=lblvalitmsg.ClientID%>').innerHTML = 'Select District';
                        document.getElementById('<%=lblvalitmsg.ClientID%>').style.color = "red";
                        document.getElementById("<%=ddlDistrict.ClientID%>").focus();
                        return false;
                    }
                }
            }

             
            if (document.getElementById("<%=txtAddress1.ClientID%>").value == "") {
                var inputtxt = document.getElementById("<%=txtAddress1.ClientID%>");
                inputtxt.classList.add('plholder');
                document.getElementById("<%=txtAddress1.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPostCode.ClientID%>").value == "") {
                var inputtxt = document.getElementById("<%=txtPostCode.ClientID%>");
                inputtxt.classList.add('plholder');
                document.getElementById("<%=txtPostCode.ClientID%>").focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {


            // inspired by http://jsfiddle.net/arunpjohny/564Lxosz/1/
            $('.table-responsive-stack').each(function (i) {
                var id = $(this).attr('id');
                //alert(id);
                $(this).find("th").each(function (i) {
                    $('#' + id + ' td:nth-child(' + (i + 1) + ')').prepend('<span class="table-responsive-stack-thead">' + $(this).text() + ':</span> ');
                    $('.table-responsive-stack-thead').hide();

                });



            });





            $('.table-responsive-stack').each(function () {
                var thCount = $(this).find("th").length;
                var rowGrow = 100 / thCount + '%';
                //console.log(rowGrow);
                $(this).find("th, td").css('flex-basis', rowGrow);
            });




            function flexTable() {
                if ($(window).width() < 768) {

                    $(".table-responsive-stack").each(function (i) {
                        $(this).find(".table-responsive-stack-thead").show();
                        $(this).find('thead').hide();
                    });


                    // window is less than 768px   
                } else {


                    $(".table-responsive-stack").each(function (i) {
                        $(this).find(".table-responsive-stack-thead").hide();
                        $(this).find('thead').show();
                    });



                }
                // flextable   
            }

            flexTable();

            window.onresize = function (event) {
                flexTable();
            };






            // document ready  
        });

    </script>
</asp:Content>
