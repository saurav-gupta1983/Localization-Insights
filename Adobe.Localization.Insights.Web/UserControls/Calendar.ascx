<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendar.ascx.cs" Inherits="Adobe.Localization.Insights.Web.Calendar" %>
<asp:Panel ID="WrapperPanel" runat="server" Width="100%">
    <asp:TextBox ID="DateTextBox" runat="server" CssClass="TextBox" Enabled="false" Font-Bold="true"></asp:TextBox>
    <input type="button" value="..." id="button" runat="server" /><br />
    <div id="DivCalendar" style="display: none; position: absolute" runat="server">
        <asp:Calendar ID="CalendarUserControl" runat="server" BackColor="White" Width="220px"
            ForeColor="#003399" Height="200px" Font-Size="8pt" Font-Names="Verdana" BorderColor="#3366CC"
            DayNameFormat="Shortest" CellPadding="1" OnSelectionChanged="CalendarUserControl_SelectionChanged"
            OnVisibleMonthChanged="CalendarUserControl_VisibleMonthChanged" BorderWidth="1px">
            <TodayDayStyle ForeColor="White" BackColor="#99CCCC"></TodayDayStyle>
            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666"></SelectorStyle>
            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF"></NextPrevStyle>
            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px"></DayHeaderStyle>
            <SelectedDayStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#185394"></SelectedDayStyle>
            <TitleStyle Font-Bold="True" BorderColor="#3366CC" BackColor="#003399" BorderWidth="1px"
                Font-Size="10pt" ForeColor="#CCCCFF" Height="25px"></TitleStyle>
            <WeekendDayStyle BackColor="#CCCCFF"></WeekendDayStyle>
            <OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
        </asp:Calendar>
    </div>
</asp:Panel>
<script type="text/javascript">
    function OnClick(id) {
        if (document.getElementById(id).style.display == "none") {
            document.getElementById(id).style.display = "";
        }
        else
            document.getElementById(id).style.display = "none";
    }
</script>
