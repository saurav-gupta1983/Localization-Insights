<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefineProductOwnersUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.DefineProductOwnersUserControl" %>
<asp:Panel ID="WrapperPanel" runat="server" Width="90%" Style="left: 30px; position: relative;
    top: 20px">
    <asp:Panel ID="Panel" runat="server" Width="100%" Style="position: relative;">
        <table style="width: 80%; height: 100%; color: White">
            <tr>
                <td align="left" colspan="2">
                    <asp:Label ID="LabelHeading" runat="server" Text="Define Product Owners" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Underline="true" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 20px" colspan="2">
                </td>
            </tr>
        </table>
        <asp:Panel ID="PanelDisplayProduct" runat="server" Width="100%">
            <table style="width: 80%; height: 100%; color: White">
                <tr style="height: 20px">
                    <td style="width: 50%">
                        <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                            ForeColor="AntiqueWhite" Font-Size="Small" />
                    </td>
                    <td style="width: 50%">
                        <asp:DropDownList ID="DropDownListProduct" runat="server" Width="80%" AutoPostBack="true"
                            OnSelectedIndexChanged="DropDownListProduct_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px" colspan="2">
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td style="width: 50%">
                        <asp:Label ID="LabelProductVersion" runat="server" Text="Product Version:" CssClass="HyperLinkMenu"
                            ForeColor="AntiqueWhite" Font-Size="Small" />
                    </td>
                    <td style="width: 50%">
                        <asp:Label ID="LabelVersionID" runat="server" Text="" CssClass="HyperLinkMenu" Visible="false"
                            ForeColor="AntiqueWhite" Font-Size="Small" />
                        <asp:TextBox ID="TextBoxVersion" runat="server" Text="Default" CssClass="HyperLinkMenu"
                            Font-Size="Small" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px" colspan="2">
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
                    <asp:GridView ID="GridViewProductOwnerDetails" runat="server" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                        Width="100%" AllowPaging="True" OnRowDataBound="GridViewProductOwnerDetails_RowDataBound"
                        OnRowUpdating="GridViewProductOwnerDetails_RowUpdating" OnRowCancelingEdit="GridViewProductOwnerDetails_RowCancelingEdit">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                            <asp:TemplateField HeaderText="ID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="IDLabel" runat="server" Text='<%# Bind("UserProductID") %>' Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Role">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProjectRoleID" runat="server" Text='<%# Bind("ProjectRoleID") %>'
                                        Visible="true"></asp:Label>
                                    <asp:DropDownList ID="DropDownListProjectRoleID" CssClass="DropDownList" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="DropDownListProjectRoleID_OnSelectedIndexChanged"
                                        Width="80%" Visible="false">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label ID="LabelUserID" runat="server" Text='<%# Bind("UserID") %>'
                                        Visible="true"></asp:Label>
                                    <asp:DropDownList ID="DropDownListUserID" CssClass="DropDownList" runat="server"
                                        Width="80%" Visible="false">
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
