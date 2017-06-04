using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaGame.ActorModel.Exceptions
{
    public class SimulatedException : Exception
    {
        public SimulatedException(string message) : base(message)
        {
            
        }
    }
}
