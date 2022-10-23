using EaseLogMeIn.DatabaseLayer;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.BusinessLayer
{
    public interface IWebUsersManager
    {
        int Add(Users users);
        int Update(Users users);
        int Delete(Guid userId);
        List<Users> Get(string Name);
        int Activate(Guid userId);
        int DeActivate(Guid userId);
        Users Authenticate(string UserId, string Password);
        int ChangePassword(Guid UniqueId, string NewPassword, string OldPassword);

    }

    public class WebUsersManager : IWebUsersManager
    {
        readonly IWebUsersContext webUser;
        public WebUsersManager(IWebUsersContext webUsersContext)
        {
            webUser = webUsersContext;
        }
        public int Activate(Guid userId)
        {
            return webUser.Activate(userId);
        }
        public int Add(Users users)
        {
            return webUser.Add(users);
        }
        public Users Authenticate(string UserId, string Password)
        {
            return webUser.Authenticate(UserId, Password);
        }
        public int DeActivate(Guid userId)
        {
            return webUser.DeActivate(userId);
        }
        public int Delete(Guid userId)
        {
            return webUser.Delete(userId);
        }
        public List<Users> Get(string Name)
        {
            return webUser.Get(Name);
        }
        public int Update(Users users)
        {
            return webUser.Update(users);
        }
        public int ChangePassword(Guid UniqueId, string NewPassword, string OldPassword)
        {
            return webUser.ChangePassword(UniqueId, NewPassword, OldPassword);
        }

    }
}
