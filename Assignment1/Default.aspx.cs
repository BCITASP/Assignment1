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
        static string[] FILES_TO_SEARCH = Directory.GetFiles(HostingEnvironment.MapPath("~/files"));


        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ArrayList filesList = PerformSearch(txtSearchTerm.Text);
            if (filesList.Count > 0)
            {
                SaveResult(filesList);
                SetResultsViewingArea(true);   
            }
            else
            {
                SetResultsViewingArea(false);
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

        private void SaveResult(ArrayList filesList)
        {
            string[] filesArray = (string[])filesList.ToArray(typeof(string));
            SearchProperties search = new SearchProperties(filesArray, filesList.Count);
            ViewState["searchresult"] = search;
        }

        private ArrayList PerformSearch(string searchTerms)
        {
            string cleanedSearchTerms = RemoveExcludedWords(searchTerms);
            ArrayList filenamesList = Search(cleanedSearchTerms);
            return filenamesList;
        }

        private ArrayList Search(string cleanedSearchTerms)
        {
            ArrayList filenames = new ArrayList();
            if (cleanedSearchTerms.Length > 0)
            {
                string[] cleanedSearchTermsArray = cleanedSearchTerms.Split(' ');
                foreach (string file in FILES_TO_SEARCH)
                {
                    string content = File.ReadAllText(file);
                    foreach (string word in cleanedSearchTermsArray)
                    {
                        if (content.Contains(word))
                        {
                            filenames.Add(file);
                            break;
                        }
                    }
                }
            }            
            return filenames;
        }

        private string RemoveExcludedWords(string searchTerms)
        {
            string[] searchTermsArray = searchTerms.Split(' ');
            string[] excludedWordsArray = File.ReadAllText(MapPath("~/exclusion/exclusion.txt")).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < searchTermsArray.Length; i++ )
            {
                foreach (string word in excludedWordsArray)
                {
                    if (searchTermsArray[i].Equals(word))
                    {
                        searchTermsArray[i] = "";
                    }
                }
            }
            var qry = from s in searchTermsArray
                          where s.ToString().Length > 0
                          select s;
            searchTerms = String.Join(" ", qry.ToArray());
            return searchTerms.Trim();
        }
    }
}