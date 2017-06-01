using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaGame.ActorModel.Messages
{
    public class CreatePlayer
    {
        public string PlayerName { get; }

        public CreatePlayer(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
