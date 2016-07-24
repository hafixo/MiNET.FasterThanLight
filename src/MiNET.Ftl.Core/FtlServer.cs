﻿using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MiNET.Ftl.Core.Proxy;
using MiNET.Utils;

namespace MiNET.Ftl.Core
{
	public class FtlServer
	{
		MiNetServer _proxy;

		public FtlServer()
		{
			int threads;
			int iothreads;
			ThreadPool.GetMinThreads(out threads, out iothreads);

			DedicatedThreadPool threadPool = new DedicatedThreadPool(new DedicatedThreadPoolSettings(threads));
			ProxyMessageHandler.FastThreadPool = threadPool;

			List<EndPoint> remoteServers = new List<EndPoint>();
			remoteServers.Add(new IPEndPoint(IPAddress.Loopback, 51234));
			_proxy = new MiNetServer();
			_proxy.ServerRole = ServerRole.Proxy;
			_proxy.ServerManager = new ProxyServerManager(remoteServers);
		}

		public void StartServer()
		{
			_proxy.StartServer();
		}

		public void StopServer()
		{
			_proxy.StopServer();
		}
	}
}