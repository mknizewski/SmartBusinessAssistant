using SBA.Core.BOL.Infrastructure;

namespace SBA.Core.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Startup.Run(args);
            System.Console.ReadKey();
        }
    }
}