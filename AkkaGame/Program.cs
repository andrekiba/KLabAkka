using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaGame.ActorModel.Actors;
using AkkaGame.ActorModel.Messages;
using Nito.AsyncEx;

namespace AkkaGame
{
    internal class Program
    {
        private static ActorSystem ActorSystem { get; set; }
        private static IActorRef PlayerController { get; set; }
        private static void Main(string[] args)
        {
            try
            {
                AsyncContext.Run(() => MainAsync(args));
            }
            catch (Exception ex)
            {
                ColorConsole.WriteRed(ex.Message);
            }
        }
        private static async Task MainAsync(string[] args)
        {
            ActorSystem = ActorSystem.Create("AkkaGame");

            PlayerController = ActorSystem.ActorOf<PlayerControllerActor>("PlayerController");

            DisplayInstructions();

            var stop = false;
            do
            {
                var action = Console.ReadLine();

                if (action == null)
                    continue;

                var playerName = action.Split(' ')[1];

                if (action.ToLowerInvariant().Contains("create"))
                {
                    CreatePlayer(playerName);
                }
                else if (action.ToLowerInvariant().Contains("hit"))
                {
                    var damage = int.Parse(action.Split(' ')[2]);
                    HitPlayer(playerName, damage);
                }
                else if (action.ToLowerInvariant().Contains("display"))
                {
                    DisplayPlayer(playerName);
                }
                else if (action.ToLowerInvariant().Contains("error"))
                {
                    ErrorPlayer(playerName);
                }
                else if (action.ToLowerInvariant().Contains("stop"))
                {
                    await StopSystem();
                    stop = true;
                }
                else
                {
                    ColorConsole.WriteRed("Unknown command");
                }
            } while (!stop);
        }

        private static void ErrorPlayer(string playerName)
        {
            ActorSystem.ActorSelection($"/user/PlayerController/{playerName}")
                  .Tell(new SimulateError());
        }

        private static void DisplayPlayer(string playerName)
        {
            ActorSystem.ActorSelection($"/user/PlayerController/{playerName}")
                  .Tell(new DisplayStatus());
        }

        private static void HitPlayer(string playerName, int damage)
        {
            ActorSystem.ActorSelection($"/user/PlayerController/{playerName}")
                  .Tell(new HitPlayer(damage));
        }

        private static void CreatePlayer(string playerName)
        {
            PlayerController.Tell(new CreatePlayer(playerName));
        }

        private static async Task StopSystem()
        {
            await ActorSystem.Terminate();
            ColorConsole.WriteGreen("Actor system shutdown");
            Console.ReadLine();
        }

        private static void DisplayInstructions()
        {
            Thread.Sleep(500);
            ColorConsole.WriteWhite("Available commands:");
            ColorConsole.WriteWhite("create\t<playername>");
            ColorConsole.WriteWhite("hit\t<playername>");
            ColorConsole.WriteWhite("display\t<playername>");
            ColorConsole.WriteWhite("error\t<playername>");
            ColorConsole.WriteWhite("stop");
            Console.WriteLine();
        }
    }
}
