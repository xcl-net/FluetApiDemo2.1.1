using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluetApiNetFrameworkDemo2._1._1
{
    public class Blog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
        public DateTime? CreatedTime { get; set; }
        public double Double { get; set; }
        public float Float { get; set; }

    }
}
