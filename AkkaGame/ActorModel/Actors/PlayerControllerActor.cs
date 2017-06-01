using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaGame.ActorModel.Messages;

namespace AkkaGame.ActorModel.Actors
{
    public class PlayerControllerActor : ReceiveActor
    {
        private const int defaultStartingHealth = 100;

        public PlayerControllerActor()
        {
            //ColorConsole.WriteGreen("Creating a PlayerControllerActor");

            Receive<CreatePlayer>(message => HandleCreatePlayerMessage(message));
        }

        private static void HandleCreatePlayerMessage(CreatePlayer message)
        {
            Context.ActorOf(Props.Create(() => new PlayerActor(message.PlayerName, defaultStartingHealth)), message.PlayerName);
            ColorConsole.WriteGreen($"PlayerActor '{message.PlayerName}' created");
        }
    }
}
