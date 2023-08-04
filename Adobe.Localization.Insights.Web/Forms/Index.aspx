<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs"
    Inherits="Adobe.Localization.Insights.Web.Index" Title="Oogway - Adobe Localization Insights"
    StylesheetTheme="InsightStyles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <ajax:ScriptManager ID="ScriptManager1" runat="server">
    </ajax:ScriptManager>
    <div id="ControlId" runat="server">
        <asp:Panel ID="WrapperPanel" runat="server" Width="100%" Style="left: 20px; position: absolute;
            top: 50px">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="LabelHeading" runat="server" Text="Welcome to Localization Testing Insights"
                                        CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 40px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 70%" id="TDHomePageMessage" runat="server">
                                    <asp:Label ID="LabelMessage" runat="server" Text="Message" CssClass="HyperLinkMenu"
                                        ForeColor="AntiqueWhite" Visible="false" Font-Size="Small"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; background-color: #212121">
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelShowProducts" runat="server" Width="100%" Height="100%">
                                        <table id="Table4" style="width: 100%; color: White" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 24%">
                                                </td>
                                                <td style="width: 24%; min-width: 100px">
                                                    <asp:DropDownList ID="DropDownListProducts" runat="server" Width="100%" Height="25px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="DropDownListProducts_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 4%;">
                                                </td>
                                                <td style="width: 24%; min-width: 100px">
                                                    <asp:DropDownList ID="DropDownListProductVersion" runat="server" Width="100%" AutoPostBack="true"
                                                        Height="25px" OnSelectedIndexChanged="DropDownListProductVersion_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 24%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; height: 80px" colspan="5">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 24%">
                                                </td>
                                                <td colspan="3" style="width: 52%">
                                                    <table style="width: 100%; color: White" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 5%">
                                                            </td>
                                                            <td align="center">
                                                                <table style="width: 100%; color: White" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:ImageButton ID="ImageButtonReport" ToolTip="Report Submission" runat="server"
                                                                                OnClick="ImageButtonReport_Click" ImageUrl="~/Images/Report.jpg" AlternateText="Report Submission"
                                                                                Enabled="false"></asp:ImageButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 10px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="LabelReport" runat="server" Text="Report Submission" CssClass="HyperLinkMenu"
                                                                                ForeColor="#DB512D" Font-Size="Small"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 5%">
                                                            </td>
                                                            <td align="center">
                                                                <table style="width: 100%; color: White" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:ImageButton ID="ImageButtonTestManagement" ToolTip="Localization Testing Insights"
                                                                                runat="server" OnClick="ImageButtonTestManagement_Click" ImageUrl="~/Images/Testing.jpg"
                                                                                AlternateText="LOC Test Management" Enabled="false"></asp:ImageButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 10px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="LabelTestManagement" runat="server" Text="LOC Test Management" CssClass="HyperLinkMenu"
                                                                                ForeColor="#DB512D" Font-Size="Small"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 5%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 24%">
                                                </td>
                                            </tr>
                                        </table>
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
        </asp:Panel>
    </div>
</asp:Content>
