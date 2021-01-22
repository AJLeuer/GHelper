using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using GHelperLogic.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility.Wrappers;
using Moq;
using NUnit.Framework;
using SixLabors.ImageSharp;
using Color = System.Drawing.Color;

namespace GHelperTest
{
	[TestFixture]
	public static class GHubSettingsFileReaderTest
	{
		private static GHubSettingsFileReader? settingsFileReader;
		private static Stream TestSettingsFile = 
			new MemoryStream(Properties.Resources.ExampleGHUBSettings, false);
		
		[SetUp]
		public static void Setup()
		{
			settingsFileReader = new GHubSettingsFileReader();
			TestSettingsFile = 
				new MemoryStream(Properties.Resources.ExampleGHUBSettings, false);
			
			TestHelpers.StubImageFileHTTPResponses();
		}

		[TearDown]
		public static void TearDown()
		{
			TestSettingsFile.Close();
		}

		[Test]
		public static void ShouldDeserializeAllContexts()
		{
			Collection<Context> contexts = settingsFileReader!.ReadData(TestSettingsFile).contexts;
			
			Assert.AreEqual(3, contexts.Count);
		}

		[Test]
		public static void ShouldDeserializeContextProperties()
		{
			Collection<Context> contexts = settingsFileReader!.ReadData(TestSettingsFile).contexts;
			
			Assert.AreEqual(
				Guid.Parse("420fd454-0c36-499d-bde4-146823b16147"), 
				contexts[1].ApplicationID);
		}
		
		[Test]
		public static void ShouldDeserializeDesktopContexts()
		{
			Collection<Context> contexts = settingsFileReader!.ReadData(TestSettingsFile).contexts;
			
			Assert.AreEqual(
				typeof(DesktopContext), 
				contexts[1].GetType());

			Assert.AreNotEqual(
				typeof(DesktopContext),
				contexts[0].GetType());
		}
		
		[Test]
		public static void ShouldDeserializeAllProfiles()
		{
			Collection<Profile> profiles = settingsFileReader!.ReadData(TestSettingsFile).profiles;
			
			Assert.AreEqual(5, profiles.Count);
		}

		[Test]
		public static void ShouldDeserializeProfileProperties()
		{
			Collection<Profile> profiles = settingsFileReader!.ReadData(TestSettingsFile).profiles;
			
			Assert.AreEqual(
				"Horizon Zero Dawn Complete Edition", 
				profiles[2].Name);
			Assert.AreEqual(
				Color.FromArgb(0x00, 0xFF, 0x40),
				profiles[2].CategoryColors![1].Hex);
		}
		
		[Test]
		public static void ShouldDeserializeDefaultProfiles()
		{
			Collection<Profile> profiles = settingsFileReader!.ReadData(TestSettingsFile).profiles;
			
			Assert.AreEqual(typeof(DefaultProfile), profiles[3].GetType());
			Assert.AreNotEqual(typeof(DefaultProfile), profiles[1].GetType());
		}
		
		[Test]
		public static void ShouldMatchContextsWithProfiles()
		{
			(Collection<Context> contexts, Collection<Profile> profiles) 
				= settingsFileReader!.ReadData(TestSettingsFile);

			Context? bg3Context = contexts.FirstOrDefault((Context context) => context.Name == "Baldur's Gate 3");
			IEnumerable<Profile> bg3Profiles 
				= profiles.Where((Profile profile) => profile.Context?.Name == "Baldur's Gate 3");
			
			Assert.AreEqual(2, bg3Context!.Profiles.Count);
			Assert.AreEqual(2, bg3Profiles.Count());
		}
		
				
		[TestFixture]
		public static class CustomGameTests
		{
			[SetUp]
			public static void SetupCustomGameTests()
			{
				settingsFileReader = new GHubSettingsFileReader();
				TestSettingsFile = 
					new MemoryStream(Properties.Resources.ExampleCustomGameGHUBSettings, false);
			}
			
			
			[TearDown]
			public static void TearDownCustomGameTests()
			{
				TestSettingsFile.Close();
			}

			[Test]
			public static void ShouldDeserializePosterDataOfCustomContexts()
			{
				Collection<Context> contexts = settingsFileReader!.ReadData(TestSettingsFile).contexts;
				
				Assert.IsTrue(contexts[0].HasPoster);
				Assert.IsTrue(contexts[0].IsCustom);
				Assert.AreEqual(new Size(256),contexts[0].Poster.Size());
			}
		}

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