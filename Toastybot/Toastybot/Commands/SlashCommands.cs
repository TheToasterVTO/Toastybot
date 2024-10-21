using Discord.Commands;
using Discord.Interactions;
using DemoFile.Game.Cs;
using DemoFile.Game;
using DemoFile;
using DemoFile.Sdk;
using Discord;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using SteamKit2.Internal;
using ProtoBuf;
using System.Xml;
namespace Toastybot.commands
{

    public class SlashCommands : InteractionModuleBase
    {
        [SlashCommand("test", "im losing my mind")]
        public async Task Test()
        {
            await Context.Interaction.RespondAsync("real");
        }

        [SlashCommand("hltv-rating-calculator", "Outputs HLTV 2.0 rating")]
        public async Task HLTVRating(double kast, double kpr, double dpr, double adr, double apr)
        {
            CommandUtils.Rating rating = CommandUtils.RatingCalculator(kast, kpr, dpr, adr, apr);
            string[,] strings = { { "Kast:",string.Format("{0:0.##}", kast), }, { "KPR:",string.Format("{0:0.##}", kpr) }, { "DPR:",string.Format("{0:0.##}", dpr) }, { "ADR:",string.Format("{0:0.##}", adr) },
                                { "APR:",string.Format("{0:0.##}", apr) }, { "Impact:",string.Format("{0:0.##}", rating.impact) }, { "Rating 2.0:", string.Format("{0:0.##}", rating.rating)} };

            string Stats = CommandUtils.StatFormat(strings);
            await Context.Interaction.RespondAsync("```\n" + Stats + "\n```");
        }

