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
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:3721/api/contacts");
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            string content = "{Name:\"张三\",PhoneNo:\"12345678\",EmailAddress: " +
                "\"zhangsan@gmail.com\",Address:\"江苏省苏州市星湖街328号\"}";
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            request.GetResponse();      
        }
    }
}
