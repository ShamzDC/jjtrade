<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="TransactionPosting.aspx.cs"
    Inherits="Client.Admin.TransactionPosting" %>
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
                        <li class="breadcrumb-item"><a href="Currency.aspx">Debit,Credit Note</a></li>
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
                                ID="lblUser" class="ml05" ForeColor="Red" Font-Bold="true" value="Welcome"></asp:Label>
                        </p>
                        <p class="txt-darkgrey">
                            [ If From Currency Unit Value >= To Currency Unit Value then Converted Amt= Trxn Amt * To Currency Unit Value else  Converted Amt= Trxn Amt / To Currency Unit Value ] 
                        </p>
                        <h3 class="page-header txt-darkgrey">
                            <span>Debit,Credit Note</span>
                            <asp:Label ID="lbladmin" runat="server" Text=""></asp:Label>
                       <p>
                           
                    <asp:Label ID="lblDebit" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                    <asp:Label ID="lblCredit" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>    </p>
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
                      <asp:Panel ID="pnlButton_Post" runat="server">
                    <button type="submit" id="btnSave" runat="server" class="btn btn-primary" onserverclick="btnSave_Click"
                        onclientclick="return validaterequirment(); ">  </button>
                    <button type="reset" id="btnClear" runat="server" class="btn btn-primary" onserverclick="btnClear_Click" causesvalidation="false">
                        <i class='fa fa-refresh' aria-hidden='true'></i>Clear</button>
                    <button type="button" id="btnDownload" runat="server" class="btn btn-primary" onserverclick="btnDownload_Click" causesvalidation="false">
                        <i class="fa fa-download" aria-hidden="true"></i>DownLoad</button>
                           <button type="button" id="btnStatusReport" runat="server" class="btn btn-primary" onserverclick="btnStatusReport_Click" causesvalidation="false">
                        <i class="fa fa-binoculars" aria-hidden="true"></i>Status Report</button>

                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEffectiveDate"
                        runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Enter Date"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCustomerName"
                        runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Enter CustomerName"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCr_Dr_unit"
                        runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Enter CR / DR Unit"></asp:RequiredFieldValidator>

                           &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red" Font-Bold="true" ControlToValidate="ddlCredit_Debit"
                                ErrorMessage="Select Cr/Dr" InitialValue="0" ></asp:RequiredFieldValidator>

                    </asp:Panel>

                    <asp:Panel ID="pnlButton_Process" runat="server">
                    <button type="submit" id="btnSave_Process" runat="server" class="btn btn-primary" onserverclick="btnSave_Process_ServerClick"
                        onclientclick="return validaterequirment(); ">
                    </button>
                    <button type="reset" id="btnClear_Process" runat="server" class="btn btn-primary" onserverclick="btnClear_Process_ServerClick" causesvalidation="false">
                        <i class='fa fa-refresh' aria-hidden='true'></i>Clear</button>

                         &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtTrxnID"
                        runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Select Trxn"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtProcessEffectiveDate"
                        runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Enter Date"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtToAgentCurrencyPerUnit_DB"
                        runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Per Unit Missing"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtConvertedTrxnAmt"
                        runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Trxn Amt missing"></asp:RequiredFieldValidator>

                         &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Red" Font-Bold="true" ControlToValidate="ddlToAgentSearch"
                                ErrorMessage="Select Agent" InitialValue="0" ></asp:RequiredFieldValidator>

                    </asp:Panel>

                    <asp:Panel ID="PnlStatusReportControl" runat="server">
                         <asp:Label ID="Label2" runat="server" Text="Status">
                        </asp:Label>
                        <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>Completed</asp:ListItem>
                            <asp:ListItem>Pending</asp:ListItem>
                        </asp:DropDownList>
                         <button type="button" id="btnDownloadStatusRpt" runat="server" class="btn btn-primary" onserverclick="btnDownloadStatusRpt_Click" causesvalidation="false">
                        <i class="fa fa-download" aria-hidden="true"></i>DownLoad</button>
                        </asp:Panel>

                </div>
            </div>
            <asp:Panel ID="pnlControl_Post" runat="server">
                <div class="col-md-12">
                    <asp:HiddenField ID="hdnTrxnId" runat="server" />
                    <asp:HiddenField ID="hdnFromAgentPerUnit" runat="server" />

                     <div class="col-md-2">
                        <asp:Label ID="lblTrxnIDNew" runat="server" Text="TrxnID">
                        </asp:Label>
                        <asp:TextBox ID="txtTID"  runat="server" Enabled="false"   ></asp:TextBox>

                    </div>

                    <div class="col-md-2">

                        <asp:Label ID="lblAgentEffDate" runat="server" Text="Effective Date">
                        </asp:Label>
                        <div class='input-group date'>

                            <asp:TextBox ID="txtEffectiveDate" runat="server" class="form-control" placeholder="Date"
                                MaxLength="20"></asp:TextBox>
                            <label for='contentbody_txtEffectiveDate' class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </label>
                        </div>

                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label5" runat="server" Text="Customer Name">
                        </asp:Label>
                        <asp:TextBox ID="txtCustomerName" class="form-control" runat="server"></asp:TextBox>

                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label3" runat="server" Text="Credit /Debit">
                        </asp:Label>
                        <asp:DropDownList ID="ddlCredit_Debit" runat="server" class="form-control">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>Credit</asp:ListItem>
                            <asp:ListItem>Debit</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="CR/DR">
                        </asp:Label>
                        <asp:TextBox ID="txtCr_Dr_unit" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        <asp:Label ID="Label8" runat="server" Text="Notes">
                        </asp:Label>
                        <asp:TextBox ID="txtNotes" class="form-control" runat="server"></asp:TextBox>
                    </div>

                </div>
            </asp:Panel>

            <asp:Panel ID="pnlControl_Process" runat="server">
                <div class="col-md-12">

                    <div class="col-md-2">
                        <asp:Label ID="Label11" runat="server" Text="TrxnID">
                        </asp:Label>
                         <asp:TextBox ID="txtSno"  runat="server" Enabled="false"  Visible="false" ></asp:TextBox>
                        <asp:TextBox ID="txtTrxnID"  runat="server" Enabled="false"   ></asp:TextBox>
                    </div>

                    <div class="col-md-2">

                        <asp:Label ID="Label4" runat="server" Text="Effective Date">
                        </asp:Label>
                        <div class='input-group date'>

                            <asp:TextBox ID="txtProcessEffectiveDate" runat="server" class="form-control" placeholder="Date"
                                MaxLength="10"></asp:TextBox>
                            <label for='contentbody_txtProcessEffectiveDate' class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </label>
                        </div>

                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label6" runat="server" Text="To User">
                        </asp:Label>
                        <asp:DropDownList ID="ddlToAgentSearch" class="form-control" OnSelectedIndexChanged="ddlToAgentSearch_SelectedIndexChanged"
                            runat="server" AutoPostBack="true">
                        </asp:DropDownList>

                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label7" runat="server" Text="UnitValue">
                        </asp:Label>
                        <asp:TextBox ID="txtToAgentCurrencyPerUnit_DB"  runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label9" runat="server" Text="UnitValue-Modify">
                        </asp:Label>
                        <asp:TextBox ID="txtToAgentCurrencyPerUnit" AutoPostBack="true" runat="server"
                            OnTextChanged="txtToAgentCurrency_TextChanged"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        <asp:Label ID="Label10" runat="server" Text="Converted Trxn Amt">
                        </asp:Label>
                        <%--<asp:TextBox ID="txtConvertedTrxnAmt"    runat="server" Enabled="false"></asp:TextBox>--%>

                        <asp:TextBox ID="txtConvertedTrxnAmt" runat="server" AutoPostBack="true"  OnTextChanged="txtConvertedTrxnAmt_TextChanged" ></asp:TextBox>

                    </div>

                </div>
            </asp:Panel>

             <asp:Panel ID="pnlGrid" runat="server">

            <div class="col-md-12">


                <asp:GridView ID="GridCurrency" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Found"
                    class="table table-bordered table-striped table-hover"    AllowSorting="false">
                    <Columns>



                        <%-- <asp:TemplateField HeaderText="S.No">

                                <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                                     <asp:HiddenField ID="hdnsno" runat="server" Value=<%#Container.DataItemIndex+1 %> />

                        </ItemTemplate>

                        </asp:TemplateField>--%>

                         <asp:TemplateField HeaderText="Trxn.ID">
                            <ItemTemplate>
                                <asp:Label ID="lblTrxnIDNew" runat="server" Text='<%#Eval("TrxnID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Effective Date">
                            <ItemTemplate>
                                <asp:Label ID="EffectiveDate" runat="server" Text='<%#Eval("EffectiveDate") %>'></asp:Label>
                                <asp:HiddenField ID="hdnGrdTrxnId" runat="server" Value='<%#Eval("TrxnID") %>' />

                                <asp:HiddenField ID="hdnGrdIsProcessed" runat="server" Value='<%#Eval("IsProcessed") %>' />
                                <asp:HiddenField ID="hdnGrdAgentTime" runat="server" Value='<%#Eval("DiffHrs") %>' />

                                <%--  <asp:HiddenField ID="hdnGrdCredit" runat="server"  value='<%#Eval("Credit_Amt") %>' />
                                    <asp:HiddenField ID="hdnGrdDebit" runat="server"  value='<%#Eval("Debit_Amt") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="From User" >
                            <ItemTemplate>
                                <asp:Label ID="lblFromAgent" runat="server" Text='<%#Eval("AgentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="To User" >
                            <ItemTemplate>
                                <asp:Label ID="lbl_To_Agent" runat="server" Text='<%#Eval("ToAgentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Customer Name">
                            <ItemTemplate>
                                <asp:Label ID="To_Agent" runat="server" Text='<%#Eval("Customer_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Opening Balance">
                            <ItemTemplate>
                                <asp:Label ID="LblopBal" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Debit">
                            <ItemTemplate>
                                <asp:Label ID="LblDebitAmt" runat="server" Text='<%#Eval("Debit_Amt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit">
                            <ItemTemplate>
                                <asp:Label ID="LblCreditAmt" runat="server" Text='<%#Eval("Credit_Amt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Notes">
                            <ItemTemplate>
                                <asp:Label ID="Notes" runat="server" Text='<%#Eval("Notes") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cl.Bal">
                            <ItemTemplate>
                                <asp:Label ID="LblClosingBalance" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="Trxn_Status" runat="server" Text='<%#Eval("Trxn_Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Update/Delete/Process/Move">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                            <ItemTemplate>
                                <asp:LinkButton ID="Imgedit" runat="server" OnClick="Edit1" EventArgument='<%# Eval("TrxnID") %>'
                                     data-placement="left" title="" data-original-title="Update" CausesValidation="false"
                                    class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>&nbsp Update"></asp:LinkButton>

                                <asp:LinkButton ID="Imgdelete" runat="server" OnClick="Delete1" EventArgument='<%# Eval("TrxnID") %>'
                                     data-placement="right" title="" data-original-title="Delete" CausesValidation="false"
                                    OnClientClick="return confirm('Are you sure you want to Delete this record?');"
                                    class="btn btn-danger btn-xs" Text="<i class='fa fa-trash'></i>&nbsp Delete"></asp:LinkButton>

                                <asp:LinkButton ID="ImgProcess" runat="server" OnClick="Process" EventArgument='<%# Eval("TrxnID") %>'
                                    data-placement="left" title="" data-original-title="Process" CausesValidation="false"
                                    class="btn btn-process btn-xs" Text="<i class='fa fa-check' ></i>&nbsp Process"></asp:LinkButton>

                                <asp:LinkButton ID="ImgProcessEdit" runat="server" OnClick="EditProcess"  EventArgument='<%# Eval("TrxnID") %>'
                                    data-placement="left" title="" data-original-title="Process Edit" CausesValidation="false"
                                    class="btn btn-info btn-xs" Text="<i class='fa fa-edit' ></i>&nbsp Edit" ></asp:LinkButton>

                                 <asp:LinkButton ID="ImgReport" runat="server" OnClick="ReportProcess"  EventArgument='<%# Eval("TrxnID") %>'
                                    data-placement="left" title="" data-original-title="View Details" CausesValidation="false"
                                    class="btn btn-view btn-xs" Text="<i class='fa fa-eye' ></i>&nbsp View" ></asp:LinkButton>

                                <asp:LinkButton ID="ImgPendingMove" runat="server" OnClick="MoveToPending"  EventArgument='<%# Eval("TrxnID") %>'
                                    data-placement="left" title="" data-original-title="Move To Pending" CausesValidation="false"
                                    OnClientClick="return confirm('Are you sure you want to Move this record?');"
                                    class="btn btn-view btn-xs" Text="<i class='fas fa-undo' ></i>&nbsp Move" ></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
                 </asp:Panel>
              <asp:Panel ID="pnlReport" runat="server">
                   <button type="button" id="btnGotoProcess" runat="server" class="btn btn-primary" onserverclick="btnGotoProcess_Click"
                        causesvalidation="false">
                        <i class="fa fa-bars" aria-hidden="true"></i> Goto Process</button>

                   <button type="button" id="btnProcessExcel" runat="server" class="btn btn-primary" onserverclick="btnProcessExcel_Click" causesvalidation="false">
                        <i class="fa fa-download" aria-hidden="true"></i>Excel</button>
                   <button type="button" id="btnProcessPDF" runat="server" class="btn btn-primary" onserverclick="btnProcessPDF_Click" causesvalidation="false">
                        <i class="fa fa-print" aria-hidden="true"></i>PDF</button>

                  <div class="col-md-12" id="divreportcontrols" runat="server">

                    <div class="col-md-3">
                        <asp:Label ID="lblTrxnIDRpt" runat="server" Text="TrxnID :" class="alignlabelnew" >
                        </asp:Label> 
                          <asp:Label ID="lblTrxnIDRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                       <div class="col-md-3">
                        <asp:Label ID="lblAgentTrxnDateRpt" runat="server" Text="User Trxn Date :" class="alignlabelnew">
                        </asp:Label> 
                          <asp:Label ID="lblAgentTrxnDateRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                     
                      <div class="col-md-3">
                        <asp:Label ID="lblCustomerNameRpt" runat="server" Text="Customer Name :" class="alignlabelnew">
                        </asp:Label> 
                          <asp:Label ID="lblCustomerNameRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                       <div class="col-md-3">
                        <asp:Label ID="lblTrxnCr_DrRpt" runat="server" Text="Cr/ Dr :" class="alignlabelnew">
                        </asp:Label> 
                          <asp:Label ID="lblTrxnCr_DrRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                      
                       <div class="col-md-3">
                        <asp:Label ID="lblToAgentCurrencyRpt" runat="server" Text="To Currency :" class="alignlabelnew">
                        </asp:Label>  
                          <asp:Label ID="lblToAgentCurrencyRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                       <div class="col-md-3">
                        <asp:Label ID="lblFromAgentRpt" runat="server" Text="From User :" class="alignlabelnew">
                        </asp:Label>
                          <asp:Label ID="lblFromAgentRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                       
                       <div class="col-md-3">
                        <asp:Label ID="lblToAgentRpt" runat="server" Text="To User :" class="alignlabelnew">
                        </asp:Label>  
                          <asp:Label ID="lblToAgentRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                      <div class="col-md-3">
                        <asp:Label ID="lbllblTrxnAmtRpt" runat="server" Text="Trxn Amt :" class="alignlabelnew">
                        </asp:Label>  
                          <asp:Label ID="lbllblTrxnAmtRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                      
                       <div class="col-md-3">
                        <asp:Label ID="lblToCurrency_PerUnitRpt" runat="server" Text="Currency Per Unit :" class="alignlabelnew">
                        </asp:Label>  
                          <asp:Label ID="lblToCurrency_PerUnitRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                       <div class="col-md-3">
                        <asp:Label ID="lblConvertedAmtRpt" runat="server" Text="Converted Amt :" class="alignlabelnew">
                        </asp:Label> 
                          <asp:Label ID="lblConvertedAmtRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>


                       <div class="col-md-3">
                        <asp:Label ID="lblProcessedDateByAdminRpt" runat="server" Text="Processed Date :" class="alignlabelnew">
                        </asp:Label>  
                          <asp:Label ID="lblProcessedDateByAdminRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                      <div class="col-md-3">
                        <asp:Label ID="lblStatusRpt" runat="server" Text="Status :" class="alignlabelnew">
                        </asp:Label> 
                          <asp:Label ID="lblStatusRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                      <div class="col-md-3">
                        <asp:Label ID="lblNotesRpt" runat="server" Text="Notes :" class="alignlabelnew">
                        </asp:Label> 
                          <asp:Label ID="lblNotesRptVal" runat="server" class="alignnew" >
                        </asp:Label>
                    </div>
                  </div>

                  <asp:GridView ID="GvStatus" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover"
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
                        <asp:TemplateField HeaderText="From User">
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

                        <asp:TemplateField HeaderText="To UserCurrency">
                            <ItemTemplate>
                                <asp:Label ID="lblToAgentCurrency" runat="server" Text='<%#Eval("To_Currency") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Per Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblToCurrency_PerUnit" runat="server" Text='<%#Eval("ToCurrency_PerUnit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="ConvertedAmt">
                            <ItemTemplate>
                                <asp:Label ID="lblIsValid" runat="server" Text='<%#Eval("ToAmt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                               <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Trxn_Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Completed Date">
                            <ItemTemplate>
                               <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("ProcessedDateByAdmin") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Notes">
                            <ItemTemplate>
                                <asp:Label ID="Notes" runat="server" Text='<%#Eval("Notes") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
                  </asp:Panel>
        </div>


        <asp:HiddenField ID="hdnFromCurrency" runat="server" />
         <asp:HiddenField ID="hdnToAgentCurr" runat="server" />
         <asp:HiddenField ID="hdnToAgentPerUnt" runat="server" />
        <asp:HiddenField ID="hdnTrxnVal" runat="server" />
        <asp:HiddenField ID="hdnOrigTrxnType" runat="server" />

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

    <script type="text/javascript">
        function validaterequirment() {
            if (document.getElementById("<%=ddlCredit_Debit.ClientID%>").value == "0") {
                alert("Select Cr /Dr ");
                document.getElementById("<%=ddlCredit_Debit.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtCustomerName.ClientID%>").value == "") {
                alert("Please Enter Customer Name");
                document.getElementById("<%=txtCustomerName.ClientID%>").focus();
                return false;
            }

        }
        function ValidatePhoneNo() {
            if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 43 || event.keyCode == 32)
                return event.returnValue;
            return event.returnValue = '';
        }
    </script>

</asp:Content>