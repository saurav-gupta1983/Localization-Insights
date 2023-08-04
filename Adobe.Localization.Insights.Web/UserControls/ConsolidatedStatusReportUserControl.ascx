<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsolidatedStatusReportUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.ConsolidatedStatusReportUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Calendar.ascx" TagName="DateCalendar" TagPrefix="uc1" %>
<body>
    <table style="width: 80%; height: 100%;" runat="server" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td align="left">
                <asp:Label ID="LabelHeader" runat="server" Text="View Consolidated Status/Efforts"
                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelProduct" runat="server" Width="100%">
                    <table id="Table4" style="width: 100%; color: White" runat="server" border="0" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProductName" runat="server" Text="Illustrator" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                                <asp:DropDownList ID="DropDownListProducts" runat="server" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownListProducts_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelProductVersion" runat="server" Width="100%">
                    <table id="Table2" style="width: 100%; color: White" runat="server" border="0" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProductVersion" runat="server" Text="Product Release:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <table id="TableSelectProductRelease" style="width: 100%; color: White" runat="server"
                                    border="0" cellpadding="0" cellspacing="0">
                                    <tr style="width: 100%">
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="DropDownListProductYear" runat="server" Width="90%" AutoPostBack="true"
                                                OnSelectedIndexChanged="DropDownListProductYear_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 80%">
                                            <asp:DropDownList ID="DropDownListProductVersion" runat="server" Width="75%" AutoPostBack="true"
                                                OnSelectedIndexChanged="DropDownListProductVersion_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelProjectPhaseType" runat="server" Width="100%">
                    <table id="Table1" style="width: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelTestingType" runat="server" Text="Testing Type:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                                <asp:Label ID="LabelTestingTypeValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                            </td>
                            <td style="width: 50%" align="justify">
                                <table width="80%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="width: 30%">
                                            <asp:RadioButton ID="RadioBtnPhaseBoth" Checked="true" Text="Both" ForeColor="#DB512D"
                                                AutoPostBack="true" Font-Size="Small" GroupName="RadioButtonPhaseType" runat="server"
                                                OnCheckedChanged="RadioBtnPhaseBoth_OnCheckedChanged" />
                                        </td>
                                        <td style="width: 34%">
                                            <asp:RadioButton ID="RadioBtnPhaseFunctional" Checked="false" Text="Functional" ForeColor="#DB512D"
                                                AutoPostBack="true" Font-Size="Small" GroupName="RadioButtonPhaseType" runat="server"
                                                OnCheckedChanged="RadioBtnPhaseFunctional_OnCheckedChanged" />
                                        </td>
                                        <td style="width: 33%">
                                            <asp:RadioButton ID="RadioBtnPhasePhaseLinguistic" Checked="false" Text="Linguistic"
                                                AutoPostBack="true" ForeColor="#DB512D" Font-Size="Small" GroupName="RadioButtonPhaseType"
                                                runat="server" OnCheckedChanged="RadioBtnPhasePhaseLinguistic_OnCheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelProjectPhase" runat="server" Width="100%">
                    <table id="Table3" style="width: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProjectPhases" runat="server" Text="Project Phase:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <table style="width: 80%" cellpadding="0" cellspacing="0" border="0">
                                    <tr style="width: 100%">
                                        <td style="width: 50%">
                                            <asp:DropDownList ID="DropDownListPhaseType" runat="server" Width="100%" AutoPostBack="true"
                                                OnSelectedIndexChanged="DropDownListPhaseType_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 5%">
                                        </td>
                                        <td style="width: 45%">
                                            <asp:DropDownList ID="DropDownListProductSprint" runat="server" Width="100%" Visible="false"
                                                AutoPostBack="true" OnSelectedIndexChanged="DropDownListProductSprint_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:DropDownList ID="DropDownListProjectPhases" runat="server" Width="100%" AutoPostBack="true"
                                                OnSelectedIndexChanged="DropDownListProjectPhases_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelVendor" runat="server" Width="100%">
                    <table id="TableVendor" style="width: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelVendor" runat="server" Text="Team:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="LabelVendorName" runat="server" Text="" CssClass="HyperLinkMenu" ForeColor="#DB512D"
                                    Visible="false" Font-Size="Small" />
                                <asp:DropDownList ID="DropDownListVendor" runat="server" Width="80%" Visible="true"
                                    AutoPostBack="true" OnSelectedIndexChanged="DropDownListVendor_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelWeek" runat="server" Width="100%">
                    <table id="TableWeek" style="width: 100%; color: White" runat="server" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelWeek" runat="server" Text="Week:" CssClass="HyperLinkMenu" ForeColor="#DB512D"
                                    Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="width: 30%">
                                            <asp:DropDownList ID="DropDownListReportingType" runat="server" Width="90%" AutoPostBack="true"
                                                OnSelectedIndexChanged="DropDownListReportingType_OnSelectedIndexChanged">
                                                <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                                                <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                                                <asp:ListItem Text="Total" Value="Total"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 70%">
                                            <asp:DropDownList ID="DropDownListWeek" runat="server" Width="71%" AutoPostBack="true"
                                                OnSelectedIndexChanged="DropDownListWeek_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelReportGenerated" runat="server" Width="100%" Visible="false">
                    <table style="width: 100%; color: White" runat="server" cellpadding="0" cellspacing="0"
                        border="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelDateRange" runat="server" Text="Reported Date Range:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="LabelDateRangeValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <%--            <tr>
                <td style="height: 20px" align="left">
                    <asp:Button ID="ButtonGenerateWSR" Text="Button Generate" runat="server" Enabled="false"
                        OnClick="ButtonGenerateWSR_Click" />
                </td>
            </tr>
            <tr>
                <td style="height: 20px" align="left">
                    <asp:Label ID="LabelMessage" CssClass="medium" runat="server" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>--%>
    </table>
    <ajaxToolkit:TabContainer ID="TabContainerWSR" runat="server">
        <ajaxToolkit:TabPanel ID="TabPanelMetrics" runat="server" HeaderText="Efforts Metrics"
            TabIndex="1" BackColor="#212121">
            <ContentTemplate>
                <table style="width: 100%; background-color: #212121">
                    <tr style="height: 20px">
                        <td style="height: 20px" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2%">
                        </td>
                        <td>
                            <asp:GridView ID="GridViewEffortsTrack" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                Width="100%" OnRowCreated="GridViewEffortsTrack_RowCreated" OnRowDataBound="GridViewEffortsTrack_RowDataBound">
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                </Columns>
                                <HeaderStyle BackColor="#00C0C0" />
                                <RowStyle HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="#FFE0C0" />
                                <SelectedRowStyle BackColor="CornflowerBlue" />
                            </asp:GridView>
                        </td>
                        <td style="width: 2%">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px" colspan="3">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanelIssuesandAccomplishments" runat="server" HeaderText="Issues and Accomplishments"
            TabIndex="2" BackColor="#212121">
            <ContentTemplate>
                <table style="width: 100%; background-color: #212121">
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
                        <td>
                            <asp:Panel ID="PanelFeaturesTested" runat="server" Width="100%">
                                <table id="Table5" style="width: 100%; height: 100%; color: White" runat="server">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelFeaturesTested" runat="server" Text="Features Tested:" ForeColor="Blue"
                                                Font-Underline="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelFeaturesTestedDetails" runat="server" Width="100%" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelAccomplishments" runat="server" Width="100%">
                                <table id="TableAccomplishments" style="width: 100%; height: 100%; color: White"
                                    runat="server">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelGreenAccomplishments" runat="server" Text="Green Accomplishments"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelGreenAccomplishmentDetails" runat="server" Width="100%" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelRisks" runat="server" Width="100%">
                                <table id="TableRisks" style="width: 100%; height: 100%; color: White" runat="server">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelYellowIssues" runat="server" Text="Yellow Issues"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelYellowIssueDetails" runat="server" Width="100%" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelRedIssues" runat="server" Width="100%">
                                <table id="TableRedIssues" style="width: 100%; height: 100%; color: White" runat="server">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelRedIssues" runat="server" Text="Red Issues"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelRedIssueDetails" runat="server" Width="100%" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; background-color: #212121">
                    <tr style="height: 20px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelNextWeekPriority" runat="server" Text="Next Week Priority" CssClass="HyperLinkMenu"
                                ForeColor="Blue" Font-Underline="true" Font-Size="Medium" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelOutstandingDeliverables" runat="server" Width="100%">
                                <table id="TableOutstandingDeliverables" style="width: 100%; height: 100%; color: White"
                                    runat="server">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelBlueDeliverables" runat="server" Text="Blue Deliverables"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelBlueDeliverableDetails" runat="server" Width="100%" ForeColor="White"
                                                Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridViewOutstandingDeliverables" runat="server" BackColor="White"
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                Width="80%" OnRowDataBound="GridViewOutstandingDeliverables_OnRowDataBound">
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
                                                    <asp:TemplateField HeaderText="Original Date">
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
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelNewDeliverables" runat="server" Width="100%">
                                <table id="TableNewDeliverables" style="width: 100%; height: 100%; color: White"
                                    runat="server">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelBlackDeliverables" runat="server" Text="Black Deliverables"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelBlackDeliverableDetails" runat="server" Width="100%" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelNotes" runat="server" Width="100%">
                                <table id="Table6" style="width: 100%; height: 100%; color: White" runat="server">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelNotes" runat="server" Text="Additonal Notes (if any):" ForeColor="Blue"
                                                Font-Underline="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelNotesDetails" runat="server" Width="100%" ForeColor="White"></asp:Label><br />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelFooterPadding" runat="server" Width="100%" Height="100%" Visible="false">
                </asp:Panel>
            </td>
        </tr>
    </table>
</body>
