using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EaseLogMeIn.DatabaseLayer;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace EaseLogMeIn.BusinessLayer
{
    public interface IExceptionLoger
    {
        void AddException(CustomException exception);
        void AddException(Exception exception, CustomException Data);
        void AddRequest(RequestLoger loger);
    }
    public class ExceptionLoger : IExceptionLoger
    {
        readonly IExceptionContext exceptionLoger;
        public ExceptionLoger(IExceptionContext context)
        {
            exceptionLoger = context;
        }
        public void AddException(CustomException exception)
        {
            exceptionLoger.AddException(exception);
        }
        public void AddException(Exception exception, CustomException Data)
        {
            try
            {
                Data = Data ?? new CustomException();
                Data.Error = GetExceptionDetails(exception);
                Data.Date = DateTime.Now.IST();
                Data.StackTrace = exception.StackTrace;
                if (ReadOnlyKeys.LogSource == "1")
                { exceptionLoger.AddException(Data); }
                else { WriteToFile(JsonConvert.SerializeObject(Data), Guid.NewGuid().ToString()); }
            }
            catch (Exception ex)
            {
                string filename = Guid.NewGuid().ToString();
                var d = new { Message = GetExceptionDetails(ex), ex.StackTrace };
                WriteToFile(JsonConvert.SerializeObject(d), filename); // Current error
                WriteToFile(JsonConvert.SerializeObject(Data), filename); // Original Error
            }
        }
        public void AddRequest(RequestLoger loger)
        {
            exceptionLoger.AddRequest(loger);
        }
        private string GetExceptionDetails(Exception exception)
        {
            StringBuilder builder = new StringBuilder();
            while (exception != null)
            {
                builder.AppendLine(exception.Message);
                exception = exception.InnerException;
            }
            return builder.ToString();
        }

        private void WriteToFile(string Mesasage, string FileName)
        {
            try
            {
                FileName = FileName ?? Guid.NewGuid().ToString();
                string currentFolder = DateTime.Now.IST().ToString("dd MMM yy");
                string logFile = Path.Combine(ReadOnlyKeys.LogFolder, currentFolder);
                string folder = HttpContext.Current.Server.MapPath("~/" + logFile);// Guid.NewGuid().ToString() + ".json";
                DirectoryInfo di = new DirectoryInfo(folder);
                if (!di.Exists) { di.Create(); }
                File.AppendAllText(Path.Combine(folder, FileName), Mesasage);
            }
            catch (Exception) { }
        }
    }
}
