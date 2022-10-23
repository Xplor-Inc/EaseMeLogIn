using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.DatabaseLayer
{
    public interface IUserActionDataContext
    {
        int Add(UserActionData data);
        List<UserData> Search(UserActionSearch search, out int TotalCount);

    }
    public class UserActionDataContext : IUserActionDataContext
    {
        private readonly EaseLogMeInDbContext easeLogMeIn = new EaseLogMeInDbContext();

        public int Add(UserActionData data)
        {
            easeLogMeIn.UserActionData.Add(data);
            return easeLogMeIn.SaveChanges();
        }

        public List<UserData> Search(UserActionSearch param, out int TotalCount)
        {
            List<UserData> userData = new List<UserData>();
            var history = (from d in easeLogMeIn.UserActionData
                           join E in easeLogMeIn.Employee
                           on d.UserId equals E.UniqueId
                           join W in easeLogMeIn.Website
                           on d.WebsiteId equals W.UniqueId into d1
                           from q in d1.DefaultIfEmpty()
                           where d.CreateDate >= param.FromDate && d.CreateDate <= param.ToDate
                           where d.UserId == param.UserId || param.UserId == Guid.Empty
                           where d.WebsiteId == param.WebsiteId || param.WebsiteId == Guid.Empty
                           where d.Data == param.KeyWord || param.KeyWord == null
                           where d.ActionType == param.ActionType || param.ActionType == ActionType.Both

                           select new
                           {
                               E.Name,
                               d.CreateDate,
                               WebName = q.Name,
                               WebUser = q.UserId,
                               d.ActionType,
                               d.Data,
                               d.IsPasted,
                               q.Salt
                           }).ToList();

            int record = (param.PageSize * param.PageIndex);
            TotalCount = history.Count();
            var data = history
                .OrderByDescending(t => t.CreateDate)
                .Skip(record)
                .Take(param.PageSize).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                userData.Add(new UserData
                {
                    AccessTime = data[i].CreateDate,
                    EmployeeName = data[i].Name,
                    IsPasted = data[i].IsPasted,
                    WebName = data[i].WebName,
                    WebUser = data[i].WebUser == null ? data[i].WebUser : Security.Decrypt(data[i].WebUser, data[i].Salt),
                    Data = data[i].Data,
                    Action = data[i].ActionType
                });
            }
            return userData;
        }
    }
}
