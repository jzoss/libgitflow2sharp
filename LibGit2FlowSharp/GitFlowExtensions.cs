using LibGit2Sharp;

namespace LibGit2FlowSharp
{
    public static partial class GitFlowExtensions
    {
        public static Flow Flow(this Repository repository)
        {
            return new Flow(repository);
        }


        internal static void Log(string message)
        {
            
        }

        internal static void LogError(string message, string error = "")
        {

        }

    }
}
