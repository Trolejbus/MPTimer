using Autofac;
using MPTimer.Extensions;
using MPTimer.Interfaces;
using MPTimer.Models;

namespace MPTimer.Controllers
{
    internal class SourceControlController : ISourceControlController, IStartableAsync
    {
        public event Action<IEnumerable<TraySourceControl>>? SourceControlLoaded;
        public event Action<IEnumerable<TraySourceControlStatus>>? SourceControlStatusUpdated;

        private readonly ISourceControlService _sourceControlService;
        private readonly List<FileSystemWatcher> fileSystemWatchers = new List<FileSystemWatcher>();

        private IEnumerable<TraySourceControl> sourceControls = new List<TraySourceControl>();
        private List<TraySourceControlStatus> sourceControlStatuses = new List<TraySourceControlStatus>();

        public SourceControlController(ISourceControlService sourceControlService)
        {
            _sourceControlService = sourceControlService;
        }

        public async Task StartAsync()
        {
            await LoadSourceControls();    
        }

        private async Task LoadSourceControls()
        {
            sourceControls = await _sourceControlService.GetWatched();
            SourceControlLoaded?.Invoke(sourceControls);
            UpdateStatuses(sourceControls);
        }

        private void UpdateStatuses(IEnumerable<TraySourceControl>  sourceControls)
        {
            foreach (var sourceControl in sourceControls)
            {
                var fileSystemWatcher = new FileSystemWatcher(Path.Combine(sourceControl.Path, @".git"), "HEAD*");
                fileSystemWatcher.NotifyFilter = NotifyFilters.Attributes
                    | NotifyFilters.CreationTime
                    | NotifyFilters.DirectoryName
                    | NotifyFilters.FileName
                    | NotifyFilters.LastAccess
                    | NotifyFilters.LastWrite
                    | NotifyFilters.Security
                    | NotifyFilters.Size;
                fileSystemWatcher.Renamed += (_, e) => OnRenamed(sourceControl.Id, sourceControl.Path);
                fileSystemWatcher.EnableRaisingEvents = true;
                fileSystemWatchers.Add(fileSystemWatcher);
                var status = new TraySourceControlStatus(sourceControl.Id, DateTime.UtcNow, GetBranch(sourceControl.Path));
                sourceControlStatuses.Add(status);
                _sourceControlService.ChangeBranch(sourceControl.Id, status.BranchName);
            }

            SourceControlStatusUpdated?.Invoke(sourceControlStatuses);
        }

        private void OnRenamed(Guid id, string path)
        {
            var branch = GetBranch(path);
            sourceControlStatuses.First(s => s.SourceControlId == id).BranchName = branch;
            SourceControlStatusUpdated?.Invoke(sourceControlStatuses);
            _sourceControlService.ChangeBranch(id, branch);
        }

        private static string GetBranch(string path)
        {
            Thread.Sleep(1000);
            var content = File.ReadAllText(Path.Combine(path, ".git/HEAD"));
            content = content.TrimStart("ref: refs/").TrimEnd('\n');
            return content.TrimStart("heads/");
        }
    }
}
