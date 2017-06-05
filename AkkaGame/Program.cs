using System;
using System.Text.RegularExpressions;
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
        #region Fields

        private static ActorSystem ActorSystem { get; set; }
        private static IActorRef PlayerController { get; set; }

        #endregion

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

                string playerName;

                if (Regex.IsMatch(action, @"create\s\w+"))
                {
                    playerName = action.Split(' ')[1];
                    CreatePlayer(playerName);
                }
                else if (Regex.IsMatch(action, @"hit\s\w+\s\d+"))
                {
                    playerName = action.Split(' ')[1];
                    var damage = int.Parse(action.Split(' ')[2]);
                    HitPlayer(playerName, damage);
                }
                else if (Regex.IsMatch(action, @"heal\s\w+\s\d+"))
                {
                    playerName = action.Split(' ')[1];
                    var care = int.Parse(action.Split(' ')[2]);
                    HealPlayer(playerName, care);
                }
                else if (Regex.IsMatch(action, @"display\s\w+"))
                {
                    playerName = action.Split(' ')[1];
                    DisplayPlayer(playerName);
                }
                else if (Regex.IsMatch(action, @"error\s\w+\s(0|1)"))
                {
                    playerName = action.Split(' ')[1];
                    var simulate = action.Split(' ')[2] == "1";
                    ErrorPlayer(playerName, simulate);
                }
                else if (Regex.IsMatch(action, @"poison\s\w+"))
                {
                    playerName = action.Split(' ')[1];
                    PoisonPillPlayer(playerName);
                }
                else if (action == "stop")
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

        #region Methods

        private static void ErrorPlayer(string playerName, bool simulate)
        {
            ActorSystem.ActorSelection($"/user/PlayerController/{playerName}")
                  .Tell(new SimulateError(simulate));
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

        private static void PoisonPillPlayer(string playerName)
        {
            ActorSystem.ActorSelection($"/user/PlayerController/{playerName}")
                  .Tell(PoisonPill.Instance);
        }

        private static void HealPlayer(string playerName, int care)
        {
            ActorSystem.ActorSelection($"/user/PlayerController/{playerName}")
                  .Tell(new HealPlayer(care));
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
            ColorConsole.WriteWhite("poison\t<playername>");
            ColorConsole.WriteWhite("stop");
            Console.WriteLine();
        }

        #endregion

    }
}
