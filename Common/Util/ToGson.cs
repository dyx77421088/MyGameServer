using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Util
{
    public class ToGson
    {
        public static string Info(int status, object info)
        {
            return "{\"status\":" + status + ",\"message\":\"" + info + "\"}";
        }

        public static string Info(int status, string message, object data)
        {
            return "{\"status\":" + status + ",\"message\":\"" + message + "\" " + ",\"data\":" + data + " }";
        }

        public static string Success(object data)
        {
            return "{\"status\":" + 200 + ",\"data\":" + data + "}";
        }
    }
}
