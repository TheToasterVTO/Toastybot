

namespace Toastybot.commands
{
    // public class testCommands : BaseCommandModule
    // {
    //     [Command("test")]
    //     public async Task Test(CommandContext ctx)
    //     {
    //         await ctx.Channel.SendMessageAsync("real");
    //     }

    //     [Command("add")]
    //     public async Task Add(CommandContext ctx, int num1, int num2)
    //     {
    //         int result = num1 + num2;

    //         await ctx.Channel.SendMessageAsync(result.ToString());
    //     }

    //     [Command("embed")]
    //     public async Task EmbedMessage(CommandContext ctx)
    //     {
    //         var message = new DiscordEmbedBuilder
    //         {
    //             Title = "real",
    //             Description = $"moment {ctx.User.Username}",
    //             Color = DiscordColor.Red,
    //         };

    //         await ctx.Channel.SendMessageAsync(embed: message);

    //     }

    //     [Command("image")]
    //     public async Task Image(CommandContext ctx)
    //     {
    //         var messagebuilder = new DiscordMessageBuilder();
    //         FileStream fs = File.Open("Image\\a81ed59fe78e69c6223dcf9472bc0efa_webp.png", FileMode.Open);
            
    //         messagebuilder.AddFile("Image.png",fs);
            
    //         var message = new DiscordEmbedBuilder
    //         {
    //             Title = "image",
    //             ImageUrl = Formatter.AttachedImageUrl("Image.png")
    //         };
    //         messagebuilder.AddEmbed(message);
    //         await ctx.Channel.SendMessageAsync(messagebuilder);

    //     }

    //     [Command("gif")]
    //     public async Task Gif(CommandContext ctx)
    //     {
    //         var messagebuilder = new DiscordMessageBuilder();
    //         Random random = new Random();
    //         var files = Directory.GetFiles("Image", "*.gif");
    //         var num = random.Next(files.Length);
    //         var file = files[num];
    //         using (FileStream fs = File.Open(file, FileMode.Open)){
    //             messagebuilder.AddFile("Image.gif",fs);
            
    //         var message = new DiscordEmbedBuilder
    //         {
    //             Title = "gif",
    //             ImageUrl = Formatter.AttachedImageUrl("Image.gif")
    //         };
    //         messagebuilder.AddEmbed(message);
    //         await ctx.Channel.SendMessageAsync(messagebuilder);
    //        } 
    //     }

    //     [Command("inter")]
    //     public async Task Interactivity(CommandContext ctx)
    //     {
    //         var Interactivity = ToastyBot.Client.GetInteractivity();

    //         var messageToRetrive = await Interactivity.WaitForMessageAsync(m => m.Content == "Hello");

    //         await ctx.Channel.SendMessageAsync($"{ctx.User.Username} said Hello");
    //     }

    //     [Command("inter1")]
    //     public async Task Interactivity1(CommandContext ctx)
    //     {
    //         var Interactivity = ToastyBot.Client.GetInteractivity();

    //         var messageToReact = await Interactivity.WaitForReactionAsync(m => m.Message.Id == 1281469726299656284);
    //         if (messageToReact.Result.Message.Id == 1281469726299656284)
    //         {
    //             await ctx.Channel.SendMessageAsync($"{ctx.User.Username} used the emoji with name {messageToReact.Result.Emoji.Name}");

    //         }

    //     }
    // }
}