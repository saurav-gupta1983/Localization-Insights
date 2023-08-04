<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefineUserAccessUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.DefineUserAccessUserControl" %>
<style type="text/css">
    .style2
    {
        width: 91px;
    }
</style>
<asp:Panel ID="WrapperPanel" runat="server" Width="80%" Style="left: 30px; position: relative;
    top: 5px; text-align: center;">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="LabelHeading" runat="server" Text="Weekly Status Report - Define User Access"
                    CssClass="HyperLinkMenu" ForeColor="AntiqueWhite" Font-Underline="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" 
        style="text-align: left; width: 59%;">
        <tr>
            <td colspan="2">
                <asp:Label ID="MessageLabel" CssClass="medium" runat="server" Text="" Width="100%"
                    ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr style="height: 5px">
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td style="width: 200px">
                <asp:Label ID="LabelUserID" runat="server" Text="UserID:" CssClass="HyperLinkMenu"
                    ForeColor="AntiqueWhite" Font-Size="Small" />
            </td>
            <td>
                <asp:TextBox ID="TextBoxUserID" runat="server" Width="80%" Height="20px"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr style="height: 10px">
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click">
                </asp:Button>
            </td>
        </tr>
        <tr style="height: 20px">
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelSections" runat="server" Text="Sections:" ForeColor="AntiqueWhite" />
            </td>
            <td>
                <asp:DropDownList ID="DropDownListSection" runat="server" Width="80%" AutoPostBack="true"
                    OnSelectedIndexChanged="DropDownListSection_OnSelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelSelectAccess" runat="server" Text="Select Access:" ForeColor="AntiqueWhite" />
            </td>
            <td align="left">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonGeneral" runat="server" Text="General" ForeColor="AntiqueWhite"
                                GroupName="AccessList" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonView" runat="server" Text="View" ForeColor="AntiqueWhite"
                                GroupName="AccessList" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonWrite" runat="server" Text="Write" ForeColor="AntiqueWhite"
                                GroupName="AccessList" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonReporting" runat="server" Text="Reporting" ForeColor="AntiqueWhite"
                                GroupName="AccessList" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonAdmin" runat="server" Text="Administrator" ForeColor="AntiqueWhite"
                                GroupName="AccessList" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonAdminAndReport" runat="server" Text="Administrator and Reporting"
                                GroupName="AccessList" ForeColor="AntiqueWhite" />
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 20px">
            <td colspan="2" align="center">
                <asp:Button ID="ButtonSave" runat="server" Text="Save Details" OnClick="ButtonSave_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
