using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace AndroidPadSimulator.ViewModels;

public partial class MessagesViewModel : ObservableObject
{
    public MainWindowViewModel? MainViewModel { get; set; }

    [ObservableProperty]
    private string _currentTime = string.Empty;

    [ObservableProperty]
    private ObservableCollection<MessageThread> _messageThreads = new();

    [ObservableProperty]
    private ObservableCollection<MessageItem> _currentMessages = new();

    [ObservableProperty]
    private MessageThread? _selectedThread;

    [ObservableProperty]
    private string _newMessageText = string.Empty;

    [ObservableProperty]
    private bool _isInConversation = false;

    public MessagesViewModel()
    {
        UpdateTime();
        LoadMessageThreads();
    }

    private void UpdateTime()
    {
        CurrentTime = DateTime.Now.ToString("HH:mm");
    }

    private void LoadMessageThreads()
    {
        MessageThreads = new ObservableCollection<MessageThread>
        {
            new() { 
                ContactName = "张三", 
                LastMessage = "好的，明天见！", 
                Time = "14:30", 
                UnreadCount = 2,
                AvatarColor = "#4CAF50",
                Messages = new ObservableCollection<MessageItem>
                {
                    new() { Content = "你好，在吗？", IsSent = false, Time = "14:25" },
                    new() { Content = "在的，有什么事吗？", IsSent = true, Time = "14:26" },
                    new() { Content = "明天有空一起吃饭吗？", IsSent = false, Time = "14:28" },
                    new() { Content = "好的，明天见！", IsSent = true, Time = "14:30" },
                }
            },
            new() { 
                ContactName = "李四", 
                LastMessage = "文件已发送，请查收", 
                Time = "12:15", 
                UnreadCount = 0,
                AvatarColor = "#2196F3",
                Messages = new ObservableCollection<MessageItem>
                {
                    new() { Content = "能把那个文档发给我吗？", IsSent = false, Time = "12:10" },
                    new() { Content = "好的，稍等", IsSent = true, Time = "12:12" },
                    new() { Content = "文件已发送，请查收", IsSent = true, Time = "12:15" },
                }
            },
            new() { 
                ContactName = "王五", 
                LastMessage = "收到了，谢谢！", 
                Time = "昨天", 
                UnreadCount = 0,
                AvatarColor = "#FF9800",
                Messages = new ObservableCollection<MessageItem>
                {
                    new() { Content = "快递到了，记得去取", IsSent = true, Time = "昨天 18:30" },
                    new() { Content = "收到了，谢谢！", IsSent = false, Time = "昨天 18:45" },
                }
            },
            new() { 
                ContactName = "10086", 
                LastMessage = "您的余额不足，请及时充值", 
                Time = "昨天", 
                UnreadCount = 1,
                AvatarColor = "#9C27B0",
                Messages = new ObservableCollection<MessageItem>
                {
                    new() { Content = "您的余额不足，请及时充值", IsSent = false, Time = "昨天 09:00" },
                }
            },
            new() { 
                ContactName = "赵六", 
                LastMessage = "周末一起去打球吧", 
                Time = "2天前", 
                UnreadCount = 0,
                AvatarColor = "#F44336",
                Messages = new ObservableCollection<MessageItem>
                {
                    new() { Content = "周末有空吗？", IsSent = false, Time = "2天前" },
                    new() { Content = "有的，怎么了？", IsSent = true, Time = "2天前" },
                    new() { Content = "周末一起去打球吧", IsSent = false, Time = "2天前" },
                }
            },
        };
    }

    [RelayCommand]
    private void GoBack()
    {
        if (IsInConversation)
        {
            IsInConversation = false;
            SelectedThread = null;
        }
        else
        {
            MainViewModel?.CloseMessagesCommand.Execute(null);
        }
    }

    [RelayCommand]
    private void OpenConversation(MessageThread thread)
    {
        SelectedThread = thread;
        CurrentMessages = thread.Messages;
        IsInConversation = true;
        thread.UnreadCount = 0;
    }

    [RelayCommand]
    private void SendMessage()
    {
        if (!string.IsNullOrWhiteSpace(NewMessageText) && SelectedThread != null)
        {
            var message = new MessageItem
            {
                Content = NewMessageText,
                IsSent = true,
                Time = DateTime.Now.ToString("HH:mm")
            };
            
            CurrentMessages.Add(message);
            SelectedThread.LastMessage = NewMessageText;
            SelectedThread.Time = "刚刚";
            NewMessageText = string.Empty;
        }
    }
}

public class MessageThread
{
    public string ContactName { get; set; } = string.Empty;
    public string LastMessage { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public int UnreadCount { get; set; }
    public string AvatarColor { get; set; } = "#4CAF50";
    public ObservableCollection<MessageItem> Messages { get; set; } = new();
}

public class MessageItem
{
    public string Content { get; set; } = string.Empty;
    public bool IsSent { get; set; }
    public string Time { get; set; } = string.Empty;
}
