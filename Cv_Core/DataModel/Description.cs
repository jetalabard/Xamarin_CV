using Newtonsoft.Json;
using System;
using System.Xml;

namespace Cv_Core.DataModel
{
    public class Description : AbstractEntity<Description>
    {
        public string Title;

        public string Image;

        public string Summary;

        public string Name;

        public string Email;

        public string Phone;

        public string FbLink;

        public string LinkedinLink;

        public string Adress;

        public double Lat;

        public double Long;

        public Description() : base(null) { }

        public Description(XmlNode node) : base(node) { }

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
                            case Constants.HEADING:
                                Title = column.InnerText;
                                break;
                            case Constants.NAME:
                                Name = column.InnerText;
                                break;
                            case Constants.SUMMARY:
                                Summary = column.InnerText;
                                break;
                            case Constants.PICTURE:
                                Image = column.InnerText;
                                break;
                            case Constants.PHONE:
                                Phone = column.InnerText;
                                break;
                            case Constants.FACEBOOK:
                                FbLink = column.InnerText;
                                break;
                            case Constants.LINKEDIN:
                                LinkedinLink = column.InnerText;
                                break;
                            case Constants.EMAIL:
                                Email = column.InnerText;
                                break;
                            case Constants.ADRESS:
                                Adress = column.InnerText;
                                break;
                            case Constants.LAT:
                                double lat = 0;
                                double.TryParse(column.InnerText.Replace('.', ','), out lat);
                                Lat = lat;
                                break;
                            case Constants.LONG:
                                double longi = 0;
                                double.TryParse(column.InnerText.Replace('.', ','), out longi);
                                Long = longi;
                                break;
                            case Constants.LINKAPP:
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
