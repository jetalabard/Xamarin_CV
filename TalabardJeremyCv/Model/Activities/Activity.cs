using System;
using System.Collections.Generic;
using System.Xml;

namespace TalabardJeremyCv.Model
{
    public abstract class Activity : AbstractEntity<Activity>
    {
        public string Title;
        
        public string SubTitle;
        
        public string Image;
        
        public string Summary;
        
        public string Date;
        
        public int? IdLinks;


        public List<Link> Links;
        

        public Activity(XmlNode node) : base(null)
        {
            ReadXml(node);
        }


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
                            case Constants.PICTURE:
                                Image = column.InnerText;
                                break;
                            case Constants.SUMMARY:
                                Summary = column.InnerText;
                                break;
                            case Constants.DATE:
                                Date = column.InnerText;
                                break;
                            case Constants.IDLINKS:
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
