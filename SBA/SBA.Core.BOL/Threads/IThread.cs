using System.Threading.Tasks;

namespace SBA.Core.BOL.Threads
{
    public interface IThread
    {
        void DoJob(params string[] jobParams);
        void RunJob();
    }
}