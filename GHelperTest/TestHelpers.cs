using System;
using System.IO;
using GHelperLogic.IO;
using GHelperLogic.Utility.Wrappers;
using Moq;

namespace GHelperTest
{
	public static partial class GHubSettingsFileReaderOutputTests
	{
		public static class TestHelpers
		{
			public static void StubImageFileHTTPResponses()
			{
				var stubPosterImageFile = new MemoryStream(Properties.Resources.TestImage);
				var clientMock = new Mock<WebClientInterface> { CallBase = false };
				clientMock.Setup(
				                 (WebClientInterface webClient) =>
					                 webClient.OpenRead(It.IsAny<Uri>())).Returns(stubPosterImageFile);
				
				IOHelper.Client = clientMock.Object;
			}
		}
	}
}