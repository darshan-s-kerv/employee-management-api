using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementApi.Repository.Interfaces
{
    public interface ITokenRepository
    {
        public Task<string> CreateJwtToken(IdentityUser user);
    }
}
