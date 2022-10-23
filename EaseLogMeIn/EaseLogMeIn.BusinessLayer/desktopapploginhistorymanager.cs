using EaseLogMeIn.DatabaseLayer;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.BusinessLayer
{
    public interface IDesktopAppLoginHistoryManager
    {
        int AddLogin(DesktopAppLoginHistory history);
        void UpdateLogOut(DesktopAppLoginHistory history);
        void UpdateVisitCount(int WebId);
        List<DesktopAppLoginHistory> Search(SearchAccessLog history, out int TotalCount);
    }
    public class DesktopAppLoginHistoryManager : IDesktopAppLoginHistoryManager
    {
        private readonly IDesktopAppLoginHistoryContext desktopAppLogin;
        public DesktopAppLoginHistoryManager(IDesktopAppLoginHistoryContext desktopApp)
        {
            desktopAppLogin = desktopApp;
        }
        public int AddLogin(DesktopAppLoginHistory website)
        {
         return   desktopAppLogin.AddLogin(website);
        }
        public void UpdateLogOut(DesktopAppLoginHistory history)
        {
            desktopAppLogin.UpdateLogOut(history);
        }
        public void UpdateVisitCount(int WebId)
        {
            desktopAppLogin.UpdateVisitCount(WebId);
        }
        public List<DesktopAppLoginHistory> Search(SearchAccessLog history, out int TotalCount)
        {
            return desktopAppLogin.Search(history, out TotalCount);
        }
    }

}
