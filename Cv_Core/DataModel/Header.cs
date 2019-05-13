using System;
using System.Xml;

namespace Cv_Core.DataModel
{
    public class Header : AbstractEntity<Header>
    {
        public string Title;

        public string SubTitle;

        public string SubSubTitle;

        public string Summary;

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
                            case Constants.TITLE:
                                Title = column.InnerText;
                                break;
                            case Constants.SUBTITLE:
                                SubTitle = column.InnerText;
                                break;
                            case Constants.SUBSUBTITLE:
                                SubSubTitle = column.InnerText;
                                break;
                            case Constants.SUMMARY:
                                Summary = column.InnerText;
                                break;
                          
                            default:
                                throw new XmlException("Undexpected attribute in " + column.Name);

                        }
                    }
                }
            }


        }

        public Header(XmlNode node) : base(node) { }
    }
}
