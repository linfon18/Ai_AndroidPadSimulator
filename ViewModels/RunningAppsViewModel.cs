using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;

namespace AndroidPadSimulator.ViewModels;

public partial class RunningAppInfo : ObservableObject
{
    [ObservableProperty]
    private string _appName;

    [ObservableProperty]
    private string _packageName;

    [ObservableProperty]
    private string _icon;

    [ObservableProperty]
    private double _memoryUsage;

    [ObservableProperty]
    private double _cpuUsage;

    [ObservableProperty]
    private bool _isSystemApp;

    [ObservableProperty]
    private bool _isForeground;

    [ObservableProperty]
    private string _runningTime;

    public RunningAppInfo(string appName, string packageName, string icon, double memoryUsage, double cpuUsage, bool isSystemApp = false, bool isForeground = false, string runningTime = "")
    {
        AppName = appName;
        PackageName = packageName;
        Icon = icon;
        MemoryUsage = memoryUsage;
        CpuUsage = cpuUsage;
        IsSystemApp = isSystemApp;
        IsForeground = isForeground;
        RunningTime = runningTime;
    }
}

public partial class RunningAppsViewModel : ObservableObject
{
    public MainWindowViewModel? MainViewModel { get; set; }

    [ObservableProperty]
    private int _totalRunningApps = 8;

    [ObservableProperty]
    private double _totalMemoryUsage = 3.2;

    [ObservableProperty]
    private double _totalCpuUsage = 15.7;

    [ObservableProperty]
    private string _sortBy = "内存使用";

    [ObservableProperty]
    private bool _showSystemApps = false;

    [ObservableProperty]
    private List<RunningAppInfo> _runningAppsList;

    public RunningAppsViewModel()
    {
        // 模拟正在运行的应用列表数据
        RunningAppsList = new List<RunningAppInfo>
        {
            new RunningAppInfo("Google Chrome", "com.android.chrome", "C", 456.8, 4.2, false, true, "2小时30分钟"),
            new RunningAppInfo("Gmail", "com.google.android.gm", "G", 123.4, 1.8, false, false, "1小时15分钟"),
            new RunningAppInfo("Google 地图", "com.google.android.apps.maps", "M", 234.5, 2.5, false, false, "45分钟"),
            new RunningAppInfo("系统界面", "com.android.systemui", "S", 189.2, 3.1, true, true, "一直运行"),
            new RunningAppInfo("Google Play 服务", "com.google.android.gms", "P", 345.6, 3.8, true, true, "一直运行"),
            new RunningAppInfo("相机", "com.android.camera2", "Ca", 98.7, 1.2, true, false, "30分钟"),
            new RunningAppInfo("YouTube", "com.google.android.youtube", "Y", 312.3, 2.8, false, false, "1小时45分钟"),
            new RunningAppInfo("Google 助手", "com.google.android.apps.googleassistant", "A", 156.8, 2.1, true, false, "50分钟")
        };
    }

    [RelayCommand]
    private void GoBack()
    {
        MainViewModel?.CloseRunningAppsCommand.Execute(null);
    }

    [RelayCommand]
    private void OpenAppDetails(RunningAppInfo app)
    {
        // 打开应用详情逻辑
    }

    [RelayCommand]
    private void ForceStopApp(RunningAppInfo app)
    {
        // 强制停止应用逻辑
    }

    [RelayCommand]
    private void ClearAppCache(RunningAppInfo app)
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
    private void StopAllApps()
    {
        // 停止所有非系统应用逻辑
    }

    [RelayCommand]
    private void RefreshAppList()
    {
        // 刷新正在运行的应用列表
    }
}
