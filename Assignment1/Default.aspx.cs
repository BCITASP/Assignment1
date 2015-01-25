using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Hosting;

namespace Assignment1
{
    public partial class Default : System.Web.UI.Page
    {
        SearchProperties searchresult;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && ViewState["searchresult"] != null)
            {
                searchresult = (SearchProperties)ViewState["searchresult"];
                SetResultsViewingArea(true);
                
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack && searchresult != null)
            {
                BindDataToControls(searchresult);
            }
        }

        #region Event Handlers
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchFiles search = new SearchFiles(txtSearchTerm.Text);
            ArrayList filesList = search.PerformSearch();
            if (filesList.Count > 0)
            {
                SearchProperties searchresult = SaveResult(filesList);
                SetResultsViewingArea(true);
                BindDataToControls(searchresult);
            }
            else
            {
                SetResultsViewingArea(false);
            }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            string filepath = searchresult.filesList[searchresult.currentFileIndex];
            string fileName = Path.GetFileName(filepath);
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
            response.TransmitFile(filepath);
            response.Flush();
            response.End();
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            searchresult.currentFileIndex = 0;
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            searchresult.currentFileIndex -= 1;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            searchresult.currentFileIndex += 1;
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            searchresult.currentFileIndex = searchresult.lastFileIndex;
        }
        #endregion

        private void BindDataToControls(SearchProperties searchresult)
        {
            txtFileName.Text = Path.GetFileName(searchresult.filesList[searchresult.currentFileIndex]);
            lblResultCount.Text = String.Format("File {0} of {1}", searchresult.currentFileIndex + 1, searchresult.numberOfFiles);
            txtFileContent.Text = File.ReadAllText(searchresult.filesList[searchresult.currentFileIndex]);
            SetNavigationButtons(searchresult);
        }

        private void SetNavigationButtons(SearchProperties searchresult)
        {
            // First (of all files) button
            if (searchresult.currentFileIndex == 0 || searchresult.numberOfFiles == 0)
            {
                btnFirst.Enabled = false;
            }
            else
            {
                btnFirst.Enabled = true;
            }
            // Previous button
            if (searchresult.currentFileIndex == 0 || searchresult.numberOfFiles == 0)
            {
                btnPrevious.Enabled = false;
            }
            else
            {
                btnPrevious.Enabled = true;
            }
            // Next button
            if (searchresult.currentFileIndex < searchresult.lastFileIndex)
            {
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
            }
            // Last (of all files) button
            if (searchresult.currentFileIndex == searchresult.lastFileIndex)
            {
                btnLast.Enabled = false;
            }
            else
            {
                btnLast.Enabled = true;
            }
        }

        

        private void SetResultsViewingArea(bool setResultsViewable)
        {
            if(setResultsViewable)
            {
                pnlResult.Visible = true;
                lblNotFound.Visible = false;
            }
            else
            {
                pnlResult.Visible = false;
                lblNotFound.Visible = true;
            }
        }

        private SearchProperties SaveResult(ArrayList filesList)
        {
            string[] filesArray = (string[])filesList.ToArray(typeof(string));
            SearchProperties search = new SearchProperties(filesArray, filesList.Count);
            ViewState["searchresult"] = search;
            return search;
        }
    }
}