using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;
using AkkaGame.ActorModel.Messages;

namespace AkkaGame.ActorModel.Actors
{
    public class PlayerActor : ReceiveActor
    {
        //private readonly ILoggingAdapter logger = Context.GetLogger();
        private readonly string playerName;
        private int health;

        public PlayerActor(string playerName, int startingHealth)
        {
            this.playerName = playerName;
            this.health = startingHealth;

            //ColorConsole.WriteGreen($"{this.playerName} created");

            Receive<HitPlayer>(message => HitPlayer(message));
            Receive<DisplayStatus>(message => DisplayPlayerStatus());
            Receive<SimulateError>(message => SimulateError());
        }

        private void HitPlayer(HitPlayer message)
        {
            ColorConsole.WriteOrange($"{playerName} received HitPlayer");

            health -= message.Damage;

            ColorConsole.WriteOrange($"{playerName} hit, current health is {health}");

            //logger.Info("{Player} hit, current health is {Health}", playerName, health);
        }

        private void DisplayPlayerStatus()
        {
            ColorConsole.WriteOrange($"{playerName} received DisplayStatus");

            ColorConsole.WriteGreen($"{playerName} has {health} health");
        }

        private void SimulateError()
        {
            ColorConsole.WriteOrange($"{playerName} received SimulateError");

            throw new ApplicationException($"Simulated exception in player: {playerName}");
        }
    }
}
