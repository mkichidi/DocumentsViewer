<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DriverDocuments.aspx.cs" Inherits="DocumentsViewer.DriverDocuments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Driver Documents</title>
    <%--  <link href="css/bootstrap.css" rel="stylesheet">
<link href="css/style.css" rel="stylesheet" type="text/css" media="all" />--%>
    <%--<link rel="stylesheet" href="docsupport/style.css" />
    <link rel="stylesheet" href="docsupport/prism.css" />
    <link rel="stylesheet" href="chosen.css" />--%>
    <style type="text/css">
        table {
            border-collapse: collapse;
            border: 1px solid black;
        }

            table td {
                padding: 2px 8px;
                border-right: 1px solid black;
            }

            table tr {
                padding: 2px 8px;
                border-right: 1px solid black;
                /*font-size:5 em;*/
            }
    </style>
    <script type="text/javascript">
        function OnSelectedIndexChanged(s, e) {
            //LoadingPanel.Show();
            e.processOnServer = true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ssx" runat="server"></asp:ScriptManager>
        <h5 class="top-text" style="color: #ff0000; font-size: 2em;">Driver Documents</h5>
        <%-- <asp:UpdatePanel runat="server" ID="Upd" ChildrenAsTriggers="true" UpdateMode="Always">
            <ContentTemplate>--%>


        <div id="Panel" align="center" style="width: 100%">
            <div align="center" style="width: 100%">

                <div style="width: 100%" align="center">
                    <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="Callback">
                        <ClientSideEvents CallbackComplete="function(s, e) { LoadingPanel.Hide(); }" />
                    </dx:ASPxCallback>


                    <div>
                        <div align="center" style="padding-right: 10px; float:left">
                            <asp:Label runat="server" Style="text-align: right; font-size: 2em; float: left" Text="Vehicles">Driver name : </asp:Label>
                            <dx:ASPxComboBox Font-Size="Large" Style="float: left; margin-top: 7px; margin-left: 5px;" ID="ASPxComboBox1" runat="server" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" AutoPostBack="true"
                                TextFormatString="{0}">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="NAme" />
                                    <%--<dx:ListBoxColumn FieldName="Description" Width="300px" />--%>
                                </Columns>
                                <ClientSideEvents SelectedIndexChanged="OnSelectedIndexChanged" />

                            </dx:ASPxComboBox>
                        </div>
                        <div style=" float:left">
                            <asp:Label runat="server" Style="text-align: right; font-size: 2em;">DL Number :</asp:Label>
                            <asp:TextBox Font-Size="Large" Height="25px" ID="txtDLNo" Style="margin-top: -17px" runat="server"></asp:TextBox>
                        </div>
                        <div style=" float:left;margin-left:10px">
                            <asp:Label runat="server" Style="text-align: right; font-size: 2em;">SL Number :</asp:Label>
                            <asp:TextBox Font-Size="Large" Height="25px" ID="txtSLNo" runat="server"></asp:TextBox>
                        </div>
                        <div style="float:left;margin-left: 20px; margin-top:10px">
                            <asp:Button runat="server" Text="Search" OnClick="DdlVehicles_SelectedIndexChanged"  Font-Size="Large"/></div>
                    </div>
                    <%--<asp:DropDownList ID="DdlVehicles" runat="server" AutoPostBack="true" CssClass="chosen-select" OnSelectedIndexChanged="DdlVehicles_SelectedIndexChanged"></asp:DropDownList>--%>
                </div>
            </div>
            <br />
            <div align="center">
                <asp:GridView Font-Size="XX-Large" Style="margin-top: 30px" BorderWidth="1" AlternatingRowStyle-BackColor="LightPink" BorderColor="Black" OnRowCommand="GvDocuments_RowCommand" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Large" HeaderStyle-BackColor="Orange" ID="GvDocuments" class="table" runat="server" AutoGenerateColumns="false" EmptyDataText="No Documents available">
                    <Columns>
                        <asp:TemplateField HeaderText="DocumentId" Visible="false">
                            <ItemTemplate>
                                <asp:TextBox ID="txtID" runat="server" Text='<%# Eval("DocumentId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderStyle-Font-Size="XX-Large" DataField="DriverName" HeaderText="Driver Name" ControlStyle-Font-Size="Larger" />
                        <asp:BoundField HeaderStyle-Font-Size="XX-Large" DataField="DLNo" HeaderText="DL Number" ControlStyle-Font-Size="Larger" />
                        <asp:BoundField HeaderStyle-Font-Size="XX-Large" DataField="SLNo" HeaderText="SL Number" ControlStyle-Font-Size="Larger" />
                        <asp:BoundField HeaderStyle-Font-Size="XX-Large" DataField="DocumentName" HeaderText="Document Name" ControlStyle-Font-Size="Larger" />

                        <asp:TemplateField HeaderText="Document Path" Visible="false">
                            <ItemTemplate>
                                <asp:Literal ID="Path" runat="server"
                                    Text='<%# Eval("Path") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-Font-Size="XX-Large" HeaderText="Document">
                            <ItemTemplate>
                                <asp:Button Text="View Document" runat="server" CommandName="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True"></dx:ASPxLoadingPanel>
        <%--<dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True">
    </dx:ASPxLoadingPanel>--%>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        <%--<script src="docsupport/jquery-3.2.1.min.js" type="text/javascript"></script>
        <script src="chosen.jquery.js" type="text/javascript"></script>
        <script src="docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
        <script src="docsupport/init.js" type="text/javascript" charset="utf-8"></script>--%>
    </form>
    <%--<script src="js/bootstrap.js"></script>--%>
</body>
</html>
