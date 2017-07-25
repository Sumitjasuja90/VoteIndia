using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObject;
using BusinessObject;
using VoteIndia.Models;
namespace VoteIndia.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            QuestionsAnswer qa = new QuestionsAnswer().GetAllQuestionsFromXML();
            return View(qa);
        }
    }
}