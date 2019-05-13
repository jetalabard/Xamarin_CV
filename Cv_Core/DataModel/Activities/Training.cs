using System.Xml;

namespace Cv_Core.DataModel
{
    public class Training : Activity
    {
        public Training() : base(null)
        {
        }

        public Training(XmlNode node) : base(node) { }
    }
}
