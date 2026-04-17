using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using AndroidPadSimulator.ViewModels;
using System;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia;

namespace AndroidPadSimulator.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        // 启动进入动画
        _ = AnimatePageEntry();
    }

    private async Task AnimatePageEntry()
    {
        // 等待控件加载完成
        await Task.Delay(50);

        // 页面从右侧滑入
        const int duration = 300;
        const int steps = 20;
        const double stepDuration = duration / (double)steps;

        for (int i = 0; i <= steps; i++)
        {
            double progress = i / (double)steps;
            // 使用 OutQuad 缓动
            double easedProgress = 1 - Math.Pow(1 - progress, 2);

            this.Opacity = easedProgress;
            double translateX = 50 - (easedProgress * 50);
            this.RenderTransform = new TranslateTransform(translateX, 0);

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        this.Opacity = 1;
        this.RenderTransform = null;
    }

    private void OnSystemUpdateClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // 关闭设置界面并打开系统更新
        if (DataContext is SettingsViewModel viewModel)
        {
            viewModel.MainViewModel?.CloseSettingsCommand.Execute(null);
            viewModel.MainViewModel?.OpenSoftwareUpdateCommand.Execute(null);
        }
    }
}
