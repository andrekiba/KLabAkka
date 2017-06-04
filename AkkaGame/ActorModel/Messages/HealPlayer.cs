namespace AkkaGame.ActorModel.Messages
{
    public class HealPlayer
    {
        public int Care { get; }

        public HealPlayer(int care)
        {
            Care = care;
        }
    }
}
