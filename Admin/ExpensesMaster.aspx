<%@ Page Language="C#" MasterPageFile="../Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ExpensesMaster.aspx.cs" Inherits="Client.Admin.ExpensesMaster" %>

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
                        <li class="breadcrumb-item"><a href="ExpensesMaster.aspx">Expenses Master</a></li>
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
                                ID="lblUser" class="ml05" value="Welcome"></asp:Label>
                        </p>
                        <h3 class="page-header txt-darkgrey">
                            <span>Expenses Master</span>
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
                    <button type="button" id="btnAdd" runat="server" class="btn btn-default btn_radius" onserverclick="btnAdd_Click" causesvalidation="false">
                        <i class="fa fa-plus" aria-hidden="true"></i> Add</button>
                    <button type="button" id="btnModify" runat="server" class="btn btn-default btn_radius" onserverclick="btnModify_Click" causesvalidation="false">
                        <i class="fa fa-eye" aria-hidden="true"></i> View Grid</button>
                    <button type="button" id="btnDownload" runat="server" class="btn btn-default btn_radius" onserverclick="btnDownload_Click"
                        causesvalidation="false">
                        <i class="fa fa-download" aria-hidden="true"></i> DownLoad</button>
                </div>
            </div>
            <asp:Panel ID="pnlControl" runat="server">
                <div class="col-md-12">
                    <asp:HiddenField ID="hdnExpId" runat="server" />
                    
                    <div class="col-md-12 form-group">
                        <asp:Label ID="Label3" runat="server" CssClass="txtbld" Text="Expenses Details"> </asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtExpensesName"
                            runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Enter Expenses Details"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtExpensesName" placeholder="Expenses Name" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    

                    <div class="col-md-4 form-group">
                        <button type="submit" id="btnSave" runat="server" class="btn btn-primary btn_radius" onserverclick="btnSave_Click">
                        </button>
                        <button type="reset" id="btnClear" runat="server" class="btn btn-primary btn_radius" onserverclick="btnClear_Click" causesvalidation="false">
                            <i class='fa fa-refresh' aria-hidden='true'></i> Clear</button>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlGrid" runat="server">
                <div class="col-md-12">
                    <asp:GridView ID="GridExpenses" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Found"
                        class="table table-bordered table-striped table-hover">
                        <RowStyle CssClass="odd-row" />
                        <AlternatingRowStyle CssClass="even-row" />
                        <Columns>
                            <asp:TemplateField HeaderText="Exp ID">
                                <ItemTemplate>
                                    <asp:Label ID="ExpTypeID" runat="server" Text='<%#Eval("ExpTypeID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expenses Details">
                                <ItemTemplate>
                                    <asp:Label ID="ExpensesType" runat="server" Text='<%#Eval("ExpType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                            <asp:TemplateField HeaderText="Update">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Imgedit" runat="server" OnClick="Edit" EventArgument='<%# Eval("ExpTypeID") %>'
                                        data-toggle="tooltip" data-placement="left" title="" data-original-title="Update"
                                        OnClientClick="return confirm('Are you sure you want to Edit this record?');"
                                        class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>"></asp:LinkButton>
                                    <asp:LinkButton ID="Imgdelete" runat="server" OnClick="Delete" EventArgument='<%# Eval("ExpTypeID") %>'
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
        table.pagination > tbody > tr > td:first-child > a {
            margin-left: 0;
        }

        table.pagination > tbody > tr > td > a {
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

            table.pagination > tbody > tr > td > a:hover, table.pagination > tbody > tr > td > a:active, table.pagination > tbody > tr > td > a:focus {
                z-index: 2;
                color: #212121;
                background-color: #60bb46;
                border-color: #60bb46;
                color: White;
                cursor: pointer;
            }

            table.pagination > tbody > tr > td > a.active {
                background-color: #eee;
                border-color: #ddd;
            }

        table#BodyContent_rptPaging > tbody > tr > td:first-child > a {
            margin-left: 0;
        }

        table#BodyContent_rptPaging > tbody > tr > td > a {
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

            table#BodyContent_rptPaging > tbody > tr > td > a:hover, table.pagination > tbody > tr > td > a:active, table.pagination > tbody > tr > td > a:focus {
                z-index: 2;
                color: #212121;
                background-color: #60bb46;
                border-color: #60bb46;
                cursor: pointer;
            }

            table#BodyContent_rptPaging > tbody > tr > td > a.active {
                background-color: #eee;
                border-color: #ddd;
            }

        tbody {
            background-color: #eeeeee;
        }
    </style>

</asp:Content>
