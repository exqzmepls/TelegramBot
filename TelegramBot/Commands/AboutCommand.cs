using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands
{
    public class AboutCommand : ICommand
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public AboutCommand(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public async Task ExecuteAsync(Message requestMessage)
        {
            var chatId = requestMessage.Chat.Id;
            var aboutText = $"List of available commands:";
            await _telegramBotClient.SendTextMessageAsync(chatId, aboutText);
        }

        public bool IsRequestedByMessage(string? text)
        {
            var trimmedText = text?.Trim();
            var isRequested = trimmedText == "/about";
            return isRequested;
        }
    }
}
