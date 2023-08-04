<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs"
    Inherits="Adobe.Localization.Insights.Web.Report" Title="Oogway - Adobe Localization Insights"
    ValidateRequest="false" StylesheetTheme="InsightStyles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <ajax:ScriptManager ID="ScriptManager1" runat="server">
    </ajax:ScriptManager>
    <div id="ControlId" runat="server">
    </div>
</asp:Content>
