using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Timers;

namespace AndroidPadSimulator.ViewModels;

public partial class HomeScreenViewModel : ObservableObject
{
    private readonly Timer _timer;

    [ObservableProperty]
    private string _currentTime = string.Empty;

    [ObservableProperty]
    private Bitmap? _wallpaperSource;

    public MainWindowViewModel? MainViewModel { get; set; }

    public HomeScreenViewModel()
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
            var assetLoader = AssetLoader.Open(new Uri("avares://AndroidPadSimulator/Assets/wallpaper.jpg"));
            WallpaperSource = new Bitmap(assetLoader);
        }
        catch
        {
            WallpaperSource = null;
        }
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        CurrentTime = DateTime.Now.ToString("HH:mm");
    }

    [RelayCommand]
    private void OpenControlCenter()
    {
        MainViewModel?.OpenControlCenterCommand.Execute(null);
    }

    [RelayCommand]
    private void OpenSettings()
    {
        MainViewModel?.OpenSettingsCommand.Execute(null);
    }

    [RelayCommand]
    private void OpenPhone()
    {
        MainViewModel?.OpenPhoneCommand.Execute(null);
    }

    [RelayCommand]
    private void OpenMessages()
    {
        MainViewModel?.OpenMessagesCommand.Execute(null);
    }

    [RelayCommand]
    private void OpenChrome()
    {
        MainViewModel?.OpenChromeCommand.Execute(null);
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
