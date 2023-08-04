<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageAssignProductUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.MessageAssignProductUserControl" %>
<ajax:UpdatePanel ID="HomeUpdatePanel" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td width="10%">
                </td>
                <td>
                    <table id="Table1" style="width: 100%; height: 100%;" runat="server">
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" id="Td1" runat="server">
                                    <asp:Label ID="LabelHeading" runat="server" Text="WELCOME TO LOCALIZATION INSIGHTS"
                                        CssClass="HyperLinkMenu" ForeColor="AntiqueWhite" Font-Underline="true" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 70%" id="HomePageMessagetd" runat="server">
                                <asp:Panel ID="PanelWelcome" runat="server" Width="100%" Visible="false">
                                    <asp:Label ID="HomePageMessageLabel" runat="server" Text="HOME PAGE" CssClass="HyperLinkMenu"
                                        ForeColor="AntiqueWhite" Font-Size="Small"></asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="PanelWSR" runat="server" Width="100%" Visible="false">
                                    <asp:Label ID="LabelWSR" runat="server" Text="Weekly Status Report" CssClass="HyperLinkMenu"
                                        ForeColor="AntiqueWhite" Font-Size="Small"></asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="PanelMetrics" runat="server" Width="100%" Visible="false">
                                    <asp:Label ID="LabelMetrics" runat="server" Text="Weekly Status Report" CssClass="HyperLinkMenu"
                                        ForeColor="AntiqueWhite" Font-Size="Small"></asp:Label>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="5%">
                </td>
            </tr>
        </table>
    </ContentTemplate>
</ajax:UpdatePanel>
