<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Logon.aspx.cs"
    Inherits="Adobe.Localization.Insights.Web.Logon" Title="Adobe Localization Insights"
    StylesheetTheme="InsightStyles" %>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="Adobe.Localization.Insights.Web.Logon" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adobe Localization Insights</title>
</head>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <ajax:ScriptManager ID="ScriptManager1" runat="server">
    </ajax:ScriptManager>
    <%--    <body style="background-color: #333333">
    --%>
    <form id="formLogin" runat="server">
    <table>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
                <table>
                    <tr>
                        <td style="width: 50px">
                        </td>
                        <td>
                            <asp:Label ID="LabelHeader" runat="server" Text="Adobe Localization Insights" ForeColor="AntiqueWhite"
                                Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%; height: 100%">
        <tr>
            <td style="height: 10px" colspan="3">
            </td>
        </tr>
        <tr style="width: 100%; height: 500px">
            <td width="30%">
            </td>
            <td style="width: 600px; height: 100%">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr style="height: 20px; width: 100%;">
                        <td style="height: 30px; width: 100%; text-align: left; background-color: #3399FF;
                            font-size: medium; font-weight: normal; color: #FFFFFF; vertical-align: middle"
                            colspan="2">
                            &nbsp;&nbsp;Welcome to Localization Team Insights&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr style="background-color: #808080;">
                        <td style="width: 20px">
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="height: 30px; width: 100%; text-align: left; font-size: middle; font-weight: normal;
                                        color: #000000; vertical-align: bottom">
                                        <u>Sign In</u>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--                                        <asp:Login ID="UserLogin" runat="server" Width="340px">
                                            <LayoutTemplate>
                                                <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0">
                                                                <tr>
                                                                    <td style="height: 20px; width: 100%; text-align: left; font-size: small; font-weight: normal;
                                                                        color: #000000;" valign="bottom" colspan="2">
                                                                        Sign in using your Adobe Username and Password
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 20px;" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 100px">
                                                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name: &nbsp;&nbsp;</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="UserName" runat="server" Width="200px"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="UserLogin">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password: &nbsp;&nbsp;</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="UserLogin">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 10px;" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" style="color: Red;">
                                                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="UserLogin"
                                                                            OnClick="LoginButton_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                        </asp:Login>
                                        --%>
                                        <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0">
                                                        <tr>
                                                            <td style="height: 20px; width: 100%; text-align: left; font-size: small; font-weight: normal;
                                                                color: #000000;" valign="bottom" colspan="2">
                                                                Sign in using your Adobe Username and Password
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 20px;" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 100px">
                                                                <asp:Label ID="UserNameLabel" runat="server">User Name: &nbsp;&nbsp;</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="UserID" runat="server" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserID"
                                                                    ErrorMessage="User Name is required." ToolTip="User Name is required.">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px;" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="PasswordLabel" runat="server">Password: &nbsp;&nbsp;</asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="UserPassword" TextMode="Password" Width="200px" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="UserPassword"
                                                                    ErrorMessage="Password is required." ToolTip="Password is required.">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px;" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2" style="color: Red;">
                                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Button ID="LoginButton" runat="server" Text="Log In" OnClick="LoginButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="30%">
            </td>
        </tr>
        <tr>
            <td style="height: 20%" colspan="3">
            </td>
        </tr>
    </table>
    </form>
    <%-- </body>--%>
    <%--</html>--%>
</asp:Content>
