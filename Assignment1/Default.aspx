<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"  Inherits="Assignment1.Default" ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>COMP4976 Assignment 1 - SearchStar By Alan Lai & Andres Aguilar</title>
    <script type="text/javascript">
        function print()
        {
            var breakTag ='<br>';
            var divtoprint = document.getElementById('txtFileContent').innerHTML;
            var popupWin = window.open('', '_blank', 'width=500,height=400');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divtoprint.replace(/([^>\r\n]?)(\r\n|\n\r|\r|\n)/g, '$1'+ breakTag +'$2') + '</html>');
            popupWin.document.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="background: url(images/logo.png); background-repeat:no-repeat; width: auto; height: 201px;">
        <br /><br />
            <br />
            <br />
        <asp:Label ID="lblSearchTerm" runat="server" Text="Search Term:"></asp:Label>
        <asp:TextBox ID="txtSearchTerm" runat="server" Width="251px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="vldSearchTermRequired" runat="server" Display="Dynamic" ErrorMessage="A search term is required!" ControlToValidate="txtSearchTerm" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        
        <br />
        
        <asp:Panel ID="pnlResult" runat="server" Height="520px" Width="1017px" Visible="False">
                    <asp:Label ID="lblFileName" runat="server" Text="File:"></asp:Label>
        <asp:TextBox ID="txtFileName" runat="server" ReadOnly="True"></asp:TextBox>
                    <asp:Label ID="lblResultCount" runat="server" Text="ResultCount"></asp:Label>
                    <asp:Button ID="btnFirst" runat="server" Text="First" OnClick="btnFirst_Click" />
                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" />
                    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
                    <asp:Button ID="btnLast" runat="server" Text="Last" OnClick="btnLast_Click" />
                    <br />
                    <asp:TextBox ID="txtFileContent" runat="server" Height="456px" TextMode="MultiLine" Width="750px"></asp:TextBox>
                    <br />
                    <asp:ImageButton ID="btnPrint" runat="server" Height="67px" ImageUrl="~/images/printericon.png" Width="67px" OnClientClick="print()" />
                    <asp:ImageButton ID="btnSave" runat="server" Height="67px" ImageUrl="~/images/diskicon.jpg" Width="67px" OnClick="btnSave_Click" />
                    <br />
        </asp:Panel>
        <br />
        <asp:Label ID="lblNotFound" runat="server" ForeColor="Red" Text="No files were found with your search terms" Visible="False"></asp:Label>
        <br />

    </div>
    </form>
</body>
</html>
