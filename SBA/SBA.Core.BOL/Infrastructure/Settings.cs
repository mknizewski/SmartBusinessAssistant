using SBA.Core.BOL.ThreadsSupervisior;

namespace SBA.Core.BOL.Infrastructure
{
    public static class Settings
    {
        internal static core Core => core.Default;
        internal static ThreadSupervisior Supervisior;
    }
}