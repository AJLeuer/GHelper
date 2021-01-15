using System;
using System.Collections.ObjectModel;
using System.IO;
using GHelper.Models;
using GHelperLogic.IO;
using NUnit.Framework;

namespace GHelperTest
{
	public class GHubSettingsFileReaderTest
	{
		private GHubSettingsFileReader settingsFileReader;
		private static readonly Stream TestSettingsFile = 
			new MemoryStream(Properties.Resources.ExampleGHUBSettings, false);
		
		[SetUp]
		public void Setup()
		{
			settingsFileReader = new GHubSettingsFileReader();
		}

		[Test]
		public void ShouldDeserializeAllContexts()
		{
			Collection<Context> contexts = settingsFileReader.ReadContexts(TestSettingsFile);
			
			Assert.AreEqual(3, contexts.Count);
			Assert.AreEqual(
				Guid.Parse("420fd454-0c36-499d-bde4-146823b16147"), 
				contexts[1].ApplicationID);
		}
	}
}