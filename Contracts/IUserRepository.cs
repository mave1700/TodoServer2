using Entities.Models;
using System;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        void CreateUser(User user);
        User GetUserWithDetails(Guid userId);
        User GetUserWithDetails(string userName);

        User GetUser(Guid userId);
        User GetUser(string userName);

    }
}
