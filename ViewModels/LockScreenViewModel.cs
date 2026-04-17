using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.IO;
using System.Timers;

namespace AndroidPadSimulator.ViewModels;

public partial class LockScreenViewModel : ObservableObject
{
    private readonly Timer _timer;

    [ObservableProperty]
    private string _currentTime = string.Empty;

    [ObservableProperty]
    private string _currentTimeLarge = string.Empty;

    [ObservableProperty]
    private string _currentDate = string.Empty;

    [ObservableProperty]
    private bool _isLockScreenVisible = true;

    [ObservableProperty]
    private bool _isCameraOpen = false;

    [ObservableProperty]
    private Bitmap? _wallpaperSource;

    public LockScreenViewModel()
    {
        _timer = new Timer(1000);
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true;
        _timer.Start();

        LoadWallpaper();
        UpdateTime();
    }

    private void LoadWallpaper()
    {
        try
        {
            // 尝试从Assets加载壁纸
            var assetLoader = AssetLoader.Open(new Uri("avares://AndroidPadSimulator/Assets/wallpaper.jpg"));
            WallpaperSource = new Bitmap(assetLoader);
        }
        catch
        {
            // 如果加载失败，使用纯色背景
            WallpaperSource = null;
        }
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        var now = DateTime.Now;
        CurrentTime = now.ToString("HH:mm");
        CurrentTimeLarge = now.ToString("HH:mm");
        CurrentDate = $"{now:MM月dd日} {GetWeekDay(now.DayOfWeek)}";
    }

    private static string GetWeekDay(DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Monday => "星期一",
            DayOfWeek.Tuesday => "星期二",
            DayOfWeek.Wednesday => "星期三",
            DayOfWeek.Thursday => "星期四",
            DayOfWeek.Friday => "星期五",
            DayOfWeek.Saturday => "星期六",
            DayOfWeek.Sunday => "星期日",
            _ => string.Empty
        };
    }

    [RelayCommand]
    private void OpenCamera()
    {
        IsCameraOpen = true;
    }

    [RelayCommand]
    private void CloseCamera()
    {
        IsCameraOpen = false;
    }

    [RelayCommand]
    private void Unlock()
    {
        IsLockScreenVisible = false;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
