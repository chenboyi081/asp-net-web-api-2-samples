using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest
            .Create("http://localhost:3721/api/contacts");
            request.Method = "POST";
            request.ContentType = "application/xml; charset=utf-8";
            string content = "<Contact xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.artech.com/\"><Address>江苏省苏州市星湖街328号</Address><EmailAddress>zhangsan@gmail.com</EmailAddress><Name>张三</Name><PhoneNo>12345678</PhoneNo></Contact>";
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            request.GetResponse();
        }
    }
}
