<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefineProductMasterDataUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.DefineProductMasterDataUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Calendar.ascx" TagName="DateCalendar" TagPrefix="uc1" %>
<asp:Panel ID="Panel" runat="server" Width="100%" Style="position: relative;">
    <table style="width: 80%; color: White">
        <tr>
            <td align="left" colspan="2">
                <asp:Label ID="LabelHeader" runat="server" Text="Define Product Master Data" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 20px" colspan="2">
            </td>
        </tr>
        <tr style="height: 20px">
            <td style="width: 50%">
                <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" />
            </td>
            <td style="width: 50%">
                <asp:Label ID="LabelProductValue" runat="server" Text="Default" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" />
            </td>
        </tr>
        <tr style="height: 20px">
            <td style="width: 50%">
                <asp:Label ID="LabelProductVersion" runat="server" Text="Product Release:" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" />
            </td>
            <td style="width: 50%">
                <asp:DropDownList ID="DropDownListProductVersion" runat="server" Width="50%" AutoPostBack="true"
                    Visible="false" OnSelectedIndexChanged="DropDownListProductVersion_OnSelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label ID="LabelProductVersionValue" runat="server" Text="" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" Visible="true" />
            </td>
        </tr>
        <tr>
            <td style="height: 20px" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="LabelMessage" CssClass="medium" runat="server" Text="" Width="100%"
                    ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr style="height: 5px">
            <td>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr style="width: 100%">
            <td style="width: 100%">
                <ajaxToolkit:TabContainer ID="TabContainerPopulateProductMasterDataDetails" runat="server">
                    <ajaxToolkit:TabPanel ID="TabPanelLocalesandPlatforms" runat="server" HeaderText="Locales and Platforms"
                        TabIndex="0">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td align="center">
                                        <table style="width: 100%; background-color: #212121">
                                            <tr>
                                                <td style="height: 20px" colspan="5">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 2%">
                                                </td>
                                                <td colspan="4" align="left">
                                                    <asp:Button ID="ButtonUpdateLocalesandPlatforms" runat="server" Text="Update Locales and Platforms"
                                                        OnClick="ButtonUpdateLocalesandPlatforms_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="5">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="width: 47%">
                                                    <asp:GridView ID="GridViewLocales" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                        Width="100%" OnRowDataBound="GridViewLocales_RowDataBound">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="LocaleID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelLocaleID" runat="server" Text='<%# Bind("LocaleID") %>' Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Locale Tiers">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelLocaleTiers" runat="server" Text='<%# Bind("Tier") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Locales" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelLocales" runat="server" Text='<%# Bind("Locale") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBoxLocalesSelected" runat="server" Width="80%" Checked="false">
                                                                    </asp:CheckBox>
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
                                                <td style="width: 47%">
                                                    <asp:GridView ID="GridViewPlatforms" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                        Width="100%" OnRowDataBound="GridViewPlatforms_RowDataBound">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ProductPlatformID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelProductPlatformID" runat="server" Text='<%# Bind("ProductPlatformID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PlatformID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelPlatformID" runat="server" Text='<%# Bind("PlatformID") %>' Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Platform Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelPlatformType" runat="server" Text='<%# Bind("PlatformType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Platform" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelPlatform" runat="server" Text='<%# Bind("Platform") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Priority" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TextBoxPlatformPriority" runat="server" Text='<%# Bind("Priority") %>'
                                                                        Width="80%"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBoxPlatformsSelected" runat="server" Width="80%" Checked="false">
                                                                    </asp:CheckBox>
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
                                            <tr>
                                                <td style="height: 20px" colspan="5">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanelDefineSprints" runat="server" HeaderText="Define Product Sprints"
                        TabIndex="1">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td align="center">
                                        <table style="width: 100%; background-color: #212121">
                                            <tr>
                                                <td style="height: 20px" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="width: 96%">
                                                    <asp:GridView ID="GridViewDefineProductSprints" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                        Width="100%" OnRowDataBound="GridViewDefineProductSprints_RowDataBound" OnRowUpdating="GridViewDefineProductSprints_RowUpdating"
                                                        OnRowCancelingEdit="GridViewDefineProductSprints_RowCancelingEdit">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelProductSprintID" runat="server" Text='<%# Bind("ProductSprintID") %>'
                                                                        Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sprint">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelProductSprint" runat="server" Text='<%# Bind("Sprint") %>' Visible="true"></asp:Label>
                                                                    <asp:TextBox ID="TextBoxProductSprint" CssClass="TextBox" runat="server" Width="80%"
                                                                        Visible="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sprint Details">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelProductSprintDetails" runat="server" Text='<%# Bind("SprintDetails") %>'
                                                                        Visible="true"></asp:Label>
                                                                    <asp:TextBox ID="TextBoxProductSprintDetails" CssClass="TextBox" runat="server" Width="80%"
                                                                        Visible="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Start Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelSprintStartDate" runat="server" Text='<%# Bind("StartDate") %>'
                                                                        Visible="true"></asp:Label>
                                                                    <uc1:DateCalendar ID="DateCalendarSprintStartDate" runat="server" Visible="false" />
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelSprintEndDate" runat="server" Text='<%# Bind("EndDate") %>' Visible="true"></asp:Label>
                                                                    <uc1:DateCalendar ID="DateCalendarSprintEndDate" runat="server" Visible="false" />
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
                                                <td style="width: 2%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" colspan="3">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanelDefineBuilds" runat="server" HeaderText="Define Language Groups"
                        TabIndex="2">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td align="center">
                                        <table style="width: 100%; background-color: #212121">
                                            <tr>
                                                <td style="height: 20px" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="width: 96%">
                                                    <asp:GridView ID="GridViewProductBuilds" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                        Width="100%" OnRowDataBound="GridViewProductBuilds_RowDataBound" OnRowUpdating="GridViewProductBuilds_RowUpdating"
                                                        OnRowCancelingEdit="GridViewProductBuilds_RowCancelingEdit">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelProjectBuildDetailID" runat="server" Text='<%# Bind("ProjectBuildDetailID") %>'
                                                                        Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Language Group Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelProjectBuildCode" runat="server" Text='<%# Bind("ProjectBuildCode") %>'
                                                                        Visible="true"></asp:Label>
                                                                    <asp:TextBox ID="TextBoxProjectBuildCode" CssClass="TextBox" runat="server" Width="80%"
                                                                        Visible="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Language Group">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelProjectBuildDetails" runat="server" Text='<%# Bind("ProjectBuild") %>'
                                                                        Visible="true"></asp:Label>
                                                                    <asp:TextBox ID="TextBoxProjectBuildDetails" CssClass="TextBox" runat="server" Width="80%"
                                                                        Visible="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Release Build">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBoxReleaseBuildSelected" runat="server" Width="80%" Checked="false">
                                                                    </asp:CheckBox>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButtonSave" runat="server" CausesValidation="False" CommandName="Add"
                                                                        Visible="false" Text="SAVE" OnCommand="ProductBuildsLinkButtonSaveDetails_Click"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="False" CommandName="Update"
                                                                        Text="UPDATE" OnCommand="ProductBuildsLinkButtonUpdateDetails_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        Visible="true" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                        Visible="false" Text="CANCEL" OnCommand="ProductBuildsLinkButtonCancelDetails_Click"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                    <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                                        Visible="true" Text="DELETE" OnCommand="ProductBuildsLinkButtonDeleteDetails_Click"
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
                                            <tr>
                                                <td style="height: 20px" colspan="3">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanelProjectBuildLocales" runat="server" HeaderText="Define Language Group-Locales Association"
                        TabIndex="3">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td align="center">
                                        <table style="width: 100%; background-color: #212121">
                                            <tr>
                                                <td style="height: 20px" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 2%">
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:Button ID="ButtonUpdateProjectBuildLocales" runat="server" Text="Update Locales Association"
                                                        OnClick="ButtonUpdateProjectBuildLocales_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 3px" colspan="5">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="width: 96%">
                                                    <asp:GridView ID="GridViewProjectBuildLocales" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                        Width="100%" OnRowDataBound="GridViewProjectBuildLocales_RowDataBound">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelProjectBuildLocaleID" runat="server"></asp:Label>
                                                                    <asp:Label ID="LabelProductLocaleID" Text='<%# Bind("ProductLocaleID") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Locale Tiers">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelLocaleTiers" runat="server" Text='<%# Bind("Tier") %>' Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Locales" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelLocale" runat="server" Text='<%# Bind("Locale") %>' Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <ControlStyle ForeColor="Navy" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Language Group">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelProjectBuildDetails" runat="server" Visible="false"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownListProjectBuild" runat="server" Visible="false" />
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
                                            <tr>
                                                <td style="height: 20px" colspan="3">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
    </table>
</asp:Panel>
<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td>
            <asp:Panel ID="PanelFooterPadding" runat="server" Width="100%" Height="100%" Visible="false">
            </asp:Panel>
        </td>
    </tr>
</table>
