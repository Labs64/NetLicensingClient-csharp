<%@ Page Language="C#" Inherits="NetLicensingDemo.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
    <title>Default</title>
    <style>
table.fixed1 td:first-child { width: 600px;}
table.fixed2 td:first-child { width: 200px;}
table.fixed2 td:first-child+td { width: 400px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Table id="tbl1" CssClass="fixed2" Style="padding: 1px;" BorderStyle="None" runat="server" CellSpacing="2">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right"><asp:Label>Hardware ID (mock):</asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox id="textHardwareId" runat="server" Width="95%">Arbitrary HW-related data</asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center"><asp:Button id="buttonGetLicenseeNumber" runat="server" Text="Generate Licensee Number" OnClick="buttonGetLicenseeNumberClicked"/></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right"><asp:Label>Licensee Number:</asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox id="textLicenseeNumber" runat="server" Width="95%" OnTextChanged="textLicenseeNumberChanged">demo-licensee</asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center"><asp:Button id="buttonValidate" runat="server" Text="Validate Licenses" OnClick="buttonValidateClicked"/></asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <br/>
        <asp:Table id="tblP1" CssClass="fixed1" BorderStyle="Solid" BorderWidth="1" runat="server" CellSpacing="2">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center"><b><asp:Label>Module 1: Try &amp; Buy</asp:Label></b></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center"><asp:TextBox id="textTryBuy" runat="server" Width="95%" ReadOnly="true">-</asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <br/>
        <asp:Table id="tblP2" CssClass="fixed1" BorderStyle="Solid" BorderWidth="1" runat="server" CellSpacing="2">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center"><b><asp:Label>Module 2: Subscription</asp:Label></b></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center"><asp:TextBox id="textSubscription" runat="server" Width="95%" ReadOnly="true">-</asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <br/>
        <asp:Label id="message" runat="server" />

    </form>
</body>
</html>
