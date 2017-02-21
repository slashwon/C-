using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Controls;

namespace LiteTranser
{
    class NetworkHandler
    {
        static string BASE_URL = "http://fanyi.youdao.com/openapi.do?keyfrom=LiteTranser&key=1266940761&type=data&doctype=json&version=1.1&q=";
        public NetworkHandler() { }
        private string Translate(string word)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(BASE_URL + word);
            System.Net.WebResponse resp = request.GetResponse();
            Stream respStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(respStream, Encoding.GetEncoding("utf-8"));
            string json = reader.ReadToEnd();
            return json;
        }

        private RootBean ParseJson(string json)
        {
            RootBean bean = Newtonsoft.Json.JsonConvert.DeserializeObject<RootBean>(json);
            return bean;
        }

        public string DoTranslate(string word)
        {
            if (null==word)
            {
                return "";
            }
            string json = Translate(word);
            RootBean rootBean = ParseJson(json);
            return rootBean.ConvertToString();
        }
    }
}

