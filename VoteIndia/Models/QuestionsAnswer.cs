using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace VoteIndia.Models
{
    public class QuestionsAnswer
    {
        public int id;
        public string Description;
        public List<string> Options = new List<string>();

        public QuestionsAnswer GetAllQuestionsFromXML()
        {
            XDocument doc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/XML/QuestionsAnswers.xml"));
            XElement questions = doc.Element("questions");
            QuestionsAnswer result = new QuestionsAnswer();
            foreach (XElement qa in questions.Elements("question"))
            {
                if (qa.Attribute("id").Value == "1")
                {
                    result.Description = qa.Element("description").Value;
                    result.Options.Add(qa.Element("options").Element("a").Value);
                    result.Options.Add(qa.Element("options").Element("b").Value);
                    result.Options.Add(qa.Element("options").Element("c").Value);
                    result.Options.Add(qa.Element("options").Element("d").Value);
                    break;
                }
            }
            return result;
        }
    }
}