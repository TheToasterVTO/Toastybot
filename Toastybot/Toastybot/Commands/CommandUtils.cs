using DemoFile;
using DemoFile.Game.Cs;
namespace Toastybot.commands
{
    public class CommandUtils
    {
        public static Rating RatingCalculator(double kast, double kpr, double dpr, double adr, double apr)
        {
            double impact = 2.13 * kpr + (.42 * apr) - .41;
            double rating = (0.00738764 * kast) + (0.35912389 * kpr) + (-0.5329508 * dpr) + (0.2372603 * impact) + (0.0032397 * adr) + .1587;
            rating = rating - 0.01;

            return new Rating { rating = rating, impact = impact };
        }

        public struct Rating
        {
            public double rating;
            public double impact;
        }

        public static string StatFormat(string[,] strings)
        {
            int[] lengths = { 0, 0 };
            var top = "╔";
            var bottom = "╚";
            var seperator = "╟";
            for (int i = 0; i < lengths.Length; i++)
            {
                for (int j = 0; j < strings.GetLength(0); j++)
                {
                    lengths[i] = Math.Max(strings[j, i].Length, lengths[i]);
                }
                top += new string('═', lengths[i] + 2);
                bottom += new string('═', lengths[i] + 2);
                seperator += new string('═', lengths[i] + 2);
                if (i == lengths.Length - 1)
                {
                    top += "╗\n";
                    bottom += "╝";
                    seperator += "╢\n";
                }
                else
                {
                    seperator += "┼";
                    bottom += "╧";
                    top += "╤";
                }

            }
            string[] result = { "║", "║", "║", "║", "║", "║", "║" };
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < lengths.Length; j++)
                {
                    result[i] += ' ';
                    result[i] += strings[i, j];
                    result[i] += new string(' ', lengths[j] - strings[i, j].Length + 1);
                    if (j == lengths.Length - 1)
                    {
                        result[i] += "║\n";
                    }
                    else
                    {
                        result[i] += "│";
                    }
                }
            }
            string FinalResult = top;
            for (int i = 0; i < result.Length; i++)
            {
                FinalResult += result[i];
                if (i == result.Length - 1)
                {
                    FinalResult += bottom;
                }
                else
                {
                    FinalResult += seperator;
                }
            }
            return FinalResult;
        }

        public struct PlayerStats
        {
            //Player name
            public required string Name { get; set; }
            //Player rank
            public required int CompRank { get; set; }
            //crosshair code
            public required string CrosshairCode { get; set; }
            //number of kills
            public required double kills { get; set; }
            //number of deaths
            public required double deaths { get; set; }
            //number of assists
            public required int assists { get; set; }
            //kill/death difference
            public required double KDD { get; set; }
            //kill/death ratio
            public required string KDR { get; set; }
            //Average damage per round
            public required string ADR { get; set; }
            //headshot percentage
            public required string HSPercentage { get; set; }
            //Percentage of rounds with a kill, assist, survived or death traded
            public required string KAST { get; set; }
            //HLTV 2.0 Rating
            public required string Rating { get; set; }
            //Enemies flashed
            public required int EF { get; set; }
            //Flash assists
            public required int FlashAssists { get; set; }
            //Enemy blind time
            public required string EBT { get; set; }
            //Utility damage
            public required int UD { get; set; }
            //First kill difference
            public required int FKD { get; set; }
            //Trade kills
            public required int TradeKills { get; set; }
            //1vX
            public required int Clutches { get; set; }
            //3kill+ multikills
            public required int Multikills { get; set; }
        }
    }
}