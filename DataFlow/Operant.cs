using System;

namespace DataFlow
{
    public class Operant
    {
        public OperantOrder Order { get; set; }
        public int NextCommandId { get; set; }
        public double Value { get; set; }
    }
}
