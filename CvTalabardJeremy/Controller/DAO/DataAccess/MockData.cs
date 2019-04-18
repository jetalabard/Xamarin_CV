using System;
using System.Collections.Generic;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.DAO
{
    public class MockData : IDataAccess
    {

        public Cv Cv()
        {
            return new Cv()
            {
                Name = "Mon cv",
                Title = "CV",
                Url = "https://www.dropbox.com/s/7sqfxslw2z328vc/Talabard_Jeremy_CV.pdf?dl=1"
            };
        }

        public Description Description()
        {
            return new Description()
            {
                Title = "Libelle Test",
                Name = "Test test",
                Adress = "Rue de Lyon, Lyon",
                Email = "test@test@hotmail.fr",
                FbLink = "https://www.facebook.com/",
                Image = "",
                LinkedinLink = "https://fr.linkedin.com/",
                Phone = "0677189651",
                Lat = 45.74362609999999,
                Long = 4.833899400000064,
                Summary = "Julia Child met haar merkwaardige stem, Miranda Priestly met haar blikken die kunnen doden en hippie Donna in haar tuinpakken: Meryl Streep heeft al heel veel verschillende vrouwen gespeeld. "
            };
        }

        public IList<Header> Headers()
        {
            throw new NotImplementedException();
        }

        public IList<Hobie> Hobies()
        {
            return new List<Hobie>
        {
            new Hobie()
            {
                Id = 1,
                IdLinks = null,
                Name = "Judo",
                SubTitle = "Ceinture Noire",
                Image = "judo.jpg",
                Summary = "Je fais du judo depuis 19 ans",
                Date = "2000 - 2019"
            },
            new Hobie()
            {
                Id = 2,
                IdLinks = null,
                Name = "Théatre",
                SubTitle = "Pattafyx",
                Image = "theatre.jpg",
                Summary = "J'adore le théatre",
                Date = "2004 - 2017"
            }
            };
        }

        public IList<Job> Jobs()
        {
            throw new NotImplementedException();
        }

        public IList<Knowledge> Knowledges()
        {
            throw new NotImplementedException();
        }

        public IList<Link> Links()
        {
            throw new NotImplementedException();
        }

        public IList<PersonalProject> PersonalProjects()
        {
            throw new NotImplementedException();
        }

        public IList<Project> Projects()
        {
            throw new NotImplementedException();
        }

        public IList<Training> Trainings()
        {
            return new List<Training>
        {
            new Training()
            {
                Id = 1,
                IdLinks = null,
                Name = "Bac S",
                SubTitle = "",
                Image = "bac.jpg",
                Summary = "",
                Date = "2012"
            },
            new Training()
            {
                Id = 2,
                IdLinks = null,
                Name = "Master Informatique",
                SubTitle = "Alternance",
                Image = "master.jpg",
                Summary = "Alternance chez CGI",
                Date = "2017-2018"
            }
        };
        }
    }
}