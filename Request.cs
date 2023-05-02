using System;
using System.Collections.Generic;

namespace HTTPParser
{
    public class Request : HTTPmsg
    {
        public string method;
        public string element;


        public Request() { }
        
        public override string GetMsg()
        {
            string msg = string.Format("{0} {1} HTTP/1.1\r\n", method, element);
            foreach(var h in headers)
            {
                msg += string.Format("{0}: {1}\r\n", h.Key, h.Value);
            }
            return msg + "\r\n" + body;
        }

        public static bool TryParseMsg(string msg, out Request req)
        {
            req = null;
            try
            {
                string[] s = msg.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
                string head = s[0];
                string body = s.Length > 1 ? s[1] : "";


                string[] headers = head.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                string[] firstRow = headers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string method = firstRow[0];
                if (firstRow.Length <= 1)
                    return false;

                string element = firstRow[1];


                req = new Request();
                //char[] separators = new char[] {':', ' '};
                for (int i = 1; i < headers.Length; ++i)  //Skip the first row of the request since it has a different format 
                {
                    string[] header = headers[i].Split(": ", StringSplitOptions.RemoveEmptyEntries);
                    if (header.Length == 2)
                        req.SetHeader(header[0], header[1]);
                }

                req.element = element;
                req.body = body;
                req.method = method;
                return true;
            }
            catch(IndexOutOfRangeException)
            {
                req = null;
                return false;
            }
        }
    }
}
