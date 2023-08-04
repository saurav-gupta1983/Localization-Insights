<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportsFilterUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.ReportsFilterUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Src="Calendar.ascx" TagName="DateCalendar" TagPrefix="uc1" %>--%>
<script type="text/javascript">
    //Function restricting user to Enter only Numeric Data
    function NumericOnly() {
        if (event.keyCode != 46) {
            if (event.keyCode < 48 || event.keyCode > 57) {
                alert("Please enter Numeric values for SLA Limits");
                event.returnValue = false;
            }
        }
    }    

</script>
<body title="Filter Critera">
    <style>
        legend
        {
            color: Blue;
        }
    </style>
    <asp:Panel ID="WrapperPanel" runat="server" Width="100%">
        <asp:Panel ID="PanelFilter" runat="server" Width="100%" GroupingText="Filter Criteria"
            Font-Bold="true" BorderColor="White" ForeColor="AntiqueWhite">
            <table id="Table1" style="width: 100%; height: 100%; color: White" runat="server">
                <%--<tr>
                <td align="left" colspan="2">
                    <asp:Label ID="LabelHeading" runat="server" Text="View / Generate Weekly Status Report "
                        CssClass="HyperLinkMenu" ForeColor="AntiqueWhite" Font-Underline="true" Font-Size="Medium"></asp:Label>
                </td>
            </tr>--%>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="PanelReportingType" runat="server" Width="100%">
                            <table width="100%">
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="LabelWeek" runat="server" Text="Reporting:" CssClass="HyperLinkMenu"
                                            ForeColor="AntiqueWhite" Font-Size="Small" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListReportingType" runat="server" Width="30%" AutoPostBack="true"
                                            OnSelectedIndexChanged="DropDownListReportingType_OnSelectedIndexChanged">
                                            <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                                            <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp; &nbsp;
                                        <asp:DropDownList ID="DropDownListWeek" runat="server" Width="30%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="PanelProducts" runat="server">
                            <table width="100%">
                                <tr style="width: 100%">
                                    <td style="width: 100px">
                                        <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                                            ForeColor="AntiqueWhite" Font-Size="Small" />
                                    </td>
                                    <td id="ShowProductCheckBoxes" runat="server">
                                        <%--                                        <asp:CheckBox ID="temp" runat="server" OnCheckedChanged="CheckBox_OnCheckChanged" />
                                        --%>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="height: 15px">
                    <td>
                        <asp:Panel ID="PanelVendors" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="LabelVendor" runat="server" Text="Vendor:" CssClass="HyperLinkMenu"
                                            ForeColor="AntiqueWhite" Font-Size="Small" />
                                    </td>
                                    <td id="ShowVendorCheckBoxes" runat="server">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
</body>
