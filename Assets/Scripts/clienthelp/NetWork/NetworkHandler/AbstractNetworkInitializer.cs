using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    public abstract class AbstractNetworkInitializer
    {
        public abstract void Initial(NetworkHandlerPipeline pipeline);
    }
}
