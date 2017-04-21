using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace Network
{
    public class NetUtil
    {
        public static IPAddress GetIPV4Address(string hostname)
        {
            IPAddress[] hosts = null;
            try
            {
                hosts = Dns.GetHostAddresses(hostname);
            }
            catch (Exception e)
            {
                //Logger.Error("Dns.GetHostAddresses " + e.Message);
                return null;
            }
            if (hosts == null || hosts.Length == 0)
            {
                return null;
            }

            IPAddress host = null;
            foreach (IPAddress address in hosts)
            {
                //select IP V4
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    host = address;
                    break;
                }
            }

            return host;
        }

        public static void WwwDownload(String url,String savePath)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            //创建本地文件写入流
            Stream stream = new FileStream(savePath, FileMode.Create);
            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();
        }
    }
}
