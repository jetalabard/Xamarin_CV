using Cv_Core.DataModel;
using System;
using System.Collections.Generic;

namespace Cv_Core.DataManagement
{
    internal class MockData : IDataAccess
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
                Title = "Description Générale",
                Name = "Test test",
                Adress = "Rue de Lyon, Lyon",
                Email = "test@test@hotmail.fr",
                FbLink = "https://www.facebook.com/",
                Image = "http://talabardjeremy.epizy.com/img/jeremy_photo.JPG",
                LinkedinLink = "https://fr.linkedin.com/",
                Phone = "0677189651",
                Lat = 45.74362609999999,
                Long = 4.833899400000064,
                Summary = "Julia Child met haar merkwaardige stem, Miranda Priestly met haar blikken die kunnen doden en hippie Donna in haar tuinpakken: Meryl Streep heeft al heel veel verschillende vrouwen gespeeld. "
            };
        }

        public ICollection<Header> Headers()
        {
            throw new NotImplementedException();
        }

        public ICollection<Hobie> Hobies()
        {
            return new List<Hobie>
        {
            new Hobie()
            {
                Id = 1,
                IdLinks = null,
                Title = "Judo",
                SubTitle = "Ceinture Noire",
                Image = "judo.jpg",
                Summary = "Je fais du judo depuis 19 ans",
                Date = "2000 - 2019"
            },
            new Hobie()
            {
                Id = 2,
                IdLinks = null,
                Title = "Théatre",
                SubTitle = "Pattafyx",
                Image = "theatre.jpg",
                Summary = "J'adore le théatre",
                Date = "2004 - 2017"
            }
            };
        }

        public ICollection<Job> Jobs()
        {
            throw new NotImplementedException();
        }

        public ICollection<Knowledge> Knowledges()
        {
            throw new NotImplementedException();
        }

        public ICollection<Link> Links()
        {
            throw new NotImplementedException();
        }

        public ICollection<PersonalProject> PersonalProjects()
        {
            throw new NotImplementedException();
        }

        public ICollection<Project> Projects()
        {
            throw new NotImplementedException();
        }

        public ICollection<Training> Trainings()
        {
            return new List<Training>
        {
            new Training()
            {
                Id = 1,
                IdLinks = null,
                Title = "Bac S",
                SubTitle = "",
                Image = "bac.jpg",
                Summary = "",
                Date = "2012"
            },
            new Training()
            {
                Id = 2,
                IdLinks = null,
                Title = "Master Informatique",
                SubTitle = "Alternance",
                Image = "master.jpg",
                Summary = "Alternance chez CGI",
                Date = "2017-2018"
            }
        };
        }
    }
}