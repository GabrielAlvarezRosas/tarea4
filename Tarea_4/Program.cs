using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Tarea_4;
using static Telegram.Bot.TelegramBotClient;

class Program
{
    static async Task Main(string[] args)
    {
        var botClient = new TelegramBotClient("8514664837:AAF_mUXvCoV6YKeofu4aLwCyUJ_FgCglpLs");
        using var cts = new CancellationTokenSource();

        var me = await botClient.GetMe();
        Console.WriteLine($"Bot conectado: {me.FirstName} (ID: {me.Id})");
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }
        };

        botClient.StartReceiving(
        BotHandlers.HandleUpdateAsync,
            BotErrorHandler.HandleErrorAsync,
            receiverOptions,
            cancellationToken: cts.Token
        );

        Console.WriteLine(" Bot escuchando... Presiona Enter para detener.");
        Console.ReadLine();
        cts.Cancel();

    }
}