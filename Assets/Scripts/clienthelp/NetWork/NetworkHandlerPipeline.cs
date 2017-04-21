using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    public class NetworkHandlerPipeline
    {
        AbstractNetworkInHandler inHeader;
        AbstractNetworkInHandler inTailer;
        AbstractNetworkOutHandler outHeader;
        AbstractNetworkOutHandler outTailer;

        public AbstractNetworkInHandler InHeader
        {
            get { return inHeader; }
        }

        public AbstractNetworkOutHandler OutHeader
        {
            get { return outHeader; }
        }

        public void AddHandler(AbstractNetworkInHandler handler)
        {
            lock(this)
            {
                if (inHeader ==null)
                {
                    inHeader = handler;
                }
                if(inTailer == null)
                {
                    inTailer = handler;
                }
                else
                {
                    inTailer.NextInHandler = handler;
                    inTailer = handler;
                }
            }
        }

        public void AddHandler(AbstractNetworkOutHandler handler)
        {
            lock (this)
            {
                if (outTailer == null)
                {
                    outTailer = handler;
                }

                handler.NextOutHandler = outHeader;
                outHeader = handler;
            }
        }

    }
}
