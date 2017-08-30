/**
* @brief WEB-Print-Agent
*
* @author yueping du <duyueping@vip.qq.com>
* @since 2017-08-30 16:45:00
*/

using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using IniFile;//ini简单类

using System.Web.Script.Serialization;
using System.Collections.Generic;

using System.Data;
using System.Drawing;

namespace WindowsFormsPrint
{

    public partial class Form : System.Windows.Forms.Form
    {
        private static HttpListener listerner;

        public static string str;


        public Form()
        {
            InitializeComponent();

            Listen();


        }
        private void Listen()
        {
            try
            {

                HttpListener listerner = new HttpListener();
                listerner.AuthenticationSchemes = AuthenticationSchemes.Anonymous;//指定身份验证 Anonymous匿名访问
                listerner.Prefixes.Add("http://127.0.0.1:31400/");
                listerner.Start();
                listerner.BeginGetContext(new AsyncCallback(GetContextCallBack), listerner);

            }

            catch
            {
            }
        }

        public void GetContextCallBack(IAsyncResult ar)
        {
            try
            {
                listerner = ar.AsyncState as HttpListener;
                HttpListenerContext context = listerner.EndGetContext(ar);
                listerner.BeginGetContext(new AsyncCallback(GetContextCallBack), listerner);
                str = context.Request.RawUrl;
                string[] strr = str.Split(new char[] { '=' });
                str = strr[1];
                str = UrlDecoder.UrlDecode(str, System.Text.Encoding.UTF8);
                string sPath = Application.StartupPath + "\\config.ini";//读取软件当前目录
                ClsIniFile ini = new ClsIniFile(sPath);
                string printerName = ini.IniReadValue("DeskServer", "PrinterName");

                printDocument1.PrinterSettings.PrinterName = printerName;
                printDocument1.Print();

                //其它处理code  
            }
            catch (Exception eg)
            {
                Console.WriteLine(eg.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var obj = jss.DeserializeObject(str);
            Dictionary<string, object> dic = (Dictionary<string, object>)obj;
            var obj_id = dic["id"];
            var obj_time = dic["ptime"];
            var obj_paytype = dic["paytype"];
            var obj_ys = dic["ys"];
            var obj_ss = dic["ss"];
            var obj_zl = dic["zl"];
            var obj_sl = dic["sl"];

            var data = dic["order"];//得到所有行的信息，数组类型，每一个数组是一个Dictionary类型的键值对，即为列  
            Array rows = (Array)data;//这里从rows 的每一个元素为一个Dictionary类的对象，相当于datatable中的一行的数据  

            DataTable dt = new DataTable();
            foreach (Dictionary<string, object> cols in rows)
            {
                //为datatable添加列  
                if (dt.Columns.Count == 0)
                {
                    foreach (string key in cols.Keys)
                    {
                        dt.Columns.Add(key);
                    }
                }
                DataRow dr = dt.NewRow();
                //为行中的每一列列赋值  
                foreach (string keyname in cols.Keys)
                {
                    dr[keyname] = cols[keyname];
                }
                dt.Rows.Add(dr);
            }

           
            Font drawFont = new Font("Arial", 8);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            string name = "店名:" + obj_id.ToString();
            string time = "时间:" + obj_time.ToString();

            e.Graphics.DrawString(name, drawFont, drawBrush, 0, 50);
            e.Graphics.DrawString(time, drawFont, drawBrush, 0, 80);
            e.Graphics.DrawString("-----------------------------------------------------------------------", drawFont, drawBrush, 0, 100);
            e.Graphics.DrawString("商品名称", drawFont, drawBrush, 0, 120);
            e.Graphics.DrawString("数量", drawFont, drawBrush, 80, 120);
            e.Graphics.DrawString("价格", drawFont, drawBrush, 160, 120);
            e.Graphics.DrawString("小计", drawFont, drawBrush, 240, 120);
            int la = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                la = (dt.Rows.Count * 50) + 150;

                e.Graphics.DrawString(dt.Rows[i]["goodsname"].ToString(), drawFont, drawBrush, 0, (i * 50) + 150);
                e.Graphics.DrawString(dt.Rows[i]["goodsnum"].ToString(), drawFont, drawBrush, 80, (i * 50) + 150);
                decimal goodsp = decimal.Parse(dt.Rows[i]["goodsp"].ToString()) / 100;
                e.Graphics.DrawString(goodsp.ToString(), drawFont, drawBrush, 160, (i * 50) + 150);
                e.Graphics.DrawString(goodsp.ToString(), drawFont, drawBrush, 240, (i * 50) + 150);
                e.Graphics.DrawString("-----------------------------------------------------------------------", drawFont, drawBrush, 0, (dt.Rows.Count * 40) + 150);

            }
            e.Graphics.DrawString("应收:" + decimal.Parse(obj_ys.ToString()) / 100, drawFont, drawBrush, 0, la);
            e.Graphics.DrawString("实收:" + decimal.Parse(obj_ss.ToString()) / 100, drawFont, drawBrush, 0, la + 30);
            e.Graphics.DrawString("找零:" + decimal.Parse(obj_zl.ToString()) / 100, drawFont, drawBrush, 0, la + 60);
            e.Graphics.DrawString("付款方式:" + obj_paytype.ToString() == "1" ? "现金" : "网银", drawFont, drawBrush, 0, la + 90);
            e.Graphics.DrawString(obj_sl.ToString(), drawFont, drawBrush, 120, la + 140);

        }


    }
}
