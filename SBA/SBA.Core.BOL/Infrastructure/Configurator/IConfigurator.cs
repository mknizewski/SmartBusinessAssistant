using SBA.Core.BOL.Threads;

namespace SBA.Core.BOL.Infrastructure.Configurator
{
    public interface IConfigurator<T> where T : BaseThread, IThread
    {
        T Configure();
    }
}