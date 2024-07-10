<%@ Page Language="C#" MasterPageFile="../Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="EmployeeMaster.aspx.cs" Inherits="Client.Admin.EmployeeMaster" %>

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
                        <li class="breadcrumb-item"><a href="EmployeeMaster.aspx">Employee Master</a></li>
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
                            <span>Employee Master</span>
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
                    <asp:HiddenField ID="hdnEmpId" runat="server" />

                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label3" runat="server" CssClass="txtbld" Text="Employee Name"> </asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEmpname"
                            runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Enter Expenses Details"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtEmpname" placeholder="Employee Name" class="form-control" runat="server"></asp:TextBox>
                    </div>

                   <div class="col-md-4 form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="txtbld" Text="DOB"> </asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDOB"
                            runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Enter DOB"></asp:RequiredFieldValidator>
                        <div class='input-group date'>
                            <asp:TextBox ID="txtDOB" runat="server" class="form-control" placeholder="Date"
                                MaxLength="20"></asp:TextBox>
                            <label for='contentbody_txtDOB' class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </label>
                        </div>
                    </div>

                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label2" runat="server" CssClass="txtbld" Text="DOH"> </asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtDOH"
                           runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Enter DOH"></asp:RequiredFieldValidator>
                      <div class='input-group date'>

                        <asp:TextBox ID="txtDOH" runat="server" class="form-control" placeholder="DOH"
                            MaxLength="20"></asp:TextBox>
                        <label for='contentbody_txtDOB' class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </label>
                      </div>
                    </div>

                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label4" runat="server" CssClass="txtbld" Text="Address Line1"> </asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtAddressLine1"
         runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="Enter Address Line1"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtAddressLine1" placeholder="Address Line1" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label5" runat="server" CssClass="txtbld" Text="Address Line2"> </asp:Label>
               
                        <asp:TextBox ID="txtAddressLine2" placeholder="Address Line2" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label6" runat="server" CssClass="txtbld" Text="Address Line3"> </asp:Label>
  
                        <asp:TextBox ID="txtAddressLine3" placeholder="Address Line3" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-3 form-group">
                        <asp:Label ID="Label7" runat="server" CssClass="txtbld" Text="City"> </asp:Label>
   
                        <asp:TextBox ID="txtCity" placeholder="City" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-3 form-group">
                        <asp:Label ID="Label8" runat="server" CssClass="txtbld" Text="State"> </asp:Label>
                         
                        <asp:TextBox ID="txtState" placeholder="State" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-3 form-group">
                        <asp:Label ID="Label9" runat="server" CssClass="txtbld" Text="Country"> </asp:Label>
                       
     
    <asp:TextBox ID="txtCountry" placeholder="Country" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3 form-group">
                        <asp:Label ID="Label10" runat="server" CssClass="txtbld" Text="Pincode"> </asp:Label>
                      
    <asp:TextBox ID="txtPincode" placeholder="Pincode" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-3 form-group">
                        <asp:Label ID="Label11" runat="server" CssClass="txtbld" Text="Primary ContactNumber"> </asp:Label>
                       
     
    <asp:TextBox ID="txtPrimaryContactNumber" placeholder="Primary ContactNumber" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-3 form-group">
                        <asp:Label ID="Label12" runat="server" CssClass="txtbld" Text="Secondary ContactNumber"> </asp:Label>
                        
     
    <asp:TextBox ID="txtSecondaryContactNumber" placeholder="Secondary ContactNumber" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-3 form-group">
                        <asp:Label ID="Label13" runat="server" CssClass="txtbld" Text="Company EmailID"> </asp:Label>
                       
     
    <asp:TextBox ID="txtEmpCompanyEmailID" placeholder="CompanyEmailID" class="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-3 form-group">
                        <asp:Label ID="Label14" runat="server" CssClass="txtbld" Text="Personal EmailID"> </asp:Label>
                         
     
    <asp:TextBox ID="txtEmpPersonalEmailID" placeholder="Personal EmailID" class="form-control" runat="server"></asp:TextBox>
                    </div>


                    <div class="col-md-2 form-group">
                        <asp:Label ID="Label15" runat="server" CssClass="txtbld" Text="Is Active?"></asp:Label>
                        <asp:RadioButtonList ID="RD_IsActive" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1"> Yes</asp:ListItem>
                            <asp:ListItem Value="0"> No</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>


                    <div class="col-md-2 form-group">
                        <asp:Label ID="Label16" runat="server" CssClass="txtbld" Text="Is Admin?"></asp:Label>
                        <asp:RadioButtonList ID="RD_IsAdmin" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1"> Yes</asp:ListItem>
                            <asp:ListItem Value="0"> No</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label17" runat="server" CssClass="txtbld" Text="User name"> </asp:Label>
                         
     
    <asp:TextBox ID="txtUsername" placeholder="User name" class="form-control" runat="server"></asp:TextBox>
                    </div>


                    <div class="col-md-4 form-group">
                        <asp:Label ID="Label18" runat="server" CssClass="txtbld" Text="Password"> </asp:Label>
                        
     
    <asp:TextBox ID="txtPwd" placeholder="Password" class="form-control" runat="server"></asp:TextBox>
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
                    <asp:GridView ID="GridEmployee" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Found"
                        class="table table-bordered table-striped table-hover">
                        <RowStyle CssClass="odd-row" />
                        <AlternatingRowStyle CssClass="even-row" />
                        <Columns>
                            <asp:TemplateField HeaderText="Emp ID">
                                <ItemTemplate>
                                    <asp:Label ID="EmpID" runat="server" Text='<%#Eval("EmpID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp name">
                                <ItemTemplate>
                                    <asp:Label ID="Empname" runat="server" Text='<%#Eval("Empname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AddressLine1">
                                <ItemTemplate>
                                    <asp:Label ID="AddressLine1" runat="server" Text='<%#Eval("AddressLine1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ContactNumber">
                                <ItemTemplate>
                                    <asp:Label ID="PrimaryContactNumber" runat="server" Text='<%#Eval("PrimaryContactNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Update">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Imgedit" runat="server" OnClick="Edit" EventArgument='<%# Eval("EmpID") %>'
                                        data-toggle="tooltip" data-placement="left" title="" data-original-title="Update"
                                        OnClientClick="return confirm('Are you sure you want to Edit this record?');"
                                        class="btn btn-info btn-xs" Text="<i class='fa fa-pencil'></i>"></asp:LinkButton>
                                    <asp:LinkButton ID="Imgdelete" runat="server" OnClick="Delete" EventArgument='<%# Eval("EmpID") %>'
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
