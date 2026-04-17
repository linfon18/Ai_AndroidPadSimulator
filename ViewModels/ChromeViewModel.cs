using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace AndroidPadSimulator.ViewModels;

public partial class ChromeViewModel : ObservableObject
{
    public MainWindowViewModel? MainViewModel { get; set; }

    [ObservableProperty]
    private string _currentTime = string.Empty;

    [ObservableProperty]
    private string _url = "https://www.google.com";

    [ObservableProperty]
    private string _pageTitle = "Google";

    [ObservableProperty]
    private bool _isLoading = false;

    [ObservableProperty]
    private ObservableCollection<TabItem> _tabs = new();

    [ObservableProperty]
    private int _selectedTabIndex = 0;

    [ObservableProperty]
    private ObservableCollection<BookmarkItem> _bookmarks = new();

    [ObservableProperty]
    private ObservableCollection<HistoryItem> _history = new();

    [ObservableProperty]
    private bool _showBookmarks = false;

    public ChromeViewModel()
    {
        UpdateTime();
        LoadTabs();
        LoadBookmarks();
        LoadHistory();
    }

    private void UpdateTime()
    {
        CurrentTime = DateTime.Now.ToString("HH:mm");
    }

    private void LoadTabs()
    {
        Tabs = new ObservableCollection<TabItem>
        {
            new() { Title = "Google", Url = "https://www.google.com", IsActive = true },
            new() { Title = "GitHub", Url = "https://github.com", IsActive = false },
            new() { Title = "Stack Overflow", Url = "https://stackoverflow.com", IsActive = false },
        };
    }

    private void LoadBookmarks()
    {
        Bookmarks = new ObservableCollection<BookmarkItem>
        {
            new() { Title = "Google", Url = "https://www.google.com", IconColor = "#4285F4" },
            new() { Title = "YouTube", Url = "https://www.youtube.com", IconColor = "#FF0000" },
            new() { Title = "GitHub", Url = "https://github.com", IconColor = "#333333" },
            new() { Title = "Gmail", Url = "https://gmail.com", IconColor = "#EA4335" },
            new() { Title = "百度", Url = "https://www.baidu.com", IconColor = "#2932E1" },
            new() { Title = "Bilibili", Url = "https://www.bilibili.com", IconColor = "#FB7299" },
            new() { Title = "知乎", Url = "https://www.zhihu.com", IconColor = "#0084FF" },
            new() { Title = "淘宝", Url = "https://www.taobao.com", IconColor = "#FF5000" },
        };
    }

    private void LoadHistory()
    {
        History = new ObservableCollection<HistoryItem>
        {
            new() { Title = "Google", Url = "https://www.google.com", Time = "今天 14:30" },
            new() { Title = "GitHub - AndroidPadSimulator", Url = "https://github.com/...", Time = "今天 12:15" },
            new() { Title = "Stack Overflow", Url = "https://stackoverflow.com", Time = "昨天 18:45" },
            new() { Title = "Avalonia UI Documentation", Url = "https://docs.avaloniaui.net", Time = "昨天 10:20" },
            new() { Title = "YouTube", Url = "https://www.youtube.com", Time = "2天前" },
        };
    }

    [RelayCommand]
    private void GoBack()
    {
        MainViewModel?.CloseChromeCommand.Execute(null);
    }

    [RelayCommand]
    private void Navigate()
    {
        if (string.IsNullOrWhiteSpace(Url))
            return;

        IsLoading = true;
        
        // 模拟加载
        System.Threading.Tasks.Task.Run(async () =>
        {
            await System.Threading.Tasks.Task.Delay(1000);
            IsLoading = false;
            
            // 更新页面标题
            PageTitle = Url.Replace("https://", "").Replace("http://", "").Split('/')[0];
            
            // 更新当前标签页
            if (SelectedTabIndex >= 0 && SelectedTabIndex < Tabs.Count)
            {
                Tabs[SelectedTabIndex].Title = PageTitle;
                Tabs[SelectedTabIndex].Url = Url;
            }
            
            // 添加到历史记录
            History.Insert(0, new HistoryItem
            {
                Title = PageTitle,
                Url = Url,
                Time = "刚刚"
            });
        });
    }

    [RelayCommand]
    private void NewTab()
    {
        Tabs.Add(new TabItem { Title = "新标签页", Url = "about:blank", IsActive = false });
        SelectedTabIndex = Tabs.Count - 1;
        Url = "";
        PageTitle = "新标签页";
    }

    [RelayCommand]
    private void CloseTab(TabItem tab)
    {
        Tabs.Remove(tab);
        if (Tabs.Count == 0)
        {
            NewTab();
        }
    }

    [RelayCommand]
    private void SelectTab(TabItem tab)
    {
        for (int i = 0; i < Tabs.Count; i++)
        {
            Tabs[i].IsActive = Tabs[i] == tab;
            if (Tabs[i] == tab)
            {
                SelectedTabIndex = i;
                Url = tab.Url;
                PageTitle = tab.Title;
            }
        }
    }

    [RelayCommand]
    private void NavigateToBookmark(BookmarkItem bookmark)
    {
        Url = bookmark.Url;
        Navigate();
        ShowBookmarks = false;
    }

    [RelayCommand]
    private void ToggleBookmarks()
    {
        ShowBookmarks = !ShowBookmarks;
    }

    [RelayCommand]
    private void GoHome()
    {
        Url = "https://www.google.com";
        Navigate();
    }

    [RelayCommand]
    private void Refresh()
    {
        Navigate();
    }
}

public class TabItem
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class BookmarkItem
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string IconColor { get; set; } = "#4285F4";
}

public class HistoryItem
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
}
