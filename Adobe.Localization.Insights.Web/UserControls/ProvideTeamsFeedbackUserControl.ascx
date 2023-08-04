<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProvideTeamsFeedbackUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.ProvideTeamsFeedbackUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="Panel" runat="server" Width="100%">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left">
                <h1>
                    <asp:Label ID="LabelHeader" runat="server" Text="Provide Teams Feedback" ForeColor="#DB512D"
                        Font-Underline="true" Font-Size="Medium"></asp:Label>
                </h1>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="PanelSearchFilter" GroupingText="Search Feedback" CssClass="Panel"
        Width="100%" Font-Bold="True">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="height: 20px" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
                <td style="width: 50%">
                    <asp:Label ID="LabelProduct" CssClass="medium" runat="server" Text="Product:" ForeColor="#DB512D"></asp:Label>
                </td>
                <td style="width: 50%">
                    <table cellpadding="0" cellspacing="0" border="0" width="80%">
                        <tr>
                            <td style="width: 49%">
                                <asp:DropDownList ID="DropDownListProducts" CssClass="TextBox" runat="server" OnSelectedIndexChanged="DropDownListProducts_OnSelectedIndexChanged"
                                    AutoPostBack="true" Width="100%">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 49%">
                                <asp:DropDownList ID="DropDownListProductVersions" CssClass="TextBox" runat="server"
                                    Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DropDownListProductVersions_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
                <td style="width: 50%">
                    <asp:Label ID="LabelLoggedForTeam" CssClass="medium" runat="server" Text="Logged for Team (By You & For You):"
                        ForeColor="#DB512D"></asp:Label>
                </td>
                <td style="width: 50%">
                    <asp:DropDownList ID="DropDownListLoggedForTeam" CssClass="TextBox" runat="server"
                        Width="80%" OnSelectedIndexChanged="DropDownListLoggedForTeam_OnSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelMessages" runat="server" Width="100%" Visible="true">
        <table style="width: 100%; height: 100%;">
            <tr>
                <td style="height: 20px" colspan="2">
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
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelFeedbackLogged" runat="server" Width="100%">
        <ajaxToolkit:TabContainer ID="TabContainerTeamFeedback" runat="server"><ajaxToolkit:TabPanel ID="TabPanelLoggedByYourTeam" runat="server" HeaderText="Provide Feedback"
                TabIndex="0">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td align="center">
                                <asp:GridView ID="GridViewFeedback" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    PageSize="20" Width="100%" AllowPaging="True" OnRowDataBound="GridViewFeedback_RowDataBound"
                                    OnRowUpdating="GridViewFeedback_RowUpdating" OnRowCancelingEdit="GridViewFeedback_RowCancelingEdit"
                                    OnPageIndexChanging="GridViewFeedback_PageIndexChanging">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                                        <asp:TemplateField HeaderText="TeamFeedbackID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelTeamFeedbackID" runat="server" Text='<%# Bind("TeamFeedbackID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Incident">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelIncidentDetails" runat="server" Text='<%# Bind("IncidentDetails") %>'
                                                    Visible="true"></asp:Label>
                                                <asp:TextBox ID="TextBoxIncidentDetails" CssClass="TextBox" runat="server" Width="100%"
                                                    Visible="false" Text='<%# Bind("IncidentDetails") %>' TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProductVersionID" runat="server" Text='<%# Bind("ProductVersionID") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="LabelProductVersion" runat="server" Text='<%# Bind("ProductVersionID") %>'
                                                    Visible="true"></asp:Label>
                                                <asp:DropDownList ID="DropDownListProduct" runat="server" Visible="false" AutoPostBack="true"
                                                    OnSelectedIndexChanged="DropDownListProduct_OnSelectedIndexChanged" />
                                                <br />
                                                <asp:DropDownList ID="DropDownListProductVersion" runat="server" Visible="false" />
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Logged For team">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelVendor" runat="server" Text='<%# Bind("VendorID") %>' Visible="true"></asp:Label>
                                                <asp:DropDownList ID="DropDownListSelectVendor" runat="server" Visible="false" Width="95%" />
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Severity">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSeverity" runat="server" Text='<%# Bind("Severity") %>' Visible="true"></asp:Label>
                                                <asp:DropDownList ID="DropDownListSeverity" runat="server" Width="95%" Visible="false">
                                                    <asp:ListItem Selected="true" Text="Sev 0  -   Appreciations" Value="0"></asp:ListItem>
                                                    <asp:ListItem Selected="false" Text="Sev 1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Selected="false" Text="Sev 2" Value="2"></asp:ListItem>
                                                    <asp:ListItem Selected="false" Text="Sev 3" Value="3"></asp:ListItem>
                                                    <asp:ListItem Selected="false" Text="Sev 4" Value="4"></asp:ListItem>
                                                    <asp:ListItem Selected="false" Text="Sev 5" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Logged By">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelLoggedBy" runat="server" Text='<%# Bind("AddedBy") %>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonSave" runat="server" CausesValidation="False" CommandName="Add"
                                                    Visible="false" Text="SAVE" OnCommand="SaveFeedbackDetailsLinkButton_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="False" CommandName="Update"
                                                    Text="UPDATE" OnCommand="UpdateFeedbackDetailsLinkButton_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    Visible="true" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Visible="false" Text="CANCEL" OnCommand="CancelFeedbackDetailsLinkButton_Click"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Visible="true" Text="DELETE" OnCommand="DeleteFeedbackDetailsLinkButton_Click"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel><ajaxToolkit:TabPanel ID="TabPanelLoggedForYourTeam" runat="server" HeaderText="Release Feedback"
                TabIndex="1">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td align="center">
                                <asp:GridView ID="GridViewLoggedTeamFeedback" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    PageSize="20" Width="100%" AllowPaging="True" OnRowDataBound="GridViewLoggedTeamFeedback_RowDataBound"
                                    OnPageIndexChanging="GridViewLoggedTeamFeedback_PageIndexChanging">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                                        <asp:TemplateField HeaderText="Incident">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelIncidentDetails" runat="server" Text='<%# Bind("IncidentDetails") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product / ProductRelease">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProductVersion" runat="server" Text='<%# Bind("ProductVersionID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Severity">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSeverity" runat="server" Text='<%# Bind("Severity") %>' Visible="true"></asp:Label>
                                                <asp:DropDownList ID="DropDownListSeverity" runat="server" Width="80%" Visible="false">
                                                    <asp:ListItem Selected="true" Text="Sev 0  -   Appreciations" Value="0"></asp:ListItem>
                                                    <asp:ListItem Selected="false" Text="Sev 1  -   Reminded to follow instructions"
                                                        Value="1"></asp:ListItem>
                                                    <asp:ListItem Selected="false" Text="Sev 2" Value="2"></asp:ListItem>
                                                    <asp:ListItem Selected="false" Text="Sev 3" Value="3"></asp:ListItem>
                                                    <asp:ListItem Selected="false" Text="Sev 4" Value="4"></asp:ListItem>
                                                    <asp:ListItem Selected="false" Text="Sev 5 - Did not execute the test case but reported as Executed"
                                                        Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Logged By">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelLoggedBy" runat="server" Text='<%# Bind("AddedBy") %>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#00C0C0" />
                                    <AlternatingRowStyle BackColor="#FFE0C0" />
                                    <SelectedRowStyle BackColor="CornflowerBlue" />
                                </asp:GridView>
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel></ajaxToolkit:TabContainer>
    </asp:Panel>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="PanelFooterPadding" runat="server" Width="100%" Height="100%" Visible="false">
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
