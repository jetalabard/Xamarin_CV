using System;
using System.Xml;

namespace Cv_Core.DataModel
{
    public class Link : AbstractEntity<Link>
    {

        public int IdLinks { get; set; }

        public string LinkTitle { get; set; }

        public string Url { get; set; }

        public bool Important { get; set; }

        public bool IsUrl { get; set; }

        public string Title { get; set; }

        public Link(XmlNode node) : base(node) { }

        public override void ReadXml(XmlNode node)
        {
            XmlNodeList columns = node.SelectNodes(Constants.COLUMN);
            if (columns != null)
            {
                foreach (XmlNode column in columns)
                {
                    XmlAttribute attributeName = column.Attributes[Constants.NAME];
                    if (attributeName != null)
                    {
                        switch (attributeName.Value)
                        {
                            case Constants.ID:
                                Id = Convert.ToInt16(column.InnerText);
                                break;
                            case Constants.WORDING:
                                Title = column.InnerText;
                                break;
                            case Constants.ISURL:
                                IsUrl = column.InnerText == "0";
                                break;
                            case Constants.LINK:
                                Url = column.InnerText;
                                break;
                            case Constants.IMPORTANCE:
                                Important =column.InnerText == "0";
                                break;
                            case Constants.WORDINGLINK:
                                LinkTitle = column.InnerText;
                                break;
                            case Constants.IDLINK:
                                IdLinks = Convert.ToInt16(column.InnerText);
                                break;
                            default:
                                throw new XmlException("Undexpected attribute in " + column.Name);

                        }
                    }
                }
            }            
        }
    }
}
