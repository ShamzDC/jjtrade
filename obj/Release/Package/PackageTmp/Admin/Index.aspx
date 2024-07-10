<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Client.Admin.Index" %>

<!DOCTYPE html>
<html lang="en" class="no-js">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="user-scalable=no, initial-scale=1, maximum-scale=1, minimum-scale=1, width=320, height=device-height" />
    <meta name="author" content="" />
    <meta property="og:type" content="website" />
    <meta property="og:image" content="images/page-icon.jpg" />
    <meta property="og:title" content="E-Commerce" />
    <meta property="og:description" content="" />
    <meta property="og:url" content="" />
      <title>Forex</title>
    <link rel="shortcut icon"  />
    <base href="#/">
    <link href="vendor/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/theme.css" rel="stylesheet" type="text/css" />
    <link href="vendor/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <style>
        .admin-logo img
        {
            margin: auto;
        }
        .admin-con
        {
            display: table;
            width: 100%;
            height: 100vh;
        }
        .admin-row
        {
            display: table-cell;
            vertical-align: middle;
        }
        .panel-heading label[for=rdbAdmin]
        {
            margin-right: 20px;
        }
        
        .panel-login
        {
            width: 420px;
            margin: auto;
            position: relative;
            border-color: #ccc;
            padding: 29px;
        }
        .panel-login > .panel-heading
        {
            color: #00415d;
            background-color: #fff;
            border-color: #fff;
            text-align: center;
        }
        .panel-login > .panel-heading a
        {
            text-decoration: none;
            color: #666;
            font-weight: bold;
            font-size: 15px;
            -webkit-transition: all 0.1s linear;
            -moz-transition: all 0.1s linear;
            transition: all 0.1s linear;
        }
        .panel-login > .panel-heading a.active
        {
            color: #029f5b;
            font-size: 18px;
        }
        .panel-login > .panel-heading hr
        {
            margin-top: 10px;
            margin-bottom: 0px;
            clear: both;
            border: 0;
            height: 1px;
            background-image: -webkit-linear-gradient(left,rgba(0, 0, 0, 0),rgba(0, 0, 0, 0.15),rgba(0, 0, 0, 0));
            background-image: -moz-linear-gradient(left,rgba(0,0,0,0),rgba(0,0,0,0.15),rgba(0,0,0,0));
            background-image: -ms-linear-gradient(left,rgba(0,0,0,0),rgba(0,0,0,0.15),rgba(0,0,0,0));
            background-image: -o-linear-gradient(left,rgba(0,0,0,0),rgba(0,0,0,0.15),rgba(0,0,0,0));
        }
        .forgot-password
        {
            text-decoration: none;
            color: #888;
        }
        .forgot-password:hover, .forgot-password:focus
        {
            text-decoration: underline;
            color: #666;
        }
        .validation
        {
            float: left;
            margin-top: -24px;
            margin-left: -10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="admin-con">
        <div class="admin-row">
            <div class="panel panel-login shadow-z-1">
                <div class="admin-logo">
                     <img src="images/innerlogo.png" class="img-responsive" alt="logo" /> 

                </div>
                <hr>
                <!--Update Panel-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div id="login-form" role="form" style="display: block;">
                                <div class="form-group">    UserName                                
                                    <asp:TextBox runat="server" class="form-control" ID="txtusername" placeholder="UserName"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtusername"
                                        ErrorMessage="*" class="validation"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group"> Password
                                    <asp:TextBox runat="server" class="form-control" TextMode="Password" ID="txtpassword"
                                        placeholder="Password"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpassword"
                                        ErrorMessage="*" class="validation"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">Admin/User
                                    <asp:DropDownList runat="server" ID="ddl_Admin_Agent"  >
                                        <asp:ListItem>Admin</asp:ListItem>
                                        <asp:ListItem>User</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group text-center">
                                    <label>
                                        <input type="checkbox" id="showpass">
                                        Show password</label></div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-12" align="center">
                                            <asp:Button runat="server" class="btn btn-primary" Text="Login" ID="btnlogin" OnClick="btnlogin_Click">
                                            </asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="vendor/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script>
        $("#showpass").on("click", function () {
            var type = $("#txtpassword").attr("type");
            if (type == "text")
            { $("#txtpassword").attr("type", "password"); }
            else
            { $("#txtpassword").attr("type", "text"); }
        });
    </script>
</body>
</html>
