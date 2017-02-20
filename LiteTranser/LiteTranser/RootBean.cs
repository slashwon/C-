using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteTranser
{
    class RootBean : System.Object
    {
        public RootBean() { }
        public int errorCode;
        public string query;
        public List<String> translation;
        public List<WebBean> web;
    }
}
