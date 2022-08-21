using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp.Contract.Payment.Cart
{
    public class CartStatusResponseMessage : IResponseMessage
    {
        public string BusErrorMessage { get; set; }
        public string ActivityId { get; set; }
        public Guid ActivityUserId { get; set; }
    }
}