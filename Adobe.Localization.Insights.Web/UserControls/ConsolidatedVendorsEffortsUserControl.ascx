<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsolidatedVendorsEffortsUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.ConsolidatedVendorsEffortsUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Calendar.ascx" TagName="DateCalendar" TagPrefix="uc1" %>
<asp:Panel ID="PanelFilter" runat="server" Width="100%" Font-Bold="true" BorderColor="White"
    ForeColor="AntiqueWhite">
    <table style="width: 100%; height: 100%; color: White" runat="server">
        <tr>
            <td align="left">
                <asp:Label ID="LabelHeader" runat="server" Text="View Consolidated Daily Efforts By Team / User" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelVendor" runat="server" Width="100%">
                    <table id="TableVendor" style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelSelectVendor" runat="server" Text="Team:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="LabelSelectVendorValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                                <asp:DropDownList ID="DropDownListVendor" runat="server" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownListVendor_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelSelectUser" runat="server" Width="100%">
                    <table id="Table2" style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelSelectUser" runat="server" Text="User:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="LabelSelectUserValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                                <asp:DropDownList ID="DropDownListUser" runat="server" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownListUser_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelProduct" runat="server" Width="100%">
                    <table style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProductValue" runat="server" Text="" CssClass="HyperLinkMenu"
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
                    <table style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProductVersion" runat="server" Text="Product Release:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProductVersionValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                                <asp:DropDownList ID="DropDownListProductVersion" runat="server" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownListProductVersion_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelProjectPhase" runat="server" Width="100%">
                    <table style="width: 100%; height: 100%; color: White" runat="server">
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
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelWeekStartDate" runat="server" Width="100%">
                    <table id="TableWeekStartDate" style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelWeekStartDate" runat="server" Text="Week Date:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <uc1:DateCalendar ID="DateCalendarWeekStartDate" runat="server" Width="80%" Visible="true" />
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
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%--        <tr>
            <td>
                <asp:Label ID="LabelMessage" CssClass="medium" runat="server" Text="" Width="100%"
                    ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr style="height: 5px">
            <td>
            </td>
        </tr>
        --%>
        <tr>
            <td>
                <asp:Panel ID="WrapperPanelEffortsData" runat="server">
                    <table style="width: 100%; background-color: #212121">
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewEffortsTrack" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="100%">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="WSR Section">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelWSRSection" runat="server" Text='<%# Bind("WSRSection") %>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelWSRParameterID" runat="server" Text='<%# Bind("WSRParameterID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WSR Parameter">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelWSRParameter" runat="server" Text='<%# Bind("WSRParameter") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day1" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelEffortsDay1" runat="server" Text='<%# Bind("EffortsDay1") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day2" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelEffortsDay2" runat="server" Text='<%# Bind("EffortsDay2") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day3" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelEffortsDay3" runat="server" Text='<%# Bind("EffortsDay3") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day4" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelEffortsDay4" runat="server" Text='<%# Bind("EffortsDay4") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day5" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelEffortsDay5" runat="server" Text='<%# Bind("EffortsDay5") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day6" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelEffortsDay6" runat="server" Text='<%# Bind("EffortsDay6") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day7" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelEffortsDay7" runat="server" Text='<%# Bind("EffortsDay7") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelRemarks" runat="server" Width="90%" Text='<%# Bind("Remarks") %>'
                                                        Visible="true"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle ForeColor="Navy" />
                                            </asp:TemplateField>--%>
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
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="PanelFooterPadding" runat="server" Width="100%" Height="100%" Visible="false">
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
