using Avalonia.Controls;
using Avalonia.Media;
using AndroidPadSimulator.ViewModels;
using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.VisualTree;

namespace AndroidPadSimulator.Views;

public partial class ReconnectingView : UserControl
{
    private double _rotationAngle = 0;
    private System.Timers.Timer? _animationTimer;

    public ReconnectingView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        StartLoadingAnimation();
        
        if (DataContext is ReconnectingViewModel viewModel)
        {
            viewModel.StartReconnecting();
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        StopLoadingAnimation();
    }

    private void StartLoadingAnimation()
    {
        _animationTimer = new System.Timers.Timer(16); // 60fps
        _animationTimer.Elapsed += (s, e) =>
        {
            _rotationAngle += 5;
            if (_rotationAngle >= 360) _rotationAngle = 0;

            // 在主线程更新UI
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                // 查找所有Arc元素并更新
                var arcs = this.GetVisualDescendants();
                foreach (var arc in arcs)
                {
                    if (arc is Arc arcElement)
                    {
                        arcElement.StartAngle = _rotationAngle;
                    }
                }
            });
        };
        _animationTimer.AutoReset = true;
        _animationTimer.Start();
    }

    private void StopLoadingAnimation()
    {
        _animationTimer?.Stop();
        _animationTimer?.Dispose();
    }
}
