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
		public static void ShouldDeserializeAllApplications()
		{
			Collection<Application> applications = settingsFileReader!.ReadData(TestSettingsFile).applications;
			
			Assert.AreEqual(3, applications.Count);
		}

		[Test]
		public static void ShouldDeserializeApplicationProperties()
		{
			Collection<Application> applications = settingsFileReader!.ReadData(TestSettingsFile).applications;
			
			Assert.AreEqual(
				Guid.Parse("420fd454-0c36-499d-bde4-146823b16147"), 
				applications[1].ApplicationID);
		}
		
		[Test]
		public static void ShouldDeserializeDesktopApplications()
		{
			Collection<Application> applications = settingsFileReader!.ReadData(TestSettingsFile).applications;
			
			Assert.AreEqual(
				typeof(DesktopApplication), 
				applications[1].GetType());

			Assert.AreNotEqual(
				typeof(DesktopApplication),
				applications[0].GetType());
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
		public static void ShouldMatchApplicationsWithProfiles()
		{
			(Collection<Application> applications, Collection<Profile> profiles) 
				= settingsFileReader!.ReadData(TestSettingsFile);

			Application? bg3Application = applications.FirstOrDefault((Application application) => application.Name == "Baldur's Gate 3");
			IEnumerable<Profile> bg3Profiles 
				= profiles.Where((Profile profile) => profile.Application?.Name == "Baldur's Gate 3");
			
			Assert.AreEqual(2, bg3Application!.Profiles.Count);
			Assert.AreEqual(2, bg3Profiles.Count());
		}
		
				
		[TestFixture]
		public static class CustomApplicationTests
		{
			[SetUp]
			public static void SetupCustomApplicationTests()
			{
				settingsFileReader = new GHubSettingsFileReader();
				TestSettingsFile = 
					new MemoryStream(Properties.Resources.ExampleCustomGameGHUBSettings, false);
			}
			
			
			[TearDown]
			public static void TearDownCustomApplicationTests()
			{
				TestSettingsFile.Close();
			}

			[Test]
			public static void ShouldDeserializePosterDataOfCustomApplications()
			{
				Collection<Application> applications = settingsFileReader!.ReadData(TestSettingsFile).applications;
				
				Assert.IsTrue(applications[0].HasPoster);
				Assert.IsTrue(applications[0].IsCustom);
				Assert.AreEqual(new Size(256),applications[0].Poster.Size());
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