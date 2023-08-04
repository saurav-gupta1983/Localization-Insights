<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocalesVsPlatformsDataUserControl_Old.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.LocalesVsPlatformsDataUserControl" %>
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
    <table id="Table1" style="width: 100%; height: 100%; color: White" runat="server">
        <tr>
            <td align="left">
                <asp:Label ID="LabelHeader" runat="server" Text="Header" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelProduct" runat="server" Width="100%">
                    <table style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProduct" runat="server" Text="Product" CssClass="HyperLinkMenu"
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
                    <table style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProductVersion" runat="server" Text="Product Version" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:DropDownList ID="DropDownListProductVersion" runat="server" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownListProductVersion_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelProjectPhase" runat="server" Width="100%">
                    <table style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelProjectPhases" runat="server" Text="Project Phase" CssClass="HyperLinkMenu"
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
                <asp:Panel ID="PanelTestCases" runat="server" Width="100%">
                    <table style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelTotalTestCases" runat="server" Text="TCs Count" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="LabelTestCasesValue" runat="server" Text="NOT SET" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelCoverages" runat="server" Width="100%">
                    <table id="TableCoverages" style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelCoverages" runat="server" Text="Label Coverages" CssClass="HyperLinkMenu"
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
                <asp:Panel ID="PanelVendor" runat="server" Width="100%">
                    <table id="TableVendor" style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelSelectVendor" runat="server" Text="Vendor" CssClass="HyperLinkMenu"
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
                <asp:Panel ID="PanelPlatformsAndLocales" runat="server" Width="100%">
                    <table style="width: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelPlatformsAndLocales" runat="server" Text="Label Platforms and Locales"
                                    CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%" align="justify">
                                <table width="80%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="width: 30%">
                                            <asp:RadioButton ID="RadioBtnPlatformsBoth" Checked="true" Text="Both Platforms"
                                                ForeColor="#DB512D" AutoPostBack="true" Font-Size="Small" GroupName="RadioButtonPlatformType"
                                                runat="server" OnCheckedChanged="RadioBtnPlatformsBoth_OnCheckedChanged" />
                                        </td>
                                        <td style="width: 34%">
                                            <asp:RadioButton ID="RadioBtnPlatformsWin" Checked="false" Text="WIN" ForeColor="#DB512D"
                                                AutoPostBack="true" Font-Size="Small" GroupName="RadioButtonPlatformType" runat="server"
                                                OnCheckedChanged="RadioBtnPlatformsWin_OnCheckedChanged" />
                                        </td>
                                        <td style="width: 33%">
                                            <asp:RadioButton ID="RadioBtnPlatformsMac" Checked="false" Text="MAC" AutoPostBack="true"
                                                ForeColor="#DB512D" Font-Size="Small" GroupName="RadioButtonPlatformType" runat="server"
                                                OnCheckedChanged="RadioBtnPlatformsMac_OnCheckedChanged" />
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
                <asp:Panel ID="PanelSelectUserType" runat="server" Width="100%" Visible="false">
                    <table style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelSelectUserView" runat="server" Text="Select View" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <asp:DropDownList ID="DropDownListUserType" runat="server" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownListUserType_OnSelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="Owner" Value="Owner"></asp:ListItem>
                                    <asp:ListItem Selected="False" Text="User" Value="User"></asp:ListItem>
                                    <asp:ListItem Selected="False" Text="Display Only" Value="Display Only"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelDistributionType" runat="server" Width="100%" Enabled="false"
                    Visible="false">
                    <table style="width: 100%; height: 100%; color: White" runat="server">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="LabelDistributionType" runat="server" Text="Distribution Type" CssClass="HyperLinkMenu"
                                    ForeColor="#DB512D" Font-Size="Small" />
                            </td>
                            <td style="width: 50%">
                                <table style="width: 100%; height: 100%; color: White" runat="server">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelTestCasesDistribution" runat="server" Text="TestCases Distribution"
                                                CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                                            <asp:RadioButton ID="RadioBtnManual" Checked="true" Text="Manual" ForeColor="#DB512D"
                                                Font-Size="Small" GroupName="RadioButtonListDistributeTestCases" runat="server" />
                                            <asp:RadioButton ID="RadioButtonEqual" Checked="false" Text="Equal" ForeColor="#DB512D"
                                                Font-Size="Small" GroupName="RadioButtonListDistributeTestCases" runat="server" />
                                            <asp:RadioButton ID="RadioButtonLocaleWeight" Checked="false" Text="Locale Weight"
                                                ForeColor="#DB512D" Font-Size="Small" GroupName="RadioButtonListDistributeTestCases"
                                                runat="server" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelAcrossLocalesandPlatforms" runat="server" Text="Across Locales/Platforms"
                                                CssClass="HyperLinkMenu" ForeColor="#DB512D" Font-Size="Small" />
                                            <asp:RadioButton ID="RadioButtonNone" Checked="true" Text="None" ForeColor="#DB512D"
                                                Font-Size="Small" GroupName="RadioButtonListDistributionType" runat="server" />
                                            <asp:RadioButton ID="RadioButtonLocales" Checked="false" Text="Locales" ForeColor="#DB512D"
                                                Font-Size="Small" GroupName="RadioButtonListDistributionType" runat="server" />
                                            <asp:RadioButton ID="RadioButtonBoth" Checked="false" Text="Locales and Platforms"
                                                ForeColor="#DB512D" Font-Size="Small" GroupName="RadioButtonListDistributionType"
                                                runat="server" />
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
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="ButtonDistributeTestCases" runat="server" OnClick="ButtonDistributeTestCases_Click"
                    Text="Button Distribute" Visible="false" />
                <asp:Button ID="ButtonUpdateLocalesVsPlatformsMatrix" runat="server" Text="Update Locales Vs Platforms Matrix"
                    OnClick="ButtonUpdateLocalesVsPlatformsMatrix_Click" />
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
                </asp:Panel>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="PanelFooterPadding" runat="server" Width="100%" Height="100%" Visible="false">
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
