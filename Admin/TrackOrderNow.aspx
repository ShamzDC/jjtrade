<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="TrackOrderNow.aspx.cs" Inherits="Client.Admin.TrackOrderNow" %>

<asp:Content ID="TrackorderHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="TrackorderBody" ContentPlaceHolderID="Body" runat="server">
    <!--Section Header-->
    <section class="parent-header">
        <div class="row">
            <div class="grey-container">
                <div class="col-md-6">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#"><i class="fa fa-home" aria-hidden="true"></i>
                            Home</a></li>
                        <li class="breadcrumb-item"><a href="TrackOrderNow.aspx">TrackOrder for OrderNow</a></li>
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
                            <span>TrackOrder for OrderNow</span>
                            <asp:Label ID="lbladmin" runat="server" Text=""></asp:Label>
                        </h3>
                    </div>
                    <div class="form-group ml15">
                        <div class="custom-dropdown">
                        </div>
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
                    <button type="button" id="btnconfirm" runat="server" class="btn btn-primary" onserverclick="btnconfirm_Click">
                        <i class="fa fa-check-circle" aria-hidden="true"></i>Confirmed [<asp:Label ID="lblcount"
                            runat="server"></asp:Label>]</button>
                    <button type="button" id="btnship" runat="server" class="btn btn-primary" onserverclick="btnship_Click">
                        <i class="fa fa-cart-plus" aria-hidden="true"></i>Processed [<asp:Label ID="lblcount1"
                            runat="server"></asp:Label>]</button>
                    <button type="button" id="btndispatch" runat="server" class="btn btn-primary" onserverclick="btndispatch_Click">
                        <i class="fa fa-truck" aria-hidden="true"></i>Dispatched [<asp:Label ID="lblcount2"
                            runat="server"></asp:Label>]</button>
                    <button type="button" id="btndeliver" runat="server" class="btn btn-primary" onserverclick="btndeliver_Click">
                        <i class="fa fa-gift" aria-hidden="true"></i>Delivered [<asp:Label ID="lblcount3"
                            runat="server"></asp:Label>]</button>
                </div>
            </div>
            <asp:Panel ID="PnlControl" runat="server">
                <div class="col-md-12">
                    <asp:GridView ID="Gridordersub" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Bound"
                        class="table table-bordered table-striped table-hover">
                        <Columns>
                            <asp:TemplateField HeaderText="OrderID">
                                <ItemTemplate>
                                    <asp:Label ID="OrderMainID" runat="server" Text='<%#Eval("OrderMainID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="ProductImage">
                                <ItemTemplate>
                                    <asp:Image ID="ProductImage" runat="server" ImageUrl='<%#"~/ItemImage/" +Eval("ImageName") %>'
                                        Height="50" Width="50" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="OrderNumber">
                                <ItemTemplate>
                                    <asp:Label ID="OrderNumber" runat="server" Text='<%#Eval("OrderNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OrderDate">
                                <ItemTemplate>
                                    <asp:Label ID="OrderDate" runat="server" Text='<%#Eval("OrderDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DeliveredDate">
                                <ItemTemplate>
                                    <asp:Label ID="DeliveredDate" runat="server" Text='<%#Eval("ExpectedDeliveryDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemName">
                                <ItemTemplate>
                                    <asp:Label ID="ItemName" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--  <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="Quantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Price">
                                <ItemTemplate>
                                    <asp:Label ID="Price" runat="server" Text='<%#Eval("Price") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                            <asp:TemplateField HeaderText="User_UserID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUser_UserID" runat="server" Text='<%#Eval("User_UserID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TotalAmount">
                                <ItemTemplate>
                                    <asp:Label ID="TotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OrderStatus">
                                <ItemTemplate>
                                    <asp:Label ID="OrderStatus" runat="server" Text='<%#Eval("OrderStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View">
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="updatepanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:ImageButton ID="ImgView" ImageUrl="images/eye.png" AlternateText="View" runat="server"
                                                CommandArgument='<%# Eval("OrderMainID") %>' OnCommand="View" CssClass="viewstsbu" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkorderstatus" onclick="CheckOne(this)"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblId" Text="OrderStatus" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlStatus" class="form-control" runat="server">
                            <asp:ListItem Value="0">--Select Status--</asp:ListItem>
                            <asp:ListItem Value="1">Confirmed</asp:ListItem>
                            <asp:ListItem Value="2">Processed</asp:ListItem>
                            <asp:ListItem Value="3">Dispatched</asp:ListItem>
                            <asp:ListItem Value="4">Delivered</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group" id="divdate" runat="server">
                        <div class='input-group date' id='datetimepicker1'>
                            <asp:TextBox runat="server" class="form-control" ID="txtdeliverydate" placeholder="ExpeDeliveryDate/DeliveryDate/"></asp:TextBox>
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <button type="submit" id="btnSave" runat="server" class="btn btn-primary" usesubmitbehavior="false"
                            onserverclick="btnSave_Click" onclientclick="return validaterequirment()">
                            <i class='fa fa-floppy-o' aria-hidden='true'></i>Save</button>
                        <%--<button type="reset" id="btnClear" runat="server" class="btn btn-primary" onserverclick="btnClear_Click">
                    <i class='fa fa-refresh' aria-hidden='true'></i>Clear</button>--%>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="modal fade editadminres" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <div class="modal-title">
                            Order Details
                        </div>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="updatepanel4" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="form">
                                        <asp:GridView ID="Gridordersub1" AutoGenerateColumns="false" runat="server" EmptyDataText="No Details Found"
                                            class="table table-bordered table-striped table-hover">
                                            <Columns>
                                                <asp:TemplateField HeaderText="OrderID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="OrderMainID" runat="server" Text='<%#Eval("OrderMainID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ProductImage">
                                                    <ItemTemplate>
                                                        <asp:Image ID="ProductImage" runat="server" ImageUrl='<%#"~/ItemImage/" +Eval("ImageName") %>'
                                                            Height="50" Width="50" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OrderNumber">
                                                    <ItemTemplate>
                                                        <asp:Label ID="OrderNumber" runat="server" Text='<%#Eval("OrderNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OrderDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="OrderDate" runat="server" Text='<%#Eval("OrderDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ItemName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ItemName" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Price" runat="server" Text='<%#Eval("Price") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User_UserID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUser_UserID" runat="server" Text='<%#Eval("User_UserID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TotalAmount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OrderStatus">
                                                    <ItemTemplate>
                                                        <asp:Label ID="OrderStatus" runat="server" Text='<%#Eval("OrderStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OrderStatus">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkorderstatus"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--Section Content-->
    <script>
        function CheckOne(obj) {
            var grid = obj.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
            //__doPostBack('Chk_user', '')
        }  
    </script>
    <script type="text/javascript">
        function validaterequirment() {
            if (document.getElementById("<%=ddlStatus.ClientID%>").value == "0") {
                alert("Select OrderStatus");
                document.getElementById("<%=ddlStatus.ClientID%>").focus();
                return false;
            }
        }        
    </script>
</asp:Content>
