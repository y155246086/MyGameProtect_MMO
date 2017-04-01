using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    public class NetworkParameters
    {
        public static short MESSAGE_HEADER_FLAG = 3637;

        public static int _MAX_RECV_BUFFER_SIZE = 2048;
        public static int _MAX_SEND_BUFFER_SIZE = 2048;
        public static int _MAX_EVENT_NUMBER = 3000;
        public static int _MAX_UNCOMPRESS_MESSAGE_SIZE = 1048576;// 1024*1024

        public static int _Connection_STATUS_CLOSED = 0;
        public static int _Connection_STATUS_CONNECTING = 1;
        public static int _Connection_STATUS_CONNECTED = 1;

        public static UInt16 NETWORK_MSG_DATA = 1;
        public static UInt16 NETWORK_MSG_EXCHANGE_INFO = 2;
        public static UInt16 NETWORK_MSG_DATA_LZO = 3;

        public static String TXHttpHeader = "tgw_l7_forward\r\nHost: play2.ld.qq.com:80\r\n\r\n";
    }

}