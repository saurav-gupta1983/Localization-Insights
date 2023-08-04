<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefineProductWSRParametersUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.DefineProductWSRParametersUserControl" %>
<asp:Panel ID="Panel" runat="server" Width="100%" Style="position: relative;">
    <table style="width: 80%; height: 100%; color: White">
        <tr>
            <td align="left" colspan="2">
                <asp:Label ID="LabelHeader" runat="server" Text="Define Product WSR Parameters" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 20px" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="ButtonUpdateProductWSRParameters" runat="server" Text="Update Product WSR Parameters"
                    OnClick="ButtonUpdateProductWSRParameters_Click" />
            </td>
        </tr>
        <tr>
            <td style="height: 20px" colspan="2">
            </td>
        </tr>
    </table>
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
                <table style="width: 100%; background-color: #212121">
                    <tr>
                        <td style="width: 96%">
                            <asp:GridView ID="GridViewProductWSRParameters" runat="server" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                Width="90%" OnRowDataBound="GridViewProductWSRParameters_RowDataBound">
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ProductWSRParameterID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelProductWSRParameterID" runat="server" Text='' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WSR Section" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelWSRSection" runat="server" Text='<%# Bind("WSRSection") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle ForeColor="Navy" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WSRParameterID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelWSRParameterID" runat="server" Text='<%# Bind("WSRParameterID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle ForeColor="Navy" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WSR Parameter">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelWSRParameter" runat="server" Text='<%# Bind("WSRParameter") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle ForeColor="Navy" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBoxSelectedWSRParameters" runat="server" Width="80%"></asp:CheckBox>
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
                </table>
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
