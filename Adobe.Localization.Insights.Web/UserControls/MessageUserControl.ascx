<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.MessageUserControl" %>
<ajax:UpdatePanel ID="HomeUpdatePanel" runat="server">
    <ContentTemplate>
        <table style="width: 100%;">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td align="left">
                                <asp:Label ID="LabelHeading" runat="server" Text="Header" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" runat="server">
                                <asp:Label ID="LabelMessage" runat="server" Text="Definition" CssClass="HyperLinkMenu"
                                    ForeColor="AntiqueWhite" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; background-color: #212121">
                        <tr>
                            <td>
                                <asp:Panel ID="PanelShowAllChildScreens" runat="server" Width="100%" Height="100%">
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td>
                                <asp:Panel ID="PanelFooterPadding" runat="server" Width="100%" Height="100%" Visible="false">
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</ajax:UpdatePanel>
