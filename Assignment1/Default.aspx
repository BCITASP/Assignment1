<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Assignment1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblSearchTerm" runat="server" Text="Search Term:"></asp:Label>
        <asp:TextBox ID="txtSearchTerm" runat="server" Width="268px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        <asp:RequiredFieldValidator ID="vldSearchTermRequired" runat="server" Display="Dynamic" ErrorMessage="A search term is required!" ControlToValidate="txtSearchTerm" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:Panel ID="pnlResult" runat="server" Height="520px" Width="1017px">
                    <asp:Label ID="lblFileName" runat="server" Text="File:"></asp:Label>
        <asp:TextBox ID="txtFileName" runat="server" ReadOnly="True"></asp:TextBox>
                    <asp:Label ID="lblResultCount" runat="server" Text="ResultCount"></asp:Label>
                    <asp:Button ID="btnFirst" runat="server" Text="First" />
                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" />
                    <asp:Button ID="btnNext" runat="server" Text="Next" />
                    <asp:Button ID="btnLast" runat="server" Text="Last" />
                    <br />
                    <asp:TextBox ID="txtFileContent" runat="server" Height="456px" TextMode="MultiLine" Width="750px"></asp:TextBox>
                    <br />
                    <asp:ImageButton ID="btnPrint" runat="server" Height="67px" ImageUrl="~/images/printericon.png" Width="67px" />
                    <asp:ImageButton ID="btnSave" runat="server" Height="67px" ImageUrl="~/images/diskicon.jpg" Width="67px" />
                    <br />
        </asp:Panel>
        <br />
        <asp:Label ID="lblNotFound" runat="server" Text="No files were found with your search terms" Visible="False"></asp:Label>
        <br />

    </div>
    </form>
</body>
</html>
