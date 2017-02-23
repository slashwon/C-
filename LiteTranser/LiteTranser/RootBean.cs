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
        public Basic basic;

        public string ConvertToString()
        {
            if (errorCode != 0) return "";

            StringBuilder sb = new StringBuilder();
            sb.Append("查询词: " + query + "\n"); 
            sb.Append("译: ");
            if (null != translation)
            {

                for (int i = 0; i < translation.Count; i++)
                {
                    sb.Append(translation[i]);
                    if (i < translation.Count - 1)
                        sb.Append(";");
                    else sb.Append("\n");
                }
            }

            // convert "explains"
            if (null!=basic && null!=basic.explains)
            {
                sb.Append("解释:\n");
                foreach (string explain in basic.explains)
                {
                    sb.Append("\t" + explain + "\n");
                }

                if (null != basic.phonetic)
                {
                    sb.Append("发音: " + basic.phonetic + "\n");
                }

                if (null!=basic.uk_phonetic)
                {
                    sb.Append("\t[美:]" + basic.us_phonetic+"\n");
                }

                if (null != basic.us_phonetic)
                {
                    sb.Append("\t[英:]" + basic.us_phonetic + "\n");
                }
            }

            if (null!=web)
            {
                foreach (WebBean w in web)
                {
                    sb.Append("例句: " + w.key + "\n");
                    for (int i = 0; i < w.value.Count; i++)
                    {
                        sb.Append("\t" + w.value[i]);
                        if (i < w.value.Count - 1) sb.Append(";\n");
                        else sb.Append("\n");
                    }
                }
            }
            
            return sb.ToString();
        }
    }
}
