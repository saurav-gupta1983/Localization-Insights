<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintainUserProfileAndPreferencesUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.MaintainUserProfileAndPreferencesUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="Panel" runat="server" Width="100%">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left">
                <h1>
                    <asp:Label ID="LabelHeader" runat="server" Text="Maintain User Profile and Preferences" ForeColor="#DB512D" Font-Underline="true"
                        Font-Size="Medium"></asp:Label>
                </h1>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="text-align: left">
        <tr>
            <td align="left">
                <asp:Button ID="ButtonSaveUserInformation" runat="server" Text="Save User Profile and Preferences" OnClick="ButtonSaveUserInformation_Click"
                    Visible="true" />
            </td>
        </tr>
        <tr style="height: 10px">
            <td>
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelMessages" runat="server" Width="100%" Visible="true">
        <table style="width: 100%; height: 100%;">
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
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelUserProfileInformation" runat="server" Width="100%" Visible="true">
        <table style="width: 80%; height: 100%;">
            <tr style="height: 20px">
                <td style="width: 40%">
                    <asp:Label ID="LabelLoginID" runat="server" Text="Login ID:" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                </td>
                <td>
                    <asp:Label ID="LabelLoginIDValue" runat="server" Text="" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                    <asp:Label ID="LabelUserIDValue" runat="server" Text="" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" Visible="false" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td style="width: 40%">
                    <asp:Label ID="LabelVendor" runat="server" Text="Team:" ForeColor="AntiqueWhite"
                        Font-Size="Small" />
                </td>
                <td>
                    <asp:Label ID="LabelVendorValue" runat="server" Text="" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                    <asp:Label ID="LabelVendorID" runat="server" Text="" CssClass="HyperLinkMenu" ForeColor="AntiqueWhite"
                        Font-Size="Small" Visible="false" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelFirstName" runat="server" Text="First Name:" ForeColor="AntiqueWhite"
                        Font-Size="Small" />
                </td>
                <td>
                    <asp:TextBox ID="TextBoxFirstName" CssClass="TextBox" runat="server" Width="80%"
                        Visible="true"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelLastName" runat="server" Text="Last Name:" ForeColor="AntiqueWhite"
                        Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxLastName" CssClass="TextBox" runat="server" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="2">
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelNickName" runat="server" Text="Nick Name:" ForeColor="AntiqueWhite"
                        Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxNickName" CssClass="TextBox" runat="server" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelEmailID" runat="server" Text="Email ID:" ForeColor="AntiqueWhite"
                        Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxEmailID" CssClass="TextBox" runat="server" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelAlternateEmailID" runat="server" Text="Alternate Email ID:" ForeColor="AntiqueWhite"
                        Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxAlternateEmailID" CssClass="TextBox" runat="server" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelContactNo" runat="server" Text="Contact No:" ForeColor="AntiqueWhite"
                        Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxContactNo" CssClass="TextBox" runat="server" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelManagerLoginID" runat="server" Text="Manager LoginID:" ForeColor="AntiqueWhite"
                        Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxManagerLoginID" CssClass="TextBox" runat="server" Width="80%"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelSetVisibleProducts" runat="server" Width="100%" Visible="true">
        <table style="width: 100%; background-color: #212121">
            <tr style="height: 20px">
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td style="width: 35%">
                    <asp:GridView ID="GridViewProducts" runat="server" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                        Width="90%" OnRowDataBound="GridViewProducts_RowDataBound">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:TemplateField HeaderText="UserProductPreferenceID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LabelUserProductPreferenceID" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ProductID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProductID" runat="server" Text='<%# Bind("ProductID") %>' Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProduct" runat="server" Text='<%# Bind("Product") %>' Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBoxProductSelected" runat="server" Width="80%"></asp:CheckBox>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#00C0C0" />
                        <AlternatingRowStyle BackColor="#FFE0C0" />
                        <SelectedRowStyle BackColor="CornflowerBlue" />
                    </asp:GridView>
                </td>
                <td style="width: 1%">
                </td>
                <td style="width: 63%">
                    <asp:GridView ID="GridViewProjectRoles" runat="server" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                        Width="90%">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:TemplateField HeaderText="Project Role">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProjectRole" runat="server" Text='<%# Bind("ProjectRole") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProduct" runat="server" Text='<%# Bind("Product") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#00C0C0" />
                        <AlternatingRowStyle BackColor="#FFE0C0" />
                        <SelectedRowStyle BackColor="CornflowerBlue" />
                    </asp:GridView>
                </td>
                <td style="width: 1%">
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
