using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.DatabaseLayer
{
    public interface IDesktopAppLoginHistoryContext
    {
        int AddLogin(DesktopAppLoginHistory history);
        void UpdateLogOut(DesktopAppLoginHistory history);
        void UpdateVisitCount(int WebId);
        List<DesktopAppLoginHistory> Search(SearchAccessLog history, out int TotalCount);
    }
    public class DesktopAppLoginHistoryContext : IDesktopAppLoginHistoryContext
    {
        readonly EaseLogMeInDbContext easeLogMeIn = new EaseLogMeInDbContext();
        public int AddLogin(DesktopAppLoginHistory history)
        {
            history = easeLogMeIn.DesktopAppLoginHistory.Add(history);
            easeLogMeIn.SaveChanges();
            return history.Id;
        }
        public void UpdateLogOut(DesktopAppLoginHistory history)
        {
            DesktopAppLoginHistory temp = easeLogMeIn.DesktopAppLoginHistory.FirstOrDefault(h => h.Id == history.Id);
            if (temp != null)
            {
                DateTime login = temp.LoginTime;
                temp.LogoutTime = DateTime.Now.IST();
                TimeSpan t = temp.LogoutTime.Value - temp.LoginTime;
                temp.TimeSpend = string.Format("{0}d, {1}:{2}:{3}", t.Days, t.Hours < 10 ? ("0" + t.Hours) : t.Hours.ToString(), t.Minutes < 10 ? ("0" + t.Minutes) : t.Minutes.ToString(), t.Seconds < 10 ? ("0" + t.Seconds) : t.Seconds.ToString());
                
                easeLogMeIn.SaveChanges();
            }
        }

       public void UpdateVisitCount(int WebId)
        {
            DesktopAppLoginHistory temp = easeLogMeIn.DesktopAppLoginHistory.FirstOrDefault(h => h.Id == WebId);
            if (temp != null)
            {
                int count = temp.VisitedWebCounts;
                temp.VisitedWebCounts = count + 1; 
                easeLogMeIn.SaveChanges();
            }
        }
        public List<DesktopAppLoginHistory> Search(SearchAccessLog param, out int TotalCount)
        {
            int record = (param.PageSize * param.PageIndex);
            var history = easeLogMeIn.DesktopAppLoginHistory
                .Where(l => (l.UniqueId == param.UserId || param.UserId == Guid.Empty)
                && l.LoginTime >= param.FromDate && l.LoginTime <= param.ToDate)
                .OrderByDescending(t => t.LoginTime)
                .Skip(record)
                .Take(param.PageSize).ToList();

            TotalCount = easeLogMeIn.DesktopAppLoginHistory
                .Where(l => (l.UniqueId == param.UserId || param.UserId == Guid.Empty)
                && l.LoginTime >= param.FromDate && l.LoginTime <= param.ToDate).Count();
            return history;
        }
    }
}
