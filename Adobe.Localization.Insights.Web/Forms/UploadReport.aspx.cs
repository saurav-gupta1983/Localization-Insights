using System;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Globalization;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;
using DAO = Adobe.Localization.Insights.DataLayer.DataAccessObjects;
using Query = Adobe.Localization.Insights.DataLayer.Query;
using System.Windows.Forms;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text;

namespace Adobe.Localization.Insights.Web
{
    /// <summary>
    /// UploadReport
    /// </summary>
    public partial class UploadReport : System.Web.UI.Page
    {
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        DataSet dt1 = new DataSet();
        DataSet dt = new DataSet();
        DataSet dt_later = new DataSet();

        DTO.Users use = new DTO.Users();
        DTO.Product pr = new DTO.Product();
        DTO.ProjectPhases pp = new DTO.ProjectPhases();
        static int red = 0;
        static int yellow = 0;
        static int col_no = 0, extra = 0;
        static int green = 0;
        static int count_red = 0;
        static int count_yellow = 0;

        static string date1 = "";
        static string date2 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Menu1.Visible = false;
                MultiView1.Visible = false;
                Label2.Visible = false;
                Label3.Visible = false;
                label_filename.Visible = false;
                label_weeks.Visible = false;
                Submit.Visible = false;
                hider.Attributes.Add("style", "display:none;");
                popup_box.Attributes.Add("style", "display:none;");
                popup_box_success_upload.Attributes.Add("style", "display:none;");
                Guessed_Year.Visible = false;


