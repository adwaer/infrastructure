using In.DataAccess.Entity.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Represents a user in the identity system.
    /// </summary>
    public class User : IdentityUser, IHasKey
    {
        public string Salt { get; set; }
        public string GoogleId { get; set; }
        public string FacebookId { get; set; }
        public string LinkedinId { get; set; }
    }
}