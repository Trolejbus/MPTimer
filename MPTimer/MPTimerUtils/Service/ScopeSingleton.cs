using Autofac;

namespace MPTimerUtils.Service
{
    public static class ScopeSingleton
    {
        private static ILifetimeScope? _scope;
        public static ILifetimeScope Scope
        {
            get => _scope ?? throw new Exception("Scope has not been set;");
            set => _scope = value;
        }
    }
}
