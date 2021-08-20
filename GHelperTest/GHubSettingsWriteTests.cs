using System.IO;
using System.Linq;
using System.Text;
using GHelperLogic.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Optional.Unsafe;

namespace GHelperTest
{
	[TestFixture]
	public static partial class GHubSettingsWriteTests
	{
		private static GHubSettingsFileReaderWriter? settingsFileReaderWriter;
		private static MemoryStream TestSettingsFile = new (Properties.Resources.ExampleJSONGHUBSettings, true);
		
		[SetUp]
		public static void Setup()
		{
			TestSettingsFile = new MemoryStream(Properties.Resources.ExampleJSONGHUBSettings, true);
			settingsFileReaderWriter = new GHubSettingsFileReaderWriter(TestSettingsFile);

			TestHelpers.StubImageFileHTTPResponses();
		}

		[TearDown]
		public static void TearDown()
		{
			TestSettingsFile.Close();
		}

		[Test]
		public static void SerializedGHubSettingsJSONShouldMatchInput()
		{
			var (testSettingsFileOriginal, testSettingsFileDuplicate) = TestSettingsFile.Duplicate();
			GHubSettingsFileReaderWriter settingsFileReaderWriter2 = new (testSettingsFileOriginal);
			GHubSettingsFile gHubSettingsFile = settingsFileReaderWriter2.Read().ValueOrFailure()!;

			string reSerializedGHubSettingsFile = settingsFileReaderWriter?.Serialize(gHubSettingsFile)!;
			string originalGHubSettingsFile = Encoding.UTF8.GetString(testSettingsFileDuplicate.ToArray(), 0 , (int) TestSettingsFile.Length);

			JObject originalGHubSettingsFileJSON = ConvertToJSONObject(originalGHubSettingsFile);
			JObject reSerializedGHubSettingsFileJSON = ConvertToJSONObject(reSerializedGHubSettingsFile);
			
			Assert.AreEqual(originalGHubSettingsFileJSON.Children().Count(), reSerializedGHubSettingsFileJSON.Children().Count());
		}
		
		[TestFixture]
		public static class CustomApplicationTests
		{
			[SetUp]
			public static void SetupCustomApplicationTests()
			{
				TestSettingsFile = new MemoryStream(Properties.Resources.ExampleJSONCustomGameGHUBSettings, false);
				settingsFileReaderWriter = new GHubSettingsFileReaderWriter(TestSettingsFile);
			}
			
			
			[TearDown]
			public static void TearDownCustomApplicationTests()
			{
				TestSettingsFile.Close();
			}

			[Test]
			public static void ShouldNotSerializePosterDataOfCustomApplications()
			{
				var (testSettingsFileOriginal, _) = TestSettingsFile.Duplicate();
				settingsFileReaderWriter = new GHubSettingsFileReaderWriter(testSettingsFileOriginal);
				GHubSettingsFile gHubSettingsFile = settingsFileReaderWriter.Read().ValueOrFailure()!;

				string reSerializedGHubSettingsFile = JsonConvert.SerializeObject(gHubSettingsFile, Formatting.Indented);
				GHubSettingsFile reDeserializedGHubSettingsFile = JsonConvert.DeserializeObject<GHubSettingsFile>(reSerializedGHubSettingsFile);

				Assert.Null(reDeserializedGHubSettingsFile.Applications?.Applications?[0].Poster);
			}
		}

		private static JObject ConvertToJSONObject(string jsonString)
		{
			using TextReader reader = new StreamReader(new StringStream(jsonString));
			using JsonReader jsonTextReader = new JsonTextReader(reader);
			JObject parsedJSONObject = JObject.Load(jsonTextReader);
			return parsedJSONObject;
		}
	}
}