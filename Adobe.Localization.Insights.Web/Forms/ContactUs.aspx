<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs"
    Inherits="Adobe.Localization.Insights.Web.ContactUs" Title="Oogway - Adobe Localization Insights"
    StylesheetTheme="InsightStyles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="ControlId" runat="server">
        <table style="width: 100%; height: 100%">
            <tr style="height: 80%">
                <td style="width: 20px">
                </td>
                <td>
                    <table id="Table1" style="width: 100%; height: 100%;" runat="server">
                        <tr>
                            <td align="left" id="Td1" runat="server" colspan="2">
                                <asp:Label ID="LabelHeading" runat="server" Text="Contact Us" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" id="TDHomePageMessage" runat="server" colspan="2">
                                <asp:Label ID="LabelFeedback" runat="server" Text="Feedback" CssClass="HyperLinkMenu"
                                    ForeColor="AntiqueWhite" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px" colspan="2">
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td style="width: 20%">
                                <asp:Label ID="LabelSubject" runat="server" Text="Subject" ForeColor="AntiqueWhite"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td style="width: 80%" align="left">
                                <asp:TextBox ID="TextBoxSubject" CssClass="TextBox" runat="server" Width="50%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="LabelEmailBody" runat="server" Text="Body" ForeColor="AntiqueWhite"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <asp:TextBox ID="TextBoxEmailBody" CssClass="TextBox" runat="server" Width="50%"
                                    TextMode="MultiLine" Height="60px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Button ID="ButtonSendMessage" runat="server" Text="Send Message" OnClick="ButtonSendMessage_Click" />
                                <asp:Label ID="LabelMessage" runat="server" Text="Operation Message" CssClass="medium"
                                    ForeColor="red" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 20%">
                <td style="height: 200px" colspan="2">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
