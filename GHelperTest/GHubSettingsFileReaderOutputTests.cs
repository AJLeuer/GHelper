using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using GHelperLogic.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SixLabors.ImageSharp;
using Color = System.Drawing.Color;

namespace GHelperTest
{
	[TestFixture]
	public static partial class GHubSettingsFileReaderOutputTests
	{
		private static GHubSettingsFileReaderWriter? settingsFileReaderWriter;
		private static MemoryStream TestSettingsFile = 
			new MemoryStream(Properties.Resources.ExampleGHUBSettings, true);
		
		[SetUp]
		public static void Setup()
		{
			settingsFileReaderWriter = new GHubSettingsFileReaderWriter();
			TestSettingsFile = 
				new MemoryStream(Properties.Resources.ExampleGHUBSettings, true);
			
			TestHelpers.StubImageFileHTTPResponses();
		}

		[TearDown]
		public static void TearDown()
		{
			TestSettingsFile.Close();
		}

		[Test]
		public static void SerializedGHubSettingsJSONShouldMatchInputVerbatim()
		{
			var (testSettingsFileOriginal, testSettingsFileDuplicate) = TestSettingsFile.Duplicate();
			GHubSettingsFile gHubSettingsFile = settingsFileReaderWriter?.DeserializeData(testSettingsFileOriginal)!;

			string reSerializedGHubSettingsFile = JsonConvert.SerializeObject(gHubSettingsFile, Formatting.Indented);
			string originalGHubSettingsFile = Encoding.UTF8.GetString(testSettingsFileDuplicate.ToArray(), 0 , (int) TestSettingsFile.Length);

			JObject originalGHubSettingsFileJSON = ConvertToJSONObject(originalGHubSettingsFile);
			JObject reSerializedGHubSettingsFileJSON = ConvertToJSONObject(reSerializedGHubSettingsFile);
			
			Assert.AreEqual(originalGHubSettingsFileJSON, reSerializedGHubSettingsFileJSON);
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