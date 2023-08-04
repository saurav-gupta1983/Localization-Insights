<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefineProjectWindUpUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.DefineProjectWindUpUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Calendar.ascx" TagName="DateCalendar" TagPrefix="uc1" %>
<asp:Panel ID="Panel" runat="server" Width="100%">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <h1>
                    <asp:Label ID="LabelHeader" runat="server" Text="Project Wind-up / Closure" ForeColor="#DB512D"
                        Font-Underline="true" Font-Size="Medium"></asp:Label>
                </h1>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="text-align: left">
        <tr style="height: 20px">
            <td style="width: 40%">
                <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" />
            </td>
            <td>
                <asp:Label ID="LabelProductValue" runat="server" Text="Illustrator" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" />
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
                <asp:Label ID="LabelProductVersion" runat="server" Text="Product Release:" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" />
            </td>
            <td>
                <asp:DropDownList ID="DropDownListProductVersion" runat="server" Width="50%" AutoPostBack="true"
                    Visible="false" OnSelectedIndexChanged="DropDownListProductVersion_OnSelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label ID="LabelProductVersionValue" runat="server" Text="" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" Visible="true" />
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
                <asp:Label ID="LabelWindUpID" CssClass="medium" runat="server" Width="100%" Visible="false" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelPopulateProjectWindUpDetails" runat="server" Width="100%" Visible="true">
        <table style="width: 100%; background-color: #212121">
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Button ID="ButtonSaveProjectWindUpDetails" runat="server" Text="Save Project Wind-Up Details"
                        OnClick="ButtonSaveProjectWindUpDetails_Click" />
                    <asp:Button ID="ButtonSubmitWindUpDetails" runat="server" Text="Submit Wind-Up Details"
                        OnClick="ButtonSubmitWindUpDetails_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelMessage" CssClass="medium" runat="server" Text="" Width="100%"
                        ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr style="height: 10px">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelGMDetails" Text="Product GM Details" runat="server" ForeColor="#DB512D"
                        Font-Underline="True" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelNoProjectBuildsAvailable" runat="server" ForeColor="AntiqueWhite"
                        Text="There are no Builds defined for this release." Font-Size="Small" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="GridViewMasterDataDetails" runat="server" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                        Width="100%" OnRowDataBound="GridViewMasterDataDetails_RowDataBound"
                        OnRowUpdating="GridViewMasterDataDetails_RowUpdating" OnRowCancelingEdit="GridViewMasterDataDetails_RowCancelingEdit">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                            <asp:TemplateField HeaderText="ID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProjectBuildDetailID" runat="server" Text='<%# Bind("ProjectBuildDetailID") %>'
                                        Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Build">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProjectBuildDetails" runat="server" Text='<%# Bind("ProjectBuild") %>'
                                        Visible="true"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Build Code">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProjectBuildCode" runat="server" Text='<%# Bind("ProjectBuildCode") %>'
                                        Visible="true"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Build#">
                                <ItemTemplate>
                                    <asp:Label ID="LabelBuildNo" runat="server" Text='<%# Bind("GMBuildNo") %>' Visible="true"></asp:Label>
                                    <asp:TextBox ID="TextBoxBuildNo" CssClass="TextBox" Text='<%# Bind("GMBuildNo") %>'
                                        runat="server" Width="80%" Visible="false"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GM Date">
                                <ItemTemplate>
                                    <asp:Label ID="LabelGMDate" runat="server" Text='<%# Bind("GMDate") %>' Visible="true"></asp:Label>
                                    <uc1:DateCalendar ID="DateCalendarGMDate" runat="server" Visible="false" />
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Build Path">
                                <ItemTemplate>
                                    <asp:Label ID="LabelBuildPath" runat="server" Text='<%# Bind("GMBuildPath") %>' Visible="true"></asp:Label>
                                    <asp:TextBox ID="TextBoxBuildPath" CssClass="TextBox" Text='<%# Bind("GMBuildPath") %>'
                                        runat="server" Width="80%" Visible="false"></asp:TextBox>
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
                                        Enabled="false" Visible="true" Text="DELETE" OnCommand="LinkButtonDeleteDetails_Click"
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
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelComments" Text="Post Mortem:" runat="server" ForeColor="AntiqueWhite"
                        Font-Underline="True" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelShowComments" runat="server" ForeColor="AntiqueWhite" Visible="false"></asp:Label>
                    <asp:TextBox ID="TextBoxComments" runat="server" Visible="true" Height="100px" TextMode="MultiLine"
                        Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelLearnings" Text="Learnings:" runat="server" ForeColor="AntiqueWhite"
                        Font-Underline="True" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelShowLearnings" runat="server" ForeColor="AntiqueWhite" Visible="false"></asp:Label>
                    <asp:TextBox ID="TextBoxLearnings" runat="server" Visible="true" Height="100px" TextMode="MultiLine"
                        Width="100%"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelBestPractices" Text="Best Practices:" runat="server" ForeColor="AntiqueWhite"
                        Font-Underline="True" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelShowBestPractices" runat="server" ForeColor="AntiqueWhite" Visible="false"></asp:Label>
                    <asp:TextBox ID="TextBoxBestPractices" runat="server" Visible="true" Height="100px"
                        TextMode="MultiLine" Width="100%"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
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
</asp:Panel>
