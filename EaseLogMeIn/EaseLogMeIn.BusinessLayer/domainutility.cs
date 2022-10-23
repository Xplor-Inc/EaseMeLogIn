using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EaseLogMeIn.BusinessLayer
{
    public class WebAppUtility
    {
        public static bool IsEmptyText(string Text)
        {
            if (Text == null) { return true; }
            return Text.Trim().Length < 1;
        }
        public static bool IsValidUrl(string Url)
        {
            if (Url == null || Url.Trim().Length < 10) { return false; }
            Regex regex = new Regex("^(https?:\\/\\/)?((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.?)+[a-z]{2,}|((\\d{1,3}\\.){3}\\d{1,3}))(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*(\\?[;&amp;a-z\\d%_.~+=-]*)?(\\#[-a-z\\d_]*)?$");
            return regex.IsMatch(Url);
        }
    }
}
