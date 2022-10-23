using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.DatabaseLayer
{
    public interface IWebsiteUserMappingContext
    {
        int Add(List<WebsiteUserMapping> mappings, MappingType mappingType);
        int Update(List<WebsiteUserMapping> mappings);
        List<WebsiteUserMapping> GetByUser(Guid UserId);
        List<WebsiteUserMapping> GetByWebsite(Guid WebsiteId);
    }
    public class WebsiteUserMappingContext : IWebsiteUserMappingContext
    {
        readonly EaseLogMeInDbContext easeLogMeIn = new EaseLogMeInDbContext();

        public int Add(List<WebsiteUserMapping> mappings, MappingType mappingType)
        {
            if (mappings == null || mappings.Count == 0) { throw new ArgumentNullException("mappings", "mappings is null"); }
            if (mappingType == MappingType.User)
            {
                var UserId = mappings[0].UserId;
                var exist = easeLogMeIn.WebsiteUserMapping.Where(w => w.UserId == UserId);
                if (exist.Count() > 0) { easeLogMeIn.WebsiteUserMapping.RemoveRange(exist); }
            }
           else if (mappingType == MappingType.Website)
            {
                var WebsiteId = mappings[0].WebsiteId;
                var exist = easeLogMeIn.WebsiteUserMapping.Where(w => w.WebsiteId == WebsiteId);
                if (exist.Count() > 0) { easeLogMeIn.WebsiteUserMapping.RemoveRange(exist); }
            }
            easeLogMeIn.WebsiteUserMapping.AddRange(mappings);
            return easeLogMeIn.SaveChanges();
        }

        public List<WebsiteUserMapping> GetByUser(Guid UserId)
        {
            return easeLogMeIn.WebsiteUserMapping.Where(w => w.UserId == UserId).ToList();
        }

        public List<WebsiteUserMapping> GetByWebsite(Guid WebsiteId)
        {
            return easeLogMeIn.WebsiteUserMapping.Where(w => w.WebsiteId == WebsiteId).ToList();
        }

        public int Update(List<WebsiteUserMapping> mappings)
        {
            int result = 0;
            if (mappings == null) { throw new ArgumentNullException("mappings", "mappings is null"); }
            for (int i = 0; i < mappings.Count; i++)
            {
                Guid webId = mappings[i].WebsiteId;
                Guid UId = mappings[i].UserId;
                var mapping = easeLogMeIn.WebsiteUserMapping.FirstOrDefault(q => q.UserId == UId && q.WebsiteId == webId);
                if (mapping != null)
                {
                    easeLogMeIn.SaveChanges();
                }
                easeLogMeIn.WebsiteUserMapping.Add(mappings[i]);
                result += easeLogMeIn.SaveChanges();
            }
            return result;
        }
    }
}
