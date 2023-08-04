<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocalesVsPlatformsCombinedMatrixDataUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.LocalesVsPlatformsCombinedMatrixDataUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="PanelFilter" runat="server" Width="100%" Font-Bold="true" BorderColor="White"
    ForeColor="AntiqueWhite">
    <table style="width: 100%; height: 100%; color: White" runat="server">
        <tr>
            <td>
                <asp:Panel ID="PanelVendorDetails" runat="server" Width="100%">
                    <table style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelVendor" runat="server" Text="" CssClass="HyperLinkMenu" ForeColor="#DB512D"
                                    Font-Size="Small" Font-Underline="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="height: 100%">
        <tr>
            <td>
                <asp:Panel ID="WrapperPanelMatrix" runat="server" Width="100%" Height="100%">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="height: 30px">
            </td>
        </tr>
    </table>
</asp:Panel>
