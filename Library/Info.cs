using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Library
{
    public static class Info
    {
        public static string relativePath = @"..\";

        public static async Task<List<Team>> GetTeams(string URL)
        {
            RestClient restClient = new RestClient(URL);
            RestResponse<Team> restResponse = await restClient.ExecuteAsync<Team>(new RestRequest());
            if (!restResponse.IsSuccessful)
            {
            }
            List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(restResponse.Content);
            return teams;
        }

        public static async Task<List<Match>> GetMatches(string URL)
        {
            RestClient restClient = new RestClient(URL);
            var restResponse = await restClient.ExecuteAsync(new RestRequest());
            if (!restResponse.IsSuccessful)
            {
                throw new Exception($"Failed to get matches: {restResponse.StatusCode} - {restResponse.ErrorMessage}");
            }
            var matches = JsonConvert.DeserializeObject<List<Match>>(restResponse.Content);
            return matches;
        }

        public static ISet<Player> GetPlayersFromFile(string path)
        {
            ISet<Player> players = new HashSet<Player>();
            string[] strings = [""];
            try
            {
                strings = File.ReadAllLines(path);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
            int iterations = int.Parse(strings[0]);
            for (int i = 1; i < iterations; i++)
            {
                players.Add(Player.Parse(strings[i]));
            }
            return players;
        }

        public static async Task GetPlayersFromApiAsync(Team team, IList<Match> allMatches, bool v, ISet<Player> allPlayers)
        {
            string url = "";
            if (v)
            {
                url = $"https://worldcup-vua.nullbit.hr/men/matches/country?fifa_code={team.FifaCode}";
            }
            else
            {
                url = $"https://worldcup-vua.nullbit.hr/women/matches/country?fifa_code={team.FifaCode}";
            }


            List<Match> matches = await GetMatches(url);

            foreach (Match match in matches)
            {
                allMatches.Add(match);
            }

            if (allMatches[0].AwayTeamCountry == team.Country)
            {
                foreach (Player player in allMatches[0].AwayTeamStatistics.StartingEleven.Concat<Player>(allMatches[0].AwayTeamStatistics.Substitutes))
                {
                    allPlayers.Add(player);
                }
            }
            else if (allMatches[0].HomeTeamCountry == team.Country)
            {
                foreach (Player player in allMatches[0].HomeTeamStatistics.StartingEleven.Concat<Player>(allMatches[0].HomeTeamStatistics.Substitutes))
                {
                    allPlayers.Add(player);
                }
            }
        }

        public static async Task GetStartingElevenApiAsync(Team team, IList<Match> allMatches, bool v, ISet<Player> allPlayers)
        {
            string url = "";
            if (v)
            {
                url = $"https://worldcup-vua.nullbit.hr/men/matches/country?fifa_code={team.FifaCode}";
            }
            else
            {
                url = $"https://worldcup-vua.nullbit.hr/women/matches/country?fifa_code={team.FifaCode}";
            }


            List<Match> matches = await GetMatches(url);

            foreach (Match match in matches)
            {
                allMatches.Add(match);
            }

            if (allMatches[0].AwayTeamCountry == team.Country)
            {
                foreach (Player player in allMatches[0].AwayTeamStatistics.StartingEleven)
                {
                    allPlayers.Add(player);
                }
            }
            else if (allMatches[0].HomeTeamCountry == team.Country)
            {
                foreach (Player player in allMatches[0].HomeTeamStatistics.StartingEleven)
                {
                    allPlayers.Add(player);
                }
            }
        }

        public static async Task GetSubstitutesElevenApiAsync(Team team, IList<Match> allMatches, bool v, ISet<Player> allPlayers)
        {
            string url = "";
            if (v)
            {
                url = $"https://worldcup-vua.nullbit.hr/men/matches/country?fifa_code={team.FifaCode}";
            }
            else
            {
                url = $"https://worldcup-vua.nullbit.hr/women/matches/country?fifa_code={team.FifaCode}";
            }


            List<Match> matches = await GetMatches(url);

            foreach (Match match in matches)
            {
                allMatches.Add(match);
            }

            if (allMatches[0].AwayTeamCountry == team.Country)
            {
                foreach (Player player in allMatches[0].AwayTeamStatistics.Substitutes)
                {
                    allPlayers.Add(player);
                }
            }
            else if (allMatches[0].HomeTeamCountry == team.Country)
            {
                foreach (Player player in allMatches[0].HomeTeamStatistics.Substitutes)
                {
                    allPlayers.Add(player);
                }
            }
        }
    }
}
