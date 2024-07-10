<%@ Page Title="" Language="C#" MasterPageFile="../Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BillCollection.aspx.cs"
    Inherits="Client.Admin.BillCollection" %>



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
                            <span>Billing Collection</span>
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
                <div class="form-group">
                    <button type="button" id="btnAdd" runat="server" class="btn btn-default btn_radius" onserverclick="btnAdd_Click" causesvalidation="false">
                        <i class="fa fa-plus" aria-hidden="true"></i> Add</button>
                    <button type="button" id="btnModify" runat="server" class="btn btn-default btn_radius" onserverclick="btnModify_Click" causesvalidation="false">
                        <i class="fa fa-eye" aria-hidden="true"></i> View Grid</button>
                    <button type="button" id="Button1" runat="server" class="btn btn-default btn_radius" onserverclick="btnDownload_Click"
                        causesvalidation="false">
                        <i class="fa fa-download" aria-hidden="true"></i> DownLoad</button>
                </div>
            </div>



            <asp:Panel ID="pnlControl" runat="server">
                <div class="col-md-12">
                    <div class="col-md-3 form-group">
                        <asp:HiddenField ID="hdnBillingId" runat="server" />
                        <asp:Label ID="Label4" runat="server" CssClass="txtbld" Text="Receipt No">
                        </asp:Label>

                                                 &nbsp;&nbsp;&nbsp;&nbsp; 
 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReceiptNo"
     ErrorMessage="Enter ReceiptNo" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtReceiptNo" runat="server" class="form-control"
                                MaxLength="20"></asp:TextBox>

                    </div>



                    <div class="col-md-3 form-group">
                        <asp:Label ID="Label2" runat="server" CssClass="txtbld" Text="Receipt Date">
                        </asp:Label>

                        &nbsp;&nbsp;&nbsp;&nbsp; 
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEffectiveDate"
    ErrorMessage="Enter Date" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>

                        <div class='input-group date'>
                            <asp:TextBox ID="txtEffectiveDate" runat="server" class="form-control" placeholder="Date"
                                MaxLength="20"></asp:TextBox>
                            <label for='contentbody_txtEffectiveDate' class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </label>
                         </div>
                    </div>
                    <div class="col-md-3 form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="txtbld" Text="Customer">
                        </asp:Label>

                            <asp:DropDownList ID="ddlCustomer" class="form-control"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                            </asp:DropDownList>

                    </div>

                    <div class="col-md-3 form-group">
                        <asp:Label ID="Label3" runat="server" CssClass="txtbld" Text="Pay To">
                        </asp:Label>

                            <asp:DropDownList ID="ddlPayTo" class="form-control"
                                runat="server">
                            </asp:DropDownList>

                    </div>

                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label5" runat="server" CssClass="txtbld" Text="Mode of Pay">
                        </asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp; 
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtModeofPay"
    ErrorMessage="Enter Mode Of Pay" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtModeofPay" runat="server" class="form-control"
                                MaxLength="20"></asp:TextBox>

                    </div>
                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label6" runat="server" CssClass="txtbld" Text="Remarks">
                        </asp:Label>

                            <asp:TextBox ID="txtRemarks" runat="server" class="form-control"
                                MaxLength="20"></asp:TextBox>

                    </div>
                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label17" runat="server" CssClass="txtbld" Text="Total Amount">
                        </asp:Label>
                                               
                            <asp:TextBox ID="txttotAmount" runat="server" class="form-control"
                                MaxLength="20"></asp:TextBox>
                    </div>

                    <div class="col-md-4 form-group">

                        <button type="submit" id="btnSave" runat="server" class="btn btn-primary btn_radius" onserverclick="btnSave_Click">
                            <i class='fa fa-refresh' aria-hidden='true'></i> Save
                        </button>
                        <button type="reset" id="btnClear" runat="server" class="btn btn-primary btn_radius" onserverclick="btnClear_Click" causesvalidation="false">
                            <i class='fa fa-refresh' aria-hidden='true'></i> Clear</button> 
                    </div>

                </div>

                <div class="col-md-12">
                    <asp:GridView ID="GrdCollection" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Found" DataKeyNames="BHID"
                        class="table table-bordered table-striped table-hover" ShowHeaderWhenEmpty="true" OnRowDataBound="GrdCollection_Add_RowDataBound" >
                        <RowStyle CssClass="odd-row" />
                        <AlternatingRowStyle CssClass="even-row" />
                        <Columns>

                            <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="45px">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Select" HeaderStyle-Font-Bold="true" HeaderStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_Select" runat="server" HeaderText="Select" Width="10px" />
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bill No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnBHID" runat="server" Value='<%#Eval("BHID") %>' />

                                    <asp:Label ID="LblBIll_no" runat="server" Text='<%#Eval("BIll_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Date">
                                <ItemTemplate>
                                    <asp:Label ID="LblBilldate" runat="server" Text='<%#Eval("Bill_date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BillAmt">
                                <ItemTemplate>
                                    <asp:Label ID="LblTotalAmount" runat="server" Text='<%#Eval("Total_Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="AlreadyReceived">
                                <ItemTemplate>
                                    <asp:Label ID="LblAlreadyReceived" runat="server" Text='<%#Eval("AlreadyReceived") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BalanceAmt">
                                <ItemTemplate>
                                    <asp:Label ID="LblBalanceAmt" runat="server" Text='<%#Eval("BalanceAmt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="AmtPaid">
                                <ItemTemplate>

                                    <asp:TextBox ID="txtAmtPaid" runat="server" Text='<%#Eval("CollectedAmt") %>' ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                    </asp:GridView>
                </div>


            </asp:Panel>


            <asp:Panel ID="pnlGrid" runat="server">
                <div class="col-md-12">
                    <asp:GridView ID="GridAllCollection" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Found"
                        class="table table-bordered table-striped table-hover">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="CHID" runat="server" Text='<%#Eval("CHID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ReceiptNo">
                                <ItemTemplate>
                                    <asp:Label ID="ReceiptNo" runat="server" Text='<%#Eval("Receipt_No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receipt Date">
                                <ItemTemplate>
                                    <asp:Label ID="ReceiptDate" runat="server" Text='<%#Eval("Receipt_date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate>
                                    <asp:Label ID="CustomerName" runat="server" Text='<%#Eval("CUSTOMER_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payto">
                                <ItemTemplate>
                                    <asp:Label ID="Payto" runat="server" Text='<%#Eval("Empname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TotalAmt">
                                <ItemTemplate>
                                    <asp:Label ID="TotalAmt" runat="server" Text='<%#Eval("Total_Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Imgedit" runat="server" OnClick="Edit" EventArgument='<%# Eval("CHID") %>'
                                        data-toggle="tooltip" data-placement="left" title="" data-original-title="Update"
                                        OnClientClick="return confirm('Are you sure you want to Edit this record?');"
                                        class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>"></asp:LinkButton>
                                    <asp:LinkButton ID="Imgdelete" runat="server" OnClick="Delete" EventArgument='<%# Eval("CHID") %>'
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
