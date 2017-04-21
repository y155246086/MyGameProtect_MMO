using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    public delegate void OnMessageReceive<T>(T message) where T : Message;
    public delegate void OnMessageReceive2(Message message);
}
