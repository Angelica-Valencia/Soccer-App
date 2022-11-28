using System;
using System.Collections.Generic;

namespace Soccer_App.Model
{
    public class DatumIncidents
    {
        public int id { get; set; }
        public int event_id { get; set; }
        public string incident_type { get; set; }
        public int? time { get; set; }
        public object time_over { get; set; }
        public int? order { get; set; }
        public string text { get; set; }
        public int? scoring_team { get; set; }
        public int? player_team { get; set; }
        public int? home_score { get; set; }
        public int? away_score { get; set; }
        public string card_type { get; set; }
        public object is_missed { get; set; }
        public string reason { get; set; }
        public int? length { get; set; }
        public Player player { get; set; }
        public PlayerTwoIn player_two_in { get; set; }
    }

    public class NameTranslIncidents
    {
        public string en { get; set; }
    }

    public class Player
    {
        public int? id { get; set; }
        public int? sport_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public NameTranslIncidents name_translations { get; set; }
        public string name_short { get; set; }
        public bool? has_photo { get; set; }
        public string photo { get; set; }
        public string position { get; set; }
        public string position_name { get; set; }
    }

    public class PlayerTwoIn
    {
        public int? id { get; set; }
        public int? sport_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public NameTranslIncidents name_translations { get; set; }
        public string name_short { get; set; }
        public bool? has_photo { get; set; }
        public string photo { get; set; }
        public string position { get; set; }
        public string position_name { get; set; }
    }

    public class RootIncidents
    {
        public List<DatumIncidents> data { get; set; }
        public object meta { get; set; }
    }
}

