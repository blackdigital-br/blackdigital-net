
using BlackDigital.Model;

namespace BlackDigital.Authentication
{
    /// <summary>
    /// Base class for security filters that carry the authenticated user identifier and their roles.
    /// </summary>
    public abstract class SecurityFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityFilter"/> class with an empty role list.
        /// </summary>
        public SecurityFilter()
        {
            Roles = new List<string>();
        }

        /// <summary>
        /// Gets or sets the identifier of the authenticated user.
        /// </summary>
        public Id? UserId { get; protected set; }

        /// <summary>
        /// Gets or sets the list of roles assigned to the authenticated user.
        /// </summary>
        public List<string> Roles { get; protected set; }
    }
}
