using BFH.EADN.QuizManagementService.Contracts;
using BFH.EADN.UI.Web.Models;
using BFH.EADN.UI.Web.Utils;
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
            new User
            {
                Id = Guid.NewGuid(),
                UserName = "Admin",
                Password = "AAorcLme9Z/b9oJF5rbRcchQyM+j+SkjkOldeEIVXTx/eT4b6eDQmbyyhifxsqIYBw=="
            }
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
        //{
            //return Task<User>.Factory.StartNew(
            //    () =>
            //    {
            //        ISession session = ClientProxy.GetProxy<ISession>();
            //        Common.Types.Contracts.User user = session.GetUserByName(userName);
            //        User retUser = null;
            //        if (user != null)
            //        {
            //            retUser = new User
            //            {
            //                Id = user.Id,
            //                Password = user.Password,
            //                UserName = user.Name
            //            };
            //        }
            //        return retUser;
            //    });
        //}

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task<string>.Factory.StartNew(() => Users.Single(account => account.Id == user.Id).Password);
            return Task<string>.Factory.StartNew(() => 
            {
                ISession session = ClientProxy.GetProxy<ISession>();
                Common.Types.Contracts.User contractUser = session.GetUserById(user.Id);
                if(user == null)
                {
                    throw new InvalidOperationException("no user found with id");
                }

                return user.Password;                
            });
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