using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace Assignment1
{
    public partial class Default : System.Web.UI.Page
    {
        string[] searchfiles;
        int currentFileIndex;
        int lastFileIndex;
        int resultCount;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                currentFileIndex = 0;
                lastFileIndex = 0;
                resultCount = 0;
                searchfiles = Directory.GetFiles(Page.MapPath("~/files"));
                ViewState["searchfiles"] = searchfiles;
            } 
            else
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch(txtSearchTerm.Text);
        }

        private void PerformSearch(string searchTerms)
        {
            string cleanedSearchTerms = RemoveExcludedWords(searchTerms);
            ArrayList filenamesList = Search(cleanedSearchTerms);
        }

        private ArrayList Search(string cleanedSearchTerms)
        {
            ArrayList filenames = new ArrayList();
            string[] cleanedSearchTermsArray = cleanedSearchTerms.Split(' ');
            foreach(string file in (string[])ViewState["searchfiles"])
            {
                string content = File.ReadAllText(file);
                foreach(string word in cleanedSearchTermsArray)
                {
                    if (content.Contains(word))
                    {
                        filenames.Add(file);
                        break;
                    }
                }
            }
            return filenames;
        }

        private string RemoveExcludedWords(string searchTerms)
        {
            string[] excludedWordsArray = File.ReadAllText(MapPath("~/exclusion/exclusion.txt")).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in excludedWordsArray)
            {
                searchTerms.Replace(word, "");
            }
            return searchTerms;
        }
    }
}