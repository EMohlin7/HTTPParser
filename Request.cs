using System;
using System.Collections.Generic;

namespace HTTPParser
{
    public class Request : HTTPmsg
    {
        public string method;
        public string element;

        public Request(){}
        public Request(string msg)
        {
            ParseMsg(msg);
        }
        
        public override string GetMsg()
        {
            string msg = string.Format("{0} {1} HTTP/1.1\r\n", method, element);
            foreach(var h in headers)
            {
                msg += string.Format("{0}: {1}\r\n", h.Key, h.Value);
            }
            return msg + "\r\n" + body;
        }

        protected override void ParseMsg(string msg)
        {
            string[] s = msg.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
            string head = s[0];
            string body = s.Length > 1 ? s[1] : "";
            this.body = body;

            string[] headers = head.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            string[] firstRow = headers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            method = firstRow[0];
            element = firstRow[1];

            //char[] separators = new char[] {':', ' '};
            for(int i = 1; i < headers.Length; ++i)  //Skip the first row of the request since it has a different format 
            {
                string[] header = headers[i].Split(": ", StringSplitOptions.RemoveEmptyEntries);
                SetHeader(header[0], header[1]);
            }
        }
    }
}
