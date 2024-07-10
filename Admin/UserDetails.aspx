<%@ Page Title="" Language="C#" MasterPageFile="../Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="UserDetails.aspx.cs" Inherits="Client.Admin.UserDetails" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <!--Section Header-->
    <section class="parent-header">
        <div class="row">
            <div class="grey-container">
                <div class="col-md-6">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#"><i class="fa fa-home" aria-hidden="true"></i>
                            Home</a></li>
                        <li class="breadcrumb-item"><a href="UserDetails.aspx">User Details</a></li>
                    </ol>
                </div>
                <div class="col-md-6">
                    <div class="pull-right widget">
                        <a href="#" id="panel-fullscreen-ic" role="button" data-toggle="tooltip" data-placement="bottom"
                            title="Toggle fullscreen"><i class="fa fa-arrows-alt" aria-hidden="true"></i>
                        </a>
                    </div>
                </div>
            </div>
            <div class="col-lg-12">
                <div class="form-inline">
                    <div class="form-group">
                        <p class="txt-darkgrey">
                            Welcome<i class="fa fa-user ml1" aria-hidden="true"></i><asp:Label runat="server"
                                ID="lblUser" class="ml05" value="Welcome"></asp:Label></p>
                        <h3 class="page-header txt-darkgrey">
                            <span>User Details/ Credential Update</span>
                            <asp:Label ID="lbladmin" runat="server" Text=""></asp:Label>
                        </h3>
                    </div>
                    
                </div>
                <hr />
            </div>
        </div>
    </section>
    <!--Section Header-->
    <!--Section Content-->
    <section class="page-white">
        <div class="row">
          
            
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="txtbld" Text="Employee"></asp:Label>
                         <asp:DropDownList ID="ddlEmpID" class="form-control"
      runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpID_SelectedIndexChanged" >
  </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" CssClass="txtbld" Text="User Name"> </asp:Label>
                          &nbsp;&nbsp;&nbsp;&nbsp;  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="Enter New User Name" ForeColor="Red" Font-Bold="true" ></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtUserName" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" CssClass="txtbld" Text="New Password"> </asp:Label>
                         &nbsp;&nbsp;&nbsp;&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Enter New Password" ForeColor="Red" Font-Bold="true"  ></asp:RequiredFieldValidator> 
                        <asp:TextBox ID="txtPassword" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    

                    

                    <div class="form-group">
                        <button type="submit" id="btnSave" runat="server" class="btn btn-primary btn_radius" onserverclick="btnSave_Click">
                             <i class='fa fa-save' aria-hidden='true'></i> Update</button>
                        
                        <button type="reset" id="btnClear" runat="server" class="btn btn-primary btn_radius" onserverclick="btnClear_Click"
                            causesvalidation="false">
                            <i class='fa fa-refresh' aria-hidden='true'></i> Clear</button>
                    </div>
                </div>
          
               
        </div>
    </section>

       <style>
        table.pagination > tbody > tr > td:first-child > a
        {
            margin-left: 0;
        }
        table.pagination > tbody > tr > td > a
        {
            position: relative;
            float: left;
            margin: 0px 15px;
            padding: 6px 12px;
            margin-left: -1px;
            line-height: 1.42857143;
            color: #212121;
            text-decoration: none;
            background-color: #fff;
            border: 1px solid #fff;
        }
        
        table.pagination > tbody > tr > td > a:hover, table.pagination > tbody > tr > td > a:active, table.pagination > tbody > tr > td > a:focus
        {
            z-index: 2;
            color: #212121;
            background-color: #60bb46;
            border-color: #60bb46;
            color: White;
            cursor: pointer;
        }
        table.pagination > tbody > tr > td > a.active
        {
            background-color: #eee;
            border-color: #ddd;
        }
        
        table#BodyContent_rptPaging > tbody > tr > td:first-child > a
        {
            margin-left: 0;
        }
        table#BodyContent_rptPaging > tbody > tr > td > a
        {
            position: relative;
            float: left;
            margin: 0px 15px;
            padding: 6px 12px;
            margin-left: -1px;
            line-height: 1.42857143;
            color: #212121;
            text-decoration: none;
            background-color: #dddddd;
            border: 1px solid #ddd;
        }
        
        table#BodyContent_rptPaging > tbody > tr > td > a:hover, table.pagination > tbody > tr > td > a:active, table.pagination > tbody > tr > td > a:focus
        {
            z-index: 2;
            color: #212121;
            background-color: #60bb46;
            border-color: #60bb46;
            cursor: pointer;
        }
        table#BodyContent_rptPaging > tbody > tr > td > a.active
        {
            background-color: #eee;
            border-color: #ddd;
        }
        tbody
        {
            background-color: #eeeeee;
        }
    </style>

</asp:Content>
