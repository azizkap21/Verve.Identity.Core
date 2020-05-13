using Microsoft.AspNetCore.Identity;

namespace Verve.Identity.Core.Service
{
    public class NormolizationHandler : ILookupNormalizer
    {
        public string NormalizeName(string name)
        {
            return name.NormalizedString();
        }

        public string NormalizeEmail(string email)
        {
            return email.NormalizedString("@");
        }
    }
}