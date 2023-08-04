<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestCasesMetricsReportUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.TestCasesMetricsReportUserControl" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="ReportsFilterUserControl.ascx" TagName="ReportsFilterUserControl"
    TagPrefix="uc1" %>
<body title="Test Cases Metrics Report">
    <asp:Panel ID="WrapperPanel" runat="server" Width="90%" Style="left: 30px; position: relative;
        top: 20px">
        <table id="Table1" style="width: 100%; height: 100%; color: White" runat="server">
            <tr>
                <td align="left">
                    <asp:Label ID="LabelHeading" runat="server" Text="Metrics - Test Cases " CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Underline="true" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:ReportsFilterUserControl ID="ReportsFilterUserControl" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td style="height: 20px" align="left">
                    <asp:Button ID="ButtonShowReports" Text="Show Reports" runat="server" OnClick="ButtonShowReports_Click" />
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td>
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</body>
