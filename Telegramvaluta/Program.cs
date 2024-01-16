using Telegram.Bot.Types;

namespace Telegramvaluta
{
    public class Program
    {

        static async Task Main(string[] args)
        {
            const string MyBot = "6815692764:AAGypf3Soqub5DPpbbEORyGnB8DAczx1q0o";

            TelegramBotHandler handler = new TelegramBotHandler(MyBot);


            try
            {
                await handler.BotHandle();
            }
            catch (Exception e)
            {
                throw new Exception();
            }

        }
    }
}
