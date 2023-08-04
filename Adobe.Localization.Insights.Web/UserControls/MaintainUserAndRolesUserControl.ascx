<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintainUserAndRolesUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.MaintainUserAndRolesUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="Panel" runat="server" Width="100%">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left">
                <h1>
                    <asp:Label ID="LabelHeader" runat="server" Text="Maintain User, Roles and Products Assignment"
                        ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
                </h1>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="PanelSearchFilter" GroupingText="Search Users" CssClass="Panel"
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
                    <asp:Label ID="LabelUser" CssClass="medium" runat="server" Text="Login ID / FirstName / Email ID:"
                        ForeColor="#DB512D"></asp:Label>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="TextBoxUser" CssClass="TextBox" runat="server" Width="80%"></asp:TextBox>
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
                    <asp:Label ID="LabelVendor" CssClass="medium" runat="server" Text="Team:" ForeColor="#DB512D"></asp:Label>
                </td>
                <td style="width: 50%">
                    <asp:Label ID="LabelVendorValue" CssClass="medium" runat="server" Text="" ForeColor="#DB512D"></asp:Label>
                    <asp:DropDownList ID="DropDownListVendor" CssClass="TextBox" runat="server" Width="80%"
                        OnSelectedIndexChanged="DropDownListVendor_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
                <td align="left" style="height: 24px" colspan="2">
                    <asp:Button ID="ButtonSearch" runat="server" Width="100px" Text="Search Users" OnClick="ButtonSearch_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelShowUserDetails" runat="server" Width="100%" Visible="false">
        <table style="width: 100%; height: 100%;">
            <tr style="height: 20px">
                <td colspan="2">
                </td>
            </tr>
            <tr style="height: 20px">
                <td style="width: 40%">
                    <asp:Label ID="LabelUserName" runat="server" Text="User Name:" CssClass="HyperLinkMenu"
                        ForeColor="#DB512D" Font-Size="Small" />
                </td>
                <td>
                    <asp:Label ID="LabelUserNameValue" runat="server" Text="" CssClass="HyperLinkMenu"
                        ForeColor="#DB512D" Font-Size="Small" />
                    <asp:Label ID="LabelUserIDValue" runat="server" Text="" CssClass="HyperLinkMenu"
                        ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 24px" colspan="2">
                    <asp:Button ID="ButtonBackToUsers" runat="server" Text="Back to Search Results" OnClick="ButtonBackToUsers_Click" />
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
    <asp:Panel ID="PanelUserDetails" runat="server" Width="100%" Visible="true">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="center">
                    <asp:GridView ID="GridViewUsersDetails" runat="server" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                        PageSize="20" Width="100%" AllowPaging="True" OnRowDataBound="GridViewUsersDetails_RowDataBound"
                        OnRowUpdating="GridViewUsersDetails_RowUpdating" OnRowCancelingEdit="GridViewUsersDetails_RowCancelingEdit"
                        OnPageIndexChanging="GridViewUsersDetails_PageIndexChanging">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                            <asp:TemplateField HeaderText="UserID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LabelUserID" runat="server" Text='<%# Bind("UserID") %>' Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Login ID">
                                <ItemTemplate>
                                    <asp:Label ID="LabelLoginID" runat="server" Text='<%# Bind("LoginID") %>' Visible="true"></asp:Label>
                                    <asp:TextBox ID="TextBoxLoginID" CssClass="TextBox" runat="server" Width="80%" Visible="false"
                                        Text='<%# Bind("LoginID") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="First Name">
                                <ItemTemplate>
                                    <asp:Label ID="LabelFirstName" runat="server" Text='<%# Bind("FirstName") %>' Visible="true"></asp:Label>
                                    <asp:TextBox ID="TextBoxFirstName" CssClass="TextBox" runat="server" Width="80%"
                                        Visible="false" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Name">
                                <ItemTemplate>
                                    <asp:Label ID="LabelLastName" runat="server" Text='<%# Bind("LastName") %>' Visible="true"></asp:Label>
                                    <asp:TextBox ID="TextBoxLastName" CssClass="TextBox" runat="server" Width="80%" Visible="false"
                                        Text='<%# Bind("LastName") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Team">
                                <ItemTemplate>
                                    <asp:Label ID="LabelVendor" runat="server" Text='<%# Bind("Vendor") %>' Visible="true"></asp:Label>
                                    <asp:DropDownList ID="DropDownListSelectVendor" runat="server" Visible="false" />
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonSave" runat="server" CausesValidation="False" CommandName="Add"
                                        Visible="false" Text="SAVE" OnCommand="SaveViewUsersDetailsLinkButton_Click"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="False" CommandName="Update"
                                        Text="UPDATE" OnCommand="UpdateViewUsersDetailsLinkButton_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        Visible="true" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                        Visible="false" Text="CANCEL" OnCommand="CancelViewUsersDetailsLinkButton_Click"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                        Visible="true" Text="DELETE" OnCommand="DeleteViewUsersDetailsLinkButton_Click"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonViewUserRoles" runat="server" CausesValidation="False"
                                        CommandName="ViewRoles" Visible="true" Text="VIEW ROLES" OnCommand="LinkButtonViewUserRoles_Click"
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
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelUserRolesDetails" runat="server" Width="100%" Visible="false">
        <ajaxToolkit:TabContainer ID="TabContainerPopulateUserRolesData" runat="server">
            <ajaxToolkit:TabPanel ID="TabPanelUserRoles" runat="server" HeaderText="User Project Roles"
                TabIndex="1" BackColor="#212121">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td>
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td style="width: 2%">
                            </td>
                            <td align="left">
                                <asp:Button ID="ButtonSaveUserProjectRoles" runat="server" Text="Save Project Roles"
                                    OnClick="ButtonSaveUserProjectRoles_Click" />
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 96%">
                                <asp:GridView ID="GridViewUserProjectRoles" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="100%" OnRowDataBound="GridViewUserProjectRoles_RowDataBound">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="UserProjectRoleID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelUserProjectRoleID" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ProjectRoleID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProjectRoleID" runat="server" Text='<%# Bind("ProjectRoleID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Role Code">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProjectRoleCode" runat="server" Text='<%# Bind("ProjectRoleCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Role">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProjectRole" runat="server" Text='<%# Bind("ProjectRole") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Role">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelRole" runat="server" Text='<%# Bind("Role") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxRolesSelected" runat="server" Width="80%"></asp:CheckBox>
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
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanelProductsAssigned" runat="server" HeaderText="Products Assigned"
                TabIndex="1" BackColor="#212121">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 96%">
                                <asp:GridView ID="GridViewAssignedProducts" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="100%" OnRowDataBound="GridViewAssignedProducts_RowDataBound" OnRowUpdating="GridViewAssignedProduct_RowUpdating"
                                    OnRowCancelingEdit="GridViewAssignedProduct_RowCancelingEdit">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                                        <asp:TemplateField HeaderText="UserProductID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelUserProductID" runat="server" Text='<%# Bind("UserProductID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Role">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelRole" runat="server" Text="Product"></asp:Label>
                                                <asp:Label ID="LabelRoleID" runat="server" Text="1" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UserProjectRoleID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelUserProjectRoleID" runat="server" Text='<%# Bind("UserProjectRoleID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Role">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProjectRole" runat="server" Text='<%# Bind("ProjectRole") %>'
                                                    Visible="true"></asp:Label>
                                                <asp:DropDownList ID="DropDownListProjectRoles" runat="server" Visible="false" Width="90%" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProduct" runat="server" Text='<%# Bind("Product") %>'></asp:Label>
                                                <asp:DropDownList ID="DropDownListProduct" runat="server" Visible="false" OnSelectedIndexChanged="DropDownListProduct_OnSelectedIndexChanged"
                                                    Width="90%" AutoPostBack="true" />
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Release">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProductVersion" runat="server" Text='<%# Bind("ProductVersion") %>'></asp:Label>
                                                <asp:DropDownList ID="DropDownListProductVersion" runat="server" Visible="false"
                                                    Width="90%" />
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
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
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
