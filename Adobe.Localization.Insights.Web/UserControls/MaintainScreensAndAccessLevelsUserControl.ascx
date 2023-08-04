<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintainScreensAndAccessLevelsUserControl.ascx.cs"
    Inherits="Adobe.Localization.Insights.Web.UserControls.MaintainScreensAndAccessLevelsUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script type="text/javascript">
    //Function restricting user to Enter only Numeric Data
    function NumericOnly() {
        //        alert(event.keyCode);
        if (event.keyCode != 46 && event.keyCode != 190 && event.keyCode != 8) {
            if (event.keyCode < 48 || event.keyCode > 57) {
                alert("Please enter Numeric value.");
                event.returnValue = false;
            }
        }
    }    
</script>
<asp:Panel ID="Panel" runat="server" Width="100%" Style="position: relative;">
    <table style="width: 80%; height: 100%; color: White">
        <tr>
            <td align="left">
                <asp:Label ID="LabelHeading" runat="server" Text="Maintain Screen Access" CssClass="HyperLinkMenu"
                    ForeColor="#DB512D" Font-Underline="true" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 30px">
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
            <td align="left">
                <asp:Panel ID="PanelPopulateScreensData" runat="server" Width="100%">
                    <ajaxToolkit:TabContainer ID="TabContainerPopulateScreensData" runat="server">
                        <ajaxToolkit:TabPanel ID="TabPanelSetScreensDetails" runat="server" HeaderText="Screen Details"
                            TabIndex="1" BackColor="#212121">
                            <ContentTemplate>
                                <table style="width: 100%; background-color: #212121">
                                    <tr style="height: 30px">
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2%">
                                        </td>
                                        <td style="width: 96%">
                                            <asp:GridView ID="GridViewScreenDetails" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                Width="100%" OnRowDataBound="GridViewScreenDetails_RowDataBound" OnRowCancelingEdit="GridViewScreenDetails_RowCancelingEdit"
                                                OnRowUpdating="GridViewScreenDetails_RowUpdating">
                                                <RowStyle ForeColor="#000066" />
                                                <Columns>
                                                    <asp:BoundField ReadOnly="True" DataField="ScreenID" HeaderText="Screen ID" />
                                                    <asp:TemplateField HeaderText="ScreenID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelScreenID" runat="server" Text='<%# Bind("ScreenID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Parent ScreenID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelParentScreenID" runat="server" Text='<%# Bind("ParentScreenID") %>'></asp:Label>
                                                            <asp:TextBox ID="TextBoxParentScreenID" runat="server" Text='<%# Bind("ParentScreenID") %>'
                                                                Width="80%" Visible="false" onkeydown="NumericOnly()"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ControlStyle ForeColor="Navy" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Screen Identifier" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelScreenIdentifier" runat="server" Text='<%# Bind("ScreenIdentifier") %>'></asp:Label>
                                                            <asp:TextBox ID="TextBoxScreenIdentifier" runat="server" Text='<%# Bind("ScreenIdentifier") %>'
                                                                Width="80%" Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ControlStyle ForeColor="Navy" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sequence" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSequence" runat="server" Text='<%# Bind("Sequence") %>'></asp:Label>
                                                            <asp:TextBox ID="TextBoxSequence" runat="server" Text='<%# Bind("Sequence") %>' onkeydown="NumericOnly()"
                                                                Visible="false" Width="80%"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ControlStyle ForeColor="Navy" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Page Name" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelPageName" runat="server" Text='<%# Bind("PageName") %>'></asp:Label>
                                                            <asp:TextBox ID="TextBoxPageName" runat="server" Text='<%# Bind("PageName") %>' Visible="false"
                                                                Width="90%"></asp:TextBox>
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
                        <ajaxToolkit:TabPanel ID="TabPanelSetProductScreenAccess" runat="server" HeaderText="Screen Access"
                            TabIndex="1" BackColor="#212121">
                            <ContentTemplate>
                                <table style="width: 100%; background-color: #212121">
                                    <tr style="height: 20px">
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2%">
                                        </td>
                                        <td style="width: 96%">
                                            <asp:GridView ID="GridViewProductScreenAccess" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                Width="100%" OnRowDataBound="GridViewProductScreenAccess_RowDataBound" AllowPaging="true"
                                                PageSize="30" OnPageIndexChanging="GridViewProductScreenAccess_PageIndexChanging">
                                                <RowStyle ForeColor="#000066" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ScreenAccessExists" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelScreenAccessExists" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ScreenID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelScreenID" runat="server" Text='<%# Bind("ScreenID") %>'></asp:Label>
                                                            <%--  <asp:Label ID="Label1" runat="server" Text="("></asp:Label>
                                                            <asp:Label ID="LabelParentScreenID" runat="server" Text='<%# Bind("ParentScreenID") %>'></asp:Label>
                                                            <asp:Label ID="Label2" runat="server" Text="-"></asp:Label>
                                                            <asp:Label ID="LabelSequence" runat="server" Text='<%# Bind("Sequence") %>'></asp:Label>
                                                            <asp:Label ID="Label3" runat="server" Text=")"></asp:Label>
                                                            --%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Screen Identifier" ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelScreenIdentifier" runat="server" Text='<%# Bind("Screen") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IQE">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonRead1" Text="R" runat="server" GroupName="IQE" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReadWrite1" Text="RW" runat="server" GroupName="IQE" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReport1" Text="RP" runat="server" GroupName="IQE" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonClear1" Text="NA" runat="server" Checked="false"
                                                                            GroupName="IQE" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IPM">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonRead2" Text="R" runat="server" GroupName="IPM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReadWrite2" Text="RW" runat="server" GroupName="IPM" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReport2" Text="RP" runat="server" GroupName="IPM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonClear2" Text="NA" runat="server" Checked="false"
                                                                            GroupName="IPM" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TTM">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonRead5" Text="R" runat="server" GroupName="TTM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReadWrite5" Text="RW" runat="server" GroupName="TTM" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReport5" Text="RP" runat="server" GroupName="TTM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonClear5" Text="NA" runat="server" Checked="false"
                                                                            GroupName="TTM" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TTL">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonRead6" Text="R" runat="server" GroupName="TTL" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReadWrite6" Text="RW" runat="server" GroupName="TTL" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReport6" Text="RP" runat="server" GroupName="TTL" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonClear6" Text="NA" runat="server" Checked="false"
                                                                            GroupName="TTL" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TPM">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonRead8" Text="R" runat="server" GroupName="TTL" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReadWrite8" Text="RW" runat="server" GroupName="TTL" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReport8" Text="RP" runat="server" GroupName="TTL" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonClear8" Text="NA" runat="server" Checked="false"
                                                                            GroupName="TTL" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Client - Product Manager">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonRead10" Text="R" runat="server" GroupName="CPM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReadWrite10" Text="RW" runat="server" GroupName="CPM" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReport10" Text="RP" runat="server" GroupName="CPM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonClear10" Text="NA" runat="server" Checked="false"
                                                                            GroupName="CPM" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButtonSave" runat="server" CausesValidation="False" CommandName="Save"
                                                                Visible="true" Text="SAVE" OnCommand="LinkButtonSaveProductAccessDetails_Click"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        </ItemTemplate>
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
                        <ajaxToolkit:TabPanel ID="TabPanelSetGroupScreenAccess" runat="server" HeaderText="Group - Screen Access"
                            TabIndex="1" BackColor="#212121">
                            <ContentTemplate>
                                <table style="width: 100%; background-color: #212121">
                                    <tr style="height: 20px">
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2%">
                                        </td>
                                        <td style="width: 96%">
                                            <asp:GridView ID="GridViewGroupScreenAccess" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                Width="100%" OnRowDataBound="GridViewGroupScreenAccess_RowDataBound" AllowPaging="true"
                                                PageSize="30" OnPageIndexChanging="GridViewGroupScreenAccess_PageIndexChanging">
                                                <RowStyle ForeColor="#000066" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ScreenAccessExists" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelScreenAccessExists" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Screen ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelScreenID" runat="server" Text='<%# Bind("ScreenID") %>'></asp:Label>
                                                            <%--                                                            <asp:Label ID="Label1" runat="server" Text="("></asp:Label>
                                                            <asp:Label ID="LabelParentScreenID" runat="server" Text='<%# Bind("ParentScreenID") %>'></asp:Label>
                                                            <asp:Label ID="Label2" runat="server" Text="-"></asp:Label>
                                                            <asp:Label ID="LabelSequence" runat="server" Text='<%# Bind("Sequence") %>'></asp:Label>
                                                            <asp:Label ID="Label3" runat="server" Text=")"></asp:Label>
                                                            --%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Screen Identifier">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelScreenIdentifier" runat="server" Text='<%# Bind("Screen") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RPM">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonRead4" Text="R" runat="server" GroupName="RPM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReadWrite4" Text="RW" runat="server" GroupName="RPM" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReport4" Text="RP" runat="server" GroupName="RPM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonClear4" Text="NA" runat="server" Checked="false"
                                                                            GroupName="RPM" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GPM">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonRead3" Text="R" runat="server" GroupName="GPM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReadWrite3" Text="RW" runat="server" GroupName="GPM" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReport3" Text="RP" runat="server" GroupName="GPM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonClear3" Text="NA" runat="server" Checked="false"
                                                                            GroupName="GPM" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ADM">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonRead9" Text="R" runat="server" GroupName="ADM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReadWrite9" Text="RW" runat="server" GroupName="ADM" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReport9" Text="RP" runat="server" GroupName="ADM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonClear9" Text="NA" runat="server" Checked="false"
                                                                            GroupName="ADM" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LNG">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonRead11" Text="R" runat="server" GroupName="ADM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReadWrite11" Text="RW" runat="server" GroupName="ADM" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonReport11" Text="RP" runat="server" GroupName="ADM" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="RadiobuttonClear11" Text="NA" runat="server" Checked="false"
                                                                            GroupName="LNG" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButtonSave" runat="server" CausesValidation="False" CommandName="Save"
                                                                Visible="true" Text="SAVE" OnCommand="LinkButtonSaveGroupAccessDetails_Click"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        </ItemTemplate>
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
