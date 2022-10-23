using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EaseLogMeIn.DatabaseLayer
{

    public interface IWebsiteAccessContext
    {
        int Add(WebsiteAccessLog website);
        List<WebHistory> Search(SearchAccessLog history, out int TotalCount);
    }
    public class WebsiteAccessContext : IWebsiteAccessContext
    {
        readonly EaseLogMeInDbContext easeLogMeIn = new EaseLogMeInDbContext();
        public int Add(WebsiteAccessLog website)
        {
            easeLogMeIn.WebsiteAccessLog.Add(website);
            return easeLogMeIn.SaveChanges();
        }
        public List<WebHistory> Search(SearchAccessLog param, out int TotalCount)
        {
            List<WebHistory> histories = new List<WebHistory>();
            var history = (from l in easeLogMeIn.WebsiteAccessLog
                           join E in easeLogMeIn.Employee
                           on l.UserId equals E.UniqueId
                           join W in easeLogMeIn.Website
                           on l.WebsiteId equals W.UniqueId
                           where l.UserId == param.UserId || param.UserId == Guid.Empty
                           where l.WebsiteId == param.WebsiteId || param.WebsiteId == Guid.Empty
                           where l.AccessTime >= param.FromDate && l.AccessTime <= param.ToDate
                           select new
                           {
                               E.Name,
                               l.AccessTime,
                               W.Salt,
                               l.IPAddress,
                               WebName = W.Name,
                               WebUser = W.UserId
                           }).ToList();

            int record = (param.PageSize * param.PageIndex);
            TotalCount = history.Count();
            var data = history
                .OrderByDescending(t => t.AccessTime)
                .Skip(record)
                .Take(param.PageSize).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                histories.Add(new WebHistory
                {
                    AccessTime = data[i].AccessTime,
                    Name = data[i].Name,
                    WebName = data[i].WebName,
                    WebUser =Security.Decrypt(data[i].WebUser, data[i].Salt),
                    IPAddress = data[i].IPAddress
                });
            }
            return histories;
        }
    }
}
