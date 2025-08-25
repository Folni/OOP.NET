using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Library
{

    public enum TypeOfEvent
    {
        Goal,
        GoalOwn,
        GoalPenalty,
        SubstitutionIn,
        SubstitutionOut,
        YellowCard,
        Unknown
    };

    public enum Positions
    {
        Defender,
        Forward,
        Goalie,
        Midfield,
        Null
    };

    public partial class Match
    {

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("fifa_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FifaId { get; set; }

        [JsonProperty("weather")]
        public Weather Weather { get; set; }

        [JsonProperty("attendance")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Attendance { get; set; }

        [JsonProperty("officials")]
        public List<string> Officials { get; set; }

        [JsonProperty("stage_name")]
        public string StageName { get; set; }

        [JsonProperty("home_team_country")]
        public string HomeTeamCountry { get; set; }

        [JsonProperty("away_team_country")]
        public string AwayTeamCountry { get; set; }

        [JsonProperty("datetime")]
        public DateTimeOffset? Datetime { get; set; }

        [JsonProperty("winner")]
        public string Winner { get; set; }

        [JsonProperty("winner_code")]
        public string WinnerCode { get; set; }

        [JsonProperty("home_team")]
        public MatchTeam HomeTeam { get; set; }

        [JsonProperty("away_team")]
        public MatchTeam AwayTeam { get; set; }

        [JsonProperty("home_team_events")]
        public List<Event> HomeTeamEvents { get; set; }

        [JsonProperty("away_team_events")]
        public List<Event> AwayTeamEvents { get; set; }

        [JsonProperty("home_team_statistics")]
        public TeamStatistics HomeTeamStatistics { get; set; }

        [JsonProperty("away_team_statistics")]
        public TeamStatistics AwayTeamStatistics { get; set; }

        [JsonProperty("last_event_update_at")]
        public DateTimeOffset? LastEventUpdateAt { get; set; }

        [JsonProperty("last_score_update_at")]
        public DateTimeOffset? LastScoreUpdateAt { get; set; }
        public override string ToString() => $"{HomeTeam.Country} vs {AwayTeam.Country}";
    }

    public partial class MatchTeam
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("goals")]
        public long? Goals { get; set; }

        [JsonProperty("penalties")]
        public long? Penalties { get; set; }
    }

    public partial class Event
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type_of_event")]
        [JsonConverter(typeof(TypeOfEventConverter))]
        public TypeOfEvent TypeOfEvent { get; set; }

        [JsonProperty("player")]
        public string Player { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }
    }

    public partial class TeamStatistics
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("attempts_on_goal")]
        public long? AttemptsOnGoal { get; set; }

        [JsonProperty("on_target")]
        public long? OnTarget { get; set; }

        [JsonProperty("off_target")]
        public long? OffTarget { get; set; }

        [JsonProperty("blocked")]
        public long? Blocked { get; set; }

        [JsonProperty("woodwork")]
        public long? Woodwork { get; set; }

        [JsonProperty("corners")]
        public long? Corners { get; set; }

        [JsonProperty("offsides")]
        public long? Offsides { get; set; }

        [JsonProperty("ball_possession")]
        public long? BallPossession { get; set; }

        [JsonProperty("pass_accuracy")]
        public long? PassAccuracy { get; set; }

        [JsonProperty("num_passes")]
        public long? NumPasses { get; set; }

        [JsonProperty("passes_completed")]
        public long? PassesCompleted { get; set; }

        [JsonProperty("distance_covered")]
        public long? DistanceCovered { get; set; }

        [JsonProperty("balls_recovered")]
        public long? BallsRecovered { get; set; }

        [JsonProperty("tackles")]
        public long? Tackles { get; set; }

        [JsonProperty("clearances")]
        public long? Clearances { get; set; }

        [JsonProperty("yellow_cards")]
        public long? YellowCards { get; set; }

        [JsonProperty("red_cards")]
        public long? RedCards { get; set; }

        [JsonProperty("fouls_committed")]
        public long? FoulsCommitted { get; set; }

        [JsonProperty("tactics")]
        public string Tactics { get; set; }

        [JsonProperty("starting_eleven")]
        public List<Player> StartingEleven { get; set; }

        [JsonProperty("substitutes")]
        public List<Player> Substitutes { get; set; }
    }

    public partial class Player
    {
        public static char Delimiter = '|';

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("captain")]
        public bool? Captain { get; set; }

        [JsonProperty("shirt_number")]
        public long? ShirtNumber { get; set; }

        [JsonProperty("position")]
        public Positions Position { get; set; }

        public bool Favorite { get; set; }
        public int NoGoals { get; set; }
        public int NoYellowCards { get; set; }
        public string ImagePath { get; set; }

        public static Player Parse(string player)
        {
            string[] properties = player.Split(Delimiter);

            return new Player()
            {
                Name = properties[0],
                ShirtNumber = int.Parse(properties[1]),
                Captain = int.Parse(properties[2]) == 500 ? true : false,
                Position = (Positions)Positions.Parse(typeof(Positions), properties[3]),
                Favorite = int.Parse(properties[4]) == 700 ? true : false,
                ImagePath = string.IsNullOrEmpty(properties[5]) == false ? properties[5] : null,
                NoYellowCards = 0,
                NoGoals = 0,
            };
        }

        public static string Format(Player player)
        {
            return $"{player.Name}{Delimiter}{player.ShirtNumber}{Delimiter}{((bool)player.Captain ? 500 : -500)}{Delimiter}{player.Position}{Delimiter}{((bool)player.Favorite ? 700 : -700)}{Delimiter}{(player.ImagePath is not null ? player.ImagePath:string.Empty)}";
        }

        public override bool Equals(object obj)
        {
            return obj is Player player &&
                   Name == player.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public override string ToString() => $"{Name} {ShirtNumber} {((Boolean)Captain ? "DA" : "NE")} {Position}";
    }

    public partial class Weather
    {
        [JsonProperty("humidity")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Humidity { get; set; }

        [JsonProperty("temp_celsius")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? TempCelsius { get; set; }

        [JsonProperty("temp_farenheit")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? TempFarenheit { get; set; }

        [JsonProperty("wind_speed")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? WindSpeed { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }



    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TypeOfEventConverter.Singleton,
                PositionConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class TypeOfEventConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeOfEvent) || t == typeof(TypeOfEvent?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader)?.Trim().ToLowerInvariant().Replace(" ", "-");
            switch (value)
            {
                case "goal":
                    return TypeOfEvent.Goal;
                case "goal-own":
                    return TypeOfEvent.GoalOwn;
                case "goal-penalty":
                    return TypeOfEvent.GoalPenalty;
                case "substitution-in":
                    return TypeOfEvent.SubstitutionIn;
                case "substitution-out":
                    return TypeOfEvent.SubstitutionOut;
                case "yellow-card":
                    return TypeOfEvent.YellowCard;
                default:
                    return TypeOfEvent.Unknown;
            }
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeOfEvent)untypedValue;
            switch (value)
            {
                case TypeOfEvent.Goal:
                    serializer.Serialize(writer, "goal");
                    return;
                case TypeOfEvent.GoalOwn:
                    serializer.Serialize(writer, "goal-own");
                    return;
                case TypeOfEvent.GoalPenalty:
                    serializer.Serialize(writer, "goal-penalty");
                    return;
                case TypeOfEvent.SubstitutionIn:
                    serializer.Serialize(writer, "substitution-in");
                    return;
                case TypeOfEvent.SubstitutionOut:
                    serializer.Serialize(writer, "substitution-out");
                    return;
                case TypeOfEvent.YellowCard:
                    serializer.Serialize(writer, "yellow-card");
                    return;
                default:
                    serializer.Serialize(writer, "Unkown");
                    return;
            }
        }

        public static readonly TypeOfEventConverter Singleton = new TypeOfEventConverter();
    }

    internal class PositionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Positions) || t == typeof(Positions?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Defender":
                    return Positions.Defender;
                case "Forward":
                    return Positions.Forward;
                case "Goalie":
                    return Positions.Goalie;
                case "Midfield":
                    return Positions.Midfield;
                default:
                    return Positions.Null;
            }
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Positions)untypedValue;
            switch (value)
            {
                case Positions.Defender:
                    serializer.Serialize(writer, "Defender");
                    return;
                case Positions.Forward:
                    serializer.Serialize(writer, "Forward");
                    return;
                case Positions.Goalie:
                    serializer.Serialize(writer, "Goalie");
                    return;
                case Positions.Midfield:
                    serializer.Serialize(writer, "Midfield");
                    return;
            }
        }

        public static readonly PositionConverter Singleton = new PositionConverter();
    }
}