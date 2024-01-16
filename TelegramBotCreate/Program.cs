using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotCreate
{
    public class Program
    { 

        static async Task Main(string[] args)
        {
            const string MyBot = "6808479886:AAHQMWoqjaONXgyQQwGlbv07-0XM9RHYUCs";

            TelegramBotHandler handler = new TelegramBotHandler(MyBot);

            await handler.BotHandle();
  
            
        }
    }
}