namespace AkkaGame.ActorModel.Messages
{
    public class SimulateError
    {
        public bool Simulate { get; }

        public SimulateError(bool simulate)
        {
            Simulate = simulate;
        }
    }
}
