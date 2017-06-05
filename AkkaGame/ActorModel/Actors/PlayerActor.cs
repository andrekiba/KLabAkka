using System;
using Akka.Actor;
using AkkaGame.ActorModel.Messages;

namespace AkkaGame.ActorModel.Actors
{
    public class PlayerActor : ReceiveActor
    {
        private readonly string playerName;
        private int health;

        public PlayerActor(string playerName, int startingHealth)
        {
            this.playerName = playerName;
            this.health = startingHealth;

            Receive<HitPlayer>(message => HitPlayer(message));
            Receive<HealPlayer>(message => HealPlayer(message));
            Receive<DisplayStatus>(message => DisplayPlayerStatus());
            Receive<SimulateError>(message => SimulateError());

            ColorConsole.WriteViolet($"PlayerActor '{playerName}' created");
        }

        #region Methods

        private void HitPlayer(HitPlayer message)
        {
            health -= message.Damage;

            ColorConsole.WriteGreen($"{playerName} hit, current health is {health}");          
        }

        private void HealPlayer(HealPlayer message)
        {
            health += message.Care;

            ColorConsole.WriteGreen($"{playerName} healed, current health is {health}");            
        }

        private void DisplayPlayerStatus()
        {
            ColorConsole.WriteGreen($"{playerName} has {health} health");
        }

        private void SimulateError()
        {
            throw new ApplicationException($"Simulated exception in player: {playerName}");
        }

        #endregion

    }
}
