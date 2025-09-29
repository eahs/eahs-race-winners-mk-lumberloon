using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RaceWinners
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            DataService ds = new DataService();
            var data = await ds.GetGroupRanksAsync();
            Random rand = new Random();

            // Find total runners
            int totalRunners = 0;
            foreach (var g in data)
                totalRunners += g.Ranks.Count;

            // Draw 5 winning white balls (1–69) + 1 Powerball (1–26)
            HashSet<int> whiteBalls = new HashSet<int>();
            while (whiteBalls.Count < 5)
                whiteBalls.Add(rand.Next(1, 70));
            int powerBall = rand.Next(1, 27);

            Console.WriteLine("Winning White Balls: ");
            foreach (var w in whiteBalls) Console.Write(w + " ");
            Console.WriteLine("\nPowerball: " + powerBall);

            // Money results
            Dictionary<string, int> money = new Dictionary<string, int>();

            foreach (var group in data)
            {
                int classMoney = 0;

                foreach (var rank in group.Ranks)
                {
                    // Tickets based on rank (1st gets max, last gets 0)
                    int tickets = totalRunners - rank;

                    for (int t = 0; t < tickets; t++)
                    {
                        // Generate random ticket (5 whites + 1 PB)
                        HashSet<int> ticketWhites = new HashSet<int>();
                        while (ticketWhites.Count < 5)
                            ticketWhites.Add(rand.Next(1, 70));
                        int ticketPower = rand.Next(1, 27);

                        // Count matches
                        int whiteMatches = 0;
                        foreach (int num in ticketWhites)
                        {
                            if (whiteBalls.Contains(num))
                                whiteMatches++;
                        }
                        bool powerMatch = (ticketPower == powerBall);

                        // Prize rules
                        int prize = 0;
                        if (whiteMatches == 5 && powerMatch) prize = 100000000;
                        else if (whiteMatches == 5) prize = 1000000;
                        else if (whiteMatches == 4 && powerMatch) prize = 50000;
                        else if (whiteMatches == 4) prize = 100;
                        else if (whiteMatches == 3 && powerMatch) prize = 100;
                        else if (whiteMatches == 3) prize = 7;
                        else if (whiteMatches == 2 && powerMatch) prize = 7;
                        else if (whiteMatches == 1 && powerMatch) prize = 4;
                        else if (whiteMatches == 0 && powerMatch) prize = 4;

                        classMoney += prize;
                    }
                }

                money[group.Name] = classMoney;
                Console.WriteLine(group.Name + " total: $" + classMoney);
            }

            // Find winner
            string winner = "";
            int topPrize = 0;
            foreach (var entry in money)
            {
                if (entry.Value > topPrize)
                {
                    topPrize = entry.Value;
                    winner = entry.Key;
                }
            }

            Console.WriteLine("\n" + winner + " wins!");
        }
    }
}
