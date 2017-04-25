using System;
using System.Collections.Generic;
using System.Text;
using Network;
using System.Net;
using Messages;
using com.kz.protocol;
using Common;
using com.kz.message.proto;

namespace ClientHelper
{
    public partial class ClientNetHelper
	{
		private Network.Message waitSendAuthMsg = null;
		
		public bool LoginAS(string asHost, int asPort, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo)
		{
			connection = NetworkManager.GetInstance().CreateConnection(this.type);
			connection.SetNetworkInitializer(networkInitializer);
			try
			{
				connection.ConnectAsync(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0), new IPEndPoint(NetUtil.GetIPV4Address(asHost), asPort));

				CA_LoginAuthMessage loginMsg = new CA_LoginAuthMessage();
				loginMsg.Protocol.email = accountName;
				loginMsg.Protocol.password = password;
				loginMsg.Protocol.channelID = channelId;
				loginMsg.Protocol.randomSeed = randomSeed;
				loginMsg.Protocol.version = version;
				loginMsg.Protocol.deviceInfo = deviceInfo;

				waitSendAuthMsg = loginMsg;
                //connection.Send(waitSendAuthMsg);
				return true;
			}
			catch(Exception ex)
			{
				Logger.Error(ex.ToString());
				Console.Write(ex.ToString());
				return false;
			}


			//If using ConnectAsync, need set the connection handler
			//ASLoginConnectionHandler connectionHandler = new ASLoginConnectionHandler(req);
			//connectionHandler.ClientHelper = this;
			//messageInitializer.SetConnectionActiveHandler(connectionHandler);

		}

		public bool ChangePasswordAS(string asHost, int asPort, string accountName, string oldPassword, string newPassword)
		{
			connection = NetworkManager.GetInstance().CreateConnection(this.type);
			connection.SetNetworkInitializer(networkInitializer);
			try
			{
				connection.ConnectAsync(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0), new IPEndPoint(NetUtil.GetIPV4Address(asHost), asPort));

				CA_ChangePasswordMessage message = new CA_ChangePasswordMessage();
				message.Protocol.email = accountName;
				message.Protocol.newPassword = newPassword;
				message.Protocol.oldPassword = oldPassword;

				waitSendAuthMsg = message;
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
				Console.Write(ex.ToString());
				return false;
			}
		}

		public bool BindAccountAS(string asHost, int asPort, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo)
		{
			connection = NetworkManager.GetInstance().CreateConnection(this.type);
			connection.SetNetworkInitializer(networkInitializer);
			try
			{
				connection.ConnectAsync(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0), new IPEndPoint(NetUtil.GetIPV4Address(asHost), asPort));

				CA_BindAccountMessage message = new CA_BindAccountMessage();
				message.Protocol.email = accountName;
				message.Protocol.password = password;
				message.Protocol.channelID = channelId;
				message.Protocol.randomSeed = randomSeed;
				message.Protocol.version = version;
				message.Protocol.deviceInfo = deviceInfo;

				waitSendAuthMsg = message;
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
				Console.Write(ex.ToString());
				return false;
			}
		}

		public bool QuickLoginAS(string asHost, int asPort, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo)
		{
			connection = NetworkManager.GetInstance().CreateConnection(this.type);
			connection.SetNetworkInitializer(networkInitializer);
			try
			{
				connection.ConnectAsync(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0), new IPEndPoint(NetUtil.GetIPV4Address(asHost), asPort));

				CA_QuickLoginAuthMessage loginMessage = new CA_QuickLoginAuthMessage();
				loginMessage.Protocol.channelID = channelId;
				loginMessage.Protocol.randomSeed = randomSeed;
				loginMessage.Protocol.version = version;
				loginMessage.Protocol.deviceInfo = deviceInfo;

				waitSendAuthMsg = loginMessage;
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
				Console.Write(ex.ToString());
				return false;
			}
		}

		public bool CreateAccountAS(string asHost, int asPort, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo)
		{
			connection = NetworkManager.GetInstance().CreateConnection(this.type);
			connection.SetNetworkInitializer(networkInitializer);
			try
			{
				connection.ConnectAsync(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0), new IPEndPoint(NetUtil.GetIPV4Address(asHost), asPort));

				CA_CreateAccountMessage createMessage = new CA_CreateAccountMessage();
				createMessage.Protocol.email = accountName;
				createMessage.Protocol.password = password;
				createMessage.Protocol.channelID = channelId;
				createMessage.Protocol.randomSeed = randomSeed;
				createMessage.Protocol.version = version;
				createMessage.Protocol.deviceInfo = deviceInfo;

				waitSendAuthMsg = createMessage;
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
				Console.Write(ex.ToString());
				return false;
			}
		}

		public bool ConnectToGS(string gsHost, int gsPort)
		{
            //Console.WriteLine("connecting game server..... ip = {0}, port = {1}", gsHost, gsPort);
			connection = NetworkManager.GetInstance().CreateConnection(this.type);
			connection.SetNetworkInitializer(networkInitializer);
			try
			{
				connection.ConnectAsync(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0), new IPEndPoint(NetUtil.GetIPV4Address(gsHost), gsPort));
			}
			catch(Exception ex)
			{
				Console.Write(ex.ToString());
				return false;
			}
			return true;
		}

	}
}
