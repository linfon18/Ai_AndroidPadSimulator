using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace AndroidPadSimulator.ViewModels;

public partial class UserProfileViewModel : ObservableObject
{
    public MainWindowViewModel? MainViewModel { get; set; }

    [ObservableProperty]
    private string _userName = "用户";

    [ObservableProperty]
    private string _email = "user@example.com";

    [ObservableProperty]
    private string _profileImage = "U";

    [ObservableProperty]
    private string _accountType = "Google 账号";

    [ObservableProperty]
    private bool _isSyncEnabled = true;

    [ObservableProperty]
    private bool _isBackupEnabled = true;

    [ObservableProperty]
    private bool _isLocationHistoryEnabled = false;

    [ObservableProperty]
    private string _lastSync = "最近同步: 今天 10:30";

    [RelayCommand]
    private void GoBack()
    {
        MainViewModel?.CloseUserProfileCommand.Execute(null);
    }

    [RelayCommand]
    private void EditProfile()
    {
        // 编辑个人资料逻辑
    }

    [RelayCommand]
    private void ManageAccount()
    {
        // 管理Google账号逻辑
    }

    [RelayCommand]
    private void ToggleSync()
    {
        IsSyncEnabled = !IsSyncEnabled;
    }

    [RelayCommand]
    private void ToggleBackup()
    {
        IsBackupEnabled = !IsBackupEnabled;
    }

    [RelayCommand]
    private void ToggleLocationHistory()
    {
        IsLocationHistoryEnabled = !IsLocationHistoryEnabled;
    }
}
