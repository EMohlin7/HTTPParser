using System;
using System.Collections.Generic;

namespace HTTPParser
{
    public class Request
    {
        public Dictionary<string, string> ParseRequest(string msg)
        {
            var parse = new Dictionary<string, string>();
            string[] s = msg.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
            string head = s[0];
            string body = s.Length > 1 ? s[1] : "";
            parse.Add("Body", body);

            string[] headers = head.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            string[] firstRow = headers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            parse.Add("Method", firstRow[0]);
            parse.Add("Element", firstRow[1]);

            char[] separators = new char[] {':', ' '};
            for(int i = 1; i < headers.Length; ++i)  //Skip the first row of the request since it has a different format 
            {
                string[] header = headers[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                parse.Add(header[0], header[1]);
            }

            return parse;
        }
    }
}
