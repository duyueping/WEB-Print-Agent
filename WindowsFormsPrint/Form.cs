using System;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using IniFile;//ini简单类
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Newtonsoft.Json;
using System.Text;

namespace WindowsFormsPrint
{

    public partial class Form : System.Windows.Forms.Form
    {
        private static HttpListener httOtherRequest;   //其他超做请求监听
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
                httOtherRequest = new HttpListener();
                httOtherRequest.Prefixes.Add("http://127.0.0.1:31400/");  //添加监听地址 注意是以/结尾。
                httOtherRequest.Start(); //允许该监听地址接受请求的传入。
                Thread ThreadhttOtherRequest = new Thread(new ThreadStart(GethttOtherRequest));
                ThreadhttOtherRequest.Start();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 执行其他超做请求监听行为
        /// </summary>
        public  void GethttOtherRequest()
        {
            while (true)
            {
                HttpListenerContext requestContext = httOtherRequest.GetContext(); //接受到新的请求
                try
                {
                    //reecontext 为开启线程传入的 requestContext请求对象
                    Thread subthread = new Thread(new ParameterizedThreadStart((reecontext) =>
                    {
                        var request = (HttpListenerContext)reecontext;
                        var str =(request.Request.QueryString["a"]); //接受GET请求过来的参数；
                        str = UrlDecoder.UrlDecode(str, System.Text.Encoding.UTF8);
                        //在此处执行你需要进行的操作>>比如什么缓存数据读取，队列消息处理，邮件消息队列添加等等。
                        GetContextCallBack();
                        var msg = "打印成功";
                        request.Response.StatusCode = 200;
                        request.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                        request.Response.ContentType = "application/json";
                        requestContext.Response.ContentEncoding = Encoding.UTF8;
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { success = true, behavior = msg }));
                        request.Response.ContentLength64 = buffer.Length;
                        var output = request.Response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();


                    }));
                    subthread.Start(requestContext); 
                }
                catch (Exception ex)
                {
                    try
                    {
                        requestContext.Response.StatusCode = 500;
                        requestContext.Response.ContentType = "application/text";
                        requestContext.Response.ContentEncoding = Encoding.UTF8;
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("System Error");
                        //对客户端输出相应信息.
                        requestContext.Response.ContentLength64 = buffer.Length;
                        System.IO.Stream output = requestContext.Response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        //关闭输出流，释放相应资源
                        output.Close();
                    }
                    catch { }
                }
            }
        }


        public  void GetContextCallBack()
        {
            try
            {
                string sPath = Application.StartupPath + "\\config.ini";//读取软件当前目录
                ClsIniFile ini = new ClsIniFile(sPath);
                string printerName = ini.IniReadValue("DeskServer", "PrinterName");
                printDocument1.PrinterSettings.PrinterName = printerName;
                printDocument1.Print();
            }
            catch (Exception eg)
            {

            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var obj = jss.DeserializeObject(str);
            Dictionary<string, object> dic = (Dictionary<string, object>)obj;
            var obj_tid = dic["tid"];//打印分類
            if (obj_tid.ToString() == "0")
            {
                #region
                var obj_id = dic["id"];//店鋪名
                var obj_oid = dic["oid"];//訂單號
                var obj_time = dic["ptime"];//提交時間
                var obj_paytype = dic["paytype"];//支付方式
                var obj_ys = dic["ys"];//應收
                var obj_ss = dic["ss"];//實收
                var obj_zl = dic["zl"];//找零
                var obj_sl = dic["sl"];//標語
                var obj_truename = dic["truename"];//用戶名
                var obj_telephone = dic["telephone"];//門號
                var obj_address = dic["address"];//地址

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


                Font drawFont = new Font("Arial", 11);
                Font drawFontoid = new Font("Arial", 16);
                SolidBrush drawBrush = new SolidBrush(Color.Black);

                string name = "店名:" + obj_id.ToString();
                string time = "时间:" + obj_time.ToString();

                e.Graphics.DrawString(obj_oid.ToString(), drawFontoid, drawBrush, 64, 0);
                e.Graphics.DrawString(name, drawFont, drawBrush, 0, 50);
                e.Graphics.DrawString(time, drawFont, drawBrush, 0, 80);
                e.Graphics.DrawString("-----------------------------------------------------------------------", drawFont, drawBrush, 0, 100);
                e.Graphics.DrawString("商品名称", drawFont, drawBrush, 0, 120);
                e.Graphics.DrawString("数量", drawFont, drawBrush, 150, 120);
                //e.Graphics.DrawString("价格", drawFont, drawBrush, 130, 120);
                e.Graphics.DrawString("小计", drawFont, drawBrush, 200, 120);
                int la = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    la = (dt.Rows.Count * 50) + 150;

                    e.Graphics.DrawString(dt.Rows[i]["goodsname"].ToString(), drawFont, drawBrush, 0, (i * 50) + 150);
                    e.Graphics.DrawString("×" + dt.Rows[i]["goodsnum"].ToString(), drawFont, drawBrush, 150, (i * 50) + 150);
                    decimal goodsp = decimal.Parse(dt.Rows[i]["goodsp"].ToString());
                    decimal goodsa = goodsp * int.Parse(dt.Rows[i]["goodsnum"].ToString());
                    //e.Graphics.DrawString(goodsp.ToString(), drawFont, drawBrush, 130, (i * 50) + 150);
                    e.Graphics.DrawString(goodsa.ToString(), drawFont, drawBrush, 200, (i * 50) + 150);
                    e.Graphics.DrawString("-----------------------------------------------------------------------", drawFont, drawBrush, 0, (dt.Rows.Count * 40) + 150);

                }
                e.Graphics.DrawString("应收:" + decimal.Parse(obj_ys.ToString()), drawFont, drawBrush, 0, la);
                e.Graphics.DrawString("实收:" + decimal.Parse(obj_ss.ToString()), drawFont, drawBrush, 0, la + 30);
                e.Graphics.DrawString("找零:" + decimal.Parse(obj_zl.ToString()), drawFont, drawBrush, 0, la + 60);
                e.Graphics.DrawString("付款方式:" + obj_paytype.ToString(), drawFont, drawBrush, 0, la + 90);
                e.Graphics.DrawString("用户姓名:" + obj_truename.ToString(), drawFont, drawBrush, 0, la + 120);
                e.Graphics.DrawString("用户手机:" + obj_telephone.ToString(), drawFont, drawBrush, 0, la + 150);
                e.Graphics.DrawString(obj_address.ToString(), drawFont, drawBrush, 0, la + 180);
                e.Graphics.DrawString(obj_sl.ToString(), drawFont, drawBrush, 120, la + 230);

                #endregion
            }
            else if (obj_tid.ToString() == "2")
            {
                #region
                var obj_order = dic["order"];//訂單號
                var obj_goods = dic["goods"];//產品
                var obj_chef = dic["chef"];//廚師
                var obj_time = dic["time"];//完成時間
                Font drawFont = new Font("Arial", 14);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                int la = 10;
                e.Graphics.DrawString("单号:" + obj_order.ToString(), drawFont, drawBrush, 0, la);
                e.Graphics.DrawString("产品:" + obj_goods.ToString(), drawFont, drawBrush, 0, la + 30);
                e.Graphics.DrawString("厨师:" + obj_chef.ToString(), drawFont, drawBrush, 0, la + 60);
                e.Graphics.DrawString("时间:" + obj_time.ToString(), drawFont, drawBrush, 0, la + 90);
                #endregion
            }

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
