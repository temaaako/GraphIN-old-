using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphIN
{
    public class DataMapper
    {
        public float X { get; set; }
        public float Y { get; set; }

        public DataMapper() { }
        public DataMapper(float x, float y) 
        {
            X = x;
            Y = y;
        }

    }
}
