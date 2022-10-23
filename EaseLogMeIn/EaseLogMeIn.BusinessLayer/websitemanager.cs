using EaseLogMeIn.DatabaseLayer;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.BusinessLayer
{
    public interface IWebsiteManager
    {
        List<Website> Get(string UserId);

        int Add(Website website, out Guid UserId);
        int Add(Website website);
        int Update(Website website);
        int Delete(Guid UniqueId);
        int Block(Guid UniqueId);
        int UnBlock(Guid UniqueId);
        List<Website> Get(string Name, int PageSize, int PageIndex, out int Count);
        List<MappingData> GetMapping();

        // Desktop Application API
        Website IsValidWebsite(Guid WebsiteId, Guid UserId);

    }

    public class WebsiteManager : IWebsiteManager
    {
        readonly IWebsiteContext webContext;
        public WebsiteManager(IWebsiteContext _website)
        {
            webContext = _website;
        }
        
        public int Add(Website website)
        {
            return webContext.Add(website);
        }
        public int Update(Website website)
        {
            return webContext.Update(website);
        }
        public List<Website> Get(string Name, int PageSize, int PageIndex, out int Count)
        {
            List<Website> temp = webContext.Get(Name, PageSize, PageIndex, out Count);
            for (int i = 0; i < temp.Count; i++)
            {
                string salt = temp[i].Salt;
                temp[i].Password = Security.Decrypt(temp[i].Password, salt);
                temp[i].UserId = Security.Decrypt(temp[i].UserId, salt);
            }
            return temp;
        }
        public int Delete(Guid UniqueId)
        {
            return webContext.Delete(UniqueId);
        }
        public int Block(Guid UniqueId)
        {
            return webContext.Block(UniqueId);
        }
        public int UnBlock(Guid UniqueId)
        {
            return webContext.UnBlock(UniqueId);
        }
        public List<MappingData> GetMapping()
        {
            return webContext.GetMapping();
        }
        public int Add(Website website, out Guid UserId)
        {
            return webContext.Add(website, out UserId);
        }

        public List<Website> Get(string UserId)
        {
            return webContext.Get(UserId);
        }

        public Website IsValidWebsite(Guid WebsiteId, Guid UserId)
        {
            return webContext.IsValidWebsite(WebsiteId, UserId);
        }
    }
}
