using System.IO;

namespace Cv_Core
{
    public static class Constants
    {
        public static string[] TITLES_KNOWLEDGES = new string[] { "Langages et Framework de programmation", "Méthodologie et outils" };
        public const string DATABASE_FILE_NAME = "Database_cv.xml";
        public const string IMG_FILE_NAME = "Img.zip";

        public const string SHARED_DATABASE_PATH = "path_database";

        public const string TABLE_HOBIE = "hobie";
        public const string TABLE_KNOWLEDGE = "competence";
        public const string TABLE_DESCRIPTION = "description";
        public const string TABLE_DOCUMENT = "document";
        public const string TABLE_TRAINING = "formation";
        public const string TABLE_HEAD = "head";
        public const string TABLE_JOB = "job";
        public const string TABLE_LINK = "link";
        public const string TABLE_LINKS = "links";
        public const string TABLE_PERSONALPROJECT = "personalproject";
        public const string TABLE_PROJECT = "project";
        public const string TABLE_PICTURE = "image";
        public const string TABLE_USER = "user";

        // XML Constants
        public const string TITLE = "title";
        public const string PICTURE = "photo";
        public const string SUBTITLE = "subtitle";
        public const string SUMMARY = "resume";
        public const string IDLINKS = "idlinks";
        public const string IDLINK = "idLinks";
        public const string DATE = "date";
        public const string ID = "ID";

        public const string NAME = "name";
        public const string TYPE = "type";
        public const string IMAGE = "image";

        public const string HEADING = "intitule";
        public const string EMAIL = "email";
        public const string PHONE = "tel";
        public const string FACEBOOK = "faceBookLink";
        public const string LINKEDIN = "linkedinLink";
        public const string LAT = "LAT";
        public const string LONG = "LONG";
        public const string ADRESS = "adress";

        public const string WORDINGLINK = "libllink";

        public const string ISURL = "url";
        public const string IMPORTANCE = "importance";

        public const string WORDING = "libl";

        public const string LINK = "link";
        public const string SUBSUBTITLE = "subsubtitle";



        //constants for menu
        public const string PAGE_HOBIE = "Loisirs";
        public const string PAGE_KNOWLEDGE = "Compétences Informatique";
        public const string HOME_PAGE = "Accueil";
        public const string PAGE_CV = "Cv";
        public const string PAGE_TRAINING = "Formations";
        public const string PAGE_JOB = "Emplois";
        public const string PAGE_PERSONALPROJECT = "Projets Personels";
        public const string PAGE_PROJECT = "Projets / Stages";
        public const string PAGE_SETTING = "Paramètre";


        public const string MESSAGE_CALL_PERMISSION_DENIED = "Vous n'avez pas accordé la permission à l'application de passer un appel. (Aller dans les paramètres pour le modifier)";

        //sharred preference constants
        public const string REFRESH_MODE_PREFERENCES = "refresh_mode";

        //Parameters
        public const string ADRESS_PARAMETER = "adress";
        public const string TABLE = "table";
        public const string DATABASE = "database";
        public const string COLUMN = "column";

        public const string LINKAPP = "linkApp";

        public static string DATE_DOWNLAOD = "date_download_database";
    }
}
