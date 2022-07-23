using Samp.Contract.Cart.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp.Contract.Cart.Requests
{
    public class MovieEntityRequestMessage : IRequestMessage
    {
        public string ProductDatabase { get; set; }
        public string ProductId { get; set; }
        public string ActivityId { get; set; }
        public Guid ActivityUserId { get; set; }
    }
}