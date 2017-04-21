using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common;

namespace Network
{
    public class TCPConnection : IConnection
    {
        private Socket socket;
        private IPEndPoint local;
        private IPEndPoint remote;
        private int status;

        private BufferManager bufferManager;

        NetworkBuffer msgBuffer = new NetworkBuffer(NetworkParameters._MAX_UNCOMPRESS_MESSAGE_SIZE, true);
        private int evenCount;
        Object eventLock = new Object();
        Object statusLock = new Object();

        Queue<SocketAsyncEventArgs> freeEventArgs = new Queue<SocketAsyncEventArgs>();
        Queue<SocketAsyncEventArgs> processEventArgs = new Queue<SocketAsyncEventArgs>();
        Queue<SocketAsyncEventArgs> appendEventArgs = new Queue<SocketAsyncEventArgs>();

        public TCPConnection()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.NoDelay = true;
            this.evenCount = 0;
            this.status = NetworkParameters._Connection_STATUS_CLOSED;

            bufferManager = new BufferManager(NetworkParameters._MAX_EVENT_NUMBER * NetworkParameters._MAX_RECV_BUFFER_SIZE, NetworkParameters._MAX_RECV_BUFFER_SIZE);
            bufferManager.InitBuffer();
        }

        private SocketAsyncEventArgs GetEventArg(bool needBuffer)
        {
            //SocketAsyncEventArgs eventArg = new SocketAsyncEventArgs();
            //eventArg.RemoteEndPoint = remote;
            //eventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketEventArg_Completed);
            //if (needBuffer)
            //    eventArg.SetBuffer(new byte[NetworkParameters._MAX_RECV_BUFFER_SIZE], 0, NetworkParameters._MAX_RECV_BUFFER_SIZE);
            //return eventArg;
            lock (eventLock)
            {
                if (freeEventArgs.Count > 0)
                {
                    SocketAsyncEventArgs eventArg = freeEventArgs.Dequeue();
                    if (needBuffer && eventArg.Buffer == null)
                    {
                        bufferManager.SetBuffer(eventArg);
                    }
                    else if (needBuffer == false && eventArg.Buffer != null)
                    {
                        bufferManager.FreeBuffer(eventArg);
                    }
                    return eventArg;
                }
                else if (evenCount < NetworkParameters._MAX_EVENT_NUMBER)
                {
                    evenCount++;
                    SocketAsyncEventArgs eventArg = new SocketAsyncEventArgs();
                    eventArg.RemoteEndPoint = remote;
                    eventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketEventArg_Completed);
                    if (needBuffer)
                    {
                        bufferManager.SetBuffer(eventArg);
                    }

                    return eventArg;
                }
                return null;
            }
        }

        private void PushFreeEventArg(SocketAsyncEventArgs eventArg, bool clearBuffer)
        {
            lock (eventLock)
            {
                if (clearBuffer)
                {
                    if (eventArg.Buffer != null)
                    {
                        bufferManager.FreeBuffer(eventArg);
                    }
                    else
                    {
                        eventArg.SetBuffer(null, 0, 0);
                    }
                }
                freeEventArgs.Enqueue(eventArg);
            }
        }

        private void PushAppendEventArg(SocketAsyncEventArgs eventArg)
        {
            lock (eventLock)
            {
                appendEventArgs.Enqueue(eventArg);
            }
        }

        //[MethodImpl(MethodImplOptions.Synchronized)] 
        public override void ConnectAsync(IPEndPoint localAddress, IPEndPoint remoteAddress)
        {
            this.remote = remoteAddress;
            this.local = localAddress;
            lock (statusLock)
            {
                status = NetworkParameters._Connection_STATUS_CONNECTING;
            }

            SocketAsyncEventArgs eventArg = GetEventArg(false);
            if (eventArg == null)
            {
                Logger.Error("Can't eventArg when connecting");
                return;
            }
            bool willRaiseEvent = socket.ConnectAsync(eventArg);
            if (!willRaiseEvent)
            {
                ProcessConnect(eventArg, false);
            }

        }
        public override void Disconnect()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Disconnect(true);
                lock (statusLock)
                {
                    status = NetworkParameters._Connection_STATUS_CLOSED;
                }
            }
            catch (Exception e)
            { Logger.Error(e.Message); }
        }

        public override bool isConnected()
        {
            return (socket != null) && socket.Connected == true;
        }

        internal override bool _Send(byte[] buffer, int offset, int count)
        {
            if (socket == null || socket.Connected == false)
            {
                return false;
            }

            SocketAsyncEventArgs eventArg = GetEventArg(false);
            if (eventArg == null)
            {
                Logger.Error("There is no SocketAsyncEventArgs available");
                return false;
            }
            //eventArg.SetBuffer(buffer, offset, count);
            if (bufferManager.SetBuffer(eventArg, buffer, offset, count) == false)
            {
                Logger.Error("There is no memory available in buffermanager");
                return false;
            }

            bool willRaiseEvent = socket.SendAsync(eventArg);
            if (!willRaiseEvent)
            {
                ProcessSend(eventArg, false);
            }

            return true;
        }

        public override void Update()
        {
            Queue<SocketAsyncEventArgs> tempStack;
            lock (eventLock)
            {
                tempStack = appendEventArgs;
                appendEventArgs = processEventArgs;
                processEventArgs = tempStack;
            }

            SocketAsyncEventArgs eventArg = null;

            while (processEventArgs.Count > 0)
            {
                eventArg = processEventArgs.Dequeue();
                switch (eventArg.LastOperation)
                {
                    case SocketAsyncOperation.Connect:
                        ProcessConnect(eventArg, true);
                        break;
                    case SocketAsyncOperation.Receive:
                        ProcessReceive(eventArg, true);
                        break;
                    case SocketAsyncOperation.Send:
                        ProcessSend(eventArg, true);
                        break;
                    default:
                        if (handlerPipeline.InHeader != null)
                        {
                            handlerPipeline.InHeader.OnDisconnected(this, eventArg.SocketError);
                        }
                        Logger.Error("Invalid operation completed");
                        break;
                }
            }
        }

        /// <summary>
        /// A single callback s uised for all socket operations. This method forwards execution on to the correct handler 
        /// based on the type of completed operation
        /// </summary>
        void SocketEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    Logger.Debug("Receive SocketAsyncOperation.Connect");
                    ProcessConnect(e, false);
                    break;
                case SocketAsyncOperation.Receive:
                    if (AbstractMessageInitializer.getDefaultLogger() != null)
                        AbstractMessageInitializer.getDefaultLogger()("Receive SocketAsyncOperation.Receive:" + e.BytesTransferred);
                    else
                        Logger.Debug("Receive SocketAsyncOperation.Receive  {0}", e.BytesTransferred);
                    ProcessReceive(e, false);
                    break;
                case SocketAsyncOperation.Send:
                    //Logger.Debug("Receive SocketAsyncOperation.Send");
                    ProcessSend(e, false);
                    break;
                default:
                    if (handlerPipeline.InHeader != null)
                    {
                        handlerPipeline.InHeader.OnDisconnected(this, e.SocketError);
                    }
                    Logger.Error("Invalid operation completed");
                    throw new Exception("Invalid operation completed");
            }
        }

        /// <summary>
        /// Called when a ConnectAsync operation completes
        /// </summary>
        private void ProcessConnect(SocketAsyncEventArgs e, bool isPolling)
        {
            if (!isPolling)
            {
                SocketError socketError = e.SocketError;
                PushAppendEventArg(e);

                //if failed to connect to remote point, this will lead to endless loop
                if (socketError == SocketError.Success)
                {
                    SocketAsyncEventArgs eventArg = GetEventArg(true);
                    if (eventArg == null)
                    {
                        return;
                    }

                    bool willRaiseEvent = socket.ReceiveAsync(eventArg);
                    if (!willRaiseEvent)
                    {
                        ProcessReceive(eventArg, false);
                    }
                }

                return;
            }

            if (e.SocketError == SocketError.Success)
            {
                Logger.Debug("Successfully connected to the server");


                if (handlerPipeline.InHeader != null)
                {
                    handlerPipeline.InHeader.OnConnected(this, e.SocketError);
                }

                PushFreeEventArg(e, true);

                lock (statusLock)
                {
                    status = NetworkParameters._Connection_STATUS_CONNECTED;
                }
            }
            else
            {
                Logger.Error("Failed to connect to {0}, Error Code:{1}", remote.ToString(), e.SocketError);
                if (handlerPipeline.InHeader != null)
                {
                    handlerPipeline.InHeader.OnConnected(this, e.SocketError);
                }

                PushFreeEventArg(e, true);

                lock (statusLock)
                {
                    status = NetworkParameters._Connection_STATUS_CLOSED;
                }
            }
        }

        /// <summary>
        /// Called when a ReceiveAsync operation completes
        /// </summary>
        private void ProcessReceive(SocketAsyncEventArgs e, bool isPolling)
        {
            if (!isPolling)
            {
                SocketError socketError = e.SocketError;
                int bytesTransferred = e.BytesTransferred;
                PushAppendEventArg(e);
                if (socketError == SocketError.Success && bytesTransferred > 0)
                {
                    SocketAsyncEventArgs eventArg = GetEventArg(true);
                    if (eventArg == null)
                    {
                        try
                        {
                            this.Disconnect();
                        }
                        catch (Exception)
                        { }
                        Logger.Error("There is no event to post for receive, so actively close the socket");
                        if (handlerPipeline.InHeader != null)
                        {
                            handlerPipeline.InHeader.OnDisconnected(this, SocketError.NoBufferSpaceAvailable);
                        }
                        return;
                    }

                    if (socket != null)
                    {
                        bool willRaiseEvent = socket.ReceiveAsync(eventArg);
                        if (!willRaiseEvent)
                        {
                            ProcessReceive(eventArg, false);
                        }
                    }
                }

                return;
            }

            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
            {
                // Data has now been sent and received from the server.
                if (handlerPipeline.InHeader != null)
                {
                    try
                    {
                        msgBuffer.WriteBytes(e.Buffer, e.Offset, e.BytesTransferred);
                        handlerPipeline.InHeader.OnReceived(this, msgBuffer, e.Offset, e.BytesTransferred);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("***" + ex.Message);
                        Logger.Error("***" + ex.ToString());
                    }
                }

                PushFreeEventArg(e, false);
            }
            else
            {
                if (handlerPipeline.InHeader != null)
                {
                    handlerPipeline.InHeader.OnDisconnected(this, e.SocketError);
                }
                lock (statusLock)
                {
                    if (status != NetworkParameters._Connection_STATUS_CLOSED)
                    {
                        // close the socket associated with the client
                        try
                        {
                            this.Disconnect();
                        }
                        // throws if client process has already closed
                        catch (Exception) { }
                        //socket.Close();

                        Logger.Error("Disconnected from {0}, Error code:{1}", remote.ToString(), e.SocketError);
                        status = NetworkParameters._Connection_STATUS_CLOSED;
                    }

                }
                PushFreeEventArg(e, false);
            }

        }


        /// <summary>
        /// Called when a SendAsync operation completes
        /// </summary>
        private void ProcessSend(SocketAsyncEventArgs e, bool isPolling)
        {
            if (!isPolling)
            {
                PushAppendEventArg(e);
                return;
            }

            if (e.SocketError != SocketError.Success)
            {
                if (handlerPipeline.InHeader != null)
                {
                    handlerPipeline.InHeader.OnDisconnected(this, e.SocketError);
                }
                lock (statusLock)
                {
                    if (status != NetworkParameters._Connection_STATUS_CLOSED)
                    {
                        // close the socket associated with the client
                        try
                        {
                            this.Disconnect();
                        }
                        // throws if client process has already closed
                        catch (Exception) { }

                        Logger.Error("Disconnected from {0}, Error code:{1}", remote.ToString(), e.SocketError);
                        status = NetworkParameters._Connection_STATUS_CLOSED;
                    }
                }
            }

            PushFreeEventArg(e, true);
        }

        private IPAddress GetIPV4Address(string hostname)
        {
            IPAddress[] hosts = null;
            try
            {
                hosts = Dns.GetHostAddresses(hostname);
            }
            catch (Exception ex)
            {
                Logger.Error("Dns.GetHostAddresses " + ex.Message);
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
    }

}
