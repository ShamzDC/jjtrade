<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="Area.aspx.cs" Inherits="Client.Admin.District" %>

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
                        <li class="breadcrumb-item"><a href="District.aspx">District</a></li>
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
                            <span>District</span>
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
                        causesvalidation="true">
                        <i class="fa fa-plus" aria-hidden="true"></i>Add</button>
                    <button type="button" id="btnModify" runat="server" class="btn btn-primary" onserverclick="btnModify_Click"
                        causesvalidation="false">
                        <i class="fa fa-eye" aria-hidden="true"></i>View Grid</button>
                </div>
            </div>
            <asp:Panel ID="pnlControl" runat="server">
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="DistrictID"></asp:Label>
                        <asp:TextBox ID="txtdistrictid" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="StateName">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlstate"
                                ErrorMessage="Select State" InitialValue="0"></asp:RequiredFieldValidator></asp:Label>
                        <asp:DropDownList ID="ddlstate" class="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="DistrictName">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdistrictname"
                                ErrorMessage="Enter Districtname"></asp:RequiredFieldValidator></asp:Label>
                        <asp:TextBox ID="txtdistrictname" class="form-control" runat="server"></asp:TextBox>
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
                    <asp:GridView ID="GridDistrict" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover">
                        <Columns>
                            <asp:TemplateField HeaderText="DistrictID">
                                <ItemTemplate>
                                    <asp:Label ID="DistrictID" runat="server" Text='<%#Eval("DistrictID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StateName">
                                <ItemTemplate>
                                    <asp:Label ID="StateName" runat="server" Text='<%#Eval("StateName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DistrictName">
                                <ItemTemplate>
                                    <asp:Label ID="DistrictName" runat="server" Text='<%#Eval("DistrictName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Imgedit" runat="server" OnClick="Edit" EventArgument='<%# Eval("DistrictID") %>'
                                        data-toggle="tooltip" data-placement="left" title="" data-original-title="Update"
                                        OnClientClick="return confirm('Are you sure you want to Edit this record?');"
                                        class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>"></asp:LinkButton>
                                    <asp:LinkButton ID="Imgdelete" runat="server" OnClick="Delete" EventArgument='<%# Eval("DistrictID") %>'
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
</asp:Content>
