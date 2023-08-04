<%@ Page Language="C#" MasterPageFile="~/BlankSite.Master" AutoEventWireup="true"
    CodeBehind="Logon.aspx.cs" Inherits="Adobe.Localization.Insights.Web.Logon" Title="Oogway - Adobe Localization Insights"
    StylesheetTheme="InsightStyles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table style="width: 100%; height: 100%">
        <tr align="center">
            <td style="height: 20px">
            </td>
        </tr>
        <tr align="center">
            <td style="width: 40%;">
            </td>
            <td align="center" style="width: 460px; height: 391px;">
                <table border="0" cellspacing="0" cellpadding="0" width="460px" style="background-image: url('../Images/HomePage_New.png');
                    height: 391px">
                    <tr>
                        <td>
                            <table style="width: 100%; height: 100%">
                                <tr>
                                    <td colspan="3" style="height: 238px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 192px; vertical-align: middle" align="right" valign="middle">
                                        <asp:Label ID="LabelLoginID" Text="LoginID" runat="server" ForeColor="#F3DDCB" Font-Names="Arial Rounded MT Bold"
                                            Font-Size="Large"></asp:Label>
                                    </td>
                                    <td align="left" style="vertical-align: middle;">
                                        <asp:TextBox ID="TextBoxLoginID" runat="server" Width="206px" Height="25px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredUserName" runat="server" ControlToValidate="TextBoxLoginID"
                                            ErrorMessage="Login ID Mandatory" ToolTip="Login ID Mandatory">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 192px; vertical-align: middle" align="right" valign="middle">
                                        <asp:Label ID="LabelPassword" Text="Password" runat="server" ForeColor="#F3DDCB"
                                            Font-Names="Arial Rounded MT Bold" Font-Size="Large"></asp:Label>
                                    </td>
                                    <td align="left" style="vertical-align: middle">
                                        <asp:TextBox ID="TextBoxPassword" TextMode="Password" Width="206px" Height="25px"
                                            runat="server" Style="margin-top: 3px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredPassword" runat="server" ControlToValidate="TextBoxPassword"
                                            ErrorMessage="Password Mandatory" ToolTip="Password Mandatory">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table style="width: 100%; height: 100%">
                                            <tr>
                                                <td style="width: 315px;">
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="ButtonLogin" runat="server" Text="Log In" OnClick="ButtonLogin_Click"
                                                        Height="25px" Width="88px" Style="margin-top: 6px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Label ID="LabelFailureText" runat="server" Font-Bold="true" ForeColor="Black"
                                            Font-Size="Small"></asp:Label>
                                        <%--                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False" Text="DisplayText"></asp:Literal>
                                        --%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 40%;">
            </td>
        </tr>
        <%--        <tr style="width: 100%; height: 300px">
            <td width="20%">
            </td>
            <td style="width: 600px; height: 100%">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr style="height: 20px; width: 100%;">
                        <td style="height: 30px; width: 100%; text-align: left; background-color: #3399FF;
                            font-size: medium; font-weight: normal; color: #FFFFFF; vertical-align: middle"
                            colspan="2">
                            &nbsp;&nbsp;<asp:Label ID="LabelWelcome" runat="server" Text="Welcome" ForeColor="#DB512D"
                                Font-Size="Medium" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
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
                                        <u>
                                            <asp:Label ID="LabelSignIn" runat="server" Text="SignIn"></asp:Label></u>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Login ID="UserLogin" runat="server" Width="340px">
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
                                                                        <asp:Label ID="LabelUserName" runat="server" AssociatedControlID="UserName">User Name: &nbsp;&nbsp;</asp:Label>
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
                                                                        <asp:Button ID="ButtonLogin" runat="server" CommandName="Login" Text="Log In" ValidationGroup="UserLogin"
                                                                            OnClick="ButtonLogin_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                        </asp:Login>
                                        <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0">
                                                        <tr>
                                                            <td style="height: 20px; width: 100%; text-align: left; font-size: small; font-weight: normal;
                                                                color: #000000;" valign="bottom" colspan="2">
                                                                <asp:Label ID="LabelCredentialMessage" runat="server" Text="CredentialMessage">
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 20px;" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 100px; height: 20px">
                                                                <asp:Label ID="LabelLoginID" Text="LoginID" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBoxLoginID" runat="server" Width="200px" Height="20px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredUserName" runat="server" ControlToValidate="TextBoxLoginID"
                                                                    ErrorMessage="Login ID Mandatory" ToolTip="Login ID Mandatory">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 5px;" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 100px; height: 20px">
                                                                <asp:Label ID="LabelPassword" runat="server" Text="Password" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBoxPassword" TextMode="Password" Width="200px" runat="server"
                                                                    Height="20px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredPassword" runat="server" ControlToValidate="TextBoxPassword"
                                                                    ErrorMessage="Password Mandatory" ToolTip="Password Mandatory">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBoxRememberMe" runat="server" Text="Remember me next time."
                                                                    Visible="false" />
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
                                                                <asp:Button ID="ButtonLogin" runat="server" Text="Log In" OnClick="ButtonLogin_Click" />
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
            <td width="20%">
            </td>
        </tr>
        --%>
        <tr>
            <td style="height: 20%" colspan="3">
            </td>
        </tr>
    </table>
</asp:Content>
