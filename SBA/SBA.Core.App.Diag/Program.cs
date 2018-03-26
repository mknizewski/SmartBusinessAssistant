using SBA.BOL.Common.Factory;
using System;

namespace SBA.Core.App.Diag
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Konsola diagnostyczna Core");
            Console.WriteLine("Polecenia:");
            Console.WriteLine("- Aby uruchomić natychmiastowo joba wpisz: <name> <parameters>");
            Console.WriteLine("- Aby wyłączyć wpisz: exit");
            SimpleFactory.Get<DiagManager>().Process();
        }
    }
}