using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;

namespace AndroidPadSimulator.ViewModels;

public partial class AppInfo : ObservableObject
{
    [ObservableProperty]
    private string _appName;

    [ObservableProperty]
    private string _packageName;

    [ObservableProperty]
    private string _version;

    [ObservableProperty]
    private double _size;

    [ObservableProperty]
    private string _lastUpdated;

    [ObservableProperty]
    private string _icon;

    [ObservableProperty]
    private bool _isSystemApp;

    [ObservableProperty]
    private bool _isEnabled;

    public AppInfo(string appName, string packageName, string version, double size, string lastUpdated, string icon, bool isSystemApp = false, bool isEnabled = true)
    {
        AppName = appName;
        PackageName = packageName;
        Version = version;
        Size = size;
        LastUpdated = lastUpdated;
        Icon = icon;
        IsSystemApp = isSystemApp;
        IsEnabled = isEnabled;
    }
}

public partial class InstalledAppsViewModel : ObservableObject
{
    public MainWindowViewModel? MainViewModel { get; set; }

    [ObservableProperty]
    private int _totalApps = 42;

    [ObservableProperty]
    private int _userApps = 35;

    [ObservableProperty]
    private int _systemApps = 7;

    [ObservableProperty]
    private string _sortBy = "名称";

    [ObservableProperty]
    private bool _showSystemApps = false;

    [ObservableProperty]
    private bool _showDisabledApps = false;

    [ObservableProperty]
    private List<AppInfo> _appsList;

    public InstalledAppsViewModel()
    {
        // 模拟应用列表数据
        AppsList = new List<AppInfo>
        {
            new AppInfo("Google Chrome", "com.android.chrome", "122.0.6261.95", 234.5, "2025-02-15", "C"),
            new AppInfo("Gmail", "com.google.android.gm", "2025.02.05.506578972", 128.3, "2025-02-10", "G"),
            new AppInfo("Google Play 商店", "com.android.vending", "42.8.18-21 [0] [PR] 572573406", 56.7, "2025-02-08", "P", true),
            new AppInfo("YouTube", "com.google.android.youtube", "19.02.35", 189.2, "2025-02-12", "Y"),
            new AppInfo("Google 地图", "com.google.android.apps.maps", "112.0.0000", 156.8, "2025-02-14", "M"),
            new AppInfo("Google 相册", "com.google.android.apps.photos", "6.85.0.650866467", 102.4, "2025-02-09", "Ph"),
            new AppInfo("Google 助手", "com.google.android.apps.googleassistant", "14.44.16.29.arm64", 89.6, "2025-02-11", "A", true),
            new AppInfo("设置", "com.android.settings", "16", 78.9, "2025-02-01", "S", true),
            new AppInfo("相机", "com.android.camera2", "16", 45.2, "2025-02-01", "Ca", true),
            new AppInfo("图库", "com.android.gallery3d", "16", 32.1, "2025-02-01", "Ga", true),
            new AppInfo("音乐", "com.google.android.music", "9.108.9112-290911200", 67.8, "2025-02-07", "Mu"),
            new AppInfo("日历", "com.google.android.calendar", "2025.01.05.506578972", 28.5, "2025-02-05", "Ca")
        };
    }

    [RelayCommand]
    private void GoBack()
    {
        MainViewModel?.CloseInstalledAppsCommand.Execute(null);
    }

    [RelayCommand]
    private void OpenAppDetails(AppInfo app)
    {
        // 打开应用详情逻辑
    }

    [RelayCommand]
    private void UninstallApp(AppInfo app)
    {
        // 卸载应用逻辑
    }

    [RelayCommand]
    private void DisableApp(AppInfo app)
    {
        app.IsEnabled = !app.IsEnabled;
    }

    [RelayCommand]
    private void ClearAppData(AppInfo app)
    {
        // 清除应用数据逻辑
    }

    [RelayCommand]
    private void ClearAppCache(AppInfo app)
    {
        // 清除应用缓存逻辑
    }

    [RelayCommand]
    private void ChangeSortOrder()
    {
        // 更改排序方式逻辑
    }

    [RelayCommand]
    private void ToggleShowSystemApps()
    {
        ShowSystemApps = !ShowSystemApps;
        // 刷新应用列表
        RefreshAppList();
    }

    [RelayCommand]
    private void ToggleShowDisabledApps()
    {
        ShowDisabledApps = !ShowDisabledApps;
        // 刷新应用列表
        RefreshAppList();
    }

    private void RefreshAppList()
    {
        // 根据筛选条件刷新应用列表
    }
}
