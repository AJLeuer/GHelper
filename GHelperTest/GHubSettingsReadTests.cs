using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GHelperLogic.IO;
using GHelperLogic.Model;
using NUnit.Framework;
using Optional.Unsafe;
using SixLabors.ImageSharp;
using Color = System.Drawing.Color;

namespace GHelperTest 
{
	[TestFixture]
	public static class GHubSettingsReadTests
	{
		private static GHubSettingsIO? SettingsFileReader;
		private static Stream TestSettingsFile = new MemoryStream(Properties.Resources.ExampleJSONGHUBSettings, false);
		
		[SetUp]
		public static void Setup()
		{
			TestSettingsFile = new MemoryStream(Properties.Resources.ExampleJSONGHUBSettings, false);
			SettingsFileReader = new GHubSettingsFileReaderWriter(gHubSettingsStream: TestSettingsFile);

			GHubSettingsWriteTests.TestHelpers.StubImageFileHTTPResponses();
		}

		[TearDown]
		public static void TearDown()
		{
			TestSettingsFile.Close();
		}

		[Test]
		public static void ShouldDeserializeAllApplications()
		{
			ICollection<Application> applications = SettingsFileReader!.Read().ValueOrFailure().Applications?.Applications!;
			
			Assert.AreEqual(4, applications.Count);
		}

		[Test]
		public static void ShouldDeserializeApplicationProperties()
		{
			ICollection<Application> applications = SettingsFileReader!.Read().ValueOrFailure().Applications?.Applications!;

			Assert.AreEqual(
				Guid.Parse("420fd454-0c36-499d-bde4-146823b16147"), 
				applications.ElementAt(1).ApplicationID);
		}
		
		[Test]
		public static void ShouldDeserializeDesktopApplications()
		{
			ICollection<Application> applications = SettingsFileReader!.Read().ValueOrFailure().Applications?.Applications!;

			Assert.AreEqual(
				typeof(DesktopApplication), 
				applications.ElementAt(1).GetType());

			Assert.AreNotEqual(
				typeof(DesktopApplication),
				applications.ElementAt(0).GetType());
		}
		
		[Test]
		public static void ShouldDeserializeCustomApplications()
		{
			ICollection<Application> applications = SettingsFileReader!.Read().ValueOrFailure().Applications?.Applications!;

			Assert.AreEqual(
			                typeof(CustomApplication), 
			                applications.ElementAt(3).GetType());

			Assert.AreNotEqual(
			                   typeof(CustomApplication),
			                   applications.ElementAt(0).GetType());
		}
		
		[Test]
		public static void ShouldDeserializeAllProfiles()
		{
			ICollection<Profile> profiles = SettingsFileReader!.Read().ValueOrFailure().Profiles?.Profiles!;
			
			Assert.AreEqual(5, profiles.Count);
		}

		[Test]
		public static void ShouldDeserializeProfileProperties()
		{
			ICollection<Profile> profiles = SettingsFileReader!.Read().ValueOrFailure().Profiles?.Profiles!;

			Assert.AreEqual(
				"Horizon Zero Dawn Complete Edition", 
				profiles.ElementAt(2).Name);
			Assert.AreEqual(
				Color.FromArgb(0x00, 0xFF, 0x40),
				profiles.ElementAt(2).CategoryColors![1].Hex);
		}

		[Test]
		public static void ShouldMatchApplicationsWithProfiles()
		{
			GHubSettingsFile gHubSettingsFile = SettingsFileReader!.Read().ValueOrFailure();
			ICollection<Application> applications = gHubSettingsFile.Applications?.Applications!;
			ICollection<Profile> profiles = gHubSettingsFile.Profiles?.Profiles!;

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
				TestSettingsFile = new MemoryStream(Properties.Resources.ExampleJSONCustomGameGHUBSettings, false);
				SettingsFileReader = new GHubSettingsFileReaderWriter(TestSettingsFile);
			}
			
			
			[TearDown]
			public static void TearDownCustomApplicationTests()
			{
				TestSettingsFile.Close();
			}

			[Test]
			public static void ShouldDeserializePosterDataOfCustomApplications()
			{
				ICollection<Application> applications = SettingsFileReader!.Read().ValueOrFailure().Applications?.Applications!;

				Assert.IsTrue(applications.ElementAt(0).HasPoster);
				Assert.IsTrue(applications.ElementAt(0).IsCustom);
				Assert.AreEqual(new Size(256),applications.ElementAt(0).Poster.Size());
			}
		}
	}
}