using SBA.Core.BOL.Dictionaries;
using SBA.Core.BOL.ThreadsSupervisior;
using SBA.DAL.Context.InferenceDb.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        public static void SeedWordVariety()
        {
            var wordVarietyDictionary = SimpleFactory.Get<WordVarietyDictionary>();
            var context = SimpleFactory.Get<SbaInferenceContext>();

            wordVarietyDictionary.SeedData(context);
        }

        public static void ClearCurrentConsoleLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void ProcessingScreen(string loadingMessage)
        {
            Console.WriteLine($"{loadingMessage} |");
            Thread.Sleep(150);
            ClearCurrentConsoleLine();

            Console.WriteLine($"{loadingMessage} /");
            Thread.Sleep(150);
            ClearCurrentConsoleLine();

            Console.WriteLine($"{loadingMessage} --");
            Thread.Sleep(150);
            ClearCurrentConsoleLine();

            Console.WriteLine($"{loadingMessage} \\");
            Thread.Sleep(150);
            ClearCurrentConsoleLine();

            Console.WriteLine($"{loadingMessage} |");
            Thread.Sleep(150);
            ClearCurrentConsoleLine();

            Console.WriteLine($"{loadingMessage} /");
            Thread.Sleep(150);
            ClearCurrentConsoleLine();

            Console.WriteLine($"{loadingMessage} --");
            Thread.Sleep(150);
            ClearCurrentConsoleLine();

            Console.WriteLine($"{loadingMessage} \\");
            Thread.Sleep(150);
            ClearCurrentConsoleLine();
        }

        public static void RunTask(Action action, string loadingMessage)
        {
            var task = Task.Run(() => action());
            while (!task.IsCompleted) { ProcessingScreen(loadingMessage); }
            Console.WriteLine($"{loadingMessage} OK");
        }

        public static void RunTaskBackground(Action action, string message)
        {
            Task.Run(() => action());
            Console.WriteLine($"{message} (in background)");
        }
    }
}