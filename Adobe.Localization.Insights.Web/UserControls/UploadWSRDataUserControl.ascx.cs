using System;
using System.IO;
using System.Collections;
using System.Data;
using BO = Adobe.Localization.Insights.BusinessLayer;
using System.Windows.Forms;
using System.Threading;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    public partial class UploadWSRDataUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable company;
        private DTO.WSRData wsrData;

        int countItems = 0;
        #endregion

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            getDataIntoDataTableFromObjLibrary(@"C:\Documents and Settings\sauragup\Desktop\Weekly Status Report.doc");
            //getDataIntoDataTableFromHTML(@"C:\Documents and Settings\sauragup\Desktop\Weekly Status Report.doc");
            //company = getDataIntoDataTableFromDoc(@"C:\Documents and Settings\sauragup\Desktop\Weekly Status Report.doc");
            //company = getDataIntoDataTableFromDoc(@"C:\Documents and Settings\149115\Desktop\Copy of India Commercial File.doc");
            //BO.InsightsBO.BulkTableInsert(company, "Company", Session["UserID"].ToString());
        }

        private void getDataIntoDataTableFromObjLibrary(string path)
        {
            //Word.ApplicationClass wordApp = new Word.ApplicationClass();
            //object file = path;
            //object nullobj = System.Reflection.Missing.Value;
            //Word.Document doc = wordApp.Documents.Open(
            //ref file, ref nullobj, ref nullobj,
            //                                      ref nullobj, ref nullobj, ref nullobj,
            //                                      ref nullobj, ref nullobj, ref nullobj,
            //                                      ref nullobj, ref nullobj, ref nullobj);
            //doc.ActiveWindow.Selection.WholeStory();
            //doc.ActiveWindow.Selection.Copy();
            //doc.Close();
            ////IDataObject idat = null;
            ////Exception threadEx = null;
            //Thread staThread = new Thread(_thGetText);
            //staThread.SetApartmentState(ApartmentState.STA);
            //staThread.Start();
            //staThread.Join();
            //IDataObject data = Clipboard.GetDataObject();
            //string Text = idat.GetData(DataFormats.Text).ToString();

            //            string folder_to_save_in = @"c:\temp\documents\";

            //          string filePath = folder_to_save_in + FileUpload1.FileName;

            // This bit does the actual file upload:

            //        FileUpload1.SaveAs(filePath);
        }

        private void _thGetText()
        {
            IDataObject data = Clipboard.GetDataObject();
            string text = data.GetData(DataFormats.Text).ToString();
            getDataIntoDataTableFromString(text);
        }

        /*
private void getDataIntoDataTableFromHTML(string filePath)
{
    Word.ApplicationClass wordApplication = new Word.ApplicationClass();
    // Opening a Word doc requires many parameters, but we leave most of them blank...

    object o_nullobject = System.Reflection.Missing.Value;
    object o_filePath = filePath;

    Word.Document doc = wordApplication.Documents.Open(ref o_filePath, 
    ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject,
    ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject,
    ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject);
    // Here we save it in html format...

    // This assumes it was called "something.doc"

    string newfilename = filePath.Replace(".doc", ".html");
    object o_newfilename = newfilename;
    object o_format = Word.WdSaveFormat.wdFormatHTML;
    object o_encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;
    object o_endings = Word.WdLineEndingType.wdCRLF;

     wordApplication.ActiveDocument.SaveAs(ref o_newfilename, ref o_format, ref o_nullobject,
    ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject,
    ref o_nullobject, ref o_nullobject, ref o_encoding, ref o_nullobject,
    ref o_nullobject, ref o_endings, ref o_nullobject);

    // Finally, close original...
    doc.Close(ref o_nullobject, ref o_nullobject, ref o_nullobject);
}

       */

        private void getDataIntoDataTableFromString(string text)
        {
            text = text.Replace("\"\t", "<Bullets>");
            text = text.Replace("\t", "<TableColumn>");
            text = text.Replace("\r\n", "<TableRow>");

            text = text.Replace("Issues and Accomplishments", "<NewSection>Issues and Accomplishments");
            text = text.Replace("Red (Urgent help needed from management team)", "<DataSection>Red (Urgent help needed from management team)");
            text = text.Replace("Yellow (Issues)", "<DataSection>Yellow (Issues)");
            text = text.Replace("Green (Key accomplishments, progress, acknowledgments)", "<DataSection>Green (Key accomplishments, progress, acknowledgments)");

            text = text.Replace("Next Week Priorities", "<NewSection>Next Week Priorities");
            text = text.Replace("Blue (Outstanding deliverables)", "<DataSection>Blue (Outstanding deliverables)");
            text = text.Replace("Black (New deliverables)", "<DataSection>Black (New deliverables)");

            text = text.Replace("Test Metrics", "<NewSection>Test Metrics");
            text = text.Replace("Test Case Metrics", "<DataSection>Test Case Metrics");
            text = text.Replace("Bug Metrics", "<DataSection>Bug Metrics");

            text = text.Replace("QA Testing Efforts Spent", "<NewSection>QA Testing Efforts Spent");

            // countItems = 

            string[] separator = new string[] { "<TableRow>" };

            string[] wsrData = text.Split(new string[] { separator[0] }, StringSplitOptions.None);

            ParseData(wsrData);

            //foreach (string data in wsrData)
            //{
            //    Console.WriteLine(data);
            //}
        }

        private void ParseData(string[] wsrArrayData)
        {
            wsrData = new DTO.WSRData();

            for (int counter = 0; counter < wsrArrayData.Length; counter++)
            {
                string data = wsrArrayData[0];

                #region "Issues and Accomplishments"

                if (data.Contains("<NewSection>Issues and Accomplishments"))
                {
                    counter++;
                    if (data.Contains("<DataSection>Red"))
                    {
                        for (; counter < wsrArrayData.Length; counter++)
                        {
                            if (data.Contains("Issue/s"))
                            {
                                break;
                            }
                        }
                    }
                }

                #endregion
            }

        }

        /// <summary>
        /// getDataIntoDataTableFromDoc
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private DataTable getDataIntoDataTableFromDoc(string filePath)
        {
            #region Commented
            //if (DataFileUpload.PostedFile != null)
            //{
            //    // Get a reference to PostedFile object 
            //    System.Web.HttpPostedFile myFile = DataFileUpload.PostedFile;

            //    // Get size of uploaded file 
            //    int nFileLen = myFile.ContentLength;

            //    // make sure the size of the file is > 0 
            //    if (nFileLen > 0)
            //    {
            //        // Allocate a buffer for reading of the file 
            //        byte[] myData = new byte[nFileLen - 1];

            //        // Read uploaded file from the Stream 
            //        myFile.InputStream.Read(myData, 0, nFileLen-1);

            //        MemoryStream s = new MemoryStream(myData);

            //        Stream stream = s;
            //        string Input;

            //        StreamReader Input_S = new StreamReader(stream);

            //        while (true)
            //        {
            //            Input = Input_S.ReadLine();
            //            if (Input == null)
            //            {
            //                break;
            //            }
            //        }
            //    }
            //}
            #endregion

            //DataTable companyDetails = new DataTable();
            //object nullobj = System.Reflection.Missing.Value;
            //object file = filePath;
            ////object Format = (int)Word.;
            //Word.ApplicationClass wordApp = new Word.ApplicationClass();

            //// here on Document.Open there should be 9 arg.
            //Word.Document doc = wordApp.Documents.Open(ref file,
            //ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj,
            //ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj,
            //ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj);

            ////// Here the word content is copeied into a string which helps to store it into  textbox.

            //Word.Document doc1 = wordApp.ActiveDocument;

            try
            {
                //FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                //StreamReader sReader = new StreamReader(fStream);
                //filePath = sReader.ReadToEnd();
                //sReader.Close();

                //string m_Content = doc1.Content.Text;
                //Word.Rows row = doc1.Content.Tables[0].Rows;
                //    ArrayList splitters = new ArrayList();

                //    splitters.Add(",");
                //    splitters.Add("Estd.:");
                //    splitters.Add("Telephone:");
                //    splitters.Add("Mobile:");
                //    splitters.Add("Fax.:");
                //    splitters.Add("E-mail:");
                //    splitters.Add("Website:");
                //    splitters.Add("Executive:");
                //    splitters.Add("Activity:");

                //    companyDetails.Columns.Add("CompanyName");
                //    companyDetails.Columns.Add("Address");
                //    companyDetails.Columns.Add("Estd");
                //    companyDetails.Columns.Add("Telephone");
                //    companyDetails.Columns.Add("Mobile");
                //    companyDetails.Columns.Add("Fax");
                //    companyDetails.Columns.Add("E-mail");
                //    companyDetails.Columns.Add("Website");
                //    companyDetails.Columns.Add("Executive");
                //    companyDetails.Columns.Add("Activity");
                //    companyDetails.Columns.Add("Others");

                //    string[] companies = m_Content.Split(Environment.NewLine.ToCharArray());
                //    foreach (string company in companies)
                //    {
                //        if (company.Length > 0)
                //        {
                //            string tempCompany = company;
                //            DataRow dr = companyDetails.NewRow();

                //            int i;
                //            string[] separator = new string[] { "&nbsp;" };
                //            for (i = 1; i < splitters.Count; i++)
                //            {
                //                tempCompany = tempCompany.Replace(splitters[i].ToString(), separator[0].ToString() + splitters[i].ToString());
                //            }

                //            string[] companyData = tempCompany.Split(separator, StringSplitOptions.None);

                //            i = 0;
                //            foreach (string data in companyData)
                //            {
                //                if (i == 0)
                //                {
                //                    dr[i] = data.Split(new string[] { splitters[0].ToString() }, StringSplitOptions.None)[0].Trim().Trim(new char[] { '.' }).Trim();
                //                    dr[i + 1] = data.Substring(dr[i].ToString().Length + splitters[i].ToString().Length).Trim().Trim(new char[] { '.' }).Trim();
                //                    i += 1;
                //                    continue;
                //                }
                //                else
                //                {
                //                    if (i == splitters.Count)
                //                    {
                //                        dr[i] = dr[i] + data;
                //                    }
                //                    while (i < splitters.Count)
                //                    {
                //                        if ((bool)data.StartsWith(splitters[i].ToString()))
                //                        {
                //                            dr[i + 1] = data.Replace(splitters[i].ToString(), "").Trim().Trim(new char[] { '.' }).Trim();
                //                            i++;
                //                            break;
                //                        }
                //                        i++;
                //                    }
                //                }
                //            }
                //            companyDetails.Rows.Add(dr);
                //        }
                //    }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            //finally
            //{
            //    doc1.Close(ref nullobj, ref nullobj, ref nullobj);
            //}
            return null;
        }
    }
}