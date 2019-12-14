using Entities.Models;
using System;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserWithDetails(Guid userId);
        User GetUserWithDetails(string userName);

        User GetUser(Guid userId);
        User GetUser(string userName);

    }
}
