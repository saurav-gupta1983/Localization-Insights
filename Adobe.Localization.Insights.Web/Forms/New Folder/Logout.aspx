<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs"
    Inherits="Adobe.Localization.Insights.Web.Logout" Title="Adobe Localization Insights"
    StylesheetTheme="InsightStyles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <ajax:ScriptManager ID="ScriptManager1" runat="server">
    </ajax:ScriptManager>
    <div id="ControlId" runat="server">
        <table style="width: 100%; height: 100%">
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr style="width: 100%; height: 500px">
                <td width="30%">
                </td>
                <td style="width: 600px; height: 100%">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="height: 20px; width: 100%; text-align: left; font-size: small; font-weight: normal;
                                color: #000000;" valign="bottom">
                                <asp:Label ID="LabelMessage" runat="server" Text="Message" ForeColor="AntiqueWhite"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="ButtonLogin" runat="server" Text="Re-Login" OnClick="ButtonLogin_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="30%">
                </td>
            </tr>
            <tr>
                <td style="height: 20%" colspan="3">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
