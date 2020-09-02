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

        public void CreateUser (User user)
        {
            Create(user);
        }

        public User GetUserWithDetails(Guid userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
                .Include(ts => ts.Tasks)
                .FirstOrDefault();
        }

        public User GetUserWithDetails(string userName)
        {
            return FindByCondition(user => user.Username.Equals(userName))
                .Include(ts => ts.Tasks)
                .FirstOrDefault();
        }

        public User GetUser(Guid userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
                .FirstOrDefault();
        }

        public User GetUser(string userName)
        {
            return FindByCondition(user => user.Username.Equals(userName))
                .FirstOrDefault();
        }
    }
}
