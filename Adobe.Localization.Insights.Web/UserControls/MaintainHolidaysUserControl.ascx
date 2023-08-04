<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintainHolidaysUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.MaintainHolidaysUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Calendar.ascx" TagName="DateCalendar" TagPrefix="uc1" %>
<%--<asp:Panel ID="WrapperPanel" runat="server" Width="90%" Style="left: 30px; position: relative;
    top: 20px">
--%>
<asp:Panel ID="Panel" runat="server" Width="100%" Style="position: relative;">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left">
                <h1>
                    <asp:Label ID="LabelHeader" runat="server" Text="Maintain Holidays List" ForeColor="#DB512D" Font-Underline="true"
                        Font-Size="Medium"></asp:Label>
                </h1>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="PanelSearchFilter" GroupingText="Search Holidays" Width="100%"
        CssClass="Panel" Font-Bold="True">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PanelWeek" runat="server" Width="100%">
                        <table id="TableWeek" style="width: 100%; height: 100%; color: White" runat="server">
                            <tr>
                                <td style="height: 20px">
                                </td>
                                <td style="width: 50%">
                                    <asp:Label ID="LabelDate" CssClass="medium" runat="server" Text="Date:" ForeColor="#DB512D"></asp:Label>
                                </td>
                                <td style="width: 50%">
                                    <asp:DropDownList ID="DropDownListReportingType" runat="server" Width="30%" AutoPostBack="true"
                                        OnSelectedIndexChanged="DropDownListReportingType_OnSelectedIndexChanged">
                                        <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                                        <asp:ListItem Text="Yearly" Value="Yearly"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DropDownListReportingDate" runat="server" Width="50%" AutoPostBack="true"
                                        OnSelectedIndexChanged="DropDownListReportingDate_OnSelectedIndexChanged">
                                    </asp:DropDownList>
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
            <tr>
                <td>
                    <asp:Panel ID="PanelVendor" runat="server" Width="100%">
                        <table id="TableVendor" style="width: 100%; height: 100%; color: White" runat="server">
                            <tr>
                                <td style="height: 20px">
                                </td>
                                <td style="width: 50%">
                                    <asp:Label ID="LabelVendor" CssClass="medium" runat="server" Text="Team:" ForeColor="#DB512D"></asp:Label>
                                </td>
                                <td style="width: 50%">
                                    <asp:Label ID="LabelVendorValue" CssClass="medium" runat="server" Text="Vendor" ForeColor="#DB512D"
                                        Visible="false"></asp:Label>
                                    <asp:DropDownList ID="DropDownListVendors" CssClass="TextBox" runat="server" Width="80%"
                                        AutoPostBack="true" OnSelectedIndexChanged="DropDownListVendors_OnSelectedIndexChanged">
                                    </asp:DropDownList>
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
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelMessage" CssClass="medium" runat="server" Text="" Width="100%"
                    ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr style="height: 5px">
            <td>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="GridViewHolidayDetails" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                    Width="100%" OnRowDataBound="GridViewHolidayDetails_RowDataBound" OnRowUpdating="GridViewHolidayDetails_RowUpdating"
                    OnRowCancelingEdit="GridViewHolidayDetails_RowCancelingEdit">
                    <RowStyle ForeColor="#000066" Height="20px" />
                    <Columns>
                        <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                        <asp:TemplateField HeaderText="ID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="LabelHolidayID" runat="server" Text='<%# Bind("HolidayID") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Team">
                            <ItemTemplate>
                                <%--<asp:Label ID="LabelVendorID" runat="server" Text='<%# Bind("VendorID") %>' Visible="false"></asp:Label>--%>
                                <asp:Label ID="LabelVendor" runat="server" Text='<%# Bind("Vendor") %>' Visible="true"></asp:Label>
                                <asp:DropDownList ID="DropDownListSelectVendor" CssClass="DropDownList" runat="server"
                                    Width="80%" Visible="false">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Navy" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="LabelHolidayReason" runat="server" Text='<%# Bind("HolidayReason") %>'
                                    Visible="true"></asp:Label>
                                <asp:TextBox ID="TextBoxHolidayReason" CssClass="TextBox" runat="server" Width="80%"
                                    Text='<%# Bind("HolidayReason") %>' Visible="false"></asp:TextBox>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Navy" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Date">
                            <ItemTemplate>
                                <asp:Label ID="LabelStartDate" runat="server" Text='<%# Bind("StartDate") %>' Visible="true"></asp:Label>
                                <uc1:DateCalendar ID="DateCalendarStartDate" runat="server" Date='<%# Bind("StartDate") %>'
                                    Visible="false"></uc1:DateCalendar>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Navy" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date">
                            <ItemTemplate>
                                <asp:Label ID="LabelEndDate" runat="server" Text='<%# Bind("EndDate") %>' Visible="true"></asp:Label>
                                <uc1:DateCalendar ID="DateCalendarEndDate" runat="server" Date='<%# Bind("EndDate") %>'
                                    Visible="false"></uc1:DateCalendar>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Navy" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonSave" runat="server" CausesValidation="False" CommandName="Add"
                                    Visible="false" Text="SAVE" OnCommand="LinkButtonSaveDetails_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="False" CommandName="Update"
                                    Text="UPDATE" OnCommand="LinkButtonUpdateDetails_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    Visible="true" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Visible="false" Text="CANCEL" OnCommand="LinkButtonCancelDetails_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Visible="true" Text="DELETE" OnCommand="LinkButtonDeleteDetails_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                            <ControlStyle ForeColor="Navy" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#00C0C0" />
                    <AlternatingRowStyle BackColor="#FFE0C0" />
                    <SelectedRowStyle BackColor="CornflowerBlue" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelFooterPadding" runat="server" Width="100%" Height="100%" Visible="false">
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
<%--</asp:Panel>--%>
