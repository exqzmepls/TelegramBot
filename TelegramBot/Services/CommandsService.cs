using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands;

namespace TelegramBot.Services
{
    public class CommandsService : ICommandsService
    {
        private readonly ICommand _unknownCommand;
        private readonly IReadOnlyCollection<ICommand> _commands;

        public CommandsService(ITelegramBotClient telegramBotClient, IWeatherService weatherService)
        {
            _unknownCommand = new UnknownCommand(telegramBotClient);

            _commands = new ICommand[]
            {
                new StartCommand(telegramBotClient),
                new AboutCommand(telegramBotClient),
                new HelloCommand(telegramBotClient),
                new WeatherCommand(telegramBotClient, weatherService)
            };
        }

        public ICommand GetCommand(Message message)
        {
            var messageText = message.Text;
            var command = _commands.SingleOrDefault(c => c.IsRequestedByMessage(messageText));
            if (command != null)
                return command;

            return _unknownCommand;
        }
    }
}
