using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library
{
    //Tool from the json2csharp website
    public class Team
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("alternate_name")]
        public object AlternateName { get; set; }

        [JsonProperty("fifa_code")]
        public string FifaCode { get; set; }

        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        [JsonProperty("group_letter")]
        public string GroupLetter { get; set; }
        public HashSet<string> Opponents { get; set; }
        public long GamesPlayed { get; set; }
        public long Wins { get; set; }
        public long Losses { get; set; }
        public long Ties { get; set; }
        public long GoalsScored { get; set; }
        public long GoalsTaken { get; set; }
        public override string ToString() => $"{Country} ({FifaCode})";
    }
}