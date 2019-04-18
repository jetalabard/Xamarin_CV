using System;
using System.Xml;

namespace TalabardJeremyCv.Model
{
    public class Link : AbstractEntity<Link>
    {

        public int IdLinks;

        public string LinkTitle;

        public string Url;

        public bool Important;

        public bool IsUrl;

        public string Title;
        
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
                                IsUrl = column.InnerText == "1";
                                break;
                            case Constants.LINK:
                                Url = column.InnerText;
                                break;
                            case Constants.IMPORTANCE:
                                Important =column.InnerText == "1";
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
