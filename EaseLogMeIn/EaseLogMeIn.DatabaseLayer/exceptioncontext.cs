using EaseLogMeIn.Models;

namespace EaseLogMeIn.DatabaseLayer
{
    public interface IExceptionContext {
        int AddException(CustomException exception);
        void AddRequest(RequestLoger loger);
    }
    public class ExceptionContext : IExceptionContext
    {
        readonly EaseLogMeInDbContext easeLogMeIn = new EaseLogMeInDbContext();
        public int AddException(CustomException exception)
        {
            easeLogMeIn.CustomException.Add(exception);
            return easeLogMeIn.SaveChanges();
        }
        public void AddRequest(RequestLoger loger)
        {
            easeLogMeIn.RequestLoger.Add(loger);
            easeLogMeIn.SaveChanges();
        }
    }
}
