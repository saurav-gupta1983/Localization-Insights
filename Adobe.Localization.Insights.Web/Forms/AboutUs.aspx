<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs"
    Inherits="Adobe.Localization.Insights.Web.AboutUs" Title="Oogway - Adobe Localization Insights"
    StylesheetTheme="InsightStyles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="ControlId" runat="server">
        <table style="width: 100%; height: 100%">
            <tr>
                <td>
                    <table id="Table1" style="width: 100%; height: 100%;" runat="server">
                        <tr>
                            <td align="left" id="Td1" runat="server">
                                <asp:Label ID="LabelHeading" runat="server" Text="About Us" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70%" id="TDHomePageMessage" runat="server">
                                <asp:Panel ID="PanelContactUS" runat="server" Width="100%">
                                    <asp:Label ID="LabelMessage" runat="server" Text="Message" CssClass="HyperLinkMenu"
                                        ForeColor="AntiqueWhite" Font-Size="Small"></asp:Label>
                                    <br />
                                    <br />
                                    <ul>
                                        <li style="color: White">
                                            <asp:Label ID="LabelIE" runat="server" Text="IE" CssClass="HyperLinkMenu" ForeColor="AntiqueWhite"
                                                Font-Size="Small"></asp:Label>
                                        </li>
                                        <li style="color: White">
                                            <asp:Label ID="LabelIQE" runat="server" Text="IQE" CssClass="HyperLinkMenu" ForeColor="AntiqueWhite"
                                                Font-Size="Small"></asp:Label>
                                        </li>
                                        <li style="color: White">
                                            <asp:Label ID="LabelIPM" runat="server" Text="IPM" CssClass="HyperLinkMenu" ForeColor="AntiqueWhite"
                                                Font-Size="Small"></asp:Label>
                                        </li>
                                    </ul>
                                    <br />
                                    <br />
                                    <asp:Label ID="LabelTerms" runat="server" Text="Terms" CssClass="HyperLinkMenu" ForeColor="AntiqueWhite"
                                        Font-Size="Medium" Font-Underline="true"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="LabelI18N" runat="server" Text="I18N" CssClass="HyperLinkMenu" ForeColor="AntiqueWhite"
                                        Font-Size="Small"></asp:Label>
                                    <br />
                                    <asp:Label ID="LabelL10N" runat="server" Text="L10N" CssClass="HyperLinkMenu" ForeColor="AntiqueWhite"
                                        Font-Size="Small"></asp:Label>
                                    <br />
                                    <asp:Label ID="LabelT9N" runat="server" Text="T9N" CssClass="HyperLinkMenu" ForeColor="AntiqueWhite"
                                        Font-Size="Small"></asp:Label>
                                    <br />
                                    <asp:Label ID="LabelG11N" runat="server" Text="G11N" CssClass="HyperLinkMenu" ForeColor="AntiqueWhite"
                                        Font-Size="Small"></asp:Label>
                                    <br />
                                    <asp:Label ID="LabelWorldReadiness" runat="server" Text="World-Readiness" CssClass="HyperLinkMenu"
                                        ForeColor="AntiqueWhite" Font-Size="Small"></asp:Label>
                                    <br />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 20%" colspan="3">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
