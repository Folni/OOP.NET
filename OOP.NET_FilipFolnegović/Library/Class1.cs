using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Library
{
    public static class Information
    {
        public static string relativePath = @"..\";

        // Methods 1 (GetMatches), 2 (GetPlayersFromFile), and 4 (GetTeams) remain the same.
        // ... (Keep your existing GetMatches, GetPlayersFromFile, GetTeams methods)

        // ----------------------------------------------------
        // 3) REVISED: Get Players & Matches for a specific team
        // ----------------------------------------------------
        /// <summary>
        /// Fetches all matches for a given team and aggregates a unique set of all players
        /// who participated in those matches.
        /// </summary>
        /// <param name="team">The team to get data for.</param>
        /// <param name="isMenWorldCup">Specifies whether to use the Men's or Women's API.</param>
        /// <returns>A tuple containing the list of matches and a set of players for the team.</returns>
        public static async Task<(List<Match> TeamMatches, ISet<Player> TeamPlayers)> GetTeamDataAsync(Team team, bool isMenWorldCup)
        {
            if (team == null || string.IsNullOrWhiteSpace(team.FifaCode))
            {
                // Return empty collections if the team is invalid
                return (new List<Match>(), new HashSet<Player>());
            }

            // Get all matches for the given team
            List<Match> teamMatches = await GetMatches(team.FifaCode, isMenWorldCup);
            ISet<Player> teamPlayers = new HashSet<Player>();

            // Iterate through ALL fetched matches to gather every player
            foreach (var match in teamMatches)
            {
                IEnumerable<Player> playersInMatch = null;

                // Determine if the team was home or away to get the correct player list
                if (match.HomeTeamCountry == team.Country)
                {
                    playersInMatch = match.HomeTeamStatistics.StartingEleven
                                        .Concat(match.HomeTeamStatistics.Substitutes);
                }
                else if (match.AwayTeamCountry == team.Country)
                {
                    playersInMatch = match.AwayTeamStatistics.StartingEleven
                                        .Concat(match.AwayTeamStatistics.Substitutes);
                }

                // Add the players from this match to the set (duplicates are handled automatically)
                if (playersInMatch != null)
                {
                    foreach (var player in playersInMatch)
                    {
                        teamPlayers.Add(player);
                    }
                }
            }

            return (teamMatches, teamPlayers);
        }

        // In Library/Information.cs, inside the Information class

        // ---------------------------
        // 1) Get Matches by FIFA Code
        // ---------------------------
        public static async Task<List<Match>> GetMatches(string fifaCode, bool isMenWorldCup)
        {
            if (string.IsNullOrWhiteSpace(fifaCode))
                throw new ArgumentException("FIFA code must be provided", nameof(fifaCode));

            string baseUrl = isMenWorldCup
                ? "https://worldcup-vua.nullbit.hr/men/matches"
                : "https://worldcup-vua.nullbit.hr/women/matches";

            // The API endpoint for matches by country is `/country?fifa_code=XXX`
            string fullUrl = $"{baseUrl}/country?fifa_code={fifaCode}";

            var restClient = new RestClient();
            var request = new RestRequest(fullUrl, Method.Get);

            var restResponse = await restClient.ExecuteAsync<List<Match>>(request);

            if (!restResponse.IsSuccessful)
                throw new Exception($"API Error: {restResponse.StatusCode} - {restResponse.Content}");

            if (restResponse.Data == null)
                return new List<Match>(); // Return an empty list if API returns no data

            return restResponse.Data;
        }
        public static async Task<List<Team>> GetTeams(bool isMenWorldCup)
        {
            string baseUrl = isMenWorldCup
                ? "https://worldcup-vua.nullbit.hr/men/teams"
                : "https://worldcup-vua.nullbit.hr/women/teams";

            var restClient = new RestClient();
            var request = new RestRequest(baseUrl, Method.Get);

            var restResponse = await restClient.ExecuteAsync<List<Team>>(request);

            if (!restResponse.IsSuccessful)
                throw new Exception($"API Error: {restResponse.StatusCode} - {restResponse.Content}");

            if (restResponse.Data == null)
                return new List<Team>();

            return restResponse.Data;
        }
    }
}