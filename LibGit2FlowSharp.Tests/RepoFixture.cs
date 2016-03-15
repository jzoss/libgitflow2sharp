using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Xunit;
using LibGit2Sharp;

namespace LibGit2FlowSharp.Tests
{
    public class RepoFixture : IDisposable
    {

        private const string basepath = @"C:\test\";
        private string _remoteRepoPath;
        private string _localRepoPath;


        public RepoFixture()
        {
            _remoteRepoPath = TestHelpers.CreateEmptyRepo(basepath, "testRemote", true);
            _localRepoPath = Path.Combine(basepath, "testLocal");
            Repository.Clone(_remoteRepoPath, _localRepoPath);
            SetupRepo();
        }

        private void SetupRepo()
        {
            using (var repo = new Repository(_localRepoPath))
            {
                var author = repo.Config.BuildSignature(DateTimeOffset.Now);
                repo.Flow().Init(new GitFlowRepoSettings(), author);
                TestHelpers.AddCommitToRepo(repo);
                PushOptions options = new PushOptions();
                Remote remote = repo.Network.Remotes["origin"];
                repo.Network.Push(repo.Head);
            }
        }

        public string RemoteRepoPath
        {
            get { return _remoteRepoPath; }
        }

        public string LocalRepoPath
        {
            get { return _localRepoPath; }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [CollectionDefinition("Repo collection")]
    public class DatabaseCollection : ICollectionFixture<RepoFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
