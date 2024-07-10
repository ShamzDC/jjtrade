<%@ Page Title="" Language="C#" MasterPageFile="../Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="RptBilling.aspx.cs"
    Inherits="Client.Admin.RptBilling" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <section class="parent-header">
        <div class="row">
            <div class="grey-container">
                <div class="col-md-6">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#"><i class="fa fa-home" aria-hidden="true"></i>
                            Home</a></li>
                        <li class="breadcrumb-item"><a href="BillingDashboard.aspx">Billing Dashboard</a></li>
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
                            <span>Billing </span>
                            <asp:Label ID="lbladmin" runat="server" Text=""></asp:Label>
                        </h3>
                    </div>
                </div>
                <hr />
            </div>
        </div>
    </section>

    <section class="page-white">
        <div class="row"> 
            <div class="col-md-12">
                <div class="col-md-2">

                    <asp:Label ID="Label4" runat="server" Text="Report Based on">
                    </asp:Label>


                    <div class='input-group date'>
                        <asp:DropDownList ID="ddlReportType" class="form-control"
                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="CustomerWise">Customer Wise</asp:ListItem>
                            <asp:ListItem Value="PackageNoWise">PackageNo Wise</asp:ListItem>
                            <asp:ListItem Value="ProductNameWise">ProductName Wise</asp:ListItem>
                            <asp:ListItem Value="DateWise">Date Wise</asp:ListItem>
                            <asp:ListItem Value="AllData">All Data</asp:ListItem>
                        </asp:DropDownList>


                    </div>

                </div>

                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" Text="Shipment No">
                    </asp:Label>


                    <div class='input-group date'>
                        <asp:DropDownList ID="ddlSelectValue" class="form-control"
                            runat="server">
                        </asp:DropDownList>


                    </div>

                </div>

                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" Text="From Date">
                    </asp:Label> 
                    <div class='input-group date'>
                        <asp:TextBox ID="txtFromDate" runat="server" class="form-control" placeholder="From Date"
                            MaxLength="20"></asp:TextBox>
                        <label for='contentbody_txtFromDate' class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </label>
                    </div>

                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label6" runat="server" Text="To Date">
                    </asp:Label>


                    <div class='input-group date'>
                        <asp:TextBox ID="txtToDate" runat="server" class="form-control" placeholder="To Date"
                            MaxLength="20"></asp:TextBox>
                        <label for='contentbody_txtToDate' class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </label>
                    </div>

                </div> 

            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <button type="button" id="btnAdd" runat="server" class="btn btn-primary" onserverclick="btnAdd_Click" causesvalidation="false">
                        <i class="fa fa-plus" aria-hidden="true"></i>Add</button>
                    <button type="button" id="btnModify" runat="server" class="btn btn-primary" onserverclick="btnModify_Click" causesvalidation="false">
                        <i class="fa fa-eye" aria-hidden="true"></i>View Grid</button>
                    <button type="button" id="Button1" runat="server" class="btn btn-primary" onserverclick="btnDownload_Click"
                        causesvalidation="false">
                        <i class="fa fa-download" aria-hidden="true"></i>DownLoad</button>

                </div>
            </div>


            <div class="col-md-12"> 

                <asp:GridView ID="GrdBilling" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Found"  
                    class="table table-bordered table-striped table-hover" ShowHeaderWhenEmpty="true" >
                    <Columns>

                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="45px">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Package No">
                            <ItemTemplate>

                                <asp:Label ID="LblPackage" runat="server" Text='<%#Eval("Package_No") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Name">
                            <ItemTemplate>
                                <asp:Label ID="LblProductName" runat="server" Text='<%#Eval("PRODUCT_NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shop Name">
                            <ItemTemplate>
                                <asp:Label ID="LblProductName" runat="server" Text='<%#Eval("PRODUCT_NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:Label ID="LblQty" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="LblUnit" runat="server" Text='<%#Eval("Unit_Type_Value") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Weight">
                            <ItemTemplate>
                                <asp:Label ID="LblWeight" runat="server" Text='<%#Eval("Gross_weight") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="LblRate" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>




                    </Columns>
                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                </asp:GridView>
            </div>

        </div>
    </section>
</asp:Content>
