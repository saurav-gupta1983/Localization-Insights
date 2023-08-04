<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintainLocalesVsPlatformsMatrixUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.MaintainLocalesVsPlatformsMatrixUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
<asp:Panel ID="PanelFilter" runat="server" Width="100%" Font-Bold="true" BorderColor="White"
    ForeColor="#DB512D">
    <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="left">
                <asp:Label ID="LabelHeader" runat="server" Text="Update Locales Vs Platform Matrix"
                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelProduct" runat="server" Width="100%">
                    <table style="width: 100%; height: 100%; color: White" runat="server" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProductValue" runat="server" Text="Default" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelProductVersion" runat="server" Width="100%">
                    <table style="width: 100%; height: 100%; color: White" runat="server" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProductVersion" runat="server" Text="Product Release:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:DropDownList ID="DropDownListProductVersion" runat="server" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownListProductVersion_OnSelectedIndexChanged" Visible="false">
                                </asp:DropDownList>
                                <asp:Label ID="LabelProductVersionID" runat="server" Text="Product Version ID" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                                <asp:Label ID="LabelProductVersionValue" runat="server" Text="Product Version" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelProjectPhase" runat="server" Width="100%">
                    <table style="width: 100%; height: 100%; color: White" runat="server" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProjectPhases" runat="server" Text="Project Phase:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:DropDownList ID="DropDownListProjectPhases" runat="server" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownListProjectPhases_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelVendor" runat="server" Width="100%">
                    <table id="TableVendor" style="width: 100%; height: 100%; color: White" runat="server"
                        cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelSelectVendor" runat="server" Text="Team:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="LabelVendorName" runat="server" Text="" CssClass="HyperLinkMenu" Visible="false"
                                    ForeColor="#DB512D" Font-Size="Small" />
                                <asp:DropDownList ID="DropDownListSelectVendor" runat="server" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownListSelectVendor_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelSelectUserType" runat="server" Width="100%" Visible="false">
                    <table id="Table2" style="width: 100%; height: 100%; color: White" runat="server"
                        cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelSelectUserView" runat="server" Text="Select Display Mode:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:DropDownList ID="DropDownListUserView" runat="server" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownListUserView_OnSelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="Define Matrix" Value="Define Matrix"></asp:ListItem>
                                    <asp:ListItem Selected="False" Text="Update Status" Value="Update Status"></asp:ListItem>
                                    <asp:ListItem Selected="False" Text="Display Only" Value="Display Only"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr style="height: 10px;">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelCoverages" runat="server" Width="100%">
                    <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelCoverages" runat="server" Text="Coverages:" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:DropDownList ID="DropDownListCoverages" runat="server" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownListCoverages_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelTestCases" runat="server" Width="100%">
                    <table style="width: 100%; height: 100%; color: White" runat="server" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelTotalTestCases" runat="server" Text="Coverage Test Cases Count:"
                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="LabelTestCasesValue" runat="server" Text="NOT SET" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                                <asp:Label ID="LabelTestSuite" runat="server" Text="" CssClass="HyperLinkMenu" ForeColor="#DB512D"
                                    Font-Size="Small" Visible="false" />
                                <asp:Label ID="LabelTestCasesCount" runat="server" Text="" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelPlatformsAndLocales" runat="server" Width="100%">
                    <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelPlatformsAndLocales" runat="server" Text="Locales and Platforms Filter:"
                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%" align="justify">
                                <table width="80%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="width: 15%">
                                            <asp:CheckBox ID="CheckBoxPlatformsWin" Checked="true" Text="WIN" ForeColor="#DB512D"
                                                AutoPostBack="true" Font-Size="Small" runat="server" OnCheckedChanged="CheckBoxPlatformsWin_OnCheckedChanged" />
                                        </td>
                                        <td style="width: 15%">
                                            <asp:CheckBox ID="CheckBoxPlatformsMac" Checked="true" Text="MAC" AutoPostBack="true"
                                                ForeColor="#DB512D" Font-Size="Small" runat="server" OnCheckedChanged="CheckBoxPlatformsMac_OnCheckedChanged" />
                                        </td>
                                        <td style="width: 35%" align="right">
                                            <asp:DropDownList ID="DropDownListLocaleBuilds" runat="server" Width="95%" AutoPostBack="true"
                                                OnSelectedIndexChanged="DropDownListLocaleBuilds_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 35%" align="right">
                                            <asp:DropDownList ID="DropDownListLocales" runat="server" Width="95%" AutoPostBack="true"
                                                OnSelectedIndexChanged="DropDownListLocales_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="height: 5px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelTestStudioIntegration" CssClass="medium" runat="server" Text="**For any testing on Test Studio integration, use <a href='http://sjwlapps1.corp.adobe.com:7001/TSSTG/TS/TS.html' Target='_blank'>TS Staging</a>"
                    Width="100%" ForeColor="red" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 5px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelDefineMatrix" runat="server" Width="100%" Visible="false">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Panel ID="PanelCopyOTMatrix" runat="server" Width="100%">
                                    <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 50%">
                                                <asp:Label ID="LabelCopyOTMatrix" runat="server" Text="Reuse already created OT Matrix:"
                                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                                            </td>
                                            <td style="width: 50%">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 50%">
                                                            <asp:DropDownList ID="DropDownListSelectProjectPhase" runat="server" Width="95%"
                                                                AutoPostBack="true" OnSelectedIndexChanged="DropDownListSelectProjectPhase_OnSelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 50%">
                                                            <asp:DropDownList ID="DropDownListSelectCoverage" runat="server" Width="95%">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="2">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="PanelDistributionType" runat="server" Width="100%">
                                    <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 50%">
                                                <asp:Label ID="LabelDistributionType" runat="server" Text="Distribution Parameters:"
                                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                                            </td>
                                            <td style="width: 50%">
                                                <table style="width: 100%;" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LabelTestCasesRepository" runat="server" Text="TestCases Repository:"
                                                                CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                                                            <asp:RadioButton ID="RadioButtonTestStudio" Checked="false" Text="Test Studio" ForeColor="#DB512D"
                                                                Font-Size="Small" GroupName="RadioButtonListTestCasesRepository" runat="server" />
                                                            <asp:RadioButton ID="RadioButtonDocument" Checked="true" Text="Document" ForeColor="#DB512D"
                                                                Font-Size="Small" GroupName="RadioButtonListTestCasesRepository" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="PanelTestCasesDistribution" runat="server" Width="100%">
                                                                <asp:Label ID="LabelTestCasesDistribution" runat="server" Text="TestCases Distribution:"
                                                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                                                                <asp:RadioButton ID="RadioButtonManual" Checked="true" Text="Manual" ForeColor="#DB512D"
                                                                    Font-Size="Small" GroupName="RadioButtonListDistributeTestCases" runat="server" />
                                                                <asp:RadioButton ID="RadioButtonEqual" Checked="false" Text="Automatic(Equal)" ForeColor="#DB512D"
                                                                    Font-Size="Small" GroupName="RadioButtonListDistributeTestCases" runat="server" />
                                                                <asp:RadioButton ID="RadioButtonLocaleWeight" Checked="false" Text="Locale Weight"
                                                                    ForeColor="#DB512D" Font-Size="Small" GroupName="RadioButtonListDistributeTestCases"
                                                                    runat="server" Visible="false" />
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="PanelAcrossLocalesandPlatforms" runat="server" Width="100%">
                                                                <asp:Label ID="LabelAcrossLocalesandPlatforms" runat="server" Text="Across Locales/Platforms:"
                                                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                                                                <asp:RadioButton ID="RadioButtonNone" Checked="true" Text="None" ForeColor="#DB512D"
                                                                    Font-Size="Small" GroupName="RadioButtonListDistributionType" runat="server" />
                                                                <asp:RadioButton ID="RadioButtonLocales" Checked="false" Text="Locales" ForeColor="#DB512D"
                                                                    Font-Size="Small" GroupName="RadioButtonListDistributionType" runat="server" />
                                                                <asp:RadioButton ID="RadioButtonPlatforms" Checked="false" Text="Platforms" ForeColor="#DB512D"
                                                                    Font-Size="Small" GroupName="RadioButtonListDistributionType" runat="server" />
                                                                <asp:RadioButton ID="RadioButtonBoth" Checked="false" Text="Locales and Platforms"
                                                                    ForeColor="#DB512D" Font-Size="Small" GroupName="RadioButtonListDistributionType"
                                                                    runat="server" />
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="PanelSelectionCriteria" runat="server" Width="100%">
                                    <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 50%">
                                            </td>
                                            <td style="width: 50%">
                                                <table style="width: 100%;" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LabelDistributionCriteria" runat="server" Text="Distribution Criteria:"
                                                                CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                                                            <asp:CheckBox ID="CheckBoxProductArea" Checked="true" Text="Product/Sub Area" ForeColor="#DB512D"
                                                                Font-Size="Small" GroupName="CheckBoxDistributionCriteria" runat="server" />
                                                            <asp:CheckBox ID="CheckBoxPriority" Checked="true" Text="Priority" ForeColor="#DB512D"
                                                                Font-Size="Small" GroupName="CheckBoxDistributionCriteria" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="2">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="ButtonSaveMatrix" runat="server" OnClick="ButtonSaveMatrix_Click"
                                    Text="Save/Update OT Matrix" />
                                &nbsp;
                                <asp:Button ID="ButtonCreateTestSuite" runat="server" OnClick="ButtonCreateTestSuite_Click"
                                    Text="Create Test Suite" Enabled="false" />
                                &nbsp;
                                <asp:Button ID="ButtonResetMatrix" runat="server" OnClick="ButtonResetMatrix_Click"
                                    Text="Reset Matrix" />
                                &nbsp;
                                <asp:Button ID="ButtonUpdateMatrixManually" runat="server" Text="Update OT Matrix manually"
                                    OnClick="ButtonUpdateMatrixManually_Click" Enabled="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelUpdateStatus" runat="server" Width="100%" Visible="false">
                    <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Button ID="ButtonGenerateSID" runat="server" OnClick="ButtonGenerateSID_Click"
                                    Text="Refresh SID Information" Enabled="false" />
                                &nbsp;
                                <asp:Button ID="ButtonUpdateSIDMatrixManually" runat="server" Text="Update OT Matrix Manually"
                                    OnClick="ButtonUpdateMatrixManually_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
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
            <td>
                <asp:Panel ID="WrapperPanelMatrix" runat="server">
                    <table style="width: 100%; background-color: #212121">
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewLocalesVsPlatformMatrix" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                    Width="100%" OnRowCreated="GridViewLocalesVsPlatformMatrix_RowCreated" OnRowDataBound="GridViewLocalesVsPlatformMatrix_RowDataBound">
                                    <RowStyle ForeColor="#000066" />
                                    <Columns>
                                    </Columns>
                                    <HeaderStyle BackColor="#00C0C0" />
                                    <RowStyle HorizontalAlign="Center" />
                                    <AlternatingRowStyle BackColor="#FFE0C0" />
                                    <SelectedRowStyle BackColor="CornflowerBlue" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelFooterPadding" runat="server" Width="100%" Height="100%" Visible="false">
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
