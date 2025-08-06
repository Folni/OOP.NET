using Library;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lib
{
    public static class Information
    {
        public static string relativePath = @"..\";

        public static async Task<List<Team>> GetTeams(string URL)
        {
            var restClient = new RestClient(URL);
            var restResponse = await restClient.ExecuteAsync<List<Team>>(new RestRequest());

            if (!restResponse.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve teams: {restResponse.StatusCode} - {restResponse.ErrorMessage}");
            }

            return restResponse.Data;
        }

        public static async Task<List<Match>> GetMatches(string URL)
        {
            var restClient = new RestClient(URL);
            var restResponse = await restClient.ExecuteAsync<List<Match>>(new RestRequest());

            if (!restResponse.IsSuccessful)
            {
                throw new Exception($"No matches found: {restResponse.StatusCode} - {restResponse.ErrorMessage}");
            }

            return restResponse.Data;
        }

        public static ISet<Player> GetPlayersFromFile(string path)
        {
            ISet<Player> players = new HashSet<Player>();
            string[] strings;

            try
            {
                strings = File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                // Throw a specific exception!!!
                throw new IOException($"An error occurred while reading the file at {path}.", ex);
            }

            // Check if the file is empty or malformed
            if (strings.Length < 1)
            {
                return players;
            }

            try
            {
                int iterations = int.Parse(strings[0]);
                for (int i = 1; i <= iterations && i < strings.Length; i++)
                {
                    players.Add(Player.Parse(strings[i]));
                }
            }
            catch (FormatException ex)
            {
                throw new FormatException("The file contains malformed data or an invalid line count.", ex);
            }

            return players;
        }

        public static async Task GetPlayersFromApiAsync(Team team, IList<Match> allMatches, bool choice, ISet<Player> allPlayers)
        {
            string url = choice
                ? $"https://worldcup-vua.nullbit.hr/men/matches/country?fifa_code={team.FifaCode}"
                : $"https://worldcup-vua.nullbit.hr/women/matches/country?fifa_code={team.FifaCode}";

            List<Match> matches = await GetMatches(url);

            if (matches.Any())
            {
                // Add all matches to the list
                foreach (Match match in matches)
                {
                    allMatches.Add(match);
                }

                var firstMatch = allMatches[0];

                if (firstMatch.AwayTeamCountry == team.Country)
                {
                    var playersInMatch = firstMatch.AwayTeamStatistics.StartingEleven.Concat(firstMatch.AwayTeamStatistics.Substitutes);
                    foreach (Player player in playersInMatch)
                    {
                        allPlayers.Add(player);
                    }
                }
                else if (firstMatch.HomeTeamCountry == team.Country)
                {
                    var playersInMatch = firstMatch.HomeTeamStatistics.StartingEleven.Concat(firstMatch.HomeTeamStatistics.Substitutes);
                    foreach (Player player in playersInMatch)
                    {
                        allPlayers.Add(player);
                    }
                }
            }
        }

      
    }
}