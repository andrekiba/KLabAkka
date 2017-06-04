namespace AkkaGame.ActorModel.Messages
{
    public class HitPlayer
    {
        public int Damage { get; }

        public HitPlayer(int damage)
        {
            Damage = damage;
        }
    }
}
