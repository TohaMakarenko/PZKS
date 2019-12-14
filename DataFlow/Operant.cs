using System;

namespace DataFlow
{
    public class Operant
    {
        public double Value { get; set; }
        public OperantOrder Order { get; set; }
        public int NextCommandId { get; set; }
    }
}
