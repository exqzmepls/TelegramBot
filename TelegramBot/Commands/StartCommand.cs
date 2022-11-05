using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands
{
    public class StartCommand : ICommand
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public StartCommand(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public async Task ExecuteAsync(Message requestMessage)
        {
            var chat = requestMessage.Chat;
            var chatId = chat.Id;
            var text = "Welcome! Use commands presented below.";
            await _telegramBotClient.SendTextMessageAsync(chatId, text);
        }

        public bool IsRequestedByMessage(string? text)
        {
            var trimmedText = text?.Trim();
            var isRequested = trimmedText == "/start";
            return isRequested;
        }
    }
}
