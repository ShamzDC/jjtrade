<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NonBusinessDayList.aspx.cs" Inherits="Client.Admin.NonBusinessDayList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <script type="text/javascript">
        function validaterequirment() {
            if (document.getElementById("<%=txtholiday.ClientID%>").value == "") {
                alert("Enter HolidayName");
                document.getElementById("<%=txtholiday.ClientID%>").focus();
                return false;
            }
        }
    </script>
    <!--Section Header-->
    <section class="parent-header">
        <div class="row">
            <div class="grey-container">
                <div class="col-md-6">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#"><i class="fa fa-home" aria-hidden="true"></i>
                            Home</a></li>
                        <li class="breadcrumb-item"><a href="NonBusinessDayList.aspx">Non-businessDayList</a></li>
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
                            <span>Non-businessDayList</span>
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
            <asp:Panel ID="pnlControl" runat="server">
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="HolidayID"></asp:Label>
                        <asp:TextBox ID="txtholidayid" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="HolidayDate"></asp:Label>
                           <div class='input-group date'>
                                                    <asp:TextBox ID="txtholidaydate" runat="server" class="form-control" placeholder="HolidayDate"
                                                        MaxLength="20" ></asp:TextBox>
                                                    <label for='contentbody_txtholidaydate' class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </label>
                                                </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="Holiday Name"></asp:Label>
                        <asp:TextBox ID="txtholiday" class="form-control" runat="server"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <asp:Label ID="Label3" Text="IsActive" class="ml05" runat="server"></asp:Label>
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
            <asp:Panel ID="pnlGrid" runat="server">
                <div class="col-md-12">
                    <asp:GridView ID="GridHoliday" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover">
                        <Columns>
                            <asp:TemplateField HeaderText="HolidayID">
                                <ItemTemplate>
                                    <asp:Label ID="HolidayID" runat="server" Text='<%#Eval("HolidayID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          <asp:TemplateField HeaderText="HolidayDate">
                        <ItemTemplate>
                            <asp:Label ID="HolidayDate" runat="server" Text='<%#Convert.ToDateTime(Eval("HolidayDate")).ToString("dd/MM/yyyy")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HolidayName">
                        <ItemTemplate>
                            <asp:Label ID="HolidayName" runat="server" Text='<%#Eval("HolidayName") %>'></asp:Label>
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
                                    <asp:LinkButton ID="Imgedit" runat="server" OnClick="Edit" EventArgument='<%# Eval("HolidayID") %>'
                                        data-toggle="tooltip" data-placement="left" title="" data-original-title="Update"
                                        OnClientClick="return confirm('Are you sure you want to Edit this record?');"
                                        class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>"></asp:LinkButton>
                                    <asp:LinkButton ID="Imgdelete" runat="server" OnClick="Delete" EventArgument='<%# Eval("HolidayID") %>'
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
