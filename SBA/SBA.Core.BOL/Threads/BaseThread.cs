using System;
using System.Threading.Tasks;

namespace SBA.Core.BOL.Threads
{
    public abstract class BaseThread : IThread
    {
        public ExcecutionPlan ExcecutionPlan { get; set; }
        public Task Job { get; set; }
        public abstract void DoJob();

        private void StartOutput() =>
            Console.WriteLine("Started thread: {0} at {1} [{2}]",
                ExcecutionPlan.ThreadName,
                DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"),
                Job.Id);

        private void EndOutput() =>
            Console.WriteLine("Ended thread: {0} at {1} [{2}]",
                ExcecutionPlan.ThreadName,
                DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"),
                Job.Id);

        private void ExceptionOutput(Exception ex) =>
            Console.WriteLine("Exception on thread: {0} at {1}\nMessage: {2}\nInnerMessage: {3}",
                ExcecutionPlan.ThreadName,
                DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"),
                ex.Message,
                ex?.InnerException?.Message);

        private void UpdateExcecutationPlan()
        {
            ExcecutionPlan.LastExecuteTime = DateTime.Now;
        }

        public void RunJob() =>
            Job = Task.Run(async delegate 
            {
                try
                {
                    if (ExcecutionPlan.ExecuteTime != null)
                        await Task.Delay(ExcecutionPlan.ExecuteTime);

                    UpdateExcecutationPlan();
                    StartOutput();
                    DoJob();
                }
                catch (Exception ex) // TODO: Rejestrować ex do pliku .log
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