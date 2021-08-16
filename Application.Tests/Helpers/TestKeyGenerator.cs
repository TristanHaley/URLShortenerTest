using System;
using System.Linq;

namespace Application.Tests.Helpers
{
    public static class TestKeyGenerator
    {
        public static string GetKey()
        {
            var guid = Guid.NewGuid().ToString();
            return string.Join("", guid.Take(8));
        }
    }
}