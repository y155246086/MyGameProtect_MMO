using System;
using System.Collections.Generic;
using System.Text;
using Network;

namespace ClientHelper
{
    public class ClientHelperNetworkInitializer : AbstractNetworkInitializer
    {
        AbstractMessageInitializer msgInitializer;
        MessageDelegateProcessor msgDelegateProcessor;

        public ClientHelperNetworkInitializer(AbstractMessageInitializer msgInitializer, MessageDelegateProcessor msgDelegateProcessor)
        {
            this.msgInitializer = msgInitializer;
            this.msgDelegateProcessor = msgDelegateProcessor;
        }
        public override void Initial(NetworkHandlerPipeline pipeline)
        {
            pipeline.AddHandler(new LengthFieldBasedFrameDecoder(200 * 1024, 0, 4, 0, 4));
            pipeline.AddHandler(new LengthFieldPrepender(4, 0, false));
            pipeline.AddHandler(new MessageInProcessor(msgInitializer, msgDelegateProcessor));
            pipeline.AddHandler(new MessageOutProcessor());
        }
    }
}
