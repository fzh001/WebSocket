using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DownLoadDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Environment.CurrentDirectory;
            var csredis = new CSRedis.CSRedisClient("127.0.0.1:6379,defaultDatabase=1,poolsize=50,ssl=false,writeBuffer=10240");
            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
            //Install-Package Caching.CSRedis (本篇不需要) 
            //注册mvc分布式缓存
            //services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
            // var list = RedisHelper.LPop("video");
            var list = RedisHelper.LRange<Temp>("video", 0, 20);
            foreach(var item in list)
            {
              //  var item = RedisHelper.RPop<Temp>("video");
                var desPath = Path.Combine(path, item.name).ToString() + ".mp4";
                DownLoad(item.url, "", desPath);
            }
          
            
            //var l = new List<Thread>();
            //foreach (var item in list)
            //{
            //    Thread thread = new Thread(() =>
            //    {
            //       // var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Temp>(item.Value);
            //        var desPath = Path.Combine(path, item.name).ToString() + ".mp4";
            //        DownLoad(item.url, "http://kkphm.com", desPath);
            //    });
            //    l.Add(thread);
            //}
            //foreach (var item in l.Take(10))
            //{
            //    item.Start();
            //}
            Console.ReadLine();
        }

        /// <summary>

        /// 下载视频

        /// </summary>

        /// <param name="url">视频网址路径</param>

        /// <param name="Cheaturl">欺骗服务器的网址，可为null，

        /// 对于一些网站没有下载权限就要设置，就是你是在哪个网页看到这个视频的网址</param>

        /// <param name="DesPath">保存的路径(路径的文件名带后缀),如string DesPath = "D:\\text.mp4";</param>

        static void DownLoad(string url, string Cheaturl, string DesPath)
        {
            try
            {
                //指定http下载地址
                HttpWebRequest resquest = (HttpWebRequest)WebRequest.Create(url);
                //指定欺骗服务器下载地址
                resquest.Referer = Cheaturl;
                //复制下载流,并写入文件
                DateTime start = DateTime.Now;
                using (HttpWebResponse response = (HttpWebResponse)resquest.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (Stream fsStream = new FileStream(DesPath, FileMode.Create,FileAccess.Write))
                            {
                                // stream.CopyTo(fsStream);
                                //StreamWriter sw = new StreamWriter(fsStream);
                                // sw.Write(stream);
                                // sw.Close();
                                // fsStream.Write(stream.g);
                                byte[] buffer = new byte[1024 * 1024 * 2];
                                while (true)
                                {
                                    int read = stream.Read(buffer, 0, buffer.Length);
                                    if (read <= 0) break;
                                    fsStream.Write(buffer, 0, read);
                                    fsStream.Flush();
                                }
                              
                                fsStream.Dispose();
                            }
                        }
                        DateTime end = DateTime.Now;
                        var sec = end.Subtract(start).TotalSeconds;
                        Console.WriteLine("下载完毕!!耗时" + sec);
                    }
                    else
                    {
                        Console.WriteLine("下载失败" + response.StatusCode);
                    }

                }
            }
            catch (Exception)
            {

                
            }
        }

        /// <summary>
        /// Http方式下载文件
        /// </summary>
        /// <param name="url">http地址</param>
        /// <param name="localfile">本地文件</param>
        /// <returns></returns>
        public static bool Download(string url, string localfile)
        {
            bool flag = false;
            long startPosition = 0; // 上次下载的文件起始位置
            FileStream writeStream; // 写入本地文件流对象

            // 判断要下载的文件夹是否存在
            if (File.Exists(localfile))
            {

                writeStream = File.OpenWrite(localfile);             // 存在则打开要下载的文件
                startPosition = writeStream.Length;                  // 获取已经下载的长度
                writeStream.Seek(startPosition, SeekOrigin.Current); // 本地文件写入位置定位
            }
            else
            {
                writeStream = new FileStream(localfile, FileMode.Create);// 文件不保存创建一个文件
                startPosition = 0;
            }


            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);// 打开网络连接

                if (startPosition > 0)
                {
                    myRequest.AddRange((int)startPosition);// 设置Range值,与上面的writeStream.Seek用意相同,是为了定义远程文件读取位置
                }


                Stream readStream = myRequest.GetResponse().GetResponseStream();// 向服务器请求,获得服务器的回应数据流


                byte[] btArray = new byte[512];// 定义一个字节数据,用来向readStream读取内容和向writeStream写入内容
                int contentSize = readStream.Read(btArray, 0, btArray.Length);// 向远程文件读第一次

                while (contentSize > 0)// 如果读取长度大于零则继续读
                {
                    writeStream.Write(btArray, 0, contentSize);// 写入本地文件
                    contentSize = readStream.Read(btArray, 0, btArray.Length);// 继续向远程文件读取
                }

                //关闭流
                writeStream.Close();
                readStream.Close();

                flag = true;        //返回true下载成功
            }
            catch (Exception)
            {
                writeStream.Close();
                flag = false;       //返回false下载失败
            }

            return flag;
        }
    }

    public class Temp
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
