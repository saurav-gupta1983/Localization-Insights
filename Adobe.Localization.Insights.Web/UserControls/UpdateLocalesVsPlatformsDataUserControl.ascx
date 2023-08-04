<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateLocalesVsPlatformsDataUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.UpdateLocalesVsPlatformsDataUserControl" %>
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
            <td align="center">
                <asp:Label ID="LabelHeader" runat="server" Text="Update Locales vs Platform Matrix"
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
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 50%">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="LabelProduct" runat="server" Text="Product:" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
                                        </td>
                                        <td style="width: 50%">
                                            <asp:Label ID="LabelProductValue" runat="server" Text="Default" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
                                            <asp:Label ID="LabelProductVersionValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="LabelSelectVendor" runat="server" Text="Team:" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
                                        </td>
                                        <td style="width: 50%">
                                            <asp:Label ID="LabelVendorName" runat="server" Text="" CssClass="HyperLinkMenu" ForeColor="#DB512D"
                                                Font-Size="Small" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="LabelProjectPhases" runat="server" Text="Project Phase:" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
                                        </td>
                                        <td style="width: 50%">
                                            <asp:Label ID="LabelProjectPhaseValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="LabelPlatformsFilter" runat="server" Text="Platforms Filter:" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
                                        </td>
                                        <td style="width: 50%" align="justify">
                                            <asp:Label ID="LabelPlatformsFilterValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="LabelCoverages" runat="server" Text="Phase Coverage:" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
                                        </td>
                                        <td style="width: 50%">
                                            <asp:Label ID="LabelCoveragesValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="LabelLocalesFilter" runat="server" Text="Locales Filter:" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
                                        </td>
                                        <td style="width: 50%" align="justify">
                                            <asp:Label ID="LabelLocalesFilterValue" runat="server" Text="" CssClass="HyperLinkMenu"
                                                ForeColor="#DB512D" Font-Size="Small" />
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
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="ButtonUpdateLocalesVsPlatformsMatrix" runat="server" Text="Update Locales Vs Platforms Matrix"
                    OnClick="ButtonUpdateLocalesVsPlatformsMatrix_Click" />
                &nbsp;
                <asp:Button ID="ButtonClose" runat="server" Text="Close" OnClientClick="window.close();" />
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
</asp:Panel>
