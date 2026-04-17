using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace AndroidPadSimulator.ViewModels;

public partial class AboutDeviceViewModel : ViewModelBase
{
    public MainWindowViewModel? MainViewModel { get; set; }
    [ObservableProperty]
    private string _deviceName = "Pixel Tablet";

    [ObservableProperty]
    private string _androidVersion = "Android 16";

    [ObservableProperty]
    private string _androidVersionCode = "Baklava";

    [ObservableProperty]
    private string _buildNumber = "BP22.250103.008";

    [ObservableProperty]
    private string _securityPatch = "2025年2月5日";

    [ObservableProperty]
    private string _kernelVersion = "5.15.148-android14-11-g5c8cbd5aab3d";

    [ObservableProperty]
    private string _basebandVersion = "g5300q-230612-240612-B-1234567";

    [ObservableProperty]
    private string _model = "Pixel Tablet";

    [ObservableProperty]
    private string _legalInfo = "法律信息";

    [ObservableProperty]
    private string _regulatoryLabels = "监管标签";

    [ObservableProperty]
    private string _safetyAndRegulatory = "安全与监管手册";

    [ObservableProperty]
    private string _status = "状态";

    [ObservableProperty]
    private string _simStatus = "SIM卡状态";

    [ObservableProperty]
    private string _imei = "358901050123456";

    [ObservableProperty]
    private string _ipAddress = "192.168.1.100";

    [ObservableProperty]
    private string _wifiMacAddress = "02:00:00:00:00:00";

    [ObservableProperty]
    private string _bluetoothAddress = "AA:BB:CC:DD:EE:FF";

    [ObservableProperty]
    private string _uptime = "3天 8小时 45分钟";

    [ObservableProperty]
    private bool _isDeveloperOptionsVisible = false;

    [ObservableProperty]
    private int _buildNumberClickCount = 0;

    [ObservableProperty]
    private string _developerOptionsStatus = "您已处于开发者模式";

    [RelayCommand]
    private void OnBuildNumberClick()
    {
        BuildNumberClickCount++;
        if (BuildNumberClickCount >= 7 && !IsDeveloperOptionsVisible)
        {
            IsDeveloperOptionsVisible = true;
        }
    }

    [RelayCommand]
    private void OnAndroidVersionClick()
        {
        // 显示Android版本彩蛋
    }

    [RelayCommand]
    private void OnCheckUpdate()
    {
        // 导航到系统更新页面
    }
}
