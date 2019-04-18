using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TalabardJeremyCv.Model
{
   public abstract class Activity
    {
        [JsonProperty("ID")]
        public int Id;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("subtitle")]
        public string SubTitle;

        [JsonProperty("title")]
        public string Name;

        [JsonProperty("photo")]
        public string Image;

        [JsonProperty("resume")]
        public string Summary;

        [JsonProperty("date")]
        public string Date;

        [JsonProperty("idlinks")]
        public int? IdLinks;


        public List<Link> Links;

    }
}
