using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public class Flow
    {
        internal Repository Repository { get; private set; }
        internal Prefix Prefix { get; private set; }
        public Flow(Repository repository)
        {
            Repository = repository;
            Prefix = new Prefix(this);
        }
    }
}
