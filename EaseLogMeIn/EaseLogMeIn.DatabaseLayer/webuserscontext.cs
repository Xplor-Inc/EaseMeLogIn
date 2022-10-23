using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.DatabaseLayer
{
    public interface IWebUsersContext
    {
        int Add(Users users);
        int Update(Users users);
        int Delete(Guid userId);
        int Activate(Guid userId);
        int DeActivate(Guid userId);
        List<Users> Get(string Name);
        Users Authenticate(string UserId, string Password);
        int ChangePassword(Guid UniqueId, string NewPassword, string OldPassword);
    }

    public class WebUsersContext : IWebUsersContext
    {
        readonly EaseLogMeInDbContext easeLogMeIn = new EaseLogMeInDbContext();
        public int Activate(Guid userId)
        {
            var temp = easeLogMeIn.WebUsers.FirstOrDefault(e => e.UniqueId == userId);
            if (temp != null)
            {
                temp.UpdateDate = DateTime.Now.IST();
                temp.UpdatedBy = AuthorizedUser.UserId;
                temp.IsActive = true;
                return easeLogMeIn.SaveChanges();
            }
            return 0;

        }
        public int Add(Users users)
        {
            var temp = easeLogMeIn.WebUsers.FirstOrDefault(e => e.UserId == users.UserId);
            if (temp == null)
            {
                users.Password = Security.Encrypt(ReadOnlyKeys.DefaultPassword, out string Salt);
                users.Salt = Salt;
                users.UniqueId = Guid.NewGuid();
                easeLogMeIn.WebUsers.Add(users);
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }

        public Users Authenticate(string UserId, string Password)
        {
            var temp = easeLogMeIn.WebUsers.FirstOrDefault(e => e.UserId == UserId && e.IsActive);
            if (temp != null)
            {
                Password = Security.Encrypt(Password, temp.Salt);
                if (temp.Password == Password)
                {
                    temp.LastActivityDate = DateTime.Now.IST();
                    easeLogMeIn.SaveChanges();
                    return temp;
                }
            }
            return null;
        }

        public int ChangePassword(Guid UniqueId, string NewPassword, string OldPassword)
        {
            var temp = easeLogMeIn.WebUsers.FirstOrDefault(e => e.UniqueId == UniqueId);
            if (temp != null)
            {
                NewPassword = Security.Encrypt(NewPassword, temp.Salt);
                OldPassword = Security.Encrypt(OldPassword, temp.Salt);
                if (temp.Password == OldPassword)
                {
                    temp.LastActivityDate = DateTime.Now.IST();
                    temp.Password = NewPassword;
                    return easeLogMeIn.SaveChanges();
                }
            }
            return 0;
        }

        public int DeActivate(Guid userId)
        {
            var temp = easeLogMeIn.WebUsers.FirstOrDefault(e => e.UniqueId == userId);
            if (temp != null)
            {
                temp.UpdateDate = DateTime.Now.IST();
                temp.UpdatedBy = AuthorizedUser.UserId;
                temp.IsActive = false;
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }
        public int Delete(Guid userId)
        {
            var temp = easeLogMeIn.WebUsers.FirstOrDefault(e => e.UniqueId == userId);
            if (temp != null)
            {
                temp.IsDeleted = true;
                temp.DeleteDate = DateTime.Now.IST();
                temp.DeletedBy = AuthorizedUser.UserId;
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }
        public List<Users> Get(string Name)
        {
            return easeLogMeIn.WebUsers.Where(e => e.Name.Contains(Name) && !e.IsDeleted).ToList();
        }
        public int Update(Users users)
        {
            var temp = easeLogMeIn.WebUsers.FirstOrDefault(e => e.UniqueId == users.UniqueId);
            if (temp != null)
            {
                temp.UpdateDate = DateTime.Now.IST();
                temp.UpdatedBy = AuthorizedUser.UserId;
                temp.Name = users.Name;
                temp.UserId = users.UserId;
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }
    }
}
