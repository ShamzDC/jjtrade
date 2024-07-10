<%@ Page Language="C#" MasterPageFile="../Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="DebitNote.aspx.cs" Inherits="Client.Admin.DebitNote" %>

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
                        <li class="breadcrumb-item"><a href="DebitNote.aspx">Debit Note</a></li>
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
                            <span>Debit Note</span>
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
                    <asp:HiddenField ID="hdnDRCR_ID" runat="server" />

                    <div class="col-md-4 form-group">

                        <asp:Label ID="Label8" runat="server" CssClass="txtbld" Text="Debit/Credit No"> </asp:Label>

                        &nbsp;&nbsp;&nbsp;&nbsp; 
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDebitCreditNo"
    ErrorMessage="Enter Debit/CreditNo" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDebitCreditNo" class="form-control" runat="server"></asp:TextBox>

                    </div>
                    <div class="col-md-4 form-group">

                        <asp:Label ID="Label2" runat="server" CssClass="txtbld" Text="Txrn Type"> </asp:Label>

                        <asp:DropDownList ID="ddlTrxnType" class="form-control"
                            runat="server">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>Debit</asp:ListItem>
                            
                        </asp:DropDownList>

                    </div>
                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label3" runat="server" CssClass="txtbld" Text="Date"> </asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEffectiveDate"
                            runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Enter Date"></asp:RequiredFieldValidator>
                        <div class='input-group date'>
                            <asp:TextBox ID="txtEffectiveDate" runat="server" class="form-control" placeholder="Date"
                                MaxLength="20"></asp:TextBox>
                            <label for='contentbody_txtEffectiveDate' class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </label>
                        </div>
                    </div>
                    <div class="col-md-6 form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="txtbld" Text="Expenses Type"> </asp:Label>
                        
                          <asp:DropDownList ID="ddlExpType" class="form-control"
                              runat="server">
                          </asp:DropDownList>
                    </div>
                    <div class="col-md-6 form-group">
                        <asp:Label ID="Label4" runat="server" CssClass="txtbld" Text="Shipment No"> </asp:Label>
                         
                         <asp:DropDownList ID="ddlShipmentNo" class="form-control"
                             runat="server">
                         </asp:DropDownList>
                    </div>

                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label5" runat="server" CssClass="txtbld" Text="Emp Name"> </asp:Label>
                        <asp:DropDownList ID="ddlEmp" class="form-control"
                        runat="server">
                    </asp:DropDownList>
                          
                    </div>

                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label6" runat="server" CssClass="txtbld" Text="Amount"> </asp:Label>
                         
                                                &nbsp;&nbsp;&nbsp;&nbsp; 
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount"
    ErrorMessage="Enter Amount" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>

      <asp:TextBox ID="txtAmount" placeholder="Enter Amount" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label7" runat="server" CssClass="txtbld" Text="Remarks"> </asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
      <asp:TextBox ID="txtRemarks" placeholder="Enter Remarks" class="form-control" runat="server"></asp:TextBox>
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
                    <asp:GridView ID="GridDrCRNote" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Found"
                        class="table table-bordered table-striped table-hover">
                        <RowStyle CssClass="odd-row" />
                        <AlternatingRowStyle CssClass="even-row" />
                        <Columns>
                            <asp:TemplateField HeaderText="DRCR_ID">
                                <ItemTemplate>
                                    <asp:Label ID="DRCR_ID" runat="server" Text='<%#Eval("DRCR_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DRCR No">
                                <ItemTemplate>
                                    <asp:Label ID="DRCR_No" runat="server" Text='<%#Eval("DRCR_No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trxn Type">
                                <ItemTemplate>
                                    <asp:Label ID="TrxnType" runat="server" Text='<%#Eval("TrxnType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trxn Date">
                                <ItemTemplate>
                                    <asp:Label ID="TrxnDate" runat="server" Text='<%#Eval("TrxnDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Exp Type">
                                <ItemTemplate>
                                    <asp:Label ID="ExpType" runat="server" Text='<%#Eval("ExpType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shipment No">
                                <ItemTemplate>
                                    <asp:Label ID="Shipment_No" runat="server" Text='<%#Eval("Shipment_No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trxn Amt">
                                <ItemTemplate>
                                    <asp:Label ID="TrxnAmt" runat="server" Text='<%#Eval("TrxnAmt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Update">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Imgedit" runat="server" OnClick="Edit" EventArgument='<%# Eval("DRCR_ID") %>'
                                        data-toggle="tooltip" data-placement="left" title="" data-original-title="Update"
                                        OnClientClick="return confirm('Are you sure you want to Edit this record?');"
                                        class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>"></asp:LinkButton>
                                    <asp:LinkButton ID="Imgdelete" runat="server" OnClick="Delete" EventArgument='<%# Eval("DRCR_ID") %>'
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
