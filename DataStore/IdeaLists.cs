using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SongStorm.DataStore
{
    public class IdeaLists
    {
        public string GetIdea(string type)
        {
            Random random = new Random();
            var ideaList = File.ReadAllLines("Content/Text/" + type + ".txt");
            var randomLineNumber = random.Next(9, ideaList.Length - 1);
            string idea = ideaList[randomLineNumber];
            idea = idea.Replace("\\", "").Replace("*", "");

            return idea;

        } 
    }
}
