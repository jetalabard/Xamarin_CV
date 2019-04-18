using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Xml;
using TalabardJeremyCv;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.Controller.Services
{
    public class ParserXML
    {
        /// <summary>
        /// the reader of the file
        /// </summary>
        private XmlReader _Reader;

        public ICollection<Knowledge> Knowledges = new List<Knowledge>();

        public ICollection<Description> Descriptions = new List<Description>();

        public ICollection<Hobie> Hobies = new List<Hobie>();

        public ICollection<Document> Documents = new List<Document>();

        public ICollection<Training> Trainings = new List<Training>();

        public ICollection<Head> Heads = new List<Head>();

        public ICollection<Link> Links = new List<Link>();

        public ICollection<Job> Jobs = new List<Job>();

        public ICollection<PersonalProject> PersonalProjects = new List<PersonalProject>();

        public ICollection<Project> Projects = new List<Project>();

        public ParserXML(string xmlFile)
        {
            InitReader();
            _Reader.ReadStartElement(Constants.DATABASE, Constants.URI_XML);
            while (_Reader.NodeType == XmlNodeType.Element)
            {
                if (_Reader.LocalName == Constants.TABLE_KNOWLEDGE && _Reader.NamespaceURI == Constants.URI_XML)
                {
                    Knowledges.Add(new Knowledge(mReader));
                }
                else if (_Reader.LocalName == Constants.TABLE_DESCRIPTION && _Reader.NamespaceURI == Constants.URI_XML)
                {
                    Descriptions.Add(new Description(mReader));
                }
                else if (_Reader.LocalName == Constants.TABLE_HOBIE && _Reader.NamespaceURI == Constants.URI_XML)
                {
                    Hobies.Add(new Hobie(mReader));
                }
                else if (_Reader.LocalName == Constants.TABLE_DOCUMENT && _Reader.NamespaceURI == Constants.URI_XML)
                {
                    Documents.Add(new Document(mReader));
                }
                else if (_Reader.LocalName == Constants.TABLE_TRAINING && _Reader.NamespaceURI == Constants.URI_XML)
                {
                    Trainings.Add(new Training(mReader));
                }
                else if (_Reader.LocalName == Constants.TABLE_HEAD && _Reader.NamespaceURI == Constants.URI_XML)
                {
                    Heads.Add(new Head(mReader));
                }
                else if (_Reader.LocalName == Constants.TABLE_JOB && _Reader.NamespaceURI == Constants.URI_XML)
                {
                    Jobs.Add(new Job(mReader));
                }
                else if (_Reader.LocalName == Constants.TABLE_LINK && _Reader.NamespaceURI == Constants.URI_XML)
                {
                    Links.Add(new Link(mReader));
                }
                else if (_Reader.LocalName == Constants.TABLE_PERSONALPROJECT && _Reader.NamespaceURI == Constants.URI_XML)
                {
                    PersonalProjects.Add(new PersonalProject(mReader));
                }
                else if (_Reader.LocalName == Constants.TABLE_PROJECT && _Reader.NamespaceURI == Constants.URI_XML)
                {
                    Projects.Add(new Project(mReader));
                }
                else
                {
                    throw new XmlException("Undexpected node : " + mReader.Name);
                }
            }
            mReader.ReadEndElement();
        }

       
        /// <summary>
        /// </summary>
        private void InitReader()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;

            //schema validation
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(XMLTags.URI, mXsdFile);
            settings.ValidationEventHandler += ValidationHandler;

            _Reader = XmlReader.Create(mXmlFile, settings);
        }
    }
}