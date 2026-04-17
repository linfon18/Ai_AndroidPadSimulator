using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace AndroidPadSimulator.ViewModels;

public partial class SoftwareUpdateViewModel : ObservableObject
{
    public MainWindowViewModel? MainViewModel { get; set; }

    [ObservableProperty]
    private string _updateStatus = "已是最新版本";

    [ObservableProperty]
    private string _updateDetail = "上次检查：今天 10:30";

    [ObservableProperty]
    private double _downloadProgress = 0;

    [ObservableProperty]
    private bool _isChecking = false;

    [ObservableProperty]
    private bool _isDownloading = false;

    [ObservableProperty]
    private bool _isUpToDate = true;

    [ObservableProperty]
    private bool _canCheckUpdate = true;

    [ObservableProperty]
    private bool _canDownload = false;

    [ObservableProperty]
    private bool _canInstall = false;

    private System.Timers.Timer? _progressTimer;

    public SoftwareUpdateViewModel()
    {
    }

    [RelayCommand]
    private void GoBack()
    {
        MainViewModel?.CloseSoftwareUpdateCommand.Execute(null);
    }

    [RelayCommand]
    private async Task CheckUpdate()
    {
        IsChecking = true;
        CanCheckUpdate = false;
        IsUpToDate = false;
        UpdateStatus = "正在检查更新...";
        UpdateDetail = "连接到 Google 服务器";

        // 模拟检查过程
        await Task.Delay(2000);
        UpdateDetail = "正在验证设备信息...";
        await Task.Delay(1500);
        UpdateDetail = "正在获取更新信息...";
        await Task.Delay(1500);

        IsChecking = false;
        
        // 模拟发现更新
        UpdateStatus = "Android 16.1 可用";
        UpdateDetail = "版本号：BP22.250205.001\n大小：1.2 GB";
        CanDownload = true;
    }

    [RelayCommand]
    private void DownloadUpdate()
    {
        CanDownload = false;
        IsDownloading = true;
        UpdateStatus = "正在下载...";
        UpdateDetail = "预计剩余时间：3 分钟";
        DownloadProgress = 0;

        // 模拟下载进度
        _progressTimer = new System.Timers.Timer(100);
        _progressTimer.Elapsed += (s, e) =>
        {
            DownloadProgress += 0.5;
            if (DownloadProgress >= 100)
            {
                _progressTimer?.Stop();
                _progressTimer?.Dispose();
                
                Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    IsDownloading = false;
                    UpdateStatus = "下载完成";
                    UpdateDetail = "Android 16.1 已准备好安装";
                    CanInstall = true;
                });
            }
        };
        _progressTimer.AutoReset = true;
        _progressTimer.Start();
    }

    [RelayCommand]
    private async Task InstallUpdate()
    {
        CanInstall = false;
        UpdateStatus = "正在安装...";
        UpdateDetail = "设备将在安装完成后重启";

        // 模拟安装过程
        await Task.Delay(3000);
        
        // 关闭更新界面并重启
        MainViewModel?.CloseSoftwareUpdateCommand.Execute(null);
        await Task.Delay(500);
        MainViewModel?.RebootSystemCommand.Execute(null);
    }

    public void Reset()
    {
        _progressTimer?.Stop();
        _progressTimer?.Dispose();
        
        UpdateStatus = "已是最新版本";
        UpdateDetail = "上次检查：今天 10:30";
        DownloadProgress = 0;
        IsChecking = false;
        IsDownloading = false;
        IsUpToDate = true;
        CanCheckUpdate = true;
        CanDownload = false;
        CanInstall = false;
    }
}
