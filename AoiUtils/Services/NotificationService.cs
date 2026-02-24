using System;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AoiUtils.Services;

public partial class NotificationService : ObservableObject
{
    [ObservableProperty]
    private string _message = "";

    [ObservableProperty]
    private bool _isVisible;

    private Timer? _timer;

    public void Show(string message, int durationMs = 5000)
    {
        Message = message;
        IsVisible = true;

        _timer?.Stop();
        _timer = new Timer(durationMs);
        _timer.Elapsed += (s, e) => 
        {
            IsVisible = false;
            _timer.Stop();
        };
        _timer.Start();
    }
}
