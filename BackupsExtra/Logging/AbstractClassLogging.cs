namespace BackupsExtra.Logging
{
    public abstract class AbstractClassLogging
    {
        public AbstractClassLogging()
        {
        }

        public TypeLogging TypeLogging
        {
            get;
            protected set;
        }

        public bool Configuration
        {
            get;
            set;
        }
    }
}