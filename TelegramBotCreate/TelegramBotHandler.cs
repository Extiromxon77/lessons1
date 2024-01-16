using System.Security.Cryptography.X509Certificates;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotCreate
{
    public class TelegramBotHandler
    {
        public string Token { get; set; }
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
                MessageType.Video =>HandleVideoMessageAsync(botClient, update, cancellationToken),
                MessageType.Photo => HandleVideoMessageAsync(botClient, update, cancellationToken),
                MessageType.Audio => HandleAudioMessageAsync(botClient, update, cancellationToken),
                _ => HandleUnknownMessageTypeAsync(update, update, cancellationToken),
            };
        }

        public async Task HandleVideoMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message sendVideo = await botClient.SendVideoAsync(
                chatId: update.Message.Chat.Id,
                video: InputFile.FromUri("https://img.freepik.com/free-photo/abstract-glowing-flame-drops-electric-illumination-generative-ai_188544-8092.jpg"),
                cancellationToken: cancellationToken);
        }

        public async Task HandlePhotoMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message sendPhoto = await botClient.SendPhotoAsync(
                chatId: update.Message.Chat.Id,
                photo: InputFile.FromUri("https://img.freepik.com/free-photo/abstract-glowing-flame-drops-electric-illumination-generative-ai_188544-8092.jpg"),
                cancellationToken: cancellationToken);

        }

        public async Task HandleAudioMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message sendAudio = await botClient.SendAudioAsync(
                chatId: update.Message.Chat.Id,
                audio: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3"),
                cancellationToken: cancellationToken);

        }
        public async Task HandleTextMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message sendText = await botClient.SendTextMessageAsync(
               chatId: update.Message.Chat.Id,
               text: "You said:\n" + update.Message.Text,
               cancellationToken: cancellationToken);
        }
        public Task HandleEditedMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task HandleUnknownUpdateType(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task HandleUnknownMessageTypeAsync(Update update1, Update update2, CancellationToken cancellationToken)
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