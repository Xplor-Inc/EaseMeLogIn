using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EaseLogMeIn.Models;
using EaseLogMeIn.DatabaseLayer;

namespace EaseLogMeIn.BusinessLayer
{
    public interface IUserActionDataManager
    {
        int Add(UserActionData data);
        List<UserData> Search(UserActionSearch search, out int TotalCount);
    }

    public class UserActionDataManage : IUserActionDataManager
    {
        readonly IUserActionDataContext userAction;
        public UserActionDataManage(IUserActionDataContext context)
        {
            userAction = context;
        }

        public int Add(UserActionData data)
        {
            return userAction.Add(data);
        }
       public List<UserData> Search(UserActionSearch search, out int TotalCount)
        {
            return userAction.Search(search, out TotalCount);
        }
    }
}
