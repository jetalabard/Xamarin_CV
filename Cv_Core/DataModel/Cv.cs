using System;
using System.Xml;

namespace Cv_Core.DataModel
{
    public class Cv : AbstractEntity<Cv>
    {
        public string Name;

        public string Title;

        public string Url;

        public string Type;

        public Cv() : base(null) { }
        

        public Cv(XmlNode node) : base(node) { }
       
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
                            case Constants.NAME:
                                Name = column.InnerText;
                                break;
                            case Constants.LINK:
                                Url = column.InnerText;
                                break;
                            case Constants.TYPE:
                                Type = column.InnerText;
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
