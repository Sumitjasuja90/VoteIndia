using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoteIndia.Models;
namespace VoteIndia.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int question_id = 1;
            QuestionsAnswer qa = new QuestionsAnswer().GetAllQuestionsFromXML(question_id);
            return View(qa);
        }

        /// <summary>
        /// This action returns the result of the given question in precentage.
        /// </summary>
        /// <returns></returns>
        [HttpPost] 
        public ActionResult Index(FormCollection form, QuestionsAnswer model)
        {
            int question_id = 1;
            Boolean isSaved = new QuestionsAnswer().SaveAnswerInXML(question_id, Convert.ToString(form["selectanswer"]));
            QuestionsResult result = null;
            if (isSaved)
            {
                result = new QuestionsResult().GetResultFromXML(question_id);
            }
            return View("_Result", result);
        }
    }
}