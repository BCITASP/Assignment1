using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Assignment1
{
    public class SearchFiles
    {
        private string[] filesToSearch;
        private string searchTerms;
        public SearchFiles(string searchTerms)
        {
            filesToSearch = Directory.GetFiles(HostingEnvironment.MapPath("~/files"));
            this.searchTerms = searchTerms;
        }

        public ArrayList PerformSearch()
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
                foreach (string file in filesToSearch)
                {
                    string content = File.ReadAllText(file).ToLower();
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
            string[] excludedWordsArray = File.ReadAllText(HostingEnvironment.MapPath("~/exclusion/exclusion.txt")).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < searchTermsArray.Length; i++)
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
            return searchTerms.ToLower().Trim();
        }
    }
}