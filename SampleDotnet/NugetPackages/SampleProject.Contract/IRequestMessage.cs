using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Contract
{
    public interface IRequestMessage
    {
        public string ActivityId { get; set; }
        public Guid ActivityUserId { get; set; }
    }
}