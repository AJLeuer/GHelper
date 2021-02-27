using System;
using System.Collections.ObjectModel;
using GHelperLogic.Model;
using NDepend.Path;
using NUnit.Framework;

namespace GHelperTest
{
	[TestFixture]
	public static class ProfileTests
	{
		[Test]
		public static void CopiesShouldBeIdentical()
		{
			var profile = new Profile
		                  {
			                  Name = "BloodFeast5",
			                  ApplicationID = Guid.NewGuid(),
			                  Version = 2,
			                  ApplicationPath = PathHelpers.ToAbsoluteFilePath(@"c:\Users\Adam\BloodFeast5.exe"),
			                  IsInstalled = true,
			                  Commands = new Collection<Command> { new Command { CardID = Guid.NewGuid() }, new Command { CardID = Guid.NewGuid() } }
		                  };

			Profile copy = new (profile);
			
			Assert.AreEqual(profile, copy);
		}
	}
}