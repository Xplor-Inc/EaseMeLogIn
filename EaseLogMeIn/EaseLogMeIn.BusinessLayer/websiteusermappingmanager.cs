using EaseLogMeIn.DatabaseLayer;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.BusinessLayer
{
    public interface IWebsiteUserMappingManager
    {
        int Add(List<WebsiteUserMapping> mappings, MappingType mappingType);
        int Update(List<WebsiteUserMapping> mappings);
        List<WebsiteUserMapping> GetByUser(Guid UserId);
        List<WebsiteUserMapping> GetByWebsite(Guid WebsiteId);
    }
    public class WebsiteUserMappingManager : IWebsiteUserMappingManager
    {
        readonly IWebsiteUserMappingContext context;
        public WebsiteUserMappingManager(IWebsiteUserMappingContext _context)
        {
            context = _context;
        }
        public int Add(List<WebsiteUserMapping> mappings, MappingType mappingType)
        {
            return context.Add(mappings, mappingType);
        }

        public List<WebsiteUserMapping> GetByUser(Guid UserId)
        {
            return context.GetByUser(UserId);
        }

        public List<WebsiteUserMapping> GetByWebsite(Guid WebsiteId)
        {
            return context.GetByWebsite(WebsiteId);
        }

        public int Update(List<WebsiteUserMapping> mappings)
        {
            return context.Update(mappings);
        }
    }

}
