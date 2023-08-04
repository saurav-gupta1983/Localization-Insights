<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefineVendorAssociationsUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.DefineVendorAssociationsUserControl" %>
<asp:Panel ID="WrapperPanel" runat="server" Width="90%" Style="left: 30px; position: relative;
    top: 20px">
    <asp:Panel ID="Panel" runat="server" Width="100%" Style="position: relative;">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <h1>
                        <asp:Label ID="HeaderLabel" runat="server" Text="Define Vendor Associations" ForeColor="AntiqueWhite"
                            Font-Underline="true" Font-Size="Medium"></asp:Label>
                    </h1>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="SearchFilterPanel" GroupingText="Users Search Criteria"
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
                        <asp:Label ID="LabelUser" CssClass="medium" runat="server" Text=" User ID / User Name / Email ID: "
                            ForeColor="AntiqueWhite"></asp:Label>
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
                        <asp:Label ID="LabelVendor" CssClass="medium" runat="server" Text="Vendor: " ForeColor="AntiqueWhite"></asp:Label>
                    </td>
                    <td style="width: 50%">
                        <asp:DropDownList ID="DropDownListVendor" CssClass="TextBox" runat="server" Width="80%">
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
                        <asp:Button ID="SearchButton" runat="server" Width="75px" Text="Search" CssClass="Button"
                            OnClick="SearchButton_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
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
                    <asp:GridView ID="GridViewUserDetails" runat="server" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                        PageSize="50" Width="100%" AllowPaging="True" OnRowDataBound="GridViewUserDetails_RowDataBound"
                        OnPageIndexChanging="GridViewUserDetails_PageIndexChanging" OnRowUpdating="GridViewUserDetails_RowUpdating"
                        OnRowCancelingEdit="GridViewUserDetails_RowCancelingEdit">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                            <asp:TemplateField HeaderText="ID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="IDLabel" runat="server" Text='<%# Bind("UserVendorID") %>' Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User ID">
                                <ItemTemplate>
                                    <asp:Label ID="CodeLabel" runat="server" Text='<%# Bind("UserID") %>' Visible="true"></asp:Label>
                                    <asp:TextBox ID="CodeTextBox" CssClass="TextBox" runat="server" Width="80%" Visible="false"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("UserName") %>' Visible="true"></asp:Label>
                                    <asp:TextBox ID="DescriptionTextBox" CssClass="TextBox" runat="server" Width="80%"
                                        Visible="false"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor">
                                <ItemTemplate>
                                    <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("VendorID") %>' Visible="true"></asp:Label>
                                    <asp:DropDownList ID="TypeDropDownList" CssClass="DropDownList" runat="server" Width="80%"
                                        Visible="false">
                                    </asp:DropDownList>
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
            <tr style="height: 20px">
                <td>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
