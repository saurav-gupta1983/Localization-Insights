<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateOwnersandUsersUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.UpdateOwnersandUsersUserControl" %>
<%--<%@ Register Src="AddUpdateUsersUserControl.ascx" TagName="AddUpdateUsers" TagPrefix="ucUsers" %>
--%>
<%@ Register Src="DefineProductOwnersUserControl.ascx" TagName="DefineProductOwners"
    TagPrefix="ucOwner" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="WrapperPanel" runat="server" Width="90%" Style="left: 30px; position: relative;
    top: 20px">
    <asp:Panel ID="Panel" runat="server" Width="100%" Style="position: relative;">
        <table style="width: 80%; height: 100%; color: White">
            <tr>
                <td align="left" colspan="2">
                    <asp:Label ID="LabelHeading" runat="server" Text="Update Owners and Users" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Underline="true" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 20px" colspan="2">
                </td>
            </tr>
            <tr style="height: 20px">
                <td style="width: 50%">
                    <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                </td>
                <td style="width: 50%">
                    <asp:Label ID="LabelProductValue" runat="server" Text="Default" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
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
                    <asp:DropDownList ID="DropDownListProductVersion" runat="server" Width="80%" AutoPostBack="true"
                        OnSelectedIndexChanged="DropDownListProductVersion_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 20px" colspan="2">
                </td>
            </tr>
        </table>
        <asp:Panel ID="PanelPopulateUsers" runat="server" Width="100%">
            <ajaxToolkit:TabContainer ID="TabContainerPopulateUsers" runat="server">
                <ajaxToolkit:TabPanel ID="TabPanelOwnersAndUsers" runat="server" HeaderText="Owners and Users"
                    TabIndex="1" BackColor="#333333">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="center">
                                    <table style="width: 100%; background-color: #333333">
                                        <tr>
                                            <td>
                                                <ucOwner:DefineProductOwners ID="DefineProductOwners" runat="server">
                                                </ucOwner:DefineProductOwners>
                                            </td>
                                        </tr>
                                        <tr style="height: 20px">
                                            <td colspan="5">
                                            </td>
                                        </tr>
                                        <tr style="height: 20px">
                                            <td colspan="5">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanelAddNewUser" runat="server" HeaderText="Add New User"
                    TabIndex="1" BackColor="#333333">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="center">
                                    <table style="width: 100%; background-color: #333333">
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr style="height: 20px">
                                            <td colspan="5">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </asp:Panel>
    </asp:Panel>
</asp:Panel>
