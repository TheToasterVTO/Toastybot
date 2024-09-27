using Discord.Commands;
using Discord.Interactions;
using DemoFile.Game.Cs;
using DemoFile.Game;
using DemoFile;
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
            string[,] strings = { { "Kast:",string.Format("{0:0.##}", kast) }, { "KPR:",string.Format("{0:0.##}", kpr) }, { "DPR:",string.Format("{0:0.##}", dpr) }, { "ADR:",string.Format("{0:0.##}", adr) },
                                { "APR:",string.Format("{0:0.##}", apr) }, { "Impact:",string.Format("{0:0.##}", rating.impact) }, { "Rating 2.0:", string.Format("{0:0.##}", rating.rating)} };
            
            string Stats = CommandUtils.StatFormat(strings);
            await Context.Interaction.RespondAsync("```\n"+Stats+"\n```");
        }

        
    }

}