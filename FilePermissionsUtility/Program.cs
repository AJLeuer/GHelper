using System;
using System.IO;

namespace FilePermissionsUtility
{
    static class Program
    {
        static void Main()
        {
            var file = new FileInfo(Environment.GetCommandLineArgs()[1]);
            FilePermissionsEditor.TakeOwnership(file);
        }
    }
}
