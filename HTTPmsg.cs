
using System.Collections.Generic;

namespace HTTPParser
{
    public abstract class HTTPmsg
    {
        protected Dictionary<string, string> headers;
        public string body;

        public bool HeaderExists(string header)
        {
            return headers.ContainsKey(header);
        }

        public void SetHeader(string header, string value)
        {
            if(!HeaderExists(header))
                headers.Add(header, value);
            else
                headers[header] = value;
        }

        public string RemoveHeader(string header)
        {
            return headers.Remove(header, out string value) ? value : ""; 
        }

        public abstract string GetMsg();

        protected abstract void ParseMsg(string msg);
    }
}
