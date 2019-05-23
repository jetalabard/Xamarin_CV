using System.Collections.Generic;
using System.Xml;

namespace Cv_Core.DataModel
{
    public abstract class Activity : AbstractEntity<Activity>
    {
        public string Title { get; set; }
        
        public string SubTitle { get; set; }

        public string Image { get; set; }

        public string Summary { get; set; }

        public string Date { get; set; }

        public int? IdLinks { get; set; }


        public List<Link> Links { get; set; }


        public Activity(XmlNode node) : base(null)
        {
            ReadXml(node);
        }

        public string SubTitleWithDate
        {

            get
            {
                return string.Format("{0} - {1}", SubTitle, Date);
            }
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
                                Id = System.Convert.ToInt16(column.InnerText);
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
                                IdLinks = System.Convert.ToInt16(column.InnerText);
                                break;
                            default:
                                throw new XmlException("Undexpected attribute in " + column.Name);

                        }
                    }
                }


            }


        }

        public static ICollection<Activity> Convert<T>(ICollection<T> activities) where T : Activity
        {
            return activities == null ? null: new List<Activity>(activities);
        }
    }
}
