using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AoiUtils.Core.Models;
using AoiUtils.Core.Services;
using AoiUtils.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AoiUtils.ViewModels;

public partial class DebloatViewModel : ViewModelBase
{
    private readonly DebloatService _debloatService;
    private readonly List<DebloatTask> _allTasks;
    
    [ObservableProperty]
    private LocalizationManager _localizer;

    [ObservableProperty]
    private ObservableCollection<DebloatTask> _filteredTasks;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusText = "";

    public DebloatViewModel(DebloatService debloatService, LocalizationManager localizer)
    {
        _debloatService = debloatService;
        _localizer = localizer;
        _allTasks = _debloatService.GetTasks();
        _filteredTasks = new ObservableCollection<DebloatTask>(_allTasks);
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterTasks();
    }

    private void FilterTasks()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredTasks = new ObservableCollection<DebloatTask>(_allTasks);
        }
        else
        {
            var filtered = _allTasks.Where(t => 
                t.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || 
                t.Category.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            
            FilteredTasks = new ObservableCollection<DebloatTask>(filtered);
        }
    }

    [RelayCommand]
    private async Task RunSelectedTasksAsync()
    {
        var selected = _allTasks.Where(t => t.IsSelected).ToList();
        if (!selected.Any()) return;

        IsBusy = true;
        
        foreach (var task in selected)
        {
            StatusText = string.Format(Localizer["Applying"], task.Name);
            await _debloatService.RunTaskAsync(task);
        }

        StatusText = Localizer["OptimizationComplete"];
        IsBusy = false;
    }
}
