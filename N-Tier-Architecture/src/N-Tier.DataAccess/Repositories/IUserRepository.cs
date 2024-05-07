using N_Tier.Core.Entities.Identity;

namespace N_Tier.DataAccess.Repositories
{
    public interface IUserRepository
    {
        IQueryable<ApplicationUser> GetAll();

        Task<ApplicationUser> GetByIdAsync(string id);
    }
}
