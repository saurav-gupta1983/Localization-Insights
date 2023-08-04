<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterDataUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.MasterDataUserControl" %>
<asp:Panel ID="Panel" runat="server" Width="100%" Style="position: relative;">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left">
                <h1>
                    <asp:Label ID="LabelHeader" runat="server" Text="Header" ForeColor="#DB512D" Font-Underline="true"
                        Font-Size="Medium"></asp:Label>
                </h1>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <asp:Label ID="LabelMessage" CssClass="medium" runat="server" Text="" Width="100%"
                    ForeColor="red"></asp:Label>
                <asp:Label ID="LabelTypeValue" CssClass="medium" runat="server" Text="" Width="100%"
                    Visible="false"></asp:Label>
            </td>
        </tr>
        <tr style="height: 5px">
            <td>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="GridViewMasterDataDetails" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                    Width="100%" OnRowDataBound="GridViewMasterDataDetails_RowDataBound" OnRowUpdating="GridViewMasterDataDetails_RowUpdating"
                    OnRowCancelingEdit="GridViewMasterDataDetails_RowCancelingEdit">
                    <RowStyle ForeColor="#000066" Height="20px" />
                    <Columns>
                        <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                        <asp:TemplateField HeaderText="ID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="LabelID" runat="server" Text='<%# Bind("ID") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grid Header Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelCode" runat="server" Text='<%# Bind("Code") %>' Visible="true"></asp:Label>
                                <asp:TextBox ID="TextBoxCode" CssClass="TextBox" runat="server" Width="80%" Visible="false"></asp:TextBox>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Navy" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grid Header Description">
                            <ItemTemplate>
                                <asp:Label ID="LabelDescription" runat="server" Text='<%# Bind("Description") %>'
                                    Visible="true"></asp:Label>
                                <asp:TextBox ID="TextBoxDescription" CssClass="TextBox" runat="server" Width="80%"
                                    TextMode="MultiLine" Visible="false"></asp:TextBox>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Navy" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grid Header Type" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelType" runat="server" Text='<%# Bind("Type") %>' Visible="true"></asp:Label>
                                <asp:DropDownList ID="DropDownListType" CssClass="DropDownList" runat="server" Width="80%"
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
        <tr>
            <td>
                <asp:Panel ID="PanelFooterPadding" runat="server" Width="100%" Height="100%" Visible="false">
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
