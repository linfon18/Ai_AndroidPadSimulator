using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace AndroidPadSimulator.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    public MainWindowViewModel? MainViewModel { get; set; }

    [ObservableProperty]
    private string _currentTime = string.Empty;

    [ObservableProperty]
    private bool _isWifiEnabled = true;

    [ObservableProperty]
    private bool _isBluetoothEnabled = true;

    [ObservableProperty]
    private bool _isMobileDataEnabled = true;

    [ObservableProperty]
    private bool _isAirplaneModeEnabled = false;

    [ObservableProperty]
    private bool _isLocationEnabled = true;

    [ObservableProperty]
    private bool _isNfcEnabled = false;

    [ObservableProperty]
    private int _brightness = 80;

    [ObservableProperty]
    private int _volume = 60;

    [ObservableProperty]
    private string _deviceName = "Pixel Tablet";

    [ObservableProperty]
    private string _androidVersion = "Android 16";

    [ObservableProperty]
    private string _securityPatch = "2025年2月1日";

    // 显示设置
    [ObservableProperty]
    private bool _isDarkModeEnabled = true;

    [ObservableProperty]
    private bool _isAutoRotateEnabled = true;

    [ObservableProperty]
    private bool _isEyeComfortEnabled = false;

    [ObservableProperty]
    private int _fontSize = 14;

    [ObservableProperty]
    private int _screenTimeout = 5;

    // 声音设置
    [ObservableProperty]
    private bool _isSilentModeEnabled = false;

    [ObservableProperty]
    private bool _isVibrationEnabled = true;

    [ObservableProperty]
    private int _ringtoneVolume = 70;

    [ObservableProperty]
    private int _mediaVolume = 60;

    [ObservableProperty]
    private int _alarmVolume = 80;

    // 存储设置
    [ObservableProperty]
    private string _totalStorage = "128 GB";

    [ObservableProperty]
    private string _usedStorage = "45.2 GB";

    [ObservableProperty]
    private string _availableStorage = "82.8 GB";

    [ObservableProperty]
    private double _storageUsagePercent = 35;

    // 电池设置
    [ObservableProperty]
    private int _batteryLevel = 78;

    [ObservableProperty]
    private bool _isPowerSavingEnabled = false;

    [ObservableProperty]
    private bool _isBatteryOptimizationEnabled = true;

    // 应用设置
    [ObservableProperty]
    private int _installedAppsCount = 42;

    [ObservableProperty]
    private int _runningAppsCount = 8;

    // 安全设置
    [ObservableProperty]
    private bool _isFingerprintEnabled = true;

    [ObservableProperty]
    private bool _isFaceUnlockEnabled = false;

    [ObservableProperty]
    private bool _isScreenLockEnabled = true;

    // 隐私设置
    [ObservableProperty]
    private bool _isAppPermissionsEnabled = true;

    [ObservableProperty]
    private bool _isUsageAccessEnabled = false;

    // 辅助功能
    [ObservableProperty]
    private bool _isTalkBackEnabled = false;

    [ObservableProperty]
    private bool _isMagnificationEnabled = false;

    [ObservableProperty]
    private bool _isColorCorrectionEnabled = false;

    // 语言和输入法
    [ObservableProperty]
    private string _currentLanguage = "中文 (简体)";

    [ObservableProperty]
    private string _currentKeyboard = "Gboard";

    // 日期和时间
    [ObservableProperty]
    private bool _isAutoTimeEnabled = true;

    [ObservableProperty]
    private bool _isAutoTimeZoneEnabled = true;

    [ObservableProperty]
    private string _currentTimeZone = "GMT+08:00 中国标准时间";

    public SettingsViewModel()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        CurrentTime = DateTime.Now.ToString("HH:mm");
    }

    [RelayCommand]
    private void GoBack()
    {
        MainViewModel?.CloseSettingsCommand.Execute(null);
    }

    [RelayCommand]
    private void OpenAboutDevice()
    {
        MainViewModel?.OpenAboutDeviceCommand.Execute(null);
    }

    [RelayCommand]
    private void ToggleWifi()
    {
        IsWifiEnabled = !IsWifiEnabled;
        CheckNetworkStatus();
    }

    [RelayCommand]
    private void ToggleBluetooth()
    {
        IsBluetoothEnabled = !IsBluetoothEnabled;
    }

    [RelayCommand]
    private void ToggleMobileData()
    {
        IsMobileDataEnabled = !IsMobileDataEnabled;
        CheckNetworkStatus();
    }

    private void CheckNetworkStatus()
    {
        // 彩蛋：如果WLAN和移动网络都关闭了，触发重新连接
        if (!IsWifiEnabled && !IsMobileDataEnabled)
        {
            // 延迟一下，让用户看到开关状态变化
            Task.Run(async () =>
            {
                await Task.Delay(500);
                MainViewModel?.TriggerReconnectingCommand.Execute(null);
            });
        }
    }

    [RelayCommand]
    private void ToggleAirplaneMode()
    {
        IsAirplaneModeEnabled = !IsAirplaneModeEnabled;
    }

    [RelayCommand]
    private void ToggleLocation()
    {
        IsLocationEnabled = !IsLocationEnabled;
    }

    [RelayCommand]
    private void ToggleNfc()
    {
        IsNfcEnabled = !IsNfcEnabled;
    }

    [RelayCommand]
    private void ToggleDarkMode()
    {
        IsDarkModeEnabled = !IsDarkModeEnabled;
    }

    [RelayCommand]
    private void ToggleAutoRotate()
    {
        IsAutoRotateEnabled = !IsAutoRotateEnabled;
    }

    [RelayCommand]
    private void ToggleEyeComfort()
    {
        IsEyeComfortEnabled = !IsEyeComfortEnabled;
    }

    [RelayCommand]
    private void ToggleSilentMode()
    {
        IsSilentModeEnabled = !IsSilentModeEnabled;
    }

    [RelayCommand]
    private void ToggleVibration()
    {
        IsVibrationEnabled = !IsVibrationEnabled;
    }

    [RelayCommand]
    private void TogglePowerSaving()
    {
        IsPowerSavingEnabled = !IsPowerSavingEnabled;
    }

    [RelayCommand]
    private void ToggleBatteryOptimization()
    {
        IsBatteryOptimizationEnabled = !IsBatteryOptimizationEnabled;
    }

    [RelayCommand]
    private void ToggleFingerprint()
    {
        IsFingerprintEnabled = !IsFingerprintEnabled;
    }

    [RelayCommand]
    private void ToggleFaceUnlock()
    {
        IsFaceUnlockEnabled = !IsFaceUnlockEnabled;
    }

    [RelayCommand]
    private void ToggleScreenLock()
    {
        IsScreenLockEnabled = !IsScreenLockEnabled;
    }

    [RelayCommand]
    private void ToggleAppPermissions()
    {
        IsAppPermissionsEnabled = !IsAppPermissionsEnabled;
    }

    [RelayCommand]
    private void ToggleUsageAccess()
    {
        IsUsageAccessEnabled = !IsUsageAccessEnabled;
    }

    [RelayCommand]
    private void ToggleTalkBack()
    {
        IsTalkBackEnabled = !IsTalkBackEnabled;
    }

    [RelayCommand]
    private void ToggleMagnification()
    {
        IsMagnificationEnabled = !IsMagnificationEnabled;
    }

    [RelayCommand]
    private void ToggleColorCorrection()
    {
        IsColorCorrectionEnabled = !IsColorCorrectionEnabled;
    }

    [RelayCommand]
    private void ToggleAutoTime()
    {
        IsAutoTimeEnabled = !IsAutoTimeEnabled;
    }

    [RelayCommand]
    private void ToggleAutoTimeZone()
    {
        IsAutoTimeZoneEnabled = !IsAutoTimeZoneEnabled;
    }

    // 用户相关命令
    [RelayCommand]
    private void OpenUserProfile()
    {
        // 关闭设置页面并打开用户个人资料页面
        MainViewModel?.CloseSettingsCommand.Execute(null);
        MainViewModel?.OpenUserProfileCommand.Execute(null);
    }

    // 存储相关命令
    [RelayCommand]
    private void OpenStorageSettings()
    {
        // 关闭设置页面并打开存储设置页面
        MainViewModel?.CloseSettingsCommand.Execute(null);
        MainViewModel?.OpenStorageSettingsCommand.Execute(null);
    }

    // 应用相关命令
    [RelayCommand]
    private void OpenInstalledApps()
    {
        // 关闭设置页面并打开已安装应用页面
        MainViewModel?.CloseSettingsCommand.Execute(null);
        MainViewModel?.OpenInstalledAppsCommand.Execute(null);
    }

    [RelayCommand]
    private void OpenRunningApps()
    {
        // 关闭设置页面并打开正在运行应用页面
        MainViewModel?.CloseSettingsCommand.Execute(null);
        MainViewModel?.OpenRunningAppsCommand.Execute(null);
    }
}
