using System;
using System.Threading;
using OnitamaTestClient.Models.Enums;
using RemoteBotClient;

namespace OnitamaTestClient
{
    class Program {
        private const string ApiKey = "API_KEY";

        static void Main(string[] args)
        {
            var botInterface = RemoteBotClientInitializer.Init(ApiKey, forceLocal: false);

            var testbot = new TestBot(botInterface);
            testbot.Run();
        }
    }
}
