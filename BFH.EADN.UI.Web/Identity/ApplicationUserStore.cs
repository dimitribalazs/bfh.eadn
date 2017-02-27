using BFH.EADN.UI.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BFH.EADN.UI.Web.Identity
{
    public class ApplicationUserStore : IUserStore<User, Guid>, IUserPasswordStore<User, Guid>
    {

        public static List<User> Users = new List<User>()
        {
            // PW: Test@22
            new User { Id = Guid.NewGuid(), UserName = "Admin", Password = "AAorcLme9Z/b9oJF5rbRcchQyM+j+SkjkOldeEIVXTx/eT4b6eDQmbyyhifxsqIYBw==" }
        };

        #region IUserStore
        public Task CreateAsync(User user)
        {
            return Task.Factory.StartNew(() => Users.Add(user));
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // Currently no implementation
        }

        public Task<User> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task<User>.Factory.StartNew(
                () => Users.FirstOrDefault(account => string.Equals(account.UserName, userName, StringComparison.InvariantCultureIgnoreCase)
           ));
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task<string>.Factory.StartNew(() => Users.Single(account => account.Id == user.Id).Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            return Task.Factory.StartNew(() => user.Password = passwordHash);
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}