using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AoiUtils.Core.Models;
using AoiUtils.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AoiUtils.ViewModels;

public partial class DebloatViewModel : ViewModelBase
{
    private readonly DebloatService _debloatService;

    [ObservableProperty]
    private ObservableCollection<DebloatTask> _tasks;

    [ObservableProperty]
    private bool _isRunning;

    [ObservableProperty]
    private string _statusText = "";

    public DebloatViewModel(DebloatService debloatService)
    {
        _debloatService = debloatService;
        _tasks = new ObservableCollection<DebloatTask>(_debloatService.GetTasks());
    }

    [RelayCommand]
    private async Task RunSelectedTasksAsync()
    {
        var selected = Tasks.Where(t => t.IsSelected).ToList();
        if (!selected.Any()) return;

        IsRunning = true;
        
        foreach (var task in selected)
        {
            StatusText = $"Applying: {task.Name}...";
            await _debloatService.RunTaskAsync(task);
        }

        StatusText = "Optimization complete! Some changes may require a restart.";
        IsRunning = false;
    }
}
