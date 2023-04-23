
namespace BlackDigital.Authentication
{
    public abstract class SecurityFilter
    {
        public SecurityFilter()
        {
            Roles = new List<string>();
        }

        public Id? UserId { get; protected set; }

        public List<string> Roles { get; protected set; }
    }
}
