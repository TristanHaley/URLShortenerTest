using System;

namespace Infrastructure
{
    public interface IServerDateTime
    {
        public DateTime Now    { get; }
        public DateTime UtcNow { get; }
    }
}