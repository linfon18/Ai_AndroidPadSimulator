using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;
using AndroidPadSimulator.ViewModels;

namespace AndroidPadSimulator.Views;

public partial class LockScreenView : UserControl
{
    private Point _startPoint;
    private bool _isDragging;
    private const double UnlockThreshold = -100; // 向上滑动超过100像素解锁
    private CancellationTokenSource? _idleAnimationCts;

    public LockScreenView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        // 启动空闲动画
        StartIdleAnimations();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        // 停止空闲动画
        _idleAnimationCts?.Cancel();
    }

    private void StartIdleAnimations()
    {
        _idleAnimationCts = new CancellationTokenSource();
        var token = _idleAnimationCts.Token;

        // 启动解锁图标脉冲动画
        _ = AnimateUnlockIconPulse(token);
        
        // 启动时间文本呼吸动画
        _ = AnimateTimeBreathing(token);
    }

    private async Task AnimateUnlockIconPulse(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                if (UnlockIcon != null && !_isDragging)
                {
                    // 脉冲动画 - 放大到1.1倍再缩小回1.0倍
                    for (int i = 0; i <= 20; i++)
                    {
                        if (token.IsCancellationRequested || _isDragging) break;
                        double progress = i / 20.0;
                        double scale = 1 + Math.Sin(progress * Math.PI) * 0.1;
                        UnlockIcon.RenderTransform = new ScaleTransform(scale, scale);
                        await Task.Delay(50, token);
                    }
                }
                // 等待2秒后再次脉冲
                await Task.Delay(2000, token);
            }
        }
        catch (OperationCanceledException)
        {
            // 动画被取消，正常退出
        }
    }

    private async Task AnimateTimeBreathing(CancellationToken token)
    {
        try
        {
            // 查找时间文本控件
            var timeTextBlock = this.FindControl<TextBlock>("TimeTextBlock");
            if (timeTextBlock == null) return;

            while (!token.IsCancellationRequested)
            {
                // 呼吸效果 - 透明度在0.9到1.0之间变化
                for (int i = 0; i <= 40; i++)
                {
                    if (token.IsCancellationRequested) break;
                    double progress = i / 40.0;
                    double opacity = 0.9 + Math.Sin(progress * Math.PI * 2) * 0.1;
                    timeTextBlock.Opacity = opacity;
                    await Task.Delay(50, token);
                }
            }
        }
        catch (OperationCanceledException)
        {
            // 动画被取消，正常退出
        }
    }

    private void OnUnlockAreaPressed(object? sender, PointerPressedEventArgs e)
    {
        _isDragging = true;
        _startPoint = e.GetPosition(this);
        
        // 按下时缩小图标
        if (UnlockIcon != null)
        {
            AnimatePressEffect();
        }
    }

    private void OnUnlockAreaMoved(object? sender, PointerEventArgs e)
    {
        if (!_isDragging) return;

        var currentPoint = e.GetPosition(this);
        var deltaY = currentPoint.Y - _startPoint.Y;

        // 只允许向上滑动
        if (deltaY < 0)
        {
            // 移动图标
            if (UnlockIcon != null)
            {
                var translateY = Math.Max(deltaY, -150);
                UnlockIcon.RenderTransform = new TranslateTransform(0, translateY);
                
                // 根据滑动距离调整透明度
                var progress = Math.Min(Math.Abs(translateY) / 100, 1);
                UnlockIcon.Opacity = 1 - progress * 0.5;
                
                // 同时调整整个底部区域的位置
                var bottomArea = sender as Border;
                if (bottomArea != null)
                {
                    var areaTranslateY = Math.Max(deltaY * 0.3, -50);
                    bottomArea.RenderTransform = new TranslateTransform(0, areaTranslateY);
                }
            }
        }
    }

    private async void OnUnlockAreaReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (!_isDragging) return;
        _isDragging = false;

        var currentPoint = e.GetPosition(this);
        var deltaY = currentPoint.Y - _startPoint.Y;

        if (deltaY < UnlockThreshold)
        {
            // 滑动距离足够，执行解锁动画
            await AnimateUnlock();
            
            // 执行解锁
            if (DataContext is LockScreenViewModel viewModel)
            {
                viewModel.UnlockCommand.Execute(null);
            }
        }
        else
        {
            // 滑动距离不够，恢复原位动画
            await AnimateReset();
        }
    }

    private async void AnimatePressEffect()
    {
        if (UnlockIcon == null) return;

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
            UnlockIcon.RenderTransform = new ScaleTransform(scale, scale);

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        UnlockIcon.RenderTransform = new ScaleTransform(0.9, 0.9);
    }

    private async Task AnimateUnlock()
    {
        if (UnlockIcon == null) return;

        // 使用简化的动画实现
        const int duration = 300;
        const int steps = 30;
        const double stepDuration = duration / (double)steps;

        double startY = 0;
        double startOpacity = UnlockIcon.Opacity;

        for (int i = 0; i <= steps; i++)
        {
            double progress = i / (double)steps;
            // 二次缓动效果
            double easedProgress = progress * progress;
            
            double translateY = startY - (easedProgress * 200);
            double opacity = startOpacity - (easedProgress * startOpacity);
            
            UnlockIcon.RenderTransform = new TranslateTransform(0, translateY);
            UnlockIcon.Opacity = opacity;

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        UnlockIcon.RenderTransform = new TranslateTransform(0, -200);
        UnlockIcon.Opacity = 0;
    }

    private async Task AnimateReset()
    {
        if (UnlockIcon == null) return;

        // 使用简化的动画实现
        const int duration = 200;
        const int steps = 20;
        const double stepDuration = duration / (double)steps;

        double startY = (UnlockIcon.RenderTransform as TranslateTransform)?.Y ?? 0;
        double startOpacity = UnlockIcon.Opacity;

        for (int i = 0; i <= steps; i++)
        {
            double progress = i / (double)steps;
            // 弹跳缓动效果
            double easedProgress;
            if (progress < 0.5)
                easedProgress = 2 * progress * progress;
            else
                easedProgress = 1 - Math.Pow(-2 * progress + 2, 2) / 2;
            
            double translateY = startY - (easedProgress * startY);
            double opacity = startOpacity + (easedProgress * (1 - startOpacity));
            
            UnlockIcon.RenderTransform = new TranslateTransform(0, translateY);
            UnlockIcon.Opacity = opacity;

            await Task.Delay(TimeSpan.FromMilliseconds(stepDuration));
        }

        UnlockIcon.RenderTransform = null;
        UnlockIcon.Opacity = 1;
        
        // 恢复底部区域位置
        if (UnlockArea != null)
        {
            UnlockArea.RenderTransform = null;
        }
    }
}