        [SlashCommand("demotest", "test")]
        public async Task HLTVRatingDemo()
        {
            await Context.Interaction.DeferAsync();
            var path = "C:\\Users\\andre\\Desktop\\Competitive Nuke.dem";
            List<CommandUtils.PlayerStats> cts = new List<CommandUtils.PlayerStats>();
            List<CommandUtils.PlayerStats> ts = new List<CommandUtils.PlayerStats>();
            var demo = new CsDemoParser();
            Dictionary<string, int> FlashAssistsDictionary = new Dictionary<string, int>();
            Dictionary<string, float> EnemyBlindTimeDictionary = new Dictionary<string, float>();
            Dictionary<string, int> FirstDeathDictionary = new Dictionary<string, int>();
            Dictionary<string, int> TradeKillsDictionary = new Dictionary<string, int>();
            HashSet<string> TradeValues = new HashSet<string>();
            Dictionary<string, double> KASTDictionary = new Dictionary<string, double>();
            HashSet<string> KASTValues = new HashSet<string>();
            Dictionary<string, bool> SurvivedRound = new Dictionary<string, bool>();
            Dictionary<string, string> DeathTradeDictionary = new Dictionary<string, string>();
            bool FirstDeath = false;

            demo.Source1GameEvents.CsRoundStartBeep += e =>
            {
                foreach (var player in demo.Players)
                {
                    if (!SurvivedRound.ContainsKey(player.PlayerName))
                        SurvivedRound.Add(player.PlayerName, true);
                }
            };
            demo.Source1GameEvents.RoundEnd += e =>
            {
                foreach (var player in demo.Players)
                {
                    if (KASTValues.Contains(player.PlayerName))
                    {
                        if (!KASTDictionary.ContainsKey(player.PlayerName))
                            KASTDictionary.Add(player.PlayerName, 0);

                        KASTDictionary[player.PlayerName]++;
                    }
                    else if (SurvivedRound[player.PlayerName])
                    {
                        if (!KASTDictionary.ContainsKey(player.PlayerName))
                            KASTDictionary.Add(player.PlayerName, 0);

                        KASTDictionary[player.PlayerName]++;
                    }
                    else if (DeathTradeDictionary.ContainsKey(DeathTradeDictionary[player.PlayerName]))
                    {
                        if (!KASTDictionary.ContainsKey(player.PlayerName))
                            KASTDictionary.Add(player.PlayerName, 0);

                        KASTDictionary[player.PlayerName]++;
                    }
                }
                DeathTradeDictionary.Clear();
                SurvivedRound.Clear();
                TradeValues.Clear();
                KASTValues.Clear();
                FirstDeath = false;
            };
            demo.Source1GameEvents.PlayerDeath += e =>
            {
                if (e.Attacker is not null)
                {
                    DeathTradeDictionary.Add(e.Player.PlayerName, e.Attacker.PlayerName);
                    
                    if (!TradeValues.Contains(e.Attacker.PlayerName))
                    {
                        TradeValues.Add(e.Attacker.PlayerName);

                    }
                    if (TradeValues.Contains(e.Player.PlayerName))
                    {
                        if (!TradeKillsDictionary.ContainsKey(e.Attacker.PlayerName))
                            TradeKillsDictionary.Add(e.Attacker.PlayerName, 0);

                        TradeKillsDictionary[e.Attacker.PlayerName]++;
                    }

                    if (!KASTValues.Contains(e.Attacker.PlayerName))
                    {
                        KASTValues.Add(e.Attacker.PlayerName);
                    }
                    else if (e.Assister is not null && !KASTValues.Contains(e.Assister.PlayerName))
                    {
                        KASTValues.Add(e.Assister.PlayerName);
                    }
                    SurvivedRound[e.Player.PlayerName] = !SurvivedRound[e.Player.PlayerName];

                }

                if (!FirstDeath)
                {
                    if (!FirstDeathDictionary.ContainsKey(e.Player.PlayerName))
                    {
                        FirstDeathDictionary.Add(e.Player.PlayerName, 0);
                    }
                    --FirstDeathDictionary[e.Player.PlayerName];
                    if (!FirstDeathDictionary.ContainsKey(e.Attacker.PlayerName))
                    {
                        FirstDeathDictionary.Add(e.Attacker.PlayerName, 0);
                    }
                    ++FirstDeathDictionary[e.Attacker.PlayerName];
                    FirstDeath = !FirstDeath;
                }
                if (e.Assistedflash)
                {
                    if (!FlashAssistsDictionary.ContainsKey(e.Assister.PlayerName))
                    {
                        FlashAssistsDictionary.Add(e.Assister.PlayerName, 0);
                    }
                    ++FlashAssistsDictionary[e.Assister.PlayerName];
                }
            };
            demo.Source1GameEvents.PlayerBlind += e =>
            {
                if (e.Player.Team != e.Attacker.Team)
                {

                    if (!EnemyBlindTimeDictionary.ContainsKey(e.Attacker.PlayerName))
                    {
                        EnemyBlindTimeDictionary.Add(e.Attacker.PlayerName, 0);
                    }
                    EnemyBlindTimeDictionary[e.Attacker.PlayerName] = EnemyBlindTimeDictionary[e.Attacker.PlayerName] + e.BlindDuration;

                }

            };
            var reader = DemoFileReader.Create(demo, File.OpenRead(path));
            await reader.ReadAllAsync();
            var ct = demo.TeamCounterTerrorist.CSPlayerControllers;
            var t = demo.TeamTerrorist.CSPlayerControllers;
            foreach (var player in ct)
            {

                var Playerstats = player.ActionTrackingServices.MatchStats;
                double kills = Playerstats.Kills;
                double deaths = Playerstats.Deaths;
                var assists = Playerstats.Assists;
                double kd = kills / deaths;
                double Damage = Playerstats.Damage;
                var TotalRoundsPlayed = demo.GameRules.TotalRoundsPlayed;
                int flashAssists = 0;
                float EnemyBlindTime = 0;
                int FirstDeathDifference = 0;
                int TradeKills = 0;
                double KAST = 0;
                if (FlashAssistsDictionary.ContainsKey(player.PlayerName))
                    flashAssists = FlashAssistsDictionary[player.PlayerName];
                if (EnemyBlindTimeDictionary.ContainsKey(player.PlayerName))
                    EnemyBlindTime = EnemyBlindTimeDictionary[player.PlayerName];
                if (FirstDeathDictionary.ContainsKey(player.PlayerName))
                    FirstDeathDifference = FirstDeathDictionary[player.PlayerName];
                if (TradeKillsDictionary.ContainsKey(player.PlayerName))
                    TradeKills = TradeKillsDictionary[player.PlayerName];
                if (KASTDictionary.ContainsKey(player.PlayerName))
                    KAST = KASTDictionary[player.PlayerName] / TotalRoundsPlayed * 100;
                var rating = CommandUtils.RatingCalculator(KAST, kills / TotalRoundsPlayed, deaths / TotalRoundsPlayed, Damage / TotalRoundsPlayed, assists / TotalRoundsPlayed);

                cts.Add(new CommandUtils.PlayerStats()
                {
                    Name = player.PlayerName,
                    CompRank = player.CompetitiveRanking,
                    CrosshairCode = player.CrosshairCodes,
                    kills = kills,
                    deaths = deaths,
                    assists = assists,
                    KDD = kills - deaths,
                    KDR = string.Format("{0:0.##}", kd),
                    ADR = string.Format("{0:0.#}", Damage / TotalRoundsPlayed),
                    HSPercentage = string.Format("{0:0}", Playerstats.HeadShotKills / kills * 100) + "%",
                    KAST = string.Format("{0:0.#}", KAST) + "%",
                    Rating = string.Format("{0:0.##}", rating.rating),
                    EF = Playerstats.EnemiesFlashed,
                    FlashAssists = flashAssists,
                    EBT = string.Format("{0:0.#}", EnemyBlindTime) + "s",
                    UD = Playerstats.UtilityDamage,
                    FKD = FirstDeathDifference,
                    TradeKills = TradeKills,
                    Clutches = 0,
                    Multikills = Playerstats.Enemy3Ks + Playerstats.Enemy4Ks + Playerstats.Enemy5Ks
                });
            }
            string FinalResult = "";
            for (int i = 0; i < cts.Count; i++)
            {
                FinalResult += cts[i].Name + " " + cts[i].kills + " " + cts[i].assists + " " + cts[i].deaths + " " + cts[i].KDD + " " + cts[i].KDR + " " + cts[i].ADR + " " + cts[i].HSPercentage + " " + cts[i].KAST + " " + cts[i].Rating + " "
                            + cts[i].EF + " " + cts[i].FlashAssists + " " + cts[i].EBT + " " + cts[i].UD + " " + cts[i].FKD + " " + cts[i].TradeKills + " " + cts[i].Clutches + " " + cts[i].Multikills + "\n";
            }
            await Context.Interaction.FollowupAsync(FinalResult);
        }
    }

}