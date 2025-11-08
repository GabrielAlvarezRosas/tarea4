using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions;

namespace Tarea_4
{
    public static class BotErrorHandler
    {
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiEx => $"Telegram API Error: [{apiEx.ErrorCode}] {apiEx.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine($"Error: {errorMessage}");
            return Task.CompletedTask;
        }
    }
}
