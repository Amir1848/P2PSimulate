using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulatedNode.Models
{
    public class SimulateException : Exception
    {
        public SimulateException(string message): base(message)
        {

        }
    }
}
