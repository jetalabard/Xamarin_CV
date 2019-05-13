using Android.OS;
using Android.Runtime;
using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Cv_Core.DataModel
{
    public class Knowledge : AbstractEntity<Knowledge>
    {
        public string Name;

        public string Image;

        public string Type;


        public Knowledge(XmlNode node) : base(node) { }


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
                            case Constants.IMAGE:
                                Image = column.InnerText;
                                break;
                            case Constants.TYPE:
                                Type = column.InnerText;
                                break;
                            case Constants.NAME:
                                Name = column.InnerText;
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
