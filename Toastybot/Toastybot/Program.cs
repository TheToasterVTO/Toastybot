using Discord.WebSocket;
using Toastybot.commands;
using Toastybot.config;
using Discord;
namespace Toastybot
{
    public class ToastyBot
    {
        public static DiscordSocketClient Client { get; set; }
        static async Task Main(string[] args)
        {
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();
            

            Client = new DiscordSocketClient();
            Client.Log += Log;
            await Client.LoginAsync(TokenType.Bot, jsonReader.token);
            await Client.StartAsync();
            await Task.Delay(-1);
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        // private static async Task MessageDeletedHandler(DiscordClient sender, MessageDeleteEventArgs e)
        // {
        //     ulong ChannelId = 1281589744567975989;
        //     var message = new DiscordEmbedBuilder
        //     {
        //         Title = $"{e.Message.Author.Username} deleted a message",
        //         Description = $"{e.Message.Content}\n\n {e.Message.Timestamp}"

        //     };
        //     await sender.SendMessageAsync(await sender.GetChannelAsync(ChannelId), embed: message);
        // }

        // private static async Task CommandErroredHandler(CommandsNextExtension sender, CommandErrorEventArgs e)
        // {

        //     var message = new DiscordEmbedBuilder
        //     {
        //         Title = ":x: An error has occured, tell Toasty about it",
        //         Description = e.Exception.Message,
        //         Color = DiscordColor.Red,
        //     };

        //     await e.Context.Channel.SendMessageAsync(embed: message);
        // }

        // // private static async Task MessageCreatedHandler(DiscordClient sender, MessageCreateEventArgs e)
        // // {

        // //     if (!e.Author.IsBot){
        // //         var message = new DiscordEmbedBuilder
        // //         {
        // //             Title = "real",
        // //             Description = $"{e.Author.Username} sent a message",
        // //         };
        // //         await sender.SendMessageAsync(await sender.GetChannelAsync(1281589744567975989),embed:message);
        // //     }
        // // }
        // private static async Task OnClient_Ready(DiscordClient sender, ReadyEventArgs e)
        // {
        //     var message = new DiscordMessageBuilder
        //     {
        //         Content = "✅ Bot has logged in!"
        //     };
        //     var user = await sender.GetUserAsync(409060256413384713);
        //     user.SendMessageAsync(message);
        // }
    }
}