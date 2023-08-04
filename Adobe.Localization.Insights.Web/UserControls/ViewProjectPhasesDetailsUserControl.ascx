<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewProjectPhasesDetailsUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.ViewProjectPhasesDetailsUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Calendar.ascx" TagName="DateCalendar" TagPrefix="uc1" %>
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
                <asp:Label ID="LabelHeader" runat="server" Text="Maintain Project Phases" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="text-align: left">
        <tr style="height: 20px">
            <td style="width: 40%">
                <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" />
            </td>
            <td>
                <asp:Label ID="LabelProductValue" runat="server" Text="" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" />
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
                <asp:Label ID="LabelProductVersion" runat="server" Text="Product Release:" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" />
            </td>
            <td>
                <asp:DropDownList ID="DropDownListProductVersion" runat="server" Width="50%" AutoPostBack="true"
                    Visible="false" OnSelectedIndexChanged="DropDownListProductVersion_OnSelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label ID="LabelProductVersionValue" runat="server" Text="" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Size="Small" Visible="true" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="PanelUpdateProjectPhaseDetails" runat="server" Width="100%" Visible="false">
                    <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                        <tr style="height: 20px">
                            <td style="width: 40%">
                                <asp:Label ID="LabelProjectPhasePopulate" runat="server" Text="Project Phase:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td>
                                <asp:Label ID="LabelProjectPhasePopulateValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                                <asp:Label ID="LabelProjectPhaseID" runat="server" Text="" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <asp:Button ID="ButtonViewProjectPhases" runat="server" Text="View all Project Phases"
                    OnClick="ButtonViewProjectPhases_Click" Visible="false" />
                &nbsp; &nbsp; &nbsp;
                </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="LabelMessage" CssClass="medium" runat="server" Text="" Width="100%"
                    ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr style="height: 5px">
            <td>
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelPopulateVersionDetailsData" runat="server" Width="100%" Visible="false">
        <ajaxToolkit:TabContainer ID="TabContainerPopulateVersionDetailsData" runat="server">
            <ajaxToolkit:TabPanel ID="TabPanelLocalesVsPlatforms" runat="server" HeaderText="Locales Vs Platform Matrix"
                Height="100%" TabIndex="0">
                <ContentTemplate>
                    <table style="width: 100%; height: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td>
                                <asp:Panel ID="PanelCoverages" runat="server" Width="100%">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 30%">
                                                <asp:Label ID="LabelCoverages" runat="server" Text="Coverages:" CssClass="HyperLinkMenu"
                                                    ForeColor="#DB512D" Font-Size="Small" />
                                            </td>
                                            <td style="width: 70%">
                                                <asp:DropDownList ID="DropDownListCoverages" runat="server" Width="100%" AutoPostBack="true"
                                                    OnSelectedIndexChanged="DropDownListCoverages_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="height: 20px">
                                            <td colspan="2">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td>
                                <asp:Label ID="LabelNoLocalesVsPlatformMatrixDefined" runat="server" Text="No Locales and Platforms Matrix Defined"
                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td>
                                <asp:Panel ID="LoadLocalesVsPlatformsMatrixControl" runat="server" Width="100%" Height="100%">
                                </asp:Panel>
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanelLocalesAndPlatforms" runat="server" HeaderText="Locales and Platforms"
                TabIndex="1" BackColor="#212121">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="5">
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td style="width: 2%">
                            </td>
                            <td align="left" colspan="4">
                                <asp:Button ID="ButtonSaveLocalesAndPlatforms" runat="server" Text="Save Locales and Platforms"
                                    OnClick="ButtonSavePhaseDetails_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td colspan="4">
                                <asp:Label ID="LabelNoLocalesAndPlatforms" runat="server" Text="No Locales and Platforms Defined"
                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 60%">
                                <asp:GridView ID="GridViewPhaseLocales" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="90%" OnRowDataBound="GridViewPhaseLocales_RowDataBound">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ProjectLocaleID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProjectLocaleID" runat="server" Text='<%# Bind("ProjectLocaleID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ProductLocaleID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProductLocaleID" runat="server" Text='<%# Bind("ProductLocaleID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Locale Tier" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelLocaleTier" runat="server" Text='<%# Bind("Tier") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Locales">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelLocales" runat="server" Text='<%# Bind("Locale") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Locale Weight" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelLocaleWeight" runat="server" Text='<%# Bind("LocaleWeight") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:TextBox ID="TextBoxLocaleWeight" runat="server" Text='<%# Bind("LocaleWeight") %>'
                                                    Width="80%"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Team" ControlStyle-Width="100%">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelVendor" runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="DropDownListSelectVendor" CssClass="DropDownList" runat="server"
                                                    Width="80%" Visible="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxLocalesSelected" runat="server" Width="80%"></asp:CheckBox>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#00C0C0" />
                                    <AlternatingRowStyle BackColor="#FFE0C0" />
                                    <SelectedRowStyle BackColor="CornflowerBlue" />
                                </asp:GridView>
                            </td>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 35%">
                                <asp:GridView ID="GridViewPhasePlatforms" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="90%" OnRowDataBound="GridViewPhasePlatforms_RowDataBound">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ProjectPlatformID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProjectPlatformID" runat="server" Text='<%# Bind("ProjectPlatformID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ProductPlatformID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProductPlatformID" runat="server" Text='<%# Bind("ProductPlatformID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Platform Type" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPlatformType" runat="server" Text='<%# Bind("PlatformType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Platform">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPlatform" runat="server" Text='<%# Bind("Platform") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxPlatformsSelected" runat="server" Width="80%"></asp:CheckBox>
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
                        <tr style="height: 20px">
                            <td colspan="5">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanelAboutProjectPhase" runat="server" HeaderText="About Project Phase"
                TabIndex="2" BackColor="#212121">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td style="width: 2%">
                            </td>
                            <td align="right">
                                <asp:Button ID="ButtonEditAboutProjectPhase" runat="server" Text="Edit About Project Phase"
                                    OnClick="ButtonEditAboutProjectPhase_Click" />
                                <asp:Button ID="ButtonSaveAboutPhaseDetails" runat="server" Text="Save About Project Phase"
                                    OnClick="ButtonSavePhaseDetails_Click" Visible="false" />
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td>
                                <asp:Label ID="LabelAboutProjectPhase" runat="server" ForeColor="AntiqueWhite" Font-Size="Small"></asp:Label>
                                <asp:TextBox ID="TextBoxAboutProjectPhase" runat="server" Visible="False" Height="50px"
                                    TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td>
                                <asp:Panel ID="PanelNoCoverageDetails" runat="server" Width="100%" Height="100%"
                                    Visible="False">
                                    <table>
                                        <tr style="height: 20px">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr style="height: 20px">
                                            <td>
                                                <asp:Label ID="LabelNoCoverageDetails" runat="server" Text="No Coverage Details"
                                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td>
                                <asp:GridView ID="GridViewPhaseCoverage" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="100%" OnRowDataBound="GridViewPhaseCoverage_RowDataBound" OnRowUpdating="GridViewPhaseCoverage_RowUpdating"
                                    OnRowCancelingEdit="GridViewPhaseCoverage_RowCancelingEdit">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                                        <asp:TemplateField HeaderText="PhaseCoverageDetailID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPhaseCoverageDetailID" runat="server" Text='<%# Bind("PhaseCoverageDetailID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phase Coverage Details">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelCoverageDetails" runat="server" Text='<%# Bind("PhaseCoverageDetails") %>'
                                                    Visible="true"></asp:Label>
                                                <asp:TextBox ID="TextBoxCoverageDetails" CssClass="TextBox" runat="server" Width="95%"
                                                    Height="40px" TextMode="MultiLine" Visible="false" Text='<%# Bind("PhaseCoverageDetails") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Test Suite" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSuiteID" runat="server" Text='<%# Bind("SuiteID") %>' Visible="true"></asp:Label>
                                                <asp:TextBox ID="TextBoxSuiteID" CssClass="TextBox" runat="server" Width="90%" Visible="false"
                                                    Text='<%# Bind("SuiteID") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Test Cases Count">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelTestCasesCount" runat="server" Text='<%# Bind("TestCasesCount") %>'
                                                    Visible="true"></asp:Label>
                                                <asp:TextBox ID="TextBoxTestCasesCount" CssClass="TextBox" runat="server" Width="90%"
                                                    Visible="false" Text='<%# Bind("TestCasesCount") %>' onkeydown="NumericOnly()"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonSave" runat="server" CausesValidation="False" CommandName="Add"
                                                    Visible="false" Text="SAVE" OnCommand="LinkButtonSaveCoverageDetails_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="False" CommandName="Update"
                                                    Text="UPDATE" OnCommand="LinkButtonUpdateCoverageDetails_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    Visible="true" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Visible="false" Text="CANCEL" OnCommand="LinkButtonCancelCoverageDetails_Click"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Visible="true" Text="DELETE" OnCommand="LinkButtonDeleteCoverageDetails_Click"
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
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
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
