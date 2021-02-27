using System;
using GHelperLogic.Model;
using NDepend.Path;
using NodaTime;
using NUnit.Framework;

namespace GHelperTest
{
	[TestFixture]
	public static class ApplicationTests
	{
		[Test]
		public static void CopiesShouldBeIdentical()
		{
			var application = new Application
			                  {
				                  Name = "BloodFeast5",
				                  ApplicationID = Guid.NewGuid(),
				                  LastRunTime = new LocalDateTime(1999, 9, 10, 0, 0),
				                  Version = 2,
				                  ApplicationPath = PathHelpers.ToAbsoluteFilePath(@"c:\Users\Adam\BloodFeast5.exe"),
				                  IsInstalled = true,
				                  Profiles = { new Profile { ID = Guid.NewGuid() }, new Profile { ID = Guid.NewGuid() } }
			                  };

			Application copy = new (application);
			
			Assert.AreEqual(application, copy);
		}
	}
}