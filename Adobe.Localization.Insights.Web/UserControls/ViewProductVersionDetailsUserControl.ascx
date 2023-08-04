<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewProductVersionDetailsUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.ViewProductVersionDetailsUserControl" %>
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
    <asp:Panel ID="PanelUpdateProductVersionDetails" runat="server" Width="100%">
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
            <tr style="height: 20px">
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
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelButtons" runat="server" Width="100%">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="text-align: left">
            <tr>
                <td align="left">
                    <asp:Button ID="ButtonViewProductVersion" runat="server" Text="View All Product Versions"
                        OnClick="ButtonViewProductVersion_Click" Visible="true" />
                    &nbsp; &nbsp;
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
    <asp:Panel ID="PanelPopulateVersionDetailsData" runat="server" Width="100%">
        <ajaxToolkit:TabContainer ID="TabContainerPopulateVersionDetailsData" runat="server">
            <ajaxToolkit:TabPanel ID="TabPanelProjectPhases" runat="server" HeaderText="Project Phases"
                TabIndex="0">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelNoProjectPhasesDefined" runat="server" Text="No Phases Defined"
                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 96%">
                                <asp:GridView ID="GridViewProjectPhases" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="100%" OnRowDataBound="GridViewProjectPhases_RowDataBound">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProjectPhaseID" runat="server" Text='<%# Bind("ProjectPhaseID") %>'
                                                    Visible="true"></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Phase">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonProjectPhase" runat="server" CausesValidation="False"
                                                    CommandName="ViewPhases" Visible="true" Text='<%# Bind("ProjectPhase") %>' OnCommand="LinkButtonViewPhaseDetails_Click"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:Label ID="LabelProjectPhase" runat="server" Text='<%# Bind("ProjectPhase") %>'
                                                    Visible="false"></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelStatusID" runat="server" Text='<%# Bind("StatusID") %>' Visible="true"></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPhaseStartDate" runat="server" Text='<%# Bind("PhaseStartDate") %>'
                                                    Visible="true"></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPhaseEndDate" runat="server" Text='<%# Bind("PhaseEndDate") %>'
                                                    Visible="true"></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phase Type">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPhaseType" runat="server" Visible="true"></asp:Label>
                                                <asp:Label ID="LabelPhaseTypeID" runat="server" Text='<%# Bind("PhaseTypeID") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="LabelProductSprintID" runat="server" Text='<%# Bind("ProductSprintID") %>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Testing Type">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelTestingTypeID" runat="server" Text='<%# Bind("TestingTypeID") %>'
                                                    Visible="true"></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TRs Planned">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelTestCasesPlanned" runat="server" Text='<%# Bind("TotalCount") %>'
                                                    Visible="true"></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TRs Executed">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelTestCasesExecuted" runat="server" Text='<%# Bind("TotalExecuted") %>'
                                                    Visible="true"></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="About Project Phase" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelAboutProjectPhase" runat="server" Text='<%# Bind("AboutProjectPhase") %>'
                                                    Visible="true"></asp:Label></ItemTemplate>
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
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanelTestStrategy" runat="server" HeaderText="Test Strategy"
                TabIndex="1">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelNoTestStrategyDefined" runat="server" Text="No Test Strategy Defined"
                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 96%">
                                <%--<asp:GridView ID="GridViewTestStrategy" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="100%" Visible="false">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                    </Columns>
                                    <HeaderStyle BackColor="#00C0C0" />
                                    <AlternatingRowStyle BackColor="#FFE0C0" />
                                    <SelectedRowStyle BackColor="CornflowerBlue" />
                                </asp:GridView>--%>
                                <asp:Repeater ID="RepeaterTestStrategy" runat="server">
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="LabelPhaseType" runat="server" Text="PhaseType" CssClass="HyperLinkMenu"
                                                        ForeColor="#DB512D" Font-Underline="true" Font-Size="Small"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 5px">
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelNoCoveragesDefined" runat="server" Text="No Coverages Defined."
                                                        CssClass="HyperLinkMenu" ForeColor="AntiqueWhite" Font-Size="Small" Visible="false" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td style="width: 100%">
                                                    <asp:GridView ID="GridViewTestStrategy" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                        Width="100%">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
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
                                    </ItemTemplate>
                                </asp:Repeater>
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
            <ajaxToolkit:TabPanel ID="TabPanelLocalesVsPlatform" runat="server" HeaderText="Locales Vs Platforms Matrix"
                TabIndex="2" BackColor="#212121">
                <ContentTemplate>
                    <table style="width: 100%; height: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr style="height: 20px">
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
                TabIndex="3" BackColor="#212121">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="5">
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
                            <td style="width: 48%">
                                <asp:GridView ID="GridViewLocales" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="90%">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="LocaleID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelLocaleID" runat="server" Text='<%# Bind("LocaleID") %>' Visible="true"></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Locale Tier">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelTier" runat="server" Text='<%# Bind("Tier") %>'></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Locales">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelLocales" runat="server" Text='<%# Bind("Locale") %>'></asp:Label></ItemTemplate>
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
                            <td style="width: 47%">
                                <asp:GridView ID="GridViewPlatforms" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="90%">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="PlatformID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPlatformID" runat="server" Text='<%# Bind("PlatformID") %>' Visible="true"></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Platform Type">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPlatformType" runat="server" Text='<%# Bind("PlatformType") %>'></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Platform">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPlatform" runat="server" Text='<%# Bind("Platform") %>'></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Priority">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPlatformPriority" runat="server" Text='<%# Bind("Priority") %>'
                                                    Width="80%"></asp:Label></ItemTemplate>
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
            <ajaxToolkit:TabPanel ID="TabPanelAboutProduct" runat="server" HeaderText="About Product"
                TabIndex="4" BackColor="#212121">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td style="width: 2%">
                            </td>
                            <td style="width: 96%" align="right">
                                <asp:Button ID="ButtonEditAboutProduct" runat="server" Text="Edit About Product"
                                    OnClick="ButtonEditAboutProduct_Click" />
                                <asp:Button ID="ButtonSaveAboutProduct" runat="server" Text="Save About Product"
                                    Visible="False" OnClick="ButtonSaveAboutProduct_Click" />
                                <asp:Label ID="LabelProductVersionID" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 96%">
                                <asp:Label ID="LabelAboutProduct" runat="server" ForeColor="AntiqueWhite" Font-Size="Small"></asp:Label>
                                <asp:TextBox ID="TextBoxAboutProduct" runat="server" Visible="False" Height="50px"
                                    TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 96%">
                                <asp:Panel ID="PanelNoProductFeatures" runat="server" Width="100%" Height="100%"
                                    Visible="False">
                                    <table>
                                        <tr style="height: 20px">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr style="height: 20px">
                                            <td>
                                                <asp:Label ID="LabelNoProductFeatures" runat="server" Text="No Product Features"
                                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                                            </td>
                                        </tr>
                                        <tr style="height: 20px">
                                            <td colspan="3">
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
                            <td style="width: 96%">
                                <asp:Panel ID="PanelProductFeatures" runat="server" Width="100%" Height="100%" Visible="False">
                                    <uc1:MasterData ID="MasterDataProductFeatures" runat="server" />
                                </asp:Panel>
                            </td>
                            <td style="width: 2%">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanelImportantLinks" runat="server" HeaderText="Document Links"
                Visible="true" TabIndex="5">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <%--                       <tr>
                            <td colspan="5">
                                <asp:Label ID="LabelLinks" runat="server" Text="Links" CssClass="HyperLinkMenu" ForeColor="AntiqueWhite"
                                    Font-Size="Small" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 96%">
                                <asp:GridView ID="GridViewDocumentLinks" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="100%" OnRowDataBound="GridViewDocumentLinks_RowDataBound" OnRowUpdating="GridViewDocumentLinks_RowUpdating"
                                    OnRowCancelingEdit="GridViewDocumentLinks_RowCancelingEdit">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                                        <asp:TemplateField HeaderText="ProductLinkID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProductLinkID" runat="server" Text='<%# Bind("ProductLinkID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document Name">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelDocumentName" runat="server" Text='<%# Bind("DocumentName") %>'
                                                    Visible="true"></asp:Label>
                                                <asp:TextBox ID="TextBoxDocumentName" CssClass="TextBox" runat="server" Text='<%# Bind("DocumentName") %>'
                                                    Width="80%" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document Link">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelDocumentLink" runat="server" Visible="true"></asp:Label>
                                                <asp:TextBox ID="TextBoxDocumentLink" CssClass="TextBox" runat="server" Text='<%# Bind("DocumentLink") %>'
                                                    Width="80%" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonSave" runat="server" CausesValidation="False" CommandName="Add"
                                                    Visible="false" Text="SAVE" OnCommand="LinkButtonSaveDocumentLinks_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="False" CommandName="Update"
                                                    Text="UPDATE" OnCommand="LinkButtonUpdateDocumentLinks_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    Visible="true" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Visible="false" Text="CANCEL" OnCommand="LinkButtonCancelDocumentLinks_Click"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Visible="true" Text="DELETE" OnCommand="LinkButtonDeleteDocumentLinks_Click"
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
                        <%--                        <tr>
                            <td colspan="5">
                                <asp:Label ID="LabelUpload" runat="server" Text="Upload" CssClass="HyperLinkMenu"
                                    ForeColor="AntiqueWhite" Font-Size="Small" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 96%">
                                <asp:GridView ID="GridViewUploadDocuments" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="100%" OnRowDataBound="GridViewUploadDocuments_RowDataBound" OnRowUpdating="GridViewUploadDocuments_RowUpdating"
                                    OnRowCancelingEdit="GridViewUploadDocuments_RowCancelingEdit">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:BoundField ReadOnly="True" DataField="Row" HeaderText="S.No." />
                                        <asp:TemplateField HeaderText="ProductLinkID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProductLinkID" runat="server" Text='<%# Bind("ProductLinkID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grid Document Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelDocumentName" runat="server" Text='<%# Bind("DocumentName") %>'
                                                    Visible="true"></asp:Label>
                                                <asp:TextBox ID="TextBoxDocumentName" CssClass="TextBox" runat="server" Text='<%# Bind("DocumentName") %>'
                                                    Width="80%" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grid Document Link">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelDocumentLink" runat="server" Text='<%# Bind("DocumentLink") %>'
                                                    Visible="true"></asp:Label>
                                                <asp:TextBox ID="TextBoxDocumentLink" CssClass="TextBox" runat="server" Text='<%# Bind("DocumentLink") %>'
                                                    Width="80%" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonSave" runat="server" CausesValidation="False" CommandName="Add"
                                                    Visible="false" Text="SAVE" OnCommand="LinkButtonSaveDocumentLinks_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:LinkButton ID="LinkButton" runat="server" CausesValidation="False" CommandName="Update"
                                                    Text="UPDATE" OnCommand="LinkButtonUpdateDocumentLinks_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    Visible="true" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Visible="false" Text="CANCEL" OnCommand="LinkButtonCancelDocumentLinks_Click"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Visible="true" Text="DELETE" OnCommand="LinkButtonDeleteDocumentLinks_Click"
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
                        --%>
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanelOwnersAndUsers" runat="server" HeaderText="Team Information"
                TabIndex="6">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #212121">
                        <tr style="height: 20px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelNoActiveTeam" runat="server" Text="No Active Team" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 2%">
                            </td>
                            <td>
                                <asp:GridView ID="GridViewProductUsers" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="100%">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Team">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelUserName" runat="server" Text='<%# Bind("Vendor") %>'></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adobe UserID">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelUserID" runat="server" Text='<%# Bind("LoginID") %>'></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Role">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelProjectRole" runat="server" Text='<%# Bind("ProjectRole") %>'></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email ID">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelEmailID" runat="server" Text='<%# Bind("EmailID") %>'></asp:Label></ItemTemplate>
                                            <ControlStyle ForeColor="Navy" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact No">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label></ItemTemplate>
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
