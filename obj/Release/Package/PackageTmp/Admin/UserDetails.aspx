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
                            <span>User Details</span>
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
            <div class="col-md-12">
                <div class="form-group">
                    <button type="button" id="btnAdd" runat="server" class="btn btn-primary" onserverclick="btnAdd_Click"
                        causesvalidation="false">
                        <i class="fa fa-plus" aria-hidden="true"></i>Add</button>
                    <button type="button" id="btnModify" runat="server" class="btn btn-primary" onserverclick="btnModify_Click"
                        causesvalidation="false">
                        <i class="fa fa-eye" aria-hidden="true"></i>View Grid</button>
                     <button type="button" id="btnDownload" runat="server" class="btn btn-primary" onserverclick="btnDownload_Click"  causesvalidation="false">
                        <i class="fa fa-download" aria-hidden="true"></i>DownLoad</button> 
                </div>
            </div>
            <asp:Panel ID="pnlControl" runat="server">
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="UserID"></asp:Label>
                        <asp:TextBox ID="txtUserID" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="UserName"> </asp:Label>
                          &nbsp;&nbsp;&nbsp;&nbsp;  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="Enter User Name" ForeColor="Red" Font-Bold="true" ></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtUserName" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Pwd"> </asp:Label>
                         &nbsp;&nbsp;&nbsp;&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Enter Password" ForeColor="Red" Font-Bold="true"  ></asp:RequiredFieldValidator> 
                        <asp:TextBox ID="txtPassword" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="Admin/Agent"></asp:Label>
                         <asp:RadioButtonList ID="RD_Admin_Agent" runat="server" RepeatDirection="Horizontal" 
                              >
                            
                              <asp:ListItem>Admin</asp:ListItem>
                              <asp:ListItem>Employee</asp:ListItem>
                         </asp:RadioButtonList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server" Text="IsActive"></asp:Label>
                         <asp:RadioButtonList ID="RD_IsActive" runat="server" RepeatDirection="Horizontal">
                             <asp:ListItem>Yes</asp:ListItem>
                              <asp:ListItem>No</asp:ListItem>
                         </asp:RadioButtonList>
                    </div>

                    <div class="form-group" id="divCurrency" runat="server" visible="false">
                        <asp:Label ID="Label7" runat="server" Text="PrimaryCurrency"> </asp:Label>
                        <asp:DropDownList ID="ddlPrimaryCurrency" runat="server" > </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <button type="submit" id="btnSave" runat="server" class="btn btn-primary" onserverclick="btnSave_Click">
                        </button>
                        <button type="reset" id="btnClear" runat="server" class="btn btn-primary" onserverclick="btnClear_Click"
                            causesvalidation="false">
                            <i class='fa fa-refresh' aria-hidden='true'></i>Clear</button>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlGrid" runat="server">
                <div class="col-md-12">
                    <asp:GridView ID="GirdUserDetails" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover">
                        <Columns>
                            <asp:TemplateField HeaderText="UserID">
                                <ItemTemplate>
                                    <asp:Label ID="UserID" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User name">
                                <ItemTemplate>
                                    <asp:Label ID="Username" runat="server" Text='<%#Eval("Username") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Password">
                                <ItemTemplate>
                                    <asp:Label ID="Pwd" runat="server" Text='<%#Eval("Pwd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agent/Admin">
                                <ItemTemplate>
                                    <asp:Label ID="IsAgent_Admin" runat="server" Text='<%#Eval("IsAgent_Admin") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IsActive">
                                <ItemTemplate>
                                    <asp:Label ID="IsActive" runat="server" Text='<%#Eval("IsActive") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Update">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Imgedit" runat="server" OnClick="Edit" EventArgument='<%# Eval("UserID") %>'
                                        data-toggle="tooltip" data-placement="left" title="" data-original-title="Update"
                                        OnClientClick="return confirm('Are you sure you want to Edit this record?');"
                                        class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>"></asp:LinkButton>
                                    <asp:LinkButton ID="Imgdelete" runat="server" OnClick="Delete" EventArgument='<%# Eval("UserID") %>'
                                        data-toggle="tooltip" data-placement="right" title="" data-original-title="Delete"
                                        OnClientClick="return confirm('Are you sure you want to Delete this record?');"
                                        class="btn btn-danger btn-xs" Text="<i class='fa fa-trash'></i>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                

            </asp:Panel>
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
