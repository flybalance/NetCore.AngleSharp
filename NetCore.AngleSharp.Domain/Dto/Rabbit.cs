using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.AngleSharpProgram.Domain.Dto
{
    public class Rabbit
    {
        public string Host { get; set; }

        public string VirtualHost { get; set; }

        public string Exchange { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
