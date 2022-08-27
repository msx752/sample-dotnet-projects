using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp.Core.AppSettings
{
    public class RabbitMqOptions
    {
        public RabbitMqOptions()
        {
            Host = "localhost";
            Port = 5672;
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Durable { get; set; }
    }
}