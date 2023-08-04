<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubmitWSRDataUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.WSRDataUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Calendar.ascx" TagName="DateCalendar" TagPrefix="uc1" %>
<script type="text/javascript">
    //Function restricting user to Enter only Numeric Data
    function NumericOnly() {
        //alert(event.keyCode);
        if (event.keyCode < 37 || event.keyCode > 40) {
            if (event.keyCode != 116 && event.keyCode != 46 && event.keyCode != 190 && event.keyCode != 8 && event.keyCode != 9 && event.keyCode != 16) {
                if (event.keyCode < 48 || event.keyCode > 57) {
                    alert("Please enter Numeric value.");
                    event.returnValue = false;
                }
            }
        }
    }    
</script>
<body>
    <asp:Panel ID="PanelHeader" runat="server" Width="100%" Font-Bold="true" BorderColor="White">
        <table style="width: 80%; height: 100%;" runat="server" cellpadding="0" cellspacing="0"
            border="0">
            <tr>
                <td align="left">
                    <asp:Label ID="LabelHeader" runat="server" Text="Submit Weekly Status" CssClass="HyperLinkMenu"
                        ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PanelProduct" runat="server" Width="100%">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                                        ForeColor="#DB512D" Font-Size="Small" />
                                </td>
                                <td style="width: 50%">
                                    <asp:Label ID="LabelProductValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                        ForeColor="#DB512D" Font-Size="Small" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PanelProductVersion" runat="server" Width="100%">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="LabelProductVersion" runat="server" Text="Product Release:" CssClass="HyperLinkMenu"
                                        ForeColor="#DB512D" Font-Size="Small" />
                                </td>
                                <td style="width: 50%">
                                    <asp:DropDownList ID="DropDownListProductVersion" runat="server" Width="80%" AutoPostBack="true"
                                        OnSelectedIndexChanged="DropDownListProductVersion_OnSelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>
                                    <asp:Label ID="LabelProductVersionValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                        ForeColor="#DB512D" Font-Size="Small" Visible="true" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PanelProjectPhase" runat="server" Width="100%">
                        <table id="Table3" style="width: 100%; height: 100%; color: White" runat="server">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="LabelProjectPhases" runat="server" Text="Project Phase:" CssClass="HyperLinkMenu"
                                        ForeColor="#DB512D" Font-Size="Small" />
                                </td>
                                <td style="width: 50%">
                                    <asp:DropDownList ID="DropDownListProjectPhases" runat="server" Width="80%" AutoPostBack="true"
                                        OnSelectedIndexChanged="DropDownListProjectPhases_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PanelVendor" runat="server" Width="100%">
                        <table id="TableVendor" style="width: 100%; height: 100%; color: White" runat="server">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="LabelVendor" runat="server" Text="Team:" CssClass="HyperLinkMenu"
                                        ForeColor="#DB512D" Font-Size="Small" />
                                </td>
                                <td style="width: 50%">
                                    <asp:Label ID="LabelVendorName" runat="server" Text="" CssClass="HyperLinkMenu" ForeColor="#DB512D"
                                        Font-Size="Small" Visible="false" />
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
                        <table id="TableWeek" style="width: 100%; height: 100%; color: White" runat="server">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="LabelWeek" runat="server" Text="Week:" CssClass="HyperLinkMenu" ForeColor="#DB512D"
                                        Font-Size="Small" />
                                </td>
                                <td style="width: 50%">
                                    <asp:DropDownList ID="DropDownListWeek" runat="server" Width="80%" Visible="false">
                                    </asp:DropDownList>
                                    <asp:Label ID="LabelWeekName" runat="server" Text="WEEK 2" CssClass="HyperLinkMenu"
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
            <tr>
                <td style="height: 20px" align="left">
                    <asp:Button ID="ButtonSubmitWSR" Text="Submit Weekly Status" runat="server" OnClick="ButtonSubmitWSR_Click" />
                    <asp:Button ID="ButtonImport" Text="Import Weekly Status" runat="server" OnClick="ButtonImport_Click"
                        Visible="false" />
                </td>
            </tr>
            <tr>
                <td style="height: 5px">
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="LabelMessage" CssClass="medium" runat="server" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 5px">
                </td>
            </tr>
        </table>
        <ajaxToolkit:TabContainer ID="TabContainerWSR" runat="server">
            <ajaxToolkit:TabPanel ID="TabPanelIssuesandAccomplishments" runat="server" HeaderText="Issues and Accomplishments"
                TabIndex="1" BackColor="#212121">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td>
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
                                            <asp:Label ID="LabelRedIssues" runat="server" Text="Red Issues"></asp:Label>
                                            <%--<font style="color: Red; font-weight: bold">Red</font><font style="color: white"> (Urgent
                                    help needed from management team)
                                    <p>
                                        Items below represent critical issues that are keeping Vendor QE Team from delivering
                                        on schedule. These items need management help in resolving. Stated below issues
                                        must be resolved by scheduled date</p>
                                </font>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBoxRedIssues" runat="server" TextMode="MultiLine" Width="80%"
                                                Height="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelYellowIssues" runat="server" Text="Yellow Issues"></asp:Label>
                                            <%--<font style="color: Yellow; font-weight: bold">Yellow</font> <font style="color: white">
                                    (Issues)
                                    <p>
                                        Items below represent issues that management should be aware of but do not keep
                                        Vendor QE Team from delivering on schedule. These items may not need management
                                        help in resolving. Stated below issues must be resolved by scheduled date.</p>
                                </font>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBoxYellowIssues" runat="server" TextMode="MultiLine" Width="80%"
                                                Height="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelGreenAccomplishments" runat="server" Text="Green Accomplishments"></asp:Label>
                                            <%--<font style="color: Green; font-weight: bold">Green</font> <font style="color: white">
                                    (Key accomplishments, progress, acknowledgments)
                                    <p>
                                        Items below represent accomplishments or progress made on deliverables for the week.</p>
                                </font>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBoxGreenAccomplishments" runat="server" TextMode="MultiLine"
                                                Width="80%" Height="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanelMetrics" runat="server" HeaderText="Metrics" ForeColor="Black"
                TabIndex="2" BackColor="#212121">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td>
                                <table style="width: 100%; background-color: #212121">
                                    <tr style="height: 20px">
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelResources" runat="server" Text="Team Information (worked this week):"
                                                CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%; vertical-align: middle">
                                                <tr align="left">
                                                    <td style="width: 20%">
                                                        <asp:Label ID="LabelResourceCount" runat="server" Text="Team Count:" CssClass="HyperLinkMenu"
                                                            ForeColor="#DB512D" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                                                        <asp:TextBox ID="TextBoxResourcesCount" runat="server" Width="40px" onkeydown="NumericOnly()"></asp:TextBox>
                                                    </td>
                                                    <td align="right" style="width: 60%">
                                                        <asp:Label ID="LabelResourceName" runat="server" Text="Team Names(IDs):" CssClass="HyperLinkMenu"
                                                            ForeColor="#DB512D" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                                                        <asp:TextBox ID="TextBoxResourcesName" runat="server" Width="75%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridViewEffortsTrack" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                Width="100%" OnRowCreated="GridViewEffortsTrack_RowCreated" OnRowDataBound="GridViewEffortsTrack_RowDataBound">
                                                <RowStyle ForeColor="#000066" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="WSR Section">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelWSRSection" runat="server" Text='<%# Bind("WSRSection") %>' Visible="true"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dummy" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelDummyID" runat="server" Text='<%# Bind("WSRParameterID") %>'
                                                                Visible="true"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WSR Parameter">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelWSRDetailID" runat="server" Text='<%# Bind("WSRDetailID") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="LabelWSRParameterID" runat="server" Text='<%# Bind("WSRParameterID") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="LabelWSRParameter" runat="server" Text='<%# Bind("WSRParameter") %>'
                                                                Visible="true"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Revised Quantity" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBoxRevisedQuantity" CssClass="TextBox" runat="server" Width="40px"
                                                                Text='<%# Bind("RevisedQuantity") %>' Visible="true" onkeydown="NumericOnly()"></asp:TextBox>
                                                            <asp:Label ID="LabelRevisedQuantity" runat="server" Text='NA' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle ForeColor="Navy" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actual Efforts (hrs)" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-VerticalAlign="Middle" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelActualEfforts" runat="server" Text='<%# Bind("ActualEfforts") %>'
                                                                Visible="true"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle ForeColor="Navy" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Efforts (hrs)" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBoxRevisedEfforts" CssClass="TextBox" runat="server" Width="40px"
                                                                Text='<%# Bind("RevisedEfforts") %>' Visible="true" onkeydown="NumericOnly()"></asp:TextBox>
                                                            <asp:Label ID="LabelRevisedEfforts" runat="server" Text='NA' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle ForeColor="Navy" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBoxRemarks" CssClass="TextBox" runat="server" Width="90%" Text='<%# Bind("Remarks") %>'
                                                                Visible="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ControlStyle ForeColor="Navy" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#00C0C0" />
                                                <AlternatingRowStyle BackColor="#FFE0C0" />
                                                <SelectedRowStyle BackColor="CornflowerBlue" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="LabelFeaturesTested" runat="server" Text="Features Tested:" CssClass="HyperLinkMenu"
                                                            ForeColor="#DB512D" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxFeaturesTested" runat="server" Width="100%" TextMode="MultiLine"
                                                            Height="50px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="LabelNotes" runat="server" Text="Additional Notes (if any):" CssClass="HyperLinkMenu"
                                                            ForeColor="#DB512D" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxNotes" runat="server" Width="100%" TextMode="MultiLine"
                                                            Height="50px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanelNextWeekPriority" runat="server" HeaderText="Next Week Activities"
                TabIndex="3" BackColor="#212121">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td>
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
                                            <asp:Label ID="LabelBlueDeliverables" runat="server" Text="Blue Deliverables"></asp:Label>
                                            <%--<font style="color: Blue; font-weight: bold">Blue</font> <font style="color: white">
                                    (Outstanding deliverables)
                                    <p>
                                        Items below represent previous week’s deliverables extended into current week. Original
                                        schedule date and the reason for extending originally scheduled date must be included.
                                    </p>
                                </font>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridViewOutstandingDeliverables" runat="server" BackColor="White"
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="5" AutoGenerateColumns="False"
                                                Width="100%" OnRowCommand="GridViewOutstandingDeliverables_OnRowCommand" OnRowDataBound="GridViewOutstandingDeliverables_OnRowDataBound"
                                                OnRowDeleting="GridViewOutstandingDeliverables_OnRowDeleting">
                                                <RowStyle ForeColor="#000066" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="OutDeliverablesID" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelOutDeliverablesID" runat="server" Text='<%# Bind("OutStandingDeliverablesID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tasks" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBoxPrevWeekDeliverables" runat="server" TextMode="MultiLine"
                                                                Text='<%# Bind("PrevWeekDeliverables") %>' Width="100%"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Original Date" Visible="true">
                                                        <ItemTemplate>
                                                            <%-- <asp:TextBox ID="TextBoxOrigScheduleDate" runat="server" TextMode="MultiLine" Text='<%# Bind("OriginalScheduleDate") %>'
                                                    Width="100%"></asp:TextBox>--%>
                                                            <uc1:DateCalendar ID="DateCalendarOrigScheduleDate" runat="server" Date='<%# Bind("OriginalScheduleDate") %>'>
                                                            </uc1:DateCalendar>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reason" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBoxReason" runat="server" TextMode="MultiLine" Text='<%# Bind("Reason") %>'
                                                                Width="100%"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Button ID="ButtonSave" runat="server" CausesValidation="False" CommandName="Save"
                                                                Text="Save Row"></asp:Button>&nbsp;&nbsp;
                                                            <asp:Button ID="ButtonDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                                Text="Delete Row"></asp:Button>
                                                        </ItemTemplate>
                                                        <ControlStyle ForeColor="Navy" />
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
                                        <td>
                                            <asp:Label ID="LabelBlackDeliverables" runat="server" Text="Black Deliverables"></asp:Label>
                                            <%--<font style="color: Black; font-weight: bold">Black</font> <font style="color: white">
                                    (New deliverables)
                                    <p>
                                        Items below represent new deliverables scheduled/planned for the next week.</p>
                                </font>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBoxNewDeliverables" runat="server" TextMode="MultiLine" Width="80%"
                                                Height="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 20px">
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </asp:Panel>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="height: 20px">
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
