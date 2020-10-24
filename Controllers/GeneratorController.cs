using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using SongStorm.ViewModels;
using SongStorm.DataStore;

namespace SongStorm.Controllers
{
    public class GeneratorController : Controller
    {

        IdeaLists il = new IdeaLists();

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(IdeaVM ivmIn, string loophole)
        {
            return View();
        }

        //-------------------------------------//

        [HttpPost]
        public ActionResult Idea(IdeaVM ivm)
        {
            SidebarVM svm = new SidebarVM();
            ivm.Sidebar = svm;

            return View("Expand", ivm);
        }

        [HttpPost]
        public ActionResult GetIdea(string ideaTypes)
        {
            string pChordRetrieved = "";
            string jChordRetrieved = "";
            string structureRetrieved = "";
            string typeRetrieved = "";
            string typeOut = "";
            string ideaOut = "";
            string chordsOutPR = "";
            string chordsOutJ = "";

            if (String.IsNullOrEmpty(ideaTypes))
            {
                List<string> typesList = new List<string>();
                typesList.Add("Situation/Sentiment");
                typesList.Add("Word");
                typesList.Add("Chord Progression (Pop/Rock)");
                typesList.Add("Chord Progression (Jazz)");
                typesList.Add("Song Structure");

                Random random = new Random();
                int randomType = random.Next(typesList.Count);
                ideaTypes = typesList[randomType];
            }

            string[] types = ideaTypes.Split('-');

            //Removes song types from list for use in boolean
            var ilist = new List<string>(types);
            ilist.Remove("Song Type");
            ilist.Remove("Chord Progression (Jazz)");
            ilist.Remove("Chord Progression (Pop/Rock)");

            string[] arrNoType = ilist.ToArray();

            bool moreThanOneType = (arrNoType.Count() > 2);

            if (types.Contains("Situation/Sentiment"))
            {
                string situationRetrieved = il.GetIdea("Situation");
                string ideaAddition = "About " + situationRetrieved + ", ";
                ideaOut += ideaAddition;
            }
            if (types.Contains("Word"))
            {
                string wordRetrieved = il.GetIdea("Words");
                //string ideaAddition = moreThanOneType ? "That uses the word '" + wordRetrieved + "'" : "That uses the word '" + wordRetrieved + "'";
                ideaOut += "That uses the word '" + wordRetrieved + "'";
            }

            if (types.Contains("Chord Progression (Pop/Rock)"))
            {
                pChordRetrieved = il.GetIdea("PopRock_Chords");
                chordsOutPR = "Using the chord progression: " + pChordRetrieved;
            }
            if (types.Contains("Chord Progression (Jazz)"))
            {
                jChordRetrieved = il.GetIdea("Jazz_Chords");
                chordsOutJ = "Using the chord progression: " + jChordRetrieved;
            }
            if (types.Contains("Song Structure"))
            {
                structureRetrieved = il.GetIdea("Structure");
                string ideaAddition = moreThanOneType ? ", that contains " + structureRetrieved + "" : "That has " + structureRetrieved + "";
                ideaOut += ideaAddition;

            }
            if (types.Contains("Song Type"))
            {
                typeRetrieved = il.GetIdea("Types");
                typeOut = typeRetrieved;
            }
            else
            {
                typeOut = "song";
            }

            return Json(new { ideaOut, chordsOutPR, chordsOutJ, typeOut });
        }

        [HttpPost]
        public ActionResult Expand(IdeaVM ivm)
        {

            return View();
        }

    }
}
