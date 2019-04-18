using Newtonsoft.Json;

namespace TalabardJeremyCv.Model
{
    public class Description
    {
        [JsonProperty("title")]
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

    }
}
