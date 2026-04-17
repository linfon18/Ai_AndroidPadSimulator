using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;
using AndroidPadSimulator.ViewModels;

namespace AndroidPadSimulator.Views;

public partial class ControlCenterView : UserControl
{
    private Point _startPoint;
    private bool _isDragging;
    private const double CloseThreshold = 100; // 向下滑动超过100像素关闭

    public ControlCenterView()
    {
        InitializeComponent();
        this.Opacity = 0;
        this.RenderTransform = new TranslateTransform(0, -100);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        AnimateOpen();
    }

    private async void AnimateOpen()
    {
        // 使用简化的动画实现
        const int duration = 300;
        const int steps = 30;
        const double stepDuration = duration / (double)steps;

        for (int i = 0; i <= steps; i++)
        {
            double progress = i / (double)steps;
            // 使用缓动函数
            double easedProgress = progress * progress; // 二次缓动
            
            this.Opacity = easedProgress;
            var translateY = -100 + (easedProgress * 100);
            this.RenderTransform = new TranslateTransform(0, translateY);

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        this.Opacity = 1;
        this.RenderTransform = new TranslateTransform(0, 0);
    }

    public async Task AnimateClose()
    {
        // 使用简化的动画实现
        const int duration = 250;
        const int steps = 25;
        const double stepDuration = duration / (double)steps;

        for (int i = 0; i <= steps; i++)
        {
            double progress = i / (double)steps;
            // 使用缓动函数
            double easedProgress = progress * progress; // 二次缓动
            
            this.Opacity = 1 - easedProgress;
            var translateY = 0 - (easedProgress * 100);
            this.RenderTransform = new TranslateTransform(0, translateY);

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        this.Opacity = 0;
        this.RenderTransform = new TranslateTransform(0, -100);
    }

    private void OnControlCenterPressed(object? sender, PointerPressedEventArgs e)
    {
        _isDragging = true;
        _startPoint = e.GetPosition(this);
    }

    private void OnContentPressed(object? sender, PointerPressedEventArgs e)
    {
        // 内容区域按下，开始拖拽
        _isDragging = true;
        _startPoint = e.GetPosition(this);
    }

    private void OnBackgroundPressed(object? sender, PointerPressedEventArgs e)
    {
        // 背景按下，标记为点击背景
        _isDragging = false;
    }

    private async void OnBackgroundReleased(object? sender, PointerReleasedEventArgs e)
    {
        // 点击背景关闭控制中心
        await AnimateClose();
        CloseControlCenter();
    }

    private void OnControlCenterMoved(object? sender, PointerEventArgs e)
    {
        if (!_isDragging) return;

        var currentPoint = e.GetPosition(this);
        var deltaY = currentPoint.Y - _startPoint.Y;

        // 只允许向下滑动
        if (deltaY > 0)
        {
            // 移动控制中心
            var translateY = Math.Min(deltaY * 0.5, 50);
            this.RenderTransform = new TranslateTransform(0, translateY);
            
            // 根据滑动距离调整透明度
            var progress = Math.Min(translateY / 50, 1);
            this.Opacity = 1 - progress * 0.3;
        }
    }

    private async void OnControlCenterReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (!_isDragging) return;
        _isDragging = false;

        var currentPoint = e.GetPosition(this);
        var deltaY = currentPoint.Y - _startPoint.Y;

        if (deltaY > CloseThreshold)
        {
            // 滑动距离足够，执行关闭动画
            await AnimateClose();
            
            // 执行关闭
            CloseControlCenter();
        }
        else
        {
            // 滑动距离不够，恢复原位动画
            AnimateOpen();
        }
    }

    private void CloseControlCenter()
    {
        // 查找MainWindow并关闭控制中心
        var window = this.GetVisualRoot() as Window;
        if (window?.DataContext is MainWindowViewModel mainViewModel)
        {
            mainViewModel.CloseControlCenterCommand.Execute(null);
        }
    }

    private void OnSettingsClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // 关闭控制中心
        CloseControlCenter();
        
        // 打开设置
        var window = this.GetVisualRoot() as Window;
        if (window?.DataContext is MainWindowViewModel mainViewModel)
        {
            mainViewModel.OpenSettingsCommand.Execute(null);
        }
    }
}
