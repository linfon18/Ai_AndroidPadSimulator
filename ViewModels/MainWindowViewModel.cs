using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace AndroidPadSimulator.ViewModels;

public enum ScreenState
{
    LockScreen,
    HomeScreen,
    AppDrawer
}

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private LockScreenViewModel _lockScreenViewModel;

    [ObservableProperty]
    private HomeScreenViewModel _homeScreenViewModel;

    [ObservableProperty]
    private ControlCenterViewModel _controlCenterViewModel;

    [ObservableProperty]
    private SettingsViewModel _settingsViewModel;

    [ObservableProperty]
    private ReconnectingViewModel _reconnectingViewModel;

    [ObservableProperty]
    private SoftwareUpdateViewModel _softwareUpdateViewModel;

    [ObservableProperty]
    private AboutDeviceViewModel _aboutDeviceViewModel;

    [ObservableProperty]
    private UserProfileViewModel _userProfileViewModel;

    [ObservableProperty]
    private StorageSettingsViewModel _storageSettingsViewModel;

    [ObservableProperty]
    private InstalledAppsViewModel _installedAppsViewModel;

    [ObservableProperty]
    private RunningAppsViewModel _runningAppsViewModel;

    [ObservableProperty]
    private PhoneViewModel _phoneViewModel;

    [ObservableProperty]
    private MessagesViewModel _messagesViewModel;

    [ObservableProperty]
    private ChromeViewModel _chromeViewModel;

    [ObservableProperty]
    private ScreenState _currentScreen = ScreenState.LockScreen;

    [ObservableProperty]
    private bool _isControlCenterOpen = false;

    [ObservableProperty]
    private bool _isSettingsOpen = false;

    [ObservableProperty]
    private bool _isReconnectingOpen = false;

    [ObservableProperty]
    private bool _isSoftwareUpdateOpen = false;

    [ObservableProperty]
    private bool _isAboutDeviceOpen = false;

    [ObservableProperty]
    private bool _isUserProfileOpen = false;

    [ObservableProperty]
    private bool _isStorageSettingsOpen = false;

    [ObservableProperty]
    private bool _isInstalledAppsOpen = false;

    [ObservableProperty]
    private bool _isRunningAppsOpen = false;

    [ObservableProperty]
    private bool _isPhoneOpen = false;

    [ObservableProperty]
    private bool _isMessagesOpen = false;

    [ObservableProperty]
    private bool _isChromeOpen = false;

    [ObservableProperty]
    private double _lockScreenOpacity = 1;

    [ObservableProperty]
    private double _homeScreenOpacity = 0;

    [ObservableProperty]
    private double _lockScreenTranslateY = 0;

    [ObservableProperty]
    private double _homeScreenTranslateY = 50;

    public bool IsLockScreenVisible => LockScreenOpacity > 0;
    public bool IsHomeScreenVisible => HomeScreenOpacity > 0;

    public MainWindowViewModel()
    {
        _lockScreenViewModel = new LockScreenViewModel();
        _homeScreenViewModel = new HomeScreenViewModel();
        _controlCenterViewModel = new ControlCenterViewModel();
        _settingsViewModel = new SettingsViewModel();
        _reconnectingViewModel = new ReconnectingViewModel();
        _softwareUpdateViewModel = new SoftwareUpdateViewModel();
        _aboutDeviceViewModel = new AboutDeviceViewModel();
        _userProfileViewModel = new UserProfileViewModel();
        _storageSettingsViewModel = new StorageSettingsViewModel();
        _installedAppsViewModel = new InstalledAppsViewModel();
        _runningAppsViewModel = new RunningAppsViewModel();
        _phoneViewModel = new PhoneViewModel();
        _messagesViewModel = new MessagesViewModel();
        _chromeViewModel = new ChromeViewModel();

        // 设置主ViewModel引用
        _homeScreenViewModel.MainViewModel = this;
        _settingsViewModel.MainViewModel = this;
        _reconnectingViewModel.MainViewModel = this;
        _softwareUpdateViewModel.MainViewModel = this;
        _aboutDeviceViewModel.MainViewModel = this;
        _userProfileViewModel.MainViewModel = this;
        _storageSettingsViewModel.MainViewModel = this;
        _installedAppsViewModel.MainViewModel = this;
        _runningAppsViewModel.MainViewModel = this;
        _phoneViewModel.MainViewModel = this;
        _messagesViewModel.MainViewModel = this;
        _chromeViewModel.MainViewModel = this;

        // 监听锁屏解锁事件
        _lockScreenViewModel.PropertyChanged += async (s, e) =>
        {
            if (e.PropertyName == nameof(LockScreenViewModel.IsLockScreenVisible))
            {
                if (!_lockScreenViewModel.IsLockScreenVisible)
                {
                    await TransitionToHomeScreen();
                }
            }
        };
    }

    private async Task TransitionToHomeScreen()
    {
        // 动画持续时间
        const int duration = 300;
        const int steps = 30;
        const double stepDuration = duration / (double)steps;

        // 执行动画
        for (int i = 0; i <= steps; i++)
        {
            double progress = i / (double)steps;
            LockScreenOpacity = 1 - progress;
            HomeScreenOpacity = progress;
            LockScreenTranslateY = progress * 50;
            HomeScreenTranslateY = 50 - (progress * 50);

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        // 完成过渡
        CurrentScreen = ScreenState.HomeScreen;
        LockScreenOpacity = 0;
        HomeScreenOpacity = 1;
        LockScreenTranslateY = 50;
        HomeScreenTranslateY = 0;
        
        // 通知属性变化
        OnPropertyChanged(nameof(IsLockScreenVisible));
        OnPropertyChanged(nameof(IsHomeScreenVisible));
    }

    [RelayCommand]
    private async Task GoToLockScreen()
    {
        // 动画持续时间
        const int duration = 300;
        const int steps = 30;
        const double stepDuration = duration / (double)steps;

        // 执行动画
        for (int i = 0; i <= steps; i++)
        {
            double progress = i / (double)steps;
            LockScreenOpacity = progress;
            HomeScreenOpacity = 1 - progress;
            LockScreenTranslateY = 50 - (progress * 50);
            HomeScreenTranslateY = progress * 50;

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        // 完成过渡
        CurrentScreen = ScreenState.LockScreen;
        LockScreenViewModel.IsLockScreenVisible = true;
        LockScreenOpacity = 1;
        HomeScreenOpacity = 0;
        LockScreenTranslateY = 0;
        HomeScreenTranslateY = 50;
        
        // 通知属性变化
        OnPropertyChanged(nameof(IsLockScreenVisible));
        OnPropertyChanged(nameof(IsHomeScreenVisible));
    }

    [RelayCommand]
    private void OpenControlCenter()
    {
        IsControlCenterOpen = true;
    }

    [RelayCommand]
    private void CloseControlCenter()
    {
        IsControlCenterOpen = false;
    }

    [RelayCommand]
    private void OpenSettings()
    {
        IsSettingsOpen = true;
    }

    [RelayCommand]
    private void CloseSettings()
    {
        IsSettingsOpen = false;
    }

    [RelayCommand]
    private void TriggerReconnecting()
    {
        // 关闭设置界面
        IsSettingsOpen = false;
        // 显示重新连接界面
        IsReconnectingOpen = true;
    }

    [RelayCommand]
    private async Task RebootSystem()
    {
        // 关闭重新连接界面
        IsReconnectingOpen = false;
        
        // 模拟重启过程
        // 1. 黑屏
        await Task.Delay(1000);
        
        // 2. 回到锁屏
        CurrentScreen = ScreenState.LockScreen;
        LockScreenOpacity = 1;
        LockScreenTranslateY = 0;
        HomeScreenOpacity = 0;
        HomeScreenTranslateY = 0;
        
        // 3. 重置设置状态
        SettingsViewModel.IsWifiEnabled = true;
        SettingsViewModel.IsMobileDataEnabled = true;
        SettingsViewModel.IsBluetoothEnabled = true;
    }

    [RelayCommand]
    private void OpenSoftwareUpdate()
    {
        SoftwareUpdateViewModel.Reset();
        IsSoftwareUpdateOpen = true;
    }

    [RelayCommand]
    private void CloseSoftwareUpdate()
    {
        IsSoftwareUpdateOpen = false;
    }

    [RelayCommand]
    private void OpenAboutDevice()
    {
        IsAboutDeviceOpen = true;
    }

    [RelayCommand]
    private void CloseAboutDevice()
    {
        IsAboutDeviceOpen = false;
    }

    [RelayCommand]
    private void OpenUserProfile()
    {
        IsUserProfileOpen = true;
    }

    [RelayCommand]
    private void CloseUserProfile()
    {
        IsUserProfileOpen = false;
    }

    [RelayCommand]
    private void OpenStorageSettings()
    {
        IsStorageSettingsOpen = true;
    }

    [RelayCommand]
    private void CloseStorageSettings()
    {
        IsStorageSettingsOpen = false;
    }

    [RelayCommand]
    private void OpenInstalledApps()
    {
        IsInstalledAppsOpen = true;
    }

    [RelayCommand]
    private void CloseInstalledApps()
    {
        IsInstalledAppsOpen = false;
    }

    [RelayCommand]
    private void OpenRunningApps()
    {
        IsRunningAppsOpen = true;
    }

    [RelayCommand]
    private void CloseRunningApps()
    {
        IsRunningAppsOpen = false;
    }

    [RelayCommand]
    private void OpenPhone()
    {
        IsPhoneOpen = true;
    }

    [RelayCommand]
    private void ClosePhone()
    {
        IsPhoneOpen = false;
    }

    [RelayCommand]
    private void OpenMessages()
    {
        IsMessagesOpen = true;
    }

    [RelayCommand]
    private void CloseMessages()
    {
        IsMessagesOpen = false;
    }

    [RelayCommand]
    private void OpenChrome()
    {
        IsChromeOpen = true;
    }

    [RelayCommand]
    private void CloseChrome()
    {
        IsChromeOpen = false;
    }
}
