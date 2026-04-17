using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace AndroidPadSimulator.ViewModels;

public partial class PhoneViewModel : ObservableObject
{
    public MainWindowViewModel? MainViewModel { get; set; }

    [ObservableProperty]
    private string _currentTime = string.Empty;

    [ObservableProperty]
    private string _phoneNumber = string.Empty;

    [ObservableProperty]
    private ObservableCollection<CallLogItem> _callLogs = new();

    [ObservableProperty]
    private ObservableCollection<ContactItem> _contacts = new();

    [ObservableProperty]
    private int _selectedTabIndex = 0;

    public PhoneViewModel()
    {
        UpdateTime();
        LoadCallLogs();
        LoadContacts();
    }

    private void UpdateTime()
    {
        CurrentTime = DateTime.Now.ToString("HH:mm");
    }

    private void LoadCallLogs()
    {
        CallLogs = new ObservableCollection<CallLogItem>
        {
            new() { Name = "张三", Number = "138****8888", Time = "今天 14:30", Type = CallType.Incoming, Duration = "5:23" },
            new() { Name = "李四", Number = "139****6666", Time = "今天 12:15", Type = CallType.Outgoing, Duration = "2:10" },
            new() { Name = "王五", Number = "137****9999", Time = "昨天 18:45", Type = CallType.Missed, Duration = "" },
            new() { Name = "赵六", Number = "136****5555", Time = "昨天 09:20", Type = CallType.Incoming, Duration = "8:45" },
            new() { Name = "10086", Number = "10086", Time = "2天前", Type = CallType.Outgoing, Duration = "1:30" },
        };
    }

    private void LoadContacts()
    {
        Contacts = new ObservableCollection<ContactItem>
        {
            new() { Name = "张三", Number = "138****8888", AvatarColor = "#4CAF50" },
            new() { Name = "李四", Number = "139****6666", AvatarColor = "#2196F3" },
            new() { Name = "王五", Number = "137****9999", AvatarColor = "#FF9800" },
            new() { Name = "赵六", Number = "136****5555", AvatarColor = "#9C27B0" },
            new() { Name = "钱七", Number = "135****7777", AvatarColor = "#F44336" },
            new() { Name = "孙八", Number = "134****3333", AvatarColor = "#00BCD4" },
            new() { Name = "周九", Number = "133****2222", AvatarColor = "#795548" },
            new() { Name = "吴十", Number = "132****1111", AvatarColor = "#607D8B" },
        };
    }

    [RelayCommand]
    private void GoBack()
    {
        MainViewModel?.ClosePhoneCommand.Execute(null);
    }

    [RelayCommand]
    private void AppendNumber(string number)
    {
        if (PhoneNumber.Length < 15)
        {
            PhoneNumber += number;
        }
    }

    [RelayCommand]
    private void DeleteNumber()
    {
        if (PhoneNumber.Length > 0)
        {
            PhoneNumber = PhoneNumber[..^1];
        }
    }

    [RelayCommand]
    private void ClearNumber()
    {
        PhoneNumber = string.Empty;
    }

    [RelayCommand]
    private void Call()
    {
        // 模拟拨打电话
        if (!string.IsNullOrEmpty(PhoneNumber))
        {
            // 添加到通话记录
            CallLogs.Insert(0, new CallLogItem
            {
                Name = PhoneNumber,
                Number = PhoneNumber,
                Time = "刚刚",
                Type = CallType.Outgoing,
                Duration = "0:00"
            });
        }
    }

    [RelayCommand]
    private void SelectContact(ContactItem contact)
    {
        PhoneNumber = contact.Number;
    }

    [RelayCommand]
    private void SetTabIndex(string index)
    {
        if (int.TryParse(index, out int tabIndex))
        {
            SelectedTabIndex = tabIndex;
        }
    }
}

public class CallLogItem
{
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public CallType Type { get; set; }
    public string Duration { get; set; } = string.Empty;
}

public enum CallType
{
    Incoming,
    Outgoing,
    Missed
}

public class ContactItem
{
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string AvatarColor { get; set; } = "#4CAF50";
}