                SheetName.Text = "Status";


            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void upload(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                if (FileUpload1.HasFile)
                {
                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    string[] validFileTypes = { "xls", "xlsx", "xlsm" };
                    string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                    bool isValidFile = false;
                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        if (ext == "." + validFileTypes[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }
                    if (!isValidFile)
                    {
                        Validate.ForeColor = System.Drawing.Color.Red;
                        Validate.Text = "Invalid File.Please upload a Weekly Status Report in excel format.";

                    }
                    else
                    {
                        Validate.ForeColor = System.Drawing.Color.Green;
                        Validate.Text = "File uploaded successfully.";


                        string save = @"C:\Uploads";
                        if (!System.IO.Directory.Exists(save))
                            System.IO.Directory.CreateDirectory(save);

                        string path = Path.Combine(save, FileName);
                        FileUpload1.PostedFile.SaveAs(Path.Combine(save, FileName));

                        label_filename.Text = FileName;

                        string conStr = "";
                        switch (Extension)
                        {
                            case ".xls": //Excel 97-03
                                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                                         .ConnectionString;
                                break;
                            case ".xlsx": //Excel 07
                                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                                          .ConnectionString;
                                break;

                            case ".xlsm": //Excel 10
                                conStr = ConfigurationManager.ConnectionStrings["Excel010ConString"]
                                          .ConnectionString;
                                break;


                        }

                        conStr = String.Format(conStr, path);
                        OleDbConnection connExcel = new OleDbConnection(conStr);

                        OleDbCommand cmdExcel = new OleDbCommand();
                        OleDbDataAdapter oda = new OleDbDataAdapter();


                        OleDbCommand cmdExcel1 = new OleDbCommand();
                        OleDbDataAdapter oda1 = new OleDbDataAdapter();

                        cmdExcel.Connection = connExcel;
                        cmdExcel1.Connection = connExcel;

                        DataTable dtExcelSchema = new DataTable();
                        connExcel.Open();
                        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        connExcel.Close();

                        try
                        {
                            string sheet1 = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Open();
                            string sheet = "";
                            if (SheetName.Text == "Status") sheet = "Status$";
                            else if (SheetName.Text == "AI") sheet = "'AI $'";
                            else if (SheetName.Text == "PRE") sheet = "'PRE $'";

                            if (sheet == "")
                            {
                                Validate.ForeColor = System.Drawing.Color.Red;
                                Validate.Text = "Invalid Sheet Name";
                            }
                            else
                            {
                                cmdExcel.CommandText = "SELECT * From [" + sheet + "]";
                                cmdExcel1.CommandText = "SELECT * From [Summary$]";

                                oda.SelectCommand = cmdExcel;
                                oda1.SelectCommand = cmdExcel1;

                                oda.Fill(dt);
                                oda1.Fill(dt1);

                                oda.AcceptChangesDuringFill = true;
                                dt.AcceptChanges();
                                dt1.AcceptChanges();


                                for (int a = 1; a < dt.Tables[0].Columns.Count; a++)
                                {
                                    if (dt.Tables[0].Rows[2][a] == DBNull.Value || dt.Tables[0].Rows[0][a].ToString().Contains("dd"))
                                    {
                                        dt.Tables[0].Columns.RemoveAt(a);
                                    }
                                }

                                for (int a = 0; a < dt.Tables[0].Rows.Count; a++)
                                {
                                    int count = 0;
                                    for (int c = 0; c < dt.Tables[0].Columns.Count; c++)
                                    {

                                        if (dt.Tables[0].Rows[a][c].ToString() == "" || dt.Tables[0].Rows[a][c].ToString().Trim() == string.Empty || dt.Tables[0].Rows[a][c].ToString() == "&nbsp;" || dt.Tables[0].Rows[a][c].ToString() == string.Empty || string.IsNullOrEmpty(dt.Tables[0].Rows[a][c].ToString()) || string.IsNullOrWhiteSpace(dt.Tables[0].Rows[a][c].ToString()))
                                        {
                                            count++;
                                        }
                                    }
                                    if (count == dt.Tables[0].Columns.Count) dt.Tables[0].Rows[a].Delete();
                                }

                                DataRow drDateMetrics = dt.Tables[0].NewRow();
                                dt.Tables[0].Rows.InsertAt(drDateMetrics, 1);

                                GridView1.DataSource = dt;
                                GridView1.DataBind();


                                dt1.Tables[0].Rows.RemoveAt(0);

                                if (dt1.Tables[0].Columns.Count > 50)
                                {
                                    for (int fg = 50; fg < dt1.Tables[0].Columns.Count; fg++)
                                        dt1.Tables[0].Columns.RemoveAt(fg);
                                }

                                if (dt1.Tables[0].Rows.Count > 40)
                                {
                                    for (int fg = 40; fg < dt1.Tables[0].Rows.Count; fg++)
                                        dt1.Tables[0].Rows[fg].Delete();
                                }

                                GridView2.DataSource = dt1;
                                GridView2.DataBind();

                                connExcel.Close();
                                for (int dc = 0; dc < GridView1.Rows[0].Cells.Count; dc++)
                                {
                                    GridView1.Rows[0].Cells[dc].BackColor = System.Drawing.Color.Black;
                                    GridView1.Rows[0].Cells[dc].ForeColor = System.Drawing.Color.White;

                                    GridView1.Rows[1].Cells[dc].BackColor = System.Drawing.Color.LightGray;
                                    GridView1.Rows[1].Cells[dc].ForeColor = System.Drawing.Color.Black;

                                    GridView1.Rows[2].Cells[dc].BackColor = System.Drawing.Color.Black;
                                    GridView1.Rows[2].Cells[dc].ForeColor = System.Drawing.Color.White;
                                }

                                for (int g = 3; g < GridView1.Rows.Count; g++)
                                {
                                    GridView1.Rows[g].BackColor = System.Drawing.Color.MistyRose;
                                    GridView1.Rows[g].Cells[0].Font.Bold = true;
                                }

                                for (int i = 0; i < GridView1.Rows.Count; i++)
                                {
                                    if (GridView1.Rows[i].Cells[0].Text.Contains('$'))
                                        GridView1.Rows[i].Visible = false;
                                }

                                for (int i = 30; i < GridView1.Rows.Count; i++)
                                {
                                    for (int col = 0; col < GridView1.Rows[0].Cells.Count; col++)
                                        if (GridView1.Rows[i].Cells[col].Text.Contains('#'))
                                        {
                                            GridView1.Rows[i].Visible = false;
                                            break;
                                        }
                                }

                                GridView1.Rows[2].Cells[0].Text = GridView1.Rows[0].Cells[0].Text;
                                GridView1.Rows[1].Cells[0].Text = "Actual Date -->";
                                GridView1.Rows[0].Cells[0].Text = "WSR Date -->";


                                for (int a = 0; a < GridView1.Rows[0].Cells.Count && a + 1 < GridView1.Rows[0].Cells.Count; a++)
                                {
                                    if ((GridView1.Rows[0].Cells[a].Text == "" || GridView1.Rows[0].Cells[a].Text.Trim() == string.Empty || GridView1.Rows[0].Cells[a].Text == "&nbsp;" || GridView1.Rows[0].Cells[a].Text == string.Empty || string.IsNullOrEmpty(GridView1.Rows[0].Cells[a].Text) || string.IsNullOrWhiteSpace(GridView1.Rows[0].Cells[a].Text)) && (GridView1.Rows[0].Cells[a + 1].Text == "" || GridView1.Rows[0].Cells[a + 1].Text.Trim() == string.Empty || GridView1.Rows[0].Cells[a + 1].Text == "&nbsp;" || GridView1.Rows[0].Cells[a + 1].Text == string.Empty || string.IsNullOrEmpty(GridView1.Rows[0].Cells[a + 1].Text) || string.IsNullOrWhiteSpace(GridView1.Rows[0].Cells[a + 1].Text)))
                                    {
                                        col_no = a + 1;

                                        for (int a1 = 0; a1 < GridView1.Rows.Count - 1; a1++)
                                        {
                                            for (int fg = a + 1; fg < GridView1.Rows[a1].Cells.Count; fg++)
                                                GridView1.Rows[a1].Cells[fg].Visible = false;
                                        }
                                        break;
                                    }

                                    else if (GridView1.Rows[0].Cells[a].Text.Contains("dd"))
                                    {
                                        col_no = a + 1;

                                        for (int a1 = 0; a1 < GridView1.Rows.Count - 1; a1++)
                                        {
                                            for (int fg = a + 1; fg < GridView1.Rows[a1].Cells.Count; fg++)
                                                GridView1.Rows[a1].Cells[fg].Visible = false;
                                        }
                                        break;
                                    }
                                }

                                for (int a = 0; a < GridView1.Rows.Count; a++)
                                {
                                    int count = 0;
                                    for (int c = 0; c < GridView1.Rows[a].Cells.Count; c++)
                                    {

                                        if (GridView1.Rows[a].Cells[c].Text == "" || GridView1.Rows[a].Cells[c].Text.Trim() == string.Empty || GridView1.Rows[a].Cells[c].Text == "&nbsp;" || GridView1.Rows[a].Cells[c].Text == string.Empty || string.IsNullOrEmpty(GridView1.Rows[a].Cells[c].Text) || string.IsNullOrWhiteSpace(GridView1.Rows[a].Cells[c].Text))
                                        {
                                            count++;
                                        }
                                    }
                                    if (count == GridView1.Rows[a].Cells.Count && GridView1.Rows[a].Cells[0].Text != "Features tested this week") GridView1.Rows[a].Visible = false;
                                }



                                var regex1 = new Regex("^\\d{2}");
                                var regex11 = new Regex("^\\d{1}");
                                var regex2 = new Regex(@"(?<Words>[a-zA-Z]{3})");
                                var regex3 = new Regex("(?<year>\\d\\d\\d\\d)$");

                                int count_year = 0;
                                string temp = "";
                                int y1 = 0, y2 = 0;
                                for (int i = 3; i <= col_no; i++)
                                {
                                    if (regex3.IsMatch(GridView1.Rows[0].Cells[i].Text) == true)
                                    {
                                        temp = regex3.Match(GridView1.Rows[0].Cells[i].Text).Value;
                                        count_year++;
                                        y2 = i;
                                    }
                                }


                                string[] arr = new string[col_no + 1];
                                if (count_year == 2)
                                {
                                    for (int i = 3; i <= col_no; i++)
                                    {
                                        if (regex3.IsMatch(GridView1.Rows[0].Cells[i].Text) == true)
                                        {
                                            string temp1 = regex3.Match(GridView1.Rows[0].Cells[i].Text).Value;
                                            y1 = i;

                                            for (int i1 = 3; i1 <= y1; i1++) arr[i1] = temp1;
                                            break;
                                        }
                                    }

                                    for (int j1 = y1 + 1; j1 <= col_no; j1++)
                                    {
                                        arr[j1] = temp;
                                    }

                                }
                                string myear = string.Empty;

                                for (int i = 3; i <= col_no; i++)
                                {
                                    string h = GridView1.Rows[0].Cells[i].Text;
                                    if (count_year == 0)
                                    {
                                        myear = DateTime.Now.Year.ToString();
                                        Guessed_Year.Visible = true;

                                    }
                                    else if (count_year == 1) myear = temp;

                                    else if (count_year == 2) myear = arr[i];

                                    else myear = regex3.Match(GridView1.Rows[0].Cells[i].Text).Value;

                                    if (myear == string.Empty && i - 1 >= 3) myear = regex3.Match(GridView1.Rows[0].Cells[i - 1].Text).Value;

                                    Match m1 = regex2.Match(h);
                                    var m = regex1.Match(h);
                                    var m11 = regex11.Match(h);
                                    string actual = "", num = "", num1 = "", actual1 = "", start_date = "";
                                    if (!string.IsNullOrEmpty(m.Groups[0].Value.ToString()) || !string.IsNullOrEmpty(m11.Groups[0].Value.ToString()))
                                    {
                                        string date_actual = m.Value;
                                        if (m.Value == string.Empty)
                                        {
                                            date_actual = m11.Value;
                                        }
                                        num = date_actual + m1.Value + myear;
                                        num1 = date_actual + "-" + m1.Value + "-" + myear;
                                        DateTime date = Convert.ToDateTime(num);
                                        if (date.DayOfWeek == DayOfWeek.Sunday) { }
                                        else
                                        {
                                            int inter = (int)date.DayOfWeek;
                                            date = date.Subtract(TimeSpan.FromDays(inter));
                                        }
                                        start_date = date.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                                        if (i == col_no - 2) date1 = start_date;
                                        actual = date.AddDays(6).ToString("ddMMMyyyy", CultureInfo.CreateSpecificCulture("en-US"));
                                        actual1 = date.AddDays(6).ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));

                                    }
                                    if (i == 3)
                                    {
                                        date2 = actual1;

                                    }
                                    GridView1.Rows[1].Cells[i].Text = start_date + "  to  " + actual1;

                                }

                                label_weeks.Text = date1 + " to " + date2;



                                int im = 0;

                                for (im = 0; im < GridView2.Rows.Count; im++)
                                {
                                    if (Regex.IsMatch(GridView2.Rows[im].Cells[0].Text, "Red"))
                                    {
                                        GridView2.Rows[im].ForeColor = System.Drawing.Color.Red;
                                        red = im;

                                        GridView2.Rows[im + 1].Visible = false;
                                        GridView2.Rows[im + 2].Visible = false;

                                    }
                                    else
                                        if (Regex.IsMatch(GridView2.Rows[im].Cells[0].Text, "Green"))
                                        {
                                            GridView2.Rows[im].ForeColor = System.Drawing.Color.Green;
                                            green = im;
                                            GridView2.Rows[im + 1].Visible = false;
                                            GridView2.Rows[im + 2].Visible = false;
                                        }
                                }


                                if (GridView2.Rows[green + 3].Cells[0].Text == "" || GridView2.Rows[green + 3].Cells[0].Text.Trim() == string.Empty || GridView2.Rows[green + 3].Cells[0].Text == "&nbsp;" || GridView2.Rows[green + 3].Cells[0].Text == string.Empty || string.IsNullOrEmpty(GridView2.Rows[green + 3].Cells[0].Text) || string.IsNullOrWhiteSpace(GridView2.Rows[green + 3].Cells[0].Text))
                                    GridView2.Rows[green + 3].Cells[0].Text = "No activities reported this week.";
                                else
                                {
                                    for (int r = green + 3; r < red; r++)
                                        if (GridView2.Rows[r].Cells[0].Text == "" || GridView2.Rows[r].Cells[0].Text.Trim() == string.Empty || GridView2.Rows[r].Cells[0].Text == "&nbsp;" || GridView2.Rows[r].Cells[0].Text == string.Empty || string.IsNullOrEmpty(GridView2.Rows[r].Cells[0].Text) || string.IsNullOrWhiteSpace(GridView2.Rows[r].Cells[0].Text)) { }
                                        else
                                            GridView2.Rows[r].Cells[0].Text = "-" + GridView2.Rows[r].Cells[0].Text;
                                }

                                List<String> lt_red = new List<string>();
                                List<String> lt_yellow = new List<string>();
                                for (int d = red + 3; d < GridView2.Rows.Count; d++)
                                {
                                    if ((GridView2.Rows[d].Cells[1].Text.ToLower() == "high" || GridView2.Rows[d].Cells[1].Text.ToLower() == "blocker") && !(GridView2.Rows[d].Cells[0].Text == "" || GridView2.Rows[d].Cells[0].Text.Trim() == string.Empty || GridView2.Rows[d].Cells[0].Text == "&nbsp;" || GridView2.Rows[d].Cells[0].Text == string.Empty || string.IsNullOrEmpty(GridView2.Rows[d].Cells[0].Text) || string.IsNullOrWhiteSpace(GridView2.Rows[d].Cells[0].Text)))
                                    {
                                        count_red++;
                                        lt_red.Add("-" + GridView2.Rows[d].Cells[0].Text);
                                    }
                                    else if ((GridView2.Rows[d].Cells[1].Text.ToLower() == "low" || GridView2.Rows[d].Cells[1].Text.ToLower() == "medium") && !(GridView2.Rows[d].Cells[0].Text == "" || GridView2.Rows[d].Cells[0].Text.Trim() == string.Empty || GridView2.Rows[d].Cells[0].Text == "&nbsp;" || GridView2.Rows[d].Cells[0].Text == string.Empty || string.IsNullOrEmpty(GridView2.Rows[d].Cells[0].Text) || string.IsNullOrWhiteSpace(GridView2.Rows[d].Cells[0].Text)))
                                    {
                                        count_yellow++;
                                        lt_yellow.Add("-" + GridView2.Rows[d].Cells[0].Text);
                                    }
                                }

                                if (count_red == 0)
                                {
                                    GridView2.Rows[red + 3].Cells[0].Text = "No major issues in this cycle.";
                                    GridView2.Rows[red + count_red + 5].Cells[0].Text = "Yellow (Minor issues)";
                                    GridView2.Rows[red + count_red + 5].Cells[0].ForeColor = System.Drawing.Color.Yellow;
                                    yellow = red + count_red + 5;
                                    if (count_yellow == 0) GridView2.Rows[yellow + 1].Cells[0].Text = "No minor issues in this cycle.";
                                    else
                                    {
                                        for (int z = 0; z < count_yellow; z++) GridView2.Rows[yellow + z + 1].Cells[0].Text = lt_yellow[z];
                                    }
                                }
                                else
                                {
                                    int zi = 0;
                                    for (zi = 0; zi < count_red; zi++) GridView2.Rows[red + 3 + zi].Cells[0].Text = lt_red[zi];
                                    GridView2.Rows[red + count_red + 4].Cells[0].Text = "Yellow (Minor issues)";
                                    GridView2.Rows[red + count_red + 4].Cells[0].ForeColor = System.Drawing.Color.Yellow;
                                    yellow = red + count_red + 4;
                                    if (count_yellow == 0) GridView2.Rows[yellow + 1].Cells[0].Text = "No minor issues in this cycle.";
                                    else
                                    {
                                        for (int z = 0; z < count_yellow; z++) GridView2.Rows[yellow + z + 1].Cells[0].Text = lt_yellow[z];
                                    }
                                }



                                for (int k = 0; k < GridView1.Rows.Count; k++)
                                {
                                    if (Regex.IsMatch(GridView1.Rows[k].Cells[0].Text, "TOTAL", RegexOptions.IgnoreCase))
                                    {
                                        GridView1.Rows[k].BackColor = System.Drawing.Color.Salmon;
                                        GridView1.Rows[k].Font.Bold = true;
                                        extra = k;
                                        break;
                                    }
                                }

                                for (int k = 0; k < GridView1.Rows.Count; k++)
                                {
                                    if (Regex.IsMatch(GridView1.Rows[k].Cells[0].Text, "Effort", RegexOptions.IgnoreCase))
                                    {
                                        GridView1.Rows[k].BackColor = ColorTranslator.FromHtml("#DFFFA5");
                                        GridView1.Rows[k].Font.Bold = true;

                                    }
                                }

                                for (int a = 4; a < extra; a++)
                                {
                                    for (int c = 0; c < GridView1.Rows[a].Cells.Count; c++) { if (GridView1.Rows[a].Cells[c].Text == "N.A" || GridView1.Rows[a].Cells[c].Text == "&nbsp;") GridView1.Rows[a].Cells[c].Text = "0"; }
                                }

                                for (int a = 1; a < col_no; a += 2)
                                {
                                    GridView1.Rows[0].Cells[a + 1].Visible = false;
                                    GridView1.Rows[0].Cells[a].Attributes.Add("colspan", "2");
                                    GridView1.Rows[1].Cells[a + 1].Visible = false;
                                    GridView1.Rows[1].Cells[a].Attributes.Add("colspan", "2");
                                    for (int y = 1; y < 6; y++)
                                    {
                                        GridView1.Rows[extra + y].Cells[a + 1].Visible = false;
                                        GridView1.Rows[extra + y].Cells[a].Attributes.Add("colspan", "2");
                                    }
                                }

                                for (int a = 0; a < GridView2.Rows.Count; a++)
                                {
                                    int count = 0;
                                    for (int c = 0; c < GridView2.Rows[a].Cells.Count; c++)
                                    {

                                        if (GridView2.Rows[a].Cells[c].Text == "" || GridView2.Rows[a].Cells[c].Text.Trim() == string.Empty || GridView2.Rows[a].Cells[c].Text == "&nbsp;" || GridView2.Rows[a].Cells[c].Text == string.Empty || string.IsNullOrEmpty(GridView2.Rows[a].Cells[c].Text) || string.IsNullOrWhiteSpace(GridView2.Rows[a].Cells[c].Text) || GridView2.Rows[a].Cells[c].Text == "-")
                                        {
                                            count++;
                                        }
                                    }
                                    if (count == GridView2.Rows[a].Cells.Count) GridView2.Rows[a].Visible = false;
                                }

                                GridView2.Rows[red - 1].Visible = true;

                                for (int m = 0; m < GridView2.Rows.Count; m++)
                                {
                                    for (int n = 1; n < GridView2.Rows[0].Cells.Count; n++)
                                        GridView2.Rows[m].Cells[n].Visible = false;
                                }
                                Label1.Visible = true;
                                FileUpload1.Visible = true;
                                Upload_button.Visible = true;
                                Menu1.Visible = true;
                                MultiView1.Visible = true;
                                Label2.Visible = true;
                                Label3.Visible = true;
                                label_filename.Visible = true;
                                label_weeks.Visible = true;
                                Submit.Visible = true;
                            }
                        }
                        catch (Exception ed)
                        {
                            Validate.ForeColor = System.Drawing.Color.Red;
                            Validate.Text = "Invalid File OR Invalid Sheet Name. Please check. ";

                        }
                    }
                }
            }

        }



        #region Button Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Submit_Click(object sender, EventArgs e)
        {
            hider.Attributes.Add("style", "display:block;");
            popup_box.Attributes.Add("style", "display:block;");


            DataSet ds = new DataSet();

            DropDownList3.Enabled = false;
            DropDownList4.Enabled = false;

            ds = DAO.DAObjects.GetVendorDetails(use);
            DropDownList1.Items.Add(new ListItem("--Select--"));
            foreach (DataRow dr in ds.Tables[0].Rows)
                DropDownList1.Items.Add(new ListItem(dr["Vendor"].ToString().Trim()));


            ds = DAO.DAObjects.GetProducts(pr);
            DropDownList2.Items.Add(new ListItem("--Select--"));
            foreach (DataRow dr in ds.Tables[0].Rows)
                DropDownList2.Items.Add(new ListItem(dr["Product"].ToString().Trim()));

        }

        #endregion

        #region MenuClick Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiView1.ActiveViewIndex = index;
            Menu1.Visible = true;
            MultiView1.Visible = true;
        }

        #endregion

        protected void Submit_WSR_Click(object sender, EventArgs e)
        {


            DTO.WSRData wsrData = new DTO.WSRData();
            wsrData.DataHeader = new DTO.Header();
            wsrData.DataHeader.LoginID = "ADMIN";



            DTO.WSRData wsrParametersData = new DTO.WSRData();
            DataSet ds = new DataSet();


            wsrData.NewDeliverables = "";
            DataSet dsWeeks = Adobe.Localization.Insights.DataLayer.common.ExecuteMySQLQuery(Query.QueryDAO.GetWeekInfo(""));
            DateTime weekid = Convert.ToDateTime(GridView1.Rows[1].Cells[3].Text.Substring(0, 11));
            string latest_week = "";
            for (int i = 0; i < dsWeeks.Tables[0].Rows.Count; i++)
            {
                if (DateTime.Compare(Convert.ToDateTime(dsWeeks.Tables[0].Rows[i][1].ToString()), weekid) == 0)
                {
                    latest_week = dsWeeks.Tables[0].Rows[i][0].ToString();
                    break;
                }
            }

            string phase = DropDownList4.SelectedItem.Text;
            ds = DAO.DAObjects.GetProjectPhases(pp);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][2].ToString() == phase)
                {
                    wsrData.ProjectPhaseID = ds.Tables[0].Rows[i][1].ToString();
                    break;
                }
            }

            string vendor = DropDownList1.SelectedItem.Text;
            ds = DAO.DAObjects.GetVendorDetails(use);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][1].ToString() == vendor)
                {
                    wsrData.VendorID = ds.Tables[0].Rows[i][0].ToString();
                    break;
                }
            }
            int bug_regressed_index = 0;
            for (int it = 24; it <= GridView1.Rows.Count; it++)
                if (GridView1.Rows[it].Cells[0].Text.Contains("Bug IDs of regressed bugs"))
                {
                    bug_regressed_index = it;
                    break;
                }
            wsrData.DataHeader = new DTO.Header();
            wsrData.DataHeader.LoginID = "ADMIN";

            DataSet para = DAO.DAObjects.GetProductWSRParameters(wsrParametersData);

            int wkCount = -1;
            int wkDataRow = -1;
            int paramCount = -1;

            for (wkCount = 3; wkCount < col_no; wkCount += 2)
            {
                DateTime week_id = Convert.ToDateTime(GridView1.Rows[1].Cells[wkCount].Text.Substring(0, 11));

                for (wkDataRow = 0; wkDataRow < dsWeeks.Tables[0].Rows.Count; wkDataRow++)
                    if (DateTime.Compare(Convert.ToDateTime(dsWeeks.Tables[0].Rows[wkDataRow][1].ToString()), week_id) == 0)
                    {
                        wsrData.WeekID = dsWeeks.Tables[0].Rows[wkDataRow][0].ToString();
                        wsrData.VendorEffortsCollection = new ArrayList();

                        DataTable dtWSRData = Adobe.Localization.Insights.DataLayer.common.ExecuteMySQLQuery(Query.QueryDAO.GetWSRData(wsrData)).Tables[0];
                        if (dtWSRData.Rows.Count == 0)
                        {
                            if (wsrData.WeekID == latest_week)
                            {
                                StringBuilder strRedissues = new StringBuilder();
                                wsrData.RedIssues = string.Empty;
                                for (int d = red + 3; d < yellow - 1; d++)
                                    strRedissues.AppendLine(Regex.Replace(GridView2.Rows[d].Cells[0].Text, "-", ""));
                                if (strRedissues.ToString() == "No major issues in this cycle.\r\n")
                                    wsrData.RedIssues = "";
                                else wsrData.RedIssues = strRedissues.ToString();

                                StringBuilder strGreen = new StringBuilder();
                                wsrData.GreenAccom = string.Empty;
                                for (int d = green + 3; d < red - 1; d++)
                                    strGreen.AppendLine(Regex.Replace(GridView2.Rows[d].Cells[0].Text, "-", ""));
                                if (strGreen.ToString() == "No activities reported this week.\r\n")
                                    wsrData.GreenAccom = "";
                                else wsrData.GreenAccom = strGreen.ToString();

                                StringBuilder strYellowissues = new StringBuilder();
                                wsrData.YellowIssues = string.Empty;
                                for (int d = yellow + 1; d <= yellow + 1 + count_yellow; d++)
                                    strYellowissues.AppendLine(Regex.Replace(GridView2.Rows[d].Cells[0].Text, "-", ""));
                                if (strYellowissues.ToString() == "No minor issues in this cycle.\r\n")
                                    wsrData.YellowIssues = "";
                                else wsrData.YellowIssues = strYellowissues.ToString();

                            }
                            else
                            {
                                wsrData.GreenAccom = string.Empty;
                                wsrData.RedIssues = string.Empty;
                                wsrData.YellowIssues = string.Empty;
                            }

                            Adobe.Localization.Insights.DataLayer.common.ExecuteMySQLQuery(Query.QueryDAO.AddWSRData(wsrData));
                            wsrData.WsrDataID = Adobe.Localization.Insights.DataLayer.common.ExecuteMySQLQuery(Query.QueryDAO.GetWSRData(wsrData)).Tables[0].Rows[0][0].ToString();
                            int application_index = 0;

                            decimal app_efforts = 0, app_quantity = 0;
                            for (paramCount = 4; paramCount <= 23; paramCount++)
                                foreach (DataRow drWSRParam in para.Tables[0].Rows)
                                {

                                    if (GridView1.Rows[paramCount].Cells[0].Text.Trim().ToLower() == drWSRParam[1].ToString().Trim().ToLower() && Regex.IsMatch(GridView1.Rows[paramCount].Cells[0].Text, @"\b(Application)\b") == false)
                                    {
                                        DTO.Efforts effort = new DTO.Efforts();
                                        effort.WSRParameterID = drWSRParam[0].ToString();
                                        effort.Quantity = GridView1.Rows[paramCount].Cells[wkCount].Text == "" || GridView1.Rows[paramCount].Cells[wkCount].Text == "&nbsp;" ? "0" : GridView1.Rows[paramCount].Cells[wkCount].Text;
                                        effort.Effort = GridView1.Rows[paramCount].Cells[wkCount + 1].Text == "" || GridView1.Rows[paramCount].Cells[wkCount + 1].Text == "&nbsp;" ? "0" : GridView1.Rows[paramCount].Cells[wkCount + 1].Text;
                                        effort.Remarks = GridView1.Rows[bug_regressed_index].Cells[wkCount].Text;
                                        wsrData.VendorEffortsCollection.Add(effort);
                                        break;

                                    }
                                    else if (Regex.IsMatch(GridView1.Rows[paramCount].Cells[0].Text, @"\b(Application)\b"))
                                    {
                                        application_index = paramCount;
                                        DTO.Efforts effort = new DTO.Efforts();
                                        effort.WSRParameterID = "1";
                                        effort.Quantity = (Convert.ToDecimal(GridView1.Rows[paramCount].Cells[wkCount].Text) + app_quantity).ToString();
                                        effort.Effort = (Convert.ToDecimal(GridView1.Rows[paramCount].Cells[wkCount + 1].Text) + app_efforts).ToString();
                                        effort.Remarks = GridView1.Rows[bug_regressed_index].Cells[wkCount].Text;
                                        wsrData.VendorEffortsCollection.Add(effort);
                                        break;

                                    }
                                    else if (Regex.IsMatch(GridView1.Rows[paramCount].Cells[0].Text, @"\b(Installer|Workflows)\b"))
                                    {
                                        app_efforts += Convert.ToDecimal(GridView1.Rows[paramCount].Cells[wkCount + 1].Text);
                                        app_quantity += Convert.ToDecimal(GridView1.Rows[paramCount].Cells[wkCount].Text);
                                    }
                                    else if (Regex.IsMatch(GridView1.Rows[paramCount].Cells[0].Text, @"\b(Weekly)\b"))
                                    {
                                        DTO.Efforts effort = new DTO.Efforts();
                                        effort.WSRParameterID = "8";
                                        effort.Quantity = GridView1.Rows[paramCount].Cells[wkCount].Text == "" || GridView1.Rows[paramCount].Cells[wkCount].Text == "&nbsp;" ? "0" : GridView1.Rows[paramCount].Cells[wkCount].Text;
                                        effort.Effort = GridView1.Rows[paramCount].Cells[wkCount + 1].Text == "" || GridView1.Rows[paramCount].Cells[wkCount + 1].Text == "&nbsp;" ? "0" : GridView1.Rows[paramCount].Cells[wkCount + 1].Text;
                                        effort.Remarks = GridView1.Rows[bug_regressed_index].Cells[wkCount].Text;
                                        wsrData.VendorEffortsCollection.Add(effort);
                                        break;
                                    }

                                    else if (Regex.IsMatch(GridView1.Rows[paramCount].Cells[0].Text, @"\b(ScreenShooting)\b"))
                                    {
                                        DTO.Efforts effort = new DTO.Efforts();
                                        effort.WSRParameterID = "11";
                                        effort.Quantity = GridView1.Rows[paramCount].Cells[wkCount].Text == "" || GridView1.Rows[paramCount].Cells[wkCount].Text == "&nbsp;" ? "0" : GridView1.Rows[paramCount].Cells[wkCount].Text;
                                        effort.Effort = GridView1.Rows[paramCount].Cells[wkCount + 1].Text == "" || GridView1.Rows[paramCount].Cells[wkCount + 1].Text == "&nbsp;" ? "0" : GridView1.Rows[paramCount].Cells[wkCount + 1].Text;
                                        effort.Remarks = GridView1.Rows[bug_regressed_index].Cells[wkCount].Text;
                                        wsrData.VendorEffortsCollection.Add(effort);
                                        break;
                                    }

                                }

                            Adobe.Localization.Insights.DataLayer.common.ExecuteMySQLQuery(Query.QueryDAO.AddUpdateWSRDetails(wsrData));
                        }
                    }
            }
            popup_box.Attributes.Add("style", "display:none;");
            popup_box_success_upload.Attributes.Add("style", "display:block;");


        }

        protected void Success_Upload(object sender, EventArgs e)
        {
            hider.Attributes.Add("style", "display:none;");
            popup_box.Attributes.Add("style", "display:none;");
            popup_box_success_upload.Attributes.Add("style", "display:none;");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            hider.Attributes.Add("style", "display:none;");
            popup_box.Attributes.Add("style", "display:none;");

        }



        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Enabled = true;
            DropDownList3.Items.Clear();
            DataSet ds = DAO.DAObjects.GetProducts(new DTO.Product());

            DropDownList3.Items.Insert(0, new ListItem("--Select--"));
            string product = DropDownList2.SelectedItem.Text;
            int selected_index = DropDownList2.SelectedIndex;
            string productID = ds.Tables[0].Rows[selected_index - 1][0].ToString();

            ds = DAO.DAObjects.GetProductVersion(new DTO.Product());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][1].ToString() == productID)
                    DropDownList3.Items.Add(ds.Tables[0].Rows[i][3].ToString());
            }


        }
        string versionID = "";
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList4.Enabled = true;
            DropDownList4.Items.Clear();


            DataSet ds = new DataSet();
            DTO.Product pr = new DTO.Product();

            ds = DAO.DAObjects.GetProductVersion(pr);

            DropDownList4.Items.Insert(0, new ListItem("--Select--"));
            string pro_version = DropDownList3.SelectedItem.Text;


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][3].ToString() == pro_version)
                {
                    versionID = ds.Tables[0].Rows[i][0].ToString();

                    break;
                }
            }

            DTO.ProjectPhases pp = new DTO.ProjectPhases();


            DataSet ds1 = new DataSet();
            ds1 = DAO.DAObjects.GetProjectPhases(pp);
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                if (ds1.Tables[0].Rows[i][0].ToString() == versionID)
                    DropDownList4.Items.Add(ds1.Tables[0].Rows[i][2].ToString());
            }

        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //DropDownList4.Enabled = true;
            //DropDownList4.Items.Clear();


        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //DropDownList4.Enabled = true;
            //DropDownList4.Items.Clear();
            //DTO.ProjectPhases pp = new DTO.ProjectPhases();

            //DataSet ds1 = new DataSet();
            //ds1 = DAO.DAObjects.GetProjectPhases(pp);
            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //{
            //    if (ds1.Tables[0].Rows[i][0].ToString() == versionID && ds1.Tables[0].Rows[i][5].ToString() == "2")
            //        DropDownList4.Items.Add(ds1.Tables[0].Rows[i][2].ToString());
            //}

        }
    }
}