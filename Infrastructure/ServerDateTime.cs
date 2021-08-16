using System;

namespace Infrastructure
{
    public class ServerDateTime : IServerDateTime
    {
        public DateTime Now    => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}