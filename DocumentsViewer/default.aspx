<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="DocumentsViewer.Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Balaji Transports</title>
    <script>

        function DivClicked() {
            var btnHidden = $('#<%= btnDriver.ClientID %>');
    if (btnHidden != null) {
        btnHidden.click();
    }

    function DivClicked2() {
        var btnHidden = $('#<%= btnVehicle.ClientID %>');
                if (btnHidden != null) {
                    btnHidden.click();
                }
}

</script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <asp:Label runat="server" Style="text-align: right; font-size: 5em;">Balaji Transports</asp:Label>
        </div>
        <div align="center">
            <div style="width: 300px;" onclick="javascript:DivClicked(); return true;">
                <asp:ImageButton Id="DriverImage" runat="server" ImageUrl="~/Images/drivercolor.png" Height="200" Width="200" OnClick="DriverImage_Click"/>
                <br />
                <asp:LinkButton OnClick="btnDriver_Click" runat="server" Style="text-align: right; font-size: 2em; ">Driver Documents</asp:LinkButton>
            </div>
            <div style="width: 300px; " onclick="javascript:DivClicked2(); return true;">
                <asp:ImageButton Id="VehicleImage" runat="server" ImageUrl="~/Images/truck2.png" Height="200" Width="200" OnClick="VehicleImage_Click"/>
                <br />
                <asp:LinkButton OnClick="btnVehicle_Click" runat="server" Style="text-align: right; font-size: 2em;">Vehicle Documents</asp:LinkButton>
            </div>
        </div>
        <asp:Button runat="server" id="btnDriver" style="display:none" onclick="btnDriver_Click" />
        <asp:Button runat="server" id="btnVehicle" style="display:none" onclick="btnVehicle_Click" />
    </form>
</body>
</html>
