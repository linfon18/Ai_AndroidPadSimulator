using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;
using AndroidPadSimulator.ViewModels;

namespace AndroidPadSimulator.Views;

public partial class HomeScreenView : UserControl
{
    private Point _startPoint;
    private bool _isDragging;
    private const double ControlCenterThreshold = 80; // 向下滑动超过80像素打开控制中心
    private CancellationTokenSource? _idleAnimationCts;

    public HomeScreenView()
    {
        InitializeComponent();
        AddTapAnimationsToIcons();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        // 启动进入动画
        _ = AnimatePageEntry();
        // 启动空闲动画
        StartIdleAnimations();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        // 停止空闲动画
        _idleAnimationCts?.Cancel();
    }

    private async Task AnimatePageEntry()
    {
        // 等待控件加载完成
        await Task.Delay(100);

        // 获取所有应用图标
        var icons = new List<Border>();
        if (RootGrid != null)
        {
            foreach (var stackPanel in RootGrid.GetVisualDescendants().OfType<StackPanel>())
            {
                if (stackPanel.Children.Count > 0 && stackPanel.Children[0] is Border icon)
                {
                    icons.Add(icon);
                }
            }
        }

        // Staggered 动画 - 图标依次从下方飞入
        for (int i = 0; i < icons.Count; i++)
        {
            var icon = icons[i];
            icon.Opacity = 0;
            icon.RenderTransform = new TranslateTransform(0, 30);

            // 延迟启动每个图标的动画
            _ = AnimateIconEntry(icon, i * 50);
        }
    }

