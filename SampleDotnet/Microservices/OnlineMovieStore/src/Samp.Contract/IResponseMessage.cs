using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp.Contract
{
    public interface IResponseMessage
    {
        string BusErrorMessage { get; set; }
    }
}