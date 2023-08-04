<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintainProductVersionsUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.MaintainProductVersionsUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="MasterDataUserControl.ascx" TagName="MasterData" TagPrefix="uc1" %>
<script type="text/javascript">
    //Function restricting user to Enter only Numeric Data
    function NumericOnly() {
        //alert(event.keyCode);
        if (event.keyCode < 37 || event.keyCode > 40) {
            if (event.keyCode != 116 && event.keyCode != 46 && event.keyCode != 8 && event.keyCode != 9 && event.keyCode != 16) {
                if (event.keyCode < 48 || event.keyCode > 57) {
                    alert("Please enter Numeric value.");
                    event.returnValue = false;
                }
            }
        }
    }  
</script>
<asp:Panel ID="Panel" runat="server" Width="100%">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="LabelHeader" runat="server" Text="Maintain Product Versions" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelUpdateProductVersionDetails" runat="server" Width="100%" Visible="false">
        <table style="width: 80%; height: 100%;">
            <tr style="height: 10px">
                <td colspan="2">
                </td>
            </tr>
            <tr style="height: 20px">
                <td style="width: 30%">
                    <asp:Label ID="LabelProductPopulate" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                        ForeColor="#DB512D" Font-Size="Small" />
                </td>
                <td>
                    <asp:Label ID="LabelProductNamePopulate" runat="server" Text="Product" CssClass="HyperLinkMenu"
                        ForeColor="#DB512D" Font-Size="Small" />
                </td>
            </tr>
            <%-- <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelProductVersionPopulate" runat="server" Text="Product Release:"
                        CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                </td>
                <td>
                    <asp:Label ID="LabelProductVersionValuePopulate" runat="server" Text="Illustrator Version"
                        CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                </td>
            </tr>
            <tr style="height: 10px">
                <td colspan="2">
                </td>
            </tr>--%>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelButtons" runat="server" Width="100%">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="text-align: left">
            <tr>
                <td align="left">
                    <asp:Button ID="ButtonCreateProductVersion" runat="server" Text="Create Product Version"
                        OnClick="ButtonCreateProductVersion_Click" Visible="true" />
                    &nbsp; &nbsp;
                    <asp:Button ID="ButtonAddNewVersion" runat="server" Text="Save Product Version" OnClick="ButtonAddNewVersion_Click"
                        Visible="false" />
                    &nbsp; &nbsp;
                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click"
                        Visible="false" />
                </td>
            </tr>
            <tr style="height: 5px">
                <td>
                </td>
            </tr>
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
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelUpdateProductVersions" runat="server" Width="100%">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr style="height: 10px">
                <td>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="GridViewMasterDataDetails" runat="server" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                        Width="100%" OnRowDataBound="GridViewMasterDataDetails_RowDataBound" OnRowUpdating="GridViewMasterDataDetails_RowUpdating"
                        OnRowCancelingEdit="GridViewMasterDataDetails_RowCancelingEdit">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                            <asp:TemplateField HeaderText="ID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProductVersionID" runat="server" Text='<%# Bind("ProductVersionID") %>'
                                        Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Version">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProductVersion" runat="server" Text='<%# Bind("ProductVersion") %>'
                                        Visible="true"></asp:Label>
                                    <asp:TextBox ID="TextBoxProductVersion" CssClass="TextBox" runat="server" Width="80%"
                                        Visible="false"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Code">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProductCodeName" runat="server" Text='<%# Bind("ProductCodeName") %>'
                                        Visible="true"></asp:Label>
                                    <asp:TextBox ID="TextBoxProductCodeName" CssClass="TextBox" runat="server" Width="80%"
                                        Visible="false"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Release Type">
                                <ItemTemplate>
                                    <asp:Label ID="LabelReleaseTypeID" runat="server" Text='<%# Bind("ReleaseTypeID") %>'
                                        Visible="true"></asp:Label>
                                    <asp:DropDownList ID="DropDownListReleaseType" runat="server" Visible="false" />
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Year">
                                <ItemTemplate>
                                    <asp:Label ID="LabelProductYear" runat="server" Text='<%# Bind("ProductYear") %>'
                                        Visible="true"></asp:Label>
                                    <asp:TextBox ID="TextBoxProductYear" CssClass="TextBox" runat="server" Width="80%"
                                        onkeydown="NumericOnly()" Visible="false"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="About Product" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LabelAboutProduct" runat="server" Text='<%# Bind("AboutProduct") %>'
                                        Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:Label ID="LabelActive" runat="server" Text='<%# Bind("IsActive") %>' Visible="true"></asp:Label>
                                    <asp:CheckBox ID="CheckBoxActive" runat="server" Width="80%" Visible="false"></asp:CheckBox>
                                </ItemTemplate>
                                <ControlStyle ForeColor="Navy" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Wind-up">
                                <ItemTemplate>
                                    <asp:Label ID="LabelClosed" runat="server" Text='<%# Bind("IsClosed") %>' Visible="true"></asp:Label>
                                    <asp:CheckBox ID="CheckBoxClosed" runat="server" Width="80%" Visible="false"></asp:CheckBox>
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
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonViewVersionDetails" runat="server" CausesValidation="False"
                                        CommandName="ViewVersionDetails" Visible="true" Text="VIEW DETAILS" OnCommand="LinkButtonViewVersionDetails_Click"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
    <asp:Panel ID="PanelCreateProductVersions" runat="server" Width="100%" Visible="false">
        <table style="width: 80%; height: 100%;">
            <tr style="height: 20px">
                <td colspan="2">
                </td>
            </tr>
            <tr style="height: 20px">
                <td style="width: 40%">
                    <asp:Label ID="LabelProductCreate" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                </td>
                <td>
                    <asp:Label ID="LabelProductNameCreate" runat="server" Text="Product" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelProductVersionCreate" runat="server" Text="Product Version:"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                </td>
                <td>
                    <asp:TextBox ID="TextBoxProductVersionCreate" CssClass="TextBox" runat="server" Width="80%"
                        Visible="true"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelProductCodeNameCreate" runat="server" Text="Product Code:" ForeColor="AntiqueWhite"
                        Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxProductCodeNameCreate" CssClass="TextBox" runat="server"
                        Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelReleaseTypeCreate" runat="server" Text="Release Type:" ForeColor="AntiqueWhite"
                        Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListReleaseTypeCreate" runat="server" Width="80%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelProductYearCreate" runat="server" Text="Year:" ForeColor="AntiqueWhite"
                        Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxProductYearCreate" CssClass="TextBox" runat="server" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelActiveCreate" runat="server" Text="Active:" ForeColor="AntiqueWhite"
                        Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="CheckBoxActiveCreate" runat="server" Width="80%" Visible="true"
                        Checked="true"></asp:CheckBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelPopulateDataFromProducts" runat="server" Width="100%" Visible="false">
        <table style="width: 80%; height: 100%;">
            <tr style="height: 20px">
                <td style="height: 20px" colspan="2">
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="2">
                    <asp:Label ID="LabelPopulateDataFrom" runat="server" Text="Populate Data From:" CssClass="HyperLinkMenu"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="2">
                </td>
            </tr>
            <tr style="height: 20px">
                <td style="width: 40%">
                    <asp:Label ID="LabelPopulateProducts" runat="server" Text="Select Product:" ForeColor="AntiqueWhite"
                        Font-Size="Small" />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListPopulateProducts" runat="server" Width="80%" AutoPostBack="true"
                        OnSelectedIndexChanged="DropDownListPopulateProducts_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <asp:Label ID="LabelPopulateversions" runat="server" Text="Select Product Version:"
                        ForeColor="AntiqueWhite" Font-Size="Small" />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListPopulateVersions" runat="server" Width="80%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 20px" colspan="2">
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="2">
                    <asp:Label ID="LabelCopyData" runat="server" Text="You can copy following Data from Other Products:"
                        CssClass="HyperLinkMenu" ForeColor="AntiqueWhite" Font-Size="Small" />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="CheckBoxCopyLocales" runat="server" Text="Copy Locales" ForeColor="AntiqueWhite"
                        Font-Size="Small" Width="80%" Checked="true"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="CheckBoxCopyPlatforms" runat="server" Text="Copy Platforms" ForeColor="AntiqueWhite"
                        Font-Size="Small" Width="80%" Checked="true"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="CheckBoxCopyUsers" runat="server" Text="Copy Users and Roles" ForeColor="AntiqueWhite"
                        Font-Size="Small" Width="80%" Checked="true"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="CheckBoxCopyPhases" runat="server" Text="Copy Project Phases" ForeColor="AntiqueWhite"
                        Font-Size="Small" Width="80%" Checked="true"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="CheckBoxCopySprints" runat="server" Text="Copy Sprints" ForeColor="AntiqueWhite"
                        Font-Size="Small" Width="80%" Checked="true"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="CheckBoxCopyProjectBuilds" runat="server" Text="Copy Project Builds"
                        ForeColor="AntiqueWhite" Font-Size="Small" Width="80%" Checked="true"></asp:CheckBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td>
            <asp:Panel ID="PanelFooterPadding" runat="server" Width="100%" Height="100%" Visible="false">
            </asp:Panel>
        </td>
    </tr>
</table>
