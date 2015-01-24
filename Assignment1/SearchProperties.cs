using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment1
{
    [Serializable]
    public class SearchProperties 
    {
        public string[] filesList { set; get; }
        public int currentFileIndex { set; get; }
        public int lastFileIndex { set; get; }
        public int numberOfFiles { set; get; }

        public SearchProperties(string[] filesList, int numberOfFiles)
        {
            currentFileIndex = 0;
            lastFileIndex = filesList.Length - 1;
            this.numberOfFiles = numberOfFiles;
            this.filesList = filesList;
        }
    }
}