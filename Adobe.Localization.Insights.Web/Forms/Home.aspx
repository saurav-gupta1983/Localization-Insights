<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs"
    Inherits="Adobe.Localization.Insights.Web.Home" Title="Oogway - Adobe Localization Insights"
    StylesheetTheme="InsightStyles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <ajax:ScriptManager ID="ScriptManager1" runat="server">
    </ajax:ScriptManager>
    <div id="ControlId" runat="server">
        <%--        <asp:Panel ID="WrapperPanel" runat="server" Width="90%" Style="left: 20px; position: relative;
            top: 10px">
        --%>
        <table style="width: 100%; height: 100%">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td align="left">
                                <asp:Label ID="LabelHeading" runat="server" Text="Welcome to Localization Testing Insights" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 70%" id="TDHomePageMessage" runat="server">
                                <asp:Label ID="LabelMessage" runat="server" Text="Message" CssClass="HyperLinkMenu"
                                    ForeColor="AntiqueWhite" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; background-color: #212121">
                        <tr>
                            <td>
                                <asp:Panel ID="PanelShowProducts" runat="server" Width="100%" Height="100%">
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
        <%--        </asp:Panel>
        --%>
    </div>
</asp:Content>
