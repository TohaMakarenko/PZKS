using System;

namespace DataFlow
{
    public class Command
    {
        public int Id { get; set; }

        public CommandType Type { get; set; }
        public bool IsActive { get; set; }
        public OperantOrder Order { get; set; }
        public int NextCommandId { get; set; }
    }
}
