using System;
using System.Linq;

namespace Application.Services
{
    public class KeyGeneratorService : IKeyGeneratorService
    {
        public string GenerateUniqueKey()
        {
            // TODO: Potentially improve key generation by hashing URL
            
            var guid = Guid.NewGuid().ToString();
            return string.Join("", guid.Take(8));
        }
    }
}