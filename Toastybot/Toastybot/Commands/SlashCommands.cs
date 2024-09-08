using Discord.Commands;
using Discord.Interactions;

namespace Toastybot.commands{

    public class SlashCommands:InteractionModuleBase{
        [SlashCommand("test","im losing my mind")]
        public async Task Test()
        {
            await Context.Interaction.RespondAsync("real");
        }
    }
}