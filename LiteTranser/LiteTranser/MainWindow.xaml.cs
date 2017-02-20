using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace LiteTranser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string BASE_URL = "http://fanyi.youdao.com/openapi.do?keyfrom=LiteTranser&key=1266940761&type=data&doctype=json&version=1.1&q=";
        private TextBox tbInput;
        private State currState = State.ACTIVE;
        enum State { 
            ACTIVE,
            DISACTIVE
        }

        public MainWindow()
        {
            InitializeComponent();
            tbInput = (TextBox)FindName("tb_input");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (null != tbInput && currState==State.ACTIVE)
            {
                tbInput.IsReadOnly = true;
                currState = State.DISACTIVE;
                string word = tbInput.Text;
                string json = Translate(word);
                tbInput.Text = json;

                RootBean rootBean = ParseJson(json);

                string query = rootBean.query;
                List<String> translation = rootBean.translation;
                List<WebBean> web = rootBean.web;

                StringBuilder sb = new StringBuilder();
                sb.Append("查询词: " + query + "\n"); sb.Append("译: ");
                for (int i = 0; i < translation.Count;i++ )
                {
                    sb.Append(translation[i]);
                    if (i < translation.Count - 1)
                        sb.Append(";");
                    else sb.Append("\n");
                }
			    
			    foreach (WebBean w in web) {
				    sb.Append("例句: "+w.key+"\n");
                    for (int i = 0; i < w.value.Count; i++)
                    {
                        sb.Append("\t"+w.value[i]);
                        if (i < w.value.Count - 1) sb.Append(";\n");
                        else sb.Append("\n");
                    }
                    
				    
			    }
                tbInput.Text = sb.ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        public string Translate(string word)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(BASE_URL+word);
            System.Net.WebResponse resp = request.GetResponse();
            Stream respStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(respStream, Encoding.GetEncoding("utf-8"));
            string json = reader.ReadToEnd();
            return json;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (null != tbInput) 
            {
                currState = State.ACTIVE;
                tbInput.Text = "";
                tbInput.IsReadOnly = false;
            }
        }

        RootBean ParseJson(string json)
        {

            RootBean bean = Newtonsoft.Json.JsonConvert.DeserializeObject<RootBean>(json);
            return bean;
        }

    }
}
