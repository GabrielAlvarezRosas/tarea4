using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Telegram.Bot.Extensions;



namespace Tarea_4
{
    public static class BotHandlers
    {
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message?.Text is not string messageText) return;

            var chatId = update.Message.Chat.Id;
            Console.WriteLine($"📩 Mensaje recibido: {messageText}");

            string response = messageText switch
            {
                "/start" => "¡Bienvenido! Usa /ayuda para ver comandos.",
                "/hora" => $"La hora actual es: {DateTime.Now:T}",
                var msg when msg.StartsWith("/eco") => msg.Replace("/eco", "").TrimStart(),
                "/ayuda" => "Comandos disponibles:\n/start\n/hora\n/eco <mensaje>\n/ayuda",
                _ => "No reconozco ese comando. Usa /ayuda."
            };

            await botClient.SendMessage(chatId, response, cancellationToken: cancellationToken);
        }


    }

}
