using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Toastybot.commands;
using Toastybot.config;

namespace Toastybot
{
    public class ToastyBot
    {
        public static DiscordClient Client { get; set; }
        public static CommandsNextExtension Commands { get; set; }
        static async Task Main(string[] args)
        {
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            var discosrdConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discosrdConfig);

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            Client.Ready += OnClient_Ready;
            // Client.MessageDeleted
            Client.MessageCreated += MessageCreatedHandler;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = [jsonReader.prefix],
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.CommandErrored += CommandErroredHandler;

            Commands.RegisterCommands<testCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task CommandErroredHandler(CommandsNextExtension sender, CommandErrorEventArgs e)
        {

             var message = new DiscordEmbedBuilder
                {
                    Title = ":x: An error has occured",
                    Description = e.Exception.Message,
                };

             await e.Context.Channel.SendMessageAsync(message);
        }

        private static async Task MessageCreatedHandler(DiscordClient sender, MessageCreateEventArgs e)
        {
            
            if (!e.Author.IsBot){
                var message = new DiscordEmbedBuilder
                {
                    Title = "real",
                    Description = $"{e.Author.Username} sent a message",
                };

                await e.Channel.SendMessageAsync(embed: message);
            }
        }

        private static Task OnClient_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}