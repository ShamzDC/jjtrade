<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMaster.Master"
    CodeBehind="ItemMasterBck.aspx.cs" Inherits="KingoClient.Admin.ItemMaster" %>

<asp:Content ID="ItemHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ItemBody" ContentPlaceHolderID="Body" runat="server">
    <!--Section Header-->
    <section class="parent-header">
        <div class="row">
            <div class="grey-container">
                <div class="col-md-6">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#"><i class="fa fa-home" aria-hidden="true"></i>
                            Home</a></li>
                        <li class="breadcrumb-item"><a href="ItemMaster.aspx">Item Master</a></li>
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
                            <span>Item Master</span>
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
                        <asp:Label ID="lblId" Text="ItemId" runat="server"> </asp:Label>
                        <asp:TextBox ID="txtItemId" runat="server" class="form-control" readonly ="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCode" Text="ItemCode" runat="server">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtitemcode"
                                ErrorMessage="Enter ItemCode"></asp:RequiredFieldValidator></asp:Label>
                        <asp:TextBox ID="txtitemcode" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblitemName" Text="ItemName" runat="server">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemName"
                                ErrorMessage="Enter ItemName"></asp:RequiredFieldValidator></asp:Label>
                        <asp:TextBox ID="txtItemName" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" Text="CommonDescription" runat="server"></asp:Label>
                        <asp:TextBox ID="txtdescription" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <asp:Label ID="Label3" Text="HoursBefore" runat="server"></asp:Label>
                        <asp:TextBox ID="txtHr" runat="server"  class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label1" Text="IsActive" class="ml05" runat="server"></asp:Label>
                        <asp:CheckBox Checked="true" ID="chkStatus" runat="server" />
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
                    <asp:GridView ID="GridItem" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Bound"
                        class="table table-bordered table-striped table-hover">
                        <Columns>
                            <asp:TemplateField HeaderText="ItemId">
                                <ItemTemplate>
                                    <asp:Label ID="ItemId" runat="server" Text='<%#Eval("ItemId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemCode">
                                <ItemTemplate>
                                    <asp:Label ID="ItemCode" runat="server" Text='<%#Eval("ItemCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemName">
                                <ItemTemplate>
                                    <asp:Label ID="ItemName" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="Description" runat="server" Text='<%#Eval("CommonDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Update">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Imgedit" runat="server" OnClick="Edit" EventArgument='<%# Eval("ItemId") %>'
                                        data-toggle="tooltip" data-placement="left" title="" data-original-title="Update"
                                        OnClientClick="return confirm('Are you sure you want to Edit this record?');"
                                        class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>"></asp:LinkButton>
                                    <asp:LinkButton ID="Imgdelete" runat="server" OnClick="Delete" EventArgument='<%# Eval("ItemId") %>'
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
