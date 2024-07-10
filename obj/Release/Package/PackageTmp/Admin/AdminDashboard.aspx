<%@ Page Title="" Language="C#" MasterPageFile="../Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs"
    Inherits="Client.Admin.AdminDashboard" %>



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
                        <li class="breadcrumb-item"><a href="AdminDashboard.aspx">Admin Dashboard</a></li>
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
                            <span>Admin Dashboard</span>
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
                        <button type="submit" id="btnaud" runat="server"  class="btn btn-primary" onserverclick="btnaud_Click">
                        <i class='fa fa-usd' aria-hidden='true'></i> AUD</button>
                        <button type="reset" id="btninr" runat="server"  class="btn btn-primary" onserverclick="btninr_Click">
                           <i class='fa fa-inr' aria-hidden='true'></i> INR </button>
                       <button type="reset" id="btnlkr" runat="server"  class="btn btn-primary" onserverclick="btnlkr_Click">
                           <i class='fa fa-money' aria-hidden='true'></i> LKR </button>
                    <button type="reset" id="btnusd" runat="server"  class="btn btn-primary" onserverclick="btnusd_Click">
                           <i class='fa fa-usd' aria-hidden='true'></i> USD </button>
                     <button type="reset" id="btnall" runat="server"  class="btn btn-primary" onserverclick="btnall_Click">
                           <i class='fa fa-globe' aria-hidden='true'></i> ALL </button>

                </div>
            </div>


                <div class="col-md-12">

                    <asp:Label ID="LblTotalClosingBal" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>


                   <asp:GridView ID="GridCurrency" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Found" DataKeyNames="PrimaryCurrency"
                        class="table table-bordered table-striped table-hover" ShowHeaderWhenEmpty="true" >
                        <Columns>

                             <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="45px">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agent Name">
                                <ItemTemplate>
                                      <asp:HiddenField ID="hdnGrdAgentID" runat="server"  value='<%#Eval("AgentID") %>' />
                                    <asp:HiddenField ID="hdnGrdAgentPrmCurr" runat="server"  value='<%#Eval("PrimaryCurrency") %>' />
                                    <asp:Label ID="Lblfrom_agent" runat="server" Text='<%#Eval("AgentName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closing Balance">
                                <ItemTemplate>
                                    <asp:Label ID="LblClosingBal" runat="server" Text='<%#Eval("ClosingBal") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Imgedit" runat="server"   OnClick="Edit1" EventArgument='<%# Eval("AgentID")+ "@"+Eval("PrimaryCurrency") %>'

                                        data-toggle="tooltip" data-placement="left" title="" data-original-title="View Agent Transaction"

                                       Text="View Transaction" ></asp:LinkButton>

                                     <%--OnClientClick="return confirm('Are you sure you want to View the agent transaction?');"--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                    </asp:GridView>
                </div>


        </div>
    </section>



</asp:Content>