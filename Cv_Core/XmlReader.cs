using Cv_Core.DataModel;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Cv_Core
{
    public class XmlReader
    {

        public ICollection<Knowledge> Knowledges = new List<Knowledge>();

        public ICollection<Description> Descriptions = new List<Description>();

        public ICollection<Hobie> Hobies = new List<Hobie>();

        public ICollection<Cv> Documents = new List<Cv>();

        public ICollection<Training> Trainings = new List<Training>();

        public ICollection<Header> Heads = new List<Header>();

        public ICollection<Link> Links = new List<Link>();

        public ICollection<Job> Jobs = new List<Job>();

        public ICollection<PersonalProject> PersonalProjects = new List<PersonalProject>();

        public ICollection<Project> Projects = new List<Project>();
        


        public XmlReader(string xmlFile)
        {
            if (string.IsNullOrEmpty(xmlFile))
            {
                throw new ArgumentException("path of xml file Database must be initialized");
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);
            XmlNodeList elemList = doc.GetElementsByTagName(Constants.TABLE);
           
            foreach (XmlNode node in elemList)
            {
                XmlAttribute attribute = node.Attributes[Constants.NAME];
                if (attribute != null)
                {
                    switch (attribute.Value)
                    {
                        case Constants.TABLE_KNOWLEDGE:
                            Knowledges.Add(new Knowledge(node));
                            break;
                        case Constants.TABLE_DESCRIPTION:
                            Descriptions.Add(new Description(node));
                            break;
                        case Constants.TABLE_HOBIE:
                            Hobies.Add(new Hobie(node));
                            break;
                        case Constants.TABLE_DOCUMENT:
                            Documents.Add(new Cv(node));
                            break;
                        case Constants.TABLE_TRAINING:
                            Trainings.Add(new Training(node));
                            break;
                        case Constants.TABLE_HEAD:
                            Heads.Add(new Header(node));
                            break;
                        case Constants.TABLE_JOB:
                            Jobs.Add(new Job(node));
                            break;
                        case Constants.TABLE_LINK:
                            Links.Add(new Link(node));
                            break;
                        case Constants.TABLE_PERSONALPROJECT:
                            PersonalProjects.Add(new PersonalProject(node));
                            break;
                        case Constants.TABLE_PROJECT:
                            Projects.Add(new Project(node));
                            break;
                        case Constants.TABLE_PICTURE:
                            break;
                        case Constants.TABLE_USER:
                            break;
                        case Constants.TABLE_LINKS:
                            break;
                        default:
                            throw new XmlException("Undexpected node : " + attribute.Value);
                    }
                }
            }
        }

    }
}