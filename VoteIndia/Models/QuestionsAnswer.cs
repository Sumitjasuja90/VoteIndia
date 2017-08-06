using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace VoteIndia.Models
{
    public class QuestionsAnswer
    {
        public int id;
        public string Description;
        public string selectanswer { get; set; }
        public List<string> Options = new List<string>();
       
        /// <summary>
        /// This method gets the question description and options from XML for the given question ID.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public QuestionsAnswer GetAllQuestionsFromXML(int questionId)
        {
            XDocument doc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/XML/QuestionsAnswers.xml"));
            XElement questions = doc.Element("questions");
            QuestionsAnswer result = new QuestionsAnswer();
            foreach (XElement qa in questions.Elements("question"))
            {
                if (Convert.ToInt16(qa.Attribute("id").Value) == questionId)
                {
                   // result.id = Convert.ToInt16(qa.Element("id").Value);
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

        /// <summary>
        /// This method returns the result of the given question
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public Boolean SaveAnswerInXML(int questionId, string ansResult = "")
        {
            XDocument docAns = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/XML/QuestionsResult.xml"));
            XElement node = docAns.Descendants("question").FirstOrDefault(a => a.Attribute("id").Value == Convert.ToString(questionId)).Descendants("options").FirstOrDefault();
            string ansOption = "";
            if (!string.IsNullOrEmpty(ansResult))
            {
                switch (ansResult)
                {
                    case "Yes":
                        ansOption = "a";
                        break;
                    case "No":
                        ansOption = "b";
                        break;
                    case "Can't say":
                        ansOption = "c";
                        break;
                    case "No Impact":
                        ansOption = "d";
                        break;
                }
                var value = Convert.ToInt32(node.Element(ansOption).Value);
                node.SetElementValue(ansOption, value + 1);
                docAns.Save(HttpContext.Current.Server.MapPath("~/App_Data/XML/QuestionsResult.xml"));
            }
            return true;
         }
   }
}