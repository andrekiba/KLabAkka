using Akka.Actor;
using AkkaGame.ActorModel.Exceptions;
using AkkaGame.ActorModel.Messages;

namespace AkkaGame.ActorModel.Actors
{
    public class PlayerControllerActor : ReceiveActor
    {
        private const int DefaultStartingHealth = 100;

        public PlayerControllerActor()
        {
            Receive<CreatePlayer>(message => HandleCreatePlayerMessage(message));
        }

        private static void HandleCreatePlayerMessage(CreatePlayer message)
        {
            Context.ActorOf(Props.Create(() => new PlayerActor(message.PlayerName, DefaultStartingHealth)), message.PlayerName);
        }

        #region Strategy

        protected override SupervisorStrategy SupervisorStrategy()
        {
            //return base.SupervisorStrategy();

            return new OneForOneStrategy(x =>
            {
                if(x is SimulatedException)
                    return Directive.Restart;

                return Directive.Stop;                
            });
        }

        #endregion
    }
}
