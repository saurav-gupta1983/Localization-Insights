<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadWSRDataUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.UploadWSRDataUserControl" %>
<style type="text/css">
    .style1
    {
        width: 223px;
    }
</style>
<asp:Panel ID="WrapperPanel" runat="server" Width="80%" Style="left: 30px; position: relative;
    top: 5px; text-align: center;">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
        <%--            <tr>
                <td align="center">
                    <h1>
                        <asp:Label ID="LabelHeader" runat="server" Text="Upload Data" Font-Bold="True"></asp:Label>
                    </h1>
                </td>
            </tr>--%>
        <tr>
            <td align="left">
                <asp:Label ID="LabelHeading" runat="server" Text="Weekly Status Report - Upload Data"
                    CssClass="HyperLinkMenu" ForeColor="AntiqueWhite" Font-Underline="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="text-align: left">
        <tr>
            <td colspan="2">
                <asp:Label ID="LabelMessage" CssClass="medium" runat="server" Text="" Width="100%"
                    ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr style="height: 5px">
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td style="width:100px">
                <asp:Label ID="LabelFileUpload" runat="server" Text="WSR Path:" CssClass="HyperLinkMenu"
                    ForeColor="AntiqueWhite" Font-Size="Small" />
            </td>
            <td>
                <asp:FileUpload ID="DataFileUpload" runat="server" />
            </td>
        </tr>
        <tr style="height: 20px">
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <asp:Button ID="UploadButton" runat="server" Text="Upload WSR" OnClick="UploadButton_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
