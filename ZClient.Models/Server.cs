using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZClient.Models
{
    public class Server
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Database { get; set; }
        public uint Port { get; set; }

    }
}
