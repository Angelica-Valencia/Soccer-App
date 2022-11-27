using System;
using System.Collections.Generic;

namespace Soccer_App.Model
{
    public class AwayScoreEvetn
    {
        public int? current { get; set; }
        public int? display { get; set; }
        public int? period_1 { get; set; }
        public int? period_2 { get; set; }
        public int? normal_time { get; set; }
    }

    public class AwayTeamEvent
    {
        public int? id { get; set; }
        public int? sport_id { get; set; }
        public int? category_id { get; set; }
        public int? venue_id { get; set; }
        public int? manager_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public bool? has_logo { get; set; }
        public string logo { get; set; }
        public NameTranslEvent name_translations { get; set; }
        public string name_short { get; set; }
        public string name_full { get; set; }
        public string name_code { get; set; }
        public bool? has_sub { get; set; }
        public string gender { get; set; }
        public bool? is_nationality { get; set; }
        public string country_code { get; set; }
        public string country { get; set; }
        public string flag { get; set; }
        public string foundation { get; set; }
    }

    public class ChallengeEvent
    {
        public int? id { get; set; }
        public int? sport_id { get; set; }
        public int? league_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public NameTranslEvent name_translations { get; set; }
        public int? order { get; set; }
        public int? priority { get; set; }
    }

    public class DatumEvent
    {
        public int? id { get; set; }
        public int? sport_id { get; set; }
        public int? home_team_id { get; set; }
        public int? away_team_id { get; set; }
        public int? league_id { get; set; }
        public int? challenge_id { get; set; }
        public int? season_id { get; set; }
        public int? venue_id { get; set; }
        public int? referee_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string status_more { get; set; }
        public TimeDetails time_details { get; set; }
        public HomeTeamEvent home_team { get; set; }
        public AwayTeamEvent away_team { get; set; }
        public string start_at { get; set; }
        public int? priority { get; set; }
        public HomeScoreEvent home_score { get; set; }
        public AwayScoreEvetn away_score { get; set; }
        public int? winner_code { get; set; }
        public object aggregated_winner_code { get; set; }
        public bool? result_only { get; set; }
        public object coverage { get; set; }
        public object ground_type { get; set; }
        public int? round_number { get; set; }
        public int? series_count { get; set; }
        public int? medias_count { get; set; }
        public int? status_lineup { get; set; }
        public object first_supply { get; set; }
        public string cards_code { get; set; }
        public string event_data_change { get; set; }
        public string lasted_period { get; set; }
        public int? default_period_count { get; set; }
        public int? attendance { get; set; }
        public object cup_match_order { get; set; }
        public object cup_match_in_round { get; set; }
        public object periods { get; set; }
        public RoundInfo round_info { get; set; }
        public object periods_time { get; set; }
        public MainOdds main_odds { get; set; }
        public LeagueEvent league { get; set; }
        public ChallengeEvent challenge { get; set; }
        public SeasonEvent season { get; set; }
        public SectionEvent section { get; set; }
        public SportEvent sport { get; set; }
    }

    public class HomeScoreEvent
    {
        public int? current { get; set; }
        public int? display { get; set; }
        public int? period_1 { get; set; }
        public int? period_2 { get; set; }
        public int? normal_time { get; set; }
    }

    public class HomeTeamEvent
    {
        public int? id { get; set; }
        public int? sport_id { get; set; }
        public int? category_id { get; set; }
        public int? venue_id { get; set; }
        public int? manager_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public bool? has_logo { get; set; }
        public string logo { get; set; }
        public NameTranslEvent name_translations { get; set; }
        public string name_short { get; set; }
        public string name_full { get; set; }
        public string name_code { get; set; }
        public bool? has_sub { get; set; }
        public string gender { get; set; }
        public bool? is_nationality { get; set; }
        public string country_code { get; set; }
        public string country { get; set; }
        public string flag { get; set; }
        public string foundation { get; set; }
    }

    public class LeagueEvent
    {
        public int? id { get; set; }
        public int? sport_id { get; set; }
        public int? section_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public NameTranslEvent name_translations { get; set; }
        public bool? has_logo { get; set; }
        public string logo { get; set; }
    }

    public class MainOdds
    {
        public Outcome1 outcome_1 { get; set; }
        public OutcomeX outcome_X { get; set; }
        public Outcome2 outcome_2 { get; set; }
    }

    public class MetaEvent
    {
        public int? current_page { get; set; }
        public int? from { get; set; }
        public int? last_page { get; set; }
        public int? per_page { get; set; }
        public int? to { get; set; }
        public int? total { get; set; }
    }

    public class NameTranslEvent
    {
        public string en { get; set; }
        public string ru { get; set; }
        public string de { get; set; }
        public string es { get; set; }
        public string fr { get; set; }
        public string zh { get; set; }
        public string tr { get; set; }
        public string el { get; set; }
        public string it { get; set; }
        public string nl { get; set; }
        public string pt { get; set; }
    }

    public class Outcome1
    {
        public double? value { get; set; }
        public int? change { get; set; }
    }

    public class Outcome2
    {
        public double? value { get; set; }
        public int? change { get; set; }
    }

    public class OutcomeX
    {
        public double? value { get; set; }
        public int? change { get; set; }
    }

    public class RootEvent
    {
        public DatumEvent[] data { get; set; }
        public MetaEvent meta { get; set; }
    }

    public class RoundInfo
    {
        public int? round { get; set; }
    }

    public class SeasonEvent
    {
        public int? id { get; set; }
        public int? league_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public int? year_start { get; set; }
        public int? year_end { get; set; }
    }

    public class SectionEvent
    {
        public int? id { get; set; }
        public int? sport_id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public int? priority { get; set; }
        public string flag { get; set; }
    }

    public class SportEvent
    {
        public int? id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
    }

    public class TimeDetails
    {
        public int? injuryTime2 { get; set; }
        public int? currentPeriodStartTimestamp { get; set; }
        public int? injuryTime1 { get; set; }
    }
}

