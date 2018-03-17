using SBA.Core.BOL.Threads;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SBA.Core.BOL.ThreadsSupervisior
{
    public class ThreadWorkflow
    {
        private List<TaskStatus> _waitStatues = new List<TaskStatus>
        {
            TaskStatus.WaitingForActivation,
            TaskStatus.WaitingForChildrenToComplete,
            TaskStatus.WaitingToRun
        };

        private List<TaskStatus> _faultStatuses = new List<TaskStatus>
        {
            TaskStatus.Faulted,
            TaskStatus.Canceled
        };

        public void CheckJobs(List<BaseThread> threads)
        {
            OptimalizeWorkflow();
            foreach (var thread in threads)
                HandleJob(thread);
        }

        public void OptimalizeWorkflow() =>
            Thread.Sleep(1);

        private void HandleJob(BaseThread thread)
        {
            ThreadStatus status = GetThreadStatus(thread);
            switch (status)
            {
                case ThreadStatus.ToRun:
                case ThreadStatus.CanceledOrFaulted:
                    RunJob(thread);
                    break;
                case ThreadStatus.Working:
                case ThreadStatus.Sheduled:
                case ThreadStatus.ToSkip:
                    break;
                default:
                    break;
            }
        }

        private ThreadStatus GetThreadStatus(BaseThread thread)
        {
            if (thread.ExcecutionPlan.ForceRun)
                return ThreadStatus.ToRun;

            if (thread.ExcecutionPlan.RunManually)
                return ThreadStatus.ToSkip;

            if (thread.Job == null)
                return ThreadStatus.ToRun;

            if (_waitStatues.Contains(thread.Job.Status))
                return ThreadStatus.Sheduled;

            if (_faultStatuses.Contains(thread.Job.Status))
                return ThreadStatus.CanceledOrFaulted;

            if (thread.Job.Status == TaskStatus.RanToCompletion)
                return ThreadStatus.ToRun;

            return ThreadStatus.Working;
        }

        private void RunJob(BaseThread thread) =>
            thread.RunJob();

        private enum ThreadStatus
        {
            ToRun,
            Working,
            Sheduled,
            CanceledOrFaulted,
            ToSkip
        }
    }
}