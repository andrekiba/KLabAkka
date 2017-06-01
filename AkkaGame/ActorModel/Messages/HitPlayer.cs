using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
