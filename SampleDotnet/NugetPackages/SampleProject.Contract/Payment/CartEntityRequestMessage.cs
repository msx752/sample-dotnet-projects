using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Contract.Payment
{
    public class CartEntityRequestMessage : IRequestMessage
    {
        public string ActivityId { get; set; }
        public Guid ActivityUserId { get; set; }
        public Guid CartId { get; set; }
    }
}