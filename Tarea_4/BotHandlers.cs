using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Tarea_4
{
    public static class BotHandlers
    {
        // Diccionario para guardar estado de cada chat
        private static Dictionary<long, string> chatStates = new();

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message?.Text is not string messageText)
                return;

            var chatId = update.Message.Chat.Id;
            Console.WriteLine($"Mensaje recibido: {messageText}");


            // Si el usuario ya está en un estado
            if (chatStates.TryGetValue(chatId, out string state))
            {
                if (state == "esperando_nombre")
                {
                    string nombre = messageText.Trim();
                    await botClient.SendMessage(chatId, $"¡Hola {nombre}! Soy el bot de Alejo y Gabito, ¿cómo estás?", cancellationToken: cancellationToken);

                    chatStates[chatId] = "esperando_respuesta";
                    return; //  importantísimo: salir antes de ejecutar switch
                }

                if (state == "esperando_respuesta")
                {
                    if (messageText.ToLower().Contains("bien"))
                        await botClient.SendMessage(chatId, "¡Me alegro! 😄, para saber que puedo hacer usa /ayuda", cancellationToken: cancellationToken);
                    else if (messageText.ToLower().Contains("mal"))
                        await botClient.SendMessage(chatId, "Espero que todo mejore 🙏, si te puedo ayudar estaré encantado, para saber que puedo hacer usa /ayuda", cancellationToken: cancellationToken);
                    else
                        await botClient.SendMessage(chatId, "Interesante... muy interesante jaja, para saber que puedo hacer usa /ayuda", cancellationToken: cancellationToken);

                    chatStates.Remove(chatId); // fin del flujo
                    return; //  salir antes de ejecutar switch
                }
            }

            // Comandos normales
            string response = messageText.ToLower() switch
            {
                "/start" => "¡Bienvenido! Usa /ayuda para ver comandos.",
                "/hora" => $"La hora actual es: {DateTime.Now:T}",
                "/info" => "Este bot pertenece a Gabito y a Alejo y es su tarea 4, usa /ayuda para saber que puedo hacer",
                "/ayuda" => "Comandos disponibles:\n/start\n/hora\n/info\n/ayuda\n/saludar",
                "/saludar" => "¡Hola! ¿Cuál es tu nombre?",
                _ => "No reconozco ese comando. Usa /ayuda."
            };

            await botClient.SendMessage(chatId, response, cancellationToken: cancellationToken);

            // -------------------------------
            // Si inicia el saludo, cambiamos el estado
            if (messageText.ToLower() == "/saludar")
            {
                chatStates[chatId] = "esperando_nombre";
            }
            
        }
    }
}
