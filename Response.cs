
using System;
using System.Collections.Generic;

namespace HTTPParser
{
    public class Response : HTTPmsg
    {
        public int code;
        public Response(){}
        public Response(int code)
        {
            this.code = code;
        }
        public Response(string msg)
        {
            ParseMsg(msg);
        }

        public override string GetMsg()
        {
            string msg = string.Format("HTTP/1.1 {0}\r\n", code);
            foreach(var h in headers)
            {
                msg += string.Format("{0}: {1}\r\n", h.Key, h.Value);
            }
            return msg + "\r\n" + body;
        }

        protected override void ParseMsg(string msg)
        {
            msg = msg.ToLower();
            var parse = new Dictionary<string, string>();
            string[] s = msg.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
            string head = s[0];
            string body = s.Length > 1 ? s[1] : "";
            this.body = body;

            string[] headers = head.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            string[] firstRow = headers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if(int.TryParse(firstRow[1], out int result))
                code = result;
            else
                throw new Exception(string.Format("\"{0}\" is not a valid HTTP code", firstRow[1]));
            
            char[] separators = new char[] {':', ' '};
            for(int i = 1; i < headers.Length; ++i)  //Skip the first row of the request since it has a different format 
            {
                string[] header = headers[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                parse.Add(header[0], header[1]);
            }

            this.headers = parse;
        }

        
    }
}