using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.DAO
{
    public class WebServiceManager : IDataAccess
    {
        private static readonly HttpClient _client = new HttpClient();

        private async static Task<string> GetContent(string url)
        {
            return await _client.GetStringAsync(url);
        }

        private List<T> GetActivities<T>() where T : Activity
        {
            return GetList<T>();
        }

        private List<T> GetList<T>()
        {
            Dictionary<string, Type> Links = LinkClassToTable.Get;
            string table = Links.Where(x => x.Value == typeof(T)).FirstOrDefault().Key;
            string content = GetContent(Constants.URL + table).Result;
            return new List<T> { JsonConvert.DeserializeObject<T>(content) };
        }

        private T Get<T>()
        {
            Dictionary<string, Type> Links = LinkClassToTable.Get;
            string table = Links.Where(x => x.Value == typeof(T)).FirstOrDefault().Key;
            string content = GetContent(Constants.URL + table).Result;
            return JsonConvert.DeserializeObject<T>(content);
        }
        
        public Cv Cv()
        {
            return Get<Cv>();
        }

        public Description Description()
        {
            return Get<Description>();
        }

        public IList<Header> Headers()
        {
            return GetList<Header>();
        }

        public IList<Hobie> Hobies()
        {
            return GetActivities<Hobie>();
        }

        public IList<Job> Jobs()
        {
            return GetActivities<Job>();
        }

        public IList<Knowledge> Knowledges()
        {
            return GetList<Knowledge>();
        }

        public IList<Link> Links()
        {
            return GetList<Link>();
        }

        public IList<PersonalProject> PersonalProjects()
        {
            return GetActivities<PersonalProject>();
        }

        public IList<Project> Projects()
        {
            return GetActivities<Project>();
        }

        public IList<Training> Trainings()
        {
            return GetActivities<Training>();
        }
    }
}
