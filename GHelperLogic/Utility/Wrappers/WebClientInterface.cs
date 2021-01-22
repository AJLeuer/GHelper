using System;
using System.IO;
using System.Net;

namespace GHelperLogic.Utility.Wrappers
{
	public class WebClientInterface
	{
		public WebClient? Client { get; set; }

		public virtual Stream? OpenRead(Uri address)
		{
			return Client?.OpenRead(address);
		}

		public static implicit operator WebClient? (WebClientInterface webClientInterface)
		{
			return webClientInterface.Client;
		}
		
		public static implicit operator WebClientInterface (WebClient webClient)
		{
			return new WebClientInterface { Client = webClient };
		}
	}
}