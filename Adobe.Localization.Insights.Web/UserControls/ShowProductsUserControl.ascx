<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowProductsUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.ShowProductsUserControl" %>
<ajax:UpdatePanel ID="HomeUpdatePanel" runat="server">
    <ContentTemplate>
        <table style="width: 100%; height: 100%;">
            <tr>
                <td>
                    <table style="width: 100%; height: 100%;">
                        <tr style="width: 100%">
                            <td style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 20px; width: 100%">
                                <%--                                <asp:LinkButton ID="LinkButtonProductHeading" runat="server" Text="Product" ForeColor="Cyan" 
                                    Font-Bold="true" Font-Size="Medium" OnClick="LinkButtonProductHeading_Click"></asp:LinkButton>
                                --%>
                                <asp:LinkButton ID="LinkButtonProductHeading" runat="server" Text="Product" ForeColor="Cyan"
                                    Font-Bold="true" Font-Size="Medium"></asp:LinkButton>
                                <asp:Label ID="LabelProductID" runat="server" Text="ProductID" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 20px; width: 100%">
                                <asp:Label ID="LabelNoVersionsAvailable" runat="server" Text="No Versions Available"
                                    Visible="false" ForeColor="AntiqueWhite"></asp:Label>
                                <asp:Panel ID="PanelProductVersions" runat="server" Width="100%">
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</ajax:UpdatePanel>
