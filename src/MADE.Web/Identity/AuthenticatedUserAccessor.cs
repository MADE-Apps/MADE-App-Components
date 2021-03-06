namespace MADE.Web.Identity
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Defines an accessor for retrieving the authenticated user from a <see cref="IHttpContextAccessor"/>.
    /// </summary>
    public class AuthenticatedUserAccessor : IAuthenticatedUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedUserAccessor"/> class with an instance of the <see cref="IHttpContextAccessor"/>.
        /// </summary>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        public AuthenticatedUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the authenticated user's claims principal.
        /// </summary>
        public ClaimsPrincipal ClaimsPrincipal => this.httpContextAccessor.HttpContext.User;
    }
}