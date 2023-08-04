<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateLocalesvsPlatformMatrixForm.aspx.cs"
    Inherits="Adobe.Localization.Insights.Web.Forms.UpdateLocalesvsPlatformMatrixForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControls/UpdateLocalesVsPlatformsDataUserControl.ascx" TagName="UpdateMatrix"
    TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; height: 100%; background-color: #212121">
            <tr style="height: 20px">
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 2%">
                </td>
                <td>
                    <asp:Panel ID="LoadLocalesVsPlatformsMatrixControl" runat="server" Width="100%" Height="100%">
                        <uc1:UpdateMatrix ID="UpdateLocalesvsPlatformMatrix" runat="server" />
                    </asp:Panel>
                </td>
                <td style="width: 2%">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
