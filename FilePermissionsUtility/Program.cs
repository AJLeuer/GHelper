using System;
using System.IO;
using System.Threading;

namespace FilePermissionsUtility
{
    static class Program
    {
        static void Main()
        {
            // todo: remove debugging code
            ushort waitVariable = 0;

            while (waitVariable < 1)
            {
                Console.WriteLine("Waiting for debugger to attach");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            // end todo

            //var file = new FileInfo(Environment.GetCommandLineArgs()[1]);
            var file = new FileInfo(@"C:\ProgramData\LGHUB\depots\76775\core\applications\doom_2016_poster.png");
            FilePermissionsEditor.TakeOwnership(file);
        }
    }
}
