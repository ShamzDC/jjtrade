<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="Admin.aspx.cs" Inherits="Client.Admin.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <!--Section Header-->
    <section class="parent-header">
        <div class="row">
            <div class="grey-container">
                <div class="col-md-6">
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
                            <span>Admin</span>
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
                </div>
            </div>
            <asp:Panel ID="PnlControl" runat="server">
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblId" Text="AdminID" runat="server"> </asp:Label>
                        <asp:TextBox ID="txtadminid" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <span style="color: Red; font-weight: bold">*</span>
                        <asp:Label ID="lblCode" Text="AdminName" runat="server">
                        </asp:Label>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txtadminname" ErrorMessage="Enter AdminName"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtadminname" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <span style="color: Red; font-weight: bold">*</span>
                        <asp:Label ID="lblitemName" Text="Username" runat="server">
                        </asp:Label>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txtusername" ErrorMessage="Enter Username"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtusername" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <span style="color: Red; font-weight: bold">*</span>
                        <asp:Label ID="Label2" Text="Password" runat="server">
                        </asp:Label>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ControlToValidate="txtpassword" ErrorMessage="Enter Password"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtpassword" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label1" Text="EmailID" runat="server"></asp:Label>
                        <asp:TextBox ID="txtemail" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" Text="PhoneNo" runat="server"></asp:Label>
                        <asp:TextBox ID="txtphone" runat="server" class="form-control"></asp:TextBox>
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
            <asp:Panel ID="PnlGrid" runat="server">
                <div class="col-md-12">
                    <asp:GridView ID="GridAdmin" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Bound"
                        class="table table-bordered table-striped table-hover">
                        <Columns>
                            <asp:TemplateField HeaderText="AdminID">
                                <ItemTemplate>
                                    <asp:Label ID="AdminID" runat="server" Text='<%#Eval("adminid") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AdminName">
                                <ItemTemplate>
                                    <asp:Label ID="AdminName" runat="server" Text='<%#Eval("adminname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Username">
                                <ItemTemplate>
                                    <asp:Label ID="Username" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Password">
                                <ItemTemplate>
                                    <asp:Label ID="Password" runat="server" Text='<%#Eval("password") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Imgedit" runat="server" OnClick="Edit" EventArgument='<%# Eval("adminid") %>'
                                        data-toggle="tooltip" data-placement="left" title="" data-original-title="Update"
                                        OnClientClick="return confirm('Are you sure you want to Edit this record?');"
                                        class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>"></asp:LinkButton>
                                    <asp:LinkButton ID="Imgdelete" runat="server" OnClick="Delete" EventArgument='<%# Eval("adminid") %>'
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
    <!--Section Content-->
</asp:Content>
