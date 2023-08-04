<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintainScreenLabelsUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.MaintainScreenLabelsUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="Panel" runat="server" Width="100%">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left">
                <h1>
                    <asp:Label ID="LabelHeader" runat="server" Text="Define Screen Labels" ForeColor="#DB512D" Font-Underline="true"
                        Font-Size="Medium"></asp:Label>
                </h1>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="PanelSearchFilter" GroupingText="Search Screen Labels" Width="100%"
        CssClass="Panel" ForeColor="AntiqueWhite" Font-Bold="True">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="height: 20px" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
                <td style="width: 50%">
                    <asp:Label ID="LabelScreen" CssClass="medium" runat="server" Text="Screen:" ForeColor="#DB512D"></asp:Label>
                </td>
                <td style="width: 50%">
                    <asp:DropDownList ID="DropDownListScreen" runat="server" Width="80%" AutoPostBack="true"
                        OnSelectedIndexChanged="DropDownListScreen_OnSelectedIndexChanged" />
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
                    <asp:Label ID="LabelLocales" CssClass="medium" runat="server" Text="Locales:" ForeColor="#DB512D"></asp:Label>
                </td>
                <td style="width: 50%">
                    <asp:DropDownList ID="DropDownListLocales" CssClass="TextBox" runat="server" Width="80%"
                        OnSelectedIndexChanged="DropDownListLocales_OnSelectedIndexChanged" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelSaveScreenLabels" runat="server" Width="100%">
        <table style="width: 100%; height: 100%;">
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 24px">
                    <asp:Button ID="ButtonSaveScreenLabels" runat="server" Text="Save Screen Labels" OnClick="ButtonSaveScreenLabels_Click"
                        Visible="true" />
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
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
    <asp:Panel ID="PanelScreenLabels" runat="server" Width="100%" Visible="true">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="center">
                    <asp:GridView ID="GridViewScreenLabels" runat="server" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                        Width="100%">
                        <RowStyle ForeColor="#000066" Height="20px" />
                        <Columns>
                            <asp:TemplateField HeaderText="ScreenLocalizedLabelID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LabelScreenLocalizedLabelID" runat="server" Text='<%# Bind("ScreenLocalizedLabelID") %>'
                                        Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ScreenLabelID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LabelScreenLabelID" runat="server" Text='<%# Bind("ScreenLabelID") %>'
                                        Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Screen Label" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="LabelScreenLabel" runat="server" Text='<%# Bind("ScreenLabel") %>'
                                        Visible="true"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="English (en-US) Value" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="LabelEnglish" runat="server" Text='<%# Bind("ScreenValue") %>' Visible="true"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Localized">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBoxLocalizedValue" CssClass="TextBox" runat="server" Width="500px"
                                        TextMode="MultiLine" Text='<%# Bind("LocalizedValue") %>'></asp:TextBox>
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
</asp:Panel>
