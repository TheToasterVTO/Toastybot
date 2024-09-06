using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Toastybot.config;

namespace Toastybot
{
    internal class ToastyBot
    {
        private static DiscordClient Client{get;set;}
        private static CommandsNextExtension Commands{get;set;}
        static async Task Main(string[] args){
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            var discosrdConfig = new DiscordConfiguration(){
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discosrdConfig);

            Client.Ready += Client_Ready;

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args){
            return Task.CompletedTask;
        }
    }
}