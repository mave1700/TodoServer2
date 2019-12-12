using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public User GetUserWithDetails(Guid userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
                .Include(ts => ts.Tasks)
                .FirstOrDefault();
        }

        public User GetUserByUsername(string userName)
        {
            return FindByCondition(user => user.Username.Equals(userName))
                .FirstOrDefault();
        }
    }
}
