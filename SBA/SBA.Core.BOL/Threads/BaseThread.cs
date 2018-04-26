using SBA.Core.BOL.Infrastructure;
using System;
using System.Threading.Tasks;

namespace SBA.Core.BOL.Threads
{
    public abstract class BaseThread : IThread
    {
        public ExcecutionPlan ExcecutionPlan { get; set; }
        public Task Job { get; set; }
        public abstract T DoJob<T>() where T : class;

        private void StartOutput()
        {
            string output = string.Format("Started thread: {0} at {1} [{2}]\n",
                ExcecutionPlan.ThreadName,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Job.Id);

            SimpleFactory.GetLogger().RegisterLogToConsole(output, false);
            SimpleFactory.GetLogger().RegisterLogToFile(output);
        }
            

        private void EndOutput()
        {
            string output = string.Format("Ended thread: {0} at {1} [{2}]\n",
                ExcecutionPlan.ThreadName,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Job.Id);

            SimpleFactory.GetLogger().RegisterLogToConsole(output, false);
            SimpleFactory.GetLogger().RegisterLogToFile(output);
        }

        private void ExceptionOutput(Exception ex)
        {
            string output = string.Format("Exception on thread: {0} at {1}\nMessage: {2}\nInnerMessage: {3}\n",
                ExcecutionPlan.ThreadName,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ex.Message,
                ex?.InnerException?.Message);

            SimpleFactory.GetLogger().RegisterLogToConsole(output, false);
            SimpleFactory.GetLogger().RegisterLogToFile(output);
        }

        private void UpdateExcecutationPlan()
        {
            ExcecutionPlan.LastExecuteTime = DateTime.Now;
            ExcecutionPlan.ForceRun = false;
        }

        public void RunJob() =>
            Job = Task.Run(async delegate 
            {
                try
                {
                    if (ExcecutionPlan.ExecuteTime != null && 
                       !ExcecutionPlan.ForceRun)
                        await Task.Delay(ExcecutionPlan.ExecuteTime);

                    UpdateExcecutationPlan();
                    StartOutput();
                    DoJob<Nothing>();
                }
                catch (Exception ex)
                {
                    ExceptionOutput(ex);
                }
                finally
                {
                    EndOutput();
                }
            });
    }
}