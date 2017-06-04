using System;
using Akka.Actor;
using AkkaGame.ActorModel.Exceptions;
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

            ColorConsole.WriteViolet($"PlayerActor '{playerName}' created");

            Normal();
        }

        #region Behavior

        private void Normal()
        {
            Receive<HitPlayer>(message => HitPlayer(message));
            Receive<HealPlayer>(message => HealPlayer(message));
            Receive<DisplayStatus>(message => DisplayPlayerStatus());
            Receive<SimulateError>(message => SimulateError(message));
            ColorConsole.WriteOrange($"{playerName} has now become Normal");
        }

        private void Critical()
        {
            Receive<HitPlayer>(message => HitPlayer(message));
            Receive<HealPlayer>(message => HealPlayer(message));
            Receive<DisplayStatus>(message => DisplayPlayerStatus());
            Receive<SimulateError>(message => SimulateError(message));
            ColorConsole.WriteOrange($"{playerName} has now become Critical");
        }

        #endregion

        #region Lifecycle

        protected override void PreStart()
        {
            ColorConsole.WriteViolet($"{playerName} PreStart");

            base.PreStart();
        }

        protected override void PostStop()
        {
            ColorConsole.WriteViolet($"{playerName} PostStop");

            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteViolet($"{playerName} PreRestart");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteViolet($"{playerName} PostRestart");

            base.PostRestart(reason);
        }

        #endregion

        #region Methods

        private void HitPlayer(HitPlayer message)
        {           
            health -= message.Damage;

            ColorConsole.WriteGreen($"{playerName} hit, current health is {health}");          

            if (health < 30)
                Become(Critical);
        }

        private void HealPlayer(HealPlayer message)
        {
            health += message.Care;

            ColorConsole.WriteGreen($"{playerName} healed, current health is {health}");            

            if (health >= 30)
                Become(Normal);
        }

        private void DisplayPlayerStatus()
        {
            ColorConsole.WriteGreen($"{playerName} has {health} health");
        }

        private void SimulateError(SimulateError message)
        {
            if(message.Simulate)
                throw new SimulatedException($"Simulated exception in player: {playerName}");
            
           throw new ApplicationException($"Application exception in player: {playerName}");
        }

        #endregion

    }
}
