using System;
using System.Collections.Generic;
using System.Text;

namespace Soccer_App.Model
{
    public class Rootobject2
    {
        public Datum[] data { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public int current_page { get; set; }
        public int from { get; set; }
        public int last_page { get; set; }
        public int per_page { get; set; }
        public int to { get; set; }
        public int total { get; set; }
    }

    public class Datum
    {
        public int? id { get; set; }
        public int sport_id { get; set; }
        public int section_id { get; set; }
        public string slug { get; set; }
        public Name_Translations name_translations { get; set; }
        public bool has_logo { get; set; }
        public string logo { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int priority { get; set; }
        public Host host { get; set; }
        public int tennis_points { get; set; }
        public Fact[] facts { get; set; }
        public int? most_count { get; set; }
        public Section section { get; set; }
        public Sport sport { get; set; }
        public string country { get; set; }
    }

    public class Name_Translations
    {
        public string en { get; set; }
        public string ru { get; set; }
    }

    public class Host
    {
        public string country { get; set; }
        public string flag { get; set; }
    }

    public class Section
    {
        public int id { get; set; }
        public int sport_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public int? priority { get; set; }
        public string flag { get; set; }
    }

    public class Sport
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
    }

    public class Fact
    {
        public string name { get; set; }
        public string value { get; set; }
    }

}

