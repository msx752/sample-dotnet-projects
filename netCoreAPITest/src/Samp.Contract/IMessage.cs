using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp.Contract
{
    public interface IMessage
    {
        public string ActivityId { get; set; }
        public Guid RequestUserId { get; set; }
    }
}