using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TalabardJeremyCv.Model
{
    public class Training : Activity
    {
        public Training() : base(null)
        {
        }

        public Training(XmlNode node) : base(node) { }
    }
}
