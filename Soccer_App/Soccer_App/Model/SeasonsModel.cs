using System;
using System.Collections.Generic;

namespace Soccer_App.Model
{
    public class DatumSeason
    {
        public int? id { get; set; }
        public int? league_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public int? year_start { get; set; }
        public int? year_end { get; set; }
        public League[] league { get; set; }
    }

    public class LeagueSeason
    {
        public int? id { get; set; }
        public int? sport_id { get; set; }
        public int? section_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public NameTranslSeason[] name_translations { get; set; }
        public bool has_logo { get; set; }
        public string logo { get; set; }
    }

    public class MetaSeason
    {
        public int? current_page { get; set; }
        public int? from { get; set; }
        public int? per_page { get; set; }
        public int? to { get; set; }
    }

    public class NameTranslSeason
    {
        public string en { get; set; }
        public string ru { get; set; }
    }

    public class Root
    {
        public DatumSeason[] data { get; set; }
        public Meta meta { get; set; }
    }
}

