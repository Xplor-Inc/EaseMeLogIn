using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.DatabaseLayer
{

    public interface IWebsiteContext
    {
        List<Website> Get(string UserId);
        Website IsValidWebsite(Guid WebsiteId, Guid UserId);
        int Add(Website website);
        int Add(Website website, out Guid UserId);
        int Update(Website website);
        int Delete(Guid UniqueId);
        int Block(Guid UniqueId);
        int UnBlock(Guid UniqueId);
        List<Website> Get(string Name, int PageSize, int PageIndex, out int Count);
        List<MappingData> GetMapping();
    }
    public class WebsiteContext : IWebsiteContext
    {
        readonly EaseLogMeInDbContext easeLogMeIn = new EaseLogMeInDbContext();
        public int Add(Website website)
        {
            return AddWebsite(website, out _);
        }
        public int Add(Website website, out Guid WebsiteId)
        {
            return AddWebsite(website, out WebsiteId);
        }
        public int Block(Guid UniqueId)
        {
            var temp = easeLogMeIn.Website.FirstOrDefault(w => w.UniqueId == UniqueId);
            if (temp != null)
            {
                temp.UpdateDate = DateTime.Now.IST();
                temp.UpdatedBy = AuthorizedUser.UserId;
                temp.IsBlocked = true;
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }
        public int Delete(Guid UniqueId)
        {
            Website temp = easeLogMeIn.Website.FirstOrDefault(w => w.UniqueId == UniqueId);
            if (temp != null)
            {
                temp.IsDeleted = true;
                temp.IsActive = false;
                temp.DeleteDate = DateTime.Now.IST();
                temp.DeletedBy = AuthorizedUser.UserId;
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }
        public List<Website> Get(string Name, int PageSize, int PageIndex, out int Count)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return Search(PageSize, PageIndex, out Count);
            }
            return Search(Name, PageSize, PageIndex, out Count);
        }
        public int UnBlock(Guid UniqueId)
        {
            var temp = easeLogMeIn.Website.FirstOrDefault(w => w.UniqueId == UniqueId);
            if (temp != null)
            {
                temp.UpdateDate = DateTime.Now.IST();
                temp.UpdatedBy = AuthorizedUser.UserId;
                temp.IsBlocked = false;
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }
        public int Update(Website website)
        {
            if (website == null)
            {
                throw new ArgumentNullException("website", "Website object can not be null");
            }
            var temp = easeLogMeIn.Website.FirstOrDefault(w => w.UniqueId == website.UniqueId);
            if (temp != null)
            {
                var websites = easeLogMeIn.Website.Where(w => w.URL == website.URL && w.Name == website.Name && !w.IsDeleted && w.UniqueId != website.UniqueId);
                foreach (Website web in websites)
                {
                    string userId = Security.Decrypt(web.UserId, web.Salt);
                    if (userId == website.UserId)
                    {
                        return 0;
                    }
                }
                temp = easeLogMeIn.Website.FirstOrDefault(w => w.UniqueId == website.UniqueId);
                string salt = temp.Salt;
                temp.Name = website.Name;
                temp.URL = website.URL;
                temp.UserId = Security.Encrypt(website.UserId, salt);
                temp.Password = Security.Encrypt(website.Password, salt);
                temp.ConfigurationType = website.ConfigurationType;
                temp.UpdateDate = DateTime.Now.IST();
                temp.UpdatedBy = AuthorizedUser.UserId;
                temp.PasswordTextId = website.PasswordTextId;
                temp.UserIdTextId = website.UserIdTextId;
                temp.ButtonId = website.ButtonId;
                temp.Script = website.Script;
                return easeLogMeIn.SaveChanges();
            }
            return 0;
        }
        public List<MappingData> GetMapping()
        {
            List<MappingData> mappings = new List<MappingData>();
            var data = (from e in easeLogMeIn.Website
                        where !e.IsBlocked
                        where e.IsActive
                        select new
                        {
                            e.UniqueId,
                            e.Name,
                            e.UserId,
                            e.Salt
                        }).OrderBy(o => o.Name).ToList();

            for (int q = 0; q < data.Count; q++)
            {
                mappings.Add(new MappingData { Name = data[q].Name + " - " + Security.Decrypt(data[q].UserId, data[q].Salt), UniqueId = data[q].UniqueId });
            }
            return mappings;
        }
        private List<Website> Search(int PageSize, int PageIndex, out int Count)
        {
            int record = (PageSize * PageIndex);
            Count = easeLogMeIn.Website.Where(w => w.IsActive).Count();
            return easeLogMeIn.Website.Where(w => w.IsActive)
                .OrderBy(w => w.Name)
                .Skip(record)
                .Take(PageSize).ToList();
        }
        private List<Website> Search(string Name, int PageSize, int PageIndex, out int Count)
        {
            int record = (PageSize * PageIndex);
            Count = easeLogMeIn.Website.Where(w => w.IsActive && w.Name.Contains(Name)).Count();
            return easeLogMeIn.Website.Where(w => w.IsActive && w.Name.Contains(Name))
                .OrderBy(w => w.Name)
                .Skip(record)
                .Take(PageSize).ToList();
        }
        private int AddWebsite(Website website, out Guid WebsiteId)
        {
            if (website == null)
            {
                throw new ArgumentNullException(nameof(website), "Website object can not be null");
            }
            WebsiteId = Guid.NewGuid();
            var temp = easeLogMeIn.Website.Where(w => w.URL == website.URL && w.Name == website.Name && !w.IsDeleted);
            foreach (Website web in temp)
            {
                string userId = Security.Decrypt(web.UserId, web.Salt);
                if (userId == website.UserId)
                {
                    return 0;
                }
            }

            website.UserId = Security.Encrypt(website.UserId, out string Salt);
            website.Password = Security.Encrypt(website.Password, Salt);
            website.Salt = Salt;
            website.UniqueId = Guid.NewGuid();
            website.CreatedBy = AuthorizedUser.UserId;
            var web1 = easeLogMeIn.Website.Add(website);
            WebsiteId = web1.UniqueId;
            return easeLogMeIn.SaveChanges();
        }

        public List<Website> Get(string UserId)
        {
            var employee = easeLogMeIn.Employee.FirstOrDefault(e => e.UserId == UserId && e.IsActive);
            if (employee != null)
            {
                Guid id = employee.UniqueId;

                var webs = (from w in easeLogMeIn.Website
                            join m in easeLogMeIn.WebsiteUserMapping
                            on w.UniqueId equals m.WebsiteId
                            where m.UserId == id
                            where w.IsActive
                            select new
                            {
                                w.Salt,
                                w.ConfigurationType,
                                w.Name,
                                w.Password,
                                w.URL,
                                w.UserId,
                                w.UniqueId,
                                w.ButtonId,
                                w.Script
                            }).OrderBy(o => o.Name).ToList();
                var dto = new List<Website>();
                for (int i = 0; i < webs.Count; i++)
                {
                    dto.Add(new Website
                    {
                        ConfigurationType = webs[i].ConfigurationType,
                        Name = webs[i].Name,
                        Password = webs[i].Password,
                        URL = webs[i].URL,
                        UserId = webs[i].UserId,
                        Salt = webs[i].Salt,
                        UniqueId = webs[i].UniqueId,
                        ButtonId = webs[i].ButtonId,
                        Script = webs[i].Script
                    });
                }
                return dto;
            }
            return null;
        }

        public Website IsValidWebsite(Guid WebsiteId, Guid UserId)
        {
            var webs = (from w in easeLogMeIn.Website
                        join m in easeLogMeIn.WebsiteUserMapping
                        on w.UniqueId equals m.WebsiteId
                        where m.UserId == UserId
                        where m.WebsiteId == WebsiteId
                        where !w.IsBlocked
                        where w.IsActive
                        select new
                        {
                            w.Salt,
                            w.ConfigurationType,
                            w.Name,
                            w.Password,
                            w.URL,
                            w.UserId,
                            w.UniqueId,
                            w.PasswordTextId,
                            w.UserIdTextId,
                            w.ButtonId,
                            w.Script
                        }).OrderBy(o => o.Name).ToList();
            if (webs.Count > 0)
            {
                return new Website
                {
                    ConfigurationType = webs[0].ConfigurationType,
                    Name = webs[0].Name,
                    Password = webs[0].Password,
                    URL = webs[0].URL,
                    UserId = webs[0].UserId,
                    Salt = webs[0].Salt,
                    UniqueId = webs[0].UniqueId,
                    PasswordTextId = webs[0].PasswordTextId,
                    UserIdTextId = webs[0].UserIdTextId,
                    ButtonId = webs[0].ButtonId,
                    Script = webs[0].Script
                };
            }
            return null;
        }
    }
}
