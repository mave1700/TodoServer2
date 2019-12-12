using Entities.Models;
using System;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserWithDetails(Guid userId);
        User GetUserByUsername(string userName);
    }
}
