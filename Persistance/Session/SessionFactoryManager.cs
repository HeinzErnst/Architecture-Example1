using NHibernate;

namespace Persistance.Session
{
    public class SessionFactoryManager
    {
        private static ISessionFactory _SessionFactory;
        private static readonly object _LockObject = new object();

        public SessionFactoryManager()
        {

        }
        public ISessionFactory Instance
        {
            get
            {
                lock (_LockObject)
                {
                    if (_SessionFactory == null)
                    {
                        _SessionFactory = DbConfiguration.Get().BuildSessionFactory();
                    }

                    return _SessionFactory;
                }

            }
        }
    }
}
