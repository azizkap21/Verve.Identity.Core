using Microsoft.AspNetCore.Identity;

namespace Verve.Identity.Core.Service
{
    public class NormolizationHandler : ILookupNormalizer
    {
        public string NormalizeName(string name)
            => name.NormalizedString();
        
        public string NormalizeEmail(string email)
            => email.NormalizedString("@");
    }
}