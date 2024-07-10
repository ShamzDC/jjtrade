<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPage.aspx.cs" Inherits="TestPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Database Connection</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnFetchData" runat="server" Text="Fetch Shop Names" OnClick="btnFetchData_Click" />
            <br /><br />
            <asp:GridView ID="gvShopNames" runat="server">
                <Columns>
                    <asp:BoundField DataField="ShopName" HeaderText="Shop Name" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
