<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowProductSummaryUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.ShowProductSummaryUserControl" %>
<ajax:UpdatePanel ID="PanelProductVersion" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td align="left" style="height: 20px">
                                <asp:LinkButton ID="LinkButtonProduct" runat="server" Text="" ForeColor="#DB512D"
                                    Font-Bold="true" Font-Size="Large" OnClick="LinkButtonProduct_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Panel ID="PanelLatestProductVersion" runat="server" Width="100%">
                                    <table style="width: 100%; height: 100%;">
                                        <tr>
                                            <td align="left" style="height: 20px">
                                                <asp:LinkButton ID="LinkButtonSelectedProductVersion" runat="server" Text=""
                                                    ForeColor="Cyan" Font-Bold="true" Font-Size="Medium" OnClick="LinkButtonSelectedProductVersion_Click"></asp:LinkButton>
                                                &nbsp;
                                                <asp:Label ID="LabelViewDetails" runat="server" Text="View Details Message" ForeColor="AntiqueWhite"
                                                    Font-Size="Small"></asp:Label>
                                                <asp:Label ID="LabelProductVersionID" runat="server" Text="" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Panel ID="PanelPhasesNotCreated" runat="server" Width="100%" Visible="false">
                                                    <asp:Label ID="LabelPhasesNotCreated" runat="server" Text="Phases Not Created" ForeColor="#DB512D"
                                                        Font-Size="Small"></asp:Label>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Panel ID="PanelShowProjectPhases" runat="server" Width="100%">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 20px">
                                <asp:Panel ID="PanelActiveVersions" runat="server" Width="100%">
                                    <table style="width: 100%; height: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelActiveVersions" runat="server" Text="Active Versions" ForeColor="Cyan"
                                                    Font-Bold="true" Font-Underline="true" Font-Size="Small"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="LabelSelectActive" runat="server" Text="Select Message" ForeColor="AntiqueWhite"
                                                    Font-Size="Small"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="PanelNoActiveVersions" runat="server" Width="100%" Visible="false">
                                                    <asp:Label ID="LabelNoActiveVersions" runat="server" Text="No Active Versions" ForeColor="#DB512D"
                                                        Font-Size="Small"></asp:Label>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="PanelActiveVersionsList" runat="server" Width="100%">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Panel ID="PanelNonActiveVersions" runat="server" Width="100%">
                                    <table style="width: 100%; height: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelNonActiveVersions" runat="server" Text="Non-Active Versions"
                                                    ForeColor="Cyan" Font-Bold="true" Font-Underline="true" Font-Size="Small"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="LabelSelectNonActive" runat="server" Text="Select Message" ForeColor="AntiqueWhite"
                                                    Font-Size="Small"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Panel ID="PanelNoNonActiveVersions" runat="server" Width="100%" Visible="false">
                                                    <asp:Label ID="LabelNoNonActiveVersions" runat="server" Text="No Non-Active Versions"
                                                        ForeColor="#DB512D" Font-Size="Small"></asp:Label>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="PanelNonActiveVersionList" runat="server" Width="100%">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 50px">
                </td>
            </tr>
        </table>
    </ContentTemplate>
</ajax:UpdatePanel>
<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td>
            <asp:Panel ID="PanelFooterPadding" runat="server" Width="100%" Height="100%" Visible="false">
            </asp:Panel>
        </td>
    </tr>
</table>
