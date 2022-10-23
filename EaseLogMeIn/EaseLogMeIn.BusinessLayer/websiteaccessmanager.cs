using EaseLogMeIn.DatabaseLayer;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.BusinessLayer
{
    public interface IWebsiteAccessManager
    {
        int Add(WebsiteAccessLog website);
        List<WebHistory> Search(SearchAccessLog history, out int TotalCount);
    }
    public class WebsiteAccessManager : IWebsiteAccessManager
    {
        private readonly IWebsiteAccessContext webAccessHistory;
        public WebsiteAccessManager(IWebsiteAccessContext websiteAccess)
        {
            webAccessHistory = websiteAccess;
        }
        public int Add(WebsiteAccessLog website)
        {
            return webAccessHistory.Add(website);
        }

        public List<WebHistory> Search(SearchAccessLog history, out int TotalCount)
        {
            List<WebHistory> webHistories = webAccessHistory.Search(history, out TotalCount);            
            return webHistories;
        }
    }
}
