using System;
using System.Threading.Tasks;

namespace RaceWinners;

public class Program
{
    static async Task Main(string[] args)
    {
        DataService ds = new DataService();

        // Asynchronously retrieve the group (class) data
        var data = await ds.GetGroupRanksAsync();

        for (int i = 0; i < data.Count; i++)
        {
            // Combine the ranks to print as a list
            var ranks = String.Join(", ", data[i].Ranks);

            Console.WriteLine($"{data[i].Name} - [{ranks}]");
        }
        Console.WriteLine("\nLotto Results");

        int[] tickets = new int [data.Count];
        decimal [] winningsMoney = new decimal [data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            var ranks = String.Join (", ", data[i].Ranks);
            Console.WriteLine($"Drawing {drawing}:");

            for (int i = 0; i < data.Count; i++)
            {
                decimal money = tickets[i] * random.Next(1, 101);
                winnings[i] += money;
            }
        }
}