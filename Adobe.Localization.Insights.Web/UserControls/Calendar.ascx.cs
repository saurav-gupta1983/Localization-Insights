using System;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Adobe.Localization.Insights.Web
{
    /// <summary>
    /// Calendar
    /// </summary>
    public partial class Calendar : System.Web.UI.UserControl
    {
        #region Variable Declaration

        private string selectedDate;
        public event EventHandler OnCalendarSelectionChanged;

        #endregion

        #region Page Load Event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            CalendarUserControl.SelectionChanged += new EventHandler(CalendarControl_SelectionChanged);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// CalendarUserControl_SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CalendarUserControl_SelectionChanged(object sender, EventArgs e)
        {
            selectedDate = CalendarUserControl.SelectedDate.ToShortDateString();
            DateTextBox.Text = CalendarUserControl.SelectedDate.ToShortDateString();
            CalendarUserControl.SelectedDate = Convert.ToDateTime("01/01/0001");
            DivCalendar.Style.Add("display", "none");
            //Session["isDateChanged"] = 1;
        }

        /// <summary>
        /// GetDate
        /// </summary>
        /// <returns></returns>
        public string GetDate()
        {
            return DateTextBox.Text;
        }

        /// <summary>
        /// CalendarUserControl_VisibleMonthChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CalendarUserControl_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            DivCalendar.Style.Add("display", "inline");
        }

        /// <summary>
        /// OnSelectionChanged
        /// </summary>
        /// <param name="e"></param>
        protected void OnSelectionChanged(EventArgs e)
        {
            if (OnCalendarSelectionChanged != null)
            {
                OnCalendarSelectionChanged(this, e);
            }
        }

        /// <summary>
        /// CalendarControl_SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalendarControl_SelectionChanged(object sender, EventArgs e)
        {
            OnSelectionChanged(e);
        }

        #endregion

        #region Properties

        [Browsable(true)]
        [Category("Date")]
        [Description("Gets and sets the Date in the textbox")]
        public string Date
        {
            get
            {
                return DateTextBox.Text;
            }
            set
            {
                DateTextBox.Text = value;
            }
        }

        #endregion

    }
}