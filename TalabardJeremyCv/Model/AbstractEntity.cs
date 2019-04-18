using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TalabardJeremyCv.Model
{
    public abstract class AbstractEntity<T> : IEquatable<T> 
    {
        public int Id;

        public AbstractEntity(XmlNode node)
        {
            if(node != null)
            {
                ReadXml(node);
            }
            
        }

        public bool Equals(T other)
        {
            AbstractEntity<T> obj = other as AbstractEntity<T>;
            if (this.Id.Equals(obj.Id))
            {
                return true;
            }
            return false;
        }

        public abstract void ReadXml(XmlNode node);

    }
}