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
        public QuestionsAnswer GetResultFromXML(int questionId)
        {
            XDocument doc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/XML/QuestionsResult.xml"));
            XElement questions = doc.Element("questions");
            QuestionsAnswer result = new QuestionsAnswer();
            result.id = questionId;
            foreach (XElement qa in questions.Elements("question"))
            {
                if (Convert.ToInt16(qa.Attribute("id").Value) == questionId)
                {
                    result = CalculatePercentage(qa.Element("options"));
                    break;
                }
            }
            return result;
        }

        private QuestionsAnswer CalculatePercentage(XElement options)
        {
            QuestionsAnswer result = new QuestionsAnswer();
            Int64 totalVotes = 0;

            foreach (XElement opt in options.Elements())
            {
                totalVotes += Convert.ToInt64(opt.Value);
            }
            foreach (XElement opt in options.Elements())
            {
               string precentage = String.Format("{0:0.00}%", Convert.ToDouble(opt.Value) * 100/totalVotes);
               result.Options.Add(precentage);
            }
            return result;
        }
    }
}