using SBA.Core.BOL.ThreadsSupervisior;
using SBA.DAL.Context.InferenceDb.Infrastructure;
using System;
using System.Threading;

namespace SBA.Core.BOL.Infrastructure
{
    public static class Settings
    {
        internal static core Core => core.Default;
        internal static ThreadSupervisior Supervisior;

        public static void InitDatabase()
        {
            var context = SimpleFactory.Get<SbaInferenceContext>();
            var dbSeed = SimpleFactory.Get<SbaInferenceInitializer>();

            dbSeed.InitializeDatabase(context);
        }

        public static void ClearCurrentConsoleLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void ProcessingScreen()
        {
            Console.WriteLine("Processing .");
            Thread.Sleep(200);
            ClearCurrentConsoleLine();

            Console.WriteLine("Processing ..");
            Thread.Sleep(200);
            ClearCurrentConsoleLine();

            Console.WriteLine("Processing ...");
            Thread.Sleep(200);
            ClearCurrentConsoleLine();
        }
    }
}