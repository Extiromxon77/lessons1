using System.Security.Cryptography.X509Certificates;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegramvaluta
{
    public class TelegramBotHandler
    {
        public string Token { get; set; }
        public decimal Sum;
        public TelegramBotHandler(string token)
        {
            this.Token = token;
        }

        public async Task BotHandle()
        {
            var botClient = new TelegramBotClient($"{this.Token}");
           

            using CancellationTokenSource cts = new();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            // Send cancellation request to stop bot
            cts.Cancel();

        }


        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => HandleMessageAsync(botClient, update, cancellationToken),
                UpdateType.EditedMessage => HandleEditedMessageAsync(botClient, update, cancellationToken),
                _ => HandleUnknownUpdateType(botClient, update, cancellationToken),
            };


            try
            {
                await handler;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error chiqdi:{ex.Message}");
            }

        }

        public async Task HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            var message = update.Message;

            var handler = message.Type switch
            {
                MessageType.Text => HandleTextMessageAsync(botClient, update, cancellationToken),
                _ => HandleUnknownMessageTypeAsync(update, update, cancellationToken),

            };
        }

        public async Task HandleTextMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Message.Text == "USD")
            {
                //Echo received message text
                Message sendMessage = await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: $"{update.Message.Text} ni sumga o`tkizib beruvchi sayt",
                    parseMode: ParseMode.MarkdownV2,
                    disableNotification: true,
                    replyToMessageId: update.Message.MessageId,
                    replyMarkup: new InlineKeyboardMarkup(
                        InlineKeyboardButton.WithUrl(
                            text: "USD -> UZS",
                            url: "https://www.xe.com/currencyconverter/convert/?Amount=1&From=USD&To=UZS")),
                    cancellationToken: cancellationToken);
            }
            else if (update.Message.Text == "EUR")
            {
                //Echo received message text
                Message sendMessage = await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: $"{update.Message.Text} ni sumga o`tkizib beruvchi sayt",
                    replyToMessageId: update.Message.MessageId,
                    replyMarkup: new InlineKeyboardMarkup(
                        InlineKeyboardButton.WithUrl(
                            text: "EUR -> UZS",
                            url: "https://www.xe.com/currencyconverter/convert/?Amount=1&From=EUR&To=UZS")),
                    cancellationToken: cancellationToken);
            }
            else if (update.Message.Text == "RUBL")
            {
                //Echo received message text
                Message sendMessage = await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: $"{update.Message.Text} ni sumga o`tkizib beruvchi sayt",
                    parseMode: ParseMode.MarkdownV2,
                    disableNotification: true,
                    replyToMessageId: update.Message.MessageId,
                    replyMarkup: new InlineKeyboardMarkup(
                        InlineKeyboardButton.WithUrl(
                            text: "RUBL -> UZS",
                            url: "https://finance.rambler.ru/calculators/converter/1-RUB-UZS/")),
                    cancellationToken: cancellationToken);
            }

        }

        private Task HandleUnknownMessageTypeAsync(Update update1, Update update2, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        private Task HandleUnknownUpdateType(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private Task HandleEditedMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}