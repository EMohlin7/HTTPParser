
using System;
using System.Collections.Generic;

namespace HTTPParser
{
    public abstract class HTTPmsg
    {
        protected Dictionary<string, string> headers = new Dictionary<string, string>(
            StringComparer.InvariantCultureIgnoreCase); //Makes the dictionary case-insensitive
        public string body = "";

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

        public string GetHeader(string header)
        {
            return headers[header];
        }
        public bool TryGetHeader(string header, out string value)
        {
            bool exists = HeaderExists(header);
            value = exists ? headers[header] : "";
            return exists;
        }

        public string RemoveHeader(string header)
        {
            return headers.Remove(header, out string value) ? value : ""; 
        }

        public abstract string GetMsg();
    }
}
