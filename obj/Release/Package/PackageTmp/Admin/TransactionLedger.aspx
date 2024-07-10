<%@ Page Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="TransactionLedger.aspx.cs" Inherits="Client.Admin.TransactionLedger" %>

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
                        <li class="breadcrumb-item"><a href="Currency.aspx">Debit,Credit Note Details</a></li>
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
                            <span>Debit,Credit Note Details</span>
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
                     <asp:Label ID="Label5" runat="server" Text="To User">
                        </asp:Label>
                        <asp:DropDownList ID="ddlToAgent" runat="server" > </asp:DropDownList>
                    <button type="button" id="btnModify" runat="server" class="btn btn-primary" onserverclick="btnModify_Click">
                        <i class="fa fa-eye" aria-hidden="true"></i>View Grid</button>
                     <button type="button" id="btnDownload" runat="server" class="btn btn-primary" onserverclick="btnDownload_Click" causesvalidation="false">
                        <i class="fa fa-download" aria-hidden="true"></i>DownLoad</button>

                </div>
            </div>

                <div class="col-md-12">

                    <div class="form-group">


                      <asp:GridView ID="GvStatusLedger" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover"
                     ShowHeaderWhenEmpty="true" DataKeyNames="TrxnID"  EmptyDataText="No Data Found"   >
                    <Columns>

                         <asp:TemplateField HeaderText="TrxnID">
                            <ItemTemplate>
                                <asp:Label ID="lblTrxnID" runat="server" Text='<%#Eval("TrxnID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="User Trxn Date">
                            <ItemTemplate>
                                <asp:Label ID="lblAdminEffectiveDate" runat="server" Text='<%#Eval("Agent_EffectiveDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From user">
                            <ItemTemplate>
                                <asp:Label ID="lblFromAgent" runat="server" Text='<%#Eval("FromAgentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CustomerName">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomer_Name" runat="server" Text='<%#Eval("Customer_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cr / Dr">
                            <ItemTemplate>
                                <asp:Label ID="lblTrxnCr_Dr" runat="server" Text='<%#Eval("Trxn_CR_DR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trxn Amt">
                            <ItemTemplate>
                                <asp:Label ID="lblTrxnAmt" runat="server" Text='<%#Eval("FromAmt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                       <asp:TemplateField HeaderText="To User">
                            <ItemTemplate>
                                <asp:Label ID="lblToAgent" runat="server" Text='<%#Eval("ToAgentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="To User Currency">
                            <ItemTemplate>
                                <asp:Label ID="lblToAgentCurrency" runat="server" Text='<%#Eval("To_Currency") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Per Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblToCurrency_PerUnit" runat="server" Text='<%#Eval("ToCurrency_PerUnit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Converted Amt">
                            <ItemTemplate>
                                <asp:Label ID="lblIsValid" runat="server" Text='<%#Eval("ToAmt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                               <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Trxn_Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Notes">
                            <ItemTemplate>
                                <asp:Label ID="Notes" runat="server" Text='<%#Eval("Notes") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Completed Date">
                            <ItemTemplate>
                               <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("ProcessedDateByAdmin") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

                    </div>



                </div>

        </div>
    </section>
</asp:Content>