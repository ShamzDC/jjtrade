<%@ Page Title="" Language="C#" MasterPageFile="../Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TransactionProcess.aspx.cs"
    Inherits="Client.Admin.TransactionProcess" %>

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
                        <li class="breadcrumb-item"><a href="Currency.aspx">Debit,Credit Note-Process</a></li>
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
                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                            <span style="text-align:center; ">Debit,Credit Note-Process</span>
                            <asp:Label ID="lbladmin" runat="server" Text=""></asp:Label>
                        </p>
                        
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

                    <button type="submit" id="btnSave" runat="server" class="btn btn-primary" onserverclick="btnSave_Click">
                    </button>
                    <button type="reset" id="btnClear" runat="server" class="btn btn-primary" onserverclick="btnClear_Click" causesvalidation="false" >
                        <i class='fa fa-refresh' aria-hidden='true'></i>Clear</button>
                     <button type="button" id="btnDownload" runat="server" class="btn btn-primary" onserverclick="btnDownload_Click"
                        causesvalidation="false">
                        <i class="fa fa-download" aria-hidden="true"></i>DownLoad</button> 
                     &nbsp;&nbsp;&nbsp;&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEffectiveDate"
                                runat="server"  ForeColor="Red" Font-Bold="true" ErrorMessage="Enter Date"></asp:RequiredFieldValidator>

                </div>
            </div>

            <div class="col-md-12">   
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" Text="Process Date">
                    </asp:Label>
                 <asp:HiddenField ID="hdnTrxnId" runat="server"   />

                    <div class='input-group date'>
                        <asp:TextBox ID="txtEffectiveDate" runat="server" class="form-control" placeholder="Date"
                            MaxLength="20"></asp:TextBox>
                        <label for='contentbody_txtEffectiveDate' class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </label>
                    </div>

                </div> 
                 <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="Agent">
                    </asp:Label>
                 

                    <div class='input-group date'>
                        <asp:DropDownList ID="ddlToAgentSearch" class="form-control" 
                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlToAgentSearch_SelectedIndexChanged" ></asp:DropDownList>                                      
                    </div>

                </div> 
                 <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="Trxn Status">
                    </asp:Label>
                 

                    <div class='input-group date'>
                        <asp:DropDownList ID="ddlTrxnStatus" class="form-control" 
                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTrxnStatus_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                             
                             <asp:ListItem>Completed</asp:ListItem>
                        </asp:DropDownList>                                      
                    </div>

                </div> 

            </div>  
              <asp:Panel ID="pnlProcess" runat="server">
            <div class="col-md-12">


                <asp:GridView ID="grd_Add" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" class="table table-bordered table-striped table-hover"
                    EmptyDataText="No Data Found"
                   DataKeyNames="TrxnID" 
                    OnRowDataBound="grd_Add_RowDataBound">
                    

                      

                    <Columns>
                         <asp:TemplateField HeaderText="Select" HeaderStyle-Font-Bold="true" HeaderStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_Select" runat="server" HeaderText="Select" Width="10px" />
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>

                        <asp:TemplateField HeaderText="Effective Date">
                            <ItemTemplate> 
                                <asp:Label ID="lblAgentEffectiveDate" runat="server" Text='<%#Eval("Agent_EffectiveDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From Agent">
                            <ItemTemplate> 
                                <asp:Label ID="lblFromAgent" runat="server" Text='<%#Eval("AgentName") %>'></asp:Label>
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
                                <asp:Label ID="lblTrxnAmt" runat="server" Text='<%#Eval("Trxn_Amt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="To Agent">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlToAgent" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlToAgent_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnGrdFromAgentPerUnit" runat="server" />

                                <asp:HiddenField ID="hdnGrdToAgentCurrency" runat="server" />
                                <asp:HiddenField ID="hdnGrdToAgentPerUnit" runat="server" />

                                <asp:HiddenField ID="hdnGrdTrxnID" runat="server" Value='<%#Eval("TrxnID") %>' />
                                 <asp:HiddenField ID="hdnGrdFromAgentCurrency"   runat="server" Value='<%#Eval("From_Currency") %>' />
                                <asp:HiddenField ID="hdnGrdIsProcessed" runat="server"  value='<%#Eval("IsProcessed") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                       <asp:TemplateField HeaderText="ToAgent-Currency/UnitValue">  
                            <ItemTemplate>
                                <asp:Label ID="lblToAgentCurrency" runat="server" Text='<%#Eval("ToAgentCurrency") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Manual-UnitValue">  
                            <ItemTemplate>
                                 
                                <asp:TextBox ID="txtToAgentCurrencyPerUnit" AutoPostBack="true" Width="70px" runat="server" OnTextChanged="txtToAgentCurrency_TextChanged"  ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField HeaderText="Converted Trxn Amt">
                            <ItemTemplate>
                                <asp:Label ID="lblConvertedTrxnAmt" runat="server" Text='<%#Eval("ConvertedTrxn_Amt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IsValid">  
                            <ItemTemplate>
                                <asp:Label ID="lblIsValid" runat="server" Text='<%#Eval("IsValid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <%-- <asp:TemplateField HeaderText="opening  Balance">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOPBal" runat="server" class="form-control"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                       <%-- <asp:TemplateField HeaderText="Credit Amt">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCreditAmt" runat="server" class="form-control" Enabled="false" Text='<%#Eval("Credit_Amt") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit Amt">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDebitAmt" runat="server" class="form-control" Enabled="false" Text='<%#Eval("Debit_Amt") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <%--  <asp:TemplateField HeaderText="Closing Balance">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtclosingbalance" runat="server" class="form-control"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                     <asp:ListItem>Pending</asp:ListItem>
                                    <asp:ListItem>Completed</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Upd/Del">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                            <ItemTemplate>
                                <asp:LinkButton ID="Imgedit" runat="server" Visible="false" OnClick="Edit1" EventArgument='<%# Eval("TrxnID") %>'
                                    data-toggle="tooltip" data-placement="left" title="" data-original-title="Update"
                                    OnClientClick="return confirm('Are you sure you want to Edit this record?');"
                                    class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>"></asp:LinkButton>
                                <asp:LinkButton ID="Imgdelete" runat="server" OnClick="Delete1" EventArgument='<%# Eval("TrxnID") %>'
                                    data-toggle="tooltip" data-placement="right" title="" data-original-title="Delete"
                                    OnClientClick="return confirm('Are you sure you want to Delete this record?');"
                                    class="btn btn-danger btn-xs" Text="<i class='fa fa-trash'></i>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    
                </asp:GridView>
            </div>
                  </asp:Panel>

             <asp:Panel ID="pnlSearch" runat="server">
            <div class="col-md-12">


                 <asp:GridView ID="GvStatus" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover"
                     ShowHeaderWhenEmpty="true" DataKeyNames="TrxnID"     >
                    <Columns>                       

                        <asp:TemplateField HeaderText="Processed Date">
                            <ItemTemplate> 
                                <asp:Label ID="lblAdminEffectiveDate" runat="server" Text='<%#Eval("ProcessedDateByAdmin") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From Agent">
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
                        <asp:TemplateField HeaderText="From Amt">
                            <ItemTemplate> 
                                <asp:Label ID="lblTrxnAmt" runat="server" Text='<%#Eval("FromAmt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       

                       <asp:TemplateField HeaderText="ToAgent">  
                            <ItemTemplate>
                                <asp:Label ID="lblToAgent" runat="server" Text='<%#Eval("ToAgentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="To AgentCurrency">
                            <ItemTemplate>
                                <asp:Label ID="lblToAgentCurrency" runat="server" Text='<%#Eval("To_Currency") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Per Unit">  
                            <ItemTemplate>
                                <asp:Label ID="lblToCurrency_PerUnit" runat="server" Text='<%#Eval("ToCurrency_PerUnit") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                          <asp:TemplateField HeaderText="ToAmt">  
                            <ItemTemplate>
                                <asp:Label ID="lblIsValid" runat="server" Text='<%#Eval("ToAmt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlStatusSearch" runat="server">
                                    <asp:ListItem>Completed</asp:ListItem>
                                    <asp:ListItem>Pending</asp:ListItem>
                                    
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                       

                        <asp:TemplateField HeaderText="Upd/Del">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                            <ItemTemplate>
                                <asp:LinkButton ID="ImgeditStatus" runat="server"   OnClick="EditStatus" EventArgument='<%# Eval("TrxnID") %>'
                                    data-toggle="tooltip" data-placement="left" title="" data-original-title="Update"
                                    OnClientClick="return confirm('Are you sure you want to Edit this record?');"
                                    class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>"></asp:LinkButton>
                                <asp:LinkButton ID="ImgdeleteStatus" runat="server"  OnClick="Delete1" EventArgument='<%# Eval("TrxnID") %>'
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

          <div class="mb1">
                            <table class="pagination">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbFirst" runat="server" OnClick="lbFirst_Click" CausesValidation="false" >First</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbPrevious" runat="server" OnClick="lbPrevious_Click" CausesValidation="false" >Previous</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand"
                                            OnItemDataBound="rptPaging_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CausesValidation="false"
                                                    CommandName="newPage" Text='<%# Eval("PageText") %> '>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbNext" runat="server" OnClick="lbNext_Click" CausesValidation="false" >Next</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbLast" runat="server" OnClick="lbLast_Click" CausesValidation="false" >Last</asp:LinkButton>
                                    </td>
                                    
                                </tr>
                            </table>
                        </div>
    </section>

      <style>
        table.pagination > tbody > tr > td:first-child > a
        {
            margin-left: 0;
        }
        table.pagination > tbody > tr > td > a
        {
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
        
        table.pagination > tbody > tr > td > a:hover, table.pagination > tbody > tr > td > a:active, table.pagination > tbody > tr > td > a:focus
        {
            z-index: 2;
            color: #212121;
            background-color: #60bb46;
            border-color: #60bb46;
            color: White;
            cursor: pointer;
        }
        table.pagination > tbody > tr > td > a.active
        {
            background-color: #eee;
            border-color: #ddd;
        }
        
        table#BodyContent_rptPaging > tbody > tr > td:first-child > a
        {
            margin-left: 0;
        }
        table#BodyContent_rptPaging > tbody > tr > td > a
        {
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
        
        table#BodyContent_rptPaging > tbody > tr > td > a:hover, table.pagination > tbody > tr > td > a:active, table.pagination > tbody > tr > td > a:focus
        {
            z-index: 2;
            color: #212121;
            background-color: #60bb46;
            border-color: #60bb46;
            cursor: pointer;
        }
        table#BodyContent_rptPaging > tbody > tr > td > a.active
        {
            background-color: #eee;
            border-color: #ddd;
        }
        tbody
        {
            background-color: #eeeeee;
        }
    </style>

</asp:Content>