    private async Task AnimateIconEntry(Border icon, int delay)
    {
        await Task.Delay(delay);

        const int duration = 300;
        const int steps = 20;
        const double stepDuration = duration / (double)steps;

        for (int i = 0; i <= steps; i++)
        {
            double progress = i / (double)steps;
            // 使用 OutQuad 缓动
            double easedProgress = 1 - Math.Pow(1 - progress, 2);

            icon.Opacity = easedProgress;
            double translateY = 30 - (easedProgress * 30);
            icon.RenderTransform = new TranslateTransform(0, translateY);

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        icon.Opacity = 1;
        icon.RenderTransform = null;
    }

    private void StartIdleAnimations()
    {
        _idleAnimationCts = new CancellationTokenSource();
        var token = _idleAnimationCts.Token;

        // 启动小部件悬浮动画
        _ = AnimateWidgetFloating(token);
    }

    private async Task AnimateWidgetFloating(CancellationToken token)
    {
        try
        {
            // 查找天气小部件
            var weatherWidget = this.FindControl<Border>("WeatherWidget");
            if (weatherWidget == null) return;

            while (!token.IsCancellationRequested)
            {
                // 上下浮动效果
                for (int i = 0; i <= 60; i++)
                {
                    if (token.IsCancellationRequested) break;
                    double progress = i / 60.0;
                    double offset = Math.Sin(progress * Math.PI * 2) * 3;
                    weatherWidget.RenderTransform = new TranslateTransform(0, offset);
                    await Task.Delay(50, token);
                }
            }
        }
        catch (OperationCanceledException)
        {
            // 动画被取消，正常退出
        }
    }

    private void AddTapAnimationsToIcons()
    {
        // 为所有应用图标添加点击动画效果
        if (RootGrid != null)
        {
            foreach (var stackPanel in RootGrid.GetVisualDescendants().OfType<StackPanel>())
            {
                if (stackPanel.Children.Count > 0 && stackPanel.Children[0] is Border)
                {
                    stackPanel.PointerPressed += OnAppIconPressed;
                    stackPanel.PointerReleased += OnAppIconReleased;
                    stackPanel.PointerExited += OnAppIconExited;
                }
            }
        }
    }

    private void OnAppIconPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is StackPanel stackPanel && stackPanel.Children.Count > 0 && stackPanel.Children[0] is Border iconBorder)
        {
            AnimateIconPress(iconBorder);
        }
    }

    private void OnAppIconReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (sender is StackPanel stackPanel && stackPanel.Children.Count > 0 && stackPanel.Children[0] is Border iconBorder)
        {
            AnimateIconRelease(iconBorder);
        }
    }

    private void OnAppIconExited(object? sender, PointerEventArgs e)
    {
        if (sender is StackPanel stackPanel && stackPanel.Children.Count > 0 && stackPanel.Children[0] is Border iconBorder)
        {
            AnimateIconRelease(iconBorder);
        }
    }

    private async void AnimateIconPress(Border icon)
    {
        // 使用简化的动画实现
        const int duration = 100;
        const int steps = 10;
        const double stepDuration = duration / (double)steps;

        for (int i = 0; i <= steps; i++)
        {
            double progress = i / (double)steps;
            // 弹性缓动效果
            double easedProgress = 1 - Math.Pow(1 - progress, 2);
            
            double scale = 1 - (easedProgress * 0.1);
            icon.RenderTransform = new ScaleTransform(scale, scale);

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        icon.RenderTransform = new ScaleTransform(0.9, 0.9);
    }

    private async void AnimateIconRelease(Border icon)
    {
        // 使用简化的动画实现
        const int duration = 150;
        const int steps = 15;
        const double stepDuration = duration / (double)steps;

        for (int i = 0; i <= steps; i++)
        {
            double progress = i / (double)steps;
            // 弹跳缓动效果
            double easedProgress;
            if (progress < 0.5)
                easedProgress = 2 * progress * progress;
            else
                easedProgress = 1 - Math.Pow(-2 * progress + 2, 2) / 2;
            
            double scale = 0.9 + (easedProgress * 0.1);
            icon.RenderTransform = new ScaleTransform(scale, scale);

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        icon.RenderTransform = new ScaleTransform(1, 1);
    }

    private void OnStatusBarPressed(object? sender, PointerPressedEventArgs e)
    {
        _isDragging = true;
        _startPoint = e.GetPosition(this);
    }

    private void OnStatusBarMoved(object? sender, PointerEventArgs e)
    {
        if (!_isDragging) return;

        var currentPoint = e.GetPosition(this);
        var deltaY = currentPoint.Y - _startPoint.Y;

        // 只允许向下滑动
        if (deltaY > 0)
        {
            // 可以在这里添加视觉反馈，比如状态栏下拉效果
        }
    }

    private void OnStatusBarReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (!_isDragging) return;
        _isDragging = false;

        var currentPoint = e.GetPosition(this);
        var deltaY = currentPoint.Y - _startPoint.Y;

        if (deltaY > ControlCenterThreshold)
        {
            // 滑动距离足够，打开控制中心
            if (DataContext is HomeScreenViewModel viewModel)
            {
                viewModel.OpenControlCenterCommand.Execute(null);
            }
        }
    }

    private void OnSettingsButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // 打开设置
        if (DataContext is HomeScreenViewModel viewModel)
        {
            viewModel.OpenSettingsCommand.Execute(null);
        }
    }

    // 桌面分页滑动相关
    private Point _pageStartPoint;
    private bool _isPageDragging;
    private int _currentPage = 0;
    private const int TotalPages = 2;
    private const double PageSwipeThreshold = 100; // 滑动超过100像素切换页面

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        SetupPageSwipe();
    }

    private void SetupPageSwipe()
    {
        // 为应用区域添加滑动手势
        if (AppPagesScrollViewer != null)
        {
            AppPagesScrollViewer.PointerPressed += OnPagePointerPressed;
            AppPagesScrollViewer.PointerMoved += OnPagePointerMoved;
            AppPagesScrollViewer.PointerReleased += OnPagePointerReleased;
        }
    }

    private void OnPagePointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _isPageDragging = true;
        _pageStartPoint = e.GetPosition(this);
    }

    private void OnPagePointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_isPageDragging) return;

        var currentPoint = e.GetPosition(this);
        var deltaX = currentPoint.X - _pageStartPoint.X;

        // 可以在这里添加视觉反馈
    }

    private void OnPagePointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (!_isPageDragging) return;
        _isPageDragging = false;

        var currentPoint = e.GetPosition(this);
        var deltaX = currentPoint.X - _pageStartPoint.X;

        if (Math.Abs(deltaX) > PageSwipeThreshold)
        {
            if (deltaX > 0 && _currentPage > 0)
            {
                // 向右滑动，切换到上一页
                _currentPage--;
                AnimateToPage(_currentPage);
            }
            else if (deltaX < 0 && _currentPage < TotalPages - 1)
            {
                // 向左滑动，切换到下一页
                _currentPage++;
                AnimateToPage(_currentPage);
            }
        }
    }

    private async void AnimateToPage(int pageIndex)
    {
        if (AppPagesScrollViewer == null) return;

        var targetOffset = pageIndex * AppPagesScrollViewer.Bounds.Width;

        // 使用动画平滑滚动到目标位置
        const int duration = 300;
        const int steps = 30;
        const double stepDuration = duration / (double)steps;

        var startOffset = AppPagesScrollViewer.Offset.X;
        var distance = targetOffset - startOffset;

        for (int i = 0; i <= steps; i++)
        {
            double progress = i / (double)steps;
            // 使用 OutQuad 缓动
            double easedProgress = 1 - Math.Pow(1 - progress, 2);

            var currentOffset = startOffset + (distance * easedProgress);
            AppPagesScrollViewer.Offset = new Vector(currentOffset, 0);

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        AppPagesScrollViewer.Offset = new Vector(targetOffset, 0);
    }
}
