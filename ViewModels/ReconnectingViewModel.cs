using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace AndroidPadSimulator.ViewModels;

public partial class ReconnectingViewModel : ObservableObject
{
    public MainWindowViewModel? MainViewModel { get; set; }

    [ObservableProperty]
    private string _statusText = "正在重新连接...";

    [ObservableProperty]
    private string _detailText = "检查网络连接";

    [ObservableProperty]
    private double _progress = 0;

    private System.Timers.Timer? _timer;
    private int _currentStep = 0;

    public ReconnectingViewModel()
    {
    }

    public void StartReconnecting()
    {
        _currentStep = 0;
        Progress = 0;
        StatusText = "断线重连中...";
        DetailText = "检查网络连接";

        _timer = new System.Timers.Timer(100);
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true;
        _timer.Start();
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _currentStep++;
        Progress = Math.Min(_currentStep, 100);

        // 更新状态文本
        if (_currentStep < 20)
        {
            StatusText = "正在重新连接...";
            DetailText = "检查网络连接";
        }
        else if (_currentStep < 40)
        {
            StatusText = "网络连接失败";
            DetailText = "尝试恢复出厂设置...";
        }
        else if (_currentStep < 60)
        {
            StatusText = "正在恢复出厂设置...";
            DetailText = "清除用户数据";
        }
        else if (_currentStep < 80)
        {
            StatusText = "正在恢复出厂设置...";
            DetailText = "重新安装系统";
        }
        else if (_currentStep < 100)
        {
            StatusText = "即将重启...";
            DetailText = "系统准备中";
        }
        else
        {
            _timer?.Stop();
            _timer?.Dispose();
            
            // 触发重启
            Task.Run(async () =>
            {
                await Task.Delay(500);
                MainViewModel?.RebootSystemCommand.Execute(null);
            });
        }
    }

    public void Stop()
    {
        _timer?.Stop();
        _timer?.Dispose();
    }
}
