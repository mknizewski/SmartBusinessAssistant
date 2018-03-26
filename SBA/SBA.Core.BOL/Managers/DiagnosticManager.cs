using SBA.Core.BOL.Infrastructure;
using System;
using System.Linq;

namespace SBA.Core.BOL.Managers
{
    public interface IDiagnosticManager
    {
        string RunJob(string command);
    }

    public class DiagnosticManager : IDiagnosticManager
    {
        public string RunJob(string command)
        {
            string[] splitedCommand = command.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (splitedCommand.Length == 1)
                return Settings.Supervisior.ForceRun(splitedCommand[0]) ? "OK" : "Unable to force run this job";
            else if (splitedCommand.Length > 1)
            {
                string jobName = splitedCommand[0];
                string[] jobParams = splitedCommand
                    .Where((value, key) => key != 0)
                    .ToArray();

                return Settings.Supervisior.ForceRun(jobName, jobParams) ? "OK" : "Unable to force run this job";
            }

            return "No command found";
        }
    }
}