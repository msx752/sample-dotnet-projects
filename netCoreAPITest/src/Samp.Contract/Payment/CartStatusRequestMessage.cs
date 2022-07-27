using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp.Contract.Payment
{
    public class CartStatusRequestMessage : IRequestMessage
    {
        public Guid CartId { get; set; }

        /// <summary>
        /// CartStatus enum types; Open, LockedOnPayment
        /// </summary>
        public string CartStatus { get; set; }

        public string ActivityId { get; set; }
        public Guid ActivityUserId { get; set; }
    }
}