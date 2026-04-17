using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace AndroidPadSimulator.ViewModels;

public partial class StorageSettingsViewModel : ObservableObject
{
    public MainWindowViewModel? MainViewModel { get; set; }

    [ObservableProperty]
    private string _totalStorage = "128 GB";

    [ObservableProperty]
    private string _usedStorage = "45.2 GB";

    [ObservableProperty]
    private string _availableStorage = "82.8 GB";

    [ObservableProperty]
    private double _storageUsagePercent = 35;

    [ObservableProperty]
    private double _appsStorage = 15.3;

    [ObservableProperty]
    private double _photosStorage = 10.2;

    [ObservableProperty]
    private double _videosStorage = 8.7;

    [ObservableProperty]
    private double _documentsStorage = 4.5;

    [ObservableProperty]
    private double _systemStorage = 6.5;

    [ObservableProperty]
    private bool _isAutoCleanupEnabled = false;

    [ObservableProperty]
    private string _lastCleanup = "上次清理: 3天前";

    [ObservableProperty]
    private bool _isStorageSenseEnabled = true;

    [RelayCommand]
    private void GoBack()
    {
        MainViewModel?.CloseStorageSettingsCommand.Execute(null);
    }

    [RelayCommand]
    private void ManageAppsStorage()
    {
        // 管理应用存储逻辑
    }

    [RelayCommand]
    private void ManagePhotosStorage()
    {
        // 管理照片存储逻辑
    }

    [RelayCommand]
    private void ManageVideosStorage()
    {
        // 管理视频存储逻辑
    }

    [RelayCommand]
    private void ManageDocumentsStorage()
    {
        // 管理文档存储逻辑
    }

    [RelayCommand]
    private void CleanupStorage()
    {
        // 清理存储逻辑
    }

    [RelayCommand]
    private void ToggleAutoCleanup()
    {
        IsAutoCleanupEnabled = !IsAutoCleanupEnabled;
    }

    [RelayCommand]
    private void ToggleStorageSense()
    {
        IsStorageSenseEnabled = !IsStorageSenseEnabled;
    }

    [RelayCommand]
    private void FormatStorage()
    {
        // 格式化存储逻辑
    }
}
