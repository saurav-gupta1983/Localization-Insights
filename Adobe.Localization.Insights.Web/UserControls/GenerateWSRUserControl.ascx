<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GenerateWSRUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.GenerateWSRUserControl" %>
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
<body title="View / Generate WSR Screen">
    <asp:Panel ID="WrapperPanel" runat="server" Width="90%" Style="left: 30px; position: relative;
        top: 20px">
        <table id="Table1" style="width: 80%; height: 100%; color: White" runat="server">
            <tr>
                <td align="left" colspan="2">
                    <asp:Label ID="LabelHeading" runat="server" Text="View / Generate Weekly Status Report "
                        CssClass="HyperLinkMenu" ForeColor="AntiqueWhite" Font-Underline="true" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 20px" colspan="2">
                </td>
            </tr>
            <tr style="height: 15px">
                <td style="width: 50px">
                    <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                </td>
                <td>
                    <asp:Label ID="LabelProductName" runat="server" Text="Illustrator" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                </td>
            </tr>
            <tr style="height: 15px">
                <td>
                    <asp:Label ID="LabelVendor" runat="server" Text="Vendor:" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListVendor" runat="server" Width="30%" AutoPostBack="true"
                        OnSelectedIndexChanged="DropDownListVendor_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 15px">
                <td>
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
                    <asp:DropDownList ID="DropDownListWeek" runat="server" Width="30%" AutoPostBack="true"
                        OnSelectedIndexChanged="DropDownListWeek_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="height: 20px" align="left" colspan="2">
                    <asp:Button ID="ButtonGenerateWSR" Text="Generate Status Report" runat="server" Enabled="false"
                        OnClick="ButtonGenerateWSR_Click" />
                </td>
            </tr>
            <tr>
                <td style="height: 20px" colspan="2">
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelIssuesandAccomplishments" runat="server" Text="Issues and Accomplishments"
                        CssClass="HyperLinkMenu" ForeColor="Blue" Font-Underline="true" Font-Size="Medium"
                        Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td style="color: White">
                    <font style="color: Red; font-weight: bold">Red</font> (Urgent help needed from
                    management team)
                    <p>
                        Items below represent critical issues that are keeping Vendor QE Team from delivering
                        on schedule. These items need management help in resolving. Stated below issues
                        must be resolved by scheduled date</p>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBoxRedIssues" runat="server" TextMode="MultiLine" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td style="color: White">
                    <font style="color: Yellow; font-weight: bold">Yellow</font> (Issues)
                    <p>
                        Items below represent issues that management should be aware of but do not keep
                        Vendor QE Team from delivering on schedule. These items may not need management
                        help in resolving. Stated below issues must be resolved by scheduled date.</p>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBoxYellowIssues" runat="server" TextMode="MultiLine" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td style="color: White">
                    <font style="color: Green; font-weight: bold">Green</font> (Key accomplishments,
                    progress, acknowledgments)
                    <p>
                        Items below represent accomplishments or progress made on deliverables for the week.</p>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBoxGreenAccomplishments" runat="server" TextMode="MultiLine"
                        Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
        </table>
        <table style="width: 100%; color: #FFFFFF;">
            <tr style="height: 20px">
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="LabelMetrics" runat="server" Text="Metrics" CssClass="HyperLinkMenu"
                        ForeColor="Blue" Font-Underline="true" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <font style="color: Red; font-weight: bold">Test Cases Metrics</font>
                </td>
            </tr>
            <tr style="height: 10px">
                <td colspan="2">
                </td>
            </tr>
            <tr style="height: 25px">
                <td style="width: 38%; color: #FFFFFF;">
                    <asp:Label ID="LabelTestCasesExecuted" CssClass="medium" runat="server" Text="No. of Test cases executed this week: "></asp:Label>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="TextBoxTestCasesExecuted" CssClass="TextBox" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <font style="color: Red; font-weight: bold">Bugs Metrics</font>
                </td>
            </tr>
            <tr style="height: 10px">
                <td colspan="2">
                </td>
            </tr>
            <tr style="height: 25px; color: #FFFFFF;">
                <td style="width: 38%; color: #FFFFFF;">
                    <asp:Label ID="LabelTotalBugsFound" CssClass="medium" runat="server" Text="Number of bugs found this week"></asp:Label>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="TextBoxTotalBugsFound" CssClass="TextBox" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px">
                <td style="width: 38%; color: #FFFFFF;">
                    <asp:Label ID="LabelBugsRegressed" CssClass="medium" runat="server" Text="Number of bugs regressed this week"></asp:Label>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="TextBoxBugsRegressed" CssClass="TextBox" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px">
                <td style="width: 38%; color: #FFFFFF;">
                    <asp:Label ID="LabelBugsPending" CssClass="medium" runat="server" Text="No. of bugs pending to be regressed"></asp:Label>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="TextBoxBugsPending" CssClass="TextBox" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <font style="color: Red; font-weight: bold">QA Testing Efforts</font>
                </td>
            </tr>
            <tr style="height: 10px">
                <td colspan="2">
                </td>
            </tr>
            <tr style="height: 25px">
                <td style="width: 38%; color: #FFFFFF;">
                    <asp:Label ID="LabelMachineSetup" CssClass="medium" runat="server" Text="Machine Setup"></asp:Label>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="TextBoxMachineSetup" CssClass="TextBox" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px">
                <td style="width: 38%; color: #FFFFFF;">
                    <asp:Label ID="LabelExecutionHours" CssClass="medium" runat="server" Text="QE Man  hours spent in Test case execution"></asp:Label>
                </td>
                <td style="width: 50%; color: #FFFFFF;">
                    <asp:TextBox ID="TextBoxExecutionHours" CssClass="TextBox" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px">
                <td style="width: 38%; color: #FFFFFF;">
                    <asp:Label ID="LabelRegressionHours" CssClass="medium" runat="server" Text="QE Man hours spent in regression"></asp:Label>
                </td>
                <td style="width: 50%; color: #FFFFFF;">
                    <asp:TextBox ID="TextBoxRegressionHours" CssClass="TextBox" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px">
                <td style="width: 38%; color: #FFFFFF;">
                    <asp:Label ID="LabelMeetings" CssClass="medium" runat="server" Text="Meetings"></asp:Label>
                </td>
                <td style="width: 50%; color: #FFFFFF;">
                    <asp:TextBox ID="TextBoxMeetings" CssClass="TextBox" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 25px">
                <td style="width: 38%; color: #FFFFFF;">
                    <asp:Label ID="LabelWSR" CssClass="medium" runat="server" Text="Weekly Status Report"></asp:Label>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="TextBoxWSR" CssClass="TextBox" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabellNextWeekPriority" runat="server" Text="Next Week Priority" CssClass="HyperLinkMenu"
                        ForeColor="Blue" Font-Underline="true" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td style="color: White">
                    <font style="color: Blue; font-weight: bold">Blue</font> (Outstanding deliverables)
                    <p>
                        Items below represent previous week’s deliverables extended into current week. Original
                        schedule date and the reason for extending originally scheduled date must be included.
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridViewOutstandingDeliverables" runat="server" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                        Width="80%" AllowPaging="True">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:TemplateField HeaderText="OutDeliverablesID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LabelOutDeliverablesID" runat="server" Text='<%# Bind("OutStandingDeliverablesID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tasks">
                                <ItemTemplate>
                                    <asp:Label ID="LabelNewDeliverables" runat="server" Text='<%# Bind("PrevWeekDeliverables") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Original Schedule Date">
                                <ItemTemplate>
                                    <asp:Label ID="LabelOriginalScheduleDate" runat="server" Text='<%# Bind("OriginalScheduleDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason">
                                <ItemTemplate>
                                    <asp:Label ID="LabelReason" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#00C0C0" />
                        <AlternatingRowStyle BackColor="#FFE0C0" />
                        <SelectedRowStyle BackColor="CornflowerBlue" />
                    </asp:GridView>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td style="color: White">
                    <font style="color: Black; font-weight: bold">Black</font> (New deliverables)
                    <p>
                        Items below represent new deliverables scheduled/planned for the next week.</p>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBoxNewDeliverables" runat="server" TextMode="MultiLine" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
        </table>
    </asp:Panel>
</body>
