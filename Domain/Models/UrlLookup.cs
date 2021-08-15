using System;

namespace Domain.Models
{
    public class UrlLookup
    {
        public string Key { get; set; }
        public string Url { get; set; }

        protected bool Equals(UrlLookup other)
        {
            return Key == other.Key && Url == other.Url;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((UrlLookup)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Url);
        }
    }
}